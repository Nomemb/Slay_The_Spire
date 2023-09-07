using System;
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
        currentCount = maxCount;
        Debug.Log("Lament 추가");
    }

    public override void ActivateRelic()
    {
        Debug.Log("Lament Activation");
        if (currentCount == 0) return;
  
        Debug.Log(tm.monsterList);
        foreach (var monster in tm.monsterList)
        {
            monster.OnDamage(monster.MaxHp - 1);
        }

        currentCount--;
        countText.text = currentCount.ToString();

    }
}
