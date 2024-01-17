using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDebuff : Attack, IDebuffable
{
    public void Debuff(GameObject target = null)
    {
        if (target == null)
        {
            Debug.Log("타겟 설정되지 않음");
            return;
        }
        DebuffSystem debuffSystem = target.GetComponent<DebuffSystem>();
        debuffSystem.AddShareDebuff("Vulnerable", cardData.DebuffDuration);
    }

    protected override void ActivationCard(BaseMonster target = null)
    {
        base.ActivationCard(target);
        if (target != null) Debuff(target.gameObject);
    }
}
