using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Loxos loxos;
    public float BulletSpead=7;           //�e�ۂ̑���
    public GameObject PlayerObject1;
    public GameObject PlayerObject2;
    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        if (Loxos.target)
        {
            this.transform.LookAt(PlayerPosition1);
        }
        else if (!Loxos.target)
        {
            this.transform.LookAt(PlayerPosition2);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        
        transform.position += transform.forward * BulletSpead * Time.deltaTime;
        Invoke("Destroy", 20.0f);
        yield break;
    }
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            //
            //�v���C���[�̗̑͂����炷������se�ƃG�t�F�N�g�������ɒu��
            //
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Untagged")
        {
            //�������ɂ�se�ƃG�t�F�N�g�̂�
            Destroy(this.gameObject);
        }

    }
}
