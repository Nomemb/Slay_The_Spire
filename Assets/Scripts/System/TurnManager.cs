using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace System
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager instance;
        [SerializeField] private CharacterType characterType;
        [SerializeField] bool isPlayerTurn;
        [SerializeField] private CardDisplay hand;
        [SerializeField] private HpInteraction playerHpUi;

        [SerializeField] private UIBattleScene battleScene;

        public UnityEvent startBattle;
        public UnityEvent startPlayerTurn;
        public UnityEvent endPlayerTurn;
        public UnityEvent startEnemyTurn;
        public UnityEvent changePlayerCardCount;
        public UnityEvent changePlayerHp;

        private GameManager gm;
        private UIManager um;
        private PlayerController player;
        public List<BaseMonster> monsterList = null;
    
        void Awake()
        {
            if (instance == null) 
                instance = this;
            else 
                Destroy(gameObject);
        
        }
    
        private void Start()
        {
            gm = GameManager.instance;
            um = UIManager.instance;
        
            isPlayerTurn = gm.isPlayerTurn;
            characterType = gm.currentType;
        
            um.Init();
            battleScene.Init();
            hand = battleScene.GetComponentInChildren<CardDisplay>();
            player = FindObjectOfType<PlayerController>();
            EventSetting();
        
            StartBattle();
        }

        private void EventSetting()
        {
            // StartBattle Event
            startBattle.AddListener(um.Init);
            startBattle.AddListener(battleScene.Init);
            startBattle.AddListener(gm.BattleStart);
            startBattle.AddListener(player.BattleStart);
            startBattle.AddListener(StartPlayerTurn); 
        
            // StartPlayerTurn Event
            startPlayerTurn.AddListener(()=>gm.DrawCard(gm.currentDrawCardCount));
            startPlayerTurn.AddListener(hand.ImageSetting);
            startPlayerTurn.AddListener(um.UpdateDeckCountUI);
        
            // EndPlayerTurn Event
            endPlayerTurn.AddListener(gm.EndPlayerTurn);
            endPlayerTurn.AddListener(hand.EndPlayerTurn);
            endPlayerTurn.AddListener(battleScene.UpdateCardCount);
            endPlayerTurn.AddListener(StartEnemyTurn);
        
            // StartEnemyTurn Event
        
            // ChangePlayerCardCount Event
            changePlayerCardCount.AddListener(battleScene.UpdateCardCount);
        
            // ChangedPlayerHp Event
            changePlayerHp.AddListener(um.UpdateHpUI);
            changePlayerHp.AddListener(()=>playerHpUi.UpdateHpBar(gm.playerHp, gm.playerMaxHp));

        }
        private void StartBattle()
        {
            Debug.Log("전투 시작!");

            startBattle.Invoke();
        }
        public void StartPlayerTurn()
        {
            Debug.Log("플레이어 턴 시작!");

            startPlayerTurn.Invoke();
        }
    
        public void EndPlayerTurn()
        {
            if (!isPlayerTurn) return;
        
            Debug.Log("플레이어 턴 종료!");
            endPlayerTurn.Invoke();
        }

        private void StartEnemyTurn()
        {
            if (gm.isPlayerTurn) return;
        
            Debug.Log("상대 턴 시작!");
            startEnemyTurn.RemoveAllListeners();
            foreach (var monster in monsterList)
            {
                startEnemyTurn.AddListener(monster.DoingCurrentState);
            }

            gm.isPlayerTurn = true;
            gm.currentMana = gm.maxMana;
            startEnemyTurn.AddListener(StartPlayerTurn);
        
            startEnemyTurn.Invoke();
        }

        public void ChangePlayerCardCount()
        {
            changePlayerCardCount.Invoke();
        }

        public void ChangedPlayerHp()
        {
            Debug.Log("플레이어 HP 변경!");
            changePlayerHp.Invoke();
        }
    

    }
}
