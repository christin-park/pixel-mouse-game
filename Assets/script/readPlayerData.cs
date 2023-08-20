using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class readPlayerData : MonoBehaviour {
    public GameObject errorMessage;
    public sceneSwitch sceneSwitch;
    public static int numPlayers;
    public static string[] playerNames;
    /*
    playerData is the data in the array containing
    index      data
    --------------------------
    0          map 1 score
    1          map 2 score
    2          map 3 score
    3          avg score
    */ 
    public static int playerData = 4; 
    public static float[][] arrayPlayerData;

    //instatiate arrayPlayerData with 0
    void Start() {
        errorMessage.SetActive(false);
    }

    public void readInput(string readString) {
        try {
            numPlayers = int.Parse(readString);
            Debug.Log("input int: " + numPlayers);
            
            /*
                create player names
                for example, if 3 players are inputed into readString,
                {Player 1, Player 2, Player 3}
            */
            playerNames = new string[numPlayers];
            for (int i = 1; i <= numPlayers; ++i) {
                playerNames[i - 1] = "Player " + i;
            }
            
            /*
                arrayPlayerData creates a 2D array where the
                row is the numPlayers
                column is the playerData
                and all vals are instantiated to 0

                for example, if 3 players are inputed into readString,
                Player 1: 0, 0, 0, 0,
                Player 2: 0, 0, 0, 0,
                Player 3: 0, 0, 0, 0,

                arrayPlayerData[numPlayers][playerData]
            */

            arrayPlayerData = new float[numPlayers][];
            
            for (int i = 0; i < numPlayers; ++i) {
                arrayPlayerData[i] = new float[playerData];
                for (int j = 0; j < playerData; ++j) {
                    arrayPlayerData[i][j] = 0.0f;
                }
            }

            //print out playernames
            for (int i = 0; i < playerNames.Length; i++) {
                Debug.Log(playerNames[i]);
            }

            //print out data of arrayPlayerData
            for (int i = 0; i < numPlayers; ++i) {
                string playerDataString = "";
                for (int j = 0; j < playerData; ++j) {
                    playerDataString += arrayPlayerData[i][j] + ", ";
                }
                Debug.Log(playerDataString);
            }

            sceneSwitch.addBuildIndex();
        }
        catch (FormatException) {
            errorMessage.SetActive(true);
        }
    }

    //determine which team has the best score through bubble sort, based on the current map (not the average best score)
    //takes a parameter of which map is currently being played
    public void calculateBestTime(float[][] arrayPlayerData) {
        // for (int i = 0; i < numPlayers; ++i) {
        //     float bestTime = arrayPlayerData[i][playerData - 1];
        //     for (int j = i + 1; j < numPlayers; ++j) {
        //         if (arrayPlayerData[j][playerData - 1] < bestTime) {
        //             float temp = arrayPlayerData[i][playerData - 1];
        //             arrayPlayerData[i][playerData - 1] = arrayPlayerData[j][playerData - 1];
        //             arrayPlayerData[j][playerData - 1] = temp;
        //         }
        //     }

        //     float[] temp = arrayPlayerData[i];
        //     arrayPlayerData[i] = arrayPlayerData[bestPlayerIndex];
        //     arrayPlayerData[bestPlayerIndex] = temp;
        // }   
        for (int i = 0; i < numPlayers; ++i) {
        int bestPlayerIndex = i;
        for (int j = i + 1; j < numPlayers; ++j) {
            if (arrayPlayerData[j][playerData - 1] < arrayPlayerData[bestPlayerIndex][playerData - 1]) {
                bestPlayerIndex = j;
            }
        }
        
        float[] temp = arrayPlayerData[i];
        arrayPlayerData[i] = arrayPlayerData[bestPlayerIndex];
        arrayPlayerData[bestPlayerIndex] = temp;
    }   
    }


    //update arrayPlayerData
    public void updatePlayerData() {

    }

    //find highest score for each level
    public int highestScore(int levelIndex) {
        float bestScore = arrayPlayerData[0][levelIndex];
        int bestPlayerIndex = 0;
        for (int i = 1; i < numPlayers; ++i) {
            if (arrayPlayerData[i][levelIndex] < bestScore) {
                bestScore = arrayPlayerData[i][levelIndex];
                bestPlayerIndex = i;
            }
        }
        return bestPlayerIndex;
    }
}