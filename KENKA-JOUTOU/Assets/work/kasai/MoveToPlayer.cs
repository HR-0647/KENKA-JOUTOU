using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject PlayerObject;    //�v���C���[�I�u�W�F�N�g
    private GameObject WireObject;      //���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition;     //�v���C���[�̈ʒu���
    private Vector3 EnemyPosition;      //�G�l�~�[�̈ʒu���
    private Vector3 WirePosition;       //���C���[�̈ʒu���
    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.FindWithTag("Player");
        WireObject = GameObject.FindWithTag("Wire");

        PlayerPosition = PlayerObject.transform.position;
        WirePosition = WireObject.transform.position;
        EnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerPosition = PlayerObject.transform.position;
        EnemyPosition = transform.position;
        WirePosition = WireObject.transform.position;
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
            transform.position = EnemyPosition;
        }

        else if (Hit.EnemyHealth < 100)
        {
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
}
