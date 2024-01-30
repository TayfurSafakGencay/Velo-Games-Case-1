using Runtime.Contexts.Main.Enum;
using Runtime.Contexts.Main.Model;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Runtime.Contexts.Main.View.Library
{
  public enum LibraryMediatorEvent
  {
    OpenBookPanel,
    OpenBookListPanel,
    LoadReadyData
  }

  public class LibraryMediator : EventMediator
  {
    [Inject]
    public LibraryView view { get; set; }
    
    [Inject]
    public ILibraryModel libraryModel { get; set; }

    public override void OnRegister()
    {
      view.dispatcher.AddListener(LibraryMediatorEvent.OpenBookPanel, OnOpenAddBookPanel);
      view.dispatcher.AddListener(LibraryMediatorEvent.OpenBookListPanel, OnOpenBookListPanel);
      view.dispatcher.AddListener(LibraryMediatorEvent.LoadReadyData, OnLoadReadyData);
    }

    private void OnOpenAddBookPanel()
    {
      dispatcher.Dispatch(MainEvent.OpenPanel, PanelKey.addBookPanel);
    }

    private void OnOpenBookListPanel()
    {
      dispatcher.Dispatch(MainEvent.OpenPanel, PanelKey.bookListPanel);
    }

    private void Start()
    {
      libraryModel.Load(Application.dataPath + "/Scripts/Runtime/Contexts/Main/Data/Data.json");
    }

    private void OnApplicationQuit()
    {
      libraryModel.Save(Application.dataPath + "/Scripts/Runtime/Contexts/Main/Data/Data.json");
    }

    private void OnLoadReadyData()
    {
      libraryModel.Load(Application.dataPath + "/Scripts/Runtime/Contexts/Main/Data/ReadyData.json");
    }

    public override void OnRemove()
    {
      view.dispatcher.RemoveListener(LibraryMediatorEvent.OpenBookPanel, OnOpenAddBookPanel);
      view.dispatcher.RemoveListener(LibraryMediatorEvent.OpenBookListPanel, OnOpenBookListPanel);
      view.dispatcher.RemoveListener(LibraryMediatorEvent.LoadReadyData, OnLoadReadyData);
    }
  }
}