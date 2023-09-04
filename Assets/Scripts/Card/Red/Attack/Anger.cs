using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anger : Attack
{
    protected override void ActivationCard(BaseMonster target = null)
    {
        base.ActivationCard(target);
        AddCardToUsedDeck();
    }
}
