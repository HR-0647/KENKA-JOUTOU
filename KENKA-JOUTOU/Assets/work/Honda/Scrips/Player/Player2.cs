using UnityEngine;

public class Player2 : HP
{
    private Vector3 velosity; // �ړ��n
    private Vector3 input; // ���͒l
    [SerializeField]
    public float WalkSpeed = 1.5f;

    private Vector3 Dashvelosity; // �_�b�V���ړ��n
    private Vector3 DashInput; // �_�b�V�����͒l

    [SerializeField]
    private float DashSpeed = 5;

    private Rigidbody rig;
    [SerializeField]
    private ParticleSystem Dash;
    [SerializeField]
    private float DashTime = 0.5f;

    [SerializeField]
    private float DamageTime = 2f;

    private float invisibleTime;
    private bool DamageTrigger = false;

    private float CoolTime;
    private Animator anim = null;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PlayerHP = 100;
    }

    void Update()
    {
        CoolTime -= Time.deltaTime;
        if (DamageTrigger == true)
        {
            invisibleTime -= Time.deltaTime;
        }
        // �ڒ����Ă���̂ňړ����x��0��
        velosity = Vector3.zero;
        input = new Vector3(Input.GetAxis("Horizontal3"), 0f, Input.GetAxis("Vertical3"));

        // �����L�[������������Ă���Ƃ�
        if (input.magnitude > 1f)
        {
            transform.LookAt(transform.position + input);

            velosity += transform.forward * WalkSpeed;
        }

        Dashvelosity = Vector3.zero;
        DashInput = new Vector3(Input.GetAxis("Horizontal3"), 0f, Input.GetAxis("Vertical3"));

        if (DashInput.magnitude > 1f)
        {
            transform.LookAt(transform.position + DashInput);

            Dashvelosity += transform.forward * DashSpeed;
        }

        // �A�j���[�V����
        if (input.magnitude > 0.5f)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    void FixedUpdate()
    {
        // �L�����̈ړ�����
        rig.AddForce(input * WalkSpeed);

        rig.velocity = Vector3.ClampMagnitude(velosity, WalkSpeed);

        if (Input.GetKey("joystick button 7") && CoolTime < 0)
        {
            rig.MovePosition(transform.position + Dashvelosity * Time.deltaTime);
            Dash.Play();
            CoolTime = DashTime;
        }
    }

    // �_���[�W�̍ۖ��G���Ԃ�����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy1")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 3;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Enemy2")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 5;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Enemy3")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 10;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "FireBall")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 4;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Spike_Trap")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 10;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Rotary_blade")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 7;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }
    }
}
