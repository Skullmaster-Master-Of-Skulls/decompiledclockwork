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

namespace System.ServiceModel.Security
{
	// Token: 0x020002D1 RID: 721
	internal class TransportSecurityProtocol : SecurityProtocol
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x00059E4D File Offset: 0x0005804D
		public TransportSecurityProtocol(TransportSecurityProtocolFactory factory, EndpointAddress target, Uri via) : base(factory, target, via)
		{
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00059E58 File Offset: 0x00058058
		public override void SecureOutgoingMessage(ref Message message, TimeSpan timeout)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			string empty = string.Empty;
			try
			{
				if (base.SecurityProtocolFactory.ActAsInitiator)
				{
					this.SecureOutgoingMessageAtInitiator(ref message, empty, timeout);
				}
				else
				{
					this.SecureOutgoingMessageAtResponder(ref message, empty);
				}
				base.OnOutgoingMessageSecured(message);
			}
			catch
			{
				base.OnSecureOutgoingMessageFailure(message);
				throw;
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00059ED0 File Offset: 0x000580D0
		protected virtual void SecureOutgoingMessageAtInitiator(ref Message message, string actor, TimeSpan timeout)
		{
			IList<SupportingTokenSpecification> supportingTokens;
			base.TryGetSupportingTokens(base.SecurityProtocolFactory, base.Target, base.Via, message, timeout, true, out supportingTokens);
			this.SetUpDelayedSecurityExecution(ref message, actor, supportingTokens);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00059F08 File Offset: 0x00058108
		protected void SecureOutgoingMessageAtResponder(ref Message message, string actor)
		{
			if (base.SecurityProtocolFactory.AddTimestamp && !base.SecurityProtocolFactory.SecurityBindingElement.EnableUnsecuredResponse)
			{
				SendSecurityHeader sendSecurityHeader = base.CreateSendSecurityHeaderForTransportProtocol(message, actor, base.SecurityProtocolFactory);
				message = sendSecurityHeader.SetupExecution();
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00059F4C File Offset: 0x0005814C
		internal void SetUpDelayedSecurityExecution(ref Message message, string actor, IList<SupportingTokenSpecification> supportingTokens)
		{
			SendSecurityHeader sendSecurityHeader = base.CreateSendSecurityHeaderForTransportProtocol(message, actor, base.SecurityProtocolFactory);
			base.AddSupportingTokens(sendSecurityHeader, supportingTokens);
			message = sendSecurityHeader.SetupExecution();
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00059F7C File Offset: 0x0005817C
		public override IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			string empty = string.Empty;
			IAsyncResult result;
			try
			{
				if (base.SecurityProtocolFactory.ActAsInitiator)
				{
					result = this.BeginSecureOutgoingMessageAtInitiatorCore(message, empty, timeout, callback, state);
				}
				else
				{
					this.SecureOutgoingMessageAtResponder(ref message, empty);
					result = new CompletedAsyncResult<Message>(message, callback, state);
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

		// Token: 0x06001799 RID: 6041 RVA: 0x0005A008 File Offset: 0x00058208
		public override IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginSecureOutgoingMessage(message, timeout, null, callback, state);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0005A018 File Offset: 0x00058218
		protected virtual IAsyncResult BeginSecureOutgoingMessageAtInitiatorCore(Message message, string actor, TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IList<SupportingTokenSpecification> supportingTokens;
			if (base.TryGetSupportingTokens(base.SecurityProtocolFactory, base.Target, base.Via, message, timeoutHelper.RemainingTime(), false, out supportingTokens))
			{
				this.SetUpDelayedSecurityExecution(ref message, actor, supportingTokens);
				return new CompletedAsyncResult<Message>(message, callback, state);
			}
			return new TransportSecurityProtocol.SecureOutgoingMessageAsyncResult(actor, message, this, timeout, callback, state);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0005A074 File Offset: 0x00058274
		protected virtual Message EndSecureOutgoingMessageAtInitiatorCore(IAsyncResult result)
		{
			if (result is CompletedAsyncResult<Message>)
			{
				return CompletedAsyncResult<Message>.End(result);
			}
			return TransportSecurityProtocol.SecureOutgoingMessageAsyncResult.End(result);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0005A08C File Offset: 0x0005828C
		public override void EndSecureOutgoingMessage(IAsyncResult result, out Message message)
		{
			SecurityProtocolCorrelationState securityProtocolCorrelationState;
			this.EndSecureOutgoingMessage(result, out message, out securityProtocolCorrelationState);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0005A0A4 File Offset: 0x000582A4
		public override void EndSecureOutgoingMessage(IAsyncResult result, out Message message, out SecurityProtocolCorrelationState newCorrelationState)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			newCorrelationState = null;
			try
			{
				if (result is CompletedAsyncResult<Message>)
				{
					message = CompletedAsyncResult<Message>.End(result);
				}
				else
				{
					message = this.EndSecureOutgoingMessageAtInitiatorCore(result);
				}
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

		// Token: 0x0600179E RID: 6046 RVA: 0x0005A110 File Offset: 0x00058310
		public sealed override void VerifyIncomingMessage(ref Message message, TimeSpan timeout)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			try
			{
				this.VerifyIncomingMessageCore(ref message, timeout);
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

		// Token: 0x0600179F RID: 6047 RVA: 0x0005A1A0 File Offset: 0x000583A0
		protected void AttachRecipientSecurityProperty(Message message, IList<SecurityToken> basicTokens, IList<SecurityToken> endorsingTokens, IList<SecurityToken> signedEndorsingTokens, IList<SecurityToken> signedTokens, Dictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> tokenPoliciesMapping)
		{
			SecurityMessageProperty orCreate = SecurityMessageProperty.GetOrCreate(message);
			base.AddSupportingTokenSpecification(orCreate, basicTokens, endorsingTokens, signedEndorsingTokens, signedTokens, tokenPoliciesMapping);
			orCreate.ServiceSecurityContext = new ServiceSecurityContext(orCreate.GetInitiatorTokenAuthorizationPolicies());
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0005A1D4 File Offset: 0x000583D4
		protected virtual void VerifyIncomingMessageCore(ref Message message, TimeSpan timeout)
		{
			TransportSecurityProtocolFactory transportSecurityProtocolFactory = (TransportSecurityProtocolFactory)base.SecurityProtocolFactory;
			string empty = string.Empty;
			ReceiveSecurityHeader receiveSecurityHeader = transportSecurityProtocolFactory.StandardsManager.TryCreateReceiveSecurityHeader(message, empty, transportSecurityProtocolFactory.IncomingAlgorithmSuite, transportSecurityProtocolFactory.ActAsInitiator ? MessageDirection.Output : MessageDirection.Input);
			bool flag;
			bool flag2;
			bool flag3;
			IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators = transportSecurityProtocolFactory.GetSupportingTokenAuthenticators(message.Headers.Action, out flag, out flag2, out flag3);
			if (receiveSecurityHeader != null)
			{
				receiveSecurityHeader.RequireMessageProtection = false;
				receiveSecurityHeader.ExpectBasicTokens = flag2;
				receiveSecurityHeader.ExpectSignedTokens = flag;
				receiveSecurityHeader.ExpectEndorsingTokens = flag3;
				receiveSecurityHeader.MaxReceivedMessageSize = transportSecurityProtocolFactory.SecurityBindingElement.MaxReceivedMessageSize;
				receiveSecurityHeader.ReaderQuotas = transportSecurityProtocolFactory.SecurityBindingElement.ReaderQuotas;
				if (ServiceModelAppSettings.UseConfiguredTransportSecurityHeaderLayout)
				{
					receiveSecurityHeader.Layout = transportSecurityProtocolFactory.SecurityHeaderLayout;
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (!transportSecurityProtocolFactory.ActAsInitiator)
				{
					receiveSecurityHeader.ConfigureTransportBindingServerReceiveHeader(supportingTokenAuthenticators);
					receiveSecurityHeader.ConfigureOutOfBandTokenResolver(base.MergeOutOfBandResolvers(supportingTokenAuthenticators, EmptyReadOnlyCollection<SecurityTokenResolver>.Instance));
					if (transportSecurityProtocolFactory.ExpectKeyDerivation)
					{
						receiveSecurityHeader.DerivedTokenAuthenticator = transportSecurityProtocolFactory.DerivedKeyTokenAuthenticator;
					}
				}
				receiveSecurityHeader.ReplayDetectionEnabled = transportSecurityProtocolFactory.DetectReplays;
				receiveSecurityHeader.SetTimeParameters(transportSecurityProtocolFactory.NonceCache, transportSecurityProtocolFactory.ReplayWindow, transportSecurityProtocolFactory.MaxClockSkew);
				receiveSecurityHeader.Process(timeoutHelper.RemainingTime(), SecurityUtils.GetChannelBindingFromMessage(message), transportSecurityProtocolFactory.ExtendedProtectionPolicy);
				message = receiveSecurityHeader.ProcessedMessage;
				if (!transportSecurityProtocolFactory.ActAsInitiator)
				{
					this.AttachRecipientSecurityProperty(message, receiveSecurityHeader.BasicSupportingTokens, receiveSecurityHeader.EndorsingSupportingTokens, receiveSecurityHeader.SignedEndorsingSupportingTokens, receiveSecurityHeader.SignedSupportingTokens, receiveSecurityHeader.SecurityTokenAuthorizationPoliciesMapping);
				}
				base.OnIncomingMessageVerified(message);
				return;
			}
			bool flag4 = flag3 || flag || flag2;
			if ((transportSecurityProtocolFactory.ActAsInitiator && (!transportSecurityProtocolFactory.AddTimestamp || transportSecurityProtocolFactory.SecurityBindingElement.EnableUnsecuredResponse)) || (!transportSecurityProtocolFactory.ActAsInitiator && !transportSecurityProtocolFactory.AddTimestamp && !flag4))
			{
				return;
			}
			if (string.IsNullOrEmpty(empty))
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToFindSecurityHeaderInMessageNoActor")), message);
			}
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToFindSecurityHeaderInMessage", new object[]
			{
				empty
			})), message);
		}

		// Token: 0x02000B51 RID: 2897
		private sealed class SecureOutgoingMessageAsyncResult : SecurityProtocol.GetSupportingTokensAsyncResult
		{
			// Token: 0x06007127 RID: 28967 RVA: 0x001A563C File Offset: 0x001A383C
			public SecureOutgoingMessageAsyncResult(string actor, Message message, TransportSecurityProtocol binding, TimeSpan timeout, AsyncCallback callback, object state) : base(message, binding, timeout, callback, state)
			{
				this.actor = actor;
				this.message = message;
				this.binding = binding;
				base.Start();
			}

			// Token: 0x06007128 RID: 28968 RVA: 0x001A5667 File Offset: 0x001A3867
			protected override bool OnGetSupportingTokensDone(TimeSpan timeout)
			{
				this.binding.SetUpDelayedSecurityExecution(ref this.message, this.actor, base.SupportingTokens);
				return true;
			}

			// Token: 0x06007129 RID: 28969 RVA: 0x001A5688 File Offset: 0x001A3888
			internal static Message End(IAsyncResult result)
			{
				TransportSecurityProtocol.SecureOutgoingMessageAsyncResult secureOutgoingMessageAsyncResult = AsyncResult.End<TransportSecurityProtocol.SecureOutgoingMessageAsyncResult>(result);
				return secureOutgoingMessageAsyncResult.message;
			}

			// Token: 0x0400405C RID: 16476
			private Message message;

			// Token: 0x0400405D RID: 16477
			private string actor;

			// Token: 0x0400405E RID: 16478
			private TransportSecurityProtocol binding;
		}
	}
}
