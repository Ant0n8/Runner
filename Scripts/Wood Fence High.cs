using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFenceHigh : Obstacle
{
    // Start is called before the first frame update
    void Start()
    {
        velocity = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
        DestroyObstacle();
    }

    protected override void MoveLeft()
    {
        if (gameObject.CompareTag("Obstacle") && gameManager.gameState == GameState.Playing)
        {
            transform.Translate(new Vector3(0, 0, velocity) * Time.deltaTime);
        }
    }
}