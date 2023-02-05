using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TitleSceneState
{
    FadeIn,
    Title,
    PlayerNumber,
    FadeOut
};

public class TitleManager : MonoBehaviour
{
    [SerializeField] private float MaxPlayerNum = 5;

    private TitleSceneState sceneState;

    [SerializeField] private GameObject FadeOutPrefab;
    [SerializeField] private GameObject FadeInPrefab;

    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject playerNumberUI;

    [SerializeField] private List<GameObject> numbers;

    private GameObject fadeObj;

    public static int playerNum = 2;

    // Start is called before the first frame update
    void Start()
    {
        sceneState = TitleSceneState.FadeIn;

        fadeObj = Instantiate(FadeInPrefab, Vector3.zero, Quaternion.identity);

        titleUI.SetActive(true);
        playerNumberUI.SetActive(false);

        playerNum = 2;
        numbers[playerNum - 2].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (sceneState)
        {
            case TitleSceneState.FadeIn:
                //  in animation

                if(fadeObj.GetComponent<FadeIn>().GetFadeInFinish())
                {
                    sceneState = TitleSceneState.Title;

                    Destroy(fadeObj);
                }

                break;

            case TitleSceneState.Title:

                if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
                {
                    sceneState = TitleSceneState.PlayerNumber;

                    titleUI.SetActive(false);
                    playerNumberUI.SetActive(true);
                }

                break;

            case TitleSceneState.PlayerNumber:

                if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && playerNum < MaxPlayerNum)
                {
                    numbers[playerNum - 2].SetActive(false);
                    playerNum++;
                    numbers[playerNum - 2].SetActive(true);
                }
                else if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && playerNum > 2)
                {
                    numbers[playerNum - 2].SetActive(false);
                    playerNum--;
                    numbers[playerNum - 2].SetActive(true);
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    sceneState = TitleSceneState.FadeOut;

                    fadeObj = Instantiate(FadeOutPrefab, Vector3.zero, Quaternion.identity);
                    fadeObj.GetComponent<FadeOut>().SetNextScene("PlanetLevel");
                }

                break;

            case TitleSceneState.FadeOut:
                //  in animation

                break;
        }

        
    }
}
