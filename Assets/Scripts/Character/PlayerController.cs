using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private characterType characterType;

    [SerializeField] bool isPlayerTurn;

    private void Start()
    {
        isPlayerTurn = GameManager.instance.isPlayerTurn;
        characterType = GameManager.instance.currentType;
    }
    private void Update()
    {
        if(GameManager.instance.isPlayerTurn)
        {
            TurnStart();
        }
    }

    private void TurnStart()
    {

    }
    public void TurnEnd()
    {
        GameManager.instance.isPlayerTurn = false;
    }
}
