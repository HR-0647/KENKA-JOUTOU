using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����̓~�~�b�N�p�̃X�N���v�g�ł�
/// </summary>

public class MimicSearchArea : MonoBehaviour
{
    public int Area = 5;//�J�v�Z���R���C�_�[�̃T�C�Y��ύX����
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(Area, 1, Area);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            //trigger��������
        }
    }
}
