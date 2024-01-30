using System;
using System.Collections.Generic;

namespace Runtime.Contexts.Main.Vo
{
  [Serializable]
  public class JsonVo
  {
    public Dictionary<int, BookVo> bookVos;

    public Dictionary<string, List<int>> authorsBooks;
    
    public Dictionary<string, int> bookIsbn;
    
    public Dictionary<string, BookVo> borrowedBooks;
    
    public Dictionary<string, BookVo> expiredBooks;
  }
}