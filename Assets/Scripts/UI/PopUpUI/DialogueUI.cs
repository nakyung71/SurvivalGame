using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUI : PopUpUI
{
    public override UIKey UIKey => UIKey.Pop_Dialogue;
    private void OnEnable()
    {
        popUpUIStack.Push(this);
    }
}
