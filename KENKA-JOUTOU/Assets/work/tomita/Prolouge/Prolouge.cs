using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prolouge : MonoBehaviour
{
    public Text text;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            text.text = "��l�͖�����|���ׂ�������̍Ő[���܂ŗ������A�����ɂ��ƈ���y�΂��s�k���Ă��܂����B���̌��l�͎􂢂��������A�S���ɕ����߂��Ă��܂����B";

        }
        
    }
}



