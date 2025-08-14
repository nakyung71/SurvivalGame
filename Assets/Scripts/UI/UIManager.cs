using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    List<GameObject> openedUIList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
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

    public void ShowUI(string prefabName,UIState state)
    {
        
        GameObject go = ResourceManager.Instance.GetPrefab(prefabName);
        if(CheckOpenUI(go))
        {
            Canvas canvas = ResourceManager.Instance.GetCanvas(state);
            Instantiate(go, canvas.transform, false);
            openedUIList.Add(go);
        }
        else
        {
            go.SetActive(true);
        }
        
    }
}
