using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntelligentDistanceCalculator : MonoBehaviour {

	public Transform playerCamera;
    public Transform Car_playerCamera;
    public Transform targetDestination;

	public GameObject arrow2D_Object;
    Transform TargetDestination_Points;
  
	public Image arrowImage;
	public Image destinatonImage;
	public Text distanceWithArrow_Text;
	public Text distanceOnDestination_Text;

	Vector3 destinationPosition= new Vector3();
    Transform target;
	 float totalDistance =0;
  
    GameObject Played_Level;
  
    private void Awake()
    {
     

    }
    /// <summary>
    /// /////////////////////////////////////////////////////////end switch function////////////
    /// </summary>
    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        if (playerCamera.gameObject.activeInHierarchy)
        {
            TargetDestination_Points = playerCamera;
        } else if (Car_playerCamera.gameObject.activeInHierarchy){
            TargetDestination_Points = Car_playerCamera.GetChild(0).GetChild(0).transform;
        }
        
        #region Calculate the distnace between player and target object
      // Calculate this between player and target
      totalDistance = Vector3.Distance(TargetDestination_Points.position,targetDestination.position);

        #endregion
      
        #region Check Weather the Target is front of camera or not
        Vector3 screenPort = TargetDestination_Points.GetComponent<Camera>().WorldToViewportPoint(targetDestination.position);

		if ((screenPort.x < 0)||(screenPort.x >1)||(screenPort.z-1f<0)) 
		{
			arrowImage.gameObject.SetActive(true);
			distanceWithArrow_Text.gameObject.SetActive(true);

			destinatonImage.gameObject.SetActive(false);

			#region 2D Arrow Rotation on z angle
			//player Camera Relative Position From Any Target Object
			Vector3 cameraRelative = TargetDestination_Points.InverseTransformPoint (targetDestination.position);
			//Getting Angle in Radian between x and z 
			float radianAngle = Mathf.Atan2 (cameraRelative.x, cameraRelative.z-3);

			//Converting Radian Angle to Degree Angle
			float degreeAngle = radianAngle* Mathf.Rad2Deg;

			//Roating Arrow according to the angle towards target
			arrow2D_Object.transform.localEulerAngles = new Vector3 (0,180, degreeAngle);

			#endregion

			distanceWithArrow_Text.text= Mathf.RoundToInt( totalDistance).ToString()+"m";

		}
		else
		{
			destinatonImage.gameObject.SetActive (true);

			arrowImage.gameObject.SetActive(false);

			if(totalDistance<3)
			{
				distanceOnDestination_Text.text=" ";
			}
			else
			{
				distanceOnDestination_Text.text= Mathf.RoundToInt( totalDistance).ToString()+"m";
			}

			distanceWithArrow_Text.gameObject.SetActive(false);

			#region Mapping of 3D object to 2D UI..
			//Mapping 3D Object to Screen Position for Canvas UI
			destinationPosition =TargetDestination_Points.GetComponent<Camera>().WorldToScreenPoint (targetDestination.position);
			destinatonImage.transform.position = new Vector3(destinationPosition.x,destinationPosition.y+50,0);
			#endregion
		}
		#endregion


	}
}
