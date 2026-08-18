using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A7 RID: 679
	internal abstract class ReceiveSecurityHeader : SecurityHeader
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x0004CC7C File Offset: 0x0004AE7C
		protected ReceiveSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, int headerIndex, MessageDirection direction) : base(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, direction)
		{
			this.headerIndex = headerIndex;
			this.elementManager = new ReceiveSecurityHeaderElementManager(this);
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0004CCCF File Offset: 0x0004AECF
		public Collection<SecurityToken> BasicSupportingTokens
		{
			get
			{
				return this.basicTokens;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0004CCD7 File Offset: 0x0004AED7
		public Collection<SecurityToken> SignedSupportingTokens
		{
			get
			{
				return this.signedTokens;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0004CCDF File Offset: 0x0004AEDF
		public Collection<SecurityToken> EndorsingSupportingTokens
		{
			get
			{
				return this.endorsingTokens;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0004CCE7 File Offset: 0x0004AEE7
		public ReceiveSecurityHeaderElementManager ElementManager
		{
			get
			{
				return this.elementManager;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0004CCEF File Offset: 0x0004AEEF
		public Collection<SecurityToken> SignedEndorsingSupportingTokens
		{
			get
			{
				return this.signedEndorsingTokens;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004CCF7 File Offset: 0x0004AEF7
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0004CCFF File Offset: 0x0004AEFF
		public SecurityTokenAuthenticator DerivedTokenAuthenticator
		{
			get
			{
				return this.derivedTokenAuthenticator;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.derivedTokenAuthenticator = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0004CD0E File Offset: 0x0004AF0E
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004CD16 File Offset: 0x0004AF16
		public List<SecurityTokenAuthenticator> WrappedKeySecurityTokenAuthenticator
		{
			get
			{
				return this.wrappedKeyAuthenticator;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.wrappedKeyAuthenticator = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0004CD25 File Offset: 0x0004AF25
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0004CD2D File Offset: 0x0004AF2D
		public bool EnforceDerivedKeyRequirement
		{
			get
			{
				return this.enforceDerivedKeyRequirement;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.enforceDerivedKeyRequirement = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004CD3C File Offset: 0x0004AF3C
		public byte[] PrimarySignatureValue
		{
			get
			{
				return this.primarySignatureValue;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0004CD44 File Offset: 0x0004AF44
		public bool EncryptBeforeSignMode
		{
			get
			{
				return this.orderTracker.EncryptBeforeSignMode;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004CD51 File Offset: 0x0004AF51
		public SecurityToken EncryptionToken
		{
			get
			{
				return this.encryptionTracker.Token;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0004CD5E File Offset: 0x0004AF5E
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x0004CD66 File Offset: 0x0004AF66
		public bool ExpectBasicTokens
		{
			get
			{
				return this.expectBasicTokens;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.expectBasicTokens = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0004CD75 File Offset: 0x0004AF75
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x0004CD7D File Offset: 0x0004AF7D
		public bool ReplayDetectionEnabled
		{
			get
			{
				return this.replayDetectionEnabled;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.replayDetectionEnabled = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0004CD8C File Offset: 0x0004AF8C
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x0004CD94 File Offset: 0x0004AF94
		public bool ExpectEncryption
		{
			get
			{
				return this.expectEncryption;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.expectEncryption = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0004CDA3 File Offset: 0x0004AFA3
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x0004CDAB File Offset: 0x0004AFAB
		public bool ExpectSignature
		{
			get
			{
				return this.expectSignature;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.expectSignature = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0004CDBA File Offset: 0x0004AFBA
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x0004CDC2 File Offset: 0x0004AFC2
		public bool ExpectSignatureConfirmation
		{
			get
			{
				return this.expectSignatureConfirmation;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.expectSignatureConfirmation = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0004CDD1 File Offset: 0x0004AFD1
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0004CDD9 File Offset: 0x0004AFD9
		public bool ExpectSignedTokens
		{
			get
			{
				return this.expectSignedTokens;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.expectSignedTokens = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0004CDE8 File Offset: 0x0004AFE8
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0004CDF0 File Offset: 0x0004AFF0
		public bool RequireSignedPrimaryToken
		{
			get
			{
				return this.requireSignedPrimaryToken;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.requireSignedPrimaryToken = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0004CDFF File Offset: 0x0004AFFF
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0004CE07 File Offset: 0x0004B007
		public bool ExpectEndorsingTokens
		{
			get
			{
				return this.expectEndorsingTokens;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.expectEndorsingTokens = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0004CE16 File Offset: 0x0004B016
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x0004CE1E File Offset: 0x0004B01E
		public bool HasAtLeastOneItemInsideSecurityHeaderEncrypted
		{
			get
			{
				return this.hasAtLeastOneItemInsideSecurityHeaderEncrypted;
			}
			set
			{
				this.hasAtLeastOneItemInsideSecurityHeaderEncrypted = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0004CE27 File Offset: 0x0004B027
		public SecurityHeaderTokenResolver PrimaryTokenResolver
		{
			get
			{
				return this.primaryTokenResolver;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0004CE2F File Offset: 0x0004B02F
		public SecurityTokenResolver CombinedUniversalTokenResolver
		{
			get
			{
				return this.combinedUniversalTokenResolver;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0004CE37 File Offset: 0x0004B037
		public SecurityTokenResolver CombinedPrimaryTokenResolver
		{
			get
			{
				return this.combinedPrimaryTokenResolver;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0004CE3F File Offset: 0x0004B03F
		protected EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null && FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity((OperationContext.Current != null) ? OperationContext.Current.IncomingMessage : null);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0004CE7A File Offset: 0x0004B07A
		protected void VerifySignatureEncryption()
		{
			if (this.protectionOrder == MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature && !this.orderTracker.AllSignaturesEncrypted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("PrimarySignatureIsRequiredToBeEncrypted")));
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0004CEAC File Offset: 0x0004B0AC
		internal int HeaderIndex
		{
			get
			{
				return this.headerIndex;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0004CEB4 File Offset: 0x0004B0B4
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0004CEBC File Offset: 0x0004B0BC
		internal long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				this.maxReceivedMessageSize = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0004CECB File Offset: 0x0004B0CB
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0004CED3 File Offset: 0x0004B0D3
		internal XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
			set
			{
				base.ThrowIfProcessingStarted();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.readerQuotas = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0004CEF5 File Offset: 0x0004B0F5
		public override string Name
		{
			get
			{
				return base.StandardsManager.SecurityVersion.HeaderName.Value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004CF0C File Offset: 0x0004B10C
		public override string Namespace
		{
			get
			{
				return base.StandardsManager.SecurityVersion.HeaderNamespace.Value;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0004CF23 File Offset: 0x0004B123
		public Message ProcessedMessage
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0004CF2B File Offset: 0x0004B12B
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x0004CF38 File Offset: 0x0004B138
		public MessagePartSpecification RequiredEncryptionParts
		{
			get
			{
				return this.encryptionTracker.Parts;
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
				this.encryptionTracker.Parts = value;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004CF93 File Offset: 0x0004B193
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0004CFA0 File Offset: 0x0004B1A0
		public MessagePartSpecification RequiredSignatureParts
		{
			get
			{
				return this.signatureTracker.Parts;
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
				this.signatureTracker.Parts = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004CFFB File Offset: 0x0004B1FB
		protected SignatureResourcePool ResourcePool
		{
			get
			{
				if (this.resourcePool == null)
				{
					this.resourcePool = new SignatureResourcePool();
				}
				return this.resourcePool;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0004D016 File Offset: 0x0004B216
		internal SecurityVerifiedMessage SecurityVerifiedMessage
		{
			get
			{
				return this.securityVerifiedMessage;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0004D01E File Offset: 0x0004B21E
		public SecurityToken SignatureToken
		{
			get
			{
				return this.signatureTracker.Token;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0004D02B File Offset: 0x0004B22B
		public Dictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> SecurityTokenAuthorizationPoliciesMapping
		{
			get
			{
				if (this.tokenPoliciesMapping == null)
				{
					this.tokenPoliciesMapping = new Dictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>>();
				}
				return this.tokenPoliciesMapping;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0004D046 File Offset: 0x0004B246
		public SecurityTimestamp Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0004D04E File Offset: 0x0004B24E
		public int MaxDerivedKeyLength
		{
			get
			{
				return this.maxDerivedKeyLength;
			}
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0004D056 File Offset: 0x0004B256
		internal XmlDictionaryReader CreateSecurityHeaderReader()
		{
			return this.securityVerifiedMessage.GetReaderAtSecurityHeader();
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0004D063 File Offset: 0x0004B263
		public SignatureConfirmations GetSentSignatureConfirmations()
		{
			return this.receivedSignatureConfirmations;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0004D06B File Offset: 0x0004B26B
		public void ConfigureSymmetricBindingServerReceiveHeader(SecurityTokenAuthenticator primaryTokenAuthenticator, SecurityTokenParameters primaryTokenParameters, IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators)
		{
			this.primaryTokenAuthenticator = primaryTokenAuthenticator;
			this.primaryTokenParameters = primaryTokenParameters;
			this.supportingTokenAuthenticators = supportingTokenAuthenticators;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0004D082 File Offset: 0x0004B282
		public void ConfigureSymmetricBindingServerReceiveHeader(SecurityToken wrappingToken, SecurityTokenParameters wrappingTokenParameters, IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators)
		{
			this.wrappingToken = wrappingToken;
			this.wrappingTokenParameters = wrappingTokenParameters;
			this.supportingTokenAuthenticators = supportingTokenAuthenticators;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004D099 File Offset: 0x0004B299
		public void ConfigureAsymmetricBindingServerReceiveHeader(SecurityTokenAuthenticator primaryTokenAuthenticator, SecurityTokenParameters primaryTokenParameters, SecurityToken wrappingToken, SecurityTokenParameters wrappingTokenParameters, IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators)
		{
			this.primaryTokenAuthenticator = primaryTokenAuthenticator;
			this.primaryTokenParameters = primaryTokenParameters;
			this.wrappingToken = wrappingToken;
			this.wrappingTokenParameters = wrappingTokenParameters;
			this.supportingTokenAuthenticators = supportingTokenAuthenticators;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004D0C0 File Offset: 0x0004B2C0
		public void ConfigureTransportBindingServerReceiveHeader(IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators)
		{
			this.supportingTokenAuthenticators = supportingTokenAuthenticators;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004D0CC File Offset: 0x0004B2CC
		public void ConfigureAsymmetricBindingClientReceiveHeader(SecurityToken primaryToken, SecurityTokenParameters primaryTokenParameters, SecurityToken encryptionToken, SecurityTokenParameters encryptionTokenParameters, SecurityTokenAuthenticator primaryTokenAuthenticator)
		{
			this.outOfBandPrimaryToken = primaryToken;
			this.primaryTokenParameters = primaryTokenParameters;
			this.primaryTokenAuthenticator = primaryTokenAuthenticator;
			this.allowFirstTokenMismatch = (primaryTokenAuthenticator != null);
			if (encryptionToken != null && !SecurityUtils.HasSymmetricSecurityKey(encryptionToken))
			{
				this.wrappingToken = encryptionToken;
				this.wrappingTokenParameters = encryptionTokenParameters;
				return;
			}
			this.expectedEncryptionToken = encryptionToken;
			this.expectedEncryptionTokenParameters = encryptionTokenParameters;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004D124 File Offset: 0x0004B324
		public void ConfigureSymmetricBindingClientReceiveHeader(SecurityToken primaryToken, SecurityTokenParameters primaryTokenParameters)
		{
			this.outOfBandPrimaryToken = primaryToken;
			this.primaryTokenParameters = primaryTokenParameters;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0004D134 File Offset: 0x0004B334
		public void ConfigureSymmetricBindingClientReceiveHeader(IList<SecurityToken> primaryTokens, SecurityTokenParameters primaryTokenParameters)
		{
			this.outOfBandPrimaryTokenCollection = primaryTokens;
			this.primaryTokenParameters = primaryTokenParameters;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0004D144 File Offset: 0x0004B344
		public void ConfigureOutOfBandTokenResolver(ReadOnlyCollection<SecurityTokenResolver> outOfBandResolvers)
		{
			if (outOfBandResolvers == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("outOfBandResolvers");
			}
			if (outOfBandResolvers.Count == 0)
			{
				return;
			}
			this.outOfBandTokenResolver = outOfBandResolvers;
		}

		// Token: 0x060014C5 RID: 5317
		protected abstract EncryptedData ReadSecurityHeaderEncryptedItem(XmlDictionaryReader reader, bool readXmlreferenceKeyInfoClause);

		// Token: 0x060014C6 RID: 5318
		protected abstract byte[] DecryptSecurityHeaderElement(EncryptedData encryptedData, WrappedKeySecurityToken wrappedKeyToken, out SecurityToken encryptionToken);

		// Token: 0x060014C7 RID: 5319
		protected abstract WrappedKeySecurityToken DecryptWrappedKey(XmlDictionaryReader reader);

		// Token: 0x060014C8 RID: 5320 RVA: 0x0004D169 File Offset: 0x0004B369
		public SignatureConfirmations GetSentSignatureValues()
		{
			return this.receivedSignatureValues;
		}

		// Token: 0x060014C9 RID: 5321
		protected abstract bool IsReaderAtEncryptedKey(XmlDictionaryReader reader);

		// Token: 0x060014CA RID: 5322
		protected abstract bool IsReaderAtEncryptedData(XmlDictionaryReader reader);

		// Token: 0x060014CB RID: 5323
		protected abstract bool IsReaderAtReferenceList(XmlDictionaryReader reader);

		// Token: 0x060014CC RID: 5324
		protected abstract bool IsReaderAtSignature(XmlDictionaryReader reader);

		// Token: 0x060014CD RID: 5325
		protected abstract bool IsReaderAtSecurityTokenReference(XmlDictionaryReader reader);

		// Token: 0x060014CE RID: 5326
		protected abstract void OnDecryptionOfSecurityHeaderItemRequiringReferenceListEntry(string id);

		// Token: 0x060014CF RID: 5327 RVA: 0x0004D174 File Offset: 0x0004B374
		private void MarkHeaderAsUnderstood()
		{
			MessageHeaderInfo headerInfo = base.Message.Headers[this.headerIndex];
			base.Message.Headers.UnderstoodHeaders.Add(headerInfo);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0004D1B0 File Offset: 0x0004B3B0
		protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			base.StandardsManager.SecurityVersion.WriteStartHeader(writer);
			XmlAttributeHolder[] array = this.securityElementAttributes;
			for (int i = 0; i < array.Length; i++)
			{
				writer.WriteAttributeString(array[i].Prefix, array[i].LocalName, array[i].NamespaceUri, array[i].Value);
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0004D21C File Offset: 0x0004B41C
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			XmlDictionaryReader readerAtSecurityHeader = this.GetReaderAtSecurityHeader();
			readerAtSecurityHeader.ReadStartElement();
			for (int i = 0; i < this.ElementManager.Count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				this.ElementManager.GetElementEntry(i, out receiveSecurityHeaderEntry);
				if (receiveSecurityHeaderEntry.encrypted)
				{
					XmlDictionaryReader reader = this.ElementManager.GetReader(i, false);
					writer.WriteNode(reader, false);
					reader.Close();
					readerAtSecurityHeader.Skip();
				}
				else
				{
					writer.WriteNode(readerAtSecurityHeader, false);
				}
			}
			readerAtSecurityHeader.Close();
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0004D298 File Offset: 0x0004B498
		private XmlDictionaryReader GetReaderAtSecurityHeader()
		{
			XmlDictionaryReader readerAtFirstHeader = this.SecurityVerifiedMessage.GetReaderAtFirstHeader();
			for (int i = 0; i < this.HeaderIndex; i++)
			{
				readerAtFirstHeader.Skip();
			}
			return readerAtFirstHeader;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0004D2C9 File Offset: 0x0004B4C9
		private Collection<SecurityToken> EnsureSupportingTokens(ref Collection<SecurityToken> list)
		{
			if (list == null)
			{
				list = new Collection<SecurityToken>();
			}
			return list;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0004D2D8 File Offset: 0x0004B4D8
		private void VerifySupportingToken(TokenTracker tracker)
		{
			if (tracker == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tracker");
			}
			SupportingTokenAuthenticatorSpecification spec = tracker.spec;
			if (tracker.token == null)
			{
				if (spec.IsTokenOptional)
				{
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenNotProvided", new object[]
				{
					spec.TokenParameters,
					spec.SecurityTokenAttachmentMode
				})));
			}
			else
			{
				switch (spec.SecurityTokenAttachmentMode)
				{
				case SecurityTokenAttachmentMode.Signed:
					if (!tracker.IsSigned && base.RequireMessageProtection)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenIsNotSigned", new object[]
						{
							spec.TokenParameters
						})));
					}
					this.EnsureSupportingTokens(ref this.signedTokens).Add(tracker.token);
					return;
				case SecurityTokenAttachmentMode.Endorsing:
					if (!tracker.IsEndorsing)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenIsNotEndorsing", new object[]
						{
							spec.TokenParameters
						})));
					}
					if (this.EnforceDerivedKeyRequirement && spec.TokenParameters.RequireDerivedKeys && !spec.TokenParameters.HasAsymmetricKey && !tracker.IsDerivedFrom)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingSignatureIsNotDerivedFrom", new object[]
						{
							spec.TokenParameters
						})));
					}
					this.EnsureSupportingTokens(ref this.endorsingTokens).Add(tracker.token);
					return;
				case SecurityTokenAttachmentMode.SignedEndorsing:
					if (!tracker.IsSigned && base.RequireMessageProtection)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenIsNotSigned", new object[]
						{
							spec.TokenParameters
						})));
					}
					if (!tracker.IsEndorsing)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenIsNotEndorsing", new object[]
						{
							spec.TokenParameters
						})));
					}
					if (this.EnforceDerivedKeyRequirement && spec.TokenParameters.RequireDerivedKeys && !spec.TokenParameters.HasAsymmetricKey && !tracker.IsDerivedFrom)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingSignatureIsNotDerivedFrom", new object[]
						{
							spec.TokenParameters
						})));
					}
					this.EnsureSupportingTokens(ref this.signedEndorsingTokens).Add(tracker.token);
					return;
				case SecurityTokenAttachmentMode.SignedEncrypted:
					if (!tracker.IsSigned && base.RequireMessageProtection)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenIsNotSigned", new object[]
						{
							spec.TokenParameters
						})));
					}
					if (!tracker.IsEncrypted && base.RequireMessageProtection)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SupportingTokenIsNotEncrypted", new object[]
						{
							spec.TokenParameters
						})));
					}
					this.EnsureSupportingTokens(ref this.basicTokens).Add(tracker.token);
					return;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnknownTokenAttachmentMode", new object[]
					{
						spec.SecurityTokenAttachmentMode
					})));
				}
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004D5E2 File Offset: 0x0004B7E2
		public void SetTimeParameters(NonceCache nonceCache, TimeSpan replayWindow, TimeSpan clockSkew)
		{
			this.nonceCache = nonceCache;
			this.replayWindow = replayWindow;
			this.clockSkew = clockSkew;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004D5FC File Offset: 0x0004B7FC
		public void Process(TimeSpan timeout, ChannelBinding channelBinding, ExtendedProtectionPolicy extendedProtectionPolicy)
		{
			MessageProtectionOrder requiredProtectionOrder = this.protectionOrder;
			bool flag = false;
			if (this.protectionOrder == MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature && (this.RequiredEncryptionParts == null || !this.RequiredEncryptionParts.IsBodyIncluded))
			{
				requiredProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
				flag = true;
			}
			this.channelBinding = channelBinding;
			this.extendedProtectionPolicy = extendedProtectionPolicy;
			this.orderTracker.SetRequiredProtectionOrder(requiredProtectionOrder);
			base.SetProcessingStarted();
			this.timeoutHelper = new TimeoutHelper(timeout);
			base.Message = (this.securityVerifiedMessage = new SecurityVerifiedMessage(base.Message, this));
			XmlDictionaryReader xmlDictionaryReader = this.CreateSecurityHeaderReader();
			xmlDictionaryReader.MoveToStartElement();
			if (xmlDictionaryReader.IsEmptyElement)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SecurityHeaderIsEmpty")), base.Message);
			}
			if (base.RequireMessageProtection)
			{
				this.securityElementAttributes = XmlAttributeHolder.ReadAttributes(xmlDictionaryReader);
			}
			else
			{
				this.securityElementAttributes = XmlAttributeHolder.emptyArray;
			}
			xmlDictionaryReader.ReadStartElement();
			if (this.primaryTokenParameters != null)
			{
				this.primaryTokenTracker = new TokenTracker(null, this.outOfBandPrimaryToken, this.allowFirstTokenMismatch);
			}
			this.universalTokenResolver = new SecurityHeaderTokenResolver(this);
			this.primaryTokenResolver = new SecurityHeaderTokenResolver(this);
			if (this.outOfBandPrimaryToken != null)
			{
				this.universalTokenResolver.Add(this.outOfBandPrimaryToken, SecurityTokenReferenceStyle.External, this.primaryTokenParameters);
				this.primaryTokenResolver.Add(this.outOfBandPrimaryToken, SecurityTokenReferenceStyle.External, this.primaryTokenParameters);
			}
			else if (this.outOfBandPrimaryTokenCollection != null)
			{
				for (int i = 0; i < this.outOfBandPrimaryTokenCollection.Count; i++)
				{
					this.universalTokenResolver.Add(this.outOfBandPrimaryTokenCollection[i], SecurityTokenReferenceStyle.External, this.primaryTokenParameters);
					this.primaryTokenResolver.Add(this.outOfBandPrimaryTokenCollection[i], SecurityTokenReferenceStyle.External, this.primaryTokenParameters);
				}
			}
			if (this.wrappingToken != null)
			{
				this.universalTokenResolver.ExpectedWrapper = this.wrappingToken;
				this.universalTokenResolver.ExpectedWrapperTokenParameters = this.wrappingTokenParameters;
				this.primaryTokenResolver.ExpectedWrapper = this.wrappingToken;
				this.primaryTokenResolver.ExpectedWrapperTokenParameters = this.wrappingTokenParameters;
			}
			else if (this.expectedEncryptionToken != null)
			{
				this.universalTokenResolver.Add(this.expectedEncryptionToken, SecurityTokenReferenceStyle.External, this.expectedEncryptionTokenParameters);
				this.primaryTokenResolver.Add(this.expectedEncryptionToken, SecurityTokenReferenceStyle.External, this.expectedEncryptionTokenParameters);
			}
			if (this.outOfBandTokenResolver == null)
			{
				this.combinedUniversalTokenResolver = this.universalTokenResolver;
				this.combinedPrimaryTokenResolver = this.primaryTokenResolver;
			}
			else
			{
				this.combinedUniversalTokenResolver = new AggregateSecurityHeaderTokenResolver(this.universalTokenResolver, this.outOfBandTokenResolver);
				this.combinedPrimaryTokenResolver = new AggregateSecurityHeaderTokenResolver(this.primaryTokenResolver, this.outOfBandTokenResolver);
			}
			this.allowedAuthenticators = new List<SecurityTokenAuthenticator>();
			if (this.primaryTokenAuthenticator != null)
			{
				this.allowedAuthenticators.Add(this.primaryTokenAuthenticator);
			}
			if (this.DerivedTokenAuthenticator != null)
			{
				this.allowedAuthenticators.Add(this.DerivedTokenAuthenticator);
			}
			this.pendingSupportingTokenAuthenticator = null;
			int num = 0;
			if (this.supportingTokenAuthenticators != null && this.supportingTokenAuthenticators.Count > 0)
			{
				this.supportingTokenTrackers = new List<TokenTracker>(this.supportingTokenAuthenticators.Count);
				for (int j = 0; j < this.supportingTokenAuthenticators.Count; j++)
				{
					SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification = this.supportingTokenAuthenticators[j];
					switch (supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode)
					{
					case SecurityTokenAttachmentMode.Signed:
						this.hasAtLeastOneSupportingTokenExpectedToBeSigned = true;
						break;
					case SecurityTokenAttachmentMode.Endorsing:
						this.hasEndorsingOrSignedEndorsingSupportingTokens = true;
						break;
					case SecurityTokenAttachmentMode.SignedEndorsing:
						this.hasEndorsingOrSignedEndorsingSupportingTokens = true;
						this.hasAtLeastOneSupportingTokenExpectedToBeSigned = true;
						break;
					case SecurityTokenAttachmentMode.SignedEncrypted:
						this.hasAtLeastOneSupportingTokenExpectedToBeSigned = true;
						break;
					}
					if (this.primaryTokenAuthenticator != null && this.primaryTokenAuthenticator.GetType().Equals(supportingTokenAuthenticatorSpecification.TokenAuthenticator.GetType()))
					{
						this.pendingSupportingTokenAuthenticator = supportingTokenAuthenticatorSpecification.TokenAuthenticator;
					}
					else
					{
						this.allowedAuthenticators.Add(supportingTokenAuthenticatorSpecification.TokenAuthenticator);
					}
					if (supportingTokenAuthenticatorSpecification.TokenParameters.RequireDerivedKeys && !supportingTokenAuthenticatorSpecification.TokenParameters.HasAsymmetricKey && (supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing))
					{
						num++;
					}
					this.supportingTokenTrackers.Add(new TokenTracker(supportingTokenAuthenticatorSpecification));
				}
			}
			if (this.DerivedTokenAuthenticator != null)
			{
				int num2 = (base.AlgorithmSuite.DefaultEncryptionKeyDerivationLength >= base.AlgorithmSuite.DefaultSignatureKeyDerivationLength) ? base.AlgorithmSuite.DefaultEncryptionKeyDerivationLength : base.AlgorithmSuite.DefaultSignatureKeyDerivationLength;
				this.maxDerivedKeyLength = num2 / 8;
				this.maxDerivedKeys = (2 + num) * 2;
			}
			SecurityHeaderElementInferenceEngine inferenceEngine = SecurityHeaderElementInferenceEngine.GetInferenceEngine(base.Layout);
			inferenceEngine.ExecuteProcessingPasses(this, xmlDictionaryReader);
			if (base.RequireMessageProtection)
			{
				this.ElementManager.EnsureAllRequiredSecurityHeaderTargetsWereProtected();
				this.ExecuteMessageProtectionPass(this.hasAtLeastOneSupportingTokenExpectedToBeSigned);
				if (this.RequiredSignatureParts != null && this.SignatureToken == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredSignatureMissing")), base.Message);
				}
			}
			this.EnsureDecryptionComplete();
			this.signatureTracker.SetDerivationSourceIfRequired();
			this.encryptionTracker.SetDerivationSourceIfRequired();
			if (this.EncryptionToken != null)
			{
				if (this.wrappingToken != null)
				{
					if (!(this.EncryptionToken is WrappedKeySecurityToken) || ((WrappedKeySecurityToken)this.EncryptionToken).WrappingToken != this.wrappingToken)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("EncryptedKeyWasNotEncryptedWithTheRequiredEncryptingToken", new object[]
						{
							this.wrappingToken
						})));
					}
				}
				else if (this.expectedEncryptionToken != null)
				{
					if (this.EncryptionToken != this.expectedEncryptionToken)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MessageWasNotEncryptedWithTheRequiredEncryptingToken")));
					}
				}
				else if (this.SignatureToken != null && this.EncryptionToken != this.SignatureToken)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SignatureAndEncryptionTokenMismatch", new object[]
					{
						this.SignatureToken,
						this.EncryptionToken
					})));
				}
			}
			if (this.EnforceDerivedKeyRequirement)
			{
				if (this.SignatureToken != null)
				{
					if (this.primaryTokenParameters != null)
					{
						if (this.primaryTokenParameters.RequireDerivedKeys && !this.primaryTokenParameters.HasAsymmetricKey && !this.primaryTokenTracker.IsDerivedFrom)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("PrimarySignatureWasNotSignedByDerivedKey", new object[]
							{
								this.primaryTokenParameters
							})));
						}
					}
					else if (this.wrappingTokenParameters != null && this.wrappingTokenParameters.RequireDerivedKeys && !this.signatureTracker.IsDerivedToken)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("PrimarySignatureWasNotSignedByDerivedWrappedKey", new object[]
						{
							this.wrappingTokenParameters
						})));
					}
				}
				if (this.EncryptionToken != null)
				{
					if (this.wrappingTokenParameters != null)
					{
						if (this.wrappingTokenParameters.RequireDerivedKeys && !this.encryptionTracker.IsDerivedToken)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MessageWasNotEncryptedByDerivedWrappedKey", new object[]
							{
								this.wrappingTokenParameters
							})));
						}
					}
					else if (this.expectedEncryptionTokenParameters != null)
					{
						if (this.expectedEncryptionTokenParameters.RequireDerivedKeys && !this.encryptionTracker.IsDerivedToken)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MessageWasNotEncryptedByDerivedEncryptionToken", new object[]
							{
								this.expectedEncryptionTokenParameters
							})));
						}
					}
					else if (this.primaryTokenParameters != null && !this.primaryTokenParameters.HasAsymmetricKey && this.primaryTokenParameters.RequireDerivedKeys && !this.encryptionTracker.IsDerivedToken)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MessageWasNotEncryptedByDerivedEncryptionToken", new object[]
						{
							this.primaryTokenParameters
						})));
					}
				}
			}
			if (flag && this.BasicSupportingTokens != null && this.BasicSupportingTokens.Count > 0)
			{
				this.VerifySignatureEncryption();
			}
			if (this.supportingTokenTrackers != null)
			{
				for (int k = 0; k < this.supportingTokenTrackers.Count; k++)
				{
					this.VerifySupportingToken(this.supportingTokenTrackers[k]);
				}
			}
			if (this.replayDetectionEnabled)
			{
				if (this.timestamp == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoTimestampAvailableInSecurityHeaderToDoReplayDetection")), base.Message);
				}
				if (this.primarySignatureValue == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoSignatureAvailableInSecurityHeaderToDoReplayDetection")), base.Message);
				}
				ReceiveSecurityHeader.AddNonce(this.nonceCache, this.primarySignatureValue);
				this.timestamp.ValidateFreshness(this.replayWindow, this.clockSkew);
			}
			if (this.ExpectSignatureConfirmation)
			{
				this.ElementManager.VerifySignatureConfirmationWasFound();
			}
			this.MarkHeaderAsUnderstood();
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0004DE49 File Offset: 0x0004C049
		private static void AddNonce(NonceCache cache, byte[] nonce)
		{
			if (!cache.TryAddNonce(nonce))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidOrReplayedNonce"), true));
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0004DE6F File Offset: 0x0004C06F
		private static void CheckNonce(NonceCache cache, byte[] nonce)
		{
			if (cache.CheckNonce(nonce))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidOrReplayedNonce"), true));
			}
		}

		// Token: 0x060014D9 RID: 5337
		protected abstract void EnsureDecryptionComplete();

		// Token: 0x060014DA RID: 5338
		protected abstract void ExecuteMessageProtectionPass(bool hasAtLeastOneSupportingTokenExpectedToBeSigned);

		// Token: 0x060014DB RID: 5339 RVA: 0x0004DE98 File Offset: 0x0004C098
		internal void ExecuteSignatureEncryptionProcessingPass()
		{
			for (int i = 0; i < this.elementManager.Count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				this.elementManager.GetElementEntry(i, out receiveSecurityHeaderEntry);
				switch (receiveSecurityHeaderEntry.elementCategory)
				{
				case ReceiveSecurityHeaderElementCategory.Signature:
					if (receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Primary)
					{
						this.ProcessPrimarySignature((SignedXml)receiveSecurityHeaderEntry.element, receiveSecurityHeaderEntry.encrypted);
					}
					else
					{
						this.ProcessSupportingSignature((SignedXml)receiveSecurityHeaderEntry.element, receiveSecurityHeaderEntry.encrypted);
					}
					break;
				case ReceiveSecurityHeaderElementCategory.ReferenceList:
					this.ProcessReferenceList((ReferenceList)receiveSecurityHeaderEntry.element);
					break;
				case ReceiveSecurityHeaderElementCategory.Token:
				{
					WrappedKeySecurityToken wrappedKeySecurityToken = receiveSecurityHeaderEntry.element as WrappedKeySecurityToken;
					if (wrappedKeySecurityToken != null && wrappedKeySecurityToken.ReferenceList != null)
					{
						this.ProcessReferenceList(wrappedKeySecurityToken.ReferenceList, wrappedKeySecurityToken);
					}
					break;
				}
				}
			}
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0004DF70 File Offset: 0x0004C170
		internal void ExecuteSubheaderDecryptionPass()
		{
			for (int i = 0; i < this.elementManager.Count; i++)
			{
				if (this.elementManager.GetElementCategory(i) == ReceiveSecurityHeaderElementCategory.EncryptedData)
				{
					EncryptedData element = this.elementManager.GetElement<EncryptedData>(i);
					bool flag = false;
					this.ProcessEncryptedData(element, this.timeoutHelper.RemainingTime(), i, false, ref flag);
				}
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004DFC8 File Offset: 0x0004C1C8
		internal void ExecuteReadingPass(XmlDictionaryReader reader)
		{
			int num = 0;
			while (reader.IsStartElement())
			{
				if (this.IsReaderAtSignature(reader))
				{
					this.ReadSignature(reader, -1, null);
				}
				else if (this.IsReaderAtReferenceList(reader))
				{
					this.ReadReferenceList(reader);
				}
				else if (base.StandardsManager.WSUtilitySpecificationVersion.IsReaderAtTimestamp(reader))
				{
					this.ReadTimestamp(reader);
				}
				else if (this.IsReaderAtEncryptedKey(reader))
				{
					this.ReadEncryptedKey(reader, false);
				}
				else if (this.IsReaderAtEncryptedData(reader))
				{
					this.ReadEncryptedData(reader);
				}
				else if (base.StandardsManager.SecurityVersion.IsReaderAtSignatureConfirmation(reader))
				{
					this.ReadSignatureConfirmation(reader, -1, null);
				}
				else if (this.IsReaderAtSecurityTokenReference(reader))
				{
					this.ReadSecurityTokenReference(reader);
				}
				else
				{
					this.ReadToken(reader, -1, null, null, null, this.timeoutHelper.RemainingTime());
				}
				num++;
			}
			reader.ReadEndElement();
			reader.Close();
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
		internal void ExecuteFullPass(XmlDictionaryReader reader)
		{
			bool flag = !base.RequireMessageProtection;
			int num = 0;
			while (reader.IsStartElement())
			{
				if (this.IsReaderAtSignature(reader))
				{
					SignedXml signedXml = this.ReadSignature(reader, -1, null);
					if (flag)
					{
						this.elementManager.SetBindingMode(num, ReceiveSecurityHeaderBindingModes.Endorsing);
						this.ProcessSupportingSignature(signedXml, false);
					}
					else
					{
						flag = true;
						this.elementManager.SetBindingMode(num, ReceiveSecurityHeaderBindingModes.Primary);
						this.ProcessPrimarySignature(signedXml, false);
					}
				}
				else if (this.IsReaderAtReferenceList(reader))
				{
					ReferenceList referenceList = this.ReadReferenceList(reader);
					this.ProcessReferenceList(referenceList);
				}
				else if (base.StandardsManager.WSUtilitySpecificationVersion.IsReaderAtTimestamp(reader))
				{
					this.ReadTimestamp(reader);
				}
				else if (this.IsReaderAtEncryptedKey(reader))
				{
					this.ReadEncryptedKey(reader, true);
				}
				else if (this.IsReaderAtEncryptedData(reader))
				{
					EncryptedData encryptedData = this.ReadEncryptedData(reader);
					this.ProcessEncryptedData(encryptedData, this.timeoutHelper.RemainingTime(), num, true, ref flag);
				}
				else if (base.StandardsManager.SecurityVersion.IsReaderAtSignatureConfirmation(reader))
				{
					this.ReadSignatureConfirmation(reader, -1, null);
				}
				else if (this.IsReaderAtSecurityTokenReference(reader))
				{
					this.ReadSecurityTokenReference(reader);
				}
				else
				{
					this.ReadToken(reader, -1, null, null, null, this.timeoutHelper.RemainingTime());
				}
				num++;
			}
			reader.ReadEndElement();
			reader.Close();
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0004E1F8 File Offset: 0x0004C3F8
		internal void EnsureDerivedKeyLimitNotReached()
		{
			this.numDerivedKeys++;
			if (this.numDerivedKeys > this.maxDerivedKeys)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("DerivedKeyLimitExceeded", new object[]
				{
					this.maxDerivedKeys
				})));
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0004E250 File Offset: 0x0004C450
		internal void ExecuteDerivedKeyTokenStubPass(bool isFinalPass)
		{
			for (int i = 0; i < this.elementManager.Count; i++)
			{
				if (this.elementManager.GetElementCategory(i) == ReceiveSecurityHeaderElementCategory.Token)
				{
					DerivedKeySecurityTokenStub derivedKeySecurityTokenStub = this.elementManager.GetElement(i) as DerivedKeySecurityTokenStub;
					if (derivedKeySecurityTokenStub != null)
					{
						SecurityToken securityToken = null;
						this.universalTokenResolver.TryResolveToken(derivedKeySecurityTokenStub.TokenToDeriveIdentifier, out securityToken);
						if (securityToken != null)
						{
							this.EnsureDerivedKeyLimitNotReached();
							DerivedKeySecurityToken derivedKeySecurityToken = derivedKeySecurityTokenStub.CreateToken(securityToken, this.maxDerivedKeyLength);
							this.elementManager.SetElement(i, derivedKeySecurityToken);
							this.AddDerivedKeyTokenToResolvers(derivedKeySecurityToken);
						}
						else if (isFinalPass)
						{
							throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoClauseInDerivedKeyToken", new object[]
							{
								derivedKeySecurityTokenStub.TokenToDeriveIdentifier
							})), base.Message);
						}
					}
				}
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0004E310 File Offset: 0x0004C510
		private SecurityToken GetRootToken(SecurityToken token)
		{
			if (token is DerivedKeySecurityToken)
			{
				return ((DerivedKeySecurityToken)token).TokenToDerive;
			}
			return token;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0004E327 File Offset: 0x0004C527
		private void RecordEncryptionTokenAndRemoveReferenceListEntry(string id, SecurityToken encryptionToken)
		{
			if (id == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MissingIdInEncryptedElement")), base.Message);
			}
			this.OnDecryptionOfSecurityHeaderItemRequiringReferenceListEntry(id);
			this.RecordEncryptionToken(encryptionToken);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0004E358 File Offset: 0x0004C558
		private EncryptedData ReadEncryptedData(XmlDictionaryReader reader)
		{
			EncryptedData encryptedData = this.ReadSecurityHeaderEncryptedItem(reader, base.MessageDirection == MessageDirection.Output);
			this.elementManager.AppendEncryptedData(encryptedData);
			return encryptedData;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0004E383 File Offset: 0x0004C583
		internal XmlDictionaryReader CreateDecryptedReader(byte[] decryptedBuffer)
		{
			return ContextImportHelper.CreateSplicedReader(decryptedBuffer, this.SecurityVerifiedMessage.GetEnvelopeAttributes(), this.SecurityVerifiedMessage.GetHeaderAttributes(), this.securityElementAttributes, this.ReaderQuotas);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004E3B0 File Offset: 0x0004C5B0
		private void ProcessEncryptedData(EncryptedData encryptedData, TimeSpan timeout, int position, bool eagerMode, ref bool primarySignatureFound)
		{
			if (TD.EncryptedDataProcessingStartIsEnabled())
			{
				TD.EncryptedDataProcessingStart(this.EventTraceActivity);
			}
			string id = encryptedData.Id;
			SecurityToken encryptionToken;
			byte[] decryptedBuffer = this.DecryptSecurityHeaderElement(encryptedData, this.wrappedKeyToken, out encryptionToken);
			XmlDictionaryReader reader = this.CreateDecryptedReader(decryptedBuffer);
			if (this.IsReaderAtSignature(reader))
			{
				this.RecordEncryptionTokenAndRemoveReferenceListEntry(id, encryptionToken);
				SignedXml signedXml = this.ReadSignature(reader, position, decryptedBuffer);
				if (eagerMode)
				{
					if (primarySignatureFound)
					{
						this.elementManager.SetBindingMode(position, ReceiveSecurityHeaderBindingModes.Endorsing);
						this.ProcessSupportingSignature(signedXml, true);
					}
					else
					{
						primarySignatureFound = true;
						this.elementManager.SetBindingMode(position, ReceiveSecurityHeaderBindingModes.Primary);
						this.ProcessPrimarySignature(signedXml, true);
					}
				}
			}
			else if (base.StandardsManager.SecurityVersion.IsReaderAtSignatureConfirmation(reader))
			{
				this.RecordEncryptionTokenAndRemoveReferenceListEntry(id, encryptionToken);
				this.ReadSignatureConfirmation(reader, position, decryptedBuffer);
			}
			else if (this.IsReaderAtEncryptedData(reader))
			{
				EncryptedData encryptedData2 = this.ReadSecurityHeaderEncryptedItem(reader, false);
				SecurityToken securityToken;
				byte[] decryptedBuffer2 = this.DecryptSecurityHeaderElement(encryptedData2, this.wrappedKeyToken, out securityToken);
				XmlDictionaryReader reader2 = this.CreateDecryptedReader(decryptedBuffer2);
				this.ReadToken(reader2, position, decryptedBuffer2, encryptionToken, id, timeout);
				ReceiveSecurityHeaderEntry element;
				this.ElementManager.GetElementEntry(position, out element);
				if (this.EncryptBeforeSignMode)
				{
					element.encryptedFormId = encryptedData.Id;
					element.encryptedFormWsuId = encryptedData.WsuId;
				}
				else
				{
					element.encryptedFormId = encryptedData2.Id;
					element.encryptedFormWsuId = encryptedData2.WsuId;
				}
				element.decryptedBuffer = decryptedBuffer;
				element.doubleEncrypted = true;
				this.ElementManager.ReplaceHeaderEntry(position, element);
			}
			else
			{
				this.ReadToken(reader, position, decryptedBuffer, encryptionToken, id, timeout);
			}
			if (TD.EncryptedDataProcessingSuccessIsEnabled())
			{
				TD.EncryptedDataProcessingSuccess(this.EventTraceActivity);
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004E548 File Offset: 0x0004C748
		private void ReadEncryptedKey(XmlDictionaryReader reader, bool processReferenceListIfPresent)
		{
			this.orderTracker.OnEncryptedKey();
			WrappedKeySecurityToken wrappedKeySecurityToken = this.DecryptWrappedKey(reader);
			if (wrappedKeySecurityToken.WrappingToken != this.wrappingToken)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("EncryptedKeyWasNotEncryptedWithTheRequiredEncryptingToken", new object[]
				{
					this.wrappingToken
				})));
			}
			this.universalTokenResolver.Add(wrappedKeySecurityToken);
			this.primaryTokenResolver.Add(wrappedKeySecurityToken);
			if (wrappedKeySecurityToken.ReferenceList != null)
			{
				if (!base.EncryptedKeyContainsReferenceList)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedKeyWithReferenceListNotAllowed")));
				}
				if (!this.ExpectEncryption)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptionNotExpected")), base.Message);
				}
				if (processReferenceListIfPresent)
				{
					this.ProcessReferenceList(wrappedKeySecurityToken.ReferenceList, wrappedKeySecurityToken);
				}
				this.wrappedKeyToken = wrappedKeySecurityToken;
			}
			this.elementManager.AppendToken(wrappedKeySecurityToken, ReceiveSecurityHeaderBindingModes.Primary, null);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0004E62C File Offset: 0x0004C82C
		private ReferenceList ReadReferenceList(XmlDictionaryReader reader)
		{
			if (!this.ExpectEncryption)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptionNotExpected")), base.Message);
			}
			ReferenceList referenceList = this.ReadReferenceListCore(reader);
			this.elementManager.AppendReferenceList(referenceList);
			return referenceList;
		}

		// Token: 0x060014E8 RID: 5352
		protected abstract ReferenceList ReadReferenceListCore(XmlDictionaryReader reader);

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004E671 File Offset: 0x0004C871
		private void ProcessReferenceList(ReferenceList referenceList)
		{
			this.ProcessReferenceList(referenceList, null);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0004E67B File Offset: 0x0004C87B
		private void ProcessReferenceList(ReferenceList referenceList, WrappedKeySecurityToken wrappedKeyToken)
		{
			this.orderTracker.OnProcessReferenceList();
			this.ProcessReferenceListCore(referenceList, wrappedKeyToken);
		}

		// Token: 0x060014EB RID: 5355
		protected abstract void ProcessReferenceListCore(ReferenceList referenceList, WrappedKeySecurityToken wrappedKeyToken);

		// Token: 0x060014EC RID: 5356 RVA: 0x0004E690 File Offset: 0x0004C890
		private SignedXml ReadSignature(XmlDictionaryReader reader, int position, byte[] decryptedBuffer)
		{
			if (!this.ExpectSignature)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SignatureNotExpected")), base.Message);
			}
			SignedXml signedXml = this.ReadSignatureCore(reader);
			signedXml.Signature.SignedInfo.ReaderProvider = this.ElementManager;
			int num;
			if (decryptedBuffer == null)
			{
				this.elementManager.AppendSignature(signedXml);
				num = this.elementManager.Count - 1;
			}
			else
			{
				this.elementManager.SetSignatureAfterDecryption(position, signedXml, decryptedBuffer);
				num = position;
			}
			signedXml.Signature.SignedInfo.SignatureReaderProviderCallbackContext = num;
			return signedXml;
		}

		// Token: 0x060014ED RID: 5357
		protected abstract void ReadSecurityTokenReference(XmlDictionaryReader reader);

		// Token: 0x060014EE RID: 5358 RVA: 0x0004E724 File Offset: 0x0004C924
		private void ProcessPrimarySignature(SignedXml signedXml, bool isFromDecryptedSource)
		{
			this.orderTracker.OnProcessSignature(isFromDecryptedSource);
			this.primarySignatureValue = signedXml.GetSignatureValue();
			if (this.replayDetectionEnabled)
			{
				ReceiveSecurityHeader.CheckNonce(this.nonceCache, this.primarySignatureValue);
			}
			SecurityToken securityToken = this.VerifySignature(signedXml, true, this.primaryTokenResolver, null, null);
			SecurityToken rootToken = this.GetRootToken(securityToken);
			bool isDerivedFrom = securityToken is DerivedKeySecurityToken;
			if (this.primaryTokenTracker != null)
			{
				this.primaryTokenTracker.RecordToken(rootToken);
				this.primaryTokenTracker.IsDerivedFrom = isDerivedFrom;
			}
			this.AddIncomingSignatureValue(signedXml.GetSignatureValue(), isFromDecryptedSource);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004E7B4 File Offset: 0x0004C9B4
		private void ReadSignatureConfirmation(XmlDictionaryReader reader, int position, byte[] decryptedBuffer)
		{
			if (!this.ExpectSignatureConfirmation)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SignatureConfirmationsNotExpected")), base.Message);
			}
			if (this.orderTracker.PrimarySignatureDone)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SignatureConfirmationsOccursAfterPrimarySignature")), base.Message);
			}
			ISignatureValueSecurityElement signatureValueSecurityElement = base.StandardsManager.SecurityVersion.ReadSignatureConfirmation(reader);
			if (decryptedBuffer == null)
			{
				this.AddIncomingSignatureConfirmation(signatureValueSecurityElement.GetSignatureValue(), false);
				this.elementManager.AppendSignatureConfirmation(signatureValueSecurityElement);
				return;
			}
			this.AddIncomingSignatureConfirmation(signatureValueSecurityElement.GetSignatureValue(), true);
			this.elementManager.SetSignatureConfirmationAfterDecryption(position, signatureValueSecurityElement, decryptedBuffer);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0004E858 File Offset: 0x0004CA58
		private TokenTracker GetSupportingTokenTracker(SecurityToken token)
		{
			if (this.supportingTokenTrackers == null)
			{
				return null;
			}
			for (int i = 0; i < this.supportingTokenTrackers.Count; i++)
			{
				if (this.supportingTokenTrackers[i].token == token)
				{
					return this.supportingTokenTrackers[i];
				}
			}
			return null;
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0004E8A8 File Offset: 0x0004CAA8
		protected TokenTracker GetSupportingTokenTracker(SecurityTokenAuthenticator tokenAuthenticator, out SupportingTokenAuthenticatorSpecification spec)
		{
			spec = null;
			if (this.supportingTokenAuthenticators == null)
			{
				return null;
			}
			for (int i = 0; i < this.supportingTokenAuthenticators.Count; i++)
			{
				if (this.supportingTokenAuthenticators[i].TokenAuthenticator == tokenAuthenticator)
				{
					spec = this.supportingTokenAuthenticators[i];
					return this.supportingTokenTrackers[i];
				}
			}
			return null;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0004E908 File Offset: 0x0004CB08
		protected TAuthenticator FindAllowedAuthenticator<TAuthenticator>(bool removeIfPresent) where TAuthenticator : SecurityTokenAuthenticator
		{
			if (this.allowedAuthenticators == null)
			{
				return default(TAuthenticator);
			}
			for (int i = 0; i < this.allowedAuthenticators.Count; i++)
			{
				if (this.allowedAuthenticators[i] is TAuthenticator)
				{
					TAuthenticator result = (TAuthenticator)((object)this.allowedAuthenticators[i]);
					if (removeIfPresent)
					{
						this.allowedAuthenticators.RemoveAt(i);
					}
					return result;
				}
			}
			return default(TAuthenticator);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004E97C File Offset: 0x0004CB7C
		private void ProcessSupportingSignature(SignedXml signedXml, bool isFromDecryptedSource)
		{
			if (!this.ExpectEndorsingTokens)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SupportingTokenSignaturesNotExpected")), base.Message);
			}
			XmlDictionaryReader xmlDictionaryReader;
			string id;
			object signatureTarget;
			if (!base.RequireMessageProtection)
			{
				if (this.timestamp == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SigningWithoutPrimarySignatureRequiresTimestamp")), base.Message);
				}
				xmlDictionaryReader = null;
				id = this.timestamp.Id;
				signatureTarget = null;
			}
			else
			{
				this.elementManager.GetPrimarySignature(out xmlDictionaryReader, out id);
				if (xmlDictionaryReader == null)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoPrimarySignatureAvailableForSupportingTokenSignatureVerification")), base.Message);
				}
				signatureTarget = xmlDictionaryReader;
			}
			SecurityToken securityToken = this.VerifySignature(signedXml, false, this.universalTokenResolver, signatureTarget, id);
			if (xmlDictionaryReader != null)
			{
				xmlDictionaryReader.Close();
			}
			if (securityToken == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SignatureVerificationFailed")), base.Message);
			}
			SecurityToken rootToken = this.GetRootToken(securityToken);
			TokenTracker supportingTokenTracker = this.GetSupportingTokenTracker(rootToken);
			if (supportingTokenTracker == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("UnknownSupportingToken", new object[]
				{
					securityToken
				})));
			}
			if (supportingTokenTracker.AlreadyReadEndorsingSignature)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MoreThanOneSupportingSignature", new object[]
				{
					securityToken
				})));
			}
			supportingTokenTracker.IsEndorsing = true;
			supportingTokenTracker.AlreadyReadEndorsingSignature = true;
			supportingTokenTracker.IsDerivedFrom = (securityToken is DerivedKeySecurityToken);
			this.AddIncomingSignatureValue(signedXml.GetSignatureValue(), isFromDecryptedSource);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0004EAE8 File Offset: 0x0004CCE8
		private void ReadTimestamp(XmlDictionaryReader reader)
		{
			if (this.timestamp != null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("DuplicateTimestampInSecurityHeader")), base.Message);
			}
			bool flag = base.RequireMessageProtection || this.hasEndorsingOrSignedEndorsingSupportingTokens;
			string digestAlgorithm = flag ? base.AlgorithmSuite.DefaultDigestAlgorithm : null;
			SignatureResourcePool signatureResourcePool = flag ? this.ResourcePool : null;
			this.timestamp = base.StandardsManager.WSUtilitySpecificationVersion.ReadTimestamp(reader, digestAlgorithm, signatureResourcePool);
			this.timestamp.ValidateRangeAndFreshness(this.replayWindow, this.clockSkew);
			this.elementManager.AppendTimestamp(this.timestamp);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0004EB8C File Offset: 0x0004CD8C
		private bool IsPrimaryToken(SecurityToken token)
		{
			bool flag = token == this.outOfBandPrimaryToken || (this.primaryTokenTracker != null && token == this.primaryTokenTracker.token) || token == this.expectedEncryptionToken || (token is WrappedKeySecurityToken && ((WrappedKeySecurityToken)token).WrappingToken == this.wrappingToken);
			if (!flag && this.outOfBandPrimaryTokenCollection != null)
			{
				for (int i = 0; i < this.outOfBandPrimaryTokenCollection.Count; i++)
				{
					if (this.outOfBandPrimaryTokenCollection[i] == token)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0004EC18 File Offset: 0x0004CE18
		private void ReadToken(XmlDictionaryReader reader, int position, byte[] decryptedBuffer, SecurityToken encryptionToken, string idInEncryptedForm, TimeSpan timeout)
		{
			string localName = reader.LocalName;
			string namespaceURI = reader.NamespaceURI;
			string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null);
			SecurityTokenAuthenticator securityTokenAuthenticator;
			SecurityToken securityToken = this.ReadToken(reader, this.CombinedUniversalTokenResolver, this.allowedAuthenticators, out securityTokenAuthenticator);
			if (securityToken == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCouldNotReadToken", new object[]
				{
					localName,
					namespaceURI,
					attribute
				})), base.Message);
			}
			DerivedKeySecurityToken derivedKeySecurityToken = securityToken as DerivedKeySecurityToken;
			if (derivedKeySecurityToken != null)
			{
				this.EnsureDerivedKeyLimitNotReached();
				derivedKeySecurityToken.InitializeDerivedKey(this.maxDerivedKeyLength);
			}
			if (securityTokenAuthenticator is SspiNegotiationTokenAuthenticator || securityTokenAuthenticator == this.primaryTokenAuthenticator)
			{
				this.allowedAuthenticators.Remove(securityTokenAuthenticator);
			}
			TokenTracker tokenTracker = null;
			ReceiveSecurityHeaderBindingModes mode;
			if (securityTokenAuthenticator == this.primaryTokenAuthenticator)
			{
				this.universalTokenResolver.Add(securityToken, SecurityTokenReferenceStyle.Internal, this.primaryTokenParameters);
				this.primaryTokenResolver.Add(securityToken, SecurityTokenReferenceStyle.Internal, this.primaryTokenParameters);
				if (this.pendingSupportingTokenAuthenticator != null)
				{
					this.allowedAuthenticators.Add(this.pendingSupportingTokenAuthenticator);
					this.pendingSupportingTokenAuthenticator = null;
				}
				this.primaryTokenTracker.RecordToken(securityToken);
				mode = ReceiveSecurityHeaderBindingModes.Primary;
			}
			else if (securityTokenAuthenticator == this.DerivedTokenAuthenticator)
			{
				if (securityToken is DerivedKeySecurityTokenStub)
				{
					if (base.Layout == SecurityHeaderLayout.Strict)
					{
						DerivedKeySecurityTokenStub derivedKeySecurityTokenStub = (DerivedKeySecurityTokenStub)securityToken;
						throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoClauseInDerivedKeyToken", new object[]
						{
							derivedKeySecurityTokenStub.TokenToDeriveIdentifier
						})), base.Message);
					}
				}
				else
				{
					this.AddDerivedKeyTokenToResolvers(securityToken);
				}
				mode = ReceiveSecurityHeaderBindingModes.Unknown;
			}
			else
			{
				SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification;
				tokenTracker = this.GetSupportingTokenTracker(securityTokenAuthenticator, out supportingTokenAuthenticatorSpecification);
				if (tokenTracker == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("UnknownTokenAuthenticatorUsedInTokenProcessing", new object[]
					{
						securityTokenAuthenticator
					})));
				}
				if (tokenTracker.token != null)
				{
					tokenTracker = new TokenTracker(supportingTokenAuthenticatorSpecification);
					this.supportingTokenTrackers.Add(tokenTracker);
				}
				tokenTracker.RecordToken(securityToken);
				if (encryptionToken != null)
				{
					tokenTracker.IsEncrypted = true;
				}
				bool flag;
				bool flag2;
				SecurityTokenAttachmentModeHelper.Categorize(supportingTokenAuthenticatorSpecification.SecurityTokenAttachmentMode, out flag, out flag2, out mode);
				if (flag)
				{
					if (!this.ExpectBasicTokens)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("BasicTokenNotExpected")));
					}
					if (base.RequireMessageProtection && encryptionToken != null)
					{
						this.RecordEncryptionTokenAndRemoveReferenceListEntry(idInEncryptedForm, encryptionToken);
					}
				}
				if (flag2 && !this.ExpectSignedTokens)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SignedSupportingTokenNotExpected")));
				}
				this.universalTokenResolver.Add(securityToken, SecurityTokenReferenceStyle.Internal, supportingTokenAuthenticatorSpecification.TokenParameters);
			}
			if (position == -1)
			{
				this.elementManager.AppendToken(securityToken, mode, tokenTracker);
				return;
			}
			this.elementManager.SetTokenAfterDecryption(position, securityToken, mode, decryptedBuffer, tokenTracker);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0004EEAC File Offset: 0x0004D0AC
		private SecurityToken ReadToken(XmlReader reader, SecurityTokenResolver tokenResolver, IList<SecurityTokenAuthenticator> allowedTokenAuthenticators, out SecurityTokenAuthenticator usedTokenAuthenticator)
		{
			SecurityToken securityToken = base.StandardsManager.SecurityTokenSerializer.ReadToken(reader, tokenResolver);
			if (!(securityToken is DerivedKeySecurityTokenStub))
			{
				for (int i = 0; i < allowedTokenAuthenticators.Count; i++)
				{
					SecurityTokenAuthenticator securityTokenAuthenticator = allowedTokenAuthenticators[i];
					if (securityTokenAuthenticator.CanValidateToken(securityToken))
					{
						ServiceCredentialsSecurityTokenManager.KerberosSecurityTokenAuthenticatorWrapper kerberosSecurityTokenAuthenticatorWrapper = securityTokenAuthenticator as ServiceCredentialsSecurityTokenManager.KerberosSecurityTokenAuthenticatorWrapper;
						ReadOnlyCollection<IAuthorizationPolicy> value;
						if (kerberosSecurityTokenAuthenticatorWrapper != null)
						{
							value = kerberosSecurityTokenAuthenticatorWrapper.ValidateToken(securityToken, this.channelBinding, this.extendedProtectionPolicy);
						}
						else
						{
							value = securityTokenAuthenticator.ValidateToken(securityToken);
						}
						this.SecurityTokenAuthorizationPoliciesMapping.Add(securityToken, value);
						usedTokenAuthenticator = securityTokenAuthenticator;
						return securityToken;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToFindTokenAuthenticator", new object[]
				{
					securityToken.GetType()
				})));
			}
			if (this.DerivedTokenAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToFindTokenAuthenticator", new object[]
				{
					typeof(DerivedKeySecurityToken)
				})));
			}
			usedTokenAuthenticator = this.DerivedTokenAuthenticator;
			return securityToken;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0004EF9C File Offset: 0x0004D19C
		private void AddDerivedKeyTokenToResolvers(SecurityToken token)
		{
			this.universalTokenResolver.Add(token);
			SecurityToken rootToken = this.GetRootToken(token);
			if (this.IsPrimaryToken(rootToken))
			{
				this.primaryTokenResolver.Add(token);
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0004EFD2 File Offset: 0x0004D1D2
		private void AddIncomingSignatureConfirmation(byte[] signatureValue, bool isFromDecryptedSource)
		{
			if (base.MaintainSignatureConfirmationState)
			{
				if (this.receivedSignatureConfirmations == null)
				{
					this.receivedSignatureConfirmations = new SignatureConfirmations();
				}
				this.receivedSignatureConfirmations.AddConfirmation(signatureValue, isFromDecryptedSource);
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0004EFFC File Offset: 0x0004D1FC
		private void AddIncomingSignatureValue(byte[] signatureValue, bool isFromDecryptedSource)
		{
			if (base.MaintainSignatureConfirmationState && !this.ExpectSignatureConfirmation)
			{
				if (this.receivedSignatureValues == null)
				{
					this.receivedSignatureValues = new SignatureConfirmations();
				}
				this.receivedSignatureValues.AddConfirmation(signatureValue, isFromDecryptedSource);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0004F02E File Offset: 0x0004D22E
		protected void RecordEncryptionToken(SecurityToken token)
		{
			this.encryptionTracker.RecordToken(token);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0004F03C File Offset: 0x0004D23C
		protected void RecordSignatureToken(SecurityToken token)
		{
			this.signatureTracker.RecordToken(token);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0004F04A File Offset: 0x0004D24A
		public void SetRequiredProtectionOrder(MessageProtectionOrder protectionOrder)
		{
			base.ThrowIfProcessingStarted();
			this.protectionOrder = protectionOrder;
		}

		// Token: 0x060014FE RID: 5374
		protected abstract SignedXml ReadSignatureCore(XmlDictionaryReader signatureReader);

		// Token: 0x060014FF RID: 5375
		protected abstract SecurityToken VerifySignature(SignedXml signedXml, bool isPrimarySignature, SecurityHeaderTokenResolver resolver, object signatureTarget, string id);

		// Token: 0x06001500 RID: 5376
		protected abstract bool TryDeleteReferenceListEntry(string id);

		// Token: 0x04001AC2 RID: 6850
		private SecurityTokenAuthenticator primaryTokenAuthenticator;

		// Token: 0x04001AC3 RID: 6851
		private bool allowFirstTokenMismatch;

		// Token: 0x04001AC4 RID: 6852
		private SecurityToken outOfBandPrimaryToken;

		// Token: 0x04001AC5 RID: 6853
		private IList<SecurityToken> outOfBandPrimaryTokenCollection;

		// Token: 0x04001AC6 RID: 6854
		private SecurityTokenParameters primaryTokenParameters;

		// Token: 0x04001AC7 RID: 6855
		private TokenTracker primaryTokenTracker;

		// Token: 0x04001AC8 RID: 6856
		private SecurityToken wrappingToken;

		// Token: 0x04001AC9 RID: 6857
		private SecurityTokenParameters wrappingTokenParameters;

		// Token: 0x04001ACA RID: 6858
		private SecurityToken expectedEncryptionToken;

		// Token: 0x04001ACB RID: 6859
		private SecurityTokenParameters expectedEncryptionTokenParameters;

		// Token: 0x04001ACC RID: 6860
		private SecurityTokenAuthenticator derivedTokenAuthenticator;

		// Token: 0x04001ACD RID: 6861
		private IList<SupportingTokenAuthenticatorSpecification> supportingTokenAuthenticators;

		// Token: 0x04001ACE RID: 6862
		private ChannelBinding channelBinding;

		// Token: 0x04001ACF RID: 6863
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x04001AD0 RID: 6864
		private bool expectEncryption = true;

		// Token: 0x04001AD1 RID: 6865
		private bool expectBasicTokens;

		// Token: 0x04001AD2 RID: 6866
		private bool expectSignedTokens;

		// Token: 0x04001AD3 RID: 6867
		private bool expectEndorsingTokens;

		// Token: 0x04001AD4 RID: 6868
		private bool expectSignature = true;

		// Token: 0x04001AD5 RID: 6869
		private bool requireSignedPrimaryToken;

		// Token: 0x04001AD6 RID: 6870
		private bool expectSignatureConfirmation;

		// Token: 0x04001AD7 RID: 6871
		private List<TokenTracker> supportingTokenTrackers;

		// Token: 0x04001AD8 RID: 6872
		private SignatureConfirmations receivedSignatureValues;

		// Token: 0x04001AD9 RID: 6873
		private SignatureConfirmations receivedSignatureConfirmations;

		// Token: 0x04001ADA RID: 6874
		private List<SecurityTokenAuthenticator> allowedAuthenticators;

		// Token: 0x04001ADB RID: 6875
		private SecurityTokenAuthenticator pendingSupportingTokenAuthenticator;

		// Token: 0x04001ADC RID: 6876
		private WrappedKeySecurityToken wrappedKeyToken;

		// Token: 0x04001ADD RID: 6877
		private Collection<SecurityToken> basicTokens;

		// Token: 0x04001ADE RID: 6878
		private Collection<SecurityToken> signedTokens;

		// Token: 0x04001ADF RID: 6879
		private Collection<SecurityToken> endorsingTokens;

		// Token: 0x04001AE0 RID: 6880
		private Collection<SecurityToken> signedEndorsingTokens;

		// Token: 0x04001AE1 RID: 6881
		private Dictionary<SecurityToken, ReadOnlyCollection<IAuthorizationPolicy>> tokenPoliciesMapping;

		// Token: 0x04001AE2 RID: 6882
		private List<SecurityTokenAuthenticator> wrappedKeyAuthenticator;

		// Token: 0x04001AE3 RID: 6883
		private SecurityTimestamp timestamp;

		// Token: 0x04001AE4 RID: 6884
		private SecurityHeaderTokenResolver universalTokenResolver;

		// Token: 0x04001AE5 RID: 6885
		private SecurityHeaderTokenResolver primaryTokenResolver;

		// Token: 0x04001AE6 RID: 6886
		private ReadOnlyCollection<SecurityTokenResolver> outOfBandTokenResolver;

		// Token: 0x04001AE7 RID: 6887
		private SecurityTokenResolver combinedUniversalTokenResolver;

		// Token: 0x04001AE8 RID: 6888
		private SecurityTokenResolver combinedPrimaryTokenResolver;

		// Token: 0x04001AE9 RID: 6889
		private readonly int headerIndex;

		// Token: 0x04001AEA RID: 6890
		private XmlAttributeHolder[] securityElementAttributes;

		// Token: 0x04001AEB RID: 6891
		private ReceiveSecurityHeader.OrderTracker orderTracker;

		// Token: 0x04001AEC RID: 6892
		private ReceiveSecurityHeader.OperationTracker signatureTracker;

		// Token: 0x04001AED RID: 6893
		private ReceiveSecurityHeader.OperationTracker encryptionTracker;

		// Token: 0x04001AEE RID: 6894
		private ReceiveSecurityHeaderElementManager elementManager;

		// Token: 0x04001AEF RID: 6895
		private int maxDerivedKeys;

		// Token: 0x04001AF0 RID: 6896
		private int numDerivedKeys;

		// Token: 0x04001AF1 RID: 6897
		private int maxDerivedKeyLength;

		// Token: 0x04001AF2 RID: 6898
		private bool enforceDerivedKeyRequirement = true;

		// Token: 0x04001AF3 RID: 6899
		private NonceCache nonceCache;

		// Token: 0x04001AF4 RID: 6900
		private TimeSpan replayWindow;

		// Token: 0x04001AF5 RID: 6901
		private TimeSpan clockSkew;

		// Token: 0x04001AF6 RID: 6902
		private byte[] primarySignatureValue;

		// Token: 0x04001AF7 RID: 6903
		private TimeoutHelper timeoutHelper;

		// Token: 0x04001AF8 RID: 6904
		private SecurityVerifiedMessage securityVerifiedMessage;

		// Token: 0x04001AF9 RID: 6905
		private long maxReceivedMessageSize = 65536L;

		// Token: 0x04001AFA RID: 6906
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x04001AFB RID: 6907
		private MessageProtectionOrder protectionOrder;

		// Token: 0x04001AFC RID: 6908
		private bool hasAtLeastOneSupportingTokenExpectedToBeSigned;

		// Token: 0x04001AFD RID: 6909
		private bool hasEndorsingOrSignedEndorsingSupportingTokens;

		// Token: 0x04001AFE RID: 6910
		private SignatureResourcePool resourcePool;

		// Token: 0x04001AFF RID: 6911
		private bool replayDetectionEnabled;

		// Token: 0x04001B00 RID: 6912
		private bool hasAtLeastOneItemInsideSecurityHeaderEncrypted;

		// Token: 0x04001B01 RID: 6913
		private const int AppendPosition = -1;

		// Token: 0x04001B02 RID: 6914
		private EventTraceActivity eventTraceActivity;

		// Token: 0x02000B3C RID: 2876
		private struct OrderTracker
		{
			// Token: 0x17001A4C RID: 6732
			// (get) Token: 0x060070B5 RID: 28853 RVA: 0x001A3DB9 File Offset: 0x001A1FB9
			public bool AllSignaturesEncrypted
			{
				get
				{
					return this.unencryptedSignatureCount == 0;
				}
			}

			// Token: 0x17001A4D RID: 6733
			// (get) Token: 0x060070B6 RID: 28854 RVA: 0x001A3DC4 File Offset: 0x001A1FC4
			public bool EncryptBeforeSignMode
			{
				get
				{
					return this.enforce && this.protectionOrder == MessageProtectionOrder.EncryptBeforeSign;
				}
			}

			// Token: 0x17001A4E RID: 6734
			// (get) Token: 0x060070B7 RID: 28855 RVA: 0x001A3DD9 File Offset: 0x001A1FD9
			public bool EncryptBeforeSignOrderRequirementMet
			{
				get
				{
					return this.state != ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.DecryptVerify && this.state != ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Mixed;
				}
			}

			// Token: 0x17001A4F RID: 6735
			// (get) Token: 0x060070B8 RID: 28856 RVA: 0x001A3DF2 File Offset: 0x001A1FF2
			public bool PrimarySignatureDone
			{
				get
				{
					return this.signatureCount > 0;
				}
			}

			// Token: 0x17001A50 RID: 6736
			// (get) Token: 0x060070B9 RID: 28857 RVA: 0x001A3DFD File Offset: 0x001A1FFD
			public bool SignBeforeEncryptOrderRequirementMet
			{
				get
				{
					return this.state != ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.VerifyDecrypt && this.state != ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Mixed;
				}
			}

			// Token: 0x060070BA RID: 28858 RVA: 0x001A3E18 File Offset: 0x001A2018
			private void EnforceProtectionOrder()
			{
				switch (this.protectionOrder)
				{
				case MessageProtectionOrder.SignBeforeEncrypt:
					break;
				case MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature:
					if (!this.AllSignaturesEncrypted)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("PrimarySignatureIsRequiredToBeEncrypted")));
					}
					break;
				case MessageProtectionOrder.EncryptBeforeSign:
					if (!this.EncryptBeforeSignOrderRequirementMet)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MessageProtectionOrderMismatch", new object[]
						{
							this.protectionOrder
						})));
					}
					return;
				default:
					return;
				}
				if (!this.SignBeforeEncryptOrderRequirementMet)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MessageProtectionOrderMismatch", new object[]
					{
						this.protectionOrder
					})));
				}
			}

			// Token: 0x060070BB RID: 28859 RVA: 0x001A3ED0 File Offset: 0x001A20D0
			public void OnProcessReferenceList()
			{
				if (this.referenceListCount > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("AtMostOneReferenceListIsSupportedWithDefaultPolicyCheck")));
				}
				this.referenceListCount++;
				this.state = ReceiveSecurityHeader.OrderTracker.stateTransitionTableOnDecrypt[(int)this.state];
				if (this.enforce)
				{
					this.EnforceProtectionOrder();
				}
			}

			// Token: 0x060070BC RID: 28860 RVA: 0x001A3F30 File Offset: 0x001A2130
			public void OnProcessSignature(bool isEncrypted)
			{
				if (this.signatureCount > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("AtMostOneSignatureIsSupportedWithDefaultPolicyCheck")));
				}
				this.signatureCount++;
				if (!isEncrypted)
				{
					this.unencryptedSignatureCount++;
				}
				this.state = ReceiveSecurityHeader.OrderTracker.stateTransitionTableOnVerify[(int)this.state];
				if (this.enforce)
				{
					this.EnforceProtectionOrder();
				}
			}

			// Token: 0x060070BD RID: 28861 RVA: 0x001A3FA0 File Offset: 0x001A21A0
			public void OnEncryptedKey()
			{
				if (this.numWrappedKeys == 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("WrappedKeyLimitExceeded", new object[]
					{
						this.numWrappedKeys
					})));
				}
				this.numWrappedKeys++;
			}

			// Token: 0x060070BE RID: 28862 RVA: 0x001A3FF2 File Offset: 0x001A21F2
			public void SetRequiredProtectionOrder(MessageProtectionOrder protectionOrder)
			{
				this.protectionOrder = protectionOrder;
				this.enforce = true;
			}

			// Token: 0x04004011 RID: 16401
			private static readonly ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder[] stateTransitionTableOnDecrypt = new ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder[]
			{
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Decrypt,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.VerifyDecrypt,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Decrypt,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Mixed,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.VerifyDecrypt,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Mixed
			};

			// Token: 0x04004012 RID: 16402
			private static readonly ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder[] stateTransitionTableOnVerify = new ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder[]
			{
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Verify,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Verify,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.DecryptVerify,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.DecryptVerify,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Mixed,
				ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder.Mixed
			};

			// Token: 0x04004013 RID: 16403
			private const int MaxAllowedWrappedKeys = 1;

			// Token: 0x04004014 RID: 16404
			private int referenceListCount;

			// Token: 0x04004015 RID: 16405
			private ReceiveSecurityHeader.OrderTracker.ReceiverProcessingOrder state;

			// Token: 0x04004016 RID: 16406
			private int signatureCount;

			// Token: 0x04004017 RID: 16407
			private int unencryptedSignatureCount;

			// Token: 0x04004018 RID: 16408
			private int numWrappedKeys;

			// Token: 0x04004019 RID: 16409
			private MessageProtectionOrder protectionOrder;

			// Token: 0x0400401A RID: 16410
			private bool enforce;

			// Token: 0x02000EDC RID: 3804
			private enum ReceiverProcessingOrder
			{
				// Token: 0x04004CC2 RID: 19650
				None,
				// Token: 0x04004CC3 RID: 19651
				Verify,
				// Token: 0x04004CC4 RID: 19652
				Decrypt,
				// Token: 0x04004CC5 RID: 19653
				DecryptVerify,
				// Token: 0x04004CC6 RID: 19654
				VerifyDecrypt,
				// Token: 0x04004CC7 RID: 19655
				Mixed
			}
		}

		// Token: 0x02000B3D RID: 2877
		private struct OperationTracker
		{
			// Token: 0x17001A51 RID: 6737
			// (get) Token: 0x060070C0 RID: 28864 RVA: 0x001A4030 File Offset: 0x001A2230
			// (set) Token: 0x060070C1 RID: 28865 RVA: 0x001A4038 File Offset: 0x001A2238
			public MessagePartSpecification Parts
			{
				get
				{
					return this.parts;
				}
				set
				{
					this.parts = value;
				}
			}

			// Token: 0x17001A52 RID: 6738
			// (get) Token: 0x060070C2 RID: 28866 RVA: 0x001A4041 File Offset: 0x001A2241
			public SecurityToken Token
			{
				get
				{
					return this.token;
				}
			}

			// Token: 0x17001A53 RID: 6739
			// (get) Token: 0x060070C3 RID: 28867 RVA: 0x001A4049 File Offset: 0x001A2249
			public bool IsDerivedToken
			{
				get
				{
					return this.isDerivedToken;
				}
			}

			// Token: 0x060070C4 RID: 28868 RVA: 0x001A4051 File Offset: 0x001A2251
			public void RecordToken(SecurityToken token)
			{
				if (this.token == null)
				{
					this.token = token;
					return;
				}
				if (this.token != token)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MismatchInSecurityOperationToken")));
				}
			}

			// Token: 0x060070C5 RID: 28869 RVA: 0x001A4088 File Offset: 0x001A2288
			public void SetDerivationSourceIfRequired()
			{
				DerivedKeySecurityToken derivedKeySecurityToken = this.token as DerivedKeySecurityToken;
				if (derivedKeySecurityToken != null)
				{
					this.token = derivedKeySecurityToken.TokenToDerive;
					this.isDerivedToken = true;
				}
			}

			// Token: 0x0400401B RID: 16411
			private MessagePartSpecification parts;

			// Token: 0x0400401C RID: 16412
			private SecurityToken token;

			// Token: 0x0400401D RID: 16413
			private bool isDerivedToken;
		}
	}
}
