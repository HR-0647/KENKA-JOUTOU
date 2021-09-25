using UnityEngine;
using System.Collections;

public class HomingBullet : MonoBehaviour
{
    private GameObject PlayerObject1;  //�v���C���[�I�u�W�F�N�g1
    private GameObject PlayerObject2;  //�v���C���[�I�u�W�F�N�g2
    private GameObject Target;        //�U���Ώ�
       
    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    private Vector3 lastVelocity;
    private Rigidbody rb;

    private float range1;               //�e�ۂ���v���C���[1�܂ł̋���
    private float range2;               //�e�ۂ���v���C���[2�܂ł̋���

    public float BulletSpead;           //�e�ۂ̑���
    
    public bool reflect = true;        //���˂̔���

    //�T�E���h
    public AudioClip sound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();

        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();

        //�v���C���[�܂ł̋������o��
        PlayerObject1 = GameObject.FindGameObjectWithTag("Player");
        PlayerObject2 = GameObject.FindGameObjectWithTag("Player2");
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        
    }

    // Update is called once per frame
    void Update()
    {
        this.lastVelocity = this.rb.velocity;

        //�v���C���[1�̂ق����߂��ꍇ
        if (range1<=range2)
        {
            Target = PlayerObject1; //�^�[�Q�b�g����
            StartCoroutine(Homing());
        }
        //�v���C���[2�̂ق����߂��ꍇ
        else
        {
            Target = PlayerObject2;//�^�[�Q�b�g����
            StartCoroutine(Homing());
        }
        
       
    }

    public IEnumerator Homing()
    {
        if (reflect)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, BulletSpead * Time.deltaTime);
        }
        else if(!reflect)
        {
            
            transform.position += transform.forward * BulletSpead * Time.deltaTime;   
        }
        Invoke("Destroy", 20.0f);
        yield break;

    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            //
            //���C���[�̗̑͂����炷������se�ƃG�t�F�N�g�������ɒu��
            //
            audioSource.PlayOneShot(sound);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            if (reflect)
            {
                audioSource.PlayOneShot(sound);
                //���˂���������w��
                Vector3 refrectVec = Vector3.Reflect(this.lastVelocity, collision.contacts[0].normal);
                this.rb.velocity = refrectVec;
                reflect = false;
                transform.rotation = Target.transform.rotation;
            }
            else if (!reflect)
            {
                //�������ɂ�se�ƃG�t�F�N�g�̂�
                audioSource.PlayOneShot(sound);
                Destroy(this.gameObject);

            }
        }

    }
}
