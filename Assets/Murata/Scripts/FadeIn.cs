using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private bool finish = false;
    void FadeInFinish()
    {
        finish = true;
    }

    public bool GetFadeInFinish()
    {
        return finish;
    }
}
