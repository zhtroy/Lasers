using UnityEngine;
using System.Collections;

public class ScreenEffects : MonoBehaviour {

	 int m_shakeTimes;
	 float m_shakeStrength;
	 float m_shakeInterval;
	 float m_shakeSpeed;
	private Vector2 m_positonToGo;


	public IEnumerator CameraShakeStrong(){
		m_shakeTimes =20;
		m_shakeStrength = 0.06f;
		m_shakeInterval = 0.01f;
		m_shakeSpeed = 50;
		yield return StartCoroutine (CameraShake ());
	}

	public void CameraShakeWeak(){
		m_shakeTimes = 10;
		m_shakeStrength = 0.02f;
		m_shakeInterval = 0.01f;
		m_shakeSpeed = 50;
		StartCoroutine (CameraShake ());
	}
	IEnumerator CameraShake(){
		StopCoroutine ("DoCameraShake");
		yield return StartCoroutine (DoCameraShake ());
	}

	IEnumerator DoCameraShake(){
		for(int i = 0;i<m_shakeTimes;i++){
			m_positonToGo = Random.insideUnitCircle * m_shakeStrength;
			yield return new WaitForSeconds(m_shakeInterval);
		}
		m_positonToGo = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 position = Vector2.Lerp ((Vector2)transform.position, m_positonToGo, Time.deltaTime * m_shakeSpeed);
		transform.position = new Vector3 (position.x, position.y, transform.position.z);
	}
}
