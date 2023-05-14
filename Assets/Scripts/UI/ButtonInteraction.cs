using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button;
    public Image image;
    public GameObject text;

    private RectTransform textTransform;           
    private Vector2 initOffset;                     // 초기의 RectTransform Left 값

    [SerializeField] private float moveUIDistance;  // UI를 이동시킬 값


    private void Awake()
    {
        button = this.gameObject;
        image = transform.GetChild(0).gameObject.GetComponent<Image>();
        text = transform.GetChild(1).gameObject;

        textTransform = text.GetComponent<RectTransform>();
        initOffset = textTransform.offsetMin;
    }

    IEnumerator OnPointerInteraction()
    {
        Debug.Log("OnPointer");
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);


        // 즉시 이동
        textTransform.offsetMin = new Vector2(initOffset.x + moveUIDistance, initOffset.y);
        Debug.Log(textTransform.offsetMin);

        yield return null;
    }

    IEnumerator OutPointerInteraction()
    {
        Debug.Log("OutPointer");
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        // 즉시 이동
        textTransform.offsetMin = initOffset;


        yield return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopCoroutine(OutPointerInteraction());
        StartCoroutine(OnPointerInteraction());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(OnPointerInteraction());
        StartCoroutine(OutPointerInteraction());
    }
}
