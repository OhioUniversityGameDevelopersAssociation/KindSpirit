using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour {
	//public Transform messageprefabtransform;
	public GameObject messageprefab;

	void Start(){
		//messageprefabtransform = this.gameObject.transform.GetChild (0);
		//messageprefab = this.gameObject.transform.GetChild (0).GetChild (0).gameObject;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			Debug.Log ("Player touched object");
			Instantiate (messageprefab); //set text somehow
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		// Destroy message
		Debug.Log ("someone exited collider");
		if (other.tag == "Player") 
		{
			Debug.Log ("player exited collider");
			GameObject[] messages = GameObject.FindGameObjectsWithTag ("Message"); //Destroy (messageprefab);
			foreach (GameObject o in messages) {
				Destroy (o);
			}
		}
	}
}
