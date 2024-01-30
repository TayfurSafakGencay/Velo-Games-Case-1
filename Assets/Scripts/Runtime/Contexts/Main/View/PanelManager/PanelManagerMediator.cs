using Runtime.Contexts.Main.Enum;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Contexts.Main.View.PanelManager
{
  public class PanelManagerMediator : EventMediator
  {
    [Inject]
    public PanelManagerView view { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(MainEvent.OpenPanel, OnOpenPanel);
      
      Init();
    }

    private void Init()
    {
      OnOpenPanel(PanelKey.libraryPanel);
    }

    public void OnOpenPanel(IEvent payload)
    {
      string panelKey = (string)payload.data;

      OnOpenPanel(panelKey);
    }

    public void OnOpenPanel(string panelKey)
    {
      AsyncOperationHandle<GameObject> instantiateAsync = Addressables.InstantiateAsync(panelKey, transform);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEvent.OpenPanel, OnOpenPanel);
    }
  }
}