using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Vector3 velosity; // 移動地
    private Vector3 input; // 入力値
    [SerializeField]
    public float WalkSpeed = 1.5f;

    private Vector3 Dashvelosity; // ダッシュ移動地
    private Vector3 Dashinput; // ダッシュ入力値

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
        // 接着しているので移動速度を0に
        velosity = Vector3.zero;
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 方向キーが多少押されているとき
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
    }

    void FixedUpdate()
    {
        // キャラの移動処理
        rig.MovePosition(transform.position + velosity * Time.fixedDeltaTime);

        if (Input.GetKey("joystick button 6") && CoolTime < 0)
        {
            rig.MovePosition(transform.position + Dashvelosity * Time.deltaTime);
            Dash.Play();
            CoolTime = DashTime;
        }
    }
}
