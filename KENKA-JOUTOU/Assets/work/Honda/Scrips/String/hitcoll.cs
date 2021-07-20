using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class hitcoll : MonoBehaviour
{
    public ObiSolver obi;
    void Start()
    {
        obi = GetComponent<Obi.ObiSolver>();
    }

    private void OnEnable()
    {
        obi.OnParticleCollision += Solver_OnCollision;
    }

    private void OnDisable()
    {
        obi.OnParticleCollision -= Solver_OnCollision;
    }

    private void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        foreach (Oni.Contact contact in e.contacts)
        {
            if (contact.distance < 0.01)
            {
                ObiSolver.ParticleInActor pa = obi.particleToActor[contact.bodyA];
                ObiSolver.ParticleInActor po = obi.particleToActor[contact.bodyB];
                if (pa.actor.gameObject != po.actor.gameObject)
                {
                    Debug.Log("rope collides:" + pa.actor.blueprint.name + " " + po.actor.blueprint.name, pa.actor.gameObject);
                }
            }
        }
    }
}
