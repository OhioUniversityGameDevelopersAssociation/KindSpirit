using UnityEngine;
using System.Collections;

public class GhoulMovement : ScriptedEnemyBehaviorAbstract {
    private int ghoulHealth = 10;
    private int amountOfShots = 0;
    private int roomWidth;
    private int roomLength;
	private float timeToWait;
    private float goalTime = 5f;
	private float lifeSpan = 2f;
	private float nextFire;
    private float targetX, targetY;
    private float minX;// = 1.5f;
    private float minY;// = -7.5f;
    private float maxX;// = 14.5f;
    private float maxY;// = -1.5f;
    private SpriteRenderer spriteR;
    public int attackPower;
	public Transform player;
	public Transform shotSpawn;
	public GameObject shot;

    new public virtual void Start() {
        spriteR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        roomWidth = transform.parent.GetComponent<RoomStats>().width;
        roomLength = transform.parent.GetComponent<RoomStats>().length;
        minX = 1.5f;
        maxY = -1.5f;
        maxX = (roomLength * 16) - 1.5f;
        minY = (roomWidth * -9) + 1.5f;

    }

	new public virtual void Update () {
        //Gets a random value for the ghoul to appear next
        targetX = Random.Range(minX, maxX);
		targetY = Random.Range(minY, maxY);

        //Waits for 5 seconds, then the ghoul will dissapear,
        //then reappears after another 5 seconds
        if (timeToWait > goalTime)
        {
            spriteR.enabled = false;
            
            if (timeToWait > goalTime + 5)
            {
                transform.position = new Vector2(targetX, targetY);
                if (transform.position.x < player.transform.position.x)
                {
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else if (transform.position.x > player.transform.position.x)
                {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }
                spriteR.enabled = true;
                timeToWait = 0f;
                amountOfShots = 0;
            }
        }
        else if (timeToWait > goalTime - 1)
        {
            if (amountOfShots < 1)
            {
                amountOfShots++;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
        }
            
        timeToWait += Time.deltaTime;
        //if enemy is hit then it loses life
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Decreases the players health
            other.gameObject.GetComponent<PlayerManager>().Damage(attackPower);
        }
    }

    //Destroys the ghoul
    void Kill()
    {
        ghoulHealth = 0;   
        Destroy(this.gameObject);
        Debug.Log("The Ghoul is dead");
    }

    //Takes away health if Ghoul is hit
    void Damage(int amountOfDamage)
    {
        ghoulHealth -= amountOfDamage;
        if (ghoulHealth <= 0)
        {
            Kill();
        }
    }
}
