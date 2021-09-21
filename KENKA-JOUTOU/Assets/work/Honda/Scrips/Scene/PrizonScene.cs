using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrizonScene : MonoBehaviour
{
    //�C���X�y�N�^�[�Ŏ擾
    [SerializeField, Header("- �V�[���J�ڐ於")]
    private string sceneLoadName;
    [SerializeField] private UnityEngine.UI.Text textbox; //����
    [SerializeField] private UnityEngine.UI.Text textbox2; //���O
    
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f; //1�����ɂ����鎞��

    // �C���[�W�𔒍��ɐݒ�
    [SerializeField] Color btnColor1 = Color.white; //�J���[�I���i���Ə����Ă���܂����C���X�y�N�^�[�ŕύX�\�j
    [SerializeField] Color btnColor2 = Color.black;
    // �����G
    [SerializeField] Image image;
    [SerializeField] Image image2;

    [Header("�t�F�[�h")] public Fade fade;

    private bool firstPush = false;
    private bool goNextScene = false;
    private int currentLine = 0; //�s�ԍ�
    private string cureentText = string.Empty; //������
    private float timeUntilDisplay = 0; //�\���ɂ����鎞��
    private float timeElapsed = 1; //������̕\�����J�n��������
    private int lastUpdateCharecter = -1; //�\�����̕�����
    public string[] scenarios; //�V�i���I�i�[
    private bool Player2 = false;
    private bool Player1 = false;
   
    private Animator Flashimage;
    private bool isFlash;
    private bool CorutinSelect1 = false;

    private void AnimatorSet()
    {
        Flashimage.SetBool("FlashImage", isFlash);
    }

    private void Start()
    {
        StartCoroutine(Sinario());
        StartCoroutine(Button());
    }

    private void Update()
    {
        if (currentLine < scenarios.Length && Input.GetButtonDown("CircleButton1"))
        {
            SetNextLine();
        }

        //�N���b�N����o�߂������Ԃ��z��\�����Ԃ̂Ȃ�%���m�F���A�\�����������o��
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * cureentText.Length);

        //�\�����������O��̕\���������ƈقȂ�e�L�X�g���X�V����
        if (displayCharacterCount != lastUpdateCharecter)
        {
            textbox.text = cureentText.Substring(0, displayCharacterCount);
            lastUpdateCharecter = displayCharacterCount;
        }

        if (CorutinSelect1 == true)
        {
            StopCoroutine(Sinario());
        }
    }

    //�R���[�`������e�L�X�g�A�b���Ă���L�����𔒍�������A�\���
    private IEnumerator Sinario()
    {
        SetNextLine();
        yield return null;
        currentLine = 1;
        textbox2.text = "???";
        Player1 = false;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 2;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 3;
        textbox2.text = "???";
        Player1 = false;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 4;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 5;
        textbox2.text = "???";
        Player1 = false;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 6;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 7;
        textbox2.text = "???";
        Player1 = false;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 8;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 9;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 10;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 11;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 12;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 13;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 14;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 15;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 16;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 17;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 18;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 19;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 20;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 21;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 22;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 23;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 24;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 25;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 26;
        textbox2.text = "�A�f�B�\��";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 27;
        textbox2.text = "���C�A�b�g";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;

    }

    private IEnumerator Button()
    {
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        OnClick1();
        OnClick2();

        if (!firstPush)
        {
            fade.isFadeOut=true;
            firstPush = true;
        }
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        if (!goNextScene /*&& fade.IsFadeOutComplete()*/)
        {
            SceneManager.LoadScene(sceneLoadName);
            goNextScene = true;
        }
    }

    //�摜�𔒍��ς���
    private void OnClick1()
    {
        if (Player1 == true)
        {
            image.color = btnColor1;
        }
        else if (Player1 == false)
        {
            image.color = btnColor2;
        }
    }

    private void OnClick2()
    {
        if (Player2 == true)
        {
           
            image2.color = btnColor1;
        }
        else if (Player2 == false)
        {
           
            image2.color = btnColor2;
        }
    }

    void SetNextLine()
    {
        cureentText = scenarios[currentLine];
        currentLine++;

        //�z��\�����Ԃƌ��݂̎������L���b�V��
        timeUntilDisplay = cureentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;

        //�����J�E���g��������
        lastUpdateCharecter = -1;
    }
}