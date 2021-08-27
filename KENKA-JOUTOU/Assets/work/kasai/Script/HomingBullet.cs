using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public GameObject PlayerObject1;    //�v���C���[�I�u�W�F�N�g1
    public GameObject PlayerObject2;    //�v���C���[�I�u�W�F�N�g2
    public float BulletSpead;           //�e�ۂ̑���
    public int TargetCount;             //�ǔ��̃��[�v��


    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    private bool TargetTrigger = true;  //�^�[�Q�b�g����p

    private float timeleft;             //�^�C�}�[

    private float range1;               //�e�ۂ���v���C���[1�܂ł̋���
    private float range2;               //�e�ۂ���v���C���[2�܂ł̋���

    public float ReserchTime;            //�ǔ����čēx�v���C���[�̂ق�������܂ł̎���

    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�ƃG�l�~�[�܂ł̋������o��
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        if (range1 <= range2)
        {
            TargetTrigger = true;
        }
        else if (range1 > range2)
        {
            TargetTrigger = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�ƃG�l�~�[�܂ł̋������o��
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;

        //�����ɋ߂��v���C���[�̂ق������鏈��
        if (TargetTrigger == true)
        {
            for(int i=0; i<TargetCount;i++)
            {
                //�v���C���[1�̂ق��ɕ����]��
                transform.LookAt(PlayerObject1.transform);
                //�v���C���[1�Ɍ������Ē��i(������bullet�ɑ��x��������)
                transform.position -= transform.forward * BulletSpead;
                //��~(�����ŃX�s�[�h��0�ɂ���)
                transform.position -= transform.forward * 0;
                //�^�C�}�[�������ɂ����ĐÎ~���鎞�Ԃ�݂���
                if (timeleft <= 0.0)
                {
                    i += 1;
                }
            }
            Destroy(this.gameObject);

        }
        else
        {
            for (int i = 0; i < TargetCount; i++)
            {
                //�v���C���[1�̂ق��ɕ����]��
                transform.LookAt(PlayerObject1.transform);
                //�v���C���[1�Ɍ������Ē��i(������bullet�ɑ��x��������)
                transform.position -= transform.forward * BulletSpead;
                //��~(�����ŃX�s�[�h��0�ɂ���)
                transform.position -= transform.forward * 0;
                //�^�C�}�[�������ɂ����ĐÎ~���鎞�Ԃ�݂���
                if (timeleft <= 0.0)
                {
                    i += 1;
                }
            }
            Destroy(this.gameObject);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            //
            //�v���C���[�̗̑͂����炷������se�ƃG�t�F�N�g�������ɒu��
            //

            Destroy(this.gameObject);
        }

    }
}
