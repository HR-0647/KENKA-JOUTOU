using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerestiea : MonoBehaviour
{
    public GameObject WireObject;       //ワイヤーオブジェクト
    public GameObject Bullet;           //魔法弾
    public bool DamageTrigger = false;  //ダメージ処理切り替え

    private Vector3 WirePosition;       //ワイヤーの位置情報

    private float KnockbackSpeed = 5.0f;//ノックバックのスピード

    public int EnemyHP = 500;           //エネミーの体力

    private float timeleft;             //タイマー

    private bool movetrigger = true;

    //サウンド
    public AudioClip atksound;  //攻撃音
    AudioSource audioSource;

    [SerializeField]
    private Transform[] m_telep = null;

    private int m_telepIndex = 0;

    public int AtkCount = 0;   //敵の攻撃回数

    public float MoveCooltime = 2.0f;         //テレポートのクールタイム

    public float EnemyAtkInterval = 0;      //敵の攻撃間隔

    private Vector3 CurretTargetPosition
    {
        get
        {
            if (m_telep == null || m_telep.Length <= m_telepIndex)
            {
                return Vector3.zero;
            }

            return m_telep[m_telepIndex].position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //オーディオコンポーネント取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //体力の判定
        if (EnemyHP <= 0)
        {
            Destroy(this.gameObject);
        }

        //ダメージ処理呼び出し
        if (DamageTrigger == true)
        {
            Damaged();
        }

        //ワイヤーのほうに方向転換
        transform.LookAt(WireObject.transform);//移動中などに向きが変わらないなら場所変更する


        if (movetrigger)
        {
            Invoke("Telep", MoveCooltime);

        }
        else if (!movetrigger)
        {
            Invoke("Atk", MoveCooltime);
        }






    }

    private void OnCollisionEnter(Collision collision)
    {
        ////プレイヤーと床以外のオブジェクトに衝突した場合
        //if (collision.gameObject.tag == "Untagged")
        //{
        //    //衝突音再生
        //    //audioSource.PlayOneShot(objectcllide);
        //    GetComponent<AudioSource>().PlayOneShot(objectcllide);
        //}
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }

    public void Damaged()//エネミーがダメージを受けた時の処理
    {
        //var rigidbody = GetComponent<Rigidbody>();
        EnemyHP -= 20;
        Debug.Log("hit");
        transform.position -= transform.forward * KnockbackSpeed * Time.deltaTime;
        //rigidbody.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange);
        DamageTrigger = false;
    }

    public void Atk()//攻撃
    {
        for (int i = 0; i < AtkCount; i++)
        {
            if (timeleft <= 0.0)
            {
                Instantiate(Bullet, this.transform.position + new Vector3(0.0f, 1.0f, 0.5f), Quaternion.identity);            //弾を生成(自分の目の前に生成する)
                timeleft = EnemyAtkInterval;    //攻撃のインターバル
            }
        }
        movetrigger = true;

    }

    public void Telep()
    {
        //テレポート先の座標1～最大値までをループする

        //テレポート
        m_telepIndex = (m_telepIndex + 1) % m_telep.Length;
        transform.position = m_telep[m_telepIndex].position;

        movetrigger = false;
    }
}