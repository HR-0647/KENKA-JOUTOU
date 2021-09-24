using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loxos : Enemy
{
    public bool DamageTrigger = false;  //�_���[�W�����؂�ւ�
    public bool invincible = false;     //���G����
    public static bool target=false;                 //Bullet�̑Ώې؂�ւ��p
    private bool StunTrgger = false;    //�ǂɏՓ˂����Ƃ��̔���
    private bool process = false;       //�������ɕʂ̏������Ă΂Ȃ��悤�ɂ���
    private float KnockbackSpeed = 50.0f;//�m�b�N�o�b�N�̃X�s�[�h
    private float TackleSpeed = 20.0f;//�^�b�N���̃X�s�[�h
    private Vector3 knockback = Vector3.zero;

    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2
    private  GameObject Target;        //�U���Ώ�
    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2
    private float range1;               //�v���C���[1�܂ł̋���
    private float range2;               //�v���C���[2�܂ł̋���

    public GameObject SpawnPos1;        //�e�ƎG���L�����𔭐�������ꏊ
    public GameObject SpawnPos2;        //�e�ƎG���L�����𔭐�������ꏊ
    public GameObject SpawnPos3;        //�e�ƎG���L�����𔭐�������ꏊ

    [SerializeField] GameObject enemySkelton;
    [SerializeField] GameObject enemyGoblin;
    [SerializeField] GameObject enemyMimic;
    [SerializeField] GameObject Bullet;

    //�T�E���h
    public AudioClip atksound;  //�U����
    AudioSource audioSource;

    //�A�j���[�V����
    public Animator anim;

    Rigidbody rb;
    //public static int bulletcount;
    public float EnemyAtkInterval = 3;      //�G�̍U���Ԋu
    private int Act = 1;                    //�G�̍s���p�^�[��
    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Loxos;
        EnemyHP = 300;//�G�l�~�[�̗�
        attack = 30;
        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();

        //�A�j���[�^�[�R���|�[�l���g����
        anim = GetComponent<Animator>();
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", true);

        rb = GetComponent<Rigidbody>();
        target = false;
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
        //�߂��ق��̃v���C���[������
        if(range1<=range2)
        {
            this.transform.LookAt(PlayerPosition1);
            Target = PlayerObject1;
        }
        else
        {
            this.transform.LookAt(PlayerPosition2);
            Target = PlayerObject2;
        }
        switch(Act)
        {
            case 1:
                StartCoroutine(Tackle());
                break;
            case 2:
                StartCoroutine(Summon());
                break;
            case 3:
                StartCoroutine(Remoteatk());
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
        if(collision.gameObject.tag== "Untagged")
        {
            StunTrgger = false;
        }

    }
    public IEnumerator Damaged()//�G�l�~�[���_���[�W���󂯂����̏���
    {
        if (!process)
        {
            invincible = true;//���G���Ԓ��͂��̏����͍s��Ȃ�
            process = true;

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
            process = false;
        }

    }

    public IEnumerator Tackle()//�^�b�N���U��
    {
        if (!process)
        {
            process = true;
            //Act��1�̎��Ăяo�����
            invincible = true;
            rb.AddForce(transform.forward * TackleSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(2.0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (StunTrgger)
            {
                yield return new WaitForSeconds(2.0f);
            }
            StunTrgger = false;
            Debug.Log("Atk");
            Act = 2;
            yield return new WaitForSeconds(EnemyAtkInterval);
            invincible = false;
            process = false;

        }
    }
    
    public IEnumerator Summon()//�G������
    {
        //Act��2�̎��Ăяo�����
        if (!process)
        {
            process = true;
            //Instantiate(Skelton, SpawnPos1.transform);
            GameObject skelton = Instantiate(enemySkelton) as GameObject;
            skelton.transform.position = SpawnPos1.transform.position;
            skelton.transform.rotation = SpawnPos1.transform.rotation;
            Skeltons.Trigger = false;
            yield return new WaitForSeconds(EnemyAtkInterval);
            GameObject goblin = Instantiate(enemyGoblin) as GameObject;
            goblin.transform.position = SpawnPos2.transform.position;
            goblin.transform.rotation = SpawnPos2.transform.rotation;
            Goblin.Trigger = false;
            //Instantiate(Goblin, SpawnPos2.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            GameObject mimic = Instantiate(enemyMimic) as GameObject;
            mimic.transform.position = SpawnPos3.transform.position;
            mimic.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(Mimic, SpawnPos3.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            Debug.Log("Summon");
            Act = 3;
            process = false;
        }
        
    }
    public IEnumerator Remoteatk()//���@�e
    {
        //Act��3�̎��Ăяo�����
        
        if (!process)
        {
            process = true;
            for (int i = 0; i < 5; i++)
            {
                //Instantiate(Bullet, SpawnPos1.transform);
                GameObject bullet = Instantiate(Bullet) as GameObject;
                bullet.transform.position = SpawnPos1.transform.position;
                bullet.transform.rotation = SpawnPos1.transform.rotation;
                target = !target;
                yield return new WaitForSeconds(EnemyAtkInterval);
            }
            Debug.Log("Bullet");
            Act = 1;
            //yield return new WaitForSeconds(EnemyAtkInterval);
            process = false;

        }

    }
}