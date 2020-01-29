using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelButton : MonoBehaviour
{
    [SerializeField] private Text _text;
    
    private String _name;
    private Watcher _watcher;
    private ILookable _lookable;

    public void SetWatcher(Watcher watcher)
    {
        _watcher = watcher;
    }

    public void SetLookable(ILookable lookable)
    {
        _lookable = lookable;
    }

    public void SetName(String name)
    {
        _name = name;
        _text.text = _name;
        transform.name = _name + "Button";
    }

    public void ShowModel()
    {
        _watcher.LookTo(_lookable);
    }
}
