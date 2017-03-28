using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWhenRespawn : MonoBehaviour {

    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _scale;

	// Use this for initialization
	void Start () {
        _position = transform.position;
        _rotation = transform.rotation;
        _scale = transform.localScale;
	}
	
    public void ResetGameObject() {
        
        transform.position = _position;
        transform.rotation = _rotation;
        transform.localScale = _scale;
        
    }
}
