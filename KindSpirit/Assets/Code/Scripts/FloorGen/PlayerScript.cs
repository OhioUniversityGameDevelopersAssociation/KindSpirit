using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float speed;
	public GameObject current_room;
	GameObject cam;
	FloorPlan plan;
	public bool act;
	
	void Start(){
		current_room = GameObject.Find("starting_room");
		cam = GameObject.Find("Main Camera");
		plan = current_room.GetComponent<FloorPlan>();
	}

	void FixedUpdate(){
		if(act){
			if (Input.GetKey ("w")) {transform.Translate (Vector3.up * Time.fixedDeltaTime * speed);}
			if (Input.GetKey ("s")) {transform.Translate (Vector3.down * Time.fixedDeltaTime * speed);}
			if (Input.GetKey ("a")) {transform.Translate (Vector3.left * Time.fixedDeltaTime * speed);}
			if (Input.GetKey ("d")) {transform.Translate (Vector3.right * Time.fixedDeltaTime * speed);}
		}
	}
	
	
	
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "wall"){
			current_room = other.gameObject.transform.parent.gameObject;
			cam.GetComponent<camscript>().current_room = current_room;
			cam.GetComponent<camscript>().SetStats();
		}
	}
	
	
	void EnableRoom(){
		
	}
	
	void DisableRooms(){
		
	}
	
	
}
