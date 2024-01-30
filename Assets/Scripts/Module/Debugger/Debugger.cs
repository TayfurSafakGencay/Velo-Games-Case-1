using Runtime.Contexts.Main.Vo;
using UnityEngine;

namespace Module.Debugger
{
  public class Debugger
  {
    public static void AddedNewBook(BookVo bookVo)
    {
      string greenColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.green)}";
      string whiteColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.white)}";
        
      Debug.Log($"<color={greenColor}>New book successfully added. </color>" + 
                $"<color={greenColor}>Book Name: </color>" + $"<color={whiteColor}>" + bookVo.title + "</color>, " +
                $"<color={greenColor}>Author: </color>" + $"<color={whiteColor}>" + bookVo.author + "</color>, " +
                $"<color={greenColor}>ISBN: </color>" + $"<color={whiteColor}>" + bookVo.ISBN + "</color>, " +
                $"<color={greenColor}>Copy Count: </color>" + $"<color={whiteColor}>" + bookVo.copy + "</color>" + ".");
    }

    public static void AddedCopy(BookVo bookVo)
    {
      string yellowColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.yellow)}";
      string whiteColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.white)}";
      
      Debug.Log($"<color={yellowColor}>{bookVo.copy} books added. </color>" + 
                $"<color={yellowColor}>Book Name: </color>" + $"<color={whiteColor}>" + bookVo.title + "</color>, " +
                $"<color={yellowColor}>Author: </color>" + $"<color={whiteColor}>" + bookVo.author + "</color>, " +
                $"<color={yellowColor}>ISBN: </color>" + $"<color={whiteColor}>" + bookVo.ISBN + "</color>, " +
                $"<color={yellowColor}>Copy Count: </color>" + $"<color={whiteColor}>" + bookVo.copy + "</color>" + ".");
    }
    
    public static void BorrowedBook(BookVo bookVo)
    {
      string greenColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.green)}";
      string whiteColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.white)}";
      
      Debug.Log($"<color={greenColor}>The book borrowed. </color>" + 
                $"<color={greenColor}>Book Name: </color>" + $"<color={whiteColor}>" + bookVo.title + "</color>, " +
                $"<color={greenColor}>Author: </color>" + $"<color={whiteColor}>" + bookVo.author + "</color>, " +
                $"<color={greenColor}>ISBN: </color>" + $"<color={whiteColor}>" + bookVo.ISBN + "</color>, " +
                $"<color={greenColor}>Copy Count: </color>" + $"<color={whiteColor}>" + bookVo.copy + "</color>, " +
                $"<color={greenColor}>Borrowed Count: </color>" + $"<color={whiteColor}>" + bookVo.borrowedBookCount + "</color>, " + 
                $"<color={greenColor}>Borrowed Date: </color>" + $"<color={whiteColor}>" + bookVo.borrowedDate + "</color>, " +
                $"<color={greenColor}>Deadline: </color>" + $"<color={whiteColor}>" + bookVo.endBorrowDate + "</color>, " +
                $"<color={greenColor}>Borrow Code: </color>" + $"<color={whiteColor}>" + bookVo.borrowCode + "</color>.");
    }
    
    public static void ReturnedBook(BookVo bookVo)
    {
      string greenColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.green)}";
      string whiteColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.white)}";
      
      Debug.Log($"<color={greenColor}>The book returned. </color>" + 
                $"<color={greenColor}>Book Name: </color>" + $"<color={whiteColor}>" + bookVo.title + "</color>, " +
                $"<color={greenColor}>Author: </color>" + $"<color={whiteColor}>" + bookVo.author + "</color>, " +
                $"<color={greenColor}>ISBN: </color>" + $"<color={whiteColor}>" + bookVo.ISBN + "</color>, " +
                $"<color={greenColor}>Copy Count: </color>" + $"<color={whiteColor}>" + bookVo.copy + "</color>, " +
                $"<color={greenColor}>Borrowed Count: </color>" + $"<color={whiteColor}>" + bookVo.borrowedBookCount + "</color>, " + 
                $"<color={greenColor}>Borrowed Date: </color>" + $"<color={whiteColor}>" + bookVo.borrowedDate + "</color>, " +
                $"<color={greenColor}>Deadline: </color>" + $"<color={whiteColor}>" + bookVo.endBorrowDate + "</color>, " +
                $"<color={greenColor}>Borrow Code: </color>" + $"<color={whiteColor}>" + bookVo.borrowCode + "</color>, " +
                $"<color={greenColor}>Expired: </color>" + $"<color={whiteColor}>" + bookVo.expired + "</color>.");
    }

    public static void IsbnError()
    {
      string red = $"#{ColorUtility.ToHtmlStringRGBA(Color.red)}";
      
      Debug.LogError($"<color={red}>This ISBN belongs to another author and book!</color>");
    }
    
    public static void TitleError()
    {
      string red = $"#{ColorUtility.ToHtmlStringRGBA(Color.red)}";
      
      Debug.LogError($"<color={red}>This title belongs to another author and book!</color>");
    }
    
    public static void EmptyError()
    {
      string red = $"#{ColorUtility.ToHtmlStringRGBA(Color.red)}";
      
      Debug.LogError($"<color={red}>Fields cannot be left blank!</color>");
    }
    
    public static void JsonSave()
    {
      string green = $"#{ColorUtility.ToHtmlStringRGBA(Color.green)}";
      
      Debug.Log($"<color={green}>Data saved!</color>");
    }
    
    public static void JsonLoad()
    {
      string green = $"#{ColorUtility.ToHtmlStringRGBA(Color.green)}";
      
      Debug.Log($"<color={green}>Data loaded!</color>");
    }
    
    public static void InvalidJsonPath()
    {
      string red = $"#{ColorUtility.ToHtmlStringRGBA(Color.red)}";
      
      Debug.LogError($"<color={red}>Invalid Json Path!</color>");
    }
  }
}