using System.Collections.Generic;
using UnityEngine;

public class HitkNockBack : MonoBehaviour
{
    // 与える力
    [SerializeField]
    private float power = 100;
    // 柱時に与える力
    [SerializeField]
    private float triggerPower = 10;
    // 力を与える物体までの半径
    [SerializeField]
    private float radius = 4;
    // 力の与え方
    [SerializeField]
    private ForceMode forceMode = ForceMode.Force;
    // 柱時の力の与え方
    [SerializeField]
    private ForceMode triggerForce = ForceMode.Force;
    // オブジェクトのコライダー
    [SerializeField]
    private GameObject istrigger;
    [SerializeField]
    private GameObject Player1;
    [SerializeField]
    private GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        // 敵とアイテム時の効果にしたいためタグでオブジェクトを取得しそれ以外を処理しない
        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "item" && other.gameObject.tag != "Piller")
        {
            return;
        }
        // 糸が衝突した位置を中心に半径radius以内にあるコライダを取得
        var colliders = Physics.OverlapSphere(transform.position, radius);

        // 拡張for文でコライダを順に取り出す
        foreach(var collider in colliders)
        {
            // タグがBlockだったらそのゲームオブジェクトのRigidbodyを取得しAddExplosionForceで力を加える
            if (collider.gameObject.tag == "item" || collider.gameObject.tag == "Enemy")
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, 0f, forceMode);
                // タグがEnemyなら力を加える
                //} else if(collider.gameObject.tag == "Enemy")
                //{

                //}
                // タグがPillerならisTriggerをいったん解除し,ある程度距離を置いたらプレイヤーが飛ばされる処理(予定)
            }else if(collider.gameObject.tag == "Piller")
            {
                var trigger = istrigger.GetComponent<SphereCollider>();
                trigger.isTrigger = false;
                Player1.GetComponent<Rigidbody>().AddForce(Player1.transform.position * triggerPower, triggerForce);
                Player2.GetComponent<Rigidbody>().AddForce(Player2.transform.position * triggerPower, triggerForce);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "item" && other.gameObject.tag != "Piller")
        {
            return;
        }

        var colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            if(collider.gameObject.tag == "Piller")
            {
                var falseTriger = istrigger.GetComponent<SphereCollider>();
                falseTriger.isTrigger = true;
            }
        }
    }
}
