using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button startButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartButtonClick()
    {
        if (gameManager.gameState == GameState.Start)
        {
            gameManager.gameState = GameState.Playing;
        }
    }
}