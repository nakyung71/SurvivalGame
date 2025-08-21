using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;



public class DialogueUI:PopUpUI
{
 

    private void OnEnable()
    {
        UIManager.popUpUIStack.Push(this);
        UIManager.Instance.ChangeUIActiveState(UIActiveState.Active);
    }
    private void OnDisable()
    {
        UIManager.Instance.DisablePopUpUI();
    }
}
