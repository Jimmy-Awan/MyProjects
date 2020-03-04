using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
	
	public CarSelectionScript gameOverScript;
	public SoundManagerScript soundManager_Script;

	public float timeLeft = 300.0f;
	public bool stop = true;

	private float minutes;
	private float seconds;

	public Text text;
	private bool timerWarning=false;

	void Start()
	{
		if (GameConstants.levelNo > 0) {
			startTimer (LevelConstants.levelTime [GameConstants.levelNo - 1]);
		} else {
			startTimer (1000);
		}

	}



	public void startTimer(float from){
		stop = false;
		timeLeft = from;
		Update();
		StartCoroutine(updateCoroutine());
	}

	void Update() 
	{
		
		if(!GameConstants.isGameOver && !GameConstants.isGamePause && GameConstants.isGameStarted)
		{
			if (stop)
				return;
			timeLeft -= Time.deltaTime;

			minutes = Mathf.Floor (timeLeft / 60);
			seconds = timeLeft % 60;
			if (seconds > 59)
				seconds = 59;
			if (minutes < 0) 
			{
				stop = true;
				minutes = 0;
				seconds = 0;

                if(!GameConstants.levelComplete)
                gameOverScript.LevelFail ();

			}

			if (minutes == 0 && seconds < 11 && !timerWarning) {
				
				text.color = new Color32 (255, 111, 0, 255);
				//text.transform.parent.GetChild (0).GetComponent<Animation> ().enabled = true;
				timerWarning = true;
				StartCoroutine (TimerWarningSound ());

			} else if(seconds>11){

				text.color = new Color32(255,255,255,255);
				//text.transform.parent.GetChild (0).GetComponent<Animation> ().enabled = false;
				timerWarning = false;
			}
		}

	}

	private IEnumerator updateCoroutine(){
		while(!stop){
			text.text = string.Format("{0:0}:{1:00}", minutes, seconds);
			yield return new WaitForSeconds(0.2f);
		}
	}

	private IEnumerator TimerWarningSound()
	{
		if (!GameConstants.isGameOver && !GameConstants.isGamePause && timerWarning) {
			soundManager_Script.PlayAudioClip ("timerWarning");
			yield return new WaitForSeconds (1);
			StartCoroutine (TimerWarningSound ());
		}

	}
}