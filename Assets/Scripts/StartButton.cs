using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
	public Image Inner;
	public Image Outer;

	public Color[] Colors;
	private int m_lastIdx = 0;

	void Start(){
		
	}

	public void ChangeColor(){
		
		int idx = 0;
		do {
			idx = Random.Range (0, Colors.Length - 1);
		} while(idx == m_lastIdx);
		m_lastIdx = idx;
		var color = Colors [idx];
		color.a = Inner.color.a;
		Inner.color = color;
		color.a = Outer.color.a;
		Outer.color = color;

	}
}
