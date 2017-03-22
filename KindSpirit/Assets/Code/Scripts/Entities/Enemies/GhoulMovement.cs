using UnityEngine;
using System.Collections;

public class GhoulMovement : ScriptedEnemyBehaviorAbstract {
	private float rand1, rand2;
    private int ghoulHealth = 10;
	private float timeToWait;
    private float goalTime = 5f;
	private float lifeSpan = 2f;
	//private float nextFire;
    private SpriteRenderer spriteR;
	//public Transform player;
    //public GameObject enemyBoundry;
    //public float projectileSpeed;
	//public float fireRate;
	//public Transform shotSpawn;
	//public GameObject shot;

    new public virtual void Start() {
        //Vector2 dir;

        spriteR = GetComponent<SpriteRenderer>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        //Determines the direction the projectile will travel
        //dir = player.position - transform.position;

        //Moves the projectile in the position of the players position
        //GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }

	new public virtual void Update () {
        rand1 = Random.Range(1.5f, 14.5f);
		rand2 = Random.Range(-7.5f, -1.5f);
        if (timeToWait > goalTime)
        {
           /* if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

                //plays the audio of the projectile being fired
                GetComponent<AudioSource>().Play();

                //Destroys the projectile after its lifespan
                Destroy(this.gameObject, lifeSpan);
            }*/
            spriteR.enabled = false;
            
            if (timeToWait > goalTime + 5)
            {
                transform.position = new Vector2(rand1, rand2);
                spriteR.enabled = true;
                timeToWait = 0f;
            }
        }
            
        timeToWait += Time.deltaTime;
        //if enemy is hit then it loses life
	}

    void Kill()
    {
        Destroy(this.gameObject);
    }
}
