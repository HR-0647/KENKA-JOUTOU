using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

[RequireComponent(typeof(ObiSolver))]
public class hitcoll : MonoBehaviour
{
    ObiSolver obi;

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

    private void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;
                if (col != null && col.gameObject.CompareTag("Enemy")) 
                {
                    Debug.Log(col);
                    int particleIndex = obi.simplices[contact.bodyA];

                    int ATK = col.gameObject.GetComponent<Skeltons>().EnemyHP -= 20;
                    if (col.gameObject.GetComponent<Skeltons>().EnemyHP <= 0)
                    {
                        Destroy(col.gameObject);
                    }
                }
            }
        }
    }
}
