using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    List<GameObject> UIList = new List<GameObject>();
    Canvas staticCanvas;
    Canvas popUpCanvas;

    Dictionary<string,GameObject> prefabDictionary = new Dictionary<string,GameObject>();


    private void Awake()
    {
        Instance = this;
        staticCanvas = GameObject.FindFirstObjectByType<StaticUI>().GetComponent<Canvas>();
        popUpCanvas= GameObject.FindFirstObjectByType<PopUpUI>().GetComponent<Canvas>();
    }
    void Start()
    {
        UIList = Resources.LoadAll<GameObject>("UI").ToList();
        Debug.Log(UIList.Count);
        AddUI();
    }


    void AddUI()
    {
        foreach (GameObject go in UIList)
        {
            prefabDictionary.Add(go.name, go); 
        }
        
    }

    public GameObject GetPrefab(string name)
    {
        return prefabDictionary[name];
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

