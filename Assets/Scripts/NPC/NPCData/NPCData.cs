using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "NPCSO")]

public class NPCData : ScriptableObject
{
    public string TalkerName;

    public float NPCID;

    [TextArea(3, 10)]
    public string[] NPCLines;

    public string[] choiceButtonText;

    public bool choiceExist;

    public bool nextDialogueExist;

    public float nextDialogueID_Button1;
    public float nextDialogueID_Button2;
}
