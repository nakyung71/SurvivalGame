using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestNPC : MonoBehaviour,ITalkable
{
    [SerializeField] DialogueData data;
    public DialogueData DialogueData => data;

   
    public void Talk()
    {
        DialogueManager.Instance.SetTalk(data);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Talk();
        }
    }
}
