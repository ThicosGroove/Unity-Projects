using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    PlayerController playerController;
    ScoreCount scoreCount;

    //float startDelay = 2f;
    float repeatRate = 2f;

    StartGame startGame;

    public bool isNewObstacle;

    Vector3 spawnPos = new Vector3(25, 0, 0);

    private bool hasInvoked;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        scoreCount = FindObjectOfType<ScoreCount>();
        startGame = FindObjectOfType<StartGame>();
    }

    void Update()
    {
        if (startGame.hasStartGame && !hasInvoked)
        {
            Invoke("SpawnObstacle", repeatRate);

            hasInvoked = true;
        }
    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefab.Length);

        if (!playerController.gameOver)
        {
            Instantiate(obstaclePrefab[index], spawnPos, obstaclePrefab[index].transform.rotation);
            scoreCount.hasScore = false;
        }

        hasInvoked = false;
    }
}
