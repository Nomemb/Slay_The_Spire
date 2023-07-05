using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorText : MonoBehaviour
{
    public Text uiText;
    [SerializeField] private string colorCode;

    private void Start()
    {
        var length = uiText.text.Length;
        uiText.text = uiText.text[0] + "<color=#" + colorCode + ">" + uiText.text[1..(length - 1)] + "</color>" +
                      uiText.text[length - 1];
    }
    
}
