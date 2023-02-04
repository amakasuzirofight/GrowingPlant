using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniStar : MonoBehaviour
{
    [SerializeField] float destroyPosX;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float minMoveSpeed;
    float moveSpeed;
    MiniStarPool starPool;

    public void Setting(MiniStarPool miniStarPool)
    {
        starPool = miniStarPool;
        moveSpeed = UnityEngine.Random.Range(minMoveSpeed, maxMoveSpeed);
    }
    private void Update()
    {
        if (transform.position.x >= destroyPosX)
        {
            starPool.ReleaseGameObject(gameObject);
        }
        Move();
    }
    void Move()
    {
        transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, 0);
    }
}
