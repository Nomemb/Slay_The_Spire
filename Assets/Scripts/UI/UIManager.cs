using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header ("UnLock Keys")]
    public Image[] keys;

    [System.Serializable]
    public struct PlayerInfo
    { 
        public Text playerName;
        public Text characterType;
        public Image heartImage;
        public Text heartText;
        public Image goldImage;
        public Text goldText;
    }
    public PlayerInfo playerInfo;
    [Space(10f)]
    [Header("Icons")]
    public Button mapButton;
    public Button deckButton;
    public Text deckCount;
    public Button settingButton;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerInfo.characterType.text = "the " + GameManager.instance.currentType;
        playerInfo.heartText.text = GameManager.instance.playerHp + "/" + GameManager.instance.playerMaxHp;
        playerInfo.goldText.text = GameManager.instance.playerGold.ToString();

        deckCount.text = GameManager.instance.cardData.Count.ToString();
    }

    public void HpUIUpdate()
    {
        playerInfo.heartText.text = GameManager.instance.playerHp + "/" + GameManager.instance.playerMaxHp;
    }

    public void GoldUIUpdate()
    {
        playerInfo.goldText.text = GameManager.instance.playerGold.ToString();
    }

    public void DeckCountUIUpdate()
    {
        deckCount.text = GameManager.instance.cardData.Count.ToString();
    }

}
