using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameUI gameUI;
    public InventoryUI inventoryUI;
    public DialogueUI dialogueUI;
    List<GameObject> openedUIList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       
    }
    private bool CheckOpenUI(GameObject checkObject)
    {
        foreach (GameObject go in openedUIList)
        {
            if (go==checkObject)
            {
                return true;
            }
        }
        return false;
    }

    public void ShowUI(UIKey key,UIState state)
    {
        


    }
}
