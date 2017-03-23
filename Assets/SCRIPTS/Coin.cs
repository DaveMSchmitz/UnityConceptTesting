using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int CoinValue = 1;

    private LevelManager _levelManager;

	// Use this for initialization
	void Start () {
        _levelManager = FindObjectOfType<LevelManager>();
	}
	

    void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Player")) {
            gameObject.SetActive(false);
            _levelManager.changeCoins(CoinValue);
        }
    }
}
