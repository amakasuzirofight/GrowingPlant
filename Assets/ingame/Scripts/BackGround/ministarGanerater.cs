using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ministarGanerater : MonoBehaviour
{
    [SerializeField] MiniStarPool miniStarPool;
    [SerializeField] GameObject miniStarObj;
    [SerializeField] float SpornPosX;
    [SerializeField] float MinSpornPosY;
    [SerializeField] float MaxSpornPosY;
    [SerializeField] float MinGanerateSpan;
    [SerializeField] float MaxGanerateSpan;
    [SerializeField] int MaxGanerateCount;

    float timecount;
    float limitTime;
    private void Update()
    {
        timecount += Time.deltaTime;
        if (timecount >= limitTime)
        {
            int count = UnityEngine.Random.Range(1, MaxGanerateCount);
            for (int i = 0; i < count; i++)
            {
                Ganerate();
            }
            timecount = 0;
            limitTime = UnityEngine.Random.Range(MinGanerateSpan, MaxGanerateSpan);
        }
    }
    void Ganerate()
    {
        Vector3 pos = new Vector3(SpornPosX, UnityEngine.Random.Range(MinSpornPosY, MaxSpornPosY), -5);
        miniStarPool.GetGameObject(miniStarObj, pos, Quaternion.identity).GetComponent<MiniStar>().Setting(miniStarPool);

    }
}
