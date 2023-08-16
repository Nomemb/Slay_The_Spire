using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : MonoBehaviour
{
    public string id;
    public string relicName;
    public Sprite image;
    public Text countText;
    public int count;

    public Text relicDescText;
    [SerializeField] private bool hasCount;

    protected TurnManager tm;

    protected virtual void Start()
    {
        tm = TurnManager.instance;
        
        if (!hasCount) return; 
        countText.text = count.ToString();
        countText.gameObject.SetActive(true);
    }

    protected virtual void ActivateRelic()
    {
        
    }
}
