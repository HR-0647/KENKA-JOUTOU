using UnityEngine;

public class tackleCollider : MonoBehaviour
{
    public BoxCollider tackle;
    public Loxos lx;

    private void Start()
    {
        tackle.enabled = false;
    }

    private void Update()
    {
        if (lx.tackle == true)
        {
            tackle.enabled = true;
        }
        else
        {
            tackle.enabled = false;
        }
    }
}
