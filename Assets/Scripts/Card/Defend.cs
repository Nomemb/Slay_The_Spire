using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Skill
{
    protected override void ActivationCard(BaseMonster target = null)
    {
        gm.block += cardData.Defense;
        TurnManager.instance.ChangedPlayerHp();
        base.ActivationCard(target);
    }
}
