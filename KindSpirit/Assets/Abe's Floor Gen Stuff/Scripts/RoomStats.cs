using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Column{
	public int[] row = new int[8];
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
	
	public Column[] column = new Column[16];
}
