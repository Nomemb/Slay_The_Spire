using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private string path;

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
        path = Path.Combine(Application.dataPath + "/PlayData/", "database.json");     // datapath는 프로젝트 디렉토리/Assets
    }

    public void JsonLoad()
    {
        Debug.Log("JsonLoad");
        SaveData saveData = new SaveData();

        string loadJson = File.ReadAllText(path);
        saveData = JsonUtility.FromJson<SaveData>(loadJson);

        if (saveData != null)
        {
            for (int i = 0; i < saveData.cardData.Count; i++)
            {
                GameManager.instance.fixedDeck.Add(saveData.cardData[i]);
            }

            GameManager.instance.currentType = saveData.characterType;
            GameManager.instance.playerHp = saveData.hp;
            GameManager.instance.playerMaxHp = saveData.maxHp;
            GameManager.instance.playerGold = saveData.gold;
            GameManager.instance.playerPower = saveData.addedStrength;
        }
        //}
    }

    // 캐릭터 선택 후 출정 누르면 실행됨.
    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        for(int i=0; i < GameManager.instance.fixedDeck.Count; i++)
        {
            saveData.cardData.Add(GameManager.instance.fixedDeck[i]);
        }

        saveData.characterType = GameManager.instance.currentType;
        saveData.hp = GameManager.instance.playerHp;
        saveData.maxHp = GameManager.instance.playerMaxHp;
        saveData.gold = GameManager.instance.playerGold;
        saveData.addedStrength = GameManager.instance.playerPower;

        string json = JsonUtility.ToJson(saveData, true);           // true로 읽기 쉽게 표시

        File.WriteAllText(path, json);
    }    
}
