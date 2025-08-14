using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUI : BaseUI
{
    public override UIState State => UIState.PopUp;


    Stack<PopUpUI> popUpUIStack=new Stack<PopUpUI>();
    // Start is called before the first frame update
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
        gameObject.SetActive(false);
    }
}
