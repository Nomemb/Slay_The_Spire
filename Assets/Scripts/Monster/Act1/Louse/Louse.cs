using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Louse : BaseMonster
{
    [SerializeField] private bool hasCurlUp = true;
    [SerializeField] protected int curlUp;
    
    protected virtual void Start()
    {
        base.Start();
        curlUp = Random.Range(3, 8);
        SetStatsByAscensionLevel();
    }

    protected override void Attack()
    {
        skAnim.AnimationState.SetAnimation(0, "transitiontoclosed", false);
        base.Attack();
    }
    
    private void SetStatsByAscensionLevel()
    {
        if (GameManager.instance.ascensionLevel >= 2)
        {
            Damage++;
        }

        if (GameManager.instance.ascensionLevel >= 7)
        {
            Hp++;
            curlUp = Random.Range(4, 9);
        }

        if (GameManager.instance.ascensionLevel >= 17)
        {
            curlUp = Random.Range(9, 13);
        }
    }

    public void OnDamage()
    {
        if (hasCurlUp) Block = curlUp;
    }
}
