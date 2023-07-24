using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // 카드 이미지 풀 변수들
    [SerializeField] private Sprite[] cardImagesSprites;
    [SerializeField] private Sprite[] cardBackGroundSprites;
    [SerializeField] private Sprite[] cardFrameSprites;
    [SerializeField] private Sprite[] cardBannerSprites;
    
    // 카드 드래그, 드랍 관련 변수
    public Vector3 originPos;
    private Vector3 initPos;
    
    public Card card;
    public CardData cardData;
    public Image cardBackGround;                                                 // 카드 배경 ( 색상, 카드 타입 )
    public Text cardDesc;                                                        // 카드 설명
    public Image cardImage;                                                      // 카드 이미지
    public Image cardFrame;                                                      // 
    public Text cardUIType;                                                      // 카드 UI 타입 텍스트
    public Image cardBanner;                                                     // 카드 배너 ( 카드 희귀도 )
    public Text cardUIName;                                                      // 카드 UI 이름
    public Text cardUIValue;                                                     // 카드 UI 에너지

    private void Start()
    {
        cardData = card.cardData;
        cardDesc.text = cardData.cardDesc;
        cardBackGround.sprite = ChangeCardBackGroundSprite();
        cardImage.sprite = ChangeCardImageSprite();
        cardFrame.sprite = ChangeCardFrameSprite();
        
        cardUIType.text = ChangeCardUITypeText();
        cardBanner.sprite = ChangeCardBannerSprite();
        cardUIName.text = cardData.CardName;
        cardUIValue.text = cardData.cardValue.ToString();

        initPos = transform.position;
    }

    private Sprite ChangeCardImageSprite()
    {
        Sprite img = null;
        var path = cardData.cardColor + "/" + cardData.cardType + "/" + cardData.CardImageName;
        foreach (var sprite in cardImagesSprites)
        {
            if (sprite.name != path) continue;
            img = sprite;
            break;
        }
        return img;
    }
    
    private Sprite ChangeCardBackGroundSprite()
    {
        Sprite img = null;
        var path = "512/bg_" + cardData.cardType + "_" + cardData.cardColor;
        foreach (var sprite in cardBackGroundSprites)
        {
            if (sprite.name != path) continue;
            img = sprite;
            break;
        }
        return img;
    }
    
    private Sprite ChangeCardFrameSprite()
    {
        Sprite img = null;
        var path = "512/frame_" + cardData.cardType + "_" + cardData.cardRarity;
        foreach (var sprite in cardFrameSprites)
        {
            if (sprite.name != path) continue;
            img = sprite;
            break;
        }
        return img;
    }

    private string ChangeCardUITypeText()
    {
        var typeText = cardData.cardType switch
        {
            CardType.attack => "공격",
            CardType.skill => "스킬",
            CardType.power => "파워",
            _ => throw new ArgumentOutOfRangeException()
        };

        return typeText;
    }
    
    private Sprite ChangeCardBannerSprite()
    {
        Sprite img = null;
        var path ="512/banner_" + cardData.cardRarity;
        foreach (var sprite in cardBannerSprites)
        {
            if (sprite.name != path) continue;
            img = sprite;
            break;
        }
        return img;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = transform.position;
        if (transform != null) transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cardData.cardType != CardType.attack)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            UIManager.instance.cursorObject.gameObject.SetActive(true);
            transform.position = initPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.instance.cursorObject.gameObject.SetActive(false);
        transform.position = originPos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (cardData.cardUseType != CardUseType.normal) return;
        if (Input.mousePosition.y <= 500) return;
        card.UseCard();

        if (card.cardData.cardType != CardType.power)
        {
            GameManager.instance.usedDeck.Add(this.gameObject);
        }
        Destroy(this.gameObject, 0.5f);
    }
}
