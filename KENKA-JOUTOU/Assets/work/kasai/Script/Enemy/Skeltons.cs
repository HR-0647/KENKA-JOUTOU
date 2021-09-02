using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Skeltons : MonoBehaviour
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    public GameObject WireObject;       //ワイヤーオブジェクト

    //public Rigidbody rb;


    public static bool Trigger = true;         //巡回とターゲット切り替え
    public bool DamageTrigger = false;  //ダメージ処理切り替え

    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2
    private Vector3 WirePosition;       //ワイヤーの位置情報

    public float KnockbackSpeed = 5.0f;//ノックバックのスピード

    public int EnemyHP = 100;           //エネミーの体力

    private float timeleft;             //タイマー

    private float NavNotAttachmentTime = 3; //NavMeshを外された際の待ちタイム
    private float NavMeshInterval;

    //サウンド
    public AudioClip atksound;  //攻撃音
    //public AudioClip objectcllide;//衝突音
    AudioSource audioSource;

    //アニメーション
    //Animation Animation;
    public Animator anim;

    //NavMesh関連
    [SerializeField]
    private Transform[] m_targets = null;

    [SerializeField]

    private float m_destinationThreshold = 0.0f;

    private NavMeshAgent m_navAgent = null;

    private int m_targetIndex = 0;

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
    //private float range3;               //エネミーから糸までの距離

    //public float EnemySearchArea;       //敵の索敵範囲
    public float EnemyAtkInterval;      //敵の攻撃間隔

    // Start is called before the first frame update
    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.destination = CurretTargetPosition;

        //オーディオコンポーネント取得
        audioSource = GetComponent<AudioSource>();

        //アニメーターコンポーネント所得
        anim = GetComponent<Animator>();
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", true);

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
        if (DamageTrigger == true)
        {
            Damaged();
        }

        //プレイヤーと糸までの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        WirePosition = WireObject.transform.position;

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);
        //range3 = Vector3.Distance(WirePosition, transform.position);

        //タイマー
        timeleft -= Time.deltaTime;


        //ここからエネミーの行動パターン

        //エネミーの巡回

        if (m_navAgent.remainingDistance <= m_destinationThreshold && Trigger)
        {
            {
                m_targetIndex = (m_targetIndex + 1) % m_targets.Length;
                //移動アニメーション
                anim.SetBool("Walk", true);
                m_navAgent.destination = CurretTargetPosition;
            }
        }

        //検知範囲にプレイヤーか糸がいるなら接近する
        //if (range1 <= EnemySearchArea || range2 <= EnemySearchArea || range3 <= EnemySearchArea)
        //{
        //    Trigger = false;
        //}


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

                        //if (timeleft == 3.0)
                        //{
                        //    Debug.Log(timeleft);
                        //    //攻撃アニメーションを一時的に停止させる
                        //    anim.SetBool("Atk", false);
                        //}
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
        // ノックバックを受けた際にひるむ時間
        if (m_navAgent.enabled == false)
        {
            if (NavMeshInterval > NavNotAttachmentTime)
            {
                NavMeshInterval = 0.0f;
                m_navAgent.enabled = true;
                anim.SetBool("Walk", true);
                anim.SetBool("Atk", true);
            }
            else
            {
                NavMeshInterval += Time.deltaTime;
                anim.SetBool("Idol", true);
            }
        }
    }

    public void Damaged()//エネミーがダメージを受けた時の処理
    {
        var rigidbody = GetComponent<Rigidbody>();
        Debug.Log("hit");
        //transform.position -= transform.forward * KnockbackSpeed*Time.deltaTime;
        rigidbody.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange);
        DamageTrigger = false;
    }
    //当たった時に呼ばれる関数
    private void OnCollisionEnter(Collision collision)
    {
        ////プレイヤーと床以外のオブジェクトに衝突した場合
        //if (collision.gameObject.tag == "Untagged")
        //{
        //    //衝突音再生
        //    //audioSource.PlayOneShot(objectcllide);
        //    GetComponent<AudioSource>().PlayOneShot(objectcllide);
        //}
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
