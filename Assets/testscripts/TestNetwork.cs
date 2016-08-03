using UnityEngine;
using System.Collections;
using player;
using System.IO;

public class TestNetwork : SceneControl {

	ByteBuffer buf;
	void Start () {
		buf = ByteBuffer.Allocate(10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void testByteBuff(){
		
		buf.WriteInt(10000);
		buf.WriteStringUshort("hello测试！！");
		buf.WriteByte(10);
		buf.WriteShort(1000);
		buf.WriteStringUshort("测试中文abc中文！！");
		buf.WriteStringUshort("测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！测试中文abc中文！！");

		Debug.Log("-----------------------------");

		Debug.Log(buf.ReadInt());
		Debug.Log(buf.ReadStringUShort());
		Debug.Log(buf.ReadByte());
		Debug.Log(buf.ReadShort());
		Debug.Log(buf.ReadStringUShort());
		Debug.Log(buf.ReadStringUShort());

	}

	void testProtobufEncode(){
		Player proto = new Player();
		proto.id = 12345;
		proto.name = "1玩家12345";
		proto.level = 1;
		proto.cups = 0;
		proto.sheild = 0;
		var t = new Player.PveState();
		t.id = 123;
		t.best = 1;
		t.star = 2;
		proto.pveState.Add(t);

		using (MemoryStream ms = new MemoryStream())
		{   
			ProtoBuf.Serializer.Serialize<Player>(ms, proto);
			byte[] result = new byte[ms.Length];
			//将流的位置设为0，起始点
			ms.Position = 0;
			//将流中的内容读取到二进制数组中
			ms.Read (result, 0, result.Length);

			buf.WriteBytes(result);
		}
	}

	void testProtobufDecode(){
		int len = buf.ReadableBytes();
		byte[] result = new byte[len];
		buf.ReadBytes(result, 0, len);
		using (MemoryStream ms = new MemoryStream()) {
			//将消息写入流中
			ms.Write (result, 0, result.Length);
			//将流的位置归0
			ms.Position = 0;
			//使用工具反序列化对象
			Player player = ProtoBuf.Serializer.Deserialize<Player> (ms);
			EventManager.sharedInstance().fire("LOGEVENT", 
				"***Player信息***\nid:" + player.id + ",\n"
				+ "name:" + player.name + ",\n"
				+ "level:" + player.level + ",\n"
				+ "cups:" + player.cups + ",\n"
				+ "sheild:" + player.sheild + ",\n"
				+ "pve state id:" + player.pveState[0].id + ",\n"
				+ "pve state best:" + player.pveState[0].best);
		}
	}


	void OnGUI(){
		if(GUI.Button(new Rect(100,100,200,50), "测试ByteBuff")){
			testByteBuff();
		}
		if(GUI.Button(new Rect(100,200,200,50), "测试Protobuf编码")){
			testProtobufEncode();
		}
		if(GUI.Button(new Rect(100,300,200,50), "测试Protobuf解码")){
			testProtobufDecode();
		}
	}
}
