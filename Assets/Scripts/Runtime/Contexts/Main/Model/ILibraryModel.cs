using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;

namespace Runtime.Contexts.Main.Model
{
  public interface ILibraryModel
  {
    void AddBook(BookVo bookVo);

    BookVo BorrowBook(int isbn);

    void ReturnBook(string borrowCode);

    void Expired();

    void Save(string path);

    void Load(string path);

    Dictionary<int, BookVo> GetBookList();

    Dictionary<string, int> GetBookISBNList();

    Dictionary<string, List<int>> GetAuthorBooks();

    Dictionary<string, BookVo> GetBorrowedBooks();

    Dictionary<string, BookVo> GetExpiredBooks();
  }
}