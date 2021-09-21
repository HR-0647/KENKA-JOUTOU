using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrizonScene : MonoBehaviour
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
        SetNextLine();
        yield return null;
        currentLine = 1;
        textbox2.text = "???";
        Player1 = false;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 2;
        textbox2.text = "アディソン";
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
        textbox2.text = "アディソン";
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
        textbox2.text = "アディソン";
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
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 9;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 10;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 11;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 12;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 13;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 14;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 15;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 16;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 17;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 18;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 19;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 20;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 21;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 22;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 23;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 24;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 25;
        textbox2.text = "ワイアット";
        Player1 = false;
        Player2 = true;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 26;
        textbox2.text = "アディソン";
        Player1 = true;
        Player2 = false;
        yield return new WaitUntil(() => Input.GetButtonDown("CircleButton1"));
        yield return null;
        currentLine = 27;
        textbox2.text = "ワイアット";
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