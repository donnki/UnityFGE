using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// 游戏控制根结点Manager of Managers中的第一个Manager
/// 可以通过GameEngine访问到任意一个子Manager
/// </summary>
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

	public DBManager DB{
		get { return DBManager.sharedInstance(); }
	}
	public EventManager Events{
		get { return EventManager.sharedInstance(); }
	}

	public void changeScene(string sceneName){
		preSceneName = this.Scene.SceneName;
		this.Scene.changeScene(sceneName);
	}

}
