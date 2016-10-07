using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionOut : MonoBehaviour {
	public bool TransitionToSelf = false;
	public string NextScene;
	public float Duration;
	public Color TargetColor;

	void Start(){
		Image image = GetComponent<Image> ();
		image.CrossFadeColor (Color.clear, 0f, false, true);
		image.CrossFadeColor (TargetColor, Duration,false, true);
		Invoke ("LoadNextScene", Duration);
	}
	void LoadNextScene(){
		if (TransitionToSelf) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		} else {
			if (NextScene == null) {
				Debug.LogWarning ("next scene empty");
				return;
			}
			SceneManager.LoadScene (NextScene);
		}
	}


}
