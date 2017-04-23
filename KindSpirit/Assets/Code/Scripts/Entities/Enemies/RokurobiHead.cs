using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RokurobiHead : MonoBehaviour {
	private Transform target;                           //Transform to attempt to move toward each turn.
	public float speed = .7f;
	bool logPositionThisFrame = true;
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsPlayer ();
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
}
