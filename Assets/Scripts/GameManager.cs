using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 게임 플레이 관련
    [Header("GamePlay")]
    public CharacterType currentType;
    public bool isPlayerTurn = true;
    public PlayerController player;

    public bool onDrag;

    // 덱 관련
    [Space(10f)]
    [Header("Deck")]
    public List<GameObject> fixedDeck = new List<GameObject>();         // 게임 내내 보유하고 있는 카드풀

    public List<GameObject> drawDeck = new List<GameObject>();          // 전투에서 사용할 카드 ( fixedDeck을 복사해 옴 )
    public List<GameObject> usedDeck = new List<GameObject>();          // 전투에서 사용한 카드
    public List<GameObject> expiredDeck = new List<GameObject>();       // 전투에서 소멸된 카드

    public List<GameObject> hand = new List<GameObject>();              // 손패

    // 카드 데이터베이스
    [Space(10f)]
    [Header("Card Database")]
    [SerializeField] private CardDataBase cardDB;
    
    // 플레이어 저장 스탯 관련
    [Space(10f)]
    [Header("Player Stat")]
    public int playerHp;
    public int playerMaxHp;
    public int playerGold;
    public int playerPower;
    public int ascensionLevel;
 

    // 게임 규칙 관련
    [Space(10f)]
    [Header("Game Rule")]
    [SerializeField] private int maxHandCount = 10;              // 최대로 들고있을 수 있는 카드의 수
    public int currentDrawCardCount = 5;
    public int currentMana;
    public int maxMana;
    public int block;
    
    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        ascensionLevel = 0;
        cardDB = GetComponent<CardDataBase>();
    }

    public void BattleStart()
    {
        DataManager.instance.JsonLoad();
        Debug.Log("Gm. GameStart");
        isPlayerTurn = true;
        EndPlayerTurn();
        usedDeck.Clear();                               // 혹시 모르니 비워 줌
        drawDeck.Clear();
        usedDeck = fixedDeck.ToList();                  // fixedDeck을 복사해 옴
        currentMana = maxMana;
        Shuffle();
    }

    public void Shuffle()
    {
        var count = usedDeck.Count;
        for(var i=0; i<count; i++)
        {
            var rand = Random.Range(0, usedDeck.Count);
            drawDeck.Add(usedDeck[rand]);
            usedDeck.RemoveAt(rand);
        }
        usedDeck.Clear();
        TurnManager.instance.ChangePlayerCardCount();
    }
    public void DrawCard(int count)
    {
        if (hand.Count >= maxHandCount) // 최대 손패보다 적을때만 적용됨.
        {
            return;
        }
        
        for (var i = 0; i < count; i++)
        {
            if (drawDeck.Count == 0)
            {
                Shuffle();
                DrawCard(count - i);
                return;
            }
        
            hand.Add(drawDeck[0]);
            drawDeck.RemoveAt(0);
            TurnManager.instance.ChangePlayerCardCount();
        }
    }

    public void Exit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void Embark()
    {
        Debug.Log(currentType.ToString() + " 캐릭터로 게임 시작");
        switch (currentType)
        {
            case CharacterType.Ironclad:
            {
                Ironclad currCharacter = new Ironclad();
                currCharacter.SetCharacterStat();
                currCharacter.GetInitRelic();
                break;
            }

            case CharacterType.Silent:
            {
                Silent currCharacter = new Silent();
                currCharacter.SetCharacterStat();
                currCharacter.GetInitRelic();
                break;
            }
            case CharacterType.Defect:
            {
                Defect currCharacter = new Defect();
                currCharacter.SetCharacterStat();
                currCharacter.GetInitRelic();
                break;
            }
            case CharacterType.Watcher:
            {
                Watcher currCharacter = new Watcher();
                currCharacter.SetCharacterStat();
                currCharacter.GetInitRelic();
                break;
            }
        }   
        fixedDeck.Clear();
        cardDB.InitCard();
        DataManager.instance.JsonSave();
        SceneManager.LoadScene("BattleScene");
    }

    public void StartPlayerTurn()
    {
        block = 0;
    }
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        while (hand.Count != 0)
        {
            usedDeck.Add(hand[0]);
            hand.RemoveAt(0);
        }
    }

    public GameObject GetNewCard()
    {
        return cardDB.AddNewCard();
    }
    
}
