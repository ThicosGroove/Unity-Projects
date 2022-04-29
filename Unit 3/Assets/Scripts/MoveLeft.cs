using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    PlayerController playerController;
    StartGame startGame;

    float speed = 15;
    float runningSpeed = 25;
    float startSpeed = 8;
    int leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        startGame = FindObjectOfType<StartGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startGame.hasStartGame)
        {
            transform.Translate(Vector3.left * Time.deltaTime * startSpeed);
        }
        else if (!playerController.gameOver && !playerController.running)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (!playerController.gameOver && playerController.running)
        {
            transform.Translate(Vector3.left * Time.deltaTime * runningSpeed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
