using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// これはミミック用のスクリプトです
/// </summary>

public class MimicSearchArea : MonoBehaviour
{
    public int Area = 5;//カプセルコライダーのサイズを変更する
    private float timeleft;             //タイマー
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(Area, 1, Area);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (timeleft < 0.0f)
        //{
        //    timeleft = 3.0f;
            if (other.CompareTag("Player") || other.CompareTag("Player2"))
            {
                //triggerをいじる
                mimic.Trigger = true;
                Debug.Log("a");
            }
        //}
    }
}
