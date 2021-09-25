using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Loxos loxos;
    public float BulletSpead=7;           //�e�ۂ̑���
    private GameObject PlayerObject1;
    private GameObject PlayerObject2;
    private Vector3 PlayerPosition1;    //�v���C���[�̈ʒu���1
    private Vector3 PlayerPosition2;    //�v���C���[�̈ʒu���2

    //�T�E���h
    public AudioClip sound;  

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //�I�[�f�B�I�R���|�[�l���g�擾
        audioSource = GetComponent<AudioSource>();
        PlayerObject1 = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(PlayerPosition1);
        PlayerObject2 = GameObject.FindGameObjectWithTag("Player2");
        PlayerPosition1 = PlayerObject1.transform.position;
        PlayerPosition2 = PlayerObject2.transform.position;
        if (Loxos.target)
        {
            this.transform.LookAt(PlayerPosition1);
            Debug.Log(PlayerPosition1);
        }
        else if (!Loxos.target)
        {
            this.transform.LookAt(PlayerPosition2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(PlayerPosition1);
        StartCoroutine(Move());
        //Debug.Log(PlayerObject1);
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
            audioSource.PlayOneShot(sound);
            Destroy(this.gameObject);
            Debug.Log("destroy");
        }
        if (collision.gameObject.tag == "Untagged")
        {
            //�������ɂ�se�ƃG�t�F�N�g�̂�
            audioSource.PlayOneShot(sound);
            Destroy(this.gameObject);
        }

    }
}
