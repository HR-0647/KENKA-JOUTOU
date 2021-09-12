using UnityEngine;
using System.Collections;
using System.Collections.Concurrent;

public class HomingBullet : MonoBehaviour
{
    public GameObject PlayerObject1;  //プレイヤーオブジェクト1
    public GameObject PlayerObject2;  //プレイヤーオブジェクト2
    private GameObject Target;        //攻撃対象
       
    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2

    private Vector3 lastVelocity;
    private Rigidbody rb;

    private float range1;               //弾丸からプレイヤー1までの距離
    private float range2;               //弾丸からプレイヤー2までの距離

    public float BulletSpead;           //弾丸の速さ
    
    private bool reflect = true;        //反射の判定

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        
        //プレイヤーまでの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        //reflect=false;//テスト用
    }

    // Update is called once per frame
    void Update()
    {
        this.lastVelocity = this.rb.velocity;

        //プレイヤー1のほうが近い場合
        if (range1<=range2)
        {
            Target = PlayerObject1; //ターゲット決定
            StartCoroutine(Homing());
        }
        //プレイヤー2のほうが近い場合
        else if (range1>range2)
        {
            Target = PlayerObject2;//ターゲット決定
            StartCoroutine(Homing());
        }
        
       
    }

    public IEnumerator Homing()
    {
        if (reflect)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, BulletSpead * Time.deltaTime);
        }
        else
        {
            transform.position += transform.forward * BulletSpead * Time.deltaTime;   
        }
        Invoke("Destroy", 20.0f);
        yield break;

    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {         
            //
            //ワイヤーの体力を減らす処理とseとエフェクトをここに置く
            //

            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            if (reflect)
            {
                //反射する向きを指定
                Vector3 refrectVec = Vector3.Reflect(this.lastVelocity, collision.contacts[0].normal);
                this.rb.velocity = refrectVec;
                reflect = false;
            }
            else if (!reflect)
            {
                //こっちにはseとエフェクトのみ
                Destroy(this.gameObject);
            }
        }

    }
}
