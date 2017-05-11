using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnFire : MonoBehaviour {

    [SerializeField]
    private int LevelNumber;


    void OnTriggerEnter2D(Collider2D obj) {

        if (obj.tag == "Player") {
            StartCoroutine("EndLevel");


        }
    }

    public IEnumerator EndLevel() {
        Camera.main.GetComponent<Animator>().SetTrigger("EndLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelNumber);
    }

}
