using UnityEngine;
using UnityEngine.AI;
using Obi;

[RequireComponent(typeof(NavMeshAgent))]

public class Skeltons : MonoBehaviour
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    public GameObject WireObject;      //ワイヤーオブジェクト

    public bool trigger = true;         //巡回とターゲット切り替え

    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2
    private Vector3 WirePosition;       //ワイヤーの位置情報

    public int EnemyHP = 100;      //エネミーの体力

    private float timeleft;

    //サウンド
    public AudioClip atksound;  //攻撃音
    //public AudioClip objectcllide;//衝突音
    AudioSource audioSource;

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
    private float range3;               //エネミーから糸までの距離

    public float EnemySearchArea;       //敵の索敵範囲
    public float EnemyAtkInterval;      //敵の攻撃間隔

    // Start is called before the first frame update
    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.destination = CurretTargetPosition;

        //オーディオコンポーネント取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーと糸までの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        WirePosition = WireObject.transform.position;
                
        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);
        range3 = Vector3.Distance(WirePosition, transform.position);

        //タイマー
        timeleft -= Time.deltaTime;


        //ここからエネミーの行動パターン
   
        //エネミーの巡回

        if (m_navAgent.remainingDistance <= m_destinationThreshold && trigger)
        {
            {
                m_targetIndex = (m_targetIndex + 1) % m_targets.Length;

                m_navAgent.destination = CurretTargetPosition;
                

            }
        }

        //検知範囲にプレイヤーか糸がいるなら接近する
        if (range1 <= EnemySearchArea || range2 <= EnemySearchArea || range3 <= EnemySearchArea)
        {
            trigger = false;
        }

                
        if (!trigger)
        {
                //プレイヤー1の方が近い場合
                if (range1 <= range2)
                {                    
                    m_navAgent.destination = PlayerPosition1;

                    if (range1 < m_navAgent.stoppingDistance)
                    {
                        
                        //プレイヤー1に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            //攻撃
                            Debug.Log("Atk");

                            //攻撃音再生
                            audioSource.PlayOneShot(atksound);
                        }

                }

            }
                
           
                //プレイヤー2の方が近く、プレイヤー2と密着していない場合
                if (range2 < range1)
                {                    
                    m_navAgent.destination = PlayerPosition2;

                    if (range2 <= m_navAgent.stoppingDistance)
                    {                        
                        //プレイヤー2に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            //攻撃
                            Debug.Log("Atk");
                            //攻撃音再生
                            audioSource.PlayOneShot(atksound);
                        }

                }
            }
                
                


        }

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
        if (collision.gameObject.tag == "Player"|| collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Obi")
        {
            EnemyHP -= 20;
            Debug.Log("hit");
            
        }
        //体力の判定
        if (EnemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
