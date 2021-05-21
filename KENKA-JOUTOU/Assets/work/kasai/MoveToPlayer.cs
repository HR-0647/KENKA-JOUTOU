using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private GameObject PlayerObject;    //�v���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition;     //�v���C���[�̈ʒu���
    private Vector3 EnemyPosition;     //�G�l�~�[�̈ʒu���
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
