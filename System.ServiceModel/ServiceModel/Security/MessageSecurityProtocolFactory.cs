using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002CE RID: 718
	internal abstract class MessageSecurityProtocolFactory : SecurityProtocolFactory
	{
		// Token: 0x06001757 RID: 5975 RVA: 0x00058E3E File Offset: 0x0005703E
		protected MessageSecurityProtocolFactory()
		{
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00058E70 File Offset: 0x00057070
		internal MessageSecurityProtocolFactory(MessageSecurityProtocolFactory factory) : base(factory)
		{
			if (factory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("factory");
			}
			this.applyIntegrity = factory.applyIntegrity;
			this.applyConfidentiality = factory.applyConfidentiality;
			this.identityVerifier = factory.identityVerifier;
			this.protectionRequirements = new ChannelProtectionRequirements(factory.protectionRequirements);
			this.messageProtectionOrder = factory.messageProtectionOrder;
			this.requireIntegrity = factory.requireIntegrity;
			this.requireConfidentiality = factory.requireConfidentiality;
			this.doRequestSignatureConfirmation = factory.doRequestSignatureConfirmation;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00058F23 File Offset: 0x00057123
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x00058F2B File Offset: 0x0005712B
		public bool ApplyConfidentiality
		{
			get
			{
				return this.applyConfidentiality;
			}
			set
			{
				base.ThrowIfImmutable();
				this.applyConfidentiality = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00058F3A File Offset: 0x0005713A
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x00058F42 File Offset: 0x00057142
		public bool ApplyIntegrity
		{
			get
			{
				return this.applyIntegrity;
			}
			set
			{
				base.ThrowIfImmutable();
				this.applyIntegrity = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x00058F51 File Offset: 0x00057151
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x00058F59 File Offset: 0x00057159
		public bool DoRequestSignatureConfirmation
		{
			get
			{
				return this.doRequestSignatureConfirmation;
			}
			set
			{
				base.ThrowIfImmutable();
				this.doRequestSignatureConfirmation = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x00058F68 File Offset: 0x00057168
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00058F70 File Offset: 0x00057170
		public IdentityVerifier IdentityVerifier
		{
			get
			{
				return this.identityVerifier;
			}
			set
			{
				base.ThrowIfImmutable();
				this.identityVerifier = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x00058F7F File Offset: 0x0005717F
		public ChannelProtectionRequirements ProtectionRequirements
		{
			get
			{
				return this.protectionRequirements;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00058F87 File Offset: 0x00057187
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00058F8F File Offset: 0x0005718F
		public MessageProtectionOrder MessageProtectionOrder
		{
			get
			{
				return this.messageProtectionOrder;
			}
			set
			{
				base.ThrowIfImmutable();
				this.messageProtectionOrder = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00058F9E File Offset: 0x0005719E
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x00058FA6 File Offset: 0x000571A6
		public bool RequireIntegrity
		{
			get
			{
				return this.requireIntegrity;
			}
			set
			{
				base.ThrowIfImmutable();
				this.requireIntegrity = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00058FB5 File Offset: 0x000571B5
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x00058FBD File Offset: 0x000571BD
		public bool RequireConfidentiality
		{
			get
			{
				return this.requireConfidentiality;
			}
			set
			{
				base.ThrowIfImmutable();
				this.requireConfidentiality = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00058FCC File Offset: 0x000571CC
		internal List<SecurityTokenAuthenticator> WrappedKeySecurityTokenAuthenticator
		{
			get
			{
				return this.wrappedKeyTokenAuthenticator;
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00058FD4 File Offset: 0x000571D4
		protected virtual void ValidateCorrelationSecuritySettings()
		{
			if (base.ActAsInitiator && this.SupportsRequestReply)
			{
				bool flag = this.ApplyIntegrity || this.ApplyConfidentiality;
				bool flag2 = this.RequireIntegrity || this.RequireConfidentiality;
				if (!flag && flag2)
				{
					base.OnPropertySettingsError("ApplyIntegrity", false);
				}
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005902C File Offset: 0x0005722C
		public override void OnOpen(TimeSpan timeout)
		{
			base.OnOpen(timeout);
			this.protectionRequirements.MakeReadOnly();
			if (base.DetectReplays && !this.RequireIntegrity)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("RequireIntegrity", SR.GetString("ForReplayDetectionToBeDoneRequireIntegrityMustBeSet"));
			}
			if (this.DoRequestSignatureConfirmation)
			{
				if (!this.SupportsRequestReply)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SignatureConfirmationRequiresRequestReply"));
				}
				if (!base.StandardsManager.SecurityVersion.SupportsSignatureConfirmation)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("SecurityVersionDoesNotSupportSignatureConfirmation", new object[]
					{
						base.StandardsManager.SecurityVersion
					}));
				}
			}
			this.wrappedKeyTokenAuthenticator = new List<SecurityTokenAuthenticator>(1);
			SecurityTokenAuthenticator item = new NonValidatingSecurityTokenAuthenticator<WrappedKeySecurityToken>();
			this.wrappedKeyTokenAuthenticator.Add(item);
			this.ValidateCorrelationSecuritySettings();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000590FC File Offset: 0x000572FC
		private static MessagePartSpecification ExtractMessageParts(string action, ScopedMessagePartSpecification scopedParts, bool isForSignature)
		{
			MessagePartSpecification result = null;
			if (scopedParts.TryGetParts(action, out result))
			{
				return result;
			}
			if (scopedParts.TryGetParts("*", out result))
			{
				return result;
			}
			SecurityVersion securityVersion = MessageSecurityVersion.Default.SecurityVersion;
			FaultCode subCode = new FaultCode(securityVersion.InvalidSecurityFaultCode.Value, securityVersion.HeaderNamespace.Value);
			FaultCode code = FaultCode.CreateSenderFaultCode(subCode);
			FaultReason reason = new FaultReason(SR.GetString("InvalidOrUnrecognizedAction", new object[]
			{
				action
			}), CultureInfo.CurrentCulture);
			MessageFault fault = MessageFault.CreateFault(code, reason);
			if (isForSignature)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoSignaturePartsSpecified", new object[]
				{
					action
				}), null, fault));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoEncryptionPartsSpecified", new object[]
			{
				action
			}), null, fault));
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000591D4 File Offset: 0x000573D4
		internal MessagePartSpecification GetIncomingEncryptionParts(string action)
		{
			if (!this.RequireConfidentiality)
			{
				return MessagePartSpecification.NoParts;
			}
			if (base.IsDuplexReply)
			{
				return MessageSecurityProtocolFactory.ExtractMessageParts(action, this.ProtectionRequirements.OutgoingEncryptionParts, false);
			}
			return MessageSecurityProtocolFactory.ExtractMessageParts(action, base.ActAsInitiator ? this.ProtectionRequirements.OutgoingEncryptionParts : this.ProtectionRequirements.IncomingEncryptionParts, false);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00059234 File Offset: 0x00057434
		internal MessagePartSpecification GetIncomingSignatureParts(string action)
		{
			if (!this.RequireIntegrity)
			{
				return MessagePartSpecification.NoParts;
			}
			if (base.IsDuplexReply)
			{
				return MessageSecurityProtocolFactory.ExtractMessageParts(action, this.ProtectionRequirements.OutgoingSignatureParts, true);
			}
			return MessageSecurityProtocolFactory.ExtractMessageParts(action, base.ActAsInitiator ? this.ProtectionRequirements.OutgoingSignatureParts : this.ProtectionRequirements.IncomingSignatureParts, true);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00059294 File Offset: 0x00057494
		internal MessagePartSpecification GetOutgoingEncryptionParts(string action)
		{
			if (!this.ApplyConfidentiality)
			{
				return MessagePartSpecification.NoParts;
			}
			if (base.IsDuplexReply)
			{
				return MessageSecurityProtocolFactory.ExtractMessageParts(action, this.ProtectionRequirements.OutgoingEncryptionParts, false);
			}
			return MessageSecurityProtocolFactory.ExtractMessageParts(action, base.ActAsInitiator ? this.ProtectionRequirements.IncomingEncryptionParts : this.ProtectionRequirements.OutgoingEncryptionParts, false);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000592F4 File Offset: 0x000574F4
		internal MessagePartSpecification GetOutgoingSignatureParts(string action)
		{
			if (!this.ApplyIntegrity)
			{
				return MessagePartSpecification.NoParts;
			}
			if (base.IsDuplexReply)
			{
				return MessageSecurityProtocolFactory.ExtractMessageParts(action, this.ProtectionRequirements.OutgoingSignatureParts, true);
			}
			return MessageSecurityProtocolFactory.ExtractMessageParts(action, base.ActAsInitiator ? this.ProtectionRequirements.IncomingSignatureParts : this.ProtectionRequirements.OutgoingSignatureParts, true);
		}

		// Token: 0x04001C1A RID: 7194
		internal const MessageProtectionOrder defaultMessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;

		// Token: 0x04001C1B RID: 7195
		internal const bool defaultDoRequestSignatureConfirmation = false;

		// Token: 0x04001C1C RID: 7196
		private bool applyIntegrity = true;

		// Token: 0x04001C1D RID: 7197
		private bool applyConfidentiality = true;

		// Token: 0x04001C1E RID: 7198
		private bool doRequestSignatureConfirmation;

		// Token: 0x04001C1F RID: 7199
		private IdentityVerifier identityVerifier;

		// Token: 0x04001C20 RID: 7200
		private ChannelProtectionRequirements protectionRequirements = new ChannelProtectionRequirements();

		// Token: 0x04001C21 RID: 7201
		private MessageProtectionOrder messageProtectionOrder;

		// Token: 0x04001C22 RID: 7202
		private bool requireIntegrity = true;

		// Token: 0x04001C23 RID: 7203
		private bool requireConfidentiality = true;

		// Token: 0x04001C24 RID: 7204
		private List<SecurityTokenAuthenticator> wrappedKeyTokenAuthenticator;
	}
}
