using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    [SerializeField]
    private GameObject colider;
    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;

    private void Start()
    {
        p1 = GameObject.Find("SD_unitychan_humanoid");
        p2 = GameObject.Find("SD_unitychan_humanoid (1)");
    }

    private void LateUpdate()
    {
        Vector3 A = target.transform.position;
        Vector3 B = target2.transform.position;

        float dis = Vector3.Distance(A, B);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            p1.GetComponent<Player1>().WalkSpeed = 5f;
            p2.GetComponent<Player2>().WalkSpeed = 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Player"|| other.gameObject.tag == "Player2")
        {
            p1.GetComponent<Player1>().WalkSpeed = 1f;
            p2.GetComponent<Player2>().WalkSpeed = 1f;
        }
    }
}
