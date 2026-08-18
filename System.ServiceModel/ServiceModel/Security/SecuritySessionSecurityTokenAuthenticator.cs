using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200031E RID: 798
	internal class SecuritySessionSecurityTokenAuthenticator : CommunicationObjectSecurityTokenAuthenticator, IIssuanceSecurityTokenAuthenticator, ILogonTokenCacheManager
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x0006775C File Offset: 0x0006595C
		public SecuritySessionSecurityTokenAuthenticator()
		{
			this.sessionTokenAuthenticator = new SecurityContextSecurityTokenAuthenticator();
			this.sessionTokenLifetime = SecuritySessionSecurityTokenAuthenticator.defaultSessionTokenLifetime;
			this.isClientAnonymous = false;
			this.standardsManager = SecuritySessionSecurityTokenAuthenticator.defaultStandardsManager;
			this.keyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
			this.maximumConcurrentNegotiations = 128;
			this.negotiationTimeout = NegotiationTokenAuthenticator<NegotiationTokenAuthenticatorState>.defaultServerMaxNegotiationLifetime;
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000677BF File Offset: 0x000659BF
		// (set) Token: 0x06001B9F RID: 7071 RVA: 0x000677C7 File Offset: 0x000659C7
		public IssuedSecurityTokenHandler IssuedSecurityTokenHandler
		{
			get
			{
				return this.issuedSecurityTokenHandler;
			}
			set
			{
				this.issuedSecurityTokenHandler = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x000677D0 File Offset: 0x000659D0
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x000677D8 File Offset: 0x000659D8
		public RenewedSecurityTokenHandler RenewedSecurityTokenHandler
		{
			get
			{
				return this.renewedSecurityTokenHandler;
			}
			set
			{
				this.renewedSecurityTokenHandler = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000677E1 File Offset: 0x000659E1
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x000677E9 File Offset: 0x000659E9
		public SecurityAlgorithmSuite SecurityAlgorithmSuite
		{
			get
			{
				return this.securityAlgorithmSuite;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.securityAlgorithmSuite = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x000677FD File Offset: 0x000659FD
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x00067805 File Offset: 0x00065A05
		public SecurityKeyEntropyMode KeyEntropyMode
		{
			get
			{
				return this.keyEntropyMode;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				SecurityKeyEntropyModeHelper.Validate(value);
				this.keyEntropyMode = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0006781F File Offset: 0x00065A1F
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x00067827 File Offset: 0x00065A27
		public bool IsClientAnonymous
		{
			get
			{
				return this.isClientAnonymous;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.isClientAnonymous = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0006783B File Offset: 0x00065A3B
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x00067844 File Offset: 0x00065A44
		public TimeSpan SessionTokenLifetime
		{
			get
			{
				return this.sessionTokenLifetime;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.sessionTokenLifetime = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000678BC File Offset: 0x00065ABC
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x000678C4 File Offset: 0x00065AC4
		public TimeSpan KeyRenewalInterval
		{
			get
			{
				return this.keyRenewalInterval;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.keyRenewalInterval = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0006793C File Offset: 0x00065B3C
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x00067944 File Offset: 0x00065B44
		public int MaximumConcurrentNegotiations
		{
			get
			{
				return this.maximumConcurrentNegotiations;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeNonNegative")));
				}
				this.maximumConcurrentNegotiations = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0006797B File Offset: 0x00065B7B
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00067983 File Offset: 0x00065B83
		public TimeSpan NegotiationTimeout
		{
			get
			{
				return this.negotiationTimeout;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.negotiationTimeout = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000679B9 File Offset: 0x00065BB9
		public SecurityContextSecurityTokenAuthenticator SessionTokenAuthenticator
		{
			get
			{
				return this.sessionTokenAuthenticator;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x000679C1 File Offset: 0x00065BC1
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x000679C9 File Offset: 0x00065BC9
		public ISecurityContextSecurityTokenCache IssuedTokenCache
		{
			get
			{
				return this.issuedTokenCache;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.issuedTokenCache = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x000679DD File Offset: 0x00065BDD
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x000679E8 File Offset: 0x00065BE8
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				if (!value.TrustDriver.IsSessionSupported)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("TrustDriverVersionDoesNotSupportSession"), "value"));
				}
				if (!value.SecureConversationDriver.IsSessionSupported)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SecureConversationDriverVersionDoesNotSupportSession"), "value"));
				}
				this.standardsManager = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x00067A77 File Offset: 0x00065C77
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x00067A7F File Offset: 0x00065C7F
		public SecurityTokenParameters IssuedSecurityTokenParameters
		{
			get
			{
				return this.issuedTokenParameters;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.issuedTokenParameters = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00067A93 File Offset: 0x00065C93
		// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x00067A9B File Offset: 0x00065C9B
		public BindingContext IssuerBindingContext
		{
			get
			{
				return this.issuerBindingContext;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuerBindingContext = value.Clone();
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x00067AC7 File Offset: 0x00065CC7
		// (set) Token: 0x06001BBA RID: 7098 RVA: 0x00067ACF File Offset: 0x00065CCF
		public SecurityBindingElement BootstrapSecurityBindingElement
		{
			get
			{
				return this.bootstrapSecurityBindingElement;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.bootstrapSecurityBindingElement = (SecurityBindingElement)value.Clone();
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x00067B00 File Offset: 0x00065D00
		// (set) Token: 0x06001BBC RID: 7100 RVA: 0x00067B08 File Offset: 0x00065D08
		public IMessageFilterTable<EndpointAddress> EndpointFilterTable
		{
			get
			{
				return this.endpointFilterTable;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.endpointFilterTable = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x00067B1C File Offset: 0x00065D1C
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x00067B24 File Offset: 0x00065D24
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.listenUri = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00067B38 File Offset: 0x00065D38
		public virtual XmlDictionaryString IssueAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.IssueAction;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x00067B4A File Offset: 0x00065D4A
		public virtual XmlDictionaryString IssueResponseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.IssueResponseAction;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00067B5C File Offset: 0x00065D5C
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x00067B64 File Offset: 0x00065D64
		public bool PreserveBootstrapTokens
		{
			get
			{
				return this.preserveBootstrapTokens;
			}
			set
			{
				this.preserveBootstrapTokens = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00067B6D File Offset: 0x00065D6D
		public virtual XmlDictionaryString RenewAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.RenewAction;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x00067B7F File Offset: 0x00065D7F
		public virtual XmlDictionaryString RenewResponseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.RenewResponseAction;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00067B91 File Offset: 0x00065D91
		public virtual XmlDictionaryString CloseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.CloseAction;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00067BA3 File Offset: 0x00065DA3
		public virtual XmlDictionaryString CloseResponseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.CloseResponseAction;
			}
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00067BB8 File Offset: 0x00065DB8
		public bool RemoveCachedLogonToken(string username)
		{
			if (this.RequestSecurityTokenListener != null)
			{
				for (int i = 0; i < this.RequestSecurityTokenListener.ChannelDispatchers.Count; i++)
				{
					IChannelListener listener = this.RequestSecurityTokenListener.ChannelDispatchers[i].Listener;
					if (listener != null)
					{
						ILogonTokenCacheManager property = listener.GetProperty<ILogonTokenCacheManager>();
						if (property != null)
						{
							return property.RemoveCachedLogonToken(username);
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00067C1C File Offset: 0x00065E1C
		public void FlushLogonTokenCache()
		{
			if (this.RequestSecurityTokenListener != null && this.RequestSecurityTokenListener.ChannelDispatchers.Count > 0)
			{
				for (int i = 0; i < this.RequestSecurityTokenListener.ChannelDispatchers.Count; i++)
				{
					IChannelListener listener = this.RequestSecurityTokenListener.ChannelDispatchers[i].Listener;
					if (listener != null)
					{
						ILogonTokenCacheManager property = listener.GetProperty<ILogonTokenCacheManager>();
						if (property != null)
						{
							property.FlushLogonTokenCache();
						}
					}
				}
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00067C8D File Offset: 0x00065E8D
		private Message HandleOperationException(SecuritySessionOperation operation, Message request, Exception e)
		{
			SecurityTraceRecordHelper.TraceServerSessionOperationException(operation, e, this.ListenUri);
			return this.CreateFault(request, e);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00067CA4 File Offset: 0x00065EA4
		private Message CreateFault(Message request, Exception e)
		{
			FaultCode subCode;
			FaultReason reason;
			bool flag;
			if (e is QuotaExceededException)
			{
				subCode = new FaultCode("ServerTooBusy", "http://schemas.microsoft.com/ws/2006/05/security");
				reason = new FaultReason(SR.GetString("PendingSessionsExceededFaultReason"), CultureInfo.CurrentCulture);
				flag = false;
			}
			else if (e is EndpointNotFoundException)
			{
				subCode = new FaultCode("EndpointUnavailable", request.Version.Addressing.Namespace);
				reason = new FaultReason(SR.GetString("SecurityListenerClosingFaultReason"), CultureInfo.CurrentCulture);
				flag = false;
			}
			else
			{
				subCode = new FaultCode("InvalidRequest", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				reason = new FaultReason(SR.GetString("InvalidRequestTrustFaultCode"), CultureInfo.CurrentCulture);
				flag = true;
			}
			FaultCode code;
			if (flag)
			{
				code = FaultCode.CreateSenderFaultCode(subCode);
			}
			else
			{
				code = FaultCode.CreateReceiverFaultCode(subCode);
			}
			MessageFault fault = MessageFault.CreateFault(code, reason);
			Message message = Message.CreateMessage(request.Version, fault, request.Version.Addressing.DefaultFaultAction);
			message.Headers.RelatesTo = request.Headers.MessageId;
			return message;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00067D9C File Offset: 0x00065F9C
		private void NotifyOperationCompletion(SecuritySessionOperation operation, SecurityContextSecurityToken newSessionToken, SecurityContextSecurityToken previousSessionToken, EndpointAddress remoteAddress)
		{
			if (operation == SecuritySessionOperation.Issue)
			{
				if (this.issuedSecurityTokenHandler != null)
				{
					this.issuedSecurityTokenHandler(newSessionToken, remoteAddress);
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException("IssueSessionTokenHandlerNotSet"));
			}
			else
			{
				if (operation != SecuritySessionOperation.Renew)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				if (this.renewedSecurityTokenHandler != null)
				{
					this.renewedSecurityTokenHandler(newSessionToken, previousSessionToken);
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException("RenewSessionTokenHandlerNotSet"));
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00067E17 File Offset: 0x00066017
		public override void OnAbort()
		{
			if (this.rstListener != null)
			{
				this.rstListener.Abort();
				this.rstListener = null;
			}
			base.OnAbort();
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00067E3C File Offset: 0x0006603C
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.rstListener != null)
			{
				this.rstListener.Close(timeoutHelper.RemainingTime());
				this.rstListener = null;
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00067E80 File Offset: 0x00066080
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.BootstrapSecurityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BootstrapSecurityBindingElementNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.IssuerBindingContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuerBuildContextNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.IssuedSecurityTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuedSecurityTokenParametersNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.SecurityAlgorithmSuite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityAlgorithmSuiteNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.IssuedTokenCache == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuedTokenCacheNotSet", new object[]
				{
					base.GetType()
				})));
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.SetupSessionListener();
			this.rstListener.Open(timeoutHelper.RemainingTime());
			this.sctUri = this.StandardsManager.SecureConversationDriver.TokenTypeUri;
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00067FC5 File Offset: 0x000661C5
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is SecurityContextSecurityToken;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00067FD0 File Offset: 0x000661D0
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			SecurityContextSecurityToken securityContextSecurityToken = (SecurityContextSecurityToken)token;
			return securityContextSecurityToken.AuthorizationPolicies;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00067FEC File Offset: 0x000661EC
		private static bool IsSameIdentity(ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, ServiceSecurityContext incomingContext)
		{
			Claim primaryIdentityClaim = SecurityUtils.GetPrimaryIdentityClaim(authorizationPolicies);
			if (primaryIdentityClaim == null)
			{
				return incomingContext.IsAnonymous;
			}
			return Claim.DefaultComparer.Equals(incomingContext.IdentityClaim, primaryIdentityClaim);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0006801C File Offset: 0x0006621C
		private DateTime GetKeyExpirationTime(SecurityToken currentToken, DateTime keyEffectiveTime)
		{
			DateTime dateTime = TimeoutHelper.Add(keyEffectiveTime, this.keyRenewalInterval);
			DateTime dateTime2 = (currentToken != null) ? currentToken.ValidTo : TimeoutHelper.Add(keyEffectiveTime, this.sessionTokenLifetime);
			if (dateTime > dateTime2)
			{
				dateTime = dateTime2;
			}
			return dateTime;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0006805A File Offset: 0x0006625A
		internal static ReadOnlyCollection<IAuthorizationPolicy> CreateSecureConversationPolicies(SecurityMessageProperty security, DateTime expirationTime)
		{
			return SecuritySessionSecurityTokenAuthenticator.CreateSecureConversationPolicies(security, null, expirationTime);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00068064 File Offset: 0x00066264
		private static ReadOnlyCollection<IAuthorizationPolicy> CreateSecureConversationPolicies(SecurityMessageProperty security, ReadOnlyCollection<IAuthorizationPolicy> currentTokenPolicies, DateTime expirationTime)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			List<IAuthorizationPolicy> list = new List<IAuthorizationPolicy>();
			if (security.ServiceSecurityContext != null && security.ServiceSecurityContext.AuthorizationPolicies != null)
			{
				list.AddRange(security.ServiceSecurityContext.AuthorizationPolicies);
				if (security.TransportToken != null && security.TransportToken.SecurityTokenPolicies != null && security.TransportToken.SecurityTokenPolicies.Count > 0)
				{
					foreach (IAuthorizationPolicy item in security.TransportToken.SecurityTokenPolicies)
					{
						if (list.Contains(item))
						{
							list.Remove(item);
						}
					}
				}
				if (currentTokenPolicies != null)
				{
					for (int i = 0; i < currentTokenPolicies.Count; i++)
					{
						if (list.Contains(currentTokenPolicies[i]))
						{
							list.Remove(currentTokenPolicies[i]);
						}
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].GetType() == typeof(UnconditionalPolicy))
					{
						UnconditionalPolicy unconditionalPolicy = (UnconditionalPolicy)list[j];
						UnconditionalPolicy value = new UnconditionalPolicy(unconditionalPolicy.PrimaryIdentity, unconditionalPolicy.Issuances, expirationTime);
						list[j] = value;
					}
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000681CC File Offset: 0x000663CC
		private SecurityContextSecurityToken IssueToken(RequestSecurityToken rst, Message request, SecurityContextSecurityToken currentToken, ReadOnlyCollection<IAuthorizationPolicy> currentTokenPolicies, out RequestSecurityTokenResponse rstr)
		{
			if (rst.TokenType != null && rst.TokenType != this.sctUri)
			{
				throw TraceUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CannotIssueRstTokenType", new object[]
				{
					rst.TokenType
				})), request);
			}
			SecurityMessageProperty security = request.Properties.Security;
			ServiceSecurityContext serviceSecurityContext;
			if (security != null)
			{
				serviceSecurityContext = security.ServiceSecurityContext;
			}
			else
			{
				serviceSecurityContext = ServiceSecurityContext.Anonymous;
			}
			if (serviceSecurityContext == null)
			{
				throw TraceUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("SecurityContextMissing", new object[]
				{
					request.Headers.Action
				})), request);
			}
			if (currentToken != null && !SecuritySessionSecurityTokenAuthenticator.IsSameIdentity(currentToken.AuthorizationPolicies, serviceSecurityContext))
			{
				throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("WrongIdentityRenewingToken")), request);
			}
			int keySize;
			byte[] array;
			byte[] key;
			SecurityToken securityToken;
			WSTrust.Driver.ProcessRstAndIssueKey(rst, null, this.KeyEntropyMode, this.SecurityAlgorithmSuite, out keySize, out array, out key, out securityToken);
			DateTime utcNow = DateTime.UtcNow;
			DateTime keyExpirationTime = this.GetKeyExpirationTime(currentToken, utcNow);
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = (security != null) ? SecuritySessionSecurityTokenAuthenticator.CreateSecureConversationPolicies(security, currentTokenPolicies, keyExpirationTime) : EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			SecurityContextSecurityToken securityContextSecurityToken;
			if (currentToken != null)
			{
				securityContextSecurityToken = new SecurityContextSecurityToken(currentToken, SecurityUtils.GenerateId(), key, SecurityUtils.GenerateUniqueId(), utcNow, keyExpirationTime, authorizationPolicies);
			}
			else
			{
				UniqueId contextId = SecurityUtils.GenerateUniqueId();
				string id = SecurityUtils.GenerateId();
				DateTime dateTime = utcNow;
				DateTime validTo = TimeoutHelper.Add(dateTime, this.sessionTokenLifetime);
				securityContextSecurityToken = new SecurityContextSecurityToken(contextId, id, key, dateTime, validTo, null, utcNow, keyExpirationTime, authorizationPolicies);
				if (this.preserveBootstrapTokens)
				{
					securityContextSecurityToken.BootstrapMessageProperty = ((security == null) ? null : ((SecurityMessageProperty)security.CreateCopy()));
					SecurityUtils.ErasePasswordInUsernameTokenIfPresent(securityContextSecurityToken.BootstrapMessageProperty);
				}
			}
			rstr = new RequestSecurityTokenResponse(this.standardsManager);
			rstr.Context = rst.Context;
			rstr.KeySize = keySize;
			rstr.RequestedUnattachedReference = this.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(securityContextSecurityToken, SecurityTokenReferenceStyle.External);
			rstr.RequestedAttachedReference = this.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(securityContextSecurityToken, SecurityTokenReferenceStyle.Internal);
			rstr.TokenType = this.sctUri;
			rstr.RequestedSecurityToken = securityContextSecurityToken;
			if (array != null)
			{
				rstr.SetIssuerEntropy(array);
				rstr.ComputeKey = true;
			}
			if (securityToken != null)
			{
				rstr.RequestedProofToken = securityToken;
			}
			rstr.SetLifetime(utcNow, keyExpirationTime);
			return securityContextSecurityToken;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000683EC File Offset: 0x000665EC
		private static SecurityTokenSpecification GetMatchingEndorsingSct(SecurityContextKeyIdentifierClause sctSkiClause, SecurityMessageProperty supportingTokenProperty)
		{
			if (sctSkiClause == null)
			{
				return null;
			}
			for (int i = 0; i < supportingTokenProperty.IncomingSupportingTokens.Count; i++)
			{
				if (supportingTokenProperty.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenProperty.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
				{
					SecurityContextSecurityToken securityContextSecurityToken = supportingTokenProperty.IncomingSupportingTokens[i].SecurityToken as SecurityContextSecurityToken;
					if (securityContextSecurityToken != null && sctSkiClause.Matches(securityContextSecurityToken.ContextId, securityContextSecurityToken.KeyGeneration))
					{
						return supportingTokenProperty.IncomingSupportingTokens[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00068478 File Offset: 0x00066678
		protected virtual Message ProcessRenewRequest(Message request)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			Message result;
			try
			{
				SecurityMessageProperty security = request.Properties.Security;
				if (security == null || !security.HasIncomingSupportingTokens)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("RenewSessionMissingSupportingToken")), request);
				}
				XmlDictionaryReader readerAtBodyContents = request.GetReaderAtBodyContents();
				RequestSecurityToken requestSecurityToken;
				using (readerAtBodyContents)
				{
					requestSecurityToken = this.StandardsManager.TrustDriver.CreateRequestSecurityToken(readerAtBodyContents);
					request.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				if (requestSecurityToken.RequestType != this.StandardsManager.TrustDriver.RequestTypeRenew)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidRstRequestType", new object[]
					{
						requestSecurityToken.RequestType
					})), request);
				}
				if (requestSecurityToken.RenewTarget == null)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("NoRenewTargetSpecified")), request);
				}
				SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = requestSecurityToken.RenewTarget as SecurityContextKeyIdentifierClause;
				SecurityTokenSpecification matchingEndorsingSct = SecuritySessionSecurityTokenAuthenticator.GetMatchingEndorsingSct(securityContextKeyIdentifierClause, security);
				if (securityContextKeyIdentifierClause == null || matchingEndorsingSct == null)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("BadRenewTarget", new object[]
					{
						requestSecurityToken.RenewTarget
					})), request);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse;
				SecurityContextSecurityToken securityContextSecurityToken = this.IssueToken(requestSecurityToken, request, (SecurityContextSecurityToken)matchingEndorsingSct.SecurityToken, matchingEndorsingSct.SecurityTokenPolicies, out requestSecurityTokenResponse);
				requestSecurityTokenResponse.MakeReadOnly();
				BodyWriter body = requestSecurityTokenResponse;
				if (this.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
				{
					RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = new RequestSecurityTokenResponseCollection(new List<RequestSecurityTokenResponse>(1)
					{
						requestSecurityTokenResponse
					}, this.StandardsManager);
					body = requestSecurityTokenResponseCollection;
				}
				this.NotifyOperationCompletion(SecuritySessionOperation.Renew, securityContextSecurityToken, (SecurityContextSecurityToken)matchingEndorsingSct.SecurityToken, request.Headers.ReplyTo);
				Message message = SecuritySessionSecurityTokenAuthenticator.CreateReply(request, this.RenewResponseAction, body);
				if (!securityContextSecurityToken.IsCookieMode)
				{
					this.issuedTokenCache.AddContext(securityContextSecurityToken);
				}
				result = message;
			}
			finally
			{
				SecuritySessionSecurityTokenAuthenticator.RemoveCachedTokensIfRequired(request.Properties.Security);
			}
			return result;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00068688 File Offset: 0x00066888
		private static void AddTokenToRemoveIfRequired(SecurityToken token, Collection<SecurityContextSecurityToken> sctsToRemove)
		{
			SecurityContextSecurityToken securityContextSecurityToken = token as SecurityContextSecurityToken;
			if (securityContextSecurityToken != null)
			{
				sctsToRemove.Add(securityContextSecurityToken);
			}
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000686A8 File Offset: 0x000668A8
		internal static void RemoveCachedTokensIfRequired(SecurityMessageProperty security)
		{
			if (security == null)
			{
				return;
			}
			ILogonTokenCacheManager property = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.Listener.GetProperty<ILogonTokenCacheManager>();
			Collection<ISecurityContextSecurityTokenCache> property2 = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.Listener.GetProperty<Collection<ISecurityContextSecurityTokenCache>>();
			if (property == null && (property2 == null || property2.Count == 0))
			{
				return;
			}
			Collection<SecurityContextSecurityToken> collection = new Collection<SecurityContextSecurityToken>();
			if (security.ProtectionToken != null)
			{
				SecuritySessionSecurityTokenAuthenticator.AddTokenToRemoveIfRequired(security.ProtectionToken.SecurityToken, collection);
			}
			if (security.InitiatorToken != null)
			{
				SecuritySessionSecurityTokenAuthenticator.AddTokenToRemoveIfRequired(security.InitiatorToken.SecurityToken, collection);
			}
			if (security.HasIncomingSupportingTokens)
			{
				for (int i = 0; i < security.IncomingSupportingTokens.Count; i++)
				{
					if (security.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || security.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEncrypted || security.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
					{
						SecuritySessionSecurityTokenAuthenticator.AddTokenToRemoveIfRequired(security.IncomingSupportingTokens[i].SecurityToken, collection);
					}
				}
			}
			if (property2 != null)
			{
				for (int j = 0; j < collection.Count; j++)
				{
					for (int k = 0; k < property2.Count; k++)
					{
						property2[k].RemoveContext(collection[j].ContextId, collection[j].KeyGeneration);
					}
				}
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000687FC File Offset: 0x000669FC
		protected virtual Message ProcessIssueRequest(Message request)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			Message result;
			try
			{
				RequestSecurityToken requestSecurityToken;
				using (XmlDictionaryReader readerAtBodyContents = request.GetReaderAtBodyContents())
				{
					requestSecurityToken = this.StandardsManager.TrustDriver.CreateRequestSecurityToken(readerAtBodyContents);
					request.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				if (requestSecurityToken.RequestType != null && requestSecurityToken.RequestType != this.StandardsManager.TrustDriver.RequestTypeIssue)
				{
					throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidRstRequestType", new object[]
					{
						requestSecurityToken.RequestType
					})), request);
				}
				string a;
				string a2;
				requestSecurityToken.GetAppliesToQName(out a, out a2);
				DataContractSerializer serializer;
				EndpointAddress endpointAddress;
				if (a == "EndpointReference" && a2 == request.Version.Addressing.Namespace)
				{
					if (request.Version.Addressing == AddressingVersion.WSAddressing10)
					{
						serializer = DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddress10), int.MaxValue);
						endpointAddress = requestSecurityToken.GetAppliesTo<EndpointAddress10>(serializer).ToEndpointAddress();
					}
					else
					{
						if (request.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								request.Version.Addressing
							})));
						}
						serializer = DataContractSerializerDefaults.CreateSerializer(typeof(EndpointAddressAugust2004), int.MaxValue);
						endpointAddress = requestSecurityToken.GetAppliesTo<EndpointAddressAugust2004>(serializer).ToEndpointAddress();
					}
				}
				else
				{
					endpointAddress = null;
					serializer = null;
				}
				if (this.shouldMatchRstWithEndpointFilter)
				{
					SecurityUtils.MatchRstWithEndpointFilter(request, this.endpointFilterTable, this.listenUri);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse;
				SecurityContextSecurityToken securityContextSecurityToken = this.IssueToken(requestSecurityToken, request, null, null, out requestSecurityTokenResponse);
				if (endpointAddress != null)
				{
					if (request.Version.Addressing == AddressingVersion.WSAddressing10)
					{
						requestSecurityTokenResponse.SetAppliesTo<EndpointAddress10>(EndpointAddress10.FromEndpointAddress(endpointAddress), serializer);
					}
					else
					{
						if (request.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
							{
								request.Version.Addressing
							})));
						}
						requestSecurityTokenResponse.SetAppliesTo<EndpointAddressAugust2004>(EndpointAddressAugust2004.FromEndpointAddress(endpointAddress), serializer);
					}
				}
				requestSecurityTokenResponse.MakeReadOnly();
				BodyWriter body = requestSecurityTokenResponse;
				if (this.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
				{
					RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = new RequestSecurityTokenResponseCollection(new List<RequestSecurityTokenResponse>(1)
					{
						requestSecurityTokenResponse
					}, this.StandardsManager);
					body = requestSecurityTokenResponseCollection;
				}
				this.NotifyOperationCompletion(SecuritySessionOperation.Issue, securityContextSecurityToken, null, request.Headers.ReplyTo);
				Message message = SecuritySessionSecurityTokenAuthenticator.CreateReply(request, this.IssueResponseAction, body);
				if (!securityContextSecurityToken.IsCookieMode)
				{
					this.issuedTokenCache.AddContext(securityContextSecurityToken);
				}
				result = message;
			}
			finally
			{
				SecuritySessionSecurityTokenAuthenticator.RemoveCachedTokensIfRequired(request.Properties.Security);
			}
			return result;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00068AD4 File Offset: 0x00066CD4
		internal static bool DoesSkiClauseMatchSigningToken(SecurityContextKeyIdentifierClause skiClause, Message request)
		{
			SecurityMessageProperty security = request.Properties.Security;
			if (security == null)
			{
				throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("SFxSecurityContextPropertyMissingFromRequestMessage")), request);
			}
			SecurityContextSecurityToken securityContextSecurityToken = (security.ProtectionToken != null) ? (security.ProtectionToken.SecurityToken as SecurityContextSecurityToken) : null;
			if (securityContextSecurityToken != null && skiClause.Matches(securityContextSecurityToken.ContextId, securityContextSecurityToken.KeyGeneration))
			{
				return true;
			}
			if (security.HasIncomingSupportingTokens)
			{
				for (int i = 0; i < security.IncomingSupportingTokens.Count; i++)
				{
					if (security.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing)
					{
						securityContextSecurityToken = (security.IncomingSupportingTokens[i].SecurityToken as SecurityContextSecurityToken);
						if (securityContextSecurityToken != null && skiClause.Matches(securityContextSecurityToken.ContextId, securityContextSecurityToken.KeyGeneration))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00068BA0 File Offset: 0x00066DA0
		private static Message CreateReply(Message request, XmlDictionaryString action, BodyWriter body)
		{
			if (request.Headers.MessageId != null)
			{
				Message message = Message.CreateMessage(request.Version, ActionHeader.Create(action, request.Version.Addressing), body);
				message.InitializeReply(request);
				return message;
			}
			return Message.CreateMessage(request.Version, ActionHeader.Create(action, request.Version.Addressing), body);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00068C04 File Offset: 0x00066E04
		private Message ProcessRequest(Message request)
		{
			SecuritySessionOperation operation = SecuritySessionOperation.None;
			Message result;
			try
			{
				if (request == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
				}
				if (request.Headers.Action == this.IssueAction.Value)
				{
					operation = SecuritySessionOperation.Issue;
					result = this.ProcessIssueRequest(request);
				}
				else
				{
					if (!(request.Headers.Action == this.RenewAction.Value))
					{
						throw TraceUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidActionForNegotiationMessage", new object[]
						{
							request.Headers.Action
						})), request);
					}
					operation = SecuritySessionOperation.Renew;
					result = this.ProcessRenewRequest(request);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				result = this.HandleOperationException(operation, request, ex);
			}
			return result;
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00068CCC File Offset: 0x00066ECC
		internal ServiceHostBase RequestSecurityTokenListener
		{
			get
			{
				return this.rstListener;
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00068CD4 File Offset: 0x00066ED4
		private void SetupSessionListener()
		{
			ChannelBuilder channelBuilder = new ChannelBuilder(this.IssuerBindingContext, true);
			channelBuilder.Binding.Elements.Insert(0, new ReplyAdapterBindingElement());
			channelBuilder.Binding.Elements.Insert(0, new SecuritySessionSecurityTokenAuthenticator.SecuritySessionAuthenticatorBindingElement(this));
			List<string> list = new List<string>();
			list.Add(this.IssueAction.Value);
			list.Add(this.RenewAction.Value);
			SecurityBindingElement sbe = this.IssuerBindingContext.Binding.Elements.Find<SecurityBindingElement>();
			foreach (SecurityTokenParameters securityTokenParameters in new SecurityTokenParametersEnumerable(sbe))
			{
				if (securityTokenParameters is SecureConversationSecurityTokenParameters)
				{
					SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = (SecureConversationSecurityTokenParameters)securityTokenParameters;
					if (!secureConversationSecurityTokenParameters.CanRenewSession)
					{
						list.Remove(this.RenewAction.Value);
						break;
					}
				}
			}
			MessageFilter filter = new SessionActionFilter(this.standardsManager, list.ToArray());
			SecuritySessionSecurityTokenAuthenticator.SecuritySessionHost securitySessionHost = new SecuritySessionSecurityTokenAuthenticator.SecuritySessionHost(this, filter, this.ListenUri, channelBuilder);
			this.rstListener = securitySessionHost;
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x00068DF4 File Offset: 0x00066FF4
		internal IChannelListener<TChannel> BuildResponderChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			SecurityCredentialsManager securityCredentialsManager = this.IssuerBindingContext.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			this.bootstrapSecurityBindingElement.ReaderQuotas = this.IssuerBindingContext.GetInnerProperty<XmlDictionaryReaderQuotas>();
			if (this.bootstrapSecurityBindingElement.ReaderQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EncodingBindingElementDoesNotHandleReaderQuotas")));
			}
			TransportBindingElement transportBindingElement = context.RemainingBindingElements.Find<TransportBindingElement>();
			if (transportBindingElement != null)
			{
				this.bootstrapSecurityBindingElement.MaxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
			SecurityProtocolFactory securityProtocolFactory = this.bootstrapSecurityBindingElement.CreateSecurityProtocolFactory<TChannel>(this.IssuerBindingContext.Clone(), securityCredentialsManager, true, this.IssuerBindingContext.Clone());
			if (securityProtocolFactory is MessageSecurityProtocolFactory)
			{
				MessageSecurityProtocolFactory messageSecurityProtocolFactory = (MessageSecurityProtocolFactory)securityProtocolFactory;
				messageSecurityProtocolFactory.ApplyConfidentiality = (messageSecurityProtocolFactory.ApplyIntegrity = (messageSecurityProtocolFactory.RequireConfidentiality = (messageSecurityProtocolFactory.RequireIntegrity = true)));
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.ChannelParts.IsBodyIncluded = true;
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.ChannelParts.IsBodyIncluded = true;
				MessagePartSpecification parts = new MessagePartSpecification(true);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.IssueResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, this.IssueResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.RenewResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, this.RenewResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.IssueAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(parts, this.IssueAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.RenewAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(parts, this.RenewAction);
			}
			SupportingTokenParameters supportingTokenParameters = new SupportingTokenParameters();
			SecurityContextSecurityTokenParameters securityContextSecurityTokenParameters = new SecurityContextSecurityTokenParameters();
			securityContextSecurityTokenParameters.RequireDerivedKeys = this.IssuedSecurityTokenParameters.RequireDerivedKeys;
			supportingTokenParameters.Endorsing.Add(securityContextSecurityTokenParameters);
			securityProtocolFactory.SecurityBindingElement.OperationSupportingTokenParameters.Add(this.RenewAction.Value, supportingTokenParameters);
			securityProtocolFactory.SecurityTokenManager = new SecuritySessionSecurityTokenAuthenticator.SessionRenewSecurityTokenManager(securityProtocolFactory.SecurityTokenManager, this.sessionTokenAuthenticator, (SecurityTokenResolver)this.IssuedTokenCache);
			SecurityChannelListener<TChannel> securityChannelListener = new SecurityChannelListener<TChannel>(this.bootstrapSecurityBindingElement, this.IssuerBindingContext);
			securityChannelListener.SecurityProtocolFactory = securityProtocolFactory;
			securityChannelListener.SendUnsecuredFaults = !SecurityUtils.IsCompositeDuplexBinding(context);
			ChannelBuilder channelBuilder = new ChannelBuilder(context, true);
			securityChannelListener.InitializeListener(channelBuilder);
			this.shouldMatchRstWithEndpointFilter = SecurityUtils.ShouldMatchRstWithEndpointFilter(this.bootstrapSecurityBindingElement);
			return securityChannelListener;
		}

		// Token: 0x04001D95 RID: 7573
		internal static readonly TimeSpan defaultSessionTokenLifetime = TimeSpan.MaxValue;

		// Token: 0x04001D96 RID: 7574
		internal const int defaultMaxCachedSessionTokens = 2147483647;

		// Token: 0x04001D97 RID: 7575
		internal static readonly SecurityStandardsManager defaultStandardsManager = SecurityStandardsManager.DefaultInstance;

		// Token: 0x04001D98 RID: 7576
		private bool isClientAnonymous;

		// Token: 0x04001D99 RID: 7577
		private TimeSpan sessionTokenLifetime;

		// Token: 0x04001D9A RID: 7578
		private ISecurityContextSecurityTokenCache issuedTokenCache;

		// Token: 0x04001D9B RID: 7579
		private SecurityContextSecurityTokenAuthenticator sessionTokenAuthenticator;

		// Token: 0x04001D9C RID: 7580
		private ServiceHostBase rstListener;

		// Token: 0x04001D9D RID: 7581
		private SecurityBindingElement bootstrapSecurityBindingElement;

		// Token: 0x04001D9E RID: 7582
		private BindingContext issuerBindingContext;

		// Token: 0x04001D9F RID: 7583
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001DA0 RID: 7584
		private SecurityAlgorithmSuite securityAlgorithmSuite;

		// Token: 0x04001DA1 RID: 7585
		private SecurityKeyEntropyMode keyEntropyMode;

		// Token: 0x04001DA2 RID: 7586
		private TimeSpan keyRenewalInterval;

		// Token: 0x04001DA3 RID: 7587
		private SecurityTokenParameters issuedTokenParameters;

		// Token: 0x04001DA4 RID: 7588
		private Uri listenUri;

		// Token: 0x04001DA5 RID: 7589
		private string sctUri;

		// Token: 0x04001DA6 RID: 7590
		private IMessageFilterTable<EndpointAddress> endpointFilterTable;

		// Token: 0x04001DA7 RID: 7591
		private bool shouldMatchRstWithEndpointFilter;

		// Token: 0x04001DA8 RID: 7592
		private int maximumConcurrentNegotiations;

		// Token: 0x04001DA9 RID: 7593
		private TimeSpan negotiationTimeout;

		// Token: 0x04001DAA RID: 7594
		private object thisLock = new object();

		// Token: 0x04001DAB RID: 7595
		private bool preserveBootstrapTokens;

		// Token: 0x04001DAC RID: 7596
		private IssuedSecurityTokenHandler issuedSecurityTokenHandler;

		// Token: 0x04001DAD RID: 7597
		private RenewedSecurityTokenHandler renewedSecurityTokenHandler;

		// Token: 0x02000B70 RID: 2928
		private class SecuritySessionHost : ServiceHostBase
		{
			// Token: 0x06007287 RID: 29319 RVA: 0x001AB8A4 File Offset: 0x001A9AA4
			public SecuritySessionHost(SecuritySessionSecurityTokenAuthenticator authenticator, MessageFilter filter, Uri listenUri, ChannelBuilder channelBuilder)
			{
				this.authenticator = authenticator;
				this.filter = filter;
				this.listenUri = listenUri;
				this.channelBuilder = channelBuilder;
			}

			// Token: 0x06007288 RID: 29320 RVA: 0x001AB8C9 File Offset: 0x001A9AC9
			protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
			{
				implementedContracts = null;
				return null;
			}

			// Token: 0x06007289 RID: 29321 RVA: 0x001AB8D0 File Offset: 0x001A9AD0
			protected override void InitializeRuntime()
			{
				MessageFilter contractFilter = this.filter;
				int num = 2147483637;
				Type[] supportedChannels = new Type[]
				{
					typeof(IReplyChannel),
					typeof(IDuplexChannel),
					typeof(IReplySessionChannel),
					typeof(IDuplexSessionChannel)
				};
				IChannelListener channelListener = null;
				BindingParameterCollection bindingParameterCollection = new BindingParameterCollection(this.channelBuilder.BindingParameters);
				Binding binding = this.channelBuilder.Binding;
				binding.ReceiveTimeout = this.authenticator.NegotiationTimeout;
				bindingParameterCollection.Add(new ChannelDemuxerFilter(contractFilter, num));
				DispatcherBuilder.MaybeCreateListener(true, supportedChannels, binding, bindingParameterCollection, this.listenUri, "", ListenUriMode.Explicit, base.ServiceThrottle, out channelListener);
				if (channelListener == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotCreateTwoWayListenerForNegotiation")));
				}
				ChannelDispatcher channelDispatcher = new ChannelDispatcher(channelListener, null, binding);
				channelDispatcher.MessageVersion = binding.MessageVersion;
				channelDispatcher.ManualAddressing = true;
				channelDispatcher.ServiceThrottle = new ServiceThrottle(this);
				channelDispatcher.ServiceThrottle.MaxConcurrentCalls = this.authenticator.MaximumConcurrentNegotiations;
				channelDispatcher.ServiceThrottle.MaxConcurrentSessions = this.authenticator.MaximumConcurrentNegotiations;
				EndpointDispatcher endpointDispatcher = new EndpointDispatcher(new EndpointAddress(this.listenUri, new AddressHeader[0]), "IssueAndRenewSession", "http://tempuri.org/", true);
				endpointDispatcher.DispatchRuntime.SingletonInstanceContext = new InstanceContext(null, this.authenticator, false);
				endpointDispatcher.DispatchRuntime.ConcurrencyMode = ConcurrencyMode.Multiple;
				endpointDispatcher.AddressFilter = new MatchAllMessageFilter();
				endpointDispatcher.ContractFilter = contractFilter;
				endpointDispatcher.FilterPriority = num;
				endpointDispatcher.DispatchRuntime.PrincipalPermissionMode = PrincipalPermissionMode.None;
				endpointDispatcher.DispatchRuntime.InstanceContextProvider = new SingletonInstanceContextProvider(endpointDispatcher.DispatchRuntime);
				endpointDispatcher.DispatchRuntime.SynchronizationContext = null;
				if (this.authenticator.IssuerBindingContext != null && this.authenticator.IssuerBindingContext.BindingParameters != null)
				{
					ServiceAuthenticationManager serviceAuthenticationManager = this.authenticator.IssuerBindingContext.BindingParameters.Find<ServiceAuthenticationManager>();
					if (serviceAuthenticationManager != null)
					{
						endpointDispatcher.DispatchRuntime.ServiceAuthenticationManager = new SCTServiceAuthenticationManagerWrapper(serviceAuthenticationManager);
					}
				}
				DispatchOperation dispatchOperation = new DispatchOperation(endpointDispatcher.DispatchRuntime, "*", "*", "*");
				dispatchOperation.Formatter = new MessageOperationFormatter();
				dispatchOperation.Invoker = new SecuritySessionSecurityTokenAuthenticator.SecuritySessionHost.SecuritySessionAuthenticatorInvoker(this.authenticator);
				endpointDispatcher.DispatchRuntime.UnhandledDispatchOperation = dispatchOperation;
				channelDispatcher.Endpoints.Add(endpointDispatcher);
				base.ChannelDispatchers.Add(channelDispatcher);
			}

			// Token: 0x040040D2 RID: 16594
			private ChannelBuilder channelBuilder;

			// Token: 0x040040D3 RID: 16595
			private MessageFilter filter;

			// Token: 0x040040D4 RID: 16596
			private Uri listenUri;

			// Token: 0x040040D5 RID: 16597
			private SecuritySessionSecurityTokenAuthenticator authenticator;

			// Token: 0x02000EF7 RID: 3831
			private class SecuritySessionAuthenticatorInvoker : IOperationInvoker
			{
				// Token: 0x06008552 RID: 34130 RVA: 0x001ED996 File Offset: 0x001EBB96
				internal SecuritySessionAuthenticatorInvoker(SecuritySessionSecurityTokenAuthenticator parent)
				{
					this.parent = parent;
				}

				// Token: 0x17001D46 RID: 7494
				// (get) Token: 0x06008553 RID: 34131 RVA: 0x001ED9A5 File Offset: 0x001EBBA5
				public bool IsSynchronous
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06008554 RID: 34132 RVA: 0x001ED9A8 File Offset: 0x001EBBA8
				public object[] AllocateInputs()
				{
					return EmptyArray<object>.Allocate(1);
				}

				// Token: 0x06008555 RID: 34133 RVA: 0x001ED9B0 File Offset: 0x001EBBB0
				public object Invoke(object instance, object[] inputs, out object[] outputs)
				{
					outputs = EmptyArray<object>.Allocate(0);
					return this.parent.ProcessRequest((Message)inputs[0]);
				}

				// Token: 0x06008556 RID: 34134 RVA: 0x001ED9CD File Offset: 0x001EBBCD
				public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
				}

				// Token: 0x06008557 RID: 34135 RVA: 0x001ED9DE File Offset: 0x001EBBDE
				public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
				}

				// Token: 0x04004D3C RID: 19772
				private SecuritySessionSecurityTokenAuthenticator parent;
			}
		}

		// Token: 0x02000B71 RID: 2929
		private class SecuritySessionAuthenticatorBindingElement : BindingElement
		{
			// Token: 0x0600728A RID: 29322 RVA: 0x001ABB47 File Offset: 0x001A9D47
			public SecuritySessionAuthenticatorBindingElement(SecuritySessionSecurityTokenAuthenticator authenticator)
			{
				this.authenticator = authenticator;
			}

			// Token: 0x0600728B RID: 29323 RVA: 0x001ABB56 File Offset: 0x001A9D56
			public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
			{
				if (context == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
				}
				return this.authenticator.BuildResponderChannelListener<TChannel>(context);
			}

			// Token: 0x0600728C RID: 29324 RVA: 0x001ABB77 File Offset: 0x001A9D77
			public override BindingElement Clone()
			{
				return new SecuritySessionSecurityTokenAuthenticator.SecuritySessionAuthenticatorBindingElement(this.authenticator);
			}

			// Token: 0x0600728D RID: 29325 RVA: 0x001ABB84 File Offset: 0x001A9D84
			public override T GetProperty<T>(BindingContext context)
			{
				if (typeof(T) == typeof(ISecurityCapabilities))
				{
					return (T)((object)this.authenticator.BootstrapSecurityBindingElement.GetProperty<ISecurityCapabilities>(context));
				}
				return context.GetInnerProperty<T>();
			}

			// Token: 0x040040D6 RID: 16598
			private SecuritySessionSecurityTokenAuthenticator authenticator;
		}

		// Token: 0x02000B72 RID: 2930
		public class SessionRenewSecurityTokenManager : SecurityTokenManager
		{
			// Token: 0x0600728E RID: 29326 RVA: 0x001ABBBE File Offset: 0x001A9DBE
			public SessionRenewSecurityTokenManager(SecurityTokenManager innerTokenManager, SecurityTokenAuthenticator renewTokenAuthenticator, SecurityTokenResolver renewTokenResolver)
			{
				this.innerTokenManager = innerTokenManager;
				this.renewTokenAuthenticator = renewTokenAuthenticator;
				this.renewTokenResolver = renewTokenResolver;
			}

			// Token: 0x0600728F RID: 29327 RVA: 0x001ABBDC File Offset: 0x001A9DDC
			public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
			{
				if (tokenRequirement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
				}
				if (tokenRequirement.TokenType == ServiceModelSecurityTokenTypes.SecurityContext)
				{
					outOfBandTokenResolver = this.renewTokenResolver;
					return this.renewTokenAuthenticator;
				}
				return this.innerTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
			}

			// Token: 0x06007290 RID: 29328 RVA: 0x001ABC2A File Offset: 0x001A9E2A
			public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement requirement)
			{
				return this.innerTokenManager.CreateSecurityTokenProvider(requirement);
			}

			// Token: 0x06007291 RID: 29329 RVA: 0x001ABC38 File Offset: 0x001A9E38
			public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
			{
				return this.innerTokenManager.CreateSecurityTokenSerializer(version);
			}

			// Token: 0x040040D7 RID: 16599
			private SecurityTokenManager innerTokenManager;

			// Token: 0x040040D8 RID: 16600
			private SecurityTokenAuthenticator renewTokenAuthenticator;

			// Token: 0x040040D9 RID: 16601
			private SecurityTokenResolver renewTokenResolver;
		}
	}
}
