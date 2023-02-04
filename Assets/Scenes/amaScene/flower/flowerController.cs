using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerController : MonoBehaviour
{
    [SerializeField] GameObject leafObj;
    [SerializeField] List<GameObject> leafs = new List<GameObject>();
    [SerializeField] GameObject petal;
    [SerializeField] GameObject flowerBase;

    [SerializeField] Sprite[] leafAnims;
    [SerializeField] Sprite[] petalAnims;
    [SerializeField] Sprite[] flowerBaseAnims;
    [SerializeField] float nextAnimTime;

    [SerializeField, Header("芽の高さ")] float leafHigh;
    int flowerAnimCount;
    int FlowerAnimCount
    {
        get => flowerAnimCount;
        set
        {
            if (value > leafAnims.Length - 1)
            {
                flowerAnimCount = 0;
            }
            else
            {
                flowerAnimCount = value;
            }
        }
    }
    float animtimeCount;
    void Start()
    {

    }

    void Update()
    {
        animtimeCount += Time.deltaTime;
        if (animtimeCount >= nextAnimTime)
        {
            animtimeCount = 0;
            FlowerAnimCount++;
        }
        Debug.Log(FlowerAnimCount);
        foreach (var item in leafs)
        {
            item.GetComponent<SpriteRenderer>().sprite = leafAnims[FlowerAnimCount];
        }
        petal.GetComponent<SpriteRenderer>().sprite = petalAnims[flowerAnimCount];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Growing();
        }
    }
    /// <summary>
    /// これを呼ぶと植物が成長する
    /// </summary>
    public void Growing()
    {
        //葉っぱ生成
        var obj = Instantiate(leafObj);
        obj.transform.parent = this.gameObject.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        //それ以外のオブジェクトを上にあげる
        foreach (var item in leafs)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y + leafHigh, item.transform.localPosition.z);
        }
        Debug.Log($"petal{petal.transform.localPosition.y} flower{ flowerBase.transform.localPosition.y} add{leafHigh}");
        petal.transform.localPosition = new Vector3(petal.transform.localPosition.x, petal.transform.localPosition.y + leafHigh, petal.transform.localPosition.z);
        flowerBase.transform.localPosition = new Vector3(flowerBase.transform.localPosition.x, flowerBase.transform.localPosition.y + leafHigh, flowerBase.transform.localPosition.z);
        leafs.Add(obj);
    }
}
