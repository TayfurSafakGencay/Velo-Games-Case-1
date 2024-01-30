using System;
using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.Vo;
using strange.extensions.mediation.impl;

namespace Runtime.Contexts.Main.View.AddBookPage
{
  public enum AddBookPageEvent
  {
    AddBook,
    ClosePanel
  }
  public class AddBookPageMediator : EventMediator
  {
    [Inject]
    public AddBookPageView view { get; set; }
    
    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(AddBookPageEvent.AddBook, OnAddBook);
      view.dispatcher.AddListener(AddBookPageEvent.ClosePanel, OnClosePage);
    }
    
    private void OnAddBook()
    {
      BookVo bookVo = new()
      {
        title = view.titleInput.text,
        author = view.authorInput.text,
        ISBN = Convert.ToInt32(view.ISBNInput.text),
        copy = Convert.ToInt32(view.copyInput.text),
      };
      
      libraryModel.AddBook(bookVo);
    }

    private void OnClosePage()
    {
      Destroy(gameObject);
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(AddBookPageEvent.AddBook, OnAddBook);
      view.dispatcher.RemoveListener(AddBookPageEvent.ClosePanel, OnClosePage);
    }
  }
}