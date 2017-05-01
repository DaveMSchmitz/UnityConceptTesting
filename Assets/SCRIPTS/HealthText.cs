using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour {

    private Text healthText;
    // Use this for initialization
    void Awake() {
        healthText = GetComponent<Text>();
        ChangeText(0);
    }

    public void ChangeText(int health) {
        healthText.text = "" + health;
    }
}
