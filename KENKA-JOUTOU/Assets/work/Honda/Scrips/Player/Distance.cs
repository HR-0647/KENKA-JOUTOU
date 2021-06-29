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

    // ヒップアタック関係
    [SerializeField]
    private float stampTime = 5f;
    [SerializeField]
    private float boundPower=5f;

    // クールダウンタイム
    private float CoolTime;

    private float dis;
    private Vector3 A;
    private Vector3 B;


    private void Start()
    {
        p1 = GameObject.Find("SD_unitychan_humanoid");
        p2 = GameObject.Find("SD_unitychan_humanoid (1)");
    }

    private void Update()
    {

        A = target.transform.position;
        B = target2.transform.position;

        dis = Vector3.Distance(A, B);

        if (dis > 1 && Input.GetKey("joystick button 6") && Input.GetKey("joystick button 7"))
        {
            PL1.AddForce(-p1.transform.forward * boundPower, ForceMode.VelocityChange);
            PL2.AddForce(-p2.transform.forward * boundPower, ForceMode.VelocityChange);
        }
    }

    private void LateUpdate()
    {
        // キャラの距離が10を超えると遅くなる(互いに10以下であれば速度は減速しない)
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
