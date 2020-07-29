using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recticle : MonoBehaviour {

	public Camera Cam;

	private Vector3 originalScale;

	void Start(){
		originalScale = transform.localScale;
	}

	void Update () {

		RaycastHit hit;
		float distance;


		Ray ray = new Ray (Cam.transform.position, Cam.transform.forward);
		if (Physics.Raycast (ray, out hit)) 
		{
			distance = hit.distance;
		} 
		else 
		{
			distance = Cam.farClipPlane * 0.95f;
		}
		transform.LookAt (Cam.transform.position);
		transform.Rotate (0, 180, 0);

		transform.position = Cam.transform.position + Cam.transform.forward * distance;
		transform.localScale = originalScale * distance;
	}
}
