using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	public GameObject fireball;
	public float rangedShotForceMultiplier = 1.0f;
	public int attackPower = 1; //change for scary lantern
	public bool IsSpooky = false;

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
