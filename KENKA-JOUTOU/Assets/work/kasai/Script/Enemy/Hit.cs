using UnityEngine;

public class Hit : MonoBehaviour
{
    public static float EnemyHealth = 100;

    //�����������ɌĂ΂��֐�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyHealth -= 10;
            Debug.Log("hit");
            if (EnemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
