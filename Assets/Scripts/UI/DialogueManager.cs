using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public interface ITalkable
{
    
    public void Talk();
    int TalkStep {  get; }
}

public interface IQuest
{
    //이렇게 만들고
    public void AcceptQuest();
    //작동은 이 인터페이스 상속받은 NPC가 하게하기
    //내용도 그냥 다 NPC에 넣기
    public void CheckQuestCondition();

    bool IsQuestAccepted { get; }

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
    BaseNPC npc;


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
    public void SetTalk(DialogueData data,BaseNPC baseNPC)
    {
        
        StartCoroutine(Talk(data));
        Debug.Log("일반 대화");
        npc=baseNPC;
    }

    

    IEnumerator Talk(DialogueData data)
    {
        //CharacterManager.Instance.Player.controller.ToggleCursor();
        dialogueUI.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        int index = 0;
        speakerNameText.SetText(data.TalkerName);
        moveToNextLine = true;
        Coroutine currentCoroutine = null;

        while (index<data.dialogueLines.Length)
        {
            
            yield return new WaitUntil(() => moveToNextLine);

            moveToNextLine = false;
            if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                
            }
            dialogueText.text=string.Empty;
            currentCoroutine= StartCoroutine(SetTypingEffect( data.dialogueLines[index]));
            
            if (data.choiceExist && index == data.dialogueLines.Length - 1)
            {
                Debug.Log(index);
                ShowButtons(data);
            }
            index++;
        }

        //버튼이 없으면 여기서 종료

        //버튼이 있으면 클릭시 종료
        if(data.nextDialogueExist==false&&(data.choiceButtonText.Length==0||data.choiceButtonText==null))
        {
            if(data.choiceButtonText==null)
            {
                Debug.Log("배열자체가 null");
            }
            else if(data.choiceButtonText.Length==0)
            {
                Debug.Log("배열 길이가 0");
            }
                yield return new WaitUntil(() => moveToNextLine);
            CloseDialogue();
            
        }
        else
        {
            //딕셔너리에서 다음 ID번호로 다음 데이터 가져옴
            
        }
        

    }
    void CloseDialogue()
    {
        dialogueUI.SetActive(false); //문제있음
        CharacterManager.Instance.Player.controller.ToggleCursor();
    }

    void MoveToNextDialogue()
    {
        //딕셔너리에서 다음 데이터 찾음
    }

    void TestDialogueYes()
    {
        IQuest iquest = npc.GetComponentInChildren<IQuest>();
        
        if (iquest!=null)
        {
            Debug.Log("초기 조건 만족");
            
            if(iquest.IsQuestAccepted==false)
            {
                iquest.AcceptQuest();
            }
            else
            {
                iquest.CheckQuestCondition();
            }
        }
        
        MoveToNextDialogue();
        CloseDialogue() ;
        
    }

    void TestDialogueNo()
    {
        CloseDialogue();
    }

    void ShowButtons(DialogueData data)
    {
        
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button1Text.SetText(data.choiceButtonText[0]);
        button2Text.SetText(data.choiceButtonText[1]);
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button1.onClick.AddListener(TestDialogueYes);
        button2.onClick.AddListener(TestDialogueNo);
       //중요한거는 눌렀을때 대화마다 다른 것이 시행되어야해
       //그리고 어떤건 대화로 이어지고 어떤건 퀘스트로 이어지고
       
    }

    IEnumerator SetTypingEffect(string text)
    {
        
        foreach(char letter  in text)
        {
            yield return null;
            dialogueText.text += letter;
        }
    }
    
}
