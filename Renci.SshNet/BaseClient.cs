using System;
using System.Threading;
using Renci.SshNet.Common;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet
{
	// Token: 0x02000006 RID: 6
	public abstract class BaseClient : IDisposable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002312 File Offset: 0x00000512
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000231A File Offset: 0x0000051A
		internal ISession Session { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002323 File Offset: 0x00000523
		internal IServiceFactory ServiceFactory
		{
			get
			{
				return this._serviceFactory;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000232B File Offset: 0x0000052B
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002339 File Offset: 0x00000539
		public ConnectionInfo ConnectionInfo
		{
			get
			{
				this.CheckDisposed();
				return this._connectionInfo;
			}
			private set
			{
				this._connectionInfo = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002342 File Offset: 0x00000542
		public bool IsConnected
		{
			get
			{
				this.CheckDisposed();
				return this.Session != null && this.Session.IsConnected;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000235F File Offset: 0x0000055F
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002370 File Offset: 0x00000570
		public TimeSpan KeepAliveInterval
		{
			get
			{
				this.CheckDisposed();
				return this._keepAliveInterval;
			}
			set
			{
				this.CheckDisposed();
				if (value == this._keepAliveInterval)
				{
					return;
				}
				if (value == Renci.SshNet.Session.InfiniteTimeSpan)
				{
					this.StopKeepAliveTimer();
				}
				else if (this._keepAliveTimer != null)
				{
					this._keepAliveTimer.Change(value, value);
				}
				this._keepAliveInterval = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001D RID: 29 RVA: 0x000023C4 File Offset: 0x000005C4
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x000023FC File Offset: 0x000005FC
		public event EventHandler<ExceptionEventArgs> ErrorOccurred;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001F RID: 31 RVA: 0x00002434 File Offset: 0x00000634
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x0000246C File Offset: 0x0000066C
		public event EventHandler<HostKeyEventArgs> HostKeyReceived;

		// Token: 0x06000021 RID: 33 RVA: 0x000024A1 File Offset: 0x000006A1
		protected BaseClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo) : this(connectionInfo, ownsConnectionInfo, new ServiceFactory())
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024B0 File Offset: 0x000006B0
		internal BaseClient(ConnectionInfo connectionInfo, bool ownsConnectionInfo, IServiceFactory serviceFactory)
		{
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			if (serviceFactory == null)
			{
				throw new ArgumentNullException("serviceFactory");
			}
			this.ConnectionInfo = connectionInfo;
			this._ownsConnectionInfo = ownsConnectionInfo;
			this._serviceFactory = serviceFactory;
			this._keepAliveInterval = Renci.SshNet.Session.InfiniteTimeSpan;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000250C File Offset: 0x0000070C
		public void Connect()
		{
			this.CheckDisposed();
			if (this.Session != null && this.Session.IsConnected)
			{
				throw new InvalidOperationException("The client is already connected.");
			}
			this.OnConnecting();
			this.Session = this._serviceFactory.CreateSession(this.ConnectionInfo);
			this.Session.HostKeyReceived += this.Session_HostKeyReceived;
			this.Session.ErrorOccured += this.Session_ErrorOccured;
			this.Session.Connect();
			this.StartKeepAliveTimer();
			this.OnConnected();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025A4 File Offset: 0x000007A4
		public void Disconnect()
		{
			this.CheckDisposed();
			this.OnDisconnecting();
			this.StopKeepAliveTimer();
			if (this.Session != null)
			{
				this.Session.ErrorOccured -= this.Session_ErrorOccured;
				this.Session.HostKeyReceived -= this.Session_HostKeyReceived;
				this.Session.Disconnect();
				this.Session.Dispose();
				this.Session = null;
			}
			this.OnDisconnected();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000261C File Offset: 0x0000081C
		[Obsolete("Use KeepAliveInterval to send a keep-alive message at regular intervals.")]
		public void SendKeepAlive()
		{
			this.CheckDisposed();
			this.SendKeepAliveMessage();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual void OnConnecting()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual void OnConnected()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000262C File Offset: 0x0000082C
		protected virtual void OnDisconnecting()
		{
			if (this.Session != null)
			{
				this.Session.OnDisconnecting();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual void OnDisconnected()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002644 File Offset: 0x00000844
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			EventHandler<ExceptionEventArgs> errorOccurred = this.ErrorOccurred;
			if (errorOccurred != null)
			{
				errorOccurred(this, e);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002664 File Offset: 0x00000864
		private void Session_HostKeyReceived(object sender, HostKeyEventArgs e)
		{
			EventHandler<HostKeyEventArgs> hostKeyReceived = this.HostKeyReceived;
			if (hostKeyReceived != null)
			{
				hostKeyReceived(this, e);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002683 File Offset: 0x00000883
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002694 File Offset: 0x00000894
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this.Disconnect();
				if (this._ownsConnectionInfo && this._connectionInfo != null)
				{
					IDisposable disposable = this._connectionInfo as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					this._connectionInfo = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026E6 File Offset: 0x000008E6
		protected void CheckDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002704 File Offset: 0x00000904
		~BaseClient()
		{
			this.Dispose(false);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002734 File Offset: 0x00000934
		private void StopKeepAliveTimer()
		{
			if (this._keepAliveTimer == null)
			{
				return;
			}
			this._keepAliveTimer.Dispose();
			this._keepAliveTimer = null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002754 File Offset: 0x00000954
		private void SendKeepAliveMessage()
		{
			if (this.Session == null)
			{
				return;
			}
			if (Monitor.TryEnter(this._keepAliveLock))
			{
				try
				{
					this.Session.TrySendMessage(new IgnoreMessage());
				}
				finally
				{
					Monitor.Exit(this._keepAliveLock);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027A8 File Offset: 0x000009A8
		private void StartKeepAliveTimer()
		{
			if (this._keepAliveInterval == Renci.SshNet.Session.InfiniteTimeSpan)
			{
				return;
			}
			if (this._keepAliveTimer != null)
			{
				return;
			}
			this._keepAliveTimer = new Timer(delegate(object state)
			{
				this.SendKeepAliveMessage();
			}, null, this._keepAliveInterval, this._keepAliveInterval);
		}

		// Token: 0x0400000B RID: 11
		private readonly bool _ownsConnectionInfo;

		// Token: 0x0400000C RID: 12
		private readonly IServiceFactory _serviceFactory;

		// Token: 0x0400000D RID: 13
		private readonly object _keepAliveLock = new object();

		// Token: 0x0400000E RID: 14
		private TimeSpan _keepAliveInterval;

		// Token: 0x0400000F RID: 15
		private Timer _keepAliveTimer;

		// Token: 0x04000010 RID: 16
		private ConnectionInfo _connectionInfo;

		// Token: 0x04000014 RID: 20
		private bool _isDisposed;
	}
}
