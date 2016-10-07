using UnityEngine;
using System.Collections;
using System.Net.Mime;
using System.IO;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour {
	public float BounceTime;
	public float BounceDistance;
	public float MinSwipeDistance;
	public float MaxSwipeTime;

	public float DampTime;
	public ParticleSystem DieParticle;
	public int startRow;
	public int startCol;
	public GridSystem gridSystem;
	public Color[] Colors;
	private int m_colorIdx = 0;
	private SpriteRenderer[] m_renderers;
	private Vector3 m_positionToGo;
	private Vector3 m_currentVelocity = Vector3.zero;

	private Vector2 m_swipeStartPos;
	private float m_swipeStartTime;
	private bool m_isSwipping;
	public int row {
		get;
		private set;
	}
	public int col {
		get;
		private set;
	}
	// Use this for initialization
	void Start () {
		transform.position =m_positionToGo = gridSystem.positionAt (startRow, startCol);

		row = startRow;
		col = startCol;
		m_renderers = GetComponentsInChildren<SpriteRenderer> ();
		GameManager.instance.ColorChange += OnColorChange;
	}

	void OnDestroy(){
		GameManager.instance.ColorChange -= OnColorChange;
	}
	// Update is called once per frame
	void Update () {
		int deltaCol = 0;
		int deltaRow = 0;
		#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			deltaCol--;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			deltaCol++;
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			deltaRow++;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			deltaRow--;
		}

		if (Input.touchCount > 0) {
			var touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {

				m_swipeStartPos = touch.position;
				m_swipeStartTime = Time.time;
				m_isSwipping = true;
			}
			if (touch.phase == TouchPhase.Ended) {
				if (m_isSwipping) {
					Vector2 position = touch.position;
					Vector2 delta = position - m_swipeStartPos;
					Debug.Log("swipe distance:" + delta.magnitude);
					Debug.Log("swipe time:" + (Time.time - m_swipeStartTime));
					if (delta.magnitude > MinSwipeDistance && (Time.time - m_swipeStartTime) < MaxSwipeTime) {
						if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
							if (position.x > m_swipeStartPos.x) {// swipe right
								deltaCol++;
							} else { //left
								deltaCol--;
							}
						} else {
							if (position.y > m_swipeStartPos.y) { //swipe up
								deltaRow++;
							} else {//down
								deltaRow--;
							}
						}
					}
					m_isSwipping = false;
				}
			}

			if (touch.phase == TouchPhase.Canceled) {
				m_isSwipping = false;
			}
		}
		#else
		#if UNITY_IOS
	
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {

				m_swipeStartPos = touch.position;
				m_swipeStartTime = Time.time;
				m_isSwipping = true;
			}
			if (touch.phase == TouchPhase.Ended) {
				if (m_isSwipping) {
					Vector2 position = touch.position;
					Vector2 delta = position - m_swipeStartPos;
					Debug.Log("swipe distance:" + delta.magnitude);
					Debug.Log("swipe time:" + (Time.time - m_swipeStartTime));
					if (delta.magnitude > MinSwipeDistance && (Time.time - m_swipeStartTime) < MaxSwipeTime) {
						if (Mathf.Abs (delta.x) > Mathf.Abs (delta.y)) {
							if (position.x > m_swipeStartPos.x) {// swipe right
								deltaCol++;
							} else { //left
								deltaCol--;
							}
						} else {
							if (position.y > m_swipeStartPos.y) { //swipe up
								deltaRow++;
							} else {//down
								deltaRow--;
							}
						}
					}
					m_isSwipping = false;
				}
			}

			if (touch.phase == TouchPhase.Canceled) {
				m_isSwipping = false;
			}
		}
		#endif
		#endif	
		if (deltaCol != 0 || deltaRow != 0) {
			col += deltaCol;
			row += deltaRow;
			int colInGrid = Mathf.Clamp (col, 0, gridSystem.colNum - 1);
			int rowInGrid = Mathf.Clamp (row, 0, gridSystem.rowNum - 1);
			if (colInGrid == col && rowInGrid == row) {
				m_positionToGo = gridSystem.positionAt (rowInGrid, colInGrid);
			} else {
				Vector3 bounce = new Vector3 (deltaCol, deltaRow, 0)*BounceDistance;
				StartCoroutine (BounceAtGridEdge (gridSystem.positionAt (rowInGrid, colInGrid), bounce));
			}

			row = rowInGrid;
			col = colInGrid;
		}



		transform.position= Vector3.SmoothDamp (transform.position, m_positionToGo,ref m_currentVelocity, DampTime);

	}

	IEnumerator BounceAtGridEdge(Vector3 OriginPos, Vector3 Bounce){
		m_positionToGo = OriginPos+ Bounce;
		yield return new WaitForSeconds (BounceTime);
		m_positionToGo = OriginPos;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Laser")){
			Debug.Log ("game over");
			OnGameOver ();
		
		}
	}

	void OnGameOver(){
		ParticleSystem die = (ParticleSystem)Instantiate (DieParticle, transform.position, Quaternion.identity);
		die.startColor = GetComponent<SpriteRenderer>().color;
		gameObject.SetActive (false);
		GameManager.instance.OnGameOver ();
	
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
