using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
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
        craftingSlotList = GetComponentsInChildren<CraftingSlot>().ToList();
        foreach(CraftingSlot slot in craftingSlotList) 
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

    private void Update()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }



    public void SelectSlot(CraftingSlot craftingSlot)
    {
        if(selectedCraftingSlot != null)
        {
            selectedCraftingSlot.GetComponent<Outline>().enabled = false;
        }
        selectedCraftingSlot = craftingSlot;
        selectedCraftingSlot.GetComponent<Outline>().enabled = true;
    }
    // Update is called once per frame
    
}
