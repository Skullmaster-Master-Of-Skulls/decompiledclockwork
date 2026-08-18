using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200014E RID: 334
	[__DynamicallyInvokable]
	public class NetTcpBinding : Binding, IBindingRuntimePreferences
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x000257B9 File Offset: 0x000239B9
		[__DynamicallyInvokable]
		public NetTcpBinding()
		{
			this.Initialize();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000257D2 File Offset: 0x000239D2
		[__DynamicallyInvokable]
		public NetTcpBinding(SecurityMode securityMode) : this()
		{
			this.security.Mode = securityMode;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000257E6 File Offset: 0x000239E6
		public NetTcpBinding(SecurityMode securityMode, bool reliableSessionEnabled) : this(securityMode)
		{
			this.ReliableSession.Enabled = reliableSessionEnabled;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000257FB File Offset: 0x000239FB
		[__DynamicallyInvokable]
		public NetTcpBinding(string configurationName) : this()
		{
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002580A File Offset: 0x00023A0A
		private NetTcpBinding(TcpTransportBindingElement transport, BinaryMessageEncodingBindingElement encoding, TransactionFlowBindingElement context, ReliableSessionBindingElement session, NetTcpSecurity security) : this()
		{
			this.security = security;
			this.ReliableSession.Enabled = (session != null);
			this.InitializeFrom(transport, encoding, context, session);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00025835 File Offset: 0x00023A35
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00025842 File Offset: 0x00023A42
		[DefaultValue(false)]
		public bool TransactionFlow
		{
			get
			{
				return this.context.Transactions;
			}
			set
			{
				this.context.Transactions = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00025850 File Offset: 0x00023A50
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x0002585D File Offset: 0x00023A5D
		public TransactionProtocol TransactionProtocol
		{
			get
			{
				return this.context.TransactionProtocol;
			}
			set
			{
				this.context.TransactionProtocol = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002586B File Offset: 0x00023A6B
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00025878 File Offset: 0x00023A78
		[DefaultValue(TransferMode.Buffered)]
		[__DynamicallyInvokable]
		public TransferMode TransferMode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transport.TransferMode;
			}
			[__DynamicallyInvokable]
			set
			{
				this.transport.TransferMode = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00025886 File Offset: 0x00023A86
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00025893 File Offset: 0x00023A93
		[DefaultValue(HostNameComparisonMode.StrongWildcard)]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.transport.HostNameComparisonMode;
			}
			set
			{
				this.transport.HostNameComparisonMode = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x000258A1 File Offset: 0x00023AA1
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x000258AE File Offset: 0x00023AAE
		[DefaultValue(524288L)]
		[__DynamicallyInvokable]
		public long MaxBufferPoolSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transport.MaxBufferPoolSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.transport.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000258BC File Offset: 0x00023ABC
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x000258C9 File Offset: 0x00023AC9
		[DefaultValue(65536)]
		[__DynamicallyInvokable]
		public int MaxBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transport.MaxBufferSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.transport.MaxBufferSize = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x000258D7 File Offset: 0x00023AD7
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x000258E4 File Offset: 0x00023AE4
		public int MaxConnections
		{
			get
			{
				return this.transport.MaxPendingConnections;
			}
			set
			{
				this.transport.MaxPendingConnections = value;
				this.transport.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00025903 File Offset: 0x00023B03
		internal bool IsMaxConnectionsSet
		{
			get
			{
				return this.transport.IsMaxPendingConnectionsSet;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00025910 File Offset: 0x00023B10
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0002591D File Offset: 0x00023B1D
		public int ListenBacklog
		{
			get
			{
				return this.transport.ListenBacklog;
			}
			set
			{
				this.transport.ListenBacklog = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002592B File Offset: 0x00023B2B
		internal bool IsListenBacklogSet
		{
			get
			{
				return this.transport.IsListenBacklogSet;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00025938 File Offset: 0x00023B38
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00025945 File Offset: 0x00023B45
		[DefaultValue(65536L)]
		[__DynamicallyInvokable]
		public long MaxReceivedMessageSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transport.MaxReceivedMessageSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.transport.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00025953 File Offset: 0x00023B53
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00025960 File Offset: 0x00023B60
		[DefaultValue(false)]
		public bool PortSharingEnabled
		{
			get
			{
				return this.transport.PortSharingEnabled;
			}
			set
			{
				this.transport.PortSharingEnabled = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0002596E File Offset: 0x00023B6E
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0002597B File Offset: 0x00023B7B
		[__DynamicallyInvokable]
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoding.ReaderQuotas;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				value.CopyTo(this.encoding.ReaderQuotas);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000259A1 File Offset: 0x00023BA1
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000259A4 File Offset: 0x00023BA4
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x000259AC File Offset: 0x00023BAC
		public OptionalReliableSession ReliableSession
		{
			get
			{
				return this.reliableSession;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.reliableSession.CopySettings(value);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000259D2 File Offset: 0x00023BD2
		[__DynamicallyInvokable]
		public override string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transport.Scheme;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000259DF File Offset: 0x00023BDF
		[__DynamicallyInvokable]
		public EnvelopeVersion EnvelopeVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000259E6 File Offset: 0x00023BE6
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x000259EE File Offset: 0x00023BEE
		[__DynamicallyInvokable]
		public NetTcpSecurity Security
		{
			[__DynamicallyInvokable]
			get
			{
				return this.security;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.security = value;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00025A0A File Offset: 0x00023C0A
		private static TransactionFlowBindingElement GetDefaultTransactionFlowBindingElement()
		{
			return new TransactionFlowBindingElement(false);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00025A12 File Offset: 0x00023C12
		private void Initialize()
		{
			this.transport = new TcpTransportBindingElement();
			this.encoding = new BinaryMessageEncodingBindingElement();
			this.context = NetTcpBinding.GetDefaultTransactionFlowBindingElement();
			this.session = new ReliableSessionBindingElement();
			this.reliableSession = new OptionalReliableSession(this.session);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00025A54 File Offset: 0x00023C54
		private void InitializeFrom(TcpTransportBindingElement transport, BinaryMessageEncodingBindingElement encoding, TransactionFlowBindingElement context, ReliableSessionBindingElement session)
		{
			this.HostNameComparisonMode = transport.HostNameComparisonMode;
			this.MaxBufferPoolSize = transport.MaxBufferPoolSize;
			this.MaxBufferSize = transport.MaxBufferSize;
			if (transport.IsMaxPendingConnectionsSet)
			{
				this.MaxConnections = transport.MaxPendingConnections;
			}
			if (transport.IsListenBacklogSet)
			{
				this.ListenBacklog = transport.ListenBacklog;
			}
			this.MaxReceivedMessageSize = transport.MaxReceivedMessageSize;
			this.PortSharingEnabled = transport.PortSharingEnabled;
			this.TransferMode = transport.TransferMode;
			this.ReaderQuotas = encoding.ReaderQuotas;
			this.TransactionFlow = context.Transactions;
			this.TransactionProtocol = context.TransactionProtocol;
			if (session != null)
			{
				this.session.InactivityTimeout = session.InactivityTimeout;
				this.session.Ordered = session.Ordered;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00025B20 File Offset: 0x00023D20
		private bool IsBindingElementsMatch(TcpTransportBindingElement transport, BinaryMessageEncodingBindingElement encoding, TransactionFlowBindingElement context, ReliableSessionBindingElement session)
		{
			if (!this.transport.IsMatch(transport))
			{
				return false;
			}
			if (!this.encoding.IsMatch(encoding))
			{
				return false;
			}
			if (!this.context.IsMatch(context))
			{
				return false;
			}
			if (this.reliableSession.Enabled)
			{
				if (!this.session.IsMatch(session))
				{
					return false;
				}
			}
			else if (session != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00025B84 File Offset: 0x00023D84
		private void ApplyConfiguration(string configurationName)
		{
			NetTcpBindingCollectionElement bindingCollectionElement = NetTcpBindingCollectionElement.GetBindingCollectionElement();
			NetTcpBindingElement netTcpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (netTcpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"netTcpBinding"
				})));
			}
			netTcpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00025BDC File Offset: 0x00023DDC
		private void CheckSettings()
		{
			if (!UnsafeNativeMethods.IsTailoredApplication.Value)
			{
				return;
			}
			NetTcpSecurity netTcpSecurity = this.Security;
			if (netTcpSecurity == null)
			{
				return;
			}
			SecurityMode mode = netTcpSecurity.Mode;
			if (mode == SecurityMode.None)
			{
				return;
			}
			if (mode == SecurityMode.Message)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedSecuritySetting", new object[]
				{
					"Mode",
					mode
				})));
			}
			if (mode == SecurityMode.TransportWithMessageCredential)
			{
				MessageSecurityOverTcp message = netTcpSecurity.Message;
				if (message != null)
				{
					MessageCredentialType clientCredentialType = message.ClientCredentialType;
					if (clientCredentialType == MessageCredentialType.Certificate || clientCredentialType == MessageCredentialType.IssuedToken || clientCredentialType == MessageCredentialType.Windows)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedSecuritySetting", new object[]
						{
							"Message.ClientCredentialType",
							clientCredentialType
						})));
					}
				}
			}
			TcpTransportSecurity tcpTransportSecurity = netTcpSecurity.Transport;
			if (tcpTransportSecurity != null && tcpTransportSecurity.ClientCredentialType == TcpClientCredentialType.Certificate)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedSecuritySetting", new object[]
				{
					"Transport.ClientCredentialType",
					tcpTransportSecurity.ClientCredentialType
				})));
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00025CE4 File Offset: 0x00023EE4
		[__DynamicallyInvokable]
		public override BindingElementCollection CreateBindingElements()
		{
			this.CheckSettings();
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			bindingElementCollection.Add(this.context);
			if (this.reliableSession.Enabled)
			{
				bindingElementCollection.Add(this.session);
			}
			SecurityBindingElement securityBindingElement = this.CreateMessageSecurity();
			if (securityBindingElement != null)
			{
				bindingElementCollection.Add(securityBindingElement);
			}
			bindingElementCollection.Add(this.encoding);
			BindingElement bindingElement = this.CreateTransportSecurity();
			if (bindingElement != null)
			{
				bindingElementCollection.Add(bindingElement);
			}
			this.transport.ExtendedProtectionPolicy = this.security.Transport.ExtendedProtectionPolicy;
			bindingElementCollection.Add(this.transport);
			return bindingElementCollection.Clone();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00025D80 File Offset: 0x00023F80
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 6)
			{
				return false;
			}
			TcpTransportBindingElement tcpTransportBindingElement = null;
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = null;
			TransactionFlowBindingElement transactionFlowBindingElement = null;
			ReliableSessionBindingElement reliableSessionBindingElement = null;
			SecurityBindingElement sbe = null;
			BindingElement bindingElement = null;
			foreach (BindingElement bindingElement2 in elements)
			{
				if (bindingElement2 is SecurityBindingElement)
				{
					sbe = (bindingElement2 as SecurityBindingElement);
				}
				else if (bindingElement2 is TransportBindingElement)
				{
					tcpTransportBindingElement = (bindingElement2 as TcpTransportBindingElement);
				}
				else if (bindingElement2 is MessageEncodingBindingElement)
				{
					binaryMessageEncodingBindingElement = (bindingElement2 as BinaryMessageEncodingBindingElement);
				}
				else if (bindingElement2 is TransactionFlowBindingElement)
				{
					transactionFlowBindingElement = (bindingElement2 as TransactionFlowBindingElement);
				}
				else if (bindingElement2 is ReliableSessionBindingElement)
				{
					reliableSessionBindingElement = (bindingElement2 as ReliableSessionBindingElement);
				}
				else
				{
					if (bindingElement != null)
					{
						return false;
					}
					bindingElement = bindingElement2;
				}
			}
			if (tcpTransportBindingElement == null)
			{
				return false;
			}
			if (binaryMessageEncodingBindingElement == null)
			{
				return false;
			}
			if (transactionFlowBindingElement == null)
			{
				transactionFlowBindingElement = NetTcpBinding.GetDefaultTransactionFlowBindingElement();
			}
			TcpTransportSecurity tcpTransportSecurity = new TcpTransportSecurity();
			UnifiedSecurityMode modeFromTransportSecurity = NetTcpBinding.GetModeFromTransportSecurity(bindingElement);
			NetTcpSecurity netTcpSecurity;
			if (!NetTcpBinding.TryCreateSecurity(sbe, modeFromTransportSecurity, reliableSessionBindingElement != null, bindingElement, tcpTransportSecurity, out netTcpSecurity))
			{
				return false;
			}
			if (!NetTcpBinding.SetTransportSecurity(bindingElement, netTcpSecurity.Mode, tcpTransportSecurity))
			{
				return false;
			}
			NetTcpBinding netTcpBinding = new NetTcpBinding(tcpTransportBindingElement, binaryMessageEncodingBindingElement, transactionFlowBindingElement, reliableSessionBindingElement, netTcpSecurity);
			if (!netTcpBinding.IsBindingElementsMatch(tcpTransportBindingElement, binaryMessageEncodingBindingElement, transactionFlowBindingElement, reliableSessionBindingElement))
			{
				return false;
			}
			binding = netTcpBinding;
			return true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00025EC8 File Offset: 0x000240C8
		private BindingElement CreateTransportSecurity()
		{
			return this.security.CreateTransportSecurity();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00025ED5 File Offset: 0x000240D5
		private static UnifiedSecurityMode GetModeFromTransportSecurity(BindingElement transport)
		{
			return NetTcpSecurity.GetModeFromTransportSecurity(transport);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00025EDD File Offset: 0x000240DD
		private static bool SetTransportSecurity(BindingElement transport, SecurityMode mode, TcpTransportSecurity transportSecurity)
		{
			return NetTcpSecurity.SetTransportSecurity(transport, mode, transportSecurity);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00025EE7 File Offset: 0x000240E7
		private SecurityBindingElement CreateMessageSecurity()
		{
			if (this.security.Mode == SecurityMode.Message || this.security.Mode == SecurityMode.TransportWithMessageCredential)
			{
				return this.security.CreateMessageSecurity(this.ReliableSession.Enabled);
			}
			return null;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00025F20 File Offset: 0x00024120
		private static bool TryCreateSecurity(SecurityBindingElement sbe, UnifiedSecurityMode mode, bool isReliableSession, BindingElement transportSecurity, TcpTransportSecurity tcpTransportSecurity, out NetTcpSecurity security)
		{
			if (sbe != null)
			{
				mode &= (UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
			}
			else
			{
				mode &= ~(UnifiedSecurityMode.Message | UnifiedSecurityMode.TransportWithMessageCredential);
			}
			SecurityMode mode2 = SecurityModeHelper.ToSecurityMode(mode);
			return NetTcpSecurity.TryCreate(sbe, mode2, isReliableSession, transportSecurity, tcpTransportSecurity, out security);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00025F57 File Offset: 0x00024157
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00025F67 File Offset: 0x00024167
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.security.InternalShouldSerialize();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00025F74 File Offset: 0x00024174
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTransactionProtocol()
		{
			return this.TransactionProtocol != NetTcpDefaults.TransactionProtocol;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00025F86 File Offset: 0x00024186
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReliableSession()
		{
			return !this.ReliableSession.Ordered || this.ReliableSession.InactivityTimeout != ReliableSessionDefaults.InactivityTimeout || this.ReliableSession.Enabled;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00025FB9 File Offset: 0x000241B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeListenBacklog()
		{
			return this.transport.ShouldSerializeListenBacklog();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00025FC6 File Offset: 0x000241C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMaxConnections()
		{
			return this.transport.ShouldSerializeListenBacklog();
		}

		// Token: 0x04000B7E RID: 2942
		private OptionalReliableSession reliableSession;

		// Token: 0x04000B7F RID: 2943
		private TcpTransportBindingElement transport;

		// Token: 0x04000B80 RID: 2944
		private BinaryMessageEncodingBindingElement encoding;

		// Token: 0x04000B81 RID: 2945
		private TransactionFlowBindingElement context;

		// Token: 0x04000B82 RID: 2946
		private ReliableSessionBindingElement session;

		// Token: 0x04000B83 RID: 2947
		private NetTcpSecurity security = new NetTcpSecurity();
	}
}
