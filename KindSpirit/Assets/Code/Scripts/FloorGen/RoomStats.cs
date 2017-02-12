using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class node{
	
}


[System.Serializable]
public class Column{
	public bool[] row;
}

public class RoomStats : MonoBehaviour {

	public int length;
	public int width;
	
	public int x;
	public int y;

	public GameObject[] up_doors = new GameObject[3];
	public GameObject[] down_doors = new GameObject[3];
	public GameObject[] right_doors = new GameObject[3];
	public GameObject[] left_doors = new GameObject[3];
	
	public int accessible;
	
	public List<GameObject> neighbors = new List<GameObject>();
	
	//Create 3 different arrays of collumns of differing sizes, only using the one that correlates to the number of collumns
	public Column[] column;
	
	
	
	
	//Check to see if there is an obstacle at a certain space
	public bool IsObstacle(int x, int y){
		//Choose the correct collumn array based on width
		Column[] correct = column;
		
		
		bool check = correct[x].row[y];
		
		return check;
		
	}
	
	public Vector2 GetCoordinates(GameObject obj){
		//Get x and y coordinates relative to room position
		float x = Mathf.Abs(obj.transform.position.x - transform.position.x);
		float y = Mathf.Abs(transform.position.y - obj.transform.position.y);
		
		//Use the x and y to determine position in array
		float ax = ((width*16)/x);
		float ay = ((length*9)/y);
		
		//Return calculated values in a Vector2
		Vector2 result = new Vector2(ax,ay);
		return result;
	}
	
	
}
