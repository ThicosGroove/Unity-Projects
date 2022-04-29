using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceX : MonoBehaviour
{   
    public float bottom;
    public float bounceForce;

    private PlayerControllerX playerControllerScript;
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= bottom && !playerControllerScript.gameOver)
        {
            Bounce();
        }
    }

    void Bounce()
    {
        playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }

}
