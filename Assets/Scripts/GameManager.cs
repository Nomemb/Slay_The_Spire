using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 게임 플레이 관련
    public characterType currentType;
    public bool isPlayerTurn = true;

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


    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DataManager.instance.JsonLoad();
    }

    public void BattleStart()
    {
        drawDeck.Clear();                               // 혹시 모르니 비워 줌
        drawDeck = fixedDeck.ToList();                  // fixedDeck을 복사해 옴
    }

    private void Shuffle()
    {
        int count = usedDeck.Count;
        for(int i=0; i<count; i++)
        {
            int rand = Random.Range(0, usedDeck.Count);
            drawDeck.Add(usedDeck[rand]);
            usedDeck.RemoveAt(rand);
        }
        usedDeck.Clear();
    }
    public void DrawCard(int count)
    {
        while(count > 0)
        {
            if(hand.Count < maxHandCount)                   // 최대 손패보다 적을때만 적용됨.
            {
                if(drawDeck.Count > 0)
                {
                    hand.Add(drawDeck[0]);
                    drawDeck.RemoveAt(0);
                }
                else
                {
                    Shuffle();
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
            case characterType.Ironclad:
            {
                Ironclad currCharacter = new Ironclad();
                currCharacter.SetCharacterStat();
                break;
            }

            case characterType.Silent:
            {
                Silent currCharacter = new Silent();
                currCharacter.SetCharacterStat();
                break;
            }
            case characterType.Defect:
            {
                Defect currCharacter = new Defect();
                currCharacter.SetCharacterStat();
                break;
            }
            case characterType.Watcher:
            {
                Watcher currCharacter = new Watcher();
                currCharacter.SetCharacterStat();
                break;
            }
        }
        DataManager.instance.JsonSave();
        SceneManager.LoadScene("BattleScene");
    }
}
