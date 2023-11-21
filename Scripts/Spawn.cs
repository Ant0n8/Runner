using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameManager gameManager;

    private float repeatTime;
    private int obstacleIndex;
    private Vector3 spawnPosition;
    private Coroutine spawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnStartStop();
    }

    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            repeatTime = Random.Range(1.0f, 2.0f);
            yield return new WaitForSeconds(repeatTime);

            obstacleIndex = Random.Range(0, obstaclePrefabs.Length);

            if (obstacleIndex == 2) 
            {
                float[] rocketHeights = { 1.0f, 2.5f, 4.0f };
                int rocketHeightIndex = Random.Range(0, 3);
                float rocketHeight = rocketHeights[rocketHeightIndex];
                spawnPosition = new Vector3(0, rocketHeight, 30.0f);
            }

            else
            {
                spawnPosition = new Vector3(0, 0, 30.0f);
            }

            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex], spawnPosition, obstaclePrefabs[obstacleIndex].transform.rotation);
            if (obstacleIndex == 2 && (spawnPosition.y == 2.5f || spawnPosition.y == 4.0f))
            {
                obstacle.name = "Rocket High(Clone)";
            }

            Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
            obstacleScript.gameManager = gameManager;
        }
    }

    private void SpawnStartStop()
    {
        if (gameManager.gameState == GameState.Playing && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnObstacle());
        }

        else if (gameManager.gameState != GameState.Playing && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}