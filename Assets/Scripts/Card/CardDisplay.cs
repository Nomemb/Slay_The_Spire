using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public List<Card> cards;
    public GameObject cardPrefab;
    public GameObject handUI;

    public List<GameObject> cardUI;
    
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
            cardUI.Add(newCard);
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log(gameObject);
    }
}
