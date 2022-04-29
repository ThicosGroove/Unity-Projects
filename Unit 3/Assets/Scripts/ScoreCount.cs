using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    public GameObject player;
    
    GameObject[] obstacle;

    public bool hasScore = false;
    int score = 0;

    // Update is called once per frame
    void Update()
    {
        obstacle = GameObject.FindGameObjectsWithTag("Obstacle");
        Score();
    }

    void Score()
    {
        if (!hasScore)
        {
            foreach (GameObject obstacles in obstacle)
            {
                if (obstacles.transform.position.x < player.transform.position.x)
                {
                    score += 100;

                    hasScore = true;
                    Debug.LogWarning("Score " + score);
                }
            }
        }
    }
}
