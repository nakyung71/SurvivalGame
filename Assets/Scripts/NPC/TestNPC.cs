using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestNPC : MonoBehaviour,ITalkable,IInteractable,IQuest
{
    [SerializeField] DialogueData data;
    public DialogueData DialogueData => data;

    public void AcceptQuest()
    {
        Debug.Log("Äù½ºÆ®");
    }

    public string GetInteractPrompt()
    {
        return "Å¥ºê";
    }

    public void OnInteract()
    {
        DialogueManager.Instance.SetTalk(data,this);
    }

    public void Talk()
    {
        DialogueManager.Instance.SetTalk(data);
    }

    

    
}
