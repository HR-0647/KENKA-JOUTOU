using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Hp;
    public Animation TitleAnim;
    public GameObject camera;
    public GameObject titlecamera;
    public GameObject Player1;
    public GameObject Player2;
    private float time;
    private float endtime = 0.1f;
    private bool OunButton;

    private void Start()
    {
        Player1.GetComponent<Player1>().enabled = false;
        Player2.GetComponent<Player2>().enabled = false;
    }
    private void Update()
    {
        if(Input.GetAxis("CircleButton1") > 0 || Input.GetAxis("CircleButton2") > 0)
        {
            Player1.GetComponent<Player1>().enabled = true;
            Player2.GetComponent<Player2>().enabled = true;
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
