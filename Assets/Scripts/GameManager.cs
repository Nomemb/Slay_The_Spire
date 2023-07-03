using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 게임 플레이 관련
    public CharacterType currentType;
    public bool isPlayerTurn = true;
    public PlayerController player;

    // 덱 관련
    public List<Card> fixedDeck = new List<Card>();         // 게임 내내 보유하고 있는 카드풀

    public List<Card> drawDeck = new List<Card>();          // 전투에서 사용할 카드 ( fixedDeck을 복사해 옴 )
    public List<Card> usedDeck = new List<Card>();          // 전투에서 사용한 카드
    public List<Card> expiredDeck = new List<Card>();       // 전투에서 소멸된 카드

    public List<Card> hand = new List<Card>();              // 손패

    // 플레이어 저장 스탯 관련
    public int playerHp;
    public int playerMaxHp;
    public int playerGold;
    public int playerPower;

    // 게임 규칙 관련
    [SerializeField] private int maxHandCount = 10;              // 최대로 들고있을 수 있는 카드의 수
    public int currentDrawCardCount = 5;


    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DataManager.instance.JsonLoad();
        //EventManager.instance.playerTurnEvent += DrawCard;
    }

    public void BattleStart()
    {
        drawDeck.Clear();                               // 혹시 모르니 비워 줌
        drawDeck = fixedDeck.ToList();                  // fixedDeck을 복사해 옴
        Shuffle(drawDeck);
        
        //EventManager.PlayerTurnEvent();

    }

    private void Shuffle(List<Card> shuffleDeck)
    {
        var count = shuffleDeck.Count;
        for(var i=0; i<count; i++)
        {
            var rand = Random.Range(0, shuffleDeck.Count);
            drawDeck.Add(shuffleDeck[rand]);
            shuffleDeck.RemoveAt(rand);
        }

        if (shuffleDeck.Equals(usedDeck))
        {
            shuffleDeck.Clear();
        }
    }
    public void DrawCard(int count)
    {
        while(count > 0)
        {
            if(hand.Count < maxHandCount)                       // 최대 손패보다 적을때만 적용됨.
            {
                if(drawDeck.Count > 0)                          // 뽑을 카드가 있으면
                {
                    hand.Add(drawDeck[0]);
                    drawDeck.RemoveAt(0);
                }
                else
                {
                    Shuffle(usedDeck);                                  // 사용된 덱을 셔플한 후 뽑을 카드에 넣음
                    DrawCard(count);
                }
            }
            else
            {
                return;
            }
            count--;
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
                currCharacter.InitCard();
                break;
            }

            case CharacterType.Silent:
            {
                Silent currCharacter = new Silent();
                currCharacter.SetCharacterStat();
                break;
            }
            case CharacterType.Defect:
            {
                Defect currCharacter = new Defect();
                currCharacter.SetCharacterStat();
                break;
            }
            case CharacterType.Watcher:
            {
                Watcher currCharacter = new Watcher();
                currCharacter.SetCharacterStat();
                break;
            }
        }
        DataManager.instance.JsonSave();
        SceneManager.LoadScene("BattleScene");
    }

    public void UseCard(int index)
    {
        hand[index].UseCard();
        hand.RemoveAt(index);
    }
}
