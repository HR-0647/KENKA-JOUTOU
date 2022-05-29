using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int PlayerHP;//エネミーの体力
    public int defaultPlayerHP;
    public Slider Slider;

    private void Start()
    {
        Slider.value = 1;
        PlayerHP = defaultPlayerHP;
    }
}
