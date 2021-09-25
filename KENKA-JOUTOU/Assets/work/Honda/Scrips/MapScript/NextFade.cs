using UnityEngine;

public class NextFade : MonoBehaviour
{
    public Fade fade;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            fade.isFadeIn = false;
            fade.isFadeOut = true;
        }
        
    }
}
