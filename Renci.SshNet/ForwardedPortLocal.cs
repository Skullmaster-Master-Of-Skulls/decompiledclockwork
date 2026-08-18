using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Channels;
using Renci.SshNet.Common;

namespace Renci.SshNet
{
	// Token: 0x0200001E RID: 30
	public class ForwardedPortLocal : ForwardedPort, IDisposable
	{
		// Token: 0x0600015A RID: 346 RVA: 0x000052A0 File Offset: 0x000034A0
		private void StartAccept(SocketAsyncEventArgs e)
		{
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
				e.Completed += this.AcceptCompleted;
			}
			else
			{
				e.AcceptSocket = null;
			}
			if (this.IsStarted)
			{
				try
				{
					if (!this._listener.AcceptAsync(e))
					{
						this.AcceptCompleted(null, e);
					}
				}
				catch (ObjectDisposedException)
				{
					if (!(this._status == ForwardedPortStatus.Stopped) && !(this._status == ForwardedPortStatus.Stopped))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000532C File Offset: 0x0000352C
		private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.OperationAborted || e.SocketError == SocketError.NotSocket)
			{
				return;
			}
			Socket acceptSocket = e.AcceptSocket;
			if (e.SocketError != SocketError.Success)
			{
				this.StartAccept(e);
				ForwardedPortLocal.CloseClientSocket(acceptSocket);
				return;
			}
			this.StartAccept(e);
			this.ProcessAccept(acceptSocket);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005380 File Offset: 0x00003580
		private void ProcessAccept(Socket clientSocket)
		{
			if (!this.IsStarted)
			{
				ForwardedPortLocal.CloseClientSocket(clientSocket);
				return;
			}
			CountdownEvent pendingChannelCountdown = this._pendingChannelCountdown;
			pendingChannelCountdown.AddCount();
			try
			{
				IPEndPoint ipendPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
				base.RaiseRequestReceived(ipendPoint.Address.ToString(), (uint)ipendPoint.Port);
				using (IChannelDirectTcpip channelDirectTcpip = base.Session.CreateChannelDirectTcpip())
				{
					channelDirectTcpip.Exception += this.Channel_Exception;
					channelDirectTcpip.Open(this.Host, this.Port, this, clientSocket);
					channelDirectTcpip.Bind();
					channelDirectTcpip.Close();
				}
			}
			catch (Exception exception)
			{
				base.RaiseExceptionEvent(exception);
				ForwardedPortLocal.CloseClientSocket(clientSocket);
			}
			finally
			{
				try
				{
					pendingChannelCountdown.Signal();
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000546C File Offset: 0x0000366C
		private void InitializePendingChannelCountdown()
		{
			CountdownEvent countdownEvent = Interlocked.Exchange<CountdownEvent>(ref this._pendingChannelCountdown, new CountdownEvent(1));
			if (countdownEvent != null)
			{
				countdownEvent.Dispose();
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005494 File Offset: 0x00003694
		private static void CloseClientSocket(Socket clientSocket)
		{
			if (clientSocket.Connected)
			{
				try
				{
					clientSocket.Shutdown(SocketShutdown.Send);
				}
				catch (Exception)
				{
				}
			}
			clientSocket.Dispose();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000054CC File Offset: 0x000036CC
		private void Session_Disconnected(object sender, EventArgs e)
		{
			ISession session = base.Session;
			if (session != null)
			{
				this.StopPort(session.ConnectionInfo.Timeout);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000054F4 File Offset: 0x000036F4
		private void Session_ErrorOccured(object sender, ExceptionEventArgs e)
		{
			ISession session = base.Session;
			if (session != null)
			{
				this.StopPort(session.ConnectionInfo.Timeout);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004F48 File Offset: 0x00003148
		private void Channel_Exception(object sender, ExceptionEventArgs e)
		{
			base.RaiseExceptionEvent(e.Exception);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000551C File Offset: 0x0000371C
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005524 File Offset: 0x00003724
		public string BoundHost { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000552D File Offset: 0x0000372D
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005535 File Offset: 0x00003735
		public uint BoundPort { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000553E File Offset: 0x0000373E
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00005546 File Offset: 0x00003746
		public string Host { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000554F File Offset: 0x0000374F
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00005557 File Offset: 0x00003757
		public uint Port { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005560 File Offset: 0x00003760
		public override bool IsStarted
		{
			get
			{
				return this._status == ForwardedPortStatus.Started;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005572 File Offset: 0x00003772
		public ForwardedPortLocal(uint boundPort, string host, uint port) : this(string.Empty, boundPort, host, port)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005582 File Offset: 0x00003782
		public ForwardedPortLocal(string boundHost, string host, uint port) : this(boundHost, 0U, host, port)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005590 File Offset: 0x00003790
		public ForwardedPortLocal(string boundHost, uint boundPort, string host, uint port)
		{
			if (boundHost == null)
			{
				throw new ArgumentNullException("boundHost");
			}
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			boundPort.ValidatePort("boundPort");
			port.ValidatePort("port");
			this.BoundHost = boundHost;
			this.BoundPort = boundPort;
			this.Host = host;
			this.Port = port;
			this._status = ForwardedPortStatus.Stopped;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005600 File Offset: 0x00003800
		protected override void StartPort()
		{
			if (!ForwardedPortStatus.ToStarting(ref this._status))
			{
				return;
			}
			try
			{
				this.InternalStart();
			}
			catch (Exception)
			{
				this._status = ForwardedPortStatus.Stopped;
				throw;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005644 File Offset: 0x00003844
		protected override void StopPort(TimeSpan timeout)
		{
			if (!ForwardedPortStatus.ToStopping(ref this._status))
			{
				return;
			}
			base.StopPort(timeout);
			this.StopListener();
			this.InternalStop(timeout);
			this._status = ForwardedPortStatus.Stopped;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005673 File Offset: 0x00003873
		protected override void CheckDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005690 File Offset: 0x00003890
		private void InternalStart()
		{
			IPEndPoint ipendPoint = new IPEndPoint(DnsAbstraction.GetHostAddresses(this.BoundHost)[0], (int)this.BoundPort);
			this._listener = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
			{
				NoDelay = true
			};
			this._listener.Bind(ipendPoint);
			this._listener.Listen(5);
			this.BoundPort = (uint)((IPEndPoint)this._listener.LocalEndPoint).Port;
			base.Session.ErrorOccured += this.Session_ErrorOccured;
			base.Session.Disconnected += this.Session_Disconnected;
			this.InitializePendingChannelCountdown();
			this._status = ForwardedPortStatus.Started;
			this.StartAccept(null);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000574C File Offset: 0x0000394C
		private void StopListener()
		{
			Socket listener = this._listener;
			if (listener != null)
			{
				listener.Dispose();
			}
			ISession session = base.Session;
			if (session != null)
			{
				session.ErrorOccured -= this.Session_ErrorOccured;
				session.Disconnected -= this.Session_Disconnected;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005797 File Offset: 0x00003997
		private void InternalStop(TimeSpan timeout)
		{
			this._pendingChannelCountdown.Signal();
			this._pendingChannelCountdown.Wait(timeout);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004B6A File Offset: 0x00002D6A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000057B4 File Offset: 0x000039B4
		private void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Socket listener = this._listener;
				if (listener != null)
				{
					this._listener = null;
					listener.Dispose();
				}
				CountdownEvent pendingChannelCountdown = this._pendingChannelCountdown;
				if (pendingChannelCountdown != null)
				{
					this._pendingChannelCountdown = null;
					pendingChannelCountdown.Dispose();
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000057F2 File Offset: 0x000039F2
		protected override void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			base.Dispose(disposing);
			this.InternalDispose(disposing);
			this._isDisposed = true;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005814 File Offset: 0x00003A14
		~ForwardedPortLocal()
		{
			this.Dispose(false);
		}

		// Token: 0x04000062 RID: 98
		private Socket _listener;

		// Token: 0x04000063 RID: 99
		private CountdownEvent _pendingChannelCountdown;

		// Token: 0x04000064 RID: 100
		private ForwardedPortStatus _status;

		// Token: 0x04000069 RID: 105
		private bool _isDisposed;
	}
}
