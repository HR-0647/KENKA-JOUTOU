using UnityEngine;
using UnityEngine.AI;
using Obi;

[RequireComponent(typeof(NavMeshAgent))]

public class Skeltons : MonoBehaviour
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2
    public GameObject WireObject;      //���C���[�I�u�W�F�N�g

    public bool trigger = true;         //����ƃ^�[�Q�b�g�؂�ւ�

    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2
    private Vector3 WirePosition;       //���C���[�̈ʒu���

    public int EnemyHP = 100;      //�G�l�~�[�̗̑�

    private float timeleft;

    //�T�E���h
    public AudioClip atksound;  //�U����
    //public AudioClip objectcllide;//�Փˉ�
    AudioSource audioSource;

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

            return m_targets[m_targetIndex].position;
        }
    }


    private float range1;               //�G�l�~�[����v���C���[1�܂ł̋���
    private float range2;               //�G�l�~�[����v���C���[2�܂ł̋���
    private float range3;               //�G�l�~�[���玅�܂ł̋���

    public float EnemySearchArea;       //�G�̍��G�͈�
    public float EnemyAtkInterval;      //�G�̍U���Ԋu

    // Start is called before the first frame update
    void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.destination = CurretTargetPosition;

        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�Ǝ��܂ł̋������o��
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        WirePosition = WireObject.transform.position;
                
        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);
        range3 = Vector3.Distance(WirePosition, transform.position);

        //�^�C�}�[
        timeleft -= Time.deltaTime;


        //��������G�l�~�[�̍s���p�^�[��
   
        //�G�l�~�[�̏���

        if (m_navAgent.remainingDistance <= m_destinationThreshold && trigger)
        {
            {
                m_targetIndex = (m_targetIndex + 1) % m_targets.Length;

                m_navAgent.destination = CurretTargetPosition;
                

            }
        }

        //���m�͈͂Ƀv���C���[����������Ȃ�ڋ߂���
        if (range1 <= EnemySearchArea || range2 <= EnemySearchArea || range3 <= EnemySearchArea)
        {
            trigger = false;
        }

                
        if (!trigger)
        {
                //�v���C���[1�̕����߂��ꍇ
                if (range1 <= range2)
                {                    
                    m_navAgent.destination = PlayerPosition1;

                    if (range1 < m_navAgent.stoppingDistance)
                    {
                        
                        //�v���C���[1�ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            //�U��
                            Debug.Log("Atk");

                            //�U�����Đ�
                            audioSource.PlayOneShot(atksound);
                        }

                }

            }
                
           
                //�v���C���[2�̕����߂��A�v���C���[2�Ɩ������Ă��Ȃ��ꍇ
                if (range2 < range1)
                {                    
                    m_navAgent.destination = PlayerPosition2;

                    if (range2 <= m_navAgent.stoppingDistance)
                    {                        
                        //�v���C���[2�ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            //�U��
                            Debug.Log("Atk");
                            //�U�����Đ�
                            audioSource.PlayOneShot(atksound);
                        }

                }
            }
                
                


        }

    }
    //�����������ɌĂ΂��֐�
    private void OnCollisionEnter(Collision collision)
    {
        ////�v���C���[�Ə��ȊO�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        //if (collision.gameObject.tag == "Untagged")
        //{
        //    //�Փˉ��Đ�
        //    //audioSource.PlayOneShot(objectcllide);
        //    GetComponent<AudioSource>().PlayOneShot(objectcllide);
        //}
        if (collision.gameObject.tag == "Player"|| collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Obi")
        {
            EnemyHP -= 20;
            Debug.Log("hit");
            
        }
        //�̗͂̔���
        if (EnemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
