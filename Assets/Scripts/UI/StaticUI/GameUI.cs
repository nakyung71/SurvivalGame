using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : StaticUI
{

    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition thirst;
    public Condition temperature;
    // Start is called before the first frame update
    private void Start()
    {
        
    
    CharacterManager.Instance.Player.condition.gameUI = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
