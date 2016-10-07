using UnityEngine;
using System.Collections;

public class ShareButton : MonoBehaviour {

	public void ShareToSNS(){
		Application.CaptureScreenshot ("screenShot.png");
	}
}
