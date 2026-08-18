using System;
using System.Net;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009FF RID: 2559
	internal class PeerNodeConfig
	{
		// Token: 0x0600658A RID: 25994 RVA: 0x0017ABD4 File Offset: 0x00178DD4
		public PeerNodeConfig(string meshId, ulong nodeId, PeerResolver resolver, PeerMessagePropagationFilter messagePropagationFilter, MessageEncoder encoder, Uri listenUri, IPAddress listenIPAddress, int port, long maxReceivedMessageSize, int minNeighbors, int idealNeighbors, int maxNeighbors, int maxReferrals, int connectTimeout, int maintainerInterval, PeerSecurityManager securityManager, XmlDictionaryReaderQuotas readerQuotas, long maxBufferPool, int maxSendQueueSize, int maxReceiveQueueSize)
		{
			this.connectTimeout = connectTimeout;
			this.listenIPAddress = listenIPAddress;
			this.listenUri = listenUri;
			this.maxReceivedMessageSize = maxReceivedMessageSize;
			this.minNeighbors = minNeighbors;
			this.idealNeighbors = idealNeighbors;
			this.maxNeighbors = maxNeighbors;
			this.maxReferrals = maxReferrals;
			this.maxReferralCacheSize = 50;
			this.maxResolveAddresses = 3;
			this.meshId = meshId;
			this.encoder = encoder;
			this.messagePropagationFilter = messagePropagationFilter;
			this.nodeId = nodeId;
			this.port = port;
			this.resolver = resolver;
			this.maintainerInterval = maintainerInterval;
			this.maintainerRetryInterval = new TimeSpan(100000000L);
			this.maintainerTimeout = new TimeSpan(1200000000L);
			this.unregisterTimeout = new TimeSpan(1200000000L);
			this.securityManager = securityManager;
			readerQuotas.CopyTo(this.readerQuotas);
			this.maxBufferPoolSize = maxBufferPool;
			this.maxIncomingConcurrentCalls = maxReceiveQueueSize;
			this.maxSendQueueSize = maxSendQueueSize;
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x0017ACF4 File Offset: 0x00178EF4
		internal PeerSecurityManager SecurityManager
		{
			get
			{
				return this.securityManager;
			}
		}

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x0600658C RID: 25996 RVA: 0x0017ACFC File Offset: 0x00178EFC
		public int ConnectTimeout
		{
			get
			{
				return this.connectTimeout;
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x0017AD04 File Offset: 0x00178F04
		public IPAddress ListenIPAddress
		{
			get
			{
				return this.listenIPAddress;
			}
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x0017AD0C File Offset: 0x00178F0C
		public int ListenerPort
		{
			get
			{
				return this.listenAddress.EndpointAddress.Uri.Port;
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x0600658F RID: 25999 RVA: 0x0017AD23 File Offset: 0x00178F23
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x0017AD2B File Offset: 0x00178F2B
		public int IdealNeighbors
		{
			get
			{
				return this.idealNeighbors;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x06006591 RID: 26001 RVA: 0x0017AD33 File Offset: 0x00178F33
		public int MaintainerInterval
		{
			get
			{
				return this.maintainerInterval;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x0017AD3B File Offset: 0x00178F3B
		public TimeSpan MaintainerRetryInterval
		{
			get
			{
				return this.maintainerRetryInterval;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x0017AD43 File Offset: 0x00178F43
		public TimeSpan MaintainerTimeout
		{
			get
			{
				return this.maintainerTimeout;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x0017AD4B File Offset: 0x00178F4B
		public long MaxBufferPoolSize
		{
			get
			{
				return this.maxBufferPoolSize;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06006595 RID: 26005 RVA: 0x0017AD53 File Offset: 0x00178F53
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x0017AD5B File Offset: 0x00178F5B
		public int MaxNeighbors
		{
			get
			{
				return this.maxNeighbors;
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x06006597 RID: 26007 RVA: 0x0017AD63 File Offset: 0x00178F63
		public int MaxReferrals
		{
			get
			{
				return this.maxReferrals;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x0017AD6B File Offset: 0x00178F6B
		public int MaxReferralCacheSize
		{
			get
			{
				return this.maxReferralCacheSize;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06006599 RID: 26009 RVA: 0x0017AD73 File Offset: 0x00178F73
		public int MaxResolveAddresses
		{
			get
			{
				return this.maxResolveAddresses;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x0017AD7B File Offset: 0x00178F7B
		public int MaxPendingIncomingCalls
		{
			get
			{
				return this.maxIncomingConcurrentCalls;
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x0600659B RID: 26011 RVA: 0x0017AD83 File Offset: 0x00178F83
		public int MaxPendingOutgoingCalls
		{
			get
			{
				return this.maxSendQueueSize;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x0600659C RID: 26012 RVA: 0x0017AD8B File Offset: 0x00178F8B
		public int MaxConcurrentSessions
		{
			get
			{
				return this.maxConcurrentSessions;
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x0600659D RID: 26013 RVA: 0x0017AD93 File Offset: 0x00178F93
		public int MinNeighbors
		{
			get
			{
				return this.minNeighbors;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x0600659E RID: 26014 RVA: 0x0017AD9B File Offset: 0x00178F9B
		public string MeshId
		{
			get
			{
				return this.meshId;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x0600659F RID: 26015 RVA: 0x0017ADA3 File Offset: 0x00178FA3
		public MessageEncoder MessageEncoder
		{
			get
			{
				return this.encoder;
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x060065A0 RID: 26016 RVA: 0x0017ADAB File Offset: 0x00178FAB
		public PeerMessagePropagationFilter MessagePropagationFilter
		{
			get
			{
				return this.messagePropagationFilter;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x060065A1 RID: 26017 RVA: 0x0017ADB3 File Offset: 0x00178FB3
		public ulong NodeId
		{
			get
			{
				return this.nodeId;
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x0017ADBB File Offset: 0x00178FBB
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x0017ADC3 File Offset: 0x00178FC3
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x060065A4 RID: 26020 RVA: 0x0017ADCB File Offset: 0x00178FCB
		public PeerResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x0017ADD3 File Offset: 0x00178FD3
		public TimeSpan UnregisterTimeout
		{
			get
			{
				return this.unregisterTimeout;
			}
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x0017ADDC File Offset: 0x00178FDC
		public PeerNodeAddress GetListenAddress(bool maskScopeId)
		{
			PeerNodeAddress peerNodeAddress = this.listenAddress;
			return new PeerNodeAddress(peerNodeAddress.EndpointAddress, PeerIPHelper.CloneAddresses(peerNodeAddress.IPAddresses, maskScopeId));
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0017AE07 File Offset: 0x00179007
		public void SetListenAddress(PeerNodeAddress address)
		{
			this.listenAddress = address;
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x0017AE10 File Offset: 0x00179010
		private static Uri BuildUri(string host, int port, Guid guid)
		{
			UriBuilder uriBuilder = new UriBuilder();
			uriBuilder.Host = host;
			if (port > 0)
			{
				uriBuilder.Port = port;
			}
			UriBuilder uriBuilder2 = uriBuilder;
			string str = "PeerChannelEndpoints/";
			Guid guid2 = guid;
			uriBuilder2.Path = str + guid2.ToString();
			uriBuilder.Scheme = Uri.UriSchemeNetTcp;
			TcpChannelListener.FixIpv6Hostname(uriBuilder, uriBuilder.Uri);
			return uriBuilder.Uri;
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x0017AE74 File Offset: 0x00179074
		public Uri GetSelfUri()
		{
			Guid guid = Guid.NewGuid();
			Uri result;
			if (this.listenIPAddress == null)
			{
				result = PeerNodeConfig.BuildUri(DnsCache.MachineName, this.port, guid);
			}
			else
			{
				result = PeerNodeConfig.BuildUri(this.listenIPAddress.ToString(), this.port, guid);
			}
			return result;
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x0017AEC0 File Offset: 0x001790C0
		public Uri GetMeshUri()
		{
			return new UriBuilder
			{
				Host = this.meshId,
				Scheme = "net.p2p"
			}.Uri;
		}

		// Token: 0x04003A35 RID: 14901
		private int connectTimeout;

		// Token: 0x04003A36 RID: 14902
		private MessageEncoder encoder;

		// Token: 0x04003A37 RID: 14903
		private PeerNodeAddress listenAddress;

		// Token: 0x04003A38 RID: 14904
		private IPAddress listenIPAddress;

		// Token: 0x04003A39 RID: 14905
		private Uri listenUri;

		// Token: 0x04003A3A RID: 14906
		private long maxReceivedMessageSize;

		// Token: 0x04003A3B RID: 14907
		private int minNeighbors;

		// Token: 0x04003A3C RID: 14908
		private int idealNeighbors;

		// Token: 0x04003A3D RID: 14909
		private int maxNeighbors;

		// Token: 0x04003A3E RID: 14910
		private int maxReferrals;

		// Token: 0x04003A3F RID: 14911
		private int maxReferralCacheSize;

		// Token: 0x04003A40 RID: 14912
		private int maxResolveAddresses;

		// Token: 0x04003A41 RID: 14913
		private string meshId;

		// Token: 0x04003A42 RID: 14914
		private PeerMessagePropagationFilter messagePropagationFilter;

		// Token: 0x04003A43 RID: 14915
		private ulong nodeId;

		// Token: 0x04003A44 RID: 14916
		private int port;

		// Token: 0x04003A45 RID: 14917
		private PeerResolver resolver;

		// Token: 0x04003A46 RID: 14918
		private int maintainerInterval;

		// Token: 0x04003A47 RID: 14919
		private TimeSpan maintainerRetryInterval;

		// Token: 0x04003A48 RID: 14920
		private TimeSpan maintainerTimeout;

		// Token: 0x04003A49 RID: 14921
		private TimeSpan unregisterTimeout;

		// Token: 0x04003A4A RID: 14922
		private PeerSecurityManager securityManager;

		// Token: 0x04003A4B RID: 14923
		private int maxIncomingConcurrentCalls = 128;

		// Token: 0x04003A4C RID: 14924
		private int maxConcurrentSessions = 64;

		// Token: 0x04003A4D RID: 14925
		private XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();

		// Token: 0x04003A4E RID: 14926
		private long maxBufferPoolSize;

		// Token: 0x04003A4F RID: 14927
		private int maxSendQueueSize = 128;
	}
}
