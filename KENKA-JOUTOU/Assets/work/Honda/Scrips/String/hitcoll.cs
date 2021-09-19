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

    // hitÇµÇΩéûÇÃèàóù
    private void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;

                Vector3 dis = (col.transform.position - transform.position).normalized;
                // Enemyéû
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
                // òSâÆéû
                if (col.gameObject.CompareTag("IronGrill"))
                {
                    col.GetComponent<Rigidbody>().isKinematic = false;
                    // êÅÇ¡îÚÇŒÇ∑
                    col.GetComponent<Rigidbody>().AddForce(Vector3.forward * impulse, ForceMode.Impulse);
                    Destroy(col.gameObject, time);
                }
            }
        }
    }
}
