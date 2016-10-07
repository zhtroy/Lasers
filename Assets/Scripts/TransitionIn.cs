using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionIn : MonoBehaviour {
	public Color StartColor;
	public float Duration;
	// Use this for initialization
	void Start () {
		Image image = GetComponent<Image> ();
		image.color = StartColor;
		image.CrossFadeAlpha (0f, Duration, false);
		Destroy (gameObject, Duration);
	}

}
