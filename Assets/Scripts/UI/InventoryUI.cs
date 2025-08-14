using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : PopUpUI
{
    public override UIKey UIKey => UIKey.Pop_Inventory;
    private void OnEnable()
    {
        popUpUIStack.Push(this);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
