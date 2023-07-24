using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    [Tooltip("0~1 : Base\n" +
             "2~  : Common Attack")] 
    [SerializeField] private List<GameObject> ironCladCardPool;

    private Card card;
    private CardInfo cardInfo;
    private GameManager gm;

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
}
