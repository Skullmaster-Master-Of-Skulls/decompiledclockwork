using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200083D RID: 2109
	internal sealed class ExclusiveNamedPipeTransportManager : NamedPipeTransportManager
	{
		// Token: 0x06004ED4 RID: 20180 RVA: 0x0011F505 File Offset: 0x0011D705
		public ExclusiveNamedPipeTransportManager(Uri listenUri, NamedPipeChannelListener channelListener) : base(listenUri)
		{
			base.ApplyListenerSettings(channelListener);
			base.SetHostNameComparisonMode(channelListener.HostNameComparisonMode);
			base.SetAllowedUsers(channelListener.AllowedUsers);
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x0011F530 File Offset: 0x0011D730
		internal override void OnOpen()
		{
			this.connectionListener = new BufferedConnectionListener(new PipeConnectionListener(base.ListenUri, base.HostNameComparisonMode, base.ConnectionBufferSize, base.AllowedUsers, true, int.MaxValue), base.MaxOutputDelay, base.ConnectionBufferSize);
			if (DiagnosticUtility.ShouldUseActivity)
			{
				this.connectionListener = new TracingConnectionListener(this.connectionListener, base.ListenUri.ToString(), false);
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

		// Token: 0x06004ED6 RID: 20182 RVA: 0x0011F62C File Offset: 0x0011D82C
		internal override void OnClose(TimeSpan timeout)
		{
			this.connectionDemuxer.Dispose();
			this.connectionListener.Dispose();
			base.OnClose(timeout);
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x0011F64B File Offset: 0x0011D84B
		internal override void OnAbort()
		{
			if (this.connectionDemuxer != null)
			{
				this.connectionDemuxer.Dispose();
			}
			if (this.connectionListener != null)
			{
				this.connectionListener.Dispose();
			}
			base.OnAbort();
		}

		// Token: 0x04003109 RID: 12553
		private ConnectionDemuxer connectionDemuxer;

		// Token: 0x0400310A RID: 12554
		private IConnectionListener connectionListener;
	}
}
