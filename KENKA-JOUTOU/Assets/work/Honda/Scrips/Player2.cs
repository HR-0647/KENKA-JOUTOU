using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Vector3 velosity; // �ړ��n
    private Vector3 input; // ���͒l
    [SerializeField]
    private float WalkSpeed = 1.5f;

    private Vector3 Dashvelosity; // �_�b�V���ړ��n
    private Vector3 Dashinput; // �_�b�V�����͒l
    [SerializeField]
    private float DashSpeed = 5;

    private Rigidbody rig;
    [SerializeField]
    private ParticleSystem Dash;
    [SerializeField]
    private float DashTime = 0.5f;
    private float CoolTime;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CoolTime -= Time.deltaTime;
        velosity = Vector3.zero;
        input = new Vector3(Input.GetAxis("Horizontal3"), 0f, Input.GetAxis("Vertical3"));

        if (input.magnitude > 1f)
        {
            transform.LookAt(transform.position + input);

            velosity += transform.forward * WalkSpeed;
        }

        Dashvelosity = Vector3.zero;
        Dashinput = new Vector3(Input.GetAxis("Horizontal3"), 0f, Input.GetAxis("Vertical3"));

        if (Dashinput.magnitude > 1f)
        {
            transform.LookAt(transform.position + Dashinput);

            Dashvelosity += transform.forward * DashSpeed;
        }
    }

    void FixedUpdate()
    {
        // �L�����̈ړ�����
        rig.MovePosition(transform.position + velosity * Time.fixedDeltaTime);
        if (Input.GetKeyDown("joystick button 7") && CoolTime < 0)
        {
            rig.MovePosition(transform.position + Dashvelosity * Time.deltaTime);
            Dash.Play();
            CoolTime = DashTime;
        }
    }
}
