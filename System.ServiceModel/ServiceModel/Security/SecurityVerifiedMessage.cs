using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B3 RID: 691
	internal sealed class SecurityVerifiedMessage : DelegatingMessage
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x00051A0C File Offset: 0x0004FC0C
		public SecurityVerifiedMessage(Message messageToProcess, ReceiveSecurityHeader securityHeader) : base(messageToProcess)
		{
			this.securityHeader = securityHeader;
			if (securityHeader.RequireMessageProtection)
			{
				BufferedMessage bufferedMessage = base.InnerMessage as BufferedMessage;
				XmlDictionaryReader reader;
				if (bufferedMessage != null && this.Headers.ContainsOnlyBufferedMessageHeaders)
				{
					reader = bufferedMessage.GetMessageReader();
				}
				else
				{
					this.messageBuffer = new XmlBuffer(int.MaxValue);
					XmlDictionaryWriter writer = this.messageBuffer.OpenSection(this.securityHeader.ReaderQuotas);
					base.InnerMessage.WriteMessage(writer);
					this.messageBuffer.CloseSection();
					this.messageBuffer.Close();
					reader = this.messageBuffer.GetReader(0);
				}
				this.MoveToSecurityHeader(reader, securityHeader.HeaderIndex, true);
				this.cachedReaderAtSecurityHeader = reader;
				this.state = SecurityVerifiedMessage.BodyState.Buffered;
				return;
			}
			this.envelopeAttributes = XmlAttributeHolder.emptyArray;
			this.headerAttributes = XmlAttributeHolder.emptyArray;
			this.bodyAttributes = XmlAttributeHolder.emptyArray;
			this.canDelegateCreateBufferedCopyToInnerMessage = true;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00051AF2 File Offset: 0x0004FCF2
		public override bool IsEmpty
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				if (!this.bodyDecrypted)
				{
					return base.InnerMessage.IsEmpty;
				}
				this.EnsureDecryptedBodyStatusDetermined();
				return this.isDecryptedBodyEmpty;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00051B29 File Offset: 0x0004FD29
		public override bool IsFault
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				if (!this.bodyDecrypted)
				{
					return base.InnerMessage.IsFault;
				}
				this.EnsureDecryptedBodyStatusDetermined();
				return this.isDecryptedBodyFault;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00051B60 File Offset: 0x0004FD60
		internal byte[] PrimarySignatureValue
		{
			get
			{
				return this.securityHeader.PrimarySignatureValue;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00051B6D File Offset: 0x0004FD6D
		internal ReceiveSecurityHeader ReceivedSecurityHeader
		{
			get
			{
				return this.securityHeader;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00051B75 File Offset: 0x0004FD75
		private Exception CreateBadStateException(string operation)
		{
			return new InvalidOperationException(SR.GetString("MessageBodyOperationNotValidInBodyState", new object[]
			{
				operation,
				this.state
			}));
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00051BA0 File Offset: 0x0004FDA0
		public XmlDictionaryReader CreateFullBodyReader()
		{
			SecurityVerifiedMessage.BodyState bodyState = this.state;
			if (bodyState == SecurityVerifiedMessage.BodyState.Buffered)
			{
				return this.CreateFullBodyReaderFromBufferedState();
			}
			if (bodyState != SecurityVerifiedMessage.BodyState.Decrypted)
			{
				throw TraceUtility.ThrowHelperError(this.CreateBadStateException("CreateFullBodyReader"), this);
			}
			return this.CreateFullBodyReaderFromDecryptedState();
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00051BE0 File Offset: 0x0004FDE0
		private XmlDictionaryReader CreateFullBodyReaderFromBufferedState()
		{
			if (this.messageBuffer != null)
			{
				XmlDictionaryReader reader = this.messageBuffer.GetReader(0);
				this.MoveToBody(reader);
				return reader;
			}
			return ((BufferedMessage)base.InnerMessage).GetBufferedReaderAtBody();
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00051C1C File Offset: 0x0004FE1C
		private XmlDictionaryReader CreateFullBodyReaderFromDecryptedState()
		{
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(this.decryptedBuffer, 0, this.decryptedBuffer.Length, this.securityHeader.ReaderQuotas);
			this.MoveToBody(xmlDictionaryReader);
			return xmlDictionaryReader;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00051C54 File Offset: 0x0004FE54
		private void EnsureDecryptedBodyStatusDetermined()
		{
			if (!this.isDecryptedBodyStatusDetermined)
			{
				XmlDictionaryReader xmlDictionaryReader = this.CreateFullBodyReader();
				if (Message.ReadStartBody(xmlDictionaryReader, base.InnerMessage.Version.Envelope, out this.isDecryptedBodyFault, out this.isDecryptedBodyEmpty))
				{
					this.cachedDecryptedBodyContentReader = xmlDictionaryReader;
				}
				else
				{
					xmlDictionaryReader.Close();
				}
				this.isDecryptedBodyStatusDetermined = true;
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00051CAA File Offset: 0x0004FEAA
		public XmlAttributeHolder[] GetEnvelopeAttributes()
		{
			return this.envelopeAttributes;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00051CB2 File Offset: 0x0004FEB2
		public XmlAttributeHolder[] GetHeaderAttributes()
		{
			return this.headerAttributes;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00051CBA File Offset: 0x0004FEBA
		private XmlDictionaryReader GetReaderAtEnvelope()
		{
			if (this.messageBuffer != null)
			{
				return this.messageBuffer.GetReader(0);
			}
			return ((BufferedMessage)base.InnerMessage).GetMessageReader();
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00051CE4 File Offset: 0x0004FEE4
		public XmlDictionaryReader GetReaderAtFirstHeader()
		{
			XmlDictionaryReader readerAtEnvelope = this.GetReaderAtEnvelope();
			this.MoveToHeaderBlock(readerAtEnvelope, false);
			readerAtEnvelope.ReadStartElement();
			return readerAtEnvelope;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00051D08 File Offset: 0x0004FF08
		public XmlDictionaryReader GetReaderAtSecurityHeader()
		{
			if (this.cachedReaderAtSecurityHeader != null)
			{
				XmlDictionaryReader result = this.cachedReaderAtSecurityHeader;
				this.cachedReaderAtSecurityHeader = null;
				return result;
			}
			return this.Headers.GetReaderAtHeader(this.securityHeader.HeaderIndex);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00051D44 File Offset: 0x0004FF44
		private void MoveToBody(XmlDictionaryReader reader)
		{
			if (reader.NodeType != XmlNodeType.Element)
			{
				reader.MoveToContent();
			}
			reader.ReadStartElement();
			if (reader.IsStartElement(XD.MessageDictionary.Header, this.Version.Envelope.DictionaryNamespace))
			{
				reader.Skip();
			}
			if (reader.NodeType != XmlNodeType.Element)
			{
				reader.MoveToContent();
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00051DA0 File Offset: 0x0004FFA0
		private void MoveToHeaderBlock(XmlDictionaryReader reader, bool captureAttributes)
		{
			if (reader.NodeType != XmlNodeType.Element)
			{
				reader.MoveToContent();
			}
			if (captureAttributes)
			{
				this.envelopePrefix = reader.Prefix;
				this.envelopeAttributes = XmlAttributeHolder.ReadAttributes(reader);
			}
			reader.ReadStartElement();
			reader.MoveToStartElement(XD.MessageDictionary.Header, this.Version.Envelope.DictionaryNamespace);
			if (captureAttributes)
			{
				this.headerAttributes = XmlAttributeHolder.ReadAttributes(reader);
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00051E0D File Offset: 0x0005000D
		private void MoveToSecurityHeader(XmlDictionaryReader reader, int headerIndex, bool captureAttributes)
		{
			this.MoveToHeaderBlock(reader, captureAttributes);
			reader.ReadStartElement();
			for (;;)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.MoveToContent();
				}
				if (headerIndex == 0)
				{
					break;
				}
				reader.Skip();
				headerIndex--;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00051E3D File Offset: 0x0005003D
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			if (this.state == SecurityVerifiedMessage.BodyState.Created)
			{
				base.OnBodyToString(writer);
				return;
			}
			this.OnWriteBodyContents(writer);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00051E58 File Offset: 0x00050058
		protected override void OnClose()
		{
			if (this.cachedDecryptedBodyContentReader != null)
			{
				try
				{
					this.cachedDecryptedBodyContentReader.Close();
				}
				catch (IOException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				}
				finally
				{
					this.cachedDecryptedBodyContentReader = null;
				}
			}
			if (this.cachedReaderAtSecurityHeader != null)
			{
				try
				{
					this.cachedReaderAtSecurityHeader.Close();
				}
				catch (IOException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Warning);
				}
				finally
				{
					this.cachedReaderAtSecurityHeader = null;
				}
			}
			this.messageBuffer = null;
			this.decryptedBuffer = null;
			this.state = SecurityVerifiedMessage.BodyState.Disposed;
			base.InnerMessage.Close();
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00051F08 File Offset: 0x00050108
		protected override XmlDictionaryReader OnGetReaderAtBodyContents()
		{
			if (this.state == SecurityVerifiedMessage.BodyState.Created)
			{
				return base.InnerMessage.GetReaderAtBodyContents();
			}
			if (this.bodyDecrypted)
			{
				this.EnsureDecryptedBodyStatusDetermined();
			}
			if (this.cachedDecryptedBodyContentReader != null)
			{
				XmlDictionaryReader result = this.cachedDecryptedBodyContentReader;
				this.cachedDecryptedBodyContentReader = null;
				return result;
			}
			XmlDictionaryReader xmlDictionaryReader = this.CreateFullBodyReader();
			xmlDictionaryReader.ReadStartElement();
			xmlDictionaryReader.MoveToContent();
			return xmlDictionaryReader;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00051F64 File Offset: 0x00050164
		protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
		{
			if (this.canDelegateCreateBufferedCopyToInnerMessage && base.InnerMessage is BufferedMessage)
			{
				return base.InnerMessage.CreateBufferedCopy(maxBufferSize);
			}
			return base.OnCreateBufferedCopy(maxBufferSize);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00051F8F File Offset: 0x0005018F
		internal void OnMessageProtectionPassComplete(bool atLeastOneHeaderOrBodyEncrypted)
		{
			this.canDelegateCreateBufferedCopyToInnerMessage = !atLeastOneHeaderOrBodyEncrypted;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00051F9C File Offset: 0x0005019C
		internal void OnUnencryptedPart(string name, string ns)
		{
			if (ns == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredMessagePartNotEncrypted", new object[]
				{
					name
				})), this);
			}
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredMessagePartNotEncryptedNs", new object[]
			{
				name,
				ns
			})), this);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00051FF0 File Offset: 0x000501F0
		internal void OnUnsignedPart(string name, string ns)
		{
			if (ns == null)
			{
				throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredMessagePartNotSigned", new object[]
				{
					name
				})), this);
			}
			throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("RequiredMessagePartNotSignedNs", new object[]
			{
				name,
				ns
			})), this);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00052044 File Offset: 0x00050244
		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			if (this.state == SecurityVerifiedMessage.BodyState.Created)
			{
				base.InnerMessage.WriteStartBody(writer);
				return;
			}
			XmlDictionaryReader xmlDictionaryReader = this.CreateFullBodyReader();
			xmlDictionaryReader.MoveToContent();
			writer.WriteStartElement(xmlDictionaryReader.Prefix, xmlDictionaryReader.LocalName, xmlDictionaryReader.NamespaceURI);
			writer.WriteAttributes(xmlDictionaryReader, false);
			xmlDictionaryReader.Close();
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0005209C File Offset: 0x0005029C
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			if (this.state == SecurityVerifiedMessage.BodyState.Created)
			{
				base.InnerMessage.WriteBodyContents(writer);
				return;
			}
			XmlDictionaryReader xmlDictionaryReader = this.CreateFullBodyReader();
			xmlDictionaryReader.ReadStartElement();
			while (xmlDictionaryReader.NodeType != XmlNodeType.EndElement)
			{
				writer.WriteNode(xmlDictionaryReader, false);
			}
			xmlDictionaryReader.ReadEndElement();
			xmlDictionaryReader.Close();
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x000520EB File Offset: 0x000502EB
		public void SetBodyPrefixAndAttributes(XmlDictionaryReader bodyReader)
		{
			this.bodyPrefix = bodyReader.Prefix;
			this.bodyAttributes = XmlAttributeHolder.ReadAttributes(bodyReader);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00052108 File Offset: 0x00050308
		public void SetDecryptedBody(byte[] decryptedBodyContent)
		{
			if (this.state != SecurityVerifiedMessage.BodyState.Buffered)
			{
				throw TraceUtility.ThrowHelperError(this.CreateBadStateException("SetDecryptedBody"), this);
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream);
			xmlDictionaryWriter.WriteStartElement(this.envelopePrefix, XD.MessageDictionary.Envelope, this.Version.Envelope.DictionaryNamespace);
			XmlAttributeHolder.WriteAttributes(this.envelopeAttributes, xmlDictionaryWriter);
			xmlDictionaryWriter.WriteStartElement(this.bodyPrefix, XD.MessageDictionary.Body, this.Version.Envelope.DictionaryNamespace);
			XmlAttributeHolder.WriteAttributes(this.bodyAttributes, xmlDictionaryWriter);
			xmlDictionaryWriter.WriteString(" ");
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.Flush();
			this.decryptedBuffer = ContextImportHelper.SpliceBuffers(decryptedBodyContent, memoryStream.GetBuffer(), (int)memoryStream.Length, 2);
			this.bodyDecrypted = true;
			this.state = SecurityVerifiedMessage.BodyState.Decrypted;
		}

		// Token: 0x04001B65 RID: 7013
		private byte[] decryptedBuffer;

		// Token: 0x04001B66 RID: 7014
		private XmlDictionaryReader cachedDecryptedBodyContentReader;

		// Token: 0x04001B67 RID: 7015
		private XmlAttributeHolder[] envelopeAttributes;

		// Token: 0x04001B68 RID: 7016
		private XmlAttributeHolder[] headerAttributes;

		// Token: 0x04001B69 RID: 7017
		private XmlAttributeHolder[] bodyAttributes;

		// Token: 0x04001B6A RID: 7018
		private string envelopePrefix;

		// Token: 0x04001B6B RID: 7019
		private bool bodyDecrypted;

		// Token: 0x04001B6C RID: 7020
		private SecurityVerifiedMessage.BodyState state;

		// Token: 0x04001B6D RID: 7021
		private string bodyPrefix;

		// Token: 0x04001B6E RID: 7022
		private bool isDecryptedBodyStatusDetermined;

		// Token: 0x04001B6F RID: 7023
		private bool isDecryptedBodyFault;

		// Token: 0x04001B70 RID: 7024
		private bool isDecryptedBodyEmpty;

		// Token: 0x04001B71 RID: 7025
		private XmlDictionaryReader cachedReaderAtSecurityHeader;

		// Token: 0x04001B72 RID: 7026
		private readonly ReceiveSecurityHeader securityHeader;

		// Token: 0x04001B73 RID: 7027
		private XmlBuffer messageBuffer;

		// Token: 0x04001B74 RID: 7028
		private bool canDelegateCreateBufferedCopyToInnerMessage;

		// Token: 0x02000B47 RID: 2887
		private enum BodyState
		{
			// Token: 0x0400402E RID: 16430
			Created,
			// Token: 0x0400402F RID: 16431
			Buffered,
			// Token: 0x04004030 RID: 16432
			Decrypted,
			// Token: 0x04004031 RID: 16433
			Disposed
		}
	}
}
