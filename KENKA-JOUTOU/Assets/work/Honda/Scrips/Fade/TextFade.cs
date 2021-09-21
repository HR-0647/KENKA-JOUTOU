using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public Text text;

    float fadeSpeed = 0.005f;
    float red, green, blue, alfa;
    public bool isFadeOut = false;
    public bool isFadeIn = false;

    void Start()
    {
        red = text.color.r;
        green = text.color.g;
        blue = text.color.b;
        alfa = text.color.a;
    }

    void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }

        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        alfa -= fadeSpeed;
        SetAlpha();
        if (alfa <= 0)
        {
            isFadeIn = false;
            text.enabled = false;
        }
    }

    void StartFadeOut()
    {
        text.enabled = true;
        alfa += fadeSpeed;
        SetAlpha();
        if (alfa >= 1)
        {
            isFadeOut = false;
        }
    }

    void SetAlpha()
    {
        text.color = new Color(red, green, blue, alfa);
    }
}