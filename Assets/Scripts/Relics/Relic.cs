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
    public int currentCount;
    public int maxCount;

    public Text relicDescText;
    [SerializeField] private bool hasCount;

    protected TurnManager tm;

    protected virtual void Start()
    {
        tm = TurnManager.instance;
        
        if (!hasCount) return;
        currentCount = maxCount;
        countText.text = currentCount.ToString();
        countText.gameObject.SetActive(true);
    }

    public virtual void ActivateRelic()
    {
        
    }

    public void UpdateCount()
    {
        if (!hasCount) return; 
        countText.text = currentCount.ToString();
    }
}
