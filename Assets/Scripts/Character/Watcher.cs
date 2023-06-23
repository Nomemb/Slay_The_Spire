using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : BaseCharacter
{
    public Watcher()
    {
        Debug.Log("Watcher Start");
        this.maxHp = 72;
        this.hp = maxHp;
    }
}
