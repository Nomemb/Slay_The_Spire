using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    [Tooltip("0~1 : Base\n" +
             "2~  : Common Attack")] 
    [SerializeField] private List<GameObject> ironCladCardPool = new List<GameObject>();

    private Card card;
    private CardInfo cardInfo;
    private GameManager gm;

    [SerializeField] private static int _cardPoolSize;
    private void Start()
    {
        gm = GameManager.instance;
    }
    public void InitCard()
    {
        if (gm.currentType == CharacterType.Ironclad)
        {
            InitIronClad();
        }

        _cardPoolSize = ironCladCardPool.Count;
    }

    private void InitIronClad()
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject thisCard = i switch
            {
                < 5 => ironCladCardPool[0],
                < 9 => ironCladCardPool[1],
                _ => ironCladCardPool[2]
            };
            
            gm.fixedDeck.Add(thisCard);
        }
    }

    public GameObject AddNewCard()
    {
        var randNum = Random.Range(0, _cardPoolSize);
        var newCard = ironCladCardPool[randNum];
        Debug.Log(newCard.name + " 추가");

        return newCard;
    }

    public void AddNewCard(int index)
    {
        if (index >= ironCladCardPool.Count) return;
        gm.fixedDeck.Add(ironCladCardPool[index]);
    }
}
