using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    //  地球オブジェクト
    [SerializeField] private GameObject ground;

    //  水オブジェクト
    [SerializeField] private GameObject water;
    //  溶岩オブジェクト
    [SerializeField] private GameObject lava;

    //  それぞれの生成する数
    [SerializeField] private int waterNum = 2;
    [SerializeField] private int lavaNum = 2;

    //  アイテム同士の間隔
    [SerializeField] private float ItoIDistance = 1.5f;
    //  y軸からの生成距離
    [SerializeField] private float yDistance = 0.3f;

    //  円を

    // Start is called before the first frame update
    void Start()
    {
        CreateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  アイテムを生成する
    public void CreateItems()
    {
        //  生成用のオブジェクト配列
        List<GameObject> objs = new List<GameObject>();

        //  水オブジェクトの生成
        for(int i = 0; i<waterNum;i++)
        {
            //  生成できるか
            bool createflag = true;

            //  生成場所を決める
            Vector3 pos = CreatePlace(i, water.transform.localScale, objs, ref createflag) ;
            //  生成できない場合は飛ばす
            if (!createflag) continue;
            //  生成する
            objs.Add(Instantiate(water, pos, Quaternion.identity));
        }

        //  溶岩オブジェクトの生成
        for (int i = 0; i < lavaNum; i++)
        {
            //  生成できるか
            bool createflag = true;

            //  生成場所を決める
            Vector3 pos = CreatePlace(i, water.transform.localScale, objs, ref createflag);
            //  生成できない場合は飛ばす
            if (!createflag) continue;
            //  生成する
            objs.Add(Instantiate(lava, pos, Quaternion.identity));
        }

        //  全て子オブジェクトにする
        foreach(var obj in objs)
        {
            obj.transform.parent = ground.transform;
        }
    }

    //  生成場所を決める
    private Vector3 CreatePlace(int num, Vector3 itemScale, List<GameObject> list, ref bool createFlag)
    {
        //  上下の制限座標(地球の絶対半径 - アイテムの相対半径)
        float top = ground.transform.localScale.x / 2.0f - 1.5f / 2.0f;
        //  左右の制限座標
        float side = ground.transform.localScale.y / 2.0f - 1.5f / 2.0f;
        //  座標
        Vector3 ret = Vector3.zero;
        int i = 0;
        //  偶数の場合（地球の上方に作る）
        if (num % 2 == 0)
        {
            //  座標が確定するまで繰り返す・百万回やって生成不可だったら終了
            while(i < 1000000)
            {
                i++;
                //  座標をランダムで取得
                ret = new Vector3(Random.Range(-side, side), Random.Range(yDistance, top), 0.0f);
                //  座標と中心点までの距離を取得
                float distance = Vector3.Distance(new Vector3(0.0f, 0.0f, 0.0f), ret);

                //  半径よりも小さい
                if (top > distance)
                {   
                    //  0以下なら何もしない
                    if(list.Count <= 0) return ret;

                    //  確認用の変数
                    int hit = 0;
                    //  全てのオブジェクトと当たっているか確認する
                    foreach(var obj in list)
                    {
                        //  比較用の距離
                        float length = Vector3.Distance(obj.transform.position, ret);
                        //  一つでも接しているなら処理を抜ける
                        if (length <= ItoIDistance) break;
                        hit++;
                    }

                    if (hit >= list.Count) return ret;
                }
            }
        }
        //  奇数の場合（地球の下方に作る）
        else
        {
            //  座標が確定するまで繰り返す・百万回やって生成不可だったら終了
            while (i < 1000000)
            {
                i++;
                //  座標をランダムで取得
                ret = new Vector3(Random.Range(-side, side), Random.Range(-top, -yDistance), 0.0f);
                //  座標と中心点までの距離を取得
                float distance = Vector3.Distance(new Vector3(0.0f, 0.0f, 0.0f), ret);
                //  半径よりも小さいならOK
                if (top > distance)
                {
                    //  0以下なら何もしない
                    if (list.Count <= 0) return ret;
                    //  確認用の変数
                    int hit = 0;
                    //  全てのオブジェクトと当たっているか確認する
                    foreach (var obj in list)
                    {
                        //  比較用の距離
                        float length = Vector3.Distance(obj.transform.position, ret);
                        //  一つでも接しているなら処理を抜ける
                        if (length <= ItoIDistance) break;
                        hit++;
                    }

                    if (hit >= list.Count) return ret;
                }
            }
        }

        createFlag = false;

        return ret;
    }
}
