using UnityEngine;
using System.Collections;
using System.Collections.Concurrent;

public class HomingBullet : MonoBehaviour
{
    public GameObject PlayerObject1;  //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;  //�v���C���[�I�u�W�F�N�g2
    private GameObject Target;        //�U���Ώ�
       
    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    private Vector3 lastVelocity;
    private Rigidbody rb;

    private float range1;               //�e�ۂ���v���C���[1�܂ł̋���
    private float range2;               //�e�ۂ���v���C���[2�܂ł̋���

    public float BulletSpead;           //�e�ۂ̑���
    
    private bool reflect = true;        //���˂̔���

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        
        //�v���C���[�܂ł̋������o��
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        //reflect=false;//�e�X�g�p
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
        else if (range1>range2)
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
        else
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

            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            if (reflect)
            {
                //���˂���������w��
                Vector3 refrectVec = Vector3.Reflect(this.lastVelocity, collision.contacts[0].normal);
                this.rb.velocity = refrectVec;
                reflect = false;
            }
            else if (!reflect)
            {
                //�������ɂ�se�ƃG�t�F�N�g�̂�
                Destroy(this.gameObject);
            }
        }

    }
}
