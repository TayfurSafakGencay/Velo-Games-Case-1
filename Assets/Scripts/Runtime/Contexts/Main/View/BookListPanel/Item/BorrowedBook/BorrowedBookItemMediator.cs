using strange.extensions.mediation.impl;

namespace Runtime.Contexts.Main.View.BookListPanel.Item.BorrowedBook
{
  public class BorrowedBookItemMediator : EventMediator
  {
    [Inject]
    public VIEW view { get; set; }

    public override void OnRegister()
    {
    }

    public override void OnRemove()
    {
    }
  }
}