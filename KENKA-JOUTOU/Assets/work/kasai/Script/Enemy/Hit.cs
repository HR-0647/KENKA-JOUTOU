using UnityEngine;

public class Hit : MonoBehaviour
{
   

    //当たった時に呼ばれる関数
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MoveToPlayer.EnemyHealth -= 10;
            Debug.Log("hit");
            if (MoveToPlayer.EnemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
