using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lament : Relic
{
    private bool isActivated = false;
    protected override void Start()
    {
        base.Start();
        Debug.Log("Lament 추가");
        tm.startBattle.AddListener(ActivateRelic);
    }

    protected override void ActivateRelic()
    {
        if (count == 0)
        {
            tm.startBattle.RemoveListener(ActivateRelic);
            return;
        }
        Debug.Log(tm.monsterList.Count);
        foreach (var monster in tm.monsterList)
        {
            monster.OnDamage(monster.MaxHp - 1);
        }

        count--;
        countText.text = count.ToString();

    }
}
