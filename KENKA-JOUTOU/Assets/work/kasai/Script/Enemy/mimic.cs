using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimic : Enemy
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2
    private GameObject Target;        //�U���Ώ�
    public GameObject Item;   //�h���b�v�A�C�e��

    Rigidbody rb;

    public static bool Trigger = false;  //����ƃ^�[�Q�b�g�؂�ւ�
    public bool DamageTrigger = false;  //�_���[�W�����؂�ւ�
    public bool invincible = false;     //���G����
    private bool process = false;       //������

    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    private float KnockbackSpeed = 5.0f;//�m�b�N�o�b�N�̃X�s�[�h
    private float TackleSpeed = 5.0f;
    
    //�T�E���h
    public AudioClip atksound;  //�U����

    AudioSource audioSource;

    //�A�j���[�V����
    public Animator anim;

    private float range1;               //�G�l�~�[����v���C���[1�܂ł̋���
    private float range2;               //�G�l�~�[����v���C���[2�܂ł̋���

    public float EnemyAtkInterval = 3.0f;      //�G�̍U���Ԋu
    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Mimic;
        EnemyHP = 30;//�G�l�~�[�̗�
        attack = 20;
        
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
            Instantiate(Item,this.transform.position,this.transform.rotation);          //�A�C�e���̃h���b�v
            Destroy(this.gameObject);
        }

        //�_���[�W�����Ăяo��
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        if (Trigger)
        {
            if (range1 <= range2)
            {
                this.transform.LookAt(PlayerPosition1);
                Target = PlayerObject1;
                StartCoroutine(Atk());
            }
            else
            {
                this.transform.LookAt(PlayerPosition2);
                Target = PlayerObject2;
                StartCoroutine(Atk());
            }
        }
        else
        {
            StopCoroutine(Atk());
            //idol���
            Debug.Log(Trigger);
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
        //�A�j���[�V������idol��ԂɈڍs
        //anim.SetBool("Atk", false);
        //anim.SetBool("Walk", false);


        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(2.0f);//���b�ҋ@

        Debug.Log("hit");

        DamageTrigger = false;
        invincible = false;

    }
    public IEnumerator Atk()
    {
        if (!process)
        {
            process = true;
            invincible = true;
            this.transform.LookAt(Target.transform.position);
            //transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, TackleSpeed);//�v���C���[�Ɍ������ēːi����
            rb.AddForce(transform.forward * TackleSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.5f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            yield return new WaitForSeconds(EnemyAtkInterval);
            Debug.Log("atk");
            Trigger = false;
            invincible = false;
            process = false;
        }
    }
}
