using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGround : MonoBehaviour
{
    //  ‰ñ“]‘¬“x
    [SerializeField] private float rotSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundRotate();
    }

    //  
    public void GroundRotate()
    {
        //  ‰ñ“]‚·‚é
        gameObject.transform.Rotate(new Vector3(0.0f,0.0f,rotSpeed * Time.deltaTime));
    }
}
