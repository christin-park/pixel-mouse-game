using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerMovement : MonoBehaviour {  
    //scripts
    public playingGame playingGame;
    public playerCollision playerCollision;

    //ref
    private Rigidbody2D mousePhysics;
    private SpriteRenderer mouseImg;
    public Animator mouseAnimator;
    public float moveSpeed;
    public AudioClip jumpingSound;
    private AudioSource playerAudioSource;

    //vars
    public bool isMoving = false;
    public float timer = 0.0f;
    public bool setNextGameRan = false;
    private bool isRotatingCCW = false;
    private bool isRotatingCW = false;


    void Start() {
        mousePhysics = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        //WASD basic player movement
        float horizontalVal = Input.GetAxis("Horizontal");
        float verticalVal = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontalVal, verticalVal);
        direction.Normalize();

    
        float speed = Mathf.Max(Mathf.Abs(horizontalVal), Mathf.Abs(verticalVal));
        mouseAnimator.SetFloat("speed", speed);

        mousePhysics.velocity = direction * moveSpeed;


        if (direction != Vector2.zero) {
            transform.up = -1 * direction;
        }

        //timer starts when the player moves for the first time
        //and continues to increase until the mouse gets the cheese
        if (direction != Vector2.zero) {
            isMoving = true;
        }
        if (isMoving) {
            timer += Time.deltaTime;
        }

        //temp cheat key for coding hehe
        if (Input.GetKeyDown(KeyCode.C)) {
            transform.position = new Vector2(176.4f, -235.3f);
        }

        //J jump
        if (Input.GetKeyDown(KeyCode.J)) {
            playerAudioSource.PlayOneShot(jumpingSound);
        }        

        if (Input.GetKey(KeyCode.J)) {
            mouseAnimator.SetBool("isJumping", true);
        }
        else {
            mouseAnimator.SetBool("isJumping", false);
        }

        //R reset 1
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = new Vector2(-339f, 226.2f);
        }

        //T reset 2
        if (Input.GetKeyDown(KeyCode.T)) {
            transform.position = new Vector2(-339f, 226.2f);
        }

        //Q rotate CCW
        if (Input.GetKeyDown(KeyCode.Q)) {
            isRotatingCCW = !isRotatingCCW;
        }

        if (isRotatingCCW) {
            float angle = 0.3f * 360f * Time.deltaTime;
            transform.Rotate(Vector3.forward, angle);
        }

        //E rotate CW
        if (Input.GetKeyDown(KeyCode.E)) {
            isRotatingCW = !isRotatingCW;
        }

        if (isRotatingCW) {
            float angle = -0.3f * 360f * Time.deltaTime;
            transform.Rotate(Vector3.forward, angle);
        }

        //F forward THIS DOESNT WORK YETTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("f pressed");
            Vector3 forwardDirection = transform.forward;
            forwardDirection.Normalize();
            Vector3 movement = forwardDirection * moveSpeed * Time.deltaTime;
            transform.position += movement;
        }

        //Y set spawnpoint to curr location


        //set up for next game
        if (playerCollision.cheeseTouch && !setNextGameRan) {
            mousePhysics.velocity = Vector2.zero;
            playingGame.setNextGame();

            setNextGameRan = true;

        //     //if nextPlayer is clicked, then start the game for the next player
        //     if (playingGame.nextPlayerClicked) {
        //         playerCollision.cheeseTouch = false;
        //         ++playingGame.currPlayer;
        //         playingGame.beginGame();
        //     }
        // }
            // if (playingGame.currPlayer == numPlayers) {
            //     SceneManager.LoadScene(2);
            // }   
        }

    }
}
