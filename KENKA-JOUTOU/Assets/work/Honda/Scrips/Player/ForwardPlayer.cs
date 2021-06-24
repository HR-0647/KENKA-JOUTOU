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
        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        offset = transform.position - player.transform.position - player2.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //�V�����g�����X�t�H�[���̒l��������
        transform.position = player.transform.position + offset;
    }
}
