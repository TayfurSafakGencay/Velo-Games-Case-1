using System.Collections.Generic;
using System.Globalization;
using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.Vo;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.BookListPanel.Item.BorrowedBook
{
  public enum BorrowedBookItemEvent
  {
    ReturnBook,
  }
  public class BorrowedBookItemMediator : EventMediator
  {
    [Inject]
    public BorrowedBookItemView view { get; set; }
    
    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(BorrowedBookItemEvent.ReturnBook, OnReturnBook);
      
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

      view.returnBookButton.interactable = false;
      gameObject.SetActive(true);

      view.listNumberText.text = "";
      view.isbnText.text = "<b>ISBN</b>";
      view.bookNameText.text = "<b>Title</b>";
      view.borrowedDateText.text = "<b>Borrowed Date</b>";
      view.endBorrowedDateText.text = "<b>Deadline</b>";
      view.borrowCodeText.text = "<b>Borrow Code</b>";
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
      view.listNumberText.text = view.GetID() + 1 + ".";
      view.isbnText.text = view.GetBookVo().ISBN.ToString();
      view.bookNameText.text = view.GetBookVo().title;
      view.borrowedDateText.text = view.GetBookVo().borrowedDate.ToString(CultureInfo.CurrentCulture);
      view.endBorrowedDateText.text = view.GetBookVo().endBorrowDate.ToString(CultureInfo.CurrentCulture);
      view.borrowCodeText.text = view.GetBookVo().borrowCode;

      view.endBorrowedDateText.color = view.GetBookVo().expired ? Color.red : Color.white;
    }
    
    private void OnHideItem(IEvent payload)
    {
      int id = (int)payload.data;

      if (view.GetID() != id)
        return;

      gameObject.SetActive(false);
      view.SetBookVo(null);
    }

    private void OnReturnBook()
    {
      libraryModel.ReturnBook(view.GetBookVo().borrowCode);

      view.borrowCodeText.color = view.GetBookVo().expired ? Color.red : Color.green;
      view.returnBookButton.interactable = false;
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
      view.dispatcher.RemoveListener(BorrowedBookItemEvent.ReturnBook, OnReturnBook);

      dispatcher.RemoveListener(MainEvent.UpdateBookListPanelItem, OnUpdateItem);
      dispatcher.RemoveListener(MainEvent.HideBookListPanelItem, OnHideItem);
      dispatcher.RemoveListener(MainEvent.ShowBookListPanelItem, OnShowItem);
      dispatcher.RemoveListener(MainEvent.SetCategoryTitles, OnSetCategoryTitles);
      dispatcher.RemoveListener(MainEvent.DestroyItems, OnDestroyItem);
    }
  }
}