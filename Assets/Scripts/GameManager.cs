using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 게임 플레이 관련
    public characterType currentType;
    public bool isPlayerTurn = true;

    // 게임 저장 관련
    public List<string> cardData = new List<string>();

    public int playerHp;
    public int playerMaxHp;
    public int playerGold;
    public int playerPower;



    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DataManager.instance.JsonLoad();
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
