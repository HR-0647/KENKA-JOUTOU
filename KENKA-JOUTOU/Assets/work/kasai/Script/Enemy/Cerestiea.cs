using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerestiea : Enemy
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2
    public GameObject SpawnPos1;        //�z�[�~���O�e�ƎG���L�����𔭐�������ꏊ
    public GameObject SpawnPos2;        //�z�[�~���O�e�ƎG���L�����𔭐�������ꏊ
    public GameObject SpawnPos3;        //�z�[�~���O�e�ƎG���L�����𔭐�������ꏊ

    [SerializeField] GameObject Skelton;
    [SerializeField] GameObject Goblin;
    [SerializeField] GameObject Mimic;
    [SerializeField] GameObject homingbullet;//���@�e

    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2
    private float range1;               //�v���C���[1�܂ł̋���
    private float range2;               //�v���C���[2�܂ł̋���

    public bool DamageTrigger = false;  //�_���[�W�����؂�ւ�
    public bool invincible = false;     //���G����
    private bool process = false;

    private int Act = 1;

    private float KnockbackSpeed = 50.0f;//�m�b�N�o�b�N�̃X�s�[�h
    private Vector3 knockback = Vector3.zero;

    Rigidbody rb;

    //�T�E���h
    public AudioClip atksound;  //�U����
    AudioSource audioSource;

    //�A�j���[�V����
    public Animator anim;

    [SerializeField]
    private Transform[] m_telep = null;

    private int m_telepIndex = 0;

    public float EnemyAtkInterval = 3;      //�G�̍U���Ԋu

    private Vector3 CurretTargetPosition
    {
        get
        {
            if (m_telep == null || m_telep.Length <= m_telepIndex)
            {
                return Vector3.zero;
            }

            return m_telep[m_telepIndex].position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Cerestiea;
        EnemyHP = 100;//�G�l�~�[�̗�
        attack = 15;
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
        if (DamageTrigger == true)
        {
            StartCoroutine(Damaged());
        }

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        //�v���C���[�̕�������
        if(range1<=range2)
        {
            this.transform.LookAt(PlayerPosition1);
        }
        else
        {
            this.transform.LookAt(PlayerPosition2);
        }

        switch(Act)
        {
            case 1:
                //����
                StartCoroutine(Telep());
                break;
            case 2:
                //����
                StartCoroutine(Summon());
                break;
            case 3:
                //����
                StartCoroutine(Atk());
                break;
            default:
                Act = 1;
                break;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }

    public IEnumerator Damaged()//�G�l�~�[���_���[�W���󂯂����̏���
    {
        invincible = true;//���G���Ԓ��͂��̏����͍s��Ȃ�
        
        EnemyHP -= 1;
        Slider.value = (float)EnemyHP / defaultEnemyHP;//HP�o�[�ϓ�

        rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //�m�b�N�o�b�N
        //�A�j���[�V�������ڍs
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", false);


        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(2.0f);//���b�ҋ@

        Debug.Log("hit");
        
        DamageTrigger = false;
        invincible = false;

    }

    public IEnumerator Telep()
    {
        //Act��1�̎��Ăяo�����

        if (!process)
        {
            process = true;

            //�e���|�[�g
            m_telepIndex = Random.Range(0, m_telep.Length);
            transform.position = m_telep[m_telepIndex].position;

            Act = 2;
            yield return new WaitForSeconds(EnemyAtkInterval);
            process = false;

        }
    }

    public IEnumerator Summon()
    {
        if (!process)
        {
            //Act��2�̎��Ăяo�����
            process = true;

            //Instantiate(Skelton, SpawnPos1.transform);
            GameObject skelton = Instantiate(Skelton) as GameObject;
            skelton.transform.position = SpawnPos1.transform.position;
            skelton.transform.rotation = SpawnPos1.transform.rotation;
            yield return new WaitForSeconds(EnemyAtkInterval);
            GameObject goblin = Instantiate(Goblin) as GameObject;
            goblin.transform.position = SpawnPos2.transform.position;
            goblin.transform.rotation = SpawnPos2.transform.rotation;
            //Instantiate(Goblin, SpawnPos2.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            GameObject mimic = Instantiate(Mimic) as GameObject;
            mimic.transform.position = SpawnPos3.transform.position;
            mimic.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(Mimic, SpawnPos3.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            Act = 3;
            yield return new WaitForSeconds(EnemyAtkInterval);
            process = false;

        }
    }

    public IEnumerator Atk()
    {
        if (!process)
        {
            process = true;

            //Act��3�̎��Ăяo�����
            GameObject bullet = Instantiate(homingbullet) as GameObject;
            bullet.transform.position = SpawnPos1.transform.position;
            bullet.transform.rotation = SpawnPos1.transform.rotation;
            //Instantiate(Homingbullet, SpawnPos1.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            bullet = Instantiate(homingbullet) as GameObject;
            bullet.transform.position = SpawnPos2.transform.position;
            bullet.transform.rotation = SpawnPos2.transform.rotation;
            //Instantiate(homingbullet, SpawnPos2.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            bullet = Instantiate(homingbullet) as GameObject;
            bullet.transform.position = SpawnPos3.transform.position;
            bullet.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(homingbullet, SpawnPos3.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            Act = 1;
            process = false;

        }
    }
}