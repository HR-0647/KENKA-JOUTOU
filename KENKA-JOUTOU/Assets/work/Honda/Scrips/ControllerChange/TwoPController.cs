using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPController : MonoBehaviour
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

    private float CoolTime;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CoolTime -= Time.deltaTime;
        velosity = Vector3.zero;
        input = new Vector3(Input.GetAxis("Horizontal2"), 0f, Input.GetAxis("Vertical2"));

        if (input.magnitude > 1f)
        {
            transform.LookAt(transform.position + input);

            velosity += transform.forward * WalkSpeed;
        }

        Dashvelosity = Vector3.zero;
        DashInput = new Vector3(Input.GetAxis("Horizontal2"), 0f, Input.GetAxis("Vertical2"));

        if (DashInput.magnitude > 1f)
        {
            transform.LookAt(transform.position + DashInput);

            Dashvelosity += transform.forward * DashSpeed;
        }
    }

    void FixedUpdate()
    {
        // �L�����̈ړ�����
        rig.AddForce(input * WalkSpeed);

        rig.velocity = Vector3.ClampMagnitude(velosity, WalkSpeed);

        if (Input.GetAxis("CrossButton2") > 0 && CoolTime < 0)
        {
            rig.MovePosition(transform.position + Dashvelosity * Time.deltaTime);
            Dash.Play();
            CoolTime = DashTime;
        }
    }
}
