using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public UnityEvent battleStart;
    public UnityEvent playerTurnStart;
    public UnityEvent playerTurnEnd;
    public UnityEvent playerCardCountChange;
    public UnityEvent playerHpChange;

    public UnityEvent relicAdded;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void BattleStart()
    {
        Debug.Log("스테이지 시작!");
        battleStart.Invoke();
    }
    public void TurnStart()
    {
        Debug.Log("플레이어 턴 시작!");
        playerTurnStart.Invoke();
    }
    public void TurnEnd()
    {
        Debug.Log("플레이어 턴 종료!");
        GameManager.instance.isPlayerTurn = false;
        playerTurnEnd.Invoke();
    }

    public void PlayerCardCountChange()
    {
        Debug.Log("플레이어 드로우!");
        playerCardCountChange.Invoke();
    }

    public void RelicAdded()
    {
        Debug.Log("유물 추가!");
        relicAdded.Invoke();

    }

    public void PlayerHpChange()
    {
        Debug.Log("플레이어 HP 변경");
        playerHpChange.Invoke();
    }

}
