using System.Linq;
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
        float rotaionAmount= 360f/players.Count();
        for(int i = 0; i < players.Count(); ++i)
        {

            Quaternion q = Quaternion.Euler(Vector3.forward * i* rotaionAmount);
            players[i].transform.rotation = q;
            Vector3 v = players[i].transform.up * (surfaceRadius+players[i].transform.localScale.y*0.25f);
            Debug.Log(v.ToString());
            GameObject p = Instantiate(players[i], v, q);
            p.transform.parent = surface.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
