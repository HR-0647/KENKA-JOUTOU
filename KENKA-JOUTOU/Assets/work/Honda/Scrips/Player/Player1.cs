using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Vector3 velosity; // �ړ��n
    private Vector3 input; // ���͒l
    [SerializeField]
    public float WalkSpeed = 1.5f;

    private Vector3 Dashvelosity; // �_�b�V���ړ��n
    private Vector3 Dashinput; // �_�b�V�����͒l
    [SerializeField]
    private float DashSpeed = 5;

    private Rigidbody rig;
    [SerializeField]
    private ParticleSystem Dash;
    [SerializeField]
    private float DashTime = 0.5f;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    private float CoolTime;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CoolTime -= Time.deltaTime;
        // �ڒ����Ă���̂ňړ����x��0��
        velosity = Vector3.zero;
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // �����L�[������������Ă���Ƃ�
        if (input.magnitude > 0f)
        {
            transform.LookAt(transform.position + input);

            velosity += transform.forward * WalkSpeed;
        }

        Dashvelosity = Vector3.zero;
        Dashinput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if(Dashinput.magnitude > 0f)
        {
            transform.LookAt(transform.position + Dashinput);

            Dashvelosity += transform.forward * DashSpeed;
        }

        

        // Lerp���g�������������ƒx���Ȃ�\�肾����
        //Vector3 middle = Vector3.Lerp(player1.transform.localPosition, player2.transform.localPosition, 0.5f);
        //Vector3 distance = Vector3.Lerp(-player1.transform.localPosition, -player2.transform.localPosition, 0.5f);

        //if (middle.x > distance.x)
        //{
        //    WalkSpeed = 1f;
        //}
        //else if (middle.x < distance.x)
        //{
        //    WalkSpeed = 5f;
        //}
        //if (middle.z > distance.z)
        //{
        //    WalkSpeed = 1f;
        //}
        //else if (middle.z < distance.z)
        //{
        //    WalkSpeed = 5f;
        //}

        //if (middle.x < distance.x)
        //{
        //    WalkSpeed = 1f;
        //}
        //else if (middle.x > distance.x)
        //{
        //    WalkSpeed = 5f;
        //}
        //if (middle.z < distance.z)
        //{
        //    WalkSpeed = 1f;
        //}
        //else if (middle.z > distance.z)
        //{
        //    WalkSpeed = 5f;
        //}
    }

    void FixedUpdate()
    {
        // �L�����̈ړ�����
        rig.MovePosition(transform.position + velosity * Time.fixedDeltaTime);
        if (Input.GetKeyDown("joystick button 6") && CoolTime < 0)
        {
            rig.MovePosition(transform.position + Dashvelosity * Time.deltaTime);
            Dash.Play();
            CoolTime = DashTime;
        }
    }
}
