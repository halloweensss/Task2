using System;
using UnityEngine;

public interface ILookable
{
    Transform GetTransform();
    void Activate(); 
    void Deactivate();
    void Show();
    void Hide();

    String GetName();
}
