using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum characterType { Ironclad, Silent, Defect, Watcher}
public abstract class BaseCharacter
{
    public int hp { get; protected set; }               // 현재 체력
    public int maxHp { get; protected set; }            // 최대 체력   
    public int addedStrength { get; protected set; }    // 힘
    public int block { get; protected set; }            // 방어도

    public int gold { get; protected set; }            // 돈

    public BaseCharacter()
    {
        addedStrength = 0;
        gold = 99;
    }

    public virtual void Attack(int _damage)
    {
        Debug.Log("Attack ");
    }
    
    public void SetCharacterStat()
    {
        GameManager.instance.playerHp = this.hp;
        GameManager.instance.playerMaxHp = this.maxHp;
        GameManager.instance.playerGold = this.gold;
        GameManager.instance.playerPower = this.addedStrength;
    }
}
