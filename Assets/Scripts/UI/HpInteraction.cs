using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpInteraction : MonoBehaviour
{
    GameObject target;

    private int hp;
    private int maxHp;
    private void Start()
    {
        if(target.CompareTag("Player"))
        {
            hp = GameManager.instance.playerHp;
            maxHp = GameManager.instance.playerMaxHp;
        }
        else
        {

        }
    }
}
