using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public Bounds PlayerSensor;
    public LayerMask ConsideredGround;
    public LayerMask ConsideredPlayer;
    public GameObject JumpSensor;
    public float JumpSensorRadius;


    private GameObject _player;
	
	private float _horizontal;
    
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

	public float GetMovementFromPlayer(){

        PlayerSensor.center = transform.position;
        _horizontal = 0;

        if (Mathf.Abs(_player.transform.position.x - transform.position.x) < PlayerSensor.extents.x) {
            _horizontal = Mathf.Sign(_player.transform.position.x - transform.position.x);


        }
        return _horizontal;
	}

    public bool GetJump() {
        return Physics2D.OverlapCircle(JumpSensor.transform.position, JumpSensorRadius, ConsideredGround);
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(0,0,255, .1f);
        Gizmos.DrawCube(PlayerSensor.center, PlayerSensor.size);
    }
}
