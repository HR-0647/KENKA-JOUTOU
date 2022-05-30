using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Fade fade;

    private void Update()
    {
        if(Input.GetAxis("CircleButton1") > 0|| Input.GetAxis("CircleButton2") > 0)
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
