using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlaySceneState
{
    FadeIn,
    CountDown,
    Playing,
    Result,
    FadeOut,
};

public class PlayManager : MonoBehaviour
{
    private PlaySceneState playSceneState;

    [SerializeField] private GameObject FadeOutPrefab;
    [SerializeField] private GameObject FadeInPrefab;

    private GameObject fadeObj;

    //  parentObject
    [SerializeField] private GameObject CountUI;
    [SerializeField] private GameObject resultUI;

    [SerializeField] private float MAX_WIDTH = 600.0f;
    [SerializeField] private float MAX_HEIGHT = 600.0f;
 
    [SerializeField] private List<GameObject> framePoints;
    [SerializeField] private GameObject frame;

    [SerializeField] private float moveTime = 1.0f;
    private float timer;

    private bool horizontalMove;

    [SerializeField] private List<GameObject> CountDownUI;


    [SerializeField] private float PlayTime = 60.0f;
    [SerializeField] private Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;

        horizontalMove = true;

        fadeObj = Instantiate(FadeInPrefab, Vector3.zero, Quaternion.identity);

        CountUI.SetActive(false);
        resultUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(playSceneState)
        {
            case PlaySceneState.FadeIn:

                if (fadeObj.GetComponent<FadeIn>().GetFadeInFinish())
                {
                    timeText.gameObject.SetActive(true);

                    timeText.text = ((int)PlayTime).ToString("000");

                    CountUI.SetActive(true);

                    StartCoroutine("CountStart");
                }

                break;
            case PlaySceneState.CountDown:

                CountDown();

                if (timer >= 4.0f)
                {
                    playSceneState = PlaySceneState.Playing;
                    CountUI.SetActive(false);
                    timer = 0.0f;
                }
                    break;
            case PlaySceneState.Playing:
                PlayTime -= Time.deltaTime;

                timeText.text = ((int)PlayTime).ToString("000");

                if(PlayTime < 1.0f)
                {
                    playSceneState = PlaySceneState.Result;
                    resultUI.SetActive(true);
                    timer = 0.0f;
                }
                break;
            case PlaySceneState.Result:
                FrameMove();

                if(!horizontalMove && timer > moveTime && Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
                {
                    timer = 0.0f;

                    playSceneState = PlaySceneState.FadeOut;

                    resultUI.SetActive(false);
                    timeText.gameObject.SetActive(false);

                    fadeObj = Instantiate(FadeOutPrefab, Vector3.zero, Quaternion.identity);
                    fadeObj.GetComponent<FadeOut>().SetNextScene("TitleScene");
                }

                break;
            case PlaySceneState.FadeOut:


                break;
        }
    }

    public void CountDown()
    {
        timer += Time.deltaTime;

        if (timer >= 4.0f) return;

        foreach (var countUI in CountDownUI)
        {
            countUI.SetActive(false);
        }

        CountDownUI[(int)timer].SetActive(true);

    }


    public void FrameMove()
    {
        if (!horizontalMove && timer > moveTime) return;

        timer += Time.deltaTime;
        if (horizontalMove)
        {

            Vector3 pos = Vector3.Lerp(Vector3.zero, new Vector3(MAX_WIDTH / 2.0f, 0.0f, 0.0f), timer / moveTime);
            framePoints[0].transform.localPosition = framePoints[1].transform.localPosition = pos;
            pos = Vector3.Lerp(Vector3.zero, new Vector3(-MAX_WIDTH/ 2.0f, 0.0f, 0.0f), timer / moveTime);
            framePoints[2].transform.localPosition = framePoints[3].transform.localPosition = pos;

            frame.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(new Vector2(10.0f,10.0f),new Vector2(MAX_WIDTH,10.0f), timer / moveTime);

            if (timer < moveTime) return;

            timer = 0;
            horizontalMove = false;
        }
        else
        {
            Vector3 pos = Vector3.Lerp(new Vector3(MAX_WIDTH / 2.0f, 0.0f, 0.0f), new Vector3(MAX_WIDTH / 2.0f, MAX_HEIGHT / 2.0f, 0.0f), timer / moveTime);
            framePoints[0].transform.localPosition = pos;
            pos = Vector3.Lerp(new Vector3(MAX_WIDTH / 2.0f, 0.0f, 0.0f), new Vector3(MAX_WIDTH / 2.0f, -MAX_HEIGHT / 2.0f, 0.0f), timer / moveTime);
            framePoints[1].transform.localPosition = pos;
            pos = Vector3.Lerp(new Vector3(-MAX_WIDTH / 2.0f, 0.0f, 0.0f), new Vector3(-MAX_WIDTH / 2.0f, MAX_HEIGHT / 2.0f, 0.0f), timer / moveTime);
            framePoints[2].transform.localPosition = pos;
            pos = Vector3.Lerp(new Vector3(-MAX_WIDTH / 2.0f, 0.0f, 0.0f), new Vector3(-MAX_WIDTH / 2.0f, -MAX_HEIGHT / 2.0f, 0.0f), timer / moveTime);

            frame.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(new Vector2(MAX_WIDTH, 10.0f), new Vector2(MAX_WIDTH, MAX_HEIGHT), timer / moveTime);

            framePoints[3].transform.localPosition = pos;
        }
    }

    IEnumerator CountStart()
    {
        yield return new WaitForSeconds(0.5f);
        playSceneState = PlaySceneState.CountDown;
        Destroy(fadeObj);
    }
}
