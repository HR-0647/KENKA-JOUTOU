using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;               //UI���g�p�\�ɂ���
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float speed = 0.01f;     //�������̑���
    float alfa = 0;                   //A�l�𑀍삷�邽�߂̕ϐ�
    float red, green, blue;         //RGB�𑀍삷�邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //Panel�̐F������
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed;
    }
}
