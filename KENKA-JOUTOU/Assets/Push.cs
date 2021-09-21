using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour
{
    public TextFade fade;
    public TextFade fade2;
    public GameObject text;
    public GameObject text2;

    private void Start()
    {
        StartCoroutine(Button());
    }
    IEnumerator Button()
    {
        yield return new WaitUntil(() => Input.GetAxis("CircleButton1") > 0 || Input.GetAxis("CircleButton") > 0);
        yield return new WaitForSeconds(1f);
        fade2.isFadeIn = true;
        yield return new WaitUntil(() => Input.GetAxis("CircleButton1") > 0 || Input.GetAxis("CircleButton") > 0);
        text.SetActive(false);
        text2.SetActive(true);
        yield return new WaitUntil(() => Input.GetAxis("CircleButton1") > 0 || Input.GetAxis("CircleButton") > 0);
        fade.enabled = true;
    }

}
