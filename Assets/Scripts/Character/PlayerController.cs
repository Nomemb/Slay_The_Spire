using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public BuffSystem bs;
    public DebuffSystem dbS;
    
    private void Start()
    {
        bs = GetComponent<BuffSystem>();
        dbS = GetComponent<DebuffSystem>();
    }

    public void BattleStart()
    {
        bs.Init();
        dbS.Init();
    }
}