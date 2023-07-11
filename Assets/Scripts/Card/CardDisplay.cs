using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public List<Card> cards;
    public GameObject cardPrefab;
    public GameObject handUI;

    public List<GameObject> cardUI;
    
    public void ImageSetting()
    {
        cards = GameManager.instance.hand;
        foreach (var card in cards)
        {
            var newCard = Instantiate(cardPrefab, handUI.transform);
            var cardInfo = newCard.GetComponent<CardInfo>();
            cardInfo.card = card;
            cardUI.Add(newCard);
        }
    }

    public void EndPlayerTurn()
    {
        while (cardUI.Any())
        {
            Destroy(cardUI[0]);
            cardUI.RemoveAt(0);
        }
    }
}
