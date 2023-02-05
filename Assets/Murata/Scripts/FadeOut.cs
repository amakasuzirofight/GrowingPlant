using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    private string nextScene = "";

    public void SetNextScene(string name)
    {
        nextScene = name;
    }

    void FadeOutFinish()
    {
        SceneManager.LoadScene(nextScene);
    }
}
