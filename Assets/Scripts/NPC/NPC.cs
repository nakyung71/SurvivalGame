using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//public class NPC : MonoBehaviour
//{
//    [SerializeField] NPCData[] NPCDatas;

//    public int TalkTimes { get; private set; } = 0;

//    public bool IsQuestAccepted { get; private set; } = false;

//    private int goalQuantity = 20;

//    //꼭 인스펙터 창에 대화 SO를 넣어야 진행이 됩니다.

//    [SerializeField] ItemData questItemData;
//    [SerializeField] GameObject successReward;

//    public void AcceptQuest()
//    {

//        Debug.Log($"{this.name}의 퀘스트 수락");
//        IsQuestAccepted = true;




//        //퀘스트에 필요한 메서드나 내용들 여기 적으세요
//        //예를 들어 퀘스트 시작 문구가 뜬다던가
//        // IQuest 상속 시 필수 구현(인터페이스 상속 안하면 작동x)

//    }

   

    

   

//    public void Talk()
//    {
//        if (IsQuestAccepted == false)
//        {
//            // DialogueManager.Instance.SetTalk(dialogueDatas[0], this);
//            TalkTimes++;
//        }
//        else
//        {
//            // DialogueManager.Instance.SetTalk(dialogueDatas[1], this);
//        }

//        // 대화 경로는 DialogueManager.Instance.SetTalk(data,this); 이며,
//        // 그냥 본인이 들고 있는 데이터와 자기 자신을 SetTalk 메서드의 인자로 넘겨주시면 됩니다.
//        //ITalkable 상속시 필수구현
//    }

//}
