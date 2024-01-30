using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.BookListPanel.Item.Book;
using Runtime.Contexts.Main.View.BookListPanel.Item.BorrowedBook;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.BookListPanel
{
  public enum BookListPanelEvent
  {
    ClosePanel,

    NextPage,
    PreviousPage,

    ShowAllBooks,
    ShowBooksWithNameFilter,
    ShowBooksWithAuthorFilter,
    ShowBorrowedBooks,
    ShowExpiredBooks
  }

  public class BookListPanelMediator : EventMediator
  {
    [Inject]
    public BookListPanelView view { get; set; }

    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(BookListPanelEvent.ClosePanel, OnClosePanel);
      view.dispatcher.AddListener(BookListPanelEvent.PreviousPage, OnPreviousPage);
      view.dispatcher.AddListener(BookListPanelEvent.NextPage, OnNextPage);

      view.dispatcher.AddListener(BookListPanelEvent.ShowAllBooks, OnShowAllBooks);
      view.dispatcher.AddListener(BookListPanelEvent.ShowBooksWithNameFilter, OnShowBooksWithNameFilter);
      view.dispatcher.AddListener(BookListPanelEvent.ShowBooksWithAuthorFilter, OnShowBooksWithAuthorFilter);
      view.dispatcher.AddListener(BookListPanelEvent.ShowBorrowedBooks, OnShowBorrowedBooks);
      view.dispatcher.AddListener(BookListPanelEvent.ShowExpiredBooks, OnShowExpiredBooks);

      Init();
    }

    private void Init()
    {
      view.page = 1;

      view.firstInfoText.text = "...";
      view.secondInfoText.text = "...";
    }

    private void UpdatePanel()
    {
      double pageCount = Math.Ceiling((double)view.displayBookVos.Count / view.itemInAPage);
      view.totalPage = (int)pageCount;

      if (view.totalPage == 0)
        view.totalPage = 1;

      view.currentPageText.text = view.page + " / " + view.totalPage;

      for (int i = 0; i < view.itemInAPage; i++)
      {
        int value = (view.page - 1) * view.itemInAPage + i;

        if (view.displayBookVos.Count <= value)
        {
          dispatcher.Dispatch(MainEvent.HideBookListPanelItem, i);
          continue;
        }

        BookVo vo = view.displayBookVos.ElementAt(value);
        KeyValuePair<int, BookVo> item = new(i, vo);

        dispatcher.Dispatch(MainEvent.ShowBookListPanelItem, i);
        dispatcher.Dispatch(MainEvent.UpdateBookListPanelItem, item);
      }
    }

    private void OnShowAllBooks()
    {
      view.displayBookVos.Clear();
      view.displayBookVos = libraryModel.GetBookList().Values.ToList();

      view.page = 1;
      CreateItems(1);
      SetInfoTexts(1);

      UpdatePanel();
    }

    private void OnShowBooksWithNameFilter()
    {
      view.displayBookVos.Clear();

      Dictionary<string, int> bookList = libraryModel.GetBookISBNList();
      List<string> filteredList = bookList.Keys.ToList().Where(title => title.Contains(view.searchInputField.text, StringComparison.OrdinalIgnoreCase)).ToList();

      for (int i = 0; i < filteredList.Count; i++)
      {
        int isbn = bookList[filteredList.ElementAt(i)];
        if (libraryModel.GetBookList().ContainsKey(isbn))
          view.displayBookVos.Add(libraryModel.GetBookList()[isbn]);
      }

      view.page = 1;
      CreateItems(1);
      SetInfoTexts(2);

      UpdatePanel();
    }

    private void OnShowBooksWithAuthorFilter()
    {
      view.displayBookVos.Clear();

      Dictionary<string, List<int>> authorList = libraryModel.GetAuthorBooks();
      List<string> filteredList = authorList.Keys.ToList().Where(author => author.Contains(view.searchInputField.text, StringComparison.OrdinalIgnoreCase)).ToList();

      for (int i = 0; i < filteredList.Count; i++)
      {
        List<int> isbnList = authorList[filteredList.ElementAt(i)];

        for (int j = 0; j < isbnList.Count; j++)
        {
          if (!libraryModel.GetBookList().ContainsKey(isbnList.ElementAt(j))) continue;
          BookVo bookVo = libraryModel.GetBookList()[isbnList.ElementAt(j)];
          view.displayBookVos.Add(bookVo);
        }
      }

      view.page = 1;
      CreateItems(1);
      SetInfoTexts(3);

      UpdatePanel();
    }

    private void OnShowBorrowedBooks()
    {
      view.displayBookVos.Clear();
      
      libraryModel.Expired();

      Dictionary<string, BookVo> borrowedBooks = libraryModel.GetBorrowedBooks();
      List<string> filteredList = borrowedBooks.Keys.ToList().Where(code => code.Contains(view.searchInputField.text, StringComparison.OrdinalIgnoreCase)).ToList();

      for (int i = 0; i < filteredList.Count; i++)
      {
        if (!borrowedBooks.ContainsKey(filteredList[i])) continue;
        
        BookVo book = borrowedBooks[filteredList[i]];
        view.displayBookVos.Add(book);
      }

      view.page = 1;
      CreateItems(2);
      SetInfoTexts(4);

      UpdatePanel();
    }

    private void OnShowExpiredBooks()
    {
      view.displayBookVos.Clear();
      
      libraryModel.Expired();

      Dictionary<string, BookVo> expiredBooks = libraryModel.GetExpiredBooks();
      List<string> filteredList = expiredBooks.Keys.ToList().Where(code => code.Contains(view.searchInputField.text, StringComparison.OrdinalIgnoreCase)).ToList();
      
      for (int i = 0; i < filteredList.Count; i++)
      {
        if (!expiredBooks.ContainsKey(filteredList[i])) continue;
        
        BookVo book = expiredBooks[filteredList[i]];
        view.displayBookVos.Add(book);
      }

      view.page = 1;
      CreateItems(2);
      SetInfoTexts(5);

      UpdatePanel();
    }

    private void CreateItems(int mode)
    {
      if (mode == view.panelMode)
        return;

      dispatcher.Dispatch(MainEvent.DestroyItems);
      view.panelMode = mode;

      if (view.panelMode == 1)
      {
        for (int i = -1; i < view.itemInAPage; i++)
        {
          GameObject item = Instantiate(view.bookListItem, view.itemContainer);
          BookListPanelItemView panelItemView = item.GetComponent<BookListPanelItemView>();
          panelItemView.Init(i);
        }
      }
      else if (view.panelMode == 2)
      {
        for (int i = -1; i < view.itemInAPage; i++)
        {
          GameObject item = Instantiate(view.borrowedBookListItem, view.itemContainer);
          BorrowedBookItemView panelItemView = item.GetComponent<BorrowedBookItemView>();
          panelItemView.Init(i);
        }
      }

      dispatcher.Dispatch(MainEvent.SetCategoryTitles);
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

    private void SetInfoTexts(int mode)
    {
      string redColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.red)}";
      string greenColor = $"#{ColorUtility.ToHtmlStringRGBA(Color.green)}";
      
      switch (mode)
      {
        case 1:
          view.firstInfoText.text = $"Filter is <color={redColor}>Disabled!</color>";
          view.secondInfoText.text = $"Click on the book to <color={greenColor}>Borrow</color>";
          break;
        case 2:
        case 3:
          view.firstInfoText.text = $"Filter is <color={greenColor}>Active!</color>";
          view.secondInfoText.text = $"Click on the book to <color={greenColor}>Borrow</color>";
          break;
        case 4:
          view.firstInfoText.text = $"Filter is <color={greenColor}>Active!</color>";
          view.secondInfoText.text = $"Click on the book to <color={greenColor}>Return</color>";
          break;
        case 5:
          view.firstInfoText.text = $"Filter is <color={greenColor}>Active!</color>";
          view.secondInfoText.text = $"Click on the expired book to <color={redColor}>Return</color>";
          break;
      }
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

      view.dispatcher.RemoveListener(BookListPanelEvent.ShowAllBooks, OnShowAllBooks);
      view.dispatcher.RemoveListener(BookListPanelEvent.ShowBooksWithNameFilter, OnShowBooksWithNameFilter);
      view.dispatcher.RemoveListener(BookListPanelEvent.ShowBooksWithAuthorFilter, OnShowBooksWithAuthorFilter);
      view.dispatcher.RemoveListener(BookListPanelEvent.ShowBorrowedBooks, OnShowBorrowedBooks);
      view.dispatcher.RemoveListener(BookListPanelEvent.ShowExpiredBooks, OnShowExpiredBooks);
    }
  }
}