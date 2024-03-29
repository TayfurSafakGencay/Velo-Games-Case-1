﻿using System.Collections.Generic;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.Vo;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.BookListPanel.Item.Book
{
  public enum BookListItemEvent
  {
    OnBorrowBook
  }

  public class BookListPanelItemMediator : EventMediator
  {
    [Inject]
    public BookListPanelItemView view { get; set; }

    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(BookListItemEvent.OnBorrowBook, OnBorrowBook);

      dispatcher.AddListener(MainEvent.UpdateBookListPanelItem, OnUpdateItem);
      dispatcher.AddListener(MainEvent.HideBookListPanelItem, OnHideItem);
      dispatcher.AddListener(MainEvent.ShowBookListPanelItem, OnShowItem);
      dispatcher.AddListener(MainEvent.SetCategoryTitles, OnSetCategoryTitles);
      dispatcher.AddListener(MainEvent.DestroyItems, OnDestroyItem);
    }

    private void OnSetCategoryTitles()
    {
      if (view.GetID() != -1)
        return;

      view.borrowButton.interactable = false;
      gameObject.SetActive(true);

      view.listNumberText.text = "";
      view.bookNameText.text = "<b>Title</b>";
      view.isbnText.text = "<b>ISBN</b>";
      view.authorNameText.text = "<b>Author</b>";
      view.copyText.text = "<b>Copies</b>";
      view.borrowedCountText.text = "<b>Borrowed Count</b>";
    }
    
    private void OnUpdateItem(IEvent payload)
    {
      KeyValuePair<int, BookVo> vo = (KeyValuePair<int, BookVo>)payload.data;

      if (view.GetID() != vo.Key)
        return;

      view.SetBookVo(vo.Value);

      SetItemContent();
    }

    private void SetItemContent()
    {
      view.borrowButton.interactable = view.GetBookVo().borrowedBookCount < view.GetBookVo().copy;
      view.borrowedCountText.color = view.GetBookVo().borrowedBookCount < view.GetBookVo().copy ? Color.white : Color.red;
      
      view.listNumberText.text = view.GetID() + 1 + ".";
      view.bookNameText.text = view.GetBookVo().title;
      view.authorNameText.text = view.GetBookVo().author;
      view.isbnText.text = view.GetBookVo().ISBN;
      view.copyText.text = view.GetBookVo().copy.ToString();
      view.borrowedCountText.text = view.GetBookVo().borrowedBookCount.ToString();
    }
    
    private void OnBorrowBook()
    {
      BookVo book = libraryModel.BorrowBook(view.GetBookVo().ISBN);

      view.SetBookVo(book);

      SetItemContent();
    }

    private void OnHideItem(IEvent payload)
    {
      int id = (int)payload.data;

      if (view.GetID() != id)
        return;

      gameObject.SetActive(false);
      view.SetBookVo(null);
    }

    private void OnShowItem(IEvent payload)
    {
      int id = (int)payload.data;

      if (view.GetID() != id)
        return;

      gameObject.SetActive(true);
    }
    
    private void OnDestroyItem()
    {
      Destroy(gameObject);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(BookListItemEvent.OnBorrowBook, OnBorrowBook);

      dispatcher.RemoveListener(MainEvent.UpdateBookListPanelItem, OnUpdateItem);
      dispatcher.RemoveListener(MainEvent.HideBookListPanelItem, OnHideItem);
      dispatcher.RemoveListener(MainEvent.ShowBookListPanelItem, OnShowItem);
      dispatcher.RemoveListener(MainEvent.SetCategoryTitles, OnSetCategoryTitles);
      dispatcher.RemoveListener(MainEvent.DestroyItems, OnDestroyItem);
    }
  }
}