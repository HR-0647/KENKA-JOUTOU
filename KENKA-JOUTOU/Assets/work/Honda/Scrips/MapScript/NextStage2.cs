using UnityEngine.SceneManagement;
using UnityEngine;

public class NextStage2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("HallTalk");
    }
}
