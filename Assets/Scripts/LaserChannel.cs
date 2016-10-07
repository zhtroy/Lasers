using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class LaserChannel : MonoBehaviour {
	public Laser[] Lasers;

	private int m_lastLaserIdx;
	
	public void TurnOn(float spawnDelay){
		int rand = Random.Range (0, Lasers.Length);
		m_lastLaserIdx = rand;
		Lasers [m_lastLaserIdx].TurnOn (spawnDelay);
	}

	public void TurnOff(){
		Lasers [m_lastLaserIdx].TurnOff ();
	}

}
