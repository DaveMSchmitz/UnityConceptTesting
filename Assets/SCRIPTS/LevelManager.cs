using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float RespawnDelay;
    public Text coinText;

    public int coinCount = 0;

    private PlayerController player;
    private ResetWhenRespawn[] _objectsToRespawn;
    

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        coinText.text = "" + coinCount;

        _objectsToRespawn = FindObjectsOfType<ResetWhenRespawn>();

	}
	

    public void Respawn() {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine() {

        //make the player disappear
        player.gameObject.SetActive(false);

        //wait for the delay
        yield return new WaitForSeconds(RespawnDelay);

        //reset all of the objects that said they needed to be reset if the player dies
        foreach (ResetWhenRespawn obj in _objectsToRespawn) {
            obj.gameObject.SetActive(true);
            obj.ResetGameObject();
        }

        //reset the number of coins that you have
        changeCoins(-coinCount);

        //respawn the player and make them active again
        player.transform.position = player.RespawnTransform;
        player.gameObject.SetActive(true);

    }

    //change the coin amount
    public void changeCoins(int coins) {
        coinCount += coins;
        coinText.text = "" + coinCount;
    }
}
