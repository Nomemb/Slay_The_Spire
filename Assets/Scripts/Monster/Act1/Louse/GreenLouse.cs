using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLouse : Louse
{

    protected override void Start()
    {
        Hp = Random.Range(11, 18);
        Damage = Random.Range(5, 8);
        base.Start();
    }

    protected override void ChangeNextState()
    {
        prevState = currentState;
        if (GameManager.instance.ascensionLevel >= 17)
        {
            if (prevState == MonsterState.DeBuff) currentState = MonsterState.Attack;
            sameStateCount = 0;
            return;
        }
        
        var nextState = Random.Range(1, 101);
        currentState = nextState <= 25 ? MonsterState.DeBuff : MonsterState.Attack;

        sameStateCount = prevState == currentState ? sameStateCount+1 : 0;

        if (sameStateCount == 3)
        {
            currentState = currentState == MonsterState.Attack ? MonsterState.DeBuff : MonsterState.Attack;
            sameStateCount = 0;
        }
    }
}
