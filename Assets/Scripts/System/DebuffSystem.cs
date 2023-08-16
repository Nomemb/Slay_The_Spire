using System.Collections.Generic;
using System.Linq;
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
        Slow                = 0x00000001,     // 카드 랜덤 코스트 
        Dexterity           = 0x00000002,     // 방어도 감소
        Focus               = 0x00000004,
        StrengthDown        = 0x00000008,
        Shackled            = 0x00000010,
        Strength            = 0x00000020,     // 지속효과
        NoDraw              = 0x00000040,
        Confused            = 0x00000080, 
        Poison              = 0x00000100,     // 아래로는 매 턴 감소되는 효과
        Frail               = 0x00000200,
        Vulnerable          = 0x00000400,
        Weak                = 0x00000800
    }

    public enum UniqueDebuffToEnemy
    {
        BlockReturn         = 0x00000001,
        Choked              = 0x00000002,
        CorpseExplosion     = 0x00000004,
        LockOn              = 0x00000008,
        Mark                = 0x00000010
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
        public Dictionary<string, int> sharedDebuffDict = new Dictionary<string, int>();
        
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
            
            sharedDebuffDict.Clear();
        }
        
        public void AddShareDebuff(string debuffName, int duration = 0)
        {
            SharedDebuff debuff = (SharedDebuff)Enum.Parse(typeof(SharedDebuff), debuffName);
            if ((sharedState & debuff) != debuff)
            {
                Debug.Log(debuff + " 디버프 생성");
                sharedState |= debuff;
                sharedDebuffDict.Add(debuffName, duration);

                AddDebuff(debuffName, duration);
            }
            else
            {
                sharedDebuffDict[debuffName] += duration;
                Debug.Log("현재 " + debuffName + " 디버프 : " + sharedDebuffDict[debuffName]);
                foreach (var debuffIcon in debuffUI)
                {
                    var info = debuffIcon.GetComponent<BuffInfo>();
                    if(info.buffName != debuffName) continue;

                    info.buffDuration.text = sharedDebuffDict[debuffName].ToString();
                }
            }

            PrintDebuffDictLog();
        }

        private void AddDebuff(string debuffName, int duration = 0)
        {
            GameObject temp = Instantiate(uiPrefab, transform.position, Quaternion.identity);
            BuffInfo info = temp.GetComponent<BuffInfo>();
            info.buffName = debuffName;
            info.buffDuration.text = duration.ToString();
            temp.transform.SetParent(debuffUi.transform, false);
            debuffUI.Add(temp);
        }
        private void PrintDebuffDictLog()
        {
            foreach (var debuff in sharedDebuffDict)
            {
                Debug.Log(debuff.Key + " " + debuff.Value);
            }
        }

        public void UpdateDebuffCountByTurnEnd()
        {
            DecreaseSharedDebuffDictByTurnEnd();
        }

        private void DecreaseSharedDebuffDictByTurnEnd()
        {
            var index = 0;
            var removeDebuff = new List<string>();
            foreach (var debuff in sharedDebuffDict.ToList())
            {
                var temp = (SharedDebuff)Enum.Parse(typeof(SharedDebuff), debuff.Key);
                if ((int)temp < 256) continue;          // 해당 비트 이후부터는 매 턴 카운트가 감소함.
                
                 if (sharedDebuffDict[debuff.Key] == 1)  // 다음 턴에 효과가 사라지는 경우
                 {
                     Destroy(debuffUI[index]);
                     debuffUI.RemoveAt(index);
                     sharedState &= ~temp;               // 해당 비트값 제거
                     removeDebuff.Add(debuff.Key);
                 }
                 else
                 {
                    sharedDebuffDict[debuff.Key]--;
                    var info = debuffUI[index].GetComponent<BuffInfo>();
                    info.UpdateBuffDuration(sharedDebuffDict[debuff.Key]);
                    index++;
                }
            }

            foreach (var item in removeDebuff)
            {
                sharedDebuffDict.Remove(item);
            }

            PrintDebuffDictLog();
        }
        
    }
}