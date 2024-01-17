using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPointEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 카드 드래그, 드랍 관련 변수
    public Vector3 originPos;
    [SerializeField] private Vector3 initPos;
    [SerializeField] private Vector3 onPointerPos;
    
    [Space(10f)]
    [Header("Card")]
    public Card card;
    public CardData cardData;
    
    private void Start()
    {
        initPos = transform.position;
        
        cardData = card.cardData;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.instance.onDrag)
        {
            GameManager.instance.onDrag = true;
        }
        SoundManager.instance.PlaySound("CardSelect");
        //if (transform != null) transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cardData.cardType != CardType.attack)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            //UIManager.instance.cursorObject.gameObject.SetActive(true);
            transform.position = initPos + onPointerPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //UIManager.instance.cursorObject.gameObject.SetActive(false);
        transform.position = originPos;
        GameManager.instance.onDrag = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (cardData.cardUseType != CardUseType.normal || Input.mousePosition.y <= 500 || !card.CanUseCard()) return;
        card.UseCard();
        Debug.Log("SKill OnDrop");
        GameManager.instance.onDrag = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.instance.onDrag) return;
        if(originPos == Vector3.zero) originPos = transform.position;
        EnlargeCard(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.instance.onDrag) return;
        EnlargeCard(false);
        GameManager.instance.onDrag = false;
    }

    private void EnlargeCard(bool isEnlarge)
    {
        if (isEnlarge)
        {
            Vector3 position = transform.position;
            position = new Vector3(position.x, position.y + onPointerPos.y, position.z);
            transform.position = position;
        }
        else
        {
            transform.position = originPos;
        }
    }
}
