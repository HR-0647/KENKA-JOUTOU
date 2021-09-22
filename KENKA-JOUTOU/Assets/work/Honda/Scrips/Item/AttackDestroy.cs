using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject OBJ;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy2"))
        {
            collision.gameObject.GetComponent<Skeltons>().Damaged();
        }
        if (collision.gameObject.CompareTag("Enemy1"))
        {

        }
        if (collision.gameObject.CompareTag("Enemy3"))
        {

        }
    }
}
