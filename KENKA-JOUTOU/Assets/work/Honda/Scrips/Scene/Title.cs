using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("TestMock");
    }
}
