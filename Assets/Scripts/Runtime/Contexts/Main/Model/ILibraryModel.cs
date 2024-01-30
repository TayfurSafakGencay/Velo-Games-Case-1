using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;

namespace Runtime.Contexts.Main.Model
{
  public interface ILibraryModel
  {
    void AddBook(BookVo bookVo);

    BookVo BorrowBook(string isbn);

    void ReturnBook(string borrowCode);

    void Expired();

    void Save(string path);

    void Load(string path);

    Dictionary<string, BookVo> GetBookList();

    Dictionary<string, string> GetBookISBNList();

    Dictionary<string, List<string>> GetAuthorBooks();

    Dictionary<string, BookVo> GetBorrowedBooks();

    Dictionary<string, BookVo> GetExpiredBooks();
  }
}