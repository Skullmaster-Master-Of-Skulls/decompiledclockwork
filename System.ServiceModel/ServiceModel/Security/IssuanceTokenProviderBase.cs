using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200032D RID: 813
	internal abstract class IssuanceTokenProviderBase<T> : CommunicationObjectSecurityTokenProvider where T : IssuanceTokenProviderState
	{
		// Token: 0x06001CE4 RID: 7396 RVA: 0x0006BDB0 File Offset: 0x00069FB0
		protected IssuanceTokenProviderBase()
		{
			this.cacheServiceTokens = true;
			this.serviceTokenValidityThresholdPercentage = 60;
			this.maxServiceTokenCachingTime = IssuanceTokenProviderBase<T>.DefaultClientMaxTokenCachingTime;
			this.standardsManager = null;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0006BDFE File Offset: 0x00069FFE
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x0006BE06 File Offset: 0x0006A006
		public EndpointAddress IssuerAddress
		{
			get
			{
				return this.issuerAddress;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.issuerAddress = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0006BE1A File Offset: 0x0006A01A
		// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x0006BE22 File Offset: 0x0006A022
		public EndpointAddress TargetAddress
		{
			get
			{
				return this.targetAddress;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.targetAddress = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0006BE36 File Offset: 0x0006A036
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x0006BE3E File Offset: 0x0006A03E
		public bool CacheServiceTokens
		{
			get
			{
				return this.cacheServiceTokens;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.cacheServiceTokens = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0006BE52 File Offset: 0x0006A052
		internal static TimeSpan DefaultClientMaxTokenCachingTime
		{
			get
			{
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0006BE59 File Offset: 0x0006A059
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x0006BE64 File Offset: 0x0006A064
		public int ServiceTokenValidityThresholdPercentage
		{
			get
			{
				return this.serviceTokenValidityThresholdPercentage;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value <= 0 || value > 100)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ValueMustBeInRange", new object[]
					{
						1,
						100
					})));
				}
				this.serviceTokenValidityThresholdPercentage = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0006BEC4 File Offset: 0x0006A0C4
		// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0006BECC File Offset: 0x0006A0CC
		public SecurityAlgorithmSuite SecurityAlgorithmSuite
		{
			get
			{
				return this.algorithmSuite;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.algorithmSuite = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0006BEE0 File Offset: 0x0006A0E0
		// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0006BEE8 File Offset: 0x0006A0E8
		public TimeSpan MaxServiceTokenCachingTime
		{
			get
			{
				return this.maxServiceTokenCachingTime;
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
				this.maxServiceTokenCachingTime = value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0006BF60 File Offset: 0x0006A160
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0006BF76 File Offset: 0x0006A176
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				if (this.standardsManager == null)
				{
					return SecurityStandardsManager.DefaultInstance;
				}
				return this.standardsManager;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.standardsManager = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x0006BF8A File Offset: 0x0006A18A
		// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x0006BF92 File Offset: 0x0006A192
		public ChannelProtectionRequirements ApplicationProtectionRequirements
		{
			get
			{
				return this.applicationProtectionRequirements;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.applicationProtectionRequirements = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x0006BFA6 File Offset: 0x0006A1A6
		// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x0006BFAE File Offset: 0x0006A1AE
		public Uri Via
		{
			get
			{
				return this.via;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.via = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x0006BFC2 File Offset: 0x0006A1C2
		public override bool SupportsTokenCancellation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0006BFC5 File Offset: 0x0006A1C5
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x0006BFCD File Offset: 0x0006A1CD
		protected virtual bool IsMultiLegNegotiation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001CFB RID: 7419
		protected abstract MessageVersion MessageVersion { get; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001CFC RID: 7420
		protected abstract bool RequiresManualReplyAddressing { get; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001CFD RID: 7421
		public abstract XmlDictionaryString RequestSecurityTokenAction { get; }

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001CFE RID: 7422
		public abstract XmlDictionaryString RequestSecurityTokenResponseAction { get; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0006BFD0 File Offset: 0x0006A1D0
		protected string SecurityContextTokenUri
		{
			get
			{
				this.ThrowIfCreated();
				return this.sctUri;
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0006BFE0 File Offset: 0x0006A1E0
		protected void ThrowIfCreated()
		{
			CommunicationState state = base.CommunicationObject.State;
			if (state == CommunicationState.Created)
			{
				Exception exception = new InvalidOperationException(SR.GetString("CommunicationObjectCannotBeUsed", new object[]
				{
					base.GetType().ToString(),
					state.ToString()
				}));
				throw TraceUtility.ThrowHelperError(exception, Guid.Empty, this);
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0006C03D File Offset: 0x0006A23D
		protected void ThrowIfClosedOrCreated()
		{
			base.CommunicationObject.ThrowIfClosed();
			this.ThrowIfCreated();
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0006C050 File Offset: 0x0006A250
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.targetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TargetAddressIsNotSet", new object[]
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
			this.sctUri = this.StandardsManager.SecureConversationDriver.TokenTypeUri;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0006C0DC File Offset: 0x0006A2DC
		protected void EnsureEndpointAddressDoesNotRequireEncryption(EndpointAddress target)
		{
			if (this.ApplicationProtectionRequirements == null || this.ApplicationProtectionRequirements.OutgoingEncryptionParts == null)
			{
				return;
			}
			MessagePartSpecification channelParts = this.ApplicationProtectionRequirements.OutgoingEncryptionParts.ChannelParts;
			if (channelParts == null)
			{
				return;
			}
			for (int i = 0; i < this.targetAddress.Headers.Count; i++)
			{
				AddressHeader addressHeader = target.Headers[i];
				if (channelParts.IsHeaderIncluded(addressHeader.Name, addressHeader.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("SecurityNegotiationCannotProtectConfidentialEndpointHeader", new object[]
					{
						target,
						addressHeader.Name,
						addressHeader.Namespace
					})));
				}
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0006C188 File Offset: 0x0006A388
		private DateTime GetServiceTokenEffectiveExpirationTime(SecurityToken serviceToken)
		{
			if (serviceToken.ValidTo.ToUniversalTime() >= SecurityUtils.MaxUtcDateTime)
			{
				return serviceToken.ValidTo;
			}
			long ticks = (serviceToken.ValidTo.ToUniversalTime() - serviceToken.ValidFrom.ToUniversalTime()).Ticks;
			long ticks2 = Convert.ToInt64((double)this.ServiceTokenValidityThresholdPercentage / 100.0 * (double)ticks, NumberFormatInfo.InvariantInfo);
			DateTime dateTime = TimeoutHelper.Add(serviceToken.ValidFrom.ToUniversalTime(), new TimeSpan(ticks2));
			DateTime dateTime2 = TimeoutHelper.Add(serviceToken.ValidFrom.ToUniversalTime(), this.MaxServiceTokenCachingTime);
			if (dateTime <= dateTime2)
			{
				return dateTime;
			}
			return dateTime2;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0006C24C File Offset: 0x0006A44C
		private bool IsServiceTokenTimeValid(SecurityToken serviceToken)
		{
			DateTime serviceTokenEffectiveExpirationTime = this.GetServiceTokenEffectiveExpirationTime(serviceToken);
			return DateTime.UtcNow <= serviceTokenEffectiveExpirationTime;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0006C26C File Offset: 0x0006A46C
		private SecurityToken GetCurrentServiceToken()
		{
			if (this.CacheServiceTokens && this.cachedToken != null && this.IsServiceTokenTimeValid(this.cachedToken))
			{
				return this.cachedToken;
			}
			return null;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0006C294 File Offset: 0x0006A494
		protected static void ThrowIfFault(Message message, EndpointAddress target)
		{
			SecurityUtils.ThrowIfNegotiationFault(message, target);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0006C2A0 File Offset: 0x0006A4A0
		protected override IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			object obj = this.ThisLock;
			IAsyncResult result;
			lock (obj)
			{
				SecurityToken currentServiceToken = this.GetCurrentServiceToken();
				if (currentServiceToken != null)
				{
					SecurityTraceRecordHelper.TraceUsingCachedServiceToken<T>(this, currentServiceToken, this.targetAddress);
					result = new CompletedAsyncResult<SecurityToken>(currentServiceToken, callback, state);
				}
				else
				{
					result = this.BeginNegotiation(timeout, callback, state);
				}
			}
			return result;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0006C314 File Offset: 0x0006A514
		protected override SecurityToken EndGetTokenCore(IAsyncResult result)
		{
			if (result is CompletedAsyncResult<SecurityToken>)
			{
				return CompletedAsyncResult<SecurityToken>.End(result);
			}
			return this.EndNegotiation(result);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0006C32C File Offset: 0x0006A52C
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			object obj = this.ThisLock;
			SecurityToken securityToken;
			lock (obj)
			{
				securityToken = this.GetCurrentServiceToken();
				if (securityToken != null)
				{
					SecurityTraceRecordHelper.TraceUsingCachedServiceToken<T>(this, securityToken, this.targetAddress);
				}
			}
			if (securityToken == null)
			{
				securityToken = this.DoNegotiation(timeout);
			}
			return securityToken;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0006C394 File Offset: 0x0006A594
		protected override void CancelTokenCore(TimeSpan timeout, SecurityToken token)
		{
			if (this.CacheServiceTokens)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (token == this.cachedToken)
					{
						this.cachedToken = null;
					}
				}
			}
		}

		// Token: 0x06001D0C RID: 7436
		protected abstract bool CreateNegotiationStateCompletesSynchronously(EndpointAddress target, Uri via);

		// Token: 0x06001D0D RID: 7437
		protected abstract IAsyncResult BeginCreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06001D0E RID: 7438
		protected abstract T CreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout);

		// Token: 0x06001D0F RID: 7439
		protected abstract T EndCreateNegotiationState(IAsyncResult result);

		// Token: 0x06001D10 RID: 7440
		protected abstract BodyWriter GetFirstOutgoingMessageBody(T negotiationState, out MessageProperties properties);

		// Token: 0x06001D11 RID: 7441
		protected abstract BodyWriter GetNextOutgoingMessageBody(Message incomingMessage, T negotiationState);

		// Token: 0x06001D12 RID: 7442
		protected abstract bool WillInitializeChannelFactoriesCompleteSynchronously(EndpointAddress target);

		// Token: 0x06001D13 RID: 7443
		protected abstract void InitializeChannelFactories(EndpointAddress target, TimeSpan timeout);

		// Token: 0x06001D14 RID: 7444
		protected abstract IAsyncResult BeginInitializeChannelFactories(EndpointAddress target, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06001D15 RID: 7445
		protected abstract void EndInitializeChannelFactories(IAsyncResult result);

		// Token: 0x06001D16 RID: 7446
		protected abstract IRequestChannel CreateClientChannel(EndpointAddress target, Uri via);

		// Token: 0x06001D17 RID: 7447 RVA: 0x0006C3E8 File Offset: 0x0006A5E8
		private void PrepareRequest(Message nextMessage)
		{
			this.PrepareRequest(nextMessage, null);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0006C3F2 File Offset: 0x0006A5F2
		private void PrepareRequest(Message nextMessage, RequestSecurityToken rst)
		{
			if (rst != null && !rst.IsReadOnly)
			{
				rst.Message = nextMessage;
			}
			RequestReplyCorrelator.PrepareRequest(nextMessage);
			if (this.RequiresManualReplyAddressing)
			{
				nextMessage.Headers.ReplyTo = EndpointAddress.AnonymousAddress;
			}
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0006C424 File Offset: 0x0006A624
		protected SecurityToken DoNegotiation(TimeSpan timeout)
		{
			this.ThrowIfClosedOrCreated();
			SecurityTraceRecordHelper.TraceBeginSecurityNegotiation<T>(this, this.targetAddress);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IRequestChannel requestChannel = null;
			T t = default(T);
			TimeSpan timeSpan = timeout;
			int num = 1;
			SecurityToken result;
			try
			{
				t = this.CreateNegotiationState(this.targetAddress, this.via, timeoutHelper.RemainingTime());
				this.InitializeNegotiationState(t);
				this.InitializeChannelFactories(t.RemoteAddress, timeoutHelper.RemainingTime());
				requestChannel = this.CreateClientChannel(t.RemoteAddress, this.via);
				requestChannel.Open(timeoutHelper.RemainingTime());
				Message message = null;
				for (;;)
				{
					Message nextOutgoingMessage = this.GetNextOutgoingMessage(message, t);
					if (message != null)
					{
						message.Close();
					}
					if (nextOutgoingMessage == null)
					{
						break;
					}
					using (nextOutgoingMessage)
					{
						EventTraceActivity eventTraceActivity = null;
						if (TD.MessageSentToTransportIsEnabled())
						{
							eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(nextOutgoingMessage);
						}
						TraceUtility.ProcessOutgoingMessage(nextOutgoingMessage, eventTraceActivity);
						timeSpan = timeoutHelper.RemainingTime();
						message = requestChannel.Request(nextOutgoingMessage, timeSpan);
						if (message == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("FailToRecieveReplyFromNegotiation")));
						}
						if (eventTraceActivity == null && TD.MessageReceivedFromTransportIsEnabled())
						{
							eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
						}
						TraceUtility.ProcessIncomingMessage(message, eventTraceActivity);
					}
					num += 2;
				}
				if (!t.IsNegotiationCompleted)
				{
					throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoNegotiationMessageToSend")), message);
				}
				try
				{
					requestChannel.Close(timeoutHelper.RemainingTime());
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					requestChannel.Abort();
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					requestChannel.Abort();
				}
				requestChannel = null;
				this.ValidateAndCacheServiceToken(t);
				SecurityToken serviceToken = t.ServiceToken;
				SecurityTraceRecordHelper.TraceEndSecurityNegotiation<T>(this, serviceToken, this.targetAddress);
				result = serviceToken;
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				if (ex2 is TimeoutException)
				{
					ex2 = new TimeoutException(SR.GetString("ClientSecurityNegotiationTimeout", new object[]
					{
						timeout,
						num,
						timeSpan
					}), ex2);
				}
				EndpointAddress endpointAddress = (t == null) ? null : t.RemoteAddress;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(IssuanceTokenProviderBase<T>.WrapExceptionIfRequired(ex2, endpointAddress, this.issuerAddress));
			}
			finally
			{
				this.Cleanup(requestChannel, t);
			}
			return result;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0006C704 File Offset: 0x0006A904
		private void InitializeNegotiationState(T negotiationState)
		{
			negotiationState.TargetAddress = this.targetAddress;
			if (negotiationState.Context == null && this.IsMultiLegNegotiation)
			{
				negotiationState.Context = SecurityUtils.GenerateId();
			}
			if (this.IssuerAddress != null)
			{
				negotiationState.RemoteAddress = this.IssuerAddress;
				return;
			}
			negotiationState.RemoteAddress = negotiationState.TargetAddress;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0006C780 File Offset: 0x0006A980
		private Message GetNextOutgoingMessage(Message incomingMessage, T negotiationState)
		{
			MessageProperties messageProperties = null;
			BodyWriter bodyWriter;
			if (incomingMessage == null)
			{
				bodyWriter = this.GetFirstOutgoingMessageBody(negotiationState, out messageProperties);
			}
			else
			{
				bodyWriter = this.GetNextOutgoingMessageBody(incomingMessage, negotiationState);
			}
			if (bodyWriter != null)
			{
				Message message;
				if (incomingMessage == null)
				{
					message = Message.CreateMessage(this.MessageVersion, ActionHeader.Create(this.RequestSecurityTokenAction, this.MessageVersion.Addressing), bodyWriter);
				}
				else
				{
					message = Message.CreateMessage(this.MessageVersion, ActionHeader.Create(this.RequestSecurityTokenResponseAction, this.MessageVersion.Addressing), bodyWriter);
				}
				if (messageProperties != null)
				{
					message.Properties.CopyProperties(messageProperties);
				}
				this.PrepareRequest(message, bodyWriter as RequestSecurityToken);
				return message;
			}
			return null;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0006C814 File Offset: 0x0006AA14
		private void Cleanup(IChannel rstChannel, T negotiationState)
		{
			if (negotiationState != null)
			{
				negotiationState.Dispose();
			}
			if (rstChannel != null)
			{
				rstChannel.Abort();
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0006C832 File Offset: 0x0006AA32
		protected IAsyncResult BeginNegotiation(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfClosedOrCreated();
			SecurityTraceRecordHelper.TraceBeginSecurityNegotiation<T>(this, this.targetAddress);
			return new IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0006C850 File Offset: 0x0006AA50
		protected SecurityToken EndNegotiation(IAsyncResult result)
		{
			SecurityToken securityToken = IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.End(result);
			SecurityTraceRecordHelper.TraceEndSecurityNegotiation<T>(this, securityToken, this.targetAddress);
			return securityToken;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x0006C874 File Offset: 0x0006AA74
		protected virtual void ValidateKeySize(GenericXmlSecurityToken issuedToken)
		{
			if (this.SecurityAlgorithmSuite == null)
			{
				return;
			}
			ReadOnlyCollection<SecurityKey> securityKeys = issuedToken.SecurityKeys;
			if (securityKeys == null || securityKeys.Count != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("CannotObtainIssuedTokenKeySize")));
			}
			SymmetricSecurityKey symmetricSecurityKey = securityKeys[0] as SymmetricSecurityKey;
			if (symmetricSecurityKey == null)
			{
				return;
			}
			if (this.SecurityAlgorithmSuite.IsSymmetricKeyLengthSupported(symmetricSecurityKey.KeySize))
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidIssuedTokenKeySize", new object[]
			{
				symmetricSecurityKey.KeySize
			})));
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x0006C90C File Offset: 0x0006AB0C
		private static bool ShouldWrapException(Exception e)
		{
			return e is Win32Exception || e is XmlException || e is InvalidOperationException || e is ArgumentException || e is QuotaExceededException || e is SecurityException || e is CryptographicException || e is SecurityTokenException;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x0006C95C File Offset: 0x0006AB5C
		private static Exception WrapExceptionIfRequired(Exception e, EndpointAddress targetAddress, EndpointAddress issuerAddress)
		{
			if (IssuanceTokenProviderBase<T>.ShouldWrapException(e))
			{
				Uri uri;
				if (targetAddress != null)
				{
					uri = targetAddress.Uri;
				}
				else
				{
					uri = null;
				}
				Uri uri2;
				if (issuerAddress != null)
				{
					uri2 = issuerAddress.Uri;
				}
				else
				{
					uri2 = uri;
				}
				if (uri != null)
				{
					e = new SecurityNegotiationException(SR.GetString("SoapSecurityNegotiationFailedForIssuerAndTarget", new object[]
					{
						uri2,
						uri
					}), e);
				}
				else
				{
					e = new SecurityNegotiationException(SR.GetString("SoapSecurityNegotiationFailed"), e);
				}
			}
			return e;
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
		private void ValidateAndCacheServiceToken(T negotiationState)
		{
			this.ValidateKeySize(negotiationState.ServiceToken);
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.CacheServiceTokens)
				{
					this.cachedToken = negotiationState.ServiceToken;
				}
			}
		}

		// Token: 0x04001DE9 RID: 7657
		internal const string defaultClientMaxTokenCachingTimeString = "10675199.02:48:05.4775807";

		// Token: 0x04001DEA RID: 7658
		internal const bool defaultClientCacheTokens = true;

		// Token: 0x04001DEB RID: 7659
		internal const int defaultServiceTokenValidityThresholdPercentage = 60;

		// Token: 0x04001DEC RID: 7660
		private EndpointAddress issuerAddress;

		// Token: 0x04001DED RID: 7661
		private EndpointAddress targetAddress;

		// Token: 0x04001DEE RID: 7662
		private Uri via;

		// Token: 0x04001DEF RID: 7663
		private bool cacheServiceTokens = true;

		// Token: 0x04001DF0 RID: 7664
		private int serviceTokenValidityThresholdPercentage = 60;

		// Token: 0x04001DF1 RID: 7665
		private TimeSpan maxServiceTokenCachingTime;

		// Token: 0x04001DF2 RID: 7666
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001DF3 RID: 7667
		private SecurityAlgorithmSuite algorithmSuite;

		// Token: 0x04001DF4 RID: 7668
		private ChannelProtectionRequirements applicationProtectionRequirements;

		// Token: 0x04001DF5 RID: 7669
		private SecurityToken cachedToken;

		// Token: 0x04001DF6 RID: 7670
		private object thisLock = new object();

		// Token: 0x04001DF7 RID: 7671
		private string sctUri;

		// Token: 0x02000B7A RID: 2938
		private class SecurityNegotiationAsyncResult : AsyncResult
		{
			// Token: 0x060072B2 RID: 29362 RVA: 0x001AC2CC File Offset: 0x001AA4CC
			public SecurityNegotiationAsyncResult(IssuanceTokenProviderBase<T> tokenProvider, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeout = timeout;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.tokenProvider = tokenProvider;
				this.target = tokenProvider.targetAddress;
				this.issuer = tokenProvider.issuerAddress;
				this.via = tokenProvider.via;
				bool flag = false;
				try
				{
					flag = this.StartNegotiation();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.OnSyncNegotiationFailure(ex));
				}
				if (flag)
				{
					this.OnNegotiationComplete();
					base.Complete(true);
				}
			}

			// Token: 0x060072B3 RID: 29363 RVA: 0x001AC368 File Offset: 0x001AA568
			private bool StartNegotiation()
			{
				if (this.tokenProvider.CreateNegotiationStateCompletesSynchronously(this.target, this.via))
				{
					this.negotiationState = this.tokenProvider.CreateNegotiationState(this.target, this.via, this.timeoutHelper.RemainingTime());
				}
				else
				{
					IAsyncResult asyncResult = this.tokenProvider.BeginCreateNegotiationState(this.target, this.via, this.timeoutHelper.RemainingTime(), IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.createNegotiationStateCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.negotiationState = this.tokenProvider.EndCreateNegotiationState(asyncResult);
				}
				return this.OnCreateStateComplete();
			}

			// Token: 0x060072B4 RID: 29364 RVA: 0x001AC404 File Offset: 0x001AA604
			private static void CreateNegotiationStateCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult securityNegotiationAsyncResult = (IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					securityNegotiationAsyncResult.negotiationState = securityNegotiationAsyncResult.tokenProvider.EndCreateNegotiationState(result);
					flag = securityNegotiationAsyncResult.OnCreateStateComplete();
					if (flag)
					{
						securityNegotiationAsyncResult.OnNegotiationComplete();
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = securityNegotiationAsyncResult.OnAsyncNegotiationFailure(ex);
				}
				if (flag)
				{
					securityNegotiationAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060072B5 RID: 29365 RVA: 0x001AC480 File Offset: 0x001AA680
			private bool OnCreateStateComplete()
			{
				this.tokenProvider.InitializeNegotiationState(this.negotiationState);
				return this.InitializeChannelFactories();
			}

			// Token: 0x060072B6 RID: 29366 RVA: 0x001AC49C File Offset: 0x001AA69C
			private bool InitializeChannelFactories()
			{
				if (this.tokenProvider.WillInitializeChannelFactoriesCompleteSynchronously(this.negotiationState.RemoteAddress))
				{
					this.tokenProvider.InitializeChannelFactories(this.negotiationState.RemoteAddress, this.timeoutHelper.RemainingTime());
				}
				else
				{
					IAsyncResult asyncResult = this.tokenProvider.BeginInitializeChannelFactories(this.negotiationState.RemoteAddress, this.timeoutHelper.RemainingTime(), IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.initializeChannelFactoriesCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.tokenProvider.EndInitializeChannelFactories(asyncResult);
				}
				return this.OnChannelFactoriesInitialized();
			}

			// Token: 0x060072B7 RID: 29367 RVA: 0x001AC538 File Offset: 0x001AA738
			private static void InitializeChannelFactoriesCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult securityNegotiationAsyncResult = (IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					securityNegotiationAsyncResult.tokenProvider.EndInitializeChannelFactories(result);
					flag = securityNegotiationAsyncResult.OnChannelFactoriesInitialized();
					if (flag)
					{
						securityNegotiationAsyncResult.OnNegotiationComplete();
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = securityNegotiationAsyncResult.OnAsyncNegotiationFailure(ex);
				}
				if (flag)
				{
					securityNegotiationAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060072B8 RID: 29368 RVA: 0x001AC5B0 File Offset: 0x001AA7B0
			private bool OnChannelFactoriesInitialized()
			{
				this.rstChannel = this.tokenProvider.CreateClientChannel(this.negotiationState.RemoteAddress, this.via);
				this.nextOutgoingMessage = null;
				return this.OnRequestChannelCreated();
			}

			// Token: 0x060072B9 RID: 29369 RVA: 0x001AC5E8 File Offset: 0x001AA7E8
			private bool OnRequestChannelCreated()
			{
				IAsyncResult asyncResult = this.rstChannel.BeginOpen(this.timeoutHelper.RemainingTime(), IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.openChannelCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.rstChannel.EndOpen(asyncResult);
				return this.OnRequestChannelOpened();
			}

			// Token: 0x060072BA RID: 29370 RVA: 0x001AC630 File Offset: 0x001AA830
			private static void OpenChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult securityNegotiationAsyncResult = (IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					securityNegotiationAsyncResult.rstChannel.EndOpen(result);
					flag = securityNegotiationAsyncResult.OnRequestChannelOpened();
					if (flag)
					{
						securityNegotiationAsyncResult.OnNegotiationComplete();
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = securityNegotiationAsyncResult.OnAsyncNegotiationFailure(ex);
				}
				if (flag)
				{
					securityNegotiationAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060072BB RID: 29371 RVA: 0x001AC6A8 File Offset: 0x001AA8A8
			private bool OnRequestChannelOpened()
			{
				return this.SendRequest();
			}

			// Token: 0x060072BC RID: 29372 RVA: 0x001AC6B0 File Offset: 0x001AA8B0
			private bool SendRequest()
			{
				if (this.nextOutgoingMessage == null)
				{
					return this.DoNegotiation(null);
				}
				this.tokenProvider.PrepareRequest(this.nextOutgoingMessage);
				bool flag = true;
				Message message = null;
				try
				{
					IAsyncResult asyncResult = this.rstChannel.BeginRequest(this.nextOutgoingMessage, this.timeoutHelper.RemainingTime(), IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.sendRequestCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						flag = false;
						return false;
					}
					message = this.rstChannel.EndRequest(asyncResult);
				}
				finally
				{
					if (flag && this.nextOutgoingMessage != null)
					{
						this.nextOutgoingMessage.Close();
					}
				}
				bool result;
				using (message)
				{
					if (message == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("FailToRecieveReplyFromNegotiation")));
					}
					result = this.DoNegotiation(message);
				}
				return result;
			}

			// Token: 0x060072BD RID: 29373 RVA: 0x001AC790 File Offset: 0x001AA990
			private static void SendRequestCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult securityNegotiationAsyncResult = (IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					Message message = null;
					try
					{
						message = securityNegotiationAsyncResult.rstChannel.EndRequest(result);
					}
					finally
					{
						if (securityNegotiationAsyncResult.nextOutgoingMessage != null)
						{
							securityNegotiationAsyncResult.nextOutgoingMessage.Close();
						}
					}
					using (message)
					{
						if (message == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("FailToRecieveReplyFromNegotiation")));
						}
						flag = securityNegotiationAsyncResult.DoNegotiation(message);
					}
					if (flag)
					{
						securityNegotiationAsyncResult.OnNegotiationComplete();
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = securityNegotiationAsyncResult.OnAsyncNegotiationFailure(ex);
				}
				if (flag)
				{
					securityNegotiationAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060072BE RID: 29374 RVA: 0x001AC86C File Offset: 0x001AAA6C
			private bool DoNegotiation(Message incomingMessage)
			{
				this.nextOutgoingMessage = this.tokenProvider.GetNextOutgoingMessage(incomingMessage, this.negotiationState);
				if (this.nextOutgoingMessage != null)
				{
					return this.SendRequest();
				}
				if (!this.negotiationState.IsNegotiationCompleted)
				{
					throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NoNegotiationMessageToSend")), incomingMessage);
				}
				return this.CloseRequestChannel();
			}

			// Token: 0x060072BF RID: 29375 RVA: 0x001AC8D0 File Offset: 0x001AAAD0
			private bool CloseRequestChannel()
			{
				IAsyncResult asyncResult = this.rstChannel.BeginClose(this.timeoutHelper.RemainingTime(), IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.closeChannelCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.rstChannel.EndClose(asyncResult);
				return true;
			}

			// Token: 0x060072C0 RID: 29376 RVA: 0x001AC914 File Offset: 0x001AAB14
			private static void CloseChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult securityNegotiationAsyncResult = (IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					securityNegotiationAsyncResult.rstChannel.EndClose(result);
					securityNegotiationAsyncResult.OnNegotiationComplete();
					flag = true;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = securityNegotiationAsyncResult.OnAsyncNegotiationFailure(ex);
				}
				if (flag)
				{
					securityNegotiationAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060072C1 RID: 29377 RVA: 0x001AC984 File Offset: 0x001AAB84
			private void Cleanup()
			{
				this.tokenProvider.Cleanup(this.rstChannel, this.negotiationState);
				this.rstChannel = null;
				this.negotiationState = default(T);
			}

			// Token: 0x060072C2 RID: 29378 RVA: 0x001AC9B0 File Offset: 0x001AABB0
			private Exception OnAsyncNegotiationFailure(Exception e)
			{
				EndpointAddress targetAddress = null;
				try
				{
					targetAddress = ((this.negotiationState == null) ? null : this.negotiationState.RemoteAddress);
					this.Cleanup();
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				return IssuanceTokenProviderBase<T>.WrapExceptionIfRequired(e, targetAddress, this.issuer);
			}

			// Token: 0x060072C3 RID: 29379 RVA: 0x001ACA10 File Offset: 0x001AAC10
			private Exception OnSyncNegotiationFailure(Exception e)
			{
				EndpointAddress targetAddress = (this.negotiationState == null) ? null : this.negotiationState.RemoteAddress;
				return IssuanceTokenProviderBase<T>.WrapExceptionIfRequired(e, targetAddress, this.issuer);
			}

			// Token: 0x060072C4 RID: 29380 RVA: 0x001ACA4C File Offset: 0x001AAC4C
			private void OnNegotiationComplete()
			{
				using (this.negotiationState)
				{
					SecurityToken securityToken = this.negotiationState.ServiceToken;
					this.tokenProvider.ValidateAndCacheServiceToken(this.negotiationState);
					this.serviceToken = securityToken;
				}
			}

			// Token: 0x060072C5 RID: 29381 RVA: 0x001ACAB0 File Offset: 0x001AACB0
			public static SecurityToken End(IAsyncResult result)
			{
				IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult securityNegotiationAsyncResult = AsyncResult.End<IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult>(result);
				return securityNegotiationAsyncResult.serviceToken;
			}

			// Token: 0x040040EC RID: 16620
			private static AsyncCallback createNegotiationStateCallback = Fx.ThunkCallback(new AsyncCallback(IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.CreateNegotiationStateCallback));

			// Token: 0x040040ED RID: 16621
			private static AsyncCallback initializeChannelFactoriesCallback = Fx.ThunkCallback(new AsyncCallback(IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.InitializeChannelFactoriesCallback));

			// Token: 0x040040EE RID: 16622
			private static AsyncCallback closeChannelCallback = Fx.ThunkCallback(new AsyncCallback(IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.CloseChannelCallback));

			// Token: 0x040040EF RID: 16623
			private static AsyncCallback sendRequestCallback = Fx.ThunkCallback(new AsyncCallback(IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.SendRequestCallback));

			// Token: 0x040040F0 RID: 16624
			private static AsyncCallback openChannelCallback = Fx.ThunkCallback(new AsyncCallback(IssuanceTokenProviderBase<T>.SecurityNegotiationAsyncResult.OpenChannelCallback));

			// Token: 0x040040F1 RID: 16625
			private TimeSpan timeout;

			// Token: 0x040040F2 RID: 16626
			private TimeoutHelper timeoutHelper;

			// Token: 0x040040F3 RID: 16627
			private SecurityToken serviceToken;

			// Token: 0x040040F4 RID: 16628
			private IssuanceTokenProviderBase<T> tokenProvider;

			// Token: 0x040040F5 RID: 16629
			private IRequestChannel rstChannel;

			// Token: 0x040040F6 RID: 16630
			private T negotiationState;

			// Token: 0x040040F7 RID: 16631
			private Message nextOutgoingMessage;

			// Token: 0x040040F8 RID: 16632
			private EndpointAddress target;

			// Token: 0x040040F9 RID: 16633
			private EndpointAddress issuer;

			// Token: 0x040040FA RID: 16634
			private Uri via;
		}
	}
}
