using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MiniStarPool : MonoBehaviour
{
    [SerializeField] GameObject miniStar;
    ObjectPool<GameObject> starPool;
   
    // Start is called before the first frame update
    void Start()
    {
        starPool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject OnCreatePooledObject()
    {
        return Instantiate(miniStar);
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }
    public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        miniStar = prefab;
        GameObject obj = starPool.Get();
        Transform tf = obj.transform;
        tf.position = position;
        tf.rotation = rotation;

        return obj;
    }

    public void ReleaseGameObject(GameObject obj)
    {
        starPool.Release(obj);
    }

}
