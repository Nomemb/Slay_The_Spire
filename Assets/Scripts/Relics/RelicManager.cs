using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public static RelicManager instance;
    
    public GameObject relicZone;            // 유물이 생성될 게임오브젝트
    public List<Relic> relics;              // 현재 보유중인 유물 리스트

    void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
