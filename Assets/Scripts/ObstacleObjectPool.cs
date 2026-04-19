using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    public static ObstacleObjectPool instance;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        instance = this;
    }

    private IEnumerator Start() 
    {
        for (int i = 0; i < poolSize; i++) 
        {
            CreateBarrel();
            CreateBarrier();
            CreateWall();

            if (i % 5 == 0)
            {
                yield return null;
            }
        }
    }

    private void CreateBarrel() 
    {
        var goBarrel = Instantiate(obstacleBarrelPrefab);
        goBarrel.SetActive(false);
        obstacleBarrelPool.Add(goBarrel);
    }

    private void CreateBarrier()
    {
        var goBarrier = Instantiate(obstacleBarrierPrefab);
        goBarrier.SetActive(false);
        obstacleBarrierPool.Add(goBarrier);
    }

    private void CreateWall()
    {
        var goWall = Instantiate(obstacleStoneWallPrefab);
        goWall.SetActive(false);
        obstacleStoneWallPool.Add(goWall);
    }

    public GameObject Acquire(int obstacleType)
    {
        switch (obstacleType) 
        {
            case 1:
                if (obstacleBarrelPool.Count <= 0) 
                {
                    CreateBarrel();
                }

                var goBarrel = obstacleBarrelPool[0];
                obstacleBarrelPool.RemoveAt(0);

                goBarrel.SetActive(true);

                return goBarrel;

            case 2:
                if (obstacleBarrierPool.Count <= 0)
                {
                    CreateBarrier();
                }
                var goBarrier = obstacleBarrierPool[0];
                obstacleBarrierPool.RemoveAt(0);

                goBarrier.SetActive(true);

                return goBarrier;

            case 3:
                if (obstacleStoneWallPool.Count <= 0)
                {
                    CreateWall();
                }
                var goWall = obstacleStoneWallPool[0];
                obstacleStoneWallPool.RemoveAt(0);

                goWall.SetActive(true);

                return goWall;

            default:
                Debug.Log("error obstacleType that not in 3 type : " + obstacleType);
                break;
        }

        return null;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        switch (obstacleType)
        {
            case 1:
                obstacleBarrelPool.Add(obstacle) ;

                obstacle.SetActive(false);

                break;

            case 2:
                
                obstacleBarrierPool.Add(obstacle);

                obstacle.SetActive(false);

                break;

            case 3:
                
                obstacleStoneWallPool.Add(obstacle);

                obstacle.SetActive(false);

                break;

            default:
                Debug.Log("error obstacleType that not in 3 type : " + obstacleType);
                break;
        }
    }
}
