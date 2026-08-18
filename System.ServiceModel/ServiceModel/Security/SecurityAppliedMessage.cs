using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.IO;
using System.Runtime;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002AE RID: 686
	internal sealed class SecurityAppliedMessage : DelegatingMessage
	{
		// Token: 0x06001536 RID: 5430 RVA: 0x0004FBA3 File Offset: 0x0004DDA3
		public SecurityAppliedMessage(Message messageToProcess, SendSecurityHeader securityHeader, bool signBody, bool encryptBody) : base(messageToProcess)
		{
			this.securityHeader = securityHeader;
			this.bodyProtectionMode = MessagePartProtectionModeHelper.GetProtectionMode(signBody, encryptBody, securityHeader.SignThenEncrypt);
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0004FBD2 File Offset: 0x0004DDD2
		public string BodyId
		{
			get
			{
				return this.bodyId;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0004FBDA File Offset: 0x0004DDDA
		public MessagePartProtectionMode BodyProtectionMode
		{
			get
			{
				return this.bodyProtectionMode;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0004FBE2 File Offset: 0x0004DDE2
		internal byte[] PrimarySignatureValue
		{
			get
			{
				return this.securityHeader.PrimarySignatureValue;
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0004FBEF File Offset: 0x0004DDEF
		private Exception CreateBadStateException(string operation)
		{
			return new InvalidOperationException(SR.GetString("MessageBodyOperationNotValidInBodyState", new object[]
			{
				operation,
				this.state
			}));
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0004FC18 File Offset: 0x0004DE18
		private void EnsureUniqueSecurityApplication()
		{
			if (this.delayedApplicationHandled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DelayedSecurityApplicationAlreadyCompleted")));
			}
			this.delayedApplicationHandled = true;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0004FC43 File Offset: 0x0004DE43
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			if (this.state == SecurityAppliedMessage.BodyState.Created || this.fullBodyFragment != null)
			{
				base.OnBodyToString(writer);
				return;
			}
			this.OnWriteBodyContents(writer);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0004FC64 File Offset: 0x0004DE64
		protected override void OnClose()
		{
			try
			{
				base.InnerMessage.Close();
			}
			finally
			{
				this.fullBodyBuffer = null;
				this.bodyAttributes = null;
				this.encryptedBodyContent = null;
				this.state = SecurityAppliedMessage.BodyState.Disposed;
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0004FCAC File Offset: 0x0004DEAC
		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			if (this.startBodyFragment != null || this.fullBodyFragment != null)
			{
				this.WriteStartInnerMessageWithId(writer);
				return;
			}
			switch (this.state)
			{
			case SecurityAppliedMessage.BodyState.Created:
			case SecurityAppliedMessage.BodyState.Encrypted:
				base.InnerMessage.WriteStartBody(writer);
				return;
			case SecurityAppliedMessage.BodyState.Signed:
			case SecurityAppliedMessage.BodyState.EncryptedThenSigned:
			{
				XmlDictionaryReader reader = this.fullBodyBuffer.GetReader(0);
				writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
				writer.WriteAttributes(reader, false);
				reader.Close();
				return;
			}
			case SecurityAppliedMessage.BodyState.SignedThenEncrypted:
				writer.WriteStartElement(this.bodyPrefix, XD.MessageDictionary.Body, this.Version.Envelope.DictionaryNamespace);
				if (this.bodyAttributes != null)
				{
					XmlAttributeHolder.WriteAttributes(this.bodyAttributes, writer);
				}
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateBadStateException("OnWriteStartBody"));
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0004FD88 File Offset: 0x0004DF88
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			switch (this.state)
			{
			case SecurityAppliedMessage.BodyState.Created:
				base.InnerMessage.WriteBodyContents(writer);
				return;
			case SecurityAppliedMessage.BodyState.Signed:
			case SecurityAppliedMessage.BodyState.EncryptedThenSigned:
			{
				XmlDictionaryReader reader = this.fullBodyBuffer.GetReader(0);
				reader.ReadStartElement();
				while (reader.NodeType != XmlNodeType.EndElement)
				{
					writer.WriteNode(reader, false);
				}
				reader.ReadEndElement();
				reader.Close();
				return;
			}
			case SecurityAppliedMessage.BodyState.SignedThenEncrypted:
			case SecurityAppliedMessage.BodyState.Encrypted:
				this.encryptedBodyContent.WriteTo(writer, ServiceModelDictionaryManager.Instance);
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateBadStateException("OnWriteBodyContents"));
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0004FE20 File Offset: 0x0004E020
		protected override void OnWriteMessage(XmlDictionaryWriter writer)
		{
			this.AttachChannelBindingTokenIfFound();
			this.EnsureUniqueSecurityApplication();
			SecurityAppliedMessage.MessagePrefixGenerator prefixGenerator = new SecurityAppliedMessage.MessagePrefixGenerator(writer);
			this.securityHeader.StartSecurityApplication();
			this.Headers.Add(this.securityHeader);
			base.InnerMessage.WriteStartEnvelope(writer);
			this.Headers.RemoveAt(this.Headers.Count - 1);
			this.securityHeader.ApplyBodySecurity(writer, prefixGenerator);
			base.InnerMessage.WriteStartHeaders(writer);
			this.securityHeader.ApplySecurityAndWriteHeaders(this.Headers, writer, prefixGenerator);
			this.securityHeader.RemoveSignatureEncryptionIfAppropriate();
			this.securityHeader.CompleteSecurityApplication();
			this.securityHeader.WriteHeader(writer, this.Version);
			writer.WriteEndElement();
			if (this.fullBodyFragment != null)
			{
				((IFragmentCapableXmlDictionaryWriter)writer).WriteFragment(this.fullBodyFragment, 0, this.fullBodyFragmentLength);
			}
			else
			{
				if (this.startBodyFragment != null)
				{
					((IFragmentCapableXmlDictionaryWriter)writer).WriteFragment(this.startBodyFragment.GetBuffer(), 0, (int)this.startBodyFragment.Length);
				}
				else
				{
					this.OnWriteStartBody(writer);
				}
				this.OnWriteBodyContents(writer);
				if (this.endBodyFragment != null)
				{
					((IFragmentCapableXmlDictionaryWriter)writer).WriteFragment(this.endBodyFragment.GetBuffer(), 0, (int)this.endBodyFragment.Length);
				}
				else
				{
					writer.WriteEndElement();
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0004FF70 File Offset: 0x0004E170
		private void AttachChannelBindingTokenIfFound()
		{
			ChannelBindingMessageProperty channelBindingMessageProperty = null;
			ChannelBindingMessageProperty.TryGet(base.InnerMessage, out channelBindingMessageProperty);
			if (channelBindingMessageProperty != null && this.securityHeader.ElementContainer != null && this.securityHeader.ElementContainer.EndorsingSupportingTokens != null)
			{
				foreach (SecurityToken securityToken in this.securityHeader.ElementContainer.EndorsingSupportingTokens)
				{
					ProviderBackedSecurityToken providerBackedSecurityToken = securityToken as ProviderBackedSecurityToken;
					if (providerBackedSecurityToken != null)
					{
						providerBackedSecurityToken.ChannelBinding = channelBindingMessageProperty.ChannelBinding;
					}
				}
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00050010 File Offset: 0x0004E210
		private void SetBodyId()
		{
			this.bodyId = base.InnerMessage.GetBodyAttribute("Id", this.securityHeader.StandardsManager.IdManager.DefaultIdNamespaceUri);
			if (this.bodyId == null)
			{
				this.bodyId = this.securityHeader.GenerateId();
				this.bodyIdInserted = true;
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00050068 File Offset: 0x0004E268
		public void WriteBodyToEncrypt(EncryptedData encryptedData, SymmetricAlgorithm algorithm)
		{
			encryptedData.Id = this.securityHeader.GenerateId();
			SecurityAppliedMessage.BodyContentHelper bodyContentHelper = default(SecurityAppliedMessage.BodyContentHelper);
			XmlDictionaryWriter writer = bodyContentHelper.CreateWriter();
			base.InnerMessage.WriteBodyContents(writer);
			encryptedData.SetUpEncryption(algorithm, bodyContentHelper.ExtractResult());
			this.encryptedBodyContent = encryptedData;
			this.state = SecurityAppliedMessage.BodyState.Encrypted;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000500C0 File Offset: 0x0004E2C0
		public void WriteBodyToEncryptThenSign(Stream canonicalStream, EncryptedData encryptedData, SymmetricAlgorithm algorithm)
		{
			encryptedData.Id = this.securityHeader.GenerateId();
			this.SetBodyId();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(Stream.Null);
			xmlDictionaryWriter.WriteStartElement("a");
			MemoryStream memoryStream = new MemoryStream();
			((IFragmentCapableXmlDictionaryWriter)xmlDictionaryWriter).StartFragment(memoryStream, true);
			base.InnerMessage.WriteBodyContents(xmlDictionaryWriter);
			((IFragmentCapableXmlDictionaryWriter)xmlDictionaryWriter).EndFragment();
			xmlDictionaryWriter.WriteEndElement();
			memoryStream.Flush();
			encryptedData.SetUpEncryption(algorithm, new ArraySegment<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
			this.fullBodyBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter2 = this.fullBodyBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			xmlDictionaryWriter2.StartCanonicalization(canonicalStream, false, null);
			this.WriteStartInnerMessageWithId(xmlDictionaryWriter2);
			encryptedData.WriteTo(xmlDictionaryWriter2, ServiceModelDictionaryManager.Instance);
			xmlDictionaryWriter2.WriteEndElement();
			xmlDictionaryWriter2.EndCanonicalization();
			xmlDictionaryWriter2.Flush();
			this.fullBodyBuffer.CloseSection();
			this.fullBodyBuffer.Close();
			this.state = SecurityAppliedMessage.BodyState.EncryptedThenSigned;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000501B8 File Offset: 0x0004E3B8
		public void WriteBodyToSign(Stream canonicalStream)
		{
			this.SetBodyId();
			this.fullBodyBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = this.fullBodyBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			xmlDictionaryWriter.StartCanonicalization(canonicalStream, false, null);
			this.WriteInnerMessageWithId(xmlDictionaryWriter);
			xmlDictionaryWriter.EndCanonicalization();
			xmlDictionaryWriter.Flush();
			this.fullBodyBuffer.CloseSection();
			this.fullBodyBuffer.Close();
			this.state = SecurityAppliedMessage.BodyState.Signed;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00050228 File Offset: 0x0004E428
		public void WriteBodyToSignThenEncrypt(Stream canonicalStream, EncryptedData encryptedData, SymmetricAlgorithm algorithm)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			this.WriteBodyToSignThenEncryptWithFragments(canonicalStream, false, null, encryptedData, algorithm, xmlDictionaryWriter);
			((IFragmentCapableXmlDictionaryWriter)xmlDictionaryWriter).WriteFragment(this.startBodyFragment.GetBuffer(), 0, (int)this.startBodyFragment.Length);
			((IFragmentCapableXmlDictionaryWriter)xmlDictionaryWriter).WriteFragment(this.endBodyFragment.GetBuffer(), 0, (int)this.endBodyFragment.Length);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			this.startBodyFragment = null;
			this.endBodyFragment = null;
			XmlDictionaryReader reader = xmlBuffer.GetReader(0);
			reader.MoveToContent();
			this.bodyPrefix = reader.Prefix;
			if (reader.HasAttributes)
			{
				this.bodyAttributes = XmlAttributeHolder.ReadAttributes(reader);
			}
			reader.Close();
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000502F0 File Offset: 0x0004E4F0
		public void WriteBodyToSignThenEncryptWithFragments(Stream stream, bool includeComments, string[] inclusivePrefixes, EncryptedData encryptedData, SymmetricAlgorithm algorithm, XmlDictionaryWriter writer)
		{
			IFragmentCapableXmlDictionaryWriter fragmentCapableXmlDictionaryWriter = (IFragmentCapableXmlDictionaryWriter)writer;
			this.SetBodyId();
			encryptedData.Id = this.securityHeader.GenerateId();
			this.startBodyFragment = new MemoryStream();
			BufferedOutputStream bufferedOutputStream = new BufferManagerOutputStream("XmlBufferQuotaExceeded", 1024, int.MaxValue, this.securityHeader.StreamBufferManager);
			this.endBodyFragment = new MemoryStream();
			writer.StartCanonicalization(stream, includeComments, inclusivePrefixes);
			fragmentCapableXmlDictionaryWriter.StartFragment(this.startBodyFragment, false);
			this.WriteStartInnerMessageWithId(writer);
			fragmentCapableXmlDictionaryWriter.EndFragment();
			fragmentCapableXmlDictionaryWriter.StartFragment(bufferedOutputStream, true);
			base.InnerMessage.WriteBodyContents(writer);
			fragmentCapableXmlDictionaryWriter.EndFragment();
			fragmentCapableXmlDictionaryWriter.StartFragment(this.endBodyFragment, false);
			writer.WriteEndElement();
			fragmentCapableXmlDictionaryWriter.EndFragment();
			writer.EndCanonicalization();
			int count;
			byte[] array = bufferedOutputStream.ToArray(out count);
			encryptedData.SetUpEncryption(algorithm, new ArraySegment<byte>(array, 0, count));
			this.encryptedBodyContent = encryptedData;
			this.state = SecurityAppliedMessage.BodyState.SignedThenEncrypted;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x000503E0 File Offset: 0x0004E5E0
		public void WriteBodyToSignWithFragments(Stream stream, bool includeComments, string[] inclusivePrefixes, XmlDictionaryWriter writer)
		{
			IFragmentCapableXmlDictionaryWriter fragmentCapableXmlDictionaryWriter = (IFragmentCapableXmlDictionaryWriter)writer;
			this.SetBodyId();
			BufferedOutputStream bufferedOutputStream = new BufferManagerOutputStream("XmlBufferQuotaExceeded", 1024, int.MaxValue, this.securityHeader.StreamBufferManager);
			writer.StartCanonicalization(stream, includeComments, inclusivePrefixes);
			fragmentCapableXmlDictionaryWriter.StartFragment(bufferedOutputStream, false);
			this.WriteStartInnerMessageWithId(writer);
			base.InnerMessage.WriteBodyContents(writer);
			writer.WriteEndElement();
			fragmentCapableXmlDictionaryWriter.EndFragment();
			writer.EndCanonicalization();
			this.fullBodyFragment = bufferedOutputStream.ToArray(out this.fullBodyFragmentLength);
			this.state = SecurityAppliedMessage.BodyState.Signed;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005046F File Offset: 0x0004E66F
		private void WriteInnerMessageWithId(XmlDictionaryWriter writer)
		{
			this.WriteStartInnerMessageWithId(writer);
			base.InnerMessage.WriteBodyContents(writer);
			writer.WriteEndElement();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005048A File Offset: 0x0004E68A
		private void WriteStartInnerMessageWithId(XmlDictionaryWriter writer)
		{
			base.InnerMessage.WriteStartBody(writer);
			if (this.bodyIdInserted)
			{
				this.securityHeader.StandardsManager.IdManager.WriteIdAttribute(writer, this.bodyId);
			}
		}

		// Token: 0x04001B30 RID: 6960
		private string bodyId;

		// Token: 0x04001B31 RID: 6961
		private bool bodyIdInserted;

		// Token: 0x04001B32 RID: 6962
		private string bodyPrefix = "s";

		// Token: 0x04001B33 RID: 6963
		private XmlBuffer fullBodyBuffer;

		// Token: 0x04001B34 RID: 6964
		private ISecurityElement encryptedBodyContent;

		// Token: 0x04001B35 RID: 6965
		private XmlAttributeHolder[] bodyAttributes;

		// Token: 0x04001B36 RID: 6966
		private bool delayedApplicationHandled;

		// Token: 0x04001B37 RID: 6967
		private readonly MessagePartProtectionMode bodyProtectionMode;

		// Token: 0x04001B38 RID: 6968
		private SecurityAppliedMessage.BodyState state;

		// Token: 0x04001B39 RID: 6969
		private readonly SendSecurityHeader securityHeader;

		// Token: 0x04001B3A RID: 6970
		private MemoryStream startBodyFragment;

		// Token: 0x04001B3B RID: 6971
		private MemoryStream endBodyFragment;

		// Token: 0x04001B3C RID: 6972
		private byte[] fullBodyFragment;

		// Token: 0x04001B3D RID: 6973
		private int fullBodyFragmentLength;

		// Token: 0x02000B3E RID: 2878
		private enum BodyState
		{
			// Token: 0x0400401F RID: 16415
			Created,
			// Token: 0x04004020 RID: 16416
			Signed,
			// Token: 0x04004021 RID: 16417
			SignedThenEncrypted,
			// Token: 0x04004022 RID: 16418
			EncryptedThenSigned,
			// Token: 0x04004023 RID: 16419
			Encrypted,
			// Token: 0x04004024 RID: 16420
			Disposed
		}

		// Token: 0x02000B3F RID: 2879
		private struct BodyContentHelper
		{
			// Token: 0x060070C6 RID: 28870 RVA: 0x001A40B7 File Offset: 0x001A22B7
			public XmlDictionaryWriter CreateWriter()
			{
				this.stream = new MemoryStream();
				this.writer = XmlDictionaryWriter.CreateTextWriter(this.stream);
				return this.writer;
			}

			// Token: 0x060070C7 RID: 28871 RVA: 0x001A40DB File Offset: 0x001A22DB
			public ArraySegment<byte> ExtractResult()
			{
				this.writer.Flush();
				return new ArraySegment<byte>(this.stream.GetBuffer(), 0, (int)this.stream.Length);
			}

			// Token: 0x04004025 RID: 16421
			private MemoryStream stream;

			// Token: 0x04004026 RID: 16422
			private XmlDictionaryWriter writer;
		}

		// Token: 0x02000B40 RID: 2880
		private sealed class MessagePrefixGenerator : IPrefixGenerator
		{
			// Token: 0x060070C8 RID: 28872 RVA: 0x001A4105 File Offset: 0x001A2305
			public MessagePrefixGenerator(XmlWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x060070C9 RID: 28873 RVA: 0x001A4114 File Offset: 0x001A2314
			public string GetPrefix(string namespaceUri, int depth, bool isForAttribute)
			{
				return this.writer.LookupPrefix(namespaceUri);
			}

			// Token: 0x04004027 RID: 16423
			private XmlWriter writer;
		}
	}
}
