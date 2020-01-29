using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UITables : MonoBehaviour
{
    [SerializeField] private GameObject _tableButtonPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Watcher _watcher;
    private List<Table> _tables;
    private List<TableButton> _tableButtons;

    private void Start()
    {
        _gameObject = this.gameObject;
        _tables = FindObjectsOfType<Table>().ToList();
        _tableButtons = new List<TableButton>();
        CreateTableButtons();
    }

    private void CreateTableButtons()
    {
        if(_tableButtons.Count > 0)
            RemoveAllTableButtons();
        
        foreach (var table in _tables)
        {
            TableButton tableButton = Instantiate(_tableButtonPrefab, _container).GetComponent<TableButton>();
            tableButton.SetWatcher(_watcher);
            tableButton.SetLookable(table.GetContent());
            tableButton.SetName(table.GetName());
            _tableButtons.Add(tableButton);
        }
    }

    private void RemoveAllTableButtons()
    {
        foreach (var tableButton in _tableButtons)
        {
            Destroy(tableButton.gameObject);
        }
        _tableButtons = new List<TableButton>();
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
