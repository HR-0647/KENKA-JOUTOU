using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    //public GameObject PlayerObject1;  //プレイヤーオブジェクト1
    //public GameObject PlayerObject2;  //プレイヤーオブジェクト2
    public GameObject WireObject;       //ワイヤーオブジェクト
    private float Wireposx;             //ワイヤーのx座標 
    private float Wireposy;             //ワイヤーのy座標
    public float BulletSpead;           //弾丸の速さ
    public int TargetCount;             //追尾のループ回数


    //private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    //private Vector3 PlayerPosition2;    //プレイヤーの位置情報2
    private Vector3 WirePosition;       //ワイヤーの位置情報

    //private bool TargetTrigger = true;  //ターゲット決定用

    private float timeleft;             //タイマー

    //private float range1;               //弾丸からプレイヤー1までの距離
    //private float range2;               //弾丸からプレイヤー2までの距離

    public float ReserchTime;            //追尾して再度ワイヤーのほうを見るまでの時間

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ワイヤーとエネミーまでの距離を出す
        WirePosition = WireObject.transform.position;
        //Transform bulletTransform = this.transform;

        //for(int i=0; i<TargetCount;i++)
        //{
        //Wireposx = WirePosition.x;
        //Wireposy = WirePosition.y;

        //停止(ここでスピードを0にする)
        //transform.position += transform.forward * 0;
        //タイマーをここにおいて静止する時間を設ける
        //if (timeleft <= 0.0)
        //{
        //    timeleft = ReserchTime;
        //    i += 1;
        //}
        //if(TargetCount==i)
        // {
        //     Destroy(this.gameObject);
        //}
        //}
        //ワイヤーのほうに方向転換
        transform.LookAt(WireObject.transform);
        //ワイヤーに向かって直進
        transform.position += transform.forward * BulletSpead * Time.deltaTime;


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Obi Solver" || collision.gameObject.name == "DammyWire")
        {
            //
            //ワイヤーの体力を減らす処理とseとエフェクトをここに置く
            //

            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            //こっちにはseとエフェクトのみ
            Destroy(this.gameObject);
        }

    }
}
