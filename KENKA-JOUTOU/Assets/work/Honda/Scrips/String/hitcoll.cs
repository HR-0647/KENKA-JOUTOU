using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

[RequireComponent(typeof(ObiSolver))]
public class hitcoll : MonoBehaviour
{
    ObiSolver obi;

    [SerializeField]
    float impulse = 3;

    [SerializeField]
    float boxImpulse = 3;

    [SerializeField]
    float time = 3f;

    void Awake()
    {
        obi = GetComponent<Obi.ObiSolver>();
    }

    private void OnEnable()
    {
        obi.OnCollision += Solver_OnCollision;
    }

    private void OnDisable()
    {
        obi.OnCollision -= Solver_OnCollision;
    }

    // hitした時の処理
    private void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;

                Vector3 dis = (col.transform.position - transform.position).normalized;
                // Enemy時
                if (col != null && col.gameObject.CompareTag("Enemy"))
                {
                    col.GetComponent<Skeltons>().DamageTrigger = true;
                    Debug.Log("true");

                    col.GetComponent<Skeltons>().Damaged();
                    Debug.Log("true");
                }

                // 牢屋時
                if (col.gameObject.CompareTag("IronGrill"))
                { 
                    col.GetComponent<Rigidbody>().isKinematic = false;
                    // 吹っ飛ばす
                    col.GetComponent<Rigidbody>().AddForce(Vector3.forward * impulse, ForceMode.Impulse);
                    Destroy(col.gameObject, time);
                }
            }
        }
    }
}
