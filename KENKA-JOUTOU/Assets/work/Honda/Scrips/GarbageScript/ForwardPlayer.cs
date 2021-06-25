using UnityEngine;

public class ForwardPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;   //�v���C���[���i�[�p
    [SerializeField]
    private GameObject player2;
    private Vector3 offset;      //���΋����擾�p

    // Use this for initialization
    void Start()
    {
        Vector3 center = Vector3.Lerp(player.transform.position, player2.transform.position, 0.5f);
        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        offset = transform.position + center;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = Vector3.Lerp(player.transform.position, player2.transform.position, 0.5f);
        //�V�����g�����X�t�H�[���̒l��������
        transform.position = center + offset;
    }
}
