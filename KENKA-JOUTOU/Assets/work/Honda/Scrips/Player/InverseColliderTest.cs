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
    private float Bouns = 5;

    private void Start()
    {
        Transform  Pos1 = Root1.GetComponent<Transform>();
        Transform  Pos2 = Root2.GetComponent<Transform>();
    }

    private void Update()
    {
        //Vector3 v = Root2.transform.position + Root1.transform.position;
        //colliderObject.transform.position = new Vector3(v.x, 0, v.z);
        //triggerObject.transform.position = new Vector3(v.x, 0, v.z);
    }

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
        // col.convex = true;
        // col.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (colliderObject)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") 
            {
                Rigidbody rig = Root1.transform.GetComponent<Rigidbody>();
                Rigidbody rig2 = Root2.transform.GetComponent<Rigidbody>();

                rig.AddForce(-Root1.transform.position * Bouns, ForceMode.VelocityChange);
                rig2.AddForce(-Root2.transform.position * Bouns, ForceMode.VelocityChange);
            }
        }
    }
}