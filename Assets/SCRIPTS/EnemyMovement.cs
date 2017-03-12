using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class EnemyMovement : MovementController{

	private GameObject _focus = null;

	private Transform _jumpSensorRight;
	private Transform _jumpSensorLeft;

	public override void setUp (){
		_jumpSensorRight = GameObject.Find ("JumpSensorRight").transform;
		_jumpSensorLeft = GameObject.Find ("JumpSensorLeft").transform;
	}

	public override float getMovement(){
		float horizontal = 0;

		if(_focus != null){
			horizontal = Mathf.Sign (_focus.transform.position.x - transform.position.x);
		}

		return horizontal;
	}

	public override bool getJump(){


		return Physics2D.OverlapCircle(_jumpSensorRight.position, .3f, ConsideredGround) || Physics2D.OverlapCircle(_jumpSensorLeft.position, .3f, ConsideredGround);;
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

