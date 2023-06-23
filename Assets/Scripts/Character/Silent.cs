using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silent : BaseCharacter
{
    public Silent()
    {
        Debug.Log("Silent Start");
        this.maxHp = 70;
        this.hp = maxHp;
    }
}
