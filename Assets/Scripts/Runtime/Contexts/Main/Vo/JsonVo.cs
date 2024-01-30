using System;
using System.Collections.Generic;

namespace Runtime.Contexts.Main.Vo
{
  [Serializable]
  public class JsonVo
  {
    public Dictionary<string, BookVo> bookVos;

    public Dictionary<string, List<string>> authorsBooks;
    
    public Dictionary<string, string> bookIsbn;
    
    public Dictionary<string, BookVo> borrowedBooks;
    
    public Dictionary<string, BookVo> expiredBooks;
  }
}