using UnityEngine;
using UnityEngine.AI;
using System.Collections;



[RequireComponent(typeof(NavMeshAgent))]

public class Skeltons : Enemy
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2

    Rigidbody rb;

    public static bool Trigger = true;  //����ƃ^�[�Q�b�g�؂�ւ�
    public bool DamageTrigger = false;  //�_���[�W�����؂�ւ�
    public bool invincible = false;     //���G����

    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    private float KnockbackSpeed = 50.0f;//�m�b�N�o�b�N�̃X�s�[�h
    private Vector3 knockback = Vector3.zero;




    private float timeleft;             //�^�C�}�[

    //�T�E���h
    public AudioClip atksound;  //�U����

    AudioSource audioSource;

    //�A�j���[�V����
    public Animator anim;

    //NavMesh�֘A
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


    private float range1;               //�G�l�~�[����v���C���[1�܂ł̋���
    private float range2;               //�G�l�~�[����v���C���[2�܂ł̋���

    public float EnemyAtkInterval = 3.0f;      //�G�̍U���Ԋu

    // Start is called before the first frame update
    void Start()
    {
        type = EnemyType.Skeleton;
        EnemyHP = 20;//�G�l�~�[�̗�
        attack = 3;
        m_navAgent = GetComponent<NavMeshAgent>();
        m_navAgent.destination = CurretTargetPosition;
        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();

        //�A�j���[�^�[�R���|�[�l���g����
        anim = GetComponent<Animator>();
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", true);

        rb = GetComponent<Rigidbody>();

        

    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂̔���
        if (EnemyHP <= 0)
        {
            Destroy(this.gameObject);
        }

        //�_���[�W�����Ăяo��
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }

        //�v���C���[�Ǝ��܂ł̋������o��
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        //�^�C�}�[
        timeleft -= Time.deltaTime;


        //��������G�l�~�[�̍s���p�^�[��

        //�G�l�~�[�̏���

        if (navmashtrigger)
        {
            if (Trigger)
            {
                if (m_navAgent.remainingDistance <= m_destinationThreshold)
                {

                    {

                        m_targetIndex = (m_targetIndex + 1) % m_targets.Length;
                        //�ړ��A�j���[�V����
                        anim.SetBool("Walk", true);
                        m_navAgent.destination = CurretTargetPosition;


                    }
                }
            }

            if (!Trigger)
            {
                //�v���C���[1�̕����߂��ꍇ
                if (range1 <= range2)
                {
                    if (range1 > m_navAgent.stoppingDistance)
                    {
                        m_navAgent.destination = PlayerPosition1;
                        //�ړ��A�j���[�V����
                        anim.SetBool("Atk", false);
                        anim.SetBool("Walk", true);
                    }
                    else if (range1 < m_navAgent.stoppingDistance)
                    {

                        //�v���C���[1�ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            

                            //�U���A�j���[�V����
                            anim.SetBool("Atk", true);
                            anim.SetBool("Walk", false);

                            //�U��
                            Debug.Log("Atk");
                            //Debug.Log(timeleft);

                            //�U�����Đ�
                            audioSource.PlayOneShot(atksound);

                        }


                    }

                }


                //�v���C���[2�̕����߂��A�v���C���[2�Ɩ������Ă��Ȃ��ꍇ
                if (range2 < range1)
                {
                    if (range2 > m_navAgent.stoppingDistance)
                    {
                        m_navAgent.destination = PlayerPosition2;
                        //�ړ��A�j���[�V����
                        anim.SetBool("Atk", false);
                        anim.SetBool("Walk", true);
                    }
                    else if (range2 <= m_navAgent.stoppingDistance)
                    {

                        //�v���C���[2�ɍU��������EnemyAtkInterval���̊Ԋu��u���čēx�U�����J��Ԃ�
                        if (timeleft <= 0.0)
                        {
                            timeleft = EnemyAtkInterval;
                            

                            //�U���A�j���[�V����
                            anim.SetBool("Atk", true);
                            anim.SetBool("Walk", false);

                            //�U��
                            Debug.Log("Atk");
                            //�U�����Đ�
                            audioSource.PlayOneShot(atksound);
                            //�U���A�j���[�V�������ꎞ�I�ɒ�~������
                            anim.SetBool("Atk", false);
                        }

                    }
                }




            }

        }
    }

    public IEnumerator Damaged()//�G�l�~�[���_���[�W���󂯂����̏���
    {
        invincible = true;//���G���Ԓ��͂��̏����͍s��Ȃ�
        navmashtrigger = false;
        m_navAgent.enabled = false;
        rb.isKinematic = false;

        EnemyHP -= 10;
        Slider.value = (float)EnemyHP;//HP�o�[�ϓ�
        Debug.Log(Slider.value);

        rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //�m�b�N�o�b�N
        //�A�j���[�V������idol��ԂɈڍs
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", false);


        knockback = Vector3.zero;

        yield return new WaitForSeconds(2.0f);//���b�ҋ@

        Debug.Log("hit");

        navmashtrigger = true;
        m_navAgent.enabled = true;
        rb.isKinematic = true;

        DamageTrigger = false;
        invincible = false;

    }
    //�����������ɌĂ΂��֐�
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }
}
