using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour {
	public GameObject PauseButton;
	public Text BestScoreText;

	private Animator m_animator;
	void Start(){
		m_animator = GetComponent<Animator> ();
	}
	public void ShowResult(){
		PauseButton.SetActive (false);
		BestScoreText.text = "Best " + GameInfoManager.instance.bestScore.ToString ();
		BestScoreText.gameObject.SetActive (true);
		m_animator.SetTrigger ("Show");
	}

}
