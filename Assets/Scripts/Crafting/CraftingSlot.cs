using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour,IPointerClickHandler
{

    [SerializeField] Image neededItem_1;
    [SerializeField] Image neededItem_2;
    [SerializeField] Image resultItem;
    [SerializeField] GameObject plusText;
    public CraftingData SlotCraftingData {  get; private set; }
    CraftingUI craftingUI;

    PlayerInventory inventory;
    private void Start()
    {
        craftingUI = GetComponentInParent<CraftingUI>();
        inventory = CharacterManager.Instance.Player.inventory;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        craftingUI.SelectSlot(this);
    }

    // Start is called before the first frame update
    public void SetSlot(CraftingData craftingData)
    {
        SlotCraftingData = craftingData;
        if(craftingData.neededItems.Length==1)
        {
            neededItem_1.sprite=craftingData.neededItems[0].itemData.icon;
            neededItem_2.gameObject.SetActive(false);
            plusText.SetActive(false);
            resultItem.sprite = craftingData.resultItem.icon;
        }
        else if(craftingData.neededItems.Length == 2) 
        {
            neededItem_1.sprite = craftingData.neededItems[0].itemData.icon;
            neededItem_2.sprite=craftingData.neededItems[1].itemData.icon;
            plusText.SetActive(true);
            resultItem.sprite = craftingData.resultItem.icon;
        }
    }


    public bool CheckCraftingReady()
    {
        int firstNeededItemQuantityOwn = 0;
        int secondNeededItemQuantityOwn = 0;
        if(SlotCraftingData.neededItems.Length == 1)
        {
            firstNeededItemQuantityOwn = inventory.CheckItemQuantity(SlotCraftingData.neededItems[0].itemData);
            if (firstNeededItemQuantityOwn >= SlotCraftingData.neededItems[0].neededQuantity)
            {
                inventory.ChangeItemQuantity(-SlotCraftingData.neededItems[0].neededQuantity);
                
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (SlotCraftingData.neededItems.Length == 2)
        {
            firstNeededItemQuantityOwn= inventory.CheckItemQuantity(SlotCraftingData.neededItems[0].itemData);
            secondNeededItemQuantityOwn = inventory.CheckItemQuantity(SlotCraftingData.neededItems[1].itemData);
            if (firstNeededItemQuantityOwn >= SlotCraftingData.neededItems[0].neededQuantity && secondNeededItemQuantityOwn >= SlotCraftingData.neededItems[1].neededQuantity)
            {
                inventory.ChangeItemQuantity(-SlotCraftingData.neededItems[0].neededQuantity, -SlotCraftingData.neededItems[1].neededQuantity);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;

        
        
    }
}
