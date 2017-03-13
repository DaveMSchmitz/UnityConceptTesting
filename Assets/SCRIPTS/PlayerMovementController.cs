using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController {

	public override void setUp(){}

	public override float getMovement(){
		return Input.GetAxisRaw("Horizontal");
	}

	public override bool getJump(){
		return Input.GetButtonDown ("Jump");
	}

}
