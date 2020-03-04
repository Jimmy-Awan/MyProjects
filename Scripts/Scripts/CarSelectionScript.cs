using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ChartboostSDK;

[System.Serializable]
public class CarSpecification
{
	[SerializeField]public string name;
    [SerializeField]public float engine;
    [SerializeField]public float speed;
    [SerializeField]public float brake;
	[SerializeField]public int price;


}

public class CarSelectionScript : MonoBehaviour {


	public SoundManagerScript soundManager_Script;
	private OrbitCamera currentOrbitCamera_Script;
	public LoadingScript loadingScript;

	public Animation backButtonAnimation;
	public Canvas mainMenu_UI;
	public Canvas carSelection_UI;
	public Canvas levelSelection_UI;
	public GameObject mainMenu_Objects;
    public GameObject car_spawn;
    public GameObject back_btn;
	public GameObject[] Cars;
	public CarSpecification[] carSpecification;
	public Material[] carsBody_Materials;
	public Color32[] carDefaultColor;


	public GameObject play_Button;
	public GameObject buy_Button;
	public GameObject carButton;
	public GameObject colorButton;
	public GameObject carColor_BuyButton;
	public GameObject carPrev_Button;
	public GameObject carNext_Button;
	public GameObject colorPrev_Button;
	public GameObject colorNext_Button;

	public GameObject Cars_Parent;
	public GameObject Color_Parent;
	public GameObject colorChild_for_animation;

	public GameObject modeSelection_Dialog;
	public GameObject Car_Selection_Category;
	public GameObject Car_Color_Category;

	public GameObject setting_Dialog;
	public Text steeringMode_Text;


	public Text carName_Text;
	public Text dollar_text;
	public Text xpLevel_Text;
    public Text car_price;
    public Text color_price;
    private int currentCash;
	private int currentLevelNumber;
	[HideInInspector]
	public int currentCar=0;
	private string currentColor;


	public GameObject notEnoughCash_ForCar_Dialog;
	public GameObject notEnoughCash_ForColor_Dialog;


    public Image speed;
    public Image engine;
    public Image brake;


    public GameObject lockedImage;
	public GameObject buyCar_Dailog;




	void Start () 
	{
		Cars [currentCar].SetActive (true);
		GameConstants.carNo = currentCar + 1;
		currentOrbitCamera_Script = GetComponent<OrbitCamera> ();
	
		InitialValue ();
	}

	//..................................... Script Initial Value ......................................

	#region This Function will call for every first time in the Script
	private void InitialValue()
	{

		steeringMode_Text.text = PlayerPrefs.GetString ("steeringmode").ToUpper();
//		PlayerPrefs.SetInt ("cash",100000);
		currentCash = PlayerPrefs.GetInt ("cash");
		dollar_text.text = currentCash.ToString ();

		xpLevel_Text.text = PlayerPrefs.GetInt ("xplevel").ToString ();

		currentLevelNumber = GameConstants.levelNo;
//		if(currentLevelNumber>0)
//			//levelNumber_Text.text ="Level "+ currentLevelNumber.ToString ();
//		else 
//			//levelNumber_Text.text ="Free Ride";


		PlayerPrefs.SetInt ("car" + currentCar, 1);

		buy_Button.SetActive (false);
		play_Button.SetActive (true);
		carButton.SetActive (false);
		lockedImage.SetActive (false);

		ChangeCar_Specification ();

		carPrev_Button.GetComponent<Image> ().color = Color.gray;
		colorPrev_Button.GetComponent<Image> ().color = Color.gray;

//		SetDefaultColor ();


	}
	#endregion

	//.................................... Next Button To Gameplay ...................................

	#region PLAY BUTTON TO GAMEPLAY

	public void LetsStart()
	{
		soundManager_Script.PlayAudioClip("buttonClick");
        Chartboost.showInterstitial(CBLocation.Default);
        //MoPubManager.Show_Interstitial(MoPubManager.interstitial_MM);
        PlayerPrefs.Save ();
		
		loadingScript.StartLoading ();
		
		mainMenu_Objects.SetActive (false);
		StartCoroutine (DelayToStart (2));

	}

	IEnumerator DelayToStart(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
       
        loadingScript.StopLoading();
        carSelection_UI.gameObject.SetActive(false);
        modeSelection_Dialog.SetActive(true);




    }
	#endregion

	//...................................... Back Button To Level Selection ........................................

	#region BACK BUTTON TO LEVEL SELECTION

	public void BackButton_OnClick()
	{
		soundManager_Script.PlayAudioClip("buttonClick");
		PlayerPrefs.Save ();
		Car_Selection_Category.SetActive (true);
		Car_Color_Category.SetActive (false);

		colorButton.gameObject.SetActive (true);
		if (PlayerPrefs.GetInt ("car" + currentCar) == 1) {

			play_Button.gameObject.SetActive (true);
		}else 
			play_Button.gameObject.SetActive (false);
		mainMenu_UI.gameObject.SetActive (true);
		carSelection_UI.gameObject.SetActive (false);


		backButtonAnimation["CameraMovement"].time =backButtonAnimation["CameraMovement"].length;
		backButtonAnimation ["CameraMovement"].speed = -1 ;
		backButtonAnimation.Play ();

		//loadingScript.StartLoading ();
		SetNewCarColor ();

		//StartCoroutine (DelayToBack (2));

	}

//	IEnumerator DelayToBack(float delayTime)
//	{
//		yield return new WaitForSeconds (delayTime);
//		SceneManager.LoadSceneAsync ("MainMenu");
//
//	}

	#endregion

	//....................................... View Cars in Sequences .......................................

	#region Next and Previous Button to view Cars

	public void NextCar()
	{
		soundManager_Script.PlayAudioClip("buttonClick");

		carPrev_Button.GetComponent<Image> ().color = Color.white;

		if (currentCar < 8) {

			currentCar++;

			Cars [currentCar-1].SetActive (false);
			//Cars [currentCar].transform.position = new Vector3 (Cars [currentCar].transform.position.x,Cars [currentCar].transform.position.y,Cars [currentCar].transform.position.z);
            Cars[currentCar].transform.position = car_spawn.transform.position;
            //currentOrbitCamera_Script.target = Cars [currentCar].transform;
            Cars [currentCar].SetActive (true);

			if (currentCar == 8)
				carNext_Button.GetComponent<Image> ().color = Color.gray;
			
			ChangeCar_Specification ();

			if (PlayerPrefs.GetInt ("car" + currentCar) == 1) {
				play_Button.SetActive (true);
				buy_Button.SetActive (false);
				lockedImage.SetActive (false);
				colorButton.SetActive (true);
				GameConstants.carNo = currentCar+1;

			} else {
				play_Button.SetActive (false);
				buy_Button.SetActive (true);
				colorButton.SetActive (false);
				lockedImage.SetActive (true);
			}
		}

	}


	public void PreviousCar()
	{
		soundManager_Script.PlayAudioClip("buttonClick");

		carNext_Button.GetComponent<Image> ().color = Color.white;

		if (currentCar >0) 
		{

			currentCar--;

			Cars [currentCar+1].SetActive (false);
            //Cars [currentCar].transform.position = new Vector3 (Cars [currentCar].transform.position.x, Cars [currentCar].transform.position.y, Cars [currentCar].transform.position.z);
            Cars[currentCar].transform.position = car_spawn.transform.position;

            //currentOrbitCamera_Script.target = Cars [currentCar].transform;
            Cars [currentCar].SetActive (true);

			if (currentCar == 0)
				carPrev_Button.GetComponent<Image> ().color = Color.gray;
			
			ChangeCar_Specification ();

			if (PlayerPrefs.GetInt ("car" + currentCar) == 1) 
			{
				play_Button.SetActive (true);
				buy_Button.SetActive (false);
				colorButton.SetActive (true);
				lockedImage.SetActive (false);
				GameConstants.carNo = currentCar+1;


			} 
			else
			{
				play_Button.SetActive (false);
				buy_Button.SetActive (true);
				colorButton.SetActive (false);
				lockedImage.SetActive (true);
			}
		}
	}

	#endregion

	//........................................ Car Specification Details ............................................

	#region Car Specification Change Method

	private void ChangeCar_Specification()
	{
        speed.fillAmount = carSpecification [currentCar].speed;
        engine.fillAmount = carSpecification [currentCar].engine;
        brake.fillAmount = carSpecification [currentCar].brake;
		
		carName_Text.text = carSpecification [currentCar].name;
		car_price.text = carSpecification [currentCar].price.ToString();


	}

	#endregion

	//........................................Car and Color enabling and disabling Animation.............................................

	#region Show Car and Color Button

	public void ShowCarsButton_OnClick()
	{
		soundManager_Script.PlayAudioClip("buttonClick");

		play_Button.SetActive (true);
		Cars_Parent.SetActive (true);
		Color_Parent.SetActive (false);
        back_btn.SetActive(true);
		carButton.SetActive (false);
		colorButton.SetActive (true);
        car_price.gameObject.SetActive(true);
        color_price.gameObject.SetActive(false);

		SetNewCarColor ();

	}

	 
	public void ShowColorButton_OnClick()
	{
		soundManager_Script.PlayAudioClip("buttonClick");

		play_Button.SetActive (false);
		Cars_Parent.SetActive (false);
		Color_Parent.SetActive (true);
        back_btn.SetActive(false);
		colorButton.SetActive (false);
        car_price.gameObject.SetActive(false);
        color_price.gameObject.SetActive(true);

        //carButton.transform.position = colorButton.transform.position;
        carButton.SetActive (true);
	}

	#endregion


	//........................................ SETTINGS ........................................

	#region Setting Region

	public void SettingButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		setting_Dialog.SetActive (true);

	}

	public void SteeringModeButton_OnClick(string modeName)
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		if (modeName == "buttons") {
			PlayerPrefs.SetString("steeringmode",modeName);
			steeringMode_Text.text=PlayerPrefs.GetString ("steeringmode").ToUpper();

		} else if (modeName == "wheel") {
			PlayerPrefs.SetString("steeringmode",modeName);
			steeringMode_Text.text=PlayerPrefs.GetString ("steeringmode").ToUpper();

		} else if (modeName == "tilt") {
			PlayerPrefs.SetString("steeringmode",modeName);
			steeringMode_Text.text=PlayerPrefs.GetString ("steeringmode").ToUpper();

		}

	}

	public void Setting_BackButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		setting_Dialog.SetActive (false);

	}

	#endregion

	//............................................ * Car and Color Purchasing *...........................................

	#region Purchasing Car and Color Button

	public void PurchaseCar()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

//		Debug.Log ("total Cash: " + currentCash);
		//Debug.Log ("current car price: " + carSpecification [currentCar].price);

		if (carSpecification [currentCar].price <= currentCash) {
			//CarPurchasing_Succussful ();
			buyCar_Dailog.SetActive(true);

		} 
		else 
		{
			notEnoughCash_ForCar_Dialog.SetActive (true);

		}



	}

	public void Yes_BuyCar_Button()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		CarPurchasing_Succussful ();
        carSpecification[currentCar].price = 0;
        car_price.text = carSpecification[currentCar].price.ToString();
        buyCar_Dailog.SetActive(false);

	}

	public void No_BuyCar_Button()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		buyCar_Dailog.SetActive(false);

	}

	void CarPurchasing_Succussful()
	{
		soundManager_Script.PlayAudioClip ("successfullyPurchased");

		PlayerPrefs.SetInt ("car" + currentCar, 1);
		colorButton.SetActive (true);
		play_Button.SetActive (true);
		buy_Button.SetActive (false);
		lockedImage.SetActive (false);
		GameConstants.carNo = currentCar+1;

        currentCash = currentCash - carSpecification [currentCar].price;
		dollar_text.text = currentCash.ToString ();
      

        PlayerPrefs.SetInt ("cash", currentCash);

	}

	public void PurchaseColor()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		if (currentCash > 200) {
			ColorPurchasing_Successful ();

		} else {
			
			notEnoughCash_ForColor_Dialog.SetActive (true);
		}

	}

	void ColorPurchasing_Successful()
	{
		soundManager_Script.PlayAudioClip ("successfullyPurchased");

		currentCash = currentCash - 200;
		dollar_text.text = currentCash.ToString ();
		carColor_BuyButton.SetActive (false);
		PlayerPrefs.SetInt ("cash", currentCash);
		carDefaultColor [currentCar] = carsBody_Materials [currentCar].color;
		SetNewCarColor ();
		PlayerPrefs.SetInt (currentColor, 1);

	}

	public void DialogBackButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		notEnoughCash_ForCar_Dialog.SetActive (false);
		notEnoughCash_ForColor_Dialog.SetActive (false);

	}

	#endregion

	//........................................ Next And Previous Car Colors ................................

	#region All Color Showing Region

	int colorCounter=0;

	public void NextColorButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		colorPrev_Button.GetComponent<Image> ().color = Color.white;

		if (colorCounter < 1) {
			colorNext_Button.GetComponent<Image> ().color = Color.gray;
			colorCounter++;
			iTween.MoveTo (colorChild_for_animation,iTween.Hash("x",Color_Parent.transform.GetComponent<RectTransform>().localPosition.x-835,"time",.5f,"easetype",iTween.EaseType.linear,"islocal",true));

		}

	}

	public void PreviousColorButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		colorNext_Button.GetComponent<Image> ().color = Color.white;

		if (colorCounter > 0) {
			colorPrev_Button.GetComponent<Image> ().color = Color.gray;
			colorCounter--;
			iTween.MoveTo (colorChild_for_animation,iTween.Hash("x",Color_Parent.transform.GetComponent<RectTransform>().localPosition.x+.01f,"time",.5f,"easetype",iTween.EaseType.linear,"islocal",true));

		}

	}

	#endregion

	//............................................Car Color Filling Area........................................

	#region Car Color Painting 

	public void PaintColor(string colorName)
	{
		soundManager_Script.PlayAudioClip ("buttonClick");


		switch (colorName) 
		{
		case "red":
			carsBody_Materials [currentCar].color = new Color32 (114, 0, 0, 255);
			currentColor = "red";
			break;

		case "yellow":
			carsBody_Materials [currentCar].color = new Color32(250,230,0,255);
			currentColor="yellow";
			break;

		case "green":
			carsBody_Materials [currentCar].color = new Color32(1,114,0,255);
			currentColor="green";
			break;

		case "darkblue":
			carsBody_Materials [currentCar].color = new Color32(0,34,114,255);
			currentColor="darkblue";
			break;

		case "pink":
			carsBody_Materials [currentCar].color = new Color32(114,0,100,255);
			currentColor="pink";
			break;

		case "voilet":
			carsBody_Materials [currentCar].color = new Color32(90,0,120,255);
			currentColor="voilet";
			break;

		case "lightblue":
			carsBody_Materials [currentCar].color = new Color32(0,95,120,255);
			currentColor="lightblue";
			break;

		case "gray":
			carsBody_Materials [currentCar].color = new Color32(81,81,81,255);
			currentColor="gray";
			break;

		case "copper":
			carsBody_Materials [currentCar].color = new Color32(130,60,0,255);
			currentColor="copper";
			break;

		case "black":
			carsBody_Materials [currentCar].color = new Color32(0,0,0,255);
			currentColor="black";
			break;


		}

		if (PlayerPrefs.GetInt (colorName, 0) != 1) {
			carColor_BuyButton.SetActive (true);
		}
		else {
			carColor_BuyButton.SetActive (false);
			carDefaultColor [currentCar] = carsBody_Materials [currentCar].color;
			SetNewCarColor ();
		}


	}

	private void SetNewCarColor()
	{
		if (PlayerPrefs.GetInt (currentColor, 0) != 1) 
		{
			Debug.Log ("new color set");
			//set default color of each car if new color was not purchased
			switch(currentCar)
			{
			case 0:
				carsBody_Materials [currentCar].color = carDefaultColor[currentCar];
				break;

			case 1:
				carsBody_Materials [currentCar].color = carDefaultColor[currentCar];
				break;
			case 2:
				carsBody_Materials [currentCar].color = carDefaultColor[currentCar];
				break;

			case 3:
				carsBody_Materials [currentCar].color = carDefaultColor[currentCar];
				break;
			case 4:
				carsBody_Materials [currentCar].color = carDefaultColor[currentCar];
				break;

			}
		}


	}

	#endregion


	//...................................... MODE SELECTION .....................................
	public void CareerButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		loadingScript.StartLoading ();
		//GoogleMobileAdsDemoScript.instance.ShowInterstitial ();
		StartCoroutine (DelayForCareer (2));


	}

	IEnumerator DelayForCareer(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);

		loadingScript.StopLoading ();
		modeSelection_Dialog.SetActive (false);
		carSelection_UI.gameObject.SetActive (false);
		mainMenu_UI.gameObject.SetActive (false);
		//levelSelection_Img.gameObject.SetActive (true);
		levelSelection_UI.gameObject.SetActive (true);

		//soundManager_Script.audioSource_Component.clip = soundManager_Script.selection_Clip;
		//soundManager_Script.audioSource_Component.Play ();


	}

	public void FreeRideButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		//GoogleMobileAdsDemoScript.instance.ShowInterstitial ();
		loadingScript.StartLoading ();
		StartCoroutine (FreeRideDelay (2f));

	}

	IEnumerator FreeRideDelay(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadSceneAsync ("GamePlay");
	}

	public void ModeSelection_BackButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		modeSelection_Dialog.SetActive(false);
		carSelection_UI.gameObject.SetActive (true);
		mainMenu_Objects.SetActive (true);

	}

	//...........................



	void OnApplicationQuit()
	{
		SetNewCarColor ();
		carDefaultColor [currentCar] = carsBody_Materials [currentCar].color;

	}






}
