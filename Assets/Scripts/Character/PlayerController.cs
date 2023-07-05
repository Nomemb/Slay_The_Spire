using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterType characterType;

    [SerializeField] bool isPlayerTurn;

    public UnityEvent playerTurnStart;
    public UnityEvent playerTurnEnd;
    public UnityEvent playerCardCountChange;

    private void Start()
    {
        isPlayerTurn = GameManager.instance.isPlayerTurn;
        characterType = GameManager.instance.currentType;
        TurnStart();
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
}
