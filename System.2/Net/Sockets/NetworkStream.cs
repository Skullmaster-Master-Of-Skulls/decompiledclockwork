using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x02000370 RID: 880
	public class NetworkStream : Stream
	{
		// Token: 0x06001FED RID: 8173 RVA: 0x000953FC File Offset: 0x000935FC
		internal NetworkStream()
		{
			this.m_OwnsSocket = true;
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x00095420 File Offset: 0x00093620
		public NetworkStream(Socket socket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00095453 File Offset: 0x00093653
		public NetworkStream(Socket socket, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00095490 File Offset: 0x00093690
		internal NetworkStream(NetworkStream networkStream, bool ownsSocket)
		{
			Socket socket = networkStream.Socket;
			if (socket == null)
			{
				throw new ArgumentNullException("networkStream");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000954DC File Offset: 0x000936DC
		public NetworkStream(Socket socket, FileAccess access)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0009550F File Offset: 0x0009370F
		public NetworkStream(Socket socket, FileAccess access, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x00095549 File Offset: 0x00093749
		protected Socket Socket
		{
			get
			{
				return this.m_StreamSocket;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x00095554 File Offset: 0x00093754
		internal Socket InternalSocket
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				if (this.m_CleanedUp || streamSocket == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return streamSocket;
			}
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00095588 File Offset: 0x00093788
		internal void InternalAbortSocket()
		{
			if (!this.m_OwnsSocket)
			{
				throw new InvalidOperationException();
			}
			Socket streamSocket = this.m_StreamSocket;
			if (this.m_CleanedUp || streamSocket == null)
			{
				return;
			}
			try
			{
				streamSocket.Close(0);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000955D8 File Offset: 0x000937D8
		internal void ConvertToNotSocketOwner()
		{
			this.m_OwnsSocket = false;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x000955E7 File Offset: 0x000937E7
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x000955EF File Offset: 0x000937EF
		protected bool Readable
		{
			get
			{
				return this.m_Readable;
			}
			set
			{
				this.m_Readable = value;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x000955F8 File Offset: 0x000937F8
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x00095600 File Offset: 0x00093800
		protected bool Writeable
		{
			get
			{
				return this.m_Writeable;
			}
			set
			{
				this.m_Writeable = value;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00095609 File Offset: 0x00093809
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x00095611 File Offset: 0x00093811
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00095614 File Offset: 0x00093814
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0009561C File Offset: 0x0009381C
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00095620 File Offset: 0x00093820
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x0009564E File Offset: 0x0009384E
		public override int ReadTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value, false);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x00095678 File Offset: 0x00093878
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x000956A6 File Offset: 0x000938A6
		public override int WriteTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value, false);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x000956D0 File Offset: 0x000938D0
		public virtual bool DataAvailable
		{
			get
			{
				if (this.m_CleanedUp)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				Socket streamSocket = this.m_StreamSocket;
				if (streamSocket == null)
				{
					throw new IOException(SR.GetString("net_io_readfailure", new object[]
					{
						SR.GetString("net_io_connectionclosed")
					}));
				}
				return streamSocket.Available != 0;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0009572E File Offset: 0x0009392E
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x0009573F File Offset: 0x0009393F
		// (set) Token: 0x06002006 RID: 8198 RVA: 0x00095750 File Offset: 0x00093950
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00095761 File Offset: 0x00093961
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00095774 File Offset: 0x00093974
		internal void InitNetworkStream(Socket socket, FileAccess Access)
		{
			if (!socket.Blocking)
			{
				throw new IOException(SR.GetString("net_sockets_blocking"));
			}
			if (!socket.Connected)
			{
				throw new IOException(SR.GetString("net_notconnected"));
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new IOException(SR.GetString("net_notstream"));
			}
			this.m_StreamSocket = socket;
			switch (Access)
			{
			case FileAccess.Read:
				this.m_Readable = true;
				return;
			case FileAccess.Write:
				this.m_Writeable = true;
				return;
			}
			this.m_Readable = true;
			this.m_Writeable = true;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00095808 File Offset: 0x00093A08
		internal bool PollRead()
		{
			if (this.m_CleanedUp)
			{
				return false;
			}
			Socket streamSocket = this.m_StreamSocket;
			return streamSocket != null && streamSocket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00095838 File Offset: 0x00093A38
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			return streamSocket.Poll(microSeconds, mode);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00095898 File Offset: 0x00093A98
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			int result;
			try
			{
				int num = streamSocket.Receive(buffer, offset, size, SocketFlags.None);
				result = num;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000959A4 File Offset: 0x00093BA4
		public override void Write(byte[] buffer, int offset, int size)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(SR.GetString("net_readonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			try
			{
				streamSocket.Send(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00095AA8 File Offset: 0x00093CA8
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this.m_CloseTimeout = timeout;
			this.Close();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00095AC8 File Offset: 0x00093CC8
		protected override void Dispose(bool disposing)
		{
			bool cleanedUp = this.m_CleanedUp;
			this.m_CleanedUp = true;
			if (!cleanedUp && disposing && this.m_StreamSocket != null)
			{
				this.m_Readable = false;
				this.m_Writeable = false;
				if (this.m_OwnsSocket)
				{
					Socket streamSocket = this.m_StreamSocket;
					if (streamSocket != null)
					{
						streamSocket.InternalShutdown(SocketShutdown.Both);
						streamSocket.Close(this.m_CloseTimeout);
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00095B34 File Offset: 0x00093D34
		~NetworkStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00095B64 File Offset: 0x00093D64
		internal bool Connected
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				return !this.m_CleanedUp && streamSocket != null && streamSocket.Connected;
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00095B90 File Offset: 0x00093D90
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = streamSocket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
				result = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00095CA0 File Offset: 0x00093EA0
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = streamSocket.UnsafeBeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
				result = asyncResult;
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00095D64 File Offset: 0x00093F64
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			int result;
			try
			{
				int num = streamSocket.EndReceive(asyncResult);
				result = num;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00095E20 File Offset: 0x00094020
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(SR.GetString("net_readonlystream"));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = streamSocket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
				result = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00095F30 File Offset: 0x00094130
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException(SR.GetString("net_readonlystream"));
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = streamSocket.UnsafeBeginSend(buffer, offset, size, SocketFlags.None, callback, state);
				result = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00096008 File Offset: 0x00094208
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			try
			{
				streamSocket.EndSend(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000960C4 File Offset: 0x000942C4
		internal virtual void MultipleWrite(BufferOffsetSize[] buffers)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			try
			{
				streamSocket.MultipleSend(buffers, SocketFlags.None);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00096164 File Offset: 0x00094364
		internal virtual IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = streamSocket.BeginMultipleSend(buffers, SocketFlags.None, callback, state);
				result = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00096208 File Offset: 0x00094408
		internal virtual IAsyncResult UnsafeBeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			if (buffers == null)
			{
				throw new ArgumentNullException("buffers");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = streamSocket.UnsafeBeginMultipleSend(buffers, SocketFlags.None, callback, state);
				result = asyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000962AC File Offset: 0x000944AC
		internal virtual void EndMultipleWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					SR.GetString("net_io_connectionclosed")
				}));
			}
			try
			{
				streamSocket.EndMultipleSend(asyncResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					ex.Message
				}), ex);
			}
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0009634C File Offset: 0x0009454C
		public override void Flush()
		{
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0009634E File Offset: 0x0009454E
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00096355 File Offset: 0x00094555
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00096368 File Offset: 0x00094568
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			if (timeout < 0)
			{
				timeout = 0;
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				return;
			}
			if ((mode == SocketShutdown.Send || mode == SocketShutdown.Both) && timeout != this.m_CurrentWriteTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout, silent);
				this.m_CurrentWriteTimeout = timeout;
			}
			if ((mode == SocketShutdown.Receive || mode == SocketShutdown.Both) && timeout != this.m_CurrentReadTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout, silent);
				this.m_CurrentReadTimeout = timeout;
			}
		}

		// Token: 0x04001DED RID: 7661
		private Socket m_StreamSocket;

		// Token: 0x04001DEE RID: 7662
		private bool m_Readable;

		// Token: 0x04001DEF RID: 7663
		private bool m_Writeable;

		// Token: 0x04001DF0 RID: 7664
		private bool m_OwnsSocket;

		// Token: 0x04001DF1 RID: 7665
		private int m_CloseTimeout = -1;

		// Token: 0x04001DF2 RID: 7666
		private volatile bool m_CleanedUp;

		// Token: 0x04001DF3 RID: 7667
		private int m_CurrentReadTimeout = -1;

		// Token: 0x04001DF4 RID: 7668
		private int m_CurrentWriteTimeout = -1;
	}
}
