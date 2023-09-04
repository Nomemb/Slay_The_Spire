using System;
using UnityEngine;

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

    protected int cardDamage;
    public int CardDamage => cardDamage;

    protected int cardDefense;
    public int CardDefense => cardDefense;
    protected void Start()
    {
        Init();
    }

    protected void Init()
    {
        cd = GetComponentInParent<CardDisplay>();
        if (cd == null) return;
        
        cd.cards.Add(this);
        cardDamage = cardData.Damage + gm.playerPower;

    }
    public void UseCard(BaseMonster target = null)
    {
        gm.currentMana -= cardData.cardValue;
        TurnManager.instance.ChangedPlayerMana();
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
        AddCardToUsedDeck();
        RemoveCard(thisIndex);
    }

    protected void AddCardToUsedDeck()
    {
        var thisIndex = cd.cards.IndexOf(this);
        if (cardData.cardType != CardType.power)
        {
            gm.usedDeck.Add(gm.hand[thisIndex]);
        };
    }

    private void RemoveCard(int index)
    {
        gm.hand.RemoveAt(index);
        cd.cards.RemoveAt(index);
        Destroy(cd.cardUI[index]);
        cd.cardUI.RemoveAt(index);  
    }

    public void Enchant()
    {
        
    }

    
}

