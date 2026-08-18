using System;
using System.Net;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F1 RID: 2545
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal sealed class PeerChannelFactory<TChannel> : TransportChannelFactory<TChannel>, IPeerFactory, ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x060064BC RID: 25788 RVA: 0x00177F44 File Offset: 0x00176144
		internal PeerChannelFactory(PeerTransportBindingElement bindingElement, BindingContext context, PeerResolver peerResolver) : base(bindingElement, context)
		{
			this.listenIPAddress = bindingElement.ListenIPAddress;
			this.port = bindingElement.Port;
			this.resolver = peerResolver;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = context.Binding.Elements.Find<BinaryMessageEncodingBindingElement>();
			if (binaryMessageEncodingBindingElement != null)
			{
				binaryMessageEncodingBindingElement.ReaderQuotas.CopyTo(this.readerQuotas);
			}
			else
			{
				EncoderDefaults.ReaderQuotas.CopyTo(this.readerQuotas);
			}
			this.securityManager = PeerSecurityManager.Create(bindingElement.Security, context, this.readerQuotas);
			this.securityCapabilities = bindingElement.GetProperty<ISecurityCapabilities>(context);
		}

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x060064BD RID: 25789 RVA: 0x00177FDF File Offset: 0x001761DF
		public IPAddress ListenIPAddress
		{
			get
			{
				return this.listenIPAddress;
			}
		}

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x00177FE7 File Offset: 0x001761E7
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x060064BF RID: 25791 RVA: 0x00177FEF File Offset: 0x001761EF
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x00177FF7 File Offset: 0x001761F7
		public PeerResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x00177FFF File Offset: 0x001761FF
		public override string Scheme
		{
			get
			{
				return "net.p2p";
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x00178006 File Offset: 0x00176206
		// (set) Token: 0x060064C3 RID: 25795 RVA: 0x0017800E File Offset: 0x0017620E
		public PeerNodeImplementation PrivatePeerNode
		{
			get
			{
				return this.privatePeerNode;
			}
			set
			{
				this.privatePeerNode = value;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x00178017 File Offset: 0x00176217
		// (set) Token: 0x060064C5 RID: 25797 RVA: 0x0017801F File Offset: 0x0017621F
		public PeerSecurityManager SecurityManager
		{
			get
			{
				return this.securityManager;
			}
			set
			{
				this.securityManager = value;
			}
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x00178028 File Offset: 0x00176228
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(PeerChannelFactory<TChannel>))
			{
				return (T)((object)this);
			}
			if (typeof(T) == typeof(IPeerFactory))
			{
				return (T)((object)this);
			}
			if (typeof(T) == typeof(PeerNodeImplementation))
			{
				return (T)((object)this.privatePeerNode);
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.securityCapabilities);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x001780CD File Offset: 0x001762CD
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x001780CF File Offset: 0x001762CF
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x001780D8 File Offset: 0x001762D8
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x001780E0 File Offset: 0x001762E0
		protected override TChannel OnCreateChannel(EndpointAddress to, Uri via)
		{
			base.ValidateScheme(via);
			PeerNodeImplementation peerNode = null;
			PeerNodeImplementation.Registration registration = null;
			if (this.privatePeerNode != null && via.Host == this.privatePeerNode.MeshId)
			{
				peerNode = this.privatePeerNode;
			}
			else
			{
				registration = new PeerNodeImplementation.Registration(via, this);
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				return (TChannel)((object)new PeerOutputChannel(peerNode, registration, this, to, via, base.MessageVersion));
			}
			PeerDuplexChannel peerDuplexChannel = new PeerDuplexChannel(peerNode, registration, this, to, via);
			PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel>.PeerMessageQueueAdapter queueHandler = new PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel>.PeerMessageQueueAdapter(peerDuplexChannel);
			PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel> dispatcher = new PeerMessageDispatcher<IDuplexChannel, PeerDuplexChannel>(queueHandler, peerDuplexChannel.InnerNode, this, to, via);
			peerDuplexChannel.Dispatcher = dispatcher;
			return (TChannel)((object)peerDuplexChannel);
		}

		// Token: 0x040039E9 RID: 14825
		private IPAddress listenIPAddress;

		// Token: 0x040039EA RID: 14826
		private int port;

		// Token: 0x040039EB RID: 14827
		private PeerResolver resolver;

		// Token: 0x040039EC RID: 14828
		private PeerSecurityManager securityManager;

		// Token: 0x040039ED RID: 14829
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040039EE RID: 14830
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x040039EF RID: 14831
		private PeerNodeImplementation privatePeerNode;
	}
}
