using System.Collections.Generic;
using UnityEngine;
using Obi;

public class HitkNockBack : MonoBehaviour
{
    [SerializeField]
    float impulse = 3;

    bool isCollision = false;

    public float time = 5f;

    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;
    GameObject Obi;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //プレイヤーをタグで検索し、Rigidbodyを取得
        player = GameObject.FindGameObjectWithTag("Player");
        Obi = GameObject.FindGameObjectWithTag("Obi");
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2" || collision.gameObject.tag=="Obi" && isCollision == false)
        {
            //吹っ飛ばす
            Vector3 playerVelocity = playerRigidBody.velocity;
            rigidBody.AddForce(playerVelocity * impulse, ForceMode.Impulse);
            rigidBody.AddForce(Vector3.up * impulse, ForceMode.Impulse);

            isCollision = true;
            Destroy(this.gameObject, time);
        }
    }
}
