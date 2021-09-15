using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    /// <summary>
    /// テスト用スクリプト
    /// </summary>
    bool a=false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(a);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(a);
            a = !a;
        }
    }
}
