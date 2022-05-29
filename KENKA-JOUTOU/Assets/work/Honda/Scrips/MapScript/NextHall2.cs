using UnityEngine;
using UnityEngine.SceneManagement;

public class NextHall2 : MonoBehaviour
{
    public GameObject Celes;
    public Fade fade;
    public float time = 2f;

    public void Update()
    {
        if (!Celes)
        {
            fade.isFadeIn = false;
            fade.isFadeOut = true;
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene("HallTalk2");
            }
        }
    }
}
