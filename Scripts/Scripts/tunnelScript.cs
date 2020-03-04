using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelScript : MonoBehaviour
{

	private GameObject CarSelection;
	public CarSelectionScript carSelectionScript;

	void Start ()
	{
		
		CarSelection = GameObject.Find ("CarSelection&GamePlay");
		carSelectionScript = CarSelection.GetComponent<CarSelectionScript> ();
	}

	void OnTriggerEnter (Collider other)
	{
		carSelectionScript.tutorialCanvas.SetActive (false);
		if (PlayerPrefs.GetInt ("run") == 1) {
			PlayerPrefs.SetInt ("run", 2);
		}
		carSelectionScript.isEnteredinTunnel = true;
		Debug.Log ("Tunnel");
		carSelectionScript.ExitCar_Button.SetActive (false);

	}
}
