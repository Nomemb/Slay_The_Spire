using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncounterType
{
    Normal,
    Elite,
    Boss
}

public class Stage
{
    public EncounterType stageType;
    public List<BaseMonster> monsterData;
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
            stageType = EncounterType.Normal
        };

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        monsterList.Add("RedLouse", monsterPrefabs[0]);
    }
}
