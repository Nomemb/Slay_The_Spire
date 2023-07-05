using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defect : BaseCharacter
{
    public Defect()
    {
        Debug.Log("Defect Start");
        this.MaxHp = 75;
        this.Hp = MaxHp;
    }
}
