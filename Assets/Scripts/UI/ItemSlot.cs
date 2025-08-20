using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    public ItemData Item;


    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outine;

    public InventoryUI Inventory;

    public int index;
    public bool equipped;
    public int quantity;


    private void Awake()
    {
        outine = GetComponentInChildren<Outline>();
    }
    private void OnEnable()
    {
        Inventory=GetComponentInParent<InventoryUI>();
        outine.enabled = equipped;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = Item.icon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outine != null)
        {
            outine.enabled = equipped;
        }
    }

    public void Clear()
    {
        Item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.SelectItem(index);
    }
}
