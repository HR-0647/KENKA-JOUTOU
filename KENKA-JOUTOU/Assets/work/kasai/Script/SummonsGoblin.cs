using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonsGoblin : Enemy
{

    Rigidbody rb;

    private GameObject player;
   

    public bool DamageTrigger = false;  //ダメージ処理切り替え
    public bool invincible = false;

    public float Spead = 5.0f;

    private float KnockbackSpeed = 25.0f;//ノックバックのスピード

    private float distance=0;

    public float EnemyAtkInterval = 3.0f;      //敵の攻撃間隔
    private float timeleft;             //タイマー
    //サウンド
    public AudioClip atksound;  //攻撃音

    AudioSource audioSource;

    //アニメーション
    public Animator anim;

    [SerializeField] GameObject Effect;//エフェクト
    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Goblin;
        EnemyHP = 40;//エネミー体力
        attack = 5;
        
        //オーディオコンポーネント取得
        audioSource = GetComponent<AudioSource>();

        //アニメーターコンポーネント所得
        anim = GetComponent<Animator>();
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", true);

        rb = GetComponent<Rigidbody>();


        player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //タイマー
        timeleft -= Time.deltaTime;

        //体力の判定
        if (EnemyHP <= 0)
        {
            GameObject effect = Instantiate(Effect) as GameObject;
            effect.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }

        //ダメージ処理呼び出し
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }


        distance = Vector3.Distance(player.transform.position,transform.position);

        if (distance > 1.0)
        {
            //移動アニメーション
            anim.SetBool("Walk", true);
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Spead * Time.deltaTime);
        }
        else if(distance<1.0)
        {
            if (timeleft <= 0.0)
            {
                timeleft = EnemyAtkInterval;


                //攻撃アニメーション
                anim.SetBool("Atk", true);
                anim.SetBool("Walk", false);

                //攻撃
                Debug.Log("Atk");
                //Debug.Log(timeleft);

                //攻撃音再生
                audioSource.PlayOneShot(atksound);

            }
        }
    }

    public IEnumerator Damaged()//エネミーがダメージを受けた時の処理
    {
        if (!invincible)
        {
            invincible = true;//無敵時間中はこの処理は行わない
            

            EnemyHP -= 10;
            Slider.value = (float)EnemyHP;//HPバー変動
            anim.SetBool("hit", true);
            rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //ノックバック

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(2.0f);//数秒待機

            anim.SetBool("hit", false);
            Debug.Log("hit");

            DamageTrigger = false;
            invincible = false;
            
        }

    }

    //当たった時に呼ばれる関数
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }
}
