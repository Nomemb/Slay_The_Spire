using System;
using UnityEngine;
using UnityEngine.UI;

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
    [field: Header("Stats")]
    [field:SerializeField] protected int Hp { get; set; }
    [field:SerializeField] private int MaxHp { get; set; }
    [field:SerializeField] protected int AddedStrength { get; set; }
    [field:SerializeField] protected int Damage { get; set; }
    [field:SerializeField] protected int Block { get; set; }

    private TurnManager tm;
    
    [Space(10f)]
    [Header("States")]
    [SerializeField] protected MonsterState currentState;
    [SerializeField] protected MonsterState prevState;
    [SerializeField] protected int sameStateCount;

    [Space(10f)]
    [Header("UI")]
    [SerializeField] private HpInteraction hpInter;
    [Tooltip("0: Idle, 1: Attack, 2: Defense, 3: Buff, 4: Debuff")]
    [SerializeField] protected Sprite[] stateSprites;
    public Image ImageCurrentState;
    public Text TextCurrentDamage;
    
    protected virtual void Start()
    {
        currentState = MonsterState.Idle;
        ChangeNextState();
        tm = FindObjectOfType<TurnManager>();
        tm.monsterList.Add(this);
        MaxHp = Hp;
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
        ImageCurrentState.sprite = stateSprites[(int)currentState];
        
        if (currentState == MonsterState.Attack)
        {
            TextCurrentDamage.gameObject.SetActive(true);
            TextCurrentDamage.text = (AddedStrength+Damage).ToString();
        }
        else
        {
            TextCurrentDamage.gameObject.SetActive(false);
        }
    }
    protected virtual void Attack()
    {
        var currentDamage = AddedStrength + Damage;
        Debug.Log("Attack " + currentDamage);
        GameManager.instance.playerHp -= currentDamage;
        tm.ChangedPlayerHp();
    }

    protected virtual void Defense()
    {
        Debug.Log("Defense " + Block);
    }

    protected virtual void Buff()
    { 
        Debug.Log("Buff");

    }

    protected virtual void DeBuff()
    {
        Debug.Log("DeBuff");

    }

    public virtual void OnDamage(int onDamage)
    {
        Hp = Math.Max(Hp - onDamage, 0);
        hpInter.UpdateHpBar(Hp, MaxHp);
        if (Hp <= 0)
        {
            tm.monsterList.Remove(this);
        }
    }
}
