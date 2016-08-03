using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using player;
using System.IO;
using System;

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

		ushort len = buf.ReadUshort();
		byte[] bytes = buf.ReadRemain();
		Debug.Log(len + "------" + System.Text.Encoding.UTF8.GetString(bytes));

	}

	void testSBMessage(){
		SBMessage msg = new SBMessage(1024);

		msg.Buffer.WriteInt(10000);
		msg.Buffer.WriteStringUshort("hello测试！！");
		msg.Buffer.WriteByte(10);
		msg.Buffer.WriteShort(1000);
		msg.Buffer.WriteStringUshort("测试中文abc中文！！");

		Player proto = new Player();
		proto.id = 12345;
		proto.exp = 10;
		proto.name = "1玩家12345";
		proto.level = 1;
		proto.cups = 20;
		proto.sheild = 120;

		msg.WriteProto<Player>(proto);
		Debug.Log(msg.Buffer);
  		
		SBMessage _msg = SBMessage.parseFrom(msg.ToNetworkBytes());
		Debug.Log(_msg.Buffer);
		_msg.Buffer.ReadInt();
		_msg.Buffer.ReadStringUShort();
		_msg.Buffer.ReadByte();
		_msg.Buffer.ReadShort();
		_msg.Buffer.ReadStringUShort();
		Player player = _msg.ReadProto<Player>();
		EventManager.sharedInstance().fire("LOGEVENT", 
			"***Player信息***\nid:" + player.id + ",\n"
			+ "name:" + player.name + ",\n"
			+ "exp:" + player.exp + ",\n"
			+ "level:" + player.level + ",\n"
			+ "cups:" + player.cups + ",\n"
			+ "sheild:" + player.sheild + ",\n");
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
		if(GUI.Button(new Rect(100,200,200,50), "测试SBMessage编码")){
			testSBMessage();
		}
		if(GUI.Button(new Rect(100,300,200,50), "测试Protobuf编码")){
			testProtobufEncode();
		}
		if(GUI.Button(new Rect(100,400,200,50), "测试Protobuf解码")){
			testProtobufDecode();
		}
	}
}
