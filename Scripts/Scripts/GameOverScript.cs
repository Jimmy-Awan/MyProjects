using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class GameOverScript : MonoBehaviour
{

	public SoundManagerScript soundManager_Script;
	public LoadingScript loadingScript;

	public GameObject rcc_UI;
	public GameObject gamePlay_UI;
	public GameObject stop_UI;
	public GameObject gameOver_Dialog;
	public GameObject rateUs_Dialog;
	public GameObject revive_Dialog;
	public GameObject next_Button;
	public GameObject restart_Button;
	public bool IsLoaded;
	public Image gameOverTitle_Image;

	public Sprite levelComplete_Sprite;
	public Sprite levelFailed_Sprite;
	public Sprite TimeUp_Sprite;
	public Sprite Crashed_Sprite;


	public Text earnedSkills_Text;
	public Text totalSkills_Text;
	public Text missionReward_Text;
	public Text TotalMissionReward_Text;


	int currentXP = 0;
	int currentCash = 0;

	public Countdown timer;



	#region Dialog Button Region

	public void NextButton_OnClick ()
	{
		Time.timeScale = 1;
		soundManager_Script.PlayAudioClip ("buttonClick");
		GameConstants.ResetAllStatic2 ();
		GameConstants.goingToLevelSelection = true;

		loadingScript.StartLoading ();
		StartCoroutine (NextDelay (2));
	}

	IEnumerator NextDelay (float delayTime)
	{

		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadSceneAsync ("GamePlay");

	}

	public void RestartButton_OnClick ()
	{
		
		soundManager_Script.PlayAudioClip ("buttonClick");

		if (GameConstants.levelComplete) {

			GameConstants.levelNo = GameConstants.levelNo - 1;
		}

		GameConstants.ResetAllStatic2 ();

		loadingScript.StartLoading ();
		StartCoroutine (RestartDelay (2));

	}

	IEnumerator RestartDelay (float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		Time.timeScale = 1;
		SceneManager.LoadSceneAsync ("GamePlay");

	}

	public void MainMenuButton_OnClick ()
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

	IEnumerator MainMenuDelay (float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadSceneAsync ("MainMenu");

	}

	#endregion

	//.........................


	//...................Level Complete...................
	public void LevelComplete ()
	{
		soundManager_Script.PlayAudioClip ("levelComplete");
		GameConstants.isGameOver = true;
		GameConstants.isGameStarted = false;
		rcc_UI.SetActive (false);
		gamePlay_UI.SetActive (false);
		gameOver_Dialog.SetActive (true);

		#region All skill XP Level Updates
		currentXP = PlayerPrefs.GetInt ("currentXp");
		earnedSkills_Text.text = currentXP.ToString ();
		PlayerPrefs.SetInt ("xplevel", PlayerPrefs.GetInt ("xplevel") + currentXP);
		totalSkills_Text.text = PlayerPrefs.GetInt ("xplevel").ToString ();
		#endregion


		#region All Cash Reward Update
		currentCash = LevelConstants.levelReward [GameConstants.levelNo - 1];
		missionReward_Text.text = currentCash.ToString ();
		//PlayerPrefs.SetInt ("cash", PlayerPrefs.GetInt ("cash") + currentCash);
		TotalMissionReward_Text.text = PlayerPrefs.GetInt ("cash").ToString ();
		#endregion

		#region RateUs Dialog Show Scinerio
		if (PlayerPrefs.GetInt ("rateus", 1) == 2) {
			Debug.Log ("Rate us Dailog Show");
			rateUs_Dialog.SetActive (true);

		} else {
			Debug.Log ("rate: " + PlayerPrefs.GetInt ("rateus"));
			PlayerPrefs.SetInt ("rateus", PlayerPrefs.GetInt ("rateus") + 1);

		}
		#endregion

		GameConstants.levelNo = GameConstants.levelNo + 1;

		if (GameConstants.levelNo > PlayerPrefs.GetInt ("unlocklevel")) {
			PlayerPrefs.SetInt ("unlocklevel", GameConstants.levelNo);

		}

		if (GameConstants.levelNo > 10) {

			next_Button.SetActive (false);
			restart_Button.GetComponent<RectTransform> ().anchorMax = next_Button.GetComponent<RectTransform> ().anchorMax;
			restart_Button.GetComponent<RectTransform> ().anchorMin = next_Button.GetComponent<RectTransform> ().anchorMin;
			restart_Button.GetComponent<RectTransform> ().position = next_Button.GetComponent<RectTransform> ().position;
		}



		ChangeTitle ();

		MoPubManager.Show_Interstitial (MoPubManager.interstitial_GO);

	}

	//..............Level Failed........................
	public void LevelFailed ()
	{
		soundManager_Script.PlayAudioClip ("levelFailed");
		GameConstants.isGameOver = true;

		next_Button.SetActive (false);
		rcc_UI.SetActive (false);
		stop_UI.SetActive (false);
		gamePlay_UI.SetActive (false);
		gameOver_Dialog.SetActive (true);

		restart_Button.GetComponent<RectTransform> ().anchorMax = next_Button.GetComponent<RectTransform> ().anchorMax;
		restart_Button.GetComponent<RectTransform> ().anchorMin = next_Button.GetComponent<RectTransform> ().anchorMin;
		restart_Button.GetComponent<RectTransform> ().position = next_Button.GetComponent<RectTransform> ().position;

		#region All skill XP Level Updates
		currentXP = PlayerPrefs.GetInt ("currentXp");
		earnedSkills_Text.text = currentXP.ToString ();
		PlayerPrefs.SetInt ("xplevel", PlayerPrefs.GetInt ("xplevel") + currentXP);
		totalSkills_Text.text = PlayerPrefs.GetInt ("xplevel").ToString ();
		#endregion


		#region All Cash Reward Update
		currentCash = 0;
		missionReward_Text.text = currentCash.ToString ();
		//PlayerPrefs.SetInt ("cash", PlayerPrefs.GetInt ("cash") + currentCash);
		TotalMissionReward_Text.text = PlayerPrefs.GetInt ("cash").ToString ();
		#endregion



		ChangeTitle ();


		MoPubManager.Show_Interstitial (MoPubManager.interstitial_GO);


	}

	#region Enable Titles For GameOver Dailogue

	public void ChangeTitle ()
	{
		if (GameConstants.levelComplete)
			gameOverTitle_Image.sprite = levelComplete_Sprite;

		if (GameConstants.levelFailed)
			gameOverTitle_Image.sprite = levelFailed_Sprite;

		if (GameConstants.timeup)
			gameOverTitle_Image.sprite = TimeUp_Sprite;

		if (GameConstants.crashed)
			gameOverTitle_Image.sprite = Crashed_Sprite;


	}

	#endregion

	public void RateUs_Yes ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

        GameUtilities.RateUsLink();
		rateUs_Dialog.SetActive (false);
		PlayerPrefs.SetInt ("rateus", 3);

	}

	public void RateUs_No ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		rateUs_Dialog.SetActive (false);
		PlayerPrefs.SetInt ("rateus", 1);
	}

	//	public void Revive_Yes()
	//	{
	//		soundManager_Script.PlayAudioClip ("buttonClick");
	//
	//		GoogleMobileAdsDemoScript.instance.ShowRewardBasedVideo ();
	//		revive_Dialog.SetActive (false);
	//		Time.timeScale = 1;
	//	}
	//
	//	public void Revive_No()
	//	{
	//		soundManager_Script.PlayAudioClip ("buttonClick");
	//
	//		Time.timeScale = 1;
	//		GameConstants.timeup = true;
	//		revive_Dialog.SetActive (false);
	//		LevelFailed ();
	//
	//	}

	public void FailChecker ()
	{
//		if (GoogleMobileAdsDemoScript.instance.isLoaded ()) {
//			GameConstants.isGameStarted = false;
//			rcc_UI.SetActive (false);
//			stop_UI.SetActive (false);
//			revive_Dialog.SetActive (true);
//			StartCoroutine (TimeScaleDelay(0));
//
//		} 
//		else 
		{

			GameConstants.timeup = true;
			LevelFailed ();
		}

	}

	IEnumerator TimeScaleDelay (int timeScale)
	{
		yield return new WaitForSeconds (2);
		Time.timeScale = timeScale;

	}


	//	public void GetMoreTime()
	//	{
	//		Debug.Log("timer recieved *********");
	//		timer.startTimer (60);
	//		rcc_UI.SetActive (true);
	//		GameConstants.isGameStarted = true;
	//	}
}
