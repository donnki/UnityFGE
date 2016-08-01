using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameEngine {
	private volatile static GameEngine  _instance = null;
	private static readonly object lockHelper = new object();

	/// <summary>
	/// 单例的GameEngine
	/// </summary>
	public static GameEngine sharedInstance()
	{
		if(_instance == null)
		{
			lock(lockHelper)
			{
				if(_instance == null)
					_instance = new GameEngine();
			}
		}
		return _instance;
	}

	private string preSceneName;
	public string PreScene{
		get {return preSceneName;}
	}
	private SceneControl _scene;
	public SceneControl Scene{
		get { return _scene; }
		set { _scene = value; }
	}

	public void changeScene(string sceneName){
		preSceneName = this.Scene.SceneName;
		this.Scene.changeScene(sceneName);
	}

}
