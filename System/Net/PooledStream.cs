using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x020004B9 RID: 1209
	internal class PooledStream : Stream
	{
		// Token: 0x06002562 RID: 9570 RVA: 0x000951BF File Offset: 0x000941BF
		internal PooledStream(object owner)
		{
			this.m_Owner = new WeakReference(owner);
			this.m_PooledCount = -1;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000951F7 File Offset: 0x000941F7
		internal PooledStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime)
		{
			this.m_ConnectionPool = connectionPool;
			this.m_Lifetime = lifetime;
			this.m_CheckLifetime = checkLifetime;
			this.m_Initalizing = true;
			this.m_NetworkStream = new NetworkStream();
			this.m_CreateTime = DateTime.UtcNow;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00095231 File Offset: 0x00094231
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

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x00095245 File Offset: 0x00094245
		internal IPAddress ServerAddress
		{
			get
			{
				return this.m_ServerAddress;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0009524D File Offset: 0x0009424D
		internal bool IsInitalizing
		{
			get
			{
				return this.m_Initalizing;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x00095258 File Offset: 0x00094258
		// (set) Token: 0x06002568 RID: 9576 RVA: 0x000952A1 File Offset: 0x000942A1
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

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x000952B4 File Offset: 0x000942B4
		internal bool IsEmancipated
		{
			get
			{
				WeakReference owner = this.m_Owner;
				return 0 >= this.m_PooledCount && (owner == null || !owner.IsAlive);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x000952E8 File Offset: 0x000942E8
		// (set) Token: 0x0600256B RID: 9579 RVA: 0x00095310 File Offset: 0x00094310
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

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x00095354 File Offset: 0x00094354
		internal ConnectionPool Pool
		{
			get
			{
				return this.m_ConnectionPool;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x0009535C File Offset: 0x0009435C
		internal virtual ServicePoint ServicePoint
		{
			get
			{
				return this.Pool.ServicePoint;
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00095369 File Offset: 0x00094369
		internal bool Activate(object owningObject, GeneralAsyncDelegate asyncCallback)
		{
			return this.Activate(owningObject, asyncCallback != null, -1, asyncCallback);
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x0009537C File Offset: 0x0009437C
		protected bool Activate(object owningObject, bool async, int timeout, GeneralAsyncDelegate asyncCallback)
		{
			bool result;
			try
			{
				if (this.m_Initalizing)
				{
					IPAddress serverAddress = null;
					this.m_AsyncCallback = asyncCallback;
					Socket connection = this.ServicePoint.GetConnection(this, owningObject, async, out serverAddress, ref this.m_AbortSocket, ref this.m_AbortSocket6, timeout);
					if (connection != null)
					{
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

		// Token: 0x06002570 RID: 9584 RVA: 0x00095424 File Offset: 0x00094424
		internal void Deactivate()
		{
			this.m_AsyncCallback = null;
			if (!this.m_ConnectionIsDoomed && this.m_CheckLifetime)
			{
				this.CheckLifetime();
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00095444 File Offset: 0x00094444
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
				catch
				{
					throw;
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

		// Token: 0x06002572 RID: 9586 RVA: 0x000954DC File Offset: 0x000944DC
		protected void CheckLifetime()
		{
			bool flag = !this.m_ConnectionIsDoomed;
			if (flag)
			{
				TimeSpan t = DateTime.UtcNow.Subtract(this.m_CreateTime);
				this.m_ConnectionIsDoomed = (0 < TimeSpan.Compare(this.m_Lifetime, t));
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00095520 File Offset: 0x00094520
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

		// Token: 0x06002574 RID: 9588 RVA: 0x00095574 File Offset: 0x00094574
		internal void Destroy()
		{
			this.m_Owner = null;
			this.m_ConnectionIsDoomed = true;
			this.Close(0);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0009558C File Offset: 0x0009458C
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

		// Token: 0x06002576 RID: 9590 RVA: 0x00095628 File Offset: 0x00094628
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

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x000956C0 File Offset: 0x000946C0
		protected bool UsingSecureStream
		{
			get
			{
				return this.m_NetworkStream is TlsStream;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000956D0 File Offset: 0x000946D0
		// (set) Token: 0x06002579 RID: 9593 RVA: 0x000956D8 File Offset: 0x000946D8
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

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000956E8 File Offset: 0x000946E8
		protected Socket Socket
		{
			get
			{
				return this.m_NetworkStream.InternalSocket;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x000956F5 File Offset: 0x000946F5
		public override bool CanRead
		{
			get
			{
				return this.m_NetworkStream.CanRead;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00095702 File Offset: 0x00094702
		public override bool CanSeek
		{
			get
			{
				return this.m_NetworkStream.CanSeek;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0009570F File Offset: 0x0009470F
		public override bool CanWrite
		{
			get
			{
				return this.m_NetworkStream.CanWrite;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0009571C File Offset: 0x0009471C
		public override bool CanTimeout
		{
			get
			{
				return this.m_NetworkStream.CanTimeout;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00095729 File Offset: 0x00094729
		// (set) Token: 0x06002580 RID: 9600 RVA: 0x00095736 File Offset: 0x00094736
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

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x00095744 File Offset: 0x00094744
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x00095751 File Offset: 0x00094751
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

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x0009575F File Offset: 0x0009475F
		public override long Length
		{
			get
			{
				return this.m_NetworkStream.Length;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0009576C File Offset: 0x0009476C
		// (set) Token: 0x06002585 RID: 9605 RVA: 0x00095779 File Offset: 0x00094779
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

		// Token: 0x06002586 RID: 9606 RVA: 0x00095787 File Offset: 0x00094787
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_NetworkStream.Seek(offset, origin);
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x00095798 File Offset: 0x00094798
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this.m_NetworkStream.Read(buffer, offset, size);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000957B5 File Offset: 0x000947B5
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.m_NetworkStream.Write(buffer, offset, size);
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000957C5 File Offset: 0x000947C5
		internal void MultipleWrite(BufferOffsetSize[] buffers)
		{
			this.m_NetworkStream.MultipleWrite(buffers);
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000957D4 File Offset: 0x000947D4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00095804 File Offset: 0x00094804
		internal void CloseSocket()
		{
			Socket abortSocket = this.m_AbortSocket;
			Socket abortSocket2 = this.m_AbortSocket6;
			this.m_NetworkStream.Close();
			if (abortSocket != null)
			{
				abortSocket.Close();
			}
			if (abortSocket2 != null)
			{
				abortSocket2.Close();
			}
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x0009583C File Offset: 0x0009483C
		public void Close(int timeout)
		{
			Socket abortSocket = this.m_AbortSocket;
			Socket abortSocket2 = this.m_AbortSocket6;
			this.m_NetworkStream.Close(timeout);
			if (abortSocket != null)
			{
				abortSocket.Close(timeout);
			}
			if (abortSocket2 != null)
			{
				abortSocket2.Close(timeout);
			}
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00095877 File Offset: 0x00094877
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x0009588B File Offset: 0x0009488B
		internal virtual IAsyncResult UnsafeBeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0009589F File Offset: 0x0009489F
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_NetworkStream.EndRead(asyncResult);
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000958AD File Offset: 0x000948AD
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000958C1 File Offset: 0x000948C1
		internal virtual IAsyncResult UnsafeBeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.UnsafeBeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000958D5 File Offset: 0x000948D5
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndWrite(asyncResult);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000958E3 File Offset: 0x000948E3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginMultipleWrite(BufferOffsetSize[] buffers, AsyncCallback callback, object state)
		{
			return this.m_NetworkStream.BeginMultipleWrite(buffers, callback, state);
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000958F3 File Offset: 0x000948F3
		internal void EndMultipleWrite(IAsyncResult asyncResult)
		{
			this.m_NetworkStream.EndMultipleWrite(asyncResult);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00095901 File Offset: 0x00094901
		public override void Flush()
		{
			this.m_NetworkStream.Flush();
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x0009590E File Offset: 0x0009490E
		public override void SetLength(long value)
		{
			this.m_NetworkStream.SetLength(value);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0009591C File Offset: 0x0009491C
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			this.m_NetworkStream.SetSocketTimeoutOption(mode, timeout, silent);
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x0009592C File Offset: 0x0009492C
		internal bool Poll(int microSeconds, SelectMode mode)
		{
			return this.m_NetworkStream.Poll(microSeconds, mode);
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0009593B File Offset: 0x0009493B
		internal bool PollRead()
		{
			return this.m_NetworkStream.PollRead();
		}

		// Token: 0x04002527 RID: 9511
		private bool m_CheckLifetime;

		// Token: 0x04002528 RID: 9512
		private TimeSpan m_Lifetime;

		// Token: 0x04002529 RID: 9513
		private DateTime m_CreateTime;

		// Token: 0x0400252A RID: 9514
		private bool m_ConnectionIsDoomed;

		// Token: 0x0400252B RID: 9515
		private ConnectionPool m_ConnectionPool;

		// Token: 0x0400252C RID: 9516
		private WeakReference m_Owner;

		// Token: 0x0400252D RID: 9517
		private int m_PooledCount;

		// Token: 0x0400252E RID: 9518
		private bool m_Initalizing;

		// Token: 0x0400252F RID: 9519
		private IPAddress m_ServerAddress;

		// Token: 0x04002530 RID: 9520
		private NetworkStream m_NetworkStream;

		// Token: 0x04002531 RID: 9521
		private Socket m_AbortSocket;

		// Token: 0x04002532 RID: 9522
		private Socket m_AbortSocket6;

		// Token: 0x04002533 RID: 9523
		private bool m_JustConnected;

		// Token: 0x04002534 RID: 9524
		private GeneralAsyncDelegate m_AsyncCallback;
	}
}
