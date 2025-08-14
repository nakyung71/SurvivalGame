using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIState
{
    Static,
    PopUp
}
public enum UIKey
{
    Static_Game,
    Pop_Inventory,
    Pop_DefaultPopUp,
    Pop_Dialogue
}


public abstract class BaseUI : MonoBehaviour
{
    public abstract UIState State { get; }

    public virtual UIKey UIKey { get; }
    
}
