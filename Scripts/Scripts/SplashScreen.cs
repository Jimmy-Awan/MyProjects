using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{

	public float speed = 0.75f;


	void Start ()
	{
		StartCoroutine (GoingToMainMenu ());
	
	}


	
	IEnumerator GoingToMainMenu ()
	{

		yield return new WaitForSeconds (.2f);
		GetComponent<AudioSource> ().Play ();

		yield return new WaitForSeconds (3f);

		SceneManager.LoadScene ("MainMenu");


	}
}
