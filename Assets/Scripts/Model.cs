using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour, ILookable, IModel
{
    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private Material _baseMaterial;

    [SerializeField] private String _name;
    [SerializeField] private String _description;
    [SerializeField] private bool _isActive;
    [SerializeField] private Material _outlineMaterial;

    public void Start()
    {
        _transform = this.transform;
        _collider = _transform.GetComponent<Collider>();
        _meshRenderer = _transform.GetComponent<MeshRenderer>();
        _baseMaterial = _meshRenderer.material;
        _outlineMaterial.mainTexture = _baseMaterial.mainTexture;
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
        Deactivate();
        _outlineMaterial.mainTexture = _baseMaterial.mainTexture;
        _meshRenderer.material = _outlineMaterial;
    }

    public void Hide()
    {
        Activate();
        _meshRenderer.material = _baseMaterial;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }
}
