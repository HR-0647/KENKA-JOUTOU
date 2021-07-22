using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveItem : MonoBehaviour
{
    [SerializeField]
    private GameObject Grab1;
    [SerializeField]
    private GameObject Grab2;

    public bool fastPush = false;
    public bool push = false;
    public float nextButtonDownTime = 0.3f;
    private float nowTime = 0f;

    public bool twofastPush = false;
    public bool twopush = false;
    public float twonextButtonDownTime = 0.3f;
    private float twonowTime = 0f;

    GameObject InGrab;
    GameObject InGrab2;

    private void Update()
    {
        if (!fastPush)
        {
            if (Input.GetKeyDown("joystick 1 button 2"))
            {
                if (!push)
                {
                    push = true;
                    nowTime = 0f;
                }
                else if (InGrab != null)
                {
                    if (nowTime <= nextButtonDownTime)
                    {
                        fastPush = true;
                        InGrab.GetComponent<Rigidbody>().isKinematic = false;
                        InGrab.GetComponent<Rigidbody>().useGravity = true;
                        InGrab.transform.parent = null;
                    }
                }
            }
        }
        else if(Input.GetKeyDown("joystick 1 button 2"))
        {
            fastPush = false;
            push = false;
        }


        if (!twofastPush)
        {
            if (Input.GetKeyDown("joystick 2 button 2"))
            {
                if (!twopush)
                {
                    twopush = true;
                    twonowTime = 0f;
                }
                else if (InGrab2 != null)
                {
                    if (twonowTime <= twonextButtonDownTime)
                    {
                        twofastPush = true;
                        InGrab2.GetComponent<Rigidbody>().isKinematic = false;
                        InGrab2.GetComponent<Rigidbody>().useGravity = true;
                        InGrab2.transform.parent = null;
                    }
                }
            }
        }
        else if (Input.GetKeyDown("joystick 2 button 2"))
        {
            twofastPush = false;
            twopush = false;
        }


        if (push)
        {
            nowTime += Time.deltaTime;

            if (nowTime > nextButtonDownTime)
            {
                push = false;
            }
        }

        if (twopush)
        {
            twonowTime += Time.deltaTime;

            if (twonowTime > twonextButtonDownTime)
            {
                twopush = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            if (Input.GetKeyDown("joystick 1 button 2"))
            {
                InGrab = Instantiate(Resources.Load("Prefab/Brick (1)"),Grab1.transform.position,Grab1.transform.rotation) as GameObject;
                InGrab.name = InGrab.name.Replace("(Clone)", "");
                InGrab.transform.Translate(0, 0.5f, 1);
                InGrab.transform.parent = Grab1.transform;
            }

            if (Input.GetKeyDown("joystick 2 button 2"))
            {
                InGrab2 = Instantiate(Resources.Load("Prefab/Brick (1)"),Grab2.transform.position, Grab2.transform.rotation) as GameObject;
                InGrab2.name = InGrab2.name.Replace("(Clone)", "");
                InGrab2.transform.Translate(0,0.5f,1);
                InGrab2.transform.parent = Grab2.transform;
            }
        }
    }
}
