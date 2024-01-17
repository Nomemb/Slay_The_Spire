using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JawWormPattern
{
    Chomp,
    Thrash,
    Bellow,
    Idle
}
public class JawWorm : BaseMonster
{
    private int bellowPower = 3;
    private int bellowBlock = 6;
    private int chompDamage = 11; 
    private JawWormPattern current;
    
    private bool isFirstTurn = true;
    protected override void Start()
    {
        hp = Random.Range(40, 45);
        SetStatsByAscensionLevel();
        base.Start();
        current = JawWormPattern.Idle;
    }

    protected override void SetStatsByAscensionLevel()
    {
        if (ascensionLevel >= 2)
        {
            chompDamage++;
            bellowPower++;
        }

        if (ascensionLevel >= 7)
        {
            hp += 2;
        }

        if (ascensionLevel >= 17)
        {
            bellowPower++;
            bellowBlock += 3;
        }

    }

    protected override void ChangeNextState()
    {
        prevState = currentState;
        if (isFirstTurn) // 첫 턴에는 무조건 Chomp
        {
            isFirstTurn = !isFirstTurn;
            currentState = MonsterState.Attack;
            current = JawWormPattern.Chomp;
        }
        else
        {
            var nextState = Random.Range(1, 101);
            if (nextState <= 45)
            {
                // 버프는 두번 중복 불가
                if (current == JawWormPattern.Bellow)
                {
                    ChangeDuplicateState(30, 25, JawWormPattern.Thrash, JawWormPattern.Chomp);
                    return;
                }
                currentState = MonsterState.Buff;
                current = JawWormPattern.Bellow;

                sameStateCount = 0;
            }
            else if (nextState <= 75)
            {
                // Thrash는 세번 연속 불가
                sameStateCount = current == JawWormPattern.Thrash? sameStateCount + 1 : 0;

                if (sameStateCount == 3)
                {                    
                    ChangeDuplicateState(45, 25, JawWormPattern.Bellow, JawWormPattern.Chomp);
                    return;
                }
                currentState = MonsterState.Attack;
                current = JawWormPattern.Thrash;
            }
            else
            {
                sameStateCount = current == JawWormPattern.Chomp? sameStateCount + 1 : 0;
                if (sameStateCount == 2)
                {
                    ChangeDuplicateState(45, 30, JawWormPattern.Bellow, JawWormPattern.Thrash);
                    return;
                }
                currentState = MonsterState.Attack;
                current = JawWormPattern.Chomp;
            }
        }
        ChangeSpriteForState();
    }

    private void ChangeDuplicateState(int aRate, int bRate, JawWormPattern aType, JawWormPattern bType)
    {
        sameStateCount = 0;
        int nextState = Random.Range(1, aRate + bRate + 1);
        current = nextState <= aRate ? aType : bType;
        currentState = current == JawWormPattern.Bellow ? MonsterState.Buff : MonsterState.Attack;
        ChangeSpriteForState();
    }

    private void ChangeSpriteForState()
    {
        imageCurrentState.sprite = stateSprites[(int)current];
        GetDamage();
        currentDamage = addedStrength + damage; 

        if (currentState == MonsterState.Attack)
        {
            textCurrentDamage.gameObject.SetActive(true);
            textCurrentDamage.text = currentDamage.ToString();
        }
        else
        {
            textCurrentDamage.gameObject.SetActive(false);
        }
    }
    protected override void Attack()
    {
        skAnim.AnimationState.SetAnimation(0, "chomp", false);
        GetDamage();
        base.Attack();
    }

    protected override void Buff()
    {
        addedStrength += bellowPower;
        block = bellowBlock;
        base.Buff();
    }

    private void GetDamage()
    {
        if (current == JawWormPattern.Chomp)
        {
            damage = chompDamage;
        }
        else if (current == JawWormPattern.Thrash)
        {
            damage = 7;
            block = 5;
            hpInter.UpdateBlockBar(block,hp,maxHp);
        }
    }
}
