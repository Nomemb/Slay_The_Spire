using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ironclad : BaseCharacter
{
    public Ironclad()
    {
        Debug.Log("Ironclad Start");
        this.maxHp = 80;
        this.hp = maxHp;
    }

    public override void InitCard()
    {
        Debug.Log("Ironclad Card Init");
        GameManager.instance.fixedDeck.Add(new Card("타격", CardColor.Red, CardType.Attack, CardRarity.Common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", CardColor.Red, CardType.Attack, CardRarity.Common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", CardColor.Red, CardType.Attack, CardRarity.Common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", CardColor.Red, CardType.Attack, CardRarity.Common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", CardColor.Red, CardType.Attack, CardRarity.Common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", CardColor.Red, CardType.Skill, CardRarity.Common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", CardColor.Red, CardType.Skill, CardRarity.Common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", CardColor.Red, CardType.Skill, CardRarity.Common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", CardColor.Red, CardType.Skill, CardRarity.Common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("강타", CardColor.Red, CardType.Attack, CardRarity.Common, 2, "피해를 8 줍니다.\n취약을 2 부여합니다."));
    }
}
