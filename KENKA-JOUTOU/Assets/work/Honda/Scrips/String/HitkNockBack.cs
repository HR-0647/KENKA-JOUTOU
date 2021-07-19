using System.Collections.Generic;
using UnityEngine;

public class HitkNockBack : MonoBehaviour
{
    // �^�����
    [SerializeField]
    private float power = 100;
    // �����ɗ^�����
    [SerializeField]
    private float triggerPower = 10;
    // �͂�^���镨�̂܂ł̔��a
    [SerializeField]
    private float radius = 4;
    // �̗͂^����
    [SerializeField]
    private ForceMode forceMode = ForceMode.Force;
    // �����̗̗͂^����
    [SerializeField]
    private ForceMode triggerForce = ForceMode.Force;
    // �I�u�W�F�N�g�̃R���C�_�[
    [SerializeField]
    private GameObject istrigger;
    [SerializeField]
    private GameObject Player1;
    [SerializeField]
    private GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        // �G�ƃA�C�e�����̌��ʂɂ��������߃^�O�ŃI�u�W�F�N�g���擾������ȊO���������Ȃ�
        if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "item" && other.gameObject.tag != "Piller")
        {
            return;
        }
        // �����Փ˂����ʒu�𒆐S�ɔ��aradius�ȓ��ɂ���R���C�_���擾
        var colliders = Physics.OverlapSphere(transform.position, radius);

        // �g��for���ŃR���C�_�����Ɏ��o��
        foreach(var collider in colliders)
        {
            // �^�O��Block�������炻�̃Q�[���I�u�W�F�N�g��Rigidbody���擾��AddExplosionForce�ŗ͂�������
            if (collider.gameObject.tag == "item" || collider.gameObject.tag == "Enemy")
            {
                collider.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius, 0f, forceMode);
                // �^�O��Enemy�Ȃ�͂�������
                //} else if(collider.gameObject.tag == "Enemy")
                //{

                //}
                // �^�O��Piller�Ȃ�isTrigger���������������,������x������u������v���C���[����΂���鏈��(�\��)
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
