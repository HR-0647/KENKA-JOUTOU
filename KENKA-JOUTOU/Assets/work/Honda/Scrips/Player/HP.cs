using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public int PlayerHP;//�G�l�~�[�̗̑�
    public int defaultPlayerHP;
    public Slider Slider;

    private void Start()
    {
        Slider.value = 1;
        PlayerHP = defaultPlayerHP;
    }
}
