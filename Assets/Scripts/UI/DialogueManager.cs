using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    bool moveToNextLine;
    TextMeshProUGUI button1Text;
    TextMeshProUGUI button2Text;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        button1Text = button1.GetComponentInChildren<TextMeshProUGUI>(true);
        button2Text = button2.GetComponentInChildren<TextMeshProUGUI>(true);

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
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        int index = 0;
        speakerNameText.SetText(data.TalkerName);
        moveToNextLine = true;

        while(index<data.dialogueLines.Length)
        {
            
            yield return new WaitUntil(() => moveToNextLine);

            moveToNextLine = false;
            dialogueText.SetText(data.dialogueLines[index]);
            if (data.choiceExist && index == data.dialogueLines.Length - 1)
            {
                Debug.Log(index);
                ShowButtons(data);
            }
            index++;
        }
        if(data.nextDialogueExist==false)
        {
            yield return new WaitUntil(() => moveToNextLine);

            dialogueUI.SetActive(false);
        }
        else
        {
            //사전에서 인덱스 찾아서 그걸로 다시 대화
        }

    }

    void ShowButtons(DialogueData data)
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button1Text.SetText(data.choiceButtonText[0]);
        button2Text.SetText(data.choiceButtonText[1]);
       
    }
    
}
