using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : Killable {

    [SerializeField]
    private Transform FloatingWayPoint;


    [SerializeField]
    private float ProjectileSpeed = 3;

    [SerializeField]
    private float ShootCoolDown = 3;

    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private float ProjectileLifeTime = 2;


    [SerializeField]
    private State currentState = State.Attack;

    [SerializeField]
    private int LevelNumber;

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
    void Start() {

        target = FloatingWayPoint.position;
        _player = GameObject.FindGameObjectWithTag("Player");

        numProjectiles = 1;

        if (ProjectileLifeTime >= ShootCoolDown) {
            numProjectiles = (int)(ProjectileLifeTime / ShootCoolDown) + 1;
        }

        projectiles = new GameObject[numProjectiles];
        projectileRB = new Rigidbody2D[numProjectiles];

        for (int i = 0; i < numProjectiles; ++i) {
            GameObject fire = Instantiate(Projectile, FloatingWayPoint.position, transform.rotation);
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


        if (health != null) {
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

                default:
                    Debug.Log("State Not In Switch Statement");
                    break;


            }


        }

    }


    public IEnumerator AttackCoroutine() {

        yield return new WaitForSeconds(ShootCoolDown);

        GameObject fire = projectiles[rotator];

        Rigidbody2D fireRB = projectileRB[rotator];
        rotator = (rotator + 1) % numProjectiles;



        Vector3 direction = (_player.transform.position - FloatingWayPoint.position).normalized * (10 * ProjectileSpeed);
        fire.transform.position = FloatingWayPoint.position;
        fire.SetActive(true);
        fireRB.velocity = direction;

        ++i;

        executingCo = false;
    }

    public override void killed() {
        gameObject.SetActive(false);

        GameObject.FindObjectOfType<LevelManager>().GetComponent<LevelManager>().EndLevel(LevelNumber);
    }



    public override void healthChanged() {
        healthBar.value = (float)health.getCurrentHealth() / health.getMaxHealth();
    }
}
