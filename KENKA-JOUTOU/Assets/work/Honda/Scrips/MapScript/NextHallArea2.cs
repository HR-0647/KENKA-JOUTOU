using UnityEngine;
using UnityEngine.SceneManagement;

public class NextHallArea2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            SceneManager.LoadScene("HallArea2");
        }
    }
}
