using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EncounterType
{
    Normal,
    Elite,
    Boss
}

public enum StageType
{
    Unknown,
    Shop,
    TreasureRoom,
    Rest,
    Enemy,
    Elite,
    Boss
}
[System.Serializable]
public class Stage
{
    public EncounterType encounterType;
    public StageType stageType;
    public List<GameObject> monsterData = new List<GameObject>();
    public Stage[] nextStages;
}
[System.Serializable]
public class DictionaryOfMonster : SerializableDictionary<string, GameObject> { }

public class StageManager : MonoBehaviour
{
    public Stage currentStage;

    public DictionaryOfMonster monsterList = new DictionaryOfMonster(); 
    [SerializeField] private List<GameObject> monsterPrefabs;
    [SerializeField] private GameObject spawnZone;

    private Vector3 spawnZonePos;
    
    // Start is called before the first frame update
    void Start()
    {
        currentStage = new Stage
        {
            encounterType = EncounterType.Normal,
            stageType = StageType.Unknown
        };

        Init();
    }
    
    void Init()
    {
        spawnZonePos = spawnZone.transform.position;
        foreach (var monster in monsterPrefabs)
        {
            monsterList.Add(monster.name, monster);
        }
        
        GenerateWeakMonsters();
    }

    public void GenerateWeakMonsters()
    {
        int rand = Random.Range(1, 5);
        if (rand <= 3)
        {
            currentStage.monsterData.Add(monsterList["RedLouse"]);
            currentStage.monsterData.Add(monsterList["GreenLouse"]);
        }
        else
        {
            currentStage.monsterData.Add(monsterList["JawWorm"]);
        }
    }

    public void CreateStageMonster()
    {
        CreateMonster();
    }
    private void CreateMonster()
    {
        foreach (var monster in currentStage.monsterData)
        {
            var newMonster = Instantiate(monster, monster.transform.position, quaternion.identity); ;
            newMonster.transform.SetParent(spawnZone.transform);
        }
    }
}
