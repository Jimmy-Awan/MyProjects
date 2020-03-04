using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LevelSelectionScript : MonoBehaviour {

	public SoundManagerScript soundManager_Script;
	public GameObject loadingImage;
    public LoadingScript loadingScript;
    public GameObject mainMenu_Objects;

	public GameObject levelSelection_UI;
	public GameObject mainMenu_UI;

	public GameObject lockDialog;
	public GameObject[] locked_Images;
	public GameObject levelImage_Child;
	public GameObject levelBig_Image;
	public Sprite[] levelBig_Sprite;

	public Transform hover_Image;

	public GameObject prevLevel_Button;
	public GameObject nextLevel_Button;

	public Text dollar_text;
	public Text levelNumber_Text;
	public Text levelTime_Text;
	public Text levelReward_Text;
	public Text levelDifficulty_Text;

	private int currentCash;
	private int currentLevelNumber=0;
	private int UnLockedLevelNumber = 1;


	//void Awake ()
	//{
	//	MoPubManager.Load_Interstitial (MoPubManager.interstitial_MM);
	//}
	void Start () 
	{
		lockDialog.SetActive (false);	

		GamePlayScript.comingFromLS = true;

		InitialValue ();
	}

	private void InitialValue()
	{
		
		currentCash = PlayerPrefs.GetInt ("cash");

		if (GameConstants.levelNo == 0) {
			currentLevelNumber = GameConstants.levelNo + 1;
		} else {
			currentLevelNumber = GameConstants.levelNo;
		}
		UnLockedLevelNumber = PlayerPrefs.GetInt ("unlocklevel");

		dollar_text.text = currentCash.ToString ();

		RemoveLockImages();
		ChangeSpecification ();


		prevLevel_Button.GetComponent<Image> ().color = Color.gray;
	}

	/// <summary>
	// Start Button to Car Selection
	/// </summary>


	public void StartButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");


		//MoPubManager.Show_Interstitial (MoPubManager.interstitial_MM);
		//MoPubManager.Load_Interstitial (MoPubManager.interstitial_MM);

        loadingScript.StartLoading();

		GameConstants.levelNo = currentLevelNumber;
		GameConstants.goingToLevelSelection = true;
		PlayerPrefs.Save ();

		StartCoroutine (StartDelay (2f));

	}

	IEnumerator StartDelay(float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadSceneAsync ("GamePlay");


	}

	//......................................

	public void BackButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		soundManager_Script.audioSource_Component.clip = soundManager_Script.mainmenu_Clip;
		soundManager_Script.audioSource_Component.Play ();

		//mainMenu_Objects.SetActive (true);
		mainMenu_UI.SetActive (true);

		levelSelection_UI.SetActive (false);


	}

		

	//................................................

	#region Next And Previos Level Image Scinario

	int ii=0;
	bool readyToClick=true;

	public void NextButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		prevLevel_Button.GetComponent<Image> ().color = Color.white;

		if (ii < 1 && readyToClick) {
			ii++;
			if (ii == 1) {
				nextLevel_Button.GetComponent<Image> ().color = Color.gray;
			}


			iTween.MoveTo (levelImage_Child, iTween.Hash ("x", levelImage_Child.transform.localPosition.x - 850, "time", .5f, "islocal", true, "easetype", iTween.EaseType.linear,"oncomplete","makeReadyToClick","oncompletetarget",this.gameObject));
			readyToClick = false;
		}
	}

	public void PreviousButton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		nextLevel_Button.GetComponent<Image> ().color = Color.white;

		if (ii > 0 && readyToClick) {
			ii--;

			if (ii == 0) {
				prevLevel_Button.GetComponent<Image> ().color = Color.gray;
			}


			iTween.MoveTo (levelImage_Child, iTween.Hash ("x", levelImage_Child.transform.localPosition.x + 850, "time", .5f, "islocal", true, "easetype", iTween.EaseType.linear,"oncomplete","makeReadyToClick","oncompletetarget",this.gameObject));
			readyToClick = false;
		}

	}

	void makeReadyToClick()
	{
		Debug.Log ("making it click");
		readyToClick = true;

	}
	#endregion

	bool needToChangeHover =false;
	#region Level Pressed Button 
	//...........................................

	public void PressedLeveButton_OnClick(int pressedLeveNumber)
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		if (pressedLeveNumber <= UnLockedLevelNumber) 
		{
			needToChangeHover = true;
			currentLevelNumber =pressedLeveNumber;
			levelBig_Image.GetComponent<Image> ().sprite = levelBig_Sprite [pressedLeveNumber-1];
			ChangeSpecification ();

		} else {
			needToChangeHover = false;
			lockDialog.SetActive (true);

//			hover_Image.SetParent (lastPressedImage);
//			hover_Image.GetComponent<RectTransform> ().localPosition = Vector2.zero;
		}

	}
	public void ChangeHove(Transform levelButtonObject)
	{
		if (needToChangeHover) {
			hover_Image.SetParent (levelButtonObject);
			hover_Image.GetComponent<RectTransform> ().localPosition = Vector2.zero;
		}

	}

	#endregion

	//................................................ Change Level Information
	void ChangeSpecification()
	{
		levelNumber_Text.text ="Level "+ currentLevelNumber.ToString ();
		levelTime_Text.text = LevelConstants.levelTime_LS [currentLevelNumber-1];
		levelReward_Text.text = LevelConstants.levelReward [currentLevelNumber - 1].ToString ();

		if (currentLevelNumber >= 1 && currentLevelNumber <= 3) 
		{

			levelDifficulty_Text.text= "Easy";
			levelDifficulty_Text.color = Color.green;
		}
		else if(currentLevelNumber >= 4 && currentLevelNumber <= 6)
		{
			levelDifficulty_Text.text= "Medium";
			levelDifficulty_Text.color = new Color (240,184,0);
		}
		else if(currentLevelNumber > 6 && currentLevelNumber <= 10)
		{
			levelDifficulty_Text.text= "Hard";
			levelDifficulty_Text.color = Color.red;
		}

	}

	//....................................
//
	public void Locked_OKBUtton_OnClick()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		lockDialog.SetActive (false);

	}

	//.....................................
	private void RemoveLockImages()
	{
		for (int i = 1; i <= UnLockedLevelNumber; i++) {

			locked_Images [i-1].SetActive (false);

		}

	}
}
