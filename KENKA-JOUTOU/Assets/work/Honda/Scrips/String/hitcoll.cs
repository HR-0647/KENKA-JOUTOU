using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
                if (col != null && col.gameObject.CompareTag("Enemy1"))
                {
                    col.GetComponent<Goblin>().DamageTrigger = true;
                    col.GetComponent<Goblin>().Damaged();
                    col.GetComponent<NavMeshAgent>().enabled = false;
                }
                if (col != null && col.gameObject.CompareTag("Enemy2"))
                {
                    col.GetComponent<Skeltons>().DamageTrigger = true;
                    col.GetComponent<Skeltons>().Damaged();
                }
                if (col != null && col.gameObject.CompareTag("Enemy3"))
                {
                    col.GetComponent<mimic>().DamageTrigger = true;
                    col.GetComponent<mimic>().Damaged();
                }
                if (col != null && col.gameObject.CompareTag("summon1"))
                {
                    col.GetComponent<SummonsGoblin>().DamageTrigger = true;
                    col.GetComponent<SummonsGoblin>().Damaged();
                }
                if (col != null && col.gameObject.CompareTag("summon2"))
                {
                    col.GetComponent<SummonSkelton>().DamageTrigger = true;
                    col.GetComponent<SummonSkelton>().Damaged();
                }

                // Boss時
                if(col != null && col.gameObject.CompareTag("Boss1"))
                {
                    col.GetComponent<Cerestiea>().DamageTrigger = true;
                    col.GetComponent<Cerestiea>().Damaged();
                }
                if (col != null && col.gameObject.CompareTag("Boss2"))
                {
                    col.GetComponent<Loxos>().DamageTrigger = true;
                    col.GetComponent<Loxos>().Damaged();
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
