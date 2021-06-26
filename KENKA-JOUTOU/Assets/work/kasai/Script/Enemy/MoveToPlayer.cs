using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MoveToPlayer : MonoBehaviour
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2
    public GameObject WireObject;      //���C���[�I�u�W�F�N�g
    //public Transform PlayerDistance1;
    //public Transform PlayerDistance2;
    //public Transform WireDistance;
    public bool trigger = true;         //����ƃ^�[�Q�b�g�؂�ւ�
    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2
    private Vector3 WirePosition;       //���C���[�̈ʒu���
    private Vector3 EnemyPosition;      //�G�l�~�[�̈ʒu���

    private float timeleft;

    //NavMesh�֘A
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

    //�����܂�

    //private float x1;                   //�v���C���[1��x���W
    //private float x2;                   //�v���C���[2��x���W

    //private float x3;                   //����x���W

    //private float z1;                   //�v���C���[1��y���W
    //private float z2;                   //�v���C���[2��y���W

    //private float z3;                   //����y���W

    private float range1;               //�G�l�~�[����v���C���[1�܂ł̋���
    private float range2;               //�G�l�~�[����v���C���[2�܂ł̋���
    private float range3;               //�G�l�~�[���玅�܂ł̋���

    public float EnemySearchArea;       //�G�̍��G�͈�(���u��)
    public float EnemyAtkInterval;      //�G�̍U���Ԋu(���u��)
    

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
        //�v���C���[�Ǝ��܂ł̋������o��
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

        //�^�C�}�[
        timeleft -= Time.deltaTime;

        //��������G�l�~�[�̍s���p�^�[��

        //�G�l�~�[�̏���

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
        //���m�͈͂Ƀv���C���[����������Ȃ�ڋ߂���
        if (!trigger)
        {

            //�̗͂�Max�Ȃ玅�Ɍ������Ĉړ�
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
                //���ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
                if (timeleft <= 0.0)
                {
                    timeleft = EnemyAtkInterval;
                    //�U��
                    Debug.Log("Atk");
                }
                
                

            }

            //�̗͂������Ă���Ȃ�v���C���[�Ɍ������Ĉړ�
            if (Hit.EnemyHealth < 100)
            {
                //�v���C���[1�̕����߂��ꍇ
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
                    //�v���C���[1�ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
                    if (timeleft <= 0.0)
                    {
                        timeleft = EnemyAtkInterval;
                        //�U��
                        Debug.Log("Atk");
                    }


                }
                else { }
                //�v���C���[2�̕����߂��ꍇ
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
                    //�v���C���[2�ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
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