using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silent : BaseCharacter
{
    public Silent()
    {
        Debug.Log("Silent Start");
        this.MaxHp = 70;
        this.Hp = MaxHp;
    }
}
