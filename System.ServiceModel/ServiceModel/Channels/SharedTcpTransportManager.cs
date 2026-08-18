using System;
using System.Collections.Generic;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000823 RID: 2083
	internal class SharedTcpTransportManager : TcpTransportManager, ITransportManagerRegistration
	{
		// Token: 0x06004DE0 RID: 19936 RVA: 0x0011CBA4 File Offset: 0x0011ADA4
		public SharedTcpTransportManager(Uri listenUri, TcpChannelListener channelListener)
		{
			this.HostNameComparisonMode = channelListener.HostNameComparisonMode;
			this.listenUri = listenUri;
			base.ApplyListenerSettings(channelListener);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0011CBC6 File Offset: 0x0011ADC6
		protected SharedTcpTransportManager(Uri listenUri)
		{
			this.listenUri = listenUri;
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0011CBD5 File Offset: 0x0011ADD5
		protected override bool IsCompatible(TcpChannelListener channelListener)
		{
			return (channelListener.HostedVirtualPath != null || channelListener.PortSharingEnabled) && base.IsCompatible(channelListener);
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06004DE3 RID: 19939 RVA: 0x0011CBF0 File Offset: 0x0011ADF0
		// (set) Token: 0x06004DE4 RID: 19940 RVA: 0x0011CBF8 File Offset: 0x0011ADF8
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
			set
			{
				HostNameComparisonModeHelper.Validate(value);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfOpen();
					this.hostNameComparisonMode = value;
				}
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06004DE5 RID: 19941 RVA: 0x0011CC48 File Offset: 0x0011AE48
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0011CC50 File Offset: 0x0011AE50
		internal override void OnOpen()
		{
			this.OnOpenInternal(0, Guid.Empty);
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0011CC5E File Offset: 0x0011AE5E
		protected virtual Action<Uri> GetOnViaCallback()
		{
			return null;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0011CC64 File Offset: 0x0011AE64
		private int OnDuplicatedVia(Uri via)
		{
			Action<Uri> onViaCallback = this.GetOnViaCallback();
			if (onViaCallback != null)
			{
				onViaCallback(via);
			}
			if (!this.demuxerCreated)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					SharedConnectionListener sharedConnectionListener = this.listener;
					if (sharedConnectionListener == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("Sharing_ListenerProxyStopped")));
					}
					if (!this.demuxerCreated)
					{
						this.CreateConnectionDemuxer(sharedConnectionListener);
						this.demuxerCreated = true;
					}
				}
			}
			return base.ConnectionBufferSize;
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0011CCF8 File Offset: 0x0011AEF8
		private void CreateConnectionDemuxer(SharedConnectionListener sharedListener)
		{
			IConnectionListener connectionListener = new BufferedConnectionListener(sharedListener, base.MaxOutputDelay, base.ConnectionBufferSize);
			if (DiagnosticUtility.ShouldUseActivity)
			{
				connectionListener = new TracingConnectionListener(connectionListener, this.ListenUri);
			}
			this.connectionDemuxer = new ConnectionDemuxer(connectionListener, base.MaxPendingAccepts, base.MaxPendingConnections, base.ChannelInitializationTimeout, base.IdleTimeout, base.MaxPooledConnections, new TransportSettingsCallback(base.OnGetTransportFactorySettings), new SingletonPreambleDemuxCallback(base.OnGetSingletonMessageHandler), new ServerSessionPreambleDemuxCallback(base.OnHandleServerSessionPreamble), new ErrorCallback(base.OnDemuxerError));
			this.connectionDemuxer.StartDemuxing(this.GetOnViaCallback());
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0011CD98 File Offset: 0x0011AF98
		internal void OnOpenInternal(int queueId, Guid token)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				this.queueId = queueId;
				this.token = token;
				BaseUriWithWildcard baseAddress = new BaseUriWithWildcard(this.ListenUri, this.HostNameComparisonMode);
				if (this.onDuplicatedViaCallback == null)
				{
					this.onDuplicatedViaCallback = new Func<Uri, int>(this.OnDuplicatedVia);
				}
				this.listener = new SharedConnectionListener(baseAddress, queueId, token, this.onDuplicatedViaCallback);
			}
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x0011CE20 File Offset: 0x0011B020
		protected void CleanUp(bool aborting, TimeSpan timeout)
		{
			SharedConnectionListener sharedConnectionListener = Interlocked.Exchange<SharedConnectionListener>(ref this.listener, null);
			if (sharedConnectionListener != null)
			{
				if (!aborting)
				{
					sharedConnectionListener.Stop(timeout);
				}
				else
				{
					sharedConnectionListener.Abort();
				}
			}
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.connectionDemuxer != null && this.demuxerCreated)
				{
					this.connectionDemuxer.Dispose();
				}
				this.demuxerCreated = false;
			}
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0011CEA0 File Offset: 0x0011B0A0
		private void Unregister()
		{
			TcpChannelListener.StaticTransportManagerTable.UnregisterUri(this.ListenUri, this.HostNameComparisonMode);
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0011CEB8 File Offset: 0x0011B0B8
		internal override void OnAbort()
		{
			this.CleanUp(true, TimeSpan.Zero);
			this.Unregister();
			base.OnAbort();
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0011CED2 File Offset: 0x0011B0D2
		internal override void OnClose(TimeSpan timeout)
		{
			this.CleanUp(false, timeout);
			this.Unregister();
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0011CEE2 File Offset: 0x0011B0E2
		protected virtual void OnSelecting(TcpChannelListener channelListener)
		{
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0011CEE4 File Offset: 0x0011B0E4
		IList<TransportManager> ITransportManagerRegistration.Select(TransportChannelListener channelListener)
		{
			if (!channelListener.IsScopeIdCompatible(this.hostNameComparisonMode, this.listenUri))
			{
				return null;
			}
			this.OnSelecting((TcpChannelListener)channelListener);
			IList<TransportManager> list = null;
			if (this.IsCompatible((TcpChannelListener)channelListener))
			{
				list = new List<TransportManager>();
				list.Add(this);
			}
			return list;
		}

		// Token: 0x040030B5 RID: 12469
		private SharedConnectionListener listener;

		// Token: 0x040030B6 RID: 12470
		private ConnectionDemuxer connectionDemuxer;

		// Token: 0x040030B7 RID: 12471
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x040030B8 RID: 12472
		private Uri listenUri;

		// Token: 0x040030B9 RID: 12473
		private int queueId;

		// Token: 0x040030BA RID: 12474
		private Guid token;

		// Token: 0x040030BB RID: 12475
		private Func<Uri, int> onDuplicatedViaCallback;

		// Token: 0x040030BC RID: 12476
		private bool demuxerCreated;
	}
}
