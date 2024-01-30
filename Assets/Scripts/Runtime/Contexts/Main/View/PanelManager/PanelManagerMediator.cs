using Runtime.Contexts.Main.Enum;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Contexts.Main.View.PanelManager
{
  public class PanelMediator : EventMediator
  {
    [Inject]
    public PanelManagerView managerView { get; set; }

    public override void OnRegister()
    {
      dispatcher.AddListener(MainEvent.OpenPanel, OnOpenPanel);
    }

    public void OnOpenPanel(IEvent payload)
    {
      string panelKey = (string)payload.data;
      AsyncOperationHandle<GameObject> instantiateAsync = Addressables.InstantiateAsync(panelKey, transform);
    }

    public override void OnRemove()
    {
      dispatcher.RemoveListener(MainEvent.OpenPanel, OnOpenPanel);
    }
  }
}