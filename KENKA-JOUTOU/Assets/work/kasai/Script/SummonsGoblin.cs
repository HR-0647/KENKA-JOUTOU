using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonsGoblin : Enemy
{

    Rigidbody rb;

    private GameObject player;
   

    public bool DamageTrigger = false;  //�_���[�W�����؂�ւ�
    public bool invincible = false;

    public float Spead = 5.0f;

    private float KnockbackSpeed = 25.0f;//�m�b�N�o�b�N�̃X�s�[�h

    private float distance=0;

    public float EnemyAtkInterval = 3.0f;      //�G�̍U���Ԋu
    private float timeleft;             //�^�C�}�[
    //�T�E���h
    public AudioClip atksound;  //�U����

    AudioSource audioSource;

    //�A�j���[�V����
    public Animator anim;

    [SerializeField] GameObject Effect;//�G�t�F�N�g
    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Goblin;
        EnemyHP = 40;//�G�l�~�[�̗�
        attack = 5;
        
        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();

        //�A�j���[�^�[�R���|�[�l���g����
        anim = GetComponent<Animator>();
        anim.SetBool("Atk", false);
        anim.SetBool("Walk", true);

        rb = GetComponent<Rigidbody>();


        player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //�^�C�}�[
        timeleft -= Time.deltaTime;

        //�̗͂̔���
        if (EnemyHP <= 0)
        {
            GameObject effect = Instantiate(Effect) as GameObject;
            effect.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }

        //�_���[�W�����Ăяo��
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }


        distance = Vector3.Distance(player.transform.position,transform.position);

        if (distance > 1.0)
        {
            //�ړ��A�j���[�V����
            anim.SetBool("Walk", true);
            this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Spead * Time.deltaTime);
        }
        else if(distance<1.0)
        {
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

    public IEnumerator Damaged()//�G�l�~�[���_���[�W���󂯂����̏���
    {
        if (!invincible)
        {
            invincible = true;//���G���Ԓ��͂��̏����͍s��Ȃ�
            

            EnemyHP -= 10;
            Slider.value = (float)EnemyHP;//HP�o�[�ϓ�
            anim.SetBool("hit", true);
            rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //�m�b�N�o�b�N

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(2.0f);//���b�ҋ@

            anim.SetBool("hit", false);
            Debug.Log("hit");

            DamageTrigger = false;
            invincible = false;
            
        }

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
