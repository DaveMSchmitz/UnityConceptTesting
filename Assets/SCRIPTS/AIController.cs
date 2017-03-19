using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

	private GameObject _focus;
	private float _horizontal;
    
	// Update is called once per frame
	void Update () {
		_horizontal = 0;

		if(_focus != null){
			_horizontal = Mathf.Sign (_focus.transform.position.x - transform.position.x);
			
		}
	}

	public float GetMovement(){

		return _horizontal;
	}

	public void OnTriggerEnter2D(Collider2D col){
        
		if(col.gameObject.layer == LayerMask.NameToLayer("Player")){
            
            _focus = col.gameObject;
		}
	}

	public void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			_focus = null;
		}
	}

    public void foo()
    {
        Debug.Log("Inside Foo");
    }
}
