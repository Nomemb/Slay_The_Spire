using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public characterType currentType;

    // 게임 저장 관련
    public List<string> testDataA = new List<string>();
    public List<int> testDataB = new List<int>();

    public int playerGold;
    public int playerPower;

    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Exit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void Embark()
    {
        Debug.Log(currentType.ToString() + " 캐릭터로 게임 시작");
        SceneManager.LoadScene("BattleScene");
        
    }
}
