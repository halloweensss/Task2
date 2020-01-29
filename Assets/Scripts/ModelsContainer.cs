using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsContainer : MonoBehaviour, ILookable, IContainer
{
    private Transform _transform;
    private Collider _collider;
    
    [SerializeField] private bool _isActive;
    [SerializeField] private List<GameObject> _lookables;

    public void Start()
    {
        _transform = this.transform;
        _collider = _transform.GetComponent<Collider>();
    }
    
    public Transform GetTransform()
    {
        return _transform;
    }

    public void Activate()
    {
        _isActive = true;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        _isActive = false;
        _collider.enabled = false; 
    }

    public void Show()
    {
        this.Deactivate();
        foreach (var lookable in _lookables)
        {
            lookable.GetComponent<ILookable>().Activate();
        }
    }

    public void Hide()
    {
        this.Activate();
        foreach (var lookable in _lookables)
        {
            lookable.GetComponent<ILookable>().Deactivate();
        }
    }

    public string GetName()
    {
        return null;
    }

    public List<ILookable> GetContents()
    {
        List<ILookable> lookablesList = new List<ILookable>();
        foreach (var lookable in _lookables)
        {
            lookablesList.Add(lookable.GetComponent<ILookable>());
        }

        return lookablesList;
    }
}
