using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000141 RID: 321
	[__DynamicallyInvokable]
	public class NetHttpBinding : HttpBindingBase
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x00023B23 File Offset: 0x00021D23
		[__DynamicallyInvokable]
		public NetHttpBinding() : this(BasicHttpSecurityMode.None)
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00023B2C File Offset: 0x00021D2C
		[__DynamicallyInvokable]
		public NetHttpBinding(BasicHttpSecurityMode securityMode)
		{
			this.Initialize();
			this.basicHttpSecurity.Mode = securityMode;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00023B46 File Offset: 0x00021D46
		public NetHttpBinding(BasicHttpSecurityMode securityMode, bool reliableSessionEnabled) : this(securityMode)
		{
			this.ReliableSession.Enabled = reliableSessionEnabled;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00023B5B File Offset: 0x00021D5B
		[__DynamicallyInvokable]
		public NetHttpBinding(string configurationName)
		{
			this.Initialize();
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00023B70 File Offset: 0x00021D70
		private NetHttpBinding(BasicHttpSecurity security)
		{
			this.Initialize();
			this.basicHttpSecurity = security;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00023B85 File Offset: 0x00021D85
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00023B8D File Offset: 0x00021D8D
		[DefaultValue(NetHttpMessageEncoding.Binary)]
		[__DynamicallyInvokable]
		public NetHttpMessageEncoding MessageEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageEncoding;
			}
			[__DynamicallyInvokable]
			set
			{
				this.messageEncoding = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00023B96 File Offset: 0x00021D96
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x00023B9E File Offset: 0x00021D9E
		[__DynamicallyInvokable]
		public BasicHttpSecurity Security
		{
			[__DynamicallyInvokable]
			get
			{
				return this.basicHttpSecurity;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.basicHttpSecurity = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00023BBA File Offset: 0x00021DBA
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00023BC2 File Offset: 0x00021DC2
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
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.reliableSession.CopySettings(value);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00023BE3 File Offset: 0x00021DE3
		[__DynamicallyInvokable]
		public WebSocketTransportSettings WebSocketSettings
		{
			[__DynamicallyInvokable]
			get
			{
				return base.InternalWebSocketSettings;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00023BEB File Offset: 0x00021DEB
		internal override BasicHttpSecurity BasicHttpSecurity
		{
			get
			{
				return this.basicHttpSecurity;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00023BF4 File Offset: 0x00021DF4
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingParameterCollection parameters)
		{
			if ((this.BasicHttpSecurity.Mode == BasicHttpSecurityMode.Transport || this.BasicHttpSecurity.Mode == BasicHttpSecurityMode.TransportCredentialOnly) && this.BasicHttpSecurity.Transport.ClientCredentialType == HttpClientCredentialType.InheritedFromHost)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpClientCredentialTypeInvalid", new object[]
				{
					this.BasicHttpSecurity.Transport.ClientCredentialType
				})));
			}
			return base.BuildChannelFactory<TChannel>(parameters);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00023C70 File Offset: 0x00021E70
		[__DynamicallyInvokable]
		public override BindingElementCollection CreateBindingElements()
		{
			this.CheckSettings();
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			if (this.reliableSession.Enabled)
			{
				bindingElementCollection.Add(this.session);
			}
			SecurityBindingElement securityBindingElement = this.BasicHttpSecurity.CreateMessageSecurity();
			if (securityBindingElement != null)
			{
				bindingElementCollection.Add(securityBindingElement);
			}
			NetHttpMessageEncoding netHttpMessageEncoding = this.MessageEncoding;
			if (netHttpMessageEncoding != NetHttpMessageEncoding.Text)
			{
				if (netHttpMessageEncoding != NetHttpMessageEncoding.Mtom)
				{
					bindingElementCollection.Add(this.binaryMessageEncodingBindingElement);
				}
				else
				{
					bindingElementCollection.Add(base.MtomMessageEncodingBindingElement);
				}
			}
			else
			{
				bindingElementCollection.Add(base.TextMessageEncodingBindingElement);
			}
			bindingElementCollection.Add(base.GetTransport());
			return bindingElementCollection.Clone();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00023D03 File Offset: 0x00021F03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReliableSession()
		{
			return !this.ReliableSession.Ordered || this.ReliableSession.InactivityTimeout != ReliableSessionDefaults.InactivityTimeout || this.ReliableSession.Enabled;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00023D36 File Offset: 0x00021F36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00023D44 File Offset: 0x00021F44
		internal static bool TryCreate(BindingElementCollection elements, out Binding binding)
		{
			binding = null;
			if (elements.Count > 4)
			{
				return false;
			}
			ReliableSessionBindingElement reliableSessionBindingElement = null;
			SecurityBindingElement securityBindingElement = null;
			MessageEncodingBindingElement messageEncodingBindingElement = null;
			HttpTransportBindingElement httpTransportBindingElement = null;
			foreach (BindingElement bindingElement in elements)
			{
				if (bindingElement is ReliableSessionBindingElement)
				{
					reliableSessionBindingElement = (bindingElement as ReliableSessionBindingElement);
				}
				if (bindingElement is SecurityBindingElement)
				{
					securityBindingElement = (bindingElement as SecurityBindingElement);
				}
				else if (bindingElement is TransportBindingElement)
				{
					httpTransportBindingElement = (bindingElement as HttpTransportBindingElement);
				}
				else
				{
					if (!(bindingElement is MessageEncodingBindingElement))
					{
						return false;
					}
					messageEncodingBindingElement = (bindingElement as MessageEncodingBindingElement);
				}
			}
			if (httpTransportBindingElement == null || httpTransportBindingElement.WebSocketSettings.TransportUsage != WebSocketTransportUsage.Always)
			{
				return false;
			}
			HttpsTransportBindingElement httpsTransportBindingElement = httpTransportBindingElement as HttpsTransportBindingElement;
			if (securityBindingElement != null && httpsTransportBindingElement != null && httpsTransportBindingElement.RequireClientCertificate)
			{
				return false;
			}
			HttpTransportSecurity transportSecurity = new HttpTransportSecurity();
			UnifiedSecurityMode mode;
			if (!HttpBindingBase.GetSecurityModeFromTransport(httpTransportBindingElement, transportSecurity, out mode))
			{
				return false;
			}
			if (messageEncodingBindingElement == null)
			{
				return false;
			}
			if (!(messageEncodingBindingElement is TextMessageEncodingBindingElement) && !(messageEncodingBindingElement is MtomMessageEncodingBindingElement) && !(messageEncodingBindingElement is BinaryMessageEncodingBindingElement))
			{
				return false;
			}
			if (messageEncodingBindingElement.MessageVersion != MessageVersion.Soap12WSAddressing10)
			{
				return false;
			}
			BasicHttpSecurity security;
			if (!HttpBindingBase.TryCreateSecurity(securityBindingElement, mode, transportSecurity, out security))
			{
				return false;
			}
			NetHttpBinding netHttpBinding = new NetHttpBinding(security);
			netHttpBinding.InitializeFrom(httpTransportBindingElement, messageEncodingBindingElement, reliableSessionBindingElement);
			if (!netHttpBinding.IsBindingElementsMatch(httpTransportBindingElement, messageEncodingBindingElement, reliableSessionBindingElement))
			{
				return false;
			}
			binding = netHttpBinding;
			return true;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00023E9C File Offset: 0x0002209C
		internal override void SetReaderQuotas(XmlDictionaryReaderQuotas readerQuotas)
		{
			readerQuotas.CopyTo(this.binaryMessageEncodingBindingElement.ReaderQuotas);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00023EAF File Offset: 0x000220AF
		internal override EnvelopeVersion GetEnvelopeVersion()
		{
			return EnvelopeVersion.Soap12;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00023EB8 File Offset: 0x000220B8
		internal override void CheckSettings()
		{
			base.CheckSettings();
			if (this.MessageEncoding == NetHttpMessageEncoding.Mtom && UnsafeNativeMethods.IsTailoredApplication.Value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedBindingProperty", new object[]
				{
					"MessageEncoding",
					this.MessageEncoding
				})));
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00023F18 File Offset: 0x00022118
		private void Initialize()
		{
			this.messageEncoding = NetHttpMessageEncoding.Binary;
			this.binaryMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement
			{
				MessageVersion = MessageVersion.Soap12WSAddressing10
			};
			base.TextMessageEncodingBindingElement.MessageVersion = MessageVersion.Soap12WSAddressing10;
			base.MtomMessageEncodingBindingElement.MessageVersion = MessageVersion.Soap12WSAddressing10;
			this.session = new ReliableSessionBindingElement();
			this.reliableSession = new OptionalReliableSession(this.session);
			this.WebSocketSettings.TransportUsage = WebSocketTransportUsage.WhenDuplex;
			this.WebSocketSettings.SubProtocol = "soap";
			this.basicHttpSecurity = new BasicHttpSecurity();
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00023FA8 File Offset: 0x000221A8
		private void InitializeFrom(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, ReliableSessionBindingElement session)
		{
			this.InitializeFrom(transport, encoding);
			if (encoding is BinaryMessageEncodingBindingElement)
			{
				this.messageEncoding = NetHttpMessageEncoding.Binary;
				BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = (BinaryMessageEncodingBindingElement)encoding;
				base.ReaderQuotas = binaryMessageEncodingBindingElement.ReaderQuotas;
			}
			if (encoding is TextMessageEncodingBindingElement)
			{
				this.messageEncoding = NetHttpMessageEncoding.Text;
			}
			else if (encoding is MtomMessageEncodingBindingElement)
			{
				this.messageEncoding = NetHttpMessageEncoding.Mtom;
			}
			if (session != null)
			{
				this.session.InactivityTimeout = session.InactivityTimeout;
				this.session.Ordered = session.Ordered;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00024024 File Offset: 0x00022224
		private void ApplyConfiguration(string configurationName)
		{
			NetHttpBindingCollectionElement bindingCollectionElement = NetHttpBindingCollectionElement.GetBindingCollectionElement();
			NetHttpBindingElement netHttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (netHttpBindingElement == null)
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"netHttpBinding"
				})));
			}
			netHttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002407C File Offset: 0x0002227C
		private bool IsBindingElementsMatch(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding, ReliableSessionBindingElement session)
		{
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
			NetHttpMessageEncoding netHttpMessageEncoding = this.MessageEncoding;
			if (netHttpMessageEncoding != NetHttpMessageEncoding.Text)
			{
				if (netHttpMessageEncoding != NetHttpMessageEncoding.Mtom)
				{
					if (!this.binaryMessageEncodingBindingElement.IsMatch(encoding))
					{
						return false;
					}
				}
				else if (!base.MtomMessageEncodingBindingElement.IsMatch(encoding))
				{
					return false;
				}
			}
			else if (!base.TextMessageEncodingBindingElement.IsMatch(encoding))
			{
				return false;
			}
			return base.GetTransport().IsMatch(transport);
		}

		// Token: 0x04000B57 RID: 2903
		private BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement;

		// Token: 0x04000B58 RID: 2904
		private ReliableSessionBindingElement session;

		// Token: 0x04000B59 RID: 2905
		private OptionalReliableSession reliableSession;

		// Token: 0x04000B5A RID: 2906
		private NetHttpMessageEncoding messageEncoding;

		// Token: 0x04000B5B RID: 2907
		private BasicHttpSecurity basicHttpSecurity;
	}
}
