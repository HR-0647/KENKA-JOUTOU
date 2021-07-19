using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g
    private GameObject WireObject;      //���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition1;     //�v���C���[�̈ʒu���
    private Vector3 PlayerPosition2;     //�v���C���[�̈ʒu���
    private Vector3 EnemyPosition;      //�G�l�~�[�̈ʒu���
    private Vector3 WirePosition;       //���C���[�̈ʒu���

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