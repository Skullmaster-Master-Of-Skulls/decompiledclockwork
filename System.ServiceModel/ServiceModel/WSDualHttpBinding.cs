using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000162 RID: 354
	public class WSDualHttpBinding : Binding, IBindingRuntimePreferences
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x00027844 File Offset: 0x00025A44
		public WSDualHttpBinding(string configName) : this()
		{
			this.ApplyConfiguration(configName);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00027853 File Offset: 0x00025A53
		public WSDualHttpBinding(WSDualHttpSecurityMode securityMode) : this()
		{
			this.security.Mode = securityMode;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00027867 File Offset: 0x00025A67
		public WSDualHttpBinding()
		{
			this.Initialize();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00027880 File Offset: 0x00025A80
		private WSDualHttpBinding(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, TransactionFlowBindingElement txFlow, ReliableSessionBindingElement session, CompositeDuplexBindingElement compositeDuplex, OneWayBindingElement oneWay, WSDualHttpSecurity security) : this()
		{
			this.security = security;
			this.InitializeFrom(transport, encoding, txFlow, session, compositeDuplex, oneWay);
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0002789F File Offset: 0x00025A9F
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x000278AC File Offset: 0x00025AAC
		[DefaultValue(false)]
		public bool BypassProxyOnLocal
		{
			get
			{
				return this.httpTransport.BypassProxyOnLocal;
			}
			set
			{
				this.httpTransport.BypassProxyOnLocal = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000278BA File Offset: 0x00025ABA
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000278C7 File Offset: 0x00025AC7
		[DefaultValue(null)]
		public Uri ClientBaseAddress
		{
			get
			{
				return this.compositeDuplex.ClientBaseAddress;
			}
			set
			{
				this.compositeDuplex.ClientBaseAddress = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000278D5 File Offset: 0x00025AD5
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x000278E2 File Offset: 0x00025AE2
		[DefaultValue(false)]
		public bool TransactionFlow
		{
			get
			{
				return this.txFlow.Transactions;
			}
			set
			{
				this.txFlow.Transactions = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000278F0 File Offset: 0x00025AF0
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x000278FD File Offset: 0x00025AFD
		[DefaultValue(HostNameComparisonMode.StrongWildcard)]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.httpTransport.HostNameComparisonMode;
			}
			set
			{
				this.httpTransport.HostNameComparisonMode = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002790B File Offset: 0x00025B0B
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00027918 File Offset: 0x00025B18
		[DefaultValue(524288L)]
		public long MaxBufferPoolSize
		{
			get
			{
				return this.httpTransport.MaxBufferPoolSize;
			}
			set
			{
				this.httpTransport.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00027926 File Offset: 0x00025B26
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x00027934 File Offset: 0x00025B34
		[DefaultValue(65536L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.httpTransport.MaxReceivedMessageSize;
			}
			set
			{
				if (value > 2147483647L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value.MaxReceivedMessageSize", SR.GetString("MaxReceivedMessageSizeMustBeInIntegerRange")));
				}
				this.httpTransport.MaxReceivedMessageSize = value;
				this.mtomEncoding.MaxBufferSize = (int)value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00027982 File Offset: 0x00025B82
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0002798A File Offset: 0x00025B8A
		[DefaultValue(WSMessageEncoding.Text)]
		public WSMessageEncoding MessageEncoding
		{
			get
			{
				return this.messageEncoding;
			}
			set
			{
				this.messageEncoding = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00027993 File Offset: 0x00025B93
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x000279A0 File Offset: 0x00025BA0
		[DefaultValue(null)]
		public Uri ProxyAddress
		{
			get
			{
				return this.httpTransport.ProxyAddress;
			}
			set
			{
				this.httpTransport.ProxyAddress = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000279AE File Offset: 0x00025BAE
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x000279BB File Offset: 0x00025BBB
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.textEncoding.ReaderQuotas;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				value.CopyTo(this.textEncoding.ReaderQuotas);
				value.CopyTo(this.mtomEncoding.ReaderQuotas);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000279F2 File Offset: 0x00025BF2
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x000279FA File Offset: 0x00025BFA
		public ReliableSession ReliableSession
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00027A20 File Offset: 0x00025C20
		public override string Scheme
		{
			get
			{
				return this.httpTransport.Scheme;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00027A2D File Offset: 0x00025C2D
		public EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00027A34 File Offset: 0x00025C34
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00027A41 File Offset: 0x00025C41
		[TypeConverter(typeof(EncodingConverter))]
		public Encoding TextEncoding
		{
			get
			{
				return this.textEncoding.WriteEncoding;
			}
			set
			{
				this.textEncoding.WriteEncoding = value;
				this.mtomEncoding.WriteEncoding = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00027A5B File Offset: 0x00025C5B
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00027A68 File Offset: 0x00025C68
		[DefaultValue(true)]
		public bool UseDefaultWebProxy
		{
			get
			{
				return this.httpTransport.UseDefaultWebProxy;
			}
			set
			{
				this.httpTransport.UseDefaultWebProxy = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00027A76 File Offset: 0x00025C76
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00027A7E File Offset: 0x00025C7E
		public WSDualHttpSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.security = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00027A9A File Offset: 0x00025C9A
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00027AA0 File Offset: 0x00025CA0
		private static TransactionFlowBindingElement GetDefaultTransactionFlowBindingElement()
		{
			return new TransactionFlowBindingElement(false)
			{
				TransactionProtocol = TransactionProtocol.WSAtomicTransactionOctober2004
			};
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00027AC0 File Offset: 0x00025CC0
		private void Initialize()
		{
			this.httpTransport = new HttpTransportBindingElement();
			this.messageEncoding = WSMessageEncoding.Text;
			this.txFlow = WSDualHttpBinding.GetDefaultTransactionFlowBindingElement();
			this.session = new ReliableSessionBindingElement(true);
			this.textEncoding = new TextMessageEncodingBindingElement();
			this.textEncoding.MessageVersion = MessageVersion.Soap12WSAddressing10;
			this.mtomEncoding = new MtomMessageEncodingBindingElement();
			this.mtomEncoding.MessageVersion = MessageVersion.Soap12WSAddressing10;
			this.compositeDuplex = new CompositeDuplexBindingElement();
			this.reliableSession = new ReliableSession(this.session);
			this.oneWay = new OneWayBindingElement();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00027B54 File Offset: 0x00025D54
		private void InitializeFrom(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, TransactionFlowBindingElement txFlow, ReliableSessionBindingElement session, CompositeDuplexBindingElement compositeDuplex, OneWayBindingElement oneWay)
		{
			this.BypassProxyOnLocal = transport.BypassProxyOnLocal;
			this.HostNameComparisonMode = transport.HostNameComparisonMode;
			this.MaxBufferPoolSize = transport.MaxBufferPoolSize;
			this.MaxReceivedMessageSize = transport.MaxReceivedMessageSize;
			this.ProxyAddress = transport.ProxyAddress;
			this.UseDefaultWebProxy = transport.UseDefaultWebProxy;
			if (encoding is TextMessageEncodingBindingElement)
			{
				this.MessageEncoding = WSMessageEncoding.Text;
				TextMessageEncodingBindingElement textMessageEncodingBindingElement = (TextMessageEncodingBindingElement)encoding;
				this.TextEncoding = textMessageEncodingBindingElement.WriteEncoding;
				this.ReaderQuotas = textMessageEncodingBindingElement.ReaderQuotas;
			}
			else if (encoding is MtomMessageEncodingBindingElement)
			{
				this.messageEncoding = WSMessageEncoding.Mtom;
				MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = (MtomMessageEncodingBindingElement)encoding;
				this.TextEncoding = mtomMessageEncodingBindingElement.WriteEncoding;
				this.ReaderQuotas = mtomMessageEncodingBindingElement.ReaderQuotas;
			}
			this.TransactionFlow = txFlow.Transactions;
			this.ClientBaseAddress = compositeDuplex.ClientBaseAddress;
			if (session != null)
			{
				this.session.InactivityTimeout = session.InactivityTimeout;
				this.session.Ordered = session.Ordered;
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00027C48 File Offset: 0x00025E48
		private bool IsBindingElementsMatch(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, TransactionFlowBindingElement txFlow, ReliableSessionBindingElement session, CompositeDuplexBindingElement compositeDuplex, OneWayBindingElement oneWay)
		{
			if (!this.httpTransport.IsMatch(transport))
			{
				return false;
			}
			if (this.MessageEncoding == WSMessageEncoding.Text)
			{
				if (!this.textEncoding.IsMatch(encoding))
				{
					return false;
				}
			}
			else if (this.MessageEncoding == WSMessageEncoding.Mtom && !this.mtomEncoding.IsMatch(encoding))
			{
				return false;
			}
			return this.txFlow.IsMatch(txFlow) && this.session.IsMatch(session) && this.compositeDuplex.IsMatch(compositeDuplex) && this.oneWay.IsMatch(oneWay);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00027CDC File Offset: 0x00025EDC
		private void ApplyConfiguration(string configurationName)
		{
			WSDualHttpBindingCollectionElement bindingCollectionElement = WSDualHttpBindingCollectionElement.GetBindingCollectionElement();
			WSDualHttpBindingElement wsdualHttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (wsdualHttpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"wsDualHttpBinding"
				})));
			}
			wsdualHttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00027D32 File Offset: 0x00025F32
		private SecurityBindingElement CreateMessageSecurity()
		{
			return this.Security.CreateMessageSecurity();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00027D3F File Offset: 0x00025F3F
		private static bool TryCreateSecurity(SecurityBindingElement securityElement, out WSDualHttpSecurity security)
		{
			return WSDualHttpSecurity.TryCreate(securityElement, out security);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00027D48 File Offset: 0x00025F48
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			bindingElementCollection.Add(this.txFlow);
			bindingElementCollection.Add(this.session);
			SecurityBindingElement securityBindingElement = this.CreateMessageSecurity();
			if (securityBindingElement != null)
			{
				bindingElementCollection.Add(securityBindingElement);
			}
			bindingElementCollection.Add(this.compositeDuplex);
			bindingElementCollection.Add(this.oneWay);
			WSMessageEncodingHelper.SyncUpEncodingBindingElementProperties(this.textEncoding, this.mtomEncoding);
			if (this.MessageEncoding == WSMessageEncoding.Text)
			{
				bindingElementCollection.Add(this.textEncoding);
			}
			else if (this.MessageEncoding == WSMessageEncoding.Mtom)
			{
				bindingElementCollection.Add(this.mtomEncoding);
			}
			bindingElementCollection.Add(this.httpTransport);
			return bindingElementCollection.Clone();
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00027DEC File Offset: 0x00025FEC
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 7)
			{
				return false;
			}
			SecurityBindingElement securityElement = null;
			HttpTransportBindingElement httpTransportBindingElement = null;
			MessageEncodingBindingElement messageEncodingBindingElement = null;
			TransactionFlowBindingElement transactionFlowBindingElement = null;
			ReliableSessionBindingElement reliableSessionBindingElement = null;
			CompositeDuplexBindingElement compositeDuplexBindingElement = null;
			OneWayBindingElement oneWayBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is SecurityBindingElement)
				{
					securityElement = (bindingElement as SecurityBindingElement);
				}
				else if (bindingElement is TransportBindingElement)
				{
					httpTransportBindingElement = (bindingElement as HttpTransportBindingElement);
				}
				else if (bindingElement is MessageEncodingBindingElement)
				{
					messageEncodingBindingElement = (bindingElement as MessageEncodingBindingElement);
				}
				else if (bindingElement is TransactionFlowBindingElement)
				{
					transactionFlowBindingElement = (bindingElement as TransactionFlowBindingElement);
				}
				else if (bindingElement is ReliableSessionBindingElement)
				{
					reliableSessionBindingElement = (bindingElement as ReliableSessionBindingElement);
				}
				else if (bindingElement is CompositeDuplexBindingElement)
				{
					compositeDuplexBindingElement = (bindingElement as CompositeDuplexBindingElement);
				}
				else
				{
					if (!(bindingElement is OneWayBindingElement))
					{
						return false;
					}
					oneWayBindingElement = (bindingElement as OneWayBindingElement);
				}
			}
			if (httpTransportBindingElement == null)
			{
				return false;
			}
			if (messageEncodingBindingElement == null)
			{
				return false;
			}
			if (!messageEncodingBindingElement.CheckEncodingVersion(EnvelopeVersion.Soap12))
			{
				return false;
			}
			if (compositeDuplexBindingElement == null)
			{
				return false;
			}
			if (oneWayBindingElement == null)
			{
				return false;
			}
			if (reliableSessionBindingElement == null)
			{
				return false;
			}
			if (transactionFlowBindingElement == null)
			{
				transactionFlowBindingElement = WSDualHttpBinding.GetDefaultTransactionFlowBindingElement();
			}
			WSDualHttpSecurity wsdualHttpSecurity;
			if (!WSDualHttpBinding.TryCreateSecurity(securityElement, out wsdualHttpSecurity))
			{
				return false;
			}
			WSDualHttpBinding wsdualHttpBinding = new WSDualHttpBinding(httpTransportBindingElement, messageEncodingBindingElement, transactionFlowBindingElement, reliableSessionBindingElement, compositeDuplexBindingElement, oneWayBindingElement, wsdualHttpSecurity);
			if (!wsdualHttpBinding.IsBindingElementsMatch(httpTransportBindingElement, messageEncodingBindingElement, transactionFlowBindingElement, reliableSessionBindingElement, compositeDuplexBindingElement, oneWayBindingElement))
			{
				return false;
			}
			binding = wsdualHttpBinding;
			return true;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00027F58 File Offset: 0x00026158
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00027F68 File Offset: 0x00026168
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTextEncoding()
		{
			return !this.TextEncoding.Equals(TextEncoderDefaults.Encoding);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00027F7D File Offset: 0x0002617D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReliableSession()
		{
			return !this.ReliableSession.Ordered || this.ReliableSession.InactivityTimeout != ReliableSessionDefaults.InactivityTimeout;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00027FA3 File Offset: 0x000261A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x04000BB9 RID: 3001
		private WSMessageEncoding messageEncoding;

		// Token: 0x04000BBA RID: 3002
		private ReliableSession reliableSession;

		// Token: 0x04000BBB RID: 3003
		private HttpTransportBindingElement httpTransport;

		// Token: 0x04000BBC RID: 3004
		private TextMessageEncodingBindingElement textEncoding;

		// Token: 0x04000BBD RID: 3005
		private MtomMessageEncodingBindingElement mtomEncoding;

		// Token: 0x04000BBE RID: 3006
		private TransactionFlowBindingElement txFlow;

		// Token: 0x04000BBF RID: 3007
		private ReliableSessionBindingElement session;

		// Token: 0x04000BC0 RID: 3008
		private CompositeDuplexBindingElement compositeDuplex;

		// Token: 0x04000BC1 RID: 3009
		private OneWayBindingElement oneWay;

		// Token: 0x04000BC2 RID: 3010
		private WSDualHttpSecurity security = new WSDualHttpSecurity();
	}
}
