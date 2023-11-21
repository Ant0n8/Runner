using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public GameManager gameManager;

    private float velocity = 20.0f;
    private float boundary = -140.0f;
    private int backgroundCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
        SpawnBackground();
        DestroyBackground();
    }

    private void MoveLeft()
    {
        if (gameObject.CompareTag("Background") && gameManager.gameState == GameState.Playing)
        {
            transform.Translate(new Vector3(velocity, 0, 0) * Time.deltaTime);
        }
    }

    private void SpawnBackground()
    {
        backgroundCount = GameObject.FindGameObjectsWithTag("Background").Length;

        if (backgroundCount < 2)
        {
            GameObject background = Instantiate(backgroundPrefab, new Vector3(-5, 0, 290), backgroundPrefab.transform.rotation);
            background.name = "Background(Clone)";
        }
    }

    private void DestroyBackground()
    {
        if (transform.position.z <= boundary && gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }
    }
}