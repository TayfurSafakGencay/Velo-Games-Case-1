using System.Collections.Generic;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

namespace Runtime.Contexts.Main.View.BookListPanel
{
  public class BookListPanelView : EventView
  {
    public GameObject bookListItem;

    public GameObject borrowedBookListItem;

    public Transform itemContainer;
    
    public TextMeshProUGUI currentPageText;

    public TMP_InputField searchInputField;

    public TextMeshProUGUI firstInfoText;

    public TextMeshProUGUI secondInfoText;

    [HideInInspector]
    public int page = 1;

    [HideInInspector]
    public int totalPage;

    [HideInInspector]
    public int itemInAPage = 17;

    [HideInInspector]
    public int panelMode;

    public List<BookVo> displayBookVos = new();

    public void OnClosePanel()
    {
      dispatcher.Dispatch(BookListPanelEvent.ClosePanel);
    }

    public void OnShowAllBooks()
    {
      dispatcher.Dispatch(BookListPanelEvent.ShowAllBooks);
    }

    public void OnShowBooksWithNameFilter()
    {
      dispatcher.Dispatch(BookListPanelEvent.ShowBooksWithNameFilter);
    }

    public void OnShowBooksWithAuthorFilter()
    {
      dispatcher.Dispatch(BookListPanelEvent.ShowBooksWithAuthorFilter);
    }

    public void OnShowBorrowedBooks()
    {
      dispatcher.Dispatch(BookListPanelEvent.ShowBorrowedBooks);
    }

    public void OnShowExpiredBooks()
    {
      dispatcher.Dispatch(BookListPanelEvent.ShowExpiredBooks);
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