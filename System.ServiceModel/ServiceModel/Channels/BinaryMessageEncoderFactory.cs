using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D7 RID: 2519
	internal class BinaryMessageEncoderFactory : MessageEncoderFactory
	{
		// Token: 0x06006386 RID: 25478 RVA: 0x00173B84 File Offset: 0x00171D84
		public BinaryMessageEncoderFactory(MessageVersion messageVersion, int maxReadPoolSize, int maxWritePoolSize, int maxSessionSize, XmlDictionaryReaderQuotas readerQuotas, long maxReceivedMessageSize, BinaryVersion version, CompressionFormat compressionFormat)
		{
			this.messageVersion = messageVersion;
			this.maxReadPoolSize = maxReadPoolSize;
			this.maxWritePoolSize = maxWritePoolSize;
			this.maxSessionSize = maxSessionSize;
			this.thisLock = new object();
			this.onStreamedReaderClose = new OnXmlDictionaryReaderClose(this.ReturnStreamedReader);
			this.readerQuotas = new XmlDictionaryReaderQuotas();
			if (readerQuotas != null)
			{
				readerQuotas.CopyTo(this.readerQuotas);
			}
			this.bufferedReadReaderQuotas = EncoderHelpers.GetBufferedReadQuotas(this.readerQuotas);
			this.MaxReceivedMessageSize = maxReceivedMessageSize;
			this.binaryVersion = version;
			this.compressionFormat = compressionFormat;
			this.messageEncoder = new BinaryMessageEncoderFactory.BinaryMessageEncoder(this, false, 0);
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x06006387 RID: 25479 RVA: 0x00173C24 File Offset: 0x00171E24
		public static IXmlDictionary XmlDictionary
		{
			get
			{
				return XD.Dictionary;
			}
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x00173C2B File Offset: 0x00171E2B
		public override MessageEncoder Encoder
		{
			get
			{
				return this.messageEncoder;
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06006389 RID: 25481 RVA: 0x00173C33 File Offset: 0x00171E33
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x00173C3B File Offset: 0x00171E3B
		public int MaxWritePoolSize
		{
			get
			{
				return this.maxWritePoolSize;
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x0600638B RID: 25483 RVA: 0x00173C43 File Offset: 0x00171E43
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x0600638C RID: 25484 RVA: 0x00173C4B File Offset: 0x00171E4B
		public int MaxReadPoolSize
		{
			get
			{
				return this.maxReadPoolSize;
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x0600638D RID: 25485 RVA: 0x00173C53 File Offset: 0x00171E53
		public int MaxSessionSize
		{
			get
			{
				return this.maxSessionSize;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x0600638E RID: 25486 RVA: 0x00173C5B File Offset: 0x00171E5B
		public CompressionFormat CompressionFormat
		{
			get
			{
				return this.compressionFormat;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x0600638F RID: 25487 RVA: 0x00173C63 File Offset: 0x00171E63
		// (set) Token: 0x06006390 RID: 25488 RVA: 0x00173C6B File Offset: 0x00171E6B
		private long MaxReceivedMessageSize { get; set; }

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06006391 RID: 25489 RVA: 0x00173C74 File Offset: 0x00171E74
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x00173C7C File Offset: 0x00171E7C
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

		// Token: 0x06006393 RID: 25491 RVA: 0x00173CE8 File Offset: 0x00171EE8
		public override MessageEncoder CreateSessionEncoder()
		{
			return new BinaryMessageEncoderFactory.BinaryMessageEncoder(this, true, this.maxSessionSize);
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x00173CF8 File Offset: 0x00171EF8
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
				xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream, this.binaryVersion.Dictionary, null, false);
				if (TD.WritePoolMissIsEnabled())
				{
					TD.WritePoolMiss(xmlDictionaryWriter.GetType().Name);
				}
			}
			else
			{
				((IXmlBinaryWriterInitializer)xmlDictionaryWriter).SetOutput(stream, this.binaryVersion.Dictionary, null, false);
			}
			return xmlDictionaryWriter;
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x00173DB4 File Offset: 0x00171FB4
		private void ReturnStreamedWriter(XmlDictionaryWriter xmlWriter)
		{
			xmlWriter.Close();
			this.streamedWriterPool.Return(xmlWriter);
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x00173DCC File Offset: 0x00171FCC
		private BinaryMessageEncoderFactory.BinaryBufferedMessageWriter TakeBufferedWriter()
		{
			if (this.bufferedWriterPool == null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.bufferedWriterPool == null)
					{
						this.bufferedWriterPool = new SynchronizedPool<BinaryMessageEncoderFactory.BinaryBufferedMessageWriter>(this.maxWritePoolSize);
					}
				}
			}
			BinaryMessageEncoderFactory.BinaryBufferedMessageWriter binaryBufferedMessageWriter = this.bufferedWriterPool.Take();
			if (binaryBufferedMessageWriter == null)
			{
				binaryBufferedMessageWriter = new BinaryMessageEncoderFactory.BinaryBufferedMessageWriter(this.binaryVersion.Dictionary);
				if (TD.WritePoolMissIsEnabled())
				{
					TD.WritePoolMiss(binaryBufferedMessageWriter.GetType().Name);
				}
			}
			return binaryBufferedMessageWriter;
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x00173E68 File Offset: 0x00172068
		private void ReturnMessageWriter(BinaryMessageEncoderFactory.BinaryBufferedMessageWriter messageWriter)
		{
			this.bufferedWriterPool.Return(messageWriter);
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x00173E7C File Offset: 0x0017207C
		private XmlDictionaryReader TakeStreamedReader(Stream stream)
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
				xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(stream, this.binaryVersion.Dictionary, this.readerQuotas, null, this.onStreamedReaderClose);
				if (TD.ReadPoolMissIsEnabled())
				{
					TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
				}
			}
			else
			{
				((IXmlBinaryReaderInitializer)xmlDictionaryReader).SetInput(stream, this.binaryVersion.Dictionary, this.readerQuotas, null, this.onStreamedReaderClose);
			}
			return xmlDictionaryReader;
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x00173F4C File Offset: 0x0017214C
		private void ReturnStreamedReader(XmlDictionaryReader xmlReader)
		{
			this.streamedReaderPool.Return(xmlReader);
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x00173F60 File Offset: 0x00172160
		private BinaryMessageEncoderFactory.BinaryBufferedMessageData TakeBufferedData(BinaryMessageEncoderFactory.BinaryMessageEncoder messageEncoder)
		{
			if (this.bufferedDataPool == null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.bufferedDataPool == null)
					{
						this.bufferedDataPool = new SynchronizedPool<BinaryMessageEncoderFactory.BinaryBufferedMessageData>(this.maxReadPoolSize);
					}
				}
			}
			BinaryMessageEncoderFactory.BinaryBufferedMessageData binaryBufferedMessageData = this.bufferedDataPool.Take();
			if (binaryBufferedMessageData == null)
			{
				binaryBufferedMessageData = new BinaryMessageEncoderFactory.BinaryBufferedMessageData(this, 2);
				if (TD.ReadPoolMissIsEnabled())
				{
					TD.ReadPoolMiss(binaryBufferedMessageData.GetType().Name);
				}
			}
			binaryBufferedMessageData.SetMessageEncoder(messageEncoder);
			return binaryBufferedMessageData;
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x00173FFC File Offset: 0x001721FC
		private void ReturnBufferedData(BinaryMessageEncoderFactory.BinaryBufferedMessageData messageData)
		{
			messageData.SetMessageEncoder(null);
			this.bufferedDataPool.Return(messageData);
		}

		// Token: 0x04003973 RID: 14707
		private const int maxPooledXmlReaderPerMessage = 2;

		// Token: 0x04003974 RID: 14708
		private BinaryMessageEncoderFactory.BinaryMessageEncoder messageEncoder;

		// Token: 0x04003975 RID: 14709
		private MessageVersion messageVersion;

		// Token: 0x04003976 RID: 14710
		private int maxReadPoolSize;

		// Token: 0x04003977 RID: 14711
		private int maxWritePoolSize;

		// Token: 0x04003978 RID: 14712
		private CompressionFormat compressionFormat;

		// Token: 0x04003979 RID: 14713
		private volatile SynchronizedPool<XmlDictionaryWriter> streamedWriterPool;

		// Token: 0x0400397A RID: 14714
		private volatile SynchronizedPool<XmlDictionaryReader> streamedReaderPool;

		// Token: 0x0400397B RID: 14715
		private volatile SynchronizedPool<BinaryMessageEncoderFactory.BinaryBufferedMessageData> bufferedDataPool;

		// Token: 0x0400397C RID: 14716
		private volatile SynchronizedPool<BinaryMessageEncoderFactory.BinaryBufferedMessageWriter> bufferedWriterPool;

		// Token: 0x0400397D RID: 14717
		private volatile SynchronizedPool<RecycledMessageState> recycledStatePool;

		// Token: 0x0400397E RID: 14718
		private object thisLock;

		// Token: 0x0400397F RID: 14719
		private int maxSessionSize;

		// Token: 0x04003980 RID: 14720
		private OnXmlDictionaryReaderClose onStreamedReaderClose;

		// Token: 0x04003981 RID: 14721
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x04003982 RID: 14722
		private XmlDictionaryReaderQuotas bufferedReadReaderQuotas;

		// Token: 0x04003983 RID: 14723
		private BinaryVersion binaryVersion;

		// Token: 0x02000E4D RID: 3661
		private class BinaryBufferedMessageData : BufferedMessageData
		{
			// Token: 0x060082E4 RID: 33508 RVA: 0x001E3B46 File Offset: 0x001E1D46
			public BinaryBufferedMessageData(BinaryMessageEncoderFactory factory, int maxPoolSize) : base(factory.RecycledStatePool)
			{
				this.factory = factory;
				this.readerPool = new Pool<XmlDictionaryReader>(maxPoolSize);
				this.onClose = new OnXmlDictionaryReaderClose(base.OnXmlReaderClosed);
			}

			// Token: 0x17001CF4 RID: 7412
			// (get) Token: 0x060082E5 RID: 33509 RVA: 0x001E3B79 File Offset: 0x001E1D79
			public override MessageEncoder MessageEncoder
			{
				get
				{
					return this.messageEncoder;
				}
			}

			// Token: 0x17001CF5 RID: 7413
			// (get) Token: 0x060082E6 RID: 33510 RVA: 0x001E3B81 File Offset: 0x001E1D81
			public override XmlDictionaryReaderQuotas Quotas
			{
				get
				{
					return this.factory.readerQuotas;
				}
			}

			// Token: 0x060082E7 RID: 33511 RVA: 0x001E3B8E File Offset: 0x001E1D8E
			public void SetMessageEncoder(BinaryMessageEncoderFactory.BinaryMessageEncoder messageEncoder)
			{
				this.messageEncoder = messageEncoder;
			}

			// Token: 0x060082E8 RID: 33512 RVA: 0x001E3B98 File Offset: 0x001E1D98
			protected override XmlDictionaryReader TakeXmlReader()
			{
				ArraySegment<byte> buffer = base.Buffer;
				XmlDictionaryReader xmlDictionaryReader = this.readerPool.Take();
				if (xmlDictionaryReader != null)
				{
					((IXmlBinaryReaderInitializer)xmlDictionaryReader).SetInput(buffer.Array, buffer.Offset, buffer.Count, this.factory.binaryVersion.Dictionary, this.factory.bufferedReadReaderQuotas, this.messageEncoder.ReaderSession, this.onClose);
				}
				else
				{
					xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(buffer.Array, buffer.Offset, buffer.Count, this.factory.binaryVersion.Dictionary, this.factory.bufferedReadReaderQuotas, this.messageEncoder.ReaderSession, this.onClose);
					if (TD.ReadPoolMissIsEnabled())
					{
						TD.ReadPoolMiss(xmlDictionaryReader.GetType().Name);
					}
				}
				return xmlDictionaryReader;
			}

			// Token: 0x060082E9 RID: 33513 RVA: 0x001E3C68 File Offset: 0x001E1E68
			protected override void ReturnXmlReader(XmlDictionaryReader reader)
			{
				this.readerPool.Return(reader);
			}

			// Token: 0x060082EA RID: 33514 RVA: 0x001E3C77 File Offset: 0x001E1E77
			protected override void OnClosed()
			{
				this.factory.ReturnBufferedData(this);
			}

			// Token: 0x04004A61 RID: 19041
			private BinaryMessageEncoderFactory factory;

			// Token: 0x04004A62 RID: 19042
			private BinaryMessageEncoderFactory.BinaryMessageEncoder messageEncoder;

			// Token: 0x04004A63 RID: 19043
			private Pool<XmlDictionaryReader> readerPool;

			// Token: 0x04004A64 RID: 19044
			private OnXmlDictionaryReaderClose onClose;
		}

		// Token: 0x02000E4E RID: 3662
		private class BinaryBufferedMessageWriter : BufferedMessageWriter
		{
			// Token: 0x060082EB RID: 33515 RVA: 0x001E3C85 File Offset: 0x001E1E85
			public BinaryBufferedMessageWriter(IXmlDictionary dictionary)
			{
				this.dictionary = dictionary;
			}

			// Token: 0x060082EC RID: 33516 RVA: 0x001E3C94 File Offset: 0x001E1E94
			public BinaryBufferedMessageWriter(IXmlDictionary dictionary, XmlBinaryWriterSession session)
			{
				this.dictionary = dictionary;
				this.session = session;
			}

			// Token: 0x060082ED RID: 33517 RVA: 0x001E3CAC File Offset: 0x001E1EAC
			protected override XmlDictionaryWriter TakeXmlWriter(Stream stream)
			{
				XmlDictionaryWriter xmlDictionaryWriter = this.writer;
				if (xmlDictionaryWriter == null)
				{
					xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream, this.dictionary, this.session, false);
				}
				else
				{
					this.writer = null;
					((IXmlBinaryWriterInitializer)xmlDictionaryWriter).SetOutput(stream, this.dictionary, this.session, false);
				}
				return xmlDictionaryWriter;
			}

			// Token: 0x060082EE RID: 33518 RVA: 0x001E3CFA File Offset: 0x001E1EFA
			protected override void ReturnXmlWriter(XmlDictionaryWriter writer)
			{
				writer.Close();
				if (this.writer == null)
				{
					this.writer = writer;
				}
			}

			// Token: 0x04004A65 RID: 19045
			private XmlDictionaryWriter writer;

			// Token: 0x04004A66 RID: 19046
			private IXmlDictionary dictionary;

			// Token: 0x04004A67 RID: 19047
			private XmlBinaryWriterSession session;
		}

		// Token: 0x02000E4F RID: 3663
		private class BinaryMessageEncoder : MessageEncoder, ICompressedMessageEncoder, ITraceSourceStringProvider
		{
			// Token: 0x060082EF RID: 33519 RVA: 0x001E3D14 File Offset: 0x001E1F14
			public BinaryMessageEncoder(BinaryMessageEncoderFactory factory, bool isSession, int maxSessionSize)
			{
				this.factory = factory;
				this.isSession = isSession;
				this.maxSessionSize = maxSessionSize;
				this.remainingReaderSessionSize = maxSessionSize;
				this.normalContentType = (isSession ? factory.binaryVersion.SessionContentType : factory.binaryVersion.ContentType);
				this.gzipCompressedContentType = (isSession ? BinaryVersion.GZipVersion1.SessionContentType : BinaryVersion.GZipVersion1.ContentType);
				this.deflateCompressedContentType = (isSession ? BinaryVersion.DeflateVersion1.SessionContentType : BinaryVersion.DeflateVersion1.ContentType);
				this.sessionCompressionFormat = this.factory.CompressionFormat;
				this.maxReceivedMessageSize = this.factory.MaxReceivedMessageSize;
				CompressionFormat compressionFormat = this.factory.CompressionFormat;
				if (compressionFormat == CompressionFormat.GZip)
				{
					this.contentType = this.gzipCompressedContentType;
					return;
				}
				if (compressionFormat == CompressionFormat.Deflate)
				{
					this.contentType = this.deflateCompressedContentType;
					return;
				}
				this.contentType = this.normalContentType;
			}

			// Token: 0x17001CF6 RID: 7414
			// (get) Token: 0x060082F0 RID: 33520 RVA: 0x001E3DFE File Offset: 0x001E1FFE
			public override string ContentType
			{
				get
				{
					return this.contentType;
				}
			}

			// Token: 0x17001CF7 RID: 7415
			// (get) Token: 0x060082F1 RID: 33521 RVA: 0x001E3E06 File Offset: 0x001E2006
			public override MessageVersion MessageVersion
			{
				get
				{
					return this.factory.messageVersion;
				}
			}

			// Token: 0x17001CF8 RID: 7416
			// (get) Token: 0x060082F2 RID: 33522 RVA: 0x001E3E13 File Offset: 0x001E2013
			public override string MediaType
			{
				get
				{
					return this.contentType;
				}
			}

			// Token: 0x17001CF9 RID: 7417
			// (get) Token: 0x060082F3 RID: 33523 RVA: 0x001E3E1B File Offset: 0x001E201B
			public XmlBinaryReaderSession ReaderSession
			{
				get
				{
					return this.readerSession;
				}
			}

			// Token: 0x17001CFA RID: 7418
			// (get) Token: 0x060082F4 RID: 33524 RVA: 0x001E3E23 File Offset: 0x001E2023
			public bool CompressionEnabled
			{
				get
				{
					return this.factory.CompressionFormat > CompressionFormat.None;
				}
			}

			// Token: 0x060082F5 RID: 33525 RVA: 0x001E3E34 File Offset: 0x001E2034
			private ArraySegment<byte> AddSessionInformationToMessage(ArraySegment<byte> messageData, BufferManager bufferManager, int maxMessageSize)
			{
				int num = 0;
				byte[] array = messageData.Array;
				if (this.writerSession.HasNewStrings)
				{
					IList<XmlDictionaryString> newStrings = this.writerSession.GetNewStrings();
					for (int i = 0; i < newStrings.Count; i++)
					{
						int byteCount = Encoding.UTF8.GetByteCount(newStrings[i].Value);
						num += IntEncoder.GetEncodedSize(byteCount) + byteCount;
					}
					int num2 = messageData.Offset + messageData.Count;
					int num3 = maxMessageSize - num2;
					if (num3 - num < 0)
					{
						string @string = SR.GetString("MaxSentMessageSizeExceeded", new object[]
						{
							maxMessageSize
						});
						if (TD.MaxSentMessageSizeExceededIsEnabled())
						{
							TD.MaxSentMessageSizeExceeded(@string);
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QuotaExceededException(@string));
					}
					int num4 = messageData.Offset + messageData.Count + num;
					if (array.Length < num4)
					{
						byte[] array2 = bufferManager.TakeBuffer(num4);
						Buffer.BlockCopy(array, messageData.Offset, array2, messageData.Offset, messageData.Count);
						bufferManager.ReturnBuffer(array);
						array = array2;
					}
					Buffer.BlockCopy(array, messageData.Offset, array, messageData.Offset + num, messageData.Count);
					int num5 = messageData.Offset;
					for (int j = 0; j < newStrings.Count; j++)
					{
						string value = newStrings[j].Value;
						int byteCount2 = Encoding.UTF8.GetByteCount(value);
						num5 += IntEncoder.Encode(byteCount2, array, num5);
						num5 += Encoding.UTF8.GetBytes(value, 0, value.Length, array, num5);
					}
					this.writerSession.ClearNewStrings();
				}
				int encodedSize = IntEncoder.GetEncodedSize(num);
				int offset = messageData.Offset - encodedSize;
				int count = encodedSize + messageData.Count + num;
				IntEncoder.Encode(num, array, offset);
				return new ArraySegment<byte>(array, offset, count);
			}

			// Token: 0x060082F6 RID: 33526 RVA: 0x001E400C File Offset: 0x001E220C
			private ArraySegment<byte> ExtractSessionInformationFromMessage(ArraySegment<byte> messageData)
			{
				if (this.isReaderSessionInvalid)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("BinaryEncoderSessionInvalid")));
				}
				byte[] array = messageData.Array;
				bool flag = true;
				int offset;
				int num2;
				try
				{
					IntDecoder intDecoder = default(IntDecoder);
					int num = intDecoder.Decode(array, messageData.Offset, messageData.Count);
					int value = intDecoder.Value;
					if (value > messageData.Count)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("BinaryEncoderSessionMalformed")));
					}
					offset = messageData.Offset + num + value;
					num2 = messageData.Count - num - value;
					if (num2 < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("BinaryEncoderSessionMalformed")));
					}
					if (value > 0)
					{
						if (value > this.remainingReaderSessionSize)
						{
							string @string = SR.GetString("BinaryEncoderSessionTooLarge", new object[]
							{
								this.maxSessionSize
							});
							if (TD.MaxSessionSizeReachedIsEnabled())
							{
								TD.MaxSessionSizeReached(@string);
							}
							Exception innerException = new QuotaExceededException(@string);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(@string, innerException));
						}
						this.remainingReaderSessionSize -= value;
						int i = value;
						int num3 = messageData.Offset + num;
						while (i > 0)
						{
							intDecoder.Reset();
							int num4 = intDecoder.Decode(array, num3, i);
							int value2 = intDecoder.Value;
							num3 += num4;
							i -= num4;
							if (value2 > i)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("BinaryEncoderSessionMalformed")));
							}
							string string2 = Encoding.UTF8.GetString(array, num3, value2);
							num3 += value2;
							i -= value2;
							this.readerSession.Add(this.idCounter, string2);
							this.idCounter++;
						}
					}
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.isReaderSessionInvalid = true;
					}
				}
				return new ArraySegment<byte>(array, offset, num2);
			}

			// Token: 0x060082F7 RID: 33527 RVA: 0x001E4210 File Offset: 0x001E2410
			public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
			{
				if (bufferManager == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bufferManager");
				}
				CompressionFormat compressionFormat = this.CheckContentType(contentType);
				if (TD.BinaryMessageDecodingStartIsEnabled())
				{
					TD.BinaryMessageDecodingStart();
				}
				if (compressionFormat != CompressionFormat.None)
				{
					MessageEncoderCompressionHandler.DecompressBuffer(ref buffer, bufferManager, compressionFormat, this.maxReceivedMessageSize);
				}
				if (this.isSession)
				{
					if (this.readerSession == null)
					{
						this.readerSession = new XmlBinaryReaderSession();
						this.messagePatterns = new MessagePatterns(this.factory.binaryVersion.Dictionary, this.readerSession, this.MessageVersion);
					}
					try
					{
						buffer = this.ExtractSessionInformationFromMessage(buffer);
					}
					catch (InvalidDataException)
					{
						MessageLogger.LogMessage(buffer, MessageLoggingSource.Malformed);
						throw;
					}
				}
				BinaryMessageEncoderFactory.BinaryBufferedMessageData binaryBufferedMessageData = this.factory.TakeBufferedData(this);
				Message message;
				if (this.messagePatterns != null)
				{
					message = this.messagePatterns.TryCreateMessage(buffer.Array, buffer.Offset, buffer.Count, bufferManager, binaryBufferedMessageData);
				}
				else
				{
					message = null;
				}
				if (message == null)
				{
					binaryBufferedMessageData.Open(buffer, bufferManager);
					RecycledMessageState recycledMessageState = binaryBufferedMessageData.TakeMessageState();
					if (recycledMessageState == null)
					{
						recycledMessageState = new RecycledMessageState();
					}
					message = new BufferedMessage(binaryBufferedMessageData, recycledMessageState);
				}
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

			// Token: 0x060082F8 RID: 33528 RVA: 0x001E4358 File Offset: 0x001E2558
			public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
			{
				if (stream == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
				}
				CompressionFormat compressionFormat = this.CheckContentType(contentType);
				if (TD.BinaryMessageDecodingStartIsEnabled())
				{
					TD.BinaryMessageDecodingStart();
				}
				if (compressionFormat != CompressionFormat.None)
				{
					stream = new MaxMessageSizeStream(MessageEncoderCompressionHandler.GetDecompressStream(stream, compressionFormat), this.maxReceivedMessageSize);
				}
				XmlDictionaryReader envelopeReader = this.factory.TakeStreamedReader(stream);
				Message message = Message.CreateMessage(envelopeReader, maxSizeOfHeaders, this.factory.messageVersion);
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

			// Token: 0x060082F9 RID: 33529 RVA: 0x001E43F4 File Offset: 0x001E25F4
			public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
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
				EventTraceActivity eventTraceActivity = null;
				if (TD.BinaryMessageEncodingStartIsEnabled())
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					TD.BinaryMessageEncodingStart(eventTraceActivity);
				}
				message.Properties.Encoder = this;
				if (this.isSession)
				{
					if (this.writerSession == null)
					{
						this.writerSession = new BinaryMessageEncoderFactory.XmlBinaryWriterSessionWithQuota(this.maxSessionSize);
						this.sessionMessageWriter = new BinaryMessageEncoderFactory.BinaryBufferedMessageWriter(this.factory.binaryVersion.Dictionary, this.writerSession);
					}
					messageOffset += 5;
				}
				if (messageOffset < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageOffset", messageOffset, SR.GetString("ValueMustBeNonNegative")));
				}
				if (messageOffset > maxMessageSize)
				{
					string @string = SR.GetString("MaxSentMessageSizeExceeded", new object[]
					{
						maxMessageSize
					});
					if (TD.MaxSentMessageSizeExceededIsEnabled())
					{
						TD.MaxSentMessageSizeExceeded(@string);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QuotaExceededException(@string));
				}
				base.ThrowIfMismatchedMessageVersion(message);
				BinaryMessageEncoderFactory.BinaryBufferedMessageWriter binaryBufferedMessageWriter;
				if (this.isSession)
				{
					binaryBufferedMessageWriter = this.sessionMessageWriter;
				}
				else
				{
					binaryBufferedMessageWriter = this.factory.TakeBufferedWriter();
				}
				ArraySegment<byte> arraySegment = binaryBufferedMessageWriter.WriteMessage(message, bufferManager, messageOffset, maxMessageSize);
				if (MessageLogger.LogMessagesAtTransportLevel && !this.readerSessionForLoggingIsInvalid)
				{
					if (this.isSession)
					{
						if (this.readerSessionForLogging == null)
						{
							this.readerSessionForLogging = new XmlBinaryReaderSession();
						}
						if (this.writerSession.HasNewStrings)
						{
							foreach (XmlDictionaryString xmlDictionaryString in this.writerSession.GetNewStrings())
							{
								XmlBinaryReaderSession xmlBinaryReaderSession = this.readerSessionForLogging;
								int num = this.writeIdCounter;
								this.writeIdCounter = num + 1;
								xmlBinaryReaderSession.Add(num, xmlDictionaryString.Value);
							}
						}
					}
					XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(arraySegment.Array, arraySegment.Offset, arraySegment.Count, XD.Dictionary, XmlDictionaryReaderQuotas.Max, this.readerSessionForLogging, null);
					MessageLogger.LogMessage(ref message, reader, MessageLoggingSource.TransportSend);
				}
				else
				{
					this.readerSessionForLoggingIsInvalid = true;
				}
				if (this.isSession)
				{
					arraySegment = this.AddSessionInformationToMessage(arraySegment, bufferManager, maxMessageSize);
				}
				else
				{
					this.factory.ReturnMessageWriter(binaryBufferedMessageWriter);
				}
				if (TD.MessageWrittenByEncoderIsEnabled())
				{
					TD.MessageWrittenByEncoder(eventTraceActivity ?? EventTraceActivityHelper.TryExtractActivity(message), arraySegment.Count, this);
				}
				CompressionFormat compressionFormat = this.CheckCompressedWrite(message);
				if (compressionFormat != CompressionFormat.None)
				{
					MessageEncoderCompressionHandler.CompressBuffer(ref arraySegment, bufferManager, compressionFormat);
				}
				return arraySegment;
			}

			// Token: 0x060082FA RID: 33530 RVA: 0x001E4694 File Offset: 0x001E2894
			public override void WriteMessage(Message message, Stream stream)
			{
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
				}
				if (stream == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("stream"));
				}
				EventTraceActivity eventTraceActivity = null;
				if (TD.BinaryMessageEncodingStartIsEnabled())
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					TD.BinaryMessageEncodingStart(eventTraceActivity);
				}
				CompressionFormat compressionFormat = this.CheckCompressedWrite(message);
				if (compressionFormat != CompressionFormat.None)
				{
					stream = MessageEncoderCompressionHandler.GetCompressStream(stream, compressionFormat);
				}
				base.ThrowIfMismatchedMessageVersion(message);
				message.Properties.Encoder = this;
				XmlDictionaryWriter xmlDictionaryWriter = this.factory.TakeStreamedWriter(stream);
				message.WriteMessage(xmlDictionaryWriter);
				xmlDictionaryWriter.Flush();
				if (TD.StreamedMessageWrittenByEncoderIsEnabled())
				{
					TD.StreamedMessageWrittenByEncoder(eventTraceActivity ?? EventTraceActivityHelper.TryExtractActivity(message));
				}
				this.factory.ReturnStreamedWriter(xmlDictionaryWriter);
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportSend);
				}
				if (compressionFormat != CompressionFormat.None)
				{
					stream.Close();
				}
			}

			// Token: 0x060082FB RID: 33531 RVA: 0x001E4764 File Offset: 0x001E2964
			public override bool IsContentTypeSupported(string contentType)
			{
				bool result = true;
				if (!base.IsContentTypeSupported(contentType))
				{
					result = (this.CompressionEnabled && ((this.factory.CompressionFormat == CompressionFormat.GZip && base.IsContentTypeSupported(contentType, this.gzipCompressedContentType, this.gzipCompressedContentType)) || (this.factory.CompressionFormat == CompressionFormat.Deflate && base.IsContentTypeSupported(contentType, this.deflateCompressedContentType, this.deflateCompressedContentType)) || base.IsContentTypeSupported(contentType, this.normalContentType, this.normalContentType)));
				}
				return result;
			}

			// Token: 0x060082FC RID: 33532 RVA: 0x001E47E8 File Offset: 0x001E29E8
			public void SetSessionContentType(string contentType)
			{
				if (base.IsContentTypeSupported(contentType, this.gzipCompressedContentType, this.gzipCompressedContentType))
				{
					this.sessionCompressionFormat = CompressionFormat.GZip;
					return;
				}
				if (base.IsContentTypeSupported(contentType, this.deflateCompressedContentType, this.deflateCompressedContentType))
				{
					this.sessionCompressionFormat = CompressionFormat.Deflate;
					return;
				}
				this.sessionCompressionFormat = CompressionFormat.None;
			}

			// Token: 0x060082FD RID: 33533 RVA: 0x001E4836 File Offset: 0x001E2A36
			public void AddCompressedMessageProperties(Message message, string supportedCompressionTypes)
			{
				message.Properties.Add("BinaryMessageEncoder.SupportedCompressionTypes", supportedCompressionTypes);
			}

			// Token: 0x060082FE RID: 33534 RVA: 0x001E4849 File Offset: 0x001E2A49
			private static bool ContentTypeEqualsOrStartsWith(string contentType, string supportedContentType)
			{
				return contentType == supportedContentType || contentType.StartsWith(supportedContentType, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060082FF RID: 33535 RVA: 0x001E4860 File Offset: 0x001E2A60
			private CompressionFormat CheckContentType(string contentType)
			{
				CompressionFormat result = CompressionFormat.None;
				if (contentType == null)
				{
					result = this.sessionCompressionFormat;
				}
				else if (!this.CompressionEnabled)
				{
					if (!BinaryMessageEncoderFactory.BinaryMessageEncoder.ContentTypeEqualsOrStartsWith(contentType, this.ContentType))
					{
						throw FxTrace.Exception.AsError(new ProtocolException(SR.GetString("EncoderUnrecognizedContentType", new object[]
						{
							contentType,
							this.ContentType
						})));
					}
				}
				else if (this.factory.CompressionFormat == CompressionFormat.GZip && BinaryMessageEncoderFactory.BinaryMessageEncoder.ContentTypeEqualsOrStartsWith(contentType, this.gzipCompressedContentType))
				{
					result = CompressionFormat.GZip;
				}
				else if (this.factory.CompressionFormat == CompressionFormat.Deflate && BinaryMessageEncoderFactory.BinaryMessageEncoder.ContentTypeEqualsOrStartsWith(contentType, this.deflateCompressedContentType))
				{
					result = CompressionFormat.Deflate;
				}
				else
				{
					if (!BinaryMessageEncoderFactory.BinaryMessageEncoder.ContentTypeEqualsOrStartsWith(contentType, this.normalContentType))
					{
						throw FxTrace.Exception.AsError(new ProtocolException(SR.GetString("EncoderUnrecognizedContentType", new object[]
						{
							contentType,
							this.ContentType
						})));
					}
					result = CompressionFormat.None;
				}
				return result;
			}

			// Token: 0x06008300 RID: 33536 RVA: 0x001E4944 File Offset: 0x001E2B44
			private CompressionFormat CheckCompressedWrite(Message message)
			{
				CompressionFormat compressionFormat = this.sessionCompressionFormat;
				string text;
				if (compressionFormat != CompressionFormat.None && !this.isSession && message.Properties.TryGetValue<string>("BinaryMessageEncoder.SupportedCompressionTypes", out text) && text != null)
				{
					text = text.ToLowerInvariant();
					if ((compressionFormat == CompressionFormat.GZip && !text.Contains("gzip")) || (compressionFormat == CompressionFormat.Deflate && !text.Contains("deflate")))
					{
						compressionFormat = CompressionFormat.None;
					}
				}
				return compressionFormat;
			}

			// Token: 0x06008301 RID: 33537 RVA: 0x001E49A6 File Offset: 0x001E2BA6
			string ITraceSourceStringProvider.GetSourceString()
			{
				return base.GetTraceSourceString();
			}

			// Token: 0x04004A68 RID: 19048
			private const string SupportedCompressionTypesMessageProperty = "BinaryMessageEncoder.SupportedCompressionTypes";

			// Token: 0x04004A69 RID: 19049
			private BinaryMessageEncoderFactory factory;

			// Token: 0x04004A6A RID: 19050
			private bool isSession;

			// Token: 0x04004A6B RID: 19051
			private BinaryMessageEncoderFactory.XmlBinaryWriterSessionWithQuota writerSession;

			// Token: 0x04004A6C RID: 19052
			private BinaryMessageEncoderFactory.BinaryBufferedMessageWriter sessionMessageWriter;

			// Token: 0x04004A6D RID: 19053
			private XmlBinaryReaderSession readerSession;

			// Token: 0x04004A6E RID: 19054
			private XmlBinaryReaderSession readerSessionForLogging;

			// Token: 0x04004A6F RID: 19055
			private bool readerSessionForLoggingIsInvalid;

			// Token: 0x04004A70 RID: 19056
			private int writeIdCounter;

			// Token: 0x04004A71 RID: 19057
			private int idCounter;

			// Token: 0x04004A72 RID: 19058
			private int maxSessionSize;

			// Token: 0x04004A73 RID: 19059
			private int remainingReaderSessionSize;

			// Token: 0x04004A74 RID: 19060
			private bool isReaderSessionInvalid;

			// Token: 0x04004A75 RID: 19061
			private MessagePatterns messagePatterns;

			// Token: 0x04004A76 RID: 19062
			private string contentType;

			// Token: 0x04004A77 RID: 19063
			private string normalContentType;

			// Token: 0x04004A78 RID: 19064
			private string gzipCompressedContentType;

			// Token: 0x04004A79 RID: 19065
			private string deflateCompressedContentType;

			// Token: 0x04004A7A RID: 19066
			private CompressionFormat sessionCompressionFormat;

			// Token: 0x04004A7B RID: 19067
			private readonly long maxReceivedMessageSize;
		}

		// Token: 0x02000E50 RID: 3664
		private class XmlBinaryWriterSessionWithQuota : XmlBinaryWriterSession
		{
			// Token: 0x06008302 RID: 33538 RVA: 0x001E49AE File Offset: 0x001E2BAE
			public XmlBinaryWriterSessionWithQuota(int maxSessionSize)
			{
				this.bytesRemaining = maxSessionSize;
			}

			// Token: 0x17001CFB RID: 7419
			// (get) Token: 0x06008303 RID: 33539 RVA: 0x001E49BD File Offset: 0x001E2BBD
			public bool HasNewStrings
			{
				get
				{
					return this.newStrings != null;
				}
			}

			// Token: 0x06008304 RID: 33540 RVA: 0x001E49C8 File Offset: 0x001E2BC8
			public override bool TryAdd(XmlDictionaryString s, out int key)
			{
				if (this.bytesRemaining == 0)
				{
					key = -1;
					return false;
				}
				int num = Encoding.UTF8.GetByteCount(s.Value);
				num += IntEncoder.GetEncodedSize(num);
				if (num > this.bytesRemaining)
				{
					key = -1;
					this.bytesRemaining = 0;
					return false;
				}
				if (base.TryAdd(s, out key))
				{
					if (this.newStrings == null)
					{
						this.newStrings = new List<XmlDictionaryString>();
					}
					this.newStrings.Add(s);
					this.bytesRemaining -= num;
					return true;
				}
				return false;
			}

			// Token: 0x06008305 RID: 33541 RVA: 0x001E4A4B File Offset: 0x001E2C4B
			public IList<XmlDictionaryString> GetNewStrings()
			{
				return this.newStrings;
			}

			// Token: 0x06008306 RID: 33542 RVA: 0x001E4A53 File Offset: 0x001E2C53
			public void ClearNewStrings()
			{
				this.newStrings = null;
			}

			// Token: 0x04004A7C RID: 19068
			private int bytesRemaining;

			// Token: 0x04004A7D RID: 19069
			private List<XmlDictionaryString> newStrings;
		}
	}
}
