using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    [SerializeField] private CharacterType characterType;
    [SerializeField] bool isPlayerTurn;
    [SerializeField] private CardDisplay hand;
    [SerializeField] private GameManager gm;
    [SerializeField] private UIManager um;
    

    public UnityEvent startBattle;
    public UnityEvent startPlayerTurn;
    public UnityEvent endPlayerTurn;
    public UnityEvent startEnemyTurn;
    public UnityEvent changePlayerCardCount;
    public UnityEvent changePlayerHp;

    private void Start()
    {
        isPlayerTurn = GameManager.instance.isPlayerTurn;
        characterType = GameManager.instance.currentType;
        gm = FindObjectOfType<GameManager>();
        um = FindObjectOfType<UIManager>();
        EventSetting();
        
        StartBattle();
    }

    private void EventSetting()
    {
        // StartBattle Event
        startBattle.AddListener(gm.BattleStart);
        startBattle.AddListener(()=>gm.DrawCard(gm.currentDrawCardCount));
        startBattle.AddListener(hand.ImageSetting);
        startBattle.AddListener(um.UpdateDeckCountUI);
        
        // StartPlayerTurn Event
        startPlayerTurn.AddListener(()=>gm.DrawCard(gm.currentDrawCardCount));
        startPlayerTurn.AddListener(hand.ImageSetting);
        startPlayerTurn.AddListener(um.UpdateDeckCountUI);
        
        // EndPlayerTurn Event
        endPlayerTurn.AddListener(gm.EndPlayerTurn);
        endPlayerTurn.AddListener(hand.EndPlayerTurn);
        endPlayerTurn.AddListener(um.UpdateCardCount);
        endPlayerTurn.AddListener(StartEnemyTurn);
        
        // StartEnemyTurn Event
        
        // ChangePlayerCardCount Event
        changePlayerCardCount.AddListener(um.UpdateCardCount);
        
        // ChangedPlayerHp Event
        changePlayerHp.AddListener(um.UpdateHpUI);

    }
    public void StartBattle()
    {
        Debug.Log("전투 시작!");

        startBattle.Invoke();
    }
    public void StartPlayerTurn()
    {
        Debug.Log("플레이어 턴 시작!");

        startPlayerTurn.Invoke();
    }
    
    public void EndPlayerTurn()
    {
        if (!isPlayerTurn) return;
        
        Debug.Log("플레이어 턴 종료!");
        gm.isPlayerTurn = false;

        endPlayerTurn.Invoke();
    }

    public void StartEnemyTurn()
    {
        if (gm.isPlayerTurn) return;
        
        Debug.Log("상대 턴 시작!");
        //startEnemyTurn.AddListener( EnemyDoing() );
        Debug.Log("Doing Something..");
        Debug.Log("상대 턴 종료!");
        
        gm.isPlayerTurn = true;
        // startEnemyTurn.AddListener(StartPlayerTurn);
        
        startEnemyTurn.Invoke();
    }

    public void ChangePlayerCardCount()
    {
        Debug.Log("플레이어 드로우!");
        changePlayerCardCount.Invoke();
    }

    public void ChangedPlayerHp()
    {
        Debug.Log("플레이어 HP 변경!");
        changePlayerHp.Invoke();
    }
}
