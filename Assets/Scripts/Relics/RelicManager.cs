using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.String;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;
    
    public GameObject relicZone;                                   // 유물이 생성될 게임오브젝트
    [SerializeField] private List<GameObject> relics;              // 현재 보유중인 유물 리스트
    public List<GameObject> hasRelics = new List<GameObject>();
    
    
    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void GetRelic(string relicName)
    {
        foreach (var relic in relics)
        {
            if (Compare(relic.name, relicName, StringComparison.OrdinalIgnoreCase) != 0) continue;
            hasRelics.Add(relic);
            GameObject _relic = Instantiate(relic, transform.position, Quaternion.identity);
            _relic.transform.SetParent(relicZone.transform);
        }
    }

    public GameObject ActivateRelic(string relicName)
    {
        foreach (var tempRelic in hasRelics)
        {
            if (Compare(tempRelic.name, relicName, StringComparison.OrdinalIgnoreCase) != 0) continue;

            return tempRelic;
        }

        return null;
    }
}
