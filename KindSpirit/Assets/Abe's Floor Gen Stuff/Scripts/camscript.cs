using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camscript : MonoBehaviour {

	public GameObject current_room;
	public GameObject anton;
	public float boundary = 2.5f;
	RoomStats rm;
	public int state;
	
	
	int pause_timer;
	int move_timer;
	float spd = 1.5f;
	
	
	// Use this for initialization
	void Start () {
		current_room = GameObject.Find("starting_room");
		SetStats();
		anton = GameObject.Find("anton");
		state = 1;
	}
	
	public void SetStats(){
		rm = current_room.GetComponent<RoomStats>();
	}
	
	
	void FixedUpdate(){
		//State 1, follow player, don't go past boundaries
		if(state == 1){
			transform.position = anton.transform.position;
			float new_x = transform.position.x;
			float new_y = transform.position.y;
			float bound = boundary;
			//Make sure camera doesn't leave left boundary
			if(transform.position.x < (current_room.transform.position.x+boundary)){new_x = current_room.transform.position.x+boundary;}
			//Make sure camera doesn't leave right boundary
			else{
				bound = boundary;
				if(rm.width == 1){bound = boundary;}
				else{bound = boundary*((rm.width*2)-1);}
				if(transform.position.x > (current_room.transform.position.x+bound)){new_x = current_room.transform.position.x+bound;}
			}
			//Make sure camera doesn't leave upper boundary
			if(transform.position.y > (current_room.transform.position.y-boundary)){new_y = current_room.transform.position.y-boundary;}
			//Make sure camera doesn't leave lower boundary
			else{
				bound = boundary;
				if(rm.length == 1){bound = -1*boundary;}
				else{bound = -1*boundary*((rm.length*2)-1);}
				if(transform.position.y < (current_room.transform.position.y+bound)){new_y = current_room.transform.position.y+bound;}
			}
			transform.position = new Vector3(new_x,new_y, -10f);
		}
		//State 2, pause and go to the right
		else if(state == 2){
			if(pause_timer < 3){pause_timer += 1;}
			else{
				if(move_timer < 50){
					move_timer += 1;
					transform.Translate(Vector3.right*spd*3.2f*Time.deltaTime);
					anton.transform.Translate(Vector3.right*spd*0.8f*Time.deltaTime);
				}
				else{
					state = 1;
					pause_timer = 0;
					move_timer = 0;
					anton.GetComponent<PlayerScript>().act = true;
				}
			}
		}
		//State 3, pause and go down
		else if(state == 3){
			if(pause_timer < 3){pause_timer += 1;}
			else{
				if(move_timer < 50){
					move_timer += 1;
					transform.Translate(Vector3.down*spd*3.2f*Time.deltaTime);
					anton.transform.Translate(Vector3.down*spd*0.8f*Time.deltaTime);
				}
				else{
					state = 1;
					pause_timer = 0;
					move_timer = 0;
					anton.GetComponent<PlayerScript>().act = true;
				}
			}
		}
		//State 4, pause and go to the left
		else if(state == 4){
			if(pause_timer < 3){pause_timer += 1;}
			else{
				if(move_timer < 50){
					move_timer += 1;
					transform.Translate(Vector3.left*spd*3.2f*Time.deltaTime);
					anton.transform.Translate(Vector3.left*spd*0.8f*Time.deltaTime);
				}
				else{
					state = 1;
					pause_timer = 0;
					move_timer = 0;
					anton.GetComponent<PlayerScript>().act = true;
				}
			}
		}
		//State 5, pause and go up
		else if(state == 5){
			if(pause_timer < 3){pause_timer += 1;}
			else{
				if(move_timer < 50){
					move_timer += 1;
					transform.Translate(Vector3.up*spd*3.2f*Time.deltaTime);
					anton.transform.Translate(Vector3.up*spd*0.8f*Time.deltaTime);
				}
				else{
					state = 1;
					pause_timer = 0;
					move_timer = 0;
					anton.GetComponent<PlayerScript>().act = true;
				}
			}
		}
		
	}
}
