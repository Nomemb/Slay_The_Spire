using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLouse : Louse
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Hp = Random.Range(10, 16);
        Damage = Random.Range(5, 8);
        base.Start();
    }
    protected override void ChangeNextState()
    {
        prevState = currentState;
        if (GameManager.instance.ascensionLevel >= 17)
        {
            if (prevState == MonsterState.Buff) currentState = MonsterState.Attack;
            sameStateCount = 0;
            return;
        }
        
        var nextState = Random.Range(1, 101);
        currentState = nextState <= 25 ? MonsterState.Buff : MonsterState.Attack;

        sameStateCount = prevState == currentState ? sameStateCount+1 : 0;
        if (sameStateCount == 3)
        {
            currentState = currentState == MonsterState.Attack ? MonsterState.Buff : MonsterState.Attack;
            sameStateCount = 0;
        }

        base.ChangeNextState();
    }

    protected override void Buff()
    {
        base.Buff();
        AddedStrength = GameManager.instance.ascensionLevel >= 17 ? AddedStrength + 4 : AddedStrength + 3;
    }
}
