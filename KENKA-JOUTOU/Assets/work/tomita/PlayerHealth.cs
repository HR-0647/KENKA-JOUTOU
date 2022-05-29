using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip damageSound;
    public AudioClip destroySound;
    public static int playerHP;
    public Slider Slider;
    public int defaultPlayerHP;

    private void Start()
    {
        playerHP = defaultPlayerHP;//初期値設定
        Slider.value = 1;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            var enemy = collision.collider.GetComponent<Enemy>();
            DecreasePlayerHP(enemy.attack);
        }
    }
   

    private void DecreasePlayerHP(int amount)
    {
        //AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position);
        //Destroy(other.gameObject);
        playerHP -= amount;
        Slider.value = (float)playerHP / defaultPlayerHP;
        if (playerHP == 0)
        {
            // GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            //  Destroy(effect, 1.0f);
            //  AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);

            // プレーヤーを破壊するのではなく、非アクティブ状態にする（ポイント）
            this.gameObject.SetActive(false);
        }
    }
}