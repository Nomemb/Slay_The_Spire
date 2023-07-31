using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum MonsterState
{
    Idle,
    Attack,
    Defense,
    Buff,
    DeBuff
}
public abstract class BaseMonster : MonoBehaviour, IDamageable
{
    [field: Header("Stats")]
    [field:SerializeField] protected int Hp { get; set; }
    [field:SerializeField] private int MaxHp { get; set; }
    [field:SerializeField] protected int AddedStrength { get; set; }
    [field:SerializeField] protected int Damage { get; set; }
    [field:SerializeField] protected int Block { get; set; }

    protected int currentDamage;
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
    public Image imageCurrentState;
    public Text textCurrentDamage;
    public GameObject buffUI;

    [Space(10f)]
    [Header("Animation")]
    [SerializeField] protected SkeletonAnimation skAnim;
    
    [Space(10f)]
    [Header("BuffSystem")]
    public SharedDebuff enemyShareState;
    public UniqueDebuffToEnemy enemyUniqueState;
    
    private BuffSystem bs;
    private DebuffSystem dbS;

    [SerializeField] protected PlayerController player;
    protected virtual void Start()
    {
        currentState = MonsterState.Idle;
        currentDamage = AddedStrength + Damage;
        
        ChangeNextState();
        tm = FindObjectOfType<TurnManager>();
        player = FindObjectOfType<PlayerController>();
        
        tm.monsterList.Add(this);
        MaxHp = Hp;
        
        bs = GetComponent<BuffSystem>();
        dbS = GetComponent<DebuffSystem>();

        enemyShareState = 0x00000000;
        enemyUniqueState = 0x00000000;
        
        //hpInter = GetComponentInChildren<HpInteraction>();
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
                Debuff();
                break;
            
            case MonsterState.Idle:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        skAnim.AnimationState.AddAnimation(0, "idle", true, 0f);
        ChangeNextState();
    }

    protected virtual void ChangeNextState()
    {
        imageCurrentState.sprite = stateSprites[(int)currentState];
        
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
    protected virtual void Attack()
    {
        currentDamage = AddedStrength + Damage;
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

    protected virtual void Debuff()
    {
        Debug.Log("Debuff");

    }

    public virtual void OnDamage(int onDamage)
    {
        Hp = Math.Max(Hp - onDamage, 0);
        hpInter.UpdateHpBar(Hp, MaxHp);
        if (Hp <= 0)
        {
            Debug.Log(gameObject.name + " 사망");
            tm.monsterList.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
