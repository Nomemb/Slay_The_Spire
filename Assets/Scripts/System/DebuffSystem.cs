using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// https://slay-the-spire.fandom.com/wiki/Debuff
namespace System
{
    [Flags]
    public enum SharedDebuff
    {
        Confused            = 0x00000001,     // 카드 랜덤 코스트 
        Dexterity           = 0x00000002,     // 방어도 감소
        Focus               = 0x00000004,
        Frail               = 0x00000008,
        NoDraw              = 0x00000010,
        Poison              = 0x00000020,
        Shackled            = 0x00000040,
        Slow                = 0x00000080, 
        Strength            = 0x00000100,
        StrengthDown        = 0x00000200,
        Vulnerable          = 0x00000400,
        Weak                = 0x00000800,
    }

    public enum UniqueDebuffToEnemy
    {
        BlockReturn         = 0x00000001,
        Choked              = 0x00000002,
        CorpseExplosion     = 0x00000004,
        LockOn              = 0x00000008,
        Mark                = 0x00000010,
    }

    public enum UniqueDebuffToPlayer
    {
        Bias            = 0x00000001,
        Constricted     = 0x00000002,
        DrawReduction   = 0x00000004,
        Entangled       = 0x00000008,
        Fasting         = 0x00000010,
        Hex             = 0x00000020,
        NoBlock         = 0x00000040,
        WraithForm      = 0x00000080
    }
    public class DebuffSystem : MonoBehaviour
    {
        public Dictionary<string, int> debuffDict = new Dictionary<string, int>();
        
        public SharedDebuff sharedState;
        public UniqueDebuffToPlayer playerUniqueState;
        public UniqueDebuffToEnemy enemyUniqueState;

        public GameObject debuffUi;
        public GameObject uiPrefab;

        public List<GameObject> debuffUI = new List<GameObject>();
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            sharedState = 0x00000000;
            playerUniqueState = 0x00000000;
            enemyUniqueState = 0x00000000;
            
            debuffDict.Clear();
        }
        
        public void AddShareDebuff(string debuffName, int duration = 0)
        {
            SharedDebuff debuff = (SharedDebuff)Enum.Parse(typeof(SharedDebuff), debuffName);
            if ((sharedState & debuff) != debuff)
            {
                Debug.Log(debuff + " 디버프 생성");
                sharedState |= debuff;
                debuffDict.Add(debuffName, duration);

                GameObject temp = Instantiate(uiPrefab, transform.position, quaternion.identity);
                BuffInfo info = temp.GetComponent<BuffInfo>();
                info.buffName = debuffName;
                info.buffDuration.text = duration.ToString();
                temp.transform.SetParent(debuffUi.transform, false);
                debuffUI.Add(temp);
            }
            else
            {
                debuffDict[debuffName] += duration;
                Debug.Log("현재 " + debuffName + " 디버프 : " + debuffDict[debuffName]);
                foreach (var debuffIcon in debuffUI)
                {
                    var info = debuffIcon.GetComponent<BuffInfo>();
                    if(info.buffName != debuffName) continue;

                    info.buffDuration.text = debuffDict[debuffName].ToString();
                }
            }
        }
    }
}