using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public interface ITalkable
{
    DialogueData DialogueData { get; }
    public void Talk();
}
public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;
    [SerializeField] GameObject dialogueUI;
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    bool moveToNextLine;

    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            moveToNextLine = true;
        }
    }
    public void SetTalk(DialogueData data)
    {
        StartCoroutine(Talk(data));
    }

    IEnumerator Talk(DialogueData data)
    {
        dialogueUI.SetActive(true);
        int index = 0;
        speakerNameText.SetText(data.TalkerName);
        moveToNextLine = true;

        while(index<data.dialogueLines.Length)
        {
            yield return new WaitUntil(() => moveToNextLine);

            moveToNextLine = false;
            dialogueText.SetText(data.dialogueLines[index]);
            index++;
        }
        yield return new WaitUntil(() => moveToNextLine);
        
        dialogueUI.SetActive(false);
        
       

    }
    
}
