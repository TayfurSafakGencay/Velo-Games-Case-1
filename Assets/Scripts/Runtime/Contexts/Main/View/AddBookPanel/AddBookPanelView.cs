using strange.extensions.mediation.impl;
using TMPro;

namespace Runtime.Contexts.Main.View.AddBookPage
{
  public class AddBookPageView : EventView
  {
    public TMP_InputField titleInput;

    public TMP_InputField authorInput;

    public TMP_InputField ISBNInput;

    public TMP_InputField copyInput;
    
    public void OnAddBook()
    {
      dispatcher.Dispatch(AddBookPageEvent.AddBook);
    }

    public void OnClosePanel()
    {
      dispatcher.Dispatch(AddBookPageEvent.ClosePanel);
    }
  }
}