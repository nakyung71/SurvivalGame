using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultPopUpUI : PopUpUI
{
    TextMeshProUGUI warningText;
    Button yesButton;
    Button noButton;

    public override UIKey UIKey => UIKey.Pop_DefaultPopUp;
    private void OnEnable()
    {
        popUpUIStack.Push(this);
        //문구 수정//문제는 이거 데이터를 어떻게 가져옴?
        
    }

    void PressYesButton()
    {
        //이게 아이템 창일때는 yes누르면 버리고 싶고
        //이게 퀘스트 창이면 yes누르면 수락하고 싶고

        //만약 인터페이스 같은걸 만들면 어떨까 예를들어 아이템 창이나 퀘스트창에
        //IPOPUP같은거 해서 void POPUPActivate()
        //그럼 여기에 그 인터페이스를 넘겨서 IPOPUP popup.POPUPActivate() 같이?
        //이 문구에 대한 정보는 누가 들고 있지?
        //이 문구에 대한 데이터를 팝업 부모클래스에 virtual로 두고, 필요한 자식들만? 정보는 각각 필요한 자식들이 드는게 맞아보임..
        //그건 그렇다치고 그럼 여기에 신호를 어떻게 줘? 부모 클래스는 자식 클래스 대부분을 모름...;



    }

    void PressNoButton()
    {

    }

    private void Update()
    {
        
    }


}
