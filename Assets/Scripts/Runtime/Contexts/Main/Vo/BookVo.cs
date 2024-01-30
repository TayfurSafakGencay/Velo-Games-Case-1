using System;

namespace Runtime.Contexts.Main.Vo
{
  [Serializable]
  public class BookVo
  {
    public string title;

    public string author;

    public int ISBN;

    public int copy;

    public int borrowedBookCount;

    public string borrowCode;
    
    public DateTime borrowedDate;
    
    public DateTime endBorrowDate;

    public bool expired;
  }
}