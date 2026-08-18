using System;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E4 RID: 2532
	internal class MtomMessageEncoder : MessageEncoder, ITraceSourceStringProvider
	{
		// Token: 0x060063EF RID: 25583 RVA: 0x001750E0 File Offset: 0x001732E0
		public MtomMessageEncoder(MessageVersion version, Encoding writeEncoding, int maxReadPoolSize, int maxWritePoolSize, int maxBufferSize, XmlDictionaryReaderQuotas quotas)
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
			this.maxReadPoolSize = maxReadPoolSize;
			this.maxWritePoolSize = maxWritePoolSize;
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			quotas.CopyTo(this.readerQuotas);
			this.bufferedReadReaderQuotas = EncoderHelpers.GetBufferedReadQuotas(this.readerQuotas);
			this.maxBufferSize = maxBufferSize;
			this.onStreamedReaderClose = new OnXmlDictionaryReaderClose(this.ReturnStreamedReader);
			this.thisLock = new object();
			if (version.Envelope == EnvelopeVersion.Soap12)
			{
				this.contentEncodingMap = TextMessageEncoderFactory.Soap12Content;
			}
			else
			{
				if (version.Envelope != EnvelopeVersion.Soap11)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid MessageVersion", new object[0])));
				}
				this.contentEncodingMap = TextMessageEncoderFactory.Soap11Content;
			}
			this.version = version;
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x001751E3 File Offset: 0x001733E3
		private static UriGenerator MimeBoundaryGenerator
		{
			get
			{
				if (MtomMessageEncoder.mimeBoundaryGenerator == null)
				{
					MtomMessageEncoder.mimeBoundaryGenerator = new UriGenerator("uuid", "+");
				}
				return MtomMessageEncoder.mimeBoundaryGenerator;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x060063F1 RID: 25585 RVA: 0x00175205 File Offset: 0x00173405
		public override string ContentType
		{
			get
			{
				return "multipart/related; type=\"application/xop+xml\"";
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060063F2 RID: 25586 RVA: 0x0017520C File Offset: 0x0017340C
		public int MaxWritePoolSize
		{
			get
			{
				return this.maxWritePoolSize;
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x00175214 File Offset: 0x00173414
		public int MaxReadPoolSize
		{
			get
			{
				return this.maxReadPoolSize;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x0017521C File Offset: 0x0017341C
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060063F5 RID: 25589 RVA: 0x00175224 File Offset: 0x00173424
		public int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060063F6 RID: 25590 RVA: 0x0017522C File Offset: 0x0017342C
		public override string MediaType
		{
			get
			{
				return "multipart/related";
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060063F7 RID: 25591 RVA: 0x00175233 File Offset: 0x00173433
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x0017523B File Offset: 0x0017343B
		internal bool IsMTOMContentType(string contentType)
		{
			return base.IsContentTypeSupported(contentType, this.ContentType, this.MediaType);
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x00175250 File Offset: 0x00173450
		internal bool IsTextContentType(string contentType)
		{
			string mediaType = TextMessageEncoderFactory.GetMediaType(this.version);
			string contentType2 = TextMessageEncoderFactory.GetContentType(mediaType, this.writeEncoding);
			return base.IsContentTypeSupported(contentType, contentType2, mediaType);
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x0017527F File Offset: 0x0017347F
		public override bool IsContentTypeSupported(string contentType)
		{
			if (contentType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("contentType"));
			}
			return this.IsMTOMContentType(contentType) || this.IsTextContentType(contentType);
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001752AC File Offset: 0x001734AC
		internal override bool IsCharSetSupported(string charSet)
		{
			Encoding encoding;
			return charSet == null || charSet.Length == 0 || TextEncoderDefaults.TryGetEncoding(charSet, out encoding);
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x001752CE File Offset: 0x001734CE
		private string GenerateStartInfoString()
		{
			if (this.version.Envelope != EnvelopeVersion.Soap12)
			{
				return "text/xml";
			}
			return "application/soap+xml";
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x001752F0 File Offset: 0x001734F0
		public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
		{
			if (bufferManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bufferManager");
			}
			if (contentType == this.ContentType)
			{
				contentType = null;
			}
			if (TD.MtomMessageDecodingStartIsEnabled())
			{
				TD.MtomMessageDecodingStart();
			}
			MtomMessageEncoder.MtomBufferedMessageData mtomBufferedMessageData = this.TakeBufferedReader();
			mtomBufferedMessageData.ContentType = contentType;
			mtomBufferedMessageData.Open(buffer, bufferManager);
			RecycledMessageState recycledMessageState = mtomBufferedMessageData.TakeMessageState();
			if (recycledMessageState == null)
			{
				recycledMessageState = new RecycledMessageState();
			}
			Message message = new BufferedMessage(mtomBufferedMessageData, recycledMessageState);
			message.Properties.Encoder = this;
			if (MessageLogger.LogMessagesAtTransportLevel)
			{
				MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportReceive);
			}
			if (TD.MessageReadByEncoderIsEnabled())
			{
				TD.MessageReadByEncoder(EventTraceActivityHelper.TryExtractActivity(message, true), buffer.Count, this);
			}
			return message;
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x00175394 File Offset: 0x00173594
		public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
			}
			if (contentType == this.ContentType)
			{
				contentType = null;
			}
			if (TD.MtomMessageDecodingStartIsEnabled())
			{
				TD.MtomMessageDecodingStart();
			}
			XmlReader envelopeReader = this.TakeStreamedReader(stream, contentType);
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

		// Token: 0x060063FF RID: 25599 RVA: 0x0017541C File Offset: 0x0017361C
		public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
		{
			return this.WriteMessage(message, maxMessageSize, bufferManager, messageOffset, this.GenerateStartInfoString(), null, null, true);
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x00175440 File Offset: 0x00173640
		internal string GetContentType(out string boundary)
		{
			string startInfo = this.GenerateStartInfoString();
			boundary = MtomMessageEncoder.MimeBoundaryGenerator.Next();
			return this.FormatContentType(boundary, startInfo);
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x00175469 File Offset: 0x00173669
		internal string FormatContentType(string boundary, string startInfo)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0};start=\"<{1}>\";boundary=\"{2}\";start-info=\"{3}\"", new object[]
			{
				"multipart/related; type=\"application/xop+xml\"",
				"http://tempuri.org/0",
				boundary,
				startInfo
			});
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x00175498 File Offset: 0x00173698
		internal ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset, string boundary)
		{
			return this.WriteMessage(message, maxMessageSize, bufferManager, messageOffset, this.GenerateStartInfoString(), boundary, "http://tempuri.org/0", false);
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x001754C0 File Offset: 0x001736C0
		private ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset, string startInfo, string boundary, string startUri, bool writeMessageHeaders)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (bufferManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bufferManager");
			}
			if (maxMessageSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxMessageSize", maxMessageSize, SR.GetString("ValueMustBeNonNegative")));
			}
			if (messageOffset < 0 || messageOffset > maxMessageSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageOffset", messageOffset, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					maxMessageSize
				})));
			}
			base.ThrowIfMismatchedMessageVersion(message);
			EventTraceActivity eventTraceActivity = null;
			if (TD.MtomMessageEncodingStartIsEnabled())
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				TD.MtomMessageEncodingStart(eventTraceActivity);
			}
			message.Properties.Encoder = this;
			MtomMessageEncoder.MtomBufferedMessageWriter mtomBufferedMessageWriter = this.TakeBufferedWriter();
			mtomBufferedMessageWriter.StartInfo = startInfo;
			mtomBufferedMessageWriter.Boundary = boundary;
			mtomBufferedMessageWriter.StartUri = startUri;
			mtomBufferedMessageWriter.WriteMessageHeaders = writeMessageHeaders;
			mtomBufferedMessageWriter.MaxSizeInBytes = maxMessageSize;
			ArraySegment<byte> result = mtomBufferedMessageWriter.WriteMessage(message, bufferManager, messageOffset, maxMessageSize);
			this.ReturnMessageWriter(mtomBufferedMessageWriter);
			if (TD.MessageWrittenByEncoderIsEnabled())
			{
				TD.MessageWrittenByEncoder(eventTraceActivity ?? EventTraceActivityHelper.TryExtractActivity(message), result.Count, this);
			}
			if (MessageLogger.LogMessagesAtTransportLevel)
			{
				string contentType = null;
				if (boundary != null)
				{
					contentType = this.FormatContentType(boundary, startInfo ?? this.GenerateStartInfoString());
				}
				XmlDictionaryReader reader = XmlDictionaryReader.CreateMtomReader(result.Array, result.Offset, result.Count, MtomMessageEncoderFactory.GetSupportedEncodings(), contentType, XmlDictionaryReaderQuotas.Max, int.MaxValue, null);
				MessageLogger.LogMessage(ref message, reader, MessageLoggingSource.TransportSend);
			}
			return result;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x00175647 File Offset: 0x00173847
		public override void WriteMessage(Message message, Stream stream)
		{
			this.WriteMessage(message, stream, this.GenerateStartInfoString(), null, null, true);
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x0017565A File Offset: 0x0017385A
		internal void WriteMessage(Message message, Stream stream, string boundary)
		{
			this.WriteMessage(message, stream, this.GenerateStartInfoString(), boundary, "http://tempuri.org/0", false);
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00175671 File Offset: 0x00173871
		public override IAsyncResult BeginWriteMessage(Message message, Stream stream, AsyncCallback callback, object state)
		{
			return new MtomMessageEncoder.WriteMessageAsyncResult(message, stream, this, callback, state);
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x0017567E File Offset: 0x0017387E
		internal IAsyncResult BeginWriteMessage(Message message, Stream stream, string boundary, AsyncCallback callback, object state)
		{
			return new MtomMessageEncoder.WriteMessageAsyncResult(message, stream, boundary, this, callback, state);
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0017568D File Offset: 0x0017388D
		public override void EndWriteMessage(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x00175698 File Offset: 0x00173898
		private void WriteMessage(Message message, Stream stream, string startInfo, string boundary, string startUri, bool writeMessageHeaders)
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
			if (TD.MtomMessageEncodingStartIsEnabled())
			{
				eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				TD.MtomMessageEncodingStart(eventTraceActivity);
			}
			message.Properties.Encoder = this;
			if (MessageLogger.LogMessagesAtTransportLevel)
			{
				MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportSend);
			}
			XmlDictionaryWriter xmlDictionaryWriter = this.TakeStreamedWriter(stream, startInfo, boundary, startUri, writeMessageHeaders);
			if (this.writeEncoding.WebName == "utf-8")
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
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x00175770 File Offset: 0x00173970
		private XmlDictionaryWriter TakeStreamedWriter(Stream stream, string startInfo, string boundary, string startUri, bool writeMessageHeaders)
		{
			if (this.streamedWriterPool == null)
			{
				object obj = this.thisLock;
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
				xmlDictionaryWriter = XmlDictionaryWriter.CreateMtomWriter(stream, this.writeEncoding, int.MaxValue, startInfo, boundary, startUri, writeMessageHeaders, false);
				if (TD.WritePoolMissIsEnabled())
				{
					TD.WritePoolMiss(xmlDictionaryWriter.GetType().Name);
				}
			}
			else
			{
				((IXmlMtomWriterInitializer)xmlDictionaryWriter).SetOutput(stream, this.writeEncoding, int.MaxValue, startInfo, boundary, startUri, writeMessageHeaders, false);
			}
			return xmlDictionaryWriter;
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x00175834 File Offset: 0x00173A34
		private void ReturnStreamedWriter(XmlDictionaryWriter xmlWriter)
		{
			xmlWriter.Close();
			this.streamedWriterPool.Return(xmlWriter);
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x0017584C File Offset: 0x00173A4C
		private MtomMessageEncoder.MtomBufferedMessageWriter TakeBufferedWriter()
		{
			if (this.bufferedWriterPool == null)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.bufferedWriterPool == null)
					{
						this.bufferedWriterPool = new SynchronizedPool<MtomMessageEncoder.MtomBufferedMessageWriter>(this.maxWritePoolSize);
					}
				}
			}
			MtomMessageEncoder.MtomBufferedMessageWriter mtomBufferedMessageWriter = this.bufferedWriterPool.Take();
			if (mtomBufferedMessageWriter == null)
			{
				mtomBufferedMessageWriter = new MtomMessageEncoder.MtomBufferedMessageWriter(this);
				if (TD.WritePoolMissIsEnabled())
				{
					TD.WritePoolMiss(mtomBufferedMessageWriter.GetType().Name);
				}
			}
			return mtomBufferedMessageWriter;
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x001758E0 File Offset: 0x00173AE0
		private void ReturnMessageWriter(MtomMessageEncoder.MtomBufferedMessageWriter messageWriter)
		{
			this.bufferedWriterPool.Return(messageWriter);
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x001758F4 File Offset: 0x00173AF4
		private MtomMessageEncoder.MtomBufferedMessageData TakeBufferedReader()
		{
			if (this.bufferedReaderPool == null)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.bufferedReaderPool == null)
					{
						this.bufferedReaderPool = new SynchronizedPool<MtomMessageEncoder.MtomBufferedMessageData>(this.maxReadPoolSize);
					}
				}
			}
			MtomMessageEncoder.MtomBufferedMessageData mtomBufferedMessageData = this.bufferedReaderPool.Take();
			if (mtomBufferedMessageData == null)
			{
				mtomBufferedMessageData = new MtomMessageEncoder.MtomBufferedMessageData(this, 2);
				if (TD.ReadPoolMissIsEnabled())
				{
					TD.ReadPoolMiss(mtomBufferedMessageData.GetType().Name);
				}
			}
			return mtomBufferedMessageData;
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x00175988 File Offset: 0x00173B88
		private void ReturnBufferedData(MtomMessageEncoder.MtomBufferedMessageData messageData)
		{
			this.bufferedReaderPool.Return(messageData);
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x0017599C File Offset: 0x00173B9C
		private XmlReader TakeStreamedReader(Stream stream, string contentType)
		{
			if (this.streamedReaderPool == null)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.streamedReaderPool == null)
					{
						this.streamedReaderPool = new SynchronizedPool<XmlDictionaryReader>(this.maxReadPoolSize);
					}
				}
			}
			XmlDictionaryReader xmlDictionaryReader = this.streamedReaderPool.Take();
			try
			{
				if (contentType == null || this.IsMTOMContentType(contentType))
				{
					if (xmlDictionaryReader != null && xmlDictionaryReader is IXmlMtomReaderInitializer)
					{
						((IXmlMtomReaderInitializer)xmlDictionaryReader).SetInput(stream, MtomMessageEncoderFactory.GetSupportedEncodings(), contentType, this.readerQuotas, this.maxBufferSize, this.onStreamedReaderClose);
					}
					else
					{
						xmlDictionaryReader = XmlDictionaryReader.CreateMtomReader(stream, MtomMessageEncoderFactory.GetSupportedEncodings(), contentType, this.readerQuotas, this.maxBufferSize, this.onStreamedReaderClose);
						if (TD.ReadPoolMissIsEnabled())
						{
							TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
						}
					}
				}
				else if (xmlDictionaryReader != null && xmlDictionaryReader is IXmlTextReaderInitializer)
				{
					((IXmlTextReaderInitializer)xmlDictionaryReader).SetInput(stream, TextMessageEncoderFactory.GetEncodingFromContentType(contentType, this.contentEncodingMap), this.readerQuotas, this.onStreamedReaderClose);
				}
				else
				{
					xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(stream, TextMessageEncoderFactory.GetEncodingFromContentType(contentType, this.contentEncodingMap), this.readerQuotas, this.onStreamedReaderClose);
					if (TD.ReadPoolMissIsEnabled())
					{
						TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
					}
				}
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorCreatingMtomReader"), innerException));
			}
			catch (XmlException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorCreatingMtomReader"), innerException2));
			}
			return xmlDictionaryReader;
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00175B40 File Offset: 0x00173D40
		private void ReturnStreamedReader(XmlDictionaryReader xmlReader)
		{
			this.streamedReaderPool.Return(xmlReader);
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06006412 RID: 25618 RVA: 0x00175B54 File Offset: 0x00173D54
		private SynchronizedPool<RecycledMessageState> RecycledStatePool
		{
			get
			{
				if (this.recycledStatePool == null)
				{
					object obj = this.thisLock;
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

		// Token: 0x06006413 RID: 25619 RVA: 0x00175BC0 File Offset: 0x00173DC0
		string ITraceSourceStringProvider.GetSourceString()
		{
			return base.GetTraceSourceString();
		}

		// Token: 0x040039A0 RID: 14752
		private Encoding writeEncoding;

		// Token: 0x040039A1 RID: 14753
		private volatile SynchronizedPool<XmlDictionaryWriter> streamedWriterPool;

		// Token: 0x040039A2 RID: 14754
		private volatile SynchronizedPool<XmlDictionaryReader> streamedReaderPool;

		// Token: 0x040039A3 RID: 14755
		private volatile SynchronizedPool<MtomMessageEncoder.MtomBufferedMessageData> bufferedReaderPool;

		// Token: 0x040039A4 RID: 14756
		private volatile SynchronizedPool<MtomMessageEncoder.MtomBufferedMessageWriter> bufferedWriterPool;

		// Token: 0x040039A5 RID: 14757
		private volatile SynchronizedPool<RecycledMessageState> recycledStatePool;

		// Token: 0x040039A6 RID: 14758
		private object thisLock;

		// Token: 0x040039A7 RID: 14759
		private MessageVersion version;

		// Token: 0x040039A8 RID: 14760
		private const int maxPooledXmlReadersPerMessage = 2;

		// Token: 0x040039A9 RID: 14761
		private int maxReadPoolSize;

		// Token: 0x040039AA RID: 14762
		private int maxWritePoolSize;

		// Token: 0x040039AB RID: 14763
		private static UriGenerator mimeBoundaryGenerator;

		// Token: 0x040039AC RID: 14764
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x040039AD RID: 14765
		private XmlDictionaryReaderQuotas bufferedReadReaderQuotas;

		// Token: 0x040039AE RID: 14766
		private int maxBufferSize;

		// Token: 0x040039AF RID: 14767
		private OnXmlDictionaryReaderClose onStreamedReaderClose;

		// Token: 0x040039B0 RID: 14768
		internal TextMessageEncoderFactory.ContentEncoding[] contentEncodingMap;

		// Token: 0x040039B1 RID: 14769
		private const string mtomMediaType = "multipart/related";

		// Token: 0x040039B2 RID: 14770
		private const string mtomContentType = "multipart/related; type=\"application/xop+xml\"";

		// Token: 0x040039B3 RID: 14771
		private const string mtomStartUri = "http://tempuri.org/0";

		// Token: 0x02000E54 RID: 3668
		private class MtomBufferedMessageData : BufferedMessageData
		{
			// Token: 0x0600831B RID: 33563 RVA: 0x001E50C8 File Offset: 0x001E32C8
			public MtomBufferedMessageData(MtomMessageEncoder messageEncoder, int maxReaderPoolSize) : base(messageEncoder.RecycledStatePool)
			{
				this.messageEncoder = messageEncoder;
				this.readerPool = new Pool<XmlDictionaryReader>(maxReaderPoolSize);
				this.onClose = new OnXmlDictionaryReaderClose(base.OnXmlReaderClosed);
			}

			// Token: 0x17001D02 RID: 7426
			// (get) Token: 0x0600831C RID: 33564 RVA: 0x001E50FB File Offset: 0x001E32FB
			public override MessageEncoder MessageEncoder
			{
				get
				{
					return this.messageEncoder;
				}
			}

			// Token: 0x17001D03 RID: 7427
			// (get) Token: 0x0600831D RID: 33565 RVA: 0x001E5103 File Offset: 0x001E3303
			public override XmlDictionaryReaderQuotas Quotas
			{
				get
				{
					return this.messageEncoder.bufferedReadReaderQuotas;
				}
			}

			// Token: 0x0600831E RID: 33566 RVA: 0x001E5110 File Offset: 0x001E3310
			protected override void OnClosed()
			{
				this.messageEncoder.ReturnBufferedData(this);
			}

			// Token: 0x0600831F RID: 33567 RVA: 0x001E5120 File Offset: 0x001E3320
			protected override XmlDictionaryReader TakeXmlReader()
			{
				XmlDictionaryReader result;
				try
				{
					ArraySegment<byte> buffer = base.Buffer;
					XmlDictionaryReader xmlDictionaryReader = this.readerPool.Take();
					if (this.ContentType == null || this.messageEncoder.IsMTOMContentType(this.ContentType))
					{
						if (xmlDictionaryReader != null && xmlDictionaryReader is IXmlMtomReaderInitializer)
						{
							((IXmlMtomReaderInitializer)xmlDictionaryReader).SetInput(buffer.Array, buffer.Offset, buffer.Count, MtomMessageEncoderFactory.GetSupportedEncodings(), this.ContentType, this.Quotas, this.messageEncoder.MaxBufferSize, this.onClose);
						}
						else
						{
							xmlDictionaryReader = XmlDictionaryReader.CreateMtomReader(buffer.Array, buffer.Offset, buffer.Count, MtomMessageEncoderFactory.GetSupportedEncodings(), this.ContentType, this.Quotas, this.messageEncoder.MaxBufferSize, this.onClose);
							if (TD.ReadPoolMissIsEnabled())
							{
								TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
							}
						}
					}
					else if (xmlDictionaryReader != null && xmlDictionaryReader is IXmlTextReaderInitializer)
					{
						((IXmlTextReaderInitializer)xmlDictionaryReader).SetInput(buffer.Array, buffer.Offset, buffer.Count, TextMessageEncoderFactory.GetEncodingFromContentType(this.ContentType, this.messageEncoder.contentEncodingMap), this.Quotas, this.onClose);
					}
					else
					{
						xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(buffer.Array, buffer.Offset, buffer.Count, TextMessageEncoderFactory.GetEncodingFromContentType(this.ContentType, this.messageEncoder.contentEncodingMap), this.Quotas, this.onClose);
						if (TD.ReadPoolMissIsEnabled())
						{
							TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
						}
					}
					result = xmlDictionaryReader;
				}
				catch (FormatException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorCreatingMtomReader"), innerException));
				}
				catch (XmlException innerException2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("SFxErrorCreatingMtomReader"), innerException2));
				}
				return result;
			}

			// Token: 0x06008320 RID: 33568 RVA: 0x001E5324 File Offset: 0x001E3524
			protected override void ReturnXmlReader(XmlDictionaryReader xmlReader)
			{
				if (xmlReader != null)
				{
					this.readerPool.Return(xmlReader);
				}
			}

			// Token: 0x04004A8D RID: 19085
			private MtomMessageEncoder messageEncoder;

			// Token: 0x04004A8E RID: 19086
			private Pool<XmlDictionaryReader> readerPool;

			// Token: 0x04004A8F RID: 19087
			internal string ContentType;

			// Token: 0x04004A90 RID: 19088
			private OnXmlDictionaryReaderClose onClose;
		}

		// Token: 0x02000E55 RID: 3669
		private class MtomBufferedMessageWriter : BufferedMessageWriter
		{
			// Token: 0x06008321 RID: 33569 RVA: 0x001E5336 File Offset: 0x001E3536
			public MtomBufferedMessageWriter(MtomMessageEncoder messageEncoder)
			{
				this.messageEncoder = messageEncoder;
			}

			// Token: 0x06008322 RID: 33570 RVA: 0x001E5350 File Offset: 0x001E3550
			protected override XmlDictionaryWriter TakeXmlWriter(Stream stream)
			{
				XmlDictionaryWriter xmlDictionaryWriter = this.writer;
				if (xmlDictionaryWriter == null)
				{
					xmlDictionaryWriter = XmlDictionaryWriter.CreateMtomWriter(stream, this.messageEncoder.writeEncoding, this.MaxSizeInBytes, this.StartInfo, this.Boundary, this.StartUri, this.WriteMessageHeaders, false);
				}
				else
				{
					this.writer = null;
					((IXmlMtomWriterInitializer)xmlDictionaryWriter).SetOutput(stream, this.messageEncoder.writeEncoding, this.MaxSizeInBytes, this.StartInfo, this.Boundary, this.StartUri, this.WriteMessageHeaders, false);
				}
				if (this.messageEncoder.writeEncoding.WebName != "utf-8")
				{
					xmlDictionaryWriter.WriteStartDocument();
				}
				return xmlDictionaryWriter;
			}

			// Token: 0x06008323 RID: 33571 RVA: 0x001E53FA File Offset: 0x001E35FA
			protected override void ReturnXmlWriter(XmlDictionaryWriter writer)
			{
				writer.Close();
				if (this.writer == null)
				{
					this.writer = writer;
				}
			}

			// Token: 0x04004A91 RID: 19089
			private MtomMessageEncoder messageEncoder;

			// Token: 0x04004A92 RID: 19090
			internal bool WriteMessageHeaders;

			// Token: 0x04004A93 RID: 19091
			internal string StartInfo;

			// Token: 0x04004A94 RID: 19092
			internal string StartUri;

			// Token: 0x04004A95 RID: 19093
			internal string Boundary;

			// Token: 0x04004A96 RID: 19094
			internal int MaxSizeInBytes = int.MaxValue;

			// Token: 0x04004A97 RID: 19095
			private XmlDictionaryWriter writer;
		}

		// Token: 0x02000E56 RID: 3670
		private class WriteMessageAsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x06008324 RID: 33572 RVA: 0x001E5411 File Offset: 0x001E3611
			public WriteMessageAsyncResult(Message message, Stream stream, MtomMessageEncoder encoder, AsyncCallback callback, object state) : base(callback, state)
			{
				this.encoder = encoder;
				this.message = message;
				this.stream = stream;
				base.Schedule();
			}

			// Token: 0x06008325 RID: 33573 RVA: 0x001E5438 File Offset: 0x001E3638
			public WriteMessageAsyncResult(Message message, Stream stream, string boundary, MtomMessageEncoder encoder, AsyncCallback callback, object state) : base(callback, state)
			{
				this.encoder = encoder;
				this.message = message;
				this.stream = stream;
				this.boundary = boundary;
				this.writeBoundary = true;
				base.Schedule();
			}

			// Token: 0x06008326 RID: 33574 RVA: 0x001E5470 File Offset: 0x001E3670
			protected override void OnDoWork()
			{
				this.encoder.WriteMessage(this.message, this.stream, this.encoder.GenerateStartInfoString(), string.IsNullOrEmpty(this.boundary) ? null : this.boundary, this.writeBoundary ? "http://tempuri.org/0" : null, !this.writeBoundary);
			}

			// Token: 0x04004A98 RID: 19096
			private string boundary;

			// Token: 0x04004A99 RID: 19097
			private MtomMessageEncoder encoder;

			// Token: 0x04004A9A RID: 19098
			private Message message;

			// Token: 0x04004A9B RID: 19099
			private Stream stream;

			// Token: 0x04004A9C RID: 19100
			private bool writeBoundary;
		}
	}
}
