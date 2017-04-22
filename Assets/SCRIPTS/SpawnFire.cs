using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnFire : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D obj) {

        if (obj.tag == "Player") {
            SceneManager.LoadScene(2);

        }
    }
}
