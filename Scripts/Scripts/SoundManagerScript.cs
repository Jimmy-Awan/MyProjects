using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class SoundManagerScript : MonoBehaviour {


	public AudioSource audioSource_Component;
	public AudioSource hornAudioSource_Component;
	public AudioSource GameoverAudioSource_Component;

	public AudioClip buttonClick_Clip;
	public AudioClip lightOnOff_Clip;
	//public AudioClip selection_Clip;
	public AudioClip mainmenu_Clip;
	public AudioClip horn_Clip;
	public AudioClip seatBelt_Clip;
	public AudioClip parkingComplete_Clip;

	public AudioClip levelComplete_Clip;
	public AudioClip levelFailed_Clip;
	public AudioClip xpIncreament_Clip;
	public AudioClip xpDecreament_Clip;
	public AudioClip carPurchased_Clip;
	public AudioClip timerWarning_Clip;
//	public AudioClip logo_Clip;



	void Start () 
	{
		audioSource_Component = GetComponent<AudioSource> ();
	}
	


	public void PlayAudioClip(string currentClipName)
	{
		switch(currentClipName)
		{

		case "buttonClick":
			audioSource_Component.PlayOneShot (buttonClick_Clip);
			break;

//		case "selection":
//			audioSource_Component.clip = selection_Clip;
//			audioSource_Component.Play ();
//			break;

		case "mainmenu":
			audioSource_Component.clip = mainmenu_Clip;
			audioSource_Component.Play ();
			break;

		case "hornStart":
			hornAudioSource_Component.clip = horn_Clip;
			hornAudioSource_Component.Play ();
			break;

		case "hornStop":
			hornAudioSource_Component.clip = horn_Clip;
			hornAudioSource_Component.Stop ();
			break;

		case "seatBelt":
			audioSource_Component.PlayOneShot (seatBelt_Clip);
			break;

		case "levelComplete":
			audioSource_Component.Stop ();
			GameoverAudioSource_Component.clip = levelComplete_Clip;
			GameoverAudioSource_Component.Play ();

		break;

		case "levelFailed":
			audioSource_Component.Stop ();
			GameoverAudioSource_Component.clip = levelFailed_Clip;
			GameoverAudioSource_Component.Play ();

			break;
		case "xpIncreament":
			audioSource_Component.PlayOneShot (xpIncreament_Clip);

			break;
		case "xpDecreament":
			audioSource_Component.PlayOneShot (xpDecreament_Clip);

			break;

		case "lightOnOff":
			audioSource_Component.PlayOneShot (lightOnOff_Clip);
			break;

		case "parkingComplete":
			audioSource_Component.PlayOneShot (parkingComplete_Clip);
			break;

		case "successfullyPurchased":
			audioSource_Component.PlayOneShot (carPurchased_Clip);
			break;

		case "timerWarning":
			audioSource_Component.PlayOneShot (timerWarning_Clip);
			break;

		case "trafficHorn":
			audioSource_Component.PlayOneShot (horn_Clip);
			break;



		}


	}


}
