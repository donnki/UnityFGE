using UnityEngine;
using System.Collections;
using battle;
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
		Battle b = new Battle();
		b.id = 11231234L;
		b.destory = 2;
		b.star = 3;
		b.attacker = 1231214L;
		b.defender = 12312315L;

		using (MemoryStream ms = new MemoryStream())
		{   
			ProtoBuf.Serializer.Serialize<Battle>(ms, b);
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
			Battle battle = ProtoBuf.Serializer.Deserialize<Battle> (ms);
			Debug.Log("~~~~id:" + battle.id + ","
				+ "destory:" + battle.destory + ","
				+ "star:" + battle.star + ","
				+ "attacker:" + battle.attacker + ","
				+ "defender:" + battle.defender + ",");
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
