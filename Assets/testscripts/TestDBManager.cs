using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestDBManager : SceneControl {

	// Use this for initialization
	void Start () {
		System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
		stopwatch.Start(); // 开始监视代码运行时间
		DBManager.sharedInstance().initConnection(Application.streamingAssetsPath, "data.db");
		stopwatch.Start(); // 开始监视代码运行时间
		Debug.Log(string.Format("创建数据库连接所花时间: {0} ms", stopwatch.ElapsedMilliseconds));
		EventManager.sharedInstance().fire("LOGEVENT", string.Format("创建数据库连接所花时间: {0} ms", stopwatch.ElapsedMilliseconds));

//		DBManager.sharedInstance().loadTable("i18n");
//		DBManager.sharedInstance().dumpCache();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(GUI.Button(new Rect(100,100,200,50), "测试加载I18N表")){
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start(); // 开始监视代码运行时间
			int count = DBManager.sharedInstance().loadTable("i18n").Count;
			stopwatch.Stop(); // 开始监视代码运行时间
			Debug.Log(string.Format("读取i18n整张表所花时间: {0} ms，共{1}条记录", stopwatch.ElapsedMilliseconds, count));
			EventManager.sharedInstance().fire("LOGEVENT", string.Format("读取i18n整张表所花时间: {0} ms，共{1}条记录", stopwatch.ElapsedMilliseconds, count));
		}

		if(GUI.Button(new Rect(100,200,200,50), "测试读取I18N表其中一条数据")){
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start(); // 开始监视代码运行时间
			DBManager.sharedInstance().findByID("i18n", "d_sk_2092");  //读过一次后加入缓存
			stopwatch.Stop(); // 停止监视		
			Debug.Log(string.Format("测试读取I18N表其中一条数据: {0} ms", stopwatch.ElapsedMilliseconds));
			EventManager.sharedInstance().fire("LOGEVENT", string.Format("测试读取I18N表其中一条数据: {0} ms", stopwatch.ElapsedMilliseconds));
		}

		if(GUI.Button(new Rect(100,300,200,50), "打印数据库缓存")){
			string s = DBManager.sharedInstance().dumpCache();
			EventManager.sharedInstance().fire("LOGEVENT", s);
		}
	}
}
