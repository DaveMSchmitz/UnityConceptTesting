using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MovementController{

	public float JumpSensorRadius;
	public GameObject JumpSensor;
	public GameObject PlayerSensor;

	private AIController ai;

	public override void setUp (){
		ai = PlayerSensor.GetComponent<AIController> ();

	}

	public override float getMovement(){


		return ai.GetMovement();
	}

	public override bool getJump(){
	
		return Physics2D.OverlapCircle(JumpSensor.transform.position, JumpSensorRadius, ConsideredGround);
	}


}

