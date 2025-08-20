using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData",menuName ="DialogueSO")]
public class DialogueData : ScriptableObject

{
    public string TalkerName;

    public float dialogueID;

    [TextArea(3,10)]
    public string[] dialogueLines;

    public string[] choiceButtonText;

    public bool choiceExist;

    public bool nextDialogueExist;

    public float nextDialogueID_Button1;
    public float nextDialogueID_Button2;

}
