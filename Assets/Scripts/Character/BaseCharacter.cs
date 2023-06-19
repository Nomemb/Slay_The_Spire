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

    public virtual void Attack(int _damage)
    {
        Debug.Log("Attack ");
    }


}
