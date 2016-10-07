using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameInfoManager.instance.needTutorial == 0) {
			Text text = GetComponent<Text> ();
			text.text = "Best " + GameInfoManager.instance.bestScore;
		} else {
			gameObject.SetActive (false);
		}
	}
}
