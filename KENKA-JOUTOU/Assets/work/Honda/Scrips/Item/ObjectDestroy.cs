using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            if(Input.GetKeyDown("joystick button 2"))
            Destroy(gameObject);
        }else if (other.gameObject.CompareTag("Grab2"))
        {
            if(Input.GetKeyDown("joystick button 1"))
            {
                Destroy(gameObject);
            }
        }
    }
    
}
