using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    public float BulletSpead;           //弾丸の速さ
    public int TargetCount;             //追尾のループ回数


    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2

    private bool TargetTrigger = true;  //ターゲット決定用

    private float timeleft;             //タイマー

    private float range1;               //弾丸からプレイヤー1までの距離
    private float range2;               //弾丸からプレイヤー2までの距離

    public float ReserchTime;            //追尾して再度プレイヤーのほうを見るまでの時間

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーとエネミーまでの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        if (range1 <= range2)
        {
            TargetTrigger = true;
        }
        else if (range1 > range2)
        {
            TargetTrigger = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーとエネミーまでの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        //自分に近いプレイヤーのほうを見る処理
        if (TargetTrigger == true)
        {
            for(int i=0; i<TargetCount;i++)
            {
                //プレイヤー1のほうに方向転換
                transform.LookAt(PlayerObject1.transform);
                //プレイヤー1に向かって直進(ここでbulletに速度を加える)
                transform.position -= transform.forward * BulletSpead;
                //停止(ここでスピードを0にする)
                transform.position -= transform.forward * 0;
                //タイマーをここにおいて静止する時間を設ける
                if (timeleft <= 0.0)
                {
                    i += 1;
                }
            }
            Destroy(this.gameObject);

        }
        else
        {
            for (int i = 0; i < TargetCount; i++)
            {
                //プレイヤー1のほうに方向転換
                transform.LookAt(PlayerObject1.transform);
                //プレイヤー1に向かって直進(ここでbulletに速度を加える)
                transform.position -= transform.forward * BulletSpead;
                //停止(ここでスピードを0にする)
                transform.position -= transform.forward * 0;
                //タイマーをここにおいて静止する時間を設ける
                if (timeleft <= 0.0)
                {
                    i += 1;
                }
            }
            Destroy(this.gameObject);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            //
            //プレイヤーの体力を減らす処理とseとエフェクトをここに置く
            //

            Destroy(this.gameObject);
        }

    }
}
