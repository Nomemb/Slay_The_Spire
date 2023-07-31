using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void Start()
    {
        Init();
    }
    public void Init()
    {
        playerInfo.characterType.text = "the " + GameManager.instance.currentType;
        playerInfo.heartText.text = GameManager.instance.playerHp + "/" + GameManager.instance.playerMaxHp;
        playerInfo.goldText.text = GameManager.instance.playerGold.ToString();

        deckCount.text = GameManager.instance.fixedDeck.Count.ToString();
    }
    public void UpdateHpUI()
    {
        playerInfo.heartText.text = GameManager.instance.playerHp + "/" + GameManager.instance.playerMaxHp;
    }

    public void UpdateGoldUI()
    {
        playerInfo.goldText.text = GameManager.instance.playerGold.ToString();
    }

    public void UpdateDeckCountUI()
    {
        deckCount.text = GameManager.instance.fixedDeck.Count.ToString();
    }
    
}
