using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrugItOff : Defend
{
    protected override void ActivationCard(BaseMonster target = null)
    {
        GameManager.instance.DrawCard(1);
        base.ActivationCard(target);
    }
}
