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


    void Start() {
        mousePhysics = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
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

        //trigger jumping animation
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerAudioSource.PlayOneShot(jumpingSound);
        }        

        if (Input.GetKey(KeyCode.Space)) {
            mouseAnimator.SetBool("isJumping", true);
        }
        else {
            mouseAnimator.SetBool("isJumping", false);
        }

        //timer starts when the player moves for the first time.
        //and continues to increase until the mouse gets the cheese
        if (direction != Vector2.zero) {
            isMoving = true;
        }

        if (isMoving) {
            timer += Time.deltaTime;
        }

        //cheat key for coding hehe
        if (Input.GetKeyDown(KeyCode.C)) {
            transform.position = new Vector2(176.4f, -235.3f);
        }

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