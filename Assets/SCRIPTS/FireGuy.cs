using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireGuy : Killable {

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

    [SerializeField]
    private float SleepTime = 10;

    [SerializeField]
    private int ShotsPerAttack = 10;

    [SerializeField]
    private State currentState = State.Attack;

    [SerializeField]
    private int LevelNumber;

    private new Rigidbody2D rigidbody;
    private bool executingCo = false;



    //for attack
    private Vector3 target;
    private GameObject _player;
    private int rotator = 0;
    private int numProjectiles;
    private GameObject[] projectiles;
    private Rigidbody2D[] projectileRB;
    private Slider healthBar;
    private HealthController health;


    //sleep
    private Vector3 sleepPosition;


    

    private int i = 0;

    private enum State {
        Sleep, Attack
    }

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        target = FloatingWayPoint.position;
        _player = GameObject.FindGameObjectWithTag("Player");

        sleepPosition = transform.position;

        numProjectiles = 1;

        if (ProjectileLifeTime >= ShootCoolDown) {
            numProjectiles = (int) (ProjectileLifeTime / ShootCoolDown) + 1;
        }

        projectiles = new GameObject[numProjectiles];
        projectileRB = new Rigidbody2D[numProjectiles];

        for (int i = 0; i < numProjectiles; ++i) {
            GameObject fire = Instantiate(Projectile, transform.position, transform.rotation);
            projectiles[i] = fire;
            fire.SetActive(false);
            projectileRB[i] = fire.GetComponent<Rigidbody2D>();
        }


        health = GetComponent<HealthController>();
        healthBar = GetComponentInChildren<Slider>();
        healthBar.value = health.getCurrentHealth();
    }

    void OnEnable() {
        currentState = State.Attack;
        executingCo = false;

        
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        
        if(health != null) {
            health.setHealth(health.getMaxHealth());
            healthBar.value = health.getCurrentHealth();
        }

        i = 0;
    }

    void Update() {
        if (!executingCo) {
            executingCo = true;

            switch (currentState) {
                case State.Attack:
                    StartCoroutine("AttackCoroutine");
                    break;

                case State.Sleep:
                    StartCoroutine("SleepCoroutine");
                    break;

                default:
                    Debug.Log("State Not In Switch Statement");
                    break;


            }
            

        }

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        switch (currentState) {
            case State.Attack:
                AttackMovement();
                break;

            case State.Sleep:
                SleepMovement();
                break;

            default:
                Debug.Log("State Not In Switch Statement");
                break;


        }
        
	}

    public IEnumerator AttackCoroutine() {

        yield return new WaitForSeconds(ShootCoolDown);

        GameObject fire = projectiles[rotator];
        
        Rigidbody2D fireRB = projectileRB[rotator];
        rotator = (rotator + 1) % numProjectiles;



        Vector3 direction = (_player.transform.position - transform.position).normalized * (10 * ProjectileSpeed);
        fire.transform.position = transform.position;
        fire.SetActive(true);
        fireRB.velocity = direction;

        ++i;

        if (i > ShotsPerAttack) {

            currentState = State.Sleep;
            i = 0;
            
        }

        executingCo = false;
    }

    public void AttackMovement() {

        if (Mathf.Abs(target.y - transform.position.y) < .1) {
            target = new Vector3(UnityEngine.Random.Range(FloatingWayPoint.position.x - 2, FloatingWayPoint.position.x + 2), UnityEngine.Random.Range(FloatingWayPoint.position.y - 2, FloatingWayPoint.position.y + 2), transform.position.z);
            
        } else {
            rigidbody.MovePosition(transform.position + (target - transform.position).normalized * (MoveSpeed * Time.deltaTime));
            
        }

    }

    public IEnumerator SleepCoroutine() {

        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<SpriteRenderer>().color = new Color(82f/255, 78f/255, 78f/255, 255/255);

        yield return new WaitForSeconds(SleepTime);
        currentState = State.Attack;
        executingCo = false;

        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void SleepMovement() {

        if ((Mathf.Abs(transform.position.x - sleepPosition.x) > .1) || (Mathf.Abs(transform.position.y - sleepPosition.y) > .1)) {
            rigidbody.MovePosition(transform.position + (sleepPosition - transform.position).normalized * (MoveSpeed * Time.deltaTime));

        }
        
    }

    public override void killed() {
        gameObject.SetActive(false);

        GameObject.FindObjectOfType<LevelManager>().GetComponent<LevelManager>().EndLevel(LevelNumber);
    }



    public override void healthChanged() {
        healthBar.value = (float)health.getCurrentHealth()/health.getMaxHealth();
    }
}
