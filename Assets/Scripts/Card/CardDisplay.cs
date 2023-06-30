using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.XR;

public class CardDisplay : MonoBehaviour
{
    public List<Card> cards;
    public GameObject cardPrefab;
    public GameObject handUI;
    

    
    void Start()
    {
        ImageSetting();
    }

    void ImageSetting()
    {
        Debug.Log("ImageSetting");
        cards = GameManager.instance.hand;
        for (int i = 0; i < cards.Count; ++i)
        { 
            GameObject newCard = Instantiate(cardPrefab, handUI.transform);
            CardInfo cardInfo = newCard.GetComponent<CardInfo>();
            cardInfo.card = cards[i];
        }
    }
    
}
