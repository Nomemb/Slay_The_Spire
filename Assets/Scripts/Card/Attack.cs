using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : Card
{
    protected override void ActivationCard(BaseMonster target = null)
    {
        if (target == null)
        {
            Debug.Log("타겟 설정되지 않음");
        }
        else
        {
            Debug.Log("타겟 : " + target.name);
            int onDamage = cardData.Damage + gm.playerPower;
            Debug.Log(onDamage);
            target.OnDamage(onDamage);
        }
    }
}
