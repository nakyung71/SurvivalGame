using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHorse : BaseNPC,ITalkable,IQuest,IInteractable
{
    [SerializeField] DialogueData[] dialogueDatas;
    [SerializeField] ItemData questItemData;
    [SerializeField] GameObject successReward;

    public int TalkStep { get; private set; } = 0;

    public bool IsQuestAccepted { get; private set; } = false;

    private int goalQuantity = 3;
    public void AcceptQuest()
    {

        Debug.Log($"{this.name}의 퀘스트 수락");
        IsQuestAccepted = true;

    }

    public void CheckQuestCondition()
    {
        int itemquantity = CharacterManager.Instance.Player.inventory.CheckItemQuantity(questItemData);
        if (itemquantity >= goalQuantity)
        {
            CharacterManager.Instance.Player.inventory.ChangeItemQuantity(-goalQuantity);

            successReward.SetActive(true);
            UIManager.Instance.questCompleteUI.gameObject.SetActive(true);
            
            TalkStep = 1;
        }
    }

    public string GetInteractPrompt()
    {
        return "수상하게 생긴 말이다";

    }

    public void OnInteract()
    {
        Talk();

    }

    public void Talk()
    {
        if (IsQuestAccepted == false)
        {
            DialogueManager.Instance.SetTalk(dialogueDatas[0], this);
            
        }
        else if(IsQuestAccepted == true&&TalkStep==0)
        {
            DialogueManager.Instance.SetTalk(dialogueDatas[1], this);
        }
        else if(IsQuestAccepted==true&&TalkStep==1)
        {
            DialogueManager.Instance.SetTalk(dialogueDatas[2], this);
        }

    }


}
