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

    private void Start()
    {
        isPlayerTurn = GameManager.instance.isPlayerTurn;
        characterType = GameManager.instance.currentType;
        TurnStart();
    }

    public void TurnStart()
    {
        playerTurnStart.Invoke();
        Debug.Log("플레이어 턴 시작!");
    }
    public void TurnEnd()
    {
        GameManager.instance.isPlayerTurn = false;
        playerTurnEnd.Invoke();
        Debug.Log("플레이어 턴 종료!");
    }
}
