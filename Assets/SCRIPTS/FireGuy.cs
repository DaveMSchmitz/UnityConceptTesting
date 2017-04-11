using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGuy : MonoBehaviour {

    [SerializeField]
    private Transform FloatingWayPoint;

    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private float ProjectileSpeed = 3;

    [SerializeField]
    private float ShootCoolDown = 3;

    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private float ProjectileLifeTime = 2;

    private new Rigidbody2D rigidbody;
    private Vector3 target;
    private bool executingCo = false;
    private GameObject _player;
    private int rotator = 0;
    private int numProjectiles;
    private GameObject[] projectiles;
    private Rigidbody2D[] projectileRB;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        target = FloatingWayPoint.position;
        _player = GameObject.FindGameObjectWithTag("Player");

        numProjectiles = 1;

        if (ProjectileLifeTime >= ShootCoolDown) {
            numProjectiles = (int) (ProjectileLifeTime / ShootCoolDown) + 1;
        }

        projectiles = new GameObject[numProjectiles];
        projectileRB = new Rigidbody2D[numProjectiles];

        for (int i = 0; i < numProjectiles; ++i) {
            GameObject fire = Instantiate(Projectile, transform.position, transform.rotation);
            projectiles[i] = fire;
            projectileRB[i] = fire.GetComponent<Rigidbody2D>();
        }


    }
    void Update() {
        if (!executingCo) {
            executingCo = true;

            StartCoroutine("shootCo");

        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Mathf.Abs(target.y - transform.position.y) < .1) {
            target = new Vector3(Random.Range(FloatingWayPoint.position.x - 2, FloatingWayPoint.position.x + 2), Random.Range(FloatingWayPoint.position.y - 2, FloatingWayPoint.position.y + 2), transform.position.z);
        } else {
            rigidbody.MovePosition(transform.position + (target - transform.position).normalized * (MoveSpeed  * Time.deltaTime));
        }

        
	}

    public IEnumerator shootCo() {

        GameObject fire = projectiles[rotator];
        Rigidbody2D fireRB = projectileRB[rotator];
        rotator = (rotator + 1) % numProjectiles;
        
        

        Vector3 direction = (_player.transform.position - transform.position).normalized * (10 * ProjectileSpeed);
        
        fireRB.velocity = direction;
        fireRB.position = transform.position;



        yield return new WaitForSeconds(ShootCoolDown);

        executingCo = false;
    }
}
