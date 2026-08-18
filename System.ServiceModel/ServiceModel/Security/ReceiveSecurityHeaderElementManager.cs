using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002AC RID: 684
	internal sealed class ReceiveSecurityHeaderElementManager : ISignatureReaderProvider
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x0004F2BB File Offset: 0x0004D4BB
		public ReceiveSecurityHeaderElementManager(ReceiveSecurityHeader securityHeader)
		{
			this.securityHeader = securityHeader;
			this.elements = new ReceiveSecurityHeaderEntry[8];
			if (securityHeader.RequireMessageProtection)
			{
				this.headerIds = new string[securityHeader.ProcessedMessage.Headers.Count];
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0004F2F9 File Offset: 0x0004D4F9
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0004F301 File Offset: 0x0004D501
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0004F309 File Offset: 0x0004D509
		public bool IsPrimaryTokenSigned
		{
			get
			{
				return this.isPrimaryTokenSigned;
			}
			set
			{
				this.isPrimaryTokenSigned = value;
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0004F314 File Offset: 0x0004D514
		public void AppendElement(ReceiveSecurityHeaderElementCategory elementCategory, object element, ReceiveSecurityHeaderBindingModes bindingMode, string id, TokenTracker supportingTokenTracker)
		{
			if (id != null)
			{
				this.VerifyIdUniquenessInSecurityHeader(id);
			}
			this.EnsureCapacityToAdd();
			ReceiveSecurityHeaderEntry[] array = this.elements;
			int num = this.count;
			this.count = num + 1;
			array[num].SetElement(elementCategory, element, bindingMode, id, false, null, supportingTokenTracker);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004F35D File Offset: 0x0004D55D
		public void AppendSignature(SignedXml signedXml)
		{
			this.AppendElement(ReceiveSecurityHeaderElementCategory.Signature, signedXml, ReceiveSecurityHeaderBindingModes.Unknown, signedXml.Id, null);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0004F36F File Offset: 0x0004D56F
		public void AppendReferenceList(ReferenceList referenceList)
		{
			this.AppendElement(ReceiveSecurityHeaderElementCategory.ReferenceList, referenceList, ReceiveSecurityHeaderBindingModes.Unknown, null, null);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0004F37C File Offset: 0x0004D57C
		public void AppendEncryptedData(EncryptedData encryptedData)
		{
			this.AppendElement(ReceiveSecurityHeaderElementCategory.EncryptedData, encryptedData, ReceiveSecurityHeaderBindingModes.Unknown, encryptedData.Id, null);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0004F38E File Offset: 0x0004D58E
		public void AppendSignatureConfirmation(ISignatureValueSecurityElement signatureConfirmationElement)
		{
			this.AppendElement(ReceiveSecurityHeaderElementCategory.SignatureConfirmation, signatureConfirmationElement, ReceiveSecurityHeaderBindingModes.Unknown, signatureConfirmationElement.Id, null);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0004F3A0 File Offset: 0x0004D5A0
		public void AppendTimestamp(SecurityTimestamp timestamp)
		{
			this.AppendElement(ReceiveSecurityHeaderElementCategory.Timestamp, timestamp, ReceiveSecurityHeaderBindingModes.Unknown, timestamp.Id, null);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004F3B2 File Offset: 0x0004D5B2
		public void AppendSecurityTokenReference(SecurityKeyIdentifierClause strClause, string strId)
		{
			if (!string.IsNullOrEmpty(strId))
			{
				this.VerifyIdUniquenessInSecurityHeader(strId);
				this.AppendElement(ReceiveSecurityHeaderElementCategory.SecurityTokenReference, strClause, ReceiveSecurityHeaderBindingModes.Unknown, strId, null);
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0004F3CE File Offset: 0x0004D5CE
		public void AppendToken(SecurityToken token, ReceiveSecurityHeaderBindingModes mode, TokenTracker supportingTokenTracker)
		{
			this.AppendElement(ReceiveSecurityHeaderElementCategory.Token, token, mode, token.Id, supportingTokenTracker);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004F3E0 File Offset: 0x0004D5E0
		public void EnsureAllRequiredSecurityHeaderTargetsWereProtected()
		{
			for (int i = 0; i < this.count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				this.GetElementEntry(i, out receiveSecurityHeaderEntry);
				if (!receiveSecurityHeaderEntry.signed)
				{
					switch (receiveSecurityHeaderEntry.elementCategory)
					{
					case ReceiveSecurityHeaderElementCategory.SignatureConfirmation:
					case ReceiveSecurityHeaderElementCategory.Timestamp:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredSecurityHeaderElementNotSigned", new object[]
						{
							receiveSecurityHeaderEntry.elementCategory,
							receiveSecurityHeaderEntry.id
						})));
					case ReceiveSecurityHeaderElementCategory.Token:
					{
						ReceiveSecurityHeaderBindingModes bindingMode = receiveSecurityHeaderEntry.bindingMode;
						if (bindingMode == ReceiveSecurityHeaderBindingModes.Signed || bindingMode == ReceiveSecurityHeaderBindingModes.SignedEndorsing || bindingMode == ReceiveSecurityHeaderBindingModes.Basic)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredSecurityTokenNotSigned", new object[]
							{
								receiveSecurityHeaderEntry.element,
								receiveSecurityHeaderEntry.bindingMode
							})));
						}
						break;
					}
					}
				}
				if (!receiveSecurityHeaderEntry.encrypted && receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Token && receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Basic)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredSecurityTokenNotEncrypted", new object[]
					{
						receiveSecurityHeaderEntry.element,
						receiveSecurityHeaderEntry.bindingMode
					})));
				}
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0004F514 File Offset: 0x0004D714
		private void EnsureCapacityToAdd()
		{
			if (this.count == this.elements.Length)
			{
				ReceiveSecurityHeaderEntry[] destinationArray = new ReceiveSecurityHeaderEntry[this.elements.Length * 2];
				Array.Copy(this.elements, 0, destinationArray, 0, this.count);
				this.elements = destinationArray;
			}
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0004F55C File Offset: 0x0004D75C
		public object GetElement(int index)
		{
			return this.elements[index].element;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0004F56F File Offset: 0x0004D76F
		public T GetElement<T>(int index) where T : class
		{
			return (T)((object)this.elements[index].element);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0004F587 File Offset: 0x0004D787
		public void GetElementEntry(int index, out ReceiveSecurityHeaderEntry element)
		{
			element = this.elements[index];
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0004F59B File Offset: 0x0004D79B
		public ReceiveSecurityHeaderElementCategory GetElementCategory(int index)
		{
			return this.elements[index].elementCategory;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0004F5B0 File Offset: 0x0004D7B0
		public void GetPrimarySignature(out XmlDictionaryReader reader, out string id)
		{
			for (int i = 0; i < this.count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				this.GetElementEntry(i, out receiveSecurityHeaderEntry);
				if (receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Signature && receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Primary)
				{
					reader = this.GetReader(i, false);
					id = receiveSecurityHeaderEntry.id;
					return;
				}
			}
			reader = null;
			id = null;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0004F604 File Offset: 0x0004D804
		internal XmlDictionaryReader GetReader(int index, bool requiresEncryptedFormReader)
		{
			if (!requiresEncryptedFormReader)
			{
				byte[] decryptedBuffer = this.elements[index].decryptedBuffer;
				if (decryptedBuffer != null)
				{
					return this.securityHeader.CreateDecryptedReader(decryptedBuffer);
				}
			}
			XmlDictionaryReader xmlDictionaryReader = this.securityHeader.CreateSecurityHeaderReader();
			xmlDictionaryReader.ReadStartElement();
			int num = 0;
			while (xmlDictionaryReader.IsStartElement() && num < index)
			{
				xmlDictionaryReader.Skip();
				num++;
			}
			return xmlDictionaryReader;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0004F664 File Offset: 0x0004D864
		public XmlDictionaryReader GetSignatureVerificationReader(string id, bool requiresEncryptedFormReaderIfDecrypted)
		{
			for (int i = 0; i < this.count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				this.GetElementEntry(i, out receiveSecurityHeaderEntry);
				bool flag = receiveSecurityHeaderEntry.encrypted && requiresEncryptedFormReaderIfDecrypted;
				bool flag2 = receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Signed || receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.SignedEndorsing;
				if (receiveSecurityHeaderEntry.MatchesId(id, flag))
				{
					this.SetSigned(i);
					if (!this.IsPrimaryTokenSigned)
					{
						this.IsPrimaryTokenSigned = (receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Primary && receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Token);
					}
					return this.GetReader(i, flag);
				}
				if (receiveSecurityHeaderEntry.MatchesId(id, flag2))
				{
					this.SetSigned(i);
					if (!this.IsPrimaryTokenSigned)
					{
						this.IsPrimaryTokenSigned = (receiveSecurityHeaderEntry.bindingMode == ReceiveSecurityHeaderBindingModes.Primary && receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.Token);
					}
					return this.GetReader(i, flag2);
				}
			}
			return null;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0004F72D File Offset: 0x0004D92D
		private void OnDuplicateId(string id)
		{
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("DuplicateIdInMessageToBeVerified", new object[]
			{
				id
			})), this.securityHeader.SecurityVerifiedMessage);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0004F758 File Offset: 0x0004D958
		public void SetBindingMode(int index, ReceiveSecurityHeaderBindingModes bindingMode)
		{
			this.elements[index].bindingMode = bindingMode;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0004F76C File Offset: 0x0004D96C
		public void SetElement(int index, object element)
		{
			this.elements[index].element = element;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004F780 File Offset: 0x0004D980
		public void ReplaceHeaderEntry(int index, ReceiveSecurityHeaderEntry element)
		{
			this.elements[index] = element;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004F78F File Offset: 0x0004D98F
		public void SetElementAfterDecryption(int index, ReceiveSecurityHeaderElementCategory elementCategory, object element, ReceiveSecurityHeaderBindingModes bindingMode, string id, byte[] decryptedBuffer, TokenTracker supportingTokenTracker)
		{
			if (id != null)
			{
				this.VerifyIdUniquenessInSecurityHeader(id);
			}
			this.elements[index].PreserveIdBeforeDecryption();
			this.elements[index].SetElement(elementCategory, element, bindingMode, id, true, decryptedBuffer, supportingTokenTracker);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004F7CA File Offset: 0x0004D9CA
		public void SetSignatureAfterDecryption(int index, SignedXml signedXml, byte[] decryptedBuffer)
		{
			this.SetElementAfterDecryption(index, ReceiveSecurityHeaderElementCategory.Signature, signedXml, ReceiveSecurityHeaderBindingModes.Unknown, signedXml.Id, decryptedBuffer, null);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004F7DE File Offset: 0x0004D9DE
		public void SetSignatureConfirmationAfterDecryption(int index, ISignatureValueSecurityElement signatureConfirmationElement, byte[] decryptedBuffer)
		{
			this.SetElementAfterDecryption(index, ReceiveSecurityHeaderElementCategory.SignatureConfirmation, signatureConfirmationElement, ReceiveSecurityHeaderBindingModes.Unknown, signatureConfirmationElement.Id, decryptedBuffer, null);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004F7F2 File Offset: 0x0004D9F2
		internal void SetSigned(int index)
		{
			this.elements[index].signed = true;
			if (this.elements[index].supportingTokenTracker != null)
			{
				this.elements[index].supportingTokenTracker.IsSigned = true;
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004F830 File Offset: 0x0004DA30
		public void SetTimestampSigned(string id)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.elements[i].elementCategory == ReceiveSecurityHeaderElementCategory.Timestamp && this.elements[i].id == id)
				{
					this.SetSigned(i);
				}
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0004F882 File Offset: 0x0004DA82
		public void SetTokenAfterDecryption(int index, SecurityToken token, ReceiveSecurityHeaderBindingModes mode, byte[] decryptedBuffer, TokenTracker supportingTokenTracker)
		{
			this.SetElementAfterDecryption(index, ReceiveSecurityHeaderElementCategory.Token, token, mode, token.Id, decryptedBuffer, supportingTokenTracker);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0004F898 File Offset: 0x0004DA98
		internal bool TryGetTokenElementIndexFromStrId(string strId, out int index)
		{
			index = -1;
			SecurityKeyIdentifierClause securityKeyIdentifierClause = null;
			for (int i = 0; i < this.Count; i++)
			{
				if (this.GetElementCategory(i) == ReceiveSecurityHeaderElementCategory.SecurityTokenReference)
				{
					securityKeyIdentifierClause = (this.GetElement(i) as SecurityKeyIdentifierClause);
					if (securityKeyIdentifierClause.Id == strId)
					{
						break;
					}
				}
			}
			if (securityKeyIdentifierClause == null)
			{
				return false;
			}
			for (int j = 0; j < this.Count; j++)
			{
				if (this.GetElementCategory(j) == ReceiveSecurityHeaderElementCategory.Token)
				{
					SecurityToken securityToken = this.GetElement(j) as SecurityToken;
					if (securityToken.MatchesKeyIdentifierClause(securityKeyIdentifierClause))
					{
						index = j;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004F91C File Offset: 0x0004DB1C
		public void VerifyUniquenessAndSetBodyId(string id)
		{
			if (id != null)
			{
				this.VerifyIdUniquenessInSecurityHeader(id);
				this.VerifyIdUniquenessInMessageHeadersAndBody(id, this.headerIds.Length);
				this.bodyId = id;
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0004F93E File Offset: 0x0004DB3E
		public void VerifyUniquenessAndSetBodyContentId(string id)
		{
			if (id != null)
			{
				this.VerifyIdUniquenessInSecurityHeader(id);
				this.VerifyIdUniquenessInMessageHeadersAndBody(id, this.headerIds.Length);
				this.bodyContentId = id;
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0004F960 File Offset: 0x0004DB60
		public void VerifyUniquenessAndSetDecryptedHeaderId(string id, int headerIndex)
		{
			if (id != null)
			{
				this.VerifyIdUniquenessInSecurityHeader(id);
				this.VerifyIdUniquenessInMessageHeadersAndBody(id, headerIndex);
				if (this.predecryptionHeaderIds == null)
				{
					this.predecryptionHeaderIds = new string[this.headerIds.Length];
				}
				this.predecryptionHeaderIds[headerIndex] = this.headerIds[headerIndex];
				this.headerIds[headerIndex] = id;
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0004F9B3 File Offset: 0x0004DBB3
		public void VerifyUniquenessAndSetHeaderId(string id, int headerIndex)
		{
			if (id != null)
			{
				this.VerifyIdUniquenessInSecurityHeader(id);
				this.VerifyIdUniquenessInMessageHeadersAndBody(id, headerIndex);
				this.headerIds[headerIndex] = id;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0004F9D0 File Offset: 0x0004DBD0
		private void VerifyIdUniquenessInHeaderIdTable(string id, int headerCount, string[] headerIdTable)
		{
			for (int i = 0; i < headerCount; i++)
			{
				if (headerIdTable[i] == id)
				{
					this.OnDuplicateId(id);
				}
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0004F9FC File Offset: 0x0004DBFC
		private void VerifyIdUniquenessInSecurityHeader(string id)
		{
			for (int i = 0; i < this.count; i++)
			{
				if (this.elements[i].id == id || this.elements[i].encryptedFormId == id)
				{
					this.OnDuplicateId(id);
				}
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0004FA54 File Offset: 0x0004DC54
		private void VerifyIdUniquenessInMessageHeadersAndBody(string id, int headerCount)
		{
			this.VerifyIdUniquenessInHeaderIdTable(id, headerCount, this.headerIds);
			if (this.predecryptionHeaderIds != null)
			{
				this.VerifyIdUniquenessInHeaderIdTable(id, headerCount, this.predecryptionHeaderIds);
			}
			if (this.bodyId == id || this.bodyContentId == id)
			{
				this.OnDuplicateId(id);
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0004FAA8 File Offset: 0x0004DCA8
		XmlDictionaryReader ISignatureReaderProvider.GetReader(object callbackContext)
		{
			int index = (int)callbackContext;
			return this.GetReader(index, false);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0004FAC4 File Offset: 0x0004DCC4
		public void VerifySignatureConfirmationWasFound()
		{
			for (int i = 0; i < this.count; i++)
			{
				ReceiveSecurityHeaderEntry receiveSecurityHeaderEntry;
				this.GetElementEntry(i, out receiveSecurityHeaderEntry);
				if (receiveSecurityHeaderEntry.elementCategory == ReceiveSecurityHeaderElementCategory.SignatureConfirmation)
				{
					return;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SignatureConfirmationWasExpected")));
		}

		// Token: 0x04001B1C RID: 6940
		private const int InitialCapacity = 8;

		// Token: 0x04001B1D RID: 6941
		private readonly ReceiveSecurityHeader securityHeader;

		// Token: 0x04001B1E RID: 6942
		private ReceiveSecurityHeaderEntry[] elements;

		// Token: 0x04001B1F RID: 6943
		private int count;

		// Token: 0x04001B20 RID: 6944
		private readonly string[] headerIds;

		// Token: 0x04001B21 RID: 6945
		private string[] predecryptionHeaderIds;

		// Token: 0x04001B22 RID: 6946
		private string bodyId;

		// Token: 0x04001B23 RID: 6947
		private string bodyContentId;

		// Token: 0x04001B24 RID: 6948
		private bool isPrimaryTokenSigned;
	}
}
