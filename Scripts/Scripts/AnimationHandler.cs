using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour {


	private Animation animationComponent;
//	public SoundManagerScript soundManagerScript;



	void Start()
	{
		animationComponent = GetComponent<Animation> ();


	}

	public void PauseAnimation()
	{

		animationComponent [animationComponent.clip.name].speed = 0;
		StartCoroutine (PlayAgain());
	}


	IEnumerator PlayAgain()
	{
		yield return new WaitForSeconds (1);
		animationComponent [animationComponent.clip.name].speed = 1;

	}


//	public void AudioPlayListener(string clipName)
//	{
//		if(clipName=="logo")
//			soundManagerScript.PlayAudioClip (clipName);
//
//	}
}
