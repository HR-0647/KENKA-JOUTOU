using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScene : MonoBehaviour
{
    [SerializeField]
    GameObject GoalArea;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(Player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player)
        {
            SceneManager.LoadScene("Title");
            Debug.Log("Go");
        }
    }
}
