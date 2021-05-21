using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject PlayerObject;    //プレイヤーオブジェクト
    private Vector3 PlayerPosition;     //プレイヤーの位置情報
    private Vector3 EnemyPosition;     //エネミーの位置情報
    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.FindWithTag("Player");

        PlayerPosition = PlayerObject.transform.position;
        EnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition = PlayerObject.transform.position;
        EnemyPosition = transform.position;

        if (PlayerPosition.x > EnemyPosition.x)
        {
            EnemyPosition.x = EnemyPosition.x + 0.01f;
        }
        else if (PlayerPosition.x < EnemyPosition.x)
        {
            EnemyPosition.x = EnemyPosition.x - 0.01f;
        }
        if (PlayerPosition.z > EnemyPosition.z)
        {
            EnemyPosition.z = EnemyPosition.z + 0.01f;
        }
        else if (PlayerPosition.z < EnemyPosition.z)
        {
            EnemyPosition.z = EnemyPosition.z - 0.01f;
        }

        transform.position = EnemyPosition;
    }
}
