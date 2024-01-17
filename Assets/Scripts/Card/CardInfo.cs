using System;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardInfo : MonoBehaviour
{
    // 카드 이미지 풀 변수들
    [SerializeField] private Sprite[] cardImagesSprites;
    [SerializeField] private Sprite[] cardBackGroundSprites;
    [SerializeField] private Sprite[] cardFrameSprites;
    [SerializeField] private Sprite[] cardBannerSprites;

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


    public int currentDamage;
    public int currentDefense;

    [SerializeField] private bool isHand;
    private void Start()
    {
        cardData = card.cardData;
        
        currentDamage = card.CardDamage;
        currentDefense = card.CardDefense;
        
        cardDesc.text = UpdateDesc();
        cardBackGround.sprite = ChangeCardBackGroundSprite();
        cardImage.sprite = ChangeCardImageSprite();
        cardFrame.sprite = ChangeCardFrameSprite();
        
        cardUIType.text = ChangeCardUITypeText();
        cardBanner.sprite = ChangeCardBannerSprite();
        cardUIName.text = cardData.CardName;
        cardUIValue.text = cardData.cardValue.ToString();


        isHand = true;
    }

    private string UpdateDesc()
    {
        StringBuilder temp = new StringBuilder();
        for (int i = 0; i < cardData.cardDesc.Count; ++i)
        {
            switch (cardData.cardDesc[i])
            {
                case "Damage":
                    temp.Append(currentDamage.ToString());
                    Debug.Log("Damage");
                    break;
                case "BuffDuration":
                    temp.Append(cardData.BuffDuration.ToString());
                    break;
                case "DebuffDuration":
                    temp.Append(cardData.DebuffDuration.ToString());
                    break;
                case "Defense":
                    temp.Append(currentDefense.ToString());
                    Debug.Log("Defense");
                    break;
                default:
                    temp.Append(cardData.cardDesc[i]);
                    break;
            }
        }
        return temp.ToString();
    }
    private Sprite ChangeCardBackGroundSprite()
    {
        Sprite img = null;
        string path = "512/bg_" + cardData.cardType + "_" + cardData.cardColor;
        foreach (var sprite in cardBackGroundSprites)
        {
            if (sprite.name != path) continue;
            img = sprite;
            break;
        }
        return img;
    }
    
    private Sprite ChangeCardImageSprite()
    {
        Sprite img = null;
        string path = cardData.cardColor + "/" + cardData.cardType + "/" + cardData.CardImageName;
        foreach (var sprite in cardImagesSprites)
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
        string path = "512/frame_" + cardData.cardType + "_" + cardData.cardRarity;
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
        string typeText = cardData.cardType switch
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
        string path ="512/banner_" + cardData.cardRarity;
        foreach (var sprite in cardBannerSprites)
        {
            if (sprite.name != path) continue;
            img = sprite;
            break;
        }
        return img;
    }
}
