using UnityEngine;
using System.Collections;
using battle;
using ProtoBuf;
using System.IO;
using System.Runtime.InteropServices;  
public class TestProtobuf : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Debug.Log("persistentDataPath: " + Application.persistentDataPath);
		Debug.Log("dataPath: " + Application.dataPath);
//		saveProtobuf();
		loadProtobuf();
	}

	public string savePath {
		get{
			string path = Application.persistentDataPath;
			return path;
		}
	}
	public void saveProtobuf(){
		Battle b = new Battle();
		b.id = 11231234L;
		b.destory = 2;
		b.star = 3;
		b.attacker = 1231214L;
		b.defender = 12312315L;
		using (FileStream f = new FileStream(savePath + "/battleproto.dat", FileMode.OpenOrCreate))
		{   
			Debug.Log("~~~~~");
			ProtoBuf.Serializer.Serialize<Battle>(f, b);
     		}
	}

	public void loadProtobuf(){
//		TextAsset objectAsset = Resources.Load("battle", typeof(TextAsset)) as TextAsset;
//		using(MemoryStream m = new MemoryStream(objectAsset.bytes))
//		{   
//
//			Battle b2 = ProtoBuf.Serializer.Deserialize<Battle>(m);
//			Debug.Log("~~~~" + b2.id+", " + b2.attacker);
//		}
//
		using(FileStream f = new FileStream(savePath + "/battleproto.dat", FileMode.Open))
		{
			Battle b2 = ProtoBuf.Serializer.Deserialize<Battle>(f);
			Debug.Log("~~~~" + b2.id+", " + b2.attacker);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
