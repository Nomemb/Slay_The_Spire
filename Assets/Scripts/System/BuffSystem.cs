using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// https://slay-the-spire.fandom.com/wiki/Buffs#Player
namespace System
{
    [Flags]
    public enum SharedBuff
    {
        Artifact,
        Barricade,
        Buffer,
        Dexterity,
        DrawCard,
        Energized,
        Focus,
        Intangible,
        Mantra,
        Metallicize,
        NextTurnBlock,
        PlatedArmor,
        Regen,
        Ritual,
        Strength,
        Thorns,
        Vigor
    }

    public enum PlayerBuff
    {
        Accuracy,
        AfterImage,
        Amplify,
        BattleHymn,
        Berserk,
        Blasphemer,
        Blur,
        Brutality,
        Burst,
        Collect,
        Combust,
        Corruption,
        CreativeAI,
        DarkEmbrace,
        DemonForm,
        Deva,
        Devotion,
        DoubleDamage,
        DoubleTap,
        Duplication,
        EchoFrom,
        Electro,
        Envenom,
        Equilibrium,
        Establishment,
        Evolve,
        FeelNoPain,
        FireBreathing,
        FlameBarrier,
        Foresight,
        FreeAttackPower,
        HeatSink,
        Hello,
        
    }

    [Flags]
    public enum EnemyBuff
    {
        Angry,
        BackAttack,
        BeatOfDeath,
        Curiosity,
        CurlUp,
        Enrage,
        Explosive,
        Fading,
        Invincible,
        LifeLink,
        Malleable,
        Minion,
        ModeShift,
        PainfulStabs,
        Reactive,
        SharpHide,
        Shifting,
        Split,
        SporeCloud,
        Stasis,
        StrengthUp,
        Thievery,
        TimeWarp,
        Unawakened
    }
    
    public class BuffSystem : MonoBehaviour
    {
        public Dictionary<string, int> sharedBuffDict = new Dictionary<string, int>();
        public Dictionary<string, int> enemyBuffDict = new Dictionary<string, int>();
        
        public SharedBuff sharedState;
        public PlayerBuff playerState;
        public EnemyBuff enemyState;
        
        public GameObject buffUi;
        public GameObject uiPrefab;
        
        public List<GameObject> buffUI = new List<GameObject>();

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            sharedState = 0x00000000;
            playerState = 0x00000000;
            enemyState = 0x00000000;
            
            sharedBuffDict.Clear();
        }
        
        public void AddShareBuff(string buffName, int duration = 0)
        {
            var buff = (SharedBuff)Enum.Parse(typeof(SharedBuff), buffName);
            if ((sharedState & buff) != buff)
            {
                Debug.Log(buff + " 버프 생성");
                sharedState |= buff;
                sharedBuffDict.Add(buffName, duration);

                AddBuff(buffName, duration);
            }
            else
            {
                sharedBuffDict[buffName] += duration;
                Debug.Log("현재 " + buffName + " 버프 : " + sharedBuffDict[buffName]);
                foreach (var debuffIcon in buffUI)
                {
                    var info = debuffIcon.GetComponent<BuffInfo>();
                    if(info.buffName != buffName) continue;

                    info.UpdateBuffDuration(sharedBuffDict[buffName]);
                }
            }

            PrintBuffDictLog();
        }

        public void AddEnemyBuff(string buffName, int duration = 0)
        {
            EnemyBuff buff = (EnemyBuff)Enum.Parse(typeof(EnemyBuff), buffName);
            if ((enemyState & buff) != buff)
            {
                Debug.Log(buff + " 버프 생성");
                enemyState |= buff;
                enemyBuffDict.Add(buffName, duration);

                AddBuff(buffName, duration);
            }
        }

        private void AddBuff(string buffName, int duration = 0)
        {
            GameObject temp = Instantiate(uiPrefab, transform.position, Quaternion.identity);
            BuffInfo info = temp.GetComponent<BuffInfo>();
            temp.name = buffName;
            info.buffName = buffName;
            info.UpdateBuffDuration(duration);
            temp.transform.SetParent(buffUi.transform, false);
            buffUI.Add(temp);
        }

        private void PrintBuffDictLog()
        {
            foreach (var debuff in sharedBuffDict)
            {
                Debug.Log(debuff.Key + " " + debuff.Value);
            }
        }
        
        public void UpdateBuffCountByTurnEnd()
        {
            DecreaseShareBuffDictByTurnEnd();
        }

        private void DecreaseShareBuffDictByTurnEnd()
        {
            foreach (var buff in sharedBuffDict)
            {
                var temp = (SharedBuff)Enum.Parse(typeof(SharedBuff), buff.Key);
                Debug.Log(temp + " " + (byte)temp);
            }

            PrintBuffDictLog();
        }

        public void RemoveBuff(Dictionary<string, int> dict, string buffName)
        {
            if (dict.ContainsKey(buffName))
            {
                Destroy(buffUI[buffUI.FindIndex(x=> string.Compare(x.name, buffName, StringComparison.OrdinalIgnoreCase)==0)]);
                dict.Remove(buffName);
            }
        }
    }
}
