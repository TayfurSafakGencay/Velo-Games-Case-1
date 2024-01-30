using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Contexts.Main.View.BookListPanel
{
  public class BookListView : EventView
  {
    public GameObject bookListItem;

    public Transform itemContainer;
    
    public TextMeshProUGUI currentPageText;

    [HideInInspector]
    public int page = 1;

    [HideInInspector]
    public int totalPage;

    [HideInInspector]
    public BookVo bookVo;
    
    public void OnClosePanel()
    {
      dispatcher.Dispatch(BookListPanelEvent.ClosePanel);
    }

    public void OnNextPage()
    {
      dispatcher.Dispatch(BookListPanelEvent.NextPage);
    }

    public void OnPreviousPage()
    {
      dispatcher.Dispatch(BookListPanelEvent.PreviousPage);
    }
  }
}