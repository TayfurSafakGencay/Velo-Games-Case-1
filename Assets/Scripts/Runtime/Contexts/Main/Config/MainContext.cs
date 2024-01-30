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
    }
  }
}