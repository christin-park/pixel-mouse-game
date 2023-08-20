using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class selectLevel : MonoBehaviour {
    public readPlayerData readPlayerData;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public TextMeshProUGUI[] textArray;

    public static int level;

    void Start() {
        button1.onClick.AddListener(playMaze1);
        button2.onClick.AddListener(playMaze2);
        button3.onClick.AddListener(playMaze3);
        button4.onClick.AddListener(goBack);

        updateAllText();
    }

    public void playMaze1() {
        SceneManager.LoadScene(3);
        level = 1;
    }

    public void playMaze2() {
        SceneManager.LoadScene(4);
        level = 2;
    }

    public void playMaze3() {
        SceneManager.LoadScene(5);
        level = 3;
    }

    public void goBack() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void updateAllText() {
        for (int i = 0; i < 3; ++i) {
            int indexBest = readPlayerData.highestScore(i);
            string bestPlayer = readPlayerData.playerNames[indexBest];
            float bestTime = readPlayerData.arrayPlayerData[indexBest][i];
            textArray[i].text = "Best time : " + bestPlayer + ", " + bestTime.ToString() + "s";
        }
        
    }

}
