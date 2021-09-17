using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    /// <summary>
    /// テスト用スクリプト
    /// </summary>
    //bool a=false;
    public GameObject Target;
    private bool StunTrgger;
    public float EnemyAtkInterval;
    public float TackleSpeed=20;
    public GameObject SpawnPos1;        //弾と雑魚キャラを発生させる場所
    public GameObject SpawnPos2;        //弾と雑魚キャラを発生させる場所
    public GameObject SpawnPos3;        //弾と雑魚キャラを発生させる場所

    [SerializeField] GameObject Skelton;
    [SerializeField] GameObject Goblin;
    [SerializeField] GameObject Mimic;
    [SerializeField]
    private Transform[] m_telep = null;

    private int m_telepIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(a);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Tackle());
            //Debug.Log(a);
            //a = !a;
            //StartCoroutine(Summon());
            StartCoroutine(Telep());
        }
    }
    public IEnumerator Tackle()//タックル攻撃//loxos
    {
        //Actが1の時呼び出される
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, TackleSpeed);
        if (StunTrgger)
        {
            yield return new WaitForSeconds(2.0f);
        }
        StunTrgger = false;
        Debug.Log("Atk");
        //Act = 2;
        yield return new WaitForSeconds(EnemyAtkInterval);
    }
    public IEnumerator Summon()//雑魚召喚
    {
        //Actが2の時呼び出される

        Instantiate(Skelton, SpawnPos1.transform);
        yield return new WaitForSeconds(EnemyAtkInterval);
        Instantiate(Goblin, SpawnPos2.transform);
        yield return new WaitForSeconds(EnemyAtkInterval);
        Instantiate(Mimic, SpawnPos3.transform);
        yield return new WaitForSeconds(EnemyAtkInterval);
        Debug.Log("Summon");
       

    }
    //public IEnumerator Remoteatk()//魔法弾
    //{
    //    //Actが3の時呼び出される
    //    //1秒毎に5回弾が発射される
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Instantiate(Bullet, SpawnPos1.transform);
    //        target = !target;
    //        yield return new WaitForSeconds(1.0f);
    //    }
    //    Debug.Log("Bullet");
    //    //Act = 1;
    //    yield return new WaitForSeconds(EnemyAtkInterval);


    //}
    public IEnumerator Telep()
    {
        //Actが1の時呼び出される

        //テレポート
        m_telepIndex = Random.Range(0, m_telep.Length);
        transform.position = m_telep[m_telepIndex].position;

        //Act = 2;
        yield return new WaitForSeconds(EnemyAtkInterval);
    }
}
