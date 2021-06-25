using UnityEngine;

public class Player2 : MonoBehaviour
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

        

        // Lerpを使い距離が離れると遅くなる予定だった
        //Vector3 middle = Vector3.Lerp(player1.transform.localPosition, player2.transform.localPosition, 0.5f);
        //Vector3 distance = Vector3.Lerp(-player1.transform.localPosition, -player2.transform.localPosition, 0.5f);

        //Debug.Log()

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
        // キャラの移動処理
        rig.MovePosition(transform.position + velosity * Time.fixedDeltaTime);
        if (Input.GetKeyDown("joystick button 7") && CoolTime < 0)
        {
            rig.MovePosition(transform.position + Dashvelosity * Time.deltaTime);
            Dash.Play();
            CoolTime = DashTime;
        }
    }
}
