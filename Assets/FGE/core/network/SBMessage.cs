using System;
using System.IO;
using System.Security.Cryptography;

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

	/// <summary>
	/// TripleDES Encrypts the message.
	/// </summary>
	/// <returns>加密后的message.</returns>
	/// <param name="msg">加密前的SBMessage</param>
	/// <param name="key">24字节的key字符串</param>
	public static SBMessage encryptMessage(SBMessage msg, string key){
		byte[] plaintextBuffer =  msg.Buffer.ToArray();

		TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
		{
			Key = System.Text.Encoding.Default.GetBytes(key),
			Mode = CipherMode.ECB,
			Padding = PaddingMode.PKCS7
		};
		ICryptoTransform cTransform = tdes.CreateEncryptor();
		byte[] resultArray = cTransform.TransformFinalBlock(plaintextBuffer, 0, plaintextBuffer.Length);
		tdes.Clear();

		SBMessage _msg = new SBMessage(msg.packetId);
		_msg.WriteBytes(resultArray);
		return _msg;
	}

	/// <summary>
	/// TripleDES Decrypts the message.
	/// </summary>
	/// <returns>解密后的message</returns>
	/// <param name="msg">解密前的SBMessage</param>
	/// <param name="key">24字节的key字符串</param>
	public static SBMessage decryptMessage(SBMessage msg, string key){
		byte[] plaintextBuffer =  msg.Buffer.ToArray();

		TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
		{
			Key = System.Text.Encoding.Default.GetBytes(key),
			Mode = CipherMode.ECB,
			Padding = PaddingMode.PKCS7
		};

		ICryptoTransform cTransform = tdes.CreateDecryptor();
		byte[] resultArray = cTransform.TransformFinalBlock(plaintextBuffer, 0, plaintextBuffer.Length);
		tdes.Clear();

		SBMessage _msg = new SBMessage(msg.packetId);
		_msg.WriteBytes(resultArray);
		return _msg;
	}

	public SBMessage WriteBytes(byte[] bytes){
		buffer.WriteBytes(bytes);
		return this;
	}

	public byte[] ReadBytes(byte[] bytes){
		buffer.ReadBytes(bytes, 0, bytes.Length);
		return bytes;
	}

	public SBMessage WriteProto<T>(T proto){
		using (MemoryStream ms = new MemoryStream())
		{   
			ProtoBuf.Serializer.Serialize<T>(ms, proto);
			byte[] result = new byte[ms.Length];
			//将流的位置设为0，起始点
			ms.Position = 0;
			//将流中的内容读取到二进制数组中
			ms.Read (result, 0, result.Length);
			buffer.WriteShort((short)result.Length); 		//先写入proto的长度short
			buffer.WriteBytes(result);				//再写入proto的字节码
		}
		return this;
	}

	public T ReadProto<T>(){
		short len = buffer.ReadShort();
		byte[] result = new byte[len];
		buffer.ReadBytes(result, 0, len);
		using (MemoryStream ms = new MemoryStream()) {
			//将消息写入流中
			ms.Write (result, 0, result.Length);
			//将流的位置归0
			ms.Position = 0;
			//使用工具反序列化对象
			T obj = ProtoBuf.Serializer.Deserialize<T> (ms);
			return obj;
		}
	}

	public override string ToString(){
		return "MessageID:" + packetId + "\n" + BitConverter.ToString(buffer.ToArray());
	}
}

