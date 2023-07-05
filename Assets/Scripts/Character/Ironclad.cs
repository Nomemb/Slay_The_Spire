using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ironclad : BaseCharacter
{
    public Ironclad()
    {
        Debug.Log("Ironclad Start");
        this.MaxHp = 80;
        this.Hp = MaxHp;
    }

    public override void InitCard()
    {
        base.InitCard();
        Debug.Log("Ironclad Card Init");
        GameManager.instance.fixedDeck.Add(new Card("타격", "strike", CardColor.red, CardType.attack, CardRarity.common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", "strike", CardColor.red, CardType.attack, CardRarity.common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", "strike", CardColor.red, CardType.attack, CardRarity.common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", "strike", CardColor.red, CardType.attack, CardRarity.common, 1, "피해를 6 줍니다."));
        GameManager.instance.fixedDeck.Add(new Card("타격", "strike", CardColor.red, CardType.attack, CardRarity.common, 1, "피해를 6 줍니다."));

        GameManager.instance.fixedDeck.Add(new Card("수비", "defend", CardColor.red, CardType.skill, CardRarity.common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", "defend", CardColor.red, CardType.skill, CardRarity.common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", "defend", CardColor.red, CardType.skill, CardRarity.common, 1, "방어도를 5 얻습니다."));
        GameManager.instance.fixedDeck.Add(new Card("수비", "defend", CardColor.red, CardType.skill, CardRarity.common, 1, "방어도를 5 얻습니다."));

        GameManager.instance.fixedDeck.Add(new Card("강타", "bash", CardColor.red, CardType.attack, CardRarity.common, 2, "피해를 8 줍니다.\n취약을 2 부여합니다."));
    }
}
