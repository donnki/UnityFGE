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


public class TestEventManager : MonoBehaviour {
	public GameObject cube;
	// Use this for initialization
	private TestEventObject obj1, obj2;
	void Start () {
		obj1 = new TestEventObject("object 1");
		obj2 = new TestEventObject("object 2"); 
	}
	
	void Update () {
	 
	}

	void doTest(){
		EventManager.sharedInstance().fire("testEvent", new string[]{"~~~", "!!!!"});
	}

	void OnGUI() {
		if(GUI.Button(new Rect(100,100,200,50), "触发事件")){
			doTest();	
		}

		if(GUI.Button(new Rect(100,200,200,50), "增加OBJ1监听")){
			obj1.addEvent();
		}
		if(GUI.Button(new Rect(300,200,200,50), "移除OBJ1监听")){
			obj1.removeEvent();
		}

		if(GUI.Button(new Rect(100,300,200,50), "增加OBJ2监听")){
			obj2.addEvent();
		}
		if(GUI.Button(new Rect(300,300,200,50), "移除OBJ2监听")){
			obj2.removeEvent();
		}

		if(GUI.Button(new Rect(100,400,200,50), "清除全部事件")){
			EventManager.sharedInstance().clear("testEvent");
		}

	} 
		
}
