using UnityEngine;
using UnityEngine.SceneManagement;

public class AgainOrRetry : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOver;

    [SerializeField]
    private string Return;

    [SerializeField]
    private string BuckTitle;

    public Player1 Player1;
    public OnePController Player1_2;
    public Player2 Player2;
    public TwoPController Player2_2;

    public void Update()
    {
        if(Player1.Slider.value == 0 || Player1_2.Slider.value == 0 || Player2.Slider.value == 0 || Player2_2.Slider.value == 0)
        {
            GameOver.SetActive(true);
            //ReturnOrBuckTitle();

            Debug.Log(Input.GetAxis("CircleButton1"));
            if (Input.GetAxis("CircleButton1") > 0)
            {
                SceneManager.LoadScene(Return);
            }
            if (Input.GetAxis("CrossButton1") > 0)
            {
                SceneManager.LoadScene(BuckTitle);
            }
        }
    }

    private void ReturnOrBuckTitle()
    {
        if (Input.GetAxis("CircleButton1") > 0)
        {
            SceneManager.LoadScene(Return);
        }
        if (Input.GetAxis("CrossButton1") > 0)
        {
            SceneManager.LoadScene(BuckTitle);
        }
    }
}
