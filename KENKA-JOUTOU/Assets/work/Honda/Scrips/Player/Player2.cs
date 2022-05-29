using UnityEngine;

public class Player2 : HP
{
    private Vector3 velosity; // �ړ��n
    private Vector3 input; // ���͒l

    public float WalkSpeed = 1.5f;

    private Vector3 Dashvelosity; // �_�b�V���ړ��n
    private Vector3 Dashinput; // �_�b�V�����͒l

    public GameObject Potion2;

    [SerializeField]
    private float DashSpeed = 5;

    private Rigidbody rig;
    [SerializeField]
    private ParticleSystem Dash;
    [SerializeField]
    private float DashTime = 0.5f;
    [SerializeField]
    private float stackTime = 1.5f;

    [SerializeField]
    private float DamageTime = 1.3f;

    private float invisibleTime;
    private bool DamageTrigger = false;
    private bool stack = false;

    private float CoolTime;
    private float stackCTime;
    private Animator anim = null;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        DamageTrigger = false;
        PlayerHP = 100;
    }

    void Update()
    {
        Debug.Log(stackCTime);
        CoolTime -= Time.deltaTime;
        if (stack == true)
        {
            stackCTime -= Time.deltaTime;
        }
        if (stackCTime <= 0)
        {
            stack = false;
            Debug.Log("AAA");
            WalkSpeed = 2.5f;
            DashSpeed = 15f;
        }
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
        Dashinput = new Vector3(Input.GetAxis("Horizontal3"), 0f, Input.GetAxis("Vertical3"));

        if (Dashinput.magnitude > 1f)
        {
            transform.LookAt(transform.position + Dashinput);

            Dashvelosity += transform.forward * DashSpeed;
        }

        if (Potion2 != null)
        {
            // ������100�ȉ��Ńv���C���[HP�����݂���ꍇ
            if (Slider.value < 100)
            {
                if (Input.GetAxis("TriangleButton1") > 0)
                {
                    PlayerHP = 100;
                    Slider.value = Slider.maxValue;
                    Potion2.SetActive(false);
                }
            }
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

        if (Input.GetAxis("R2") > 0 && CoolTime < 0)
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

        if (collision.gameObject.tag == "summon1")
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

        if (collision.gameObject.tag == "summon2")
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

        if (collision.gameObject.tag == "Bullet1")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 15;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Bullet2")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 20;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.collider.gameObject.tag == "Tackle")
        {
            DamageTrigger = true;
            Debug.Log("A");
            if (invisibleTime < 0)
            {
                PlayerHP -= 45;
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

        if (collision.collider.gameObject.tag == "Leghold_trap")
        {
            DamageTrigger = true;
            stack = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 10;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                WalkSpeed = 0f;
                DashSpeed = 0f;
                stackCTime = stackTime;
                DamageTrigger = false;
            }
        }
        if(collision.gameObject.tag == "Potion")
        {
            Potion2.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Potion")
        {
            Potion2.SetActive(true);
        }
    }
}
