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
    [SerializeField]
    private Rigidbody PL1;
    [SerializeField]
    private Rigidbody PL2;
    [SerializeField]
    private float stampTime = 5f;
    [SerializeField]
    private float boundPower=5f;

    private float CoolTime;
    private float dis;


    private void Start()
    {
        p1 = GameObject.Find("SD_unitychan_humanoid");
        p2 = GameObject.Find("SD_unitychan_humanoid (1)");
    }

    private void Update()
    {
        CoolTime -= Time.deltaTime;

        if (dis > 10 && CoolTime < 0)
        {
            PL1.AddForce(-p1.transform.position * boundPower,ForceMode.VelocityChange);
            PL2.AddForce(-p2.transform.position * boundPower,ForceMode.VelocityChange);

            CoolTime = stampTime;
        }
    }

    private void LateUpdate()
    {
        // ƒLƒƒƒ‰‚Ì‹——£‚ª10‚ð’´‚¦‚é‚Æ’x‚­‚È‚é(ŒÝ‚¢‚É10ˆÈ‰º‚Å‚ ‚ê‚Î‘¬“x‚ÍŒ¸‘¬‚µ‚È‚¢)
        Vector3 A = target.transform.position;
        Vector3 B = target2.transform.position;

        dis = Vector3.Distance(A, B);
        if (dis > 10)
        {
            p1.GetComponent<Player1>().WalkSpeed = 1f;
            p2.GetComponent<Player2>().WalkSpeed = 1f;
        }
        else
        {
            p1.GetComponent<Player1>().WalkSpeed = 5f;
            p2.GetComponent<Player2>().WalkSpeed = 5f;
        }
    }
}
