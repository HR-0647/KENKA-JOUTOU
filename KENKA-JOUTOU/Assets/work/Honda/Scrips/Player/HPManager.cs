using UnityEngine;

public class HPManager : HP
{
    public CapsuleCollider Player1;
    public CapsuleCollider Player2;

    [SerializeField]
    private float DamageTime = 2f;

    private float invisibleTime;
    private bool DamageTrigger = false;

    private void Start()
    {
        PlayerHP = 100;
        DamageTrigger = false;
    }

    private void Update()
    {
        if (DamageTrigger == true)
        {
            invisibleTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy1")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 3;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
                Debug.Log("atk");
            }
        }

        if (collision.gameObject.tag == "Enemy2")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 5;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }

        if (collision.gameObject.tag == "Enemy3")
        {
            DamageTrigger = true;
            if (invisibleTime < 0)
            {
                PlayerHP -= 10;
                Slider.value = PlayerHP;
                invisibleTime = DamageTime;
                DamageTrigger = false;
            }
        }
    }
}
