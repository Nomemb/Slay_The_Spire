using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public List<Card> cards;

    public List<GameObject> cardUI = new List<GameObject>();
    public void ImageSetting()
    {
        foreach (var card in GameManager.instance.hand)
        {
            GameObject tempCard = Instantiate(card, this.transform.position, Quaternion.identity);
            tempCard.transform.SetParent(this.transform);
            cardUI.Add(tempCard);

        }
    }

    public void EndPlayerTurn()
    {
        while (cards.Count != 0)
        {
            Destroy(cards[0]);
            cards.RemoveAt(0);
        }
        
        while (cardUI.Count != 0)
        {
            Destroy(cardUI[0]);
            cardUI.RemoveAt(0);
        }
    }
}
