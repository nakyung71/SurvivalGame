using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : BaseUI
{
    public override UIState State => UIState.PopUp;


    protected static Stack<PopUpUI> popUpUIStack=new Stack<PopUpUI>();
    // Start is called before the first frame update
    //리스트에 넣는건 본인들이지만, 결국 그걸 관리하는 건 여기서?

    public virtual void Awake()
    {
        Debug.Log(transform.name+"!!!");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //근데 단순히 끄는게 아니라 켜져있던 제일 마지막 창을 꺼야함,,
            //스택 자료구조?
            DisableUI();
        }
    }
    

   
    //버튼으로도 끄거나 esc로도 끄기
    void DisableUI()
    {
        Debug.Log("끄기 시도"+transform.name);
        Debug.Log(popUpUIStack.Count);
        if(popUpUIStack.Count > 0)
        {
            
            PopUpUI popUp = popUpUIStack.Pop();
            if (popUp != null)
            {
                popUp.gameObject.SetActive(false);
            }
        }
       
        
    }
}
