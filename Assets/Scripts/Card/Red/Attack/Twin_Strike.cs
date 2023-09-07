using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twin_Strike : Strike
{
    protected override void ActivationCard(BaseMonster target = null)
    {
        base.ActivationCard(target);
        base.ActivationCard(target);
    }
}
