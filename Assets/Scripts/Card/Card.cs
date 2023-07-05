using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CardColor{red, green, blue, purple, colorless, status, curse}
public enum CardType{attack, skill, power}
public enum CardRarity{common, uncommon, rare}

[System.Serializable]
public class Card  
{
    public string cardName;                 // 카드 이름
    public string cardImageName;            // 카드 이미지 이름
    public CardColor cardColor;             // 카드 색깔
    public CardType cardType;               // 카드 타입 ( 공격, 스킬, 파워 )
    public CardRarity cardRarity;           // 카드 희귀도 ( 일반, 희귀, 특별 )
    public int cardValue;                   // 카드 에너지
    public string cardDesc;                 // 카드 설명

    private GameManager gm = GameManager.instance;
    
    public Card()
    {
    }
    
    public Card(string cardName, string cardImageName, CardColor cardColor, CardType cardType, CardRarity cardRarity, int cardValue, string cardDesc)
    {
        this.cardName = cardName;
        this.cardImageName = cardImageName;
        this.cardColor = cardColor;
        this.cardType = cardType;
        this.cardRarity = cardRarity;
        this.cardValue = cardValue;
        this.cardDesc = cardDesc;
    }

    public void UseCard()
    {
        // DoingSomething();
        
        /*
        if(card == "휘발성")
        {
            gm.expiredDeck.Add(this);
        }
         */
        if (cardType != CardType.power)
        {
            gm.usedDeck.Add(this);
        }
    }

    public void Enchant()
    {
        
    }
    
}

