using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipAttack : MonoBehaviour
{
    public Transform target;
    public Transform target2;

    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;

    // ヒップアタック関係
    [SerializeField]
    private float stampspeed = 5f;
    [SerializeField]
    private float timer = 1f;

    // クールダウンタイム
    private float CoolTime;

    // distance用のvector
    private float dis;
    private Vector3 A;
    private Vector3 B;

    // ヒップアタック用のvector
    Vector3 curPos;
    Vector3 curPos2;
    private bool isMove = false;

    // 今回の疑問としてはVectorは構造体で異なる値を一つにして利用するため随時更新する必要があった
    // クラスは参照をするので更新を書かなくとも元から取るので必要ない
    void Start()
    {
        p1 = GameObject.Find("SD_unitychan_humanoid");
        p2 = GameObject.Find("SD_unitychan_humanoid (1)");
    }

    // 現段階では設定した方のプレイヤーにヒップアタックさせることができた
    // X座標０地点に行かずに実装できた
    // なんとかして真ん中にしたい
    void Update()
    {
        A = target.transform.position;
        B = target2.transform.position;

        dis = Vector3.Distance(A, B);

        dis = target.transform.position.x;

        curPos = p1.transform.position;
        curPos2 = p2.transform.position;

        hipAttack();
    }

    private void hipAttack()
    {
        if (Input.GetKey("joystick button 6") && Input.GetKey("joystick button 7"))
        {
            isMove = true;
        }

        if (isMove)
        {
            curPos.x = Mathf.Lerp(curPos.x, dis, Time.deltaTime * stampspeed);
            curPos2.x = Mathf.Lerp(curPos2.x, dis, Time.deltaTime * stampspeed);

            p1.transform.position = curPos;
            p2.transform.position = curPos2;

            CoolTime -= Time.deltaTime;
        }

        if (CoolTime < 0)
        {
            isMove = false;
            CoolTime = timer;
        }
    }
}
