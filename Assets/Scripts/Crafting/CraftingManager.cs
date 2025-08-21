using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    PlayerInventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = CharacterManager.Instance.Player.inventory;
    }

    
   
}
