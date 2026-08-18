using System;
using System.Collections.Generic;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009DA RID: 2522
	internal class MessagePatterns
	{
		// Token: 0x060063B9 RID: 25529 RVA: 0x00174440 File Offset: 0x00172640
		static MessagePatterns()
		{
			BinaryFormatBuilder binaryFormatBuilder = new BinaryFormatBuilder();
			MessageDictionary messageDictionary = XD.MessageDictionary;
			Message12Dictionary message12Dictionary = XD.Message12Dictionary;
			AddressingDictionary addressingDictionary = XD.AddressingDictionary;
			Addressing10Dictionary addressing10Dictionary = XD.Addressing10Dictionary;
			char prefix = "s"[0];
			char prefix2 = "a"[0];
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.Envelope.Key));
			binaryFormatBuilder.AppendDictionaryXmlnsAttribute(prefix, binaryFormatBuilder.GetStaticKey(message12Dictionary.Namespace.Key));
			binaryFormatBuilder.AppendDictionaryXmlnsAttribute(prefix2, binaryFormatBuilder.GetStaticKey(addressing10Dictionary.Namespace.Key));
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.Header.Key));
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.Action.Key));
			binaryFormatBuilder.AppendPrefixDictionaryAttribute(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.MustUnderstand.Key), '1');
			binaryFormatBuilder.AppendDictionaryTextWithEndElement();
			MessagePatterns.commonFragment = binaryFormatBuilder.ToByteArray();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.MessageId.Key));
			binaryFormatBuilder.AppendUniqueIDWithEndElement();
			MessagePatterns.requestFragment1 = binaryFormatBuilder.ToByteArray();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.ReplyTo.Key));
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.Address.Key));
			binaryFormatBuilder.AppendDictionaryTextWithEndElement(binaryFormatBuilder.GetStaticKey(addressing10Dictionary.Anonymous.Key));
			binaryFormatBuilder.AppendEndElement();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.To.Key));
			binaryFormatBuilder.AppendPrefixDictionaryAttribute(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.MustUnderstand.Key), '1');
			binaryFormatBuilder.AppendDictionaryTextWithEndElement(binaryFormatBuilder.GetSessionKey(1));
			binaryFormatBuilder.AppendEndElement();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.Body.Key));
			MessagePatterns.requestFragment2 = binaryFormatBuilder.ToByteArray();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.RelatesTo.Key));
			binaryFormatBuilder.AppendUniqueIDWithEndElement();
			MessagePatterns.responseFragment1 = binaryFormatBuilder.ToByteArray();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix2, binaryFormatBuilder.GetStaticKey(addressingDictionary.To.Key));
			binaryFormatBuilder.AppendPrefixDictionaryAttribute(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.MustUnderstand.Key), '1');
			binaryFormatBuilder.AppendDictionaryTextWithEndElement(binaryFormatBuilder.GetStaticKey(addressing10Dictionary.Anonymous.Key));
			binaryFormatBuilder.AppendEndElement();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.Body.Key));
			MessagePatterns.responseFragment2 = binaryFormatBuilder.ToByteArray();
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.Envelope.Key));
			binaryFormatBuilder.AppendDictionaryXmlnsAttribute(prefix, binaryFormatBuilder.GetStaticKey(message12Dictionary.Namespace.Key));
			binaryFormatBuilder.AppendDictionaryXmlnsAttribute(prefix2, binaryFormatBuilder.GetStaticKey(addressing10Dictionary.Namespace.Key));
			binaryFormatBuilder.AppendPrefixDictionaryElement(prefix, binaryFormatBuilder.GetStaticKey(messageDictionary.Body.Key));
			MessagePatterns.bodyFragment = binaryFormatBuilder.ToByteArray();
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x00174725 File Offset: 0x00172925
		public MessagePatterns(IXmlDictionary dictionary, XmlBinaryReaderSession readerSession, MessageVersion messageVersion)
		{
			this.dictionary = dictionary;
			this.readerSession = readerSession;
			this.messageVersion = messageVersion;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x00174744 File Offset: 0x00172944
		public Message TryCreateMessage(byte[] buffer, int offset, int size, BufferManager bufferManager, BufferedMessageData messageData)
		{
			int num = offset;
			int num2 = size;
			int num3 = BinaryFormatParser.MatchBytes(buffer, num, num2, MessagePatterns.commonFragment);
			if (num3 == 0)
			{
				return null;
			}
			num += num3;
			num2 -= num3;
			num3 = BinaryFormatParser.MatchKey(buffer, num, num2);
			if (num3 == 0)
			{
				return null;
			}
			int offset2 = num;
			int num4 = num3;
			num += num3;
			num2 -= num3;
			num3 = BinaryFormatParser.MatchBytes(buffer, num, num2, MessagePatterns.requestFragment1);
			MessageIDHeader messageIDHeader;
			RelatesToHeader relatesToHeader;
			XmlDictionaryString anonymous;
			int num6;
			if (num3 != 0)
			{
				num += num3;
				num2 -= num3;
				num3 = BinaryFormatParser.MatchUniqueID(buffer, num, num2);
				if (num3 == 0)
				{
					return null;
				}
				int offset3 = num;
				int num5 = num3;
				num += num3;
				num2 -= num3;
				num3 = BinaryFormatParser.MatchBytes(buffer, num, num2, MessagePatterns.requestFragment2);
				if (num3 == 0)
				{
					return null;
				}
				num += num3;
				num2 -= num3;
				if (BinaryFormatParser.MatchAttributeNode(buffer, num, num2))
				{
					return null;
				}
				UniqueId messageId = BinaryFormatParser.ParseUniqueID(buffer, offset3, num5);
				messageIDHeader = MessageIDHeader.Create(messageId, this.messageVersion.Addressing);
				relatesToHeader = null;
				if (!this.readerSession.TryLookup(1, out anonymous))
				{
					return null;
				}
				num6 = MessagePatterns.requestFragment1.Length + num5 + MessagePatterns.requestFragment2.Length;
			}
			else
			{
				num3 = BinaryFormatParser.MatchBytes(buffer, num, num2, MessagePatterns.responseFragment1);
				if (num3 == 0)
				{
					return null;
				}
				num += num3;
				num2 -= num3;
				num3 = BinaryFormatParser.MatchUniqueID(buffer, num, num2);
				if (num3 == 0)
				{
					return null;
				}
				int offset4 = num;
				int num7 = num3;
				num += num3;
				num2 -= num3;
				num3 = BinaryFormatParser.MatchBytes(buffer, num, num2, MessagePatterns.responseFragment2);
				if (num3 == 0)
				{
					return null;
				}
				num += num3;
				num2 -= num3;
				if (BinaryFormatParser.MatchAttributeNode(buffer, num, num2))
				{
					return null;
				}
				UniqueId messageId2 = BinaryFormatParser.ParseUniqueID(buffer, offset4, num7);
				relatesToHeader = RelatesToHeader.Create(messageId2, this.messageVersion.Addressing);
				messageIDHeader = null;
				anonymous = XD.Addressing10Dictionary.Anonymous;
				num6 = MessagePatterns.responseFragment1.Length + num7 + MessagePatterns.responseFragment2.Length;
			}
			num6 += MessagePatterns.commonFragment.Length + num4;
			int key = BinaryFormatParser.ParseKey(buffer, offset2, num4);
			XmlDictionaryString dictionaryAction;
			if (!this.TryLookupKey(key, out dictionaryAction))
			{
				return null;
			}
			ActionHeader actionHeader = ActionHeader.Create(dictionaryAction, this.messageVersion.Addressing);
			if (this.toHeader == null)
			{
				this.toHeader = ToHeader.Create(new Uri(anonymous.Value), this.messageVersion.Addressing);
			}
			int num8 = num6 - MessagePatterns.bodyFragment.Length;
			offset += num8;
			size -= num8;
			Buffer.BlockCopy(MessagePatterns.bodyFragment, 0, buffer, offset, MessagePatterns.bodyFragment.Length);
			messageData.Open(new ArraySegment<byte>(buffer, offset, size), bufferManager);
			MessagePatterns.PatternMessage patternMessage = new MessagePatterns.PatternMessage(messageData, this.messageVersion);
			MessageHeaders headers = patternMessage.Headers;
			headers.AddActionHeader(actionHeader);
			if (messageIDHeader != null)
			{
				headers.AddMessageIDHeader(messageIDHeader);
				headers.AddReplyToHeader(ReplyToHeader.AnonymousReplyTo10);
			}
			else
			{
				headers.AddRelatesToHeader(relatesToHeader);
			}
			headers.AddToHeader(this.toHeader);
			return patternMessage;
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x00174A07 File Offset: 0x00172C07
		private bool TryLookupKey(int key, out XmlDictionaryString result)
		{
			if (BinaryFormatParser.IsSessionKey(key))
			{
				return this.readerSession.TryLookup(BinaryFormatParser.GetSessionKey(key), out result);
			}
			return this.dictionary.TryLookup(BinaryFormatParser.GetStaticKey(key), out result);
		}

		// Token: 0x04003986 RID: 14726
		private static readonly byte[] commonFragment;

		// Token: 0x04003987 RID: 14727
		private static readonly byte[] requestFragment1;

		// Token: 0x04003988 RID: 14728
		private static readonly byte[] requestFragment2;

		// Token: 0x04003989 RID: 14729
		private static readonly byte[] responseFragment1;

		// Token: 0x0400398A RID: 14730
		private static readonly byte[] responseFragment2;

		// Token: 0x0400398B RID: 14731
		private static readonly byte[] bodyFragment;

		// Token: 0x0400398C RID: 14732
		private const int ToValueSessionKey = 1;

		// Token: 0x0400398D RID: 14733
		private IXmlDictionary dictionary;

		// Token: 0x0400398E RID: 14734
		private XmlBinaryReaderSession readerSession;

		// Token: 0x0400398F RID: 14735
		private ToHeader toHeader;

		// Token: 0x04003990 RID: 14736
		private MessageVersion messageVersion;

		// Token: 0x02000E51 RID: 3665
		private sealed class PatternMessage : ReceivedMessage
		{
			// Token: 0x06008307 RID: 33543 RVA: 0x001E4A5C File Offset: 0x001E2C5C
			public PatternMessage(IBufferedMessageData messageData, MessageVersion messageVersion)
			{
				this.messageData = messageData;
				this.recycledMessageState = messageData.TakeMessageState();
				if (this.recycledMessageState == null)
				{
					this.recycledMessageState = new RecycledMessageState();
				}
				this.properties = this.recycledMessageState.TakeProperties();
				if (this.properties == null)
				{
					this.properties = new MessageProperties();
				}
				this.headers = this.recycledMessageState.TakeHeaders();
				if (this.headers == null)
				{
					this.headers = new MessageHeaders(messageVersion);
				}
				else
				{
					this.headers.Init(messageVersion);
				}
				XmlDictionaryReader messageReader = messageData.GetMessageReader();
				messageReader.ReadStartElement();
				ReceivedMessage.VerifyStartBody(messageReader, messageVersion.Envelope);
				base.ReadStartBody(messageReader);
				this.reader = messageReader;
			}

			// Token: 0x06008308 RID: 33544 RVA: 0x001E4B14 File Offset: 0x001E2D14
			public PatternMessage(IBufferedMessageData messageData, MessageVersion messageVersion, KeyValuePair<string, object>[] properties, MessageHeaders headers)
			{
				this.messageData = messageData;
				this.messageData.Open();
				this.recycledMessageState = this.messageData.TakeMessageState();
				if (this.recycledMessageState == null)
				{
					this.recycledMessageState = new RecycledMessageState();
				}
				this.properties = this.recycledMessageState.TakeProperties();
				if (this.properties == null)
				{
					this.properties = new MessageProperties();
				}
				if (properties != null)
				{
					this.properties.CopyProperties(properties);
				}
				this.headers = this.recycledMessageState.TakeHeaders();
				if (this.headers == null)
				{
					this.headers = new MessageHeaders(messageVersion);
				}
				if (headers != null)
				{
					this.headers.CopyHeadersFrom(headers);
				}
				XmlDictionaryReader messageReader = messageData.GetMessageReader();
				messageReader.ReadStartElement();
				ReceivedMessage.VerifyStartBody(messageReader, messageVersion.Envelope);
				base.ReadStartBody(messageReader);
				this.reader = messageReader;
			}

			// Token: 0x17001CFC RID: 7420
			// (get) Token: 0x06008309 RID: 33545 RVA: 0x001E4BEE File Offset: 0x001E2DEE
			public override MessageHeaders Headers
			{
				get
				{
					if (base.IsDisposed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateMessageDisposedException());
					}
					return this.headers;
				}
			}

			// Token: 0x17001CFD RID: 7421
			// (get) Token: 0x0600830A RID: 33546 RVA: 0x001E4C0F File Offset: 0x001E2E0F
			public override MessageProperties Properties
			{
				get
				{
					if (base.IsDisposed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateMessageDisposedException());
					}
					return this.properties;
				}
			}

			// Token: 0x0600830B RID: 33547 RVA: 0x001E4C30 File Offset: 0x001E2E30
			internal override void SetProperty(string name, object value)
			{
				MessageProperties messageProperties = this.properties;
				if (messageProperties != null)
				{
					messageProperties[name] = value;
				}
			}

			// Token: 0x17001CFE RID: 7422
			// (get) Token: 0x0600830C RID: 33548 RVA: 0x001E4C4F File Offset: 0x001E2E4F
			public override MessageVersion Version
			{
				get
				{
					if (base.IsDisposed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateMessageDisposedException());
					}
					return this.headers.MessageVersion;
				}
			}

			// Token: 0x17001CFF RID: 7423
			// (get) Token: 0x0600830D RID: 33549 RVA: 0x001E4C75 File Offset: 0x001E2E75
			internal override RecycledMessageState RecycledMessageState
			{
				get
				{
					return this.recycledMessageState;
				}
			}

			// Token: 0x0600830E RID: 33550 RVA: 0x001E4C80 File Offset: 0x001E2E80
			private XmlDictionaryReader GetBufferedReaderAtBody()
			{
				XmlDictionaryReader messageReader = this.messageData.GetMessageReader();
				messageReader.ReadStartElement();
				messageReader.ReadStartElement();
				return messageReader;
			}

			// Token: 0x0600830F RID: 33551 RVA: 0x001E4CA8 File Offset: 0x001E2EA8
			protected override void OnBodyToString(XmlDictionaryWriter writer)
			{
				using (XmlDictionaryReader bufferedReaderAtBody = this.GetBufferedReaderAtBody())
				{
					while (bufferedReaderAtBody.NodeType != XmlNodeType.EndElement)
					{
						writer.WriteNode(bufferedReaderAtBody, false);
					}
				}
			}

			// Token: 0x06008310 RID: 33552 RVA: 0x001E4CEC File Offset: 0x001E2EEC
			protected override void OnClose()
			{
				Exception ex = null;
				try
				{
					base.OnClose();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				try
				{
					this.properties.Dispose();
				}
				catch (Exception ex3)
				{
					if (Fx.IsFatal(ex3))
					{
						throw;
					}
					if (ex == null)
					{
						ex = ex3;
					}
				}
				try
				{
					if (this.reader != null)
					{
						this.reader.Close();
					}
				}
				catch (Exception ex4)
				{
					if (Fx.IsFatal(ex4))
					{
						throw;
					}
					if (ex == null)
					{
						ex = ex4;
					}
				}
				try
				{
					this.recycledMessageState.ReturnHeaders(this.headers);
					this.recycledMessageState.ReturnProperties(this.properties);
					this.messageData.ReturnMessageState(this.recycledMessageState);
					this.recycledMessageState = null;
					this.messageData.Close();
					this.messageData = null;
				}
				catch (Exception ex5)
				{
					if (Fx.IsFatal(ex5))
					{
						throw;
					}
					if (ex == null)
					{
						ex = ex5;
					}
				}
				if (ex != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
			}

			// Token: 0x06008311 RID: 33553 RVA: 0x001E4E04 File Offset: 0x001E3004
			protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
			{
				KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[this.Properties.Count];
				((ICollection<KeyValuePair<string, object>>)this.Properties).CopyTo(array, 0);
				this.messageData.EnableMultipleUsers();
				return new MessagePatterns.PatternMessageBuffer(this.messageData, this.Version, array, this.headers);
			}

			// Token: 0x06008312 RID: 33554 RVA: 0x001E4E54 File Offset: 0x001E3054
			protected override XmlDictionaryReader OnGetReaderAtBodyContents()
			{
				XmlDictionaryReader result = this.reader;
				this.reader = null;
				return result;
			}

			// Token: 0x06008313 RID: 33555 RVA: 0x001E4E70 File Offset: 0x001E3070
			protected override string OnGetBodyAttribute(string localName, string ns)
			{
				return null;
			}

			// Token: 0x04004A7E RID: 19070
			private IBufferedMessageData messageData;

			// Token: 0x04004A7F RID: 19071
			private MessageHeaders headers;

			// Token: 0x04004A80 RID: 19072
			private RecycledMessageState recycledMessageState;

			// Token: 0x04004A81 RID: 19073
			private MessageProperties properties;

			// Token: 0x04004A82 RID: 19074
			private XmlDictionaryReader reader;
		}

		// Token: 0x02000E52 RID: 3666
		private class PatternMessageBuffer : MessageBuffer
		{
			// Token: 0x06008314 RID: 33556 RVA: 0x001E4E74 File Offset: 0x001E3074
			public PatternMessageBuffer(IBufferedMessageData messageDataAtBody, MessageVersion messageVersion, KeyValuePair<string, object>[] properties, MessageHeaders headers)
			{
				this.messageDataAtBody = messageDataAtBody;
				this.messageDataAtBody.Open();
				this.recycledMessageState = this.messageDataAtBody.TakeMessageState();
				if (this.recycledMessageState == null)
				{
					this.recycledMessageState = new RecycledMessageState();
				}
				this.headers = this.recycledMessageState.TakeHeaders();
				if (this.headers == null)
				{
					this.headers = new MessageHeaders(messageVersion);
				}
				this.headers.CopyHeadersFrom(headers);
				this.properties = properties;
				this.messageVersion = messageVersion;
			}

			// Token: 0x17001D00 RID: 7424
			// (get) Token: 0x06008315 RID: 33557 RVA: 0x001E4F08 File Offset: 0x001E3108
			public override int BufferSize
			{
				get
				{
					object obj = this.ThisLock;
					int count;
					lock (obj)
					{
						if (this.closed)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
						}
						count = this.messageDataAtBody.Buffer.Count;
					}
					return count;
				}
			}

			// Token: 0x17001D01 RID: 7425
			// (get) Token: 0x06008316 RID: 33558 RVA: 0x001E4F70 File Offset: 0x001E3170
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x06008317 RID: 33559 RVA: 0x001E4F78 File Offset: 0x001E3178
			public override void Close()
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.closed)
					{
						this.closed = true;
						this.recycledMessageState.ReturnHeaders(this.headers);
						this.messageDataAtBody.ReturnMessageState(this.recycledMessageState);
						this.messageDataAtBody.Close();
						this.recycledMessageState = null;
						this.messageDataAtBody = null;
						this.properties = null;
						this.messageVersion = null;
						this.headers = null;
					}
				}
			}

			// Token: 0x06008318 RID: 33560 RVA: 0x001E5014 File Offset: 0x001E3214
			public override Message CreateMessage()
			{
				object obj = this.ThisLock;
				Message result;
				lock (obj)
				{
					if (this.closed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateBufferDisposedException());
					}
					result = new MessagePatterns.PatternMessage(this.messageDataAtBody, this.messageVersion, this.properties, this.headers);
				}
				return result;
			}

			// Token: 0x04004A83 RID: 19075
			private bool closed;

			// Token: 0x04004A84 RID: 19076
			private MessageHeaders headers;

			// Token: 0x04004A85 RID: 19077
			private IBufferedMessageData messageDataAtBody;

			// Token: 0x04004A86 RID: 19078
			private MessageVersion messageVersion;

			// Token: 0x04004A87 RID: 19079
			private KeyValuePair<string, object>[] properties;

			// Token: 0x04004A88 RID: 19080
			private object thisLock = new object();

			// Token: 0x04004A89 RID: 19081
			private RecycledMessageState recycledMessageState;
		}
	}
}
