using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	private Text m_text;
	private Animator m_animator;
	// Use this for initialization
	void Start () {
		m_text = GetComponent<Text> ();
		m_animator = GetComponent<Animator> ();
		GameManager.instance.ScoreChange += OnScoreChange;
	}

	void OnDestroy(){
		GameManager.instance.ScoreChange -= OnScoreChange;
	}

	void OnScoreChange(int number){
		m_text.text = number.ToString ();
		m_animator.SetTrigger ("Pop");
	}
}
