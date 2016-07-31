using UnityEngine;
using System.Collections;

public class TestEventObject {
	private string name;
	public TestEventObject(string name){
		this.name = name;
	}

	void onFireEvent(object data){
		Debug.Log(name + " onFireEvent~~~~~" + data);
	}

	public void addEvent(){
		EventManager.sharedInstance().on("testEvent", onFireEvent); 
	}

	public void removeEvent(){
		EventManager.sharedInstance().clear("testEvent", onFireEvent); 
	}
}
