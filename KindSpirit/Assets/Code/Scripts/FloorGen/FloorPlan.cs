using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlan : MonoBehaviour {

	public List<GameObject> room_list = new List<GameObject>();
	public List<GameObject> floor_rooms = new List<GameObject>();
	GameObject[,] grid = new GameObject[5, 5];
	int cur_x;
	int cur_y;
	public bool trigger_create;
	GameObject gameManager; //The object containing the gamemanager script.

	void Start()
	{
		gameManager = GameObject.Find ("GameManager");
	}
	
	void Update(){
		if(trigger_create){
			trigger_create = false;
			Create_Floor();
		}
	}
	
	void Create_Floor(){
		Reset_Field ();
		
		//Add the current room to the list
		floor_rooms.Add(gameObject);
		
		//Place Rooms all throughout the floor
		while (cur_y < 5) {
			while (cur_x < 5) {
				//If the cursor is pointing to an empty space, we need to place a room
				GameObject check = grid [cur_x, cur_y];
				if (check == null) {
					Place_New_Room ();
				}
				cur_x += 1;
			}
			cur_x = 0;
			cur_y += 1;
		}
		//Assign Neighbors to every room
		int f = 0;
		while(f < floor_rooms.Count){
			Assign_Neighbors(floor_rooms[f]);
			f += 1;
		}
		//Start accessible recursion, starting at the starting room
		Accessible_Recursion(floor_rooms[0]);
		//Go through every room, make sure all are accessible
		f = 0;
		while(f < floor_rooms.Count){
			RoomStats ch = floor_rooms[f].GetComponent<RoomStats>();
			//If a room is not accessible, we have a problem, fix it
			if(ch.accessible != 1){
				
			}
			f += 1;
		}
		gameManager.GetComponent<GameManager> ().GenerationComplete ();
	}

	void Place_New_Room(){
		bool placed = false;
		int loop_time = 0;
		GameObject attempt = room_list[0];
		while(placed == false){
			//Pick a random room from the list
			attempt = room_list[Random.Range(1,9)];
			placed = Insert_Room(attempt);
			loop_time += 1;
			if(loop_time > 50){
				placed = true;
			}
		}
		GameObject nroom = Instantiate(attempt);
		nroom.transform.position = new Vector3(cur_x*16,cur_y*-9,0);
		RoomStats new_stats = nroom.GetComponent<RoomStats>();
		new_stats.x = cur_x+1;
		new_stats.y = cur_y+1;
		
		//If we have reached this point, the room is legal, so insert it to the grid
		int c_x = cur_x;
		int c_y = cur_y;
		while (c_y <= cur_y + new_stats.length - 1) {
			while (c_x <= cur_x + new_stats.width - 1) {
				grid [c_x, c_y] = nroom;
				c_x += 1;
			}
			c_x = cur_x;
			c_y += 1;
		}
		
		
		floor_rooms.Add(nroom);
	}

	void Reset_Field(){
		cur_x = 0;
		cur_y = 0;
		int ax = 0;
		int ay = 0;
		while (ax < 5) {
			while (ay < 5) {
				grid [ax, ay] = null;
				ay += 1;
			}
			ay = 0;
			ax += 1;
		}
		grid [0, 0] = this.gameObject;
	}
	//Place a room into the grid, but return false if there is no room to fit this room
	bool Insert_Room(GameObject new_room){
		RoomStats stats = new_room.GetComponent<RoomStats> ();
		//First, make sure the room will not go out of bounds
		if (cur_x + stats.width > 5){
			return false;
		} else if (cur_y + stats.length > 5) {
			return false;
		} else {
			//Make sure that placing the room here won't cause conflicts
			int check_x = cur_x;
			int check_y = cur_y;
			while (check_x <= cur_x + stats.width - 1) {
				while (check_y <= cur_y + stats.length - 1) {
					GameObject check = grid [check_x, check_y];
					if (check != null) {
						return false;
					}
					check_y += 1;
				}
				check_y = cur_y;
				check_x += 1;
			}
			//Room placed, return true
			return true;
		}
	}	
	
	void Assign_Neighbors(GameObject room){
		
		RoomStats stats = room.GetComponent<RoomStats>();
		int d = 0;
		int nd = 0;
		//Get upper neighbors
		if(stats.y > 1){
			d = 0;
			while(d < stats.width){
				if(stats.up_doors[d] != null){
					//Make sure the potential neighbor also has a door leading to this one
					RoomStats n_stats = grid[stats.x+d-1,stats.y-2].GetComponent<RoomStats>();
					//Subtract the x coordinate of the new room from the x coordinate of the door to find out which door to check for the new room
					nd = (stats.x+d)-n_stats.x;
					
					if(n_stats.down_doors[nd] != null){
						stats.neighbors.Add(n_stats.gameObject);
					}
					else{
						stats.up_doors[d].GetComponent<SpriteRenderer>().sprite = null;
					}
				}
				d += 1;
			}
		}
		else{
			//Set all upper doors to disappear
			d = 0;
			while(d < stats.width){
				if(stats.up_doors[d] != null){
					stats.up_doors[d].GetComponent<SpriteRenderer>().sprite = null;
				}
				d+=1;
			}
		}
		//Get right neighbors
		if(stats.x+stats.width < 6){
			d = 0;
			while(d < stats.length){
				
				if(stats.right_doors[d] != null){
					//Make sure the potential neighbor also has a door leading to this one
					RoomStats n_stats = grid[stats.x+stats.width-1,stats.y+d-1].GetComponent<RoomStats>();
					//Subtract the y coordinate of the new room from the y coordinate of the door to find out which door to check for the new room
					nd = (stats.y+d)-n_stats.y;
					
					if(n_stats.left_doors[nd] != null){
						stats.neighbors.Add(n_stats.gameObject);
					}
					else{
						stats.right_doors[d].GetComponent<SpriteRenderer>().sprite = null;
					}
				}
				d += 1;
			}
		}
		else{
			//Set all right doors to disappear
			d = 0;
			while(d < stats.length){
				if(stats.right_doors[d] != null){
					stats.right_doors[d].GetComponent<SpriteRenderer>().sprite = null;
				}
				d+=1;				
			}
		}
		//Get lower neighbors
		if(stats.y+stats.length < 6){
			d = 0;
			while(d < stats.width){
				if(stats.down_doors[d] != null){
					//Make sure the potential neighbor also has a door leading to this one
					RoomStats n_stats = grid[stats.x+d-1,stats.y+stats.length-1].GetComponent<RoomStats>();
					//Subtract the x coordinate of the new room from the x coordinate of the door to find out which door to check for the new room
					nd = (stats.x+d)-n_stats.x;
					
					if(n_stats.up_doors[nd] != null){
						stats.neighbors.Add(n_stats.gameObject);
					}
					else{
						stats.down_doors[d].GetComponent<SpriteRenderer>().sprite = null;
					}
				}
				d += 1;
			}
		}
		else{
			//Set all lower doors to disappear
			d = 0;
			while(d < stats.width){
				if(stats.down_doors[d] != null){
					stats.down_doors[d].GetComponent<SpriteRenderer>().sprite = null;
				}
				d+=1;
			}
		}
		//Get left neighbors
		if(stats.x > 1){
			d = 0;
			while(d < stats.length){
				if(stats.right_doors[d] != null){
					//Make sure the potential neighbor also has a door leading to this one
					RoomStats n_stats = grid[stats.x-2,stats.y+d-1].GetComponent<RoomStats>();
					//Subtract the y coordinate of the new room from the y coordinate of the door to find out which door to check for the new room
					nd = (stats.y+d)-n_stats.y;
					
					if(n_stats.right_doors[nd] != null){
						stats.neighbors.Add(n_stats.gameObject);
					}
					else{
						stats.left_doors[d].GetComponent<SpriteRenderer>().sprite = null;
					}
				}
				d += 1;
			}
		}		
		else{
			//Set all left doors to disappear
			d = 0;
			while(d < stats.length){
				if(stats.left_doors[d] != null){
					stats.left_doors[d].GetComponent<SpriteRenderer>().sprite = null;
				}
				d+=1;
			}
		}
	}
		
	void Accessible_Recursion(GameObject room){
		RoomStats stats = room.GetComponent<RoomStats>();
		//Set Current room to accessible
		stats.accessible = 1;
		int a = 0;
		//Go through list of neighbors
		while(a < stats.neighbors.Count){
			GameObject check_r = stats.neighbors[a];
			if(check_r.GetComponent<RoomStats>().accessible != 1){
				Accessible_Recursion(check_r);
			}
			a += 1;
		}
	}
	
	
	
}
