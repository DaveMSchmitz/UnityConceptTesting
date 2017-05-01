using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour {

    private Text coinText;
    // Use this for initialization
    void Awake () {
        coinText = GetComponent<Text>();
        ChangeText(0);
	}
	
    public void ChangeText(int coins) {
        coinText.text = "" + coins;
    }

}
