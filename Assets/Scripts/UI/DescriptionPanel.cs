using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Text _nameText;
    private String _description;
    private String _name;

    public void SetDescription(String description)
    {
        _description = description;
        _descriptionText.text = _description;
    }

    public void SetName(String name)
    {
        _name = name;
        _nameText.text = _name;
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
