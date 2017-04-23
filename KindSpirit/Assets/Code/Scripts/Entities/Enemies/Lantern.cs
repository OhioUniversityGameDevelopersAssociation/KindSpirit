using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	public GameObject fireball;
	public float rangedShotForceMultiplier = 1.0f;
	public int attackPower = 1; //change for scary lantern
	public bool IsSpooky = false;
	int roomwidth;
	int roomlength;
	float roomMinxBoundary;
	float roomMinyBoundary;
	float roomMaxxBoundary;  
	float roomMaxyBoundary; 
	float targetxcoord;
	float targetycoord;
	bool logPositionThisFrame = true;
	public float speed;
	Vector3 target;

	public void Awake()
	{
		roomwidth = transform.parent.GetComponent<RoomStats> ().width;
		roomlength = transform.parent.GetComponent<RoomStats> ().length;
		roomMinxBoundary = 1.5f;
		roomMinyBoundary = -1.5f;
		roomMaxxBoundary = (roomwidth*16) - 1.5f;
		roomMaxyBoundary = (roomwidth*-9) + 1.5f;
		target = new Vector3(targetxcoord, targetycoord, 0);
	}


	public void Update()
	{
		targetxcoord = Random.Range (roomMinxBoundary, roomMaxxBoundary);
		targetycoord = Random.Range (roomMinyBoundary, roomMaxyBoundary);

		Vector3 previousPos = transform.position;
		transform.position += (target - transform.position).normalized * speed * Time.deltaTime;

		if (Vector3.Distance (target, transform.position) <= .5) {
			target = new Vector3(targetxcoord, targetycoord, 0);
		}
		//log position every other frame
		if (logPositionThisFrame == true) {
			logPositionThisFrame = false;
		}
		else if(logPositionThisFrame == false){
			logPositionThisFrame = true;
		}

		if (logPositionThisFrame) {
			previousPos = transform.position;
		} 
		else
		{
			//face direction it is moving
			if(GreaterXdirection(gameObject.transform.position,previousPos))
			{
				transform.localRotation = Quaternion.Euler(0, 180, 0); //old code-woud NOT work for player controls, would flip all the directions
			}
			else if (GreaterXdirection (gameObject.transform.position, previousPos) == false) {
				transform.localRotation = Quaternion.Euler(0, 0, 0);
			}
		}

	}

	public bool GreaterXdirection(Vector3 a, Vector3 b) {
		return (a.x > b.x);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && other.isTrigger == true) {
			InstantiateFireball (0, 1);
			InstantiateFireball (1, 0);
			InstantiateFireball (-1, 0);
			InstantiateFireball (0, -1);

			if (IsSpooky == true) {
				InstantiateFireball (.5f, .5f);
				InstantiateFireball (-.5f, -.5f);
				InstantiateFireball (-.5f, .5f);
				InstantiateFireball (.5f, -.5f);
			}
		}
	}

	void InstantiateFireball(float xcoord, float ycoord)
	{
		Vector2 dir = new Vector2(xcoord, ycoord);
		Vector3 placementVector = new Vector3(transform.position.x + xcoord*.1f, transform.position.y +ycoord*.1f, 0f);
		RangedAttackBox rangedAttackInstance = Instantiate(fireball, placementVector, Quaternion.identity).GetComponent<RangedAttackBox>();
		rangedAttackInstance.SetAttackPower(attackPower);
		rangedAttackInstance.SetLifeSpan(3);
		rangedAttackInstance.SetIgnoreCollisions (this.tag);
		rangedAttackInstance.GetComponent<Rigidbody2D>().velocity = dir * rangedShotForceMultiplier;
	}
}
