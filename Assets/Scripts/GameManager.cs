using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
	public GameObject NewRecordText;
	public Button PauseButton;
	public Button ResumeButton;
	public PlayerController Player;
	public LaserGrid Grid;
	public GameObject[] Tutorials;
	public ScreenEffects screenEffects;
	public ResultPanel resultPanel;
	public int[] ColorChangeScores;
	private int m_colorIdx = 0;

	public static GameManager instance = null;



	public delegate void NumberChangeHandler<T>(T number);
	public delegate void ColorChangeHandler();

	public bool gameEndo {
		get;
		set;
	}
	private bool m_autoPause;
	public event NumberChangeHandler<int> ScoreChange;
	public event ColorChangeHandler ColorChange;

	private int _score;
	public int score {
		get {
			return _score;
		}
		set {
			if (!gameEndo) {
				_score = value;
				if (_score > GameInfoManager.instance.bestScore) {
					GameInfoManager.instance.bestScore = _score;
					NewRecordText.SetActive (true);
				}
				if (ScoreChange != null) {
					ScoreChange (_score);
				}
				if (m_colorIdx < ColorChangeScores.Length && _score >= ColorChangeScores [m_colorIdx]) {
					m_colorIdx++;
					ColorChange ();
				}

			}
		}
	}



	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

		if (GameInfoManager.instance.needTutorial == 1) {
			GameInfoManager.instance.needTutorial = 0;
			StartCoroutine (DoTutorial ());
		} else {
			Tutorials[0].SetActive (false);
			Grid.Begin (1f);
		}
	}
		
	IEnumerator DoTutorial(){
		int idx = 0;
		yield return new WaitForSeconds (0.5f);
		yield return new WaitUntil (() => (Player.col != Player.startCol || Player.row != Player.startRow));
		Tutorials[idx].SetActive (false);
		idx++;
		Tutorials [idx].SetActive (true);
		yield return new WaitForSeconds (1.5f);
		Grid.Begin (0);
		yield return new WaitForSeconds (1f);
		Tutorials [idx].SetActive (false);



	}

		
	public void SetTimeScale(float timeScale){
		Time.timeScale = timeScale;
	}
		
	public void OnGameOver(){
		gameEndo = true;
		StartCoroutine (DoGameOver ());

	}

	IEnumerator DoGameOver(){
		yield return StartCoroutine( screenEffects.CameraShakeStrong ());
		resultPanel.ShowResult ();
	}

	void OnApplicationPause(bool pauseState){
		if (pauseState ) {
			if (PauseButton.IsActive()) {
				PauseButton.onClick.Invoke ();
				m_autoPause = true;
				Debug.Log ("pause");
			}
		} else {
			if (m_autoPause && ResumeButton.IsActive()) {
				ResumeButton.onClick.Invoke ();
				m_autoPause = false;
				Debug.Log ("resume");
			}
		}
	}
			
}
