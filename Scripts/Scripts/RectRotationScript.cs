using UnityEngine;
using System.Collections;

public class RectRotationScript : MonoBehaviour {
	[Range(-10,10)]
	public float rotationSpeed;


	private RectTransform newRect;


	void Start()
	{
		newRect = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		newRect.Rotate (0, 0, rotationSpeed);
	
	}
}
