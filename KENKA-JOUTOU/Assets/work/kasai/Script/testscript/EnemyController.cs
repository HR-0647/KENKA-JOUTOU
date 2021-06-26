using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト
    public GameObject PlayerObject2;    //プレイヤーオブジェクト
    private GameObject WireObject;      //ワイヤーオブジェクト
    private Vector3 PlayerPosition1;     //プレイヤーの位置情報
    private Vector3 PlayerPosition2;     //プレイヤーの位置情報
    private Vector3 EnemyPosition;      //エネミーの位置情報
    private Vector3 WirePosition;       //ワイヤーの位置情報

    [SerializeField]
    private NavMeshAgent m_navAgent = null;
    

    private void Start()
    {
        WireObject = GameObject.FindWithTag("Wire");

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        WirePosition = WireObject.transform.position;
        EnemyPosition = transform.position;

        m_navAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        if (true)
        {
            m_navAgent.destination = WireObject.transform.position;
        }
    }

} 