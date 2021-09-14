using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Loxos loxos;
    public float BulletSpead=7;           //弾丸の速さ
    public GameObject PlayerObject1;
    public GameObject PlayerObject2;
    private GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        if(loxos.target)
        {
            Target = PlayerObject1;
        }
        else
        {
            Target = PlayerObject2;
        }
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Move()
    {
        this.transform.LookAt(Target.transform.position);
        transform.position += transform.forward * BulletSpead * Time.deltaTime;
        Invoke("Destroy", 10.0f);
        yield break;
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            //
            //プレイヤーの体力を減らす処理とseとエフェクトをここに置く
            //
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            //こっちにはseとエフェクトのみ
            Destroy(this.gameObject);
        }

    }
}
