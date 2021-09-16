using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// これはミミック用のスクリプトです
/// </summary>

public class MimicSearchArea : MonoBehaviour
{
    public int Area = 5;//カプセルコライダーのサイズを変更する
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(Area, 1, Area);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            //triggerをいじる
        }
    }
}
