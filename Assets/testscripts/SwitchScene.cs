using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {
	public UnityEngine.UI.Text logger;

	public void ChangeScene(string sceneName){
		GameEngine.sharedInstance().changeScene(sceneName);
	}

	void Awake(){
		EventManager.sharedInstance().on("LOGEVENT", this.LogEvent);
	}

	void OnDestroy(){
		EventManager.sharedInstance().clear("LOGEVENT", this.LogEvent);
	}

	void LogEvent(object logText){
		logger.text = logger.text + "\n" + logText;
	}
}
