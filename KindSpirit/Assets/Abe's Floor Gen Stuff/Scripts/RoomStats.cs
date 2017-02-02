using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Column{
	public int[] row = new int[9];
	public int[] row2 = new int[18];
	public int[] row3 = new int[27];
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
	public Column[] column = new Column[16];
	public Column[] column2 = new Column[32];
	public Column[] column3 = new Column[48];
	
	
	
	
	//Check to see if there is an obstacle at a certain space
	public bool IsObstacle(int x, int y){
		//Choose the correct collumn array based on width
		Column[] correct = column;
		switch (width){
			case 1:
				correct = column;
			break;	
			case 2:
				correct = column2; 
			break;
			case 3:
				correct = column3;
			break;
		}
		
		int check = 0;
		//Choose the correct row array based on length
		switch (length){
			case 1:
				check = correct[x].row[y];
			break;	
			case 2:
				check = correct[x].row2[y]; 
			break;
			case 3:
				check = correct[x].row3[y];
			break;
		}
		
		if(check == 1){return true;}
		else{return false;}
		
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
