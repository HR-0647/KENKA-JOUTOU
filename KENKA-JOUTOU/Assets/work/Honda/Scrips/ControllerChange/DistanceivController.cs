using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceivController : MonoBehaviour
{
    public Transform target;
    public Transform target2;

    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;

    // distance用のvector
    private float dis;
    private Vector3 A;
    private Vector3 B;

    private void Start()
    {
        p1 = GameObject.Find("player_1_");
        p2 = GameObject.Find("player_2_");
    }

    private void Update()
    {
        A = target.transform.position;
        B = target2.transform.position;

        dis = Vector3.Distance(A, B);
    }

    private void LateUpdate()
    {
        // キャラの距離が10を超えると遅くなる(互いに10以下であれば速度は減速しない)
        // 13に達した時点で移動ができなくなる
        if (dis > 10)
        {
            p1.GetComponent<OnePController>().WalkSpeed = 1f;
            p2.GetComponent<TwoPController>().WalkSpeed = 1f;
        }
        else
        {
            p1.GetComponent<OnePController>().WalkSpeed = 5f;
            p2.GetComponent<TwoPController>().WalkSpeed = 5f;
        }

        if (dis > 13)
        {
            p1.GetComponent<OnePController>().WalkSpeed = 0.1f;
            p2.GetComponent<TwoPController>().WalkSpeed = 0.1f;
        }
    }
}
