using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Ironclad, Silent, Defect, Watcher}
public abstract class BaseCharacter
{
    public int Hp { get; protected set; }               // 현재 체력
    public int MaxHp { get; protected set; }            // 최대 체력   
    public int AddedStrength { get; protected set; }    // 힘
    public int Block { get; protected set; }            // 방어도

    public int Gold { get; protected set; }            // 돈

    protected BaseCharacter()
    {
        AddedStrength = 0;
        Gold = 99;
    }

    public virtual void Attack(int damage)
    {
        Debug.Log("Attack ");
    }
    
    public void SetCharacterStat()
    {
        GameManager.instance.playerHp = this.Hp;
        GameManager.instance.playerMaxHp = this.MaxHp;
        GameManager.instance.playerGold = this.Gold;
        GameManager.instance.playerPower = this.AddedStrength;
    }

    public virtual void InitCard()
    {
        GameManager.instance.fixedDeck.Clear();
    }
}
