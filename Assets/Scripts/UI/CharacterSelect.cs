using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Sprite[] characterBackgroundImages;
    public Image backgroundImage;
    public Image[] characterButtonBackgrounds;


    public CharacterType characterType;

    public void SelectCharacter(int _typeNum)
    {
        backgroundImage.gameObject.SetActive(true);
        characterType = (CharacterType)_typeNum;
        GameManager.instance.currentType = characterType;                   // 선택된 캐릭터로 게임매니저 바꿔줌

        backgroundImage.sprite = characterBackgroundImages[(int)characterType];    // 해당 캐릭터 이미지로 BGI 변경
        for(int i=0; i<characterButtonBackgrounds.Length; i++) 
        {
            if(i == (int)characterType)
            {
                characterButtonBackgrounds[i].gameObject.SetActive(true);
            }
            else
            {
                characterButtonBackgrounds[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectCharacterBack()
    {
        backgroundImage.gameObject.SetActive(false);

        for (int i = 0; i < characterButtonBackgrounds.Length; i++)
        {
            characterButtonBackgrounds[i].gameObject.SetActive(false);
        }
    }

}
