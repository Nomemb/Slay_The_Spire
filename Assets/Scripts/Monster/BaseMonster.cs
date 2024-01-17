using System;
using Spine.Unity;
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
public abstract class BaseMonster : MonoBehaviour, IDamageable, IBlockable
{
    [field: Header("Stats")] 
    [field: SerializeField] protected int hp;
    [field: SerializeField] protected int maxHp;
    [field: SerializeField] protected int addedStrength;
    [field: SerializeField] protected int damage;
    [field: SerializeField] protected int block;
    
    public int Hp => hp;
    public int MaxHp => maxHp;
    public int AddedStrength => addedStrength;
    public int Damage => damage;
    public int Block => block;

    protected int currentDamage;
    private TurnManager tm;
    
    [Space(10f)]
    [Header("States")]
    [SerializeField] protected MonsterState currentState;
    [SerializeField] protected MonsterState prevState;
    [SerializeField] protected int sameStateCount;

    [Space(10f)]
    [Header("UI")]
    [SerializeField] protected HpInteraction hpInter;
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
    
    protected BuffSystem bs;
    protected DebuffSystem dbS;

    [SerializeField] protected PlayerController player;
    [SerializeField] protected int ascensionLevel;
    protected virtual void Start()
    {
        currentState = MonsterState.Idle;

        ChangeNextState();
        tm = FindObjectOfType<TurnManager>();
        player = FindObjectOfType<PlayerController>();
        ascensionLevel = GameManager.instance.ascensionLevel;
        
        Debug.Log(this.name + " 추가");
        tm.monsterList.Add(this);
        bs = GetComponent<BuffSystem>();
        dbS = GetComponent<DebuffSystem>();
        maxHp = hp;

        GameObject temp = RelicManager.instance.ActivateRelic("Lament");
        if (temp)
        {
            var lament = temp.GetComponent<Lament>();
            if(lament.currentCount > 0) hp = 1;
        }
        
        enemyShareState = 0x00000000;
        enemyUniqueState = 0x00000000;
        
        hpInter.UpdateHpBar(hp, maxHp);
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
    protected virtual void Attack()
    {
        if ((player.dbS.sharedState & SharedDebuff.Vulnerable) == SharedDebuff.Vulnerable)
        {
            currentDamage = (int)Math.Ceiling(currentDamage * 1.5);
        }

        Debug.Log("Attack " + currentDamage);
        
        if (GameManager.instance.block >= currentDamage)
        {
            GameManager.instance.block -= currentDamage;
            tm.ChangedPlayerHp();
            return;
        }

        currentDamage -= GameManager.instance.block;
        SoundManager.instance.PlaySound("DefenseBreak");
        GameManager.instance.block = 0;
        GameManager.instance.playerHp -= currentDamage;
        tm.ChangedPlayerHp();
    }

    protected virtual void Defense()
    {
        Debug.Log("Defense " + block);
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
        // 취약 상태라면 ( 데미지를 50% 더 받음 )
        if ((dbS.sharedState & SharedDebuff.Vulnerable) == SharedDebuff.Vulnerable)
        {
            onDamage = (int)Math.Ceiling(onDamage * 1.5);
        }
        Debug.Log(onDamage);
        if (block >= onDamage)
        {
            block -= onDamage;
            hpInter.UpdateBlockBar(block, hp, maxHp);
            return;
        }

        onDamage -= block;
        block = 0;
        hpInter.UpdateBlockBar(block, hp, maxHp);
        hp = Math.Max(hp - onDamage, 0);
        hpInter.UpdateHpBar(hp, maxHp);
        if (hp <= 0)
        {
            Debug.Log(gameObject.name + " 사망");
            tm.monsterList.Remove(this);
            tm.IsClearStage();
            Destroy(this.gameObject);
        }
    }

    protected virtual void SetStatsByAscensionLevel()
    {
        // 초월 레벨에 따른 스탯 조정 함수

    }

    public void ResetBlock()
    {
        block = 0;
        hpInter.UpdateBlockBar(block, hp, maxHp);
    }
}
