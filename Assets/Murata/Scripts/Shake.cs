using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //  Width of swing
    [SerializeField] private float swingWidth = 10.0f;
    //  Speed
    [SerializeField] private float swingSpeed = 0.1f;

    private float centerPosition;
    private float radian = 0.0f;

    private void Start()
    {
        centerPosition = gameObject.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
            centerPosition + Mathf.Sin(radian) * swingWidth,
            gameObject.transform.localPosition.z);

        radian += swingSpeed * Time.deltaTime;
    }
}
