using System.Collections;
using System.Linq;
using UnityEngine;

public class InverseColliderTest : MonoBehaviour
{
    [SerializeField]
    private float colliderSize;
    [SerializeField]
    private GameObject Root1;
    [SerializeField]
    private GameObject Root2;
    private bool isActivated = false;
    [SerializeField]
    private GameObject colliderObject;
    [SerializeField]
    private GameObject triggerObject;
    [SerializeField]
    private float MaxStringBouns = 5;
    [SerializeField]
    private float ShrinkTime = 3;
    [SerializeField]
    private SphereCollider ShrinkObj;
    [SerializeField]
    private float EnabledTime = 1;

    private float Timer;
    private float SphereTimer;

    private void Start()
    {
        Transform  Pos1 = Root1.GetComponent<Transform>();
        Transform  Pos2 = Root2.GetComponent<Transform>();
        StartCoroutine("RadiusContlloler");
    }

    // スフィアとシリンジをプレイヤーの真ん中に置くようヴェクターで追従
    private void Update()
    {
        Vector3 v = Root2.transform.position + Root1.transform.position;

        triggerObject.transform.position = v;
    }

    // プレスを発生させるスフィアコライダーに触れたとたんに外枠のシリンジコライダーを追加する
    private void OnTriggerEnter(Collider c)
    {
        if (!isActivated && c.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            CreateInverseCollider();
        }
        
    }

    private void CreateInverseCollider()
    {
        // Cylinderの生成
        colliderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        colliderObject.transform.position = transform.position;
        colliderObject.transform.SetParent(transform);
        colliderObject.transform.localScale = new Vector3(colliderSize, 5, colliderSize);
        

        // Colliderオブジェクトの描画は不要なのでRendererを消す
        Destroy(colliderObject.GetComponent<MeshRenderer>());

        // 元々存在するColliderを削除
        Collider[] colliders = colliderObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }

        // メッシュの面を逆にしてからMeshColliderを設定
        var mesh = colliderObject.GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        var col = colliderObject.AddComponent<MeshCollider>();
        //col.convex = true;
        //col.isTrigger = true;
    }

    // 一定の距離に達したら中心に処理をする(予定)
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rig = Root1.GetComponent<Rigidbody>();
        Rigidbody rig2 = Root2.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "Player")
        {
            // 物理を無視した引き寄せ(プレス)
            rig.AddForce(-Root1.transform.position * MaxStringBouns, ForceMode.VelocityChange);
        }else if(other.gameObject.tag == "Player2")
        {
            rig2.AddForce(-Root2.transform.position * MaxStringBouns, ForceMode.VelocityChange);
        }
        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    Rigidbody rig = Root1.transform.GetComponent<Rigidbody>();
    //    Rigidbody rig2 = Root2.transform.GetComponent<Rigidbody>();
    //    Vector3 Point = Vector3.Lerp(Root1.transform.position, Root2.transform.position, 0.5f);

    //    // 物理を考慮した引き寄せ(伸びているときの移動遅延)
    //    rig.AddForce(-Point - Root1.transform.position * MaxStringBouns, ForceMode.VelocityChange);
    //    rig2.AddForce(-Point - Root2.transform.position * MaxStringBouns, ForceMode.VelocityChange);
    //}

    // コルーチンを使い一旦プレス発生と解除を行う
    private IEnumerator RadiusContlloler()
    {
        while (true)
        {
            ShrinkObj.radius = 9;
            ShrinkObj.enabled = true;
            yield return new WaitForSeconds(ShrinkTime);
            ShrinkObj.enabled = false;
            yield return new WaitForSeconds(EnabledTime);
        }
    }
}