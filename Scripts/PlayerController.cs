using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private float jumpForce = 1000.0f;

    public Animator playerAnimator;
    private Rigidbody playerRigidBody;
    private float gravityModifier = 3.0f;
    private bool isGrounded;
    private bool isSliding;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Slide();
        keepPosition();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetBool("Slide") && isGrounded && !isSliding && gameManager.gameState == GameState.Playing)
        {
            playerAnimator.SetTrigger("Jump");
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !playerAnimator.GetBool("Jump") && isGrounded && gameManager.gameState == GameState.Playing)
        {
            playerAnimator.SetBool("Slide", true);
            isSliding = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerAnimator.SetBool("Slide", false);
            StartCoroutine(SlideExitDelay());
        }
    }

    private IEnumerator SlideExitDelay()
    {
        yield return new WaitForSeconds(0.15f);
        isSliding = false;
    }

    private void Run()
    {
        if (transform.position.y <= 0.2 && !playerAnimator.GetBool("Slide") && !playerAnimator.GetBool("Jump") && isGrounded && gameManager.gameState == GameState.Playing)
        {
            playerAnimator.SetBool("Run", true);
        }

        else
        {
            playerAnimator.SetBool("Run", false);
        }
    }

    private void keepPosition()
    {
        if (transform.position.y != 0 && isGrounded)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (transform.position.x != 0 || transform.position.z != 0)
        {
            transform.position = new Vector3(0, transform.position.y, 0);
        }

        if (transform.rotation.x != 0 || transform.rotation.y != 0 || transform.rotation.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle") && !collision.gameObject.name.Contains("High"))
        {
            gameManager.gameState = GameState.GameOver;
            playerAnimator.SetBool("Idle", false);
            playerAnimator.SetBool("Death", true);
        }
  
        else if (collision.gameObject.CompareTag("Obstacle") && !playerAnimator.GetBool("Slide") && !isSliding && collision.gameObject.name.Contains("High"))
        {
            gameManager.gameState = GameState.GameOver;
            playerAnimator.SetBool("Idle", false);
            playerAnimator.SetBool("Death", true);
        }
    }
}