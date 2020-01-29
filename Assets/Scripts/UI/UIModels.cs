using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModels : MonoBehaviour
{
    [SerializeField] private GameObject _modelButtonPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Watcher _watcher;
    private List<ILookable> _lookables = new List<ILookable>();
    private List<ModelButton> _modelButtons = new List<ModelButton>();



    public void SetLookables(List<ILookable> lookables)
    {
        _lookables = lookables;
    }

    public void CreateModelsButton()
    {
        if (_modelButtons.Count > 0)
            RemoveAllModelsButtons();

       foreach (var lookable in _lookables)
       {
           ModelButton modelButton = Instantiate(_modelButtonPrefab, _container).GetComponent<ModelButton>();
           modelButton.SetWatcher(_watcher);
           modelButton.SetLookable(lookable);
           modelButton.SetName(lookable.GetName());
           _modelButtons.Add(modelButton);
       }
    }
    
    private void RemoveAllModelsButtons()
    {
        foreach (var modelButton in _modelButtons)
        {
            Destroy(modelButton.gameObject);
        }
        _modelButtons = new List<ModelButton>();
    }

    public void Show()
    {
        _gameObject.SetActive(true);
    }

    public void Hide()
    {
        _gameObject.SetActive(false);
    }
}
