using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleScene : MonoBehaviour
{
    [Header("Deck")] 
    public Button drawDeckButton;

    public Button usedDeckButton;
    public Button expiredDeckButton;
    
    public Text drawDeckCount;
    public Text usedDeckCount;
    public Text expiredDeckCount;

    [Header("Button")] 
    public Button endTurnButton;
    
    [Header("Cursor")] 
    public GameObject cursorObject;
    public Image imageCursor;

    [Header("BattleUI")]
    private Camera camera;

    private void Start()
    {
        Init();
        endTurnButton.onClick.AddListener(TurnManager.instance.EndPlayerTurn);
    }

    public void Init()
    {
        camera = Camera.main;

        drawDeckCount.text = GameManager.instance.drawDeck.Count.ToString();
        usedDeckCount.text = GameManager.instance.usedDeck.Count.ToString();
        expiredDeckCount.text = GameManager.instance.expiredDeck.Count.ToString();
    }

    public void UpdateCardCount()
    {
        
        expiredDeckButton.gameObject.SetActive(GameManager.instance.expiredDeck.Count > 0);
        drawDeckCount.text = GameManager.instance.drawDeck.Count.ToString();
        usedDeckCount.text = GameManager.instance.usedDeck.Count.ToString();
        expiredDeckCount.text = GameManager.instance.expiredDeck.Count.ToString();
    }
}
