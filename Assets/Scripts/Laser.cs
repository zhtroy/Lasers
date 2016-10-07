using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Laser : MonoBehaviour {
	public GameObject Beam;
	public GameObject Glow;
	private Animator m_animator;
	private bool m_isOn;
	private float m_spawnDelay;
	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator> ();
	}
	
	public void TurnOn(float spawnDelay){
		if (!m_isOn) {
			m_spawnDelay = spawnDelay;
			m_animator.SetTrigger ("Spawn");
			m_isOn = true;
		}
	}

	void RealTurnOn(){
		Beam.SetActive (true);
		Glow.SetActive (true);
	}
	void SpawnFinish(){
		Invoke ("RealTurnOn", m_spawnDelay);
	}

	public void TurnOff(){
		if (m_isOn) {
			m_animator.SetTrigger ("Die");
			m_isOn = false;
			Beam.SetActive (false);
			Glow.SetActive (false);
		}
	}
}
