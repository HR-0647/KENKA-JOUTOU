using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerestiea : Enemy
{
    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    public GameObject SpawnPos1;        //ホーミング弾と雑魚キャラを発生させる場所
    public GameObject SpawnPos2;        //ホーミング弾と雑魚キャラを発生させる場所
    public GameObject SpawnPos3;        //ホーミング弾と雑魚キャラを発生させる場所

    [SerializeField] GameObject enemySkelton;
    [SerializeField] GameObject enemyGoblin;
    [SerializeField] GameObject enemyMimic;
    [SerializeField] GameObject homingbullet;//魔法弾
    [SerializeField] GameObject summonEffect;//エフェクト

    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2
    private float range1;               //プレイヤー1までの距離
    private float range2;               //プレイヤー2までの距離

    public bool DamageTrigger = false;  //ダメージ処理切り替え
    public bool invincible = false;     //無敵時間
    private bool process = false;

    private int Act = 1;

    private float KnockbackSpeed = 25.0f;//ノックバックのスピード
    private Vector3 knockback = Vector3.zero;

    Rigidbody rb;

    //サウンド
    //public AudioClip atksound;  //攻撃音
    //AudioSource audioSource;

    //アニメーション
    public Animator anim;

    [SerializeField]
    private Transform[] m_telep = null;

    private int m_telepIndex = 0;

    public float EnemyAtkInterval = 3;      //敵の攻撃間隔

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
        //type = EnemyType.Cerestiea;
        EnemyHP = 100;//エネミー体力
        attack = 15;
        //オーディオコンポーネント取得
        //audioSource = GetComponent<AudioSource>();
        
        //アニメーターコンポーネント所得
        anim = GetComponent<Animator>();
        anim.SetBool("magic", false);
        anim.SetBool("death", false);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //体力の判定
        if (EnemyHP <= 0)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("magic", false);
            anim.SetBool("death", true);
            Destroy(this.gameObject);
        }

        //ダメージ処理呼び出し
        if (DamageTrigger == true && invincible == false)
        {
            StartCoroutine(Damaged());
        }

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);

        //プレイヤーの方を見る
        if(range1<=range2)
        {
            this.transform.LookAt(PlayerPosition1);
        }
        else
        {
            this.transform.LookAt(PlayerPosition2);
        }

        switch(Act)
        {
            case 1:
                //処理
                StartCoroutine(Telep());
                break;
            case 2:
                //処理
                StartCoroutine(Summon());
                break;
            case 3:
                //処理
                StartCoroutine(Atk());
                break;
            default:
                Act = 1;
                break;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            DamageTrigger = true;
        }

    }

    public IEnumerator Damaged()//エネミーがダメージを受けた時の処理
    {
        if (!invincible)
        {
            invincible = true;//無敵時間中はこの処理は行わない

            EnemyHP -= 10;
            Slider.value = (float)EnemyHP;//HPバー変動

            rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //ノックバック
                                                                                        //アニメーションを移行
            anim.SetBool("magic", false);
            anim.SetBool("death", false);


            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(2.0f);//数秒待機

            Debug.Log("hitSERES");

            DamageTrigger = false;
            invincible = false;
        }
    }

    public IEnumerator Telep()
    {
        //Actが1の時呼び出される

        if (!process)
        {
            process = true;

            //テレポート
            anim.SetBool("magic", true);
            m_telepIndex = Random.Range(0, m_telep.Length);
            transform.position = m_telep[m_telepIndex].position;

            Act = 2;
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);
            process = false;

        }
    }

    public IEnumerator Summon()
    {
        if (!process)
        {
            //Actが2の時呼び出される
            process = true;
            //召喚1回目
            anim.SetBool("magic", true);
            //Instantiate(Skelton, SpawnPos1.transform);
            GameObject effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos1.transform.position;
            GameObject skelton = Instantiate(enemySkelton) as GameObject;
            skelton.transform.position = SpawnPos1.transform.position;
            skelton.transform.rotation = SpawnPos1.transform.rotation;
            Skeltons.Trigger = false;
            
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);

            //召喚2回目
            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos2.transform.position;
            GameObject goblin = Instantiate(enemyGoblin) as GameObject;
            goblin.transform.position = SpawnPos2.transform.position;
            goblin.transform.rotation = SpawnPos2.transform.rotation;
            Goblin.Trigger = false;
            
            //Instantiate(Goblin, SpawnPos2.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);

            //召喚3回目
            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos3.transform.position;
            GameObject mimic = Instantiate(enemyMimic) as GameObject;
            mimic.transform.position = SpawnPos3.transform.position;
            mimic.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(Mimic, SpawnPos3.transform);
            
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);
            Act = 3;
           
            process = false;

        }
    }

    public IEnumerator Atk()
    {
        if (!process)
        {
            process = true;


            //Actが3の時呼び出される
            anim.SetBool("magic", true);
            GameObject effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos1.transform.position;
            GameObject bullet = Instantiate(homingbullet) as GameObject;
            bullet.transform.position = SpawnPos1.transform.position;
            bullet.transform.rotation = SpawnPos1.transform.rotation;
            //Instantiate(Homingbullet, SpawnPos1.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);

            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos1.transform.position;
            bullet = Instantiate(homingbullet) as GameObject;
            bullet.transform.position = SpawnPos2.transform.position;
            bullet.transform.rotation = SpawnPos2.transform.rotation;
            //Instantiate(homingbullet, SpawnPos2.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);

            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos1.transform.position;
            bullet = Instantiate(homingbullet) as GameObject;
            bullet.transform.position = SpawnPos3.transform.position;
            bullet.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(homingbullet, SpawnPos3.transform);
            yield return new WaitForSeconds(EnemyAtkInterval);
            anim.SetBool("magic", false);
            Act = 1;
            process = false;

        }
    }
}