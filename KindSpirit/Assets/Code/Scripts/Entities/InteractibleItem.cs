using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractibleItem : MonoBehaviour {
	//public Transform messageprefabtransform;
	//public GameObject messageprefab;
	public string message;
	void Start(){
		//messageprefabtransform = this.gameObject.transform.GetChild (0);
		//messageprefab = this.gameObject.transform.GetChild (0).GetChild (0).gameObject;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			//Debug.Log ("Player touched object");
			GameManager.messageObject.GetComponent<Image> ().enabled = true; //display text
			GameManager.messageObject.transform.GetChild (0).gameObject.SetActive (true); //display text box background image
			GameManager.messageObject.transform.GetChild (0).GetComponent<Text> ().text = message;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		// Destroy message
		//Debug.Log ("someone exited collider");
		if (other.tag == "Player") 
		{
			GameManager.messageObject.GetComponent<Image> ().enabled = false; //stop displaing text
			GameManager.messageObject.transform.GetChild (0).gameObject.SetActive (false); //stop displaying text box background image
		}

	}
}
