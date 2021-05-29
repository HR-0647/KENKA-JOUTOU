using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveToPlayer : MonoBehaviour
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト
    public GameObject PlayerObject2;    //プレイヤーオブジェクト
    private GameObject WireObject;      //ワイヤーオブジェクト
    private Vector3 PlayerPosition1;     //プレイヤーの位置情報
    private Vector3 PlayerPosition2;     //プレイヤーの位置情報
    private Vector3 EnemyPosition;      //エネミーの位置情報
    private Vector3 WirePosition;       //ワイヤーの位置情報

    private float x1;
    private float x2;
    private float z1;
    private float z2;

    private float range1;
    private float range2;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerObject = GameObject.FindWithTag("Player");
        WireObject = GameObject.FindWithTag("Wire");

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        WirePosition = WireObject.transform.position;
        EnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        EnemyPosition = transform.position;
        WirePosition = WireObject.transform.position;

        x1 = PlayerPosition1.x - EnemyPosition.x;
        x2 = PlayerPosition2.x - EnemyPosition.x;
        z1 = PlayerPosition1.z - EnemyPosition.z;
        z2 = PlayerPosition2.z - EnemyPosition.z;

        range1 = Mathf.Abs(x1) + Mathf.Abs(z1);
        range2 = Mathf.Abs(x2) + Mathf.Abs(z2);

        if (Hit.EnemyHealth == 100)
        {

            if (WirePosition.x > EnemyPosition.x)
            {
                EnemyPosition.x = EnemyPosition.x + 0.01f;
            }
            else if (WirePosition.x < EnemyPosition.x)
            {
                EnemyPosition.x = EnemyPosition.x - 0.01f;
            }

            if (WirePosition.z > EnemyPosition.z)
            {
                EnemyPosition.z = EnemyPosition.z + 0.01f;
            }
            else if (WirePosition.z < EnemyPosition.z)
            {
                EnemyPosition.z = EnemyPosition.z - 0.01f;
            }

        }

        else if (Hit.EnemyHealth < 100)
        {
            if (range1 <= range2)
            {
                if (PlayerPosition1.x > EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x + 0.01f;
                }
                else if (PlayerPosition1.x < EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x - 0.01f;
                }

                if (PlayerPosition1.z > EnemyPosition.z)
                {
                    EnemyPosition.z = EnemyPosition.z + 0.01f;
                }
                else if (PlayerPosition1.z < EnemyPosition.z)
                {
                    EnemyPosition.z = EnemyPosition.z - 0.01f;
                }
            }
            else if (range2 < range1)
            {
                if (PlayerPosition2.x > EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x + 0.01f;
                }
                else if (PlayerPosition2.x < EnemyPosition.x)
                {
                    EnemyPosition.x = EnemyPosition.x - 0.01f;
                }

                if (PlayerPosition2.z > EnemyPosition.z)
                {
                    EnemyPosition.z = EnemyPosition.z + 0.01f;
                }
                else if (PlayerPosition2.z < EnemyPosition.z)
                {
                    EnemyPosition.z = EnemyPosition.z - 0.01f;
                }
            }


        }

        transform.position = EnemyPosition;

    }
}