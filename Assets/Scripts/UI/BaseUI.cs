using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIState
{
    Static,
    PopUp
}

public abstract class BaseUI : MonoBehaviour
{
    public abstract UIState State { get; }
    public void ShowUI()
    {
        
    }
}
