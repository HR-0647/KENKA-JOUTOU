using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HallTalk2 : MonoBehaviour
{
    //インスペクターで取得
    [SerializeField, Header("- シーン遷移先名")]
    private string sceneLoadName;
    [SerializeField] private UnityEngine.UI.Text textbox; //文章
    [SerializeField] private UnityEngine.UI.Text textbox2; //名前

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f; //1文字にかかる時間

    // イメージを白黒に設定
    [SerializeField] Color btnColor1 = Color.white; //カラー選択（白と書いてありますがインスペクターで変更可能）
    [SerializeField] Color btnColor2 = Color.black;
    // 立ち絵
    [SerializeField] Image image;
    [SerializeField] Image image2;
    [SerializeField] Image image3;
    [SerializeField] Image image4;

    [Header("フェード")] public Fade fade;

    private bool firstPush = false;
    private bool goNextScene = false;
    private int currentLine = 0; //行番号
    private string cureentText = string.Empty; //文字列
    private float timeUntilDisplay = 0; //表示にかかる時間
    private float timeElapsed = 1; //文字列の表示を開始した時間
    private int lastUpdateCharecter = -1; //表示中の文字数
    public string[] scenarios; //シナリオ格納
    private bool Player2 = false;
    private bool Player1 = false;
    private bool Boss1 = false;
    private bool Boss2 = false;
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

        //クリックから経過した時間が想定表示時間のなん%を確認し、表示文字数を出す
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * cureentText.Length);

        //表示文字数が前回の表示文字数と異なるテキストを更新する
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

    //コルーチンからテキスト、話しているキャラを白黒させる、表情差分
    private IEnumerator Sinario()
    {
        Boss1 = true;
        SetNextLine();
        yield return null;
        currentLine = 1;
        textbox2.text = "セレス";
        Player1 = false;
        Player2 = false;
        Boss1 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 2;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 3;
        textbox2.text = "セレス";
        Player1 = false;
        Player2 = false;
        Boss1 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 4;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 5;
        textbox2.text = "セレス";
        Player1 = false;
        Player2 = false;
        Boss1 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 6;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 7;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 8;
        textbox2.text = "セレス";
        Player1 = false;
        Player2 = false;
        Boss1 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 9;
        textbox2.text = "アディソン&ワイアット";
        Player1 = true;
        Player2 = true;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 10;
        textbox2.text = "セレス";
        Player1 = false;
        Player2 = false;
        Boss1 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 11;
        textbox2.text = "???";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 12;
        textbox2.text = "セレス";
        Player1 = false;
        Player2 = false;
        Boss1 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 13;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        Boss1 = false;
        Boss2 = false;
        image4.enabled = true;
        image3.enabled = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 14;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 15;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 16;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 17;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 18;
        textbox2.text = "アディソン&ワイアット";
        Player1 = true;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 19;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 20;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 21;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 22;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 23;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 24;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 25;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 26;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 27;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 28;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 29;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 30;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 31;
        textbox2.text = "アディソン&ワイアット";
        Player1 = true;
        Player2 = true;
        Boss1 = false;
        Boss2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 32;
        textbox2.text = "ロキソス";
        Player1 = false;
        Player2 = false;
        Boss1 = false;
        Boss2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
    }

    private IEnumerator Button()
    {
        //1
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //2
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //3
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //4
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //5
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //6
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //7
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //8
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //9
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //10
        OnClick1();
        OnClick2();
        OnClick3();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //11
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //12
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //13
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //14
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //15
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //16
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //17
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //18
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //19
        OnClick1();
        OnClick2();
        OnClick3(); 
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //20
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //21
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //22
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //23
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //24
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //25
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4(); yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //26
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4(); yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //27
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //28
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //29
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //30
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //31
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //32
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        //33
        OnClick1();
        OnClick2();
        OnClick3();
        OnClick4();

        if (!firstPush)
        {
            fade.isFadeOut = true;
            firstPush = true;
        }
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        if (!goNextScene || fade.alfa <= 0)
        {
            SceneManager.LoadScene(sceneLoadName);
            goNextScene = true;
        }
    }

    //画像を白黒変える
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

    private void OnClick3()
    {
        if (Boss1 == true)
        {
            image3.color = btnColor1;
        }
        else if (Boss1 == false)
        {
            image3.color = btnColor2;
        }
    }

    private void OnClick4()
    {
        if (Boss2 == true)
        {
            image4.color = btnColor1;
        }
        else if (Boss2 == false)
        {
            image4.color = btnColor2;
        }
    }

    void SetNextLine()
    {
        cureentText = scenarios[currentLine];
        currentLine++;

        //想定表示時間と現在の時刻をキャッシュ
        timeUntilDisplay = cureentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;

        //文字カウントを初期化
        lastUpdateCharecter = -1;
    }
}
