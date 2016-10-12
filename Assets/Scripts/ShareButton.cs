using UnityEngine;
using System.Collections;

public class ShareButton : MonoBehaviour {
	private NativeShare shareTool;


	void Start(){
		shareTool = GetComponent<NativeShare> ();
	}
	public void ShareToSNS(){
		string shareText;
		if (Application.systemLanguage == SystemLanguage.Chinese) {
			shareText = "我在#Lasers#里达到了" + GameManager.instance.score + "分，你也来试试吧。";
		} else {
			shareText = "I scored " + GameManager.instance.score + " in the game #Lasers#";
		}
		string url;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			url = "https://www.apple.com";
		} else if (Application.platform == RuntimePlatform.Android) {
			url = "https://www.baidu.com";
		} else {
			url = "";
		}
		shareTool.ShareScreenshotWithTextAndUrl (shareText, url);
			
	}
}