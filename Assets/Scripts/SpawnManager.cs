using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject obstaclePrefab;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

    void Spawn()
    {
        // 1.18 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        bool isGameOver = player.GetComponent<PlayerController>().gameOver;
        if (isGameOver)
        {
            return;
        }

        int obstacleType = Random.Range(1, 4);

        var obstacle = ObstacleObjectPool.instance.Acquire(obstacleType);

        /*Instantiate(
            obstacle,
            spawnPoint.position,
            obstacle.transform.rotation
        );*/
        obstacle.transform.SetPositionAndRotation(spawnPoint.position, obstacle.transform.rotation);

        MoveLeft obstacleScriptMoveLeft = obstacle.GetComponent<MoveLeft>();
        if (obstacleScriptMoveLeft)
        {
            obstacleScriptMoveLeft.obstacleType = obstacleType;
        }
        else 
        {
            Debug.Log("Error can't find MoveLeft script in obstacle ");
        }

    }
}
