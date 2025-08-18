using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public interface ITalkable
{
    DialogueData DialogueData { get; }
    public void Talk();
}
public class DialogueUI :MonoBehaviour
{
    

    private void OnEnable()
    {
        
    }
}
