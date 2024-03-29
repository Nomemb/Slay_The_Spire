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
        GameObject rewardGold = ObjectPool.GetObject();
        Text amountGold = rewardGold.GetComponentInChildren<Text>();
        Button rewardGoldBtn = rewardGold.GetComponentInChildren<Button>();

        EncounterType stageType = sm.currentStage.encounterType;
        int gold = 0;
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
        rewardGoldBtn.onClick.RemoveAllListeners();
        rewardGoldBtn.onClick.AddListener(()=>RewardGold(gold));
        rewardGoldBtn.onClick.AddListener(()=>ObjectPool.ReturnObject(rewardGold));
        

        rewardGold.transform.SetParent(rewardBox.transform);
    }

    private void InstantRewardCard()
    {
        GameObject rewardCard = ObjectPool.GetObject();
        Text rewardText = rewardCard.GetComponentInChildren<Text>();
        Button rewardCardBtn = rewardCard.GetComponentInChildren<Button>();

        rewardText.text = "카드 획득";
        
        rewardCardBtn.onClick.RemoveAllListeners();
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
            KeyValuePair<GameObject, int> newCardPair = GameManager.instance.GetNewCard();
            
            GameObject newCard = Instantiate(newCardPair.Key, selectNewCards.transform, true);
            newCard.GetComponent<CardPointEvent>().enabled = false;
            Button newCardBtn = newCard.AddComponent<Button>() as Button;
            
            newCardBtn.onClick.AddListener(()=>ChooseCard(newCardPair.Value));
            newCardBtn.onClick.AddListener(DataManager.instance.JsonSave);
            newCardBtn.onClick.AddListener(UIManager.instance.UpdateDeckCountUI);
            newCardBtn.onClick.AddListener(ClearCardReward);
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

    public void ChooseCard(int index)
    {
        GameManager.instance.AddNewCard(index);
    }
    private void ClearCardReward()
    {
        foreach (Transform card in selectNewCards.transform)
        {
            Destroy(card.gameObject);
        }
        
        rewardPanel.SetActive(true);
        rewardCardPanel.SetActive(false);
    }

    public void SetUI(bool active)
    {
        foreach (var ui in concealedUI)
        {
            ui.SetActive(active);
        }
    }
}
