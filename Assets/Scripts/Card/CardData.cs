using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "SO/Card Data")]
[Serializable]
public class CardData : ScriptableObject
{
    [Header("Card Name")]
    [SerializeField] private string cardName;        // 카드 이름
    public string CardName => cardName;

    [SerializeField] private string cardImageName;  // 카드 이미지 이름
    public string CardImageName => cardImageName;

    [Header("Card Type")]
    public CardColor cardColor;                     // 카드 색깔
    public CardType cardType;                       // 카드 타입 ( 공격, 스킬, 파워 )
    public CardRarity cardRarity;                   // 카드 희귀도 ( 일반, 희귀, 특별 )
    public CardUseType cardUseType;                 // 카드 사용 방법 ( 일반, 타겟팅 )
    
    [Header("Card Value")]
    public int cardValue;                           // 카드 에너지
    
    [SerializeField] private int damage;            // 카드 데미지
    public int Damage => damage;
    [SerializeField] private int defense;           // 카드 방어도
    public int Defense => defense;
    
    // public BuffType buffType;
    // public DeBuffType deBuffType;
    
    [SerializeField] private int buffDuration;     // 버프 지속시간
    public int BuffDuration => buffDuration;
    
    [SerializeField] private int deBuffDuration;   // 디버프 지속시간
    public int DebuffDuration => deBuffDuration;
    
    [Header("Card Enchant")]
    public int e_cardValue;                           // 카드 에너지
    
    [SerializeField] private int e_damage;            // 카드 데미지
    public int e_Damage => e_damage;
    [SerializeField] private int e_defense;           // 카드 방어도
    public int e_Defense => e_defense;
    
    // public BuffType buffType;
    // public DeBuffType deBuffType;
    
    [SerializeField] private int e_buffDuration;     // 버프 지속시간
    public int e_BuffDuration => e_buffDuration;
    
    [SerializeField] private int e_deBuffDuration;   // 디버프 지속시간
    public int e_DebuffDuration => e_deBuffDuration;
    
    [Header("Card Text")]
    [Multiline(4)]
    public string cardDesc;                        // 카드 설명

}
