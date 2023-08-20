using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public playerMovement playerMovement;
    public playerCollision playerCollision;
    private TextMeshPro timerText;
    
    void Start() {
        timerText = GetComponent<TextMeshPro>();
    }

    void Update() {
        if (!playerCollision.cheeseTouch) {
            timerText.text = playerMovement.timer.ToString("F1");
        }
    }
}
