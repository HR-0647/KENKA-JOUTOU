using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerestiea : MonoBehaviour
{
    public GameObject WireObject;       //���C���[�I�u�W�F�N�g
    public GameObject Bullet;           //���@�e
    public bool DamageTrigger = false;  //�_���[�W�����؂�ւ�

    private Vector3 WirePosition;       //���C���[�̈ʒu���

    private float KnockbackSpeed = 5.0f;//�m�b�N�o�b�N�̃X�s�[�h

    public int EnemyHP = 500;           //�G�l�~�[�̗̑�

    private float timeleft;             //�^�C�}�[

    private bool movetrigger = true;

    //�T�E���h
    public AudioClip atksound;  //�U����
    AudioSource audioSource;

    [SerializeField]
    private Transform[] m_telep = null;

    private int m_telepIndex = 0;

    public int AtkCount = 0;   //�G�̍U����

    public float MoveCooltime = 2.0f;         //�e���|�[�g�̃N�[���^�C��

    public float EnemyAtkInterval = 0;      //�G�̍U���Ԋu

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
        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();
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
            Damaged();
        }

        //���C���[�̂ق��ɕ����]��
        transform.LookAt(WireObject.transform);//�ړ����ȂǂɌ������ς��Ȃ��Ȃ�ꏊ�ύX����


        if (movetrigger)
        {
            Invoke("Telep", MoveCooltime);

        }
        else if (!movetrigger)
        {
            Invoke("Atk", MoveCooltime);
        }






    }

    private void OnCollisionEnter(Collision collision)
    {
        ////�v���C���[�Ə��ȊO�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        //if (collision.gameObject.tag == "Untagged")
        //{
        //    //�Փˉ��Đ�
        //    //audioSource.PlayOneShot(objectcllide);
        //    GetComponent<AudioSource>().PlayOneShot(objectcllide);
        //}
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }

    public void Damaged()//�G�l�~�[���_���[�W���󂯂����̏���
    {
        //var rigidbody = GetComponent<Rigidbody>();
        EnemyHP -= 20;
        Debug.Log("hit");
        transform.position -= transform.forward * KnockbackSpeed * Time.deltaTime;
        //rigidbody.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange);
        DamageTrigger = false;
    }

    public void Atk()//�U��
    {
        for (int i = 0; i < AtkCount; i++)
        {
            if (timeleft <= 0.0)
            {
                Instantiate(Bullet, this.transform.position + new Vector3(0.0f, 1.0f, 0.5f), Quaternion.identity);            //�e�𐶐�(�����̖ڂ̑O�ɐ�������)
                timeleft = EnemyAtkInterval;    //�U���̃C���^�[�o��
            }
        }
        movetrigger = true;

    }

    public void Telep()
    {
        //�e���|�[�g��̍��W1�`�ő�l�܂ł����[�v����

        //�e���|�[�g
        m_telepIndex = (m_telepIndex + 1) % m_telep.Length;
        transform.position = m_telep[m_telepIndex].position;

        movetrigger = false;
    }
}