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
            text.text = "二人は魔王を倒すべく魔王城の最深部まで来たが、魔王にあと一歩及ばず敗北してしまった。その後二人は呪いをかけられ、牢屋に閉じ込められてしまった。";

        }
        
    }
}



