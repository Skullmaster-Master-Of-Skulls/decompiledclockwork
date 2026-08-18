using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000318 RID: 792
	internal sealed class AcceptorSessionSymmetricTransportSecurityProtocol : TransportSecurityProtocol, IAcceptorSecuritySessionProtocol
	{
		// Token: 0x06001B5E RID: 7006 RVA: 0x00066868 File Offset: 0x00064A68
		public AcceptorSessionSymmetricTransportSecurityProtocol(SessionSymmetricTransportSecurityProtocolFactory factory) : base(factory, null, null)
		{
			if (factory.ActAsInitiator)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProtocolMustBeRecipient", new object[]
				{
					base.GetType().ToString()
				})));
			}
			this.requireDerivedKeys = factory.SecurityTokenParameters.RequireDerivedKeys;
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x000668C5 File Offset: 0x00064AC5
		private SessionSymmetricTransportSecurityProtocolFactory Factory
		{
			get
			{
				return (SessionSymmetricTransportSecurityProtocolFactory)base.SecurityProtocolFactory;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000668D2 File Offset: 0x00064AD2
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x000668D5 File Offset: 0x00064AD5
		public bool ReturnCorrelationState
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000668D8 File Offset: 0x00064AD8
		public void SetSessionTokenAuthenticator(UniqueId sessionId, SecurityTokenAuthenticator sessionTokenAuthenticator, SecurityTokenResolver sessionTokenResolver)
		{
			base.CommunicationObject.ThrowIfDisposedOrImmutable();
			this.sessionId = sessionId;
			this.sessionTokenResolver = sessionTokenResolver;
			this.sessionTokenResolverList = new ReadOnlyCollection<SecurityTokenResolver>(new Collection<SecurityTokenResolver>
			{
				this.sessionTokenResolver
			});
			this.sessionTokenAuthenticator = sessionTokenAuthenticator;
			SupportingTokenAuthenticatorSpecification item = new SupportingTokenAuthenticatorSpecification(this.sessionTokenAuthenticator, this.sessionTokenResolver, SecurityTokenAttachmentMode.Endorsing, this.Factory.SecurityTokenParameters);
			this.sessionTokenAuthenticatorSpecificationList = new Collection<SupportingTokenAuthenticatorSpecification>();
			this.sessionTokenAuthenticatorSpecificationList.Add(item);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00066958 File Offset: 0x00064B58
		public SecurityToken GetOutgoingSessionToken()
		{
			return this.outgoingSessionToken;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00066960 File Offset: 0x00064B60
		public void SetOutgoingSessionToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			this.outgoingSessionToken = token;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0006697C File Offset: 0x00064B7C
		protected override void VerifyIncomingMessageCore(ref Message message, TimeSpan timeout)
		{
			string empty = string.Empty;
			ReceiveSecurityHeader receiveSecurityHeader = this.Factory.StandardsManager.CreateReceiveSecurityHeader(message, empty, this.Factory.IncomingAlgorithmSuite, MessageDirection.Input);
			receiveSecurityHeader.RequireMessageProtection = false;
			receiveSecurityHeader.ReaderQuotas = this.Factory.SecurityBindingElement.ReaderQuotas;
			IList<SupportingTokenAuthenticatorSpecification> list = base.GetSupportingTokenAuthenticatorsAndSetExpectationFlags(this.Factory, message, receiveSecurityHeader);
			ReadOnlyCollection<SecurityTokenResolver> outOfBandResolvers = base.MergeOutOfBandResolvers(list, this.sessionTokenResolverList);
			if (list != null && list.Count > 0)
			{
				list = new List<SupportingTokenAuthenticatorSpecification>(list);
				list.Insert(0, this.sessionTokenAuthenticatorSpecificationList[0]);
			}
			else
			{
				list = this.sessionTokenAuthenticatorSpecificationList;
			}
			receiveSecurityHeader.ConfigureTransportBindingServerReceiveHeader(list);
			receiveSecurityHeader.ConfigureOutOfBandTokenResolver(outOfBandResolvers);
			receiveSecurityHeader.ExpectEndorsingTokens = true;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			receiveSecurityHeader.ReplayDetectionEnabled = this.Factory.DetectReplays;
			receiveSecurityHeader.SetTimeParameters(this.Factory.NonceCache, this.Factory.ReplayWindow, this.Factory.MaxClockSkew);
			receiveSecurityHeader.EnforceDerivedKeyRequirement = (message.Headers.Action != this.Factory.StandardsManager.SecureConversationDriver.CloseAction.Value);
			receiveSecurityHeader.Process(timeoutHelper.RemainingTime(), SecurityUtils.GetChannelBindingFromMessage(message), this.Factory.ExtendedProtectionPolicy);
			if (receiveSecurityHeader.Timestamp == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("RequiredTimestampMissingInSecurityHeader")));
			}
			bool flag = false;
			if (receiveSecurityHeader.EndorsingSupportingTokens != null)
			{
				for (int i = 0; i < receiveSecurityHeader.EndorsingSupportingTokens.Count; i++)
				{
					SecurityContextSecurityToken securityContextSecurityToken = receiveSecurityHeader.EndorsingSupportingTokens[i] as SecurityContextSecurityToken;
					if (securityContextSecurityToken != null && securityContextSecurityToken.ContextId == this.sessionId)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("NoSessionTokenPresentInMessage")));
			}
			message = receiveSecurityHeader.ProcessedMessage;
			base.AttachRecipientSecurityProperty(message, receiveSecurityHeader.BasicSupportingTokens, receiveSecurityHeader.EndorsingSupportingTokens, receiveSecurityHeader.SignedEndorsingSupportingTokens, receiveSecurityHeader.SignedSupportingTokens, receiveSecurityHeader.SecurityTokenAuthorizationPoliciesMapping);
			base.OnIncomingMessageVerified(message);
		}

		// Token: 0x04001D76 RID: 7542
		private SecurityToken outgoingSessionToken;

		// Token: 0x04001D77 RID: 7543
		private SecurityTokenAuthenticator sessionTokenAuthenticator;

		// Token: 0x04001D78 RID: 7544
		private SecurityTokenResolver sessionTokenResolver;

		// Token: 0x04001D79 RID: 7545
		private ReadOnlyCollection<SecurityTokenResolver> sessionTokenResolverList;

		// Token: 0x04001D7A RID: 7546
		private UniqueId sessionId;

		// Token: 0x04001D7B RID: 7547
		private Collection<SupportingTokenAuthenticatorSpecification> sessionTokenAuthenticatorSpecificationList;

		// Token: 0x04001D7C RID: 7548
		private bool requireDerivedKeys;
	}
}
