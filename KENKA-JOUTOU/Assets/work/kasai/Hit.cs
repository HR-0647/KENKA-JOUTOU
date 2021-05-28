using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public static float EnemyHealth = 100;

    //当たった時に呼ばれる関数
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyHealth = EnemyHealth - 10;
            Debug.Log("hit");
            if (EnemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
