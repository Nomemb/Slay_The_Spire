using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public Card card;
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
        cardDesc.text = card.cardDesc;
        cardUIType.text = card.cardType.ToString();
        cardUIName.text = card.cardName;
        cardUIValue.text = card.cardValue.ToString();
    }
}
