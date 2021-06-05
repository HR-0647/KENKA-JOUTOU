using UnityEngine;

public class Grab2 : MonoBehaviour
{
    [SerializeField]
    private Rigidbody item;
    [SerializeField]
    private GameObject Grab;
    private void Start()
    {
        item.GetComponent<Rigidbody>();
        item.GetComponent<GameObject>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("item"))
        {
            if (Input.GetKey("joystick button 1"))
            {
                item.isKinematic = true;
                item.transform.parent = Grab.transform;
            }
            else if (Input.GetKeyUp("joystick button 1"))
            {
                item.isKinematic = false;
                item.transform.parent = null;
            }
        }
    }
}
