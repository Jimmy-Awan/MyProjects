using UnityEngine;
using System.Collections;

public class ParticalsCollion : MonoBehaviour {



	void OnTriggerStay(){

		Vector3 temp = new Vector3 (0, -5, 0);

		//GetComponent<ParticleAnimator> ().force = temp;

	}

	void OnTriggerExit(){

		Vector3 temp = new Vector3 (0, -1, 0);

		//GetComponent<ParticleAnimator> ().force = temp;

	}
}
