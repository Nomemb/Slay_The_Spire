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
public class Stage
{
    public EncounterType encounterType;
    public StageType stageType;
    public List<GameObject> monsterData;
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
    }

    public void GenerateWeakMonsters()
    {
        int rand = Random.Range(1, 5);
        var newMonster1 = Instantiate(monsterList["RedLouse"], spawnZonePos, quaternion.identity);
        var newMonster2 = Instantiate(monsterList["GreenLouse"], spawnZonePos, quaternion.identity);
        
        newMonster1.transform.SetParent(spawnZone.transform);
        newMonster2.transform.SetParent(spawnZone.transform);

            
    }
}
