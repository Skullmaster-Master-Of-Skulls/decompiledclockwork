using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000290 RID: 656
	internal class WSSecurityOneDotZeroSendSecurityHeader : SendSecurityHeader
	{
		// Token: 0x06001321 RID: 4897 RVA: 0x000459D1 File Offset: 0x00043BD1
		public WSSecurityOneDotZeroSendSecurityHeader(Message message, string actor, bool mustUnderstand, bool relay, SecurityStandardsManager standardsManager, SecurityAlgorithmSuite algorithmSuite, MessageDirection direction) : base(message, actor, mustUnderstand, relay, standardsManager, algorithmSuite, direction)
		{
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x000459E4 File Offset: 0x00043BE4
		protected string EncryptionAlgorithm
		{
			get
			{
				return base.AlgorithmSuite.DefaultEncryptionAlgorithm;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000459F1 File Offset: 0x00043BF1
		protected XmlDictionaryString EncryptionAlgorithmDictionaryString
		{
			get
			{
				return base.AlgorithmSuite.DefaultEncryptionAlgorithmDictionaryString;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x000459FE File Offset: 0x00043BFE
		protected override bool HasSignedEncryptedMessagePart
		{
			get
			{
				return this.hasSignedEncryptedMessagePart;
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00045A08 File Offset: 0x00043C08
		private void AddEncryptionReference(MessageHeader header, string headerId, IPrefixGenerator prefixGenerator, bool sign, out MemoryStream plainTextStream, out string encryptedDataId)
		{
			plainTextStream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(plainTextStream);
			if (sign)
			{
				this.AddSignatureReference(header, headerId, prefixGenerator, xmlDictionaryWriter);
			}
			else
			{
				header.WriteHeader(xmlDictionaryWriter, base.Version);
				xmlDictionaryWriter.Flush();
			}
			encryptedDataId = base.GenerateId();
			this.referenceList.AddReferredId(encryptedDataId);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00045A60 File Offset: 0x00043C60
		private void AddSignatureReference(SecurityToken token, int position, SecurityTokenAttachmentMode mode)
		{
			SecurityKeyIdentifierClause keyIdentifierClause = null;
			bool strTransformEnabled = base.ShouldUseStrTransformForToken(token, position, mode, out keyIdentifierClause);
			this.AddTokenSignatureReference(token, keyIdentifierClause, strTransformEnabled);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00045A84 File Offset: 0x00043C84
		private void AddPrimaryTokenSignatureReference(SecurityToken token, SecurityTokenParameters securityTokenParameters)
		{
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = securityTokenParameters as IssuedSecurityTokenParameters;
			if (issuedSecurityTokenParameters == null)
			{
				return;
			}
			bool flag = issuedSecurityTokenParameters != null && issuedSecurityTokenParameters.UseStrTransform;
			SecurityKeyIdentifierClause keyIdentifierClause = null;
			if (SendSecurityHeader.ShouldSerializeToken(securityTokenParameters, base.MessageDirection))
			{
				if (flag)
				{
					keyIdentifierClause = securityTokenParameters.CreateKeyIdentifierClause(token, base.GetTokenReferenceStyle(securityTokenParameters));
				}
				this.AddTokenSignatureReference(token, keyIdentifierClause, flag);
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00045AD4 File Offset: 0x00043CD4
		private void AddTokenSignatureReference(SecurityToken token, SecurityKeyIdentifierClause keyIdentifierClause, bool strTransformEnabled)
		{
			if (!strTransformEnabled && token.Id == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("ElementToSignMustHaveId")), base.Message);
			}
			HashStream hashStream = this.TakeHashStream();
			XmlDictionaryWriter xmlDictionaryWriter = this.TakeUtf8Writer();
			xmlDictionaryWriter.StartCanonicalization(hashStream, false, null);
			base.StandardsManager.SecurityTokenSerializer.WriteToken(xmlDictionaryWriter, token);
			xmlDictionaryWriter.EndCanonicalization();
			if (!strTransformEnabled)
			{
				this.signedInfo.AddReference(token.Id, hashStream.FlushHashAndGetValue());
				return;
			}
			if (keyIdentifierClause != null)
			{
				if (string.IsNullOrEmpty(keyIdentifierClause.Id))
				{
					keyIdentifierClause.Id = SecurityUniqueId.Create().Value;
				}
				base.ElementContainer.MapSecurityTokenToStrClause(token, keyIdentifierClause);
				this.signedInfo.AddReference(keyIdentifierClause.Id, hashStream.FlushHashAndGetValue(), true);
				return;
			}
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00045BB4 File Offset: 0x00043DB4
		private void AddSignatureReference(SendSecurityHeaderElement[] elements)
		{
			if (elements != null)
			{
				for (int i = 0; i < elements.Length; i++)
				{
					SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
					TokenElement tokenElement = elements[i].Item as TokenElement;
					bool flag = tokenElement != null && base.SignThenEncrypt && base.ShouldUseStrTransformForToken(tokenElement.Token, i, SecurityTokenAttachmentMode.SignedEncrypted, out securityKeyIdentifierClause);
					if (!flag && elements[i].Id == null)
					{
						throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("ElementToSignMustHaveId")), base.Message);
					}
					HashStream hashStream = this.TakeHashStream();
					XmlDictionaryWriter xmlDictionaryWriter = this.TakeUtf8Writer();
					xmlDictionaryWriter.StartCanonicalization(hashStream, false, null);
					elements[i].Item.WriteTo(xmlDictionaryWriter, ServiceModelDictionaryManager.Instance);
					xmlDictionaryWriter.EndCanonicalization();
					if (flag)
					{
						if (securityKeyIdentifierClause == null)
						{
							throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
						}
						if (string.IsNullOrEmpty(securityKeyIdentifierClause.Id))
						{
							securityKeyIdentifierClause.Id = SecurityUniqueId.Create().Value;
						}
						base.ElementContainer.MapSecurityTokenToStrClause(tokenElement.Token, securityKeyIdentifierClause);
						this.signedInfo.AddReference(securityKeyIdentifierClause.Id, hashStream.FlushHashAndGetValue(), true);
					}
					else
					{
						this.signedInfo.AddReference(elements[i].Id, hashStream.FlushHashAndGetValue());
					}
				}
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00045CF4 File Offset: 0x00043EF4
		private void AddSignatureReference(SecurityToken[] tokens, SecurityTokenAttachmentMode mode)
		{
			if (tokens != null)
			{
				for (int i = 0; i < tokens.Length; i++)
				{
					this.AddSignatureReference(tokens[i], i, mode);
				}
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00045D20 File Offset: 0x00043F20
		private string GetSignatureHash(MessageHeader header, string headerId, IPrefixGenerator prefixGenerator, XmlDictionaryWriter writer, out byte[] hash)
		{
			HashStream hashStream = this.TakeHashStream();
			XmlBuffer xmlBuffer = null;
			XmlDictionaryWriter xmlDictionaryWriter;
			if (writer.CanCanonicalize)
			{
				xmlDictionaryWriter = writer;
			}
			else
			{
				xmlBuffer = new XmlBuffer(int.MaxValue);
				xmlDictionaryWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			}
			xmlDictionaryWriter.StartCanonicalization(hashStream, false, null);
			header.WriteStartHeader(xmlDictionaryWriter, base.Version);
			if (headerId == null)
			{
				headerId = base.GenerateId();
				base.StandardsManager.IdManager.WriteIdAttribute(xmlDictionaryWriter, headerId);
			}
			header.WriteHeaderContents(xmlDictionaryWriter, base.Version);
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.EndCanonicalization();
			xmlDictionaryWriter.Flush();
			if (xmlDictionaryWriter != writer)
			{
				xmlBuffer.CloseSection();
				xmlBuffer.Close();
				XmlDictionaryReader reader = xmlBuffer.GetReader(0);
				writer.WriteNode(reader, false);
				reader.Close();
			}
			hash = hashStream.FlushHashAndGetValue();
			return headerId;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00045DE0 File Offset: 0x00043FE0
		private void AddSignatureReference(MessageHeader header, string headerId, IPrefixGenerator prefixGenerator, XmlDictionaryWriter writer)
		{
			byte[] digest;
			headerId = this.GetSignatureHash(header, headerId, prefixGenerator, writer, out digest);
			this.signedInfo.AddReference(headerId, digest);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00045E0C File Offset: 0x0004400C
		private void ApplySecurityAndWriteHeader(MessageHeader header, string headerId, XmlDictionaryWriter writer, IPrefixGenerator prefixGenerator)
		{
			if (!base.RequireMessageProtection && base.ShouldSignToHeader && header.Name == XD.AddressingDictionary.To.Value && header.Namespace == base.Message.Version.Addressing.Namespace)
			{
				if (this.toHeaderHash == null)
				{
					byte[] array;
					headerId = this.GetSignatureHash(header, headerId, prefixGenerator, writer, out array);
					this.toHeaderHash = array;
					this.toHeaderId = headerId;
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TransportSecuredMessageHasMoreThanOneToHeader")));
			}
			else
			{
				switch (this.GetProtectionMode(header))
				{
				case MessagePartProtectionMode.None:
					header.WriteHeader(writer, base.Version);
					return;
				case MessagePartProtectionMode.Sign:
					this.AddSignatureReference(header, headerId, prefixGenerator, writer);
					return;
				case MessagePartProtectionMode.Encrypt:
				{
					MemoryStream stream;
					string text;
					this.AddEncryptionReference(header, headerId, prefixGenerator, false, out stream, out text);
					this.EncryptAndWriteHeader(header, text, stream, writer);
					return;
				}
				case MessagePartProtectionMode.SignThenEncrypt:
				{
					MemoryStream stream;
					string text;
					this.AddEncryptionReference(header, headerId, prefixGenerator, true, out stream, out text);
					this.EncryptAndWriteHeader(header, text, stream, writer);
					this.hasSignedEncryptedMessagePart = true;
					return;
				}
				case MessagePartProtectionMode.EncryptThenSign:
				{
					MemoryStream stream;
					string text;
					this.AddEncryptionReference(header, headerId, prefixGenerator, false, out stream, out text);
					EncryptedHeader header2 = this.EncryptHeader(header, this.encryptingSymmetricAlgorithm, this.encryptionKeyIdentifier, base.Version, text, stream);
					this.AddSignatureReference(header2, text, prefixGenerator, writer);
					return;
				}
				default:
					return;
				}
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00045F5C File Offset: 0x0004415C
		public override void ApplySecurityAndWriteHeaders(MessageHeaders headers, XmlDictionaryWriter writer, IPrefixGenerator prefixGenerator)
		{
			string[] array;
			if (base.RequireMessageProtection || base.ShouldSignToHeader)
			{
				array = headers.GetHeaderAttributes("Id", base.StandardsManager.IdManager.DefaultIdNamespaceUri);
			}
			else
			{
				array = null;
			}
			for (int i = 0; i < headers.Count; i++)
			{
				MessageHeader messageHeader = headers.GetMessageHeader(i);
				if ((base.Version.Addressing != AddressingVersion.None || !(messageHeader.Namespace == AddressingVersion.None.Namespace)) && messageHeader != this)
				{
					this.ApplySecurityAndWriteHeader(messageHeader, (array == null) ? null : array[i], writer, prefixGenerator);
				}
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00045FF4 File Offset: 0x000441F4
		private static bool CanCanonicalizeAndFragment(XmlDictionaryWriter writer)
		{
			if (!writer.CanCanonicalize)
			{
				return false;
			}
			IFragmentCapableXmlDictionaryWriter fragmentCapableXmlDictionaryWriter = writer as IFragmentCapableXmlDictionaryWriter;
			return fragmentCapableXmlDictionaryWriter != null && fragmentCapableXmlDictionaryWriter.CanFragment;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00046020 File Offset: 0x00044220
		public override void ApplyBodySecurity(XmlDictionaryWriter writer, IPrefixGenerator prefixGenerator)
		{
			SecurityAppliedMessage securityAppliedMessage = base.SecurityAppliedMessage;
			switch (securityAppliedMessage.BodyProtectionMode)
			{
			case MessagePartProtectionMode.None:
				return;
			case MessagePartProtectionMode.Sign:
			{
				HashStream hashStream = this.TakeHashStream();
				if (WSSecurityOneDotZeroSendSecurityHeader.CanCanonicalizeAndFragment(writer))
				{
					securityAppliedMessage.WriteBodyToSignWithFragments(hashStream, false, null, writer);
				}
				else
				{
					securityAppliedMessage.WriteBodyToSign(hashStream);
				}
				this.signedInfo.AddReference(securityAppliedMessage.BodyId, hashStream.FlushHashAndGetValue());
				return;
			}
			case MessagePartProtectionMode.Encrypt:
			{
				EncryptedData encryptedData = this.CreateEncryptedDataForBody();
				securityAppliedMessage.WriteBodyToEncrypt(encryptedData, this.encryptingSymmetricAlgorithm);
				this.referenceList.AddReferredId(encryptedData.Id);
				return;
			}
			case MessagePartProtectionMode.SignThenEncrypt:
			{
				HashStream hashStream = this.TakeHashStream();
				EncryptedData encryptedData = this.CreateEncryptedDataForBody();
				if (WSSecurityOneDotZeroSendSecurityHeader.CanCanonicalizeAndFragment(writer))
				{
					securityAppliedMessage.WriteBodyToSignThenEncryptWithFragments(hashStream, false, null, encryptedData, this.encryptingSymmetricAlgorithm, writer);
				}
				else
				{
					securityAppliedMessage.WriteBodyToSignThenEncrypt(hashStream, encryptedData, this.encryptingSymmetricAlgorithm);
				}
				this.signedInfo.AddReference(securityAppliedMessage.BodyId, hashStream.FlushHashAndGetValue());
				this.referenceList.AddReferredId(encryptedData.Id);
				this.hasSignedEncryptedMessagePart = true;
				return;
			}
			case MessagePartProtectionMode.EncryptThenSign:
			{
				HashStream hashStream = this.TakeHashStream();
				EncryptedData encryptedData = this.CreateEncryptedDataForBody();
				securityAppliedMessage.WriteBodyToEncryptThenSign(hashStream, encryptedData, this.encryptingSymmetricAlgorithm);
				this.signedInfo.AddReference(securityAppliedMessage.BodyId, hashStream.FlushHashAndGetValue());
				this.referenceList.AddReferredId(encryptedData.Id);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00046164 File Offset: 0x00044364
		protected static MemoryStream CaptureToken(SecurityToken token, SecurityStandardsManager serializer)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream);
			serializer.SecurityTokenSerializer.WriteToken(xmlDictionaryWriter, token);
			xmlDictionaryWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004619C File Offset: 0x0004439C
		protected static MemoryStream CaptureSecurityElement(ISecurityElement element)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream);
			element.WriteTo(xmlDictionaryWriter, ServiceModelDictionaryManager.Instance);
			xmlDictionaryWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000461D4 File Offset: 0x000443D4
		protected override ISecurityElement CompleteEncryptionCore(SendSecurityHeaderElement primarySignature, SendSecurityHeaderElement[] basicTokens, SendSecurityHeaderElement[] signatureConfirmations, SendSecurityHeaderElement[] endorsingSignatures)
		{
			if (this.referenceList == null)
			{
				return null;
			}
			if (primarySignature != null && primarySignature.Item != null && primarySignature.MarkedForEncryption)
			{
				this.EncryptElement(primarySignature);
			}
			if (basicTokens != null)
			{
				for (int i = 0; i < basicTokens.Length; i++)
				{
					if (basicTokens[i].MarkedForEncryption)
					{
						this.EncryptElement(basicTokens[i]);
					}
				}
			}
			if (signatureConfirmations != null)
			{
				for (int j = 0; j < signatureConfirmations.Length; j++)
				{
					if (signatureConfirmations[j].MarkedForEncryption)
					{
						this.EncryptElement(signatureConfirmations[j]);
					}
				}
			}
			if (endorsingSignatures != null)
			{
				for (int k = 0; k < endorsingSignatures.Length; k++)
				{
					if (endorsingSignatures[k].MarkedForEncryption)
					{
						this.EncryptElement(endorsingSignatures[k]);
					}
				}
			}
			ISecurityElement result;
			try
			{
				result = ((this.referenceList.DataReferenceCount > 0) ? this.referenceList : null);
			}
			finally
			{
				this.referenceList = null;
				this.encryptingSymmetricAlgorithm = null;
				this.encryptionKeyIdentifier = null;
			}
			return result;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000462B8 File Offset: 0x000444B8
		protected override ISignatureValueSecurityElement CompletePrimarySignatureCore(SendSecurityHeaderElement[] signatureConfirmations, SecurityToken[] signedEndorsingTokens, SecurityToken[] signedTokens, SendSecurityHeaderElement[] basicTokens, bool isPrimarySignature)
		{
			if (this.signedXml == null)
			{
				return null;
			}
			SecurityTimestamp timestamp = base.Timestamp;
			if (timestamp != null)
			{
				if (timestamp.Id == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TimestampToSignHasNoId")));
				}
				HashStream hashStream = this.TakeHashStream();
				base.StandardsManager.WSUtilitySpecificationVersion.WriteTimestampCanonicalForm(hashStream, timestamp, this.signedInfo.ResourcePool.TakeEncodingBuffer());
				this.signedInfo.AddReference(timestamp.Id, hashStream.FlushHashAndGetValue());
			}
			if (base.ShouldSignToHeader && this.signatureKey is AsymmetricSecurityKey && base.Version.Addressing != AddressingVersion.None)
			{
				if (this.toHeaderHash == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportSecurityRequireToHeader")));
				}
				this.signedInfo.AddReference(this.toHeaderId, this.toHeaderHash);
			}
			this.AddSignatureReference(signatureConfirmations);
			if (isPrimarySignature && base.ShouldProtectTokens)
			{
				this.AddPrimaryTokenSignatureReference(base.ElementContainer.SourceSigningToken, base.SigningTokenParameters);
			}
			if (base.RequireMessageProtection)
			{
				this.AddSignatureReference(signedEndorsingTokens, SecurityTokenAttachmentMode.SignedEndorsing);
				this.AddSignatureReference(signedTokens, SecurityTokenAttachmentMode.Signed);
				this.AddSignatureReference(basicTokens);
			}
			if (this.signedInfo.ReferenceCount == 0)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoPartsOfMessageMatchedPartsToSign")), base.Message);
			}
			ISignatureValueSecurityElement result;
			try
			{
				this.signedXml.ComputeSignature(this.signatureKey);
				result = this.signedXml;
			}
			finally
			{
				this.hashStream = null;
				this.signedInfo = null;
				this.signedXml = null;
				this.signatureKey = null;
				this.effectiveSignatureParts = null;
			}
			return result;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00046460 File Offset: 0x00044660
		private EncryptedData CreateEncryptedData()
		{
			return new EncryptedData
			{
				SecurityTokenSerializer = base.StandardsManager.SecurityTokenSerializer,
				KeyIdentifier = this.encryptionKeyIdentifier,
				EncryptionMethod = this.EncryptionAlgorithm,
				EncryptionMethodDictionaryString = this.EncryptionAlgorithmDictionaryString
			};
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000464AC File Offset: 0x000446AC
		private EncryptedData CreateEncryptedData(MemoryStream stream, string id, bool typeElement)
		{
			EncryptedData encryptedData = this.CreateEncryptedData();
			encryptedData.Id = id;
			encryptedData.SetUpEncryption(this.encryptingSymmetricAlgorithm, new ArraySegment<byte>(stream.GetBuffer(), 0, (int)stream.Length));
			if (typeElement)
			{
				encryptedData.Type = EncryptedData.ElementType;
			}
			return encryptedData;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000464F8 File Offset: 0x000446F8
		private EncryptedData CreateEncryptedDataForBody()
		{
			EncryptedData encryptedData = this.CreateEncryptedData();
			encryptedData.Type = EncryptedData.ContentType;
			return encryptedData;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00046518 File Offset: 0x00044718
		private void EncryptAndWriteHeader(MessageHeader plainTextHeader, string id, MemoryStream stream, XmlDictionaryWriter writer)
		{
			EncryptedHeader encryptedHeader = this.EncryptHeader(plainTextHeader, this.encryptingSymmetricAlgorithm, this.encryptionKeyIdentifier, base.Version, id, stream);
			encryptedHeader.WriteHeader(writer, base.Version);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00046550 File Offset: 0x00044750
		private void EncryptElement(SendSecurityHeaderElement element)
		{
			string id = base.GenerateId();
			ISecurityElement item = this.CreateEncryptedData(WSSecurityOneDotZeroSendSecurityHeader.CaptureSecurityElement(element.Item), id, true);
			this.referenceList.AddReferredId(id);
			element.Replace(id, item);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004658C File Offset: 0x0004478C
		protected virtual EncryptedHeader EncryptHeader(MessageHeader plainTextHeader, SymmetricAlgorithm algorithm, SecurityKeyIdentifier keyIdentifier, MessageVersion version, string id, MemoryStream stream)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HeaderEncryptionNotSupportedInWsSecurityJan2004", new object[]
			{
				plainTextHeader.Name,
				plainTextHeader.Namespace
			})));
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000465C0 File Offset: 0x000447C0
		private HashStream TakeHashStream()
		{
			HashStream hashStream;
			if (this.hashStream == null)
			{
				hashStream = (this.hashStream = new HashStream(CryptoHelper.CreateHashAlgorithm(base.AlgorithmSuite.DefaultDigestAlgorithm)));
			}
			else
			{
				hashStream = this.hashStream;
				hashStream.Reset();
			}
			return hashStream;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00046604 File Offset: 0x00044804
		private XmlDictionaryWriter TakeUtf8Writer()
		{
			return this.signedInfo.ResourcePool.TakeUtf8Writer();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00046618 File Offset: 0x00044818
		private MessagePartProtectionMode GetProtectionMode(MessageHeader header)
		{
			if (!base.RequireMessageProtection)
			{
				return MessagePartProtectionMode.None;
			}
			bool sign = this.signedInfo != null && this.effectiveSignatureParts.IsHeaderIncluded(header);
			bool encrypt = this.referenceList != null && base.EncryptionParts.IsHeaderIncluded(header);
			return MessagePartProtectionModeHelper.GetProtectionMode(sign, encrypt, base.SignThenEncrypt);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004666C File Offset: 0x0004486C
		protected override void StartEncryptionCore(SecurityToken token, SecurityKeyIdentifier keyIdentifier)
		{
			this.encryptingSymmetricAlgorithm = SecurityUtils.GetSymmetricAlgorithm(this.EncryptionAlgorithm, token);
			if (this.encryptingSymmetricAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToCreateSymmetricAlgorithmFromToken", new object[]
				{
					this.EncryptionAlgorithm
				})));
			}
			this.encryptionKeyIdentifier = keyIdentifier;
			this.referenceList = new ReferenceList();
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000466D0 File Offset: 0x000448D0
		protected override void StartPrimarySignatureCore(SecurityToken token, SecurityKeyIdentifier keyIdentifier, MessagePartSpecification signatureParts, bool generateTargettableSignature)
		{
			SecurityAlgorithmSuite algorithmSuite = base.AlgorithmSuite;
			string defaultCanonicalizationAlgorithm = algorithmSuite.DefaultCanonicalizationAlgorithm;
			XmlDictionaryString defaultCanonicalizationAlgorithmDictionaryString = algorithmSuite.DefaultCanonicalizationAlgorithmDictionaryString;
			if (defaultCanonicalizationAlgorithm != "http://www.w3.org/2001/10/xml-exc-c14n#")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnsupportedCanonicalizationAlgorithm", new object[]
				{
					algorithmSuite.DefaultCanonicalizationAlgorithm
				})));
			}
			string signatureMethod;
			XmlDictionaryString signatureMethodDictionaryString;
			algorithmSuite.GetSignatureAlgorithmAndKey(token, out signatureMethod, out this.signatureKey, out signatureMethodDictionaryString);
			string defaultDigestAlgorithm = algorithmSuite.DefaultDigestAlgorithm;
			XmlDictionaryString defaultDigestAlgorithmDictionaryString = algorithmSuite.DefaultDigestAlgorithmDictionaryString;
			this.signedInfo = new PreDigestedSignedInfo(ServiceModelDictionaryManager.Instance, defaultCanonicalizationAlgorithm, defaultCanonicalizationAlgorithmDictionaryString, defaultDigestAlgorithm, defaultDigestAlgorithmDictionaryString, signatureMethod, signatureMethodDictionaryString);
			this.signedXml = new SignedXml(this.signedInfo, ServiceModelDictionaryManager.Instance, base.StandardsManager.SecurityTokenSerializer);
			if (keyIdentifier != null)
			{
				this.signedXml.Signature.KeyIdentifier = keyIdentifier;
			}
			if (generateTargettableSignature)
			{
				this.signedXml.Id = base.GenerateId();
			}
			this.effectiveSignatureParts = signatureParts;
			this.hashStream = this.signedInfo.ResourcePool.TakeHashStream(defaultDigestAlgorithm);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000467CB File Offset: 0x000449CB
		protected override ISignatureValueSecurityElement CreateSupportingSignature(SecurityToken token, SecurityKeyIdentifier identifier)
		{
			this.StartPrimarySignatureCore(token, identifier, MessagePartSpecification.NoParts, false);
			return this.CompletePrimarySignatureCore(null, null, null, null, false);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000467E8 File Offset: 0x000449E8
		protected override ISignatureValueSecurityElement CreateSupportingSignature(SecurityToken token, SecurityKeyIdentifier identifier, ISecurityElement elementToSign)
		{
			SecurityAlgorithmSuite algorithmSuite = base.AlgorithmSuite;
			string signatureMethod;
			SecurityKey signingKey;
			XmlDictionaryString signatureMethodDictionaryString;
			algorithmSuite.GetSignatureAlgorithmAndKey(token, out signatureMethod, out signingKey, out signatureMethodDictionaryString);
			SignedXml signedXml = new SignedXml(ServiceModelDictionaryManager.Instance, base.StandardsManager.SecurityTokenSerializer);
			SignedInfo signedInfo = signedXml.Signature.SignedInfo;
			signedInfo.CanonicalizationMethod = algorithmSuite.DefaultCanonicalizationAlgorithm;
			signedInfo.CanonicalizationMethodDictionaryString = algorithmSuite.DefaultCanonicalizationAlgorithmDictionaryString;
			signedInfo.SignatureMethod = signatureMethod;
			signedInfo.SignatureMethodDictionaryString = signatureMethodDictionaryString;
			if (elementToSign.Id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ElementToSignMustHaveId")));
			}
			Reference reference = new Reference(ServiceModelDictionaryManager.Instance, "#" + elementToSign.Id, elementToSign);
			reference.DigestMethod = algorithmSuite.DefaultDigestAlgorithm;
			reference.DigestMethodDictionaryString = algorithmSuite.DefaultDigestAlgorithmDictionaryString;
			reference.AddTransform(new ExclusiveCanonicalizationTransform());
			((StandardSignedInfo)signedInfo).AddReference(reference);
			signedXml.ComputeSignature(signingKey);
			if (identifier != null)
			{
				signedXml.Signature.KeyIdentifier = identifier;
			}
			return signedXml;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000468E8 File Offset: 0x00044AE8
		protected override void WriteSecurityTokenReferencyEntry(XmlDictionaryWriter writer, SecurityToken securityToken, SecurityTokenParameters securityTokenParameters)
		{
			SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = securityTokenParameters as IssuedSecurityTokenParameters;
			if (issuedSecurityTokenParameters == null || !issuedSecurityTokenParameters.UseStrTransform)
			{
				return;
			}
			if (!base.ElementContainer.TryGetIdentifierClauseFromSecurityToken(securityToken, out securityKeyIdentifierClause))
			{
				return;
			}
			if (securityKeyIdentifierClause != null && !string.IsNullOrEmpty(securityKeyIdentifierClause.Id))
			{
				WrappedXmlDictionaryWriter writer2 = new WrappedXmlDictionaryWriter(writer, securityKeyIdentifierClause.Id);
				base.StandardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer2, securityKeyIdentifierClause);
				return;
			}
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("TokenManagerCannotCreateTokenReference")), base.Message);
		}

		// Token: 0x04001A15 RID: 6677
		private HashStream hashStream;

		// Token: 0x04001A16 RID: 6678
		private PreDigestedSignedInfo signedInfo;

		// Token: 0x04001A17 RID: 6679
		private SignedXml signedXml;

		// Token: 0x04001A18 RID: 6680
		private SecurityKey signatureKey;

		// Token: 0x04001A19 RID: 6681
		private MessagePartSpecification effectiveSignatureParts;

		// Token: 0x04001A1A RID: 6682
		private SymmetricAlgorithm encryptingSymmetricAlgorithm;

		// Token: 0x04001A1B RID: 6683
		private ReferenceList referenceList;

		// Token: 0x04001A1C RID: 6684
		private SecurityKeyIdentifier encryptionKeyIdentifier;

		// Token: 0x04001A1D RID: 6685
		private bool hasSignedEncryptedMessagePart;

		// Token: 0x04001A1E RID: 6686
		private byte[] toHeaderHash;

		// Token: 0x04001A1F RID: 6687
		private string toHeaderId;
	}
}
