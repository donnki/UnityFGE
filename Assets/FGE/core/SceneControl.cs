using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// 场景控制的基类，所有的场景入口应该由此类派生出去。
/// </summary>
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
