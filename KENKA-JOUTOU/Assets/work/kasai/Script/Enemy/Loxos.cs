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
    private float KnockbackSpeed = 40.0f;//�m�b�N�o�b�N�̃X�s�[�h
    private float TackleSpeed = 8.0f;//�^�b�N���̃X�s�[�h
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
    [SerializeField] GameObject summonEffect;

    ////�T�E���h
    //public AudioClip atksound;  //�U����
    //AudioSource audioSource;

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
        //audioSource = GetComponent<AudioSource>();

        //�A�j���[�^�[�R���|�[�l���g����
        anim = GetComponent<Animator>();
        anim.SetBool("hit", false);
        anim.SetBool("magic", false);
        anim.SetBool("dash", false);
        anim.SetBool("death", false);
        anim.SetBool("frightend", false);


        rb = GetComponent<Rigidbody>();
        target = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂̔���
        if (EnemyHP <= 0)
        {
            anim.SetBool("death", true);
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
                this.transform.position += transform.forward * TackleSpeed * Time.deltaTime;
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
            StunTrgger = true;
        }

    }
    public IEnumerator Damaged()//�G�l�~�[���_���[�W���󂯂����̏���
    {
        if (!process)
        {
            invincible = true;//���G���Ԓ��͂��̏����͍s��Ȃ�
            process = true;

            EnemyHP -= 10;
            Slider.value = (float)EnemyHP / defaultEnemyHP;//HP�o�[�ϓ�
            anim.SetBool("hit", true);
            rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //�m�b�N�o�b�N

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(2.0f);//���b�ҋ@

            anim.SetBool("hit", false);
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
            
            //Act��1�̎��Ăяo�����
            anim.SetBool("dash", true);
            process = true;
            invincible = true;
            StunTrgger = false;
            //rb.AddForce(transform.forward * TackleSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("dash", false);
            this.transform.position += transform.forward * 0;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            if (StunTrgger)
            {
                anim.SetBool("frightend", true);
                yield return new WaitForSeconds(2.0f);
                anim.SetBool("frightend", false);
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
            //����1���
            //Instantiate(Skelton, SpawnPos1.transform);
            anim.SetBool("magic", true);
            GameObject effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos1.transform.position;
            GameObject skelton = Instantiate(enemySkelton) as GameObject;
            skelton.transform.position = SpawnPos1.transform.position;
            skelton.transform.rotation = SpawnPos1.transform.rotation;
            Skeltons.Trigger = false;
            anim.SetBool("magic", false);
            yield return new WaitForSeconds(EnemyAtkInterval);

            ////����2���
            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos2.transform.position;
            GameObject goblin = Instantiate(enemyGoblin) as GameObject;
            goblin.transform.position = SpawnPos2.transform.position;
            goblin.transform.rotation = SpawnPos2.transform.rotation;
            Goblin.Trigger = false;
            //Instantiate(Goblin, SpawnPos2.transform);
            anim.SetBool("magic", false);
            yield return new WaitForSeconds(EnemyAtkInterval);

            ////����3���
            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos3.transform.position;
            GameObject mimic = Instantiate(enemyMimic) as GameObject;
            mimic.transform.position = SpawnPos3.transform.position;
            mimic.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(Mimic, SpawnPos3.transform);
            anim.SetBool("magic", false);
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
                anim.SetBool("magic", true);
                //Instantiate(Bullet, SpawnPos1.transform);
                GameObject effect = Instantiate(summonEffect) as GameObject;
                effect.transform.position = SpawnPos1.transform.position;
                GameObject bullet = Instantiate(Bullet) as GameObject;
                bullet.transform.position = SpawnPos1.transform.position;
                bullet.transform.rotation = SpawnPos1.transform.rotation;
                target = !target; anim.SetBool("magic", false);
                anim.SetBool("magic", false);
                yield return new WaitForSeconds(EnemyAtkInterval);
            }
            Debug.Log("Bullet");
            
            Act = 1;
            //yield return new WaitForSeconds(EnemyAtkInterval);
            process = false;

        }

    }
}
