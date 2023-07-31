using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// https://slay-the-spire.fandom.com/wiki/Buffs#Player
namespace System
{
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
        public Dictionary<string, int> buffDict = new Dictionary<string, int>();
        
        public SharedBuff sharedState;
        public PlayerBuff playerState;
        public EnemyBuff enemyState;
        
        public GameObject buffUi;
        public GameObject uiPrefab;
        
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            sharedState = 0x00000000;
            playerState = 0x00000000;
            enemyState = 0x00000000;
            
            buffDict.Clear();
        }
    }
}
