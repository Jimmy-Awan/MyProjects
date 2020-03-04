using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {



	public GameObject loadingBg;
	public Image loading_OutterCircle_Image;
	public Image loading_InnerCircle_Image;
	public bool isLoadingStart=false;
	private float loadingCircleFillamount;



	
	void FixedUpdate () {
		
		if (isLoadingStart) {
			if (!loading_OutterCircle_Image.fillClockwise) {
				loading_InnerCircle_Image.fillAmount = loading_InnerCircle_Image.fillAmount + .8f * Time.fixedDeltaTime;
				loading_OutterCircle_Image.fillAmount = loading_OutterCircle_Image.fillAmount - .8f * Time.fixedDeltaTime;
			} else if(loading_OutterCircle_Image.fillClockwise) {
				loading_OutterCircle_Image.fillAmount = loading_OutterCircle_Image.fillAmount + .8f * Time.fixedDeltaTime;
				loading_InnerCircle_Image.fillAmount = loading_InnerCircle_Image.fillAmount - .8f * Time.fixedDeltaTime;
			}

			if (loading_OutterCircle_Image.fillAmount == 0) {

				loading_OutterCircle_Image.fillClockwise = true;
			}
			else if(loading_OutterCircle_Image.fillAmount==1) {

				loading_OutterCircle_Image.fillClockwise = false;
			}



		}
	
	}


	public void StartLoading()
	{
		loadingBg.SetActive (true);
		isLoadingStart = true;

	}

	public void StopLoading()
	{
		Debug.Log ("lo");
		loadingBg.SetActive (false);
		isLoadingStart = false;

	}
}
