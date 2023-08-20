using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] private StageManager sm;

    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject[] rewardPrefabs;
    [SerializeField] private GameObject rewardBox;
    public void ViewReward()
    {
        rewardPanel.SetActive(true);
        InstantRewardGold();
    }

    private void InstantRewardGold()
    {
        var rewardGold = Instantiate(rewardPrefabs[0], this.transform, true);
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
        rewardGoldBtn.onClick.AddListener(()=>Destroy(rewardGold));


        rewardGold.transform.SetParent(rewardBox.transform);
    }

    private void RewardGold(int gold)
    {
        GameManager.instance.playerGold += gold;
        UIManager.instance.UpdateGoldUI();
        DataManager.instance.JsonSave();
    }
}
