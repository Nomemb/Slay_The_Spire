using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        monsterList.Add("RedLouse", monsterPrefabs[0]);
        monsterList.Add("GreenLouse", monsterPrefabs[1]);
    }

    public void GenerateWeakMonsters()
    {
        int rand = Random.Range(1, 5);
        
    }
}
