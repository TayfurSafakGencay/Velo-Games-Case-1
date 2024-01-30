using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine.UI;

namespace Runtime.Contexts.Main.View.BookListPanel.Item.Book
{
  public class BookListPanelItemView : EventView
  {
    public TextMeshProUGUI listNumberText;
    
    public TextMeshProUGUI isbnText;
    
    public TextMeshProUGUI bookNameText;
    
    public TextMeshProUGUI authorNameText;

    public TextMeshProUGUI copyText;

    public TextMeshProUGUI borrowedCountText;
    
    public Button borrowButton;

    private BookVo bookVo;
    
    private int itemId;

    public void Init(int id)
    {
      itemId = id;
    }

    public void OnBorrow()
    {
      dispatcher.Dispatch(BookListItemEvent.OnBorrowBook);
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