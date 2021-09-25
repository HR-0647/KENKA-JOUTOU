using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leghold_trap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            //Destroy(gameObject);
        }
    }
}
