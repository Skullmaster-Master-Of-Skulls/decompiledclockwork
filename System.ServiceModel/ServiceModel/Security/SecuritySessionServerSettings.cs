using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F2 RID: 754
	internal sealed class SecuritySessionServerSettings : IListenerSecureConversationSessionSettings, ISecurityCommunicationObject
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x0005CD50 File Offset: 0x0005AF50
		public SecuritySessionServerSettings()
		{
			this.activeSessions = new Dictionary<UniqueId, SecuritySessionServerSettings.IServerSecuritySessionChannel>();
			this.maximumKeyRenewalInterval = SecuritySessionServerSettings.defaultKeyRenewalInterval;
			this.maximumPendingKeysPerSession = 5;
			this.keyRolloverInterval = SecuritySessionServerSettings.defaultKeyRolloverInterval;
			this.inactivityTimeout = SecuritySessionServerSettings.defaultInactivityTimeout;
			this.tolerateTransportFailures = true;
			this.maximumPendingSessions = 128;
			this.communicationObject = new WrapperSecurityCommunicationObject(this);
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0005CDC6 File Offset: 0x0005AFC6
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x0005CDCE File Offset: 0x0005AFCE
		internal ChannelBuilder ChannelBuilder
		{
			get
			{
				return this.channelBuilder;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.channelBuilder = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0005CDE2 File Offset: 0x0005AFE2
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0005CDEA File Offset: 0x0005AFEA
		internal SecurityListenerSettingsLifetimeManager SettingsLifetimeManager
		{
			get
			{
				return this.settingsLifetimeManager;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.settingsLifetimeManager = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0005CDFE File Offset: 0x0005AFFE
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x0005CE06 File Offset: 0x0005B006
		internal ChannelListenerBase SecurityChannelListener
		{
			get
			{
				return this.securityChannelListener;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.securityChannelListener = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0005CE1A File Offset: 0x0005B01A
		private Uri Uri
		{
			get
			{
				this.communicationObject.ThrowIfNotOpened();
				return this.listenUri;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0005CE2D File Offset: 0x0005B02D
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0005CE35 File Offset: 0x0005B035
		public SecurityTokenAuthenticator SessionTokenAuthenticator
		{
			get
			{
				return this.sessionTokenAuthenticator;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0005CE3D File Offset: 0x0005B03D
		public ISecurityContextSecurityTokenCache SessionTokenCache
		{
			get
			{
				return this.sessionTokenCache;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0005CE45 File Offset: 0x0005B045
		public SecurityTokenResolver SessionTokenResolver
		{
			get
			{
				return this.sessionTokenResolver;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005CE4D File Offset: 0x0005B04D
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x0005CE55 File Offset: 0x0005B055
		public SecurityTokenParameters IssuedSecurityTokenParameters
		{
			get
			{
				return this.issuedTokenParameters;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.issuedTokenParameters = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0005CE69 File Offset: 0x0005B069
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0005CE71 File Offset: 0x0005B071
		public SecurityStandardsManager SecurityStandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.standardsManager = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0005CE85 File Offset: 0x0005B085
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x0005CE8D File Offset: 0x0005B08D
		public bool TolerateTransportFailures
		{
			get
			{
				return this.tolerateTransportFailures;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.tolerateTransportFailures = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0005CEA1 File Offset: 0x0005B0A1
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x0005CEA9 File Offset: 0x0005B0A9
		public bool CanRenewSession
		{
			get
			{
				return this.canRenewSession;
			}
			set
			{
				this.canRenewSession = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0005CEB2 File Offset: 0x0005B0B2
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x0005CEBA File Offset: 0x0005B0BA
		public int MaximumPendingSessions
		{
			get
			{
				return this.maximumPendingSessions;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.maximumPendingSessions = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0005CEE7 File Offset: 0x0005B0E7
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x0005CEEF File Offset: 0x0005B0EF
		public TimeSpan InactivityTimeout
		{
			get
			{
				return this.inactivityTimeout;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.inactivityTimeout = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005CF2F File Offset: 0x0005B12F
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0005CF37 File Offset: 0x0005B137
		public TimeSpan MaximumKeyRenewalInterval
		{
			get
			{
				return this.maximumKeyRenewalInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.maximumKeyRenewalInterval = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005CF77 File Offset: 0x0005B177
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x0005CF7F File Offset: 0x0005B17F
		public TimeSpan KeyRolloverInterval
		{
			get
			{
				return this.keyRolloverInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.keyRolloverInterval = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0005CFBF File Offset: 0x0005B1BF
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x0005CFC7 File Offset: 0x0005B1C7
		public int MaximumPendingKeysPerSession
		{
			get
			{
				return this.maximumPendingKeysPerSession;
			}
			set
			{
				if (value <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeGreaterThanZero")));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.maximumPendingKeysPerSession = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0005CFFE File Offset: 0x0005B1FE
		// (set) Token: 0x06001926 RID: 6438 RVA: 0x0005D006 File Offset: 0x0005B206
		public SecurityProtocolFactory SessionProtocolFactory
		{
			get
			{
				return this.sessionProtocolFactory;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.sessionProtocolFactory = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0005D01A File Offset: 0x0005B21A
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0005D022 File Offset: 0x0005B222
		public TimeSpan OpenTimeout
		{
			get
			{
				return this.openTimeout;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0005D02A File Offset: 0x0005B22A
		public TimeSpan CloseTimeout
		{
			get
			{
				return this.closeTimeout;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0005D032 File Offset: 0x0005B232
		public TimeSpan SendTimeout
		{
			get
			{
				return this.sendTimeout;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0005D03A File Offset: 0x0005B23A
		public TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0005D041 File Offset: 0x0005B241
		public TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0005D048 File Offset: 0x0005B248
		public IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnClose), timeout, callback, state);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0005D05F File Offset: 0x0005B25F
		public IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0005D076 File Offset: 0x0005B276
		public void OnClosed()
		{
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0005D078 File Offset: 0x0005B278
		public void OnClosing()
		{
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0005D07A File Offset: 0x0005B27A
		public void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0005D082 File Offset: 0x0005B282
		public void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0005D08A File Offset: 0x0005B28A
		public void OnFaulted()
		{
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005D08C File Offset: 0x0005B28C
		public void OnOpened()
		{
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0005D08E File Offset: 0x0005B28E
		public void OnOpening()
		{
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0005D090 File Offset: 0x0005B290
		public void OnAbort()
		{
			this.AbortPendingChannels();
			this.OnAbortCore();
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0005D0A0 File Offset: 0x0005B2A0
		public void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.ClosePendingChannels(timeoutHelper.RemainingTime());
			this.OnCloseCore(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0005D0CF File Offset: 0x0005B2CF
		internal void Close(TimeSpan timeout)
		{
			this.communicationObject.Close(timeout);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0005D0DD File Offset: 0x0005B2DD
		internal IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0005D0ED File Offset: 0x0005B2ED
		internal void EndClose(IAsyncResult result)
		{
			this.communicationObject.EndClose(result);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0005D0FB File Offset: 0x0005B2FB
		internal void Abort()
		{
			this.communicationObject.Abort();
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0005D108 File Offset: 0x0005B308
		internal void Open(TimeSpan timeout)
		{
			this.communicationObject.Open(timeout);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0005D116 File Offset: 0x0005B316
		internal IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0005D126 File Offset: 0x0005B326
		internal void EndOpen(IAsyncResult result)
		{
			this.communicationObject.EndOpen(result);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0005D134 File Offset: 0x0005B334
		private void OnCloseCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.inactivityTimer != null)
			{
				this.inactivityTimer.Cancel();
			}
			if (this.sessionProtocolFactory != null)
			{
				this.sessionProtocolFactory.Close(false, timeoutHelper.RemainingTime());
			}
			if (this.sessionTokenAuthenticator != null)
			{
				SecurityUtils.CloseTokenAuthenticatorIfRequired(this.sessionTokenAuthenticator, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0005D194 File Offset: 0x0005B394
		private void OnAbortCore()
		{
			if (this.inactivityTimer != null)
			{
				this.inactivityTimer.Cancel();
			}
			if (this.sessionProtocolFactory != null)
			{
				this.sessionProtocolFactory.Close(true, TimeSpan.Zero);
			}
			if (this.sessionTokenAuthenticator != null)
			{
				SecurityUtils.AbortTokenAuthenticatorIfRequired(this.sessionTokenAuthenticator);
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0005D1E4 File Offset: 0x0005B3E4
		private void SetupSessionTokenAuthenticator()
		{
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = new RecipientServiceModelSecurityTokenRequirement();
			this.issuedTokenParameters.InitializeSecurityTokenRequirement(recipientServiceModelSecurityTokenRequirement);
			recipientServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
			recipientServiceModelSecurityTokenRequirement.ListenUri = this.listenUri;
			recipientServiceModelSecurityTokenRequirement.SecurityBindingElement = this.sessionProtocolFactory.SecurityBindingElement;
			recipientServiceModelSecurityTokenRequirement.SecurityAlgorithmSuite = this.sessionProtocolFactory.IncomingAlgorithmSuite;
			recipientServiceModelSecurityTokenRequirement.SupportSecurityContextCancellation = true;
			recipientServiceModelSecurityTokenRequirement.MessageSecurityVersion = this.sessionProtocolFactory.MessageSecurityVersion.SecurityTokenVersion;
			recipientServiceModelSecurityTokenRequirement.AuditLogLocation = this.sessionProtocolFactory.AuditLogLocation;
			recipientServiceModelSecurityTokenRequirement.SuppressAuditFailure = this.sessionProtocolFactory.SuppressAuditFailure;
			recipientServiceModelSecurityTokenRequirement.MessageAuthenticationAuditLevel = this.sessionProtocolFactory.MessageAuthenticationAuditLevel;
			recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Input;
			if (this.sessionProtocolFactory.EndpointFilterTable != null)
			{
				recipientServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.EndpointFilterTableProperty] = this.sessionProtocolFactory.EndpointFilterTable;
			}
			this.sessionTokenAuthenticator = this.sessionProtocolFactory.SecurityTokenManager.CreateSecurityTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, out this.sessionTokenResolver);
			if (!(this.sessionTokenAuthenticator is IIssuanceSecurityTokenAuthenticator))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionRequiresIssuanceAuthenticator", new object[]
				{
					typeof(IIssuanceSecurityTokenAuthenticator),
					this.sessionTokenAuthenticator.GetType()
				})));
			}
			if (this.sessionTokenResolver == null || !(this.sessionTokenResolver is ISecurityContextSecurityTokenCache))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionRequiresSecurityContextTokenCache", new object[]
				{
					this.sessionTokenResolver.GetType(),
					typeof(ISecurityContextSecurityTokenCache)
				})));
			}
			this.sessionTokenCache = (ISecurityContextSecurityTokenCache)this.sessionTokenResolver;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0005D38C File Offset: 0x0005B58C
		public void OnOpen(TimeSpan timeout)
		{
			if (this.sessionProtocolFactory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionProtocolFactoryShouldBeSetBeforeThisOperation")));
			}
			if (this.standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityStandardsManagerNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.issuedTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuedSecurityTokenParametersNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.maximumKeyRenewalInterval < this.keyRolloverInterval)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("KeyRolloverGreaterThanKeyRenewal")));
			}
			if (this.securityChannelListener == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityChannelListenerNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.settingsLifetimeManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySettingsLifetimeManagerNotSet", new object[]
				{
					base.GetType()
				})));
			}
			this.messageVersion = this.channelBuilder.Binding.MessageVersion;
			this.listenUri = this.securityChannelListener.Uri;
			this.openTimeout = this.securityChannelListener.InternalOpenTimeout;
			this.closeTimeout = this.securityChannelListener.InternalCloseTimeout;
			this.sendTimeout = this.securityChannelListener.InternalSendTimeout;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.pendingSessions1 = new Dictionary<UniqueId, IServerReliableChannelBinder>();
			this.pendingSessions2 = new Dictionary<UniqueId, IServerReliableChannelBinder>();
			if (this.inactivityTimeout < TimeSpan.MaxValue)
			{
				this.inactivityTimer = new IOThreadTimer(new Action<object>(this.OnTimer), this, false);
				this.inactivityTimer.Set(this.inactivityTimeout);
			}
			this.ConfigureSessionSecurityProtocolFactory();
			this.sessionProtocolFactory.Open(false, timeoutHelper.RemainingTime());
			this.SetupSessionTokenAuthenticator();
			((IIssuanceSecurityTokenAuthenticator)this.sessionTokenAuthenticator).IssuedSecurityTokenHandler = new IssuedSecurityTokenHandler(this.OnTokenIssued);
			((IIssuanceSecurityTokenAuthenticator)this.sessionTokenAuthenticator).RenewedSecurityTokenHandler = new RenewedSecurityTokenHandler(this.OnTokenRenewed);
			this.acceptNewWork = true;
			SecurityUtils.OpenTokenAuthenticatorIfRequired(this.sessionTokenAuthenticator, timeoutHelper.RemainingTime());
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005D5D2 File Offset: 0x0005B7D2
		public void StopAcceptingNewWork()
		{
			this.acceptNewWork = false;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0005D5DD File Offset: 0x0005B7DD
		private int GetPendingSessionCount()
		{
			return this.pendingSessions1.Count + this.pendingSessions2.Count + ((SecuritySessionServerSettings.IInputQueueChannelAcceptor)this.channelAcceptor).PendingCount;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005D608 File Offset: 0x0005B808
		private void AbortPendingChannels()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.pendingSessions1 != null)
				{
					foreach (IServerReliableChannelBinder serverReliableChannelBinder in this.pendingSessions1.Values)
					{
						serverReliableChannelBinder.Abort();
					}
				}
				if (this.pendingSessions2 != null)
				{
					foreach (IServerReliableChannelBinder serverReliableChannelBinder2 in this.pendingSessions2.Values)
					{
						serverReliableChannelBinder2.Abort();
					}
				}
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0005D6E4 File Offset: 0x0005B8E4
		private void ClosePendingChannels(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			object obj = this.ThisLock;
			lock (obj)
			{
				foreach (IServerReliableChannelBinder serverReliableChannelBinder in this.pendingSessions1.Values)
				{
					serverReliableChannelBinder.Close(timeoutHelper.RemainingTime());
				}
				foreach (IServerReliableChannelBinder serverReliableChannelBinder2 in this.pendingSessions2.Values)
				{
					serverReliableChannelBinder2.Close(timeoutHelper.RemainingTime());
				}
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
		private void ConfigureSessionSecurityProtocolFactory()
		{
			if (this.sessionProtocolFactory is SessionSymmetricMessageSecurityProtocolFactory)
			{
				AddressingVersion addressing = MessageVersion.Default.Addressing;
				if (this.channelBuilder != null)
				{
					MessageEncodingBindingElement messageEncodingBindingElement = this.channelBuilder.Binding.Elements.Find<MessageEncodingBindingElement>();
					if (messageEncodingBindingElement != null)
					{
						addressing = messageEncodingBindingElement.MessageVersion.Addressing;
					}
				}
				if (addressing != AddressingVersion.WSAddressing10 && addressing != AddressingVersion.WSAddressingAugust2004)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
					{
						addressing
					})));
				}
				SessionSymmetricMessageSecurityProtocolFactory sessionSymmetricMessageSecurityProtocolFactory = (SessionSymmetricMessageSecurityProtocolFactory)this.sessionProtocolFactory;
				if (!sessionSymmetricMessageSecurityProtocolFactory.ApplyIntegrity || !sessionSymmetricMessageSecurityProtocolFactory.RequireIntegrity)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionRequiresMessageIntegrity")));
				}
				MessagePartSpecification parts = new MessagePartSpecification(true);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, addressing.FaultAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, addressing.DefaultFaultAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault");
				if (sessionSymmetricMessageSecurityProtocolFactory.ApplyConfidentiality)
				{
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, addressing.FaultAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, addressing.DefaultFaultAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault");
				}
				if (sessionSymmetricMessageSecurityProtocolFactory.RequireConfidentiality)
				{
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
				}
				sessionSymmetricMessageSecurityProtocolFactory.SecurityTokenParameters = this.IssuedSecurityTokenParameters;
				return;
			}
			else
			{
				if (this.sessionProtocolFactory is SessionSymmetricTransportSecurityProtocolFactory)
				{
					SessionSymmetricTransportSecurityProtocolFactory sessionSymmetricTransportSecurityProtocolFactory = (SessionSymmetricTransportSecurityProtocolFactory)this.sessionProtocolFactory;
					sessionSymmetricTransportSecurityProtocolFactory.AddTimestamp = true;
					sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters = this.IssuedSecurityTokenParameters;
					sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters.RequireDerivedKeys = false;
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0005DA98 File Offset: 0x0005BC98
		internal IChannelAcceptor<TChannel> CreateAcceptor<TChannel>() where TChannel : class, IChannel
		{
			if (this.channelAcceptor != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SSSSCreateAcceptor")));
			}
			object listenerState = this.sessionProtocolFactory.CreateListenerSecurityState();
			if (typeof(TChannel) == typeof(IReplySessionChannel))
			{
				this.channelAcceptor = new SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IReplySessionChannel>(this.SecurityChannelListener, listenerState);
			}
			else
			{
				if (!(typeof(TChannel) == typeof(IDuplexSessionChannel)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				this.channelAcceptor = new SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IDuplexSessionChannel>(this.SecurityChannelListener, listenerState);
			}
			return (IChannelAcceptor<TChannel>)this.channelAcceptor;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0005DB4C File Offset: 0x0005BD4C
		internal IChannelListener CreateInnerChannelListener()
		{
			if (this.ChannelBuilder.CanBuildChannelListener<IDuplexSessionChannel>())
			{
				return this.ChannelBuilder.BuildChannelListener<IDuplexSessionChannel>(new MatchNoneMessageFilter(), int.MinValue);
			}
			if (this.ChannelBuilder.CanBuildChannelListener<IDuplexChannel>())
			{
				return this.ChannelBuilder.BuildChannelListener<IDuplexChannel>(new MatchNoneMessageFilter(), int.MinValue);
			}
			if (this.ChannelBuilder.CanBuildChannelListener<IReplyChannel>())
			{
				return this.ChannelBuilder.BuildChannelListener<IReplyChannel>(new MatchNoneMessageFilter(), int.MinValue);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0005DBD4 File Offset: 0x0005BDD4
		private void OnTokenRenewed(SecurityToken newToken, SecurityToken oldToken)
		{
			this.communicationObject.ThrowIfClosed();
			if (!this.acceptNewWork)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("SecurityListenerClosing")));
			}
			SecurityContextSecurityToken securityContextSecurityToken = newToken as SecurityContextSecurityToken;
			if (securityContextSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SessionTokenIsNotSecurityContextToken", new object[]
				{
					newToken.GetType(),
					typeof(SecurityContextSecurityToken)
				})));
			}
			SecurityContextSecurityToken securityContextSecurityToken2 = oldToken as SecurityContextSecurityToken;
			if (securityContextSecurityToken2 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SessionTokenIsNotSecurityContextToken", new object[]
				{
					oldToken.GetType(),
					typeof(SecurityContextSecurityToken)
				})));
			}
			SecuritySessionServerSettings.IServerSecuritySessionChannel serverSecuritySessionChannel = this.FindSessionChannel(securityContextSecurityToken.ContextId);
			if (serverSecuritySessionChannel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("CannotFindSecuritySession", new object[]
				{
					securityContextSecurityToken.ContextId
				})));
			}
			serverSecuritySessionChannel.RenewSessionToken(securityContextSecurityToken, securityContextSecurityToken2);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0005DCD4 File Offset: 0x0005BED4
		private IServerReliableChannelBinder CreateChannelBinder(SecurityContextSecurityToken sessionToken, EndpointAddress remoteAddress)
		{
			IServerReliableChannelBinder serverReliableChannelBinder = null;
			MessageFilter filter = new SecuritySessionFilter(sessionToken.ContextId, this.sessionProtocolFactory.StandardsManager, this.sessionProtocolFactory.SecurityHeaderLayout == SecurityHeaderLayout.Strict, new string[]
			{
				this.SecurityStandardsManager.SecureConversationDriver.RenewAction.Value,
				this.SecurityStandardsManager.SecureConversationDriver.RenewResponseAction.Value
			});
			int maxValue = int.MaxValue;
			TolerateFaultsMode faultMode = this.TolerateTransportFailures ? TolerateFaultsMode.Always : TolerateFaultsMode.Never;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.ChannelBuilder.CanBuildChannelListener<IDuplexSessionChannel>())
				{
					serverReliableChannelBinder = ServerReliableChannelBinder<IDuplexSessionChannel>.CreateBinder(this.ChannelBuilder, remoteAddress, filter, maxValue, faultMode, this.CloseTimeout, this.SendTimeout);
				}
				else if (this.ChannelBuilder.CanBuildChannelListener<IDuplexChannel>())
				{
					serverReliableChannelBinder = ServerReliableChannelBinder<IDuplexChannel>.CreateBinder(this.ChannelBuilder, remoteAddress, filter, maxValue, faultMode, this.CloseTimeout, this.SendTimeout);
				}
				else if (this.ChannelBuilder.CanBuildChannelListener<IReplyChannel>())
				{
					serverReliableChannelBinder = ServerReliableChannelBinder<IReplyChannel>.CreateBinder(this.ChannelBuilder, remoteAddress, filter, maxValue, faultMode, this.CloseTimeout, this.SendTimeout);
				}
			}
			if (serverReliableChannelBinder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			serverReliableChannelBinder.Open(this.OpenTimeout);
			SecuritySessionServerSettings.SessionInitiationMessageHandler sessionInitiationMessageHandler = new SecuritySessionServerSettings.SessionInitiationMessageHandler(serverReliableChannelBinder, this, sessionToken);
			sessionInitiationMessageHandler.BeginReceive(TimeSpan.MaxValue);
			return serverReliableChannelBinder;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0005DE3C File Offset: 0x0005C03C
		private void OnTokenIssued(SecurityToken issuedToken, EndpointAddress tokenRequestor)
		{
			this.communicationObject.ThrowIfClosed();
			if (!this.acceptNewWork)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("SecurityListenerClosing")));
			}
			SecurityContextSecurityToken securityContextSecurityToken = issuedToken as SecurityContextSecurityToken;
			if (securityContextSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SessionTokenIsNotSecurityContextToken", new object[]
				{
					issuedToken.GetType(),
					typeof(SecurityContextSecurityToken)
				})));
			}
			IServerReliableChannelBinder serverReliableChannelBinder = this.CreateChannelBinder(securityContextSecurityToken, tokenRequestor ?? EndpointAddress.AnonymousAddress);
			bool flag = false;
			try
			{
				this.AddPendingSession(securityContextSecurityToken.ContextId, serverReliableChannelBinder);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					serverReliableChannelBinder.Abort();
				}
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
		private void OnTimer(object state)
		{
			if (this.communicationObject.State == CommunicationState.Closed || this.communicationObject.State == CommunicationState.Faulted)
			{
				return;
			}
			try
			{
				this.ClearPendingSessions();
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
				if (this.communicationObject.State != CommunicationState.Closed && this.communicationObject.State != CommunicationState.Closing && this.communicationObject.State != CommunicationState.Faulted)
				{
					this.inactivityTimer.Set(this.inactivityTimeout);
				}
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005DF90 File Offset: 0x0005C190
		private void AddPendingSession(UniqueId sessionId, IServerReliableChannelBinder channelBinder)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.GetPendingSessionCount() + 1 > this.MaximumPendingSessions)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QuotaExceededException(SR.GetString("SecuritySessionLimitReached")));
				}
				if (this.pendingSessions1.ContainsKey(sessionId) || this.pendingSessions2.ContainsKey(sessionId))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SecuritySessionAlreadyPending", new object[]
					{
						sessionId
					})));
				}
				this.pendingSessions1.Add(sessionId, channelBinder);
			}
			SecurityTraceRecordHelper.TracePendingSessionAdded(sessionId, this.Uri);
			if (TD.SecuritySessionRatioIsEnabled())
			{
				TD.SecuritySessionRatio(this.GetPendingSessionCount(), this.MaximumPendingSessions);
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0005E068 File Offset: 0x0005C268
		private void TryCloseBinder(IServerReliableChannelBinder binder, TimeSpan timeout)
		{
			bool flag = false;
			try
			{
				binder.Close(timeout);
			}
			catch (CommunicationException exception)
			{
				flag = true;
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				flag = true;
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			finally
			{
				if (flag)
				{
					binder.Abort();
				}
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0005E0DC File Offset: 0x0005C2DC
		private void ClearPendingSessions()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.pendingSessions1.Count != 0 || this.pendingSessions2.Count != 0)
				{
					foreach (UniqueId uniqueId in this.pendingSessions2.Keys)
					{
						IServerReliableChannelBinder binder = this.pendingSessions2[uniqueId];
						try
						{
							this.TryCloseBinder(binder, this.CloseTimeout);
							this.SessionTokenCache.RemoveAllContexts(uniqueId);
						}
						catch (CommunicationException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
						catch (TimeoutException ex)
						{
							if (TD.CloseTimeoutIsEnabled())
							{
								TD.CloseTimeout(ex.Message);
							}
							DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						}
						catch (ObjectDisposedException exception2)
						{
							DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
						}
						SecurityTraceRecordHelper.TracePendingSessionClosed(uniqueId, this.Uri);
					}
					this.pendingSessions2.Clear();
					Dictionary<UniqueId, IServerReliableChannelBinder> dictionary = this.pendingSessions2;
					this.pendingSessions2 = this.pendingSessions1;
					this.pendingSessions1 = dictionary;
				}
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0005E234 File Offset: 0x0005C434
		private bool RemovePendingSession(UniqueId sessionId)
		{
			object obj = this.ThisLock;
			bool flag2;
			lock (obj)
			{
				if (this.pendingSessions1.ContainsKey(sessionId))
				{
					this.pendingSessions1.Remove(sessionId);
					flag2 = true;
				}
				else if (this.pendingSessions2.ContainsKey(sessionId))
				{
					this.pendingSessions2.Remove(sessionId);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			if (flag2)
			{
				SecurityTraceRecordHelper.TracePendingSessionActivated(sessionId, this.Uri);
				if (TD.SecuritySessionRatioIsEnabled())
				{
					TD.SecuritySessionRatio(this.GetPendingSessionCount(), this.MaximumPendingSessions);
				}
			}
			return flag2;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0005E2D8 File Offset: 0x0005C4D8
		private SecuritySessionServerSettings.IServerSecuritySessionChannel FindSessionChannel(UniqueId sessionId)
		{
			object obj = this.ThisLock;
			SecuritySessionServerSettings.IServerSecuritySessionChannel result;
			lock (obj)
			{
				this.activeSessions.TryGetValue(sessionId, out result);
			}
			return result;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0005E324 File Offset: 0x0005C524
		private void AddSessionChannel(UniqueId sessionId, SecuritySessionServerSettings.IServerSecuritySessionChannel channel)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.activeSessions.Add(sessionId, channel);
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0005E36C File Offset: 0x0005C56C
		private void RemoveSessionChannel(string sessionId)
		{
			this.RemoveSessionChannel(new UniqueId(sessionId));
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0005E37C File Offset: 0x0005C57C
		private void RemoveSessionChannel(UniqueId sessionId)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.activeSessions.Remove(sessionId);
			}
			SecurityTraceRecordHelper.TraceActiveSessionRemoved(sessionId, this.Uri);
		}

		// Token: 0x04001C71 RID: 7281
		internal const string defaultKeyRenewalIntervalString = "15:00:00";

		// Token: 0x04001C72 RID: 7282
		internal const string defaultKeyRolloverIntervalString = "00:05:00";

		// Token: 0x04001C73 RID: 7283
		internal const string defaultInactivityTimeoutString = "00:02:00";

		// Token: 0x04001C74 RID: 7284
		internal static readonly TimeSpan defaultKeyRenewalInterval = TimeSpan.Parse("15:00:00", CultureInfo.InvariantCulture);

		// Token: 0x04001C75 RID: 7285
		internal static readonly TimeSpan defaultKeyRolloverInterval = TimeSpan.Parse("00:05:00", CultureInfo.InvariantCulture);

		// Token: 0x04001C76 RID: 7286
		internal const bool defaultTolerateTransportFailures = true;

		// Token: 0x04001C77 RID: 7287
		internal const int defaultMaximumPendingSessions = 128;

		// Token: 0x04001C78 RID: 7288
		internal static readonly TimeSpan defaultInactivityTimeout = TimeSpan.Parse("00:02:00", CultureInfo.InvariantCulture);

		// Token: 0x04001C79 RID: 7289
		private int maximumPendingSessions;

		// Token: 0x04001C7A RID: 7290
		private Dictionary<UniqueId, IServerReliableChannelBinder> pendingSessions1;

		// Token: 0x04001C7B RID: 7291
		private Dictionary<UniqueId, IServerReliableChannelBinder> pendingSessions2;

		// Token: 0x04001C7C RID: 7292
		private IOThreadTimer inactivityTimer;

		// Token: 0x04001C7D RID: 7293
		private TimeSpan inactivityTimeout;

		// Token: 0x04001C7E RID: 7294
		private bool tolerateTransportFailures;

		// Token: 0x04001C7F RID: 7295
		private TimeSpan maximumKeyRenewalInterval;

		// Token: 0x04001C80 RID: 7296
		private TimeSpan keyRolloverInterval;

		// Token: 0x04001C81 RID: 7297
		private int maximumPendingKeysPerSession;

		// Token: 0x04001C82 RID: 7298
		private SecurityProtocolFactory sessionProtocolFactory;

		// Token: 0x04001C83 RID: 7299
		private ICommunicationObject channelAcceptor;

		// Token: 0x04001C84 RID: 7300
		private Dictionary<UniqueId, SecuritySessionServerSettings.IServerSecuritySessionChannel> activeSessions;

		// Token: 0x04001C85 RID: 7301
		private ChannelListenerBase securityChannelListener;

		// Token: 0x04001C86 RID: 7302
		private ChannelBuilder channelBuilder;

		// Token: 0x04001C87 RID: 7303
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001C88 RID: 7304
		private SecurityTokenParameters issuedTokenParameters;

		// Token: 0x04001C89 RID: 7305
		private SecurityTokenAuthenticator sessionTokenAuthenticator;

		// Token: 0x04001C8A RID: 7306
		private ISecurityContextSecurityTokenCache sessionTokenCache;

		// Token: 0x04001C8B RID: 7307
		private SecurityTokenResolver sessionTokenResolver;

		// Token: 0x04001C8C RID: 7308
		private WrapperSecurityCommunicationObject communicationObject;

		// Token: 0x04001C8D RID: 7309
		private volatile bool acceptNewWork;

		// Token: 0x04001C8E RID: 7310
		private MessageVersion messageVersion;

		// Token: 0x04001C8F RID: 7311
		private TimeSpan closeTimeout;

		// Token: 0x04001C90 RID: 7312
		private TimeSpan openTimeout;

		// Token: 0x04001C91 RID: 7313
		private TimeSpan sendTimeout;

		// Token: 0x04001C92 RID: 7314
		private Uri listenUri;

		// Token: 0x04001C93 RID: 7315
		private SecurityListenerSettingsLifetimeManager settingsLifetimeManager;

		// Token: 0x04001C94 RID: 7316
		private bool canRenewSession = true;

		// Token: 0x04001C95 RID: 7317
		private object thisLock = new object();

		// Token: 0x02000B59 RID: 2905
		private class SessionInitiationMessageHandler
		{
			// Token: 0x060071B6 RID: 29110 RVA: 0x001A7F37 File Offset: 0x001A6137
			public SessionInitiationMessageHandler(IServerReliableChannelBinder channelBinder, SecuritySessionServerSettings settings, SecurityContextSecurityToken sessionToken)
			{
				this.channelBinder = channelBinder;
				this.settings = settings;
				this.sessionToken = sessionToken;
			}

			// Token: 0x060071B7 RID: 29111 RVA: 0x001A7F54 File Offset: 0x001A6154
			public IAsyncResult BeginReceive(TimeSpan timeout)
			{
				return this.channelBinder.BeginTryReceive(timeout, SecuritySessionServerSettings.SessionInitiationMessageHandler.receiveCallback, this);
			}

			// Token: 0x060071B8 RID: 29112 RVA: 0x001A7F68 File Offset: 0x001A6168
			public void ProcessMessage(IAsyncResult result)
			{
				bool flag = false;
				try
				{
					RequestContext requestContext;
					if (!this.channelBinder.EndTryReceive(result, out requestContext))
					{
						this.BeginReceive(TimeSpan.MaxValue);
					}
					else if (requestContext != null)
					{
						Message requestMessage = requestContext.RequestMessage;
						object thisLock = this.settings.ThisLock;
						lock (thisLock)
						{
							if (this.settings.communicationObject.State != CommunicationState.Opened)
							{
								((IDisposable)requestContext).Dispose();
								return;
							}
							if (this.processedInitiation)
							{
								return;
							}
							this.processedInitiation = true;
						}
						if (!this.settings.RemovePendingSession(this.sessionToken.ContextId))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationException(SR.GetString("SecuritySessionNotPending", new object[]
							{
								this.sessionToken.ContextId
							})));
						}
						if (this.settings.channelAcceptor is SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IReplySessionChannel>)
						{
							SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IReplySessionChannel> securitySessionChannelAcceptor = (SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IReplySessionChannel>)this.settings.channelAcceptor;
							SecuritySessionServerSettings.SecurityReplySessionChannel securityReplySessionChannel = new SecuritySessionServerSettings.SecurityReplySessionChannel(this.settings, this.channelBinder, this.sessionToken, securitySessionChannelAcceptor.ListenerSecurityState, this.settings.SettingsLifetimeManager);
							this.settings.AddSessionChannel(this.sessionToken.ContextId, securityReplySessionChannel);
							securityReplySessionChannel.StartReceiving(requestContext);
							securitySessionChannelAcceptor.EnqueueAndDispatch(securityReplySessionChannel);
						}
						else
						{
							if (!(this.settings.channelAcceptor is SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IDuplexSessionChannel>))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new EndpointNotFoundException(SR.GetString("SecuritySessionListenerNotFound", new object[]
								{
									requestMessage.Headers.Action
								})));
							}
							SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IDuplexSessionChannel> securitySessionChannelAcceptor2 = (SecuritySessionServerSettings.SecuritySessionChannelAcceptor<IDuplexSessionChannel>)this.settings.channelAcceptor;
							SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel serverSecurityDuplexSessionChannel = new SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel(this.settings, this.channelBinder, this.sessionToken, securitySessionChannelAcceptor2.ListenerSecurityState, this.settings.SettingsLifetimeManager);
							this.settings.AddSessionChannel(this.sessionToken.ContextId, serverSecurityDuplexSessionChannel);
							serverSecurityDuplexSessionChannel.StartReceiving(requestContext);
							securitySessionChannelAcceptor2.EnqueueAndDispatch(serverSecurityDuplexSessionChannel);
						}
					}
				}
				catch (Exception exception)
				{
					flag = true;
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				finally
				{
					if (flag)
					{
						this.channelBinder.Abort();
					}
				}
			}

			// Token: 0x060071B9 RID: 29113 RVA: 0x001A81E0 File Offset: 0x001A63E0
			private static void ReceiveCallback(IAsyncResult result)
			{
				((SecuritySessionServerSettings.SessionInitiationMessageHandler)result.AsyncState).ProcessMessage(result);
			}

			// Token: 0x0400407E RID: 16510
			private static AsyncCallback receiveCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.SessionInitiationMessageHandler.ReceiveCallback));

			// Token: 0x0400407F RID: 16511
			private IServerReliableChannelBinder channelBinder;

			// Token: 0x04004080 RID: 16512
			private SecuritySessionServerSettings settings;

			// Token: 0x04004081 RID: 16513
			private SecurityContextSecurityToken sessionToken;

			// Token: 0x04004082 RID: 16514
			private bool processedInitiation;
		}

		// Token: 0x02000B5A RID: 2906
		private interface IInputQueueChannelAcceptor
		{
			// Token: 0x17001A76 RID: 6774
			// (get) Token: 0x060071BB RID: 29115
			int PendingCount { get; }
		}

		// Token: 0x02000B5B RID: 2907
		private class SecuritySessionChannelAcceptor<T> : InputQueueChannelAcceptor<T>, SecuritySessionServerSettings.IInputQueueChannelAcceptor where T : class, IChannel
		{
			// Token: 0x060071BC RID: 29116 RVA: 0x001A820B File Offset: 0x001A640B
			public SecuritySessionChannelAcceptor(ChannelListenerBase manager, object listenerState) : base(manager)
			{
				this.listenerState = listenerState;
			}

			// Token: 0x17001A77 RID: 6775
			// (get) Token: 0x060071BD RID: 29117 RVA: 0x001A821B File Offset: 0x001A641B
			public object ListenerSecurityState
			{
				get
				{
					return this.listenerState;
				}
			}

			// Token: 0x17001A78 RID: 6776
			// (get) Token: 0x060071BE RID: 29118 RVA: 0x001A8223 File Offset: 0x001A6423
			int SecuritySessionServerSettings.IInputQueueChannelAcceptor.PendingCount
			{
				get
				{
					return base.PendingCount;
				}
			}

			// Token: 0x04004083 RID: 16515
			private object listenerState;
		}

		// Token: 0x02000B5C RID: 2908
		private interface IServerSecuritySessionChannel
		{
			// Token: 0x060071BF RID: 29119
			void RenewSessionToken(SecurityContextSecurityToken newToken, SecurityContextSecurityToken supportingToken);
		}

		// Token: 0x02000B5D RID: 2909
		private abstract class ServerSecuritySessionChannel : ChannelBase, SecuritySessionServerSettings.IServerSecuritySessionChannel
		{
			// Token: 0x060071C0 RID: 29120 RVA: 0x001A822C File Offset: 0x001A642C
			protected ServerSecuritySessionChannel(SecuritySessionServerSettings settings, IServerReliableChannelBinder channelBinder, SecurityContextSecurityToken sessionToken, object listenerSecurityProtocolState, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(settings.SecurityChannelListener)
			{
				this.settings = settings;
				this.channelBinder = channelBinder;
				this.messageVersion = settings.MessageVersion;
				this.channelBinder.Faulted += this.OnInnerFaulted;
				this.securityProtocol = this.Settings.SessionProtocolFactory.CreateSecurityProtocol(null, null, listenerSecurityProtocolState, true, TimeSpan.Zero);
				if (!(this.securityProtocol is IAcceptorSecuritySessionProtocol))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProtocolMisMatch", new object[]
					{
						"IAcceptorSecuritySessionProtocol",
						base.GetType().ToString()
					})));
				}
				this.currentSessionToken = sessionToken;
				this.sessionId = sessionToken.ContextId;
				this.futureSessionTokens = new List<SecurityContextSecurityToken>(1);
				((IAcceptorSecuritySessionProtocol)this.securityProtocol).SetOutgoingSessionToken(sessionToken);
				((IAcceptorSecuritySessionProtocol)this.securityProtocol).SetSessionTokenAuthenticator(this.sessionId, this.settings.SessionTokenAuthenticator, this.settings.SessionTokenResolver);
				this.settingsLifetimeManager = settingsLifetimeManager;
				this.receiveLock = new ThreadNeutralSemaphore(1);
			}

			// Token: 0x17001A79 RID: 6777
			// (get) Token: 0x060071C1 RID: 29121 RVA: 0x001A8349 File Offset: 0x001A6549
			protected SecuritySessionServerSettings Settings
			{
				get
				{
					return this.settings;
				}
			}

			// Token: 0x17001A7A RID: 6778
			// (get) Token: 0x060071C2 RID: 29122 RVA: 0x001A8351 File Offset: 0x001A6551
			protected virtual bool CanDoSecurityCorrelation
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001A7B RID: 6779
			// (get) Token: 0x060071C3 RID: 29123 RVA: 0x001A8354 File Offset: 0x001A6554
			internal IServerReliableChannelBinder ChannelBinder
			{
				get
				{
					return this.channelBinder;
				}
			}

			// Token: 0x17001A7C RID: 6780
			// (get) Token: 0x060071C4 RID: 29124 RVA: 0x001A835C File Offset: 0x001A655C
			internal TimeSpan InternalSendTimeout
			{
				get
				{
					return base.DefaultSendTimeout;
				}
			}

			// Token: 0x17001A7D RID: 6781
			// (get) Token: 0x060071C5 RID: 29125 RVA: 0x001A8364 File Offset: 0x001A6564
			public EndpointAddress LocalAddress
			{
				get
				{
					return this.channelBinder.LocalAddress;
				}
			}

			// Token: 0x060071C6 RID: 29126 RVA: 0x001A8374 File Offset: 0x001A6574
			protected override void OnOpen(TimeSpan timeout)
			{
				this.securityProtocol.Open(timeout);
				if (this.CanDoSecurityCorrelation)
				{
					((IAcceptorSecuritySessionProtocol)this.securityProtocol).ReturnCorrelationState = true;
				}
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (base.State != CommunicationState.Closed && base.State != CommunicationState.Closing)
					{
						this.settingsLifetimeManager.AddReference();
						this.hasSecurityStateReference = true;
					}
				}
			}

			// Token: 0x060071C7 RID: 29127 RVA: 0x001A83FC File Offset: 0x001A65FC
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.OnOpen(timeout);
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060071C8 RID: 29128 RVA: 0x001A840C File Offset: 0x001A660C
			protected override void OnEndOpen(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x060071C9 RID: 29129 RVA: 0x001A8414 File Offset: 0x001A6614
			protected virtual void AbortCore()
			{
				if (this.channelBinder != null)
				{
					this.channelBinder.Abort();
				}
				if (this.securityProtocol != null)
				{
					this.securityProtocol.Close(true, TimeSpan.Zero);
				}
				this.Settings.SessionTokenCache.RemoveAllContexts(this.currentSessionToken.ContextId);
				bool flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.hasSecurityStateReference)
					{
						flag = true;
						this.hasSecurityStateReference = false;
					}
				}
				if (flag)
				{
					this.settingsLifetimeManager.Abort();
				}
			}

			// Token: 0x060071CA RID: 29130 RVA: 0x001A84BC File Offset: 0x001A66BC
			protected virtual void CloseCore(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				try
				{
					if (this.channelBinder != null)
					{
						this.channelBinder.Close(timeoutHelper.RemainingTime());
					}
					if (this.securityProtocol != null)
					{
						this.securityProtocol.Close(false, timeoutHelper.RemainingTime());
					}
					bool flag = false;
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.hasSecurityStateReference)
						{
							flag = true;
							this.hasSecurityStateReference = false;
						}
					}
					if (flag)
					{
						this.settingsLifetimeManager.Close(timeoutHelper.RemainingTime());
					}
				}
				catch (CommunicationObjectAbortedException)
				{
					if (base.State != CommunicationState.Closed)
					{
						throw;
					}
				}
				this.Settings.SessionTokenCache.RemoveAllContexts(this.currentSessionToken.ContextId);
			}

			// Token: 0x060071CB RID: 29131 RVA: 0x001A8598 File Offset: 0x001A6798
			protected virtual IAsyncResult BeginCloseCore(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x060071CC RID: 29132 RVA: 0x001A85A3 File Offset: 0x001A67A3
			protected virtual void EndCloseCore(IAsyncResult result)
			{
				SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult.End(result);
			}

			// Token: 0x060071CD RID: 29133
			protected abstract void OnCloseMessageReceived(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout);

			// Token: 0x060071CE RID: 29134
			protected abstract void OnCloseResponseMessageReceived(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout);

			// Token: 0x060071CF RID: 29135 RVA: 0x001A85AC File Offset: 0x001A67AC
			public void RenewSessionToken(SecurityContextSecurityToken newToken, SecurityContextSecurityToken supportingToken)
			{
				base.ThrowIfClosedOrNotOpen();
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (supportingToken.ContextId != this.currentSessionToken.ContextId || supportingToken.KeyGeneration != this.currentSessionToken.KeyGeneration)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("CurrentSessionTokenNotRenewed", new object[]
						{
							supportingToken.KeyGeneration,
							this.currentSessionToken.KeyGeneration
						})));
					}
					if (this.futureSessionTokens.Count == this.Settings.MaximumPendingKeysPerSession)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("TooManyPendingSessionKeys")));
					}
					this.futureSessionTokens.Add(newToken);
				}
				SecurityTraceRecordHelper.TraceNewServerSessionKeyIssued(newToken, supportingToken, this.GetLocalUri());
			}

			// Token: 0x060071D0 RID: 29136 RVA: 0x001A86A0 File Offset: 0x001A68A0
			protected Uri GetLocalUri()
			{
				if (this.channelBinder.LocalAddress == null)
				{
					return null;
				}
				return this.channelBinder.LocalAddress.Uri;
			}

			// Token: 0x060071D1 RID: 29137 RVA: 0x001A86C7 File Offset: 0x001A68C7
			private void OnInnerFaulted(IReliableChannelBinder sender, Exception exception)
			{
				base.Fault(exception);
			}

			// Token: 0x060071D2 RID: 29138 RVA: 0x001A86D0 File Offset: 0x001A68D0
			private SecurityContextSecurityToken GetSessionToken(SecurityMessageProperty securityProperty)
			{
				SecurityContextSecurityToken securityContextSecurityToken = (securityProperty.ProtectionToken != null) ? (securityProperty.ProtectionToken.SecurityToken as SecurityContextSecurityToken) : null;
				if (securityContextSecurityToken != null && securityContextSecurityToken.ContextId == this.sessionId)
				{
					return securityContextSecurityToken;
				}
				if (securityProperty.HasIncomingSupportingTokens)
				{
					for (int i = 0; i < securityProperty.IncomingSupportingTokens.Count; i++)
					{
						if (securityProperty.IncomingSupportingTokens[i].SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing)
						{
							securityContextSecurityToken = (securityProperty.IncomingSupportingTokens[i].SecurityToken as SecurityContextSecurityToken);
							if (securityContextSecurityToken != null && securityContextSecurityToken.ContextId == this.sessionId)
							{
								return securityContextSecurityToken;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x060071D3 RID: 29139 RVA: 0x001A8774 File Offset: 0x001A6974
			private bool CheckIncomingToken(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				SecurityMessageProperty security = message.Properties.Security;
				SecurityContextSecurityToken sessionToken = this.GetSessionToken(security);
				if (sessionToken == null)
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("NoSessionTokenPresentInMessage")), message);
				}
				if (!(sessionToken.KeyExpirationTime < DateTime.UtcNow) || !(message.Headers.Action != this.settings.SecurityStandardsManager.SecureConversationDriver.CloseAction.Value))
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.futureSessionTokens.Count > 0 && sessionToken.KeyGeneration != this.currentSessionToken.KeyGeneration)
						{
							bool flag2 = false;
							for (int i = 0; i < this.futureSessionTokens.Count; i++)
							{
								if (this.futureSessionTokens[i].KeyGeneration == sessionToken.KeyGeneration)
								{
									DateTime expirationTime = TimeoutHelper.Add(DateTime.UtcNow, this.settings.KeyRolloverInterval);
									this.settings.SessionTokenCache.UpdateContextCachingTime(this.currentSessionToken, expirationTime);
									this.currentSessionToken = this.futureSessionTokens[i];
									this.futureSessionTokens.RemoveAt(i);
									((IAcceptorSecuritySessionProtocol)this.securityProtocol).SetOutgoingSessionToken(this.currentSessionToken);
									flag2 = true;
									break;
								}
							}
							if (flag2)
							{
								SecurityTraceRecordHelper.TraceServerSessionKeyUpdated(this.currentSessionToken, this.GetLocalUri());
								for (int j = 0; j < this.futureSessionTokens.Count; j++)
								{
									this.Settings.SessionTokenCache.RemoveContext(this.futureSessionTokens[j].ContextId, this.futureSessionTokens[j].KeyGeneration);
								}
								this.futureSessionTokens.Clear();
							}
						}
					}
					return true;
				}
				if (this.settings.CanRenewSession)
				{
					this.SendRenewFault(requestContext, correlationState, timeout);
					return false;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SessionKeyExpiredException(SR.GetString("SecurityContextKeyExpired", new object[]
				{
					sessionToken.ContextId,
					sessionToken.KeyGeneration
				})));
			}

			// Token: 0x060071D4 RID: 29140 RVA: 0x001A89B8 File Offset: 0x001A6BB8
			public void StartReceiving(RequestContext initialRequestContext)
			{
				if (this.initialRequestContext != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AttemptToCreateMultipleRequestContext")));
				}
				this.initialRequestContext = initialRequestContext;
			}

			// Token: 0x060071D5 RID: 29141 RVA: 0x001A89E3 File Offset: 0x001A6BE3
			public RequestContext ReceiveRequest()
			{
				return this.ReceiveRequest(base.DefaultReceiveTimeout);
			}

			// Token: 0x060071D6 RID: 29142 RVA: 0x001A89F4 File Offset: 0x001A6BF4
			public RequestContext ReceiveRequest(TimeSpan timeout)
			{
				RequestContext result;
				if (this.TryReceiveRequest(timeout, out result))
				{
					return result;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
			}

			// Token: 0x060071D7 RID: 29143 RVA: 0x001A8A1D File Offset: 0x001A6C1D
			public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
			{
				return this.BeginReceiveRequest(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x060071D8 RID: 29144 RVA: 0x001A8A2D File Offset: 0x001A6C2D
			public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.BeginTryReceiveRequest(timeout, callback, state);
			}

			// Token: 0x060071D9 RID: 29145 RVA: 0x001A8A38 File Offset: 0x001A6C38
			public RequestContext EndReceiveRequest(IAsyncResult result)
			{
				RequestContext result2;
				if (this.EndTryReceiveRequest(result, out result2))
				{
					return result2;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
			}

			// Token: 0x060071DA RID: 29146 RVA: 0x001A8A61 File Offset: 0x001A6C61
			public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x060071DB RID: 29147 RVA: 0x001A8A6C File Offset: 0x001A6C6C
			public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext requestContext)
			{
				return SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.EndAsRequestContext(result, out requestContext);
			}

			// Token: 0x060071DC RID: 29148 RVA: 0x001A8A78 File Offset: 0x001A6C78
			public bool TryReceiveRequest(TimeSpan timeout, out RequestContext requestContext)
			{
				base.ThrowIfFaulted();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (!this.receiveLock.TryEnter(timeoutHelper.RemainingTime()))
				{
					requestContext = null;
					return false;
				}
				try
				{
					while (!this.isInputClosed)
					{
						if (base.State == CommunicationState.Faulted)
						{
							break;
						}
						if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
						{
							requestContext = null;
							return false;
						}
						RequestContext requestContext2;
						if (this.initialRequestContext != null)
						{
							requestContext2 = this.initialRequestContext;
							this.initialRequestContext = null;
						}
						else if (!this.channelBinder.TryReceive(timeoutHelper.RemainingTime(), out requestContext2))
						{
							requestContext = null;
							return false;
						}
						if (requestContext2 == null)
						{
							break;
						}
						if (this.isInputClosed && requestContext2.RequestMessage != null)
						{
							Message requestMessage = requestContext2.RequestMessage;
							try
							{
								ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(requestMessage);
								throw TraceUtility.ThrowHelperWarning(exception, requestMessage);
							}
							finally
							{
								requestMessage.Close();
								requestContext2.Abort();
							}
						}
						SecurityProtocolCorrelationState correlationState = null;
						bool flag;
						Message message = this.ProcessRequestContext(requestContext2, timeoutHelper.RemainingTime(), out correlationState, out flag);
						if (message != null)
						{
							requestContext = new SecuritySessionServerSettings.SecuritySessionRequestContext(requestContext2, message, correlationState, this);
							return true;
						}
					}
				}
				finally
				{
					this.receiveLock.Exit();
				}
				base.ThrowIfFaulted();
				requestContext = null;
				return true;
			}

			// Token: 0x060071DD RID: 29149 RVA: 0x001A8BC8 File Offset: 0x001A6DC8
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x060071DE RID: 29150 RVA: 0x001A8BD8 File Offset: 0x001A6DD8
			public Message Receive(TimeSpan timeout)
			{
				Message result;
				if (this.TryReceive(timeout, out result))
				{
					return result;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
			}

			// Token: 0x060071DF RID: 29151 RVA: 0x001A8C01 File Offset: 0x001A6E01
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x060071E0 RID: 29152 RVA: 0x001A8C11 File Offset: 0x001A6E11
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x060071E1 RID: 29153 RVA: 0x001A8C1C File Offset: 0x001A6E1C
			public Message EndReceive(IAsyncResult result)
			{
				Message result2;
				if (this.EndTryReceive(result, out result2))
				{
					return result2;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
			}

			// Token: 0x060071E2 RID: 29154 RVA: 0x001A8C45 File Offset: 0x001A6E45
			public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x060071E3 RID: 29155 RVA: 0x001A8C50 File Offset: 0x001A6E50
			public bool EndTryReceive(IAsyncResult result, out Message message)
			{
				return SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.EndAsMessage(result, out message);
			}

			// Token: 0x060071E4 RID: 29156 RVA: 0x001A8C5C File Offset: 0x001A6E5C
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				RequestContext requestContext;
				if (this.TryReceiveRequest(timeoutHelper.RemainingTime(), out requestContext))
				{
					if (requestContext != null)
					{
						message = requestContext.RequestMessage;
						try
						{
							requestContext.Close(timeoutHelper.RemainingTime());
							return true;
						}
						catch (TimeoutException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
							return true;
						}
					}
					message = null;
					return true;
				}
				message = null;
				return false;
			}

			// Token: 0x060071E5 RID: 29157 RVA: 0x001A8CC0 File Offset: 0x001A6EC0
			public override T GetProperty<T>()
			{
				if (typeof(T) == typeof(FaultConverter) && this.channelBinder != null)
				{
					return new SecurityChannelFaultConverter(this.channelBinder.Channel) as T;
				}
				T property = base.GetProperty<T>();
				if (property == null && this.channelBinder != null && this.channelBinder.Channel != null)
				{
					property = this.channelBinder.Channel.GetProperty<T>();
				}
				return property;
			}

			// Token: 0x060071E6 RID: 29158 RVA: 0x001A8D44 File Offset: 0x001A6F44
			private void SendFaultIfRequired(Exception e, Message unverifiedMessage, RequestContext requestContext, TimeSpan timeout)
			{
				try
				{
					if (this.channelBinder.Channel is IReplyChannel || this.channelBinder.Channel is IDuplexSessionChannel)
					{
						MessageFault messageFault = SecurityUtils.CreateSecurityMessageFault(e, this.securityProtocol.SecurityProtocolFactory.StandardsManager);
						if (messageFault != null)
						{
							TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
							try
							{
								using (Message message = Message.CreateMessage(unverifiedMessage.Version, messageFault, unverifiedMessage.Version.Addressing.DefaultFaultAction))
								{
									if (unverifiedMessage.Headers.MessageId != null)
									{
										message.InitializeReply(unverifiedMessage);
									}
									requestContext.Reply(message, timeoutHelper.RemainingTime());
									requestContext.Close(timeoutHelper.RemainingTime());
								}
							}
							catch (CommunicationException exception)
							{
								DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
							}
							catch (TimeoutException exception2)
							{
								if (TD.CloseTimeoutIsEnabled())
								{
									TD.CloseTimeout(e.Message);
								}
								DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
							}
						}
					}
				}
				finally
				{
					unverifiedMessage.Close();
					requestContext.Abort();
				}
			}

			// Token: 0x060071E7 RID: 29159 RVA: 0x001A8E6C File Offset: 0x001A706C
			private bool ShouldWrapException(Exception e)
			{
				return e is FormatException || e is XmlException;
			}

			// Token: 0x060071E8 RID: 29160 RVA: 0x001A8E84 File Offset: 0x001A7084
			private Message ProcessRequestContext(RequestContext requestContext, TimeSpan timeout, out SecurityProtocolCorrelationState correlationState, out bool isSecurityProcessingFailure)
			{
				correlationState = null;
				isSecurityProcessingFailure = false;
				if (requestContext == null)
				{
					return null;
				}
				Message result = null;
				Message requestMessage = requestContext.RequestMessage;
				bool flag = true;
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					Message unverifiedMessage = requestMessage;
					Exception ex = null;
					try
					{
						correlationState = this.VerifyIncomingMessage(ref requestMessage, timeoutHelper.RemainingTime());
					}
					catch (MessageSecurityException ex2)
					{
						isSecurityProcessingFailure = true;
						ex = ex2;
					}
					if (ex != null)
					{
						this.SendFaultIfRequired(ex, unverifiedMessage, requestContext, timeoutHelper.RemainingTime());
						flag = false;
						return null;
					}
					if (this.CheckIncomingToken(requestContext, requestMessage, correlationState, timeoutHelper.RemainingTime()))
					{
						if (requestMessage.Headers.Action == this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseAction.Value)
						{
							SecurityTraceRecordHelper.TraceServerSessionCloseReceived(this.currentSessionToken, this.GetLocalUri());
							this.isInputClosed = true;
							this.OnCloseMessageReceived(requestContext, requestMessage, correlationState, timeoutHelper.RemainingTime());
							correlationState = null;
						}
						else if (requestMessage.Headers.Action == this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction.Value)
						{
							SecurityTraceRecordHelper.TraceServerSessionCloseResponseReceived(this.currentSessionToken, this.GetLocalUri());
							this.isInputClosed = true;
							this.OnCloseResponseMessageReceived(requestContext, requestMessage, correlationState, timeoutHelper.RemainingTime());
							correlationState = null;
						}
						else
						{
							result = requestMessage;
						}
						flag = false;
					}
				}
				catch (Exception ex3)
				{
					if (ex3 is CommunicationException || ex3 is TimeoutException || Fx.IsFatal(ex3) || !this.ShouldWrapException(ex3))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MessageSecurityVerificationFailed"), ex3));
				}
				finally
				{
					if (flag)
					{
						if (requestContext.RequestMessage != null)
						{
							requestContext.RequestMessage.Close();
						}
						requestContext.Abort();
					}
				}
				return result;
			}

			// Token: 0x060071E9 RID: 29161 RVA: 0x001A907C File Offset: 0x001A727C
			internal void CheckOutgoingToken()
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.currentSessionToken.KeyExpirationTime < DateTime.UtcNow)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SessionKeyExpiredException(SR.GetString("SecuritySessionKeyIsStale")));
					}
				}
			}

			// Token: 0x060071EA RID: 29162 RVA: 0x001A90E8 File Offset: 0x001A72E8
			internal void SecureApplicationMessage(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
			{
				base.ThrowIfFaulted();
				base.ThrowIfClosedOrNotOpen();
				this.CheckOutgoingToken();
				this.securityProtocol.SecureOutgoingMessage(ref message, timeout, correlationState);
			}

			// Token: 0x060071EB RID: 29163 RVA: 0x001A910B File Offset: 0x001A730B
			internal SecurityProtocolCorrelationState VerifyIncomingMessage(ref Message message, TimeSpan timeout)
			{
				return this.securityProtocol.VerifyIncomingMessage(ref message, timeout, null);
			}

			// Token: 0x060071EC RID: 29164 RVA: 0x001A911C File Offset: 0x001A731C
			private void PrepareReply(Message request, Message reply)
			{
				if (request.Headers.ReplyTo != null)
				{
					request.Headers.ReplyTo.ApplyTo(reply);
				}
				else if (request.Headers.From != null)
				{
					request.Headers.From.ApplyTo(reply);
				}
				if (request.Headers.MessageId != null)
				{
					reply.Headers.RelatesTo = request.Headers.MessageId;
				}
				TraceUtility.CopyActivity(request, reply);
				if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
				{
					TraceUtility.AddActivityHeader(reply);
				}
			}

			// Token: 0x060071ED RID: 29165 RVA: 0x001A91B8 File Offset: 0x001A73B8
			protected void InitializeFaultCodesIfRequired()
			{
				if (!this.areFaultCodesInitialized)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (!this.areFaultCodesInitialized)
						{
							SecurityStandardsManager standardsManager = this.securityProtocol.SecurityProtocolFactory.StandardsManager;
							SecureConversationDriver secureConversationDriver = standardsManager.SecureConversationDriver;
							this.renewFaultCode = FaultCode.CreateSenderFaultCode(secureConversationDriver.RenewNeededFaultCode.Value, secureConversationDriver.Namespace.Value);
							this.renewFaultReason = new FaultReason(SR.GetString("SecurityRenewFaultReason"), CultureInfo.InvariantCulture);
							this.sessionAbortedFaultCode = FaultCode.CreateSenderFaultCode("SecuritySessionAborted", "http://schemas.microsoft.com/ws/2006/05/security");
							this.sessionAbortedFaultReason = new FaultReason(SR.GetString("SecuritySessionAbortedFaultReason"), CultureInfo.InvariantCulture);
							this.areFaultCodesInitialized = true;
						}
					}
				}
			}

			// Token: 0x060071EE RID: 29166 RVA: 0x001A9298 File Offset: 0x001A7498
			private void SendRenewFault(RequestContext requestContext, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				Message requestMessage = requestContext.RequestMessage;
				try
				{
					this.InitializeFaultCodesIfRequired();
					MessageFault fault = MessageFault.CreateFault(this.renewFaultCode, this.renewFaultReason);
					Message message;
					if (requestMessage.Headers.MessageId != null)
					{
						message = Message.CreateMessage(requestMessage.Version, fault, "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault");
						message.InitializeReply(requestMessage);
					}
					else
					{
						message = Message.CreateMessage(requestMessage.Version, fault, "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault");
					}
					try
					{
						this.PrepareReply(requestMessage, message);
						TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
						this.securityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime(), correlationState);
						message.Properties.AllowOutputBatching = false;
						this.SendMessage(requestContext, message, timeoutHelper.RemainingTime());
					}
					finally
					{
						message.Close();
					}
					SecurityTraceRecordHelper.TraceSessionRenewalFaultSent(this.currentSessionToken, this.GetLocalUri(), requestMessage);
				}
				catch (CommunicationException e)
				{
					SecurityTraceRecordHelper.TraceRenewFaultSendFailure(this.currentSessionToken, this.GetLocalUri(), e);
				}
				catch (TimeoutException e2)
				{
					SecurityTraceRecordHelper.TraceRenewFaultSendFailure(this.currentSessionToken, this.GetLocalUri(), e2);
				}
			}

			// Token: 0x060071EF RID: 29167 RVA: 0x001A93BC File Offset: 0x001A75BC
			private Message ProcessCloseRequest(Message request)
			{
				XmlDictionaryReader readerAtBodyContents = request.GetReaderAtBodyContents();
				RequestSecurityToken requestSecurityToken;
				using (readerAtBodyContents)
				{
					requestSecurityToken = this.Settings.SecurityStandardsManager.TrustDriver.CreateRequestSecurityToken(readerAtBodyContents);
					request.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				if (requestSecurityToken.RequestType != null && requestSecurityToken.RequestType != this.Settings.SecurityStandardsManager.TrustDriver.RequestTypeClose)
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("InvalidRstRequestType", new object[]
					{
						requestSecurityToken.RequestType
					})), request);
				}
				if (requestSecurityToken.CloseTarget == null)
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("NoCloseTargetSpecified")), request);
				}
				SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = requestSecurityToken.CloseTarget as SecurityContextKeyIdentifierClause;
				if (securityContextKeyIdentifierClause == null || !SecuritySessionSecurityTokenAuthenticator.DoesSkiClauseMatchSigningToken(securityContextKeyIdentifierClause, request))
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("BadCloseTarget", new object[]
					{
						requestSecurityToken.CloseTarget
					})), request);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse = new RequestSecurityTokenResponse(this.Settings.SecurityStandardsManager);
				requestSecurityTokenResponse.Context = requestSecurityToken.Context;
				requestSecurityTokenResponse.IsRequestedTokenClosed = true;
				requestSecurityTokenResponse.MakeReadOnly();
				BodyWriter body = requestSecurityTokenResponse;
				if (this.Settings.SecurityStandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
				{
					RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = new RequestSecurityTokenResponseCollection(new List<RequestSecurityTokenResponse>(1)
					{
						requestSecurityTokenResponse
					}, this.Settings.SecurityStandardsManager);
					body = requestSecurityTokenResponseCollection;
				}
				Message message = Message.CreateMessage(request.Version, ActionHeader.Create(this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction, request.Version.Addressing), body);
				this.PrepareReply(request, message);
				return message;
			}

			// Token: 0x060071F0 RID: 29168 RVA: 0x001A956C File Offset: 0x001A776C
			internal Message CreateCloseResponse(Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				Message result;
				try
				{
					Message message2 = this.ProcessCloseRequest(message);
					this.securityProtocol.SecureOutgoingMessage(ref message2, timeout, correlationState);
					message2.Properties.AllowOutputBatching = false;
					result = message2;
				}
				finally
				{
					if (message != null)
					{
						((IDisposable)message).Dispose();
					}
				}
				return result;
			}

			// Token: 0x060071F1 RID: 29169 RVA: 0x001A95C0 File Offset: 0x001A77C0
			internal void TraceSessionClosedResponseSuccess()
			{
				SecurityTraceRecordHelper.TraceSessionClosedResponseSent(this.currentSessionToken, this.GetLocalUri());
			}

			// Token: 0x060071F2 RID: 29170 RVA: 0x001A95D3 File Offset: 0x001A77D3
			internal void TraceSessionClosedResponseFailure(Exception e)
			{
				SecurityTraceRecordHelper.TraceSessionClosedResponseSendFailure(this.currentSessionToken, this.GetLocalUri(), e);
			}

			// Token: 0x060071F3 RID: 29171 RVA: 0x001A95E7 File Offset: 0x001A77E7
			internal void TraceSessionClosedSuccess()
			{
				SecurityTraceRecordHelper.TraceSessionClosedSent(this.currentSessionToken, this.GetLocalUri());
			}

			// Token: 0x060071F4 RID: 29172 RVA: 0x001A95FA File Offset: 0x001A77FA
			internal void TraceSessionClosedFailure(Exception e)
			{
				SecurityTraceRecordHelper.TraceSessionCloseSendFailure(this.currentSessionToken, this.GetLocalUri(), e);
			}

			// Token: 0x060071F5 RID: 29173 RVA: 0x001A9610 File Offset: 0x001A7810
			protected void SendCloseResponse(RequestContext requestContext, Message closeResponse, TimeSpan timeout)
			{
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					try
					{
						this.SendMessage(requestContext, closeResponse, timeoutHelper.RemainingTime());
					}
					finally
					{
						if (closeResponse != null)
						{
							((IDisposable)closeResponse).Dispose();
						}
					}
					this.TraceSessionClosedResponseSuccess();
				}
				catch (CommunicationException e)
				{
					this.TraceSessionClosedResponseFailure(e);
				}
				catch (TimeoutException e2)
				{
					this.TraceSessionClosedResponseFailure(e2);
				}
			}

			// Token: 0x060071F6 RID: 29174 RVA: 0x001A9688 File Offset: 0x001A7888
			internal IAsyncResult BeginSendCloseResponse(RequestContext requestContext, Message closeResponse, TimeSpan timeout, AsyncCallback callback, object state)
			{
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					return this.BeginSendMessage(requestContext, closeResponse, timeoutHelper.RemainingTime(), callback, state);
				}
				catch (CommunicationException e)
				{
					this.TraceSessionClosedResponseFailure(e);
				}
				catch (TimeoutException e2)
				{
					this.TraceSessionClosedResponseFailure(e2);
				}
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060071F7 RID: 29175 RVA: 0x001A96F0 File Offset: 0x001A78F0
			internal void EndSendCloseResponse(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				try
				{
					this.EndSendMessage(result);
				}
				catch (CommunicationException e)
				{
					this.TraceSessionClosedResponseFailure(e);
				}
				catch (TimeoutException e2)
				{
					this.TraceSessionClosedResponseFailure(e2);
				}
			}

			// Token: 0x060071F8 RID: 29176 RVA: 0x001A9748 File Offset: 0x001A7948
			internal Message CreateCloseMessage(TimeSpan timeout)
			{
				RequestSecurityToken requestSecurityToken = new RequestSecurityToken(this.Settings.SecurityStandardsManager);
				requestSecurityToken.RequestType = this.Settings.SecurityStandardsManager.TrustDriver.RequestTypeClose;
				requestSecurityToken.CloseTarget = this.Settings.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(this.currentSessionToken, SecurityTokenReferenceStyle.External);
				requestSecurityToken.MakeReadOnly();
				Message message = Message.CreateMessage(this.messageVersion, ActionHeader.Create(this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseAction, this.messageVersion.Addressing), requestSecurityToken);
				RequestReplyCorrelator.PrepareRequest(message);
				if (this.LocalAddress != null)
				{
					message.Headers.ReplyTo = this.LocalAddress;
				}
				else if (message.Version.Addressing == AddressingVersion.WSAddressing10)
				{
					message.Headers.ReplyTo = null;
				}
				else
				{
					if (message.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							message.Version.Addressing
						})));
					}
					message.Headers.ReplyTo = EndpointAddress.AnonymousAddress;
				}
				this.securityProtocol.SecureOutgoingMessage(ref message, timeout, null);
				message.Properties.AllowOutputBatching = false;
				return message;
			}

			// Token: 0x060071F9 RID: 29177 RVA: 0x001A988C File Offset: 0x001A7A8C
			protected void SendClose(TimeSpan timeout)
			{
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					using (Message message = this.CreateCloseMessage(timeoutHelper.RemainingTime()))
					{
						this.SendMessage(null, message, timeoutHelper.RemainingTime());
					}
					this.TraceSessionClosedSuccess();
				}
				catch (CommunicationException e)
				{
					this.TraceSessionClosedFailure(e);
				}
				catch (TimeoutException e2)
				{
					this.TraceSessionClosedFailure(e2);
				}
			}

			// Token: 0x060071FA RID: 29178 RVA: 0x001A9910 File Offset: 0x001A7B10
			internal IAsyncResult BeginSendClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					Message response = this.CreateCloseMessage(timeoutHelper.RemainingTime());
					return this.BeginSendMessage(null, response, timeoutHelper.RemainingTime(), callback, state);
				}
				catch (CommunicationException e)
				{
					this.TraceSessionClosedFailure(e);
				}
				catch (TimeoutException e2)
				{
					this.TraceSessionClosedFailure(e2);
				}
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060071FB RID: 29179 RVA: 0x001A9984 File Offset: 0x001A7B84
			internal void EndSendClose(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				try
				{
					this.EndSendMessage(result);
				}
				catch (CommunicationException e)
				{
					this.TraceSessionClosedFailure(e);
				}
				catch (TimeoutException e2)
				{
					this.TraceSessionClosedFailure(e2);
				}
			}

			// Token: 0x060071FC RID: 29180 RVA: 0x001A99DC File Offset: 0x001A7BDC
			protected void SendMessage(RequestContext requestContext, Message message, TimeSpan timeout)
			{
				if (this.channelBinder.CanSendAsynchronously)
				{
					this.channelBinder.Send(message, timeout);
					return;
				}
				if (requestContext != null)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					requestContext.Reply(message, timeoutHelper.RemainingTime());
					requestContext.Close(timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x060071FD RID: 29181 RVA: 0x001A9A2A File Offset: 0x001A7C2A
			internal IAsyncResult BeginSendMessage(RequestContext requestContext, Message response, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult(this, requestContext, response, timeout, callback, state);
			}

			// Token: 0x060071FE RID: 29182 RVA: 0x001A9A39 File Offset: 0x001A7C39
			internal void EndSendMessage(IAsyncResult result)
			{
				SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.End(result);
			}

			// Token: 0x04004084 RID: 16516
			private FaultCode renewFaultCode;

			// Token: 0x04004085 RID: 16517
			private FaultReason renewFaultReason;

			// Token: 0x04004086 RID: 16518
			private FaultCode sessionAbortedFaultCode;

			// Token: 0x04004087 RID: 16519
			private FaultReason sessionAbortedFaultReason;

			// Token: 0x04004088 RID: 16520
			private volatile bool areFaultCodesInitialized;

			// Token: 0x04004089 RID: 16521
			private IServerReliableChannelBinder channelBinder;

			// Token: 0x0400408A RID: 16522
			private SecurityProtocol securityProtocol;

			// Token: 0x0400408B RID: 16523
			private SecurityContextSecurityToken currentSessionToken;

			// Token: 0x0400408C RID: 16524
			private UniqueId sessionId;

			// Token: 0x0400408D RID: 16525
			private List<SecurityContextSecurityToken> futureSessionTokens;

			// Token: 0x0400408E RID: 16526
			private SecuritySessionServerSettings settings;

			// Token: 0x0400408F RID: 16527
			private RequestContext initialRequestContext;

			// Token: 0x04004090 RID: 16528
			private volatile bool isInputClosed;

			// Token: 0x04004091 RID: 16529
			private ThreadNeutralSemaphore receiveLock;

			// Token: 0x04004092 RID: 16530
			private MessageVersion messageVersion;

			// Token: 0x04004093 RID: 16531
			private SecurityListenerSettingsLifetimeManager settingsLifetimeManager;

			// Token: 0x04004094 RID: 16532
			private volatile bool hasSecurityStateReference;

			// Token: 0x02000EEC RID: 3820
			private class SendMessageAsyncResult : AsyncResult
			{
				// Token: 0x06008504 RID: 34052 RVA: 0x001EBA68 File Offset: 0x001E9C68
				public SendMessageAsyncResult(SecuritySessionServerSettings.ServerSecuritySessionChannel sessionChannel, RequestContext requestContext, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.sessionChannel = sessionChannel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.requestContext = requestContext;
					this.message = message;
					bool flag = true;
					try
					{
						IAsyncResult asyncResult = this.BeginSend(message);
						if (!asyncResult.CompletedSynchronously)
						{
							flag = false;
							return;
						}
						this.EndSend(asyncResult);
						flag = false;
					}
					finally
					{
						if (flag)
						{
							this.message.Close();
						}
					}
					base.Complete(true);
				}

				// Token: 0x06008505 RID: 34053 RVA: 0x001EBAE8 File Offset: 0x001E9CE8
				private IAsyncResult BeginSend(Message response)
				{
					if (this.sessionChannel.channelBinder.CanSendAsynchronously)
					{
						return this.sessionChannel.channelBinder.BeginSend(response, this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.sendCallback, this);
					}
					if (this.requestContext != null)
					{
						return this.requestContext.BeginReply(response, SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.sendCallback, this);
					}
					return new SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.SendCompletedAsyncResult(SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.sendCallback, this);
				}

				// Token: 0x06008506 RID: 34054 RVA: 0x001EBB50 File Offset: 0x001E9D50
				private void EndSend(IAsyncResult result)
				{
					try
					{
						if (result is SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.SendCompletedAsyncResult)
						{
							SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.SendCompletedAsyncResult.End(result);
						}
						else if (this.sessionChannel.channelBinder.CanSendAsynchronously)
						{
							this.sessionChannel.channelBinder.EndSend(result);
						}
						else
						{
							this.requestContext.EndReply(result);
							this.requestContext.Close(this.timeoutHelper.RemainingTime());
						}
					}
					finally
					{
						if (this.message != null)
						{
							this.message.Close();
						}
					}
				}

				// Token: 0x06008507 RID: 34055 RVA: 0x001EBBDC File Offset: 0x001E9DDC
				private static void SendCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult sendMessageAsyncResult = (SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						sendMessageAsyncResult.EndSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					sendMessageAsyncResult.Complete(false, exception);
				}

				// Token: 0x06008508 RID: 34056 RVA: 0x001EBC30 File Offset: 0x001E9E30
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult>(result);
				}

				// Token: 0x04004D06 RID: 19718
				private static AsyncCallback sendCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.SendCallback));

				// Token: 0x04004D07 RID: 19719
				private SecuritySessionServerSettings.ServerSecuritySessionChannel sessionChannel;

				// Token: 0x04004D08 RID: 19720
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004D09 RID: 19721
				private RequestContext requestContext;

				// Token: 0x04004D0A RID: 19722
				private Message message;

				// Token: 0x02000FBF RID: 4031
				private class SendCompletedAsyncResult : CompletedAsyncResult
				{
					// Token: 0x060088AF RID: 34991 RVA: 0x001FD173 File Offset: 0x001FB373
					public SendCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
					{
					}

					// Token: 0x060088B0 RID: 34992 RVA: 0x001FD17D File Offset: 0x001FB37D
					public new static void End(IAsyncResult result)
					{
						AsyncResult.End<SecuritySessionServerSettings.ServerSecuritySessionChannel.SendMessageAsyncResult.SendCompletedAsyncResult>(result);
					}
				}
			}

			// Token: 0x02000EED RID: 3821
			private class CloseCoreAsyncResult : AsyncResult
			{
				// Token: 0x0600850A RID: 34058 RVA: 0x001EBC54 File Offset: 0x001E9E54
				public CloseCoreAsyncResult(SecuritySessionServerSettings.ServerSecuritySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.channel = channel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					bool flag = false;
					if (this.channel.channelBinder != null)
					{
						try
						{
							IAsyncResult asyncResult = this.channel.channelBinder.BeginClose(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult.channelBinderCloseCallback, this);
							if (!asyncResult.CompletedSynchronously)
							{
								return;
							}
							this.channel.channelBinder.EndClose(asyncResult);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (this.channel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						flag = this.OnChannelBinderClosed();
					}
					if (flag)
					{
						this.RemoveSessionTokenFromCache();
						base.Complete(true);
					}
				}

				// Token: 0x0600850B RID: 34059 RVA: 0x001EBD0C File Offset: 0x001E9F0C
				private void RemoveSessionTokenFromCache()
				{
					this.channel.Settings.SessionTokenCache.RemoveAllContexts(this.channel.currentSessionToken.ContextId);
				}

				// Token: 0x0600850C RID: 34060 RVA: 0x001EBD34 File Offset: 0x001E9F34
				private static void ChannelBinderCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult closeCoreAsyncResult = (SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						try
						{
							closeCoreAsyncResult.channel.channelBinder.EndClose(result);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeCoreAsyncResult.channel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
						}
						if (!flag)
						{
							flag = closeCoreAsyncResult.OnChannelBinderClosed();
						}
						if (flag)
						{
							closeCoreAsyncResult.RemoveSessionTokenFromCache();
						}
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
						closeCoreAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x0600850D RID: 34061 RVA: 0x001EBDD0 File Offset: 0x001E9FD0
				private bool OnChannelBinderClosed()
				{
					bool result;
					try
					{
						if (this.channel.securityProtocol != null)
						{
							this.channel.securityProtocol.Close(false, this.timeoutHelper.RemainingTime());
						}
						bool flag = false;
						object thisLock = this.channel.ThisLock;
						lock (thisLock)
						{
							if (this.channel.hasSecurityStateReference)
							{
								flag = true;
								this.channel.hasSecurityStateReference = false;
							}
						}
						if (!flag)
						{
							result = true;
						}
						else
						{
							IAsyncResult asyncResult = this.channel.settingsLifetimeManager.BeginClose(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult.settingsLifetimeManagerCloseCallback, this);
							if (!asyncResult.CompletedSynchronously)
							{
								result = false;
							}
							else
							{
								this.channel.settingsLifetimeManager.EndClose(asyncResult);
								result = true;
							}
						}
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.channel.State != CommunicationState.Closed)
						{
							throw;
						}
						result = true;
					}
					return result;
				}

				// Token: 0x0600850E RID: 34062 RVA: 0x001EBECC File Offset: 0x001EA0CC
				private static void SettingsLifetimeManagerCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult closeCoreAsyncResult = (SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						closeCoreAsyncResult.channel.settingsLifetimeManager.EndClose(result);
						flag = true;
					}
					catch (CommunicationObjectAbortedException)
					{
						if (closeCoreAsyncResult.channel.State != CommunicationState.Closed)
						{
							throw;
						}
						flag = true;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					finally
					{
						if (flag)
						{
							closeCoreAsyncResult.RemoveSessionTokenFromCache();
						}
					}
					closeCoreAsyncResult.Complete(false, exception);
				}

				// Token: 0x0600850F RID: 34063 RVA: 0x001EBF68 File Offset: 0x001EA168
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult>(result);
				}

				// Token: 0x04004D0B RID: 19723
				private static AsyncCallback channelBinderCloseCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult.ChannelBinderCloseCallback));

				// Token: 0x04004D0C RID: 19724
				private static AsyncCallback settingsLifetimeManagerCloseCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySessionChannel.CloseCoreAsyncResult.SettingsLifetimeManagerCloseCallback));

				// Token: 0x04004D0D RID: 19725
				private SecuritySessionServerSettings.ServerSecuritySessionChannel channel;

				// Token: 0x04004D0E RID: 19726
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EEE RID: 3822
			protected class SoapSecurityInputSession : ISecureConversationSession, ISecuritySession, ISession, IInputSession
			{
				// Token: 0x06008511 RID: 34065 RVA: 0x001EBFA0 File Offset: 0x001EA1A0
				public SoapSecurityInputSession(SecurityContextSecurityToken sessionToken, SecuritySessionServerSettings settings, SecuritySessionServerSettings.ServerSecuritySessionChannel channel)
				{
					this.channel = channel;
					this.securityContextTokenId = sessionToken.ContextId;
					Claim primaryIdentityClaim = SecurityUtils.GetPrimaryIdentityClaim(sessionToken.AuthorizationPolicies);
					if (primaryIdentityClaim != null)
					{
						this.remoteIdentity = EndpointIdentity.CreateIdentity(primaryIdentityClaim);
					}
					this.sessionTokenIdentifier = settings.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(sessionToken, SecurityTokenReferenceStyle.External);
					this.standardsManager = settings.SessionProtocolFactory.StandardsManager;
				}

				// Token: 0x17001D43 RID: 7491
				// (get) Token: 0x06008512 RID: 34066 RVA: 0x001EC005 File Offset: 0x001EA205
				public string Id
				{
					get
					{
						return this.securityContextTokenId.ToString();
					}
				}

				// Token: 0x17001D44 RID: 7492
				// (get) Token: 0x06008513 RID: 34067 RVA: 0x001EC012 File Offset: 0x001EA212
				public EndpointIdentity RemoteIdentity
				{
					get
					{
						return this.remoteIdentity;
					}
				}

				// Token: 0x06008514 RID: 34068 RVA: 0x001EC01A File Offset: 0x001EA21A
				public void WriteSessionTokenIdentifier(XmlDictionaryWriter writer)
				{
					this.channel.ThrowIfDisposedOrNotOpen();
					this.standardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, this.sessionTokenIdentifier);
				}

				// Token: 0x06008515 RID: 34069 RVA: 0x001EC040 File Offset: 0x001EA240
				public bool TryReadSessionTokenIdentifier(XmlReader reader)
				{
					this.channel.ThrowIfDisposedOrNotOpen();
					if (!this.standardsManager.SecurityTokenSerializer.CanReadKeyIdentifierClause(reader))
					{
						return false;
					}
					SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = this.standardsManager.SecurityTokenSerializer.ReadKeyIdentifierClause(reader) as SecurityContextKeyIdentifierClause;
					return securityContextKeyIdentifierClause != null && securityContextKeyIdentifierClause.Matches(this.securityContextTokenId, null);
				}

				// Token: 0x04004D0F RID: 19727
				private SecuritySessionServerSettings.ServerSecuritySessionChannel channel;

				// Token: 0x04004D10 RID: 19728
				private UniqueId securityContextTokenId;

				// Token: 0x04004D11 RID: 19729
				private EndpointIdentity remoteIdentity;

				// Token: 0x04004D12 RID: 19730
				private SecurityKeyIdentifierClause sessionTokenIdentifier;

				// Token: 0x04004D13 RID: 19731
				private SecurityStandardsManager standardsManager;
			}

			// Token: 0x02000EEF RID: 3823
			private class ReceiveRequestAsyncResult : AsyncResult
			{
				// Token: 0x06008516 RID: 34070 RVA: 0x001EC098 File Offset: 0x001EA298
				public ReceiveRequestAsyncResult(SecuritySessionServerSettings.ServerSecuritySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					channel.ThrowIfFaulted();
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.channel = channel;
					if (!channel.receiveLock.EnterAsync(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.onWait, this))
					{
						return;
					}
					bool flag = false;
					bool flag2 = true;
					try
					{
						flag = this.WaitComplete();
						flag2 = false;
					}
					finally
					{
						if (flag2)
						{
							this.channel.receiveLock.Exit();
						}
					}
					if (flag)
					{
						this.Complete(true);
					}
				}

				// Token: 0x06008517 RID: 34071 RVA: 0x001EC124 File Offset: 0x001EA324
				private static void OnWait(object state, Exception asyncException)
				{
					SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult receiveRequestAsyncResult = (SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult)state;
					bool flag = false;
					Exception ex = asyncException;
					if (ex != null)
					{
						flag = true;
					}
					else
					{
						try
						{
							flag = receiveRequestAsyncResult.WaitComplete();
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							flag = true;
							ex = ex2;
						}
					}
					if (flag)
					{
						receiveRequestAsyncResult.Complete(false, ex);
					}
				}

				// Token: 0x06008518 RID: 34072 RVA: 0x001EC17C File Offset: 0x001EA37C
				private bool WaitComplete()
				{
					if (this.channel.isInputClosed)
					{
						return true;
					}
					this.channel.ThrowIfFaulted();
					ServiceModelActivity activity = (DiagnosticUtility.ShouldUseActivity && this.channel.initialRequestContext != null) ? TraceUtility.ExtractActivity(this.channel.initialRequestContext.RequestMessage) : null;
					bool result;
					using (ServiceModelActivity.BoundOperation(activity))
					{
						if (this.channel.initialRequestContext != null)
						{
							this.innerRequestContext = this.channel.initialRequestContext;
							this.channel.initialRequestContext = null;
							bool flag;
							this.requestMessage = this.channel.ProcessRequestContext(this.innerRequestContext, this.timeoutHelper.RemainingTime(), out this.correlationState, out flag);
							if (this.requestMessage != null || this.channel.isInputClosed)
							{
								this.expired = false;
								return true;
							}
						}
						if (this.timeoutHelper.RemainingTime() == TimeSpan.Zero)
						{
							this.expired = true;
							result = true;
						}
						else
						{
							IAsyncResult asyncResult = this.channel.ChannelBinder.BeginTryReceive(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.onReceive, this);
							if (!asyncResult.CompletedSynchronously)
							{
								result = false;
							}
							else
							{
								result = this.CompleteReceive(asyncResult);
							}
						}
					}
					return result;
				}

				// Token: 0x06008519 RID: 34073 RVA: 0x001EC2CC File Offset: 0x001EA4CC
				private bool CompleteReceive(IAsyncResult result)
				{
					for (;;)
					{
						this.expired = !this.channel.ChannelBinder.EndTryReceive(result, out this.innerRequestContext);
						if (this.expired || this.innerRequestContext == null)
						{
							goto IL_117;
						}
						bool flag;
						this.requestMessage = this.channel.ProcessRequestContext(this.innerRequestContext, this.timeoutHelper.RemainingTime(), out this.correlationState, out flag);
						if (this.requestMessage != null)
						{
							if (!this.channel.isInputClosed)
							{
								goto IL_117;
							}
							ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(this.requestMessage);
							try
							{
								throw TraceUtility.ThrowHelperWarning(exception, this.requestMessage);
							}
							finally
							{
								this.requestMessage.Close();
								this.innerRequestContext.Abort();
							}
						}
						if (this.channel.isInputClosed || this.channel.State == CommunicationState.Faulted)
						{
							goto IL_117;
						}
						if (this.timeoutHelper.RemainingTime() == TimeSpan.Zero)
						{
							break;
						}
						result = this.channel.ChannelBinder.BeginTryReceive(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.onReceive, this);
						if (!result.CompletedSynchronously)
						{
							return false;
						}
					}
					this.expired = true;
					IL_117:
					this.channel.ThrowIfFaulted();
					return true;
				}

				// Token: 0x0600851A RID: 34074 RVA: 0x001EC40C File Offset: 0x001EA60C
				private new void Complete(bool synchronous)
				{
					try
					{
						this.channel.receiveLock.Exit();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							TraceUtility.TraceEvent(TraceEventType.Warning, 524289, SR.GetString("TraceCodeAsyncCallbackThrewException"), ex.ToString());
						}
					}
					base.Complete(synchronous);
				}

				// Token: 0x0600851B RID: 34075 RVA: 0x001EC474 File Offset: 0x001EA674
				private new void Complete(bool synchronous, Exception exception)
				{
					try
					{
						this.channel.receiveLock.Exit();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							TraceUtility.TraceEvent(TraceEventType.Warning, 524289, SR.GetString("TraceCodeAsyncCallbackThrewException"), ex.ToString());
						}
					}
					base.Complete(synchronous, exception);
				}

				// Token: 0x0600851C RID: 34076 RVA: 0x001EC4DC File Offset: 0x001EA6DC
				private static SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult End(IAsyncResult result)
				{
					return AsyncResult.End<SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult>(result);
				}

				// Token: 0x0600851D RID: 34077 RVA: 0x001EC4E4 File Offset: 0x001EA6E4
				public static bool EndAsMessage(IAsyncResult result, out Message message)
				{
					SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult receiveRequestAsyncResult = SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.End(result);
					message = receiveRequestAsyncResult.requestMessage;
					if (message != null && receiveRequestAsyncResult.innerRequestContext != null)
					{
						try
						{
							receiveRequestAsyncResult.innerRequestContext.Close(receiveRequestAsyncResult.timeoutHelper.RemainingTime());
						}
						catch (TimeoutException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
					}
					return !receiveRequestAsyncResult.expired;
				}

				// Token: 0x0600851E RID: 34078 RVA: 0x001EC548 File Offset: 0x001EA748
				public static bool EndAsRequestContext(IAsyncResult result, out RequestContext requestContext)
				{
					SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult receiveRequestAsyncResult = SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.End(result);
					if (receiveRequestAsyncResult.requestMessage == null)
					{
						requestContext = null;
					}
					else
					{
						requestContext = new SecuritySessionServerSettings.SecuritySessionRequestContext(receiveRequestAsyncResult.innerRequestContext, receiveRequestAsyncResult.requestMessage, receiveRequestAsyncResult.correlationState, receiveRequestAsyncResult.channel);
					}
					return !receiveRequestAsyncResult.expired;
				}

				// Token: 0x0600851F RID: 34079 RVA: 0x001EC594 File Offset: 0x001EA794
				private static void OnReceive(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult receiveRequestAsyncResult = (SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						flag = receiveRequestAsyncResult.CompleteReceive(result);
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
						receiveRequestAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x04004D14 RID: 19732
				private static FastAsyncCallback onWait = new FastAsyncCallback(SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.OnWait);

				// Token: 0x04004D15 RID: 19733
				private static AsyncCallback onReceive = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySessionChannel.ReceiveRequestAsyncResult.OnReceive));

				// Token: 0x04004D16 RID: 19734
				private SecuritySessionServerSettings.ServerSecuritySessionChannel channel;

				// Token: 0x04004D17 RID: 19735
				private RequestContext innerRequestContext;

				// Token: 0x04004D18 RID: 19736
				private Message requestMessage;

				// Token: 0x04004D19 RID: 19737
				private SecurityProtocolCorrelationState correlationState;

				// Token: 0x04004D1A RID: 19738
				private bool expired;

				// Token: 0x04004D1B RID: 19739
				private TimeoutHelper timeoutHelper;
			}
		}

		// Token: 0x02000B5E RID: 2910
		private abstract class ServerSecuritySimplexSessionChannel : SecuritySessionServerSettings.ServerSecuritySessionChannel
		{
			// Token: 0x060071FF RID: 29183 RVA: 0x001A9A41 File Offset: 0x001A7C41
			public ServerSecuritySimplexSessionChannel(SecuritySessionServerSettings settings, IServerReliableChannelBinder channelBinder, SecurityContextSecurityToken sessionToken, object listenerSecurityState, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(settings, channelBinder, sessionToken, listenerSecurityState, settingsLifetimeManager)
			{
				this.session = new SecuritySessionServerSettings.ServerSecuritySessionChannel.SoapSecurityInputSession(sessionToken, settings, this);
			}

			// Token: 0x17001A7E RID: 6782
			// (get) Token: 0x06007200 RID: 29184 RVA: 0x001A9A6A File Offset: 0x001A7C6A
			public IInputSession Session
			{
				get
				{
					return this.session;
				}
			}

			// Token: 0x06007201 RID: 29185 RVA: 0x001A9A74 File Offset: 0x001A7C74
			private void CleanupPendingCloseState()
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.closeResponse != null)
					{
						this.closeResponse.Close();
						this.closeResponse = null;
					}
					if (this.closeRequestContext != null)
					{
						this.closeRequestContext.Abort();
						this.closeRequestContext = null;
					}
				}
			}

			// Token: 0x06007202 RID: 29186 RVA: 0x001A9AE4 File Offset: 0x001A7CE4
			protected override void AbortCore()
			{
				base.AbortCore();
				base.Settings.RemoveSessionChannel(this.session.Id);
				this.CleanupPendingCloseState();
			}

			// Token: 0x06007203 RID: 29187 RVA: 0x001A9B08 File Offset: 0x001A7D08
			protected override void CloseCore(TimeSpan timeout)
			{
				base.CloseCore(timeout);
				this.inputSessionClosedHandle.Abort(this);
				base.Settings.RemoveSessionChannel(this.session.Id);
			}

			// Token: 0x06007204 RID: 29188 RVA: 0x001A9B33 File Offset: 0x001A7D33
			protected override void EndCloseCore(IAsyncResult result)
			{
				base.EndCloseCore(result);
				this.inputSessionClosedHandle.Abort(this);
				base.Settings.RemoveSessionChannel(this.session.Id);
			}

			// Token: 0x06007205 RID: 29189 RVA: 0x001A9B5E File Offset: 0x001A7D5E
			protected override void OnAbort()
			{
				this.AbortCore();
				this.inputSessionClosedHandle.Abort(this);
			}

			// Token: 0x06007206 RID: 29190 RVA: 0x001A9B72 File Offset: 0x001A7D72
			protected override void OnFaulted()
			{
				this.AbortCore();
				this.inputSessionClosedHandle.Fault(this);
				base.OnFaulted();
			}

			// Token: 0x06007207 RID: 29191 RVA: 0x001A9B8C File Offset: 0x001A7D8C
			private bool ShouldSendCloseResponseOnClose(out RequestContext pendingCloseRequestContext, out Message pendingCloseResponse)
			{
				bool result = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.canSendCloseResponse = true;
					if (!this.sentCloseResponse && this.receivedClose && this.closeResponse != null)
					{
						this.sentCloseResponse = true;
						result = true;
						pendingCloseRequestContext = this.closeRequestContext;
						pendingCloseResponse = this.closeResponse;
						this.closeResponse = null;
						this.closeRequestContext = null;
					}
					else
					{
						this.canSendCloseResponse = false;
						pendingCloseRequestContext = null;
						pendingCloseResponse = null;
					}
				}
				return result;
			}

			// Token: 0x06007208 RID: 29192 RVA: 0x001A9C20 File Offset: 0x001A7E20
			private bool SendCloseResponseOnCloseIfRequired(TimeSpan timeout)
			{
				bool result = false;
				RequestContext requestContext;
				Message message;
				bool flag = this.ShouldSendCloseResponseOnClose(out requestContext, out message);
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				bool flag2 = true;
				if (flag)
				{
					try
					{
						base.SendCloseResponse(requestContext, message, timeoutHelper.RemainingTime());
						this.inputSessionClosedHandle.Set();
						flag2 = false;
					}
					catch (CommunicationObjectAbortedException)
					{
						if (base.State != CommunicationState.Closed)
						{
							throw;
						}
						result = true;
					}
					finally
					{
						if (flag2)
						{
							if (message != null)
							{
								message.Close();
							}
							if (requestContext != null)
							{
								requestContext.Abort();
							}
						}
					}
				}
				return result;
			}

			// Token: 0x06007209 RID: 29193 RVA: 0x001A9CAC File Offset: 0x001A7EAC
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				bool flag = this.SendCloseResponseOnCloseIfRequired(timeoutHelper.RemainingTime());
				if (flag)
				{
					return;
				}
				bool flag2 = this.WaitForInputSessionClose(timeoutHelper.RemainingTime(), out flag);
				if (flag)
				{
					return;
				}
				if (!flag2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
					{
						timeoutHelper.OriginalTimeout
					})));
				}
				this.CloseCore(timeoutHelper.RemainingTime());
			}

			// Token: 0x0600720A RID: 29194 RVA: 0x001A9D28 File Offset: 0x001A7F28
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				RequestContext requestContext;
				Message message;
				bool sendCloseResponse = this.ShouldSendCloseResponseOnClose(out requestContext, out message);
				return new SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult(this, sendCloseResponse, requestContext, message, timeout, callback, state);
			}

			// Token: 0x0600720B RID: 29195 RVA: 0x001A9D4C File Offset: 0x001A7F4C
			protected override void OnEndClose(IAsyncResult result)
			{
				SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.End(result);
			}

			// Token: 0x0600720C RID: 29196 RVA: 0x001A9D54 File Offset: 0x001A7F54
			private bool WaitForInputSessionClose(TimeSpan timeout, out bool wasAborted)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				wasAborted = false;
				try
				{
					Message message;
					if (base.TryReceive(timeoutHelper.RemainingTime(), out message))
					{
						if (message != null)
						{
							using (message)
							{
								ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
								throw TraceUtility.ThrowHelperWarning(exception, message);
							}
						}
						return this.inputSessionClosedHandle.Wait(timeoutHelper.RemainingTime(), false);
					}
				}
				catch (CommunicationObjectAbortedException)
				{
					if (base.State != CommunicationState.Closed)
					{
						throw;
					}
					wasAborted = true;
				}
				return false;
			}

			// Token: 0x0600720D RID: 29197 RVA: 0x001A9DE8 File Offset: 0x001A7FE8
			protected override void OnCloseResponseMessageReceived(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				message.Close();
				requestContext.Abort();
				base.Fault(new ProtocolException(SR.GetString("UnexpectedSecuritySessionCloseResponse")));
			}

			// Token: 0x0600720E RID: 29198 RVA: 0x001A9E0C File Offset: 0x001A800C
			protected override void OnCloseMessageReceived(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				if (base.State == CommunicationState.Created)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServerReceivedCloseMessageStateIsCreated", new object[]
					{
						base.GetType().ToString()
					})));
				}
				if (this.SendCloseResponseOnCloseReceivedIfRequired(requestContext, message, correlationState, timeout))
				{
					this.inputSessionClosedHandle.Set();
				}
			}

			// Token: 0x0600720F RID: 29199 RVA: 0x001A9E68 File Offset: 0x001A8068
			private bool SendCloseResponseOnCloseReceivedIfRequired(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				bool flag = false;
				ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? TraceUtility.ExtractActivity(message) : null;
				bool flag2 = true;
				bool result;
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					Message message2 = null;
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (!this.receivedClose)
						{
							this.receivedClose = true;
							message2 = base.CreateCloseResponse(message, correlationState, timeoutHelper.RemainingTime());
							if (this.canSendCloseResponse)
							{
								this.sentCloseResponse = true;
								flag = true;
							}
							else
							{
								this.closeRequestContext = requestContext;
								this.closeResponse = message2;
								flag2 = false;
							}
						}
					}
					if (flag)
					{
						base.SendCloseResponse(requestContext, message2, timeoutHelper.RemainingTime());
						flag2 = false;
					}
					else if (flag2)
					{
						requestContext.Close(timeoutHelper.RemainingTime());
						flag2 = false;
					}
					result = flag;
				}
				finally
				{
					message.Close();
					if (flag2)
					{
						requestContext.Abort();
					}
					if (DiagnosticUtility.ShouldUseActivity && serviceModelActivity != null)
					{
						serviceModelActivity.Stop();
					}
				}
				return result;
			}

			// Token: 0x04004095 RID: 16533
			private SecuritySessionServerSettings.ServerSecuritySessionChannel.SoapSecurityInputSession session;

			// Token: 0x04004096 RID: 16534
			private bool receivedClose;

			// Token: 0x04004097 RID: 16535
			private bool canSendCloseResponse;

			// Token: 0x04004098 RID: 16536
			private bool sentCloseResponse;

			// Token: 0x04004099 RID: 16537
			private RequestContext closeRequestContext;

			// Token: 0x0400409A RID: 16538
			private Message closeResponse;

			// Token: 0x0400409B RID: 16539
			private InterruptibleWaitObject inputSessionClosedHandle = new InterruptibleWaitObject(false);

			// Token: 0x02000EF0 RID: 3824
			private class CloseAsyncResult : AsyncResult
			{
				// Token: 0x06008521 RID: 34081 RVA: 0x001EC61C File Offset: 0x001EA81C
				public CloseAsyncResult(SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel sessionChannel, bool sendCloseResponse, RequestContext closeRequestContext, Message closeResponse, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.sessionChannel = sessionChannel;
					this.closeRequestContext = closeRequestContext;
					this.closeResponse = closeResponse;
					bool flag = false;
					bool flag2 = this.OnSendCloseResponse(sendCloseResponse, out flag);
					if (flag || flag2)
					{
						base.Complete(true);
					}
				}

				// Token: 0x06008522 RID: 34082 RVA: 0x001EC670 File Offset: 0x001EA870
				private bool OnSendCloseResponse(bool shouldSendCloseResponse, out bool wasChannelAborted)
				{
					wasChannelAborted = false;
					try
					{
						if (shouldSendCloseResponse)
						{
							bool flag = true;
							try
							{
								IAsyncResult asyncResult = this.sessionChannel.BeginSendCloseResponse(this.closeRequestContext, this.closeResponse, this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.sendCloseResponseCallback, this);
								if (!asyncResult.CompletedSynchronously)
								{
									flag = false;
									return false;
								}
								this.sessionChannel.EndSendCloseResponse(asyncResult);
								this.sessionChannel.inputSessionClosedHandle.Set();
							}
							finally
							{
								if (flag)
								{
									this.CleanupCloseState();
								}
							}
						}
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						wasChannelAborted = true;
					}
					return wasChannelAborted || this.OnReceiveNullMessage(out wasChannelAborted);
				}

				// Token: 0x06008523 RID: 34083 RVA: 0x001EC728 File Offset: 0x001EA928
				private void CleanupCloseState()
				{
					if (this.closeResponse != null)
					{
						this.closeResponse.Close();
					}
					if (this.closeRequestContext != null)
					{
						this.closeRequestContext.Abort();
					}
				}

				// Token: 0x06008524 RID: 34084 RVA: 0x001EC750 File Offset: 0x001EA950
				private static void SendCloseResponseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						bool flag2 = false;
						try
						{
							closeAsyncResult.sessionChannel.EndSendCloseResponse(result);
							closeAsyncResult.sessionChannel.inputSessionClosedHandle.Set();
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag2 = true;
							flag = true;
						}
						finally
						{
							closeAsyncResult.CleanupCloseState();
						}
						if (!flag2)
						{
							flag = closeAsyncResult.OnReceiveNullMessage(out flag2);
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x06008525 RID: 34085 RVA: 0x001EC80C File Offset: 0x001EAA0C
				private bool OnReceiveNullMessage(out bool wasChannelAborted)
				{
					wasChannelAborted = false;
					bool flag = false;
					Message message = null;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.BeginTryReceive(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.receiveCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						flag = this.sessionChannel.EndTryReceive(asyncResult, out message);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						wasChannelAborted = true;
					}
					if (wasChannelAborted)
					{
						return true;
					}
					if (flag)
					{
						return this.OnMessageReceived(message);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
					{
						this.timeoutHelper.OriginalTimeout
					})));
				}

				// Token: 0x06008526 RID: 34086 RVA: 0x001EC8C8 File Offset: 0x001EAAC8
				private static void ReceiveCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						Message message = null;
						bool flag2 = false;
						bool flag3 = false;
						try
						{
							flag3 = closeAsyncResult.sessionChannel.EndTryReceive(result, out message);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag2 = true;
							flag = true;
						}
						if (!flag2)
						{
							if (!flag3)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
								{
									closeAsyncResult.timeoutHelper.OriginalTimeout
								})));
							}
							flag = closeAsyncResult.OnMessageReceived(message);
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x06008527 RID: 34087 RVA: 0x001EC9A4 File Offset: 0x001EABA4
				private bool OnMessageReceived(Message message)
				{
					if (message != null)
					{
						try
						{
							ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
							throw TraceUtility.ThrowHelperWarning(exception, message);
						}
						finally
						{
							if (message != null)
							{
								((IDisposable)message).Dispose();
							}
						}
					}
					bool closeCompleted = false;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.inputSessionClosedHandle.BeginWait(this.timeoutHelper.RemainingTime(), true, SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.waitCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						this.sessionChannel.inputSessionClosedHandle.EndWait(asyncResult);
						closeCompleted = true;
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						return true;
					}
					catch (TimeoutException)
					{
						closeCompleted = false;
					}
					return this.OnWaitOver(closeCompleted);
				}

				// Token: 0x06008528 RID: 34088 RVA: 0x001ECA64 File Offset: 0x001EAC64
				private static void WaitForInputSessionCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						bool closeCompleted = false;
						bool flag2 = false;
						try
						{
							closeAsyncResult.sessionChannel.inputSessionClosedHandle.EndWait(result);
							closeCompleted = true;
						}
						catch (TimeoutException)
						{
							closeCompleted = false;
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag2 = true;
							flag = true;
						}
						if (!flag2)
						{
							flag = closeAsyncResult.OnWaitOver(closeCompleted);
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x06008529 RID: 34089 RVA: 0x001ECB18 File Offset: 0x001EAD18
				private bool OnWaitOver(bool closeCompleted)
				{
					if (!closeCompleted)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
						{
							this.timeoutHelper.OriginalTimeout
						})));
					}
					IAsyncResult asyncResult = this.sessionChannel.BeginCloseCore(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.closeCoreCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.sessionChannel.EndCloseCore(asyncResult);
					return true;
				}

				// Token: 0x0600852A RID: 34090 RVA: 0x001ECB90 File Offset: 0x001EAD90
				private static void CloseCoreCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						closeAsyncResult.sessionChannel.EndCloseCore(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					closeAsyncResult.Complete(false, exception);
				}

				// Token: 0x0600852B RID: 34091 RVA: 0x001ECBEC File Offset: 0x001EADEC
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult>(result);
				}

				// Token: 0x04004D1C RID: 19740
				private static readonly AsyncCallback sendCloseResponseCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.SendCloseResponseCallback));

				// Token: 0x04004D1D RID: 19741
				private static readonly AsyncCallback receiveCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.ReceiveCallback));

				// Token: 0x04004D1E RID: 19742
				private static readonly AsyncCallback waitCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.WaitForInputSessionCloseCallback));

				// Token: 0x04004D1F RID: 19743
				private static readonly AsyncCallback closeCoreCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel.CloseAsyncResult.CloseCoreCallback));

				// Token: 0x04004D20 RID: 19744
				private SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel sessionChannel;

				// Token: 0x04004D21 RID: 19745
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004D22 RID: 19746
				private RequestContext closeRequestContext;

				// Token: 0x04004D23 RID: 19747
				private Message closeResponse;
			}
		}

		// Token: 0x02000B5F RID: 2911
		private class SecurityReplySessionChannel : SecuritySessionServerSettings.ServerSecuritySimplexSessionChannel, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
		{
			// Token: 0x06007210 RID: 29200 RVA: 0x001A9F68 File Offset: 0x001A8168
			public SecurityReplySessionChannel(SecuritySessionServerSettings settings, IServerReliableChannelBinder channelBinder, SecurityContextSecurityToken sessionToken, object listenerSecurityState, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(settings, channelBinder, sessionToken, listenerSecurityState, settingsLifetimeManager)
			{
			}

			// Token: 0x17001A7F RID: 6783
			// (get) Token: 0x06007211 RID: 29201 RVA: 0x001A9F77 File Offset: 0x001A8177
			protected override bool CanDoSecurityCorrelation
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06007212 RID: 29202 RVA: 0x001A9F7A File Offset: 0x001A817A
			public bool WaitForRequest(TimeSpan timeout)
			{
				return base.ChannelBinder.WaitForRequest(timeout);
			}

			// Token: 0x06007213 RID: 29203 RVA: 0x001A9F88 File Offset: 0x001A8188
			public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.ChannelBinder.BeginWaitForRequest(timeout, callback, state);
			}

			// Token: 0x06007214 RID: 29204 RVA: 0x001A9F98 File Offset: 0x001A8198
			public bool EndWaitForRequest(IAsyncResult result)
			{
				return base.ChannelBinder.EndWaitForRequest(result);
			}
		}

		// Token: 0x02000B60 RID: 2912
		private class SecuritySessionRequestContext : RequestContextBase
		{
			// Token: 0x06007215 RID: 29205 RVA: 0x001A9FA6 File Offset: 0x001A81A6
			public SecuritySessionRequestContext(RequestContext requestContext, Message requestMessage, SecurityProtocolCorrelationState correlationState, SecuritySessionServerSettings.ServerSecuritySessionChannel channel) : base(requestMessage, channel.InternalCloseTimeout, channel.InternalSendTimeout)
			{
				this.requestContext = requestContext;
				this.correlationState = correlationState;
				this.channel = channel;
			}

			// Token: 0x06007216 RID: 29206 RVA: 0x001A9FD3 File Offset: 0x001A81D3
			protected override void OnAbort()
			{
				this.requestContext.Abort();
			}

			// Token: 0x06007217 RID: 29207 RVA: 0x001A9FE0 File Offset: 0x001A81E0
			protected override void OnClose(TimeSpan timeout)
			{
				this.requestContext.Close(timeout);
			}

			// Token: 0x06007218 RID: 29208 RVA: 0x001A9FF0 File Offset: 0x001A81F0
			protected override void OnReply(Message message, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (message != null)
				{
					this.channel.SecureApplicationMessage(ref message, timeoutHelper.RemainingTime(), this.correlationState);
				}
				this.requestContext.Reply(message, timeoutHelper.RemainingTime());
			}

			// Token: 0x06007219 RID: 29209 RVA: 0x001AA038 File Offset: 0x001A8238
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (message != null)
				{
					this.channel.SecureApplicationMessage(ref message, timeoutHelper.RemainingTime(), this.correlationState);
				}
				return this.requestContext.BeginReply(message, timeoutHelper.RemainingTime(), callback, state);
			}

			// Token: 0x0600721A RID: 29210 RVA: 0x001AA080 File Offset: 0x001A8280
			protected override void OnEndReply(IAsyncResult result)
			{
				this.requestContext.EndReply(result);
			}

			// Token: 0x0400409C RID: 16540
			private RequestContext requestContext;

			// Token: 0x0400409D RID: 16541
			private SecuritySessionServerSettings.ServerSecuritySessionChannel channel;

			// Token: 0x0400409E RID: 16542
			private SecurityProtocolCorrelationState correlationState;
		}

		// Token: 0x02000B61 RID: 2913
		private class ServerSecurityDuplexSessionChannel : SecuritySessionServerSettings.ServerSecuritySessionChannel, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x0600721B RID: 29211 RVA: 0x001AA08E File Offset: 0x001A828E
			public ServerSecurityDuplexSessionChannel(SecuritySessionServerSettings settings, IServerReliableChannelBinder channelBinder, SecurityContextSecurityToken sessionToken, object listenerSecurityState, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(settings, channelBinder, sessionToken, listenerSecurityState, settingsLifetimeManager)
			{
				this.session = new SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.SoapSecurityServerDuplexSession(sessionToken, settings, this);
			}

			// Token: 0x17001A80 RID: 6784
			// (get) Token: 0x0600721C RID: 29212 RVA: 0x001AA0C3 File Offset: 0x001A82C3
			public EndpointAddress RemoteAddress
			{
				get
				{
					return base.ChannelBinder.RemoteAddress;
				}
			}

			// Token: 0x17001A81 RID: 6785
			// (get) Token: 0x0600721D RID: 29213 RVA: 0x001AA0D0 File Offset: 0x001A82D0
			public Uri Via
			{
				get
				{
					return this.RemoteAddress.Uri;
				}
			}

			// Token: 0x17001A82 RID: 6786
			// (get) Token: 0x0600721E RID: 29214 RVA: 0x001AA0DD File Offset: 0x001A82DD
			public IDuplexSession Session
			{
				get
				{
					return this.session;
				}
			}

			// Token: 0x0600721F RID: 29215 RVA: 0x001AA0E5 File Offset: 0x001A82E5
			public void Send(Message message)
			{
				this.Send(message, base.DefaultSendTimeout);
			}

			// Token: 0x06007220 RID: 29216 RVA: 0x001AA0F4 File Offset: 0x001A82F4
			public void Send(Message message, TimeSpan timeout)
			{
				this.CheckOutputOpen();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecureApplicationMessage(ref message, timeoutHelper.RemainingTime(), null);
				base.ChannelBinder.Send(message, timeoutHelper.RemainingTime());
			}

			// Token: 0x06007221 RID: 29217 RVA: 0x001AA132 File Offset: 0x001A8332
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x06007222 RID: 29218 RVA: 0x001AA144 File Offset: 0x001A8344
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.CheckOutputOpen();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecureApplicationMessage(ref message, timeoutHelper.RemainingTime(), null);
				return base.ChannelBinder.BeginSend(message, timeoutHelper.RemainingTime(), callback, state);
			}

			// Token: 0x06007223 RID: 29219 RVA: 0x001AA185 File Offset: 0x001A8385
			public void EndSend(IAsyncResult result)
			{
				base.ChannelBinder.EndSend(result);
			}

			// Token: 0x06007224 RID: 29220 RVA: 0x001AA193 File Offset: 0x001A8393
			protected override void AbortCore()
			{
				base.AbortCore();
				base.Settings.RemoveSessionChannel(this.session.Id);
				this.CleanupPendingCloseState();
			}

			// Token: 0x06007225 RID: 29221 RVA: 0x001AA1B8 File Offset: 0x001A83B8
			private void CleanupPendingCloseState()
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.closeResponseMessage != null)
					{
						this.closeResponseMessage.Close();
						this.closeResponseMessage = null;
					}
					if (this.closeRequestContext != null)
					{
						this.closeRequestContext.Abort();
						this.closeRequestContext = null;
					}
				}
			}

			// Token: 0x06007226 RID: 29222 RVA: 0x001AA228 File Offset: 0x001A8428
			protected override void OnAbort()
			{
				this.AbortCore();
				this.inputSessionCloseHandle.Abort(this);
				this.outputSessionCloseHandle.Abort(this);
			}

			// Token: 0x06007227 RID: 29223 RVA: 0x001AA248 File Offset: 0x001A8448
			protected override void OnFaulted()
			{
				this.AbortCore();
				this.inputSessionCloseHandle.Fault(this);
				this.outputSessionCloseHandle.Fault(this);
				base.OnFaulted();
			}

			// Token: 0x06007228 RID: 29224 RVA: 0x001AA26E File Offset: 0x001A846E
			protected override void CloseCore(TimeSpan timeout)
			{
				base.CloseCore(timeout);
				this.inputSessionCloseHandle.Abort(this);
				this.outputSessionCloseHandle.Abort(this);
				base.Settings.RemoveSessionChannel(this.session.Id);
			}

			// Token: 0x06007229 RID: 29225 RVA: 0x001AA2A5 File Offset: 0x001A84A5
			protected override void EndCloseCore(IAsyncResult result)
			{
				base.EndCloseCore(result);
				this.inputSessionCloseHandle.Abort(this);
				this.outputSessionCloseHandle.Abort(this);
				base.Settings.RemoveSessionChannel(this.session.Id);
			}

			// Token: 0x0600722A RID: 29226 RVA: 0x001AA2DC File Offset: 0x001A84DC
			protected void CheckOutputOpen()
			{
				base.ThrowIfClosedOrNotOpen();
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.isOutputClosed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationException(SR.GetString("OutputNotExpected")));
					}
				}
			}

			// Token: 0x0600722B RID: 29227 RVA: 0x001AA340 File Offset: 0x001A8540
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x0600722C RID: 29228 RVA: 0x001AA34B File Offset: 0x001A854B
			protected override void OnEndClose(IAsyncResult result)
			{
				SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.End(result);
			}

			// Token: 0x0600722D RID: 29229 RVA: 0x001AA354 File Offset: 0x001A8554
			internal bool WaitForOutputSessionClose(TimeSpan timeout, out bool wasAborted)
			{
				wasAborted = false;
				bool result;
				try
				{
					result = this.outputSessionCloseHandle.Wait(timeout, false);
				}
				catch (CommunicationObjectAbortedException)
				{
					if (base.State != CommunicationState.Closed)
					{
						throw;
					}
					wasAborted = true;
					result = true;
				}
				return result;
			}

			// Token: 0x0600722E RID: 29230 RVA: 0x001AA398 File Offset: 0x001A8598
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.CloseOutputSession(timeoutHelper.RemainingTime());
				if (base.State == CommunicationState.Closed)
				{
					return;
				}
				bool flag2;
				bool flag = this.WaitForInputSessionClose(timeoutHelper.RemainingTime(), out flag2);
				if (flag2)
				{
					return;
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
					{
						timeoutHelper.OriginalTimeout
					})));
				}
				bool flag3 = this.WaitForOutputSessionClose(timeoutHelper.RemainingTime(), out flag2);
				if (flag2)
				{
					return;
				}
				if (!flag3)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ServiceSecurityCloseOutputSessionTimeout", new object[]
					{
						timeoutHelper.OriginalTimeout
					})));
				}
				this.CloseCore(timeoutHelper.RemainingTime());
			}

			// Token: 0x0600722F RID: 29231 RVA: 0x001AA460 File Offset: 0x001A8660
			private bool WaitForInputSessionClose(TimeSpan timeout, out bool wasAborted)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				wasAborted = false;
				try
				{
					Message message;
					if (!base.TryReceive(timeoutHelper.RemainingTime(), out message))
					{
						return false;
					}
					if (message != null)
					{
						using (message)
						{
							ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
							throw TraceUtility.ThrowHelperWarning(exception, message);
						}
					}
					if (!this.inputSessionCloseHandle.Wait(timeoutHelper.RemainingTime(), false))
					{
						return false;
					}
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (!this.isInputClosed)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ShutdownRequestWasNotReceived")));
						}
					}
					return true;
				}
				catch (CommunicationObjectAbortedException)
				{
					if (base.State != CommunicationState.Closed)
					{
						throw;
					}
					wasAborted = true;
				}
				return false;
			}

			// Token: 0x06007230 RID: 29232 RVA: 0x001AA550 File Offset: 0x001A8750
			protected override void OnCloseMessageReceived(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				if (base.State == CommunicationState.Created)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServerReceivedCloseMessageStateIsCreated", new object[]
					{
						base.GetType().ToString()
					})));
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				bool flag = false;
				bool flag2 = true;
				try
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						this.receivedClose = true;
						if (!this.isInputClosed)
						{
							this.isInputClosed = true;
							flag = true;
							if (!this.isOutputClosed)
							{
								this.closeRequestContext = requestContext;
								this.closeResponseMessage = base.CreateCloseResponse(message, null, timeoutHelper.RemainingTime());
								flag2 = false;
							}
						}
					}
					if (flag)
					{
						this.inputSessionCloseHandle.Set();
					}
					if (flag2)
					{
						requestContext.Close(timeoutHelper.RemainingTime());
						flag2 = false;
					}
				}
				finally
				{
					message.Close();
					if (flag2)
					{
						requestContext.Abort();
					}
				}
			}

			// Token: 0x06007231 RID: 29233 RVA: 0x001AA64C File Offset: 0x001A884C
			protected override void OnCloseResponseMessageReceived(RequestContext requestContext, Message message, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				bool flag = true;
				try
				{
					bool flag2 = false;
					bool flag3 = false;
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						flag2 = this.sentClose;
						if (flag2 && !this.isInputClosed)
						{
							this.isInputClosed = true;
							flag3 = true;
						}
					}
					if (!flag2)
					{
						base.Fault(new ProtocolException(SR.GetString("UnexpectedSecuritySessionCloseResponse")));
					}
					else
					{
						if (flag3)
						{
							this.inputSessionCloseHandle.Set();
						}
						requestContext.Close(timeout);
						flag = false;
					}
				}
				finally
				{
					message.Close();
					if (flag)
					{
						requestContext.Abort();
					}
				}
			}

			// Token: 0x06007232 RID: 29234 RVA: 0x001AA6FC File Offset: 0x001A88FC
			private void DetermineCloseOutputSessionMessage(out bool sendClose, out bool sendCloseResponse, out Message pendingCloseResponseMessage, out RequestContext pendingCloseRequestContext)
			{
				sendClose = false;
				sendCloseResponse = false;
				pendingCloseResponseMessage = null;
				pendingCloseRequestContext = null;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (!this.isOutputClosed)
					{
						this.isOutputClosed = true;
						if (this.receivedClose)
						{
							if (this.closeResponseMessage != null)
							{
								pendingCloseResponseMessage = this.closeResponseMessage;
								pendingCloseRequestContext = this.closeRequestContext;
								this.closeResponseMessage = null;
								this.closeRequestContext = null;
								sendCloseResponse = true;
							}
						}
						else
						{
							sendClose = true;
							this.sentClose = true;
						}
						this.outputSessionCloseHandle.Reset();
					}
				}
			}

			// Token: 0x06007233 RID: 29235 RVA: 0x001AA79C File Offset: 0x001A899C
			private void CloseOutputSession(TimeSpan timeout)
			{
				bool flag = false;
				bool flag2 = false;
				try
				{
					Message message;
					RequestContext requestContext;
					this.DetermineCloseOutputSessionMessage(out flag, out flag2, out message, out requestContext);
					if (flag2)
					{
						bool flag3 = true;
						try
						{
							base.SendCloseResponse(requestContext, message, timeout);
							flag3 = false;
							goto IL_41;
						}
						finally
						{
							if (flag3)
							{
								message.Close();
								requestContext.Abort();
							}
						}
					}
					if (flag)
					{
						base.SendClose(timeout);
					}
					IL_41:;
				}
				catch (CommunicationObjectAbortedException)
				{
					if (base.State != CommunicationState.Closed)
					{
						throw;
					}
				}
				finally
				{
					if (flag || flag2)
					{
						this.outputSessionCloseHandle.Set();
					}
				}
			}

			// Token: 0x06007234 RID: 29236 RVA: 0x001AA834 File Offset: 0x001A8A34
			private IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06007235 RID: 29237 RVA: 0x001AA83F File Offset: 0x001A8A3F
			private void EndCloseOutputSession(IAsyncResult result)
			{
				SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult.End(result);
			}

			// Token: 0x06007236 RID: 29238 RVA: 0x001AA847 File Offset: 0x001A8A47
			public bool WaitForMessage(TimeSpan timeout)
			{
				return base.ChannelBinder.WaitForRequest(timeout);
			}

			// Token: 0x06007237 RID: 29239 RVA: 0x001AA855 File Offset: 0x001A8A55
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.ChannelBinder.BeginWaitForRequest(timeout, callback, state);
			}

			// Token: 0x06007238 RID: 29240 RVA: 0x001AA865 File Offset: 0x001A8A65
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return base.ChannelBinder.EndWaitForRequest(result);
			}

			// Token: 0x0400409F RID: 16543
			private SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.SoapSecurityServerDuplexSession session;

			// Token: 0x040040A0 RID: 16544
			private bool isInputClosed;

			// Token: 0x040040A1 RID: 16545
			private bool isOutputClosed;

			// Token: 0x040040A2 RID: 16546
			private bool sentClose;

			// Token: 0x040040A3 RID: 16547
			private bool receivedClose;

			// Token: 0x040040A4 RID: 16548
			private RequestContext closeRequestContext;

			// Token: 0x040040A5 RID: 16549
			private Message closeResponseMessage;

			// Token: 0x040040A6 RID: 16550
			private InterruptibleWaitObject outputSessionCloseHandle = new InterruptibleWaitObject(true);

			// Token: 0x040040A7 RID: 16551
			private InterruptibleWaitObject inputSessionCloseHandle = new InterruptibleWaitObject(false);

			// Token: 0x02000EF1 RID: 3825
			private class CloseOutputSessionAsyncResult : AsyncResult
			{
				// Token: 0x0600852D RID: 34093 RVA: 0x001ECC60 File Offset: 0x001EAE60
				public CloseOutputSessionAsyncResult(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.sessionChannel = sessionChannel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.sessionChannel.DetermineCloseOutputSessionMessage(out this.sendClose, out this.sendCloseResponse, out this.closeResponseMessage, out this.closeRequestContext);
					if (!this.sendClose && !this.sendCloseResponse)
					{
						base.Complete(true);
						return;
					}
					bool flag = true;
					try
					{
						IAsyncResult asyncResult = this.BeginSend(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult.sendCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							flag = false;
							return;
						}
						this.EndSend(asyncResult);
					}
					finally
					{
						if (flag)
						{
							this.Cleanup();
						}
					}
					base.Complete(true);
				}

				// Token: 0x0600852E RID: 34094 RVA: 0x001ECD0C File Offset: 0x001EAF0C
				private static void SendCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult closeOutputSessionAsyncResult = (SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						closeOutputSessionAsyncResult.EndSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					closeOutputSessionAsyncResult.Cleanup();
					closeOutputSessionAsyncResult.Complete(false, exception);
				}

				// Token: 0x0600852F RID: 34095 RVA: 0x001ECD68 File Offset: 0x001EAF68
				private IAsyncResult BeginSend(AsyncCallback callback, object state)
				{
					if (this.sendClose)
					{
						return this.sessionChannel.BeginSendClose(this.timeoutHelper.RemainingTime(), callback, state);
					}
					return this.sessionChannel.BeginSendCloseResponse(this.closeRequestContext, this.closeResponseMessage, this.timeoutHelper.RemainingTime(), callback, state);
				}

				// Token: 0x06008530 RID: 34096 RVA: 0x001ECDBA File Offset: 0x001EAFBA
				private void EndSend(IAsyncResult result)
				{
					if (this.sendClose)
					{
						this.sessionChannel.EndSendClose(result);
						return;
					}
					this.sessionChannel.EndSendCloseResponse(result);
				}

				// Token: 0x06008531 RID: 34097 RVA: 0x001ECDDD File Offset: 0x001EAFDD
				private void Cleanup()
				{
					if (this.closeResponseMessage != null)
					{
						this.closeResponseMessage.Close();
					}
					if (this.closeRequestContext != null)
					{
						this.closeRequestContext.Abort();
					}
					this.sessionChannel.outputSessionCloseHandle.Set();
				}

				// Token: 0x06008532 RID: 34098 RVA: 0x001ECE15 File Offset: 0x001EB015
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult>(result);
				}

				// Token: 0x04004D24 RID: 19748
				private static AsyncCallback sendCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseOutputSessionAsyncResult.SendCallback));

				// Token: 0x04004D25 RID: 19749
				private SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel sessionChannel;

				// Token: 0x04004D26 RID: 19750
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004D27 RID: 19751
				private bool sendClose;

				// Token: 0x04004D28 RID: 19752
				private bool sendCloseResponse;

				// Token: 0x04004D29 RID: 19753
				private Message closeResponseMessage;

				// Token: 0x04004D2A RID: 19754
				private RequestContext closeRequestContext;
			}

			// Token: 0x02000EF2 RID: 3826
			private class CloseAsyncResult : AsyncResult
			{
				// Token: 0x06008534 RID: 34100 RVA: 0x001ECE38 File Offset: 0x001EB038
				public CloseAsyncResult(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.sessionChannel = sessionChannel;
					bool flag = false;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.BeginCloseOutputSession(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.closeOutputSessionCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						this.sessionChannel.EndCloseOutputSession(asyncResult);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						flag = true;
					}
					if (flag || this.OnOutputSessionClosed())
					{
						base.Complete(true);
					}
				}

				// Token: 0x06008535 RID: 34101 RVA: 0x001ECECC File Offset: 0x001EB0CC
				private static void CloseOutputSessionCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						bool flag2 = false;
						try
						{
							closeAsyncResult.sessionChannel.Session.EndCloseOutputSession(result);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
							flag2 = true;
						}
						if (!flag2)
						{
							flag = closeAsyncResult.OnOutputSessionClosed();
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x06008536 RID: 34102 RVA: 0x001ECF68 File Offset: 0x001EB168
				private bool OnOutputSessionClosed()
				{
					bool flag = false;
					Message message = null;
					bool flag2 = false;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.BeginTryReceive(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.receiveCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						flag2 = this.sessionChannel.EndTryReceive(asyncResult, out message);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						flag = true;
					}
					if (flag)
					{
						return true;
					}
					if (flag2)
					{
						return this.OnMessageReceived(message);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
					{
						this.timeoutHelper.OriginalTimeout
					})));
				}

				// Token: 0x06008537 RID: 34103 RVA: 0x001ED020 File Offset: 0x001EB220
				private static void ReceiveCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						Message message = null;
						bool flag2 = false;
						bool flag3 = false;
						try
						{
							flag2 = closeAsyncResult.sessionChannel.EndTryReceive(result, out message);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
							flag3 = true;
						}
						if (!flag3)
						{
							if (!flag2)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
								{
									closeAsyncResult.timeoutHelper.OriginalTimeout
								})));
							}
							flag = closeAsyncResult.OnMessageReceived(message);
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x06008538 RID: 34104 RVA: 0x001ED0FC File Offset: 0x001EB2FC
				private bool OnMessageReceived(Message message)
				{
					if (message != null)
					{
						try
						{
							ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
							throw TraceUtility.ThrowHelperWarning(exception, message);
						}
						finally
						{
							if (message != null)
							{
								((IDisposable)message).Dispose();
							}
						}
					}
					bool flag = false;
					bool inputSessionClosed = false;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.inputSessionCloseHandle.BeginWait(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.inputSessionWaitCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						try
						{
							this.sessionChannel.inputSessionCloseHandle.EndWait(asyncResult);
							inputSessionClosed = true;
						}
						catch (TimeoutException)
						{
							inputSessionClosed = false;
						}
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						flag = true;
					}
					return flag || this.OnInputSessionWaitOver(inputSessionClosed);
				}

				// Token: 0x06008539 RID: 34105 RVA: 0x001ED1C8 File Offset: 0x001EB3C8
				private static void WaitForInputSessionCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					bool inputSessionClosed = false;
					try
					{
						bool flag2 = false;
						try
						{
							closeAsyncResult.sessionChannel.inputSessionCloseHandle.EndWait(result);
							inputSessionClosed = true;
						}
						catch (TimeoutException)
						{
							inputSessionClosed = false;
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag2 = true;
							flag = true;
						}
						if (!flag2)
						{
							flag = closeAsyncResult.OnInputSessionWaitOver(inputSessionClosed);
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x0600853A RID: 34106 RVA: 0x001ED27C File Offset: 0x001EB47C
				private bool OnInputSessionWaitOver(bool inputSessionClosed)
				{
					if (inputSessionClosed)
					{
						object thisLock = this.sessionChannel.ThisLock;
						lock (thisLock)
						{
							if (!this.sessionChannel.isInputClosed)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ShutdownRequestWasNotReceived")));
							}
						}
						bool outputSessionClosed = false;
						bool flag2 = false;
						try
						{
							IAsyncResult asyncResult = this.sessionChannel.outputSessionCloseHandle.BeginWait(this.timeoutHelper.RemainingTime(), true, SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.outputSessionWaitCallback, this);
							if (!asyncResult.CompletedSynchronously)
							{
								return false;
							}
							this.sessionChannel.outputSessionCloseHandle.EndWait(asyncResult);
							outputSessionClosed = true;
						}
						catch (CommunicationObjectAbortedException)
						{
							if (this.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag2 = true;
						}
						catch (TimeoutException)
						{
							outputSessionClosed = false;
						}
						return flag2 || this.OnOutputSessionWaitOver(outputSessionClosed);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ServiceSecurityCloseTimeout", new object[]
					{
						this.timeoutHelper.OriginalTimeout
					})));
				}

				// Token: 0x0600853B RID: 34107 RVA: 0x001ED3A8 File Offset: 0x001EB5A8
				private static void WaitForOutputSessionCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					bool outputSessionClosed = false;
					try
					{
						bool flag2 = false;
						try
						{
							closeAsyncResult.sessionChannel.outputSessionCloseHandle.EndWait(result);
							outputSessionClosed = true;
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag2 = true;
							flag = true;
						}
						catch (TimeoutException)
						{
							outputSessionClosed = false;
						}
						if (!flag2)
						{
							flag = closeAsyncResult.OnOutputSessionWaitOver(outputSessionClosed);
						}
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
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x0600853C RID: 34108 RVA: 0x001ED45C File Offset: 0x001EB65C
				private bool OnOutputSessionWaitOver(bool outputSessionClosed)
				{
					if (!outputSessionClosed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ServiceSecurityCloseOutputSessionTimeout", new object[]
						{
							this.timeoutHelper.OriginalTimeout
						})));
					}
					IAsyncResult asyncResult = this.sessionChannel.BeginCloseCore(this.timeoutHelper.RemainingTime(), SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.closeCoreCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.sessionChannel.EndCloseCore(asyncResult);
					return true;
				}

				// Token: 0x0600853D RID: 34109 RVA: 0x001ED4D4 File Offset: 0x001EB6D4
				private static void CloseCoreCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						closeAsyncResult.sessionChannel.EndCloseCore(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					closeAsyncResult.Complete(false, exception);
				}

				// Token: 0x0600853E RID: 34110 RVA: 0x001ED530 File Offset: 0x001EB730
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult>(result);
				}

				// Token: 0x04004D2B RID: 19755
				private static readonly AsyncCallback receiveCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.ReceiveCallback));

				// Token: 0x04004D2C RID: 19756
				private static readonly AsyncCallback inputSessionWaitCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.WaitForInputSessionCloseCallback));

				// Token: 0x04004D2D RID: 19757
				private static readonly AsyncCallback closeOutputSessionCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.CloseOutputSessionCallback));

				// Token: 0x04004D2E RID: 19758
				private static readonly AsyncCallback outputSessionWaitCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.WaitForOutputSessionCloseCallback));

				// Token: 0x04004D2F RID: 19759
				private static readonly AsyncCallback closeCoreCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel.CloseAsyncResult.CloseCoreCallback));

				// Token: 0x04004D30 RID: 19760
				private SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel sessionChannel;

				// Token: 0x04004D31 RID: 19761
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EF3 RID: 3827
			private class SoapSecurityServerDuplexSession : SecuritySessionServerSettings.ServerSecuritySessionChannel.SoapSecurityInputSession, IDuplexSession, IInputSession, ISession, IOutputSession
			{
				// Token: 0x06008540 RID: 34112 RVA: 0x001ED5B7 File Offset: 0x001EB7B7
				public SoapSecurityServerDuplexSession(SecurityContextSecurityToken sessionToken, SecuritySessionServerSettings settings, SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel channel) : base(sessionToken, settings, channel)
				{
					this.channel = channel;
				}

				// Token: 0x06008541 RID: 34113 RVA: 0x001ED5C9 File Offset: 0x001EB7C9
				public void CloseOutputSession()
				{
					this.CloseOutputSession(this.channel.DefaultCloseTimeout);
				}

				// Token: 0x06008542 RID: 34114 RVA: 0x001ED5DC File Offset: 0x001EB7DC
				public void CloseOutputSession(TimeSpan timeout)
				{
					this.channel.ThrowIfFaulted();
					this.channel.ThrowIfNotOpened();
					Exception ex = null;
					try
					{
						this.channel.CloseOutputSession(timeout);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex == null)
					{
						return;
					}
					this.channel.Fault(ex);
					if (ex is CommunicationException)
					{
						throw ex;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(ex);
				}

				// Token: 0x06008543 RID: 34115 RVA: 0x001ED654 File Offset: 0x001EB854
				public IAsyncResult BeginCloseOutputSession(AsyncCallback callback, object state)
				{
					return this.BeginCloseOutputSession(this.channel.DefaultCloseTimeout, callback, state);
				}

				// Token: 0x06008544 RID: 34116 RVA: 0x001ED66C File Offset: 0x001EB86C
				public IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
				{
					this.channel.ThrowIfFaulted();
					this.channel.ThrowIfNotOpened();
					Exception ex = null;
					try
					{
						return this.channel.BeginCloseOutputSession(timeout, callback, state);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex == null)
					{
						return null;
					}
					this.channel.Fault(ex);
					if (ex is CommunicationException)
					{
						throw ex;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(ex);
				}

				// Token: 0x06008545 RID: 34117 RVA: 0x001ED6EC File Offset: 0x001EB8EC
				public void EndCloseOutputSession(IAsyncResult result)
				{
					Exception ex = null;
					try
					{
						this.channel.EndCloseOutputSession(result);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex == null)
					{
						return;
					}
					this.channel.Fault(ex);
					if (ex is CommunicationException)
					{
						throw ex;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(ex);
				}

				// Token: 0x04004D32 RID: 19762
				private SecuritySessionServerSettings.ServerSecurityDuplexSessionChannel channel;
			}
		}

		// Token: 0x02000B62 RID: 2914
		internal class SecuritySessionDemuxFailureHandler : IChannelDemuxFailureHandler
		{
			// Token: 0x06007239 RID: 29241 RVA: 0x001AA873 File Offset: 0x001A8A73
			public SecuritySessionDemuxFailureHandler(SecurityStandardsManager standardsManager)
			{
				if (standardsManager == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("standardsManager");
				}
				this.standardsManager = standardsManager;
			}

			// Token: 0x0600723A RID: 29242 RVA: 0x001AA895 File Offset: 0x001A8A95
			public void HandleDemuxFailure(Message message)
			{
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 458834, SR.GetString("TraceCodeSecuritySessionDemuxFailure"), message);
				}
			}

			// Token: 0x0600723B RID: 29243 RVA: 0x001AA8C8 File Offset: 0x001A8AC8
			public Message CreateSessionDemuxFaultMessage(Message message)
			{
				MessageFault fault = SecurityUtils.CreateSecurityContextNotFoundFault(this.standardsManager, message.Headers.Action);
				Message message2 = Message.CreateMessage(message.Version, fault, message.Version.Addressing.DefaultFaultAction);
				if (message.Headers.MessageId != null)
				{
					message2.InitializeReply(message);
				}
				return message2;
			}

			// Token: 0x0600723C RID: 29244 RVA: 0x001AA924 File Offset: 0x001A8B24
			private IAsyncResult BeginHandleDemuxFailure<TFaultContext>(Message message, TFaultContext faultContext, AsyncCallback callback, object state)
			{
				this.HandleDemuxFailure(message);
				Message fault = this.CreateSessionDemuxFaultMessage(message);
				return new SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext>(fault, faultContext, callback, state);
			}

			// Token: 0x0600723D RID: 29245 RVA: 0x001AA94A File Offset: 0x001A8B4A
			public IAsyncResult BeginHandleDemuxFailure(Message message, RequestContext faultContext, AsyncCallback callback, object state)
			{
				return this.BeginHandleDemuxFailure<RequestContext>(message, faultContext, callback, state);
			}

			// Token: 0x0600723E RID: 29246 RVA: 0x001AA957 File Offset: 0x001A8B57
			public IAsyncResult BeginHandleDemuxFailure(Message message, IOutputChannel faultContext, AsyncCallback callback, object state)
			{
				return this.BeginHandleDemuxFailure<IOutputChannel>(message, faultContext, callback, state);
			}

			// Token: 0x0600723F RID: 29247 RVA: 0x001AA964 File Offset: 0x001A8B64
			public void EndHandleDemuxFailure(IAsyncResult result)
			{
				if (result is SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<RequestContext>)
				{
					SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<RequestContext>.End(result);
					return;
				}
				if (result is SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<IOutputChannel>)
				{
					SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<IOutputChannel>.End(result);
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidAsyncResult"), "result"));
			}

			// Token: 0x040040A8 RID: 16552
			private SecurityStandardsManager standardsManager;

			// Token: 0x02000EF4 RID: 3828
			private class SendFaultAsyncResult<TFaultContext> : AsyncResult
			{
				// Token: 0x06008546 RID: 34118 RVA: 0x001ED750 File Offset: 0x001EB950
				public SendFaultAsyncResult(Message fault, TFaultContext faultContext, AsyncCallback callback, object state) : base(callback, state)
				{
					this.faultContext = faultContext;
					this.message = fault;
					IAsyncResult asyncResult = this.BeginSend(fault);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.EndSend(asyncResult);
					base.Complete(true);
				}

				// Token: 0x06008547 RID: 34119 RVA: 0x001ED794 File Offset: 0x001EB994
				private IAsyncResult BeginSend(Message message)
				{
					bool flag = true;
					IAsyncResult result;
					try
					{
						IAsyncResult asyncResult;
						if (this.faultContext is RequestContext)
						{
							asyncResult = ((RequestContext)((object)this.faultContext)).BeginReply(message, SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext>.sendCallback, this);
						}
						else
						{
							asyncResult = ((IOutputChannel)((object)this.faultContext)).BeginSend(message, SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext>.sendCallback, this);
						}
						flag = false;
						result = asyncResult;
					}
					finally
					{
						if (flag && message != null)
						{
							message.Close();
						}
					}
					return result;
				}

				// Token: 0x06008548 RID: 34120 RVA: 0x001ED818 File Offset: 0x001EBA18
				private void EndSend(IAsyncResult result)
				{
					using (this.message)
					{
						if (this.faultContext is RequestContext)
						{
							((RequestContext)((object)this.faultContext)).EndReply(result);
						}
						else
						{
							((IOutputChannel)((object)this.faultContext)).EndSend(result);
						}
					}
				}

				// Token: 0x06008549 RID: 34121 RVA: 0x001ED888 File Offset: 0x001EBA88
				private static void SendCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext> sendFaultAsyncResult = (SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext>)result.AsyncState;
					Exception exception = null;
					try
					{
						sendFaultAsyncResult.EndSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					sendFaultAsyncResult.Complete(false, exception);
				}

				// Token: 0x0600854A RID: 34122 RVA: 0x001ED8DC File Offset: 0x001EBADC
				internal static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext>>(result);
				}

				// Token: 0x04004D33 RID: 19763
				private Message message;

				// Token: 0x04004D34 RID: 19764
				private static AsyncCallback sendCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionServerSettings.SecuritySessionDemuxFailureHandler.SendFaultAsyncResult<TFaultContext>.SendCallback));

				// Token: 0x04004D35 RID: 19765
				private TFaultContext faultContext;
			}
		}
	}
}
