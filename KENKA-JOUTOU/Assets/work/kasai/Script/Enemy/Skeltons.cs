using UnityEngine;
using UnityEngine.AI;
using System.Collections;



[RequireComponent(typeof(NavMeshAgent))]

public class Skeltons : Enemy
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2

    Rigidbody rb;

    public static bool Trigger = true;  //巡回とターゲット切り替え
    public bool DamageTrigger = false;  //ダメージ処理切り替え
    public bool invincible = false;     //無敵時間

    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2

    private float KnockbackSpeed = 50.0f;//ノックバックのスピード
    private Vector3 knockback = Vector3.zero;




    private float timeleft;             //タイマー

    //サウンド
    public AudioClip atksound;  //攻撃音

    AudioSource audioSource;

    //アニメーション
    public Animator anim;

    //NavMesh関連
    [SerializeField]
    private Transform[] m_targets = null;

    [SerializeField]

    private float m_destinationThreshold = 0.0f;

    private NavMeshAgent m_navAgent = null;

    private int m_targetIndex = 0;

    private bool navmashtrigger = true;

    private Vector3 CurretTargetPosition
    {
        get
        {
            if (m_targets == null || m_targets.Length <= m_targetIndex)
            {
                return Vector3.zero;
            }

            return m_targets[m_targetIndex].position;
        }
    }


    private float range1;               //エネミーからプレイヤー1までの距離
    private float range2;               //エネミーからプレイヤー2までの距離

    public float EnemyAtkInterval = 3.0f;      //敵の攻撃間隔

    // Start is called before the first frame update
    void Start()
    {
        type = EnemyType.Skeleton;
        EnemyHP = 20;//エネミー体力
        attack = 3;
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.destination = CurretTargetPosition;
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
            Destroy(this.gameObject);
        }

        //ダメージ処理呼び出し
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }

        //プレイヤーと糸までの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        //タイマー
        timeleft -= Time.deltaTime;


        //ここからエネミーの行動パターン

        //エネミーの巡回

        if (navmashtrigger)
        {
            if (Trigger)
            {
                if (m_navAgent.remainingDistance <= m_destinationThreshold)
                {

                    {

                        m_targetIndex = (m_targetIndex + 1) % m_targets.Length;
                        //移動アニメーション
                        anim.SetBool("Walk", true);
                        m_navAgent.destination = CurretTargetPosition;


                    }
                }
            }

            if (!Trigger)
            {
                //プレイヤー1の方が近い場合
                if (range1 <= range2)
                {
                    if (range1 > m_navAgent.stoppingDistance)
                    {
                        m_navAgent.destination = PlayerPosition1;
                        //移動アニメーション
                        anim.SetBool("Atk", false);
                        anim.SetBool("Walk", true);
                    }
                    else if (range1 < m_navAgent.stoppingDistance)
                    {

                        //プレイヤー1に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
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


                //プレイヤー2の方が近く、プレイヤー2と密着していない場合
                if (range2 < range1)
                {
                    if (range2 > m_navAgent.stoppingDistance)
                    {
                        m_navAgent.destination = PlayerPosition2;
                        //移動アニメーション
                        anim.SetBool("Atk", false);
                        anim.SetBool("Walk", true);
                    }
                    else if (range2 <= m_navAgent.stoppingDistance)
                    {

                        //プレイヤー2に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            

                            //攻撃アニメーション
                            anim.SetBool("Atk", true);
                            anim.SetBool("Walk", false);

                            //攻撃
                            Debug.Log("Atk");
                            //攻撃音再生
                            audioSource.PlayOneShot(atksound);
                            //攻撃アニメーションを一時的に停止させる
                            anim.SetBool("Atk", false);
                        }

                    }
                }




            }

        }
    }

    public IEnumerator Damaged()//エネミーがダメージを受けた時の処理
    {
        invincible = true;//無敵時間中はこの処理は行わない
        navmashtrigger = false;
        m_navAgent.enabled = false;
        rb.isKinematic = false;

        EnemyHP -= 10;
        Slider.value = (float)EnemyHP;//HPバー変動
        Debug.Log(Slider.value);

        rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //ノックバック
        //アニメーションをidol状態に移行
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", false);


        knockback = Vector3.zero;

        yield return new WaitForSeconds(2.0f);//数秒待機

        Debug.Log("hit");

        navmashtrigger = true;
        m_navAgent.enabled = true;
        rb.isKinematic = true;

        DamageTrigger = false;
        invincible = false;

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
