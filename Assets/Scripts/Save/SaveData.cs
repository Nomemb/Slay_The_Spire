using System.Collections.Generic;


[System.Serializable]
public class SaveData
{
    public List<Card> cardData = new List<Card>();

    public characterType characterType;
    public int hp;
    public int maxHp;
    public int gold;
    public int addedStrength;
}
