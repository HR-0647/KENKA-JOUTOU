using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    //public GameObject PlayerObject1;  //�v���C���[�I�u�W�F�N�g1
    //public GameObject PlayerObject2;  //�v���C���[�I�u�W�F�N�g2
    public GameObject WireObject;       //���C���[�I�u�W�F�N�g
    private float Wireposx;             //���C���[��x���W 
    private float Wireposy;             //���C���[��y���W
    public float BulletSpead;           //�e�ۂ̑���
    public int TargetCount;             //�ǔ��̃��[�v��


    //private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    //private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2
    private Vector3 WirePosition;       //���C���[�̈ʒu���

    //private bool TargetTrigger = true;  //�^�[�Q�b�g����p

    private float timeleft;             //�^�C�}�[

    //private float range1;               //�e�ۂ���v���C���[1�܂ł̋���
    //private float range2;               //�e�ۂ���v���C���[2�܂ł̋���

    public float ReserchTime;            //�ǔ����čēx���C���[�̂ق�������܂ł̎���

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���C���[�ƃG�l�~�[�܂ł̋������o��
        WirePosition = WireObject.transform.position;
        //Transform bulletTransform = this.transform;

        //for(int i=0; i<TargetCount;i++)
        //{
        //Wireposx = WirePosition.x;
        //Wireposy = WirePosition.y;

        //��~(�����ŃX�s�[�h��0�ɂ���)
        //transform.position += transform.forward * 0;
        //�^�C�}�[�������ɂ����ĐÎ~���鎞�Ԃ�݂���
        //if (timeleft <= 0.0)
        //{
        //    timeleft = ReserchTime;
        //    i += 1;
        //}
        //if(TargetCount==i)
        // {
        //     Destroy(this.gameObject);
        //}
        //}
        //���C���[�̂ق��ɕ����]��
        transform.LookAt(WireObject.transform);
        //���C���[�Ɍ������Ē��i
        transform.position += transform.forward * BulletSpead * Time.deltaTime;


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Obi Solver" || collision.gameObject.name == "DammyWire")
        {
            //
            //���C���[�̗̑͂����炷������se�ƃG�t�F�N�g�������ɒu��
            //

            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            //�������ɂ�se�ƃG�t�F�N�g�̂�
            Destroy(this.gameObject);
        }

    }
}
