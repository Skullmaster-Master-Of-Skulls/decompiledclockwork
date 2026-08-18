using System;
using System.Net;
using System.Net.Sockets;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200084D RID: 2125
	internal sealed class ExclusiveTcpTransportManager : TcpTransportManager, ISocketListenerSettings
	{
		// Token: 0x06004F83 RID: 20355 RVA: 0x00122D84 File Offset: 0x00120F84
		public ExclusiveTcpTransportManager(ExclusiveTcpTransportManagerRegistration registration, TcpChannelListener channelListener, IPAddress ipAddressAny, UriHostNameType ipHostNameType)
		{
			base.ApplyListenerSettings(channelListener);
			this.listenSocket = channelListener.GetListenSocket(ipHostNameType);
			if (this.listenSocket != null)
			{
				this.ipAddress = ((IPEndPoint)this.listenSocket.LocalEndPoint).Address;
			}
			else if (channelListener.Uri.HostNameType == ipHostNameType)
			{
				this.ipAddress = IPAddress.Parse(channelListener.Uri.DnsSafeHost);
			}
			else
			{
				this.ipAddress = ipAddressAny;
			}
			this.listenBacklog = channelListener.ListenBacklog;
			this.registration = registration;
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x00122E12 File Offset: 0x00121012
		public IPAddress IPAddress
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x00122E1A File Offset: 0x0012101A
		public int ListenBacklog
		{
			get
			{
				return this.listenBacklog;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x00122E22 File Offset: 0x00121022
		int ISocketListenerSettings.BufferSize
		{
			get
			{
				return base.ConnectionBufferSize;
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x00122E2A File Offset: 0x0012102A
		bool ISocketListenerSettings.TeredoEnabled
		{
			get
			{
				return this.registration.TeredoEnabled;
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x00122E37 File Offset: 0x00121037
		int ISocketListenerSettings.ListenBacklog
		{
			get
			{
				return this.ListenBacklog;
			}
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x00122E40 File Offset: 0x00121040
		internal override void OnOpen()
		{
			SocketConnectionListener socketConnectionListener;
			if (this.listenSocket != null)
			{
				socketConnectionListener = new SocketConnectionListener(this.listenSocket, this, false);
				this.listenSocket = null;
			}
			else
			{
				int num = this.registration.ListenUri.Port;
				if (num == -1)
				{
					num = 808;
				}
				socketConnectionListener = new SocketConnectionListener(new IPEndPoint(this.ipAddress, num), this, false);
			}
			this.connectionListener = new BufferedConnectionListener(socketConnectionListener, base.MaxOutputDelay, base.ConnectionBufferSize);
			if (DiagnosticUtility.ShouldUseActivity)
			{
				this.connectionListener = new TracingConnectionListener(this.connectionListener, this.registration.ListenUri.ToString(), false);
			}
			this.connectionDemuxer = new ConnectionDemuxer(this.connectionListener, base.MaxPendingAccepts, base.MaxPendingConnections, base.ChannelInitializationTimeout, base.IdleTimeout, base.MaxPooledConnections, new TransportSettingsCallback(base.OnGetTransportFactorySettings), new SingletonPreambleDemuxCallback(base.OnGetSingletonMessageHandler), new ServerSessionPreambleDemuxCallback(base.OnHandleServerSessionPreamble), new ErrorCallback(base.OnDemuxerError));
			bool flag = false;
			try
			{
				this.connectionDemuxer.StartDemuxing();
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.connectionDemuxer.Dispose();
				}
			}
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x00122F70 File Offset: 0x00121170
		internal override void OnClose(TimeSpan timeout)
		{
			this.Cleanup();
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x00122F78 File Offset: 0x00121178
		internal override void OnAbort()
		{
			this.Cleanup();
			base.OnAbort();
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00122F88 File Offset: 0x00121188
		private void Cleanup()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.closed)
				{
					return;
				}
				this.closed = true;
			}
			if (this.connectionDemuxer != null)
			{
				this.connectionDemuxer.Dispose();
			}
			if (this.connectionListener != null)
			{
				this.connectionListener.Dispose();
			}
			this.registration.OnClose(this);
		}

		// Token: 0x04003154 RID: 12628
		private bool closed;

		// Token: 0x04003155 RID: 12629
		private ConnectionDemuxer connectionDemuxer;

		// Token: 0x04003156 RID: 12630
		private IConnectionListener connectionListener;

		// Token: 0x04003157 RID: 12631
		private IPAddress ipAddress;

		// Token: 0x04003158 RID: 12632
		private int listenBacklog;

		// Token: 0x04003159 RID: 12633
		private Socket listenSocket;

		// Token: 0x0400315A RID: 12634
		private ExclusiveTcpTransportManagerRegistration registration;
	}
}
