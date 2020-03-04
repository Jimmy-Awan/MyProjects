using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerCarScript : MonoBehaviour {



	public GamePlayScript gamePlayScript;
	public GameOverScript gameOverScript;

	public RCC_Camera player_RccCamera;
	private RCC_CarControllerV3 player_RccCar;


	private BoxCollider boxCollider;
	private SpriteRenderer destinationSprite;

	bool solidLineCossed=false;


	void Start () {
		player_RccCar = GetComponent<RCC_CarControllerV3> ();
	}

	void Update () {

		if (gamePlayScript.isParked) {
			destinationSprite.color = Color.green;
		}
	
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "finaldestination") {
			other.transform.GetChild (0).gameObject.SetActive (false);
			boxCollider = other.GetComponent<BoxCollider> ();
			destinationSprite = other.GetComponent<SpriteRenderer> ();
		}
		if (other.gameObject.tag == "PickUpLocation") {
			Debug.Log (gamePlayScript.dropoffArrows.Length);
			gamePlayScript.pickupArrows [GameConstants.levelNo-1].SetActive (false);
			gamePlayScript.pickupIndicators [GameConstants.levelNo - 1].SetActive (false);
			gamePlayScript.dropoffArrows [GameConstants.levelNo-1].SetActive (true);
			gamePlayScript.dropoffLocations [GameConstants.levelNo - 1].SetActive (true);
			gamePlayScript.dropoffIndicators [GameConstants.levelNo - 1].SetActive (true);
			other.transform.GetChild (0).gameObject.SetActive (false);
			boxCollider = other.GetComponent<BoxCollider> ();
			destinationSprite = other.GetComponent<SpriteRenderer> ();
			gamePlayScript.goingToPick = true;
		}
		if ((other.gameObject.tag == "leftIndicator" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Left) 
			|| (other.gameObject.tag == "rightIndicator" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Right)) {

			gamePlayScript.Xp_Increament ("indicator");
			other.gameObject.SetActive (false);
			StartCoroutine (IndicatorOff (1));

		} if ((other.gameObject.tag == "leftIndicator" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Right) 
			|| (other.gameObject.tag == "rightIndicator" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Left)) {

			gamePlayScript.Xp_Decreament ("indicator");
			other.gameObject.SetActive (false);
			StartCoroutine (IndicatorOff (1));
		}
		if ((other.gameObject.tag == "leftIndicator" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Off) 
			|| (other.gameObject.tag == "rightIndicator" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Off)) {

			gamePlayScript.Xp_Decreament ("indicator");
			other.gameObject.SetActive (false);
			StartCoroutine (IndicatorOff (1));
		}

		if ((other.gameObject.tag == "straight" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Left) 
			|| (other.gameObject.tag == "straight" && player_RccCar.indicatorsOn == RCC_CarControllerV3.IndicatorsOn.Right)) {

			gamePlayScript.Xp_Decreament ("indicator");
			other.gameObject.SetActive (false);
			StartCoroutine (IndicatorOff (1));
		}

	}

	IEnumerator IndicatorOff(float delayTime){

		yield return new WaitForSeconds (delayTime);
		player_RccCar.indicatorsOn = RCC_CarControllerV3.IndicatorsOn.Off;

	}



	void OnTriggerStay(Collider other)
	{
//		Debug.Log ("collider tag: "+other.tag);

		if (!GameConstants.isGameOver && !GameConstants.isGamePause && GameConstants.isGameStarted) {
			
			if (other.gameObject.tag == "finaldestination" && GetComponent<PlayerCarScript> ().enabled) {
			
				if (player_RccCar.speed.ToString ("0") == "0") {

					gamePlayScript.goingToStop = true;
					other.gameObject.GetComponent<SpriteRenderer> ().color = new Color (255, 189, 6, 255);

				} else {
					gamePlayScript.goingToStop = false;
					other.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;

				}

			}
			if (other.gameObject.tag == "PickUpLocation" && GetComponent<PlayerCarScript> ().enabled) {
				if (player_RccCar.speed.ToString ("0") == "0") {
					gamePlayScript.goingToPick = true;
					other.gameObject.GetComponent<SpriteRenderer> ().color = new Color (255, 189, 6, 255);

				} else {
					gamePlayScript.goingToPick = false;
					other.gameObject.GetComponent<SpriteRenderer> ().color = Color.white;

				}
			}
			if (other.gameObject.tag == "solidLine") {
			
				if (!solidLineCossed) {
					gamePlayScript.Xp_Decreament ("solidLine");
					solidLineCossed = true;

					StartCoroutine (Toggle_SolidLineCross (6f));
				}

			}
		}

	}


	void OnTriggerExit(Collider other)
	{


		if (other.gameObject.tag == "finaldestination") {
			other.transform.GetChild (0).gameObject.SetActive (true);
			gamePlayScript.goingToStop = false;

		}
		if (other.gameObject.tag == "PickUpLocation") {
			other.transform.GetChild (0).gameObject.SetActive (false);
			gamePlayScript.goingToPick = false;

		}

	}
	bool readyToCrash=true;

	void OnCollisionEnter(Collision other)
	{

		if (other.gameObject.tag == "water") {

			Vector3 temp = other.transform.position;
			GameObject tempWaterSpray = Instantiate (gamePlayScript.waterSpray, temp, Quaternion.identity) as GameObject;
			other.gameObject.tag = "Untagged";

			Destroy (tempWaterSpray, 20f);

		}
		if (!GameConstants.isGameOver &&!GameConstants.isGamePause&& GameConstants.isGameStarted && readyToCrash) {
			gamePlayScript.Xp_Decreament ("carCrach");
			readyToCrash = false;
			StartCoroutine (ReadyForCrash ());
		}
	}


	IEnumerator Toggle_SolidLineCross(float toggleDelay)
	{
		yield return new WaitForSeconds (toggleDelay);
		solidLineCossed = false;


	}

	IEnumerator ReadyForCrash()
	{
		yield return new WaitForSeconds (2);
		readyToCrash = true;
	}

}
