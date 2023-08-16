using UnityEngine;
using UnityEngine.UI;

public class HpInteraction : MonoBehaviour
{
    [SerializeField] private Text hpText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Image hpBG;

    [SerializeField] private GameObject blockUI;
    [SerializeField] private Text blockText;
    [SerializeField] private Slider blockSlider;

    
    public void UpdateHpBar(int hp, int maxHp)
    {
        hpText.text = hp + "/" + maxHp;
        hpSlider.value = 1 - (float)hp / maxHp;
    }
    
    public void UpdateBlockBar(int block, int hp, int maxHp)
    {
        if (block == 0)
        {
            blockUI.gameObject.SetActive(false);
            hpBG.gameObject.SetActive(true);
            return;
        }
        blockUI.gameObject.SetActive(true);
        hpBG.gameObject.SetActive(false);
        Debug.Log(block);
        blockText.text = block + "";
        blockSlider.value = 1 - (float)hp / maxHp;

        UpdateHpBar(hp, maxHp);
    }
}
