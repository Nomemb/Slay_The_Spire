using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defect : BaseCharacter
{
    public Defect()
    {
        Debug.Log("Defect Start");
        this.maxHp = 75;
        this.hp = maxHp;
    }
}
