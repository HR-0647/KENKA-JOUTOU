using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loxos : Enemy
{
    public bool DamageTrigger = false;  //ダメージ処理切り替え
    public bool invincible = false;     //無敵時間
    public static bool target=false;                 //Bulletの対象切り替え用
    private bool StunTrgger = false;    //壁に衝突したときの判定
    private bool process = false;       //処理中に別の処理を呼ばないようにする
    private float KnockbackSpeed = 40.0f;//ノックバックのスピード
    private float TackleSpeed = 8.0f;//タックルのスピード
    private Vector3 knockback = Vector3.zero;

    public GameObject PlayerObject1;    //プレイヤーオブジェクト1
    public GameObject PlayerObject2;    //プレイヤーオブジェクト2
    private  GameObject Target;        //攻撃対象
    private Vector3 PlayerPosition1;    //プレイヤーの位置情報1
    private Vector3 PlayerPosition2;    //プレイヤーの位置情報2
    private float range1;               //プレイヤー1までの距離
    private float range2;               //プレイヤー2までの距離

    public GameObject SpawnPos1;        //弾と雑魚キャラを発生させる場所
    public GameObject SpawnPos2;        //弾と雑魚キャラを発生させる場所
    public GameObject SpawnPos3;        //弾と雑魚キャラを発生させる場所

    [SerializeField] GameObject enemySkelton;
    [SerializeField] GameObject enemyGoblin;
    [SerializeField] GameObject enemyMimic;
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject summonEffect;

    ////サウンド
    //public AudioClip atksound;  //攻撃音
    //AudioSource audioSource;

    //アニメーション
    public Animator anim;

    Rigidbody rb;
    //public static int bulletcount;
    public float EnemyAtkInterval = 3;      //敵の攻撃間隔
    private int Act = 1;                    //敵の行動パターン
    // Start is called before the first frame update
    void Start()
    {
        //type = EnemyType.Loxos;
        EnemyHP = 300;//エネミー体力
        attack = 30;
        //オーディオコンポーネント取得
        //audioSource = GetComponent<AudioSource>();

        //アニメーターコンポーネント所得
        anim = GetComponent<Animator>();
        anim.SetBool("hit", false);
        anim.SetBool("magic", false);
        anim.SetBool("dash", false);
        anim.SetBool("death", false);
        anim.SetBool("frightend", false);


        rb = GetComponent<Rigidbody>();
        target = false;
    }

    // Update is called once per frame
    void Update()
    {
        //体力の判定
        if (EnemyHP <= 0)
        {
            anim.SetBool("death", true);
            Destroy(this.gameObject);
        }

        //ダメージ処理呼び出し
        if (DamageTrigger == true)
        {
            StartCoroutine(Damaged());
        }

        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;


        range1 = Vector3.Distance(PlayerPosition1, transform.position);
        range2 = Vector3.Distance(PlayerPosition2, transform.position);
        //近いほうのプレイヤーを見る
        if(range1<=range2)
        {
            this.transform.LookAt(PlayerPosition1);
            Target = PlayerObject1;
        }
        else
        {
            this.transform.LookAt(PlayerPosition2);
            Target = PlayerObject2;
        }
        switch(Act)
        {
            case 1:
                this.transform.position += transform.forward * TackleSpeed * Time.deltaTime;
                StartCoroutine(Tackle());
                break;
            case 2:
                StartCoroutine(Summon());
                break;
            case 3:
                StartCoroutine(Remoteatk());
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
        if(collision.gameObject.tag== "Untagged")
        {
            StunTrgger = true;
        }

    }
    public IEnumerator Damaged()//エネミーがダメージを受けた時の処理
    {
        if (!process)
        {
            invincible = true;//無敵時間中はこの処理は行わない
            process = true;

            EnemyHP -= 10;
            Slider.value = (float)EnemyHP / defaultEnemyHP;//HPバー変動
            anim.SetBool("hit", true);
            rb.AddForce(-transform.forward * KnockbackSpeed, ForceMode.VelocityChange); //ノックバック

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            yield return new WaitForSeconds(2.0f);//数秒待機

            anim.SetBool("hit", false);
            Debug.Log("hit");

            DamageTrigger = false;
            invincible = false;
            process = false;
        }

    }

    public IEnumerator Tackle()//タックル攻撃
    {
        if (!process)
        {
            
            //Actが1の時呼び出される
            anim.SetBool("dash", true);
            process = true;
            invincible = true;
            StunTrgger = false;
            //rb.AddForce(transform.forward * TackleSpeed, ForceMode.VelocityChange);
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("dash", false);
            this.transform.position += transform.forward * 0;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            if (StunTrgger)
            {
                anim.SetBool("frightend", true);
                yield return new WaitForSeconds(2.0f);
                anim.SetBool("frightend", false);
            }
            StunTrgger = false;
            Debug.Log("Atk");
            Act = 2;
            yield return new WaitForSeconds(EnemyAtkInterval);
            
            invincible = false;
            process = false;

        }
    }
    
    public IEnumerator Summon()//雑魚召喚
    {
        //Actが2の時呼び出される
        if (!process)
        {
            process = true;
            //召喚1回目
            //Instantiate(Skelton, SpawnPos1.transform);
            anim.SetBool("magic", true);
            GameObject effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos1.transform.position;
            GameObject skelton = Instantiate(enemySkelton) as GameObject;
            skelton.transform.position = SpawnPos1.transform.position;
            skelton.transform.rotation = SpawnPos1.transform.rotation;
            Skeltons.Trigger = false;
            anim.SetBool("magic", false);
            yield return new WaitForSeconds(EnemyAtkInterval);

            ////召喚2回目
            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos2.transform.position;
            GameObject goblin = Instantiate(enemyGoblin) as GameObject;
            goblin.transform.position = SpawnPos2.transform.position;
            goblin.transform.rotation = SpawnPos2.transform.rotation;
            Goblin.Trigger = false;
            //Instantiate(Goblin, SpawnPos2.transform);
            anim.SetBool("magic", false);
            yield return new WaitForSeconds(EnemyAtkInterval);

            ////召喚3回目
            anim.SetBool("magic", true);
            effect = Instantiate(summonEffect) as GameObject;
            effect.transform.position = SpawnPos3.transform.position;
            GameObject mimic = Instantiate(enemyMimic) as GameObject;
            mimic.transform.position = SpawnPos3.transform.position;
            mimic.transform.rotation = SpawnPos3.transform.rotation;
            //Instantiate(Mimic, SpawnPos3.transform);
            anim.SetBool("magic", false);
            yield return new WaitForSeconds(EnemyAtkInterval);
            Debug.Log("Summon");
            Act = 3;
            process = false;
        }
        
    }
    public IEnumerator Remoteatk()//魔法弾
    {
        //Actが3の時呼び出される
        
        if (!process)
        {
            process = true;
            for (int i = 0; i < 5; i++)
            {
                anim.SetBool("magic", true);
                //Instantiate(Bullet, SpawnPos1.transform);
                GameObject effect = Instantiate(summonEffect) as GameObject;
                effect.transform.position = SpawnPos1.transform.position;
                GameObject bullet = Instantiate(Bullet) as GameObject;
                bullet.transform.position = SpawnPos1.transform.position;
                bullet.transform.rotation = SpawnPos1.transform.rotation;
                target = !target; anim.SetBool("magic", false);
                anim.SetBool("magic", false);
                yield return new WaitForSeconds(EnemyAtkInterval);
            }
            Debug.Log("Bullet");
            
            Act = 1;
            //yield return new WaitForSeconds(EnemyAtkInterval);
            process = false;

        }

    }
}
