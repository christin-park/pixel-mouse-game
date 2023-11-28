using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour {

    public GameObject mazeKey;
    public GameObject mazeCheese;
    public GameObject mazeWall;

    public AudioClip mazeKeySound;
    public AudioClip mazeCheeseSound;
    public AudioClip mazeWallSound;

    private AudioSource playerAudioSource;

    public playerMovement playerMovement;
    public playingGame playingGame;
    public demo demo;

    public bool cheeseTouch = false;

    void Start() {
        playerAudioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("maze-key")) {
            mazeKey.SetActive(false);
            mazeWall.SetActive(false);
            playerAudioSource.PlayOneShot(mazeKeySound);   
        }
        else if (collision.gameObject.CompareTag("maze-cheese")) {
            mazeCheese.SetActive(false);
            playerAudioSource.PlayOneShot(mazeCheeseSound);
            playerMovement.isMoving = false;
            Debug.Log(playerMovement.timer);
            cheeseTouch = true;
        }
        else if (collision.gameObject.CompareTag("maze-barrier")) {
            transform.position = playerMovement.spawnPoint;
            playerAudioSource.PlayOneShot(mazeWallSound);
        }
        else if (collision.gameObject.CompareTag("maze-wall")) {
            if (!playerMovement.isJumping) {
                transform.position = playerMovement.spawnPoint;
                playerAudioSource.PlayOneShot(mazeWallSound);
            }    
        }
        else if (collision.gameObject.CompareTag("maze-cheese-demo")) {
            mazeCheese.SetActive(false);
            playerAudioSource.PlayOneShot(mazeCheeseSound);
            playerMovement.isMoving = false;
            Debug.Log(playerMovement.timer);
            cheeseTouch = true;
            demo.finishDemo();
        }
    }


}
