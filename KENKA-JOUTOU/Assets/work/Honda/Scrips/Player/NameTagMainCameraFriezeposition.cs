using UnityEngine;
using UnityEngine.UI;

public class NameTagMainCameraFriezeposition : MonoBehaviour
{
	private GameObject namePlate;   //�@���O��\�����Ă���v���[�g
	public Text nameText;   //�@���O��\������e�L�X�g

	void Start()
	{
		namePlate = nameText.transform.parent.gameObject;
	}

	void LateUpdate()
	{
		namePlate.transform.rotation = Camera.main.transform.rotation;
	}

	void SetName(string name)
	{
		nameText.text = name;
	}
}
