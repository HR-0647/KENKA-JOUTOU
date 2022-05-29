using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextend : MonoBehaviour
{
    public GameObject Lox;
    public Fade fade;
    public float time = 2f;

    public void Update()
    {
        if (!Lox)
        {
            fade.isFadeIn = false;
            fade.isFadeOut = true;
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene("END");
            }
        }
    }
}
