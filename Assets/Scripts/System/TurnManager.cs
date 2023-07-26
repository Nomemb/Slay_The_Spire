using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private HpInteraction playerHpUi;
    

    public UnityEvent startBattle;
    public UnityEvent startPlayerTurn;
    public UnityEvent endPlayerTurn;
    public UnityEvent startEnemyTurn;
    public UnityEvent changePlayerCardCount;
    public UnityEvent changePlayerHp;

    public List<BaseMonster> monsterList = null;
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
        startBattle.AddListener(um.Init);
        startBattle.AddListener(StartPlayerTurn); 
        
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
        changePlayerHp.AddListener(()=>playerHpUi.UpdateHpBar(gm.playerHp, gm.playerMaxHp));

    }
    private void StartBattle()
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
        endPlayerTurn.Invoke();
    }

    private void StartEnemyTurn()
    {
        if (gm.isPlayerTurn) return;
        
        Debug.Log("상대 턴 시작!");
        startEnemyTurn.RemoveAllListeners();
        foreach (var monster in monsterList)
        {
            startEnemyTurn.AddListener(monster.DoingCurrentState);
        }

        gm.isPlayerTurn = true;
        gm.currentMana = gm.maxMana;
        startEnemyTurn.AddListener(StartPlayerTurn);
        
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
