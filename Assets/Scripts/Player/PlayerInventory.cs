using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryUI inventoryUI;
    ItemSlot questItemSlot;

    public void GetInventoryInfo(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }

    public int CheckItemQuantity(ItemData item)
    {
        foreach(ItemSlot slot  in inventoryUI.slots)
        {
            if(slot.Item==item)
            {
                questItemSlot = slot;
                return slot.quantity;
            }
        }
        return 0;
    }

    public void ChangeItemQuantity(int quantity)
    {
        //그냥 여기서 해도 되긴...한데 되도록 나중에 바꾸자

        questItemSlot.quantity += quantity;
        inventoryUI.UpdateUI();
    }
}
