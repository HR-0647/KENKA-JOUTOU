using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text textbox;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        textbox.text = "「（どこからか、声が聞こえる）」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「アディソン」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「（この声は、ワイアットか？）」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「起きて、アディソン」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「（あー、もう少し寝たい……）」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「起きろって言ってんだろ！　この脳筋クソ野郎！」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「う、うわあああ」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「お、起きたね。おはよう」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「うん、おはよう。アディソン」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「なんかさっき、ものすごい罵声を浴びせられた気がするんだが」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「罵声？　気のせいだと思うよ☆」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「……そうか。ところでここはどこなんだ？」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「さっき私たちのことを監視していたゴブリンに聞いたんだけど、ここは魔王城の地下牢獄らしいわね」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「地下牢獄？　あー、そういや俺たち魔王にやられたんだっけ」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「そうよ、あの変人魔王……いや、魔物だから変魔魔王？」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「変魔て」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「まあいいわ。とにかく私たちは、またあの変魔魔王に合い、倒さなくちゃいけないの」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「なるほどな。なら早くここから脱出しないと」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「その前に、後ろ見てみて」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「後ろ？　いったい何が……なんだこの黄色い糸！」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「これは呪いの糸らしいわよ」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「ゴブリンが教えてくれたのか？」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「いえ、魔王からの手紙に書いてあったわ」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「手紙ぃ？」";
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        yield return null;

        textbox.text = "「これよ」";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
