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


    private void Start()
    {
        craftingUI = GetComponentInParent<CraftingUI>();
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
             
        }
    }


    public bool CheckCraftingReady()
    {
        int firstNeededItemQuantity = 0;
        int secondNeededItemQuantity = 0;
        if(SlotCraftingData.neededItems.Length == 1)
        {
            firstNeededItemQuantity = CharacterManager.Instance.Player.inventory.CheckItemQuantity(SlotCraftingData.neededItems[0].itemData);
            if (firstNeededItemQuantity >= SlotCraftingData.neededItems[0].neededQuantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (SlotCraftingData.neededItems.Length == 2)
        {
            firstNeededItemQuantity= CharacterManager.Instance.Player.inventory.CheckItemQuantity(SlotCraftingData.neededItems[0].itemData);
            secondNeededItemQuantity = CharacterManager.Instance.Player.inventory.CheckItemQuantity(SlotCraftingData.neededItems[1].itemData);
            if (firstNeededItemQuantity >= SlotCraftingData.neededItems[0].neededQuantity && secondNeededItemQuantity >= SlotCraftingData.neededItems[1].neededQuantity)
            {
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
