using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200001A RID: 26
	internal abstract class SocketHandler
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00004707 File Offset: 0x00003707
		private SocketHandler()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004724 File Offset: 0x00003724
		public SocketHandler(Socket socket, Stream netStream)
		{
			this._beginReadCallback = new AsyncCallback(this.BeginReadMessageCallback);
			this._creationTime = DateTime.UtcNow;
			this.NetSocket = socket;
			this.NetStream = netStream;
			this._dataBuffer = CoreChannel.BufferPool.GetBuffer();
			this._dataBufferSize = this._dataBuffer.Length;
			this._dataOffset = 0;
			this._dataCount = 0;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000047A1 File Offset: 0x000037A1
		internal SocketHandler(Socket socket, RequestQueue requestQueue, Stream netStream) : this(socket, netStream)
		{
			this._requestQueue = requestQueue;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000047B2 File Offset: 0x000037B2
		public DateTime CreationTime
		{
			get
			{
				return this._creationTime;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000047BA File Offset: 0x000037BA
		public bool RaceForControl()
		{
			return 1 == Interlocked.Exchange(ref this._controlCookie, 0);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000047CE File Offset: 0x000037CE
		public void ReleaseControl()
		{
			this._controlCookie = 1;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000047D8 File Offset: 0x000037D8
		internal bool IsLocalhost()
		{
			if (this.NetSocket == null || this.NetSocket.RemoteEndPoint == null)
			{
				return true;
			}
			IPAddress address = ((IPEndPoint)this.NetSocket.RemoteEndPoint).Address;
			return IPAddress.IsLoopback(address) || CoreChannel.IsLocalIpAddress(address);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004824 File Offset: 0x00003824
		internal bool IsLocal()
		{
			if (this.NetSocket == null)
			{
				return true;
			}
			IPAddress address = ((IPEndPoint)this.NetSocket.RemoteEndPoint).Address;
			return IPAddress.IsLoopback(address);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004858 File Offset: 0x00003858
		internal bool CustomErrorsEnabled()
		{
			bool result;
			try
			{
				result = RemotingConfiguration.CustomErrorsEnabled(this.IsLocalhost());
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060000AA RID: 170
		protected abstract void PrepareForNewMessage();

		// Token: 0x060000AB RID: 171 RVA: 0x0000488C File Offset: 0x0000388C
		protected virtual void SendErrorMessageIfPossible(Exception e)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000488E File Offset: 0x0000388E
		public virtual void OnInputStreamClosed()
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004890 File Offset: 0x00003890
		public virtual void Close()
		{
			if (this._requestQueue != null)
			{
				this._requestQueue.ScheduleMoreWorkIfNeeded();
			}
			if (this.NetStream != null)
			{
				this.NetStream.Close();
				this.NetStream = null;
			}
			if (this.NetSocket != null)
			{
				this.NetSocket.Close();
				this.NetSocket = null;
			}
			if (this._dataBuffer != null)
			{
				CoreChannel.BufferPool.ReturnBuffer(this._dataBuffer);
				this._dataBuffer = null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004903 File Offset: 0x00003903
		public WaitCallback DataArrivedCallback
		{
			set
			{
				this._dataArrivedCallback = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000490C File Offset: 0x0000390C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004914 File Offset: 0x00003914
		public object DataArrivedCallbackState
		{
			get
			{
				return this._dataArrivedCallbackState;
			}
			set
			{
				this._dataArrivedCallbackState = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000491D File Offset: 0x0000391D
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004925 File Offset: 0x00003925
		public WindowsIdentity ImpersonationIdentity
		{
			get
			{
				return this._impersonationIdentity;
			}
			set
			{
				this._impersonationIdentity = value;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004930 File Offset: 0x00003930
		public void BeginReadMessage()
		{
			bool flag = false;
			try
			{
				if (this._requestQueue != null)
				{
					this._requestQueue.ScheduleMoreWorkIfNeeded();
				}
				this.PrepareForNewMessage();
				if (this._dataCount == 0)
				{
					this._beginReadAsyncResult = this.NetStream.BeginRead(this._dataBuffer, 0, this._dataBufferSize, this._beginReadCallback, null);
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception e)
			{
				this.CloseOnFatalError(e);
			}
			catch
			{
				this.CloseOnFatalError(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
			}
			if (flag)
			{
				if (this._requestQueue != null)
				{
					this._requestQueue.ProcessNextRequest(this);
				}
				else
				{
					this.ProcessRequestNow();
				}
				this._beginReadAsyncResult = null;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000049F0 File Offset: 0x000039F0
		public void BeginReadMessageCallback(IAsyncResult ar)
		{
			bool flag = false;
			try
			{
				this._beginReadAsyncResult = null;
				this._dataOffset = 0;
				this._dataCount = this.NetStream.EndRead(ar);
				if (this._dataCount <= 0)
				{
					this.Close();
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception e)
			{
				this.CloseOnFatalError(e);
			}
			catch
			{
				this.CloseOnFatalError(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
			}
			if (flag)
			{
				if (this._requestQueue != null)
				{
					this._requestQueue.ProcessNextRequest(this);
					return;
				}
				this.ProcessRequestNow();
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004A90 File Offset: 0x00003A90
		internal void CloseOnFatalError(Exception e)
		{
			try
			{
				this.SendErrorMessageIfPossible(e);
				this.Close();
			}
			catch
			{
				try
				{
					this.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004AD8 File Offset: 0x00003AD8
		internal void ProcessRequestNow()
		{
			try
			{
				WaitCallback dataArrivedCallback = this._dataArrivedCallback;
				if (dataArrivedCallback != null)
				{
					dataArrivedCallback(this);
				}
			}
			catch (Exception e)
			{
				this.CloseOnFatalError(e);
			}
			catch
			{
				this.CloseOnFatalError(new Exception(CoreChannel.GetResourceString("Remoting_nonClsCompliantException")));
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B38 File Offset: 0x00003B38
		internal void RejectRequestNowSinceServerIsBusy()
		{
			this.CloseOnFatalError(new RemotingException(CoreChannel.GetResourceString("Remoting_ServerIsBusy")));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004B4F File Offset: 0x00003B4F
		public int ReadByte()
		{
			if (this.Read(this._byteBuffer, 0, 1) != -1)
			{
				return (int)this._byteBuffer[0];
			}
			return -1;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004B6C File Offset: 0x00003B6C
		public void WriteByte(byte value, Stream outputStream)
		{
			this._byteBuffer[0] = value;
			outputStream.Write(this._byteBuffer, 0, 1);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004B85 File Offset: 0x00003B85
		public ushort ReadUInt16()
		{
			this.Read(this._byteBuffer, 0, 2);
			return (ushort)((int)(this._byteBuffer[0] & byte.MaxValue) | (int)this._byteBuffer[1] << 8);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004BB0 File Offset: 0x00003BB0
		public void WriteUInt16(ushort value, Stream outputStream)
		{
			this._byteBuffer[0] = (byte)value;
			this._byteBuffer[1] = (byte)(value >> 8);
			outputStream.Write(this._byteBuffer, 0, 2);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004BD8 File Offset: 0x00003BD8
		public int ReadInt32()
		{
			this.Read(this._byteBuffer, 0, 4);
			return (int)(this._byteBuffer[0] & byte.MaxValue) | (int)this._byteBuffer[1] << 8 | (int)this._byteBuffer[2] << 16 | (int)this._byteBuffer[3] << 24;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004C25 File Offset: 0x00003C25
		public void WriteInt32(int value, Stream outputStream)
		{
			this._byteBuffer[0] = (byte)value;
			this._byteBuffer[1] = (byte)(value >> 8);
			this._byteBuffer[2] = (byte)(value >> 16);
			this._byteBuffer[3] = (byte)(value >> 24);
			outputStream.Write(this._byteBuffer, 0, 4);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004C68 File Offset: 0x00003C68
		protected bool ReadAndMatchFourBytes(byte[] buffer)
		{
			this.Read(this._byteBuffer, 0, 4);
			return this._byteBuffer[0] == buffer[0] && this._byteBuffer[1] == buffer[1] && this._byteBuffer[2] == buffer[2] && this._byteBuffer[3] == buffer[3];
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004CC0 File Offset: 0x00003CC0
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (this._dataCount > 0)
			{
				int num2 = Math.Min(this._dataCount, count);
				StreamHelper.BufferCopy(this._dataBuffer, this._dataOffset, buffer, offset, num2);
				this._dataCount -= num2;
				this._dataOffset += num2;
				count -= num2;
				offset += num2;
				num += num2;
			}
			while (count > 0)
			{
				if (count < 256)
				{
					this.BufferMoreData();
					int num3 = Math.Min(this._dataCount, count);
					StreamHelper.BufferCopy(this._dataBuffer, this._dataOffset, buffer, offset, num3);
					this._dataCount -= num3;
					this._dataOffset += num3;
					count -= num3;
					offset += num3;
					num += num3;
				}
				else
				{
					int num4 = this.ReadFromSocket(buffer, offset, count);
					count -= num4;
					offset += num4;
					num += num4;
				}
			}
			return num;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004DA4 File Offset: 0x00003DA4
		private int BufferMoreData()
		{
			int num = this.ReadFromSocket(this._dataBuffer, 0, this._dataBufferSize);
			this._dataOffset = 0;
			this._dataCount = num;
			return num;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004DD4 File Offset: 0x00003DD4
		private int ReadFromSocket(byte[] buffer, int offset, int count)
		{
			int num = this.NetStream.Read(buffer, offset, count);
			if (num <= 0)
			{
				throw new RemotingException(CoreChannel.GetResourceString("Remoting_Socket_UnderlyingSocketClosed"));
			}
			return num;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004E05 File Offset: 0x00003E05
		protected byte[] ReadToByte(byte b)
		{
			return this.ReadToByte(b, null);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004E10 File Offset: 0x00003E10
		protected byte[] ReadToByte(byte b, ValidateByteDelegate validator)
		{
			byte[] array = null;
			if (this._dataCount == 0)
			{
				this.BufferMoreData();
			}
			int num = this._dataOffset + this._dataCount;
			int dataOffset = this._dataOffset;
			int num2 = dataOffset;
			bool flag = false;
			while (!flag)
			{
				bool flag2 = num2 == num;
				flag = (!flag2 && this._dataBuffer[num2] == b);
				if (validator != null && !flag2 && !flag && !validator(this._dataBuffer[num2]))
				{
					throw new RemotingException(CoreChannel.GetResourceString("Remoting_Http_InvalidDataReceived"));
				}
				if (flag2 || flag)
				{
					int num3 = num2 - dataOffset;
					if (array == null)
					{
						array = new byte[num3];
						StreamHelper.BufferCopy(this._dataBuffer, dataOffset, array, 0, num3);
					}
					else
					{
						int num4 = array.Length;
						byte[] array2 = new byte[num4 + num3];
						StreamHelper.BufferCopy(array, 0, array2, 0, num4);
						StreamHelper.BufferCopy(this._dataBuffer, dataOffset, array2, num4, num3);
						array = array2;
					}
					this._dataOffset += num3;
					this._dataCount -= num3;
					if (flag2)
					{
						this.BufferMoreData();
						num = this._dataOffset + this._dataCount;
						dataOffset = this._dataOffset;
						num2 = dataOffset;
					}
					else if (flag)
					{
						this._dataOffset++;
						this._dataCount--;
					}
				}
				else
				{
					num2++;
				}
			}
			return array;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004F61 File Offset: 0x00003F61
		protected string ReadToChar(char ch)
		{
			return this.ReadToChar(ch, null);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004F6C File Offset: 0x00003F6C
		protected string ReadToChar(char ch, ValidateByteDelegate validator)
		{
			byte[] array = this.ReadToByte((byte)ch, validator);
			if (array == null)
			{
				return null;
			}
			if (array.Length == 0)
			{
				return string.Empty;
			}
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004FA0 File Offset: 0x00003FA0
		public string ReadToEndOfLine()
		{
			string result = this.ReadToChar('\r');
			if (this.ReadByte() == 10)
			{
				return result;
			}
			return null;
		}

		// Token: 0x0400009C RID: 156
		protected Socket NetSocket;

		// Token: 0x0400009D RID: 157
		protected Stream NetStream;

		// Token: 0x0400009E RID: 158
		private DateTime _creationTime;

		// Token: 0x0400009F RID: 159
		private RequestQueue _requestQueue;

		// Token: 0x040000A0 RID: 160
		private byte[] _dataBuffer;

		// Token: 0x040000A1 RID: 161
		private int _dataBufferSize;

		// Token: 0x040000A2 RID: 162
		private int _dataOffset;

		// Token: 0x040000A3 RID: 163
		private int _dataCount;

		// Token: 0x040000A4 RID: 164
		private AsyncCallback _beginReadCallback;

		// Token: 0x040000A5 RID: 165
		private IAsyncResult _beginReadAsyncResult;

		// Token: 0x040000A6 RID: 166
		private WaitCallback _dataArrivedCallback;

		// Token: 0x040000A7 RID: 167
		private object _dataArrivedCallbackState;

		// Token: 0x040000A8 RID: 168
		private WindowsIdentity _impersonationIdentity;

		// Token: 0x040000A9 RID: 169
		private byte[] _byteBuffer = new byte[4];

		// Token: 0x040000AA RID: 170
		private int _controlCookie = 1;
	}
}
