using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUseZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 카드 정보를 받아옴
        var dropped = eventData.pointerDrag;
        var cardInfo = dropped.GetComponent<CardInfo>();
        var card = cardInfo.GetComponent<Card>();
        
        if (card.cardData.cardUseType != CardUseType.target) return;
        
        // 몬스터 정보를 받아옴
        var monster = GetComponentInParent<BaseMonster>();

        if (!card.CanUseCard()) return;
        
        // 해당 몬스터에게 사용
        card.UseCard(monster);
        GameManager.instance.onDrag = false;

    }
}
