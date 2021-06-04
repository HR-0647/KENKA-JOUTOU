using System.Linq;
using UnityEngine;

public class InverseColliderTest : MonoBehaviour
{
    [SerializeField]
    private float colliderSize;
    [SerializeField]
    private PhysicMaterial stringMax;

    private bool isActivated = false;
    private GameObject colliderObject;

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
        // Cylinder�̐���
        colliderObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        colliderObject.transform.position = transform.position;
        colliderObject.transform.SetParent(transform);
        colliderObject.transform.localScale = new Vector3(colliderSize, 5, colliderSize);

        // Collider�I�u�W�F�N�g�̕`��͕s�v�Ȃ̂�Renderer������
        Destroy(colliderObject.GetComponent<MeshRenderer>());

        // ���X���݂���Collider���폜
        Collider[] colliders = colliderObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }

        // ���b�V���̖ʂ��t�ɂ��Ă���MeshCollider��ݒ�
        var mesh = colliderObject.GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        colliderObject.AddComponent<MeshCollider>();
        colliderObject.GetComponent<MeshCollider>().material = stringMax;
    }
}