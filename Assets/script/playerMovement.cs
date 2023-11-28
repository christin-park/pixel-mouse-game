using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour {  
    //scripts
    public playingGame playingGame;
    public playerCollision playerCollision;
    public demo demo;

    //ref
    public Rigidbody2D mousePhysics;
    public GameObject mouseImg;
    public Animator mouseAnimator;
    public float moveSpeed;
    public AudioClip jumpingSound;
    private AudioSource playerAudioSource;
    public GameObject imgSpawnPoint;
    public GameObject jumpWall;
    public BoxCollider2D jumpWallCollider;
    public Renderer jumpWallRenderer;
    public GameObject popup;
    public TMP_Text popupText;


    //vars
    public bool isMoving = false;
    public bool jumpBool = false;
    public float timer = 0.0f;
    public bool setNextGameRan = false;
    public bool rTriggered = false;
    public bool tTriggered = false;
    public bool uTriggered = false;
    private int jCount = 0;
    private float jTimer = 0f;
    private bool jPopup = false;   
    private int rCount = 0;
    private float rTimer = 0f;
    private bool rPopup = false;   
    private int tCount = 0;
    private float tTimer = 0f;
    private bool tPopup = false;   
    private int uCount = 0;
    private float uTimer = 0f;
    private bool uPopup = false;   
    private float qTimer = 0f;
    private float eTimer = 0f;
    private bool isRotatingCCW = false;
    private bool isRotatingCW = false;
    public bool isJumping = false;
    public Vector2 spawnPoint;
    private bool allowMovement = true;


    void Start() {
        mousePhysics = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
        jumpWallRenderer = jumpWall.GetComponent<Renderer>();
        jumpWallCollider = jumpWall.GetComponent<BoxCollider2D>();
        mouseAnimator = GetComponent<Animator>();
        spawnPoint = transform.position;
        popup.SetActive(false);

    }

    void Update() {
        if (allowMovement) {
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

            //temp cheat keys for coding hehe
            // if (Input.GetKeyDown(KeyCode.C)) {
            //     transform.position = new Vector2(176.4f, -235.3f);
            // }
            // if (Input.GetKeyDown(KeyCode.K)) {
            //     transform.position = new Vector2(26f, 125f);
            // }
            
            //ARDUINO COMPATIBLE VERSION
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //J jump
            if (Input.GetKey(KeyCode.J)) {
                mouseAnimator.SetTrigger("jump");
                mouseAnimator.SetBool("isJumping", true);
                playerAudioSource.PlayOneShot(jumpingSound);
                jumpWallRenderer.sortingLayerName = "Default";
                jumpWallCollider.enabled = false;
                Invoke("resetIsJumping", 1.4f);

                jCount++;
                if (Time.time - jTimer > 1f) {
                    jCount = 1;
                    jPopup = false;
                }

                if (jCount == 1) {
                    jTimer = Time.time;
                }

                if (jCount >= 2 && !jPopup) {
                    popup.SetActive(true);
                    popupText.text = "'J' should only register once for every button press!";
                    jPopup = true;
                }
            }

            //R reset 1
            if (Input.GetKey(KeyCode.R)) {
                resetLevel();
                rTriggered = true;

                rCount++;
                if (Time.time - rTimer > 1f) {
                    rCount = 1;
                    rPopup = false;
                }

                if (rCount == 1) {
                    rTimer = Time.time;
                }

                if (rCount >= 2 && !rPopup) {
                    popup.SetActive(true);
                    popupText.text = "'R' can only be triggered once!";
                    rPopup = true;
                }
            }

            //T reset 2
            if (Input.GetKey(KeyCode.T)) {
                resetLevel();
                tTriggered = true;

                tCount++;
                if (Time.time - tTimer > 1f) {
                    tCount = 1;
                    tPopup = false;
                }

                if (tCount == 1) {
                    tTimer = Time.time;
                }

                if (tCount >= 2 && !tPopup) {
                    popup.SetActive(true);
                    popupText.text = "'T' can only be triggered once!";
                    tPopup = true;
                }
            }

            //Q rotate CCW
            if (Input.GetKeyDown(KeyCode.Q)) {
                qTimer = Time.time;
            }

            if (Input.GetKey(KeyCode.Q)) {
                float angle = 0.3f * 360f * Time.deltaTime;
                transform.Rotate(Vector3.forward, angle);

                float qPressDuration = Time.time - qTimer;
                if (qPressDuration > 0.05f) {
                    popup.SetActive(true);
                    popupText.text = "'Q' should be coded as a toggle button!";
                }

            }
            
            //E rotate CW
            if (Input.GetKeyDown(KeyCode.E)) {
                eTimer = Time.time;
            }
            if (Input.GetKey(KeyCode.E)) {
                float angle = -0.3f * 360f * Time.deltaTime;
                transform.Rotate(Vector3.forward, angle);

                float ePressDuration = Time.time - eTimer;
                if (ePressDuration > 0.05f) {
                    popup.SetActive(true);
                    popupText.text = "'E' should be coded as a toggle button!";
                }
            }

            //F forward
            if (Input.GetKey(KeyCode.F)) {
                float currAngle = Mathf.Atan2(-transform.up.y, - transform.up.x);

                float horizontalForward = Mathf.Cos(currAngle);
                float verticalForward = Mathf.Sin(currAngle);
                Vector2 forwardDirection = new Vector2(horizontalForward, verticalForward);

                float forwardSpeed = Mathf.Max(Mathf.Abs(horizontalForward), Mathf.Abs(verticalForward));
                mouseAnimator.SetFloat("speed", forwardSpeed);

                mousePhysics.velocity = forwardDirection * moveSpeed;
            }

            //U set spawnpoint to curr location
            if (Input.GetKey(KeyCode.U)) {
                spawnPoint = transform.position;
                imgSpawnPoint.transform.position = new Vector2(spawnPoint.x, spawnPoint.y);

                uCount++;
                if (Time.time - uTimer > 1f) {
                    uCount = 1;
                    uPopup = false;
                }

                if (uCount == 1) {
                    uTimer = Time.time;
                }

                if (uCount >= 2 && !uPopup) {
                    popup.SetActive(true);
                    popupText.text = "'U' can only be triggered once!";
                    uPopup = true;
                }
            }

        }

        //if the button behavior rules are violated, error popup appears
        if (popup.activeSelf) {
            mousePhysics.velocity = Vector2.zero;
            mouseAnimator.enabled = false;
            allowMovement = false;
        }
        else {
            mouseAnimator.enabled = true;
            allowMovement = true;
        }


        /*
        //KEYBOARD COMPATIBLE VERSION
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //J jump
        if (Input.GetKeyDown(KeyCode.J) && (isJumping == false)) {
            isJumping = true;
            mouseAnimator.SetTrigger("jump");
            mouseAnimator.SetBool("isJumping", true);
            playerAudioSource.PlayOneShot(jumpingSound);
            jumpWallRenderer.sortingLayerName = "Default";
            jumpWallCollider.enabled = false;
            Invoke("resetIsJumping", 1.4f);
        }
        // if (Input.GetKey(KeyCode.J) && !jumpBool) {
        //     jumpBool = true;
        //     Debug.Log("hi j pressed");
        //     mouseAnimator.SetBool("isJumping", true);
        // }
        // else if (!Input.GetKey(KeyCode.J)) {
        //     jumpBool = false;
        //     mouseAnimator.SetBool("isJumping", false);
        // }

        //R reset 1
        if (Input.GetKeyDown(KeyCode.R)) {
            if (rTriggered) {
                return;
            }
            rTriggered = true;
            resetLevel();
        }

        //T reset 2
        if (Input.GetKeyDown(KeyCode.T)) {
            if (tTriggered) {
                return;
            }
            tTriggered = true;
            resetLevel();
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

        //F forward
        if (Input.GetKey(KeyCode.F)) {
            float currAngle = Mathf.Atan2(-transform.up.y, - transform.up.x);
            Debug.Log(currAngle);

            float horizontalForward = Mathf.Cos(currAngle);
            float verticalForward = Mathf.Sin(currAngle);
            Vector2 forwardDirection = new Vector2(horizontalForward, verticalForward);

            float forwardSpeed = Mathf.Max(Mathf.Abs(horizontalForward), Mathf.Abs(verticalForward));
            mouseAnimator.SetFloat("speed", forwardSpeed);

            mousePhysics.velocity = forwardDirection * moveSpeed;
        }

        //U set spawnpoint to curr location
        if (Input.GetKeyDown(KeyCode.U)) {
            if (uTriggered) {
                return;
            }
            uTriggered = true;
            spawnPoint = transform.position;
            imgSpawnPoint.transform.position = new Vector2(spawnPoint.x, spawnPoint.y);
        }
        */

        //set up for next game
        if (playerCollision.cheeseTouch && !setNextGameRan) {
            mousePhysics.velocity = Vector2.zero;
            playingGame.setNextGame();
            setNextGameRan = true;
        }
        if (demo.leaderboard.activeSelf) {
            mousePhysics.velocity = Vector2.zero;
        }

    }

    //reset isJumping after the jump animation has finished
    public void resetIsJumping() {
        isJumping = false;
        jumpWallCollider.enabled = true;
        mouseAnimator.SetBool("isJumping", false);
    }
    //reset level
    public void resetLevel() {
        transform.position = new Vector2(-339f, 226.2f);
        imgSpawnPoint.transform.position = new Vector2(-339f, 226.2f);
        spawnPoint = new Vector2(-339f, 226.2f);
        timer = 0.0f;
        isMoving = false;
        playerCollision.mazeKey.SetActive(true);
        playerCollision.mazeWall.SetActive(true);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
