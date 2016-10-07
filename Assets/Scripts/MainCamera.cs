using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public float FixedScreenWidth = 2f;
	private Camera m_mainCamera;
	void Awake(){
		m_mainCamera = GetComponent<Camera> ();
		SetCameraSize ();
	}
		
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetCameraSize(){
		m_mainCamera.orthographicSize = FixedScreenWidth/m_mainCamera.aspect/2f;
	}
	
}
