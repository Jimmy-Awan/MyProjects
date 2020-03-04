using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayScript : MonoBehaviour {

	public LoadingScript loadingScript;
	public SoundManagerScript soundManager_Script;
	public GameOverScript gameOverScript;
	public GameObject gear;
	public RCC_Camera rccCamera;
	public RCC_MobileButtons rcc_MobileButton;

	public GameObject Car;
	public GameObject[] LevelsObject;
	public GameObject[] LevelSpawnPoint;
	public GameObject freeRideLevel;

	public GameObject waterSpray;

	public GameObject pauseDialog_UI;
	public GameObject RCC_UI;
	public GameObject pickedup_Dialog;

	public GameObject seatBelt_Button;

	public Image startEngin_Image;
	public Image doubleIndicator_Image;

	public Sprite startEngin_Sprite;
	public Sprite stopEngin_Sprite;
	public Sprite doubleIndicatorON_Sprite;
	public Sprite doubleIndicatorOFF_Sprite;


	public GameObject[] pickupArrows;
	public GameObject[] dropoffArrows;
	public GameObject[] pickupLocations;
	public GameObject[] dropoffLocations;
	public GameObject[] pickupIndicators;
	public GameObject[] dropoffIndicators;
	private bool isEnginStart = true;
	private bool isDoubleIndicatorPressed=false;

	public GameObject stopParent_Image;
	public Image stopOutter_Image;
	public Image stopInner_Image;
	public Image stop_Image;
	public Image Tick_Image;

	public GameObject pickParent_Image;
	public Image pickOutter_Image;
	public Image pickInner_Image;
	public Image pick_Image;
	public Image pickTick_Image;
	public GameObject xpIncreament;
	public GameObject xpDecreament;

	public GameObject dayParticals;
	public GameObject nightParticals;

	public Text currentXp_Text;
	private int currentXp_integer=0;
	public Text currentLevel_Text;

	public bool goingToStop=false;
	public bool goingToPick=false;
	public bool isParked=false;
	public bool isParkedForPickup=false;

	private bool isSeatBeltOn=false;
	private bool isHeadLightOn=false;
	private bool parked = false;
	private bool pickedup= false;
	private bool checkNight = false;

	public Material daySkyMaterial;
	public Material nightSkyMaterial;
	public Material buldingMaterial;

	public Material car_material;
	public Texture[] car_textures;
	public Texture dayBuldingTexture;
	public Texture nightBuldingTexture;


	[HideInInspector]
	public bool temporaryEditorValue = false;
	[HideInInspector]
	public int levelNo=1;
	[HideInInspector]
	public int carNumber = 1;


	public GameObject objective_Dialog;

	public static bool comingFromLS=true;

	void Awake()
	{ 

		MoPubManager.HideBanner ();
		MoPubManager.Load_Interstitial (MoPubManager.interstitial_GO);


		#if UNITY_EDITOR
		if (temporaryEditorValue) 
		{
			GameConstants.levelNo = levelNo;
			GameConstants.carNo = carNumber;


		}
		#endif


	}

	void Start () 
	{	

		pauseDialog_UI.SetActive (false);

		InitialValues ();

	
	}


	#region Update Function
	void Update()
	{
		int i = gear.gameObject.GetComponent<RCC_UIDashboardButton> ().gearDirection;
		if (!GameConstants.isGameOver && !GameConstants.isGamePause && GameConstants.isGameStarted) {
			if (goingToStop) {
			
				if (!parked) {
					Debug.Log (stopOutter_Image.fillAmount);
//					stopInner_Image.gameObject.SetActive (false);
//					stopOutter_Image.gameObject.SetActive (false);
					stopParent_Image.SetActive (true);
					stopOutter_Image.fillAmount += .3f * Time.deltaTime;
				}

				if (stopOutter_Image.fillAmount == 1 && !isParked) {

					parked = true;
					isParked = true;

					stopOutter_Image.color = Color.green;
					stopInner_Image.color = Color.green;

					stop_Image.gameObject.SetActive (false);
					Tick_Image.gameObject.SetActive (true);

					soundManager_Script.PlayAudioClip ("parkingComplete");

					if (parked) {
						currentXp_integer = currentXp_integer + 300;
						PlayerPrefs.SetInt ("currentXp", currentXp_integer);
						currentXp_Text.text = PlayerPrefs.GetInt ("currentXp").ToString ();

						gameOverScript.rcc_UI.SetActive (false);
						StartCoroutine (CallLevelComplete (1.2f));
					}
				}


			}
			else if(goingToPick){
				
				if (!pickedup) {
					//Debug.Log ("outer if");
					pickParent_Image.SetActive (true);
					pickOutter_Image.fillAmount += .3f * Time.deltaTime;
				}
					if (pickOutter_Image.fillAmount == 1 && !pickedup) {
					//	Debug.Log ("outer in");
						pickedup = true;
						isParkedForPickup = true;
						pickOutter_Image.color = Color.green;
						pickInner_Image.color = Color.green;
						pick_Image.gameObject.SetActive (false);
						pickTick_Image.gameObject.SetActive (true);
						pickupLocations [GameConstants.levelNo-1].SetActive (false);
						if (pickedup) {	
							pickParent_Image.SetActive (false);
							pickedup_Dialog.SetActive (true);
							RCC_UI.SetActive (false);
							Time.timeScale = 0;
						}

					}

				} else if (stopOutter_Image.fillAmount == 1 && !isParkedForPickup) {
					//Debug.Log ("else if");
					pickedup = true;
					isParkedForPickup = true;
					stopOutter_Image.color = Color.green;
					stopInner_Image.color = Color.green;

					stop_Image.gameObject.SetActive (false);


					if (pickedup) {
						//pickedup_Dialog.SetActive (true);	
						//Tick_Image.gameObject.SetActive (false);
						stopInner_Image.gameObject.SetActive (false);
						stopOutter_Image.gameObject.SetActive (false);
						stopParent_Image.gameObject.SetActive (false);
					}
				}
			}
			else {
				stopOutter_Image.fillAmount = 0;
				stopParent_Image.SetActive (false);
				
				pickOutter_Image.fillAmount = 0;
				pickParent_Image.SetActive (false);

			}
		}


	#endregion

	#region InitalValue For GamePlay
	private void InitialValues()
	{
		if (GameConstants.levelNo % 2 == 0) {

			RenderSettings.skybox = nightSkyMaterial;
			buldingMaterial.mainTexture = nightBuldingTexture;
			nightParticals.SetActive (true);
			dayParticals.SetActive (false);
			checkNight = true;
		}
		if (GameConstants.levelNo % 2 != 0 || GameConstants.levelNo == 0) {

			RenderSettings.skybox = daySkyMaterial;
			buldingMaterial.mainTexture = dayBuldingTexture;
			dayParticals.SetActive (true);
			nightParticals.SetActive (false);
			checkNight = false;
		}

		Debug.Log ("initial alue in game play: " + GameConstants.levelNo);
		if (GameConstants.levelNo > 0) {
			
			//Cars [GameConstants.carNo - 1].SetActive (true);
			car_material.mainTexture= car_textures[GameConstants.carNo-1];
			LevelsObject [GameConstants.levelNo - 1].SetActive (true);


			rccCamera.playerCar = Car.transform;
			Car.transform.position = LevelSpawnPoint [GameConstants.levelNo - 1].transform.position;
			Car.transform.rotation = LevelSpawnPoint [GameConstants.levelNo - 1].transform.rotation;
			currentLevel_Text.text = GameConstants.levelNo.ToString ();
		} else {
			freeRideLevel.SetActive (true);
			currentLevel_Text.text="FR";
			Car.SetActive (true);
			rccCamera.playerCar = Car.transform;
			Car.transform.position = LevelSpawnPoint [GameConstants.levelNo].transform.position;
			Car.transform.rotation = LevelSpawnPoint [GameConstants.levelNo].transform.rotation;
		}


		switch (PlayerPrefs.GetString ("steeringmode")) {
		case "buttons":
			rcc_MobileButton.ChangeController (0);
			break;
		case "wheel":
			rcc_MobileButton.ChangeController (2);
			break;
		case "tilt":
			rcc_MobileButton.ChangeController (1);
			break;


		}

		GameConstants.isGameStarted = true;

		PlayerPrefs.SetInt ("currentXp", currentXp_integer);
		currentXp_Text.text = PlayerPrefs.GetInt ("currentXp").ToString();

		objective_Dialog.SetActive (true);
		RCC_UI.SetActive (false);
		Time.timeScale = 0;


	}
	#endregion

	public void OkButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		Time.timeScale = 1;

		RCC_UI.SetActive (true);
		objective_Dialog.SetActive (false);

	}

	public void Pickedup_OkButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		Time.timeScale = 1;

		RCC_UI.SetActive (true);
		pickedup_Dialog.SetActive (false);

	}
	#region Pause Buttons Functions
	public void PauseButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
        MoPubManager.Show_Interstitial (MoPubManager.interstitial_GO);
		int i = gear.gameObject.GetComponent<RCC_UIDashboardButton> ().gearDirection;
		Debug.Log("Paused: " + i);
		//stopParent_Image.SetActive (false);
		//pickParent_Image.SetActive (true);
		pauseDialog_UI.SetActive (true);
		RCC_UI.SetActive (false);
		Time.timeScale = 0;

	}
	public IEnumerator Delay(){
		Tick_Image.gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);

	}
	public void ResumeButton_OnClick()
	{
		Time.timeScale = 1;

		soundManager_Script.PlayAudioClip ("buttonClick");
        MoPubManager.Load_Interstitial (MoPubManager.interstitial_GO);
		pauseDialog_UI.SetActive (false);

		//stopParent_Image.SetActive (true);
		//pickParent_Image.SetActive (true);
		RCC_UI.SetActive (true);
		if(gear.gameObject.GetComponent<RCC_UIDashboardButton> ().gearDirection==1){
			gear.gameObject.GetComponent<RCC_UIDashboardButton> ().ChangeGear ();
			gear.gameObject.GetComponent<RCC_UIDashboardButton> ().ChangeGear ();
		}




	}

	public void RestartButton_OnClick()
	{
		
		soundManager_Script.PlayAudioClip ("buttonClick");

		GameConstants.ResetAllStatic2 ();
		Time.timeScale = 1;
		loadingScript.StartLoading ();
		StartCoroutine (RestartDelay (2));

	}

	IEnumerator RestartDelay(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadSceneAsync ("GamePlay");

	}

	public void MainMenuButton_OnClick()
	{
		Time.timeScale = 1;
		soundManager_Script.PlayAudioClip ("buttonClick");
		GameConstants.ResetAllStatic ();
		GameConstants.goingToLevelSelection = false;

		loadingScript.StartLoading ();

		MoPubManager.ShowBanner ();
		MoPubManager.Load_Interstitial (MoPubManager.interstitial_MM);

		StartCoroutine (MainMenuDelay (2));

	}

	IEnumerator MainMenuDelay(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadSceneAsync ("MainMenu");

	}

	#endregion

	//............... Horn...........
	public void HornPressed_Event_OnDown()
	{
		soundManager_Script.PlayAudioClip ("hornStart");

	}

	public void HornRelease_Event_OnUp()
	{
		soundManager_Script.PlayAudioClip ("hornStop");

	}

	//.......... Seat Belt
	public void SeatBelt_Button_OnClick()
	{
		soundManager_Script.PlayAudioClip ("seatBelt");
		if (!isSeatBeltOn) {
			Xp_Increament ("seatBelt");

			isSeatBeltOn = true;
		}

		Destroy (seatBelt_Button, 0.5f);


	}
	//.......... Head Light Check In Night
	public void Head_Light_Button_OnClick()
	{
		if (!isHeadLightOn && checkNight) {
			Xp_Increament ("headLight");

			isHeadLightOn = true;
		}

	}

	//........... Start Engine.................
	public void StartEnginButton_OnClick()
	{

		/*............

		This Function Perform in RCC_UIDashboardButton Script


		............*/


		soundManager_Script.PlayAudioClip ("buttonClick");

		if (isEnginStart) {
			startEngin_Image.sprite = stopEngin_Sprite;
			isEnginStart = false;
		} else {
			startEngin_Image.sprite = startEngin_Sprite;
			isEnginStart = true;
		}

	}

	//................. Double Indicators.........
	public void DoubleIndicator()
	{
		/*............

		This Function Perform in RCC_UIDashboardButton Script


		............*/

		soundManager_Script.PlayAudioClip ("buttonClick");

		if (!isDoubleIndicatorPressed) {
			doubleIndicator_Image.sprite = doubleIndicatorON_Sprite;
			doubleIndicator_Image.GetComponent<Animation> ().Play ();
			isDoubleIndicatorPressed = true;

		} else {
			doubleIndicator_Image.sprite = doubleIndicatorOFF_Sprite;
			doubleIndicator_Image.GetComponent<Animation> ().Stop ();
			isDoubleIndicatorPressed = false;
		}


	}
		
	//........ Right Left Indicator................
	public void RightLeftIndicator()
	{
		/*............

		This Function Perform in RCC_UIDashboardButton Script


		............*/
		soundManager_Script.PlayAudioClip ("buttonClick");
		Debug.Log ("Indicator");
		doubleIndicator_Image.sprite = doubleIndicatorOFF_Sprite;
		doubleIndicator_Image.GetComponent<Animation> ().Stop ();
		isDoubleIndicatorPressed = false;

	}

	//.........Accelaration.......................
	public void RaceButton_OnClick()
	{
		/*............

		This Function Perform in RCC_UIDashboardButton Script


		............*/

		if (!isSeatBeltOn) {

			Xp_Decreament ("seatBelt");


		}

		if (!isHeadLightOn && checkNight) {

			Xp_Decreament ("headLight");


		}



	}

	#region XP INC DEC

	public void Xp_Decreament(string mistakeName)
	{
		soundManager_Script.PlayAudioClip ("xpDecreament");

		GameConstants.misstakeCounter += 1;

		if (GameConstants.levelNo <= 0) {
			if (GameConstants.misstakeCounter >= LevelConstants.levelMistakes [GameConstants.levelNo + 10]) {

//				GameConstants.levelFailed = true;
//				gameOverScript.LevelFailed ();

			}
		} else {
			if (GameConstants.misstakeCounter >= LevelConstants.levelMistakes [GameConstants.levelNo -1]) {
//
//				GameConstants.levelFailed = true;
//				gameOverScript.LevelFailed ();

			}


		}

		xpDecreament.SetActive (true);
		xpDecreament.transform.GetChild (0).GetComponent<Animation> ().Stop ();
		xpDecreament.transform.GetChild (0).GetComponent<Animation> ().Play ();


		switch (mistakeName) 
		{

		case "seatBelt":
			
			currentXp_integer = currentXp_integer - 75;
			xpDecreament.transform.GetChild (0).GetComponent<Text>().text="No Seat Belt ON \n -150 XP";

			break;

		case "carCrach":

			currentXp_integer = currentXp_integer - 50;
			xpDecreament.transform.GetChild (0).GetComponent<Text>().text="Car Crashed \n -50 XP";

			break;

		case "speedLimit":

			currentXp_integer = currentXp_integer - 100;
			xpDecreament.transform.GetChild (0).GetComponent<Text>().text="SpeedLimit Crossed 45KM/H \n -100 XP";

			break;

		case "solidLine":

			//currentXp_integer = currentXp_integer - 50;
			//xpDecreament.transform.GetChild (0).GetComponent<Text>().text="Solid Line Crossed \n -50 XP";

			break;

		case "indicator":

			currentXp_integer = currentXp_integer - 50;
			xpDecreament.transform.GetChild (0).GetComponent<Text>().text="Wrong Indicator \n -75 XP";

			break;

		case "headLight":

			currentXp_integer = currentXp_integer - 50;
			xpDecreament.transform.GetChild (0).GetComponent<Text>().text="Head Light Off \n -75 XP";

			break;

		}

		PlayerPrefs.SetInt ("currentXp", currentXp_integer);
		currentXp_Text.text = PlayerPrefs.GetInt ("currentXp").ToString();


	}


	public void Xp_Increament(string bonusName)
	{
		soundManager_Script.PlayAudioClip ("xpIncreament");

		xpIncreament.SetActive (true);
		xpIncreament.transform.GetChild (0).GetComponent<Animation> ().Stop ();
		xpIncreament.transform.GetChild (0).GetComponent<Animation> ().Play ();


		switch (bonusName) 
		{

		case "seatBelt":

			currentXp_integer = currentXp_integer + 100;
			xpIncreament.transform.GetChild (0).GetComponent<Text> ().text = "Seat Belt ON \n +100 XP";

			break;

		case "indicator":

			currentXp_integer = currentXp_integer + 75;
			xpIncreament.transform.GetChild (0).GetComponent<Text> ().text = "Perfect indicator \n +75 XP";

			break;

		case "headLight":

			currentXp_integer = currentXp_integer + 75;
			xpIncreament.transform.GetChild (0).GetComponent<Text> ().text = "Head Light On \n +75 XP";

			break;

		case "parking":
//			currentXp_integer = currentXp_integer + 100;
//			xpIncreament.transform.GetChild (0).GetComponent<Text> ().text = "Perfect indicator \n +100 XP";

			break;

		}

		PlayerPrefs.SetInt ("currentXp", currentXp_integer);
		currentXp_Text.text = PlayerPrefs.GetInt ("currentXp").ToString ();


	}

	#endregion

	//.................................................................Level Complete Calling..............................
	IEnumerator CallLevelComplete(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		GameConstants.levelComplete = true;
		goingToStop = false;
		stopParent_Image.SetActive (false);
		gameOverScript.LevelComplete ();

	}

}
