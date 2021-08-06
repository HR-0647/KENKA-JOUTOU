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

    private void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            if (contact.distance < 0.01)
            {
                ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;
                // Enemyéû
                if (col != null && col.gameObject.CompareTag("Enemy")) 
                {
                    int particleIndex = obi.simplices[contact.bodyA];

                    // EnemyÇÃëÃóÕÇå∏ÇÁÇµÅA0Ç…Ç»Ç¡ÇΩÇÁè¡ñ≈Ç∑ÇÈ
                    int ATK = col.gameObject.GetComponent<Skeltons>().EnemyHP -= 20;
                    if (col.gameObject.GetComponent<Skeltons>().EnemyHP <= 0)
                    {
                        Destroy(col.gameObject);
                    }
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
