using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

//Attach this code to a button
//The player clicks on the button to quit the game
//Created by Julie Bodette

//may need to create an actual build of the game to test this
//generally works in actual builds but not in the unity editor

	public void onClick()
	{
		Application.Quit();
	}
}
