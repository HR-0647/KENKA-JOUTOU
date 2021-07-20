using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveItem : MonoBehaviour
{
    [SerializeField]
    private GameObject Grab1;
    [SerializeField]
    private GameObject Grab2;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            if (Input.GetKeyDown("joystick button 2"))
            {
                GameObject InGrab = Instantiate(Resources.Load("Prefab/HaveItem"),Grab1.transform.position,Grab1.transform.rotation) as GameObject;
                InGrab.name = InGrab.name.Replace("(Clone)", "");
                InGrab.transform.Translate(0, 0.5f, 1);
                InGrab.transform.parent = Grab1.transform;
            }
            if (Input.GetKeyDown("joystick button 1"))
            {
                GameObject InGrab2 = Instantiate(Resources.Load("Prefab/HaveItem"),Grab2.transform.position, Grab2.transform.rotation) as GameObject;
                InGrab2.name = InGrab2.name.Replace("(Clone)", "");
                InGrab2.transform.Translate(0,0.5f,1);
                InGrab2.transform.parent = Grab2.transform;
            }
        }
    }
}
