using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardColor{Red, Green, Blue, Purple, Colorless, Status, Curse}
public enum CardType{Attack, Skill, Power}
public enum CardRarity{Common, UnCommon, Rare}

[System.Serializable]
public class Card  
{
    public string cardName;                 // 카드 이름
    public CardColor cardColor;             // 카드 색깔
    public CardType cardType;               // 카드 타입 ( 공격, 스킬, 파워 )
    public CardRarity cardRarity;           // 카드 희귀도 ( 일반, 희귀, 특별 )
    public int cardValue;                   // 카드 에너지
    public string cardDesc;                 // 카드 설명

    public Card()
    {
    }
    
    public Card(string cardName, CardColor cardColor, CardType cardType, CardRarity cardRarity, int cardValue, string cardDesc)
    {
        this.cardName = cardName;
        this.cardColor = cardColor;
        this.cardType = cardType;
        this.cardRarity = cardRarity;
        this.cardValue = cardValue;
        this.cardDesc = cardDesc;
    }
}

