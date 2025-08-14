using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPopUpUI : PopUpUI
{
    public override UIKey UIKey => UIKey.Pop_DefaultPopUp;
    private void OnEnable()
    {
        popUpUIStack.Push(this);
    }


}
