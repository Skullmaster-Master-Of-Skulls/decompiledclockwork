using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200084E RID: 2126
	internal class ExclusiveTcpTransportManagerRegistration : TransportManagerRegistration
	{
		// Token: 0x06004F8D RID: 20365 RVA: 0x00123008 File Offset: 0x00121208
		public ExclusiveTcpTransportManagerRegistration(Uri listenUri, TcpChannelListener channelListener) : base(listenUri, channelListener.HostNameComparisonMode)
		{
			this.connectionBufferSize = channelListener.ConnectionBufferSize;
			this.channelInitializationTimeout = channelListener.ChannelInitializationTimeout;
			this.teredoEnabled = channelListener.TeredoEnabled;
			this.listenBacklog = channelListener.ListenBacklog;
			this.maxOutputDelay = channelListener.MaxOutputDelay;
			this.maxPendingConnections = channelListener.MaxPendingConnections;
			this.maxPendingAccepts = channelListener.MaxPendingAccepts;
			this.idleTimeout = channelListener.IdleTimeout;
			this.maxPooledConnections = channelListener.MaxPooledConnections;
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x0012308E File Offset: 0x0012128E
		public bool TeredoEnabled
		{
			get
			{
				return this.teredoEnabled;
			}
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x00123098 File Offset: 0x00121298
		public void OnClose(TcpTransportManager manager)
		{
			if (manager == this.ipv4TransportManager)
			{
				this.ipv4TransportManager = null;
			}
			else if (manager == this.ipv6TransportManager)
			{
				this.ipv6TransportManager = null;
			}
			if (this.ipv4TransportManager == null && this.ipv6TransportManager == null)
			{
				TcpChannelListener.StaticTransportManagerTable.UnregisterUri(base.ListenUri, base.HostNameComparisonMode);
			}
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x001230F0 File Offset: 0x001212F0
		private bool IsCompatible(TcpChannelListener channelListener, bool useIPv4, bool useIPv6)
		{
			return channelListener.InheritBaseAddressSettings || ((!useIPv6 || channelListener.IsScopeIdCompatible(base.HostNameComparisonMode, base.ListenUri)) && (!channelListener.PortSharingEnabled && (useIPv4 || useIPv6) && this.channelInitializationTimeout == channelListener.ChannelInitializationTimeout && this.idleTimeout == channelListener.IdleTimeout && this.maxPooledConnections == channelListener.MaxPooledConnections && this.connectionBufferSize == channelListener.ConnectionBufferSize && (!useIPv6 || this.teredoEnabled == channelListener.TeredoEnabled) && this.listenBacklog == channelListener.ListenBacklog && this.maxPendingConnections == channelListener.MaxPendingConnections && this.maxOutputDelay == channelListener.MaxOutputDelay) && this.maxPendingAccepts == channelListener.MaxPendingAccepts);
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x001231C5 File Offset: 0x001213C5
		private void ProcessSelection(TcpChannelListener channelListener, IPAddress ipAddressAny, UriHostNameType ipHostNameType, ref ExclusiveTcpTransportManager transportManager, IList<TransportManager> result)
		{
			if (transportManager == null)
			{
				transportManager = new ExclusiveTcpTransportManager(this, channelListener, ipAddressAny, ipHostNameType);
			}
			result.Add(transportManager);
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x001231E4 File Offset: 0x001213E4
		public override IList<TransportManager> Select(TransportChannelListener channelListener)
		{
			bool flag = base.ListenUri.HostNameType != UriHostNameType.IPv6 && Socket.OSSupportsIPv4;
			bool flag2 = base.ListenUri.HostNameType != UriHostNameType.IPv4 && Socket.OSSupportsIPv6;
			TcpChannelListener channelListener2 = (TcpChannelListener)channelListener;
			if (!this.IsCompatible(channelListener2, flag, flag2))
			{
				return null;
			}
			IList<TransportManager> result = new List<TransportManager>();
			if (flag)
			{
				this.ProcessSelection(channelListener2, IPAddress.Any, UriHostNameType.IPv4, ref this.ipv4TransportManager, result);
			}
			if (flag2)
			{
				this.ProcessSelection(channelListener2, IPAddress.IPv6Any, UriHostNameType.IPv6, ref this.ipv6TransportManager, result);
			}
			return result;
		}

		// Token: 0x0400315B RID: 12635
		private int connectionBufferSize;

		// Token: 0x0400315C RID: 12636
		private TimeSpan channelInitializationTimeout;

		// Token: 0x0400315D RID: 12637
		private TimeSpan idleTimeout;

		// Token: 0x0400315E RID: 12638
		private int maxPooledConnections;

		// Token: 0x0400315F RID: 12639
		private bool teredoEnabled;

		// Token: 0x04003160 RID: 12640
		private int listenBacklog;

		// Token: 0x04003161 RID: 12641
		private TimeSpan maxOutputDelay;

		// Token: 0x04003162 RID: 12642
		private int maxPendingConnections;

		// Token: 0x04003163 RID: 12643
		private int maxPendingAccepts;

		// Token: 0x04003164 RID: 12644
		private ExclusiveTcpTransportManager ipv4TransportManager;

		// Token: 0x04003165 RID: 12645
		private ExclusiveTcpTransportManager ipv6TransportManager;
	}
}
