using System.Collections.Generic;
using strange.extensions.mediation.impl;
using TMPro;

namespace Runtime.Contexts.Main.View.AddBookPanel
{
  public class AddBookPanelView : EventView
  {
    public TMP_InputField titleInput;

    public TMP_InputField authorInput;

    public TMP_InputField ISBNInput;

    public TMP_InputField copyInput;

    public List<TMP_InputField> inputFields;

    public void OnAddBook()
    {
      dispatcher.Dispatch(AddBookPanelEvent.AddBook);
    }

    public void OnClosePanel()
    {
      dispatcher.Dispatch(AddBookPanelEvent.ClosePanel);
    }
  }
}