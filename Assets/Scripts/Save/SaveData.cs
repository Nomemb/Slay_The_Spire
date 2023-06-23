using System.Collections.Generic;


[System.Serializable]
public class SaveData
{
    public List<string> cardData = new List<string>();

    public characterType characterType;
    public int hp;
    public int maxHp;
    public int gold;
    public int addedStrength;
}
