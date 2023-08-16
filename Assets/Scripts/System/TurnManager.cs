using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace System
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager instance;
        
        [Header("PlayerState")]
        [Space(3f)]
        [SerializeField] private CharacterType characterType;
        [SerializeField] bool isPlayerTurn;
        
        [SerializeField] private UIBattleScene battleScene;
        [SerializeField] private CardDisplay hand;
        [SerializeField] private HpInteraction playerHpUi;

        public UnityEvent startBattle;
        public UnityEvent startPlayerTurn;
        public UnityEvent isClearStage;
        public UnityEvent endPlayerTurn;
        public UnityEvent startEnemyTurn;
        public UnityEvent changePlayerCardCount;
        public UnityEvent changePlayerHp;

        [Space(10f)]
        [Header("Components")]
        [Space(3f)]
        [SerializeField] private GameManager gm;
        [SerializeField] private UIManager um;
        [SerializeField] private RelicManager rm;
        [SerializeField] private PlayerController player;
        [SerializeField] private RewardSystem reward;
        public List<BaseMonster> monsterList = null;

        [Space(10f)] [Header("Action")] [Space(3f)]
        public BuffSystem playerBuff;
        public DebuffSystem playerDebuff;

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
            rm = RelicManager.instance;
        
            isPlayerTurn = gm.isPlayerTurn;
            characterType = gm.currentType;
        
            um.Init();
            battleScene.Init();
            hand = battleScene.GetComponentInChildren<CardDisplay>();
            player = FindObjectOfType<PlayerController>();
            gm.player = player;
            reward = battleScene.GetComponentInChildren<RewardSystem>();
            EventSetting();
        }

        private void EventSetting()
        {
            // StartBattle Event
            startBattle.AddListener(um.Init);
            startBattle.AddListener(battleScene.Init);
            startBattle.AddListener(gm.BattleStart);
            startBattle.AddListener(player.BattleStart);
            startBattle.AddListener(StartPlayerTurn);
            startBattle.AddListener(ChangedPlayerHp);
        
            // StartPlayerTurn Event
            startPlayerTurn.AddListener(gm.StartPlayerTurn);
            startPlayerTurn.AddListener(()=>playerHpUi.UpdateBlockBar(gm.block,gm.playerHp, gm.playerMaxHp));
            startPlayerTurn.AddListener(()=>gm.DrawCard(gm.currentDrawCardCount));
            startPlayerTurn.AddListener(hand.ImageSetting);
            startPlayerTurn.AddListener(um.UpdateDeckCountUI);
            
            // IsClearStage Event
            isClearStage.AddListener(reward.ViewReward);
        
            // EndPlayerTurn Event
            endPlayerTurn.AddListener(gm.EndPlayerTurn);
            endPlayerTurn.AddListener(hand.EndPlayerTurn);
            endPlayerTurn.AddListener(battleScene.UpdateCardCount);
            endPlayerTurn.AddListener(playerBuff.UpdateBuffCountByTurnEnd);
            endPlayerTurn.AddListener(playerDebuff.UpdateDebuffCountByTurnEnd);
            endPlayerTurn.AddListener(StartEnemyTurn);
        
            // StartEnemyTurn Event
        
            // ChangePlayerCardCount Event
            changePlayerCardCount.AddListener(battleScene.UpdateCardCount);
        
            // ChangedPlayerHp Event
            changePlayerHp.AddListener(um.UpdateHpUI);
            changePlayerHp.AddListener(()=>playerHpUi.UpdateBlockBar(gm.block, gm.playerHp, gm.playerMaxHp));
            changePlayerHp.AddListener(()=>playerHpUi.UpdateHpBar(gm.playerHp, gm.playerMaxHp));

        }
        public void StartBattle()
        {
            Debug.Log("전투 시작!");

            startBattle.Invoke();
        }
        public void StartPlayerTurn()
        {
            Debug.Log("플레이어 턴 시작!");

            startPlayerTurn.Invoke();
            Debug.Log(monsterList.Count);
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

            foreach (var monster in monsterList)
            {
                monster.ResetBlock();
                startEnemyTurn.AddListener(monster.DoingCurrentState);
                var buffSys = monster.GetComponent<BuffSystem>();
                var debuffSys = monster.GetComponent<DebuffSystem>();
            }

            gm.isPlayerTurn = true;
            gm.currentMana = gm.maxMana;
            startEnemyTurn.AddListener(StartPlayerTurn);
        
            startEnemyTurn.Invoke();
            startEnemyTurn.RemoveAllListeners();
            foreach (var monster in monsterList)
            {
                var buffSys = monster.GetComponent<BuffSystem>();
                var debuffSys = monster.GetComponent<DebuffSystem>();
                
                startEnemyTurn.AddListener(buffSys.UpdateBuffCountByTurnEnd);
                startEnemyTurn.AddListener(debuffSys.UpdateDebuffCountByTurnEnd);
            }
        }

        public void ChangePlayerCardCount()
        {
            changePlayerCardCount.Invoke();
        }

        public void ChangedPlayerHp()
        {
            changePlayerHp.Invoke();
        }


        public void IsClearStage()
        {
            if (!IsCleared()) return;

            isClearStage.Invoke();
        }
        private bool IsCleared()
        {
            return monsterList.Count == 0;
        }
    }
}
