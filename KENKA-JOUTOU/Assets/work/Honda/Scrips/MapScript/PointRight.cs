using UnityEngine;

public class PointRight : MonoBehaviour
{
    public GameObject right;
    public GameObject right2;
    public GameObject right3;
    public GameObject right4;
    public GameObject right5;
    public GameObject right6;
    public GameObject right7;
    public GameObject right8;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            right.SetActive(true);
            right2.SetActive(true);
            right3.SetActive(true);
            right4.SetActive(true);
            right5.SetActive(true);
            right6.SetActive(true);
            right7.SetActive(true);
            right8.SetActive(true);
        }
        else
        {
            //right.SetActive(false);
            //right2.SetActive(false);
            //right3.SetActive(false);
            //right4.SetActive(false);
            //right5.SetActive(false);
            //right6.SetActive(false);
            //right7.SetActive(false);
            //right8.SetActive(false);
        }
    }
}
