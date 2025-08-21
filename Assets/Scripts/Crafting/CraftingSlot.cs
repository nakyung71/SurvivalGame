using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour,IPointerClickHandler
{

    [SerializeField] Image neededItem_1;
    [SerializeField] Image neededItem_2;
    [SerializeField] Image resultItem;
    [SerializeField] GameObject plusText;

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

}
