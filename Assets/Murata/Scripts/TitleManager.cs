using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool fadeing;

    [SerializeField] private GameObject FadePrefab;

    // Start is called before the first frame update
    void Start()
    {
        fadeing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeing) return;

        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            fadeing = true;

            GameObject fadeObj = Instantiate(FadePrefab, Vector3.zero, Quaternion.identity);
            fadeObj.GetComponent<SceneChanger>().SetNextScene("SampleScene");
        }
    }
}
