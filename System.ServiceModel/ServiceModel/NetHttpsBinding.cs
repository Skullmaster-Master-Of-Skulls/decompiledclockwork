using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000144 RID: 324
	public class NetHttpsBinding : HttpBindingBase
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0002410C File Offset: 0x0002230C
		public NetHttpsBinding() : this(BasicHttpsSecurityMode.Transport)
		{
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00024115 File Offset: 0x00022315
		public NetHttpsBinding(BasicHttpsSecurityMode securityMode)
		{
			this.Initialize();
			this.basicHttpsSecurity.Mode = securityMode;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002412F File Offset: 0x0002232F
		public NetHttpsBinding(BasicHttpsSecurityMode securityMode, bool reliableSessionEnabled) : this(securityMode)
		{
			this.ReliableSession.Enabled = reliableSessionEnabled;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00024144 File Offset: 0x00022344
		public NetHttpsBinding(string configurationName)
		{
			this.Initialize();
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00024159 File Offset: 0x00022359
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00024161 File Offset: 0x00022361
		[DefaultValue(NetHttpMessageEncoding.Binary)]
		public NetHttpMessageEncoding MessageEncoding
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

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0002416A File Offset: 0x0002236A
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x00024172 File Offset: 0x00022372
		public BasicHttpsSecurity Security
		{
			get
			{
				return this.basicHttpsSecurity;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.basicHttpsSecurity = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0002418E File Offset: 0x0002238E
		internal override BasicHttpSecurity BasicHttpSecurity
		{
			get
			{
				return this.basicHttpsSecurity.BasicHttpSecurity;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0002419B File Offset: 0x0002239B
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x000241A3 File Offset: 0x000223A3
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

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000241C4 File Offset: 0x000223C4
		public WebSocketTransportSettings WebSocketSettings
		{
			get
			{
				return base.InternalWebSocketSettings;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000241CC File Offset: 0x000223CC
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

		// Token: 0x06000904 RID: 2308 RVA: 0x00024248 File Offset: 0x00022448
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

		// Token: 0x06000905 RID: 2309 RVA: 0x000242DB File Offset: 0x000224DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReliableSession()
		{
			return !this.ReliableSession.Ordered || this.ReliableSession.InactivityTimeout != ReliableSessionDefaults.InactivityTimeout || this.ReliableSession.Enabled;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002430E File Offset: 0x0002250E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002431B File Offset: 0x0002251B
		internal override void SetReaderQuotas(XmlDictionaryReaderQuotas readerQuotas)
		{
			readerQuotas.CopyTo(this.binaryMessageEncodingBindingElement.ReaderQuotas);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002432E File Offset: 0x0002252E
		internal override EnvelopeVersion GetEnvelopeVersion()
		{
			return EnvelopeVersion.Soap12;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00024338 File Offset: 0x00022538
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

		// Token: 0x0600090A RID: 2314 RVA: 0x00024398 File Offset: 0x00022598
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
			base.InternalWebSocketSettings.TransportUsage = WebSocketTransportUsage.WhenDuplex;
			base.InternalWebSocketSettings.SubProtocol = "soap";
			this.basicHttpsSecurity = new BasicHttpsSecurity();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00024428 File Offset: 0x00022628
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

		// Token: 0x0600090C RID: 2316 RVA: 0x000244A4 File Offset: 0x000226A4
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

		// Token: 0x0600090D RID: 2317 RVA: 0x00024528 File Offset: 0x00022728
		private void ApplyConfiguration(string configurationName)
		{
			NetHttpsBindingCollectionElement bindingCollectionElement = NetHttpsBindingCollectionElement.GetBindingCollectionElement();
			NetHttpsBindingElement netHttpsBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (netHttpsBindingElement == null)
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"netHttpBinding"
				})));
			}
			netHttpsBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x04000B60 RID: 2912
		private BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement;

		// Token: 0x04000B61 RID: 2913
		private ReliableSessionBindingElement session;

		// Token: 0x04000B62 RID: 2914
		private OptionalReliableSession reliableSession;

		// Token: 0x04000B63 RID: 2915
		private NetHttpMessageEncoding messageEncoding;

		// Token: 0x04000B64 RID: 2916
		private BasicHttpsSecurity basicHttpsSecurity;
	}
}
