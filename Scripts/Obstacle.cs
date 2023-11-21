using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public GameManager gameManager { protected get; set; }

    protected float velocity {  get; set; }
    private float boundary = -15.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected abstract void MoveLeft();

    protected void DestroyObstacle()
    {
        if (transform.position.z <= boundary && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}