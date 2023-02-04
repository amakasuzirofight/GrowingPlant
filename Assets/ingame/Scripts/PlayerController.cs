using System.Linq;
using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float rootdistance = 0.2f;
    public GameObject rootroot;
    public string[] rootTags = {"Player1Root","Player2Root"};
    public List<GameObject> splitRoots = new List<GameObject>();
    GameObject currentroot;
    public double FPSLimit = 5.0;
    private double sinceLastFrame = 0.0;
    private int rootCounter = 0;
    private int currentRootIndex = 0;
    public Color activeRootColor = Color.green;
    public Color normalRootColor = Color.yellow;
    public Color splittableRootColor = Color.magenta;
    public KeyCode PlayerUp = KeyCode.W;
    public KeyCode PlayerLeft = KeyCode.A;
    public KeyCode PlayerDown = KeyCode.S;
    public KeyCode PlayerRight = KeyCode.D;
    public KeyCode PlayerCycleForward = KeyCode.Q;
    public KeyCode PlayerCycleBackward = KeyCode.E;
    // Start is called before the first frame update
    void Start()
    {
        currentroot = rootroot;
        splitRoots.Add(rootroot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PlayerCycleForward)) {
            currentRootIndex+=1;
            if(splitRoots.Count==currentRootIndex) currentRootIndex = 0;
            currentroot.GetComponent<SpriteRenderer>().color = splittableRootColor;
            if(!splitRoots.Any(t => t == currentroot)) splitRoots.Add(currentroot);
            currentroot = splitRoots[currentRootIndex];
            currentroot.GetComponent<SpriteRenderer>().color = activeRootColor;
        }
        if (Input.GetKeyDown(PlayerCycleBackward)) {
            currentRootIndex-=1;
            if(splitRoots.Count==-1) currentRootIndex = 0;
            currentroot.GetComponent<SpriteRenderer>().color = splittableRootColor;
            if(!splitRoots.Any(t => t == currentroot)) splitRoots.Add(currentroot);
            currentroot = splitRoots[currentRootIndex];
            currentroot.GetComponent<SpriteRenderer>().color = activeRootColor;
        }
        // If returning to node, process swap and return TODO
        if(sinceLastFrame<(1.0/FPSLimit)) {
            sinceLastFrame += Time.deltaTime;
            return;   
        }
        sinceLastFrame = 0;
        Vector3 direction = new Vector3(0,0,0);
        if (Input.GetKey(PlayerUp)) {
            direction.y += rootdistance;
        }
        if (Input.GetKey(PlayerLeft)) {
            direction.x -= rootdistance;
        }
        if (Input.GetKey(PlayerDown)) {
            direction.y -= rootdistance;
        }
        if (Input.GetKey(PlayerRight)) {
            direction.x += rootdistance;
        }
        if (direction.x != 0 || direction.y != 0) {
            if (direction.x != 0 && direction.y != 0) {
                float bidirectionalDistance = (float)Math.Sqrt((rootdistance*rootdistance)/2);
                direction.x = System.Math.Sign(direction.x)*bidirectionalDistance;
                direction.y = System.Math.Sign(direction.y)*bidirectionalDistance;
            }
            GameObject[] roots = GameObject.FindGameObjectsWithTag(rootTags[0]).Concat(GameObject.FindGameObjectsWithTag(rootTags[1])).ToArray();
            Vector3 movingTo = currentroot.transform.position+direction;
            foreach (var root in roots)
            {
                var distance = (movingTo-root.transform.position).magnitude;
                if(distance*1.5<rootdistance) return;
            }
            if(rootCounter%5==0) { //TODO || splitRoots.Any(t => t == currentroot)
                currentroot.GetComponent<SpriteRenderer>().color = splittableRootColor;
                splitRoots.Add(currentroot);
                Debug.Log("Count:"+splitRoots.Count);
                Console.Write("Count:"+splitRoots.Count);
            } else {
                currentroot.GetComponent<SpriteRenderer>().color = normalRootColor;
            }
            currentroot = Instantiate(currentroot, currentroot.transform.position+direction, Quaternion.identity, this.transform);
            currentroot.name = "root"+rootCounter;
            currentroot.GetComponent<SpriteRenderer>().color = activeRootColor;
            rootCounter+=1;
        }
    }
}
