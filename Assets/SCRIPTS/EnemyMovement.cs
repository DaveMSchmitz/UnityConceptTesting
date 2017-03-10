using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class EnemyMovement : MonoBehaviour
{

    private GameObject _focus;

	// Use this for initialization
	void Start () {
		_focus = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (_focus != null) {
			transform.position = _focus.transform.position;
		}
	}

    void OnTriggerEnter2D(Collider2D collider){
		if (collider.CompareTag("Player")) {
			_focus = collider.gameObject;
		
		
		}

    }
}
