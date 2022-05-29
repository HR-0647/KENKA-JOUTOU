using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public GameObject FireBall;
    public float interval;
    private float timer = 0f;

    private void Update()
    {
        if(timer > interval)
        {
            Attack();
            timer = 0.0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        GameObject g = Instantiate(FireBall,transform.position,Quaternion.identity);
        g.transform.SetParent(transform);
        g.SetActive(true);
    }
}
