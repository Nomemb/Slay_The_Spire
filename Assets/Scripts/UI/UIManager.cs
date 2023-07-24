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

    [Header("Deck")] 
    public Button drawDeckButton;

    public Button usedDeckButton;
    public Button expiredDeckButton;
    
    public Text drawDeckCount;
    public Text usedDeckCount;
    public Text expiredDeckCount;

    [Header("Cursor")] 
    public GameObject cursorObject;
    public Image imageCursor;

    [Header("BattleUI")] 
    public GameObject battleUI;
    private Camera camera;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        playerInfo.characterType.text = "the " + GameManager.instance.currentType;
        playerInfo.heartText.text = GameManager.instance.playerHp + "/" + GameManager.instance.playerMaxHp;
        playerInfo.goldText.text = GameManager.instance.playerGold.ToString();

        deckCount.text = GameManager.instance.fixedDeck.Count.ToString();
        camera = Camera.main;

        if (SceneManager.GetActiveScene().name.Equals("NeowScene"))
        {
            battleUI.SetActive(false);
            return;
        }

        battleUI.SetActive(true);
        
        drawDeckCount.text = GameManager.instance.drawDeck.Count.ToString();
        usedDeckCount.text = GameManager.instance.usedDeck.Count.ToString();
        expiredDeckCount.text = GameManager.instance.expiredDeck.Count.ToString();
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

    public void UpdateCardCount()
    {
        expiredDeckButton.gameObject.SetActive(GameManager.instance.expiredDeck.Count > 0);
        drawDeckCount.text = GameManager.instance.drawDeck.Count.ToString();
        usedDeckCount.text = GameManager.instance.usedDeck.Count.ToString();
        expiredDeckCount.text = GameManager.instance.expiredDeck.Count.ToString();
    }
}
