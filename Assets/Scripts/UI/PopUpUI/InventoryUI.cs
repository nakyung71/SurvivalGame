using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : PopUpUI
{
    public override UIKey UIKey => UIKey.Pop_Inventory;
    private void OnEnable()
    {
        popUpUIStack.Push(this);
        Debug.Log("스택에 넣음");
        Debug.Log(popUpUIStack.Count);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
