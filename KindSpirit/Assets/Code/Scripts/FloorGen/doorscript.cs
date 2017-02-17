using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour {

	SpriteRenderer rend;
	BoxCollider2D body;
	public int dir_state= 1;
	GameObject cam;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<SpriteRenderer>();
		body = gameObject.GetComponent<BoxCollider2D>();
		gameObject.tag = "wall";
		cam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		if(rend.sprite == null){
			body.isTrigger = false;
		}
		else{
			body.isTrigger = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(rend.sprite != null && other.gameObject.tag == "Player"){
			if(other.gameObject.GetComponent<PlayerManager>().PlayerControlEnabled){
				other.gameObject.GetComponent<PlayerManager>().DisablePlayerControl();
				GameObject.Find("Main Camera").GetComponent<camscript>().state = dir_state;
				//other.gameObject.GetComponent<Rigidbody2D>().simulated = false;
			}
			else if(GameObject.Find("Main Camera").GetComponent<camscript>().state!=dir_state){
				cam.GetComponent<camscript>().current_room = transform.parent.gameObject;
				cam.GetComponent<camscript>().SetStats();
			}
		}
	}
}
