using UnityEngine;
using System.Collections;
using SWS;
using UnityStandardAssets.Utility;


public class TrafficCar : MonoBehaviour {


	private splineMove sMove;
	private bool isStop=false;
	public SoundManagerScript soundManager_Script;

	public AutoMoveAndRotate[] tireRotation;

	void Start()
	{
		sMove = GetComponent<splineMove> ();

	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("********  Collision Active  *****");

		if (other.gameObject.tag == "Player" && !isStop) {
			sMove.Pause (0);
			isStop = true;
			StopTireRotation ();
			StartCoroutine(StartAgain());


		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			soundManager_Script.PlayAudioClip ("trafficHorn");
			isStop = true;
			StopTireRotation ();
			sMove.Pause (0);
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			StartCoroutine(StartAgain());

		}

	}

	private void StopTireRotation()
	{
		tireRotation [0].enabled = false;
		tireRotation [1].enabled = false;
		tireRotation [2].enabled = false;
		tireRotation [3].enabled = false;

	}

	private void StartTireRotation()
	{
		tireRotation [0].enabled = true;
		tireRotation [1].enabled = true;
		tireRotation [2].enabled = true;
		tireRotation [3].enabled = true;

	}

	IEnumerator StartAgain()
	{
		yield return new WaitForSeconds (2);
		sMove.Resume ();
		StartTireRotation ();
		isStop = false;

	}

}

