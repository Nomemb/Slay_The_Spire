using System;
using UnityEngine;
using UnityEngine.XR;

public enum CardColor{red, green, blue, purple, colorless, status, curse}
public enum CardType{attack, skill, power}
public enum CardRarity{common, uncommon, rare}

public enum CardUseType{normal, target}
[Serializable]
public abstract class Card : MonoBehaviour
{
    public CardData cardData;
    
    protected GameManager gm = GameManager.instance;
    protected CardDisplay cd;
    protected void Start()
    {
        cd = GetComponentInParent<CardDisplay>();
        cd.cards.Add(this);
    }
    public void UseCard(BaseMonster target = null)
    {
        gm.currentMana -= cardData.cardValue;

        ActivationCard(target);
        UsageCard();
        TurnManager.instance.ChangePlayerCardCount();
    }
    
    public bool CanUseCard()
    {
        return gm.currentMana >= cardData.cardValue;
    }

    protected virtual void ActivationCard(BaseMonster target = null)
    {
        Debug.Log(cardData.CardName + " 사용");
        // Doing Something();
    }

    protected void UsageCard()
    {
        int thisIndex = cd.cards.IndexOf(this);
        if (cardData.cardType != CardType.power)
        {
            gm.usedDeck.Add(gm.hand[thisIndex]);
        };
        gm.hand.RemoveAt(thisIndex);
        cd.cards.RemoveAt(thisIndex);
        Destroy(cd.cardUI[thisIndex]);
        cd.cardUI.RemoveAt(thisIndex);
    }
    public void Enchant()
    {
        
    }

    
}

