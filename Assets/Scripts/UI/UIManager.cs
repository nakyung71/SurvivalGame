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

    private void Start()
    {
        ShowUI(UIKey.Pop_Inventory, UIState.PopUp);
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
        
        GameObject go = ResourceManager.Instance.GetPrefab(key);
        if(go == null)
        {
            Debug.Log("UI를 찾을 수 없습니다");
            return;
        }
        else
        {
            if (CheckOpenUI(go))
            {
                Canvas canvas = ResourceManager.Instance.GetCanvas(state);
                go.SetActive(true);
            }
            else
            {
                Canvas canvas = ResourceManager.Instance.GetCanvas(state);
                Instantiate(go, canvas.transform, false);
                openedUIList.Add(go);
                go.SetActive(true);
            }

        }

    }
}
