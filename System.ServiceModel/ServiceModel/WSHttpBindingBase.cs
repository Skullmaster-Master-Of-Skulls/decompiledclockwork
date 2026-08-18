using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000161 RID: 353
	public abstract class WSHttpBindingBase : Binding, IBindingRuntimePreferences
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0002713C File Offset: 0x0002533C
		protected WSHttpBindingBase()
		{
			this.Initialize();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002714A File Offset: 0x0002534A
		protected WSHttpBindingBase(bool reliableSessionEnabled) : this()
		{
			this.ReliableSession.Enabled = reliableSessionEnabled;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002715E File Offset: 0x0002535E
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0002716B File Offset: 0x0002536B
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
				this.httpsTransport.BypassProxyOnLocal = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00027185 File Offset: 0x00025385
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00027192 File Offset: 0x00025392
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

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000271A0 File Offset: 0x000253A0
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x000271AD File Offset: 0x000253AD
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
				this.httpsTransport.HostNameComparisonMode = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000271C7 File Offset: 0x000253C7
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x000271D4 File Offset: 0x000253D4
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
				this.httpsTransport.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000271EE File Offset: 0x000253EE
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x000271FC File Offset: 0x000253FC
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
				this.httpsTransport.MaxReceivedMessageSize = value;
				this.mtomEncoding.MaxBufferSize = (int)value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00027256 File Offset: 0x00025456
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0002725E File Offset: 0x0002545E
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00027267 File Offset: 0x00025467
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x00027274 File Offset: 0x00025474
		[DefaultValue(null)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri ProxyAddress
		{
			get
			{
				return this.httpTransport.ProxyAddress;
			}
			set
			{
				this.httpTransport.ProxyAddress = value;
				this.httpsTransport.ProxyAddress = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002728E File Offset: 0x0002548E
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0002729B File Offset: 0x0002549B
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

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000272D2 File Offset: 0x000254D2
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x000272DA File Offset: 0x000254DA
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

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00027300 File Offset: 0x00025500
		public override string Scheme
		{
			get
			{
				return this.GetTransport().Scheme;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002730D File Offset: 0x0002550D
		public EnvelopeVersion EnvelopeVersion
		{
			get
			{
				return EnvelopeVersion.Soap12;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00027314 File Offset: 0x00025514
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00027321 File Offset: 0x00025521
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002733B File Offset: 0x0002553B
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00027348 File Offset: 0x00025548
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
				this.httpsTransport.UseDefaultWebProxy = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00027362 File Offset: 0x00025562
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00027365 File Offset: 0x00025565
		internal HttpTransportBindingElement HttpTransport
		{
			get
			{
				return this.httpTransport;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002736D File Offset: 0x0002556D
		internal HttpsTransportBindingElement HttpsTransport
		{
			get
			{
				return this.httpsTransport;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00027375 File Offset: 0x00025575
		internal ReliableSessionBindingElement ReliableSessionBindingElement
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002737D File Offset: 0x0002557D
		internal TransactionFlowBindingElement TransactionFlowBindingElement
		{
			get
			{
				return this.txFlow;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00027388 File Offset: 0x00025588
		private static TransactionFlowBindingElement GetDefaultTransactionFlowBindingElement()
		{
			return new TransactionFlowBindingElement(false)
			{
				TransactionProtocol = TransactionProtocol.WSAtomicTransactionOctober2004
			};
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000273A8 File Offset: 0x000255A8
		private void Initialize()
		{
			this.httpTransport = new HttpTransportBindingElement();
			this.httpsTransport = new HttpsTransportBindingElement();
			this.messageEncoding = WSMessageEncoding.Text;
			this.txFlow = WSHttpBindingBase.GetDefaultTransactionFlowBindingElement();
			this.session = new ReliableSessionBindingElement(true);
			this.textEncoding = new TextMessageEncodingBindingElement();
			this.textEncoding.MessageVersion = MessageVersion.Soap12WSAddressing10;
			this.mtomEncoding = new MtomMessageEncodingBindingElement();
			this.mtomEncoding.MessageVersion = MessageVersion.Soap12WSAddressing10;
			this.reliableSession = new OptionalReliableSession(this.session);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00027430 File Offset: 0x00025630
		private void InitializeFrom(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, TransactionFlowBindingElement txFlow, ReliableSessionBindingElement session)
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
			this.reliableSession.Enabled = (session != null);
			if (session != null)
			{
				this.session.InactivityTimeout = session.InactivityTimeout;
				this.session.Ordered = session.Ordered;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00027528 File Offset: 0x00025728
		private bool IsBindingElementsMatch(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, TransactionFlowBindingElement txFlow, ReliableSessionBindingElement session)
		{
			if (!this.GetTransport().IsMatch(transport))
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
			if (!this.txFlow.IsMatch(txFlow))
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

		// Token: 0x06000A60 RID: 2656 RVA: 0x000275AC File Offset: 0x000257AC
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			bindingElementCollection.Add(this.txFlow);
			if (this.reliableSession.Enabled)
			{
				bindingElementCollection.Add(this.session);
			}
			SecurityBindingElement securityBindingElement = this.CreateMessageSecurity();
			if (securityBindingElement != null)
			{
				bindingElementCollection.Add(securityBindingElement);
			}
			WSMessageEncodingHelper.SyncUpEncodingBindingElementProperties(this.textEncoding, this.mtomEncoding);
			if (this.MessageEncoding == WSMessageEncoding.Text)
			{
				bindingElementCollection.Add(this.textEncoding);
			}
			else if (this.MessageEncoding == WSMessageEncoding.Mtom)
			{
				bindingElementCollection.Add(this.mtomEncoding);
			}
			bindingElementCollection.Add(this.GetTransport());
			return bindingElementCollection.Clone();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00027644 File Offset: 0x00025844
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 6)
			{
				return false;
			}
			PrivacyNoticeBindingElement privacyNoticeBindingElement = null;
			TransactionFlowBindingElement transactionFlowBindingElement = null;
			ReliableSessionBindingElement rsbe = null;
			SecurityBindingElement securityBindingElement = null;
			MessageEncodingBindingElement messageEncodingBindingElement = null;
			HttpTransportBindingElement httpTransportBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is SecurityBindingElement)
				{
					securityBindingElement = (bindingElement as SecurityBindingElement);
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
					rsbe = (bindingElement as ReliableSessionBindingElement);
				}
				else
				{
					if (!(bindingElement is PrivacyNoticeBindingElement))
					{
						return false;
					}
					privacyNoticeBindingElement = (bindingElement as PrivacyNoticeBindingElement);
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
			if (!httpTransportBindingElement.AuthenticationScheme.IsSingleton())
			{
				return false;
			}
			HttpsTransportBindingElement httpsTransportBindingElement = httpTransportBindingElement as HttpsTransportBindingElement;
			if (securityBindingElement != null && httpsTransportBindingElement != null && httpsTransportBindingElement.RequireClientCertificate)
			{
				return false;
			}
			if ((privacyNoticeBindingElement != null || !WSHttpBinding.TryCreate(securityBindingElement, httpTransportBindingElement, rsbe, transactionFlowBindingElement, out binding)) && !WSFederationHttpBinding.TryCreate(securityBindingElement, httpTransportBindingElement, privacyNoticeBindingElement, rsbe, transactionFlowBindingElement, out binding) && !WS2007HttpBinding.TryCreate(securityBindingElement, httpTransportBindingElement, rsbe, transactionFlowBindingElement, out binding) && !WS2007FederationHttpBinding.TryCreate(securityBindingElement, httpTransportBindingElement, privacyNoticeBindingElement, rsbe, transactionFlowBindingElement, out binding))
			{
				return false;
			}
			if (transactionFlowBindingElement == null)
			{
				transactionFlowBindingElement = WSHttpBindingBase.GetDefaultTransactionFlowBindingElement();
				if (binding is WS2007HttpBinding || binding is WS2007FederationHttpBinding)
				{
					transactionFlowBindingElement.TransactionProtocol = TransactionProtocol.WSAtomicTransaction11;
				}
			}
			WSHttpBindingBase wshttpBindingBase = binding as WSHttpBindingBase;
			wshttpBindingBase.InitializeFrom(httpTransportBindingElement, messageEncodingBindingElement, transactionFlowBindingElement, rsbe);
			return wshttpBindingBase.IsBindingElementsMatch(httpTransportBindingElement, messageEncodingBindingElement, transactionFlowBindingElement, rsbe);
		}

		// Token: 0x06000A62 RID: 2658
		protected abstract TransportBindingElement GetTransport();

		// Token: 0x06000A63 RID: 2659
		protected abstract SecurityBindingElement CreateMessageSecurity();

		// Token: 0x06000A64 RID: 2660 RVA: 0x000277EC File Offset: 0x000259EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000277FC File Offset: 0x000259FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTextEncoding()
		{
			return !this.TextEncoding.Equals(TextEncoderDefaults.Encoding);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00027811 File Offset: 0x00025A11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReliableSession()
		{
			return !this.ReliableSession.Ordered || this.ReliableSession.InactivityTimeout != ReliableSessionDefaults.InactivityTimeout || this.ReliableSession.Enabled;
		}

		// Token: 0x04000BB1 RID: 2993
		private WSMessageEncoding messageEncoding;

		// Token: 0x04000BB2 RID: 2994
		private OptionalReliableSession reliableSession;

		// Token: 0x04000BB3 RID: 2995
		private HttpTransportBindingElement httpTransport;

		// Token: 0x04000BB4 RID: 2996
		private HttpsTransportBindingElement httpsTransport;

		// Token: 0x04000BB5 RID: 2997
		private TextMessageEncodingBindingElement textEncoding;

		// Token: 0x04000BB6 RID: 2998
		private MtomMessageEncodingBindingElement mtomEncoding;

		// Token: 0x04000BB7 RID: 2999
		private TransactionFlowBindingElement txFlow;

		// Token: 0x04000BB8 RID: 3000
		private ReliableSessionBindingElement session;
	}
}
