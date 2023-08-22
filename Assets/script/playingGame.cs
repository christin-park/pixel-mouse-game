using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class playingGame : MonoBehaviour {

    //scripts
    public readPlayerData readPlayerData;
    public playerMovement playerMovement;
    public playerCollision playerCollision;
    public selectLevel selectLevel;

    //refs
    public GameObject leaderboard;
    public TMP_Text leaderboardText;
    public Button exitSave;
    public Button exitDontSave;
    public Button nextPlayer;
    public Button forfeit;

    //vars
    public int currPlayer = 0;
    string leaderboardTitle = "Rank           Player          Map Time" + "\n";

    
    void Awake() {
        leaderboard.SetActive(false);

        exitSave.onClick.AddListener(exitSaveFunction);
        exitDontSave.onClick.AddListener(exitDontSaveFunction);
        nextPlayer.onClick.AddListener(nextPlayerFunction);
        forfeit.onClick.AddListener(forfeitFunction);
    }

    public void setNextGame() {
        //update currPlayer's time
        readPlayerData.arrayPlayerData[currPlayer][selectLevel.level - 1] = playerMovement.timer;

        //FOR DEBUGGINGGGGGGG
        for (int i = 0; i < readPlayerData.arrayPlayerData.Length; i++) {
        string rowValues = "";
            for (int j = 0; j < readPlayerData.arrayPlayerData[i].Length; j++) {
                    rowValues += readPlayerData.arrayPlayerData[i][j] + " ";
                }
            Debug.Log(rowValues);
        }
        ///////////////////////

        //calculate the best time (must create a new deep copy array to mess with the order, otherwise prev line wouldnt work anymore)
        // float[][] editArrayPlayerData = readPlayerData.arrayPlayerData; NOT A DEEP COPY D:<
        float[][] editArrayPlayerData = new float[readPlayerData.numPlayers][];
        for (int i = 0; i < readPlayerData.numPlayers; i++) {
            editArrayPlayerData[i] = new float[readPlayerData.playerData];
            for (int j = 0; j < readPlayerData.playerData; j++) {
                editArrayPlayerData[i][j] = readPlayerData.arrayPlayerData[i][j];
            }
        }
        
        readPlayerData.calculateBestTime(editArrayPlayerData);
        
        //FOR DEBUGGINGGGGGGG
        for (int i = 0; i < editArrayPlayerData.Length; i++) {
        string rowValues = "";
            for (int j = 0; j < editArrayPlayerData[i].Length; j++) {
                    rowValues += editArrayPlayerData[i][j] + " ";
                }
            Debug.Log(rowValues);
        }
        ///////////////////////


        //display leaderboard
        leaderboardText.text = $"{leaderboardTitle}\n";
            
        for (int i = 0; i < readPlayerData.numPlayers; ++i) {
            string formatLeaderboard =
                                        //rank          player                          map time                                                
                                        $"{(i + 1), -15:D2} {readPlayerData.playerNames[i], -14} {editArrayPlayerData[i][selectLevel.level - 1], -14:F2}"  ;
            
            leaderboardText.text += formatLeaderboard + "\n";
        }

        leaderboard.SetActive(true);
    }

    //button functions
    private void exitSaveFunction() {
        SceneManager.LoadScene(2); //take back to level select
    }

    private void exitDontSaveFunction() {
        SceneManager.LoadScene(2); //take back to level select

        //clear data for current level
        for (int i = 0; i < readPlayerData.numPlayers; ++i) {
            readPlayerData.arrayPlayerData[0][selectLevel.level - 1] = 0.0f;
        }
        
    }

    private void nextPlayerFunction() {
        playerMovement.setNextGameRan = false;
        playerCollision.cheeseTouch = false;
        ++currPlayer;

        //check if all players have played the game
        if ((currPlayer) == readPlayerData.numPlayers) {
                SceneManager.LoadScene(2);
        }   
        leaderboard.SetActive(false);

        playerMovement.transform.position = new Vector2(-339f, 226.2f);
        playerCollision.mazeCheese.SetActive(true);
        playerMovement.timer = 0.0f;
    }
    
    //set current player's score to 0
    private void forfeitFunction() {
        readPlayerData.arrayPlayerData[currPlayer][selectLevel.level - 1] = 0.0f;
        setNextGame();  
    }

}
