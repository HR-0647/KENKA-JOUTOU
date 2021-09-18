using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimic : Enemy
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    private GameObject Target;        //攻撃対象
    public GameObject Item;   //ドロップアイテム

    Rigidbody rb;

    public static bool Trigger = false;  //巡回とターゲット切り替え
    public bool DamageTrigger = false;  //ダメージ処理切り替え
    public bool invincible = false;     //無敵時間
    private bool process = false;       //処理中

    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2

    private float KnockbackSpeed = 5.0f;//ノックバックのスピード
    private float TackleSpeed = 5.0f;
    
    //サウンド
    public AudioClip atksound;  //攻撃音

    AudioSource audioSource;

    //アニメーション
    public Animator anim;

    private float range1;               //エネミーからプレイヤー1までの距離
    private float range2;               //エネミーからプレイヤー2までの距離

    public float EnemyAtkInterval = 3.0f;      //敵の攻撃間隔
    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Mimic;
        EnemyHP = 30;//エネミー体力
        attack = 20;
        
        //オーディオコンポーネント取得
        audioSource = GetComponent<AudioSource>();

        //アニメーターコンポーネント所得
        anim = GetComponent<Animator>();
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", true);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //体力の判定
        if (EnemyHP <= 0)
        {
            Instantiate(Item,this.transform.position,this.transform.rotation);          //アイテムのドロップ
            Destroy(this.gameObject);
        }

        //ダメージ処理呼び出し
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        if (Trigger)
        {
            if (range1 <= range2)
            {
                this.transform.LookAt(PlayerPosition1);
                Target = PlayerObject1;
                StartCoroutine(Atk());
            }
            else
            {
                this.transform.LookAt(PlayerPosition2);
                Target = PlayerObject2;
                StartCoroutine(Atk());
            }
        }
        else
        {
            StopCoroutine(Atk());
            //idol状態
            Debug.Log(Trigger);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }

    public IEnumerator Damaged()//エネミーがダメージを受けた時の処理
    {
        invincible = true;//無敵時間中はこの処理は行わない

        EnemyHP -= 1;
        Slider.value = (float)EnemyHP / defaultEnemyHP;//HPバー変動

        rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //ノックバック
        //アニメーションをidol状態に移行
        //anim.SetBool("Atk", false);
        //anim.SetBool("Walk", false);


        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(2.0f);//数秒待機

        Debug.Log("hit");

        DamageTrigger = false;
        invincible = false;

    }
    public IEnumerator Atk()
    {
        if (!process)
        {
            process = true;
            invincible = true;
            this.transform.LookAt(Target.transform.position);
            //transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, TackleSpeed);//プレイヤーに向かって突進する
            rb.AddForce(transform.forward * TackleSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.5f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            yield return new WaitForSeconds(EnemyAtkInterval);
            Debug.Log("atk");
            Trigger = false;
            invincible = false;
            process = false;
        }
    }
}
