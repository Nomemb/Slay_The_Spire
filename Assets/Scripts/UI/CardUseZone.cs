using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUseZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // card
        var dropped = eventData.pointerDrag;
        var cardInfo = dropped.GetComponent<CardInfo>();
        var card = cardInfo.GetComponent<Card>();
        
        if (card.cardData.cardUseType != CardUseType.target) return;
        
        // monster
        var monster = GetComponentInParent<BaseMonster>();

        if (!card.CanUseCard()) return;
        
        card.UseCard(monster);
        Debug.Log("Monster CardZone Drop");
        GameManager.instance.onDrag = false;

    }
}
