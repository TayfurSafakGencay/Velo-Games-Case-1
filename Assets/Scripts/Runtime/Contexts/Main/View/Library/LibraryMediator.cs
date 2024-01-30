using strange.extensions.mediation.impl;

namespace Runtime.Contexts.Main.View.Library
{
  public class LibraryMediator : EventMediator
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