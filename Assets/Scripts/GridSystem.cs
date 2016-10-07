using UnityEngine;
using System.Collections;

public class GridSystem : MonoBehaviour {
	public Transform[] GridTransforms;
	public  int rowNum;
	public  int colNum;
	public 	Color[] Colors;

	private int m_colorIdx = 0;
	private SpriteRenderer[] m_renderers;

	private Vector3 buttomLeft;

	void Start(){
		m_renderers = GetComponentsInChildren<SpriteRenderer> ();

		GameManager.instance.ColorChange += OnColorChange;
	}

	void OnDestroy(){
		GameManager.instance.ColorChange -= OnColorChange;
	}


	public Vector3 positionAt(int row, int col){
		return GridTransforms [row * colNum + col].position;
	}

	void OnColorChange(){
		int idx = 0;
		do {
			idx = Random.Range (0, Colors.Length - 1);
		} while (idx == m_colorIdx);
		m_colorIdx = idx;

		foreach (var renderer in m_renderers) {
			Color color = Colors [idx];
			color.a = renderer.color.a;
			renderer.color = color;
		}

	}



}
