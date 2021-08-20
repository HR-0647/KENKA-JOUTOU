using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxDistance = 100.0f;
    public Vector3 movepos;
    private Rigidbody rb;
    private Vector3 defaultPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Destroy(this.gameObject);
        }
        defaultPos = transform.position;
    }

    private void FixedUpdate()
    {
        {
            float d = Vector3.Distance(transform.position, defaultPos);

            if (d > maxDistance)
            {
                Destroy(this.gameObject);
            }
            else
            {
                rb.MovePosition(transform.position += movepos * Time.deltaTime * speed);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
