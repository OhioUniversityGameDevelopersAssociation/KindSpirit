using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RoomStats : MonoBehaviour {

	//Dimensions of the room
	public int length;
	public int width;
	//Position of the room
	public int x;
	public int y;
	//Reference to the door's rooms
	public GameObject[] up_doors = new GameObject[3];
	public GameObject[] down_doors = new GameObject[3];
	public GameObject[] right_doors = new GameObject[3];
	public GameObject[] left_doors = new GameObject[3];
	//If the room can be accessed normally, used for floor generation algorithm
	public int accessible;

	//Reference to all the rooms that can be accessed from this one
	public List<GameObject> neighbors = new List<GameObject>();

	//A reference to spawn a staircase from
	public GameObject staircase;

	//Called by the floor generator, gives this room a set of stairs going to the next floor
	public void Set_Stairs(){
		GameObject new_stairs = Instantiate (staircase);
		new_stairs.transform.position = new Vector3 (transform.position.x + 14, transform.position.y - 2, 0);
	}

}
