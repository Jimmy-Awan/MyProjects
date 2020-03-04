using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{


	public LoadingScript loadingScript;

    public GameObject Level_UI;

	public GameObject mainMenu_UI;

	public GameObject exit_Dialog;


	public SoundManagerScript soundManager_Script;


	void Start ()
	{
		exit_Dialog.SetActive (false);
		InitialValues ();

	
	}


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			

			Yes_ExitButton_OnClick ();
		}



	}

	private void InitialValues ()
	{
		if (PlayerPrefs.GetInt ("InitialValue", 0) == 0) {
			PlayerPrefs.SetInt ("run", 0);
			PlayerPrefs.SetInt ("car0", 1);
			PlayerPrefs.SetInt ("cash", 1000);
            PlayerPrefs.SetInt("unlocklevel", 1);

            PlayerPrefs.SetInt ("InitialValue", 1);

		}

	}


	public void PlayButton_OnClick ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		//GetComponent<Animation> ()["CameraMovement"].speed = 1;
		//GetComponent<Animation> ().Play ();
		mainMenu_UI.SetActive (false);
        Level_UI.SetActive(true);

        //loadingScript.StartLoading ();
		//SceneManager.LoadScene ("RCC City TPS");
		MoPubManager.Show_Interstitial (MoPubManager.interstitial_MM);

	}

	public void MoreApps (string i)
	{
		Application.OpenURL (i);
	}

	public void media_buttons (string i)
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		Application.OpenURL (i);
	}

	//IEnumerator DelayToStart (float delayTime)
	//{
	//	yield return new WaitForSeconds (delayTime);

	//	SceneManager.LoadSceneAsync ("GamePlay");
	
	//}

	public void Yes_ExitButton_OnClick ()
	{
		Debug.Log ("*** Application Quit ***");
		soundManager_Script.PlayAudioClip ("buttonClick");
		exit_Dialog.SetActive (true);
		Application.Quit ();
	}

	public void ExitButton_OnClick ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		MoPubManager.Show_Interstitial (MoPubManager.interstitial_MM);
		exit_Dialog.SetActive (true);

	}

	public void No_ExitButton_OnClick ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
		MoPubManager.Load_Interstitial (MoPubManager.interstitial_MM);
		exit_Dialog.SetActive (false);

	}

	//........................................ SETTINGS ........................................

	public void SettingButton_OnClick ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

	}

	IEnumerator blink ()
	{

		yield return new WaitForSeconds (0.5f);
		StartCoroutine (blink ());
	}

	public void SteeringModeButton_OnClick (string modeName)
	{
		soundManager_Script.PlayAudioClip ("buttonClick");

		if (modeName == "buttons") {
			PlayerPrefs.SetString ("steeringmode", modeName);
			//steeringMode_Text.text=PlayerPrefs.GetString ("steeringmode").ToUpper();
		
		} else if (modeName == "wheel") {
			PlayerPrefs.SetString ("steeringmode", modeName);
			//steeringMode_Text.text=PlayerPrefs.GetString ("steeringmode").ToUpper();

		} else if (modeName == "tilt") {
			PlayerPrefs.SetString ("steeringmode", modeName);
			//steeringMode_Text.text=PlayerPrefs.GetString ("steeringmode").ToUpper();

		}

	}

	public void MoreGames ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
        GameUtilities.moreAppsLink ();

	}

	public void RateUS ()
	{
		soundManager_Script.PlayAudioClip ("buttonClick");
        GameUtilities.RateUsLink ();

	}

}
