using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TimeCounter : MonoBehaviour
{
    [SerializeField] float gameLimitTime;
    [SerializeField] Text finalCountDownText;
    const int FinalCountDownTime = 3;
    Text text;
    int timelimit;
    int TimeLimit
    {
        get => timelimit;
        set
        {
            if (value == FinalCountDownTime && value == timelimit)
            {
                CountDownAnimStart();
            }
            timelimit = value;
        }
    }
    float timeCount;
    void Start()
    {
        text = GetComponent<Text>();
        timelimit = (int)gameLimitTime;
        //CountDownAnimStart();
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        TimeLimit = (int)(gameLimitTime - timeCount);
        text.text = TimeLimit.ToString();
        //if (TimeLimit <= FinalCountDownTime)
        //{
        //    CountDownAnimStart();
        //}
    }
    void CountDownAnimStart()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.OnStart(() => UpdateText("3"))
            .Append(FadeOutText())
            .AppendCallback(() => UpdateText("2"))
            .Append(FadeOutText())
            .AppendCallback(() => UpdateText("1"))
            .Append(FadeOutText())
            .AppendCallback(() => UpdateText("GAME SET!"))
            //.Append(FadeOutText())
            .OnComplete(()=>finalCountDownText.color=new Color(finalCountDownText.color.r, finalCountDownText.color.g, finalCountDownText.color.b, 0.0f));
    }
    //テキストの更新
    private void UpdateText(string _text)
    {
        InitializeAlpha();

        finalCountDownText.text = _text;
    }

    //フェードアウトさせる
    private Tween FadeOutText()
    {
        return finalCountDownText.DOFade(0, 0.8f);
    }

    //アルファ値の初期化
    private void InitializeAlpha()
    {
        finalCountDownText.color = new Color(finalCountDownText.color.r, finalCountDownText.color.g, finalCountDownText.color.b, 1.0f);
    }
}
