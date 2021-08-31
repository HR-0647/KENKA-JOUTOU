using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject OBJ;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Skeltons>().Damaged();
        }
    }
}