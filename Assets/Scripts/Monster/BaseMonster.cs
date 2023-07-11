using System;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Attack,
    Defense,
    Buff,
    DeBuff
}
public abstract class BaseMonster : MonoBehaviour
{
    public int Hp { get; protected set; }
    public int MaxHp { get; protected set; }
    public int AddedStrength { get; protected set; }
    public int Damage { get; protected set; }
    public int Block { get; protected set; }

    protected TurnManager tm;
    [SerializeField] protected MonsterState currentState;
    [SerializeField] protected MonsterState prevState;
    [SerializeField] protected int sameStateCount;

    [SerializeField] private HpInteraction hpInter;
    
    protected virtual void Start()
    {
        currentState = MonsterState.Idle;
        ChangeNextState();
        tm = FindObjectOfType<TurnManager>();
        tm.monsterList.Add(this);
        hpInter = GetComponentInChildren<HpInteraction>();
        hpInter.UpdateHpBar(Hp, MaxHp);
    }

    public void DoingCurrentState()
    {
        // Doing Something;
        switch (currentState)
        {
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Buff:
                Buff();
                break;
            case MonsterState.Defense:
                Defense();
                break;
            case MonsterState.DeBuff:
                DeBuff();
                break;
            
            case MonsterState.Idle:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        ChangeNextState();
    }

    protected virtual void ChangeNextState()
    {
    }
    protected virtual void Attack()
    {
        Debug.Log("Attack ");
        GameManager.instance.playerHp -= (AddedStrength + Damage);
        tm.ChangedPlayerHp();
    }

    protected virtual void Defense()
    {
        
    }

    protected virtual void Buff()
    {
        
    }

    protected virtual void DeBuff()
    {
        
    }

    public virtual void OnDamage(int onDamage)
    {
        Hp -= onDamage;
        hpInter.UpdateHpBar(Hp, MaxHp);
        if (Hp <= 0)
        {
            tm.monsterList.Remove(this);
            
        }
    }
}
