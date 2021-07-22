using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyController : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            if (Input.GetAxis("CircleButton1") > 0.2f)
                Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Grab2"))
        {
            if (Input.GetAxis("CircleButton2") > 0.6f)
            {
                Destroy(gameObject);
            }
        }
    }
}
