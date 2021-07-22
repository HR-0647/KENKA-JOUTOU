using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Map");
    }

    private void Update()
    {
        if(Input.GetAxis("CircleButton1") > 0 || Input.GetAxis("CircleButton2") > 0)
        {
            SceneManager.LoadScene("Map");
        }
    }
}
