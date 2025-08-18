using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData",menuName ="DialogueSO")]
public class DialogueData : ScriptableObject
{
    public string TalkerName;


    [TextArea(3,10)]
    public string[] dialogueLines;

}
