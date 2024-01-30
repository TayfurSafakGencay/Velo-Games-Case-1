using strange.extensions.mediation.impl;

namespace Runtime.Contexts.Main.View.Library
{
  public class LibraryView : EventView
  {
    public void OnOpenAddBookPanel()
    {
      dispatcher.Dispatch(LibraryMediatorEvent.OpenBookPanel);
    }

    public void OnOpenBookListPanel()
    {
      dispatcher.Dispatch(LibraryMediatorEvent.OpenBookListPanel);
    }

    public void OnExit()
    {
      dispatcher.Dispatch(LibraryMediatorEvent.Exit);
    }
  }
}