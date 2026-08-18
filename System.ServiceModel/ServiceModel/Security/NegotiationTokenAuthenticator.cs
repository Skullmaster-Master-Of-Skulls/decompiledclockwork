using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002FF RID: 767
	internal abstract class NegotiationTokenAuthenticator<T> : CommunicationObjectSecurityTokenAuthenticator, IIssuanceSecurityTokenAuthenticator, ISecurityContextSecurityTokenCacheProvider where T : NegotiationTokenAuthenticatorState
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x00061403 File Offset: 0x0005F603
		protected NegotiationTokenAuthenticator()
		{
			this.InitializeDefaults();
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00061411 File Offset: 0x0005F611
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00061419 File Offset: 0x0005F619
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

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00061422 File Offset: 0x0005F622
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0006142A File Offset: 0x0005F62A
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

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00061433 File Offset: 0x0005F633
		// (set) Token: 0x060019F6 RID: 6646 RVA: 0x0006143B File Offset: 0x0005F63B
		public bool EncryptStateInServiceToken
		{
			get
			{
				return this.encryptStateInServiceToken;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.encryptStateInServiceToken = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0006144F File Offset: 0x0005F64F
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x00061458 File Offset: 0x0005F658
		public TimeSpan ServiceTokenLifetime
		{
			get
			{
				return this.serviceTokenLifetime;
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
				this.serviceTokenLifetime = value;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000614D0 File Offset: 0x0005F6D0
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x000614D8 File Offset: 0x0005F6D8
		public int MaximumCachedNegotiationState
		{
			get
			{
				return this.maximumCachedNegotiationState;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeNonNegative")));
				}
				this.maximumCachedNegotiationState = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0006150F File Offset: 0x0005F70F
		// (set) Token: 0x060019FC RID: 6652 RVA: 0x00061517 File Offset: 0x0005F717
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

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0006154E File Offset: 0x0005F74E
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x00061558 File Offset: 0x0005F758
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
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.negotiationTimeout = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x000615D0 File Offset: 0x0005F7D0
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x000615D8 File Offset: 0x0005F7D8
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

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000615EC File Offset: 0x0005F7EC
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x000615F4 File Offset: 0x0005F7F4
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

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x00061608 File Offset: 0x0005F808
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x00061610 File Offset: 0x0005F810
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

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00061624 File Offset: 0x0005F824
		ISecurityContextSecurityTokenCache ISecurityContextSecurityTokenCacheProvider.TokenCache
		{
			get
			{
				return this.IssuedTokenCache;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0006162C File Offset: 0x0005F82C
		public virtual XmlDictionaryString RequestSecurityTokenAction
		{
			get
			{
				return this.StandardsManager.TrustDriver.RequestSecurityTokenAction;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0006163E File Offset: 0x0005F83E
		public virtual XmlDictionaryString RequestSecurityTokenResponseAction
		{
			get
			{
				return this.StandardsManager.TrustDriver.RequestSecurityTokenResponseAction;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00061650 File Offset: 0x0005F850
		public virtual XmlDictionaryString RequestSecurityTokenResponseFinalAction
		{
			get
			{
				return this.StandardsManager.TrustDriver.RequestSecurityTokenResponseFinalAction;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00061662 File Offset: 0x0005F862
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x0006166A File Offset: 0x0005F86A
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.standardsManager = ((value != null) ? value : SecurityStandardsManager.DefaultInstance);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00061688 File Offset: 0x0005F888
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x00061690 File Offset: 0x0005F890
		public SecurityTokenParameters IssuedSecurityTokenParameters
		{
			get
			{
				return this.issuedSecurityTokenParameters;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.issuedSecurityTokenParameters = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x000616A4 File Offset: 0x0005F8A4
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x000616AC File Offset: 0x0005F8AC
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

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000616C0 File Offset: 0x0005F8C0
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x000616C8 File Offset: 0x0005F8C8
		public AuditLogLocation AuditLogLocation
		{
			get
			{
				return this.auditLogLocation;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.auditLogLocation = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x000616DC File Offset: 0x0005F8DC
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000616E4 File Offset: 0x0005F8E4
		public bool SuppressAuditFailure
		{
			get
			{
				return this.suppressAuditFailure;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.suppressAuditFailure = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000616F8 File Offset: 0x0005F8F8
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x00061700 File Offset: 0x0005F900
		public AuditLevel MessageAuthenticationAuditLevel
		{
			get
			{
				return this.messageAuthenticationAuditLevel;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.messageAuthenticationAuditLevel = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00061714 File Offset: 0x0005F914
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0006171C File Offset: 0x0005F91C
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

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x00061748 File Offset: 0x0005F948
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x00061750 File Offset: 0x0005F950
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

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00061764 File Offset: 0x0005F964
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x0006176C File Offset: 0x0005F96C
		public SecurityStateEncoder SecurityStateEncoder
		{
			get
			{
				return this.securityStateEncoder;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.securityStateEncoder = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00061780 File Offset: 0x0005F980
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x00061788 File Offset: 0x0005F988
		public IList<Type> KnownTypes
		{
			get
			{
				return this.knownTypes;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value != null)
				{
					this.knownTypes = new Collection<Type>(value);
					return;
				}
				this.knownTypes = null;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000617AC File Offset: 0x0005F9AC
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000617B4 File Offset: 0x0005F9B4
		public int MaxMessageSize
		{
			get
			{
				return this.maxMessageSize;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.maxMessageSize = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000617C8 File Offset: 0x0005F9C8
		protected string SecurityContextTokenUri
		{
			get
			{
				base.CommunicationObject.ThrowIfNotOpened();
				return this.sctUri;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000617DB File Offset: 0x0005F9DB
		private object ThisLock
		{
			get
			{
				return base.CommunicationObject;
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000617E4 File Offset: 0x0005F9E4
		protected SecurityContextSecurityToken IssueSecurityContextToken(UniqueId contextId, string id, byte[] key, DateTime tokenEffectiveTime, DateTime tokenExpirationTime, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, bool isCookieMode)
		{
			return this.IssueSecurityContextToken(contextId, id, key, tokenEffectiveTime, tokenExpirationTime, null, tokenEffectiveTime, tokenExpirationTime, authorizationPolicies, isCookieMode);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00061808 File Offset: 0x0005FA08
		protected SecurityContextSecurityToken IssueSecurityContextToken(UniqueId contextId, string id, byte[] key, DateTime tokenEffectiveTime, DateTime tokenExpirationTime, UniqueId keyGeneration, DateTime keyEffectiveTime, DateTime keyExpirationTime, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, bool isCookieMode)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			if (this.securityStateEncoder == null && isCookieMode)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SctCookieNotSupported")));
			}
			byte[] cookieBlob = isCookieMode ? this.cookieSerializer.CreateCookieFromSecurityContext(contextId, id, key, tokenEffectiveTime, tokenExpirationTime, keyGeneration, keyEffectiveTime, keyExpirationTime, authorizationPolicies) : null;
			return new SecurityContextSecurityToken(contextId, id, key, tokenEffectiveTime, tokenExpirationTime, authorizationPolicies, isCookieMode, cookieBlob, keyGeneration, keyEffectiveTime, keyExpirationTime);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00061884 File Offset: 0x0005FA84
		private void InitializeDefaults()
		{
			this.encryptStateInServiceToken = false;
			this.serviceTokenLifetime = NegotiationTokenAuthenticator<T>.defaultServerIssuedTokenLifetime;
			this.maximumCachedNegotiationState = 128;
			this.negotiationTimeout = NegotiationTokenAuthenticator<T>.defaultServerMaxNegotiationLifetime;
			this.isClientAnonymous = false;
			this.standardsManager = NegotiationTokenAuthenticator<T>.defaultStandardsManager;
			this.securityStateEncoder = NegotiationTokenAuthenticator<T>.defaultSecurityStateEncoder;
			this.maximumConcurrentNegotiations = 128;
			this.maxMessageSize = int.MaxValue;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000618EC File Offset: 0x0005FAEC
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.negotiationHost != null)
			{
				this.negotiationHost.Close(timeoutHelper.RemainingTime());
				this.negotiationHost = null;
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.idlingNegotiationSessionTimer != null && !this.isTimerCancelled)
				{
					this.isTimerCancelled = true;
					this.idlingNegotiationSessionTimer.Cancel();
				}
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00061980 File Offset: 0x0005FB80
		public override void OnAbort()
		{
			if (this.negotiationHost != null)
			{
				this.negotiationHost.Abort();
				this.negotiationHost = null;
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.idlingNegotiationSessionTimer != null && !this.isTimerCancelled)
				{
					this.isTimerCancelled = true;
					this.idlingNegotiationSessionTimer.Cancel();
				}
			}
			base.OnAbort();
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00061A00 File Offset: 0x0005FC00
		public override void OnOpen(TimeSpan timeout)
		{
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
			this.SetupServiceHost();
			this.negotiationHost.Open(timeoutHelper.RemainingTime());
			this.stateCache = new NegotiationTokenAuthenticatorStateCache<T>(this.NegotiationTimeout, this.MaximumCachedNegotiationState);
			this.sctUri = this.StandardsManager.SecureConversationDriver.TokenTypeUri;
			if (this.SecurityStateEncoder != null)
			{
				this.cookieSerializer = new SecurityContextCookieSerializer(this.SecurityStateEncoder, this.KnownTypes);
			}
			if (this.negotiationTimeout < TimeSpan.MaxValue)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.activeNegotiationChannels1 = new List<IChannel>();
					this.activeNegotiationChannels2 = new List<IChannel>();
					this.idlingNegotiationSessionTimer = new IOThreadTimer(new Action<object>(this.OnIdlingNegotiationSessionTimer), this, false);
					this.isTimerCancelled = false;
					this.idlingNegotiationSessionTimer.Set(this.negotiationTimeout);
				}
			}
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00061BD0 File Offset: 0x0005FDD0
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is SecurityContextSecurityToken;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00061BDC File Offset: 0x0005FDDC
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			SecurityContextSecurityToken securityContextSecurityToken = (SecurityContextSecurityToken)token;
			return securityContextSecurityToken.AuthorizationPolicies;
		}

		// Token: 0x06001A29 RID: 6697
		protected abstract Binding GetNegotiationBinding(Binding binding);

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001A2A RID: 6698
		protected abstract bool IsMultiLegNegotiation { get; }

		// Token: 0x06001A2B RID: 6699
		protected abstract MessageFilter GetListenerFilter();

		// Token: 0x06001A2C RID: 6700 RVA: 0x00061BF8 File Offset: 0x0005FDF8
		private void SetupServiceHost()
		{
			ChannelBuilder channelBuilder = new ChannelBuilder(this.IssuerBindingContext.Clone(), true);
			channelBuilder.Binding.Elements.Insert(0, new ReplyAdapterBindingElement());
			channelBuilder.Binding = new CustomBinding(this.GetNegotiationBinding(channelBuilder.Binding));
			this.negotiationHost = new NegotiationTokenAuthenticator<T>.NegotiationHost(this, this.ListenUri, channelBuilder, this.GetListenerFilter());
		}

		// Token: 0x06001A2D RID: 6701
		protected abstract BodyWriter ProcessRequestSecurityToken(Message request, RequestSecurityToken requestSecurityToken, out T negotiationState);

		// Token: 0x06001A2E RID: 6702
		protected abstract BodyWriter ProcessRequestSecurityTokenResponse(T negotiationState, Message request, RequestSecurityTokenResponse requestSecurityTokenResponse);

		// Token: 0x06001A2F RID: 6703 RVA: 0x00061C60 File Offset: 0x0005FE60
		protected virtual void ParseMessageBody(Message message, out string context, out RequestSecurityToken requestSecurityToken, out RequestSecurityTokenResponse requestSecurityTokenResponse)
		{
			requestSecurityToken = null;
			requestSecurityTokenResponse = null;
			if (message.Headers.Action == this.RequestSecurityTokenAction.Value)
			{
				XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
				using (readerAtBodyContents)
				{
					requestSecurityToken = RequestSecurityToken.CreateFrom(this.StandardsManager, readerAtBodyContents);
					message.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				context = requestSecurityToken.Context;
				return;
			}
			if (message.Headers.Action == this.RequestSecurityTokenResponseAction.Value)
			{
				XmlDictionaryReader readerAtBodyContents2 = message.GetReaderAtBodyContents();
				using (readerAtBodyContents2)
				{
					requestSecurityTokenResponse = RequestSecurityTokenResponse.CreateFrom(this.StandardsManager, readerAtBodyContents2);
					message.ReadFromBodyContentsToEnd(readerAtBodyContents2);
				}
				context = requestSecurityTokenResponse.Context;
				return;
			}
			throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidActionForNegotiationMessage", new object[]
			{
				message.Headers.Action
			})), message);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00061D60 File Offset: 0x0005FF60
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

		// Token: 0x06001A31 RID: 6705 RVA: 0x00061DC4 File Offset: 0x0005FFC4
		private void OnTokenIssued(SecurityToken token)
		{
			if (this.issuedSecurityTokenHandler != null)
			{
				this.issuedSecurityTokenHandler(token, null);
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00061DDC File Offset: 0x0005FFDC
		private void AddNegotiationChannelForIdleTracking()
		{
			if (OperationContext.Current.SessionId == null)
			{
				return;
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.idlingNegotiationSessionTimer != null)
				{
					IChannel channel = OperationContext.Current.Channel;
					if (!this.activeNegotiationChannels1.Contains(channel) && !this.activeNegotiationChannels2.Contains(channel))
					{
						this.activeNegotiationChannels1.Add(channel);
					}
					if (this.isTimerCancelled)
					{
						this.isTimerCancelled = false;
						this.idlingNegotiationSessionTimer.Set(this.negotiationTimeout);
					}
				}
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00061E80 File Offset: 0x00060080
		private void RemoveNegotiationChannelFromIdleTracking()
		{
			if (OperationContext.Current.SessionId == null)
			{
				return;
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.idlingNegotiationSessionTimer != null)
				{
					IChannel channel = OperationContext.Current.Channel;
					this.activeNegotiationChannels1.Remove(channel);
					this.activeNegotiationChannels2.Remove(channel);
					if (this.activeNegotiationChannels1.Count == 0 && this.activeNegotiationChannels2.Count == 0)
					{
						this.isTimerCancelled = true;
						this.idlingNegotiationSessionTimer.Cancel();
					}
				}
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00061F24 File Offset: 0x00060124
		private void OnIdlingNegotiationSessionTimer(object state)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.isTimerCancelled && (base.CommunicationObject.State == CommunicationState.Opened || base.CommunicationObject.State == CommunicationState.Opening))
				{
					try
					{
						for (int i = 0; i < this.activeNegotiationChannels2.Count; i++)
						{
							this.activeNegotiationChannels2[i].Abort();
						}
						List<IChannel> list = this.activeNegotiationChannels2;
						list.Clear();
						this.activeNegotiationChannels2 = this.activeNegotiationChannels1;
						this.activeNegotiationChannels1 = list;
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
					}
					finally
					{
						if (base.CommunicationObject.State == CommunicationState.Opened || base.CommunicationObject.State == CommunicationState.Opening)
						{
							if (this.activeNegotiationChannels1.Count == 0 && this.activeNegotiationChannels2.Count == 0)
							{
								this.isTimerCancelled = true;
								this.idlingNegotiationSessionTimer.Cancel();
							}
							else
							{
								this.idlingNegotiationSessionTimer.Set(this.negotiationTimeout);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00062054 File Offset: 0x00060254
		private Message ProcessRequestCore(Message request)
		{
			if (request == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("request");
			}
			RequestSecurityToken requestSecurityToken = null;
			RequestSecurityTokenResponse requestSecurityTokenResponse = null;
			string text = null;
			bool flag = false;
			bool flag2 = true;
			T t = default(T);
			Message result;
			try
			{
				if (this.maxMessageSize < 2147483647)
				{
					string action = request.Headers.Action;
					try
					{
						using (MessageBuffer messageBuffer = request.CreateBufferedCopy(this.maxMessageSize))
						{
							request = messageBuffer.CreateMessage();
							flag = true;
						}
					}
					catch (QuotaExceededException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("SecurityNegotiationMessageTooLarge", new object[]
						{
							action,
							this.maxMessageSize
						}), innerException));
					}
				}
				try
				{
					Uri to = request.Headers.To;
					this.ParseMessageBody(request, out text, out requestSecurityToken, out requestSecurityTokenResponse);
					if (text != null)
					{
						t = this.stateCache.GetState(text);
					}
					else
					{
						t = default(T);
					}
					bool flag3 = false;
					BodyWriter bodyWriter;
					try
					{
						if (requestSecurityToken != null)
						{
							if (t != null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("NegotiationStateAlreadyPresent", new object[]
								{
									text
								})));
							}
							bodyWriter = this.ProcessRequestSecurityToken(request, requestSecurityToken, out t);
							object thisLock = t.ThisLock;
							lock (thisLock)
							{
								if (t.IsNegotiationCompleted)
								{
									if (!t.ServiceToken.IsCookieMode)
									{
										this.IssuedTokenCache.AddContext(t.ServiceToken);
									}
									this.OnTokenIssued(t.ServiceToken);
									SecurityTraceRecordHelper.TraceServiceSecurityNegotiationCompleted<T>(request, this, t.ServiceToken);
									flag3 = true;
								}
								else
								{
									this.stateCache.AddState(text, t);
									flag3 = false;
								}
								this.AddNegotiationChannelForIdleTracking();
								goto IL_285;
							}
						}
						if (t == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("CannotFindNegotiationState", new object[]
							{
								text
							})));
						}
						object thisLock2 = t.ThisLock;
						lock (thisLock2)
						{
							bodyWriter = this.ProcessRequestSecurityTokenResponse(t, request, requestSecurityTokenResponse);
							if (t.IsNegotiationCompleted)
							{
								if (!t.ServiceToken.IsCookieMode)
								{
									this.IssuedTokenCache.AddContext(t.ServiceToken);
								}
								this.OnTokenIssued(t.ServiceToken);
								SecurityTraceRecordHelper.TraceServiceSecurityNegotiationCompleted<T>(request, this, t.ServiceToken);
								flag3 = true;
							}
							else
							{
								flag3 = false;
							}
						}
						IL_285:
						if (t.IsNegotiationCompleted && null != this.ListenUri && AuditLevel.Success == (this.messageAuthenticationAuditLevel & AuditLevel.Success))
						{
							string remoteIdentityName = t.GetRemoteIdentityName();
							SecurityAuditHelper.WriteSecurityNegotiationSuccessEvent(this.auditLogLocation, this.suppressAuditFailure, request, request.Headers.To, request.Headers.Action, remoteIdentityName, base.GetType().Name);
						}
						flag2 = false;
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						if (PerformanceCounters.PerformanceCountersEnabled && null != this.ListenUri)
						{
							PerformanceCounters.AuthenticationFailed(request, this.ListenUri);
						}
						if (AuditLevel.Failure == (this.messageAuthenticationAuditLevel & AuditLevel.Failure))
						{
							try
							{
								string clientIdentity = (t != null) ? t.GetRemoteIdentityName() : string.Empty;
								SecurityAuditHelper.WriteSecurityNegotiationFailureEvent(this.auditLogLocation, this.suppressAuditFailure, request, request.Headers.To, request.Headers.Action, clientIdentity, base.GetType().Name, exception);
							}
							catch (Exception exception2)
							{
								if (Fx.IsFatal(exception2))
								{
									throw;
								}
								DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
							}
						}
						flag3 = true;
						throw;
					}
					finally
					{
						if (flag3 && t != null)
						{
							if (text != null)
							{
								this.stateCache.RemoveState(text);
							}
							t.Dispose();
						}
					}
					result = NegotiationTokenAuthenticator<T>.CreateReply(request, (bodyWriter is RequestSecurityTokenResponseCollection) ? this.RequestSecurityTokenResponseFinalAction : this.RequestSecurityTokenResponseAction, bodyWriter);
				}
				finally
				{
					if (flag)
					{
						request.Close();
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.AddNegotiationChannelForIdleTracking();
				}
				else if (t != null && t.IsNegotiationCompleted)
				{
					this.RemoveNegotiationChannelFromIdleTracking();
				}
			}
			return result;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00062564 File Offset: 0x00060764
		private Message HandleNegotiationException(Message request, Exception e)
		{
			SecurityTraceRecordHelper.TraceServiceSecurityNegotiationFailure<T>(EventTraceActivityHelper.TryExtractActivity(request), this, e);
			return this.CreateFault(request, e);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0006257C File Offset: 0x0006077C
		private Message CreateFault(Message request, Exception e)
		{
			MessageVersion version = request.Version;
			FaultCode subCode;
			FaultReason reason;
			bool flag;
			if (e is SecurityTokenValidationException || e is Win32Exception)
			{
				subCode = new FaultCode("FailedAuthentication", "http://schemas.xmlsoap.org/ws/2005/02/trust");
				reason = new FaultReason(SR.GetString("FailedAuthenticationTrustFaultCode"), CultureInfo.CurrentCulture);
				flag = true;
			}
			else if (e is QuotaExceededException)
			{
				subCode = new FaultCode("ServerTooBusy", "http://schemas.microsoft.com/ws/2006/05/security");
				reason = new FaultReason(SR.GetString("NegotiationQuotasExceededFaultReason"), CultureInfo.CurrentCulture);
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
			Message message = Message.CreateMessage(version, fault, version.Addressing.DefaultFaultAction);
			message.Headers.RelatesTo = request.Headers.MessageId;
			return message;
		}

		// Token: 0x04001CE6 RID: 7398
		internal const string defaultServerMaxNegotiationLifetimeString = "00:01:00";

		// Token: 0x04001CE7 RID: 7399
		internal const string defaultServerIssuedTokenLifetimeString = "10:00:00";

		// Token: 0x04001CE8 RID: 7400
		internal const string defaultServerIssuedTransitionTokenLifetimeString = "00:15:00";

		// Token: 0x04001CE9 RID: 7401
		internal const int defaultServerMaxActiveNegotiations = 128;

		// Token: 0x04001CEA RID: 7402
		internal static readonly TimeSpan defaultServerMaxNegotiationLifetime = TimeSpan.Parse("00:01:00", CultureInfo.InvariantCulture);

		// Token: 0x04001CEB RID: 7403
		internal static readonly TimeSpan defaultServerIssuedTokenLifetime = TimeSpan.Parse("10:00:00", CultureInfo.InvariantCulture);

		// Token: 0x04001CEC RID: 7404
		internal static readonly TimeSpan defaultServerIssuedTransitionTokenLifetime = TimeSpan.Parse("00:15:00", CultureInfo.InvariantCulture);

		// Token: 0x04001CED RID: 7405
		internal const int defaultServerMaxCachedTokens = 1000;

		// Token: 0x04001CEE RID: 7406
		internal const bool defaultServerMaintainState = true;

		// Token: 0x04001CEF RID: 7407
		internal static readonly SecurityStandardsManager defaultStandardsManager = SecurityStandardsManager.DefaultInstance;

		// Token: 0x04001CF0 RID: 7408
		internal static readonly SecurityStateEncoder defaultSecurityStateEncoder = new DataProtectionSecurityStateEncoder();

		// Token: 0x04001CF1 RID: 7409
		private NegotiationTokenAuthenticatorStateCache<T> stateCache;

		// Token: 0x04001CF2 RID: 7410
		private RenewedSecurityTokenHandler renewedSecurityTokenHandler;

		// Token: 0x04001CF3 RID: 7411
		private NegotiationTokenAuthenticator<T>.NegotiationHost negotiationHost;

		// Token: 0x04001CF4 RID: 7412
		private bool encryptStateInServiceToken;

		// Token: 0x04001CF5 RID: 7413
		private TimeSpan serviceTokenLifetime;

		// Token: 0x04001CF6 RID: 7414
		private int maximumCachedNegotiationState;

		// Token: 0x04001CF7 RID: 7415
		private TimeSpan negotiationTimeout;

		// Token: 0x04001CF8 RID: 7416
		private bool isClientAnonymous;

		// Token: 0x04001CF9 RID: 7417
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001CFA RID: 7418
		private SecurityAlgorithmSuite securityAlgorithmSuite;

		// Token: 0x04001CFB RID: 7419
		private SecurityTokenParameters issuedSecurityTokenParameters;

		// Token: 0x04001CFC RID: 7420
		private ISecurityContextSecurityTokenCache issuedTokenCache;

		// Token: 0x04001CFD RID: 7421
		private BindingContext issuerBindingContext;

		// Token: 0x04001CFE RID: 7422
		private Uri listenUri;

		// Token: 0x04001CFF RID: 7423
		private string sctUri;

		// Token: 0x04001D00 RID: 7424
		private AuditLogLocation auditLogLocation;

		// Token: 0x04001D01 RID: 7425
		private bool suppressAuditFailure;

		// Token: 0x04001D02 RID: 7426
		private AuditLevel messageAuthenticationAuditLevel;

		// Token: 0x04001D03 RID: 7427
		private SecurityStateEncoder securityStateEncoder;

		// Token: 0x04001D04 RID: 7428
		private SecurityContextCookieSerializer cookieSerializer;

		// Token: 0x04001D05 RID: 7429
		private IMessageFilterTable<EndpointAddress> endpointFilterTable;

		// Token: 0x04001D06 RID: 7430
		private IssuedSecurityTokenHandler issuedSecurityTokenHandler;

		// Token: 0x04001D07 RID: 7431
		private int maxMessageSize;

		// Token: 0x04001D08 RID: 7432
		private IList<Type> knownTypes;

		// Token: 0x04001D09 RID: 7433
		private int maximumConcurrentNegotiations;

		// Token: 0x04001D0A RID: 7434
		private List<IChannel> activeNegotiationChannels1;

		// Token: 0x04001D0B RID: 7435
		private List<IChannel> activeNegotiationChannels2;

		// Token: 0x04001D0C RID: 7436
		private IOThreadTimer idlingNegotiationSessionTimer;

		// Token: 0x04001D0D RID: 7437
		private bool isTimerCancelled;

		// Token: 0x02000B66 RID: 2918
		private class NegotiationHost : ServiceHostBase
		{
			// Token: 0x0600724B RID: 29259 RVA: 0x001AAB1D File Offset: 0x001A8D1D
			public NegotiationHost(NegotiationTokenAuthenticator<T> authenticator, Uri listenUri, ChannelBuilder channelBuilder, MessageFilter listenerFilter)
			{
				this.authenticator = authenticator;
				this.listenUri = listenUri;
				this.channelBuilder = channelBuilder;
				this.listenerFilter = listenerFilter;
			}

			// Token: 0x0600724C RID: 29260 RVA: 0x001AAB42 File Offset: 0x001A8D42
			protected override ServiceDescription CreateDescription(out IDictionary<string, ContractDescription> implementedContracts)
			{
				implementedContracts = null;
				return null;
			}

			// Token: 0x0600724D RID: 29261 RVA: 0x001AAB48 File Offset: 0x001A8D48
			protected override void InitializeRuntime()
			{
				MessageFilter messageFilter = this.listenerFilter;
				int num = 10;
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
				bindingParameterCollection.Add(new ChannelDemuxerFilter(messageFilter, num));
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
				EndpointDispatcher endpointDispatcher = new EndpointDispatcher(new EndpointAddress(this.listenUri, new AddressHeader[0]), "SecurityNegotiationContract", "http://tempuri.org/", true);
				endpointDispatcher.DispatchRuntime.SingletonInstanceContext = new InstanceContext(null, this.authenticator, false);
				endpointDispatcher.DispatchRuntime.ConcurrencyMode = ConcurrencyMode.Multiple;
				endpointDispatcher.AddressFilter = new MatchAllMessageFilter();
				endpointDispatcher.ContractFilter = messageFilter;
				endpointDispatcher.FilterPriority = num;
				endpointDispatcher.DispatchRuntime.PrincipalPermissionMode = PrincipalPermissionMode.None;
				endpointDispatcher.DispatchRuntime.InstanceContextProvider = new SingletonInstanceContextProvider(endpointDispatcher.DispatchRuntime);
				endpointDispatcher.DispatchRuntime.SynchronizationContext = null;
				DispatchOperation dispatchOperation = new DispatchOperation(endpointDispatcher.DispatchRuntime, "*", "*", "*");
				dispatchOperation.Formatter = new MessageOperationFormatter();
				dispatchOperation.Invoker = new NegotiationTokenAuthenticator<T>.NegotiationHost.NegotiationSyncInvoker(this.authenticator);
				endpointDispatcher.DispatchRuntime.UnhandledDispatchOperation = dispatchOperation;
				channelDispatcher.Endpoints.Add(endpointDispatcher);
				base.ChannelDispatchers.Add(channelDispatcher);
			}

			// Token: 0x040040AB RID: 16555
			private NegotiationTokenAuthenticator<T> authenticator;

			// Token: 0x040040AC RID: 16556
			private Uri listenUri;

			// Token: 0x040040AD RID: 16557
			private ChannelBuilder channelBuilder;

			// Token: 0x040040AE RID: 16558
			private MessageFilter listenerFilter;

			// Token: 0x02000EF5 RID: 3829
			private class NegotiationSyncInvoker : IOperationInvoker
			{
				// Token: 0x0600854C RID: 34124 RVA: 0x001ED8FD File Offset: 0x001EBAFD
				internal NegotiationSyncInvoker(NegotiationTokenAuthenticator<T> parent)
				{
					this.parent = parent;
				}

				// Token: 0x17001D45 RID: 7493
				// (get) Token: 0x0600854D RID: 34125 RVA: 0x001ED90C File Offset: 0x001EBB0C
				public bool IsSynchronous
				{
					get
					{
						return true;
					}
				}

				// Token: 0x0600854E RID: 34126 RVA: 0x001ED90F File Offset: 0x001EBB0F
				public object[] AllocateInputs()
				{
					return EmptyArray<object>.Allocate(1);
				}

				// Token: 0x0600854F RID: 34127 RVA: 0x001ED918 File Offset: 0x001EBB18
				public object Invoke(object instance, object[] inputs, out object[] outputs)
				{
					Message request = (Message)inputs[0];
					outputs = EmptyArray<object>.Allocate(0);
					object result;
					try
					{
						result = this.parent.ProcessRequestCore(request);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						result = this.parent.HandleNegotiationException(request, ex);
					}
					return result;
				}

				// Token: 0x06008550 RID: 34128 RVA: 0x001ED974 File Offset: 0x001EBB74
				public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}

				// Token: 0x06008551 RID: 34129 RVA: 0x001ED985 File Offset: 0x001EBB85
				public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}

				// Token: 0x04004D36 RID: 19766
				private NegotiationTokenAuthenticator<T> parent;
			}
		}
	}
}
