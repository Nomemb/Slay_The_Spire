using UnityEngine;
using UnityEngine.UI;

public class HpInteraction : MonoBehaviour
{
    [SerializeField] private Text hpText;
    [SerializeField] private Slider hpSlider;
    public void UpdateHpBar(int hp, int maxHp)
    {
        hpText.text = hp + "/" + maxHp;
        hpSlider.value = 1 - (float)hp / maxHp;
    }
}
