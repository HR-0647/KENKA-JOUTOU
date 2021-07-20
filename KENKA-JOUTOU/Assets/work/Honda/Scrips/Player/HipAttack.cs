using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipAttack : MonoBehaviour
{
    public Transform target;
    public Transform target2;

    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;

    // �q�b�v�A�^�b�N�֌W
    [SerializeField]
    private float stampspeed = 5f;
    [SerializeField]
    private float timer = 1f;

    // �N�[���_�E���^�C��
    private float CoolTime;

    // distance�p��vector
    private float dis;
    private Vector3 A;
    private Vector3 B;

    // �q�b�v�A�^�b�N�p��vector
    Vector3 curPos;
    Vector3 curPos2;
    private bool isMove = false;

    // ����̋^��Ƃ��Ă�Vector�͍\���̂ňقȂ�l����ɂ��ė��p���邽�ߐ����X�V����K�v��������
    // �N���X�͎Q�Ƃ�����̂ōX�V�������Ȃ��Ƃ���������̂ŕK�v�Ȃ�
    void Start()
    {
        p1 = GameObject.Find("SD_unitychan_humanoid");
        p2 = GameObject.Find("SD_unitychan_humanoid (1)");
    }

    // ���i�K�ł͐ݒ肵�����̃v���C���[�Ƀq�b�v�A�^�b�N�����邱�Ƃ��ł���
    // X���W�O�n�_�ɍs�����Ɏ����ł���
    // �Ȃ�Ƃ����Đ^�񒆂ɂ�����
    void Update()
    {
        A = target.transform.position;
        B = target2.transform.position;

        dis = Vector3.Distance(A, B);

        dis = target.transform.position.x;

        curPos = p1.transform.position;
        curPos2 = p2.transform.position;

        hipAttack();
    }

    private void hipAttack()
    {
        if (Input.GetKey("joystick button 6") && Input.GetKey("joystick button 7"))
        {
            isMove = true;
        }

        if (isMove)
        {
            curPos.x = Mathf.Lerp(curPos.x, dis, Time.deltaTime * stampspeed);
            curPos2.x = Mathf.Lerp(curPos2.x, dis, Time.deltaTime * stampspeed);

            p1.transform.position = curPos;
            p2.transform.position = curPos2;

            CoolTime -= Time.deltaTime;
        }

        if (CoolTime < 0)
        {
            isMove = false;
            CoolTime = timer;
        }
    }
}
