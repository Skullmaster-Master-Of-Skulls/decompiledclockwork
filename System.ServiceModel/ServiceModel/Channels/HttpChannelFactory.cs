using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200085E RID: 2142
	internal class HttpChannelFactory<TChannel> : TransportChannelFactory<TChannel>, IHttpTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x06005033 RID: 20531 RVA: 0x00126210 File Offset: 0x00124410
		internal HttpChannelFactory(HttpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context, HttpTransportDefaults.GetDefaultMessageEncoderFactory())
		{
			if (bindingElement.TransferMode == TransferMode.Buffered)
			{
				if (bindingElement.MaxReceivedMessageSize > 2147483647L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("bindingElement.MaxReceivedMessageSize", SR.GetString("MaxReceivedMessageSizeMustBeInIntegerRange")));
				}
				if ((long)bindingElement.MaxBufferSize != bindingElement.MaxReceivedMessageSize)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("bindingElement", SR.GetString("MaxBufferSizeMustMatchMaxReceivedMessageSize"));
				}
			}
			else if ((long)bindingElement.MaxBufferSize > bindingElement.MaxReceivedMessageSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("bindingElement", SR.GetString("MaxBufferSizeMustNotExceedMaxReceivedMessageSize"));
			}
			if (TransferModeHelper.IsRequestStreamed(bindingElement.TransferMode) && bindingElement.AuthenticationScheme != AuthenticationSchemes.Anonymous)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("bindingElement", SR.GetString("HttpAuthDoesNotSupportRequestStreaming"));
			}
			this.allowCookies = bindingElement.AllowCookies;
			if (!this.allowCookies)
			{
				Collection<HttpCookieContainerBindingElement> collection = context.BindingParameters.FindAll<HttpCookieContainerBindingElement>();
				if (collection.Count > 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MultipleCCbesInParameters", new object[]
					{
						typeof(HttpCookieContainerBindingElement)
					})));
				}
				if (collection.Count == 1)
				{
					this.allowCookies = true;
					context.BindingParameters.Remove<HttpCookieContainerBindingElement>();
				}
			}
			if (this.allowCookies)
			{
				this.httpCookieContainerManager = new HttpCookieContainerManager();
			}
			if (!bindingElement.AuthenticationScheme.IsSingleton())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("HttpRequiresSingleAuthScheme", new object[]
				{
					bindingElement.AuthenticationScheme
				}));
			}
			this.authenticationScheme = bindingElement.AuthenticationScheme;
			this.decompressionEnabled = bindingElement.DecompressionEnabled;
			this.keepAliveEnabled = bindingElement.KeepAliveEnabled;
			this.maxBufferSize = bindingElement.MaxBufferSize;
			this.transferMode = bindingElement.TransferMode;
			if (bindingElement.Proxy != null)
			{
				this.proxy = bindingElement.Proxy;
			}
			else if (bindingElement.ProxyAddress != null)
			{
				if (bindingElement.UseDefaultWebProxy)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UseDefaultWebProxyCantBeUsedWithExplicitProxyAddress")));
				}
				if (bindingElement.ProxyAuthenticationScheme == AuthenticationSchemes.Anonymous)
				{
					this.proxy = new WebProxy(bindingElement.ProxyAddress, bindingElement.BypassProxyOnLocal);
				}
				else
				{
					this.proxy = null;
					this.proxyFactory = new HttpChannelFactory<TChannel>.WebProxyFactory(bindingElement.ProxyAddress, bindingElement.BypassProxyOnLocal, bindingElement.ProxyAuthenticationScheme);
				}
			}
			else if (!bindingElement.UseDefaultWebProxy)
			{
				this.proxy = new WebProxy();
			}
			this.channelCredentials = context.BindingParameters.Find<SecurityCredentialsManager>();
			this.securityCapabilities = bindingElement.GetProperty<ISecurityCapabilities>(context);
			this.webSocketSettings = WebSocketHelper.GetRuntimeWebSocketSettings(bindingElement.WebSocketSettings);
			int bufferSize = WebSocketHelper.ComputeClientBufferSize(base.MaxReceivedMessageSize);
			this.bufferPool = new ConnectionBufferPool(bufferSize);
			Collection<ClientWebSocketFactory> collection2 = context.BindingParameters.FindAll<ClientWebSocketFactory>();
			if (collection2.Count > 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("context", SR.GetString("MultipleClientWebSocketFactoriesSpecified", new object[]
				{
					typeof(BindingContext).Name,
					typeof(ClientWebSocketFactory).Name
				}));
			}
			this.clientWebSocketFactory = ((collection2.Count == 0) ? null : collection2[0]);
			this.webSocketSoapContentType = new Lazy<string>(() => base.MessageEncoderFactory.CreateSessionEncoder().ContentType, LazyThreadSafetyMode.ExecutionAndPublication);
			if (ServiceModelAppSettings.HttpTransportPerFactoryConnectionPool)
			{
				this.uniqueConnectionGroupNamePrefix = Interlocked.Increment(ref HttpChannelFactory<TChannel>.connectionGroupNamePrefix).ToString();
				return;
			}
			this.uniqueConnectionGroupNamePrefix = string.Empty;
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x00126584 File Offset: 0x00124784
		public bool AllowCookies
		{
			get
			{
				return this.allowCookies;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x0012658C File Offset: 0x0012478C
		public AuthenticationSchemes AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x00126594 File Offset: 0x00124794
		public bool DecompressionEnabled
		{
			get
			{
				return this.decompressionEnabled;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x0012659C File Offset: 0x0012479C
		public virtual bool IsChannelBindingSupportEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06005038 RID: 20536 RVA: 0x0012659F File Offset: 0x0012479F
		public bool KeepAliveEnabled
		{
			get
			{
				return this.keepAliveEnabled;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x001265A7 File Offset: 0x001247A7
		public SecurityTokenManager SecurityTokenManager
		{
			get
			{
				return this.securityTokenManager;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x0600503A RID: 20538 RVA: 0x001265AF File Offset: 0x001247AF
		public int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x0600503B RID: 20539 RVA: 0x001265B7 File Offset: 0x001247B7
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x0600503C RID: 20540 RVA: 0x001265BF File Offset: 0x001247BF
		public TransferMode TransferMode
		{
			get
			{
				return this.transferMode;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x001265C7 File Offset: 0x001247C7
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeHttp;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x001265CE File Offset: 0x001247CE
		public WebSocketTransportSettings WebSocketSettings
		{
			get
			{
				return this.webSocketSettings;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x001265D6 File Offset: 0x001247D6
		internal string WebSocketSoapContentType
		{
			get
			{
				return this.webSocketSoapContentType.Value;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06005040 RID: 20544 RVA: 0x001265E3 File Offset: 0x001247E3
		protected ConnectionBufferPool WebSocketBufferPool
		{
			get
			{
				return this.bufferPool;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06005041 RID: 20545 RVA: 0x001265EB File Offset: 0x001247EB
		private HashAlgorithm HashAlgorithm
		{
			[SecurityCritical]
			get
			{
				if (this.hashAlgorithm == null)
				{
					this.hashAlgorithm = CryptoHelper.CreateHashAlgorithm("http://www.w3.org/2001/04/xmlenc#sha256");
				}
				else
				{
					this.hashAlgorithm.Initialize();
				}
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x00126618 File Offset: 0x00124818
		int IHttpTransportFactorySettings.MaxBufferSize
		{
			get
			{
				return this.MaxBufferSize;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06005043 RID: 20547 RVA: 0x00126620 File Offset: 0x00124820
		TransferMode IHttpTransportFactorySettings.TransferMode
		{
			get
			{
				return this.TransferMode;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06005044 RID: 20548 RVA: 0x00126628 File Offset: 0x00124828
		protected ClientWebSocketFactory ClientWebSocketFactory
		{
			get
			{
				return this.clientWebSocketFactory;
			}
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x00126630 File Offset: 0x00124830
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.securityCapabilities);
			}
			if (typeof(T) == typeof(IHttpCookieContainerManager))
			{
				return (T)((object)this.GetHttpCookieContainerManager());
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x00126691 File Offset: 0x00124891
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private HttpCookieContainerManager GetHttpCookieContainerManager()
		{
			return this.httpCookieContainerManager;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00126699 File Offset: 0x00124899
		internal virtual SecurityMessageProperty CreateReplySecurityProperty(HttpWebRequest request, HttpWebResponse response)
		{
			if (!response.IsMutuallyAuthenticated)
			{
				return null;
			}
			return this.CreateMutuallyAuthenticatedReplySecurityProperty(response);
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x001266AC File Offset: 0x001248AC
		internal Exception CreateToMustEqualViaException(Uri to, Uri via)
		{
			return new ArgumentException(SR.GetString("HttpToMustEqualVia", new object[]
			{
				to,
				via
			}));
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x001266CC File Offset: 0x001248CC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private SecurityMessageProperty CreateMutuallyAuthenticatedReplySecurityProperty(HttpWebResponse response)
		{
			string text = AuthenticationManager.CustomTargetNameDictionary[response.ResponseUri.AbsoluteUri];
			if (text == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("HttpSpnNotFound", new object[]
				{
					response.ResponseUri
				})));
			}
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = SecurityUtils.CreatePrincipalNameAuthorizationPolicies(text);
			return new SecurityMessageProperty
			{
				TransportToken = new SecurityTokenSpecification(null, readOnlyCollection),
				ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection)
			};
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x00126742 File Offset: 0x00124942
		internal override int GetMaxBufferSize()
		{
			return this.MaxBufferSize;
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0012674C File Offset: 0x0012494C
		private SecurityTokenProviderContainer CreateAndOpenTokenProvider(TimeSpan timeout, AuthenticationSchemes authenticationScheme, EndpointAddress target, Uri via, ChannelParameterCollection channelParameters)
		{
			SecurityTokenProvider securityTokenProvider = null;
			switch (authenticationScheme)
			{
			case AuthenticationSchemes.Digest:
				securityTokenProvider = TransportSecurityHelpers.GetDigestTokenProvider(this.SecurityTokenManager, target, via, this.Scheme, authenticationScheme, channelParameters);
				goto IL_81;
			case AuthenticationSchemes.Negotiate:
			case AuthenticationSchemes.Ntlm:
				securityTokenProvider = TransportSecurityHelpers.GetSspiTokenProvider(this.SecurityTokenManager, target, via, this.Scheme, authenticationScheme, channelParameters);
				goto IL_81;
			case AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate:
				break;
			default:
				if (authenticationScheme == AuthenticationSchemes.Basic)
				{
					securityTokenProvider = TransportSecurityHelpers.GetUserNameTokenProvider(this.SecurityTokenManager, target, via, this.Scheme, authenticationScheme, channelParameters);
					goto IL_81;
				}
				if (authenticationScheme == AuthenticationSchemes.Anonymous)
				{
					goto IL_81;
				}
				break;
			}
			throw Fx.AssertAndThrow("CreateAndOpenTokenProvider: Invalid authentication scheme");
			IL_81:
			SecurityTokenProviderContainer securityTokenProviderContainer;
			if (securityTokenProvider != null)
			{
				securityTokenProviderContainer = new SecurityTokenProviderContainer(securityTokenProvider);
				securityTokenProviderContainer.Open(timeout);
			}
			else
			{
				securityTokenProviderContainer = null;
			}
			return securityTokenProviderContainer;
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x001267F0 File Offset: 0x001249F0
		protected virtual void ValidateCreateChannelParameters(EndpointAddress remoteAddress, Uri via)
		{
			base.ValidateScheme(via);
			if (base.MessageVersion.Addressing == AddressingVersion.None && remoteAddress.Uri != via)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateToMustEqualViaException(remoteAddress.Uri, via));
			}
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0012683C File Offset: 0x00124A3C
		protected override TChannel OnCreateChannel(EndpointAddress remoteAddress, Uri via)
		{
			EndpointAddress remoteAddress2 = (remoteAddress != null && WebSocketHelper.IsWebSocketUri(remoteAddress.Uri)) ? new EndpointAddress(WebSocketHelper.NormalizeWsSchemeWithHttpScheme(remoteAddress.Uri), remoteAddress) : remoteAddress;
			Uri via2 = WebSocketHelper.IsWebSocketUri(via) ? WebSocketHelper.NormalizeWsSchemeWithHttpScheme(via) : via;
			return this.OnCreateChannelCore(remoteAddress2, via2);
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00126890 File Offset: 0x00124A90
		protected virtual TChannel OnCreateChannelCore(EndpointAddress remoteAddress, Uri via)
		{
			this.ValidateCreateChannelParameters(remoteAddress, via);
			this.ValidateWebSocketTransportUsage();
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return (TChannel)((object)new HttpChannelFactory<TChannel>.HttpRequestChannel((HttpChannelFactory<IRequestChannel>)this, remoteAddress, via, base.ManualAddressing));
			}
			return (TChannel)((object)new ClientWebSocketTransportDuplexSessionChannel((HttpChannelFactory<IDuplexSessionChannel>)this, this.clientWebSocketFactory, remoteAddress, via, this.WebSocketBufferPool));
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00126900 File Offset: 0x00124B00
		protected void ValidateWebSocketTransportUsage()
		{
			Type typeFromHandle = typeof(TChannel);
			if (typeFromHandle == typeof(IRequestChannel) && this.WebSocketSettings.TransportUsage == WebSocketTransportUsage.Always)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WebSocketCannotCreateRequestClientChannelWithCertainWebSocketTransportUsage", new object[]
				{
					typeof(TChannel),
					"TransportUsage",
					typeof(WebSocketTransportSettings).Name,
					this.WebSocketSettings.TransportUsage
				})));
			}
			if (typeFromHandle == typeof(IDuplexSessionChannel))
			{
				if (this.WebSocketSettings.TransportUsage == WebSocketTransportUsage.Never)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WebSocketCannotCreateRequestClientChannelWithCertainWebSocketTransportUsage", new object[]
					{
						typeof(TChannel),
						"TransportUsage",
						typeof(WebSocketTransportSettings).Name,
						this.WebSocketSettings.TransportUsage
					})));
				}
				if (!WebSocketHelper.OSSupportsWebSockets() && this.ClientWebSocketFactory == null)
				{
					throw FxTrace.Exception.AsError(new PlatformNotSupportedException(SR.GetString("WebSocketsClientSideNotSupported", new object[]
					{
						typeof(ClientWebSocketFactory).FullName
					})));
				}
			}
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00126A50 File Offset: 0x00124C50
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeSecurityTokenManager()
		{
			if (this.channelCredentials == null)
			{
				this.channelCredentials = ClientCredentials.CreateDefaultCredentials();
			}
			this.securityTokenManager = this.channelCredentials.CreateSecurityTokenManager();
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x00126A76 File Offset: 0x00124C76
		protected virtual bool IsSecurityTokenManagerRequired()
		{
			return this.AuthenticationScheme != AuthenticationSchemes.Anonymous || (this.proxyFactory != null && this.proxyFactory.AuthenticationScheme != AuthenticationSchemes.Anonymous);
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00126AA4 File Offset: 0x00124CA4
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpen(timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x00126AB4 File Offset: 0x00124CB4
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x00126ABC File Offset: 0x00124CBC
		protected override void OnOpen(TimeSpan timeout)
		{
			if (this.IsSecurityTokenManagerRequired())
			{
				this.InitializeSecurityTokenManager();
			}
			if (this.AllowCookies && !this.httpCookieContainerManager.IsInitialized)
			{
				this.httpCookieContainerManager.CookieContainer = new CookieContainer();
			}
			if (!HttpChannelFactory<TChannel>.httpWebRequestWebPermissionDenied && HttpWebRequest.DefaultMaximumErrorResponseLength != -1)
			{
				int num;
				if (this.MaxBufferSize >= 2147482623)
				{
					num = -1;
				}
				else
				{
					num = this.MaxBufferSize / 1024;
					if (num * 1024 < this.MaxBufferSize)
					{
						num++;
					}
				}
				if (num == -1 || num > HttpWebRequest.DefaultMaximumErrorResponseLength)
				{
					try
					{
						HttpWebRequest.DefaultMaximumErrorResponseLength = num;
					}
					catch (SecurityException exception)
					{
						HttpChannelFactory<TChannel>.httpWebRequestWebPermissionDenied = true;
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
					}
				}
			}
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x00126B74 File Offset: 0x00124D74
		protected override void OnClosed()
		{
			base.OnClosed();
			if (this.bufferPool != null)
			{
				this.bufferPool.Close();
			}
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x00126B90 File Offset: 0x00124D90
		internal static void TraceResponseReceived(HttpWebResponse response, Message message, object receiver)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				if (response != null && response.ResponseUri != null)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 262153, SR.GetString("TraceCodeHttpResponseReceived"), new StringTraceRecord("ResponseUri", response.ResponseUri.ToString()), receiver, null, message);
					return;
				}
				TraceUtility.TraceEvent(TraceEventType.Verbose, 262153, SR.GetString("TraceCodeHttpResponseReceived"), receiver, message);
			}
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00126BFC File Offset: 0x00124DFC
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string AppendWindowsAuthenticationInfo(string inputString, NetworkCredential credential, AuthenticationLevel authenticationLevel, TokenImpersonationLevel impersonationLevel)
		{
			return SecurityUtils.AppendWindowsAuthenticationInfo(inputString, credential, authenticationLevel, impersonationLevel);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x00126C08 File Offset: 0x00124E08
		protected virtual string OnGetConnectionGroupPrefix(HttpWebRequest httpWebRequest, SecurityTokenContainer clientCertificateToken)
		{
			return string.Empty;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x00126C0F File Offset: 0x00124E0F
		internal static bool IsWindowsAuth(AuthenticationSchemes authScheme)
		{
			return authScheme == AuthenticationSchemes.Negotiate || authScheme == AuthenticationSchemes.Ntlm;
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00126C1C File Offset: 0x00124E1C
		[SecuritySafeCritical]
		private string GetConnectionGroupName(HttpWebRequest httpWebRequest, NetworkCredential credential, AuthenticationLevel authenticationLevel, TokenImpersonationLevel impersonationLevel, SecurityTokenContainer clientCertificateToken)
		{
			if (this.credentialHashCache == null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.credentialHashCache == null)
					{
						this.credentialHashCache = new MruCache<string, string>(5);
					}
				}
			}
			string text = TransferModeHelper.IsRequestStreamed(this.TransferMode) ? "streamed" : string.Empty;
			if (HttpChannelFactory<TChannel>.IsWindowsAuth(this.AuthenticationScheme))
			{
				if (!HttpChannelFactory<TChannel>.httpWebRequestWebPermissionDenied)
				{
					try
					{
						httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
					}
					catch (SecurityException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						HttpChannelFactory<TChannel>.httpWebRequestWebPermissionDenied = true;
					}
				}
				text = this.AppendWindowsAuthenticationInfo(text, credential, authenticationLevel, impersonationLevel);
			}
			string str = this.OnGetConnectionGroupPrefix(httpWebRequest, clientCertificateToken);
			text = this.uniqueConnectionGroupNamePrefix + str + text;
			string text2 = null;
			if (!string.IsNullOrEmpty(text))
			{
				MruCache<string, string> obj = this.credentialHashCache;
				lock (obj)
				{
					if (!this.credentialHashCache.TryGetValue(text, out text2))
					{
						byte[] bytes = new UTF8Encoding().GetBytes(text);
						byte[] inArray = this.HashAlgorithm.ComputeHash(bytes);
						text2 = Convert.ToBase64String(inArray);
						this.credentialHashCache.Add(text, text2);
					}
				}
			}
			return text2;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00126D74 File Offset: 0x00124F74
		private Uri GetCredentialCacheUriPrefix(Uri via)
		{
			if (this.credentialCacheUriPrefixCache == null)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.credentialCacheUriPrefixCache == null)
					{
						this.credentialCacheUriPrefixCache = new MruCache<Uri, Uri>(10);
					}
				}
			}
			MruCache<Uri, Uri> obj = this.credentialCacheUriPrefixCache;
			Uri uri;
			lock (obj)
			{
				if (!this.credentialCacheUriPrefixCache.TryGetValue(via, out uri))
				{
					uri = new UriBuilder(via.Scheme, via.Host, via.Port).Uri;
					this.credentialCacheUriPrefixCache.Add(via, uri);
				}
			}
			return uri;
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00126E40 File Offset: 0x00125040
		private HttpWebRequest GetWebRequest(EndpointAddress to, Uri via, NetworkCredential credential, TokenImpersonationLevel impersonationLevel, AuthenticationLevel authenticationLevel, SecurityTokenProviderContainer proxyTokenProvider, SecurityTokenContainer clientCertificateToken, TimeSpan timeout, bool isWebSocketRequest)
		{
			Uri requestUri = isWebSocketRequest ? WebSocketHelper.GetWebSocketUri(via) : via;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			if (!isWebSocketRequest)
			{
				httpWebRequest.Method = "POST";
				if (TransferModeHelper.IsRequestStreamed(this.TransferMode))
				{
					httpWebRequest.SendChunked = true;
					httpWebRequest.AllowWriteStreamBuffering = false;
				}
				else
				{
					httpWebRequest.AllowWriteStreamBuffering = true;
				}
			}
			httpWebRequest.CachePolicy = HttpChannelFactory<TChannel>.requestCachePolicy;
			httpWebRequest.KeepAlive = this.keepAliveEnabled;
			if (this.decompressionEnabled)
			{
				httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			}
			else
			{
				httpWebRequest.AutomaticDecompression = DecompressionMethods.None;
			}
			if (credential != null)
			{
				httpWebRequest.Credentials = new CredentialCache
				{
					{
						this.GetCredentialCacheUriPrefix(via),
						AuthenticationSchemesHelper.ToString(this.authenticationScheme),
						credential
					}
				};
			}
			httpWebRequest.AuthenticationLevel = authenticationLevel;
			httpWebRequest.ImpersonationLevel = impersonationLevel;
			string text = this.GetConnectionGroupName(httpWebRequest, credential, authenticationLevel, impersonationLevel, clientCertificateToken);
			X509CertificateEndpointIdentity x509CertificateEndpointIdentity = to.Identity as X509CertificateEndpointIdentity;
			if (x509CertificateEndpointIdentity != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", new object[]
				{
					text,
					x509CertificateEndpointIdentity.Certificates[0].Thumbprint
				});
			}
			if (!string.IsNullOrEmpty(text))
			{
				httpWebRequest.ConnectionGroupName = text;
			}
			if (this.AuthenticationScheme == AuthenticationSchemes.Basic)
			{
				httpWebRequest.PreAuthenticate = true;
			}
			if (this.proxy != null)
			{
				httpWebRequest.Proxy = this.proxy;
			}
			else if (this.proxyFactory != null)
			{
				httpWebRequest.Proxy = this.proxyFactory.CreateWebProxy(httpWebRequest, proxyTokenProvider, timeout);
			}
			if (this.AllowCookies)
			{
				httpWebRequest.CookieContainer = this.httpCookieContainerManager.CookieContainer;
			}
			httpWebRequest.ServicePoint.UseNagleAlgorithm = false;
			return httpWebRequest;
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x00126FCC File Offset: 0x001251CC
		private void ApplyManualAddressing(ref EndpointAddress to, ref Uri via, Message message)
		{
			if (base.ManualAddressing)
			{
				Uri to2 = message.Headers.To;
				if (to2 == null)
				{
					throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ManualAddressingRequiresAddressedMessages")), message);
				}
				to = new EndpointAddress(to2, new AddressHeader[0]);
				if (base.MessageVersion.Addressing == AddressingVersion.None)
				{
					via = to2;
				}
			}
			object obj;
			if (message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)obj;
				if (!string.IsNullOrEmpty(httpRequestMessageProperty.QueryString))
				{
					UriBuilder uriBuilder = new UriBuilder(via);
					if (httpRequestMessageProperty.QueryString.StartsWith("?", StringComparison.Ordinal))
					{
						uriBuilder.Query = httpRequestMessageProperty.QueryString.Substring(1);
					}
					else
					{
						uriBuilder.Query = httpRequestMessageProperty.QueryString;
					}
					via = uriBuilder.Uri;
				}
			}
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0012709C File Offset: 0x0012529C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CreateAndOpenTokenProvidersCore(EndpointAddress to, Uri via, ChannelParameterCollection channelParameters, TimeSpan timeout, out SecurityTokenProviderContainer tokenProvider, out SecurityTokenProviderContainer proxyTokenProvider)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			tokenProvider = this.CreateAndOpenTokenProvider(timeoutHelper.RemainingTime(), this.AuthenticationScheme, to, via, channelParameters);
			if (this.proxyFactory != null)
			{
				proxyTokenProvider = this.CreateAndOpenTokenProvider(timeoutHelper.RemainingTime(), this.proxyFactory.AuthenticationScheme, to, via, channelParameters);
				return;
			}
			proxyTokenProvider = null;
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x001270F6 File Offset: 0x001252F6
		internal void CreateAndOpenTokenProviders(EndpointAddress to, Uri via, ChannelParameterCollection channelParameters, TimeSpan timeout, out SecurityTokenProviderContainer tokenProvider, out SecurityTokenProviderContainer proxyTokenProvider)
		{
			if (!this.IsSecurityTokenManagerRequired())
			{
				tokenProvider = null;
				proxyTokenProvider = null;
				return;
			}
			this.CreateAndOpenTokenProvidersCore(to, via, channelParameters, timeout, out tokenProvider, out proxyTokenProvider);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00127118 File Offset: 0x00125318
		internal HttpWebRequest GetWebRequest(EndpointAddress to, Uri via, SecurityTokenProviderContainer tokenProvider, SecurityTokenProviderContainer proxyTokenProvider, SecurityTokenContainer clientCertificateToken, TimeSpan timeout, bool isWebSocketRequest)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			TokenImpersonationLevel impersonationLevel;
			AuthenticationLevel authenticationLevel;
			NetworkCredential credential = HttpChannelUtilities.GetCredential(this.authenticationScheme, tokenProvider, timeoutHelper.RemainingTime(), out impersonationLevel, out authenticationLevel);
			return this.GetWebRequest(to, via, credential, impersonationLevel, authenticationLevel, proxyTokenProvider, clientCertificateToken, timeoutHelper.RemainingTime(), isWebSocketRequest);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0012715E File Offset: 0x0012535E
		internal static bool MapIdentity(EndpointAddress target, AuthenticationSchemes authenticationScheme)
		{
			return target.Identity != null && !(target.Identity is X509CertificateEndpointIdentity) && HttpChannelFactory<TChannel>.IsWindowsAuth(authenticationScheme);
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0012717D File Offset: 0x0012537D
		private bool MapIdentity(EndpointAddress target)
		{
			return HttpChannelFactory<TChannel>.MapIdentity(target, this.AuthenticationScheme);
		}

		// Token: 0x040031A6 RID: 12710
		private static bool httpWebRequestWebPermissionDenied = false;

		// Token: 0x040031A7 RID: 12711
		private static RequestCachePolicy requestCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

		// Token: 0x040031A8 RID: 12712
		private static long connectionGroupNamePrefix = 0L;

		// Token: 0x040031A9 RID: 12713
		private readonly ClientWebSocketFactory clientWebSocketFactory;

		// Token: 0x040031AA RID: 12714
		private bool allowCookies;

		// Token: 0x040031AB RID: 12715
		private AuthenticationSchemes authenticationScheme;

		// Token: 0x040031AC RID: 12716
		private HttpCookieContainerManager httpCookieContainerManager;

		// Token: 0x040031AD RID: 12717
		private volatile MruCache<Uri, Uri> credentialCacheUriPrefixCache;

		// Token: 0x040031AE RID: 12718
		private bool decompressionEnabled;

		// Token: 0x040031AF RID: 12719
		[SecurityCritical]
		private volatile MruCache<string, string> credentialHashCache;

		// Token: 0x040031B0 RID: 12720
		[SecurityCritical]
		private HashAlgorithm hashAlgorithm;

		// Token: 0x040031B1 RID: 12721
		private bool keepAliveEnabled;

		// Token: 0x040031B2 RID: 12722
		private int maxBufferSize;

		// Token: 0x040031B3 RID: 12723
		private IWebProxy proxy;

		// Token: 0x040031B4 RID: 12724
		private HttpChannelFactory<TChannel>.WebProxyFactory proxyFactory;

		// Token: 0x040031B5 RID: 12725
		private SecurityCredentialsManager channelCredentials;

		// Token: 0x040031B6 RID: 12726
		private SecurityTokenManager securityTokenManager;

		// Token: 0x040031B7 RID: 12727
		private TransferMode transferMode;

		// Token: 0x040031B8 RID: 12728
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x040031B9 RID: 12729
		private WebSocketTransportSettings webSocketSettings;

		// Token: 0x040031BA RID: 12730
		private ConnectionBufferPool bufferPool;

		// Token: 0x040031BB RID: 12731
		private Lazy<string> webSocketSoapContentType;

		// Token: 0x040031BC RID: 12732
		private string uniqueConnectionGroupNamePrefix;

		// Token: 0x02000D3E RID: 3390
		protected class HttpRequestChannel : RequestChannel
		{
			// Token: 0x06007C4F RID: 31823 RVA: 0x001D0B38 File Offset: 0x001CED38
			public HttpRequestChannel(HttpChannelFactory<IRequestChannel> factory, EndpointAddress to, Uri via, bool manualAddressing) : base(factory, to, via, manualAddressing)
			{
				this.factory = factory;
			}

			// Token: 0x17001BD5 RID: 7125
			// (get) Token: 0x06007C50 RID: 31824 RVA: 0x001D0B4C File Offset: 0x001CED4C
			public HttpChannelFactory<IRequestChannel> Factory
			{
				get
				{
					return this.factory;
				}
			}

			// Token: 0x17001BD6 RID: 7126
			// (get) Token: 0x06007C51 RID: 31825 RVA: 0x001D0B54 File Offset: 0x001CED54
			internal ServiceModelActivity Activity
			{
				get
				{
					return this.activity;
				}
			}

			// Token: 0x17001BD7 RID: 7127
			// (get) Token: 0x06007C52 RID: 31826 RVA: 0x001D0B5C File Offset: 0x001CED5C
			protected ChannelParameterCollection ChannelParameters
			{
				get
				{
					return this.channelParameters;
				}
			}

			// Token: 0x06007C53 RID: 31827 RVA: 0x001D0B64 File Offset: 0x001CED64
			public override T GetProperty<T>()
			{
				if (typeof(T) == typeof(ChannelParameterCollection))
				{
					if (base.State == CommunicationState.Created)
					{
						object thisLock = base.ThisLock;
						lock (thisLock)
						{
							if (this.channelParameters == null)
							{
								this.channelParameters = new ChannelParameterCollection();
							}
						}
					}
					return (T)((object)this.channelParameters);
				}
				return base.GetProperty<T>();
			}

			// Token: 0x06007C54 RID: 31828 RVA: 0x001D0BE8 File Offset: 0x001CEDE8
			private void PrepareOpen()
			{
				if (this.Factory.MapIdentity(base.RemoteAddress))
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						this.cleanupIdentity = HttpTransportSecurityHelpers.AddIdentityMapping(base.Via, base.RemoteAddress);
					}
				}
			}

			// Token: 0x06007C55 RID: 31829 RVA: 0x001D0C50 File Offset: 0x001CEE50
			private void CreateAndOpenTokenProviders(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (!base.ManualAddressing)
				{
					this.Factory.CreateAndOpenTokenProviders(base.RemoteAddress, base.Via, this.channelParameters, timeoutHelper.RemainingTime(), out this.tokenProvider, out this.proxyTokenProvider);
				}
			}

			// Token: 0x06007C56 RID: 31830 RVA: 0x001D0CA0 File Offset: 0x001CEEA0
			private void CloseTokenProviders(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (this.tokenProvider != null)
				{
					this.tokenProvider.Close(timeoutHelper.RemainingTime());
				}
				if (this.proxyTokenProvider != null)
				{
					this.proxyTokenProvider.Close(timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x06007C57 RID: 31831 RVA: 0x001D0CE9 File Offset: 0x001CEEE9
			private void AbortTokenProviders()
			{
				if (this.tokenProvider != null)
				{
					this.tokenProvider.Abort();
				}
				if (this.proxyTokenProvider != null)
				{
					this.proxyTokenProvider.Abort();
				}
			}

			// Token: 0x06007C58 RID: 31832 RVA: 0x001D0D14 File Offset: 0x001CEF14
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.PrepareOpen();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.CreateAndOpenTokenProviders(timeoutHelper.RemainingTime());
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x06007C59 RID: 31833 RVA: 0x001D0D43 File Offset: 0x001CEF43
			protected override void OnOpen(TimeSpan timeout)
			{
				this.PrepareOpen();
				this.CreateAndOpenTokenProviders(timeout);
			}

			// Token: 0x06007C5A RID: 31834 RVA: 0x001D0D52 File Offset: 0x001CEF52
			protected override void OnEndOpen(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x06007C5B RID: 31835 RVA: 0x001D0D5C File Offset: 0x001CEF5C
			private void PrepareClose(bool aborting)
			{
				if (this.cleanupIdentity)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.cleanupIdentity)
						{
							this.cleanupIdentity = false;
							HttpTransportSecurityHelpers.RemoveIdentityMapping(base.Via, base.RemoteAddress, !aborting);
						}
					}
				}
			}

			// Token: 0x06007C5C RID: 31836 RVA: 0x001D0DC8 File Offset: 0x001CEFC8
			protected override void OnAbort()
			{
				this.PrepareClose(true);
				this.AbortTokenProviders();
				base.OnAbort();
			}

			// Token: 0x06007C5D RID: 31837 RVA: 0x001D0DE0 File Offset: 0x001CEFE0
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				IAsyncResult result = null;
				using (ServiceModelActivity.BoundOperation(this.activity))
				{
					this.PrepareClose(false);
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.CloseTokenProviders(timeoutHelper.RemainingTime());
					result = base.BeginWaitForPendingRequests(timeoutHelper.RemainingTime(), callback, state);
				}
				ServiceModelActivity.Stop(this.activity);
				return result;
			}

			// Token: 0x06007C5E RID: 31838 RVA: 0x001D0E50 File Offset: 0x001CF050
			protected override void OnEndClose(IAsyncResult result)
			{
				using (ServiceModelActivity.BoundOperation(this.activity))
				{
					base.EndWaitForPendingRequests(result);
				}
				ServiceModelActivity.Stop(this.activity);
			}

			// Token: 0x06007C5F RID: 31839 RVA: 0x001D0E98 File Offset: 0x001CF098
			protected override void OnClose(TimeSpan timeout)
			{
				using (ServiceModelActivity.BoundOperation(this.activity))
				{
					this.PrepareClose(false);
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.CloseTokenProviders(timeoutHelper.RemainingTime());
					base.WaitForPendingRequests(timeoutHelper.RemainingTime());
				}
				ServiceModelActivity.Stop(this.activity);
			}

			// Token: 0x06007C60 RID: 31840 RVA: 0x001D0F04 File Offset: 0x001CF104
			protected override IAsyncRequest CreateAsyncRequest(Message message, AsyncCallback callback, object state)
			{
				if (DiagnosticUtility.ShouldUseActivity && this.activity == null)
				{
					this.activity = ServiceModelActivity.CreateActivity();
					if (FxTrace.Trace != null)
					{
						FxTrace.Trace.TraceTransfer(this.activity.Id);
					}
					ServiceModelActivity.Start(this.activity, SR.GetString("ActivityReceiveBytes", new object[]
					{
						base.RemoteAddress.Uri.ToString()
					}), ActivityType.ReceiveBytes);
				}
				return new HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest(this, callback, state);
			}

			// Token: 0x06007C61 RID: 31841 RVA: 0x001D0F7F File Offset: 0x001CF17F
			protected override IRequest CreateRequest(Message message)
			{
				return new HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelRequest(this, this.Factory);
			}

			// Token: 0x06007C62 RID: 31842 RVA: 0x001D0F8D File Offset: 0x001CF18D
			public virtual HttpWebRequest GetWebRequest(EndpointAddress to, Uri via, ref TimeoutHelper timeoutHelper)
			{
				return this.GetWebRequest(to, via, null, ref timeoutHelper);
			}

			// Token: 0x06007C63 RID: 31843 RVA: 0x001D0F9C File Offset: 0x001CF19C
			protected HttpWebRequest GetWebRequest(EndpointAddress to, Uri via, SecurityTokenContainer clientCertificateToken, ref TimeoutHelper timeoutHelper)
			{
				SecurityTokenProviderContainer securityTokenProviderContainer;
				SecurityTokenProviderContainer securityTokenProviderContainer2;
				if (base.ManualAddressing)
				{
					this.Factory.CreateAndOpenTokenProviders(to, via, this.channelParameters, timeoutHelper.RemainingTime(), out securityTokenProviderContainer, out securityTokenProviderContainer2);
				}
				else
				{
					securityTokenProviderContainer = this.tokenProvider;
					securityTokenProviderContainer2 = this.proxyTokenProvider;
				}
				HttpWebRequest webRequest;
				try
				{
					webRequest = this.Factory.GetWebRequest(to, via, securityTokenProviderContainer, securityTokenProviderContainer2, clientCertificateToken, timeoutHelper.RemainingTime(), false);
				}
				finally
				{
					if (base.ManualAddressing)
					{
						if (securityTokenProviderContainer != null)
						{
							securityTokenProviderContainer.Abort();
						}
						if (securityTokenProviderContainer2 != null)
						{
							securityTokenProviderContainer2.Abort();
						}
					}
				}
				return webRequest;
			}

			// Token: 0x06007C64 RID: 31844 RVA: 0x001D1028 File Offset: 0x001CF228
			protected IAsyncResult BeginGetWebRequest(EndpointAddress to, Uri via, SecurityTokenContainer clientCertificateToken, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				return new HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult(this, to, via, clientCertificateToken, ref timeoutHelper, callback, state);
			}

			// Token: 0x06007C65 RID: 31845 RVA: 0x001D1039 File Offset: 0x001CF239
			public virtual IAsyncResult BeginGetWebRequest(EndpointAddress to, Uri via, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state)
			{
				return this.BeginGetWebRequest(to, via, null, ref timeoutHelper, callback, state);
			}

			// Token: 0x06007C66 RID: 31846 RVA: 0x001D1049 File Offset: 0x001CF249
			public virtual HttpWebRequest EndGetWebRequest(IAsyncResult result)
			{
				return HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.End(result);
			}

			// Token: 0x06007C67 RID: 31847 RVA: 0x001D1051 File Offset: 0x001CF251
			public virtual bool WillGetWebRequestCompleteSynchronously()
			{
				return this.tokenProvider == null && !this.Factory.ManualAddressing;
			}

			// Token: 0x06007C68 RID: 31848 RVA: 0x001D106B File Offset: 0x001CF26B
			internal virtual void OnWebRequestCompleted(HttpWebRequest request)
			{
			}

			// Token: 0x0400477A RID: 18298
			private volatile bool cleanupIdentity;

			// Token: 0x0400477B RID: 18299
			private HttpChannelFactory<IRequestChannel> factory;

			// Token: 0x0400477C RID: 18300
			private SecurityTokenProviderContainer tokenProvider;

			// Token: 0x0400477D RID: 18301
			private SecurityTokenProviderContainer proxyTokenProvider;

			// Token: 0x0400477E RID: 18302
			private ServiceModelActivity activity;

			// Token: 0x0400477F RID: 18303
			private ChannelParameterCollection channelParameters;

			// Token: 0x02000F55 RID: 3925
			private class HttpChannelRequest : IRequest, IRequestBase
			{
				// Token: 0x0600871D RID: 34589 RVA: 0x001F4ECC File Offset: 0x001F30CC
				public HttpChannelRequest(HttpChannelFactory<TChannel>.HttpRequestChannel channel, HttpChannelFactory<IRequestChannel> factory)
				{
					this.channel = channel;
					this.to = channel.RemoteAddress;
					this.via = channel.Via;
					this.factory = factory;
				}

				// Token: 0x0600871E RID: 34590 RVA: 0x001F4EFC File Offset: 0x001F30FC
				private string GetConnectionGroupPrefix(Message message)
				{
					object obj;
					if (message.Properties.TryGetValue("HttpTransportConnectionGroupNamePrefix", out obj))
					{
						string text = obj as string;
						if (text != null)
						{
							return text;
						}
					}
					return string.Empty;
				}

				// Token: 0x0600871F RID: 34591 RVA: 0x001F4F30 File Offset: 0x001F3130
				public void SendRequest(Message message, TimeSpan timeout)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.factory.ApplyManualAddressing(ref this.to, ref this.via, message);
					this.webRequest = this.channel.GetWebRequest(this.to, this.via, ref timeoutHelper);
					this.webRequest.ConnectionGroupName = this.GetConnectionGroupPrefix(message) + this.webRequest.ConnectionGroupName;
					try
					{
						if (this.channel.State != CommunicationState.Opened)
						{
							this.Cleanup();
							this.channel.ThrowIfDisposedOrNotOpen();
						}
						HttpChannelUtilities.SetRequestTimeout(this.webRequest, timeoutHelper.RemainingTime());
						HttpOutput httpOutput = HttpOutput.CreateHttpOutput(this.webRequest, this.factory, message, this.factory.IsChannelBindingSupportEnabled);
						bool flag = false;
						try
						{
							httpOutput.Send(timeoutHelper.RemainingTime());
							this.channelBinding = httpOutput.TakeChannelBinding();
							httpOutput.Close();
							flag = true;
							if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
							{
								this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
								if (TD.MessageSentByTransportIsEnabled())
								{
									TD.MessageSentByTransport(this.eventTraceActivity, this.to.Uri.AbsoluteUri);
								}
							}
						}
						finally
						{
							if (!flag)
							{
								httpOutput.Abort(HttpAbortReason.Aborted);
							}
						}
					}
					finally
					{
						if (message != message)
						{
							message.Close();
						}
					}
				}

				// Token: 0x06008720 RID: 34592 RVA: 0x001F5080 File Offset: 0x001F3280
				private void Cleanup()
				{
					if (this.webRequest != null)
					{
						HttpChannelUtilities.AbortRequest(this.webRequest);
						this.TryCompleteWebRequest(this.webRequest);
					}
					ChannelBindingUtility.Dispose(ref this.channelBinding);
				}

				// Token: 0x06008721 RID: 34593 RVA: 0x001F50AC File Offset: 0x001F32AC
				public void Abort(RequestChannel channel)
				{
					this.Cleanup();
					this.abortReason = HttpAbortReason.Aborted;
				}

				// Token: 0x06008722 RID: 34594 RVA: 0x001F50BB File Offset: 0x001F32BB
				public void Fault(RequestChannel channel)
				{
					this.Cleanup();
				}

				// Token: 0x06008723 RID: 34595 RVA: 0x001F50C4 File Offset: 0x001F32C4
				public Message WaitForReply(TimeSpan timeout)
				{
					if (TD.HttpResponseReceiveStartIsEnabled())
					{
						TD.HttpResponseReceiveStart(this.eventTraceActivity);
					}
					HttpWebResponse httpWebResponse = null;
					WebException responseException = null;
					try
					{
						try
						{
							httpWebResponse = (HttpWebResponse)this.webRequest.GetResponse();
						}
						catch (NullReferenceException nullReferenceException)
						{
							if (TransferModeHelper.IsRequestStreamed(this.factory.transferMode))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateNullReferenceResponseException(nullReferenceException));
							}
							throw;
						}
						if (TD.MessageReceivedByTransportIsEnabled())
						{
							TD.MessageReceivedByTransport(this.eventTraceActivity ?? EventTraceActivity.Empty, (httpWebResponse.ResponseUri != null) ? httpWebResponse.ResponseUri.AbsoluteUri : string.Empty, EventTraceActivity.GetActivityIdFromThread());
						}
						if (DiagnosticUtility.ShouldTraceVerbose)
						{
							HttpChannelFactory<TChannel>.TraceResponseReceived(httpWebResponse, null, this);
						}
					}
					catch (WebException ex)
					{
						responseException = ex;
						httpWebResponse = HttpChannelUtilities.ProcessGetResponseWebException(ex, this.webRequest, this.abortReason);
					}
					HttpInput httpInput = HttpChannelUtilities.ValidateRequestReplyResponse(this.webRequest, httpWebResponse, this.factory, responseException, this.channelBinding);
					this.channelBinding = null;
					Message message = null;
					if (httpInput != null)
					{
						Exception ex2 = null;
						message = httpInput.ParseIncomingMessage(out ex2);
						if (message != null)
						{
							HttpChannelUtilities.AddReplySecurityProperty(this.factory, this.webRequest, httpWebResponse, message);
							if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled && this.eventTraceActivity != null)
							{
								EventTraceActivityHelper.TryAttachActivity(message, this.eventTraceActivity);
							}
						}
					}
					this.TryCompleteWebRequest(this.webRequest);
					return message;
				}

				// Token: 0x06008724 RID: 34596 RVA: 0x001F5220 File Offset: 0x001F3420
				public void OnReleaseRequest()
				{
					this.TryCompleteWebRequest(this.webRequest);
				}

				// Token: 0x06008725 RID: 34597 RVA: 0x001F522E File Offset: 0x001F342E
				private void TryCompleteWebRequest(HttpWebRequest request)
				{
					if (request == null)
					{
						return;
					}
					if (Interlocked.CompareExchange(ref this.webRequestCompleted, 1, 0) == 0)
					{
						this.channel.OnWebRequestCompleted(request);
					}
				}

				// Token: 0x04004E98 RID: 20120
				private HttpChannelFactory<TChannel>.HttpRequestChannel channel;

				// Token: 0x04004E99 RID: 20121
				private HttpChannelFactory<IRequestChannel> factory;

				// Token: 0x04004E9A RID: 20122
				private EndpointAddress to;

				// Token: 0x04004E9B RID: 20123
				private Uri via;

				// Token: 0x04004E9C RID: 20124
				private HttpWebRequest webRequest;

				// Token: 0x04004E9D RID: 20125
				private HttpAbortReason abortReason;

				// Token: 0x04004E9E RID: 20126
				private ChannelBinding channelBinding;

				// Token: 0x04004E9F RID: 20127
				private int webRequestCompleted;

				// Token: 0x04004EA0 RID: 20128
				private EventTraceActivity eventTraceActivity;

				// Token: 0x04004EA1 RID: 20129
				private const string ConnectionGroupPrefixMessagePropertyName = "HttpTransportConnectionGroupNamePrefix";
			}

			// Token: 0x02000F56 RID: 3926
			private class HttpChannelAsyncRequest : TraceAsyncResult, IAsyncRequest, IAsyncResult, IRequestBase
			{
				// Token: 0x06008726 RID: 34598 RVA: 0x001F5250 File Offset: 0x001F3450
				public HttpChannelAsyncRequest(HttpChannelFactory<TChannel>.HttpRequestChannel channel, AsyncCallback callback, object state) : base(callback, state)
				{
					this.channel = channel;
					this.to = channel.RemoteAddress;
					this.via = channel.Via;
					this.factory = channel.Factory;
				}

				// Token: 0x17001D90 RID: 7568
				// (get) Token: 0x06008727 RID: 34599 RVA: 0x001F52A6 File Offset: 0x001F34A6
				private IOThreadTimer SendTimer
				{
					get
					{
						if (this.sendTimer == null)
						{
							if (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onSendTimeout == null)
							{
								HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onSendTimeout = new Action<object>(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.OnSendTimeout);
							}
							this.sendTimer = new IOThreadTimer(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onSendTimeout, this, false);
						}
						return this.sendTimer;
					}
				}

				// Token: 0x17001D91 RID: 7569
				// (get) Token: 0x06008728 RID: 34600 RVA: 0x001F52E0 File Offset: 0x001F34E0
				private IOThreadTimer ReceiveTimer
				{
					get
					{
						if (this.receiveTimer == null)
						{
							if (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onReceiveTimeout == null)
							{
								HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onReceiveTimeout = new Action<object>(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.OnReceiveTimeout);
							}
							this.receiveTimer = new IOThreadTimer(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onReceiveTimeout, this, false);
						}
						return this.receiveTimer;
					}
				}

				// Token: 0x06008729 RID: 34601 RVA: 0x001F531A File Offset: 0x001F351A
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest>(result);
				}

				// Token: 0x0600872A RID: 34602 RVA: 0x001F5324 File Offset: 0x001F3524
				public void BeginSendRequest(Message message, TimeSpan timeout)
				{
					this.requestMessage = message;
					this.message = message;
					this.timeoutHelper = new TimeoutHelper(timeout);
					if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
					{
						this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					}
					this.factory.ApplyManualAddressing(ref this.to, ref this.via, this.requestMessage);
					if (this.channel.WillGetWebRequestCompleteSynchronously())
					{
						this.SetWebRequest(this.channel.GetWebRequest(this.to, this.via, ref this.timeoutHelper));
						if (this.SendWebRequest())
						{
							base.Complete(true);
							return;
						}
					}
					else
					{
						if (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onGetWebRequestCompleted == null)
						{
							HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onGetWebRequestCompleted = Fx.ThunkCallback(new AsyncCallback(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.OnGetWebRequestCompletedCallback));
						}
						IAsyncResult asyncResult = this.channel.BeginGetWebRequest(this.to, this.via, ref this.timeoutHelper, HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onGetWebRequestCompleted, this);
						if (asyncResult.CompletedSynchronously)
						{
							if (TD.MessageSentByTransportIsEnabled())
							{
								TD.MessageSentByTransport(this.eventTraceActivity, this.to.Uri.AbsoluteUri);
							}
							if (this.OnGetWebRequestCompleted(asyncResult))
							{
								base.Complete(true);
							}
						}
					}
				}

				// Token: 0x0600872B RID: 34603 RVA: 0x001F5440 File Offset: 0x001F3640
				private static void OnGetWebRequestCompletedCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest httpChannelAsyncRequest = (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest)result.AsyncState;
					Exception exception = null;
					bool flag;
					try
					{
						flag = httpChannelAsyncRequest.OnGetWebRequestCompleted(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						httpChannelAsyncRequest.Complete(false, exception);
					}
				}

				// Token: 0x0600872C RID: 34604 RVA: 0x001F549C File Offset: 0x001F369C
				private void AbortSend()
				{
					this.CancelSendTimer();
					if (this.request != null)
					{
						this.TryCompleteWebRequest(this.request);
						this.abortReason = HttpAbortReason.TimedOut;
						this.httpOutput.Abort(this.abortReason);
					}
				}

				// Token: 0x0600872D RID: 34605 RVA: 0x001F54D0 File Offset: 0x001F36D0
				private void CancelSendTimer()
				{
					object obj = this.sendLock;
					lock (obj)
					{
						if (this.sendTimer != null)
						{
							this.sendTimer.Cancel();
							this.sendTimer = null;
						}
					}
				}

				// Token: 0x0600872E RID: 34606 RVA: 0x001F5528 File Offset: 0x001F3728
				private void AbortReceive()
				{
					this.CancelReceiveTimer();
					if (this.request != null)
					{
						this.TryCompleteWebRequest(this.request);
						this.abortReason = HttpAbortReason.TimedOut;
						if (this.httpInput != null)
						{
							this.httpInput.Abort(this.abortReason);
						}
					}
				}

				// Token: 0x0600872F RID: 34607 RVA: 0x001F5564 File Offset: 0x001F3764
				private void CancelReceiveTimer()
				{
					object obj = this.receiveLock;
					lock (obj)
					{
						if (this.receiveTimer != null)
						{
							this.receiveTimer.Cancel();
							this.receiveTimer = null;
						}
					}
				}

				// Token: 0x06008730 RID: 34608 RVA: 0x001F55BC File Offset: 0x001F37BC
				private bool OnGetWebRequestCompleted(IAsyncResult result)
				{
					this.SetWebRequest(this.channel.EndGetWebRequest(result));
					return this.SendWebRequest();
				}

				// Token: 0x06008731 RID: 34609 RVA: 0x001F55D8 File Offset: 0x001F37D8
				private bool SendWebRequest()
				{
					this.httpOutput = HttpOutput.CreateHttpOutput(this.request, this.factory, this.requestMessage, this.factory.IsChannelBindingSupportEnabled);
					bool flag = false;
					bool result;
					try
					{
						bool flag2 = false;
						this.SetSendTimeout(this.timeoutHelper.RemainingTime());
						IAsyncResult asyncResult = this.httpOutput.BeginSend(this.timeoutHelper.RemainingTime(), HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onSend, this);
						flag = true;
						if (asyncResult.CompletedSynchronously)
						{
							flag2 = this.CompleteSend(asyncResult);
						}
						result = flag2;
					}
					finally
					{
						if (!flag)
						{
							this.httpOutput.Abort(HttpAbortReason.Aborted);
							if (this.message != this.requestMessage)
							{
								this.requestMessage.Close();
							}
						}
					}
					return result;
				}

				// Token: 0x06008732 RID: 34610 RVA: 0x001F5690 File Offset: 0x001F3890
				private bool CompleteSend(IAsyncResult result)
				{
					bool flag = false;
					try
					{
						this.httpOutput.EndSend(result);
						this.channelBinding = this.httpOutput.TakeChannelBinding();
						this.httpOutput.Close();
						flag = true;
						if (TD.MessageSentByTransportIsEnabled())
						{
							TD.MessageSentByTransport(this.eventTraceActivity, this.to.Uri.AbsoluteUri);
						}
					}
					finally
					{
						if (!flag)
						{
							this.httpOutput.Abort(HttpAbortReason.Aborted);
						}
						if (this.message != this.requestMessage)
						{
							this.requestMessage.Close();
						}
					}
					bool result2;
					try
					{
						IAsyncResult asyncResult;
						try
						{
							asyncResult = this.request.BeginGetResponse(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onGetResponse, this);
						}
						catch (NullReferenceException nullReferenceException)
						{
							if (TransferModeHelper.IsRequestStreamed(this.factory.transferMode))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateNullReferenceResponseException(nullReferenceException));
							}
							throw;
						}
						if (asyncResult.CompletedSynchronously)
						{
							result2 = this.CompleteGetResponse(asyncResult);
						}
						else
						{
							result2 = false;
						}
					}
					catch (IOException ex)
					{
						throw TraceUtility.ThrowHelperError(new CommunicationException(ex.Message, ex), this.requestMessage);
					}
					catch (WebException ex2)
					{
						throw TraceUtility.ThrowHelperError(new CommunicationException(ex2.Message, ex2), this.requestMessage);
					}
					catch (ObjectDisposedException innerException)
					{
						if (this.abortReason == HttpAbortReason.Aborted)
						{
							throw TraceUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("HttpRequestAborted", new object[]
							{
								this.to.Uri
							}), innerException), this.requestMessage);
						}
						throw TraceUtility.ThrowHelperError(new TimeoutException(SR.GetString("HttpRequestTimedOut", new object[]
						{
							this.to.Uri,
							this.timeoutHelper.OriginalTimeout
						}), innerException), this.requestMessage);
					}
					return result2;
				}

				// Token: 0x06008733 RID: 34611 RVA: 0x001F5868 File Offset: 0x001F3A68
				private bool CompleteGetResponse(IAsyncResult result)
				{
					bool result2;
					using (ServiceModelActivity.BoundOperation(this.channel.Activity))
					{
						HttpWebResponse httpWebResponse = null;
						WebException responseException = null;
						try
						{
							try
							{
								this.CancelSendTimer();
								httpWebResponse = (HttpWebResponse)this.request.EndGetResponse(result);
							}
							catch (NullReferenceException nullReferenceException)
							{
								if (TransferModeHelper.IsRequestStreamed(this.factory.transferMode))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateNullReferenceResponseException(nullReferenceException));
								}
								throw;
							}
							if (TD.MessageReceivedByTransportIsEnabled())
							{
								TD.MessageReceivedByTransport(this.eventTraceActivity ?? EventTraceActivity.Empty, this.to.Uri.AbsoluteUri, EventTraceActivity.GetActivityIdFromThread());
							}
							if (DiagnosticUtility.ShouldTraceVerbose)
							{
								HttpChannelFactory<TChannel>.TraceResponseReceived(httpWebResponse, this.message, this);
							}
						}
						catch (WebException ex)
						{
							responseException = ex;
							httpWebResponse = HttpChannelUtilities.ProcessGetResponseWebException(ex, this.request, this.abortReason);
						}
						result2 = this.ProcessResponse(httpWebResponse, responseException);
					}
					return result2;
				}

				// Token: 0x06008734 RID: 34612 RVA: 0x001F596C File Offset: 0x001F3B6C
				private void Cleanup()
				{
					if (this.request != null)
					{
						HttpChannelUtilities.AbortRequest(this.request);
						this.TryCompleteWebRequest(this.request);
					}
					ChannelBindingUtility.Dispose(ref this.channelBinding);
				}

				// Token: 0x06008735 RID: 34613 RVA: 0x001F5998 File Offset: 0x001F3B98
				private void SetSendTimeout(TimeSpan timeout)
				{
					HttpChannelUtilities.SetRequestTimeout(this.request, timeout);
					if (timeout == TimeSpan.MaxValue)
					{
						this.CancelSendTimer();
						return;
					}
					this.SendTimer.Set(timeout);
				}

				// Token: 0x06008736 RID: 34614 RVA: 0x001F59C6 File Offset: 0x001F3BC6
				private void SetReceiveTimeout(TimeSpan timeout)
				{
					if (timeout == TimeSpan.MaxValue)
					{
						this.CancelReceiveTimer();
						return;
					}
					this.ReceiveTimer.Set(timeout);
				}

				// Token: 0x06008737 RID: 34615 RVA: 0x001F59E8 File Offset: 0x001F3BE8
				public void Abort(RequestChannel channel)
				{
					this.Cleanup();
					this.abortReason = HttpAbortReason.Aborted;
				}

				// Token: 0x06008738 RID: 34616 RVA: 0x001F59F7 File Offset: 0x001F3BF7
				public void Fault(RequestChannel channel)
				{
					this.Cleanup();
				}

				// Token: 0x06008739 RID: 34617 RVA: 0x001F59FF File Offset: 0x001F3BFF
				private void SetWebRequest(HttpWebRequest webRequest)
				{
					this.request = webRequest;
					if (this.channel.State != CommunicationState.Opened)
					{
						this.Cleanup();
						this.channel.ThrowIfDisposedOrNotOpen();
					}
				}

				// Token: 0x0600873A RID: 34618 RVA: 0x001F5A27 File Offset: 0x001F3C27
				public Message End()
				{
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.End(this);
					return this.replyMessage;
				}

				// Token: 0x0600873B RID: 34619 RVA: 0x001F5A38 File Offset: 0x001F3C38
				private bool ProcessResponse(HttpWebResponse response, WebException responseException)
				{
					if (!TransferModeHelper.IsResponseStreamed(this.factory.transferMode))
					{
						this.SetReceiveTimeout(this.timeoutHelper.RemainingTime());
					}
					this.httpInput = HttpChannelUtilities.ValidateRequestReplyResponse(this.request, response, this.factory, responseException, this.channelBinding);
					this.channelBinding = null;
					if (this.httpInput != null)
					{
						if (this.abortReason == HttpAbortReason.TimedOut)
						{
							this.httpInput.Abort(this.abortReason);
							return true;
						}
						this.response = response;
						IAsyncResult asyncResult = this.httpInput.BeginParseIncomingMessage(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.onProcessIncomingMessage, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						this.CompleteParseIncomingMessage(asyncResult);
					}
					else
					{
						this.CancelReceiveTimer();
						this.replyMessage = null;
					}
					this.TryCompleteWebRequest(this.request);
					return true;
				}

				// Token: 0x0600873C RID: 34620 RVA: 0x001F5AF8 File Offset: 0x001F3CF8
				private void CompleteParseIncomingMessage(IAsyncResult result)
				{
					try
					{
						Exception ex = null;
						this.replyMessage = this.httpInput.EndParseIncomingMessage(result, out ex);
						if (this.replyMessage != null)
						{
							HttpChannelUtilities.AddReplySecurityProperty(this.factory, this.request, this.response, this.replyMessage);
						}
					}
					finally
					{
						this.CancelReceiveTimer();
					}
				}

				// Token: 0x0600873D RID: 34621 RVA: 0x001F5B5C File Offset: 0x001F3D5C
				private static void OnParseIncomingMessage(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest httpChannelAsyncRequest = (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest)result.AsyncState;
					Exception exception = null;
					try
					{
						httpChannelAsyncRequest.CompleteParseIncomingMessage(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					httpChannelAsyncRequest.Complete(false, exception);
				}

				// Token: 0x0600873E RID: 34622 RVA: 0x001F5BB0 File Offset: 0x001F3DB0
				private static void OnSend(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest httpChannelAsyncRequest = (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest)result.AsyncState;
					Exception exception = null;
					bool flag;
					try
					{
						flag = httpChannelAsyncRequest.CompleteSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						httpChannelAsyncRequest.Complete(false, exception);
					}
				}

				// Token: 0x0600873F RID: 34623 RVA: 0x001F5C0C File Offset: 0x001F3E0C
				private static void OnSendTimeout(object state)
				{
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest httpChannelAsyncRequest = (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest)state;
					httpChannelAsyncRequest.AbortSend();
				}

				// Token: 0x06008740 RID: 34624 RVA: 0x001F5C28 File Offset: 0x001F3E28
				private static void OnReceiveTimeout(object state)
				{
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest httpChannelAsyncRequest = (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest)state;
					httpChannelAsyncRequest.AbortReceive();
				}

				// Token: 0x06008741 RID: 34625 RVA: 0x001F5C44 File Offset: 0x001F3E44
				private static void OnGetResponse(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest httpChannelAsyncRequest = (HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest)result.AsyncState;
					Exception exception = null;
					bool flag;
					try
					{
						flag = httpChannelAsyncRequest.CompleteGetResponse(result);
					}
					catch (WebException ex)
					{
						flag = true;
						exception = new CommunicationException(ex.Message, ex);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						exception = ex2;
					}
					if (flag)
					{
						httpChannelAsyncRequest.Complete(false, exception);
					}
				}

				// Token: 0x06008742 RID: 34626 RVA: 0x001F5CC0 File Offset: 0x001F3EC0
				public void OnReleaseRequest()
				{
					this.TryCompleteWebRequest(this.request);
				}

				// Token: 0x06008743 RID: 34627 RVA: 0x001F5CCE File Offset: 0x001F3ECE
				private void TryCompleteWebRequest(HttpWebRequest request)
				{
					if (request == null)
					{
						return;
					}
					if (Interlocked.CompareExchange(ref this.webRequestCompleted, 1, 0) == 0)
					{
						this.channel.OnWebRequestCompleted(request);
					}
				}

				// Token: 0x04004EA2 RID: 20130
				private static AsyncCallback onProcessIncomingMessage = Fx.ThunkCallback(new AsyncCallback(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.OnParseIncomingMessage));

				// Token: 0x04004EA3 RID: 20131
				private static AsyncCallback onGetResponse = Fx.ThunkCallback(new AsyncCallback(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.OnGetResponse));

				// Token: 0x04004EA4 RID: 20132
				private static AsyncCallback onGetWebRequestCompleted;

				// Token: 0x04004EA5 RID: 20133
				private static AsyncCallback onSend = Fx.ThunkCallback(new AsyncCallback(HttpChannelFactory<TChannel>.HttpRequestChannel.HttpChannelAsyncRequest.OnSend));

				// Token: 0x04004EA6 RID: 20134
				private static Action<object> onSendTimeout;

				// Token: 0x04004EA7 RID: 20135
				private static Action<object> onReceiveTimeout;

				// Token: 0x04004EA8 RID: 20136
				private ChannelBinding channelBinding;

				// Token: 0x04004EA9 RID: 20137
				private HttpChannelFactory<IRequestChannel> factory;

				// Token: 0x04004EAA RID: 20138
				private HttpChannelFactory<TChannel>.HttpRequestChannel channel;

				// Token: 0x04004EAB RID: 20139
				private HttpOutput httpOutput;

				// Token: 0x04004EAC RID: 20140
				private HttpInput httpInput;

				// Token: 0x04004EAD RID: 20141
				private Message message;

				// Token: 0x04004EAE RID: 20142
				private Message requestMessage;

				// Token: 0x04004EAF RID: 20143
				private Message replyMessage;

				// Token: 0x04004EB0 RID: 20144
				private HttpWebResponse response;

				// Token: 0x04004EB1 RID: 20145
				private HttpWebRequest request;

				// Token: 0x04004EB2 RID: 20146
				private object sendLock = new object();

				// Token: 0x04004EB3 RID: 20147
				private object receiveLock = new object();

				// Token: 0x04004EB4 RID: 20148
				private IOThreadTimer sendTimer;

				// Token: 0x04004EB5 RID: 20149
				private IOThreadTimer receiveTimer;

				// Token: 0x04004EB6 RID: 20150
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004EB7 RID: 20151
				private EndpointAddress to;

				// Token: 0x04004EB8 RID: 20152
				private Uri via;

				// Token: 0x04004EB9 RID: 20153
				private HttpAbortReason abortReason;

				// Token: 0x04004EBA RID: 20154
				private int webRequestCompleted;

				// Token: 0x04004EBB RID: 20155
				private EventTraceActivity eventTraceActivity;
			}

			// Token: 0x02000F57 RID: 3927
			private class GetWebRequestAsyncResult : AsyncResult
			{
				// Token: 0x06008745 RID: 34629 RVA: 0x001F5D40 File Offset: 0x001F3F40
				public GetWebRequestAsyncResult(HttpChannelFactory<TChannel>.HttpRequestChannel channel, EndpointAddress to, Uri via, SecurityTokenContainer clientCertificateToken, ref TimeoutHelper timeoutHelper, AsyncCallback callback, object state) : base(callback, state)
				{
					this.to = to;
					this.via = via;
					this.clientCertificateToken = clientCertificateToken;
					this.timeoutHelper = timeoutHelper;
					this.factory = channel.Factory;
					this.tokenProvider = channel.tokenProvider;
					this.proxyTokenProvider = channel.proxyTokenProvider;
					if (this.factory.ManualAddressing)
					{
						this.factory.CreateAndOpenTokenProviders(to, via, channel.channelParameters, timeoutHelper.RemainingTime(), out this.tokenProvider, out this.proxyTokenProvider);
					}
					bool flag = false;
					if (this.factory.AuthenticationScheme == AuthenticationSchemes.Anonymous)
					{
						this.SetupWebRequest(AuthenticationLevel.None, TokenImpersonationLevel.None, null);
						flag = true;
					}
					else if (this.factory.AuthenticationScheme == AuthenticationSchemes.Basic)
					{
						if (HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.onGetUserNameCredential == null)
						{
							HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.onGetUserNameCredential = Fx.ThunkCallback(new AsyncCallback(HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.OnGetUserNameCredential));
						}
						IAsyncResult asyncResult = TransportSecurityHelpers.BeginGetUserNameCredential(this.tokenProvider, timeoutHelper.RemainingTime(), HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.onGetUserNameCredential, this);
						if (asyncResult.CompletedSynchronously)
						{
							this.CompleteGetUserNameCredential(asyncResult);
							flag = true;
						}
					}
					else
					{
						if (HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.onGetSspiCredential == null)
						{
							HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.onGetSspiCredential = Fx.ThunkCallback(new AsyncCallback(HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.OnGetSspiCredential));
						}
						IAsyncResult asyncResult = TransportSecurityHelpers.BeginGetSspiCredential(this.tokenProvider, timeoutHelper.RemainingTime(), HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult.onGetSspiCredential, this);
						if (asyncResult.CompletedSynchronously)
						{
							this.CompleteGetSspiCredential(asyncResult);
							flag = true;
						}
					}
					if (flag)
					{
						this.CloseTokenProvidersIfRequired();
						base.Complete(true);
					}
				}

				// Token: 0x06008746 RID: 34630 RVA: 0x001F5EA8 File Offset: 0x001F40A8
				public static HttpWebRequest End(IAsyncResult result)
				{
					HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult getWebRequestAsyncResult = AsyncResult.End<HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult>(result);
					return getWebRequestAsyncResult.request;
				}

				// Token: 0x06008747 RID: 34631 RVA: 0x001F5EC4 File Offset: 0x001F40C4
				private void CompleteGetUserNameCredential(IAsyncResult result)
				{
					NetworkCredential credential = TransportSecurityHelpers.EndGetUserNameCredential(result);
					this.SetupWebRequest(AuthenticationLevel.None, TokenImpersonationLevel.None, credential);
				}

				// Token: 0x06008748 RID: 34632 RVA: 0x001F5EE4 File Offset: 0x001F40E4
				private void CompleteGetSspiCredential(IAsyncResult result)
				{
					TokenImpersonationLevel impersonationLevel;
					AuthenticationLevel authenticationLevel;
					NetworkCredential credential = TransportSecurityHelpers.EndGetSspiCredential(result, out impersonationLevel, out authenticationLevel);
					if (this.factory.AuthenticationScheme == AuthenticationSchemes.Digest)
					{
						HttpChannelUtilities.ValidateDigestCredential(ref credential, impersonationLevel);
					}
					else if (this.factory.AuthenticationScheme == AuthenticationSchemes.Ntlm && authenticationLevel == AuthenticationLevel.MutualAuthRequired)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CredentialDisallowsNtlm")));
					}
					this.SetupWebRequest(authenticationLevel, impersonationLevel, credential);
				}

				// Token: 0x06008749 RID: 34633 RVA: 0x001F5F4C File Offset: 0x001F414C
				private void SetupWebRequest(AuthenticationLevel authenticationLevel, TokenImpersonationLevel impersonationLevel, NetworkCredential credential)
				{
					this.request = this.factory.GetWebRequest(this.to, this.via, credential, impersonationLevel, authenticationLevel, this.proxyTokenProvider, this.clientCertificateToken, this.timeoutHelper.RemainingTime(), false);
				}

				// Token: 0x0600874A RID: 34634 RVA: 0x001F5F91 File Offset: 0x001F4191
				private void CloseTokenProvidersIfRequired()
				{
					if (this.factory.ManualAddressing)
					{
						if (this.tokenProvider != null)
						{
							this.tokenProvider.Abort();
						}
						if (this.proxyTokenProvider != null)
						{
							this.proxyTokenProvider.Abort();
						}
					}
				}

				// Token: 0x0600874B RID: 34635 RVA: 0x001F5FC8 File Offset: 0x001F41C8
				private static void OnGetSspiCredential(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult getWebRequestAsyncResult = (HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						getWebRequestAsyncResult.CompleteGetSspiCredential(result);
						getWebRequestAsyncResult.CloseTokenProvidersIfRequired();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					getWebRequestAsyncResult.Complete(false, exception);
				}

				// Token: 0x0600874C RID: 34636 RVA: 0x001F6024 File Offset: 0x001F4224
				private static void OnGetUserNameCredential(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult getWebRequestAsyncResult = (HttpChannelFactory<TChannel>.HttpRequestChannel.GetWebRequestAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						getWebRequestAsyncResult.CompleteGetUserNameCredential(result);
						getWebRequestAsyncResult.CloseTokenProvidersIfRequired();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					getWebRequestAsyncResult.Complete(false, exception);
				}

				// Token: 0x04004EBC RID: 20156
				private static AsyncCallback onGetSspiCredential;

				// Token: 0x04004EBD RID: 20157
				private static AsyncCallback onGetUserNameCredential;

				// Token: 0x04004EBE RID: 20158
				private SecurityTokenContainer clientCertificateToken;

				// Token: 0x04004EBF RID: 20159
				private HttpChannelFactory<IRequestChannel> factory;

				// Token: 0x04004EC0 RID: 20160
				private SecurityTokenProviderContainer proxyTokenProvider;

				// Token: 0x04004EC1 RID: 20161
				private HttpWebRequest request;

				// Token: 0x04004EC2 RID: 20162
				private EndpointAddress to;

				// Token: 0x04004EC3 RID: 20163
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004EC4 RID: 20164
				private SecurityTokenProviderContainer tokenProvider;

				// Token: 0x04004EC5 RID: 20165
				private Uri via;
			}
		}

		// Token: 0x02000D3F RID: 3391
		private class WebProxyFactory
		{
			// Token: 0x06007C69 RID: 31849 RVA: 0x001D1070 File Offset: 0x001CF270
			public WebProxyFactory(Uri address, bool bypassOnLocal, AuthenticationSchemes authenticationScheme)
			{
				this.address = address;
				this.bypassOnLocal = bypassOnLocal;
				if (!authenticationScheme.IsSingleton())
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("HttpRequiresSingleAuthScheme", new object[]
					{
						authenticationScheme
					}));
				}
				this.authenticationScheme = authenticationScheme;
			}

			// Token: 0x17001BD8 RID: 7128
			// (get) Token: 0x06007C6A RID: 31850 RVA: 0x001D10C9 File Offset: 0x001CF2C9
			internal AuthenticationSchemes AuthenticationScheme
			{
				get
				{
					return this.authenticationScheme;
				}
			}

			// Token: 0x06007C6B RID: 31851 RVA: 0x001D10D4 File Offset: 0x001CF2D4
			public IWebProxy CreateWebProxy(HttpWebRequest request, SecurityTokenProviderContainer tokenProvider, TimeSpan timeout)
			{
				WebProxy webProxy = new WebProxy(this.address, this.bypassOnLocal);
				if (this.authenticationScheme != AuthenticationSchemes.Anonymous)
				{
					TokenImpersonationLevel tokenImpersonationLevel;
					AuthenticationLevel authenticationLevel;
					NetworkCredential credential = HttpChannelUtilities.GetCredential(this.authenticationScheme, tokenProvider, timeout, out tokenImpersonationLevel, out authenticationLevel);
					if (!TokenImpersonationLevelHelper.IsGreaterOrEqual(tokenImpersonationLevel, request.ImpersonationLevel))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProxyImpersonationLevelMismatch", new object[]
						{
							tokenImpersonationLevel,
							request.ImpersonationLevel
						})));
					}
					if (authenticationLevel == AuthenticationLevel.MutualAuthRequired && request.AuthenticationLevel != AuthenticationLevel.MutualAuthRequired)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProxyAuthenticationLevelMismatch", new object[]
						{
							authenticationLevel,
							request.AuthenticationLevel
						})));
					}
					webProxy.Credentials = new CredentialCache
					{
						{
							this.address,
							AuthenticationSchemesHelper.ToString(this.authenticationScheme),
							credential
						}
					};
				}
				return webProxy;
			}

			// Token: 0x04004780 RID: 18304
			private Uri address;

			// Token: 0x04004781 RID: 18305
			private bool bypassOnLocal;

			// Token: 0x04004782 RID: 18306
			private AuthenticationSchemes authenticationScheme;
		}
	}
}
