using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSheep : BaseNPC,ITalkable,IInteractable

{
    [SerializeField] DialogueData[] dialogueDatas;
    private NPCSound npcSound;
    public int TalkStep { get; private set; } = 0;

    void Start()
    {
        npcSound = GetComponentInParent<NPCSound>();
    }
    public string GetInteractPrompt()
    {
        return "장난꾸러기 같은 양이다";

    }

    public void OnInteract()
    {
        npcSound.Sheep();
        Talk();
        npcSound.Sheep();
    }

    public void Talk()
    {
        DialogueManager.Instance.SetTalk(dialogueDatas[0], this);

    }


}
