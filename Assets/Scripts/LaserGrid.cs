using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.Events;



public class LaserGrid : MonoBehaviour {
	public bool Debug;
	public ScreenEffects Effects;
	public PlayerController Player;
	public float ShootPlayerChance;
	const float LaserSpawnTime = 1f;
	public float LaserSpawnDelay;
	const float LaserDieTime = 0.25f;
	public float LaserHoldTime;
	public float LaserInterval;
	public int MaxTotalShoots;
	public int MinTotalShoots;
	public List<LaserChannel> VerticalChannels;
	public List<LaserChannel> HorizontalChannels;
	public GridSystem Grid;

	// Use this for initialization
	void Start (){
		GameManager.instance.ColorChange += OnColorChange;
	}

	void OnDestroy(){
		GameManager.instance.ColorChange -= OnColorChange;
	}
	void OnColorChange(){
		if (!Debug) {
			int score = GameManager.instance.score;
			if (score >= 5) {
				MinTotalShoots = 2;
				MaxTotalShoots = 3;
			} 
			if (score >= 10) {
				MinTotalShoots = 3;
				MaxTotalShoots = 3;
			} 
			if (score >= 20) {
				MinTotalShoots = 3;
				MaxTotalShoots = 4;
			} 
			if (score >= 50) {
				LaserSpawnDelay = 0.3f;
			}
		}
	}

	public void Begin(float time){
		Invoke ("StartShooting", time);
	}
	void StartShooting(){
		StartCoroutine (SpawnWaveRandom ());

	}
		
	IEnumerator SpawnWaveRandom(){
		int colShoots;
		int rowShoots;
		int[] rows;
		int[] cols;
		int[] lastRows = new int[0];
		int[] lastCols = new int[0];
		bool sameRow;
		bool sameCol;

		while (!GameManager.instance.gameEndo) {



			do {
				
				//col and row shoot number
				do {
					colShoots = Random.Range (0, Grid.colNum);
					rowShoots = Random.Range (0, Grid.rowNum);
				} while(colShoots+rowShoots<MinTotalShoots || colShoots+rowShoots>MaxTotalShoots);

				rows = new int[rowShoots];
				cols = new int[colShoots];

				bool rowSameAsPlayer = Random.value < ShootPlayerChance ? true: false;
				bool colSameAsPlayer = Random.value< ShootPlayerChance? true: false;
			

				//generate rows
				for (int i = 0; i < rowShoots; i++) {
					int j;
					int rand;
					if(i ==0 && rowSameAsPlayer){
						rows[i] = Player.row;
					}else{
						do {
							rand = Random.Range (0, HorizontalChannels.Count);
							for (j = 0; j < i; j++) {
								if (rows [j] == rand) {
									break;
								}
							}
						} while(rand == Player.row || j != i);
						rows[i] = rand;
					}
		
				}

				//generate cols
				for (int i = 0; i < colShoots; i++) {
					int j;
					int rand;
					if(i==0 &&colSameAsPlayer){
						cols[i] = Player.col;
					}else{
						do {
							rand = Random.Range (0, VerticalChannels.Count);
							for (j = 0; j < i; j++) {
								if (cols [j] == rand) {
									break;
								}
							}
						} while(rand == Player.col || j!= i);
						cols[i] = rand;
					}
				}

				//compare with last round
				sameCol = false;
				sameRow = false;

				Array.Sort(cols);
				if(cols.Length == lastCols.Length){
					int i;
					for(i = 0;i<cols.Length;i++){
						if(cols[i]!= lastCols[i]){
							break;
						}
					}
					if(i == cols.Length){
						sameCol = true;
					}
				}

				Array.Sort(rows);
				if(rows.Length == lastRows.Length){
					int i;
					for(i = 0;i<rows.Length;i++){
						if(rows[i]!=lastRows[i]){
							break;
						}
					}
					if(i == rows.Length){
						sameRow = true;
					}
				}


			} while(sameRow && sameCol);

			lastCols = (int[]) cols.Clone ();
			lastRows = (int[]) rows.Clone ();

			//turn on laser
			for (int j = 0; j < rows.Length; j++) {
				int row = rows [j];
				if (row < HorizontalChannels.Count && row >= 0) {
					HorizontalChannels [row].TurnOn (LaserSpawnDelay);
				}
			}
			for (int j = 0; j < cols.Length; j++) {
				int col = cols [j];
				if (col < VerticalChannels.Count && col >= 0) {
					VerticalChannels [col].TurnOn (LaserSpawnDelay);
				}
			}
			Invoke ("OnLaserSpawned", LaserSpawnTime+ LaserSpawnDelay);
			yield return new WaitForSeconds (LaserSpawnTime +LaserSpawnDelay + LaserHoldTime);

			//turn off laser
			for (int j = 0; j < rows.Length; j++) {
				int row = rows [j];
				if (row < HorizontalChannels.Count && row >= 0) {
					HorizontalChannels [row].TurnOff ();
				}
			}
			for (int j = 0; j < cols.Length; j++) {
				int col = cols [j];
				if (col < VerticalChannels.Count && col >= 0) {
					VerticalChannels [col].TurnOff ();
				}
			}

			yield return new WaitForSeconds (LaserInterval+LaserDieTime);
		}

	}
		
		

	void OnLaserSpawned(){
		GameManager.instance.score++;
		Effects.CameraShakeWeak ();
	}
		
}
