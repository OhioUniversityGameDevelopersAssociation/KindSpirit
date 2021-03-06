﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	Vector2 stairLocation; //X,Y location of stairs on previous floor. Starting from top left.
	GameObject healthText; //The gameobject holding the health UI text.
	GameObject player; //The player's gameobject.
	static public GameObject messageObject; //reference to message box to display text about objects

	void Start()
	{
		DontDestroyOnLoad (gameObject);
		SetStairLocation (new Vector2 (0, 0));

	}

	public void FloorCompleted()
	{
		Debug.Log ("Floor Completed");
	}
	public void GenerationComplete() //Ran by FloorPlan when it is done building a floor.
	{
		Debug.Log("Spawn Player, Fade in.");
		player = GameObject.Find ("Anton");
		healthText = GameObject.Find ("HealthText");
		messageObject = GameObject.Find ("messagebox");
		Debug.Log (messageObject);

	}

	void Update()
	{
		if(healthText)
		{
			healthText.GetComponent<Text> ().text = "Health: " + player.GetComponent<PlayerManager>().Health.ToString ();
		}
	}

	//Stair location manager. Stores the location of the stairs for use between floors.
	public void SetStairLocation (Vector2 location)
	{
		stairLocation = location;
	}
	public Vector2 GetStairLocation ()
	{
		return stairLocation;
	}
}