using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ZombieManager : MonoBehaviour {

	public LayerMask blockingLayer;         //Layer on which collision will be checked.

	private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.

	public int playerDamage;                            //The amount of food points to subtract from the player when attacking.

	bool logPositionThisFrame = true;
	public float speed = .7f;
	private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
	private Transform target;                           //Transform to attempt to move toward each turn.
	public bool goRight;
	public bool goUp;
	float moveDistance = 10f; //how far the zombie moves during each update
	Vector2 newPos;
	float aggroRange = 5f; //how close the player must be in order for zombie to start following them
	bool aggro = false; //true if zombie is aggroed and following player


	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start ()
	{
		//Get a component reference to this object's BoxCollider2D
		boxCollider = GetComponent <BoxCollider2D> ();

		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent <Rigidbody2D> ();

			//Get and store a reference to the attached Animator component.
			animator = GetComponent<Animator> ();

			//Find the Player GameObject using it's tag and store a reference to its transform component.
			target = GameObject.FindGameObjectWithTag ("Player").transform;


		}
	void testabegridsys()
	{
		bool obstacleishereyo = transform.parent.gameObject.GetComponent <RoomStats>().IsObstacle (5,5);
		Debug.Log (obstacleishereyo);
		//to "place object" manually change the grid zeros to ones
		Vector2 coordinates = transform.parent.gameObject.GetComponent <RoomStats>().GetCoordinates (this.gameObject);
		Debug.Log (coordinates.x.ToString() + "  , "+coordinates.y.ToString() );
	}
	void MoveinGrid()
	{
		/*
		 Vector2 vec = target.position.x - transform.position.x;
		 if(vec.magnitude <= .9f)
		 //dont move, zombie has arrived at player location */
		if (target.position.x < transform.position.x) {
			Debug.Log ("player is to the left");
			goRight = false;
		} else if (target.position.x > transform.position.x) {
			Debug.Log ("player is to the right");
			goRight = true;
		} 
		Vector2 vec = target.position - transform.position;

		//if (vec.x <= .9f) {
		//player is in line with zombie, based on x/y pos
		//go up/down

		if (target.position.y < transform.position.y) {
			Debug.Log ("player is below");
			goUp = false;
		} else if (target.position.y > transform.position.y) {
			Debug.Log ("player is above");
			goUp = true;
		} 
		//Try to go up or down, based on player location 
		if (goUp) {
			//check to see if there is obstacle in that square
			newPos.Set (transform.position.x, transform.position.y + moveDistance);
			rb2D.MovePosition (newPos); //move one square up
		} else { //go left
			newPos.Set (transform.position.x, transform.position.y - moveDistance);
			rb2D.MovePosition (newPos); //move one square down
		}
			
		//Try to go left or right, based on player location 
		if (goRight) {
			//check to see if there is obstacle in that square
			newPos.Set (transform.position.x + moveDistance, transform.position.y);
			rb2D.MovePosition (newPos); //move one square to the right
		} else { //go left
			newPos.Set (transform.position.x - moveDistance, transform.position.y);
			rb2D.MovePosition (newPos); //move one square to the left
		}
	}
	void Update()
	{
		//MoveinGrid ();
		CheckAggro ();
		if (aggro == true) {
			MoveTowardsPlayer ();
		} else {
			Wander ();
		}
		testabegridsys ();
	}
	public void Wander()
	{
		/*System.Random rnd = new System.Random ();
		int dist = rnd.Next (-5, 5);
		float d = (float)dist * .01f;
		Random.value() //gets number between 0 and 1
		//Vector2 randomPos = new Vector2 ();
		newPos.Set (transform.position.x+ d, transform.position.y + d);
		rb2D.MovePosition (newPos); //move randomly

		RaycastHit2D hit = Physics2D.Raycast (transform.position,(dist,dist));
		*/
	}
	public void CheckAggro()
	{
		//check to see if player is within aggro range
		float d = Vector2.Distance(rb2D.transform.position, target.position);
		if (d < aggroRange) {
			aggro = true;
		} else {
			aggro = false;
		}
	
	}
	public void MoveTowardsPlayer(){
		//adapted from immune system td code

		Vector3 previousPos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast (transform.position,target.position);
		if (hit != null) {
			//there is an obstacle between zombie and player
		}
		transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
		//log position every other frame
		if (logPositionThisFrame == true) {
			logPositionThisFrame = false;
		}
		else if(logPositionThisFrame == false){
			logPositionThisFrame = true;
		}

		if (logPositionThisFrame) {
			//log position
			previousPos = transform.position;
		} 
		else
		{
			//face direction it is moving
			//if(moved To the left)
			if(GreaterXdirection(gameObject.transform.position,previousPos))
			{
				//sprite has moved to the right
				//flip so it is facing to the right
				//this.gameObject.GetComponent<SpriteRenderer>().flipX = !this.gameObject.GetComponent<SpriteRenderer>().flipX; 
				transform.localRotation = Quaternion.Euler(0, 180, 0); //old code-woud NOT work for player controls, would flip all the directions
				//xrotation, yrotation, zrotation
			}
			else if (GreaterXdirection (gameObject.transform.position, previousPos) == false) {
				//sprite has moved to the left
				//flip so it is facing to the left
				transform.localRotation = Quaternion.Euler(0, 0, 0);
				//this.gameObject.GetComponent<SpriteRenderer>().flipX = !this.gameObject.GetComponent<SpriteRenderer>().flipX; 
				//xrotation, yrotation, zrotation
			}
		}

	}
	//Move returns true if it is able to move and false if not. 
	//Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
	//protected bool Move (int xDir, int yDir, out RaycastHit2D hit)

	public bool GreaterXdirection(Vector3 a, Vector3 b) {
		return (a.x > b.x);
	}
	public void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Environment") 
		{
			//move around the obstacle
			Debug.Log("avoided obstacle");
			//if (coll.gameObject.transform.position.x > rb2D.position.x)
			//{ //obstacle is to the right
				if (target.position.y < transform.position.y) //player is below
				{
					newPos.Set (transform.position.x, transform.position.y - moveDistance);
					rb2D.MovePosition (newPos); //move one square down
				Debug.Log("avoided obstacle down");
				}else if(target.position.y > transform.position.y) //player is above
				{
					newPos.Set (transform.position.x, transform.position.y + moveDistance);
					rb2D.MovePosition (newPos); //move one square up
				Debug.Log("avoided obstacle up");
				}else //player is at same y coordinate as zombie
				{
					newPos.Set (transform.position.x, transform.position.y + moveDistance);
					rb2D.MovePosition (newPos); //move one square up
				Debug.Log("avoided obstacle up random");
				}
					
			//}
		}
	}


		
		//OnCantMove

}
