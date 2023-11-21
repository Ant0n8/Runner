using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public GameManager gameManager;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(RestartButtonClick);
        mainMenuButton.onClick.AddListener(MainMenuButtonClick);
        playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RestartButtonClick()
    {
        if (gameManager.gameState == GameState.GameOver)
        {
            ResetGame();
            gameManager.gameState = GameState.Playing;
        }
    }

    private void MainMenuButtonClick()
    {
        if (gameManager.gameState == GameState.GameOver)
        {
            ResetGame();
            gameManager.gameState = GameState.Start;
        }
    }

    private void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    private void ResetGame()
    {
        gameManager.ResetScore();
        DestroyObstacles();
        playerController.playerAnimator.SetBool("Death", false);
        playerController.playerAnimator.SetBool("Idle", true);
    }
}