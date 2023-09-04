using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] private StageManager sm;

    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject rewardCardPanel;
    [SerializeField] private GameObject selectNewCards;
    
    [SerializeField] private List<GameObject> concealedUI;
    [SerializeField] private GameObject[] rewardPrefabs;
    [SerializeField] private GameObject rewardBox;
    public void ViewReward()
    {
        SetUI(false);
        rewardPanel.SetActive(true);
        InstantRewardGold();
        InstantRewardCard();
    }

    private void InstantRewardGold()
    {
        var rewardGold = ObjectPool.GetObject();
        var amountGold = rewardGold.GetComponentInChildren<Text>();
        var rewardGoldBtn = rewardGold.GetComponentInChildren<Button>();

        var stageType = sm.currentStage.encounterType;
        var gold = 0;
        switch (stageType)
        {
            case EncounterType.Normal:
                gold = Random.Range(10, 21);
                break;
            case EncounterType.Elite:
                gold = Random.Range(25, 36);
                break;
            case EncounterType.Boss:
                gold = Random.Range(95, 106);
                break;
            default:
                break;
        }
        // if(relic) 처리해야함 ( 차후에 )
        amountGold.text = gold.ToString() + " 골드";
        rewardGoldBtn.onClick.AddListener(()=>RewardGold(gold));
        rewardGoldBtn.onClick.AddListener(()=>ObjectPool.ReturnObject(rewardGold));
        

        rewardGold.transform.SetParent(rewardBox.transform);
    }

    private void InstantRewardCard()
    {
        var rewardCard = ObjectPool.GetObject();
        var rewardText = rewardCard.GetComponentInChildren<Text>();
        var rewardCardBtn = rewardCard.GetComponentInChildren<Button>();

        rewardText.text = "카드 획득";

        rewardCardBtn.onClick.AddListener(RewardCard);
        rewardCardBtn.onClick.AddListener(()=>ObjectPool.ReturnObject(rewardCard));
        
        rewardCard.transform.SetParent(rewardBox.transform);
    }

    private void RewardGold(int gold)
    {
        GameManager.instance.playerGold += gold;
        UIManager.instance.UpdateGoldUI();
        DataManager.instance.JsonSave();
    }

    private void RewardCard()
    {
        for (int i = 0; i < 3; i++)
        {
            var newCard = Instantiate(GameManager.instance.GetNewCard(), selectNewCards.transform, true);
            newCard.GetComponent<CardPointEvent>().enabled = false;
            Button newCardBtn = newCard.AddComponent<Button>() as Button;
            
            // newCardBtn.onClick.AddListener(); 선택하면 나머지 선택지를 다 지우고 fixedDeck에 해당 카드를 추가하는 함수
        }
        
        rewardPanel.SetActive(false);
        rewardCardPanel.SetActive(true);


    }

    public void ClearReward()
    {
        for (int i = 0; i < rewardBox.transform.childCount; i++)
        {
            ObjectPool.ReturnObject(rewardBox.transform.GetChild(0).gameObject);
        }
    }

    public void SetUI(bool active)
    {
        foreach (var ui in concealedUI)
        {
            ui.SetActive(active);
        }
    }
}
