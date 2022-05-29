using UnityEngine;

public class Hit : MonoBehaviour
{
   

    //“–‚½‚Á‚½‚ÉŒÄ‚Î‚ê‚éŠÖ”
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
