using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Neow : MonoBehaviour
{
    // 게임 상태 관련 변수들
    [SerializeField] private bool isFirstBossMeet;          // 첫 보스를 만났는가?

    private RelicManager rm;
    // 생성 관련 변수들
    public GameObject interaction;
    public GameObject buttonPrefab;
    
    // 창 처리
    [SerializeField] private GameObject neow;
    [SerializeField] private GameObject battle;
    [SerializeField] private GameObject characterBattleUI;


    private void Start()
    {
        rm = RelicManager.instance;
        
        if (isFirstBossMeet)                                // UI 순서때문에 역순으로 생성
        {
            GenerateForthOption();
            GenerateThirdOption();
            GenerateSecondOption();
            GenerateFirstOption();
        }
        else
        {
            ClearFailedOption();
        }
    }

    private void GenerateFirstOption()
    {
        var newButton = Instantiate(buttonPrefab, interaction.transform);
        

    }

    private void GenerateSecondOption()
    {
        
    }

    private void GenerateThirdOption()
    {
        
    }

    private void GenerateForthOption()
    {
        
    }

    private void ClearFailedOption()
    {
        var newButton2 = Instantiate(buttonPrefab, interaction.transform);
        var btn2 = newButton2.GetComponent<Button>();
        var text2 = newButton2.GetComponentInChildren<Text>();

        if (GameManager.instance == null) return;
        var addMaxHealth = Mathf.RoundToInt(GameManager.instance.playerMaxHp / 10);
        text2.text = "[ 최대 체력 + " + addMaxHealth + " 증가 ]";
        btn2.onClick.AddListener(()=>AddMaxHealth(addMaxHealth));
        btn2.onClick.AddListener(BattleStart);
        
        var newButton = Instantiate(buttonPrefab, interaction.transform);
        var btn = newButton.GetComponent<Button>();
        var text = newButton.GetComponentInChildren<Text>();

        text.text = "[ 앞으로 3번의 전투 동안 적의 체력이 1이 됩니다 ]";
        btn.onClick.AddListener(()=>rm.GetRelic("Lament"));
        btn.onClick.AddListener(BattleStart);
    }

    private void BattleStart()
    {
        DataManager.instance.JsonSave();
        characterBattleUI.SetActive(true);
        battle.SetActive(true);
        neow.SetActive(false);
        TurnManager.instance.StartBattle();
    }

    private void AddMaxHealth(int addMaxHealth)
    {
        GameManager.instance.playerMaxHp += addMaxHealth;
        GameManager.instance.playerHp = GameManager.instance.playerMaxHp;
    }
}
