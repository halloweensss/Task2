using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, ITable
{
    [SerializeField] private GameObject _content;
    [SerializeField] private String _name;

    public ILookable GetContent()
    {
        return _content.GetComponent<ILookable>();;
    }

    public String GetName()
    {
        return _name;
    }
}
