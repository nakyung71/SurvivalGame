using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public InventoryUI inventoryUI;
    ItemSlot questItemSlot;
    List<ItemSlot> questItemSlots = new List<ItemSlot>();
 

    public void GetInventoryInfo(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }

    public int CheckItemQuantity(ItemData item)//퀘스트 아이템이 2개 이상일 경우를 대비?
    {
       
        foreach (ItemSlot slot  in inventoryUI.slots)
        {

            if(slot.Item==item)
            {
                questItemSlot= slot;
                questItemSlots.Add(slot);
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

    

    public void ChangeItemQuantity(int firstItemQuantity,int secondItemQuantity)
    {
        int beforeLastIndex = questItemSlots.Count - 2;
        int lastIndex=questItemSlots.Count-1;
        questItemSlots[beforeLastIndex].quantity += firstItemQuantity;
        questItemSlots[lastIndex].quantity += secondItemQuantity;
        inventoryUI.UpdateUI();
    }
}
