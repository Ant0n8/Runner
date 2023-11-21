using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum GameState
{
    Start,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public Canvas startCanvas;
    public Canvas playingCanvas;
    public Canvas gameOverCanvas;
    public GameState gameState { get; set; }

    private int score = 0;
    private Coroutine scoreIncreaseCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Start;
        scoreText.text = $"Score: {score}";
        finalScoreText.text = $"Score: {score}";
    }

    // Update is called once per frame
    void Update()
    {
        GetGameState();
        ScoreIncreaseStartStop();
    }

    private IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            score++;
            scoreText.text = $"Score: {score}";
        }
    }

    private void ScoreIncreaseStartStop()
    {
        if (gameState == GameState.Playing && scoreIncreaseCoroutine == null)
        {
            scoreIncreaseCoroutine = StartCoroutine(IncreaseScore());
        }

        else if (gameState != GameState.Playing && scoreIncreaseCoroutine != null)
        {
            StopCoroutine(scoreIncreaseCoroutine);
            scoreIncreaseCoroutine = null;
        }
    }

    private void GetGameState()
    {
        if (gameState == GameState.Start)
        {
            startCanvas.gameObject.SetActive(true);
            playingCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(false);
        }

        else if (gameState == GameState.Playing)
        {
            startCanvas.gameObject.SetActive(false);
            playingCanvas.gameObject.SetActive(true);
            gameOverCanvas.gameObject.SetActive(false);
        }

        else if (gameState == GameState.GameOver)
        {
            startCanvas.gameObject.SetActive(false);
            playingCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);

            finalScoreText.text = $"Score: {score}";
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = $"Score: {score}";
    }
}