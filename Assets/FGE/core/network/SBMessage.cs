using System;


public class SBMessage
{
	private short packetId; 					//消息ID
	private ByteBuffer buffer;					//字节缓冲区
	public ByteBuffer Buffer{
		get {return buffer;}
	}


	public SBMessage(short packetId){
		this.packetId = packetId;
		buffer = ByteBuffer.Allocate(10);
	}

	/// <summary>
	///	一条SBMessage网络消息包内容约定：
	/// 包长度|包ID|包内容 
	/// </summary>
	/// <returns>The from.</returns>
	/// <param name="data">Data.</param>
	public static SBMessage parseFrom(byte[] data){
		ByteBuffer buf = ByteBuffer.Allocate(data.Length);
		buf.WriteBytes(data);
		buf.ReadShort(); //包长度，此处直接忽略
		short packetId = buf.ReadShort();
		byte[] bytes = buf.ReadRemain();
		SBMessage msg = new SBMessage(packetId);
		msg.WriteBytes(bytes);
		return msg;
	}

	public byte[] ToNetworkBytes(){
		buffer.ResetReaderIndex();
		ByteBuffer buf = ByteBuffer.Allocate(buffer.ReadableBytes() + 2*sizeof(short));
		buf.WriteShort((short)buffer.ReadableBytes());
		buf.WriteShort(packetId);
		buf.WriteBytes(buffer.ReadRemain());
		buf.ResetReaderIndex();
		return buf.ReadRemain();
	}

	public SBMessage WriteBytes(byte[] bytes){
		buffer.WriteBytes(bytes);
		return this;
	}

	public byte[] ReadBytes(byte[] bytes){
		buffer.ReadBytes(bytes, 0, bytes.Length);
		return bytes;
	}

	public override string ToString(){
		return "MessageID:" + packetId + "\n" + BitConverter.ToString(buffer.ToArray());
	}
}

