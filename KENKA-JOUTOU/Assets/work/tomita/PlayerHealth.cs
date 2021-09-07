using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip damageSound;
    public AudioClip destroySound;
    public int playerHP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyMissile"))
        {
            playerHP -= 1;
            AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position);
            Destroy(other.gameObject);

            if (playerHP == 0)
            {
                GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1.0f);
                AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);

                // �v���[���[��j�󂷂�̂ł͂Ȃ��A��A�N�e�B�u��Ԃɂ���i�|�C���g�j
                this.gameObject.SetActive(false);
            }
        }
    }
}