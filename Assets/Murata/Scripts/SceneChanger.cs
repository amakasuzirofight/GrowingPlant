using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string nextScene = "";

    public void SetNextScene(string name)
    {
        nextScene = name;
    }

    void SceneChange()
    {
        SceneManager.LoadScene(nextScene);
    }
}
