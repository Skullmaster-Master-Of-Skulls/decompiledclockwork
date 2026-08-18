using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B5 RID: 693
	internal abstract class SendSecurityHeader : SecurityHeader, IMessageHeaderWithSharedNamespace
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x000523A3 File Offset: 0x000505A3
		protected SendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection transferDirection) : base(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, transferDirection)
		{
			this.elementContainer = new SendSecurityHeaderElementContainer();
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000523C8 File Offset: 0x000505C8
		public SendSecurityHeaderElementContainer ElementContainer
		{
			get
			{
				return this.elementContainer;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000523D0 File Offset: 0x000505D0
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x000523D8 File Offset: 0x000505D8
		public SecurityProtocolCorrelationState CorrelationState
		{
			get
			{
				return this.correlationState;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.correlationState = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000523E7 File Offset: 0x000505E7
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00052409 File Offset: 0x00050609
		public BufferManager StreamBufferManager
		{
			get
			{
				if (this.bufferManager == null)
				{
					this.bufferManager = BufferManager.CreateBufferManager(0L, int.MaxValue);
				}
				return this.bufferManager;
			}
			set
			{
				this.bufferManager = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00052412 File Offset: 0x00050612
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x0005241C File Offset: 0x0005061C
		public MessagePartSpecification EncryptionParts
		{
			get
			{
				return this.encryptionParts;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				if (value == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("value"), base.Message);
				}
				if (!value.IsReadOnly)
				{
					throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessagePartSpecificationMustBeImmutable")), base.Message);
				}
				this.encryptionParts = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00052472 File Offset: 0x00050672
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x0005247A File Offset: 0x0005067A
		public bool EncryptPrimarySignature
		{
			get
			{
				return this.encryptSignature;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.encryptSignature = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00052489 File Offset: 0x00050689
		internal byte[] PrimarySignatureValue
		{
			get
			{
				return this.primarySignatureValue;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00052491 File Offset: 0x00050691
		protected internal SecurityTokenParameters SigningTokenParameters
		{
			get
			{
				return this.signingTokenParameters;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00052499 File Offset: 0x00050699
		protected bool ShouldSignToHeader
		{
			get
			{
				return this.shouldSignToHeader;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000524A1 File Offset: 0x000506A1
		// (set) Token: 0x060015B2 RID: 5554 RVA: 0x000524A9 File Offset: 0x000506A9
		public string IdPrefix
		{
			get
			{
				return this.idPrefix;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.idPrefix = ((string.IsNullOrEmpty(value) || value == "_") ? null : value);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000524D0 File Offset: 0x000506D0
		public override string Name
		{
			get
			{
				return base.StandardsManager.SecurityVersion.HeaderName.Value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000524E7 File Offset: 0x000506E7
		public override string Namespace
		{
			get
			{
				return base.StandardsManager.SecurityVersion.HeaderNamespace.Value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000524FE File Offset: 0x000506FE
		protected SecurityAppliedMessage SecurityAppliedMessage
		{
			get
			{
				return (SecurityAppliedMessage)base.Message;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0005250B File Offset: 0x0005070B
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00052513 File Offset: 0x00050713
		public bool SignThenEncrypt
		{
			get
			{
				return this.signThenEncrypt;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.signThenEncrypt = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00052522 File Offset: 0x00050722
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x0005252A File Offset: 0x0005072A
		public bool ShouldProtectTokens
		{
			get
			{
				return this.shouldProtectTokens;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.shouldProtectTokens = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00052539 File Offset: 0x00050739
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x00052544 File Offset: 0x00050744
		public MessagePartSpecification SignatureParts
		{
			get
			{
				return this.signatureParts;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				if (value == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("value"), base.Message);
				}
				if (!value.IsReadOnly)
				{
					throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessagePartSpecificationMustBeImmutable")), base.Message);
				}
				this.signatureParts = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0005259A File Offset: 0x0005079A
		public SecurityTimestamp Timestamp
		{
			get
			{
				return this.elementContainer.Timestamp;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x000525A7 File Offset: 0x000507A7
		public bool HasSignedTokens
		{
			get
			{
				return this.hasSignedTokens;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x000525AF File Offset: 0x000507AF
		public bool HasEncryptedTokens
		{
			get
			{
				return this.hasEncryptedTokens;
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000525B7 File Offset: 0x000507B7
		public void AddPrerequisiteToken(SecurityToken token)
		{
			base.ThrowIfProcessingStarted();
			if (token == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("token", base.Message);
			}
			this.elementContainer.PrerequisiteToken = token;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000525DF File Offset: 0x000507DF
		private void AddParameters(ref List<SecurityTokenParameters> list, SecurityTokenParameters item)
		{
			if (list == null)
			{
				list = new List<SecurityTokenParameters>();
			}
			list.Add(item);
		}

		// Token: 0x060015C1 RID: 5569
		public abstract void ApplyBodySecurity(XmlDictionaryWriter writer, IPrefixGenerator prefixGenerator);

		// Token: 0x060015C2 RID: 5570
		public abstract void ApplySecurityAndWriteHeaders(MessageHeaders headers, XmlDictionaryWriter writer, IPrefixGenerator prefixGenerator);

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000525F4 File Offset: 0x000507F4
		protected virtual bool HasSignedEncryptedMessagePart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000525F8 File Offset: 0x000507F8
		public void SetSigningToken(SecurityToken token, SecurityTokenParameters tokenParameters)
		{
			base.ThrowIfProcessingStarted();
			if ((token == null && tokenParameters != null) || (token != null && tokenParameters == null))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("TokenMustBeNullWhenTokenParametersAre")));
			}
			this.elementContainer.SourceSigningToken = token;
			this.signingTokenParameters = tokenParameters;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00052644 File Offset: 0x00050844
		public void SetEncryptionToken(SecurityToken token, SecurityTokenParameters tokenParameters)
		{
			base.ThrowIfProcessingStarted();
			if ((token == null && tokenParameters != null) || (token != null && tokenParameters == null))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("TokenMustBeNullWhenTokenParametersAre")));
			}
			this.elementContainer.SourceEncryptionToken = token;
			this.encryptingTokenParameters = tokenParameters;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00052690 File Offset: 0x00050890
		public void AddBasicSupportingToken(SecurityToken token, SecurityTokenParameters parameters)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			base.ThrowIfProcessingStarted();
			SendSecurityHeaderElement sendSecurityHeaderElement = new SendSecurityHeaderElement(token.Id, new TokenElement(token, base.StandardsManager));
			sendSecurityHeaderElement.MarkedForEncryption = true;
			this.elementContainer.AddBasicSupportingToken(sendSecurityHeaderElement);
			this.hasEncryptedTokens = true;
			this.hasSignedTokens = true;
			this.AddParameters(ref this.basicSupportingTokenParameters, parameters);
			if (this.basicTokens == null)
			{
				this.basicTokens = new List<SecurityToken>();
			}
			this.basicTokens.Add(token);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00052730 File Offset: 0x00050930
		public void AddEndorsingSupportingToken(SecurityToken token, SecurityTokenParameters parameters)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			base.ThrowIfProcessingStarted();
			this.elementContainer.AddEndorsingSupportingToken(token);
			if (!(token is ProviderBackedSecurityToken))
			{
				this.shouldSignToHeader |= (!base.RequireMessageProtection && SecurityUtils.GetSecurityKey<AsymmetricSecurityKey>(token) != null);
			}
			this.AddParameters(ref this.endorsingTokenParameters, parameters);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000527AC File Offset: 0x000509AC
		public void AddSignedEndorsingSupportingToken(SecurityToken token, SecurityTokenParameters parameters)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			base.ThrowIfProcessingStarted();
			this.elementContainer.AddSignedEndorsingSupportingToken(token);
			this.hasSignedTokens = true;
			this.shouldSignToHeader |= (!base.RequireMessageProtection && SecurityUtils.GetSecurityKey<AsymmetricSecurityKey>(token) != null);
			this.AddParameters(ref this.signedEndorsingTokenParameters, parameters);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00052828 File Offset: 0x00050A28
		public void AddSignedSupportingToken(SecurityToken token, SecurityTokenParameters parameters)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			base.ThrowIfProcessingStarted();
			this.elementContainer.AddSignedSupportingToken(token);
			this.hasSignedTokens = true;
			this.AddParameters(ref this.signedTokenParameters, parameters);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00052881 File Offset: 0x00050A81
		public void AddSignatureConfirmations(SignatureConfirmations confirmations)
		{
			base.ThrowIfProcessingStarted();
			this.signatureConfirmationsToSend = confirmations;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00052890 File Offset: 0x00050A90
		public void AddTimestamp(TimeSpan timestampValidityDuration)
		{
			DateTime utcNow = DateTime.UtcNow;
			string id = base.RequireMessageProtection ? SecurityUtils.GenerateId() : this.GenerateId();
			this.AddTimestamp(new SecurityTimestamp(utcNow, utcNow + timestampValidityDuration, id));
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000528D0 File Offset: 0x00050AD0
		public void AddTimestamp(SecurityTimestamp timestamp)
		{
			base.ThrowIfProcessingStarted();
			if (this.elementContainer.Timestamp != null)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TimestampAlreadySetForSecurityHeader")), base.Message);
			}
			if (timestamp == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("timestamp", base.Message);
			}
			this.elementContainer.Timestamp = timestamp;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0005292B File Offset: 0x00050B2B
		protected virtual ISignatureValueSecurityElement[] CreateSignatureConfirmationElements(SignatureConfirmations signatureConfirmations)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SignatureConfirmationNotSupported")));
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00052948 File Offset: 0x00050B48
		private void StartEncryption()
		{
			if (this.elementContainer.SourceEncryptionToken == null)
			{
				return;
			}
			SecurityTokenReferenceStyle tokenReferenceStyle = this.GetTokenReferenceStyle(this.encryptingTokenParameters);
			bool flag = tokenReferenceStyle == SecurityTokenReferenceStyle.Internal;
			SecurityKeyIdentifierClause securityKeyIdentifierClause = this.encryptingTokenParameters.CreateKeyIdentifierClause(this.elementContainer.SourceEncryptionToken, tokenReferenceStyle);
			if (securityKeyIdentifierClause == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
			}
			SecurityToken securityToken;
			SecurityKeyIdentifierClause securityKeyIdentifierClause2;
			if (!SecurityUtils.HasSymmetricSecurityKey(this.elementContainer.SourceEncryptionToken))
			{
				int num = Math.Max(128, base.AlgorithmSuite.DefaultSymmetricKeyLength);
				CryptoHelper.ValidateSymmetricKeyLength(num, base.AlgorithmSuite);
				byte[] array = new byte[num / 8];
				CryptoHelper.FillRandomBytes(array);
				string wrappingAlgorithm;
				XmlDictionaryString wrappingAlgorithmDictionaryString;
				base.AlgorithmSuite.GetKeyWrapAlgorithm(this.elementContainer.SourceEncryptionToken, out wrappingAlgorithm, out wrappingAlgorithmDictionaryString);
				WrappedKeySecurityToken wrappedKeySecurityToken = new WrappedKeySecurityToken(this.GenerateId(), array, wrappingAlgorithm, wrappingAlgorithmDictionaryString, this.elementContainer.SourceEncryptionToken, new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					securityKeyIdentifierClause
				}));
				this.elementContainer.WrappedEncryptionToken = wrappedKeySecurityToken;
				securityToken = wrappedKeySecurityToken;
				securityKeyIdentifierClause2 = new LocalIdKeyIdentifierClause(wrappedKeySecurityToken.Id, wrappedKeySecurityToken.GetType());
				flag = true;
			}
			else
			{
				securityToken = this.elementContainer.SourceEncryptionToken;
				securityKeyIdentifierClause2 = securityKeyIdentifierClause;
			}
			SecurityKeyIdentifierClause securityKeyIdentifierClause3;
			if (this.encryptingTokenParameters.RequireDerivedKeys)
			{
				string encryptionKeyDerivationAlgorithm = base.AlgorithmSuite.GetEncryptionKeyDerivationAlgorithm(securityToken, base.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
				string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(base.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
				if (!(encryptionKeyDerivationAlgorithm == keyDerivationAlgorithm))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						encryptionKeyDerivationAlgorithm
					})));
				}
				DerivedKeySecurityToken derivedKeySecurityToken = new DerivedKeySecurityToken(-1, 0, base.AlgorithmSuite.GetEncryptionKeyDerivationLength(securityToken, base.StandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, securityToken, securityKeyIdentifierClause2, encryptionKeyDerivationAlgorithm, this.GenerateId());
				this.encryptingToken = (this.elementContainer.DerivedEncryptionToken = derivedKeySecurityToken);
				securityKeyIdentifierClause3 = new LocalIdKeyIdentifierClause(derivedKeySecurityToken.Id, derivedKeySecurityToken.GetType());
			}
			else
			{
				this.encryptingToken = securityToken;
				securityKeyIdentifierClause3 = securityKeyIdentifierClause2;
			}
			this.skipKeyInfoForEncryption = (flag && base.EncryptedKeyContainsReferenceList && this.encryptingToken is WrappedKeySecurityToken && this.signThenEncrypt);
			SecurityKeyIdentifier keyIdentifier;
			if (this.skipKeyInfoForEncryption)
			{
				keyIdentifier = null;
			}
			else
			{
				keyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					securityKeyIdentifierClause3
				});
			}
			this.StartEncryptionCore(this.encryptingToken, keyIdentifier);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00052BAC File Offset: 0x00050DAC
		private void CompleteEncryption()
		{
			ISecurityElement securityElement = this.CompleteEncryptionCore(this.elementContainer.PrimarySignature, this.elementContainer.GetBasicSupportingTokens(), this.elementContainer.GetSignatureConfirmations(), this.elementContainer.GetEndorsingSignatures());
			if (securityElement == null)
			{
				this.elementContainer.SourceEncryptionToken = null;
				this.elementContainer.WrappedEncryptionToken = null;
				this.elementContainer.DerivedEncryptionToken = null;
				return;
			}
			if (this.skipKeyInfoForEncryption)
			{
				WrappedKeySecurityToken wrappedKeySecurityToken = this.encryptingToken as WrappedKeySecurityToken;
				wrappedKeySecurityToken.EnsureEncryptedKeySetUp();
				wrappedKeySecurityToken.EncryptedKey.ReferenceList = (ReferenceList)securityElement;
			}
			else
			{
				this.elementContainer.ReferenceList = securityElement;
			}
			this.basicTokenEncrypted = true;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00052C54 File Offset: 0x00050E54
		internal void StartSecurityApplication()
		{
			if (this.SignThenEncrypt)
			{
				this.StartSignature();
				this.StartEncryption();
				return;
			}
			this.StartEncryption();
			this.StartSignature();
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00052C78 File Offset: 0x00050E78
		internal void CompleteSecurityApplication()
		{
			if (this.SignThenEncrypt)
			{
				this.CompleteSignature();
				this.SignWithSupportingTokens();
				this.CompleteEncryption();
			}
			else
			{
				this.CompleteEncryption();
				this.CompleteSignature();
				this.SignWithSupportingTokens();
			}
			if (this.correlationState != null)
			{
				this.correlationState.SignatureConfirmations = this.GetSignatureValues();
			}
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00052CCC File Offset: 0x00050ECC
		public void RemoveSignatureEncryptionIfAppropriate()
		{
			if (this.SignThenEncrypt && this.EncryptPrimarySignature && this.SecurityAppliedMessage.BodyProtectionMode != MessagePartProtectionMode.SignThenEncrypt && (this.basicSupportingTokenParameters == null || this.basicSupportingTokenParameters.Count == 0) && (this.signatureConfirmationsToSend == null || this.signatureConfirmationsToSend.Count == 0 || !this.signatureConfirmationsToSend.IsMarkedForEncryption) && !this.HasSignedEncryptedMessagePart)
			{
				this.encryptSignature = false;
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00052D40 File Offset: 0x00050F40
		public string GenerateId()
		{
			int num = this.idCounter;
			this.idCounter = num + 1;
			int num2 = num;
			if (this.idPrefix != null)
			{
				return this.idPrefix + num2.ToString();
			}
			if (num2 < SendSecurityHeader.ids.Length)
			{
				return SendSecurityHeader.ids[num2];
			}
			return "_" + num2.ToString();
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00052D9D File Offset: 0x00050F9D
		private SignatureConfirmations GetSignatureValues()
		{
			return this.signatureValuesGenerated;
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00052DA5 File Offset: 0x00050FA5
		protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			base.StandardsManager.SecurityVersion.WriteStartHeader(writer);
			base.WriteHeaderAttributes(writer, messageVersion);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00052DC0 File Offset: 0x00050FC0
		internal static bool ShouldSerializeToken(SecurityTokenParameters parameters, MessageDirection transferDirection)
		{
			switch (parameters.InclusionMode)
			{
			case SecurityTokenInclusionMode.AlwaysToRecipient:
			case SecurityTokenInclusionMode.Once:
				return transferDirection == MessageDirection.Input;
			case SecurityTokenInclusionMode.Never:
				return false;
			case SecurityTokenInclusionMode.AlwaysToInitiator:
				return transferDirection == MessageDirection.Output;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedTokenInclusionMode", new object[]
				{
					parameters.InclusionMode
				})));
			}
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00052E28 File Offset: 0x00051028
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			if (this.basicSupportingTokenParameters != null && this.basicSupportingTokenParameters.Count > 0 && base.RequireMessageProtection && !this.basicTokenEncrypted)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BasicTokenCannotBeWrittenWithoutEncryption")), base.Message);
			}
			if (this.elementContainer.Timestamp != null && base.Layout != SecurityHeaderLayout.LaxTimestampLast)
			{
				base.StandardsManager.WSUtilitySpecificationVersion.WriteTimestamp(writer, this.elementContainer.Timestamp);
			}
			if (this.elementContainer.PrerequisiteToken != null)
			{
				base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, this.elementContainer.PrerequisiteToken);
			}
			if (this.elementContainer.SourceSigningToken != null && SendSecurityHeader.ShouldSerializeToken(this.signingTokenParameters, base.MessageDirection))
			{
				base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, this.elementContainer.SourceSigningToken);
				if (this.ShouldProtectTokens)
				{
					this.WriteSecurityTokenReferencyEntry(writer, this.elementContainer.SourceSigningToken, this.signingTokenParameters);
				}
			}
			if (this.elementContainer.DerivedSigningToken != null)
			{
				base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, this.elementContainer.DerivedSigningToken);
			}
			if (this.elementContainer.SourceEncryptionToken != null && this.elementContainer.SourceEncryptionToken != this.elementContainer.SourceSigningToken && SendSecurityHeader.ShouldSerializeToken(this.encryptingTokenParameters, base.MessageDirection))
			{
				base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, this.elementContainer.SourceEncryptionToken);
			}
			if (this.elementContainer.WrappedEncryptionToken != null)
			{
				base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, this.elementContainer.WrappedEncryptionToken);
			}
			if (this.elementContainer.DerivedEncryptionToken != null)
			{
				base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, this.elementContainer.DerivedEncryptionToken);
			}
			if (this.SignThenEncrypt && this.elementContainer.ReferenceList != null)
			{
				this.elementContainer.ReferenceList.WriteTo(writer, ServiceModelDictionaryManager.Instance);
			}
			SecurityToken[] signedSupportingTokens = this.elementContainer.GetSignedSupportingTokens();
			if (signedSupportingTokens != null)
			{
				for (int i = 0; i < signedSupportingTokens.Length; i++)
				{
					base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, signedSupportingTokens[i]);
					this.WriteSecurityTokenReferencyEntry(writer, signedSupportingTokens[i], this.signedTokenParameters[i]);
				}
			}
			SendSecurityHeaderElement[] basicSupportingTokens = this.elementContainer.GetBasicSupportingTokens();
			if (basicSupportingTokens != null)
			{
				for (int j = 0; j < basicSupportingTokens.Length; j++)
				{
					basicSupportingTokens[j].Item.WriteTo(writer, ServiceModelDictionaryManager.Instance);
					if (this.SignThenEncrypt)
					{
						this.WriteSecurityTokenReferencyEntry(writer, this.basicTokens[j], this.basicSupportingTokenParameters[j]);
					}
				}
			}
			SecurityToken[] endorsingSupportingTokens = this.elementContainer.GetEndorsingSupportingTokens();
			if (endorsingSupportingTokens != null)
			{
				for (int k = 0; k < endorsingSupportingTokens.Length; k++)
				{
					if (SendSecurityHeader.ShouldSerializeToken(this.endorsingTokenParameters[k], base.MessageDirection))
					{
						base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, endorsingSupportingTokens[k]);
					}
				}
			}
			SecurityToken[] endorsingDerivedSupportingTokens = this.elementContainer.GetEndorsingDerivedSupportingTokens();
			if (endorsingDerivedSupportingTokens != null)
			{
				for (int l = 0; l < endorsingDerivedSupportingTokens.Length; l++)
				{
					base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, endorsingDerivedSupportingTokens[l]);
				}
			}
			SecurityToken[] signedEndorsingSupportingTokens = this.elementContainer.GetSignedEndorsingSupportingTokens();
			if (signedEndorsingSupportingTokens != null)
			{
				for (int m = 0; m < signedEndorsingSupportingTokens.Length; m++)
				{
					base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, signedEndorsingSupportingTokens[m]);
					this.WriteSecurityTokenReferencyEntry(writer, signedEndorsingSupportingTokens[m], this.signedEndorsingTokenParameters[m]);
				}
			}
			SecurityToken[] signedEndorsingDerivedSupportingTokens = this.elementContainer.GetSignedEndorsingDerivedSupportingTokens();
			if (signedEndorsingDerivedSupportingTokens != null)
			{
				for (int n = 0; n < signedEndorsingDerivedSupportingTokens.Length; n++)
				{
					base.StandardsManager.SecurityTokenSerializer.WriteToken(writer, signedEndorsingDerivedSupportingTokens[n]);
				}
			}
			SendSecurityHeaderElement[] signatureConfirmations = this.elementContainer.GetSignatureConfirmations();
			if (signatureConfirmations != null)
			{
				for (int num = 0; num < signatureConfirmations.Length; num++)
				{
					signatureConfirmations[num].Item.WriteTo(writer, ServiceModelDictionaryManager.Instance);
				}
			}
			if (this.elementContainer.PrimarySignature != null && this.elementContainer.PrimarySignature.Item != null)
			{
				this.elementContainer.PrimarySignature.Item.WriteTo(writer, ServiceModelDictionaryManager.Instance);
			}
			SendSecurityHeaderElement[] endorsingSignatures = this.elementContainer.GetEndorsingSignatures();
			if (endorsingSignatures != null)
			{
				for (int num2 = 0; num2 < endorsingSignatures.Length; num2++)
				{
					endorsingSignatures[num2].Item.WriteTo(writer, ServiceModelDictionaryManager.Instance);
				}
			}
			if (!this.SignThenEncrypt && this.elementContainer.ReferenceList != null)
			{
				this.elementContainer.ReferenceList.WriteTo(writer, ServiceModelDictionaryManager.Instance);
			}
			if (this.elementContainer.Timestamp != null && base.Layout == SecurityHeaderLayout.LaxTimestampLast)
			{
				base.StandardsManager.WSUtilitySpecificationVersion.WriteTimestamp(writer, this.elementContainer.Timestamp);
			}
		}

		// Token: 0x060015D8 RID: 5592
		protected abstract void WriteSecurityTokenReferencyEntry(XmlDictionaryWriter writer, SecurityToken securityToken, SecurityTokenParameters securityTokenParameters);

		// Token: 0x060015D9 RID: 5593 RVA: 0x000532F0 File Offset: 0x000514F0
		public Message SetupExecution()
		{
			base.ThrowIfProcessingStarted();
			base.SetProcessingStarted();
			bool signBody = false;
			if (this.elementContainer.SourceSigningToken != null)
			{
				if (this.signatureParts == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("SignatureParts"), base.Message);
				}
				signBody = this.signatureParts.IsBodyIncluded;
			}
			bool encryptBody = false;
			if (this.elementContainer.SourceEncryptionToken != null)
			{
				if (this.encryptionParts == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("EncryptionParts"), base.Message);
				}
				encryptBody = this.encryptionParts.IsBodyIncluded;
			}
			SecurityAppliedMessage securityAppliedMessage = new SecurityAppliedMessage(base.Message, this, signBody, encryptBody);
			base.Message = securityAppliedMessage;
			return securityAppliedMessage;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00053392 File Offset: 0x00051592
		protected internal SecurityTokenReferenceStyle GetTokenReferenceStyle(SecurityTokenParameters parameters)
		{
			if (!SendSecurityHeader.ShouldSerializeToken(parameters, base.MessageDirection))
			{
				return SecurityTokenReferenceStyle.External;
			}
			return SecurityTokenReferenceStyle.Internal;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000533A8 File Offset: 0x000515A8
		private void StartSignature()
		{
			if (this.elementContainer.SourceSigningToken == null)
			{
				return;
			}
			SecurityTokenReferenceStyle tokenReferenceStyle = this.GetTokenReferenceStyle(this.signingTokenParameters);
			SecurityKeyIdentifierClause securityKeyIdentifierClause = this.signingTokenParameters.CreateKeyIdentifierClause(this.elementContainer.SourceSigningToken, tokenReferenceStyle);
			if (securityKeyIdentifierClause == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
			}
			SecurityToken securityToken;
			SecurityKeyIdentifierClause securityKeyIdentifierClause2;
			if (this.signingTokenParameters.RequireDerivedKeys && !this.signingTokenParameters.HasAsymmetricKey)
			{
				string signatureKeyDerivationAlgorithm = base.AlgorithmSuite.GetSignatureKeyDerivationAlgorithm(this.elementContainer.SourceSigningToken, base.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
				string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(base.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
				if (!(signatureKeyDerivationAlgorithm == keyDerivationAlgorithm))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						signatureKeyDerivationAlgorithm
					})));
				}
				DerivedKeySecurityToken derivedSigningToken = new DerivedKeySecurityToken(-1, 0, base.AlgorithmSuite.GetSignatureKeyDerivationLength(this.elementContainer.SourceSigningToken, base.StandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, this.elementContainer.SourceSigningToken, securityKeyIdentifierClause, signatureKeyDerivationAlgorithm, this.GenerateId());
				securityToken = (this.elementContainer.DerivedSigningToken = derivedSigningToken);
				securityKeyIdentifierClause2 = new LocalIdKeyIdentifierClause(securityToken.Id, securityToken.GetType());
			}
			else
			{
				securityToken = this.elementContainer.SourceSigningToken;
				securityKeyIdentifierClause2 = securityKeyIdentifierClause;
			}
			SecurityKeyIdentifier identifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				securityKeyIdentifierClause2
			});
			if (this.signatureConfirmationsToSend != null && this.signatureConfirmationsToSend.Count > 0)
			{
				ISecurityElement[] array = this.CreateSignatureConfirmationElements(this.signatureConfirmationsToSend);
				ISecurityElement[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					SendSecurityHeaderElement sendSecurityHeaderElement = new SendSecurityHeaderElement(array2[i].Id, array2[i]);
					sendSecurityHeaderElement.MarkedForEncryption = this.signatureConfirmationsToSend.IsMarkedForEncryption;
					this.elementContainer.AddSignatureConfirmation(sendSecurityHeaderElement);
				}
			}
			bool generateTargettablePrimarySignature = this.endorsingTokenParameters != null || this.signedEndorsingTokenParameters != null;
			this.StartPrimarySignatureCore(securityToken, identifier, this.signatureParts, generateTargettablePrimarySignature);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x000535B8 File Offset: 0x000517B8
		private void CompleteSignature()
		{
			ISignatureValueSecurityElement signatureValueSecurityElement = this.CompletePrimarySignatureCore(this.elementContainer.GetSignatureConfirmations(), this.elementContainer.GetSignedEndorsingSupportingTokens(), this.elementContainer.GetSignedSupportingTokens(), this.elementContainer.GetBasicSupportingTokens(), true);
			if (signatureValueSecurityElement == null)
			{
				return;
			}
			this.elementContainer.PrimarySignature = new SendSecurityHeaderElement(signatureValueSecurityElement.Id, signatureValueSecurityElement);
			this.elementContainer.PrimarySignature.MarkedForEncryption = this.encryptSignature;
			this.AddGeneratedSignatureValue(signatureValueSecurityElement.GetSignatureValue(), this.EncryptPrimarySignature);
			this.primarySignatureDone = true;
			this.primarySignatureValue = signatureValueSecurityElement.GetSignatureValue();
		}

		// Token: 0x060015DD RID: 5597
		protected abstract void StartPrimarySignatureCore(SecurityToken token, SecurityKeyIdentifier identifier, MessagePartSpecification signatureParts, bool generateTargettablePrimarySignature);

		// Token: 0x060015DE RID: 5598
		protected abstract ISignatureValueSecurityElement CompletePrimarySignatureCore(SendSecurityHeaderElement[] signatureConfirmations, SecurityToken[] signedEndorsingTokens, SecurityToken[] signedTokens, SendSecurityHeaderElement[] basicTokens, bool isPrimarySignature);

		// Token: 0x060015DF RID: 5599
		protected abstract ISignatureValueSecurityElement CreateSupportingSignature(SecurityToken token, SecurityKeyIdentifier identifier);

		// Token: 0x060015E0 RID: 5600
		protected abstract ISignatureValueSecurityElement CreateSupportingSignature(SecurityToken token, SecurityKeyIdentifier identifier, ISecurityElement primarySignature);

		// Token: 0x060015E1 RID: 5601
		protected abstract void StartEncryptionCore(SecurityToken token, SecurityKeyIdentifier keyIdentifier);

		// Token: 0x060015E2 RID: 5602
		protected abstract ISecurityElement CompleteEncryptionCore(SendSecurityHeaderElement primarySignature, SendSecurityHeaderElement[] basicTokens, SendSecurityHeaderElement[] signatureConfirmations, SendSecurityHeaderElement[] endorsingSignatures);

		// Token: 0x060015E3 RID: 5603 RVA: 0x00053650 File Offset: 0x00051850
		private void SignWithSupportingToken(SecurityToken token, SecurityKeyIdentifierClause identifierClause)
		{
			if (token == null)
			{
				throw TraceUtility.ThrowHelperArgumentNull("token", base.Message);
			}
			if (identifierClause == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
			}
			if (!base.RequireMessageProtection)
			{
				if (this.elementContainer.Timestamp == null)
				{
					throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SigningWithoutPrimarySignatureRequiresTimestamp")), base.Message);
				}
			}
			else
			{
				if (!this.primarySignatureDone)
				{
					throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PrimarySignatureMustBeComputedBeforeSupportingTokenSignatures")), base.Message);
				}
				if (this.elementContainer.PrimarySignature.Item == null)
				{
					throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SupportingTokenSignaturesNotExpected")), base.Message);
				}
			}
			SecurityKeyIdentifier identifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				identifierClause
			});
			ISignatureValueSecurityElement signatureValueSecurityElement;
			if (!base.RequireMessageProtection)
			{
				signatureValueSecurityElement = this.CreateSupportingSignature(token, identifier);
			}
			else
			{
				signatureValueSecurityElement = this.CreateSupportingSignature(token, identifier, this.elementContainer.PrimarySignature.Item);
			}
			this.AddGeneratedSignatureValue(signatureValueSecurityElement.GetSignatureValue(), this.encryptSignature);
			SendSecurityHeaderElement sendSecurityHeaderElement = new SendSecurityHeaderElement(signatureValueSecurityElement.Id, signatureValueSecurityElement);
			sendSecurityHeaderElement.MarkedForEncryption = this.encryptSignature;
			this.elementContainer.AddEndorsingSignature(sendSecurityHeaderElement);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00053784 File Offset: 0x00051984
		private void SignWithSupportingTokens()
		{
			SecurityToken[] endorsingSupportingTokens = this.elementContainer.GetEndorsingSupportingTokens();
			if (endorsingSupportingTokens != null)
			{
				for (int i = 0; i < endorsingSupportingTokens.Length; i++)
				{
					SecurityToken securityToken = endorsingSupportingTokens[i];
					SecurityKeyIdentifierClause securityKeyIdentifierClause = this.endorsingTokenParameters[i].CreateKeyIdentifierClause(securityToken, this.GetTokenReferenceStyle(this.endorsingTokenParameters[i]));
					if (securityKeyIdentifierClause == null)
					{
						throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
					}
					SecurityToken token;
					SecurityKeyIdentifierClause identifierClause;
					if (this.endorsingTokenParameters[i].RequireDerivedKeys && !this.endorsingTokenParameters[i].HasAsymmetricKey)
					{
						string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(base.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
						DerivedKeySecurityToken derivedKeySecurityToken = new DerivedKeySecurityToken(-1, 0, base.AlgorithmSuite.GetSignatureKeyDerivationLength(securityToken, base.StandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, securityToken, securityKeyIdentifierClause, keyDerivationAlgorithm, this.GenerateId());
						token = derivedKeySecurityToken;
						identifierClause = new LocalIdKeyIdentifierClause(derivedKeySecurityToken.Id, derivedKeySecurityToken.GetType());
						this.elementContainer.AddEndorsingDerivedSupportingToken(derivedKeySecurityToken);
					}
					else
					{
						token = securityToken;
						identifierClause = securityKeyIdentifierClause;
					}
					this.SignWithSupportingToken(token, identifierClause);
				}
			}
			SecurityToken[] signedEndorsingSupportingTokens = this.elementContainer.GetSignedEndorsingSupportingTokens();
			if (signedEndorsingSupportingTokens != null)
			{
				for (int j = 0; j < signedEndorsingSupportingTokens.Length; j++)
				{
					SecurityToken securityToken2 = signedEndorsingSupportingTokens[j];
					SecurityKeyIdentifierClause securityKeyIdentifierClause2 = this.signedEndorsingTokenParameters[j].CreateKeyIdentifierClause(securityToken2, this.GetTokenReferenceStyle(this.signedEndorsingTokenParameters[j]));
					if (securityKeyIdentifierClause2 == null)
					{
						throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
					}
					SecurityToken token2;
					SecurityKeyIdentifierClause identifierClause2;
					if (this.signedEndorsingTokenParameters[j].RequireDerivedKeys && !this.signedEndorsingTokenParameters[j].HasAsymmetricKey)
					{
						string keyDerivationAlgorithm2 = SecurityUtils.GetKeyDerivationAlgorithm(base.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
						DerivedKeySecurityToken derivedKeySecurityToken2 = new DerivedKeySecurityToken(-1, 0, base.AlgorithmSuite.GetSignatureKeyDerivationLength(securityToken2, base.StandardsManager.MessageSecurityVersion.SecureConversationVersion), null, 16, securityToken2, securityKeyIdentifierClause2, keyDerivationAlgorithm2, this.GenerateId());
						token2 = derivedKeySecurityToken2;
						identifierClause2 = new LocalIdKeyIdentifierClause(derivedKeySecurityToken2.Id, derivedKeySecurityToken2.GetType());
						this.elementContainer.AddSignedEndorsingDerivedSupportingToken(derivedKeySecurityToken2);
					}
					else
					{
						token2 = securityToken2;
						identifierClause2 = securityKeyIdentifierClause2;
					}
					this.SignWithSupportingToken(token2, identifierClause2);
				}
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000539D8 File Offset: 0x00051BD8
		protected bool ShouldUseStrTransformForToken(SecurityToken securityToken, int position, SecurityTokenAttachmentMode mode, out SecurityKeyIdentifierClause keyIdentifierClause)
		{
			keyIdentifierClause = null;
			IssuedSecurityTokenParameters issuedSecurityTokenParameters;
			switch (mode)
			{
			case SecurityTokenAttachmentMode.Signed:
				issuedSecurityTokenParameters = (this.signedTokenParameters[position] as IssuedSecurityTokenParameters);
				goto IL_5C;
			case SecurityTokenAttachmentMode.SignedEndorsing:
				issuedSecurityTokenParameters = (this.signedEndorsingTokenParameters[position] as IssuedSecurityTokenParameters);
				goto IL_5C;
			case SecurityTokenAttachmentMode.SignedEncrypted:
				issuedSecurityTokenParameters = (this.basicSupportingTokenParameters[position] as IssuedSecurityTokenParameters);
				goto IL_5C;
			}
			return false;
			IL_5C:
			if (issuedSecurityTokenParameters == null || !issuedSecurityTokenParameters.UseStrTransform)
			{
				return false;
			}
			keyIdentifierClause = issuedSecurityTokenParameters.CreateKeyIdentifierClause(securityToken, this.GetTokenReferenceStyle(issuedSecurityTokenParameters));
			if (keyIdentifierClause == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
			}
			return true;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00053A80 File Offset: 0x00051C80
		XmlDictionaryString IMessageHeaderWithSharedNamespace.SharedNamespace
		{
			get
			{
				return XD.UtilityDictionary.Namespace;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00053A8C File Offset: 0x00051C8C
		XmlDictionaryString IMessageHeaderWithSharedNamespace.SharedPrefix
		{
			get
			{
				return XD.UtilityDictionary.Prefix;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00053A98 File Offset: 0x00051C98
		private void AddGeneratedSignatureValue(byte[] signatureValue, bool wasEncrypted)
		{
			if (base.MaintainSignatureConfirmationState && this.signatureConfirmationsToSend == null)
			{
				if (this.signatureValuesGenerated == null)
				{
					this.signatureValuesGenerated = new SignatureConfirmations();
				}
				this.signatureValuesGenerated.AddConfirmation(signatureValue, wasEncrypted);
			}
		}

		// Token: 0x04001B75 RID: 7029
		private bool basicTokenEncrypted;

		// Token: 0x04001B76 RID: 7030
		private SendSecurityHeaderElementContainer elementContainer;

		// Token: 0x04001B77 RID: 7031
		private bool primarySignatureDone;

		// Token: 0x04001B78 RID: 7032
		private bool encryptSignature;

		// Token: 0x04001B79 RID: 7033
		private SignatureConfirmations signatureValuesGenerated;

		// Token: 0x04001B7A RID: 7034
		private SignatureConfirmations signatureConfirmationsToSend;

		// Token: 0x04001B7B RID: 7035
		private int idCounter;

		// Token: 0x04001B7C RID: 7036
		private string idPrefix;

		// Token: 0x04001B7D RID: 7037
		private bool hasSignedTokens;

		// Token: 0x04001B7E RID: 7038
		private bool hasEncryptedTokens;

		// Token: 0x04001B7F RID: 7039
		private MessagePartSpecification signatureParts;

		// Token: 0x04001B80 RID: 7040
		private MessagePartSpecification encryptionParts;

		// Token: 0x04001B81 RID: 7041
		private SecurityTokenParameters signingTokenParameters;

		// Token: 0x04001B82 RID: 7042
		private SecurityTokenParameters encryptingTokenParameters;

		// Token: 0x04001B83 RID: 7043
		private List<SecurityToken> basicTokens;

		// Token: 0x04001B84 RID: 7044
		private List<SecurityTokenParameters> basicSupportingTokenParameters;

		// Token: 0x04001B85 RID: 7045
		private List<SecurityTokenParameters> endorsingTokenParameters;

		// Token: 0x04001B86 RID: 7046
		private List<SecurityTokenParameters> signedEndorsingTokenParameters;

		// Token: 0x04001B87 RID: 7047
		private List<SecurityTokenParameters> signedTokenParameters;

		// Token: 0x04001B88 RID: 7048
		private SecurityToken encryptingToken;

		// Token: 0x04001B89 RID: 7049
		private bool skipKeyInfoForEncryption;

		// Token: 0x04001B8A RID: 7050
		private byte[] primarySignatureValue;

		// Token: 0x04001B8B RID: 7051
		private bool shouldProtectTokens;

		// Token: 0x04001B8C RID: 7052
		private BufferManager bufferManager;

		// Token: 0x04001B8D RID: 7053
		private bool shouldSignToHeader;

		// Token: 0x04001B8E RID: 7054
		private SecurityProtocolCorrelationState correlationState;

		// Token: 0x04001B8F RID: 7055
		private bool signThenEncrypt = true;

		// Token: 0x04001B90 RID: 7056
		private static readonly string[] ids = new string[]
		{
			"_0",
			"_1",
			"_2",
			"_3",
			"_4",
			"_5",
			"_6",
			"_7",
			"_8",
			"_9"
		};
	}
}
