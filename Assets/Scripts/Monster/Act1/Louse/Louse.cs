using UnityEngine;

public abstract class Louse : BaseMonster
{
    [SerializeField] private bool hasCurlUp = true;
    [SerializeField] protected int curlUp;
    
    protected override void Start()
    {
        base.Start();
        curlUp = Random.Range(3, 8);
        bs.AddEnemyBuff("CurlUp");
        SetStatsByAscensionLevel();
    }

    protected override void Attack()
    {
        skAnim.AnimationState.SetAnimation(0, "transitiontoclosed", false);
        base.Attack();
    }
    
    protected override void SetStatsByAscensionLevel()
    {
        if (ascensionLevel >= 2)
        {
            damage++;
        }

        if (ascensionLevel >= 7)
        {
            hp++;
            curlUp = Random.Range(4, 9);
        }

        if (ascensionLevel >= 17)
        {
            curlUp = Random.Range(9, 13);
        }
    }

    public override void OnDamage(int onDamage)
    {
        base.OnDamage(onDamage);
        if (hasCurlUp)
        {
            block = curlUp;
            hasCurlUp = false;
            bs.RemoveBuff(bs.enemyBuffDict, "CurlUp");
            hpInter.UpdateBlockBar(block, hp, maxHp);
        }
    }
}
