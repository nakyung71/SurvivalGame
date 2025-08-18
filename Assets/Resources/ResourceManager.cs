using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    List<BaseUI> UIList = new List<BaseUI>();
    Canvas staticCanvas;
    Canvas popUpCanvas;

    Dictionary<UIKey,GameObject> prefabDictionary = new Dictionary<UIKey,GameObject>();


    private void Awake()
    {
        Instance = this;
        staticCanvas = GameObject.FindFirstObjectByType<StaticUI>().GetComponent<Canvas>();
        popUpCanvas= GameObject.FindFirstObjectByType<PopUpUI>().GetComponent<Canvas>();
        UIList = Resources.LoadAll<BaseUI>("UI").ToList();

        AddUI();
    }
    


    void AddUI()
    {
        foreach (BaseUI go in UIList)
        {
            prefabDictionary.Add(go.UIKey, go.gameObject);
           
        }
       
    }

    public GameObject GetPrefab(UIKey key)
    {
        return prefabDictionary[key];
    }
    public Canvas GetCanvas(UIState state)
    {
        if(state==UIState.Static)
        {
            return staticCanvas;
        }
        else
        {
            return popUpCanvas;
        }
    }
}

