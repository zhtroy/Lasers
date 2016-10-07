using UnityEngine;
using System.Collections;

public class SoundToggle : MonoBehaviour {

	public void ToggleSound(){
		SoundManager.instance.ToggleVolume ();
	}
}
