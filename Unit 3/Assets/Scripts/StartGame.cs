using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public bool hasStartGame;

    // Start is called before the first frame update
    void Start()
    {
        hasStartGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            hasStartGame = true;
        }
    }
}