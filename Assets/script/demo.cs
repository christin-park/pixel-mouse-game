using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class demo : MonoBehaviour {
    //refs
    public TMP_Text textBox;
    public GameObject leaderboard;
    public Button resetButton;
    public Button resetButton2;

    //scripts
    public playerMovement playerMovement;
    public playerCollision playerCollision;

    //vars
    private string origString = "Keys to try: WASDJRTQEFU";
    private string currString;
    private Dictionary<char, KeyCode> keyDict = new Dictionary<char, KeyCode> {
        {'W', KeyCode.W},
        {'A', KeyCode.A},
        {'S', KeyCode.S},
        {'D', KeyCode.D},
        {'J', KeyCode.J},
        {'R', KeyCode.R},
        {'T', KeyCode.T},
        {'Q', KeyCode.Q},
        {'E', KeyCode.E},
        {'F', KeyCode.F},
        {'U', KeyCode.U}
    };

    void Start() {
        currString = origString;
        textBox.text = currString;
        leaderboard.SetActive(false);
        resetButton.onClick.AddListener(resetDemo);
        resetButton2.onClick.AddListener(resetDemo);
    }   

    void Update() {
        //if all keys are tried, then change string
        if (currString.Trim() == "Keys to try:") {
            currString = "All done! :D";
            textBox.text = currString;
        }

        //remove keys
        foreach (KeyValuePair<char, KeyCode> key in keyDict) {
            if (Input.GetKey(key.Value)) {
                removeChar(key.Key);
            }
        }  
    }

    private void removeChar(char letter) {
        for (int i = 12; i < currString.Length; ++i) {
            if (currString[i] == letter) {
                currString = currString.Remove(i, 1);
                textBox.text = currString;
                break;
            }
        }
    }
    public void finishDemo() {
        leaderboard.SetActive(true);
        playerMovement.mouseImg.SetActive(false);
    }

    private void resetDemo() {
        leaderboard.SetActive(false);
        playerCollision.mazeKey.SetActive(true);
        playerCollision.mazeCheese.SetActive(true);
        currString = origString;
        textBox.text = currString;
        playerMovement.resetLevel();
        playerCollision.cheeseTouch = false;
        playerMovement.timer = 0.0f;
        playerMovement.mouseImg.SetActive(true);
        playerMovement.transform.rotation = Quaternion.Euler(Vector3.zero);
        playerMovement.rTriggered = false;
        playerMovement.tTriggered = false;
        playerMovement.uTriggered = false;
        playerMovement.isMoving = false;
        playerMovement.popup.SetActive(false);
    }
}
