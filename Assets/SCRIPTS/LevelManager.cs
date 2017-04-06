using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float RespawnDelay;
    public Text coinText;
    public Text healthText;

    public int coinCount = 0;

    private PlayerController player;
    private HealthController _playerHealth;
    private ResetWhenRespawn[] _objectsToRespawn;
    

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        _playerHealth = player.gameObject.GetComponent<HealthController>();
        healthText.text = "" + _playerHealth.getCurrentHealth();

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

        //_playerHealth.changeHealth(-2);
		_playerHealth.setHealth(_playerHealth.getMaxHealth());

    }

    //change the coin amount
    public void changeCoins(int coins) {
        coinCount += coins;
        coinText.text = "" + coinCount;
    }

    //this function gets subscribed on the enabled 
    public void changeHealthText() {
        healthText.text = "" + _playerHealth.getCurrentHealth();
    }
}
