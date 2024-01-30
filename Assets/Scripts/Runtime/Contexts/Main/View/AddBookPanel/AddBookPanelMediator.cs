using System;
using Module.Debugger;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.AddBookPanel
{
  public enum AddBookPanelEvent
  {
    AddBook,
    ClosePanel
  }
  public class AddBookPanelMediator : EventMediator
  {
    [Inject]
    public AddBookPanelView view { get; set; }
    
    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(AddBookPanelEvent.AddBook, OnAddBook);
      view.dispatcher.AddListener(AddBookPanelEvent.ClosePanel, OnClosePage);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
        NavigateToNextInputField();
      }
    }

    private void NavigateToNextInputField()
    {
      for (int i = 0; i < view.inputFields.Count; i++)
      {
        if (!view.inputFields[i].isFocused) continue;
        int nextIndex = (i + 1) % view.inputFields.Count;
        view.inputFields[nextIndex].Select();
        view.inputFields[nextIndex].ActivateInputField();
        break;
      }
    }
    
    private void OnAddBook()
    {
      if (view.titleInput.text == "" || view.authorInput.text == "" || view.ISBNInput.text == "" || view.copyInput.text == "")
      {
        Debugger.EmptyError();
        return;
      }
      BookVo bookVo = new()
      {
        title = view.titleInput.text,
        author = view.authorInput.text,
        ISBN = view.ISBNInput.text,
        copy = Convert.ToInt32(view.copyInput.text),
        borrowedBookCount = 0
      };
      
      libraryModel.AddBook(bookVo);
    }

    private void OnClosePage()
    {
      Destroy(gameObject);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(AddBookPanelEvent.AddBook, OnAddBook);
      view.dispatcher.RemoveListener(AddBookPanelEvent.ClosePanel, OnClosePage);
    }
  }
}