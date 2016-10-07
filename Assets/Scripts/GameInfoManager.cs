using UnityEngine;
using System.Collections;

public class GameInfoManager : MonoBehaviour {
	public static GameInfoManager instance = null;

	public int bestScore {
		get;
		set;
	}
	public int needTutorial {
		get;
		set;
	}

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		LoadGameInfo ();

		Input.multiTouchEnabled = false;
	
	}

	void OnDestroy(){
		SaveGameInfo ();
	}


	void SaveGameInfo(){
		PlayerPrefs.SetInt ("BestScore", bestScore);
		PlayerPrefs.SetInt ("NeedTutorial", needTutorial);
	}
	void LoadGameInfo(){
		bestScore = PlayerPrefs.GetInt ("BestScore", 0);
		needTutorial = PlayerPrefs.GetInt ("NeedTutorial", 1);
	}
		
}
