using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ironclad : BaseCharacter
{
    public Ironclad()
    {
        Debug.Log("Ironclad Start");
        this.maxHp = 80;
        this.hp = maxHp;
    }
}
