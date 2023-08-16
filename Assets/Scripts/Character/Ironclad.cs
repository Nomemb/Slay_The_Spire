using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ironclad : BaseCharacter
{
    public Ironclad()
    {
        Debug.Log("Ironclad Start");
        this.MaxHp = 80;
        this.Hp = MaxHp;
        this.initRelicName = "Burning Blood";
    }
}
