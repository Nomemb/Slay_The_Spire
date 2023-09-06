using System;
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
            return;
        }
        
        Debug.Log("타겟 : " + target.name);
        // 약화 상태라면 ( 공격력 25% 감소 )
        cardDamage = cardData.Damage + gm.playerPower;
        if ((gm.player.dbS.sharedState & SharedDebuff.Weak) == SharedDebuff.Weak)
        {
            cardDamage = (int)Math.Floor((cardData.Damage + gm.playerPower) * 0.75);
        }
        
        SoundManager.instance.PlaySound("Attack");
        target.OnDamage(cardDamage);
    }
}
