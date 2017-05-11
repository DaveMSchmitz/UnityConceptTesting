using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private float RespawnDelay;

    [SerializeField]
    private bool FirstLevel = false;

    private HealthText healthText;
    //TODO make a getter
    public int coinCount = 0;
    private CoinText coinText;
    private PlayerController player;
    private HealthController _playerHealth;
    private ResetWhenRespawn[] _objectsToRespawn;
    private int levelNumber;

    // Use this for initialization
    void Start() {
        Debug.Log("In Start");
        player = FindObjectOfType<PlayerController>();
        coinText = FindObjectOfType<CoinText>();
        healthText = FindObjectOfType<HealthText>();
        _objectsToRespawn = FindObjectsOfType<ResetWhenRespawn>();

        _playerHealth = player.gameObject.GetComponent<HealthController>();
        coinCount = PlayerPrefs.GetInt("Coins");

        healthText.ChangeText(_playerHealth.getCurrentHealth());
        coinText.ChangeText(coinCount);

        if (FirstLevel) {
            PlayerPrefs.SetInt("Coins", 0);
        }

        coinCount = PlayerPrefs.GetInt("Coins");
    }

    public void Respawn() {
        StartCoroutine("RespawnCoroutine");
    }

    public IEnumerator RespawnCoroutine() {

        //make the player disappear
        player.gameObject.SetActive(false);

        foreach (ResetWhenRespawn obj in _objectsToRespawn) {
            obj.gameObject.SetActive(false);

        }

        //wait for the delay
        yield return new WaitForSeconds(RespawnDelay);

        //reset all of the objects that said they needed to be reset if the player dies
        foreach (ResetWhenRespawn obj in _objectsToRespawn) {
            obj.gameObject.SetActive(true);
            obj.ResetGameObject();
        }

        //respawn the player and make them active again
        player.transform.position = player.RespawnTransform;
        player.gameObject.SetActive(true);

        //reset the player health
        _playerHealth.setHealth(_playerHealth.getMaxHealth());

    }

    //change the coin amount
    public void changeCoins(int coins) {
        coinCount += coins;
        PlayerPrefs.SetInt("Coins", coinCount);
        coinText.ChangeText(coinCount);
    }

    //this function gets subscribed on the enabled 
    public void changeHealthText() {
        healthText.ChangeText(_playerHealth.getCurrentHealth());
    }

    public void EndLevel(int levelNumber) {
        this.levelNumber = levelNumber;
        StartCoroutine("EndScene");
    }

    private IEnumerator EndScene() {

        yield return new WaitForSeconds(1);
        Camera.main.GetComponent<Animator>().SetTrigger("EndLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelNumber);
    }
}
