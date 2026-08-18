using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200028F RID: 655
	internal class WSSecurityOneDotZeroReceiveSecurityHeader : ReceiveSecurityHeader
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0004483C File Offset: 0x00042A3C
		public WSSecurityOneDotZeroReceiveSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, int headerIndex, MessageDirection transferDirection) : base(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, headerIndex, transferDirection)
		{
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004485C File Offset: 0x00042A5C
		protected static SymmetricAlgorithm CreateDecryptionAlgorithm(SecurityToken token, string encryptionMethod, SecurityAlgorithmSuite suite)
		{
			if (encryptionMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptionMethodMissingInEncryptedData")));
			}
			suite.EnsureAcceptableEncryptionAlgorithm(encryptionMethod);
			SymmetricSecurityKey securityKey = SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(token);
			if (securityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenCannotCreateSymmetricCrypto", new object[]
				{
					token
				})));
			}
			suite.EnsureAcceptableDecryptionSymmetricKeySize(securityKey, token);
			SymmetricAlgorithm symmetricAlgorithm = securityKey.GetSymmetricAlgorithm(encryptionMethod);
			if (symmetricAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToCreateSymmetricAlgorithmFromToken", new object[]
				{
					encryptionMethod
				})));
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000448F4 File Offset: 0x00042AF4
		private void DecryptBody(XmlDictionaryReader bodyContentReader, SecurityToken token)
		{
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.ShouldReadXmlReferenceKeyInfoClause = (base.MessageDirection == MessageDirection.Output);
			encryptedData.SecurityTokenSerializer = base.StandardsManager.SecurityTokenSerializer;
			encryptedData.ReadFrom(bodyContentReader, base.MaxReceivedMessageSize);
			if (!bodyContentReader.EOF && bodyContentReader.NodeType != XmlNodeType.EndElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("BadEncryptedBody")));
			}
			if (token == null)
			{
				token = WSSecurityOneDotZeroReceiveSecurityHeader.ResolveKeyIdentifier(encryptedData.KeyIdentifier, base.PrimaryTokenResolver, false);
			}
			base.RecordEncryptionToken(token);
			using (SymmetricAlgorithm symmetricAlgorithm = WSSecurityOneDotZeroReceiveSecurityHeader.CreateDecryptionAlgorithm(token, encryptedData.EncryptionMethod, base.AlgorithmSuite))
			{
				encryptedData.SetUpDecryption(symmetricAlgorithm);
				base.SecurityVerifiedMessage.SetDecryptedBody(encryptedData.GetDecryptedBuffer());
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000449C8 File Offset: 0x00042BC8
		protected virtual DecryptedHeader DecryptHeader(XmlDictionaryReader reader, WrappedKeySecurityToken wrappedKeyToken)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("HeaderDecryptionNotSupportedInWsSecurityJan2004")));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000449E4 File Offset: 0x00042BE4
		protected override byte[] DecryptSecurityHeaderElement(EncryptedData encryptedData, WrappedKeySecurityToken wrappedKeyToken, out SecurityToken encryptionToken)
		{
			if (encryptedData.KeyIdentifier != null || wrappedKeyToken == null)
			{
				encryptionToken = WSSecurityOneDotZeroReceiveSecurityHeader.ResolveKeyIdentifier(encryptedData.KeyIdentifier, base.CombinedPrimaryTokenResolver, false);
				if (wrappedKeyToken != null && wrappedKeyToken.ReferenceList != null && encryptedData.HasId && wrappedKeyToken.ReferenceList.ContainsReferredId(encryptedData.Id) && wrappedKeyToken != encryptionToken)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedKeyWasNotEncryptedWithTheRequiredEncryptingToken", new object[]
					{
						wrappedKeyToken
					})));
				}
			}
			else
			{
				encryptionToken = wrappedKeyToken;
			}
			byte[] decryptedBuffer;
			using (SymmetricAlgorithm symmetricAlgorithm = WSSecurityOneDotZeroReceiveSecurityHeader.CreateDecryptionAlgorithm(encryptionToken, encryptedData.EncryptionMethod, base.AlgorithmSuite))
			{
				encryptedData.SetUpDecryption(symmetricAlgorithm);
				decryptedBuffer = encryptedData.GetDecryptedBuffer();
			}
			return decryptedBuffer;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00044AA4 File Offset: 0x00042CA4
		protected override WrappedKeySecurityToken DecryptWrappedKey(XmlDictionaryReader reader)
		{
			if (TD.WrappedKeyDecryptionStartIsEnabled())
			{
				TD.WrappedKeyDecryptionStart(base.EventTraceActivity);
			}
			WrappedKeySecurityToken wrappedKeySecurityToken = (WrappedKeySecurityToken)base.StandardsManager.SecurityTokenSerializer.ReadToken(reader, base.PrimaryTokenResolver);
			base.AlgorithmSuite.EnsureAcceptableKeyWrapAlgorithm(wrappedKeySecurityToken.WrappingAlgorithm, wrappedKeySecurityToken.WrappingSecurityKey is AsymmetricSecurityKey);
			if (TD.WrappedKeyDecryptionSuccessIsEnabled())
			{
				TD.WrappedKeyDecryptionSuccess(base.EventTraceActivity);
			}
			return wrappedKeySecurityToken;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00044B14 File Offset: 0x00042D14
		private bool EnsureDigestValidityIfIdMatches(SignedInfo signedInfo, string id, XmlDictionaryReader reader, bool doSoapAttributeChecks, MessagePartSpecification signatureParts, MessageHeaderInfo info, bool checkForTokensAtHeaders)
		{
			if (signedInfo == null)
			{
				return false;
			}
			if (doSoapAttributeChecks)
			{
				this.VerifySoapAttributeMatchForHeader(info, signatureParts, reader);
			}
			bool flag = false;
			bool flag2 = checkForTokensAtHeaders && base.StandardsManager.SecurityTokenSerializer.CanReadToken(reader);
			try
			{
				flag = signedInfo.EnsureDigestValidityIfIdMatches(id, reader);
			}
			catch (CryptographicException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("FailedSignatureVerification"), innerException));
			}
			if (flag && flag2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SecurityTokenFoundOutsideSecurityHeader", new object[]
				{
					info.Namespace,
					info.Name
				})));
			}
			return flag;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00044BC4 File Offset: 0x00042DC4
		protected override void ExecuteMessageProtectionPass(bool hasAtLeastOneSupportingTokenExpectedToBeSigned)
		{
			SignatureTargetIdManager idManager = base.StandardsManager.IdManager;
			MessagePartSpecification messagePartSpecification = base.RequiredEncryptionParts ?? MessagePartSpecification.NoParts;
			MessagePartSpecification messagePartSpecification2 = base.RequiredSignatureParts ?? MessagePartSpecification.NoParts;
			bool doSoapAttributeChecks = !messagePartSpecification2.IsBodyIncluded;
			bool encryptBeforeSignMode = base.EncryptBeforeSignMode;
			SignedInfo signedInfo = (this.pendingSignature != null) ? this.pendingSignature.Signature.SignedInfo : null;
			SignatureConfirmations sentSignatureConfirmations = base.GetSentSignatureConfirmations();
			if (sentSignatureConfirmations != null && sentSignatureConfirmations.Count > 0 && sentSignatureConfirmations.IsMarkedForEncryption)
			{
				base.VerifySignatureEncryption();
			}
			MessageHeaders headers = base.SecurityVerifiedMessage.Headers;
			XmlDictionaryReader readerAtFirstHeader = base.SecurityVerifiedMessage.GetReaderAtFirstHeader();
			bool flag = false;
			for (int i = 0; i < headers.Count; i++)
			{
				if (readerAtFirstHeader.NodeType != XmlNodeType.Element)
				{
					readerAtFirstHeader.MoveToContent();
				}
				if (i == base.HeaderIndex)
				{
					readerAtFirstHeader.Skip();
				}
				else
				{
					bool flag2 = false;
					string text = idManager.ExtractId(readerAtFirstHeader);
					if (text != null)
					{
						flag2 = this.TryDeleteReferenceListEntry(text);
					}
					if (!flag2 && readerAtFirstHeader.IsStartElement("EncryptedHeader", "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"))
					{
						XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(i);
						readerAtHeader.ReadStartElement("EncryptedHeader", "http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd");
						if (readerAtHeader.IsStartElement(EncryptedData.ElementName, XD.XmlEncryptionDictionary.Namespace))
						{
							string attribute = readerAtHeader.GetAttribute(XD.XmlEncryptionDictionary.Id, null);
							if (attribute != null && this.TryDeleteReferenceListEntry(attribute))
							{
								flag2 = true;
							}
						}
					}
					base.ElementManager.VerifyUniquenessAndSetHeaderId(text, i);
					MessageHeaderInfo messageHeaderInfo = headers[i];
					if (!flag2 && messagePartSpecification.IsHeaderIncluded(messageHeaderInfo.Name, messageHeaderInfo.Namespace))
					{
						base.SecurityVerifiedMessage.OnUnencryptedPart(messageHeaderInfo.Name, messageHeaderInfo.Namespace);
					}
					bool flag3 = (!flag2 || encryptBeforeSignMode) && text != null && this.EnsureDigestValidityIfIdMatches(signedInfo, text, readerAtFirstHeader, doSoapAttributeChecks, messagePartSpecification2, messageHeaderInfo, hasAtLeastOneSupportingTokenExpectedToBeSigned);
					if (flag2)
					{
						XmlDictionaryReader xmlDictionaryReader = flag3 ? headers.GetReaderAtHeader(i) : readerAtFirstHeader;
						DecryptedHeader decryptedHeader = this.DecryptHeader(xmlDictionaryReader, this.pendingDecryptionToken);
						messageHeaderInfo = decryptedHeader;
						text = decryptedHeader.Id;
						base.ElementManager.VerifyUniquenessAndSetDecryptedHeaderId(text, i);
						headers.ReplaceAt(i, decryptedHeader);
						if (xmlDictionaryReader != readerAtFirstHeader)
						{
							xmlDictionaryReader.Close();
						}
						if (!encryptBeforeSignMode && text != null)
						{
							XmlDictionaryReader headerReader = decryptedHeader.GetHeaderReader();
							flag3 = this.EnsureDigestValidityIfIdMatches(signedInfo, text, headerReader, doSoapAttributeChecks, messagePartSpecification2, messageHeaderInfo, hasAtLeastOneSupportingTokenExpectedToBeSigned);
							headerReader.Close();
						}
					}
					if (!flag3 && messagePartSpecification2.IsHeaderIncluded(messageHeaderInfo.Name, messageHeaderInfo.Namespace))
					{
						base.SecurityVerifiedMessage.OnUnsignedPart(messageHeaderInfo.Name, messageHeaderInfo.Namespace);
					}
					if (flag3 && flag2)
					{
						base.VerifySignatureEncryption();
					}
					if (flag2 && !flag3)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("EncryptedHeaderNotSigned", new object[]
						{
							messageHeaderInfo.Name,
							messageHeaderInfo.Namespace
						})));
					}
					if (!flag3 && !flag2)
					{
						readerAtFirstHeader.Skip();
					}
					flag = (flag || flag2);
				}
			}
			readerAtFirstHeader.ReadEndElement();
			if (readerAtFirstHeader.NodeType != XmlNodeType.Element)
			{
				readerAtFirstHeader.MoveToContent();
			}
			string text2 = idManager.ExtractId(readerAtFirstHeader);
			base.ElementManager.VerifyUniquenessAndSetBodyId(text2);
			base.SecurityVerifiedMessage.SetBodyPrefixAndAttributes(readerAtFirstHeader);
			bool flag4 = messagePartSpecification.IsBodyIncluded || this.HasPendingDecryptionItem();
			bool flag5 = (!flag4 || encryptBeforeSignMode) && text2 != null && this.EnsureDigestValidityIfIdMatches(signedInfo, text2, readerAtFirstHeader, false, null, null, false);
			bool flag6;
			if (flag4)
			{
				XmlDictionaryReader xmlDictionaryReader2 = flag5 ? base.SecurityVerifiedMessage.CreateFullBodyReader() : readerAtFirstHeader;
				xmlDictionaryReader2.ReadStartElement();
				string text3 = idManager.ExtractId(xmlDictionaryReader2);
				base.ElementManager.VerifyUniquenessAndSetBodyContentId(text3);
				flag6 = (text3 != null && this.TryDeleteReferenceListEntry(text3));
				if (flag6)
				{
					this.DecryptBody(xmlDictionaryReader2, this.pendingDecryptionToken);
				}
				if (xmlDictionaryReader2 != readerAtFirstHeader)
				{
					xmlDictionaryReader2.Close();
				}
				if (!encryptBeforeSignMode && signedInfo != null && signedInfo.HasUnverifiedReference(text2))
				{
					xmlDictionaryReader2 = base.SecurityVerifiedMessage.CreateFullBodyReader();
					flag5 = this.EnsureDigestValidityIfIdMatches(signedInfo, text2, xmlDictionaryReader2, false, null, null, false);
					xmlDictionaryReader2.Close();
				}
			}
			else
			{
				flag6 = false;
			}
			if (flag5 && flag6)
			{
				base.VerifySignatureEncryption();
			}
			readerAtFirstHeader.Close();
			if (this.pendingSignature != null)
			{
				this.pendingSignature.CompleteSignatureVerification();
				this.pendingSignature = null;
			}
			this.pendingDecryptionToken = null;
			flag = (flag || flag6);
			if (!flag5 && messagePartSpecification2.IsBodyIncluded)
			{
				base.SecurityVerifiedMessage.OnUnsignedPart(XD.MessageDictionary.Body.Value, base.Version.Envelope.Namespace);
			}
			if (!flag6 && messagePartSpecification.IsBodyIncluded)
			{
				base.SecurityVerifiedMessage.OnUnencryptedPart(XD.MessageDictionary.Body.Value, base.Version.Envelope.Namespace);
			}
			base.SecurityVerifiedMessage.OnMessageProtectionPassComplete(flag);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000450A4 File Offset: 0x000432A4
		protected override bool IsReaderAtEncryptedData(XmlDictionaryReader reader)
		{
			bool flag = reader.IsStartElement(EncryptedData.ElementName, XD.XmlEncryptionDictionary.Namespace);
			if (flag)
			{
				base.HasAtLeastOneItemInsideSecurityHeaderEncrypted = true;
			}
			return flag;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000450D2 File Offset: 0x000432D2
		protected override bool IsReaderAtEncryptedKey(XmlDictionaryReader reader)
		{
			return reader.IsStartElement(EncryptedKey.ElementName, XD.XmlEncryptionDictionary.Namespace);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000450E9 File Offset: 0x000432E9
		protected override bool IsReaderAtReferenceList(XmlDictionaryReader reader)
		{
			return reader.IsStartElement(ReferenceList.ElementName, ReferenceList.NamespaceUri);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000450FB File Offset: 0x000432FB
		protected override bool IsReaderAtSignature(XmlDictionaryReader reader)
		{
			return reader.IsStartElement(XD.XmlSignatureDictionary.Signature, XD.XmlSignatureDictionary.Namespace);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00045117 File Offset: 0x00043317
		protected override bool IsReaderAtSecurityTokenReference(XmlDictionaryReader reader)
		{
			return reader.IsStartElement(XD.SecurityJan2004Dictionary.SecurityTokenReference, XD.SecurityJan2004Dictionary.Namespace);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00045133 File Offset: 0x00043333
		protected override void ProcessReferenceListCore(ReferenceList referenceList, WrappedKeySecurityToken wrappedKeyToken)
		{
			this.pendingReferenceList = referenceList;
			this.pendingDecryptionToken = wrappedKeyToken;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00045144 File Offset: 0x00043344
		protected override ReferenceList ReadReferenceListCore(XmlDictionaryReader reader)
		{
			ReferenceList referenceList = new ReferenceList();
			referenceList.ReadFrom(reader);
			return referenceList;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00045160 File Offset: 0x00043360
		protected override EncryptedData ReadSecurityHeaderEncryptedItem(XmlDictionaryReader reader, bool readXmlreferenceKeyInfoClause)
		{
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.ShouldReadXmlReferenceKeyInfoClause = readXmlreferenceKeyInfoClause;
			encryptedData.SecurityTokenSerializer = base.StandardsManager.SecurityTokenSerializer;
			encryptedData.ReadFrom(reader);
			return encryptedData;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00045194 File Offset: 0x00043394
		protected override SignedXml ReadSignatureCore(XmlDictionaryReader signatureReader)
		{
			SignedXml signedXml = new SignedXml(ServiceModelDictionaryManager.Instance, base.StandardsManager.SecurityTokenSerializer);
			signedXml.Signature.SignedInfo.ResourcePool = base.ResourcePool;
			signedXml.ReadFrom(signatureReader);
			return signedXml;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000451D8 File Offset: 0x000433D8
		protected static bool TryResolveKeyIdentifier(SecurityKeyIdentifier keyIdentifier, SecurityTokenResolver resolver, bool isFromSignature, out SecurityToken token)
		{
			if (keyIdentifier != null)
			{
				return resolver.TryResolveToken(keyIdentifier, out token);
			}
			if (isFromSignature)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoKeyInfoInSignatureToFindVerificationToken")));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoKeyInfoInEncryptedItemToFindDecryptingToken")));
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00045228 File Offset: 0x00043428
		protected static SecurityToken ResolveKeyIdentifier(SecurityKeyIdentifier keyIdentifier, SecurityTokenResolver resolver, bool isFromSignature)
		{
			SecurityToken result;
			if (WSSecurityOneDotZeroReceiveSecurityHeader.TryResolveKeyIdentifier(keyIdentifier, resolver, isFromSignature, out result))
			{
				return result;
			}
			if (isFromSignature)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoForVerifyingSignature", new object[]
				{
					keyIdentifier,
					resolver
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoForDecryption", new object[]
			{
				keyIdentifier,
				resolver
			})));
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00045298 File Offset: 0x00043498
		private SecurityToken ResolveSignatureToken(SecurityKeyIdentifier keyIdentifier, SecurityTokenResolver resolver, bool isPrimarySignature)
		{
			SecurityToken securityToken;
			WSSecurityOneDotZeroReceiveSecurityHeader.TryResolveKeyIdentifier(keyIdentifier, resolver, true, out securityToken);
			RsaKeyIdentifierClause rsaKeyIdentifierClause;
			if (securityToken == null && !isPrimarySignature && keyIdentifier.Count == 1 && keyIdentifier.TryFind<RsaKeyIdentifierClause>(out rsaKeyIdentifierClause))
			{
				RsaSecurityTokenAuthenticator rsaSecurityTokenAuthenticator = base.FindAllowedAuthenticator<RsaSecurityTokenAuthenticator>(false);
				if (rsaSecurityTokenAuthenticator != null)
				{
					securityToken = new RsaSecurityToken(rsaKeyIdentifierClause.Rsa);
					ReadOnlyCollection<IAuthorizationPolicy> value = rsaSecurityTokenAuthenticator.ValidateToken(securityToken);
					SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification;
					TokenTracker supportingTokenTracker = base.GetSupportingTokenTracker(rsaSecurityTokenAuthenticator, out supportingTokenAuthenticatorSpecification);
					if (supportingTokenTracker == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("UnknownTokenAuthenticatorUsedInTokenProcessing", new object[]
						{
							rsaSecurityTokenAuthenticator
						})));
					}
					supportingTokenTracker.RecordToken(securityToken);
					base.SecurityTokenAuthorizationPoliciesMapping.Add(securityToken, value);
				}
			}
			if (securityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveKeyInfoForVerifyingSignature", new object[]
				{
					keyIdentifier,
					resolver
				})));
			}
			return securityToken;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004535C File Offset: 0x0004355C
		protected override void ReadSecurityTokenReference(XmlDictionaryReader reader)
		{
			string attribute = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
			SecurityKeyIdentifierClause securityKeyIdentifierClause = base.StandardsManager.SecurityTokenSerializer.ReadKeyIdentifierClause(reader);
			if (string.IsNullOrEmpty(securityKeyIdentifierClause.Id))
			{
				securityKeyIdentifierClause.Id = attribute;
			}
			if (!string.IsNullOrEmpty(securityKeyIdentifierClause.Id))
			{
				base.ElementManager.AppendSecurityTokenReference(securityKeyIdentifierClause, securityKeyIdentifierClause.Id);
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000453C9 File Offset: 0x000435C9
		private bool HasPendingDecryptionItem()
		{
			return this.pendingReferenceList != null && this.pendingReferenceList.DataReferenceCount > 0;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000453E3 File Offset: 0x000435E3
		protected override bool TryDeleteReferenceListEntry(string id)
		{
			return this.pendingReferenceList != null && this.pendingReferenceList.TryRemoveReferredId(id);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000453FC File Offset: 0x000435FC
		protected override void EnsureDecryptionComplete()
		{
			if (this.earlyDecryptedDataReferences != null)
			{
				for (int i = 0; i < this.earlyDecryptedDataReferences.Count; i++)
				{
					if (!this.TryDeleteReferenceListEntry(this.earlyDecryptedDataReferences[i]))
					{
						throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnexpectedEncryptedElementInSecurityHeader")), base.Message);
					}
				}
			}
			if (this.HasPendingDecryptionItem())
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToResolveDataReference", new object[]
				{
					this.pendingReferenceList.GetReferredId(0)
				})), base.Message);
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004548E File Offset: 0x0004368E
		protected override void OnDecryptionOfSecurityHeaderItemRequiringReferenceListEntry(string id)
		{
			if (!this.TryDeleteReferenceListEntry(id))
			{
				if (this.earlyDecryptedDataReferences == null)
				{
					this.earlyDecryptedDataReferences = new List<string>(4);
				}
				this.earlyDecryptedDataReferences.Add(id);
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000454BC File Offset: 0x000436BC
		protected override SecurityToken VerifySignature(SignedXml signedXml, bool isPrimarySignature, SecurityHeaderTokenResolver resolver, object signatureTarget, string id)
		{
			if (TD.SignatureVerificationStartIsEnabled())
			{
				TD.SignatureVerificationStart(base.EventTraceActivity);
			}
			SecurityToken securityToken = this.ResolveSignatureToken(signedXml.Signature.KeyIdentifier, resolver, isPrimarySignature);
			if (isPrimarySignature)
			{
				base.RecordSignatureToken(securityToken);
			}
			ReadOnlyCollection<SecurityKey> securityKeys = securityToken.SecurityKeys;
			SecurityKey securityKey = (securityKeys != null && securityKeys.Count > 0) ? securityKeys[0] : null;
			if (securityKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToCreateICryptoFromTokenForSignatureVerification", new object[]
				{
					securityToken
				})));
			}
			base.AlgorithmSuite.EnsureAcceptableSignatureKeySize(securityKey, securityToken);
			base.AlgorithmSuite.EnsureAcceptableSignatureAlgorithm(securityKey, signedXml.Signature.SignedInfo.SignatureMethod);
			signedXml.StartSignatureVerification(securityKey);
			StandardSignedInfo signedInfo = (StandardSignedInfo)signedXml.Signature.SignedInfo;
			this.ValidateDigestsOfTargetsInSecurityHeader(signedInfo, base.Timestamp, isPrimarySignature, signatureTarget, id);
			if (!isPrimarySignature)
			{
				if (!base.RequireMessageProtection && securityKey is AsymmetricSecurityKey && base.Version.Addressing != AddressingVersion.None)
				{
					int num = base.Message.Headers.FindHeader(XD.AddressingDictionary.To.Value, base.Message.Version.Addressing.Namespace);
					if (num == -1)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TransportSecuredMessageMissingToHeader")));
					}
					XmlDictionaryReader readerAtHeader = base.Message.Headers.GetReaderAtHeader(num);
					id = readerAtHeader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
					if (LocalAppContextSwitches.AllowUnsignedToHeader)
					{
						if (id != null)
						{
							signedXml.EnsureDigestValidityIfIdMatches(id, readerAtHeader);
						}
					}
					else
					{
						if (id == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnsignedToHeaderInTransportSecuredMessage")));
						}
						signedXml.EnsureDigestValidity(id, readerAtHeader);
					}
				}
				signedXml.CompleteSignatureVerification();
				return securityToken;
			}
			this.pendingSignature = signedXml;
			if (TD.SignatureVerificationSuccessIsEnabled())
			{
				TD.SignatureVerificationSuccess(base.EventTraceActivity);
			}
			return securityToken;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000456AC File Offset: 0x000438AC
		private void ValidateDigestsOfTargetsInSecurityHeader(StandardSignedInfo signedInfo, SecurityTimestamp timestamp, bool isPrimarySignature, object signatureTarget, string id)
		{
			for (int i = 0; i < signedInfo.ReferenceCount; i++)
			{
				Reference reference = signedInfo[i];
				base.AlgorithmSuite.EnsureAcceptableDigestAlgorithm(reference.DigestMethod);
				string text = reference.ExtractReferredId();
				if (isPrimarySignature || id == text)
				{
					if (timestamp != null && timestamp.Id == text && !reference.TransformChain.NeedsInclusiveContext && timestamp.DigestAlgorithm == reference.DigestMethod && timestamp.GetDigest() != null)
					{
						reference.EnsureDigestValidity(text, timestamp.GetDigest());
						base.ElementManager.SetTimestampSigned(text);
					}
					else if (signatureTarget != null)
					{
						reference.EnsureDigestValidity(id, signatureTarget);
					}
					else
					{
						int num = -1;
						XmlDictionaryReader xmlDictionaryReader = null;
						if (reference.IsStrTranform())
						{
							if (base.ElementManager.TryGetTokenElementIndexFromStrId(text, out num))
							{
								ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
								base.ElementManager.GetElementEntry(num, out receiveSecurityHeaderEntry);
								bool requiresEncryptedFormReader = receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Signed || receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.SignedEndorsing;
								if (!base.ElementManager.IsPrimaryTokenSigned)
								{
									base.ElementManager.IsPrimaryTokenSigned = (receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Primary && receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Token);
								}
								base.ElementManager.SetSigned(num);
								xmlDictionaryReader = base.ElementManager.GetReader(num, requiresEncryptedFormReader);
							}
						}
						else
						{
							xmlDictionaryReader = base.ElementManager.GetSignatureVerificationReader(text, base.EncryptBeforeSignMode);
						}
						if (xmlDictionaryReader != null)
						{
							reference.EnsureDigestValidity(text, xmlDictionaryReader);
							xmlDictionaryReader.Close();
						}
					}
					if (!isPrimarySignature)
					{
						break;
					}
				}
			}
			if (isPrimarySignature && base.RequireSignedPrimaryToken && !base.ElementManager.IsPrimaryTokenSigned)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SupportingTokenIsNotSigned", new object[]
				{
					new IssuedSecurityTokenParameters()
				})));
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0004586C File Offset: 0x00043A6C
		private void VerifySoapAttributeMatchForHeader(MessageHeaderInfo info, MessagePartSpecification signatureParts, XmlDictionaryReader reader)
		{
			if (!signatureParts.IsHeaderIncluded(info.Name, info.Namespace))
			{
				return;
			}
			EnvelopeVersion envelope = base.Version.Envelope;
			EnvelopeVersion envelopeVersion = (envelope == EnvelopeVersion.Soap11) ? EnvelopeVersion.Soap12 : EnvelopeVersion.Soap11;
			bool flag = reader.GetAttribute(XD.MessageDictionary.MustUnderstand, envelope.DictionaryNamespace) != null;
			bool flag2 = reader.GetAttribute(XD.MessageDictionary.MustUnderstand, envelopeVersion.DictionaryNamespace) != null;
			if (flag2 && !flag)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidAttributeInSignedHeader", new object[]
				{
					info.Name,
					info.Namespace,
					XD.MessageDictionary.MustUnderstand,
					envelopeVersion.DictionaryNamespace,
					XD.MessageDictionary.MustUnderstand,
					envelope.DictionaryNamespace
				})), base.SecurityVerifiedMessage);
			}
			flag = (reader.GetAttribute(envelope.DictionaryActor, envelope.DictionaryNamespace) != null);
			flag2 = (reader.GetAttribute(envelopeVersion.DictionaryActor, envelopeVersion.DictionaryNamespace) != null);
			if (flag2 && !flag)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidAttributeInSignedHeader", new object[]
				{
					info.Name,
					info.Namespace,
					envelopeVersion.DictionaryActor,
					envelopeVersion.DictionaryNamespace,
					envelope.DictionaryActor,
					envelope.DictionaryNamespace
				})), base.SecurityVerifiedMessage);
			}
		}

		// Token: 0x04001A11 RID: 6673
		private WrappedKeySecurityToken pendingDecryptionToken;

		// Token: 0x04001A12 RID: 6674
		private ReferenceList pendingReferenceList;

		// Token: 0x04001A13 RID: 6675
		private SignedXml pendingSignature;

		// Token: 0x04001A14 RID: 6676
		private List<string> earlyDecryptedDataReferences;
	}
}
