using Runtime.Contexts.Main.Model;
using Runtime.Contexts.Main.View.AddBookPanel;
using Runtime.Contexts.Main.View.BookListPanel;
using Runtime.Contexts.Main.View.BookListPanel.Item.Book;
using Runtime.Contexts.Main.View.BookListPanel.Item.BorrowedBook;
using Runtime.Contexts.Main.View.Library;
using Runtime.Contexts.Main.View.PanelManager;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.Config
{
  public class MainContext : MVCSContext
  {
    public MainContext(MonoBehaviour view) : base(view)
    {
    }

    public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void mapBindings()
    {
      base.mapBindings();

      injectionBinder.Bind<ILibraryModel>().To<LibraryModel>().ToSingleton();

      mediationBinder.Bind<LibraryView>().To<LibraryMediator>();
      mediationBinder.Bind<AddBookPanelView>().To<AddBookPanelMediator>();
      mediationBinder.Bind<BookListPanelView>().To<BookListPanelMediator>();
      mediationBinder.Bind<BookListPanelItemView>().To<BookListPanelItemMediator>();
      mediationBinder.Bind<BorrowedBookItemView>().To<BorrowedBookItemMediator>();
      mediationBinder.Bind<PanelManagerView>().To<PanelManagerMediator>();
    }
  }
}