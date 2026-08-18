using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200089D RID: 2205
	[__DynamicallyInvokable]
	public class HttpTransportBindingElement : TransportBindingElement, IWsdlExportExtension, IPolicyExportExtension, ITransportPolicyImport
	{
		// Token: 0x060053D5 RID: 21461 RVA: 0x00134D48 File Offset: 0x00132F48
		[__DynamicallyInvokable]
		public HttpTransportBindingElement()
		{
			this.allowCookies = false;
			this.authenticationScheme = AuthenticationSchemes.Anonymous;
			this.bypassProxyOnLocal = false;
			this.decompressionEnabled = true;
			this.hostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
			this.keepAliveEnabled = true;
			this.maxBufferSize = 65536;
			this.maxPendingAccepts = 0;
			this.method = string.Empty;
			this.proxyAuthenticationScheme = AuthenticationSchemes.Anonymous;
			this.proxyAddress = null;
			this.realm = "";
			this.requestInitializationTimeout = HttpTransportDefaults.RequestInitializationTimeout;
			this.transferMode = TransferMode.Buffered;
			this.unsafeConnectionNtlmAuthentication = false;
			this.useDefaultWebProxy = true;
			this.webSocketSettings = HttpTransportDefaults.GetDefaultWebSocketTransportSettings();
			this.webProxy = null;
			this.extendedProtectionPolicy = ChannelBindingUtility.DefaultPolicy;
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00134E00 File Offset: 0x00133000
		[__DynamicallyInvokable]
		protected HttpTransportBindingElement(HttpTransportBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.allowCookies = elementToBeCloned.allowCookies;
			this.authenticationScheme = elementToBeCloned.authenticationScheme;
			this.bypassProxyOnLocal = elementToBeCloned.bypassProxyOnLocal;
			this.decompressionEnabled = elementToBeCloned.decompressionEnabled;
			this.hostNameComparisonMode = elementToBeCloned.hostNameComparisonMode;
			this.inheritBaseAddressSettings = elementToBeCloned.InheritBaseAddressSettings;
			this.keepAliveEnabled = elementToBeCloned.keepAliveEnabled;
			this.maxBufferSize = elementToBeCloned.maxBufferSize;
			this.maxBufferSizeInitialized = elementToBeCloned.maxBufferSizeInitialized;
			this.maxPendingAccepts = elementToBeCloned.maxPendingAccepts;
			this.method = elementToBeCloned.method;
			this.proxyAddress = elementToBeCloned.proxyAddress;
			this.proxyAuthenticationScheme = elementToBeCloned.proxyAuthenticationScheme;
			this.realm = elementToBeCloned.realm;
			this.requestInitializationTimeout = elementToBeCloned.requestInitializationTimeout;
			this.transferMode = elementToBeCloned.transferMode;
			this.unsafeConnectionNtlmAuthentication = elementToBeCloned.unsafeConnectionNtlmAuthentication;
			this.useDefaultWebProxy = elementToBeCloned.useDefaultWebProxy;
			this.webSocketSettings = elementToBeCloned.webSocketSettings.Clone();
			this.webProxy = elementToBeCloned.webProxy;
			this.extendedProtectionPolicy = elementToBeCloned.ExtendedProtectionPolicy;
			if (elementToBeCloned.anonymousUriPrefixMatcher != null)
			{
				this.anonymousUriPrefixMatcher = new HttpAnonymousUriPrefixMatcher(elementToBeCloned.anonymousUriPrefixMatcher);
			}
			this.MessageHandlerFactory = elementToBeCloned.MessageHandlerFactory;
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060053D7 RID: 21463 RVA: 0x00134F3A File Offset: 0x0013313A
		// (set) Token: 0x060053D8 RID: 21464 RVA: 0x00134F42 File Offset: 0x00133142
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool AllowCookies
		{
			[__DynamicallyInvokable]
			get
			{
				return this.allowCookies;
			}
			[__DynamicallyInvokable]
			set
			{
				this.allowCookies = value;
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x00134F4B File Offset: 0x0013314B
		// (set) Token: 0x060053DA RID: 21466 RVA: 0x00134F53 File Offset: 0x00133153
		[DefaultValue(AuthenticationSchemes.Anonymous)]
		[__DynamicallyInvokable]
		public AuthenticationSchemes AuthenticationScheme
		{
			[__DynamicallyInvokable]
			get
			{
				return this.authenticationScheme;
			}
			[__DynamicallyInvokable]
			set
			{
				this.authenticationScheme = value;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x00134F5C File Offset: 0x0013315C
		// (set) Token: 0x060053DC RID: 21468 RVA: 0x00134F64 File Offset: 0x00133164
		[DefaultValue(false)]
		public bool BypassProxyOnLocal
		{
			get
			{
				return this.bypassProxyOnLocal;
			}
			set
			{
				this.bypassProxyOnLocal = value;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060053DD RID: 21469 RVA: 0x00134F6D File Offset: 0x0013316D
		// (set) Token: 0x060053DE RID: 21470 RVA: 0x00134F75 File Offset: 0x00133175
		[DefaultValue(true)]
		public bool DecompressionEnabled
		{
			get
			{
				return this.decompressionEnabled;
			}
			set
			{
				this.decompressionEnabled = value;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060053DF RID: 21471 RVA: 0x00134F7E File Offset: 0x0013317E
		// (set) Token: 0x060053E0 RID: 21472 RVA: 0x00134F86 File Offset: 0x00133186
		[DefaultValue(HostNameComparisonMode.StrongWildcard)]
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
			set
			{
				HostNameComparisonModeHelper.Validate(value);
				this.hostNameComparisonMode = value;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060053E1 RID: 21473 RVA: 0x00134F95 File Offset: 0x00133195
		// (set) Token: 0x060053E2 RID: 21474 RVA: 0x00134F9D File Offset: 0x0013319D
		public HttpMessageHandlerFactory MessageHandlerFactory
		{
			get
			{
				return this.httpMessageHandlerFactory;
			}
			set
			{
				this.httpMessageHandlerFactory = value;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060053E3 RID: 21475 RVA: 0x00134FA6 File Offset: 0x001331A6
		// (set) Token: 0x060053E4 RID: 21476 RVA: 0x00134FB0 File Offset: 0x001331B0
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.PolicyEnforcement == PolicyEnforcement.Always && !ExtendedProtectionPolicy.OSSupportsExtendedProtection)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PlatformNotSupportedException(SR.GetString("ExtendedProtectionNotSupported")));
				}
				this.extendedProtectionPolicy = value;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x00135001 File Offset: 0x00133201
		// (set) Token: 0x060053E6 RID: 21478 RVA: 0x00135009 File Offset: 0x00133209
		internal bool InheritBaseAddressSettings
		{
			get
			{
				return this.inheritBaseAddressSettings;
			}
			set
			{
				this.inheritBaseAddressSettings = value;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x00135012 File Offset: 0x00133212
		// (set) Token: 0x060053E8 RID: 21480 RVA: 0x0013501A File Offset: 0x0013321A
		[DefaultValue(true)]
		public bool KeepAliveEnabled
		{
			get
			{
				return this.keepAliveEnabled;
			}
			set
			{
				this.keepAliveEnabled = value;
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x00135024 File Offset: 0x00133224
		// (set) Token: 0x060053EA RID: 21482 RVA: 0x00135060 File Offset: 0x00133260
		[DefaultValue(65536)]
		[__DynamicallyInvokable]
		public int MaxBufferSize
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.maxBufferSizeInitialized || this.TransferMode != TransferMode.Buffered)
				{
					return this.maxBufferSize;
				}
				long maxReceivedMessageSize = this.MaxReceivedMessageSize;
				if (maxReceivedMessageSize > 2147483647L)
				{
					return int.MaxValue;
				}
				return (int)maxReceivedMessageSize;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBePositive")));
				}
				this.maxBufferSizeInitialized = true;
				this.maxBufferSize = value;
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x00135099 File Offset: 0x00133299
		// (set) Token: 0x060053EC RID: 21484 RVA: 0x001350A4 File Offset: 0x001332A4
		[DefaultValue(0)]
		public int MaxPendingAccepts
		{
			get
			{
				return this.maxPendingAccepts;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				if (value > 100000)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("HttpMaxPendingAcceptsTooLargeError", new object[]
					{
						100000
					})));
				}
				this.maxPendingAccepts = value;
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060053ED RID: 21485 RVA: 0x00135121 File Offset: 0x00133321
		// (set) Token: 0x060053EE RID: 21486 RVA: 0x00135129 File Offset: 0x00133329
		internal string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.method = value;
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060053EF RID: 21487 RVA: 0x00135145 File Offset: 0x00133345
		// (set) Token: 0x060053F0 RID: 21488 RVA: 0x0013514D File Offset: 0x0013334D
		[DefaultValue(null)]
		[TypeConverter(typeof(UriTypeConverter))]
		public Uri ProxyAddress
		{
			get
			{
				return this.proxyAddress;
			}
			set
			{
				this.proxyAddress = value;
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x00135156 File Offset: 0x00133356
		// (set) Token: 0x060053F2 RID: 21490 RVA: 0x0013515E File Offset: 0x0013335E
		[DefaultValue(AuthenticationSchemes.Anonymous)]
		public AuthenticationSchemes ProxyAuthenticationScheme
		{
			get
			{
				return this.proxyAuthenticationScheme;
			}
			set
			{
				if (!value.IsSingleton())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("HttpProxyRequiresSingleAuthScheme", new object[]
					{
						value
					}));
				}
				this.proxyAuthenticationScheme = value;
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x001351A1 File Offset: 0x001333A1
		// (set) Token: 0x060053F3 RID: 21491 RVA: 0x00135198 File Offset: 0x00133398
		internal IWebProxy Proxy
		{
			get
			{
				return this.webProxy;
			}
			set
			{
				this.webProxy = value;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060053F5 RID: 21493 RVA: 0x001351A9 File Offset: 0x001333A9
		// (set) Token: 0x060053F6 RID: 21494 RVA: 0x001351B1 File Offset: 0x001333B1
		[DefaultValue("")]
		public string Realm
		{
			get
			{
				return this.realm;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.realm = value;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x001351CD File Offset: 0x001333CD
		// (set) Token: 0x060053F8 RID: 21496 RVA: 0x001351D8 File Offset: 0x001333D8
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan RequestInitializationTimeout
		{
			get
			{
				return this.requestInitializationTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.requestInitializationTimeout = value;
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x0013524B File Offset: 0x0013344B
		[__DynamicallyInvokable]
		public override string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				return "http";
			}
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x00135252 File Offset: 0x00133452
		// (set) Token: 0x060053FB RID: 21499 RVA: 0x0013525A File Offset: 0x0013345A
		[DefaultValue(TransferMode.Buffered)]
		[__DynamicallyInvokable]
		public TransferMode TransferMode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transferMode;
			}
			[__DynamicallyInvokable]
			set
			{
				TransferModeHelper.Validate(value);
				this.transferMode = value;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060053FC RID: 21500 RVA: 0x00135269 File Offset: 0x00133469
		// (set) Token: 0x060053FD RID: 21501 RVA: 0x00135271 File Offset: 0x00133471
		[__DynamicallyInvokable]
		public WebSocketTransportSettings WebSocketSettings
		{
			[__DynamicallyInvokable]
			get
			{
				return this.webSocketSettings;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.webSocketSettings = value;
			}
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0013528D File Offset: 0x0013348D
		internal virtual bool GetSupportsClientAuthenticationImpl(AuthenticationSchemes effectiveAuthenticationSchemes)
		{
			return effectiveAuthenticationSchemes != AuthenticationSchemes.None && effectiveAuthenticationSchemes.IsNotSet(AuthenticationSchemes.Anonymous);
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0013529F File Offset: 0x0013349F
		internal virtual bool GetSupportsClientWindowsIdentityImpl(AuthenticationSchemes effectiveAuthenticationSchemes)
		{
			return effectiveAuthenticationSchemes != AuthenticationSchemes.None && effectiveAuthenticationSchemes.IsNotSet(AuthenticationSchemes.Anonymous);
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x001352B1 File Offset: 0x001334B1
		internal HttpAnonymousUriPrefixMatcher AnonymousUriPrefixMatcher
		{
			get
			{
				return this.anonymousUriPrefixMatcher;
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06005401 RID: 21505 RVA: 0x001352B9 File Offset: 0x001334B9
		// (set) Token: 0x06005402 RID: 21506 RVA: 0x001352C1 File Offset: 0x001334C1
		[DefaultValue(false)]
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return this.unsafeConnectionNtlmAuthentication;
			}
			set
			{
				this.unsafeConnectionNtlmAuthentication = value;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x001352CA File Offset: 0x001334CA
		// (set) Token: 0x06005404 RID: 21508 RVA: 0x001352D2 File Offset: 0x001334D2
		[DefaultValue(true)]
		public bool UseDefaultWebProxy
		{
			get
			{
				return this.useDefaultWebProxy;
			}
			set
			{
				this.useDefaultWebProxy = value;
			}
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x001352DB File Offset: 0x001334DB
		internal string GetWsdlTransportUri(bool useWebSocketTransport)
		{
			if (useWebSocketTransport)
			{
				return "http://schemas.microsoft.com/soap/websocket";
			}
			return "http://schemas.xmlsoap.org/soap/http";
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x001352EB File Offset: 0x001334EB
		[__DynamicallyInvokable]
		public override BindingElement Clone()
		{
			return new HttpTransportBindingElement(this);
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x001352F4 File Offset: 0x001334F4
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				AuthenticationSchemes effectiveAuthenticationSchemes = HttpTransportBindingElement.GetEffectiveAuthenticationSchemes(this.AuthenticationScheme, context.BindingParameters);
				return (T)((object)new SecurityCapabilities(this.GetSupportsClientAuthenticationImpl(effectiveAuthenticationSchemes), effectiveAuthenticationSchemes == AuthenticationSchemes.Negotiate, this.GetSupportsClientWindowsIdentityImpl(effectiveAuthenticationSchemes), ProtectionLevel.None, ProtectionLevel.None));
			}
			if (typeof(T) == typeof(IBindingDeliveryCapabilities))
			{
				return (T)((object)new HttpTransportBindingElement.BindingDeliveryCapabilitiesHelper());
			}
			if (typeof(T) == typeof(TransferMode))
			{
				return (T)((object)this.TransferMode);
			}
			if (typeof(T) == typeof(ExtendedProtectionPolicy))
			{
				return (T)((object)this.ExtendedProtectionPolicy);
			}
			if (typeof(T) == typeof(IAnonymousUriPrefixMatcher))
			{
				if (this.anonymousUriPrefixMatcher == null)
				{
					this.anonymousUriPrefixMatcher = new HttpAnonymousUriPrefixMatcher();
				}
				return (T)((object)this.anonymousUriPrefixMatcher);
			}
			if (typeof(T) == typeof(ITransportCompressionSupport))
			{
				return (T)((object)new HttpTransportBindingElement.TransportCompressionSupportHelper());
			}
			if (context.BindingParameters.Find<MessageEncodingBindingElement>() == null)
			{
				context.BindingParameters.Add(new TextMessageEncodingBindingElement());
			}
			return base.GetProperty<T>(context);
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x00135460 File Offset: 0x00133660
		[__DynamicallyInvokable]
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return this.WebSocketSettings.TransportUsage != WebSocketTransportUsage.Always;
			}
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && this.WebSocketSettings.TransportUsage != WebSocketTransportUsage.Never;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x001354C8 File Offset: 0x001336C8
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (typeof(TChannel) == typeof(IReplyChannel))
			{
				return this.WebSocketSettings.TransportUsage != WebSocketTransportUsage.Always;
			}
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && this.WebSocketSettings.TransportUsage != WebSocketTransportUsage.Never;
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00135530 File Offset: 0x00133730
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (this.MessageHandlerFactory != null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpPipelineNotSupportedOnClientSide", new object[]
				{
					"MessageHandlerFactory"
				})));
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("CouldnTCreateChannelForChannelType2", new object[]
				{
					context.Binding.Name,
					typeof(TChannel)
				}));
			}
			if (this.authenticationScheme == AuthenticationSchemes.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("HttpAuthSchemeCannotBeNone", new object[]
				{
					this.authenticationScheme
				}));
			}
			if (!this.authenticationScheme.IsSingleton())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("HttpRequiresSingleAuthScheme", new object[]
				{
					this.authenticationScheme
				}));
			}
			return (IChannelFactory<TChannel>)new HttpChannelFactory<TChannel>(this, context);
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00135644 File Offset: 0x00133844
		internal static AuthenticationSchemes GetEffectiveAuthenticationSchemes(AuthenticationSchemes currentAuthenticationSchemes, BindingParameterCollection bindingParameters)
		{
			if (bindingParameters == null)
			{
				return currentAuthenticationSchemes;
			}
			AuthenticationSchemes authenticationSchemes;
			if (!AuthenticationSchemesBindingParameter.TryExtract(bindingParameters, out authenticationSchemes))
			{
				return currentAuthenticationSchemes;
			}
			if (currentAuthenticationSchemes == AuthenticationSchemes.None || (AspNetEnvironment.Current.IsMetadataListener(bindingParameters) && currentAuthenticationSchemes == AuthenticationSchemes.Anonymous && authenticationSchemes.IsNotSet(AuthenticationSchemes.Anonymous)))
			{
				if (!authenticationSchemes.IsSingleton() && authenticationSchemes.IsSet(AuthenticationSchemes.Anonymous) && AspNetEnvironment.Current.AspNetCompatibilityEnabled && AspNetEnvironment.Current.IsSimpleApplicationHost && AspNetEnvironment.Current.IsWindowsAuthenticationConfigured())
				{
					authenticationSchemes ^= AuthenticationSchemes.Anonymous;
				}
				return authenticationSchemes;
			}
			return currentAuthenticationSchemes & authenticationSchemes;
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x001356D0 File Offset: 0x001338D0
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("TChannel", SR.GetString("CouldnTCreateChannelForChannelType2", new object[]
				{
					context.Binding.Name,
					typeof(TChannel)
				}));
			}
			this.UpdateAuthenticationSchemes(context);
			HttpChannelListener httpChannelListener = new HttpChannelListener<TChannel>(this, context);
			AspNetEnvironment.Current.ApplyHostedContext(httpChannelListener, context);
			return (IChannelListener<TChannel>)httpChannelListener;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00135758 File Offset: 0x00133958
		protected void UpdateAuthenticationSchemes(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			AuthenticationSchemes effectiveAuthenticationSchemes = HttpTransportBindingElement.GetEffectiveAuthenticationSchemes(this.AuthenticationScheme, context.BindingParameters);
			if (effectiveAuthenticationSchemes != AuthenticationSchemes.None)
			{
				this.AuthenticationScheme = effectiveAuthenticationSchemes;
				return;
			}
			string name = context.Binding.Name;
			if (this.AuthenticationScheme == AuthenticationSchemes.None)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AuthenticationSchemesCannotBeInheritedFromHost", new object[]
				{
					name
				})));
			}
			AuthenticationSchemes authenticationSchemes;
			AuthenticationSchemesBindingParameter.TryExtract(context.BindingParameters, out authenticationSchemes);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AuthenticationSchemes_BindingAndHostConflict", new object[]
			{
				authenticationSchemes,
				name,
				this.AuthenticationScheme
			})));
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00135818 File Offset: 0x00133A18
		void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			this.OnExportPolicy(exporter, context);
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(context.BindingElements, out flag);
			if (flag && messageEncodingBindingElement is IPolicyExportExtension)
			{
				((IPolicyExportExtension)messageEncodingBindingElement).ExportPolicy(exporter, context);
			}
			WsdlExporter.WSAddressingHelper.AddWSAddressingAssertion(exporter, context, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0013588C File Offset: 0x00133A8C
		internal virtual void OnExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			List<string> list = new List<string>();
			AuthenticationSchemes effectiveAuthenticationSchemes = HttpTransportBindingElement.GetEffectiveAuthenticationSchemes(this.AuthenticationScheme, policyContext.BindingParameters);
			if (effectiveAuthenticationSchemes != AuthenticationSchemes.None && !effectiveAuthenticationSchemes.IsSet(AuthenticationSchemes.Anonymous))
			{
				if (effectiveAuthenticationSchemes.IsSet(AuthenticationSchemes.Negotiate))
				{
					list.Add("NegotiateAuthentication");
				}
				if (effectiveAuthenticationSchemes.IsSet(AuthenticationSchemes.Ntlm))
				{
					list.Add("NtlmAuthentication");
				}
				if (effectiveAuthenticationSchemes.IsSet(AuthenticationSchemes.Digest))
				{
					list.Add("DigestAuthentication");
				}
				if (effectiveAuthenticationSchemes.IsSet(AuthenticationSchemes.Basic))
				{
					list.Add("BasicAuthentication");
				}
				if (list != null && list.Count > 0)
				{
					if (list.Count == 1)
					{
						policyContext.GetBindingAssertions().Add(new XmlDocument().CreateElement("http", list[0], "http://schemas.microsoft.com/ws/06/2004/policy/http"));
					}
					else
					{
						XmlDocument xmlDocument = new XmlDocument();
						XmlElement xmlElement = xmlDocument.CreateElement("wsp", "ExactlyOne", exporter.PolicyVersion.Namespace);
						foreach (string localName in list)
						{
							xmlElement.AppendChild(xmlDocument.CreateElement("http", localName, "http://schemas.microsoft.com/ws/06/2004/policy/http"));
						}
						policyContext.GetBindingAssertions().Add(xmlElement);
					}
				}
			}
			bool flag = WebSocketHelper.UseWebSocketTransport(this.WebSocketSettings.TransportUsage, policyContext.Contract.IsDuplex());
			if (flag && this.TransferMode != TransferMode.Buffered)
			{
				policyContext.GetBindingAssertions().Add(new XmlDocument().CreateElement("mswsp", this.TransferMode.ToString(), "http://schemas.microsoft.com/soap/websocket/policy"));
			}
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00135A3C File Offset: 0x00133C3C
		internal virtual void OnImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00135A40 File Offset: 0x00133C40
		void ITransportPolicyImport.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			ICollection<XmlElement> bindingAssertions = policyContext.GetBindingAssertions();
			List<XmlElement> list = new List<XmlElement>();
			bool flag = false;
			foreach (XmlElement xmlElement in bindingAssertions)
			{
				if (!(xmlElement.NamespaceURI != "http://schemas.microsoft.com/ws/06/2004/policy/http"))
				{
					string localName = xmlElement.LocalName;
					if (!(localName == "BasicAuthentication"))
					{
						if (!(localName == "DigestAuthentication"))
						{
							if (!(localName == "NegotiateAuthentication"))
							{
								if (!(localName == "NtlmAuthentication"))
								{
									continue;
								}
								this.AuthenticationScheme = AuthenticationSchemes.Ntlm;
							}
							else
							{
								this.AuthenticationScheme = AuthenticationSchemes.Negotiate;
							}
						}
						else
						{
							this.AuthenticationScheme = AuthenticationSchemes.Digest;
						}
					}
					else
					{
						this.AuthenticationScheme = AuthenticationSchemes.Basic;
					}
					if (flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("HttpTransportCannotHaveMultipleAuthenticationSchemes", new object[]
						{
							policyContext.Contract.Namespace,
							policyContext.Contract.Name
						})));
					}
					flag = true;
					list.Add(xmlElement);
				}
			}
			list.ForEach(delegate(XmlElement element)
			{
				bindingAssertions.Remove(element);
			});
			if (this.WebSocketSettings.TransportUsage == WebSocketTransportUsage.Always)
			{
				foreach (XmlElement xmlElement2 in bindingAssertions)
				{
					if (!(xmlElement2.NamespaceURI != "http://schemas.microsoft.com/soap/websocket/policy"))
					{
						string localName2 = xmlElement2.LocalName;
						TransferMode transferMode;
						if (!Enum.TryParse<TransferMode>(localName2, true, out transferMode) || !TransferModeHelper.IsDefined(transferMode) || transferMode == TransferMode.Buffered)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("WebSocketTransportPolicyAssertionInvalid", new object[]
							{
								policyContext.Contract.Namespace,
								policyContext.Contract.Name,
								localName2,
								TransferMode.Streamed,
								TransferMode.StreamedRequest,
								TransferMode.StreamedResponse
							})));
						}
						this.TransferMode = transferMode;
						bindingAssertions.Remove(xmlElement2);
						break;
					}
				}
			}
			this.OnImportPolicy(importer, policyContext);
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x00135C84 File Offset: 0x00133E84
		void IWsdlExportExtension.ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x00135C88 File Offset: 0x00133E88
		void IWsdlExportExtension.ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext endpointContext)
		{
			bool flag;
			MessageEncodingBindingElement messageEncodingBindingElement = this.FindMessageEncodingBindingElement(endpointContext, out flag);
			bool flag2 = WebSocketHelper.UseWebSocketTransport(this.WebSocketSettings.TransportUsage, endpointContext.ContractConversionContext.Contract.IsDuplex());
			EndpointAddress endpointAddress = endpointContext.Endpoint.Address;
			if (flag2)
			{
				endpointAddress = new EndpointAddress(WebSocketHelper.GetWebSocketUri(endpointContext.Endpoint.Address.Uri), endpointContext.Endpoint.Address);
				SoapAddressBinding soapAddressBinding = SoapHelper.GetSoapAddressBinding(endpointContext.WsdlPort);
				if (soapAddressBinding != null)
				{
					soapAddressBinding.Location = endpointAddress.Uri.AbsoluteUri;
				}
			}
			TransportBindingElement.ExportWsdlEndpoint(exporter, endpointContext, this.GetWsdlTransportUri(flag2), endpointAddress, messageEncodingBindingElement.MessageVersion.Addressing);
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x00135D34 File Offset: 0x00133F34
		internal override bool IsMatch(BindingElement b)
		{
			if (!base.IsMatch(b))
			{
				return false;
			}
			HttpTransportBindingElement httpTransportBindingElement = b as HttpTransportBindingElement;
			return httpTransportBindingElement != null && this.allowCookies == httpTransportBindingElement.allowCookies && this.authenticationScheme == httpTransportBindingElement.authenticationScheme && this.decompressionEnabled == httpTransportBindingElement.decompressionEnabled && this.hostNameComparisonMode == httpTransportBindingElement.hostNameComparisonMode && this.inheritBaseAddressSettings == httpTransportBindingElement.inheritBaseAddressSettings && this.keepAliveEnabled == httpTransportBindingElement.keepAliveEnabled && this.maxBufferSize == httpTransportBindingElement.maxBufferSize && !(this.method != httpTransportBindingElement.method) && !(this.proxyAddress != httpTransportBindingElement.proxyAddress) && this.proxyAuthenticationScheme == httpTransportBindingElement.proxyAuthenticationScheme && !(this.realm != httpTransportBindingElement.realm) && this.transferMode == httpTransportBindingElement.transferMode && this.unsafeConnectionNtlmAuthentication == httpTransportBindingElement.unsafeConnectionNtlmAuthentication && this.useDefaultWebProxy == httpTransportBindingElement.useDefaultWebProxy && this.WebSocketSettings.Equals(httpTransportBindingElement.WebSocketSettings) && this.webProxy == httpTransportBindingElement.webProxy && ChannelBindingUtility.AreEqual(this.ExtendedProtectionPolicy, httpTransportBindingElement.ExtendedProtectionPolicy);
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x00135E82 File Offset: 0x00134082
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeExtendedProtectionPolicy()
		{
			return !ChannelBindingUtility.AreEqual(this.ExtendedProtectionPolicy, ChannelBindingUtility.DefaultPolicy);
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x00135E97 File Offset: 0x00134097
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeMessageHandlerFactory()
		{
			return false;
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x00135E9A File Offset: 0x0013409A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeWebSocketSettings()
		{
			return !this.WebSocketSettings.Equals(HttpTransportDefaults.GetDefaultWebSocketTransportSettings());
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00135EB0 File Offset: 0x001340B0
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(BindingElementCollection bindingElements, out bool createdNew)
		{
			createdNew = false;
			MessageEncodingBindingElement messageEncodingBindingElement = bindingElements.Find<MessageEncodingBindingElement>();
			if (messageEncodingBindingElement == null)
			{
				createdNew = true;
				messageEncodingBindingElement = new TextMessageEncodingBindingElement();
			}
			return messageEncodingBindingElement;
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x00135ED4 File Offset: 0x001340D4
		private MessageEncodingBindingElement FindMessageEncodingBindingElement(WsdlEndpointConversionContext endpointContext, out bool createdNew)
		{
			BindingElementCollection bindingElements = endpointContext.Endpoint.Binding.CreateBindingElements();
			return this.FindMessageEncodingBindingElement(bindingElements, out createdNew);
		}

		// Token: 0x040032E9 RID: 13033
		private bool allowCookies;

		// Token: 0x040032EA RID: 13034
		private AuthenticationSchemes authenticationScheme;

		// Token: 0x040032EB RID: 13035
		private bool bypassProxyOnLocal;

		// Token: 0x040032EC RID: 13036
		private bool decompressionEnabled;

		// Token: 0x040032ED RID: 13037
		private HostNameComparisonMode hostNameComparisonMode;

		// Token: 0x040032EE RID: 13038
		private bool keepAliveEnabled;

		// Token: 0x040032EF RID: 13039
		private bool inheritBaseAddressSettings;

		// Token: 0x040032F0 RID: 13040
		private int maxBufferSize;

		// Token: 0x040032F1 RID: 13041
		private bool maxBufferSizeInitialized;

		// Token: 0x040032F2 RID: 13042
		private string method;

		// Token: 0x040032F3 RID: 13043
		private Uri proxyAddress;

		// Token: 0x040032F4 RID: 13044
		private AuthenticationSchemes proxyAuthenticationScheme;

		// Token: 0x040032F5 RID: 13045
		private string realm;

		// Token: 0x040032F6 RID: 13046
		private TimeSpan requestInitializationTimeout;

		// Token: 0x040032F7 RID: 13047
		private TransferMode transferMode;

		// Token: 0x040032F8 RID: 13048
		private bool unsafeConnectionNtlmAuthentication;

		// Token: 0x040032F9 RID: 13049
		private bool useDefaultWebProxy;

		// Token: 0x040032FA RID: 13050
		private WebSocketTransportSettings webSocketSettings;

		// Token: 0x040032FB RID: 13051
		private IWebProxy webProxy;

		// Token: 0x040032FC RID: 13052
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x040032FD RID: 13053
		private HttpAnonymousUriPrefixMatcher anonymousUriPrefixMatcher;

		// Token: 0x040032FE RID: 13054
		private HttpMessageHandlerFactory httpMessageHandlerFactory;

		// Token: 0x040032FF RID: 13055
		private int maxPendingAccepts;

		// Token: 0x02000D76 RID: 3446
		private class BindingDeliveryCapabilitiesHelper : IBindingDeliveryCapabilities
		{
			// Token: 0x06007E5F RID: 32351 RVA: 0x001D7969 File Offset: 0x001D5B69
			internal BindingDeliveryCapabilitiesHelper()
			{
			}

			// Token: 0x17001C23 RID: 7203
			// (get) Token: 0x06007E60 RID: 32352 RVA: 0x001D7971 File Offset: 0x001D5B71
			bool IBindingDeliveryCapabilities.AssuresOrderedDelivery
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001C24 RID: 7204
			// (get) Token: 0x06007E61 RID: 32353 RVA: 0x001D7974 File Offset: 0x001D5B74
			bool IBindingDeliveryCapabilities.QueuedDelivery
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x02000D77 RID: 3447
		private class TransportCompressionSupportHelper : ITransportCompressionSupport
		{
			// Token: 0x06007E62 RID: 32354 RVA: 0x001D7977 File Offset: 0x001D5B77
			public bool IsCompressionFormatSupported(CompressionFormat compressionFormat)
			{
				return true;
			}
		}
	}
}
