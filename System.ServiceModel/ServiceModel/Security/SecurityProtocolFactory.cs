using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C2 RID: 706
	internal abstract class SecurityProtocolFactory : ISecurityCommunicationObject
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x00055C4C File Offset: 0x00053E4C
		protected SecurityProtocolFactory()
		{
			this.channelSupportingTokenAuthenticatorSpecification = new Collection<SupportingTokenAuthenticatorSpecification>();
			this.scopedSupportingTokenAuthenticatorSpecification = new Dictionary<string, ICollection<SupportingTokenAuthenticatorSpecification>>();
			this.communicationObject = new WrapperSecurityCommunicationObject(this);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00055CDC File Offset: 0x00053EDC
		internal SecurityProtocolFactory(SecurityProtocolFactory factory) : this()
		{
			if (factory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("factory");
			}
			this.actAsInitiator = factory.actAsInitiator;
			this.addTimestamp = factory.addTimestamp;
			this.detectReplays = factory.detectReplays;
			this.incomingAlgorithmSuite = factory.incomingAlgorithmSuite;
			this.maxCachedNonces = factory.maxCachedNonces;
			this.maxClockSkew = factory.maxClockSkew;
			this.outgoingAlgorithmSuite = factory.outgoingAlgorithmSuite;
			this.replayWindow = factory.replayWindow;
			this.channelSupportingTokenAuthenticatorSpecification = new Collection<SupportingTokenAuthenticatorSpecification>(new List<SupportingTokenAuthenticatorSpecification>(factory.channelSupportingTokenAuthenticatorSpecification));
			this.scopedSupportingTokenAuthenticatorSpecification = new Dictionary<string, ICollection<SupportingTokenAuthenticatorSpecification>>(factory.scopedSupportingTokenAuthenticatorSpecification);
			this.standardsManager = factory.standardsManager;
			this.timestampValidityDuration = factory.timestampValidityDuration;
			this.auditLogLocation = factory.auditLogLocation;
			this.suppressAuditFailure = factory.suppressAuditFailure;
			this.serviceAuthorizationAuditLevel = factory.serviceAuthorizationAuditLevel;
			this.messageAuthenticationAuditLevel = factory.messageAuthenticationAuditLevel;
			if (factory.securityBindingElement != null)
			{
				this.securityBindingElement = (SecurityBindingElement)factory.securityBindingElement.Clone();
			}
			this.securityTokenManager = factory.securityTokenManager;
			this.privacyNoticeUri = factory.privacyNoticeUri;
			this.privacyNoticeVersion = factory.privacyNoticeVersion;
			this.endpointFilterTable = factory.endpointFilterTable;
			this.extendedProtectionPolicy = factory.extendedProtectionPolicy;
			this.nonceCache = factory.nonceCache;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00055E37 File Offset: 0x00054037
		protected WrapperSecurityCommunicationObject CommunicationObject
		{
			get
			{
				return this.communicationObject;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00055E3F File Offset: 0x0005403F
		public bool ActAsInitiator
		{
			get
			{
				return this.actAsInitiator;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00055E47 File Offset: 0x00054047
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x00055E69 File Offset: 0x00054069
		public BufferManager StreamBufferManager
		{
			get
			{
				if (this.streamBufferManager == null)
				{
					this.streamBufferManager = BufferManager.CreateBufferManager(0L, int.MaxValue);
				}
				return this.streamBufferManager;
			}
			set
			{
				this.streamBufferManager = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00055E72 File Offset: 0x00054072
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00055E7A File Offset: 0x0005407A
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				this.extendedProtectionPolicy = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00055E83 File Offset: 0x00054083
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00055E8B File Offset: 0x0005408B
		internal bool IsDuplexReply
		{
			get
			{
				return this.isDuplexReply;
			}
			set
			{
				this.isDuplexReply = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00055E94 File Offset: 0x00054094
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x00055E9C File Offset: 0x0005409C
		public bool AddTimestamp
		{
			get
			{
				return this.addTimestamp;
			}
			set
			{
				this.ThrowIfImmutable();
				this.addTimestamp = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00055EAB File Offset: 0x000540AB
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x00055EB3 File Offset: 0x000540B3
		public AuditLogLocation AuditLogLocation
		{
			get
			{
				return this.auditLogLocation;
			}
			set
			{
				this.ThrowIfImmutable();
				AuditLogLocationHelper.Validate(value);
				this.auditLogLocation = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00055EC8 File Offset: 0x000540C8
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00055ED0 File Offset: 0x000540D0
		public bool SuppressAuditFailure
		{
			get
			{
				return this.suppressAuditFailure;
			}
			set
			{
				this.ThrowIfImmutable();
				this.suppressAuditFailure = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00055EDF File Offset: 0x000540DF
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x00055EE7 File Offset: 0x000540E7
		public AuditLevel ServiceAuthorizationAuditLevel
		{
			get
			{
				return this.serviceAuthorizationAuditLevel;
			}
			set
			{
				this.ThrowIfImmutable();
				AuditLevelHelper.Validate(value);
				this.serviceAuthorizationAuditLevel = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00055EFC File Offset: 0x000540FC
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00055F04 File Offset: 0x00054104
		public AuditLevel MessageAuthenticationAuditLevel
		{
			get
			{
				return this.messageAuthenticationAuditLevel;
			}
			set
			{
				this.ThrowIfImmutable();
				AuditLevelHelper.Validate(value);
				this.messageAuthenticationAuditLevel = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00055F19 File Offset: 0x00054119
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00055F21 File Offset: 0x00054121
		public bool DetectReplays
		{
			get
			{
				return this.detectReplays;
			}
			set
			{
				this.ThrowIfImmutable();
				this.detectReplays = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00055F30 File Offset: 0x00054130
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x00055F38 File Offset: 0x00054138
		public Uri PrivacyNoticeUri
		{
			get
			{
				return this.privacyNoticeUri;
			}
			set
			{
				this.ThrowIfImmutable();
				this.privacyNoticeUri = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00055F47 File Offset: 0x00054147
		// (set) Token: 0x060016A3 RID: 5795 RVA: 0x00055F4F File Offset: 0x0005414F
		public int PrivacyNoticeVersion
		{
			get
			{
				return this.privacyNoticeVersion;
			}
			set
			{
				this.ThrowIfImmutable();
				this.privacyNoticeVersion = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00055F5E File Offset: 0x0005415E
		// (set) Token: 0x060016A5 RID: 5797 RVA: 0x00055F66 File Offset: 0x00054166
		public IMessageFilterTable<EndpointAddress> EndpointFilterTable
		{
			get
			{
				return this.endpointFilterTable;
			}
			set
			{
				this.ThrowIfImmutable();
				this.endpointFilterTable = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00055F75 File Offset: 0x00054175
		private static ReadOnlyCollection<SupportingTokenAuthenticatorSpecification> EmptyTokenAuthenticators
		{
			get
			{
				if (SecurityProtocolFactory.emptyTokenAuthenticators == null)
				{
					SecurityProtocolFactory.emptyTokenAuthenticators = Array.AsReadOnly<SupportingTokenAuthenticatorSpecification>(new SupportingTokenAuthenticatorSpecification[0]);
				}
				return SecurityProtocolFactory.emptyTokenAuthenticators;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00055F93 File Offset: 0x00054193
		internal NonValidatingSecurityTokenAuthenticator<DerivedKeySecurityToken> DerivedKeyTokenAuthenticator
		{
			get
			{
				return this.derivedKeyTokenAuthenticator;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x00055F9B File Offset: 0x0005419B
		internal bool ExpectIncomingMessages
		{
			get
			{
				return this.expectIncomingMessages;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00055FA3 File Offset: 0x000541A3
		internal bool ExpectOutgoingMessages
		{
			get
			{
				return this.expectOutgoingMessages;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00055FAB File Offset: 0x000541AB
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x00055FB3 File Offset: 0x000541B3
		internal bool ExpectKeyDerivation
		{
			get
			{
				return this.expectKeyDerivation;
			}
			set
			{
				this.expectKeyDerivation = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00055FBC File Offset: 0x000541BC
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x00055FC4 File Offset: 0x000541C4
		internal bool ExpectSupportingTokens
		{
			get
			{
				return this.expectSupportingTokens;
			}
			set
			{
				this.expectSupportingTokens = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x00055FCD File Offset: 0x000541CD
		// (set) Token: 0x060016AF RID: 5807 RVA: 0x00055FD5 File Offset: 0x000541D5
		public SecurityAlgorithmSuite IncomingAlgorithmSuite
		{
			get
			{
				return this.incomingAlgorithmSuite;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.incomingAlgorithmSuite = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00055FFC File Offset: 0x000541FC
		protected bool IsReadOnly
		{
			get
			{
				return this.CommunicationObject.State > CommunicationState.Created;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005600C File Offset: 0x0005420C
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x00056014 File Offset: 0x00054214
		public int MaxCachedNonces
		{
			get
			{
				return this.maxCachedNonces;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.maxCachedNonces = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0005603C File Offset: 0x0005423C
		// (set) Token: 0x060016B4 RID: 5812 RVA: 0x00056044 File Offset: 0x00054244
		public TimeSpan MaxClockSkew
		{
			get
			{
				return this.maxClockSkew;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.maxClockSkew = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00056075 File Offset: 0x00054275
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x0005607D File Offset: 0x0005427D
		public NonceCache NonceCache
		{
			get
			{
				return this.nonceCache;
			}
			set
			{
				this.ThrowIfImmutable();
				this.nonceCache = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0005608C File Offset: 0x0005428C
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x00056094 File Offset: 0x00054294
		public SecurityAlgorithmSuite OutgoingAlgorithmSuite
		{
			get
			{
				return this.outgoingAlgorithmSuite;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.outgoingAlgorithmSuite = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x000560BB File Offset: 0x000542BB
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x000560C3 File Offset: 0x000542C3
		public TimeSpan ReplayWindow
		{
			get
			{
				return this.replayWindow;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.replayWindow = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x000560FE File Offset: 0x000542FE
		public ICollection<SupportingTokenAuthenticatorSpecification> ChannelSupportingTokenAuthenticatorSpecification
		{
			get
			{
				return this.channelSupportingTokenAuthenticatorSpecification;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x00056106 File Offset: 0x00054306
		public Dictionary<string, ICollection<SupportingTokenAuthenticatorSpecification>> ScopedSupportingTokenAuthenticatorSpecification
		{
			get
			{
				return this.scopedSupportingTokenAuthenticatorSpecification;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0005610E File Offset: 0x0005430E
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x00056116 File Offset: 0x00054316
		public SecurityBindingElement SecurityBindingElement
		{
			get
			{
				return this.securityBindingElement;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value != null)
				{
					value = (SecurityBindingElement)value.Clone();
				}
				this.securityBindingElement = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00056135 File Offset: 0x00054335
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0005613D File Offset: 0x0005433D
		public SecurityTokenManager SecurityTokenManager
		{
			get
			{
				return this.securityTokenManager;
			}
			set
			{
				this.ThrowIfImmutable();
				this.securityTokenManager = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0005614C File Offset: 0x0005434C
		public virtual bool SupportsDuplex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0005614F File Offset: 0x0005434F
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x00056157 File Offset: 0x00054357
		public SecurityHeaderLayout SecurityHeaderLayout
		{
			get
			{
				return this.securityHeaderLayout;
			}
			set
			{
				this.ThrowIfImmutable();
				this.securityHeaderLayout = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x00056166 File Offset: 0x00054366
		public virtual bool SupportsReplayDetection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00056169 File Offset: 0x00054369
		public virtual bool SupportsRequestReply
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005616C File Offset: 0x0005436C
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x00056174 File Offset: 0x00054374
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.standardsManager = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005619B File Offset: 0x0005439B
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x000561A3 File Offset: 0x000543A3
		public TimeSpan TimestampValidityDuration
		{
			get
			{
				return this.timestampValidityDuration;
			}
			set
			{
				this.ThrowIfImmutable();
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.timestampValidityDuration = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000561DE File Offset: 0x000543DE
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x000561E6 File Offset: 0x000543E6
		public Uri ListenUri
		{
			get
			{
				return this.listenUri;
			}
			set
			{
				this.ThrowIfImmutable();
				this.listenUri = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000561F5 File Offset: 0x000543F5
		internal MessageSecurityVersion MessageSecurityVersion
		{
			get
			{
				return this.messageSecurityVersion;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x000561FD File Offset: 0x000543FD
		public TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00056204 File Offset: 0x00054404
		public TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0005620B File Offset: 0x0005440B
		public IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnClose), timeout, callback, state);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00056222 File Offset: 0x00054422
		public IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00056239 File Offset: 0x00054439
		public void OnClosed()
		{
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0005623B File Offset: 0x0005443B
		public void OnClosing()
		{
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0005623D File Offset: 0x0005443D
		public void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00056245 File Offset: 0x00054445
		public void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0005624D File Offset: 0x0005444D
		public void OnFaulted()
		{
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0005624F File Offset: 0x0005444F
		public void OnOpened()
		{
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00056251 File Offset: 0x00054451
		public void OnOpening()
		{
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00056254 File Offset: 0x00054454
		public virtual void OnAbort()
		{
			if (!this.actAsInitiator)
			{
				foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in this.channelSupportingTokenAuthenticatorSpecification)
				{
					SecurityUtils.AbortTokenAuthenticatorIfRequired(supportingTokenAuthenticatorSpecification.TokenAuthenticator);
				}
				foreach (string key in this.scopedSupportingTokenAuthenticatorSpecification.Keys)
				{
					ICollection<SupportingTokenAuthenticatorSpecification> collection = this.scopedSupportingTokenAuthenticatorSpecification[key];
					foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification2 in collection)
					{
						SecurityUtils.AbortTokenAuthenticatorIfRequired(supportingTokenAuthenticatorSpecification2.TokenAuthenticator);
					}
				}
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00056344 File Offset: 0x00054544
		public virtual void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.actAsInitiator)
			{
				foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in this.channelSupportingTokenAuthenticatorSpecification)
				{
					SecurityUtils.CloseTokenAuthenticatorIfRequired(supportingTokenAuthenticatorSpecification.TokenAuthenticator, timeoutHelper.RemainingTime());
				}
				foreach (string key in this.scopedSupportingTokenAuthenticatorSpecification.Keys)
				{
					ICollection<SupportingTokenAuthenticatorSpecification> collection = this.scopedSupportingTokenAuthenticatorSpecification[key];
					foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification2 in collection)
					{
						SecurityUtils.CloseTokenAuthenticatorIfRequired(supportingTokenAuthenticatorSpecification2.TokenAuthenticator, timeoutHelper.RemainingTime());
					}
				}
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0005644C File Offset: 0x0005464C
		public virtual object CreateListenerSecurityState()
		{
			return null;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00056450 File Offset: 0x00054650
		public SecurityProtocol CreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, bool isReturnLegSecurityRequired, TimeSpan timeout)
		{
			this.ThrowIfNotOpen();
			SecurityProtocol securityProtocol = this.OnCreateSecurityProtocol(target, via, listenerSecurityState, timeout);
			if (securityProtocol == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("ProtocolFactoryCouldNotCreateProtocol")));
			}
			return securityProtocol;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0005648D File Offset: 0x0005468D
		public virtual EndpointIdentity GetIdentityOfSelf()
		{
			return null;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00056490 File Offset: 0x00054690
		public virtual T GetProperty<T>()
		{
			if (typeof(T) == typeof(Collection<ISecurityContextSecurityTokenCache>))
			{
				this.ThrowIfNotOpen();
				Collection<ISecurityContextSecurityTokenCache> collection = new Collection<ISecurityContextSecurityTokenCache>();
				if (this.channelSupportingTokenAuthenticatorSpecification != null)
				{
					foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in this.channelSupportingTokenAuthenticatorSpecification)
					{
						if (supportingTokenAuthenticatorSpecification.TokenAuthenticator is ISecurityContextSecurityTokenCacheProvider)
						{
							collection.Add(((ISecurityContextSecurityTokenCacheProvider)supportingTokenAuthenticatorSpecification.TokenAuthenticator).TokenCache);
						}
					}
				}
				return (T)((object)collection);
			}
			return default(T);
		}

		// Token: 0x060016DE RID: 5854
		protected abstract SecurityProtocol OnCreateSecurityProtocol(EndpointAddress target, Uri via, object listenerSecurityState, TimeSpan timeout);

		// Token: 0x060016DF RID: 5855 RVA: 0x00056538 File Offset: 0x00054738
		private void VerifyTypeUniqueness(ICollection<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators)
		{
			foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in supportingTokenAuthenticators)
			{
				Type type = supportingTokenAuthenticatorSpecification.TokenAuthenticator.GetType();
				int num = 0;
				foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification2 in supportingTokenAuthenticators)
				{
					Type type2 = supportingTokenAuthenticatorSpecification2.TokenAuthenticator.GetType();
					if (supportingTokenAuthenticatorSpecification == supportingTokenAuthenticatorSpecification2)
					{
						if (num > 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MultipleSupportingAuthenticatorsOfSameType", new object[]
							{
								supportingTokenAuthenticatorSpecification.TokenParameters.GetType()
							})));
						}
						num++;
					}
					else if (type.IsAssignableFrom(type2) || type2.IsAssignableFrom(type))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MultipleSupportingAuthenticatorsOfSameType", new object[]
						{
							supportingTokenAuthenticatorSpecification.TokenParameters.GetType()
						})));
					}
				}
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00056658 File Offset: 0x00054858
		internal IList<SupportingTokenAuthenticatorSpecification> GetSupportingTokenAuthenticators(string action, out bool expectSignedTokens, out bool expectBasicTokens, out bool expectEndorsingTokens)
		{
			if (this.mergedSupportingTokenAuthenticatorsMap != null && this.mergedSupportingTokenAuthenticatorsMap.Count > 0)
			{
				if (action != null && this.mergedSupportingTokenAuthenticatorsMap.ContainsKey(action))
				{
					MergedSupportingTokenAuthenticatorSpecification mergedSupportingTokenAuthenticatorSpecification = this.mergedSupportingTokenAuthenticatorsMap[action];
					expectSignedTokens = mergedSupportingTokenAuthenticatorSpecification.ExpectSignedTokens;
					expectBasicTokens = mergedSupportingTokenAuthenticatorSpecification.ExpectBasicTokens;
					expectEndorsingTokens = mergedSupportingTokenAuthenticatorSpecification.ExpectEndorsingTokens;
					return mergedSupportingTokenAuthenticatorSpecification.SupportingTokenAuthenticators;
				}
				if (this.mergedSupportingTokenAuthenticatorsMap.ContainsKey("*"))
				{
					MergedSupportingTokenAuthenticatorSpecification mergedSupportingTokenAuthenticatorSpecification2 = this.mergedSupportingTokenAuthenticatorsMap["*"];
					expectSignedTokens = mergedSupportingTokenAuthenticatorSpecification2.ExpectSignedTokens;
					expectBasicTokens = mergedSupportingTokenAuthenticatorSpecification2.ExpectBasicTokens;
					expectEndorsingTokens = mergedSupportingTokenAuthenticatorSpecification2.ExpectEndorsingTokens;
					return mergedSupportingTokenAuthenticatorSpecification2.SupportingTokenAuthenticators;
				}
			}
			expectSignedTokens = this.expectChannelSignedTokens;
			expectBasicTokens = this.expectChannelBasicTokens;
			expectEndorsingTokens = this.expectChannelEndorsingTokens;
			if (this.channelSupportingTokenAuthenticatorSpecification != SecurityProtocolFactory.EmptyTokenAuthenticators)
			{
				return (IList<SupportingTokenAuthenticatorSpecification>)this.channelSupportingTokenAuthenticatorSpecification;
			}
			return null;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00056738 File Offset: 0x00054938
		private void MergeSupportingTokenAuthenticators(TimeSpan timeout)
		{
			if (this.scopedSupportingTokenAuthenticatorSpecification.Count == 0)
			{
				this.mergedSupportingTokenAuthenticatorsMap = null;
				return;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.expectSupportingTokens = true;
			this.mergedSupportingTokenAuthenticatorsMap = new Dictionary<string, MergedSupportingTokenAuthenticatorSpecification>();
			foreach (string key in this.scopedSupportingTokenAuthenticatorSpecification.Keys)
			{
				ICollection<SupportingTokenAuthenticatorSpecification> collection = this.scopedSupportingTokenAuthenticatorSpecification[key];
				if (collection != null && collection.Count != 0)
				{
					Collection<SupportingTokenAuthenticatorSpecification> collection2 = new Collection<SupportingTokenAuthenticatorSpecification>();
					bool expectSignedTokens = this.expectChannelSignedTokens;
					bool expectBasicTokens = this.expectChannelBasicTokens;
					bool expectEndorsingTokens = this.expectChannelEndorsingTokens;
					foreach (SupportingTokenAuthenticatorSpecification item in this.channelSupportingTokenAuthenticatorSpecification)
					{
						collection2.Add(item);
					}
					foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in collection)
					{
						SecurityUtils.OpenTokenAuthenticatorIfRequired(supportingTokenAuthenticatorSpecification.TokenAuthenticator, timeoutHelper.RemainingTime());
						collection2.Add(supportingTokenAuthenticatorSpecification);
						if ((supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing) && supportingTokenAuthenticatorSpecification.TokenParameters.RequireDerivedKeys && !supportingTokenAuthenticatorSpecification.TokenParameters.HasAsymmetricKey)
						{
							this.expectKeyDerivation = true;
						}
						SecurityTokenAttachmentMode securityTokenAttachmentMode = supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode;
						if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEncrypted || securityTokenAttachmentMode == SecurityTokenAttachmentMode.Signed || securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
						{
							expectSignedTokens = true;
							if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEncrypted)
							{
								expectBasicTokens = true;
							}
						}
						if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
						{
							expectEndorsingTokens = true;
						}
					}
					this.VerifyTypeUniqueness(collection2);
					MergedSupportingTokenAuthenticatorSpecification value = default(MergedSupportingTokenAuthenticatorSpecification);
					value.SupportingTokenAuthenticators = collection2;
					value.ExpectBasicTokens = expectBasicTokens;
					value.ExpectEndorsingTokens = expectEndorsingTokens;
					value.ExpectSignedTokens = expectSignedTokens;
					this.mergedSupportingTokenAuthenticatorsMap.Add(key, value);
				}
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00056964 File Offset: 0x00054B64
		protected RecipientServiceModelSecurityTokenRequirement CreateRecipientSecurityTokenRequirement()
		{
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = new RecipientServiceModelSecurityTokenRequirement();
			recipientServiceModelSecurityTokenRequirement.SecurityBindingElement = this.securityBindingElement;
			recipientServiceModelSecurityTokenRequirement.SecurityAlgorithmSuite = this.IncomingAlgorithmSuite;
			recipientServiceModelSecurityTokenRequirement.ListenUri = this.listenUri;
			recipientServiceModelSecurityTokenRequirement.MessageSecurityVersion = this.MessageSecurityVersion.SecurityTokenVersion;
			recipientServiceModelSecurityTokenRequirement.AuditLogLocation = this.auditLogLocation;
			recipientServiceModelSecurityTokenRequirement.SuppressAuditFailure = this.suppressAuditFailure;
			recipientServiceModelSecurityTokenRequirement.MessageAuthenticationAuditLevel = this.messageAuthenticationAuditLevel;
			recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ExtendedProtectionPolicy] = this.extendedProtectionPolicy;
			if (this.endpointFilterTable != null)
			{
				recipientServiceModelSecurityTokenRequirement.Properties.Add(ServiceModelSecurityTokenRequirement.EndpointFilterTableProperty, this.endpointFilterTable);
			}
			return recipientServiceModelSecurityTokenRequirement;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00056A08 File Offset: 0x00054C08
		private RecipientServiceModelSecurityTokenRequirement CreateRecipientSecurityTokenRequirement(SecurityTokenParameters parameters, SecurityTokenAttachmentMode attachmentMode)
		{
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = this.CreateRecipientSecurityTokenRequirement();
			parameters.InitializeSecurityTokenRequirement(recipientServiceModelSecurityTokenRequirement);
			recipientServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
			recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Input;
			recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.SupportingTokenAttachmentModeProperty] = attachmentMode;
			recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ExtendedProtectionPolicy] = this.extendedProtectionPolicy;
			return recipientServiceModelSecurityTokenRequirement;
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00056A70 File Offset: 0x00054C70
		private void AddSupportingTokenAuthenticators(SupportingTokenParameters supportingTokenParameters, bool isOptional, IList<SupportingTokenAuthenticatorSpecification> authenticatorSpecList)
		{
			for (int i = 0; i < supportingTokenParameters.Endorsing.Count; i++)
			{
				SecurityTokenRequirement tokenRequirement = this.CreateRecipientSecurityTokenRequirement(supportingTokenParameters.Endorsing[i], SecurityTokenAttachmentMode.Endorsing);
				try
				{
					SecurityTokenResolver securityTokenResolver;
					SecurityTokenAuthenticator tokenAuthenticator = this.SecurityTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement, out securityTokenResolver);
					SupportingTokenAuthenticatorSpecification item = new SupportingTokenAuthenticatorSpecification(tokenAuthenticator, securityTokenResolver, SecurityTokenAttachmentMode.Endorsing, supportingTokenParameters.Endorsing[i], isOptional);
					authenticatorSpecList.Add(item);
				}
				catch (Exception exception)
				{
					if (!isOptional || Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
			for (int j = 0; j < supportingTokenParameters.SignedEndorsing.Count; j++)
			{
				SecurityTokenRequirement tokenRequirement2 = this.CreateRecipientSecurityTokenRequirement(supportingTokenParameters.SignedEndorsing[j], SecurityTokenAttachmentMode.SignedEndorsing);
				try
				{
					SecurityTokenResolver securityTokenResolver2;
					SecurityTokenAuthenticator tokenAuthenticator2 = this.SecurityTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement2, out securityTokenResolver2);
					SupportingTokenAuthenticatorSpecification item2 = new SupportingTokenAuthenticatorSpecification(tokenAuthenticator2, securityTokenResolver2, SecurityTokenAttachmentMode.SignedEndorsing, supportingTokenParameters.SignedEndorsing[j], isOptional);
					authenticatorSpecList.Add(item2);
				}
				catch (Exception exception2)
				{
					if (!isOptional || Fx.IsFatal(exception2))
					{
						throw;
					}
				}
			}
			for (int k = 0; k < supportingTokenParameters.SignedEncrypted.Count; k++)
			{
				SecurityTokenRequirement tokenRequirement3 = this.CreateRecipientSecurityTokenRequirement(supportingTokenParameters.SignedEncrypted[k], SecurityTokenAttachmentMode.SignedEncrypted);
				try
				{
					SecurityTokenResolver securityTokenResolver3;
					SecurityTokenAuthenticator tokenAuthenticator3 = this.SecurityTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement3, out securityTokenResolver3);
					SupportingTokenAuthenticatorSpecification item3 = new SupportingTokenAuthenticatorSpecification(tokenAuthenticator3, securityTokenResolver3, SecurityTokenAttachmentMode.SignedEncrypted, supportingTokenParameters.SignedEncrypted[k], isOptional);
					authenticatorSpecList.Add(item3);
				}
				catch (Exception exception3)
				{
					if (!isOptional || Fx.IsFatal(exception3))
					{
						throw;
					}
				}
			}
			for (int l = 0; l < supportingTokenParameters.Signed.Count; l++)
			{
				SecurityTokenRequirement tokenRequirement4 = this.CreateRecipientSecurityTokenRequirement(supportingTokenParameters.Signed[l], SecurityTokenAttachmentMode.Signed);
				try
				{
					SecurityTokenResolver securityTokenResolver4;
					SecurityTokenAuthenticator tokenAuthenticator4 = this.SecurityTokenManager.CreateSecurityTokenAuthenticator(tokenRequirement4, out securityTokenResolver4);
					SupportingTokenAuthenticatorSpecification item4 = new SupportingTokenAuthenticatorSpecification(tokenAuthenticator4, securityTokenResolver4, SecurityTokenAttachmentMode.Signed, supportingTokenParameters.Signed[l], isOptional);
					authenticatorSpecList.Add(item4);
				}
				catch (Exception exception4)
				{
					if (!isOptional || Fx.IsFatal(exception4))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00056C84 File Offset: 0x00054E84
		public virtual void OnOpen(TimeSpan timeout)
		{
			if (this.SecurityBindingElement == null)
			{
				this.OnPropertySettingsError("SecurityBindingElement", true);
			}
			if (this.SecurityTokenManager == null)
			{
				this.OnPropertySettingsError("SecurityTokenManager", true);
			}
			this.messageSecurityVersion = this.standardsManager.MessageSecurityVersion;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.expectOutgoingMessages = (this.ActAsInitiator || this.SupportsRequestReply);
			this.expectIncomingMessages = (!this.ActAsInitiator || this.SupportsRequestReply);
			if (!this.actAsInitiator)
			{
				this.AddSupportingTokenAuthenticators(this.securityBindingElement.EndpointSupportingTokenParameters, false, (IList<SupportingTokenAuthenticatorSpecification>)this.channelSupportingTokenAuthenticatorSpecification);
				this.AddSupportingTokenAuthenticators(this.securityBindingElement.OptionalEndpointSupportingTokenParameters, true, (IList<SupportingTokenAuthenticatorSpecification>)this.channelSupportingTokenAuthenticatorSpecification);
				foreach (string key in this.securityBindingElement.OperationSupportingTokenParameters.Keys)
				{
					Collection<SupportingTokenAuthenticatorSpecification> collection = new Collection<SupportingTokenAuthenticatorSpecification>();
					this.AddSupportingTokenAuthenticators(this.securityBindingElement.OperationSupportingTokenParameters[key], false, collection);
					this.scopedSupportingTokenAuthenticatorSpecification.Add(key, collection);
				}
				foreach (string key2 in this.securityBindingElement.OptionalOperationSupportingTokenParameters.Keys)
				{
					ICollection<SupportingTokenAuthenticatorSpecification> collection2;
					Collection<SupportingTokenAuthenticatorSpecification> collection3;
					if (this.scopedSupportingTokenAuthenticatorSpecification.TryGetValue(key2, out collection2))
					{
						collection3 = (Collection<SupportingTokenAuthenticatorSpecification>)collection2;
					}
					else
					{
						collection3 = new Collection<SupportingTokenAuthenticatorSpecification>();
						this.scopedSupportingTokenAuthenticatorSpecification.Add(key2, collection3);
					}
					this.AddSupportingTokenAuthenticators(this.securityBindingElement.OptionalOperationSupportingTokenParameters[key2], true, collection3);
				}
				if (!this.channelSupportingTokenAuthenticatorSpecification.IsReadOnly)
				{
					if (this.channelSupportingTokenAuthenticatorSpecification.Count == 0)
					{
						this.channelSupportingTokenAuthenticatorSpecification = SecurityProtocolFactory.EmptyTokenAuthenticators;
					}
					else
					{
						this.expectSupportingTokens = true;
						foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in this.channelSupportingTokenAuthenticatorSpecification)
						{
							SecurityUtils.OpenTokenAuthenticatorIfRequired(supportingTokenAuthenticatorSpecification.TokenAuthenticator, timeoutHelper.RemainingTime());
							if ((supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing) && supportingTokenAuthenticatorSpecification.TokenParameters.RequireDerivedKeys && !supportingTokenAuthenticatorSpecification.TokenParameters.HasAsymmetricKey)
							{
								this.expectKeyDerivation = true;
							}
							SecurityTokenAttachmentMode securityTokenAttachmentMode = supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode;
							if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEncrypted || securityTokenAttachmentMode == SecurityTokenAttachmentMode.Signed || securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
							{
								this.expectChannelSignedTokens = true;
								if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEncrypted)
								{
									this.expectChannelBasicTokens = true;
								}
							}
							if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
							{
								this.expectChannelEndorsingTokens = true;
							}
						}
						this.channelSupportingTokenAuthenticatorSpecification = new ReadOnlyCollection<SupportingTokenAuthenticatorSpecification>((Collection<SupportingTokenAuthenticatorSpecification>)this.channelSupportingTokenAuthenticatorSpecification);
					}
				}
				this.VerifyTypeUniqueness(this.channelSupportingTokenAuthenticatorSpecification);
				this.MergeSupportingTokenAuthenticators(timeoutHelper.RemainingTime());
			}
			if (this.DetectReplays)
			{
				if (!this.SupportsReplayDetection)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("DetectReplays", SR.GetString("SecurityProtocolCannotDoReplayDetection", new object[]
					{
						this
					}));
				}
				if (this.MaxClockSkew == TimeSpan.MaxValue || this.ReplayWindow == TimeSpan.MaxValue)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoncesCachedInfinitely")));
				}
				if (this.nonceCache == null)
				{
					this.nonceCache = new InMemoryNonceCache(this.ReplayWindow + this.MaxClockSkew + this.MaxClockSkew, this.MaxCachedNonces);
				}
			}
			this.derivedKeyTokenAuthenticator = new NonValidatingSecurityTokenAuthenticator<DerivedKeySecurityToken>();
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00057024 File Offset: 0x00055224
		public void Open(bool actAsInitiator, TimeSpan timeout)
		{
			this.actAsInitiator = actAsInitiator;
			this.communicationObject.Open(timeout);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00057039 File Offset: 0x00055239
		public IAsyncResult BeginOpen(bool actAsInitiator, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.actAsInitiator = actAsInitiator;
			return this.CommunicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00057051 File Offset: 0x00055251
		public void EndOpen(IAsyncResult result)
		{
			this.CommunicationObject.EndOpen(result);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0005705F File Offset: 0x0005525F
		public void Close(bool aborted, TimeSpan timeout)
		{
			if (aborted)
			{
				this.CommunicationObject.Abort();
				return;
			}
			this.CommunicationObject.Close(timeout);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0005707C File Offset: 0x0005527C
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.CommunicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0005708C File Offset: 0x0005528C
		public void EndClose(IAsyncResult result)
		{
			this.CommunicationObject.EndClose(result);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0005709A File Offset: 0x0005529A
		internal void Open(string propertyName, bool requiredForForwardDirection, SecurityTokenAuthenticator authenticator, TimeSpan timeout)
		{
			if (authenticator != null)
			{
				SecurityUtils.OpenTokenAuthenticatorIfRequired(authenticator, timeout);
				return;
			}
			this.OnPropertySettingsError(propertyName, requiredForForwardDirection);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x000570B0 File Offset: 0x000552B0
		internal void Open(string propertyName, bool requiredForForwardDirection, SecurityTokenProvider provider, TimeSpan timeout)
		{
			if (provider != null)
			{
				SecurityUtils.OpenTokenProviderIfRequired(provider, timeout);
				return;
			}
			this.OnPropertySettingsError(propertyName, requiredForForwardDirection);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x000570C6 File Offset: 0x000552C6
		internal void OnPropertySettingsError(string propertyName, bool requiredForForwardDirection)
		{
			if (requiredForForwardDirection)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PropertySettingErrorOnProtocolFactory", new object[]
				{
					propertyName,
					this
				}), propertyName));
			}
			if (this.requestReplyErrorPropertyName == null)
			{
				this.requestReplyErrorPropertyName = propertyName;
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00057103 File Offset: 0x00055303
		private void ThrowIfReturnDirectionSecurityNotSupported()
		{
			if (this.requestReplyErrorPropertyName != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PropertySettingErrorOnProtocolFactory", new object[]
				{
					this.requestReplyErrorPropertyName,
					this
				}), this.requestReplyErrorPropertyName));
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00057140 File Offset: 0x00055340
		internal void ThrowIfImmutable()
		{
			this.communicationObject.ThrowIfDisposedOrImmutable();
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0005714D File Offset: 0x0005534D
		private void ThrowIfNotOpen()
		{
			this.communicationObject.ThrowIfNotOpened();
		}

		// Token: 0x04001BC5 RID: 7109
		internal const bool defaultAddTimestamp = true;

		// Token: 0x04001BC6 RID: 7110
		internal const bool defaultDeriveKeys = true;

		// Token: 0x04001BC7 RID: 7111
		internal const bool defaultDetectReplays = true;

		// Token: 0x04001BC8 RID: 7112
		internal const string defaultMaxClockSkewString = "00:05:00";

		// Token: 0x04001BC9 RID: 7113
		internal const string defaultReplayWindowString = "00:05:00";

		// Token: 0x04001BCA RID: 7114
		internal static readonly TimeSpan defaultMaxClockSkew = TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture);

		// Token: 0x04001BCB RID: 7115
		internal static readonly TimeSpan defaultReplayWindow = TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture);

		// Token: 0x04001BCC RID: 7116
		internal const int defaultMaxCachedNonces = 900000;

		// Token: 0x04001BCD RID: 7117
		internal const string defaultTimestampValidityDurationString = "00:05:00";

		// Token: 0x04001BCE RID: 7118
		internal static readonly TimeSpan defaultTimestampValidityDuration = TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture);

		// Token: 0x04001BCF RID: 7119
		internal const SecurityHeaderLayout defaultSecurityHeaderLayout = SecurityHeaderLayout.Strict;

		// Token: 0x04001BD0 RID: 7120
		private static ReadOnlyCollection<SupportingTokenAuthenticatorSpecification> emptyTokenAuthenticators;

		// Token: 0x04001BD1 RID: 7121
		private bool actAsInitiator;

		// Token: 0x04001BD2 RID: 7122
		private bool isDuplexReply;

		// Token: 0x04001BD3 RID: 7123
		private bool addTimestamp = true;

		// Token: 0x04001BD4 RID: 7124
		private bool detectReplays = true;

		// Token: 0x04001BD5 RID: 7125
		private bool expectIncomingMessages;

		// Token: 0x04001BD6 RID: 7126
		private bool expectOutgoingMessages;

		// Token: 0x04001BD7 RID: 7127
		private SecurityAlgorithmSuite incomingAlgorithmSuite = SecurityAlgorithmSuite.Default;

		// Token: 0x04001BD8 RID: 7128
		private ICollection<SupportingTokenAuthenticatorSpecification> channelSupportingTokenAuthenticatorSpecification;

		// Token: 0x04001BD9 RID: 7129
		private Dictionary<string, ICollection<SupportingTokenAuthenticatorSpecification>> scopedSupportingTokenAuthenticatorSpecification;

		// Token: 0x04001BDA RID: 7130
		private Dictionary<string, MergedSupportingTokenAuthenticatorSpecification> mergedSupportingTokenAuthenticatorsMap;

		// Token: 0x04001BDB RID: 7131
		private int maxCachedNonces = 900000;

		// Token: 0x04001BDC RID: 7132
		private TimeSpan maxClockSkew = SecurityProtocolFactory.defaultMaxClockSkew;

		// Token: 0x04001BDD RID: 7133
		private NonceCache nonceCache;

		// Token: 0x04001BDE RID: 7134
		private SecurityAlgorithmSuite outgoingAlgorithmSuite = SecurityAlgorithmSuite.Default;

		// Token: 0x04001BDF RID: 7135
		private TimeSpan replayWindow = SecurityProtocolFactory.defaultReplayWindow;

		// Token: 0x04001BE0 RID: 7136
		private SecurityStandardsManager standardsManager = SecurityStandardsManager.DefaultInstance;

		// Token: 0x04001BE1 RID: 7137
		private SecurityTokenManager securityTokenManager;

		// Token: 0x04001BE2 RID: 7138
		private SecurityBindingElement securityBindingElement;

		// Token: 0x04001BE3 RID: 7139
		private string requestReplyErrorPropertyName;

		// Token: 0x04001BE4 RID: 7140
		private NonValidatingSecurityTokenAuthenticator<DerivedKeySecurityToken> derivedKeyTokenAuthenticator;

		// Token: 0x04001BE5 RID: 7141
		private TimeSpan timestampValidityDuration = SecurityProtocolFactory.defaultTimestampValidityDuration;

		// Token: 0x04001BE6 RID: 7142
		private AuditLogLocation auditLogLocation;

		// Token: 0x04001BE7 RID: 7143
		private bool suppressAuditFailure;

		// Token: 0x04001BE8 RID: 7144
		private SecurityHeaderLayout securityHeaderLayout;

		// Token: 0x04001BE9 RID: 7145
		private AuditLevel serviceAuthorizationAuditLevel;

		// Token: 0x04001BEA RID: 7146
		private AuditLevel messageAuthenticationAuditLevel;

		// Token: 0x04001BEB RID: 7147
		private bool expectKeyDerivation;

		// Token: 0x04001BEC RID: 7148
		private bool expectChannelBasicTokens;

		// Token: 0x04001BED RID: 7149
		private bool expectChannelSignedTokens;

		// Token: 0x04001BEE RID: 7150
		private bool expectChannelEndorsingTokens;

		// Token: 0x04001BEF RID: 7151
		private bool expectSupportingTokens;

		// Token: 0x04001BF0 RID: 7152
		private Uri listenUri;

		// Token: 0x04001BF1 RID: 7153
		private MessageSecurityVersion messageSecurityVersion;

		// Token: 0x04001BF2 RID: 7154
		private WrapperSecurityCommunicationObject communicationObject;

		// Token: 0x04001BF3 RID: 7155
		private Uri privacyNoticeUri;

		// Token: 0x04001BF4 RID: 7156
		private int privacyNoticeVersion;

		// Token: 0x04001BF5 RID: 7157
		private IMessageFilterTable<EndpointAddress> endpointFilterTable;

		// Token: 0x04001BF6 RID: 7158
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x04001BF7 RID: 7159
		private BufferManager streamBufferManager;
	}
}
