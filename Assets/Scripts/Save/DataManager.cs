using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [SerializeField] private string path;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        path = Path.Combine(Application.dataPath + "/PlayData/", "database.json");     // dataPath는 프로젝트 디렉토리/Assets
    }

    public void JsonLoad()
    {
        Debug.Log("JsonLoad");

        var loadJson = File.ReadAllText(path);
        var saveData = JsonUtility.FromJson<SaveData>(loadJson);

        if (saveData != null)
        {
            GameManager.instance.fixedDeck.Clear();
            foreach (var card in saveData.cardData)
            {
                GameManager.instance.fixedDeck.Add(card);
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
        saveData.cardData.Clear();
        foreach (var card in GameManager.instance.fixedDeck)
        {
            saveData.cardData.Add(card);
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
