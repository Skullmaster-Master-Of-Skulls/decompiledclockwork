using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x02000559 RID: 1369
	public class NetworkStream : Stream
	{
		// Token: 0x0600296D RID: 10605 RVA: 0x000AD369 File Offset: 0x000AC369
		internal NetworkStream()
		{
			this.m_OwnsSocket = true;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000AD38D File Offset: 0x000AC38D
		public NetworkStream(Socket socket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000AD3C0 File Offset: 0x000AC3C0
		public NetworkStream(Socket socket, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, FileAccess.ReadWrite);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000AD3FC File Offset: 0x000AC3FC
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

		// Token: 0x06002971 RID: 10609 RVA: 0x000AD448 File Offset: 0x000AC448
		public NetworkStream(Socket socket, FileAccess access)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000AD47B File Offset: 0x000AC47B
		public NetworkStream(Socket socket, FileAccess access, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			this.InitNetworkStream(socket, access);
			this.m_OwnsSocket = ownsSocket;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000AD4B5 File Offset: 0x000AC4B5
		protected Socket Socket
		{
			get
			{
				return this.m_StreamSocket;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000AD4C0 File Offset: 0x000AC4C0
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

		// Token: 0x06002975 RID: 10613 RVA: 0x000AD4F1 File Offset: 0x000AC4F1
		internal void ConvertToNotSocketOwner()
		{
			this.m_OwnsSocket = false;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000AD500 File Offset: 0x000AC500
		// (set) Token: 0x06002977 RID: 10615 RVA: 0x000AD508 File Offset: 0x000AC508
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

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x000AD511 File Offset: 0x000AC511
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x000AD519 File Offset: 0x000AC519
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000AD522 File Offset: 0x000AC522
		public override bool CanRead
		{
			get
			{
				return this.m_Readable;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000AD52A File Offset: 0x000AC52A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000AD52D File Offset: 0x000AC52D
		public override bool CanWrite
		{
			get
			{
				return this.m_Writeable;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000AD535 File Offset: 0x000AC535
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000AD538 File Offset: 0x000AC538
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x000AD566 File Offset: 0x000AC566
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
					throw new ArgumentOutOfRangeException(SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value, false);
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000AD58C File Offset: 0x000AC58C
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x000AD5BA File Offset: 0x000AC5BA
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
					throw new ArgumentOutOfRangeException(SR.GetString("net_io_timeout_use_gt_zero"));
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value, false);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000AD5E0 File Offset: 0x000AC5E0
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

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000AD641 File Offset: 0x000AC641
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000AD652 File Offset: 0x000AC652
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x000AD663 File Offset: 0x000AC663
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

		// Token: 0x06002986 RID: 10630 RVA: 0x000AD674 File Offset: 0x000AC674
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000AD688 File Offset: 0x000AC688
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

		// Token: 0x06002988 RID: 10632 RVA: 0x000AD71C File Offset: 0x000AC71C
		internal bool PollRead()
		{
			if (this.m_CleanedUp)
			{
				return false;
			}
			Socket streamSocket = this.m_StreamSocket;
			return streamSocket != null && streamSocket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000AD748 File Offset: 0x000AC748
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

		// Token: 0x0600298A RID: 10634 RVA: 0x000AD7A8 File Offset: 0x000AC7A8
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
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
			if (!this.CanRead)
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

		// Token: 0x0600298B RID: 10635 RVA: 0x000AD8B4 File Offset: 0x000AC8B4
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
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
			if (!this.CanWrite)
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000AD9F4 File Offset: 0x000AC9F4
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this.m_CloseTimeout = timeout;
			this.Close();
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000ADA14 File Offset: 0x000ACA14
		protected override void Dispose(bool disposing)
		{
			if (!this.m_CleanedUp && disposing && this.m_StreamSocket != null)
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
			this.m_CleanedUp = true;
			base.Dispose(disposing);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000ADA78 File Offset: 0x000ACA78
		~NetworkStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000ADAA8 File Offset: 0x000ACAA8
		internal bool Connected
		{
			get
			{
				Socket streamSocket = this.m_StreamSocket;
				return !this.m_CleanedUp && streamSocket != null && streamSocket.Connected;
			}
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000ADAD4 File Offset: 0x000ACAD4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
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
			if (!this.CanRead)
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
			catch
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000ADC24 File Offset: 0x000ACC24
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanRead)
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
			catch
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000ADD28 File Offset: 0x000ACD28
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
			catch
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000ADE2C File Offset: 0x000ACE2C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
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
			if (!this.CanWrite)
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000ADF7C File Offset: 0x000ACF7C
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanWrite)
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000AE090 File Offset: 0x000AD090
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000AE18C File Offset: 0x000AD18C
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
				buffers = this.ConcatenateBuffersOnWin9x(buffers);
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000AE278 File Offset: 0x000AD278
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
				buffers = this.ConcatenateBuffersOnWin9x(buffers);
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000AE370 File Offset: 0x000AD370
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
				buffers = this.ConcatenateBuffersOnWin9x(buffers);
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000AE468 File Offset: 0x000AD468
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
			catch
			{
				throw new IOException(SR.GetString("net_io_writefailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000AE548 File Offset: 0x000AD548
		private BufferOffsetSize[] ConcatenateBuffersOnWin9x(BufferOffsetSize[] buffers)
		{
			if (ComNetOS.IsWin9x && buffers.Length > 16)
			{
				BufferOffsetSize[] array = new BufferOffsetSize[16];
				for (int i = 0; i < 16; i++)
				{
					array[i] = buffers[i];
				}
				int num = 0;
				for (int i = 15; i < buffers.Length; i++)
				{
					num += buffers[i].Size;
				}
				if (num > 0)
				{
					array[15] = new BufferOffsetSize(new byte[num], 0, num, false);
					num = 0;
					for (int i = 15; i < buffers.Length; i++)
					{
						Buffer.BlockCopy(buffers[i].Buffer, buffers[i].Offset, array[15].Buffer, num, buffers[i].Size);
						num += buffers[i].Size;
					}
				}
				buffers = array;
			}
			return buffers;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000AE5FD File Offset: 0x000AD5FD
		public override void Flush()
		{
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000AE5FF File Offset: 0x000AD5FF
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000AE610 File Offset: 0x000AD610
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

		// Token: 0x04002879 RID: 10361
		private Socket m_StreamSocket;

		// Token: 0x0400287A RID: 10362
		private bool m_Readable;

		// Token: 0x0400287B RID: 10363
		private bool m_Writeable;

		// Token: 0x0400287C RID: 10364
		private bool m_OwnsSocket;

		// Token: 0x0400287D RID: 10365
		private int m_CloseTimeout = -1;

		// Token: 0x0400287E RID: 10366
		private bool m_CleanedUp;

		// Token: 0x0400287F RID: 10367
		private int m_CurrentReadTimeout = -1;

		// Token: 0x04002880 RID: 10368
		private int m_CurrentWriteTimeout = -1;
	}
}
