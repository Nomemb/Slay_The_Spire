using UnityEngine;
using UnityEngine.UI;



public class BlinkImage : MonoBehaviour
{
    [SerializeField] private Color highlightColor;
    private float time;
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    void Update()
    {
        if(time<0.5f)
        {
            img.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 1-time);
        }
        else
        {
            img.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, time);

            if (time > 1f)
                time = 0f;
        }
        time += Time.deltaTime;
    }
}
