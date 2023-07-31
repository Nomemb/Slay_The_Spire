using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.String;


public class BuffInfo : MonoBehaviour
{
    [SerializeField] private Sprite[] buffImageSprites;
    
    public Image buffImage;
    public Text buffDuration;

    public string buffName;
    
    private void Start()
    {
        buffImage.sprite = ChangeBuffImageSprite();
    }

    private Sprite ChangeBuffImageSprite()
    {
        Sprite img = null;
        var path = "48/" + buffName;
        foreach (var sprite in buffImageSprites)
        {
            if (Compare(path, sprite.name, StringComparison.OrdinalIgnoreCase) != 0) continue;
            img = sprite;
            break;
        }
        return img;
    }
}
