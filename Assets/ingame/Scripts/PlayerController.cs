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
    public string lavaTag = "Lava";
    public string waterTag = "Water";
    public List<GameObject> splitRoots = new List<GameObject>();
    private GameObject currentroot;
    private int growth = 0;
    public double FPSLimit = 5.0;
    private double sinceLastFrame = 0.0;
    private int rootCounter = 0;
    private int currentRootIndex = 0;
    [SerializeField] SpriteRenderer activeRootRenderer;
    [SerializeField] SpriteRenderer normalRootRenderer;
    [SerializeField] SpriteRenderer splittableRootRenderer;
    //public Color activeRootColor = Color.green;
    //public Color normalRootColor = Color.yellow;
    //public Color splittableRootColor = Color.magenta;
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
        int addCount = 0;
        float angle=0;
        if (Input.GetKeyDown(PlayerCycleForward)) {
            currentRootIndex+=1;
                Debug.Log($"Updated currentRootIndex={currentRootIndex}");
            if(currentRootIndex>=splitRoots.Count) currentRootIndex = 0;
            //currentroot.GetComponent<SpriteRenderer>().color = splittableRootColor;
            currentroot.GetComponent<SpriteRenderer>().sprite = splittableRootRenderer.sprite;
            currentroot.GetComponent<SpriteRenderer>().color = splittableRootRenderer.color;
            if (!splitRoots.Any(t => t == currentroot)) splitRoots.Add(currentroot);
                Debug.Log($"Fixed currentRootIndex={currentRootIndex}");
            currentroot = splitRoots[currentRootIndex];
            //currentroot.GetComponent<SpriteRenderer>().color = activeRootColor;
            currentroot.GetComponent<SpriteRenderer>().sprite = activeRootRenderer.sprite;
            currentroot.GetComponent<SpriteRenderer>().color = activeRootRenderer.color;
        }
        if (Input.GetKeyDown(PlayerCycleBackward)) {
            currentRootIndex-=1;
                Debug.Log($"Updated currentRootIndex={currentRootIndex}");
            if(currentRootIndex<0) currentRootIndex = splitRoots.Count-1;
            //currentroot.GetComponent<SpriteRenderer>().color = splittableRootColor;
            currentroot.GetComponent<SpriteRenderer>().sprite = splittableRootRenderer.sprite;
            currentroot.GetComponent<SpriteRenderer>().color = splittableRootRenderer.color;

            if (!splitRoots.Any(t => t == currentroot)) splitRoots.Add(currentroot);
                Debug.Log($"Fixed currentRootIndex={currentRootIndex}");
            currentroot = splitRoots[currentRootIndex];
            //currentroot.GetComponent<SpriteRenderer>().color = activeRootColor;
            currentroot.GetComponent<SpriteRenderer>().sprite = activeRootRenderer.sprite;
            currentroot.GetComponent<SpriteRenderer>().color = activeRootRenderer.color;
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
            angle = 90;
            addCount++;
        }
        if (Input.GetKey(PlayerLeft)) {
            direction.x -= rootdistance;
            angle = 180;
            addCount++;
        }
        if (Input.GetKey(PlayerDown)) {
            direction.y -= rootdistance;
            angle = 270;
            addCount++;
        }
        if (Input.GetKey(PlayerRight)) {
            direction.x += rootdistance;
            angle = 0;
            addCount++;
        }
        if (direction.x != 0 || direction.y != 0) {
            if (direction.x != 0 && direction.y != 0) {
                float bidirectionalDistance = (float)Math.Sqrt((rootdistance*rootdistance)/2);
                direction.x = System.Math.Sign(direction.x)*bidirectionalDistance;
                direction.y = System.Math.Sign(direction.y)*bidirectionalDistance;
            }
            GameObject[] roots = GameObject.FindGameObjectsWithTag(rootTags[0]).Concat(GameObject.FindGameObjectsWithTag(rootTags[1])).ToArray();
            Vector3 movingTo = currentroot.transform.position+direction;
            if (!SpaceIsFree(movingTo)) return;
            SearchAndDestroyWater(movingTo);
            if(rootCounter%5==0) { //TODO || splitRoots.Any(t => t == currentroot)
                currentroot.GetComponent<SpriteRenderer>().sprite=splittableRootRenderer.sprite;
                currentroot.GetComponent<SpriteRenderer>().color = splittableRootRenderer.color;
                splitRoots.Add(currentroot);
            } else {
                //currentroot.GetComponent<SpriteRenderer>().color = normalRootColor;
                currentroot.GetComponent<SpriteRenderer>().sprite = normalRootRenderer.sprite;
                currentroot.GetComponent<SpriteRenderer>().color = normalRootRenderer.color;
            }
            currentroot = Instantiate(currentroot, currentroot.transform.position+direction, Quaternion.AngleAxis(angle==0?angle:angle/addCount, new Vector3(0.0f, 0.0f, 1.0f)), this.transform);
            currentroot.name = "root"+rootCounter;
            //currentroot.GetComponent<SpriteRenderer>().color = activeRootColor;
            currentroot.GetComponent<SpriteRenderer>().color = activeRootRenderer.color;
            currentroot.GetComponent<SpriteRenderer>().sprite = activeRootRenderer.sprite;
            rootCounter+=1;
        }
    }
    
    bool SpaceIsFree(Vector3 movingTo) {
        GameObject[] roots = GameObject.FindGameObjectsWithTag(rootTags[0]).Concat(GameObject.FindGameObjectsWithTag(rootTags[1])).ToArray();
        foreach (var root in roots)
        {
            var distance = (movingTo-root.transform.position).magnitude;
            if(distance*1.5<rootdistance) return false;
        }
        GameObject[] lavas = GameObject.FindGameObjectsWithTag(lavaTag);
        foreach (var lava in lavas)
        {
            var distance = (movingTo-lava.transform.position).magnitude;
            if(distance<lava.transform.localScale.y*15) return false;
        }
        return true;
    }

    void SearchAndDestroyWater(Vector3 movingTo) {
        GameObject[] waters = GameObject.FindGameObjectsWithTag(waterTag);

        foreach (var water in waters)
        {
            var distance = (movingTo-water.transform.position).magnitude;
            if(distance<water.transform.localScale.y*15) {
                growth += 1;
                Destroy(water);
            }
        }
    }
}
