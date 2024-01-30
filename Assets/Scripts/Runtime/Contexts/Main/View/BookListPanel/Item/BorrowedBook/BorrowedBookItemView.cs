using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Contexts.Main.View.BookListPanel.Item.BorrowedBook
{
  public class BorrowedBookItemView : EventView
  {
    public TextMeshProUGUI listNumberText;
    
    public TextMeshProUGUI isbnText;
    
    public TextMeshProUGUI bookNameText;

    public TextMeshProUGUI borrowedDateText;

    public TextMeshProUGUI endBorrowedDateText;

    public TextMeshProUGUI borrowCodeText;

    public Button returnBookButton;

    private BookVo bookVo;
    
    private int itemId;
    
    public void Init(int id)
    {
      itemId = id;
    }

    public void OnReturnBook()
    {
      dispatcher.Dispatch(BorrowedBookItemEvent.ReturnBook);
    }

    public BookVo GetBookVo()
    {
      return bookVo;
    }
    
    public void SetBookVo(BookVo vo)
    {
      bookVo = vo;
    }
    
    public int GetID()
    {
      return itemId;
    }
  }
}