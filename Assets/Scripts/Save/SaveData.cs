using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public List<GameObject> card = new List<GameObject>();

    // public CardData cardData;
    public CharacterType characterType;
    public int hp;
    public int maxHp;
    public int gold;
    public int addedStrength;
    public int ascensionLevel;
}
