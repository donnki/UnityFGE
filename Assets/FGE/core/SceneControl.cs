using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneControl : MonoBehaviour {
	private bool isLoadingNextScene;
	private AsyncOperation loadingOperation;

	void Awake(){
		GameEngine.sharedInstance().Scene = this;
		_sceneName = SceneManager.GetActiveScene().name;
	}
	
	private string _sceneName;
	public string SceneName{
		get { 
			return _sceneName; 
		}
	}

	public void changeScene(string sceneName){
		isLoadingNextScene = true;
		StartCoroutine("LoadScene", sceneName);
	}

	IEnumerator LoadScene (string scene_name)  
	{  
		loadingOperation = SceneManager.LoadSceneAsync (scene_name);  
		yield return loadingOperation;  
	}  

	public float getLoadingProgress(){
		if(isLoadingNextScene){
			return loadingOperation.progress;
		}else{
			return 0;
		}
	}


}
