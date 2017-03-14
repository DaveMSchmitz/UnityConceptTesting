using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class EnemyMovement : MovementController{

	public float JumpSensorRadius;
	public GameObject JumpSensor;

	private GameObject _focus = null;


	public override void setUp (){
		

	}

	public override float getMovement(){
		float horizontal = 0;

		if(_focus != null){
			horizontal = Mathf.Sign (_focus.transform.position.x - transform.position.x);
		}

		return horizontal;
	}

	public override bool getJump(){
	
		return Physics2D.OverlapCircle(JumpSensor.transform.position, JumpSensorRadius, ConsideredGround);
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player"){
			_focus = col.gameObject;
		}
	}

	public void OnTriggerExit2D(Collider2D col){
		if(col.tag == "Player"){
			_focus = null;
		}
	}
}

