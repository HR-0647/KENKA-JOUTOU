using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Hp;
    public Animation TitleAnim;
    public GameObject camera;
    public GameObject titlecamera;
    private float time;
    private float endtime = 0.1f;
    private bool OunButton;
    private void Update()
    {
        if(Input.GetAxis("CircleButton1") > 0 || Input.GetAxis("CircleButton2") > 0)
        {
            if (!OunButton)
            {
                OunButton = true;
                canvas.SetActive(false);
                Hp.SetActive(true);
            }

            TitleAnim.Play();
            time += Time.deltaTime;
            if (time > endtime)
            {
                endtime = 0;
                camera.SetActive(true);
                
                titlecamera.SetActive(false);
            }
        }
    }
}
