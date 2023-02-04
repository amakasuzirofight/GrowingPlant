using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] players;
    public GameObject surface;
    // Start is called before the first frame update
    void Start()
    {
        var surfaceRadius = surface.transform.localScale.y/2f;
        //foreach(var player in players) //TODO rotation...
        GameObject a = Instantiate(players[0], new Vector3(0,surfaceRadius,1), Quaternion.identity);
        GameObject b = Instantiate(players[1], new Vector3(0,-surfaceRadius,1), Quaternion.identity);
        a.transform.parent = surface.transform;
        b.transform.parent = surface.transform;
        b.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
