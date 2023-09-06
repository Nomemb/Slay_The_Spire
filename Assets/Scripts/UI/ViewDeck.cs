using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ViewDeck : MonoBehaviour
{
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject grid;
    public void ViewingDeckCard(string deckName)
    {
        var deckList = new List<GameObject>();
        if (scrollView.activeSelf)
        {
            ResetDeckCard();
            scrollView.SetActive(false);
            return;
        }
        scrollView.SetActive(true);
        
        switch (deckName)
        {
            case "FixedDeck":
                deckList = GameManager.instance.fixedDeck.ToList();
                break;
            
            case "DrawDeck":
                deckList = GameManager.instance.drawDeck.ToList();
                break;
            
            case "UsedDeck":
                deckList = GameManager.instance.usedDeck.ToList();
                break;
            
            case "ExpiredDeck":
                deckList = GameManager.instance.expiredDeck.ToList();
                break;
        }

        foreach (var card in deckList)
        {
            var tempCard = Instantiate(card, transform.position, Quaternion.identity);
            var cpe = tempCard.GetComponent<CardPointEvent>();
            cpe.enabled = false;
            tempCard.transform.SetParent(grid.transform);
        }
    }

    public void ResetDeckCard()
    {
        foreach (Transform card in grid.transform)
        {
            Destroy(card.gameObject);
        }
    }
}
