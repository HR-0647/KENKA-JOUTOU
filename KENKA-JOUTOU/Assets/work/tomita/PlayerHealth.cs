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
        playerHP = defaultPlayerHP;//�����l�ݒ�
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

            // �v���[���[��j�󂷂�̂ł͂Ȃ��A��A�N�e�B�u��Ԃɂ���i�|�C���g�j
            this.gameObject.SetActive(false);
        }
    }
}