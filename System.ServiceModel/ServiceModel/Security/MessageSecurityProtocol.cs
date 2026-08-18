using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002CD RID: 717
	internal abstract class MessageSecurityProtocol : SecurityProtocol
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x000580C6 File Offset: 0x000562C6
		protected MessageSecurityProtocol(MessageSecurityProtocolFactory factory, EndpointAddress target, Uri via) : base(factory, target, via)
		{
			this.factory = factory;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000580D8 File Offset: 0x000562D8
		protected virtual bool CacheIdentityCheckResultForToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x000580DB File Offset: 0x000562DB
		protected virtual bool DoAutomaticEncryptionMatch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x000580DE File Offset: 0x000562DE
		protected virtual bool PerformIncomingAndOutgoingMessageExpectationChecks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000580E4 File Offset: 0x000562E4
		protected bool RequiresIncomingSecurityProcessing(Message message)
		{
			return (!this.factory.ActAsInitiator || !this.factory.SecurityBindingElement.EnableUnsecuredResponse || this.factory.StandardsManager.SecurityVersion.DoesMessageContainSecurityHeader(message)) && (this.factory.RequireIntegrity || this.factory.RequireConfidentiality || this.factory.DetectReplays || this.factory.ExpectSupportingTokens);
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00058164 File Offset: 0x00056364
		protected bool RequiresOutgoingSecurityProcessing
		{
			get
			{
				return (this.factory.ActAsInitiator || !this.factory.SecurityBindingElement.EnableUnsecuredResponse) && (this.factory.ApplyIntegrity || this.factory.ApplyConfidentiality || this.factory.AddTimestamp || this.factory.ExpectSupportingTokens);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x000581CB File Offset: 0x000563CB
		protected MessageSecurityProtocolFactory MessageSecurityProtocolFactory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000581D4 File Offset: 0x000563D4
		public override IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				base.CommunicationObject.ThrowIfClosedOrNotOpen();
				this.ValidateOutgoingState(message);
				if (!this.RequiresOutgoingSecurityProcessing && message.Properties.Security == null)
				{
					result = new CompletedAsyncResult<Message>(message, callback, state);
				}
				else
				{
					result = this.BeginSecureOutgoingMessageCore(message, timeout, null, callback, state);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.OnSecureOutgoingMessageFailure(message);
				throw;
			}
			return result;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00058248 File Offset: 0x00056448
		public override IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			try
			{
				base.CommunicationObject.ThrowIfClosedOrNotOpen();
				this.ValidateOutgoingState(message);
				if (!this.RequiresOutgoingSecurityProcessing && message.Properties.Security == null)
				{
					result = new CompletedAsyncResult<Message>(message, callback, state);
				}
				else
				{
					result = this.BeginSecureOutgoingMessageCore(message, timeout, correlationState, callback, state);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.OnSecureOutgoingMessageFailure(message);
				throw;
			}
			return result;
		}

		// Token: 0x0600173A RID: 5946
		protected abstract IAsyncResult BeginSecureOutgoingMessageCore(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state);

		// Token: 0x0600173B RID: 5947 RVA: 0x000582C0 File Offset: 0x000564C0
		public override void EndSecureOutgoingMessage(IAsyncResult result, out Message message)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			try
			{
				SecurityProtocolCorrelationState securityProtocolCorrelationState;
				this.EndSecureOutgoingMessageCore(result, out message, out securityProtocolCorrelationState);
				base.OnOutgoingMessageSecured(message);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.OnSecureOutgoingMessageFailure(null);
				throw;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0005831C File Offset: 0x0005651C
		public override void EndSecureOutgoingMessage(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			try
			{
				this.EndSecureOutgoingMessageCore(result, out message, out newCorrelationState);
				base.OnOutgoingMessageSecured(message);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.OnSecureOutgoingMessageFailure(null);
				throw;
			}
		}

		// Token: 0x0600173D RID: 5949
		protected abstract void EndSecureOutgoingMessageCore(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState);

		// Token: 0x0600173E RID: 5950 RVA: 0x00058374 File Offset: 0x00056574
		protected void AttachRecipientSecurityProperty(Message message, SecurityToken protectionToken, bool isWrappedToken, IList<SecurityToken> basicTokens, IList<SecurityToken> endorsingTokens, IList<SecurityToken> signedEndorsingTokens, IList<SecurityToken> signedTokens, Dictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> tokenPoliciesMapping)
		{
			ReadOnlyCollection<IAuthorizationPolicy> tokenPolicies;
			if (isWrappedToken)
			{
				tokenPolicies = EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			else
			{
				tokenPolicies = tokenPoliciesMapping[protectionToken];
			}
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(message);
			orCreate.ProtectionToken = new SecurityTokenSpecification(protectionToken, tokenPolicies);
			base.AddSupportingTokenSpecification(orCreate, basicTokens, endorsingTokens, signedEndorsingTokens, signedTokens, tokenPoliciesMapping);
			orCreate.ServiceSecurityContext = new ServiceSecurityContext(orCreate.GetInitiatorTokenAuthorizationPolicies());
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000583CC File Offset: 0x000565CC
		protected void DoIdentityCheckAndAttachInitiatorSecurityProperty(Message message, SecurityToken protectionToken, ReadOnlyCollection<IAuthorizationPolicy> protectionTokenPolicies)
		{
			AuthorizationContext authorizationContext = this.EnsureIncomingIdentity(message, protectionToken, protectionTokenPolicies);
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(message);
			orCreate.ProtectionToken = new SecurityTokenSpecification(protectionToken, protectionTokenPolicies);
			orCreate.ServiceSecurityContext = new ServiceSecurityContext(authorizationContext, protectionTokenPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00058410 File Offset: 0x00056610
		protected AuthorizationContext EnsureIncomingIdentity(Message message, SecurityToken token, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			if (token == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoSigningTokenAvailableToDoIncomingIdentityCheck")), message);
			}
			AuthorizationContext authorizationContext = (authorizationPolicies != null) ? AuthorizationContext.CreateDefaultAuthorizationContext(authorizationPolicies) : null;
			if (this.factory.IdentityVerifier != null)
			{
				if (base.Target == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoOutgoingEndpointAddressAvailableForDoingIdentityCheckOnReply")), message);
				}
				this.factory.IdentityVerifier.EnsureIncomingIdentity(base.Target, authorizationContext);
			}
			return authorizationContext;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005848C File Offset: 0x0005668C
		protected void EnsureOutgoingIdentity(SecurityToken token, SecurityTokenAuthenticator authenticator)
		{
			if (token == this.identityVerifiedToken)
			{
				return;
			}
			if (this.factory.IdentityVerifier == null)
			{
				return;
			}
			if (base.Target == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoOutgoingEndpointAddressAvailableForDoingIdentityCheck")));
			}
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies = authenticator.ValidateToken(token);
			this.factory.IdentityVerifier.EnsureOutgoingIdentity(base.Target, authorizationPolicies);
			if (this.CacheIdentityCheckResultForToken)
			{
				this.identityVerifiedToken = token;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00058507 File Offset: 0x00056707
		protected SecurityProtocolCorrelationState GetCorrelationState(SecurityToken correlationToken)
		{
			return new SecurityProtocolCorrelationState(correlationToken);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00058510 File Offset: 0x00056710
		protected SecurityProtocolCorrelationState GetCorrelationState(SecurityToken correlationToken, ReceiveSecurityHeader securityHeader)
		{
			SecurityProtocolCorrelationState securityProtocolCorrelationState = new SecurityProtocolCorrelationState(correlationToken);
			if (securityHeader.MaintainSignatureConfirmationState && !this.factory.ActAsInitiator)
			{
				securityProtocolCorrelationState.SignatureConfirmations = securityHeader.GetSentSignatureValues();
			}
			return securityProtocolCorrelationState;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00058548 File Offset: 0x00056748
		protected SecurityToken GetCorrelationToken(SecurityProtocolCorrelationState[] correlationStates)
		{
			SecurityToken securityToken = null;
			if (correlationStates != null)
			{
				for (int i = 0; i < correlationStates.Length; i++)
				{
					if (correlationStates[i].Token != null)
					{
						if (securityToken == null)
						{
							securityToken = correlationStates[i].Token;
						}
						else if (securityToken != correlationStates[i].Token)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MultipleCorrelationTokensFound")));
						}
					}
				}
			}
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoCorrelationTokenFound")));
			}
			return securityToken;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000585C3 File Offset: 0x000567C3
		protected SecurityToken GetCorrelationToken(SecurityProtocolCorrelationState correlationState)
		{
			if (correlationState == null || correlationState.Token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("CannotFindCorrelationStateForApplyingSecurity")));
			}
			return correlationState.Token;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000585F0 File Offset: 0x000567F0
		protected static void EnsureNonWrappedToken(SecurityToken token, Message message)
		{
			if (token is WrappedKeySecurityToken)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenNotExpectedInSecurityHeader", new object[]
				{
					token
				})), message);
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0005861C File Offset: 0x0005681C
		protected SecurityToken GetTokenAndEnsureOutgoingIdentity(SecurityTokenProvider provider, bool isEncryptionOn, TimeSpan timeout, SecurityTokenAuthenticator authenticator)
		{
			SecurityToken token = SecurityProtocol.GetToken(provider, base.Target, timeout);
			if (isEncryptionOn)
			{
				this.EnsureOutgoingIdentity(token, authenticator);
			}
			return token;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00058644 File Offset: 0x00056844
		protected SendSecurityHeader ConfigureSendSecurityHeader(Message message, string actor, IList<SupportingTokenSpecification> supportingTokens, SecurityProtocolCorrelationState correlationState)
		{
			MessageSecurityProtocolFactory messageSecurityProtocolFactory = this.MessageSecurityProtocolFactory;
			SendSecurityHeader sendSecurityHeader = base.CreateSendSecurityHeader(message, actor, messageSecurityProtocolFactory);
			sendSecurityHeader.SignThenEncrypt = (messageSecurityProtocolFactory.MessageProtectionOrder != MessageProtectionOrder.EncryptBeforeSign);
			sendSecurityHeader.ShouldProtectTokens = messageSecurityProtocolFactory.SecurityBindingElement.ProtectTokens;
			sendSecurityHeader.EncryptPrimarySignature = (messageSecurityProtocolFactory.MessageProtectionOrder == MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature);
			if (messageSecurityProtocolFactory.DoRequestSignatureConfirmation && correlationState != null)
			{
				if (messageSecurityProtocolFactory.ActAsInitiator)
				{
					sendSecurityHeader.MaintainSignatureConfirmationState = true;
					sendSecurityHeader.CorrelationState = correlationState;
				}
				else if (correlationState.SignatureConfirmations != null)
				{
					sendSecurityHeader.AddSignatureConfirmations(correlationState.SignatureConfirmations);
				}
			}
			string action = message.Headers.Action;
			if (this.factory.ApplyIntegrity)
			{
				sendSecurityHeader.SignatureParts = this.factory.GetOutgoingSignatureParts(action);
			}
			if (messageSecurityProtocolFactory.ApplyConfidentiality)
			{
				sendSecurityHeader.EncryptionParts = this.factory.GetOutgoingEncryptionParts(action);
			}
			base.AddSupportingTokens(sendSecurityHeader, supportingTokens);
			return sendSecurityHeader;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00058720 File Offset: 0x00056920
		protected ReceiveSecurityHeader CreateSecurityHeader(Message message, string actor, MessageDirection transferDirection, SecurityStandardsManager standardsManager)
		{
			standardsManager = (standardsManager ?? this.factory.StandardsManager);
			ReceiveSecurityHeader receiveSecurityHeader = standardsManager.CreateReceiveSecurityHeader(message, actor, this.factory.IncomingAlgorithmSuite, transferDirection);
			receiveSecurityHeader.Layout = this.factory.SecurityHeaderLayout;
			receiveSecurityHeader.MaxReceivedMessageSize = this.factory.SecurityBindingElement.MaxReceivedMessageSize;
			receiveSecurityHeader.ReaderQuotas = this.factory.SecurityBindingElement.ReaderQuotas;
			if (this.factory.ExpectKeyDerivation)
			{
				receiveSecurityHeader.DerivedTokenAuthenticator = this.factory.DerivedKeyTokenAuthenticator;
			}
			return receiveSecurityHeader;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000587B2 File Offset: 0x000569B2
		private bool HasCorrelationState(SecurityProtocolCorrelationState[] correlationState)
		{
			return correlationState != null && correlationState.Length != 0 && (correlationState.Length != 1 || correlationState[0] != null);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000587CB File Offset: 0x000569CB
		protected ReceiveSecurityHeader ConfigureReceiveSecurityHeader(Message message, string actor, SecurityProtocolCorrelationState[] correlationStates, out IList<SupportingTokenAuthenticatorSpecification> supportingAuthenticators)
		{
			return this.ConfigureReceiveSecurityHeader(message, actor, correlationStates, null, out supportingAuthenticators);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000587DC File Offset: 0x000569DC
		protected ReceiveSecurityHeader ConfigureReceiveSecurityHeader(Message message, string actor, SecurityProtocolCorrelationState[] correlationStates, SecurityStandardsManager standardsManager, out IList<SupportingTokenAuthenticatorSpecification> supportingAuthenticators)
		{
			MessageSecurityProtocolFactory messageSecurityProtocolFactory = this.MessageSecurityProtocolFactory;
			MessageDirection transferDirection = messageSecurityProtocolFactory.ActAsInitiator ? MessageDirection.Output : MessageDirection.Input;
			ReceiveSecurityHeader receiveSecurityHeader = this.CreateSecurityHeader(message, actor, transferDirection, standardsManager);
			string action = message.Headers.Action;
			supportingAuthenticators = base.GetSupportingTokenAuthenticatorsAndSetExpectationFlags(this.factory, message, receiveSecurityHeader);
			if (messageSecurityProtocolFactory.RequireIntegrity || receiveSecurityHeader.ExpectSignedTokens)
			{
				receiveSecurityHeader.RequiredSignatureParts = messageSecurityProtocolFactory.GetIncomingSignatureParts(action);
			}
			if (messageSecurityProtocolFactory.RequireConfidentiality || receiveSecurityHeader.ExpectBasicTokens)
			{
				receiveSecurityHeader.RequiredEncryptionParts = messageSecurityProtocolFactory.GetIncomingEncryptionParts(action);
			}
			receiveSecurityHeader.ExpectEncryption = (messageSecurityProtocolFactory.RequireConfidentiality || receiveSecurityHeader.ExpectBasicTokens);
			receiveSecurityHeader.ExpectSignature = (messageSecurityProtocolFactory.RequireIntegrity || receiveSecurityHeader.ExpectSignedTokens);
			receiveSecurityHeader.SetRequiredProtectionOrder(messageSecurityProtocolFactory.MessageProtectionOrder);
			receiveSecurityHeader.RequireSignedPrimaryToken = (!messageSecurityProtocolFactory.ActAsInitiator && messageSecurityProtocolFactory.SecurityBindingElement.ProtectTokens);
			if (messageSecurityProtocolFactory.ActAsInitiator && messageSecurityProtocolFactory.DoRequestSignatureConfirmation && this.HasCorrelationState(correlationStates))
			{
				receiveSecurityHeader.MaintainSignatureConfirmationState = true;
				receiveSecurityHeader.ExpectSignatureConfirmation = true;
			}
			else if (!messageSecurityProtocolFactory.ActAsInitiator && messageSecurityProtocolFactory.DoRequestSignatureConfirmation)
			{
				receiveSecurityHeader.MaintainSignatureConfirmationState = true;
			}
			else
			{
				receiveSecurityHeader.MaintainSignatureConfirmationState = false;
			}
			return receiveSecurityHeader;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00058900 File Offset: 0x00056B00
		protected void ProcessSecurityHeader(ReceiveSecurityHeader securityHeader, ref Message message, SecurityToken requiredSigningToken, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			securityHeader.ReplayDetectionEnabled = this.factory.DetectReplays;
			securityHeader.SetTimeParameters(this.factory.NonceCache, this.factory.ReplayWindow, this.factory.MaxClockSkew);
			securityHeader.Process(timeoutHelper.RemainingTime(), SecurityUtils.GetChannelBindingFromMessage(message), this.factory.ExtendedProtectionPolicy);
			if (this.factory.AddTimestamp && securityHeader.Timestamp == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("RequiredTimestampMissingInSecurityHeader")));
			}
			if (requiredSigningToken != null && requiredSigningToken != securityHeader.SignatureToken)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("ReplyWasNotSignedWithRequiredSigningToken")), message);
			}
			if (this.DoAutomaticEncryptionMatch)
			{
				SecurityUtils.EnsureExpectedSymmetricMatch(securityHeader.SignatureToken, securityHeader.EncryptionToken, message);
			}
			if (securityHeader.MaintainSignatureConfirmationState && this.factory.ActAsInitiator)
			{
				this.CheckSignatureConfirmation(securityHeader, correlationStates);
			}
			message = securityHeader.ProcessedMessage;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00058A00 File Offset: 0x00056C00
		protected void CheckSignatureConfirmation(ReceiveSecurityHeader securityHeader, SecurityProtocolCorrelationState[] correlationStates)
		{
			SignatureConfirmations sentSignatureConfirmations = securityHeader.GetSentSignatureConfirmations();
			SignatureConfirmations signatureConfirmations = null;
			if (correlationStates != null)
			{
				for (int i = 0; i < correlationStates.Length; i++)
				{
					if (correlationStates[i].SignatureConfirmations != null)
					{
						signatureConfirmations = correlationStates[i].SignatureConfirmations;
						break;
					}
				}
			}
			if (signatureConfirmations == null)
			{
				if (sentSignatureConfirmations != null && sentSignatureConfirmations.Count > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("FoundUnexpectedSignatureConfirmations")));
				}
				return;
			}
			else
			{
				bool flag = false;
				if (sentSignatureConfirmations != null && signatureConfirmations.Count == sentSignatureConfirmations.Count)
				{
					bool[] array = new bool[signatureConfirmations.Count];
					for (int j = 0; j < signatureConfirmations.Count; j++)
					{
						byte[] b;
						bool flag2;
						signatureConfirmations.GetConfirmation(j, out b, out flag2);
						for (int k = 0; k < sentSignatureConfirmations.Count; k++)
						{
							if (!array[k])
							{
								byte[] a;
								bool flag3;
								sentSignatureConfirmations.GetConfirmation(k, out a, out flag3);
								if (flag3 == flag2 && CryptoHelper.IsEqual(a, b))
								{
									array[k] = true;
									break;
								}
							}
						}
					}
					int num = 0;
					while (num < array.Length && array[num])
					{
						num++;
					}
					if (num == array.Length)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NotAllSignaturesConfirmed")));
				}
				return;
			}
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00058B30 File Offset: 0x00056D30
		public override void SecureOutgoingMessage(ref Message message, TimeSpan timeout)
		{
			try
			{
				base.CommunicationObject.ThrowIfClosedOrNotOpen();
				this.ValidateOutgoingState(message);
				if (this.RequiresOutgoingSecurityProcessing || message.Properties.Security != null)
				{
					this.SecureOutgoingMessageCore(ref message, timeout, null);
					base.OnOutgoingMessageSecured(message);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.OnSecureOutgoingMessageFailure(message);
				throw;
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00058BA4 File Offset: 0x00056DA4
		public override SecurityProtocolCorrelationState SecureOutgoingMessage(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
		{
			SecurityProtocolCorrelationState result;
			try
			{
				base.CommunicationObject.ThrowIfClosedOrNotOpen();
				this.ValidateOutgoingState(message);
				if (!this.RequiresOutgoingSecurityProcessing && message.Properties.Security == null)
				{
					result = null;
				}
				else
				{
					SecurityProtocolCorrelationState securityProtocolCorrelationState = this.SecureOutgoingMessageCore(ref message, timeout, correlationState);
					base.OnOutgoingMessageSecured(message);
					result = securityProtocolCorrelationState;
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.OnSecureOutgoingMessageFailure(message);
				throw;
			}
			return result;
		}

		// Token: 0x06001751 RID: 5969
		protected abstract SecurityProtocolCorrelationState SecureOutgoingMessageCore(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState);

		// Token: 0x06001752 RID: 5970 RVA: 0x00058C1C File Offset: 0x00056E1C
		private void ValidateOutgoingState(Message message)
		{
			if (this.PerformIncomingAndOutgoingMessageExpectationChecks && !this.factory.ExpectOutgoingMessages)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityBindingNotSetUpToProcessOutgoingMessages")));
			}
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00058C6C File Offset: 0x00056E6C
		public override void VerifyIncomingMessage(ref Message message, TimeSpan timeout)
		{
			try
			{
				base.CommunicationObject.ThrowIfClosedOrNotOpen();
				if (this.PerformIncomingAndOutgoingMessageExpectationChecks && !this.factory.ExpectIncomingMessages)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityBindingNotSetUpToProcessIncomingMessages")));
				}
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
				}
				if (this.RequiresIncomingSecurityProcessing(message))
				{
					string empty = string.Empty;
					this.VerifyIncomingMessageCore(ref message, empty, timeout, null);
					base.OnIncomingMessageVerified(message);
				}
			}
			catch (MessageSecurityException exception)
			{
				base.OnVerifyIncomingMessageFailure(message, exception);
				throw;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.OnVerifyIncomingMessageFailure(message, ex);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MessageSecurityVerificationFailed"), ex));
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00058D48 File Offset: 0x00056F48
		public override SecurityProtocolCorrelationState VerifyIncomingMessage(ref Message message, TimeSpan timeout, params SecurityProtocolCorrelationState[] correlationStates)
		{
			SecurityProtocolCorrelationState result;
			try
			{
				base.CommunicationObject.ThrowIfClosedOrNotOpen();
				if (this.PerformIncomingAndOutgoingMessageExpectationChecks && !this.factory.ExpectIncomingMessages)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityBindingNotSetUpToProcessIncomingMessages")));
				}
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
				}
				if (!this.RequiresIncomingSecurityProcessing(message))
				{
					result = null;
				}
				else
				{
					string empty = string.Empty;
					SecurityProtocolCorrelationState securityProtocolCorrelationState = this.VerifyIncomingMessageCore(ref message, empty, timeout, correlationStates);
					base.OnIncomingMessageVerified(message);
					result = securityProtocolCorrelationState;
				}
			}
			catch (MessageSecurityException exception)
			{
				base.OnVerifyIncomingMessageFailure(message, exception);
				throw;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.OnVerifyIncomingMessageFailure(message, ex);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MessageSecurityVerificationFailed"), ex));
			}
			return result;
		}

		// Token: 0x06001755 RID: 5973
		protected abstract SecurityProtocolCorrelationState VerifyIncomingMessageCore(ref Message message, string actor, TimeSpan timeout, SecurityProtocolCorrelationState[] correlationStates);

		// Token: 0x06001756 RID: 5974 RVA: 0x00058E2C File Offset: 0x0005702C
		internal SecurityProtocolCorrelationState GetSignatureConfirmationCorrelationState(SecurityProtocolCorrelationState oldCorrelationState, SecurityProtocolCorrelationState newCorrelationState)
		{
			if (this.factory.ActAsInitiator)
			{
				return newCorrelationState;
			}
			return oldCorrelationState;
		}

		// Token: 0x04001C18 RID: 7192
		private readonly MessageSecurityProtocolFactory factory;

		// Token: 0x04001C19 RID: 7193
		private SecurityToken identityVerifiedToken;

		// Token: 0x02000B4E RID: 2894
		protected abstract class GetOneTokenAndSetUpSecurityAsyncResult : SecurityProtocol.GetSupportingTokensAsyncResult
		{
			// Token: 0x0600710B RID: 28939 RVA: 0x001A501C File Offset: 0x001A321C
			public GetOneTokenAndSetUpSecurityAsyncResult(Message m, MessageSecurityProtocol binding, SecurityTokenProvider provider, bool doIdentityChecks, SecurityTokenAuthenticator identityCheckAuthenticator, SecurityProtocolCorrelationState oldCorrelationState, TimeSpan timeout, AsyncCallback callback, object state) : base(m, binding, timeout, callback, state)
			{
				this.message = m;
				this.binding = binding;
				this.provider = provider;
				this.doIdentityChecks = doIdentityChecks;
				this.oldCorrelationState = oldCorrelationState;
				this.identityCheckAuthenticator = identityCheckAuthenticator;
			}

			// Token: 0x17001A5B RID: 6747
			// (get) Token: 0x0600710C RID: 28940 RVA: 0x001A5059 File Offset: 0x001A3259
			protected MessageSecurityProtocol Binding
			{
				get
				{
					return this.binding;
				}
			}

			// Token: 0x17001A5C RID: 6748
			// (get) Token: 0x0600710D RID: 28941 RVA: 0x001A5061 File Offset: 0x001A3261
			protected SecurityProtocolCorrelationState NewCorrelationState
			{
				get
				{
					return this.newCorrelationState;
				}
			}

			// Token: 0x17001A5D RID: 6749
			// (get) Token: 0x0600710E RID: 28942 RVA: 0x001A5069 File Offset: 0x001A3269
			protected SecurityProtocolCorrelationState OldCorrelationState
			{
				get
				{
					return this.oldCorrelationState;
				}
			}

			// Token: 0x0600710F RID: 28943 RVA: 0x001A5074 File Offset: 0x001A3274
			internal static Message End(IAsyncResult result, out SecurityProtocolCorrelationState newCorrelationState)
			{
				MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult getOneTokenAndSetUpSecurityAsyncResult = AsyncResult.End<MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult>(result);
				newCorrelationState = getOneTokenAndSetUpSecurityAsyncResult.newCorrelationState;
				return getOneTokenAndSetUpSecurityAsyncResult.message;
			}

			// Token: 0x06007110 RID: 28944 RVA: 0x001A5098 File Offset: 0x001A3298
			private bool OnGetTokenComplete(SecurityToken token)
			{
				if (token == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenProviderCannotGetTokensForTarget", new object[]
					{
						this.binding.Target
					})));
				}
				if (this.doIdentityChecks)
				{
					this.binding.EnsureOutgoingIdentity(token, this.identityCheckAuthenticator);
				}
				this.OnGetTokenDone(ref this.message, token, this.timeoutHelper.RemainingTime());
				return true;
			}

			// Token: 0x06007111 RID: 28945
			protected abstract void OnGetTokenDone(ref Message message, SecurityToken token, TimeSpan timeout);

			// Token: 0x06007112 RID: 28946 RVA: 0x001A510C File Offset: 0x001A330C
			private static void GetTokenCompleteCallback(IAsyncResult result)
			{
				if (result == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
				}
				if (result.CompletedSynchronously)
				{
					return;
				}
				MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult getOneTokenAndSetUpSecurityAsyncResult = result.AsyncState as MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult;
				if (getOneTokenAndSetUpSecurityAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("InvalidAsyncResult"));
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					SecurityToken token = getOneTokenAndSetUpSecurityAsyncResult.provider.EndGetToken(result);
					flag = getOneTokenAndSetUpSecurityAsyncResult.OnGetTokenComplete(token);
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
					getOneTokenAndSetUpSecurityAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007113 RID: 28947 RVA: 0x001A51A8 File Offset: 0x001A33A8
			protected void SetCorrelationToken(SecurityToken token)
			{
				this.newCorrelationState = new SecurityProtocolCorrelationState(token);
			}

			// Token: 0x06007114 RID: 28948 RVA: 0x001A51B8 File Offset: 0x001A33B8
			protected override bool OnGetSupportingTokensDone(TimeSpan timeout)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				IAsyncResult asyncResult = this.provider.BeginGetToken(this.timeoutHelper.RemainingTime(), MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult.getTokenCompleteCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				SecurityToken token = this.provider.EndGetToken(asyncResult);
				return this.OnGetTokenComplete(token);
			}

			// Token: 0x04004046 RID: 16454
			private readonly MessageSecurityProtocol binding;

			// Token: 0x04004047 RID: 16455
			private readonly SecurityTokenProvider provider;

			// Token: 0x04004048 RID: 16456
			private Message message;

			// Token: 0x04004049 RID: 16457
			private readonly bool doIdentityChecks;

			// Token: 0x0400404A RID: 16458
			private SecurityTokenAuthenticator identityCheckAuthenticator;

			// Token: 0x0400404B RID: 16459
			private static AsyncCallback getTokenCompleteCallback = Fx.ThunkCallback(new AsyncCallback(MessageSecurityProtocol.GetOneTokenAndSetUpSecurityAsyncResult.GetTokenCompleteCallback));

			// Token: 0x0400404C RID: 16460
			private SecurityProtocolCorrelationState newCorrelationState;

			// Token: 0x0400404D RID: 16461
			private SecurityProtocolCorrelationState oldCorrelationState;

			// Token: 0x0400404E RID: 16462
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000B4F RID: 2895
		protected abstract class GetTwoTokensAndSetUpSecurityAsyncResult : SecurityProtocol.GetSupportingTokensAsyncResult
		{
			// Token: 0x06007116 RID: 28950 RVA: 0x001A5224 File Offset: 0x001A3424
			public GetTwoTokensAndSetUpSecurityAsyncResult(Message m, MessageSecurityProtocol binding, SecurityTokenProvider primaryProvider, SecurityTokenProvider secondaryProvider, bool doIdentityChecks, SecurityTokenAuthenticator identityCheckAuthenticator, SecurityProtocolCorrelationState oldCorrelationState, TimeSpan timeout, AsyncCallback callback, object state) : base(m, binding, timeout, callback, state)
			{
				this.message = m;
				this.binding = binding;
				this.primaryProvider = primaryProvider;
				this.secondaryProvider = secondaryProvider;
				this.doIdentityChecks = doIdentityChecks;
				this.identityCheckAuthenticator = identityCheckAuthenticator;
				this.oldCorrelationState = oldCorrelationState;
			}

			// Token: 0x17001A5E RID: 6750
			// (get) Token: 0x06007117 RID: 28951 RVA: 0x001A5274 File Offset: 0x001A3474
			protected MessageSecurityProtocol Binding
			{
				get
				{
					return this.binding;
				}
			}

			// Token: 0x17001A5F RID: 6751
			// (get) Token: 0x06007118 RID: 28952 RVA: 0x001A527C File Offset: 0x001A347C
			protected SecurityProtocolCorrelationState NewCorrelationState
			{
				get
				{
					return this.newCorrelationState;
				}
			}

			// Token: 0x17001A60 RID: 6752
			// (get) Token: 0x06007119 RID: 28953 RVA: 0x001A5284 File Offset: 0x001A3484
			protected SecurityProtocolCorrelationState OldCorrelationState
			{
				get
				{
					return this.oldCorrelationState;
				}
			}

			// Token: 0x0600711A RID: 28954 RVA: 0x001A528C File Offset: 0x001A348C
			internal static Message End(IAsyncResult result, out SecurityProtocolCorrelationState newCorrelationState)
			{
				MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult getTwoTokensAndSetUpSecurityAsyncResult = AsyncResult.End<MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult>(result);
				newCorrelationState = getTwoTokensAndSetUpSecurityAsyncResult.newCorrelationState;
				return getTwoTokensAndSetUpSecurityAsyncResult.message;
			}

			// Token: 0x0600711B RID: 28955 RVA: 0x001A52AE File Offset: 0x001A34AE
			private bool OnGetPrimaryTokenComplete(SecurityToken token)
			{
				return this.OnGetPrimaryTokenComplete(token, false);
			}

			// Token: 0x0600711C RID: 28956 RVA: 0x001A52B8 File Offset: 0x001A34B8
			private bool OnGetPrimaryTokenComplete(SecurityToken token, bool primaryCallSkipped)
			{
				if (!primaryCallSkipped)
				{
					if (token == null)
					{
						throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenProviderCannotGetTokensForTarget", new object[]
						{
							this.binding.Target
						})), this.message);
					}
					if (this.doIdentityChecks)
					{
						this.binding.EnsureOutgoingIdentity(token, this.identityCheckAuthenticator);
					}
				}
				this.primaryToken = token;
				if (this.secondaryProvider == null)
				{
					return this.OnGetSecondaryTokenComplete(null, true);
				}
				IAsyncResult asyncResult = this.secondaryProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult.getSecondaryTokenCompleteCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				SecurityToken token2 = this.secondaryProvider.EndGetToken(asyncResult);
				return this.OnGetSecondaryTokenComplete(token2);
			}

			// Token: 0x0600711D RID: 28957 RVA: 0x001A5367 File Offset: 0x001A3567
			private bool OnGetSecondaryTokenComplete(SecurityToken token)
			{
				return this.OnGetSecondaryTokenComplete(token, false);
			}

			// Token: 0x0600711E RID: 28958 RVA: 0x001A5374 File Offset: 0x001A3574
			private bool OnGetSecondaryTokenComplete(SecurityToken token, bool secondaryCallSkipped)
			{
				if (!secondaryCallSkipped && token == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenProviderCannotGetTokensForTarget", new object[]
					{
						this.binding.Target
					})), this.message);
				}
				this.OnBothGetTokenCallsDone(ref this.message, this.primaryToken, token, this.timeoutHelper.RemainingTime());
				return true;
			}

			// Token: 0x0600711F RID: 28959
			protected abstract void OnBothGetTokenCallsDone(ref Message message, SecurityToken primaryToken, SecurityToken secondaryToken, TimeSpan timeout);

			// Token: 0x06007120 RID: 28960 RVA: 0x001A53D8 File Offset: 0x001A35D8
			private static void GetPrimaryTokenCompleteCallback(IAsyncResult result)
			{
				if (result == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
				}
				if (result.CompletedSynchronously)
				{
					return;
				}
				MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult getTwoTokensAndSetUpSecurityAsyncResult = result.AsyncState as MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult;
				if (getTwoTokensAndSetUpSecurityAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("InvalidAsyncResult"));
				}
				bool flag = false;
				Exception exception = null;
				try
				{
					SecurityToken token = getTwoTokensAndSetUpSecurityAsyncResult.primaryProvider.EndGetToken(result);
					flag = getTwoTokensAndSetUpSecurityAsyncResult.OnGetPrimaryTokenComplete(token);
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
					getTwoTokensAndSetUpSecurityAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007121 RID: 28961 RVA: 0x001A5474 File Offset: 0x001A3674
			private static void GetSecondaryTokenCompleteCallback(IAsyncResult result)
			{
				if (result == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
				}
				if (result.CompletedSynchronously)
				{
					return;
				}
				MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult getTwoTokensAndSetUpSecurityAsyncResult = result.AsyncState as MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult;
				if (getTwoTokensAndSetUpSecurityAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("InvalidAsyncResult"));
				}
				bool flag = false;
				Exception exception = null;
				try
				{
					SecurityToken token = getTwoTokensAndSetUpSecurityAsyncResult.secondaryProvider.EndGetToken(result);
					flag = getTwoTokensAndSetUpSecurityAsyncResult.OnGetSecondaryTokenComplete(token);
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
					getTwoTokensAndSetUpSecurityAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007122 RID: 28962 RVA: 0x001A5510 File Offset: 0x001A3710
			protected void SetCorrelationToken(SecurityToken token)
			{
				this.newCorrelationState = new SecurityProtocolCorrelationState(token);
			}

			// Token: 0x06007123 RID: 28963 RVA: 0x001A5520 File Offset: 0x001A3720
			protected override bool OnGetSupportingTokensDone(TimeSpan timeout)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				bool result = false;
				if (this.primaryProvider == null)
				{
					result = this.OnGetPrimaryTokenComplete(null);
				}
				else
				{
					IAsyncResult asyncResult = this.primaryProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult.getPrimaryTokenCompleteCallback, this);
					if (asyncResult.CompletedSynchronously)
					{
						SecurityToken token = this.primaryProvider.EndGetToken(asyncResult);
						result = this.OnGetPrimaryTokenComplete(token);
					}
				}
				return result;
			}

			// Token: 0x0400404F RID: 16463
			private readonly MessageSecurityProtocol binding;

			// Token: 0x04004050 RID: 16464
			private readonly SecurityTokenProvider primaryProvider;

			// Token: 0x04004051 RID: 16465
			private readonly SecurityTokenProvider secondaryProvider;

			// Token: 0x04004052 RID: 16466
			private Message message;

			// Token: 0x04004053 RID: 16467
			private readonly bool doIdentityChecks;

			// Token: 0x04004054 RID: 16468
			private SecurityTokenAuthenticator identityCheckAuthenticator;

			// Token: 0x04004055 RID: 16469
			private SecurityToken primaryToken;

			// Token: 0x04004056 RID: 16470
			private static readonly AsyncCallback getPrimaryTokenCompleteCallback = Fx.ThunkCallback(new AsyncCallback(MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult.GetPrimaryTokenCompleteCallback));

			// Token: 0x04004057 RID: 16471
			private static readonly AsyncCallback getSecondaryTokenCompleteCallback = Fx.ThunkCallback(new AsyncCallback(MessageSecurityProtocol.GetTwoTokensAndSetUpSecurityAsyncResult.GetSecondaryTokenCompleteCallback));

			// Token: 0x04004058 RID: 16472
			private SecurityProtocolCorrelationState newCorrelationState;

			// Token: 0x04004059 RID: 16473
			private SecurityProtocolCorrelationState oldCorrelationState;

			// Token: 0x0400405A RID: 16474
			private TimeoutHelper timeoutHelper;
		}
	}
}
