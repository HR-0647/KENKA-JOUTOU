using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MoveToPlayer : MonoBehaviour
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    public GameObject WireObject;      //ワイヤーオブジェクト
    //public Transform PlayerDistance1;
    //public Transform PlayerDistance2;
    //public Transform WireDistance;
    public bool trigger = true;         //巡回とターゲット切り替え
    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2
    private Vector3 WirePosition;       //ワイヤーの位置情報
    private Vector3 EnemyPosition;      //エネミーの位置情報

    private float timeleft;

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

            return m_targets[ m_targetIndex ].position;
        }
    }

    //ここまで

    //private float x1;                   //プレイヤー1のx座標
    //private float x2;                   //プレイヤー2のx座標

    //private float x3;                   //糸のx座標

    //private float z1;                   //プレイヤー1のy座標
    //private float z2;                   //プレイヤー2のy座標

    //private float z3;                   //糸のy座標

    private float range1;               //エネミーからプレイヤー1までの距離
    private float range2;               //エネミーからプレイヤー2までの距離
    private float range3;               //エネミーから糸までの距離

    public float EnemySearchArea;       //敵の索敵範囲(仮置き)
    public float EnemyAtkInterval;      //敵の攻撃間隔(仮置き)
    

    // Start is called before the first frame update
    void Start()
    {
        //PlayerObject = GameObject.FindWithTag("Player");
        //WireObject = GameObject.FindWithTag("Wire");

        //PlayerPosition1 = PlayerObject1.transform.position;
        //PlayerPosition2 = PlayerObject2.transform.position;
        //WirePosition = WireObject.transform.position;
        //EnemyPosition = transform.position;

        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.destination = CurretTargetPosition;

        

    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーと糸までの距離を出す
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        EnemyPosition = transform.position;
        WirePosition = WireObject.transform.position;

        //x1 = PlayerPosition1.x - EnemyPosition.x;
        //x2 = PlayerPosition2.x - EnemyPosition.x;
        //x3 = WirePosition.x - EnemyPosition.x;
        //z1 = PlayerPosition1.z - EnemyPosition.z;
        //z2 = PlayerPosition2.z - EnemyPosition.z;
        //z3 = WirePosition.z - EnemyPosition.z;

        //range1 = Vector3.Distance(PlayerDistance1.position,transform.position);
        //range2 = Vector3.Distance(PlayerDistance2.position, transform.position);
        //range3 = Vector3.Distance(WireDistance.position, transform.position);

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);
        range3 = Vector3.Distance(WirePosition, transform.position);

        //タイマー
        timeleft -= Time.deltaTime;

        //ここからエネミーの行動パターン

        //エネミーの巡回

        if (m_navAgent.remainingDistance <= m_destinationThreshold && trigger)
            //(range1 >= EnemySearchArea || range2 >= EnemySearchArea || range3 >= EnemySearchArea)
        {
            {
                m_targetIndex = (m_targetIndex + 1) % m_targets.Length;

                m_navAgent.destination = CurretTargetPosition;

            }
        }
        if (range1 <= EnemySearchArea || range2 <= EnemySearchArea || range3 <= EnemySearchArea)
        {
            trigger = false;
        }
        //検知範囲にプレイヤーか糸がいるなら接近する
        if (!trigger)
        {

            //体力がMaxなら糸に向かって移動
            if (Hit.EnemyHealth >= 100 && range3 >= 1)
            {
                m_navAgent.destination = WirePosition;
                //if (WirePosition.x > EnemyPosition.x)
                //{
                //    EnemyPosition.x = EnemyPosition.x + 0.01f;
                //}
                //else if (WirePosition.x < EnemyPosition.x)
                //{
                //    EnemyPosition.x = EnemyPosition.x - 0.01f;
                //}

                //if (WirePosition.z > EnemyPosition.z)
                //{
                //    EnemyPosition.z = EnemyPosition.z + 0.01f;
                //}
                //else if (WirePosition.z < EnemyPosition.z)
                //{
                //    EnemyPosition.z = EnemyPosition.z - 0.01f;
                //}

            }
            else if (Hit.EnemyHealth >= 100 && range3 <= 1)
            {
                //糸に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
                if (timeleft <= 0.0)
                {
                    timeleft = EnemyAtkInterval;
                    //攻撃
                    Debug.Log("Atk");
                }
                
                

            }

            //体力が減っているならプレイヤーに向かって移動
            if (Hit.EnemyHealth < 100)
            {
                //プレイヤー1の方が近い場合
                if (range1 <= range2 && range1 >= 1)
                {
                    m_navAgent.destination = PlayerPosition1;
                    //if (PlayerPosition1.x > EnemyPosition.x)
                    //{
                    //    EnemyPosition.x = EnemyPosition.x + 0.01f;
                    //}
                    //else if (PlayerPosition1.x < EnemyPosition.x)
                    //{
                    //    EnemyPosition.x = EnemyPosition.x - 0.01f;
                    //}

                    //if (PlayerPosition1.z > EnemyPosition.z)
                    //{
                    //    EnemyPosition.z = EnemyPosition.z + 0.01f;
                    //}
                    //else if (PlayerPosition1.z < EnemyPosition.z)
                    //{
                    //    EnemyPosition.z = EnemyPosition.z - 0.01f;
                    //}
                }
                else if (range1 <= range2 && range1 <= 1)
                {
                    //プレイヤー1に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
                    if (timeleft <= 0.0)
                    {
                        timeleft = EnemyAtkInterval;
                        //攻撃
                        Debug.Log("Atk");
                    }


                }
                else { }
                //プレイヤー2の方が近い場合
                if (range2 < range1 && range2 >= 1)
                {
                    m_navAgent.destination = PlayerPosition2;
                    //if (PlayerPosition2.x > EnemyPosition.x)
                    //{
                    //    EnemyPosition.x = EnemyPosition.x + 0.01f;
                    //}
                    //else if (PlayerPosition2.x < EnemyPosition.x)
                    //{
                    //    EnemyPosition.x = EnemyPosition.x - 0.01f;
                    //}

                    //if (PlayerPosition2.z > EnemyPosition.z)
                    //{
                    //    EnemyPosition.z = EnemyPosition.z + 0.01f;
                    //}
                    //else if (PlayerPosition2.z < EnemyPosition.z)
                    //{
                    //    EnemyPosition.z = EnemyPosition.z - 0.01f;
                    //}
                }
                else if (range2 < range1 && range2 <= 1)
                {
                    //プレイヤー2に攻撃した後EnemyAtkInterval分の間隔を置いて再度攻撃を繰り返す
                    if (timeleft <= 0.0)
                    {
                        timeleft = EnemyAtkInterval;
                        Debug.Log("Atk");
                    }


                }
                else { }


            }

            //transform.position = EnemyPosition;

        }

    }
}