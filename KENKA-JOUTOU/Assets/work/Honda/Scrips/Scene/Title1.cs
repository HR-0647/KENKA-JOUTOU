using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title1 : MonoBehaviour
{
    public Fade fade;

    private void Update()
    {
        if (Input.GetAxis("CircleButton") > 0)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        fade.isFadeIn = false;
        fade.isFadeOut = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Prologue");
    }
}
