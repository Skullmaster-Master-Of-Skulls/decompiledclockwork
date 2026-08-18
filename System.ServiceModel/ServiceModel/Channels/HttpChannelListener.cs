using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000868 RID: 2152
	internal abstract class HttpChannelListener : TransportChannelListener, IHttpTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x06005102 RID: 20738 RVA: 0x0012A0DC File Offset: 0x001282DC
		public HttpChannelListener(HttpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context, HttpTransportDefaults.GetDefaultMessageEncoderFactory(), bindingElement.HostNameComparisonMode)
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
			if (bindingElement.AuthenticationScheme.IsSet(AuthenticationSchemes.Basic) && bindingElement.AuthenticationScheme.IsNotSet(AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate | AuthenticationSchemes.Ntlm) && bindingElement.ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.Always)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ExtendedProtectionPolicyBasicAuthNotSupported")));
			}
			this.authenticationScheme = bindingElement.AuthenticationScheme;
			this.keepAliveEnabled = bindingElement.KeepAliveEnabled;
			base.InheritBaseAddressSettings = bindingElement.InheritBaseAddressSettings;
			this.maxBufferSize = bindingElement.MaxBufferSize;
			this.maxPendingAccepts = HttpTransportDefaults.GetEffectiveMaxPendingAccepts(bindingElement.MaxPendingAccepts);
			this.method = bindingElement.Method;
			this.realm = bindingElement.Realm;
			this.requestInitializationTimeout = bindingElement.RequestInitializationTimeout;
			this.transferMode = bindingElement.TransferMode;
			this.unsafeConnectionNtlmAuthentication = bindingElement.UnsafeConnectionNtlmAuthentication;
			this.credentialProvider = context.BindingParameters.Find<SecurityCredentialsManager>();
			this.securityCapabilities = bindingElement.GetProperty<ISecurityCapabilities>(context);
			this.extendedProtectionPolicy = HttpChannelListener.GetPolicyWithDefaultSpnCollection(bindingElement.ExtendedProtectionPolicy, this.authenticationScheme, base.HostNameComparisonModeInternal, base.Uri, out this.usingDefaultSpnList);
			this.webSocketSettings = WebSocketHelper.GetRuntimeWebSocketSettings(bindingElement.WebSocketSettings);
			if (bindingElement.AnonymousUriPrefixMatcher != null)
			{
				this.anonymousUriPrefixMatcher = new HttpAnonymousUriPrefixMatcher(bindingElement.AnonymousUriPrefixMatcher);
			}
			this.httpMessageSettings = (context.BindingParameters.Find<HttpMessageSettings>() ?? new HttpMessageSettings());
			if (this.httpMessageSettings.HttpMessagesSupported && base.MessageVersion != MessageVersion.None)
			{
				throw FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("MessageVersionNoneRequiredForHttpMessageSupport", new object[]
				{
					typeof(HttpRequestMessage).Name,
					typeof(HttpResponseMessage).Name,
					typeof(HttpMessageSettings).Name,
					typeof(MessageVersion).Name,
					typeof(MessageEncodingBindingElement).Name,
					base.MessageVersion.ToString(),
					MessageVersion.None.ToString()
				})));
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x0012A384 File Offset: 0x00128584
		public TimeSpan RequestInitializationTimeout
		{
			get
			{
				return this.requestInitializationTimeout;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x0012A38C File Offset: 0x0012858C
		public WebSocketTransportSettings WebSocketSettings
		{
			get
			{
				return this.webSocketSettings;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x0012A394 File Offset: 0x00128594
		public HttpMessageSettings HttpMessageSettings
		{
			get
			{
				return this.httpMessageSettings;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06005106 RID: 20742 RVA: 0x0012A39C File Offset: 0x0012859C
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x0012A3A4 File Offset: 0x001285A4
		public virtual bool IsChannelBindingSupportEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x06005108 RID: 20744
		public abstract bool UseWebSocketTransport { get; }

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x0012A3A7 File Offset: 0x001285A7
		internal HttpAnonymousUriPrefixMatcher AnonymousUriPrefixMatcher
		{
			get
			{
				return this.anonymousUriPrefixMatcher;
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x0012A3AF File Offset: 0x001285AF
		protected SecurityTokenAuthenticator UserNameTokenAuthenticator
		{
			get
			{
				return this.userNameTokenAuthenticator;
			}
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x0012A3B7 File Offset: 0x001285B7
		internal override void ApplyHostedContext(string virtualPath, bool isMetadataListener)
		{
			base.ApplyHostedContext(virtualPath, isMetadataListener);
			AspNetEnvironment.Current.ValidateHttpSettings(virtualPath, isMetadataListener, this.usingDefaultSpnList, ref this.authenticationScheme, ref this.extendedProtectionPolicy, ref this.realm);
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x0012A3E5 File Offset: 0x001285E5
		public AuthenticationSchemes AuthenticationScheme
		{
			get
			{
				return this.authenticationScheme;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x0600510D RID: 20749 RVA: 0x0012A3ED File Offset: 0x001285ED
		public bool KeepAliveEnabled
		{
			get
			{
				return this.keepAliveEnabled;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x0012A3F5 File Offset: 0x001285F5
		public bool ExtractGroupsForWindowsAccounts
		{
			get
			{
				return this.extractGroupsForWindowsAccounts;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x0600510F RID: 20751 RVA: 0x0012A3FD File Offset: 0x001285FD
		public HostNameComparisonMode HostNameComparisonMode
		{
			get
			{
				return base.HostNameComparisonModeInternal;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06005110 RID: 20752 RVA: 0x0012A405 File Offset: 0x00128605
		protected bool IsAuthenticationSupported
		{
			get
			{
				return this.authenticationScheme != AuthenticationSchemes.Anonymous;
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x0012A417 File Offset: 0x00128617
		private bool IsAuthenticationRequired
		{
			get
			{
				return this.AuthenticationScheme.IsNotSet(AuthenticationSchemes.Anonymous);
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x0012A429 File Offset: 0x00128629
		public int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x0012A431 File Offset: 0x00128631
		public int MaxPendingAccepts
		{
			get
			{
				return this.maxPendingAccepts;
			}
		}

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x0012A439 File Offset: 0x00128639
		public virtual string Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x0012A441 File Offset: 0x00128641
		public TransferMode TransferMode
		{
			get
			{
				return this.transferMode;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x0012A449 File Offset: 0x00128649
		public string Realm
		{
			get
			{
				return this.realm;
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x0012A451 File Offset: 0x00128651
		int IHttpTransportFactorySettings.MaxBufferSize
		{
			get
			{
				return this.MaxBufferSize;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x0012A459 File Offset: 0x00128659
		TransferMode IHttpTransportFactorySettings.TransferMode
		{
			get
			{
				return this.TransferMode;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x0012A461 File Offset: 0x00128661
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeHttp;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x0600511A RID: 20762 RVA: 0x0012A468 File Offset: 0x00128668
		internal static UriPrefixTable<ITransportManagerRegistration> StaticTransportManagerTable
		{
			get
			{
				return HttpChannelListener.transportManagerTable;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x0012A46F File Offset: 0x0012866F
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return this.unsafeConnectionNtlmAuthentication;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x0012A477 File Offset: 0x00128677
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return HttpChannelListener.transportManagerTable;
			}
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x0012A47E File Offset: 0x0012867E
		internal override ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri)
		{
			return new SharedHttpTransportManager(listenUri, this);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x0012A488 File Offset: 0x00128688
		private string GetAuthType(HttpListenerContext listenerContext)
		{
			string result = null;
			IPrincipal user = listenerContext.User;
			if (user != null && user.Identity != null)
			{
				result = user.Identity.AuthenticationType;
			}
			return result;
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x0012A4B8 File Offset: 0x001286B8
		protected string GetAuthType(HttpChannelListener.IHttpAuthenticationContext authenticationContext)
		{
			string result = null;
			if (authenticationContext.LogonUserIdentity != null)
			{
				result = authenticationContext.LogonUserIdentity.AuthenticationType;
			}
			return result;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0012A4DC File Offset: 0x001286DC
		private bool IsAuthSchemeValid(string authType)
		{
			return AuthenticationSchemesHelper.DoesAuthTypeMatch(this.authenticationScheme, authType);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0012A4EA File Offset: 0x001286EA
		internal override int GetMaxBufferSize()
		{
			return this.MaxBufferSize;
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x0012A4F4 File Offset: 0x001286F4
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(EndpointIdentity))
			{
				return (T)((object)this.identity);
			}
			if (typeof(T) == typeof(ILogonTokenCacheManager))
			{
				object obj = this.GetIdentityModelProperty<T>();
				if (obj != null)
				{
					return (T)((object)obj);
				}
			}
			else
			{
				if (typeof(T) == typeof(ISecurityCapabilities))
				{
					return (T)((object)this.securityCapabilities);
				}
				if (typeof(T) == typeof(ExtendedProtectionPolicy))
				{
					return (T)((object)this.extendedProtectionPolicy);
				}
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x0012A5B0 File Offset: 0x001287B0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private T GetIdentityModelProperty<T>()
		{
			if (typeof(T) == typeof(EndpointIdentity))
			{
				if (this.identity == null && (this.authenticationScheme.IsSet(AuthenticationSchemes.Negotiate) || this.authenticationScheme.IsSet(AuthenticationSchemes.Ntlm)))
				{
					this.identity = SecurityUtils.CreateWindowsIdentity();
				}
				return (T)((object)this.identity);
			}
			if (typeof(T) == typeof(ILogonTokenCacheManager) && this.userNameTokenAuthenticator != null)
			{
				ILogonTokenCacheManager logonTokenCacheManager = this.userNameTokenAuthenticator as ILogonTokenCacheManager;
				if (logonTokenCacheManager != null)
				{
					return (T)((object)logonTokenCacheManager);
				}
			}
			return default(T);
		}

		// Token: 0x06005124 RID: 20772
		internal abstract IAsyncResult BeginHttpContextReceived(HttpRequestContext context, Action acceptorCallback, AsyncCallback callback, object state);

		// Token: 0x06005125 RID: 20773
		internal abstract bool EndHttpContextReceived(IAsyncResult result);

		// Token: 0x06005126 RID: 20774 RVA: 0x0012A658 File Offset: 0x00128858
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeSecurityTokenAuthenticator()
		{
			ServiceCredentials serviceCredentials = this.credentialProvider as ServiceCredentials;
			if (serviceCredentials != null)
			{
				if (this.AuthenticationScheme == AuthenticationSchemes.Basic)
				{
					this.extractGroupsForWindowsAccounts = serviceCredentials.UserNameAuthentication.IncludeWindowsGroups;
				}
				else
				{
					if (this.AuthenticationScheme.IsSet(AuthenticationSchemes.Basic) && serviceCredentials.UserNameAuthentication.IncludeWindowsGroups != serviceCredentials.WindowsAuthentication.IncludeWindowsGroups)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenProviderIncludeWindowsGroupsInconsistent", new object[]
						{
							this.authenticationScheme - AuthenticationSchemes.Basic,
							serviceCredentials.UserNameAuthentication.IncludeWindowsGroups,
							serviceCredentials.WindowsAuthentication.IncludeWindowsGroups
						})));
					}
					this.extractGroupsForWindowsAccounts = serviceCredentials.WindowsAuthentication.IncludeWindowsGroups;
				}
				if (serviceCredentials.UserNameAuthentication.UserNamePasswordValidationMode == UserNamePasswordValidationMode.Custom)
				{
					this.userNameTokenAuthenticator = new CustomUserNameSecurityTokenAuthenticator(serviceCredentials.UserNameAuthentication.GetUserNamePasswordValidator());
				}
				else if (serviceCredentials.UserNameAuthentication.CacheLogonTokens)
				{
					this.userNameTokenAuthenticator = new WindowsUserNameCachingSecurityTokenAuthenticator(this.extractGroupsForWindowsAccounts, serviceCredentials.UserNameAuthentication.MaxCachedLogonTokens, serviceCredentials.UserNameAuthentication.CachedLogonTokenLifetime);
				}
				else
				{
					this.userNameTokenAuthenticator = new WindowsUserNameSecurityTokenAuthenticator(this.extractGroupsForWindowsAccounts);
				}
			}
			else
			{
				this.extractGroupsForWindowsAccounts = true;
				this.userNameTokenAuthenticator = new WindowsUserNameSecurityTokenAuthenticator(this.extractGroupsForWindowsAccounts);
			}
			this.windowsTokenAuthenticator = new WindowsSecurityTokenAuthenticator(this.extractGroupsForWindowsAccounts);
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x0012A7BB File Offset: 0x001289BB
		protected override void OnOpened()
		{
			base.OnOpened();
			if (this.IsAuthenticationSupported)
			{
				this.InitializeSecurityTokenAuthenticator();
				this.identity = this.GetIdentityModelProperty<EndpointIdentity>();
			}
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x0012A7DD File Offset: 0x001289DD
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void CloseUserNameTokenAuthenticator(TimeSpan timeout)
		{
			SecurityUtils.CloseTokenAuthenticatorIfRequired(this.userNameTokenAuthenticator, timeout);
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x0012A7EB File Offset: 0x001289EB
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void AbortUserNameTokenAuthenticator()
		{
			SecurityUtils.AbortTokenAuthenticatorIfRequired(this.userNameTokenAuthenticator);
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x0012A7F8 File Offset: 0x001289F8
		private bool ShouldProcessAuthentication(HttpChannelListener.IHttpAuthenticationContext authenticationContext)
		{
			return this.IsAuthenticationRequired || (this.IsAuthenticationSupported && authenticationContext.LogonUserIdentity.IsAuthenticated);
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x0012A819 File Offset: 0x00128A19
		private bool ShouldProcessAuthentication(HttpListenerContext listenerContext)
		{
			return this.IsAuthenticationRequired || (this.IsAuthenticationSupported && listenerContext.Request.IsAuthenticated);
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x0012A83C File Offset: 0x00128A3C
		public virtual SecurityMessageProperty ProcessAuthentication(HttpChannelListener.IHttpAuthenticationContext authenticationContext)
		{
			if (this.ShouldProcessAuthentication(authenticationContext))
			{
				SecurityMessageProperty result;
				try
				{
					result = this.ProcessAuthentication(authenticationContext.LogonUserIdentity, this.GetAuthType(authenticationContext));
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
					{
						this.WriteAuditEvent(AuditLevel.Failure, (authenticationContext.LogonUserIdentity != null) ? authenticationContext.LogonUserIdentity.Name : string.Empty, exception);
					}
					throw;
				}
				if (AuditLevel.Success == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Success))
				{
					this.WriteAuditEvent(AuditLevel.Success, (authenticationContext.LogonUserIdentity != null) ? authenticationContext.LogonUserIdentity.Name : string.Empty, null);
				}
				return result;
			}
			return null;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x0012A8F0 File Offset: 0x00128AF0
		public virtual SecurityMessageProperty ProcessAuthentication(HttpListenerContext listenerContext)
		{
			if (this.ShouldProcessAuthentication(listenerContext))
			{
				return this.ProcessRequiredAuthentication(listenerContext);
			}
			return null;
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x0012A904 File Offset: 0x00128B04
		private SecurityMessageProperty ProcessRequiredAuthentication(HttpListenerContext listenerContext)
		{
			HttpListenerBasicIdentity httpListenerBasicIdentity = null;
			WindowsIdentity windowsIdentity = null;
			SecurityMessageProperty result;
			try
			{
				windowsIdentity = (listenerContext.User.Identity as WindowsIdentity);
				if (this.AuthenticationScheme.IsSet(AuthenticationSchemes.Basic) && windowsIdentity == null)
				{
					httpListenerBasicIdentity = (listenerContext.User.Identity as HttpListenerBasicIdentity);
					result = this.ProcessAuthentication(httpListenerBasicIdentity);
				}
				else
				{
					result = this.ProcessAuthentication(windowsIdentity, this.GetAuthType(listenerContext));
				}
			}
			catch (Exception exception)
			{
				if (!Fx.IsFatal(exception) && AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
				{
					this.WriteAuditEvent(AuditLevel.Failure, (httpListenerBasicIdentity != null) ? httpListenerBasicIdentity.Name : ((windowsIdentity != null) ? windowsIdentity.Name : string.Empty), exception);
				}
				throw;
			}
			if (AuditLevel.Success == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Success))
			{
				this.WriteAuditEvent(AuditLevel.Success, (httpListenerBasicIdentity != null) ? httpListenerBasicIdentity.Name : ((windowsIdentity != null) ? windowsIdentity.Name : string.Empty), null);
			}
			return result;
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x0012A9E8 File Offset: 0x00128BE8
		protected override bool TryGetTransportManagerRegistration(HostNameComparisonMode hostNameComparisonMode, out ITransportManagerRegistration registration)
		{
			if (this.TransportManagerTable.TryLookupUri(this.Uri, hostNameComparisonMode, out registration))
			{
				HttpTransportManager httpTransportManager = registration as HttpTransportManager;
				if (httpTransportManager != null && httpTransportManager.IsHosted)
				{
					return true;
				}
				if (registration.ListenUri.Segments.Length >= base.BaseUri.Segments.Length)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x0012AA40 File Offset: 0x00128C40
		protected void WriteAuditEvent(AuditLevel auditLevel, string primaryIdentity, Exception exception)
		{
			try
			{
				if (auditLevel == AuditLevel.Success)
				{
					SecurityAuditHelper.WriteTransportAuthenticationSuccessEvent(base.AuditBehavior.AuditLogLocation, base.AuditBehavior.SuppressAuditFailure, null, this.Uri, primaryIdentity);
				}
				else
				{
					SecurityAuditHelper.WriteTransportAuthenticationFailureEvent(base.AuditBehavior.AuditLogLocation, base.AuditBehavior.SuppressAuditFailure, null, this.Uri, primaryIdentity, exception);
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2) || auditLevel == AuditLevel.Success)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
			}
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x0012AAC4 File Offset: 0x00128CC4
		private SecurityMessageProperty ProcessAuthentication(HttpListenerBasicIdentity identity)
		{
			SecurityToken token = new UserNameSecurityToken(identity.Name, identity.Password);
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.userNameTokenAuthenticator.ValidateToken(token);
			return new SecurityMessageProperty
			{
				TransportToken = new SecurityTokenSpecification(token, readOnlyCollection),
				ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection)
			};
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x0012AB10 File Offset: 0x00128D10
		private SecurityMessageProperty ProcessAuthentication(WindowsIdentity identity, string authenticationType)
		{
			SecurityUtils.ValidateAnonymityConstraint(identity, false);
			SecurityToken token = new WindowsSecurityToken(identity, SecurityUniqueId.Create().Value, authenticationType);
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.windowsTokenAuthenticator.ValidateToken(token);
			return new SecurityMessageProperty
			{
				TransportToken = new SecurityTokenSpecification(token, readOnlyCollection),
				ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection)
			};
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x0012AB68 File Offset: 0x00128D68
		private HttpStatusCode ValidateAuthentication(string authType)
		{
			if (this.IsAuthSchemeValid(authType))
			{
				return HttpStatusCode.OK;
			}
			if (AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
			{
				string @string = SR.GetString("HttpAuthenticationFailed", new object[]
				{
					this.AuthenticationScheme,
					HttpStatusCode.Unauthorized
				});
				Exception exception = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(@string));
				this.WriteAuditEvent(AuditLevel.Failure, string.Empty, exception);
			}
			return HttpStatusCode.Unauthorized;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0012ABE4 File Offset: 0x00128DE4
		public virtual HttpStatusCode ValidateAuthentication(HttpChannelListener.IHttpAuthenticationContext authenticationContext)
		{
			HttpStatusCode httpStatusCode = HttpStatusCode.OK;
			if (this.IsAuthenticationSupported)
			{
				string authType = this.GetAuthType(authenticationContext);
				httpStatusCode = this.ValidateAuthentication(authType);
			}
			if (httpStatusCode == HttpStatusCode.OK && authenticationContext.LogonUserIdentity != null && authenticationContext.LogonUserIdentity.IsAuthenticated && this.ExtendedProtectionPolicy.PolicyEnforcement == PolicyEnforcement.Always && !authenticationContext.IISSupportsExtendedProtection)
			{
				Exception exception = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PlatformNotSupportedException(SR.GetString("ExtendedProtectionNotSupported")));
				this.WriteAuditEvent(AuditLevel.Failure, string.Empty, exception);
				httpStatusCode = HttpStatusCode.Unauthorized;
			}
			return httpStatusCode;
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0012AC70 File Offset: 0x00128E70
		public virtual HttpStatusCode ValidateAuthentication(HttpListenerContext listenerContext)
		{
			HttpStatusCode result = HttpStatusCode.OK;
			if (this.IsAuthenticationSupported)
			{
				string authType = this.GetAuthType(listenerContext);
				result = this.ValidateAuthentication(authType);
			}
			return result;
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x0012AC9C File Offset: 0x00128E9C
		private static ExtendedProtectionPolicy GetPolicyWithDefaultSpnCollection(ExtendedProtectionPolicy policy, AuthenticationSchemes authenticationScheme, HostNameComparisonMode hostNameComparisonMode, Uri listenUri, out bool usingDefaultSpnList)
		{
			if (policy.PolicyEnforcement != PolicyEnforcement.Never && policy.CustomServiceNames == null && policy.CustomChannelBinding == null && authenticationScheme != AuthenticationSchemes.Anonymous && string.Equals(listenUri.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase))
			{
				usingDefaultSpnList = true;
				return new ExtendedProtectionPolicy(policy.PolicyEnforcement, policy.ProtectionScenario, HttpChannelListener.GetDefaultSpnList(hostNameComparisonMode, listenUri));
			}
			usingDefaultSpnList = false;
			return policy;
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x0012AD00 File Offset: 0x00128F00
		private static ServiceNameCollection GetDefaultSpnList(HostNameComparisonMode hostNameComparisonMode, Uri listenUri)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string dnsSafeHost = listenUri.DnsSafeHost;
			switch (hostNameComparisonMode)
			{
			case HostNameComparisonMode.StrongWildcard:
			case HostNameComparisonMode.WeakWildcard:
			{
				string hostName = Dns.GetHostEntry(string.Empty).HostName;
				HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HOST/{0}", new object[]
				{
					hostName
				}));
				HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HTTP/{0}", new object[]
				{
					hostName
				}));
				break;
			}
			case HostNameComparisonMode.Exact:
			{
				UriHostNameType hostNameType = listenUri.HostNameType;
				if (hostNameType == UriHostNameType.IPv4 || hostNameType == UriHostNameType.IPv6)
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HOST/{0}", new object[]
					{
						hostName
					}));
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HTTP/{0}", new object[]
					{
						hostName
					}));
				}
				else if (listenUri.DnsSafeHost.Contains("."))
				{
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HOST/{0}", new object[]
					{
						dnsSafeHost
					}));
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HTTP/{0}", new object[]
					{
						dnsSafeHost
					}));
				}
				else
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HOST/{0}", new object[]
					{
						dnsSafeHost
					}));
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HTTP/{0}", new object[]
					{
						dnsSafeHost
					}));
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HOST/{0}", new object[]
					{
						hostName
					}));
					HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HTTP/{0}", new object[]
					{
						hostName
					}));
				}
				break;
			}
			}
			HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HOST/{0}", new object[]
			{
				"localhost"
			}));
			HttpChannelListener.AddSpn(dictionary, string.Format(CultureInfo.InvariantCulture, "HTTP/{0}", new object[]
			{
				"localhost"
			}));
			return new ServiceNameCollection(dictionary.Values);
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x0012AF18 File Offset: 0x00129118
		private static void AddSpn(Dictionary<string, string> list, string value)
		{
			string key = value.ToLowerInvariant();
			if (!list.ContainsKey(key))
			{
				list.Add(key, value);
			}
		}

		// Token: 0x06005139 RID: 20793
		public abstract bool CreateWebSocketChannelAndEnqueue(HttpRequestContext httpRequestContext, HttpPipeline httpPipeline, HttpResponseMessage httpResponseMessage, string subProtocol, Action dequeuedCallback);

		// Token: 0x0600513A RID: 20794
		public abstract byte[] TakeWebSocketInternalBuffer();

		// Token: 0x0600513B RID: 20795
		public abstract void ReturnWebSocketInternalBuffer(byte[] buffer);

		// Token: 0x040031F1 RID: 12785
		private AuthenticationSchemes authenticationScheme;

		// Token: 0x040031F2 RID: 12786
		private bool extractGroupsForWindowsAccounts;

		// Token: 0x040031F3 RID: 12787
		private EndpointIdentity identity;

		// Token: 0x040031F4 RID: 12788
		private bool keepAliveEnabled;

		// Token: 0x040031F5 RID: 12789
		private int maxBufferSize;

		// Token: 0x040031F6 RID: 12790
		private readonly int maxPendingAccepts;

		// Token: 0x040031F7 RID: 12791
		private string method;

		// Token: 0x040031F8 RID: 12792
		private string realm;

		// Token: 0x040031F9 RID: 12793
		private readonly TimeSpan requestInitializationTimeout;

		// Token: 0x040031FA RID: 12794
		private TransferMode transferMode;

		// Token: 0x040031FB RID: 12795
		private bool unsafeConnectionNtlmAuthentication;

		// Token: 0x040031FC RID: 12796
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x040031FD RID: 12797
		private SecurityCredentialsManager credentialProvider;

		// Token: 0x040031FE RID: 12798
		private SecurityTokenAuthenticator userNameTokenAuthenticator;

		// Token: 0x040031FF RID: 12799
		private SecurityTokenAuthenticator windowsTokenAuthenticator;

		// Token: 0x04003200 RID: 12800
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x04003201 RID: 12801
		private bool usingDefaultSpnList;

		// Token: 0x04003202 RID: 12802
		private HttpAnonymousUriPrefixMatcher anonymousUriPrefixMatcher;

		// Token: 0x04003203 RID: 12803
		private HttpMessageSettings httpMessageSettings;

		// Token: 0x04003204 RID: 12804
		private WebSocketTransportSettings webSocketSettings;

		// Token: 0x04003205 RID: 12805
		private static UriPrefixTable<ITransportManagerRegistration> transportManagerTable = new UriPrefixTable<ITransportManagerRegistration>(true);

		// Token: 0x02000D49 RID: 3401
		internal interface IHttpAuthenticationContext
		{
			// Token: 0x17001BE4 RID: 7140
			// (get) Token: 0x06007CB9 RID: 31929
			WindowsIdentity LogonUserIdentity { get; }

			// Token: 0x06007CBA RID: 31930
			X509Certificate2 GetClientCertificate(out bool isValidCertificate);

			// Token: 0x17001BE5 RID: 7141
			// (get) Token: 0x06007CBB RID: 31931
			bool IISSupportsExtendedProtection { get; }

			// Token: 0x06007CBC RID: 31932
			TraceRecord CreateTraceRecord();
		}
	}
}
