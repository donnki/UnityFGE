using UnityEngine;
using System.Collections;

public class TestDBManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		DBManager.sharedInstance().initDBConnection("Data Source="+Application.streamingAssetsPath+"/data.db");
		DBManager.sharedInstance().loadTable("i18n");
		DBManager.sharedInstance().dumpCache();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
