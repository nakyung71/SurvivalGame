using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    public CraftingData[] craftingDatas;
    PlayerInventory inventory;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        inventory = CharacterManager.Instance.Player.inventory;
        
    }

    
   
}
