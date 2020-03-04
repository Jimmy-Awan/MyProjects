using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class LevelComplete : MonoBehaviour
{
	public GameObject dialogLevelComplete;
    public SoundManagerScript soundManager_Script;
    public GameObject dialogLevelFail;
	public Transform point;
	private GameObject CarSelection;
	public CarSelectionScript sc;
	private int currentCash;
	public Text damagePercentage_Text;
	public Text damageCash_Text;
	public Text distanceCovered_Text;
	public Text distanceCash_Text;
	public Text totalCash_Text;
	public GameObject rcc_Camera;
    public GameObject next_Button;
    public GameObject tunnelCamera;
	bool levelComplate = true;

	void Start ()
	{
		CarSelection = GameObject.Find ("CarSelection&GamePlay");
		sc = CarSelection.GetComponent<CarSelectionScript> ();

//		Debug.Log (currentCash);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "CameraSwitch") {

			//tunnelCamera.GetComponent<SmoothFollow> ().target = transform;
			tunnelCamera.SetActive (true);
			Debug.Log ("CameraSwitch");
			rcc_Camera.SetActive (false);
			StartCoroutine (DelayForCameraSwitch (2f));

		}

        if(other.tag == "Finish" && !GameConstants.levelComplete)
        {
            currentCash = LevelConstants.levelReward[GameConstants.levelNo - 1];
            soundManager_Script.PlayAudioClip("levelComplete");
            GameConstants.levelComplete = true;
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + currentCash);
            PlayerPrefs.Save();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            MoPubManager.Show_Interstitial(MoPubManager.interstitial_GO);
            dialogLevelComplete.SetActive(true);
            GameConstants.levelNo = GameConstants.levelNo + 1;
            MoPubManager.Show_Interstitial(MoPubManager.interstitial_GO);

            if (GameConstants.levelNo > PlayerPrefs.GetInt("unlocklevel"))
            {
                PlayerPrefs.SetInt("unlocklevel", GameConstants.levelNo);

            }
            if (GameConstants.levelNo > 4)
            {

                next_Button.SetActive(false);
               
            }
        }

    }

	IEnumerator DelayForCameraSwitch (float delayTime)
	{
		yield return new WaitForSeconds (delayTime);
		rcc_Camera.gameObject.SetActive (true);
		tunnelCamera.gameObject.SetActive (false);
	}
	// Use  for initialization
	void OnCollisionEnter (Collision col)
	{
		
	}

	IEnumerator DelayToStart (float delayTime)
	{
		yield return new WaitForSeconds (delayTime);

		MoPubManager.Show_Interstitial (MoPubManager.interstitial_GO);
		dialogLevelComplete.SetActive (true);
		
	}

	public void disableDialog ()
	{
		dialogLevelComplete.SetActive (false);
	}


}
