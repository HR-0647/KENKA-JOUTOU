using UnityEngine;

public class EnemySearchArea : MonoBehaviour
{
    public int Area = 5;//�J�v�Z���R���C�_�[�̃T�C�Y��ύX����
    
    void Start()
    {
        this.transform.localScale = new Vector3(Area, 1, Area);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player") || other.CompareTag("Player2"))
        //{
            if (other.gameObject.tag=="Enemy2")
            {
                Skeltons.Trigger = false;
            }
            if (other.gameObject.tag=="Enemy1")
            {
                Goblin.Trigger = false;
            }
        //}
    }
}
