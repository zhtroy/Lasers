using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class HurrayText : MonoBehaviour {
	[Serializable]
	public class Hurray{
		public String content;
		public int interval;
	}
	public Hurray[] Hurrays;

	private Animator m_animator;
	private Text m_text;
	private int m_counter;
	private int m_hurrayIdx;
	// Use this for initialization
	void Start () {
		m_text = GetComponent<Text> ();
		m_animator = GetComponent<Animator> ();
		m_hurrayIdx = Random.Range (0, Hurrays.Length);
		GameManager.instance.ScoreChange += OnScoreChange;
	}

	void OnDestroy(){
		GameManager.instance.ScoreChange -= OnScoreChange;
	}

	void OnScoreChange(int score){

		if (m_counter >= Hurrays [m_hurrayIdx].interval) {
			m_counter = 0;
			m_text.text = Hurrays [m_hurrayIdx].content;
			m_hurrayIdx = Random.Range (0, Hurrays.Length);
			m_animator.SetTrigger ("show");
		} else {

			m_counter++;
		}


	}

}
