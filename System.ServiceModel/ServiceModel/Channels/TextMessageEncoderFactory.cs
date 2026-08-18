using System;
using System.Globalization;
using System.IO;
using System.Net.Mime;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E5 RID: 2533
	internal class TextMessageEncoderFactory : MessageEncoderFactory
	{
		// Token: 0x06006414 RID: 25620 RVA: 0x00175BC8 File Offset: 0x00173DC8
		public TextMessageEncoderFactory(MessageVersion version, Encoding writeEncoding, int maxReadPoolSize, int maxWritePoolSize, XmlDictionaryReaderQuotas quotas)
		{
			this.messageEncoder = new TextMessageEncoderFactory.TextMessageEncoder(version, writeEncoding, maxReadPoolSize, maxWritePoolSize, quotas);
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x00175BE2 File Offset: 0x00173DE2
		public override MessageEncoder Encoder
		{
			get
			{
				return this.messageEncoder;
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06006416 RID: 25622 RVA: 0x00175BEA File Offset: 0x00173DEA
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.messageEncoder.MessageVersion;
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06006417 RID: 25623 RVA: 0x00175BF7 File Offset: 0x00173DF7
		public int MaxWritePoolSize
		{
			get
			{
				return this.messageEncoder.MaxWritePoolSize;
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06006418 RID: 25624 RVA: 0x00175C04 File Offset: 0x00173E04
		public int MaxReadPoolSize
		{
			get
			{
				return this.messageEncoder.MaxReadPoolSize;
			}
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x00175C14 File Offset: 0x00173E14
		public static Encoding[] GetSupportedEncodings()
		{
			Encoding[] supportedEncodings = TextEncoderDefaults.SupportedEncodings;
			Encoding[] array = new Encoding[supportedEncodings.Length];
			Array.Copy(supportedEncodings, array, supportedEncodings.Length);
			return array;
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x0600641A RID: 25626 RVA: 0x00175C3B File Offset: 0x00173E3B
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.messageEncoder.ReaderQuotas;
			}
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x00175C48 File Offset: 0x00173E48
		internal static string GetMediaType(MessageVersion version)
		{
			string result;
			if (version.Envelope == EnvelopeVersion.Soap12)
			{
				result = "application/soap+xml";
			}
			else if (version.Envelope == EnvelopeVersion.Soap11)
			{
				result = "text/xml";
			}
			else
			{
				if (version.Envelope != EnvelopeVersion.None)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EnvelopeVersionNotSupported", new object[]
					{
						version.Envelope
					})));
				}
				result = "application/xml";
			}
			return result;
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x00175CC0 File Offset: 0x00173EC0
		internal static string GetContentType(string mediaType, Encoding encoding)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; charset={1}", new object[]
			{
				mediaType,
				TextEncoderDefaults.EncodingToCharSet(encoding)
			});
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x00175CE4 File Offset: 0x00173EE4
		private static TextMessageEncoderFactory.ContentEncoding[] GetContentEncodingMap(MessageVersion version)
		{
			Encoding[] supportedEncodings = TextMessageEncoderFactory.GetSupportedEncodings();
			string mediaType = TextMessageEncoderFactory.GetMediaType(version);
			TextMessageEncoderFactory.ContentEncoding[] array = new TextMessageEncoderFactory.ContentEncoding[supportedEncodings.Length];
			for (int i = 0; i < supportedEncodings.Length; i++)
			{
				array[i] = new TextMessageEncoderFactory.ContentEncoding
				{
					contentType = TextMessageEncoderFactory.GetContentType(mediaType, supportedEncodings[i]),
					encoding = supportedEncodings[i]
				};
			}
			return array;
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x00175D3C File Offset: 0x00173F3C
		internal static Encoding GetEncodingFromContentType(string contentType, TextMessageEncoderFactory.ContentEncoding[] contentMap)
		{
			if (contentType == null)
			{
				return null;
			}
			for (int i = 0; i < contentMap.Length; i++)
			{
				if (contentMap[i].contentType == contentType)
				{
					return contentMap[i].encoding;
				}
			}
			int num = contentType.IndexOf(';');
			if (num == -1)
			{
				return null;
			}
			int num2 = -1;
			if (contentType.Length > num + 11 && contentType[num + 2] == 'c' && string.Compare("charset=", 0, contentType, num + 2, 8, StringComparison.OrdinalIgnoreCase) == 0)
			{
				num2 = num + 10;
			}
			else
			{
				int num3 = contentType.IndexOf("charset=", num + 1, StringComparison.OrdinalIgnoreCase);
				if (num3 != -1)
				{
					for (int j = num3 - 1; j >= num; j--)
					{
						if (contentType[j] == ';')
						{
							num2 = num3 + 8;
							break;
						}
						if (contentType[j] == '\n')
						{
							if (j == num || contentType[j - 1] != '\r')
							{
								break;
							}
							j--;
						}
						else if (contentType[j] != ' ' && contentType[j] != '\t')
						{
							break;
						}
					}
				}
			}
			string text;
			Encoding result;
			if (num2 != -1)
			{
				num = contentType.IndexOf(';', num2);
				if (num == -1)
				{
					text = contentType.Substring(num2);
				}
				else
				{
					text = contentType.Substring(num2, num - num2);
				}
				if (text.Length > 2 && text[0] == '"' && text[text.Length - 1] == '"')
				{
					text = text.Substring(1, text.Length - 2);
				}
				if (TextMessageEncoderFactory.TryGetEncodingFromCharSet(text, out result))
				{
					return result;
				}
			}
			try
			{
				ContentType contentType2 = new ContentType(contentType);
				text = contentType2.CharSet;
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("EncoderBadContentType"), innerException));
			}
			if (TextMessageEncoderFactory.TryGetEncodingFromCharSet(text, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("EncoderUnrecognizedCharSet", new object[]
			{
				text
			})));
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x00175F14 File Offset: 0x00174114
		internal static bool TryGetEncodingFromCharSet(string charSet, out Encoding encoding)
		{
			encoding = null;
			return charSet == null || charSet.Length == 0 || TextEncoderDefaults.TryGetEncoding(charSet, out encoding);
		}

		// Token: 0x040039B4 RID: 14772
		private TextMessageEncoderFactory.TextMessageEncoder messageEncoder;

		// Token: 0x040039B5 RID: 14773
		internal static TextMessageEncoderFactory.ContentEncoding[] Soap11Content = TextMessageEncoderFactory.GetContentEncodingMap(MessageVersion.Soap11WSAddressing10);

		// Token: 0x040039B6 RID: 14774
		internal static TextMessageEncoderFactory.ContentEncoding[] Soap12Content = TextMessageEncoderFactory.GetContentEncodingMap(MessageVersion.Soap12WSAddressing10);

		// Token: 0x040039B7 RID: 14775
		internal static TextMessageEncoderFactory.ContentEncoding[] SoapNoneContent = TextMessageEncoderFactory.GetContentEncodingMap(MessageVersion.None);

		// Token: 0x040039B8 RID: 14776
		internal const string Soap11MediaType = "text/xml";

		// Token: 0x040039B9 RID: 14777
		internal const string Soap12MediaType = "application/soap+xml";

		// Token: 0x040039BA RID: 14778
		private const string XmlMediaType = "application/xml";

		// Token: 0x02000E57 RID: 3671
		internal class ContentEncoding
		{
			// Token: 0x04004A9D RID: 19101
			internal string contentType;

			// Token: 0x04004A9E RID: 19102
			internal Encoding encoding;
		}

		// Token: 0x02000E58 RID: 3672
		private class TextMessageEncoder : MessageEncoder, ITraceSourceStringProvider
		{
			// Token: 0x06008328 RID: 33576 RVA: 0x001E54D8 File Offset: 0x001E36D8
			public TextMessageEncoder(MessageVersion version, Encoding writeEncoding, int maxReadPoolSize, int maxWritePoolSize, XmlDictionaryReaderQuotas quotas)
			{
				if (version == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
				}
				if (writeEncoding == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writeEncoding");
				}
				TextEncoderDefaults.ValidateEncoding(writeEncoding);
				this.writeEncoding = writeEncoding;
				this.optimizeWriteForUTF8 = TextMessageEncoderFactory.TextMessageEncoder.IsUTF8Encoding(writeEncoding);
				this.thisLock = new object();
				this.version = version;
				this.maxReadPoolSize = maxReadPoolSize;
				this.maxWritePoolSize = maxWritePoolSize;
				this.readerQuotas = new XmlDictionaryReaderQuotas();
				quotas.CopyTo(this.readerQuotas);
				this.bufferedReadReaderQuotas = EncoderHelpers.GetBufferedReadQuotas(this.readerQuotas);
				this.onStreamedReaderClose = new OnXmlDictionaryReaderClose(this.ReturnStreamedReader);
				this.mediaType = TextMessageEncoderFactory.GetMediaType(version);
				this.contentType = TextMessageEncoderFactory.GetContentType(this.mediaType, writeEncoding);
				if (version.Envelope == EnvelopeVersion.Soap12)
				{
					this.contentEncodingMap = TextMessageEncoderFactory.Soap12Content;
					return;
				}
				if (version.Envelope == EnvelopeVersion.Soap11)
				{
					this.contentEncodingMap = TextMessageEncoderFactory.Soap11Content;
					return;
				}
				if (version.Envelope == EnvelopeVersion.None)
				{
					this.contentEncodingMap = TextMessageEncoderFactory.SoapNoneContent;
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EnvelopeVersionNotSupported", new object[]
				{
					version.Envelope
				})));
			}

			// Token: 0x06008329 RID: 33577 RVA: 0x001E5617 File Offset: 0x001E3817
			private static bool IsUTF8Encoding(Encoding encoding)
			{
				return encoding.WebName == "utf-8";
			}

			// Token: 0x17001D04 RID: 7428
			// (get) Token: 0x0600832A RID: 33578 RVA: 0x001E5629 File Offset: 0x001E3829
			public override string ContentType
			{
				get
				{
					return this.contentType;
				}
			}

			// Token: 0x17001D05 RID: 7429
			// (get) Token: 0x0600832B RID: 33579 RVA: 0x001E5631 File Offset: 0x001E3831
			public int MaxWritePoolSize
			{
				get
				{
					return this.maxWritePoolSize;
				}
			}

			// Token: 0x17001D06 RID: 7430
			// (get) Token: 0x0600832C RID: 33580 RVA: 0x001E5639 File Offset: 0x001E3839
			public int MaxReadPoolSize
			{
				get
				{
					return this.maxReadPoolSize;
				}
			}

			// Token: 0x17001D07 RID: 7431
			// (get) Token: 0x0600832D RID: 33581 RVA: 0x001E5641 File Offset: 0x001E3841
			public XmlDictionaryReaderQuotas ReaderQuotas
			{
				get
				{
					return this.readerQuotas;
				}
			}

			// Token: 0x17001D08 RID: 7432
			// (get) Token: 0x0600832E RID: 33582 RVA: 0x001E5649 File Offset: 0x001E3849
			public override string MediaType
			{
				get
				{
					return this.mediaType;
				}
			}

			// Token: 0x17001D09 RID: 7433
			// (get) Token: 0x0600832F RID: 33583 RVA: 0x001E5651 File Offset: 0x001E3851
			public override MessageVersion MessageVersion
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x17001D0A RID: 7434
			// (get) Token: 0x06008330 RID: 33584 RVA: 0x001E5659 File Offset: 0x001E3859
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x06008331 RID: 33585 RVA: 0x001E5664 File Offset: 0x001E3864
			internal override bool IsCharSetSupported(string charSet)
			{
				Encoding encoding;
				return TextEncoderDefaults.TryGetEncoding(charSet, out encoding);
			}

			// Token: 0x06008332 RID: 33586 RVA: 0x001E567C File Offset: 0x001E387C
			public override bool IsContentTypeSupported(string contentType)
			{
				if (contentType == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contentType");
				}
				if (base.IsContentTypeSupported(contentType))
				{
					return true;
				}
				if (this.MessageVersion == MessageVersion.None)
				{
					if (base.IsContentTypeSupported(contentType, "text/xml", "text/xml"))
					{
						return true;
					}
					if (base.IsContentTypeSupported(contentType, "application/rss+xml", "application/rss+xml"))
					{
						return true;
					}
					if (base.IsContentTypeSupported(contentType, "text/html", "application/atom+xml"))
					{
						return true;
					}
					if (base.IsContentTypeSupported(contentType, "application/atom+xml", "application/atom+xml"))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06008333 RID: 33587 RVA: 0x001E570C File Offset: 0x001E390C
			public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
			{
				if (bufferManager == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("bufferManager"));
				}
				if (TD.TextMessageDecodingStartIsEnabled())
				{
					TD.TextMessageDecodingStart();
				}
				TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData utf8BufferedMessageData = this.TakeBufferedReader();
				utf8BufferedMessageData.Encoding = TextMessageEncoderFactory.GetEncodingFromContentType(contentType, this.contentEncodingMap);
				utf8BufferedMessageData.Open(buffer, bufferManager);
				RecycledMessageState recycledMessageState = utf8BufferedMessageData.TakeMessageState();
				if (recycledMessageState == null)
				{
					recycledMessageState = new RecycledMessageState();
				}
				Message message = new BufferedMessage(utf8BufferedMessageData, recycledMessageState);
				message.Properties.Encoder = this;
				if (TD.MessageReadByEncoderIsEnabled())
				{
					TD.MessageReadByEncoder(EventTraceActivityHelper.TryExtractActivity(message, true), buffer.Count, this);
				}
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportReceive);
				}
				return message;
			}

			// Token: 0x06008334 RID: 33588 RVA: 0x001E57B0 File Offset: 0x001E39B0
			public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
			{
				if (stream == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
				}
				if (TD.TextMessageDecodingStartIsEnabled())
				{
					TD.TextMessageDecodingStart();
				}
				XmlReader envelopeReader = this.TakeStreamedReader(stream, TextMessageEncoderFactory.GetEncodingFromContentType(contentType, this.contentEncodingMap));
				Message message = Message.CreateMessage(envelopeReader, maxSizeOfHeaders, this.version);
				message.Properties.Encoder = this;
				if (TD.StreamedMessageReadByEncoderIsEnabled())
				{
					TD.StreamedMessageReadByEncoder(EventTraceActivityHelper.TryExtractActivity(message, true));
				}
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportReceive);
				}
				return message;
			}

			// Token: 0x06008335 RID: 33589 RVA: 0x001E5834 File Offset: 0x001E3A34
			public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
			{
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
				}
				if (bufferManager == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("bufferManager"), message);
				}
				if (maxMessageSize < 0)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxMessageSize", maxMessageSize, SR.GetString("ValueMustBeNonNegative")), message);
				}
				if (messageOffset < 0 || messageOffset > maxMessageSize)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageOffset", messageOffset, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						maxMessageSize
					})), message);
				}
				base.ThrowIfMismatchedMessageVersion(message);
				EventTraceActivity eventTraceActivity = null;
				if (TD.TextMessageEncodingStartIsEnabled())
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					TD.TextMessageEncodingStart(eventTraceActivity);
				}
				message.Properties.Encoder = this;
				TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter textBufferedMessageWriter = this.TakeBufferedWriter();
				ArraySegment<byte> result = textBufferedMessageWriter.WriteMessage(message, bufferManager, messageOffset, maxMessageSize);
				this.ReturnMessageWriter(textBufferedMessageWriter);
				if (TD.MessageWrittenByEncoderIsEnabled())
				{
					TD.MessageWrittenByEncoder(eventTraceActivity ?? EventTraceActivityHelper.TryExtractActivity(message), result.Count, this);
				}
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(result.Array, result.Offset, result.Count, null, XmlDictionaryReaderQuotas.Max, null);
					MessageLogger.LogMessage(ref message, reader, MessageLoggingSource.TransportSend);
				}
				return result;
			}

			// Token: 0x06008336 RID: 33590 RVA: 0x001E596C File Offset: 0x001E3B6C
			public override void WriteMessage(Message message, Stream stream)
			{
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
				}
				if (stream == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("stream"), message);
				}
				base.ThrowIfMismatchedMessageVersion(message);
				EventTraceActivity eventTraceActivity = null;
				if (TD.TextMessageEncodingStartIsEnabled())
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					TD.TextMessageEncodingStart(eventTraceActivity);
				}
				message.Properties.Encoder = this;
				XmlDictionaryWriter xmlDictionaryWriter = this.TakeStreamedWriter(stream);
				if (this.optimizeWriteForUTF8)
				{
					message.WriteMessage(xmlDictionaryWriter);
				}
				else
				{
					xmlDictionaryWriter.WriteStartDocument();
					message.WriteMessage(xmlDictionaryWriter);
					xmlDictionaryWriter.WriteEndDocument();
				}
				xmlDictionaryWriter.Flush();
				this.ReturnStreamedWriter(xmlDictionaryWriter);
				if (TD.StreamedMessageWrittenByEncoderIsEnabled())
				{
					TD.StreamedMessageWrittenByEncoder(eventTraceActivity ?? EventTraceActivityHelper.TryExtractActivity(message));
				}
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportSend);
				}
			}

			// Token: 0x06008337 RID: 33591 RVA: 0x001E5A30 File Offset: 0x001E3C30
			public override IAsyncResult BeginWriteMessage(Message message, Stream stream, AsyncCallback callback, object state)
			{
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
				}
				if (stream == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("stream"), message);
				}
				base.ThrowIfMismatchedMessageVersion(message);
				message.Properties.Encoder = this;
				return new TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult(message, stream, this, callback, state);
			}

			// Token: 0x06008338 RID: 33592 RVA: 0x001E5A87 File Offset: 0x001E3C87
			public override void EndWriteMessage(IAsyncResult result)
			{
				TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult.End(result);
			}

			// Token: 0x06008339 RID: 33593 RVA: 0x001E5A90 File Offset: 0x001E3C90
			private XmlDictionaryWriter TakeStreamedWriter(Stream stream)
			{
				if (this.streamedWriterPool == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.streamedWriterPool == null)
						{
							this.streamedWriterPool = new SynchronizedPool<XmlDictionaryWriter>(this.maxWritePoolSize);
						}
					}
				}
				XmlDictionaryWriter xmlDictionaryWriter = this.streamedWriterPool.Take();
				if (xmlDictionaryWriter == null)
				{
					xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream, this.writeEncoding, false);
					if (TD.WritePoolMissIsEnabled())
					{
						TD.WritePoolMiss(xmlDictionaryWriter.GetType().Name);
					}
				}
				else
				{
					((IXmlTextWriterInitializer)xmlDictionaryWriter).SetOutput(stream, this.writeEncoding, false);
				}
				return xmlDictionaryWriter;
			}

			// Token: 0x0600833A RID: 33594 RVA: 0x001E5B40 File Offset: 0x001E3D40
			private void ReturnStreamedWriter(XmlWriter xmlWriter)
			{
				xmlWriter.Close();
				this.streamedWriterPool.Return((XmlDictionaryWriter)xmlWriter);
			}

			// Token: 0x0600833B RID: 33595 RVA: 0x001E5B5C File Offset: 0x001E3D5C
			private TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter TakeBufferedWriter()
			{
				if (this.bufferedWriterPool == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.bufferedWriterPool == null)
						{
							this.bufferedWriterPool = new SynchronizedPool<TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter>(this.maxWritePoolSize);
						}
					}
				}
				TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter textBufferedMessageWriter = this.bufferedWriterPool.Take();
				if (textBufferedMessageWriter == null)
				{
					textBufferedMessageWriter = new TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter(this);
					if (TD.WritePoolMissIsEnabled())
					{
						TD.WritePoolMiss(textBufferedMessageWriter.GetType().Name);
					}
				}
				return textBufferedMessageWriter;
			}

			// Token: 0x0600833C RID: 33596 RVA: 0x001E5BF0 File Offset: 0x001E3DF0
			private void ReturnMessageWriter(TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter messageWriter)
			{
				this.bufferedWriterPool.Return(messageWriter);
			}

			// Token: 0x0600833D RID: 33597 RVA: 0x001E5C04 File Offset: 0x001E3E04
			private XmlReader TakeStreamedReader(Stream stream, Encoding enc)
			{
				if (this.streamedReaderPool == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.streamedReaderPool == null)
						{
							this.streamedReaderPool = new SynchronizedPool<XmlDictionaryReader>(this.maxReadPoolSize);
						}
					}
				}
				XmlDictionaryReader xmlDictionaryReader = this.streamedReaderPool.Take();
				if (xmlDictionaryReader == null)
				{
					xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(stream, enc, this.readerQuotas, null);
					if (TD.ReadPoolMissIsEnabled())
					{
						TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
					}
				}
				else
				{
					((IXmlTextReaderInitializer)xmlDictionaryReader).SetInput(stream, enc, this.readerQuotas, this.onStreamedReaderClose);
				}
				return xmlDictionaryReader;
			}

			// Token: 0x0600833E RID: 33598 RVA: 0x001E5CB8 File Offset: 0x001E3EB8
			private void ReturnStreamedReader(XmlDictionaryReader xmlReader)
			{
				this.streamedReaderPool.Return(xmlReader);
			}

			// Token: 0x0600833F RID: 33599 RVA: 0x001E5CC9 File Offset: 0x001E3EC9
			private XmlDictionaryWriter CreateWriter(Stream stream)
			{
				return XmlDictionaryWriter.CreateTextWriter(stream, this.writeEncoding, false);
			}

			// Token: 0x06008340 RID: 33600 RVA: 0x001E5CD8 File Offset: 0x001E3ED8
			private TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData TakeBufferedReader()
			{
				if (this.bufferedReaderPool == null)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.bufferedReaderPool == null)
						{
							this.bufferedReaderPool = new SynchronizedPool<TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData>(this.maxReadPoolSize);
						}
					}
				}
				TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData utf8BufferedMessageData = this.bufferedReaderPool.Take();
				if (utf8BufferedMessageData == null)
				{
					utf8BufferedMessageData = new TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData(this, 2);
					if (TD.ReadPoolMissIsEnabled())
					{
						TD.ReadPoolMiss(utf8BufferedMessageData.GetType().Name);
					}
				}
				return utf8BufferedMessageData;
			}

			// Token: 0x06008341 RID: 33601 RVA: 0x001E5D6C File Offset: 0x001E3F6C
			private void ReturnBufferedData(TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData messageData)
			{
				this.bufferedReaderPool.Return(messageData);
			}

			// Token: 0x17001D0B RID: 7435
			// (get) Token: 0x06008342 RID: 33602 RVA: 0x001E5D80 File Offset: 0x001E3F80
			private SynchronizedPool<RecycledMessageState> RecycledStatePool
			{
				get
				{
					if (this.recycledStatePool == null)
					{
						object obj = this.ThisLock;
						lock (obj)
						{
							if (this.recycledStatePool == null)
							{
								this.recycledStatePool = new SynchronizedPool<RecycledMessageState>(this.maxReadPoolSize);
							}
						}
					}
					return this.recycledStatePool;
				}
			}

			// Token: 0x06008343 RID: 33603 RVA: 0x001E5DEC File Offset: 0x001E3FEC
			string ITraceSourceStringProvider.GetSourceString()
			{
				return base.GetTraceSourceString();
			}

			// Token: 0x04004A9F RID: 19103
			private int maxReadPoolSize;

			// Token: 0x04004AA0 RID: 19104
			private int maxWritePoolSize;

			// Token: 0x04004AA1 RID: 19105
			private volatile SynchronizedPool<XmlDictionaryWriter> streamedWriterPool;

			// Token: 0x04004AA2 RID: 19106
			private volatile SynchronizedPool<XmlDictionaryReader> streamedReaderPool;

			// Token: 0x04004AA3 RID: 19107
			private volatile SynchronizedPool<TextMessageEncoderFactory.TextMessageEncoder.UTF8BufferedMessageData> bufferedReaderPool;

			// Token: 0x04004AA4 RID: 19108
			private volatile SynchronizedPool<TextMessageEncoderFactory.TextMessageEncoder.TextBufferedMessageWriter> bufferedWriterPool;

			// Token: 0x04004AA5 RID: 19109
			private volatile SynchronizedPool<RecycledMessageState> recycledStatePool;

			// Token: 0x04004AA6 RID: 19110
			private object thisLock;

			// Token: 0x04004AA7 RID: 19111
			private string contentType;

			// Token: 0x04004AA8 RID: 19112
			private string mediaType;

			// Token: 0x04004AA9 RID: 19113
			private Encoding writeEncoding;

			// Token: 0x04004AAA RID: 19114
			private MessageVersion version;

			// Token: 0x04004AAB RID: 19115
			private bool optimizeWriteForUTF8;

			// Token: 0x04004AAC RID: 19116
			private const int maxPooledXmlReadersPerMessage = 2;

			// Token: 0x04004AAD RID: 19117
			private XmlDictionaryReaderQuotas readerQuotas;

			// Token: 0x04004AAE RID: 19118
			private XmlDictionaryReaderQuotas bufferedReadReaderQuotas;

			// Token: 0x04004AAF RID: 19119
			private OnXmlDictionaryReaderClose onStreamedReaderClose;

			// Token: 0x04004AB0 RID: 19120
			private TextMessageEncoderFactory.ContentEncoding[] contentEncodingMap;

			// Token: 0x04004AB1 RID: 19121
			private static readonly byte[] xmlDeclarationStartText = new byte[]
			{
				60,
				63,
				120,
				109,
				108
			};

			// Token: 0x04004AB2 RID: 19122
			private static readonly byte[] version10Text = new byte[]
			{
				118,
				101,
				114,
				115,
				105,
				111,
				110,
				61,
				34,
				49,
				46,
				48,
				34
			};

			// Token: 0x04004AB3 RID: 19123
			private static readonly byte[] encodingText = new byte[]
			{
				101,
				110,
				99,
				111,
				100,
				105,
				110,
				103,
				61
			};

			// Token: 0x02000F8D RID: 3981
			private class WriteMessageAsyncResult : AsyncResult
			{
				// Token: 0x0600884D RID: 34893 RVA: 0x001FAAEC File Offset: 0x001F8CEC
				public WriteMessageAsyncResult(Message message, Stream stream, TextMessageEncoderFactory.TextMessageEncoder textEncoder, AsyncCallback callback, object state) : base(callback, state)
				{
					this.message = message;
					this.textEncoder = textEncoder;
					this.xmlWriter = textEncoder.TakeStreamedWriter(stream);
					this.eventTraceActivity = null;
					if (TD.TextMessageEncodingStartIsEnabled())
					{
						this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
						TD.TextMessageEncodingStart(this.eventTraceActivity);
					}
					if (!textEncoder.optimizeWriteForUTF8)
					{
						this.xmlWriter.WriteStartDocument();
					}
					IAsyncResult result = message.BeginWriteMessage(this.xmlWriter, base.PrepareAsyncCompletion(TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult.onWriteMessage), this);
					if (base.SyncContinue(result))
					{
						base.Complete(true);
					}
				}

				// Token: 0x0600884E RID: 34894 RVA: 0x001FAB80 File Offset: 0x001F8D80
				private static bool OnWriteMessage(IAsyncResult result)
				{
					TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult writeMessageAsyncResult = (TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult)result.AsyncState;
					return writeMessageAsyncResult.HandleWriteMessage(result);
				}

				// Token: 0x0600884F RID: 34895 RVA: 0x001FABA0 File Offset: 0x001F8DA0
				private bool HandleWriteMessage(IAsyncResult result)
				{
					this.message.EndWriteMessage(result);
					if (!this.textEncoder.optimizeWriteForUTF8)
					{
						this.xmlWriter.WriteEndDocument();
					}
					this.xmlWriter.Flush();
					this.textEncoder.ReturnStreamedWriter(this.xmlWriter);
					if (TD.MessageWrittenAsynchronouslyByEncoderIsEnabled())
					{
						TD.MessageWrittenAsynchronouslyByEncoder(this.eventTraceActivity ?? EventTraceActivityHelper.TryExtractActivity(this.message));
					}
					if (MessageLogger.LogMessagesAtTransportLevel)
					{
						MessageLogger.LogMessage(ref this.message, MessageLoggingSource.TransportSend);
					}
					return true;
				}

				// Token: 0x06008850 RID: 34896 RVA: 0x001FAC22 File Offset: 0x001F8E22
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult>(result);
				}

				// Token: 0x04004F83 RID: 20355
				private static AsyncResult.AsyncCompletion onWriteMessage = new AsyncResult.AsyncCompletion(TextMessageEncoderFactory.TextMessageEncoder.WriteMessageAsyncResult.OnWriteMessage);

				// Token: 0x04004F84 RID: 20356
				private Message message;

				// Token: 0x04004F85 RID: 20357
				private TextMessageEncoderFactory.TextMessageEncoder textEncoder;

				// Token: 0x04004F86 RID: 20358
				private XmlDictionaryWriter xmlWriter;

				// Token: 0x04004F87 RID: 20359
				private EventTraceActivity eventTraceActivity;
			}

			// Token: 0x02000F8E RID: 3982
			private class UTF8BufferedMessageData : BufferedMessageData
			{
				// Token: 0x06008852 RID: 34898 RVA: 0x001FAC3E File Offset: 0x001F8E3E
				public UTF8BufferedMessageData(TextMessageEncoderFactory.TextMessageEncoder messageEncoder, int maxReaderPoolSize) : base(messageEncoder.RecycledStatePool)
				{
					this.messageEncoder = messageEncoder;
					this.readerPool = new Pool<XmlDictionaryReader>(maxReaderPoolSize);
					this.onClose = new OnXmlDictionaryReaderClose(base.OnXmlReaderClosed);
				}

				// Token: 0x17001DA9 RID: 7593
				// (set) Token: 0x06008853 RID: 34899 RVA: 0x001FAC71 File Offset: 0x001F8E71
				internal Encoding Encoding
				{
					set
					{
						this.encoding = value;
					}
				}

				// Token: 0x17001DAA RID: 7594
				// (get) Token: 0x06008854 RID: 34900 RVA: 0x001FAC7A File Offset: 0x001F8E7A
				public override MessageEncoder MessageEncoder
				{
					get
					{
						return this.messageEncoder;
					}
				}

				// Token: 0x17001DAB RID: 7595
				// (get) Token: 0x06008855 RID: 34901 RVA: 0x001FAC82 File Offset: 0x001F8E82
				public override XmlDictionaryReaderQuotas Quotas
				{
					get
					{
						return this.messageEncoder.bufferedReadReaderQuotas;
					}
				}

				// Token: 0x06008856 RID: 34902 RVA: 0x001FAC8F File Offset: 0x001F8E8F
				protected override void OnClosed()
				{
					this.messageEncoder.ReturnBufferedData(this);
				}

				// Token: 0x06008857 RID: 34903 RVA: 0x001FACA0 File Offset: 0x001F8EA0
				protected override XmlDictionaryReader TakeXmlReader()
				{
					ArraySegment<byte> buffer = base.Buffer;
					XmlDictionaryReader xmlDictionaryReader = this.readerPool.Take();
					if (xmlDictionaryReader == null)
					{
						xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(buffer.Array, buffer.Offset, buffer.Count, this.encoding, this.Quotas, this.onClose);
						if (TD.ReadPoolMissIsEnabled())
						{
							TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
						}
					}
					else
					{
						((IXmlTextReaderInitializer)xmlDictionaryReader).SetInput(buffer.Array, buffer.Offset, buffer.Count, this.encoding, this.Quotas, this.onClose);
					}
					return xmlDictionaryReader;
				}

				// Token: 0x06008858 RID: 34904 RVA: 0x001FAD3C File Offset: 0x001F8F3C
				protected override void ReturnXmlReader(XmlDictionaryReader xmlReader)
				{
					if (xmlReader != null)
					{
						this.readerPool.Return(xmlReader);
					}
				}

				// Token: 0x04004F88 RID: 20360
				private TextMessageEncoderFactory.TextMessageEncoder messageEncoder;

				// Token: 0x04004F89 RID: 20361
				private Pool<XmlDictionaryReader> readerPool;

				// Token: 0x04004F8A RID: 20362
				private OnXmlDictionaryReaderClose onClose;

				// Token: 0x04004F8B RID: 20363
				private Encoding encoding;

				// Token: 0x04004F8C RID: 20364
				private const int additionalNodeSpace = 1024;
			}

			// Token: 0x02000F8F RID: 3983
			private class TextBufferedMessageWriter : BufferedMessageWriter
			{
				// Token: 0x06008859 RID: 34905 RVA: 0x001FAD4E File Offset: 0x001F8F4E
				public TextBufferedMessageWriter(TextMessageEncoderFactory.TextMessageEncoder messageEncoder)
				{
					this.messageEncoder = messageEncoder;
				}

				// Token: 0x0600885A RID: 34906 RVA: 0x001FAD5D File Offset: 0x001F8F5D
				protected override void OnWriteStartMessage(XmlDictionaryWriter writer)
				{
					if (!this.messageEncoder.optimizeWriteForUTF8)
					{
						writer.WriteStartDocument();
					}
				}

				// Token: 0x0600885B RID: 34907 RVA: 0x001FAD72 File Offset: 0x001F8F72
				protected override void OnWriteEndMessage(XmlDictionaryWriter writer)
				{
					if (!this.messageEncoder.optimizeWriteForUTF8)
					{
						writer.WriteEndDocument();
					}
				}

				// Token: 0x0600885C RID: 34908 RVA: 0x001FAD88 File Offset: 0x001F8F88
				protected override XmlDictionaryWriter TakeXmlWriter(Stream stream)
				{
					if (this.messageEncoder.optimizeWriteForUTF8)
					{
						XmlDictionaryWriter xmlDictionaryWriter = this.writer;
						if (xmlDictionaryWriter == null)
						{
							xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(stream, this.messageEncoder.writeEncoding, false);
						}
						else
						{
							this.writer = null;
							((IXmlTextWriterInitializer)xmlDictionaryWriter).SetOutput(stream, this.messageEncoder.writeEncoding, false);
						}
						return xmlDictionaryWriter;
					}
					return this.messageEncoder.CreateWriter(stream);
				}

				// Token: 0x0600885D RID: 34909 RVA: 0x001FADEE File Offset: 0x001F8FEE
				protected override void ReturnXmlWriter(XmlDictionaryWriter writer)
				{
					writer.Close();
					if (this.messageEncoder.optimizeWriteForUTF8 && this.writer == null)
					{
						this.writer = writer;
					}
				}

				// Token: 0x04004F8D RID: 20365
				private TextMessageEncoderFactory.TextMessageEncoder messageEncoder;

				// Token: 0x04004F8E RID: 20366
				private XmlDictionaryWriter writer;
			}
		}
	}
}
