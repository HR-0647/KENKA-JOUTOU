using UnityEngine;

public class Grab1 : MonoBehaviour
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
            if (Input.GetKey("joystick button 2"))
            {
                item.isKinematic = true;
                item.transform.parent = Grab.transform;
            }
            else if (Input.GetKeyUp("joystick button 2"))
            {
                item.isKinematic = false;
                item.transform.parent = null;
            }
        }
    }
}
