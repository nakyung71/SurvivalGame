using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : PopUpUI 
{
    [SerializeField] Image craftItemImage;
    [SerializeField] TextMeshProUGUI craftItemName;
    [SerializeField] Button craftButton;
    // Start is called before the first frame update
    List<CraftingSlot> craftingSlotList= new List<CraftingSlot>();
    int index = 0;
    CraftingSlot selectedCraftingSlot;
    void Start()
    {
        this.gameObject.SetActive(false);
        craftingSlotList = GetComponentsInChildren<CraftingSlot>().ToList();
        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(PressCraftButton);
        foreach (CraftingSlot slot in craftingSlotList) 
        {
            if(CraftingManager.Instance.craftingDatas.Length<=index)
            {
                return;
            }
            slot.SetSlot(CraftingManager.Instance.craftingDatas[index]);
            index++;
        }

        
        

        //CharacterManager.Instance.Player.controller.ToggleCursor();
       
    }
    private void OnEnable()
    {
        popUpUIStack.Push(this);
    }

   



    public void SelectSlot(CraftingSlot craftingSlot)
    {
        if(selectedCraftingSlot != null)
        {
            selectedCraftingSlot.GetComponent<Outline>().enabled = false;
        }
        selectedCraftingSlot = craftingSlot;
        selectedCraftingSlot.GetComponent<Outline>().enabled = true;
        ChangeDescriptionPanel();
    }

    private void ChangeDescriptionPanel()
    {
        craftItemImage.sprite = selectedCraftingSlot.SlotCraftingData.resultItem.icon;
        craftItemName.text = selectedCraftingSlot.SlotCraftingData.resultItem.displayName;

    }

    void PressCraftButton()
    {
        Debug.Log("버튼 누름");
        if(selectedCraftingSlot.CheckCraftingReady())
        {
            CharacterManager.Instance.Player.itemData= selectedCraftingSlot.SlotCraftingData.resultItem;
            CharacterManager.Instance.Player.inventory.inventoryUI.AddItem();
            //아이템 갯수 바꾸기
            //인벤토리에 새로운 아이템 추가
        }
    }


}
