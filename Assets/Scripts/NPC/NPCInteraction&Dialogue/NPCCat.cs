using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCat : BaseNPC, ITalkable, IQuest, IInteractable
{
    [SerializeField] DialogueData[] dialogueDatas;
    [SerializeField] ItemData questItemData;
    [SerializeField] ItemData successItemData;

    public int TalkStep { get; private set; } = 0;

    public bool IsQuestAccepted { get; private set; } = false;

    private int goalQuantity = 1;
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

            CharacterManager.Instance.Player.itemData = successItemData;
            CharacterManager.Instance.Player.addItem?.Invoke();
            //인벤토리에 넣어주기

            TalkStep = 1;
        }
    }

    public string GetInteractPrompt()
    {
        return "앙증맞은 고양이다";

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
        else if (IsQuestAccepted == true && TalkStep == 0)
        {
            DialogueManager.Instance.SetTalk(dialogueDatas[1], this);
        }
        else if (IsQuestAccepted == true && TalkStep == 1)
        {
            DialogueManager.Instance.SetTalk(dialogueDatas[2], this);
        }

    }


}
