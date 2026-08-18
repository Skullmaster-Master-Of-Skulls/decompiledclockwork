using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001DC RID: 476
	internal class PooledStream : Stream
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x00062B27 File Offset: 0x00060D27
		internal PooledStream(object owner)
		{
			this.m_Owner = new WeakReference(owner);
			this.m_PooledCount = -1;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00062B5F File Offset: 0x00060D5F
		internal PooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime)
		{
			this.m_ConnectionPool = connectionPool;
			this.m_Lifetime = lifetime;
			this.m_CheckLifetime = checkLifetime;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00062B99 File Offset: 0x00060D99
		internal bool JustConnected
		{
			get
			{
				if (this.m_JustConnected)
				{
					this.m_JustConnected = false;
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00062BAD File Offset: 0x00060DAD
		internal IPAddress ServerAddress
		{
			get
			{
				return this.m_ServerAddress;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00062BB5 File Offset: 0x00060DB5
		internal bool IsInitalizing
		{
			get
			{
				return this.m_Initalizing;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00062BC0 File Offset: 0x00060DC0
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00062C09 File Offset: 0x00060E09
		internal bool CanBePooled
		{
			get
			{
				if (this.m_Initalizing)
				{
					return true;
				}
				if (!this.m_NetworkStream.Connected)
				{
					return false;
				}
				WeakReference owner = this.m_Owner;
				return !this.m_ConnectionIsDoomed && (owner == null || !owner.IsAlive);
			}
			set
			{
				this.m_ConnectionIsDoomed |= !value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00062C1C File Offset: 0x00060E1C
		internal bool IsEmancipated
		{
			get
			{
				WeakReference owner = this.m_Owner;
				return 0 >= this.m_PooledCount && (owner == null || !owner.IsAlive);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00062C50 File Offset: 0x00060E50
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x00062C78 File Offset: 0x00060E78
		internal object Owner
		{
			get
			{
				WeakReference owner = this.m_Owner;
				if (owner != null && owner.IsAlive)
				{
					return owner.Target;
				}
				return null;
			}
			set
			{
				lock (this)
				{
					if (this.m_Owner != null)
					{
						this.m_Owner.Target = value;
					}
				}
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00062CC4 File Offset: 0x00060EC4
		internal ConnectionPool Pool
		{
			get
			{
				return this.m_ConnectionPool;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00062CCC File Offset: 0x00060ECC
		internal virtual ServicePoint ServicePoint
		{
			get
			{
				return this.Pool.ServicePoint;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00062CD9 File Offset: 0x00060ED9
		internal bool Activate(object owningObject, GeneralAsyncDelegate asyncCallback)
		{
			return this.Activate(owningObject, asyncCallback != null, asyncCallback);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00062CE8 File Offset: 0x00060EE8
		protected bool Activate(object owningObject, bool async, GeneralAsyncDelegate asyncCallback)
		{
			bool result;
			try
			{
				if (this.m_Initalizing)
				{
					IPAddress serverAddress = null;
					this.m_AsyncCallback = asyncCallback;
					Socket connection = this.ServicePoint.GetConnection(this, owningObject, async, out serverAddress, ref this.m_AbortSocket, ref this.m_AbortSocket6);
					if (connection != null)
					{
						if (Logging.On)
						{
							Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_socket_connected", new object[]
							{
								connection.LocalEndPoint,
								connection.RemoteEndPoint
							}));
						}
						this.m_NetworkStream.InitNetworkStream(connection, FileAccess.ReadWrite);
						this.m_ServerAddress = serverAddress;
						this.m_Initalizing = false;
						this.m_JustConnected = true;
						this.m_AbortSocket = null;
						this.m_AbortSocket6 = null;
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					if (async && asyncCallback != null)
					{
						asyncCallback(owningObject, this);
					}
					result = true;
				}
			}
			catch
			{
				this.m_Initalizing = false;
				throw;
			}
			return result;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00062DC4 File Offset: 0x00060FC4
		internal void Deactivate()
		{
			this.m_AsyncCallback = null;
			if (!this.m_ConnectionIsDoomed && this.m_CheckLifetime)
			{
				this.CheckLifetime();
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00062DE4 File Offset: 0x00060FE4
		internal virtual void ConnectionCallback(object owningObject, Exception e, Socket socket, IPAddress address)
		{
			object state = null;
			if (e != null)
			{
				this.m_Initalizing = false;
				state = e;
			}
			else
			{
				try
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_socket_connected", new object[]
						{
							socket.LocalEndPoint,
							socket.RemoteEndPoint
						}));
					}
					this.m_NetworkStream.InitNetworkStream(socket, FileAccess.ReadWrite);
					state = this;
				}
				catch (Exception ex)
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					state = ex;
				}
				this.m_ServerAddress = address;
				this.m_Initalizing = false;
				this.m_JustConnected = true;
			}
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(owningObject, state);
			}
			this.m_AbortSocket = null;
			this.m_AbortSocket6 = null;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00062EA0 File Offset: 0x000610A0
		protected void CheckLifetime()
		{
			bool flag = !this.m_ConnectionIsDoomed;
			if (flag)
			{
				TimeSpan t = DateTime.UtcNow.Subtract(this.m_CreateTime);
				this.m_ConnectionIsDoomed = (0 < TimeSpan.Compare(this.m_Lifetime, t));
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00062EE4 File Offset: 0x000610E4
		internal void UpdateLifetime()
		{
			int connectionLeaseTimeout = this.ServicePoint.ConnectionLeaseTimeout;
			TimeSpan maxValue;
			if (connectionLeaseTimeout == -1)
			{
				maxValue = TimeSpan.MaxValue;
				this.m_CheckLifetime = false;
			}
			else
			{
				maxValue = new TimeSpan(0, 0, 0, 0, connectionLeaseTimeout);
				this.m_CheckLifetime = true;
			}
			if (maxValue != this.m_Lifetime)
			{
				this.m_Lifetime = maxValue;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00062F38 File Offset: 0x00061138
		internal void PrePush(object expectedOwner)
		{
			lock (this)
			{
				if (expectedOwner == null)
				{
					if (this.m_Owner != null && this.m_Owner.Target != null)
					{
						throw new InternalException();
					}
				}
				else if (this.m_Owner == null || this.m_Owner.Target != expectedOwner)
				{
					throw new InternalException();
				}
				this.m_PooledCount++;
				if (1 != this.m_PooledCount)
				{
					throw new InternalException();
				}
				if (this.m_Owner != null)
				{
					this.m_Owner.Target = null;
				}
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00062FD8 File Offset: 0x000611D8
		internal void PostPop(object newOwner)
		{
			lock (this)
			{
				if (this.m_Owner == null)
				{
					this.m_Owner = new WeakReference(newOwner);
				}
				else
				{
					if (this.m_Owner.Target != null)
					{
						throw new InternalException();
					}
					this.m_Owner.Target = newOwner;
				}
				this.m_PooledCount--;
				if (this.Pool != null)
				{
					if (this.m_PooledCount != 0)
					{
						throw new InternalException();
					}
				}
				else if (-1 != this.m_PooledCount)
				{
					throw new InternalException();
				}
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00063078 File Offset: 0x00061278
		protected bool UsingSecureStream
		{
			get
			{
				return this.m_NetworkStream is TlsStream;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00063088 File Offset: 0x00061288
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00063090 File Offset: 0x00061290
		internal NetworkStream NetworkStream
		{
			get
			{
				return this.m_NetworkStream;
			}
			set
			{
				this.m_Initalizing = false;
				this.m_NetworkStream = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x000630A0 File Offset: 0x000612A0
		protected Socket Socket
		{
			get
			{
				return this.m_NetworkStream.InternalSocket;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x000630AD File Offset: 0x000612AD
		public override bool CanRead
		{
			get
			{
				return this.m_NetworkStream.CanRead;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x000630BA File Offset: 0x000612BA
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x000630C7 File Offset: 0x000612C7
		public override bool CanWrite
		{
			get
			{
				return this.m_NetworkStream.CanWrite;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x000630D4 File Offset: 0x000612D4
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x000630E1 File Offset: 0x000612E1
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x000630EE File Offset: 0x000612EE
		public override int ReadTimeout
		{
			get
			{
				return this.m_NetworkStream.ReadTimeout;
			}
			set
			{
				this.m_NetworkStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x000630FC File Offset: 0x000612FC
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00063109 File Offset: 0x00061309
		public override int WriteTimeout
		{
			get
			{
				return this.m_NetworkStream.WriteTimeout;
			}
			set
			{
				this.m_NetworkStream.WriteTimeout = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x00063117 File Offset: 0x00061317
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00063124 File Offset: 0x00061324
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x00063131 File Offset: 0x00061331
		public override long Position
		{
			get
			{
				return this.m_NetworkStream.Position;
			}
			set
			{
				this.m_NetworkStream.Position = value;
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0006313F File Offset: 0x0006133F
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_NetworkStream.Seek(offset, origin);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00063150 File Offset: 0x00061350
		public override int Read(byte[] buffer, int offset, int size)
		{
			int result;
			try
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					int num = Interlocked.Increment(ref this.m_SynchronousIOClosingState);
					if ((num & 1610612736) != 0)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
				}
				result = this.m_NetworkStream.Read(buffer, offset, size);
			}
			finally
			{
				if (ServicePointManager.UseSafeSynchronousClose && 536870912 == Interlocked.Decrement(ref this.m_SynchronousIOClosingState))
				{
					try
					{
						this.TryCloseNetworkStream(false, 0);
					}
					catch
					{
					}
				}
			}
			return result;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000631E0 File Offset: 0x000613E0
		public override void Write(byte[] buffer, int offset, int size)
		{
			try
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					int num = Interlocked.Increment(ref this.m_SynchronousIOClosingState);
					if ((num & 1610612736) != 0)
					{
						throw new ObjectDisposedException(base.GetType().FullName);
					}
				}
				this.m_NetworkStream.Write(buffer, offset, size);
			}
			finally
			{
				if (ServicePointManager.UseSafeSynchronousClose && 536870912 == Interlocked.Decrement(ref this.m_SynchronousIOClosingState))
				{
					try
					{
						this.TryCloseNetworkStream(false, 0);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00063270 File Offset: 0x00061470
		internal void MultipleWrite(BufferOffsetSize[] buffers)
		{
			this.m_NetworkStream.MultipleWrite(buffers);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00063280 File Offset: 0x00061480
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.m_Owner = null;
					this.m_ConnectionIsDoomed = true;
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000632C0 File Offset: 0x000614C0
		private int InterlockedOr(ref int location1, int bitMask)
		{
			int num;
			do
			{
				num = Volatile.Read(ref location1);
			}
			while (num != Interlocked.CompareExchange(ref location1, num | bitMask, num));
			return num;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000632E4 File Offset: 0x000614E4
		private bool TryCloseNetworkStream(bool closeWithTimeout, int timeout)
		{
			if (!ServicePointManager.UseSafeSynchronousClose)
			{
				return false;
			}
			if (536870912 == Interlocked.CompareExchange(ref this.m_SynchronousIOClosingState, 1073741824, 536870912))
			{
				if (closeWithTimeout)
				{
					this.m_NetworkStream.Close(timeout);
				}
				else
				{
					this.m_NetworkStream.Close();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00063338 File Offset: 0x00061538
		private bool CancelPendingIoAndCloseIfSafe(bool closeWithTimeout, int timeout)
		{
			if (this.TryCloseNetworkStream(closeWithTimeout, timeout))
			{
				return true;
			}
			try
			{
				Socket internalSocket = this.m_NetworkStream.InternalSocket;
				UnsafeNclNativeMethods.CancelIoEx(internalSocket.SafeHandle, IntPtr.Zero);
			}
			catch
			{
			}
			return this.TryCloseNetworkStream(closeWithTimeout, timeout);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0006338C File Offset: 0x0006158C
		private void CloseConnectingSockets(bool useTimeout, int timeout)
		{
			Socket abortSocket = this.m_AbortSocket;
			Socket abortSocket2 = this.m_AbortSocket6;
			if (abortSocket != null)
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					try
					{
						UnsafeNclNativeMethods.CancelIoEx(abortSocket.SafeHandle, IntPtr.Zero);
					}
					catch
					{
					}
				}
				if (useTimeout)
				{
					abortSocket.Close(timeout);
				}
				else
				{
					abortSocket.Close();
				}
				this.m_AbortSocket = null;
			}
			if (abortSocket2 != null)
			{
				if (ServicePointManager.UseSafeSynchronousClose)
				{
					try
					{
						UnsafeNclNativeMethods.CancelIoEx(abortSocket2.SafeHandle, IntPtr.Zero);
					}
					catch
					{
					}
				}
				if (useTimeout)
				{
					abortSocket2.Close(timeout);
				}
				else
				{
					abortSocket2.Close();
				}
				this.m_AbortSocket6 = null;
			}
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00063438 File Offset: 0x00061638
		internal void CloseSocket()
		{
			if (!ServicePointManager.UseSafeSynchronousClose)
			{
				this.m_NetworkStream.Close();
			}
			else
			{
				this.InterlockedOr(ref this.m_SynchronousIOClosingState, 536870912);
				this.CancelPendingIoAndCloseIfSafe(false, 0);
			}
			this.CloseConnectingSockets(false, 0);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00063471 File Offset: 0x00061671
		public void Close(int timeout)
		{
			if (!ServicePointManager.UseSafeSynchronousClose)
			{
				this.m_NetworkStream.Close(timeout);
			}
			else
			{
				this.InterlockedOr(ref this.m_SynchronousIOClosingState, 536870912);
				this.CancelPendingIoAndCloseIfSafe(true, timeout);
			}
			this.CloseConnectingSockets(true, timeout);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000634AB File Offset: 0x000616AB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000634BF File Offset: 0x000616BF
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000634D3 File Offset: 0x000616D3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_NetworkStream.EndRead(asyncResult);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000634E1 File Offset: 0x000616E1
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000634F5 File Offset: 0x000616F5
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00063509 File Offset: 0x00061709
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndWrite(asyncResult);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00063517 File Offset: 0x00061717
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00063527 File Offset: 0x00061727
		internal void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndMultipleWrite(asyncResult);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00063535 File Offset: 0x00061735
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00063542 File Offset: 0x00061742
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.m_NetworkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00063550 File Offset: 0x00061750
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0006355E File Offset: 0x0006175E
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0006356E File Offset: 0x0006176E
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			return this.m_NetworkStream.Poll(microSeconds, mode);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0006357D File Offset: 0x0006177D
		internal bool PollRead()
		{
			return this.m_NetworkStream.PollRead();
		}

		// Token: 0x04001507 RID: 5383
		private const int ClosingFlag = 536870912;

		// Token: 0x04001508 RID: 5384
		private const int ClosedFlag = 1073741824;

		// Token: 0x04001509 RID: 5385
		private bool m_CheckLifetime;

		// Token: 0x0400150A RID: 5386
		private TimeSpan m_Lifetime;

		// Token: 0x0400150B RID: 5387
		private DateTime m_CreateTime;

		// Token: 0x0400150C RID: 5388
		private bool m_ConnectionIsDoomed;

		// Token: 0x0400150D RID: 5389
		private ConnectionPool m_ConnectionPool;

		// Token: 0x0400150E RID: 5390
		private WeakReference m_Owner;

		// Token: 0x0400150F RID: 5391
		private int m_PooledCount;

		// Token: 0x04001510 RID: 5392
		private bool m_Initalizing;

		// Token: 0x04001511 RID: 5393
		private IPAddress m_ServerAddress;

		// Token: 0x04001512 RID: 5394
		private NetworkStream m_NetworkStream;

		// Token: 0x04001513 RID: 5395
		private Socket m_AbortSocket;

		// Token: 0x04001514 RID: 5396
		private Socket m_AbortSocket6;

		// Token: 0x04001515 RID: 5397
		private bool m_JustConnected;

		// Token: 0x04001516 RID: 5398
		private int m_SynchronousIOClosingState;

		// Token: 0x04001517 RID: 5399
		private GeneralAsyncDelegate m_AsyncCallback;
	}
}
