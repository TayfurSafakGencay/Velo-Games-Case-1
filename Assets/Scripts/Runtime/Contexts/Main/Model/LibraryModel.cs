using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Module.Debugger;
using Runtime.Contexts.Main.Vo;
using Unity.Serialization.Json;
using UnityEngine;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Runtime.Contexts.Main.Model
{
  public class LibraryModel : ILibraryModel
  {
    private Dictionary<string, BookVo> books { get; set; }

    private Dictionary<string, List<string>> authorsBooks { get; set; }

    private Dictionary<string, string> bookIsbn { get; set; }

    private Dictionary<string, BookVo> borrowedBooks { get; set; }
    
    private Dictionary<string, BookVo> expiredBooks { get; set; }

    [PostConstruct]
    public void OnPostConstruct()
    {
      books = new Dictionary<string, BookVo>();
      authorsBooks = new Dictionary<string, List<string>>();
      bookIsbn = new Dictionary<string, string>();
      borrowedBooks = new Dictionary<string, BookVo>();
      expiredBooks = new Dictionary<string, BookVo>();
    }

    public void AddBook(BookVo bookVo)
    {
      if (books.ContainsKey(bookVo.ISBN))
      {
        BookVo book = books[bookVo.ISBN];
        if (book.title == bookVo.title && book.author == bookVo.author)
        {
          books[bookVo.ISBN].copy += bookVo.copy;
          Debugger.AddedCopy(bookVo);
        }
        else
        {
          Debugger.IsbnError();
        }
      }
      else
      {
        if (!bookIsbn.ContainsKey(bookVo.title))
        {
          books.Add(bookVo.ISBN, bookVo);
          bookIsbn.Add(bookVo.title, bookVo.ISBN);
        }
        else
        {
          Debugger.TitleError();
          return;
        }

        Debugger.AddedNewBook(bookVo);

        if (authorsBooks.ContainsKey(bookVo.author))
        {
          if (!authorsBooks[bookVo.author].Contains(bookVo.ISBN))
          {
            authorsBooks[bookVo.author].Add(bookVo.ISBN);
          }
        }
        else
        {
          authorsBooks.Add(bookVo.author, new List<string> { bookVo.ISBN });
        }
      }
    }

    public BookVo BorrowBook(string isbn)
    {
      BookVo book = new()
      {
        title = books[isbn].title,
        author = books[isbn].author,
        ISBN = books[isbn].ISBN,
        copy = books[isbn].copy,
        borrowedBookCount = books[isbn].borrowedBookCount,
      };

      if (book.borrowedBookCount >= book.copy)
        return null;

      book.borrowedDate = DateTime.Now;
      book.endBorrowDate = RandomEndBorrowDate(book.borrowedDate);
      book.borrowCode = CreateRandomBorrowCode(8);
      book.borrowedBookCount++;

      books[isbn].borrowedBookCount++;

      borrowedBooks.Add(book.borrowCode, book);

      Debugger.BorrowedBook(book);
      return book;
    }

    private DateTime RandomEndBorrowDate(DateTime borrowedDate)
    {
      int addRandomTime = UnityRandom.Range(0, 101);
      int second = 0, minute = 0, hour = 0, day = 0;

      if (addRandomTime <= 40)
      {
        second = UnityRandom.Range(0, 61);
      }
      else if (addRandomTime <= 70)
      {
        second = UnityRandom.Range(0, 61);
        minute = UnityRandom.Range(0, 61);
      }
      else if (addRandomTime <= 90)
      {
        second = UnityRandom.Range(0, 61);
        minute = UnityRandom.Range(0, 61);
        hour = UnityRandom.Range(0, 25);
      }
      else if (addRandomTime <= 100)
      {
        second = UnityRandom.Range(0, 61);
        minute = UnityRandom.Range(0, 61);
        hour = UnityRandom.Range(0, 25);
        day = UnityRandom.Range(0, 7);
      }

      DateTime deadline = borrowedDate;
      deadline = deadline.AddSeconds(second);
      deadline = deadline.AddMinutes(minute);
      deadline = deadline.AddHours(hour);
      deadline = deadline.AddDays(day);

      return deadline;
    }

    public void ReturnBook(string borrowCode)
    {
      if (borrowedBooks.ContainsKey(borrowCode))
      {
        string isbn = borrowedBooks[borrowCode].ISBN;
        books[isbn].borrowedBookCount--;

        Debugger.ReturnedBook(borrowedBooks[borrowCode]);
        borrowedBooks.Remove(borrowCode);
      }
      else if (expiredBooks.ContainsKey(borrowCode))
      {
        string isbn = expiredBooks[borrowCode].ISBN;
        books[isbn].borrowedBookCount--;
        
        Debugger.ReturnedBook(expiredBooks[borrowCode]);
        expiredBooks.Remove(borrowCode);
      }
    }

    public void Expired()
    {
      for (int i = 0; i < borrowedBooks.Count; i++)
      {
        BookVo book = borrowedBooks.ElementAt(i).Value;

        if (DateTime.Now <= book.endBorrowDate) continue;
        
        book.expired = true;
        expiredBooks.Add(book.borrowCode, book);
        borrowedBooks.Remove(book.borrowCode);
      }
    }

    private string CreateRandomBorrowCode(int characterCount)
    {
      SystemRandom random = new();
      StringBuilder stringBuilder = new();
      string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

      for (int i = 0; i < characterCount; i++)
      {
        stringBuilder.Append(characters[random.Next(characters.Length)]);
      }

      string randomString = stringBuilder.ToString();

      if (borrowedBooks.ContainsKey(randomString))
      {
        return CreateRandomBorrowCode(16);
      }

      return randomString;
    }

    public void Save(string path)
    {
      JsonVo jsonVo = new()
      {
        bookVos = books,
        authorsBooks = authorsBooks,
        bookIsbn = bookIsbn,
        borrowedBooks = borrowedBooks,
        expiredBooks = expiredBooks
      };

      string jsonString = JsonSerialization.ToJson(jsonVo);
      File.WriteAllText(path, jsonString);

      Debugger.JsonSave();
    }

    public void Load(string path)
    {
      if (!File.Exists(path))
      {
        Save(Application.dataPath + "/Scripts/Runtime/Contexts/Main/Data/Data.json");
        return;
      }

      string jsonLoadData = File.ReadAllText(path);

      JsonVo data = JsonSerialization.FromJson<JsonVo>(jsonLoadData);

      if (data.bookVos != null)
        books = data.bookVos;

      if (data.authorsBooks != null)
        authorsBooks = data.authorsBooks;
      
      if (data.bookIsbn != null)
        bookIsbn = data.bookIsbn;
      
      if (data.borrowedBooks != null)
        borrowedBooks = data.borrowedBooks;
      
      if (data.expiredBooks != null)
        expiredBooks = data.expiredBooks;

      Debugger.JsonLoad();
    }
    
    public Dictionary<string, BookVo> GetBookList()
    {
      return books;
    }

    public Dictionary<string, string> GetBookISBNList()
    {
      return bookIsbn;
    }

    public Dictionary<string, List<string>> GetAuthorBooks()
    {
      return authorsBooks;
    }

    public Dictionary<string, BookVo> GetBorrowedBooks()
    {
      return borrowedBooks;
    }
    
    public Dictionary<string, BookVo> GetExpiredBooks()
    {
      return expiredBooks;
    }
  }
}