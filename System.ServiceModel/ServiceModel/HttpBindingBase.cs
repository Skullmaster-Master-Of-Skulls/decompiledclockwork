using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000128 RID: 296
	[__DynamicallyInvokable]
	public abstract class HttpBindingBase : Binding, IBindingRuntimePreferences
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x000211D0 File Offset: 0x0001F3D0
		internal HttpBindingBase()
		{
			this.httpTransport = new HttpTransportBindingElement();
			this.httpsTransport = new HttpsTransportBindingElement();
			this.textEncoding = new TextMessageEncodingBindingElement();
			this.textEncoding.MessageVersion = MessageVersion.Soap11;
			this.mtomEncoding = new MtomMessageEncodingBindingElement();
			this.mtomEncoding.MessageVersion = MessageVersion.Soap11;
			this.httpsTransport.WebSocketSettings = this.httpTransport.WebSocketSettings;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00021245 File Offset: 0x0001F445
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00021252 File Offset: 0x0001F452
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool AllowCookies
		{
			[__DynamicallyInvokable]
			get
			{
				return this.httpTransport.AllowCookies;
			}
			[__DynamicallyInvokable]
			set
			{
				this.httpTransport.AllowCookies = value;
				this.httpsTransport.AllowCookies = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0002126C File Offset: 0x0001F46C
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00021279 File Offset: 0x0001F479
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00021293 File Offset: 0x0001F493
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x000212A0 File Offset: 0x0001F4A0
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000212BA File Offset: 0x0001F4BA
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x000212C7 File Offset: 0x0001F4C7
		[DefaultValue(65536)]
		[__DynamicallyInvokable]
		public int MaxBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.httpTransport.MaxBufferSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.httpTransport.MaxBufferSize = value;
				this.httpsTransport.MaxBufferSize = value;
				this.mtomEncoding.MaxBufferSize = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000212ED File Offset: 0x0001F4ED
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000212FA File Offset: 0x0001F4FA
		[DefaultValue(524288L)]
		[__DynamicallyInvokable]
		public long MaxBufferPoolSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.httpTransport.MaxBufferPoolSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.httpTransport.MaxBufferPoolSize = value;
				this.httpsTransport.MaxBufferPoolSize = value;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00021314 File Offset: 0x0001F514
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00021321 File Offset: 0x0001F521
		[DefaultValue(65536L)]
		[__DynamicallyInvokable]
		public long MaxReceivedMessageSize
		{
			[__DynamicallyInvokable]
			get
			{
				return this.httpTransport.MaxReceivedMessageSize;
			}
			[__DynamicallyInvokable]
			set
			{
				this.httpTransport.MaxReceivedMessageSize = value;
				this.httpsTransport.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0002133B File Offset: 0x0001F53B
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00021348 File Offset: 0x0001F548
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00021362 File Offset: 0x0001F562
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0002136F File Offset: 0x0001F56F
		[__DynamicallyInvokable]
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			[__DynamicallyInvokable]
			get
			{
				return this.textEncoding.ReaderQuotas;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				value.CopyTo(this.textEncoding.ReaderQuotas);
				value.CopyTo(this.mtomEncoding.ReaderQuotas);
				this.SetReaderQuotas(value);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000213AD File Offset: 0x0001F5AD
		[__DynamicallyInvokable]
		public override string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetTransport().Scheme;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x000213BA File Offset: 0x0001F5BA
		[__DynamicallyInvokable]
		public EnvelopeVersion EnvelopeVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetEnvelopeVersion();
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x000213C2 File Offset: 0x0001F5C2
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x000213CF File Offset: 0x0001F5CF
		[TypeConverter(typeof(EncodingConverter))]
		[__DynamicallyInvokable]
		public Encoding TextEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.textEncoding.WriteEncoding;
			}
			[__DynamicallyInvokable]
			set
			{
				this.textEncoding.WriteEncoding = value;
				this.mtomEncoding.WriteEncoding = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x000213E9 File Offset: 0x0001F5E9
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x000213F6 File Offset: 0x0001F5F6
		[DefaultValue(TransferMode.Buffered)]
		[__DynamicallyInvokable]
		public TransferMode TransferMode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.httpTransport.TransferMode;
			}
			[__DynamicallyInvokable]
			set
			{
				this.httpTransport.TransferMode = value;
				this.httpsTransport.TransferMode = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00021410 File Offset: 0x0001F610
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x0002141D File Offset: 0x0001F61D
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

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00021437 File Offset: 0x0001F637
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002143A File Offset: 0x0001F63A
		internal TextMessageEncodingBindingElement TextMessageEncodingBindingElement
		{
			get
			{
				return this.textEncoding;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00021442 File Offset: 0x0001F642
		internal MtomMessageEncodingBindingElement MtomMessageEncodingBindingElement
		{
			get
			{
				return this.mtomEncoding;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600080C RID: 2060
		internal abstract BasicHttpSecurity BasicHttpSecurity { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0002144A File Offset: 0x0001F64A
		internal WebSocketTransportSettings InternalWebSocketSettings
		{
			get
			{
				return this.httpTransport.WebSocketSettings;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00021457 File Offset: 0x0001F657
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeReaderQuotas()
		{
			return !EncoderDefaults.IsDefaultReaderQuotas(this.ReaderQuotas);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00021467 File Offset: 0x0001F667
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTextEncoding()
		{
			return !this.TextEncoding.Equals(BasicHttpBindingDefaults.TextEncoding);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002147C File Offset: 0x0001F67C
		internal static bool GetSecurityModeFromTransport(HttpTransportBindingElement http, HttpTransportSecurity transportSecurity, out UnifiedSecurityMode mode)
		{
			mode = UnifiedSecurityMode.None;
			if (http == null)
			{
				return false;
			}
			if (http is HttpsTransportBindingElement)
			{
				mode = (UnifiedSecurityMode.Transport | UnifiedSecurityMode.TransportWithMessageCredential);
				BasicHttpSecurity.EnableTransportSecurity((HttpsTransportBindingElement)http, transportSecurity);
			}
			else if (HttpTransportSecurity.IsDisabledTransportAuthentication(http))
			{
				mode = (UnifiedSecurityMode.None | UnifiedSecurityMode.Message);
			}
			else
			{
				if (!BasicHttpSecurity.IsEnabledTransportAuthentication(http, transportSecurity))
				{
					return false;
				}
				mode = UnifiedSecurityMode.TransportCredentialOnly;
			}
			return true;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000214C9 File Offset: 0x0001F6C9
		internal static bool TryCreateSecurity(SecurityBindingElement securityElement, UnifiedSecurityMode mode, HttpTransportSecurity transportSecurity, out BasicHttpSecurity security)
		{
			return BasicHttpSecurity.TryCreate(securityElement, mode, transportSecurity, out security);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000214D4 File Offset: 0x0001F6D4
		internal TransportBindingElement GetTransport()
		{
			BasicHttpSecurity basicHttpSecurity = this.BasicHttpSecurity;
			if (basicHttpSecurity.Mode == BasicHttpSecurityMode.Transport || basicHttpSecurity.Mode == BasicHttpSecurityMode.TransportWithMessageCredential)
			{
				basicHttpSecurity.EnableTransportSecurity(this.httpsTransport);
				return this.httpsTransport;
			}
			if (basicHttpSecurity.Mode == BasicHttpSecurityMode.TransportCredentialOnly)
			{
				basicHttpSecurity.EnableTransportAuthentication(this.httpTransport);
				return this.httpTransport;
			}
			basicHttpSecurity.DisableTransportAuthentication(this.httpTransport);
			return this.httpTransport;
		}

		// Token: 0x06000813 RID: 2067
		internal abstract EnvelopeVersion GetEnvelopeVersion();

		// Token: 0x06000814 RID: 2068 RVA: 0x0002153B File Offset: 0x0001F73B
		internal virtual void SetReaderQuotas(XmlDictionaryReaderQuotas readerQuotas)
		{
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00021540 File Offset: 0x0001F740
		internal virtual void InitializeFrom(HttpTransportBindingElement transport, MessageEncodingBindingElement encoding)
		{
			this.BypassProxyOnLocal = transport.BypassProxyOnLocal;
			this.HostNameComparisonMode = transport.HostNameComparisonMode;
			this.MaxBufferPoolSize = transport.MaxBufferPoolSize;
			this.MaxBufferSize = transport.MaxBufferSize;
			this.MaxReceivedMessageSize = transport.MaxReceivedMessageSize;
			this.ProxyAddress = transport.ProxyAddress;
			this.TransferMode = transport.TransferMode;
			this.UseDefaultWebProxy = transport.UseDefaultWebProxy;
			this.httpTransport.WebSocketSettings = transport.WebSocketSettings;
			this.httpsTransport.WebSocketSettings = transport.WebSocketSettings;
			if (encoding is TextMessageEncodingBindingElement)
			{
				TextMessageEncodingBindingElement textMessageEncodingBindingElement = (TextMessageEncodingBindingElement)encoding;
				this.TextEncoding = textMessageEncodingBindingElement.WriteEncoding;
				this.ReaderQuotas = textMessageEncodingBindingElement.ReaderQuotas;
			}
			else if (encoding is MtomMessageEncodingBindingElement)
			{
				MtomMessageEncodingBindingElement mtomMessageEncodingBindingElement = (MtomMessageEncodingBindingElement)encoding;
				this.TextEncoding = mtomMessageEncodingBindingElement.WriteEncoding;
				this.ReaderQuotas = mtomMessageEncodingBindingElement.ReaderQuotas;
			}
			this.BasicHttpSecurity.Transport.ExtendedProtectionPolicy = transport.ExtendedProtectionPolicy;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00021638 File Offset: 0x0001F838
		internal virtual void CheckSettings()
		{
			if (!UnsafeNativeMethods.IsTailoredApplication.Value)
			{
				return;
			}
			BasicHttpSecurity basicHttpSecurity = this.BasicHttpSecurity;
			if (basicHttpSecurity == null)
			{
				return;
			}
			BasicHttpSecurityMode mode = basicHttpSecurity.Mode;
			if (mode == BasicHttpSecurityMode.None)
			{
				return;
			}
			if (mode == BasicHttpSecurityMode.Message)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedSecuritySetting", new object[]
				{
					"Mode",
					mode
				})));
			}
			if (mode == BasicHttpSecurityMode.TransportWithMessageCredential)
			{
				BasicHttpMessageSecurity message = basicHttpSecurity.Message;
				if (message != null && message.ClientCredentialType == BasicHttpMessageCredentialType.Certificate)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedSecuritySetting", new object[]
					{
						"Message.ClientCredentialType",
						message.ClientCredentialType
					})));
				}
			}
			HttpTransportSecurity transport = basicHttpSecurity.Transport;
			if (transport != null && (transport.ClientCredentialType == HttpClientCredentialType.Certificate || transport.ClientCredentialType == HttpClientCredentialType.InheritedFromHost))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedSecuritySetting", new object[]
				{
					"Transport.ClientCredentialType",
					transport.ClientCredentialType
				})));
			}
		}

		// Token: 0x04000AFE RID: 2814
		private HttpTransportBindingElement httpTransport;

		// Token: 0x04000AFF RID: 2815
		private HttpsTransportBindingElement httpsTransport;

		// Token: 0x04000B00 RID: 2816
		private TextMessageEncodingBindingElement textEncoding;

		// Token: 0x04000B01 RID: 2817
		private MtomMessageEncodingBindingElement mtomEncoding;
	}
}
