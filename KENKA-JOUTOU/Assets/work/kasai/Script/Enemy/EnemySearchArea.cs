using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchArea : MonoBehaviour
{
    public int Area = 5;//カプセルコライダーのサイズを変更する
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(Area, 1, Area);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            if (other.gameObject.GetComponent<Skeltons>())
            {
                Skeltons.Trigger = false;
            }
            else if (other.gameObject.GetComponent<Goblin>())
            {
                Goblin.Trigger = false;
            }
        }
    }
}
