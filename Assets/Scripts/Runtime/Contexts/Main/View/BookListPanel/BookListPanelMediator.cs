using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.BookListPanel.Item;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.BookListPanel
{
  public enum BookListPanelEvent
  {
    ClosePanel,
    NextPage,
    PreviousPage
  }
  public class BookListMediator : EventMediator
  {
    [Inject]
    public BookListView view { get; set; }
    
    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(BookListPanelEvent.ClosePanel, OnClosePanel);
      view.dispatcher.AddListener(BookListPanelEvent.PreviousPage, OnPreviousPage);
      view.dispatcher.AddListener(BookListPanelEvent.NextPage, OnNextPage);

      Init();
    }

    private void Init()
    {
      Dictionary<int, BookVo> bookList = libraryModel.GetBookList();
      
      double pageCount = Math.Ceiling((double) bookList.Count / 5);
      view.totalPage = (int)pageCount;
      
      if (view.totalPage == 0)
        view.totalPage = 1;
      
      view.page = 1;
      
      for (int i = 0; i < 5; i++)
      {
        if (bookList.Count <= i)
          break;
        
        GameObject item = Instantiate(view.bookListItem, view.itemContainer);
        BookListItemView itemView = item.GetComponent<BookListItemView>();
        itemView.Init(i);
      }

      UpdatePanel();
    }

    private void UpdatePanel()
    {
      dispatcher.Dispatch(MainEvent.ShowBookListItem);

      view.currentPageText.text = view.page + " / " + view.totalPage;
      
      for (int i = 0; i < 5; i++)
      {
        int value = (view.page - 1) * 5 + i;

        if (libraryModel.GetBookList().Count <= value)
        {
          dispatcher.Dispatch(MainEvent.HideBookListItem);
        }
        
        BookVo vo = libraryModel.GetBookList().ElementAt(value).Value;
        KeyValuePair<int, BookVo> item = new(i, vo);
        
        dispatcher.Dispatch(MainEvent.UpdateBookListPanelItem, item);
      }
    }

    private void OnPreviousPage()
    {
      view.page--;

      if (view.page == 0)
        view.page = view.totalPage;
      
      UpdatePanel();
    }

    private void OnNextPage()
    {
      view.page++;

      if (view.page == view.totalPage + 1)
        view.page = 1;
      
      UpdatePanel();
    }

    private void OnClosePanel()
    {
      Destroy(gameObject);
    }
    
    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(BookListPanelEvent.ClosePanel, OnClosePanel);
      view.dispatcher.RemoveListener(BookListPanelEvent.PreviousPage, OnPreviousPage);
      view.dispatcher.RemoveListener(BookListPanelEvent.NextPage, OnNextPage);
    }
  }
}