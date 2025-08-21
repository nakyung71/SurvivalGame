using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public enum UIActiveState
{
    Inactive,
    Active
}
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameUI gameUI;
    public InventoryUI inventoryUI;
    public DialogueUI dialogueUI;
    public CraftingUI craftingUI;
    public QuestCompleteUI questCompleteUI;
    
    List<GameObject> openedUIList = new List<GameObject>();
    public static Stack<PopUpUI> popUpUIStack = new Stack<PopUpUI>();
    [SerializeField] PlayerInput playerInput;
    [SerializeField] UIActiveState currentState=UIActiveState.Inactive;
    private void Awake()
    {
        Instance = this;
        Debug.Log("UI매니저인스턴스 생성");
    }

    private void Start()
    {
       StartCoroutine(ClearStack());
       
    }

    IEnumerator ClearStack()
    {
        yield return new WaitForSeconds(1f);
        popUpUIStack.Clear();

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


    public void ChangeUIActiveState(UIActiveState state)
    {
        currentState = state;
    }
    public void ShowUI(GameObject gameObject)
    {
        
        gameObject.SetActive(true);
        currentState = UIActiveState.Active;


    }

   
    // Start is called before the first frame update
    //리스트에 넣는건 본인들이지만, 결국 그걸 관리하는 건 여기서?
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            questCompleteUI.gameObject.SetActive(true);
            Debug.Log("퀘스트 열어보기");
        }

        if (currentState == UIActiveState.Active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //근데 단순히 끄는게 아니라 켜져있던 제일 마지막 창을 꺼야함,,
                //스택 자료구조?
                DisablePopUpUI();
            }
            playerInput.actions.FindActionMap("Player").Disable();
            Cursor.lockState = CursorLockMode.None;
        }
        else if(currentState == UIActiveState.Inactive)
        {
            playerInput.actions.FindActionMap("Player").Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DisablePopUpUI()
    {

        Debug.Log("끄기 시도" + transform.name);

        if (popUpUIStack.Count > 0)
        {
            currentState = UIActiveState.Active;
            
            PopUpUI popUp = popUpUIStack.Pop();
            Debug.Log(popUpUIStack.Count);
            if (popUp != null)
            {
                popUp.gameObject.SetActive(false);
            }
            if(popUpUIStack.Count == 0)
            {
                currentState = UIActiveState.Inactive;
                
            }
        }



    }

    
}
