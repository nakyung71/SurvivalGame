using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour,IInteractable
{
    
    public string GetInteractPrompt()
    {
        return " 아이템을 제작할 수 있는 테이블";
    }

    public void OnInteract()
    {
        UIManager.Instance.ShowUI(UIManager.Instance.craftingUI.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
