using UnityEngine;

public class Hit : MonoBehaviour
{
   

    //�����������ɌĂ΂��֐�
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
