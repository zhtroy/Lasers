using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDownPanel : MonoBehaviour {

	public GameObject PauseButton;
	public Text CountDownText;

	void OnEnable(){
		StopCoroutine (DoCountDown ());
		StartCoroutine (DoCountDown ());
	}

	IEnumerator DoCountDown(){
		float lastTime;
		float currentTime;
		CountDownText.text = "3";
		lastTime = Time.realtimeSinceStartup;
		//delay 1s
		do {
			yield return null;
			currentTime = Time.realtimeSinceStartup;
		} while(currentTime - lastTime < 0.8f);
		lastTime = currentTime;

		CountDownText.text = "2";
		//delay 1s
		do {
			yield return null;
			currentTime = Time.realtimeSinceStartup;
		} while(currentTime - lastTime < 0.8f);
		lastTime = currentTime;

		CountDownText.text = "1";
		//delay 1s
		do {
			yield return null;
			currentTime = Time.realtimeSinceStartup;
		} while(currentTime - lastTime < 0.8f);

		gameObject.SetActive (false);
		PauseButton.SetActive (true);
		Time.timeScale = 1f;
	}
		


}
