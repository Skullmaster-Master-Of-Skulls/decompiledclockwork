using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D2 RID: 2514
	[__DynamicallyInvokable]
	public sealed class MessageHeaders : IEnumerable<MessageHeaderInfo>, IEnumerable
	{
		// Token: 0x060062CF RID: 25295 RVA: 0x0016FD94 File Offset: 0x0016DF94
		[__DynamicallyInvokable]
		public MessageHeaders(MessageVersion version, int initialSize)
		{
			this.Init(version, initialSize);
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x0016FDA4 File Offset: 0x0016DFA4
		[__DynamicallyInvokable]
		public MessageHeaders(MessageVersion version) : this(version, 4)
		{
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x0016FDB0 File Offset: 0x0016DFB0
		internal MessageHeaders(MessageVersion version, XmlDictionaryReader reader, XmlAttributeHolder[] envelopeAttributes, XmlAttributeHolder[] headerAttributes, ref int maxSizeOfHeaders) : this(version)
		{
			if (maxSizeOfHeaders < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxSizeOfHeaders", maxSizeOfHeaders, SR.GetString("ValueMustBeNonNegative")));
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reader"));
			}
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			XmlBuffer xmlBuffer = null;
			EnvelopeVersion envelope = version.Envelope;
			reader.ReadStartElement(XD.MessageDictionary.Header, envelope.DictionaryNamespace);
			while (reader.IsStartElement())
			{
				if (xmlBuffer == null)
				{
					xmlBuffer = new XmlBuffer(maxSizeOfHeaders);
				}
				BufferedHeader bufferedHeader = new BufferedHeader(version, xmlBuffer, reader, envelopeAttributes, headerAttributes);
				MessageHeaders.HeaderProcessing headerProcessing = bufferedHeader.MustUnderstand ? MessageHeaders.HeaderProcessing.MustUnderstand : ((MessageHeaders.HeaderProcessing)0);
				MessageHeaders.HeaderKind headerKind = this.GetHeaderKind(bufferedHeader);
				if (headerKind != MessageHeaders.HeaderKind.Unknown)
				{
					headerProcessing |= MessageHeaders.HeaderProcessing.Understood;
					MessageHeaders.TraceUnderstood(bufferedHeader);
					if (!LocalAppContextSwitches.AllowMultipleStandardSoapHeaders)
					{
						int num = this.FindHeaderProperty(headerKind);
						if (num >= 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateDuplicateHeaderException(headerKind));
						}
					}
				}
				MessageHeaders.Header header = new MessageHeaders.Header(headerKind, bufferedHeader, headerProcessing);
				this.AddHeader(header);
			}
			if (xmlBuffer != null)
			{
				xmlBuffer.Close();
				maxSizeOfHeaders -= xmlBuffer.BufferSize;
			}
			reader.ReadEndElement();
			this.collectionVersion = 0;
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x0016FEF9 File Offset: 0x0016E0F9
		internal MessageHeaders(MessageVersion version, XmlDictionaryReader reader, IBufferedMessageData bufferedMessageData, RecycledMessageState recycledMessageState, bool[] understoodHeaders, bool understoodHeadersModified)
		{
			this.headers = new MessageHeaders.Header[4];
			this.Init(version, reader, bufferedMessageData, recycledMessageState, understoodHeaders, understoodHeadersModified);
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x0016FF1C File Offset: 0x0016E11C
		internal MessageHeaders(MessageVersion version, MessageHeaders headers, IBufferedMessageData bufferedMessageData)
		{
			this.version = version;
			this.bufferedMessageData = bufferedMessageData;
			this.headerCount = headers.headerCount;
			this.headers = new MessageHeaders.Header[this.headerCount];
			Array.Copy(headers.headers, this.headers, this.headerCount);
			this.collectionVersion = 0;
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x0016FF78 File Offset: 0x0016E178
		[__DynamicallyInvokable]
		public MessageHeaders(MessageHeaders collection)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			this.Init(collection.version, collection.headers.Length);
			this.CopyHeadersFrom(collection);
			this.collectionVersion = 0;
		}

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x0016FFB8 File Offset: 0x0016E1B8
		// (set) Token: 0x060062D6 RID: 25302 RVA: 0x0017002C File Offset: 0x0016E22C
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindHeaderProperty(MessageHeaders.HeaderKind.Action);
				if (num < 0)
				{
					return null;
				}
				ActionHeader actionHeader = this.headers[num].HeaderInfo as ActionHeader;
				if (actionHeader != null)
				{
					return actionHeader.Action;
				}
				string result;
				using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(num))
				{
					result = ActionHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					this.SetActionHeader(ActionHeader.Create(value, this.version.Addressing));
					return;
				}
				this.SetHeaderProperty(MessageHeaders.HeaderKind.Action, null);
			}
		}

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x00170051 File Offset: 0x0016E251
		internal bool CanRecycle
		{
			get
			{
				return this.headers.Length <= 8;
			}
		}

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x060062D8 RID: 25304 RVA: 0x00170061 File Offset: 0x0016E261
		internal bool ContainsOnlyBufferedMessageHeaders
		{
			get
			{
				return this.bufferedMessageData != null && this.collectionVersion == 0;
			}
		}

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x060062D9 RID: 25305 RVA: 0x00170076 File Offset: 0x0016E276
		internal int CollectionVersion
		{
			get
			{
				return this.collectionVersion;
			}
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x060062DA RID: 25306 RVA: 0x0017007E File Offset: 0x0016E27E
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.headerCount;
			}
		}

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x060062DB RID: 25307 RVA: 0x00170088 File Offset: 0x0016E288
		// (set) Token: 0x060062DC RID: 25308 RVA: 0x001700FC File Offset: 0x0016E2FC
		[__DynamicallyInvokable]
		public EndpointAddress FaultTo
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindHeaderProperty(MessageHeaders.HeaderKind.FaultTo);
				if (num < 0)
				{
					return null;
				}
				FaultToHeader faultToHeader = this.headers[num].HeaderInfo as FaultToHeader;
				if (faultToHeader != null)
				{
					return faultToHeader.FaultTo;
				}
				EndpointAddress result;
				using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(num))
				{
					result = FaultToHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					this.SetFaultToHeader(FaultToHeader.Create(value, this.version.Addressing));
					return;
				}
				this.SetHeaderProperty(MessageHeaders.HeaderKind.FaultTo, null);
			}
		}

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x060062DD RID: 25309 RVA: 0x00170128 File Offset: 0x0016E328
		// (set) Token: 0x060062DE RID: 25310 RVA: 0x0017019C File Offset: 0x0016E39C
		[__DynamicallyInvokable]
		public EndpointAddress From
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindHeaderProperty(MessageHeaders.HeaderKind.From);
				if (num < 0)
				{
					return null;
				}
				FromHeader fromHeader = this.headers[num].HeaderInfo as FromHeader;
				if (fromHeader != null)
				{
					return fromHeader.From;
				}
				EndpointAddress result;
				using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(num))
				{
					result = FromHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					this.SetFromHeader(FromHeader.Create(value, this.version.Addressing));
					return;
				}
				this.SetHeaderProperty(MessageHeaders.HeaderKind.From, null);
			}
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060062DF RID: 25311 RVA: 0x001701C7 File Offset: 0x0016E3C7
		internal bool HasMustUnderstandBeenModified
		{
			get
			{
				if (this.understoodHeaders != null)
				{
					return this.understoodHeaders.Modified;
				}
				return this.understoodHeadersModified;
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060062E0 RID: 25312 RVA: 0x001701E4 File Offset: 0x0016E3E4
		// (set) Token: 0x060062E1 RID: 25313 RVA: 0x00170258 File Offset: 0x0016E458
		[__DynamicallyInvokable]
		public UniqueId MessageId
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindHeaderProperty(MessageHeaders.HeaderKind.MessageId);
				if (num < 0)
				{
					return null;
				}
				MessageIDHeader messageIDHeader = this.headers[num].HeaderInfo as MessageIDHeader;
				if (messageIDHeader != null)
				{
					return messageIDHeader.MessageId;
				}
				UniqueId result;
				using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(num))
				{
					result = MessageIDHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					this.SetMessageIDHeader(MessageIDHeader.Create(value, this.version.Addressing));
					return;
				}
				this.SetHeaderProperty(MessageHeaders.HeaderKind.MessageId, null);
			}
		}

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060062E2 RID: 25314 RVA: 0x00170283 File Offset: 0x0016E483
		[__DynamicallyInvokable]
		public MessageVersion MessageVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.version;
			}
		}

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060062E3 RID: 25315 RVA: 0x0017028B File Offset: 0x0016E48B
		// (set) Token: 0x060062E4 RID: 25316 RVA: 0x00170298 File Offset: 0x0016E498
		[__DynamicallyInvokable]
		public UniqueId RelatesTo
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetRelatesTo(RelatesToHeader.ReplyRelationshipType);
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetRelatesTo(RelatesToHeader.ReplyRelationshipType, value);
			}
		}

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060062E5 RID: 25317 RVA: 0x001702A8 File Offset: 0x0016E4A8
		// (set) Token: 0x060062E6 RID: 25318 RVA: 0x0017031C File Offset: 0x0016E51C
		[__DynamicallyInvokable]
		public EndpointAddress ReplyTo
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindHeaderProperty(MessageHeaders.HeaderKind.ReplyTo);
				if (num < 0)
				{
					return null;
				}
				ReplyToHeader replyToHeader = this.headers[num].HeaderInfo as ReplyToHeader;
				if (replyToHeader != null)
				{
					return replyToHeader.ReplyTo;
				}
				EndpointAddress result;
				using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(num))
				{
					result = ReplyToHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					this.SetReplyToHeader(ReplyToHeader.Create(value, this.version.Addressing));
					return;
				}
				this.SetHeaderProperty(MessageHeaders.HeaderKind.ReplyTo, null);
			}
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060062E7 RID: 25319 RVA: 0x00170348 File Offset: 0x0016E548
		// (set) Token: 0x060062E8 RID: 25320 RVA: 0x001703BC File Offset: 0x0016E5BC
		[__DynamicallyInvokable]
		public Uri To
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindHeaderProperty(MessageHeaders.HeaderKind.To);
				if (num < 0)
				{
					return null;
				}
				ToHeader toHeader = this.headers[num].HeaderInfo as ToHeader;
				if (toHeader != null)
				{
					return toHeader.To;
				}
				Uri result;
				using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(num))
				{
					result = ToHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value != null)
				{
					this.SetToHeader(ToHeader.Create(value, this.version.Addressing));
					return;
				}
				this.SetHeaderProperty(MessageHeaders.HeaderKind.To, null);
			}
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060062E9 RID: 25321 RVA: 0x001703E7 File Offset: 0x0016E5E7
		public UnderstoodHeaders UnderstoodHeaders
		{
			get
			{
				if (this.understoodHeaders == null)
				{
					this.understoodHeaders = new UnderstoodHeaders(this, this.understoodHeadersModified);
				}
				return this.understoodHeaders;
			}
		}

		// Token: 0x170017ED RID: 6125
		[__DynamicallyInvokable]
		public MessageHeaderInfo this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				if (index < 0 || index >= this.headerCount)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						this.headerCount
					})));
				}
				return this.headers[index].HeaderInfo;
			}
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x00170479 File Offset: 0x0016E679
		[__DynamicallyInvokable]
		public void Add(MessageHeader header)
		{
			this.Insert(this.headerCount, header);
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x00170488 File Offset: 0x0016E688
		internal void AddActionHeader(ActionHeader actionHeader)
		{
			this.Insert(this.headerCount, actionHeader, MessageHeaders.HeaderKind.Action);
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x00170498 File Offset: 0x0016E698
		internal void AddMessageIDHeader(MessageIDHeader messageIDHeader)
		{
			this.Insert(this.headerCount, messageIDHeader, MessageHeaders.HeaderKind.MessageId);
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x001704A8 File Offset: 0x0016E6A8
		internal void AddRelatesToHeader(RelatesToHeader relatesToHeader)
		{
			this.Insert(this.headerCount, relatesToHeader, MessageHeaders.HeaderKind.RelatesTo);
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x001704B8 File Offset: 0x0016E6B8
		internal void AddReplyToHeader(ReplyToHeader replyToHeader)
		{
			this.Insert(this.headerCount, replyToHeader, MessageHeaders.HeaderKind.ReplyTo);
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x001704C8 File Offset: 0x0016E6C8
		internal void AddToHeader(ToHeader toHeader)
		{
			this.Insert(this.headerCount, toHeader, MessageHeaders.HeaderKind.To);
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x001704D8 File Offset: 0x0016E6D8
		private void Add(MessageHeader header, MessageHeaders.HeaderKind kind)
		{
			this.Insert(this.headerCount, header, kind);
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x001704E8 File Offset: 0x0016E6E8
		private void AddHeader(MessageHeaders.Header header)
		{
			this.InsertHeader(this.headerCount, header);
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x001704F7 File Offset: 0x0016E6F7
		internal void AddUnderstood(int i)
		{
			MessageHeaders.Header[] array = this.headers;
			array[i].HeaderProcessing = (array[i].HeaderProcessing | MessageHeaders.HeaderProcessing.Understood);
			MessageHeaders.TraceUnderstood(this.headers[i].HeaderInfo);
		}

		// Token: 0x060062F4 RID: 25332 RVA: 0x00170528 File Offset: 0x0016E728
		internal void AddUnderstood(MessageHeaderInfo headerInfo)
		{
			if (headerInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("headerInfo"));
			}
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderInfo == headerInfo)
				{
					if ((this.headers[i].HeaderProcessing & MessageHeaders.HeaderProcessing.Understood) != (MessageHeaders.HeaderProcessing)0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("HeaderAlreadyUnderstood", new object[]
						{
							headerInfo.Name,
							headerInfo.Namespace
						}), "headerInfo"));
					}
					this.AddUnderstood(i);
				}
			}
		}

		// Token: 0x060062F5 RID: 25333 RVA: 0x001705C5 File Offset: 0x0016E7C5
		private void CaptureBufferedHeaders()
		{
			this.CaptureBufferedHeaders(-1);
		}

		// Token: 0x060062F6 RID: 25334 RVA: 0x001705D0 File Offset: 0x0016E7D0
		private void CaptureBufferedHeaders(int exceptIndex)
		{
			using (XmlDictionaryReader bufferedMessageHeaderReaderAtHeaderContents = MessageHeaders.GetBufferedMessageHeaderReaderAtHeaderContents(this.bufferedMessageData))
			{
				for (int i = 0; i < this.headerCount; i++)
				{
					if (bufferedMessageHeaderReaderAtHeaderContents.NodeType != XmlNodeType.Element && bufferedMessageHeaderReaderAtHeaderContents.MoveToContent() != XmlNodeType.Element)
					{
						break;
					}
					MessageHeaders.Header header = this.headers[i];
					if (i == exceptIndex || header.HeaderType != MessageHeaders.HeaderType.BufferedMessageHeader)
					{
						bufferedMessageHeaderReaderAtHeaderContents.Skip();
					}
					else
					{
						this.headers[i] = new MessageHeaders.Header(header.HeaderKind, this.CaptureBufferedHeader(bufferedMessageHeaderReaderAtHeaderContents, header.HeaderInfo), header.HeaderProcessing);
					}
				}
			}
			this.bufferedMessageData = null;
		}

		// Token: 0x060062F7 RID: 25335 RVA: 0x00170684 File Offset: 0x0016E884
		private BufferedHeader CaptureBufferedHeader(XmlDictionaryReader reader, MessageHeaderInfo headerInfo)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(this.bufferedMessageData.Quotas);
			xmlDictionaryWriter.WriteNode(reader, false);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return new BufferedHeader(this.version, xmlBuffer, 0, headerInfo);
		}

		// Token: 0x060062F8 RID: 25336 RVA: 0x001706D0 File Offset: 0x0016E8D0
		private BufferedHeader CaptureBufferedHeader(IBufferedMessageData bufferedMessageData, MessageHeaderInfo headerInfo, int bufferedMessageHeaderIndex)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter writer = xmlBuffer.OpenSection(bufferedMessageData.Quotas);
			this.WriteBufferedMessageHeader(bufferedMessageData, bufferedMessageHeaderIndex, writer);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return new BufferedHeader(this.version, xmlBuffer, 0, headerInfo);
		}

		// Token: 0x060062F9 RID: 25337 RVA: 0x00170718 File Offset: 0x0016E918
		private BufferedHeader CaptureWriteableHeader(MessageHeader writeableHeader)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter writer = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			writeableHeader.WriteHeader(writer, this.version);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return new BufferedHeader(this.version, xmlBuffer, 0, writeableHeader);
		}

		// Token: 0x060062FA RID: 25338 RVA: 0x00170764 File Offset: 0x0016E964
		[__DynamicallyInvokable]
		public void Clear()
		{
			for (int i = 0; i < this.headerCount; i++)
			{
				this.headers[i] = default(MessageHeaders.Header);
			}
			this.headerCount = 0;
			this.collectionVersion++;
			this.bufferedMessageData = null;
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x001707B0 File Offset: 0x0016E9B0
		[__DynamicallyInvokable]
		public void CopyHeaderFrom(Message message, int headerIndex)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			this.CopyHeaderFrom(message.Headers, headerIndex);
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x001707D8 File Offset: 0x0016E9D8
		[__DynamicallyInvokable]
		public void CopyHeaderFrom(MessageHeaders collection, int headerIndex)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			if (collection.version != this.version)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageHeaderVersionMismatch", new object[]
				{
					collection.version.ToString(),
					this.version.ToString()
				}), "collection"));
			}
			if (headerIndex < 0 || headerIndex >= collection.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					collection.headerCount
				})));
			}
			MessageHeaders.Header header = collection.headers[headerIndex];
			MessageHeaders.HeaderProcessing headerProcessing = header.HeaderInfo.MustUnderstand ? MessageHeaders.HeaderProcessing.MustUnderstand : ((MessageHeaders.HeaderProcessing)0);
			if ((header.HeaderProcessing & MessageHeaders.HeaderProcessing.Understood) != (MessageHeaders.HeaderProcessing)0 || header.HeaderKind != MessageHeaders.HeaderKind.Unknown)
			{
				headerProcessing |= MessageHeaders.HeaderProcessing.Understood;
			}
			switch (header.HeaderType)
			{
			case MessageHeaders.HeaderType.ReadableHeader:
				this.AddHeader(new MessageHeaders.Header(header.HeaderKind, header.ReadableHeader, headerProcessing));
				return;
			case MessageHeaders.HeaderType.BufferedMessageHeader:
				this.AddHeader(new MessageHeaders.Header(header.HeaderKind, collection.CaptureBufferedHeader(collection.bufferedMessageData, header.HeaderInfo, headerIndex), headerProcessing));
				return;
			case MessageHeaders.HeaderType.WriteableHeader:
				this.AddHeader(new MessageHeaders.Header(header.HeaderKind, header.MessageHeader, headerProcessing));
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidEnumValue", new object[]
				{
					header.HeaderType
				})));
			}
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00170979 File Offset: 0x0016EB79
		[__DynamicallyInvokable]
		public void CopyHeadersFrom(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			this.CopyHeadersFrom(message.Headers);
		}

		// Token: 0x060062FE RID: 25342 RVA: 0x001709A0 File Offset: 0x0016EBA0
		[__DynamicallyInvokable]
		public void CopyHeadersFrom(MessageHeaders collection)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("collection"));
			}
			for (int i = 0; i < collection.headerCount; i++)
			{
				this.CopyHeaderFrom(collection, i);
			}
		}

		// Token: 0x060062FF RID: 25343 RVA: 0x001709E0 File Offset: 0x0016EBE0
		[__DynamicallyInvokable]
		public void CopyTo(MessageHeaderInfo[] array, int index)
		{
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("array");
			}
			if (index < 0 || index + this.headerCount > array.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					array.Length - this.headerCount
				})));
			}
			for (int i = 0; i < this.headerCount; i++)
			{
				array[i + index] = this.headers[i].HeaderInfo;
			}
		}

		// Token: 0x06006300 RID: 25344 RVA: 0x00170A80 File Offset: 0x0016EC80
		private Exception CreateDuplicateHeaderException(MessageHeaders.HeaderKind kind)
		{
			string text;
			switch (kind)
			{
			case MessageHeaders.HeaderKind.Action:
				text = "Action";
				goto IL_7D;
			case MessageHeaders.HeaderKind.FaultTo:
				text = "FaultTo";
				goto IL_7D;
			case MessageHeaders.HeaderKind.From:
				text = "From";
				goto IL_7D;
			case MessageHeaders.HeaderKind.MessageId:
				text = "MessageID";
				goto IL_7D;
			case MessageHeaders.HeaderKind.ReplyTo:
				text = "ReplyTo";
				goto IL_7D;
			case MessageHeaders.HeaderKind.To:
				text = "To";
				goto IL_7D;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidEnumValue", new object[]
			{
				kind
			})));
			IL_7D:
			return new MessageHeaderException(SR.GetString("MultipleMessageHeaders", new object[]
			{
				text,
				this.version.Addressing.Namespace
			}), text, this.version.Addressing.Namespace, true);
		}

		// Token: 0x06006301 RID: 25345 RVA: 0x00170B48 File Offset: 0x0016ED48
		[__DynamicallyInvokable]
		public int FindHeader(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ns"));
			}
			if (ns == this.version.Addressing.Namespace)
			{
				return this.FindAddressingHeader(name, ns);
			}
			return this.FindNonAddressingHeader(name, ns, this.version.Envelope.UltimateDestinationActorValues);
		}

		// Token: 0x06006302 RID: 25346 RVA: 0x00170BC0 File Offset: 0x0016EDC0
		private int FindAddressingHeader(string name, string ns)
		{
			int num = -1;
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderKind != MessageHeaders.HeaderKind.Unknown)
				{
					MessageHeaderInfo headerInfo = this.headers[i].HeaderInfo;
					if (headerInfo.Name == name && headerInfo.Namespace == ns)
					{
						if (num >= 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MultipleMessageHeaders", new object[]
							{
								name,
								ns
							}), name, ns, true));
						}
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x06006303 RID: 25347 RVA: 0x00170C54 File Offset: 0x0016EE54
		private int FindNonAddressingHeader(string name, string ns, string[] actors)
		{
			int num = -1;
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderKind == MessageHeaders.HeaderKind.Unknown)
				{
					MessageHeaderInfo headerInfo = this.headers[i].HeaderInfo;
					if (headerInfo.Name == name && headerInfo.Namespace == ns)
					{
						for (int j = 0; j < actors.Length; j++)
						{
							if (actors[j] == headerInfo.Actor)
							{
								if (num >= 0)
								{
									if (actors.Length == 1)
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MultipleMessageHeadersWithActor", new object[]
										{
											name,
											ns,
											actors[0]
										}), name, ns, true));
									}
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MultipleMessageHeaders", new object[]
									{
										name,
										ns
									}), name, ns, true));
								}
								else
								{
									num = i;
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006304 RID: 25348 RVA: 0x00170D50 File Offset: 0x0016EF50
		[__DynamicallyInvokable]
		public int FindHeader(string name, string ns, params string[] actors)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ns"));
			}
			if (actors == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("actors"));
			}
			int num = -1;
			for (int i = 0; i < this.headerCount; i++)
			{
				MessageHeaderInfo headerInfo = this.headers[i].HeaderInfo;
				if (headerInfo.Name == name && headerInfo.Namespace == ns)
				{
					for (int j = 0; j < actors.Length; j++)
					{
						if (actors[j] == headerInfo.Actor)
						{
							if (num >= 0)
							{
								if (actors.Length == 1)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MultipleMessageHeadersWithActor", new object[]
									{
										name,
										ns,
										actors[0]
									}), name, ns, true));
								}
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MultipleMessageHeaders", new object[]
								{
									name,
									ns
								}), name, ns, true));
							}
							else
							{
								num = i;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x00170E7C File Offset: 0x0016F07C
		private int FindHeaderProperty(MessageHeaders.HeaderKind kind)
		{
			int num = -1;
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderKind == kind)
				{
					if (num >= 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateDuplicateHeaderException(kind));
					}
					num = i;
				}
			}
			return num;
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x00170ECC File Offset: 0x0016F0CC
		private int FindRelatesTo(Uri relationshipType, out UniqueId messageId)
		{
			UniqueId uniqueId = null;
			int result = -1;
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderKind == MessageHeaders.HeaderKind.RelatesTo)
				{
					Uri uri;
					UniqueId uniqueId2;
					this.GetRelatesToValues(i, out uri, out uniqueId2);
					if (relationshipType == uri)
					{
						if (uniqueId != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MultipleRelatesToHeaders", new object[]
							{
								relationshipType.AbsoluteUri
							}), "RelatesTo", this.version.Addressing.Namespace, true));
						}
						uniqueId = uniqueId2;
						result = i;
					}
				}
			}
			messageId = uniqueId;
			return result;
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x00170F6A File Offset: 0x0016F16A
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x00170F74 File Offset: 0x0016F174
		[__DynamicallyInvokable]
		public IEnumerator<MessageHeaderInfo> GetEnumerator()
		{
			MessageHeaderInfo[] array = new MessageHeaderInfo[this.headerCount];
			this.CopyTo(array, 0);
			return this.GetEnumerator(array);
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x00170F9C File Offset: 0x0016F19C
		private IEnumerator<MessageHeaderInfo> GetEnumerator(MessageHeaderInfo[] headers)
		{
			IList<MessageHeaderInfo> list = Array.AsReadOnly<MessageHeaderInfo>(headers);
			return list.GetEnumerator();
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00170FB8 File Offset: 0x0016F1B8
		internal IEnumerator<MessageHeaderInfo> GetUnderstoodEnumerator()
		{
			List<MessageHeaderInfo> list = new List<MessageHeaderInfo>();
			for (int i = 0; i < this.headerCount; i++)
			{
				if ((this.headers[i].HeaderProcessing & MessageHeaders.HeaderProcessing.Understood) != (MessageHeaders.HeaderProcessing)0)
				{
					list.Add(this.headers[i].HeaderInfo);
				}
			}
			return list.GetEnumerator();
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x00171014 File Offset: 0x0016F214
		private static XmlDictionaryReader GetBufferedMessageHeaderReaderAtHeaderContents(IBufferedMessageData bufferedMessageData)
		{
			XmlDictionaryReader messageReader = bufferedMessageData.GetMessageReader();
			if (messageReader.NodeType == XmlNodeType.Element)
			{
				messageReader.Read();
			}
			else
			{
				messageReader.ReadStartElement();
			}
			if (messageReader.NodeType == XmlNodeType.Element)
			{
				messageReader.Read();
			}
			else
			{
				messageReader.ReadStartElement();
			}
			return messageReader;
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x0017105C File Offset: 0x0016F25C
		private XmlDictionaryReader GetBufferedMessageHeaderReader(IBufferedMessageData bufferedMessageData, int bufferedMessageHeaderIndex)
		{
			if (this.nodeCount > 4096 || this.attrCount > 2048)
			{
				this.CaptureBufferedHeaders();
				return this.headers[bufferedMessageHeaderIndex].ReadableHeader.GetHeaderReader();
			}
			XmlDictionaryReader bufferedMessageHeaderReaderAtHeaderContents = MessageHeaders.GetBufferedMessageHeaderReaderAtHeaderContents(bufferedMessageData);
			for (;;)
			{
				if (bufferedMessageHeaderReaderAtHeaderContents.NodeType != XmlNodeType.Element)
				{
					bufferedMessageHeaderReaderAtHeaderContents.MoveToContent();
				}
				if (bufferedMessageHeaderIndex == 0)
				{
					break;
				}
				this.Skip(bufferedMessageHeaderReaderAtHeaderContents);
				bufferedMessageHeaderIndex--;
			}
			return bufferedMessageHeaderReaderAtHeaderContents;
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x001710CC File Offset: 0x0016F2CC
		private void Skip(XmlDictionaryReader reader)
		{
			if (reader.MoveToContent() == XmlNodeType.Element && !reader.IsEmptyElement)
			{
				int depth = reader.Depth;
				do
				{
					this.attrCount += reader.AttributeCount;
					this.nodeCount++;
				}
				while (reader.Read() && depth < reader.Depth);
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					this.nodeCount++;
					reader.Read();
					return;
				}
			}
			else
			{
				this.attrCount += reader.AttributeCount;
				this.nodeCount++;
				reader.Read();
			}
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x0017116B File Offset: 0x0016F36B
		[__DynamicallyInvokable]
		public T GetHeader<T>(string name, string ns)
		{
			return this.GetHeader<T>(name, ns, DataContractSerializerDefaults.CreateSerializer(typeof(T), name, ns, int.MaxValue));
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x0017118C File Offset: 0x0016F38C
		[__DynamicallyInvokable]
		public T GetHeader<T>(string name, string ns, params string[] actors)
		{
			int num = this.FindHeader(name, ns, actors);
			if (num < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("HeaderNotFound", new object[]
				{
					name,
					ns
				}), name, ns));
			}
			return this.GetHeader<T>(num);
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x001711D8 File Offset: 0x0016F3D8
		[__DynamicallyInvokable]
		public T GetHeader<T>(string name, string ns, XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			int num = this.FindHeader(name, ns);
			if (num < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("HeaderNotFound", new object[]
				{
					name,
					ns
				}), name, ns));
			}
			return this.GetHeader<T>(num, serializer);
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x0017123C File Offset: 0x0016F43C
		[__DynamicallyInvokable]
		public T GetHeader<T>(int index)
		{
			if (index < 0 || index >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			MessageHeaderInfo headerInfo = this.headers[index].HeaderInfo;
			return this.GetHeader<T>(index, DataContractSerializerDefaults.CreateSerializer(typeof(T), headerInfo.Name, headerInfo.Namespace, int.MaxValue));
		}

		// Token: 0x06006312 RID: 25362 RVA: 0x001712D4 File Offset: 0x0016F4D4
		[__DynamicallyInvokable]
		public T GetHeader<T>(int index, XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			T result;
			using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(index))
			{
				result = (T)((object)serializer.ReadObject(readerAtHeader));
			}
			return result;
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x0017132C File Offset: 0x0016F52C
		private MessageHeaders.HeaderKind GetHeaderKind(MessageHeaderInfo headerInfo)
		{
			MessageHeaders.HeaderKind headerKind = MessageHeaders.HeaderKind.Unknown;
			if (headerInfo.Namespace == this.version.Addressing.Namespace && this.version.Envelope.IsUltimateDestinationActor(headerInfo.Actor))
			{
				string name = headerInfo.Name;
				if (name.Length > 0)
				{
					char c = name[0];
					if (c <= 'F')
					{
						if (c != 'A')
						{
							if (c == 'F')
							{
								if (name == "From")
								{
									headerKind = MessageHeaders.HeaderKind.From;
								}
								else if (name == "FaultTo")
								{
									headerKind = MessageHeaders.HeaderKind.FaultTo;
								}
							}
						}
						else if (name == "Action")
						{
							headerKind = MessageHeaders.HeaderKind.Action;
						}
					}
					else if (c != 'M')
					{
						if (c != 'R')
						{
							if (c == 'T')
							{
								if (name == "To")
								{
									headerKind = MessageHeaders.HeaderKind.To;
								}
							}
						}
						else if (name == "ReplyTo")
						{
							headerKind = MessageHeaders.HeaderKind.ReplyTo;
						}
						else if (name == "RelatesTo")
						{
							headerKind = MessageHeaders.HeaderKind.RelatesTo;
						}
					}
					else if (name == "MessageID")
					{
						headerKind = MessageHeaders.HeaderKind.MessageId;
					}
				}
			}
			this.ValidateHeaderKind(headerKind);
			return headerKind;
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x00171434 File Offset: 0x0016F634
		private void ValidateHeaderKind(MessageHeaders.HeaderKind headerKind)
		{
			if (this.version.Envelope == EnvelopeVersion.None && headerKind != MessageHeaders.HeaderKind.Action && headerKind != MessageHeaders.HeaderKind.To)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HeadersCannotBeAddedToEnvelopeVersion", new object[]
				{
					this.version.Envelope
				})));
			}
			if (this.version.Addressing == AddressingVersion.None && headerKind != MessageHeaders.HeaderKind.Unknown && headerKind != MessageHeaders.HeaderKind.Action && headerKind != MessageHeaders.HeaderKind.To)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AddressingHeadersCannotBeAddedToAddressingVersion", new object[]
				{
					this.version.Addressing
				})));
			}
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x001714D4 File Offset: 0x0016F6D4
		[__DynamicallyInvokable]
		public XmlDictionaryReader GetReaderAtHeader(int headerIndex)
		{
			if (headerIndex < 0 || headerIndex >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			switch (this.headers[headerIndex].HeaderType)
			{
			case MessageHeaders.HeaderType.ReadableHeader:
				return this.headers[headerIndex].ReadableHeader.GetHeaderReader();
			case MessageHeaders.HeaderType.BufferedMessageHeader:
				return this.GetBufferedMessageHeaderReader(this.bufferedMessageData, headerIndex);
			case MessageHeaders.HeaderType.WriteableHeader:
			{
				MessageHeader messageHeader = this.headers[headerIndex].MessageHeader;
				BufferedHeader bufferedHeader = this.CaptureWriteableHeader(messageHeader);
				this.headers[headerIndex] = new MessageHeaders.Header(this.headers[headerIndex].HeaderKind, bufferedHeader, this.headers[headerIndex].HeaderProcessing);
				this.collectionVersion++;
				return bufferedHeader.GetHeaderReader();
			}
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidEnumValue", new object[]
				{
					this.headers[headerIndex].HeaderType
				})));
			}
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0017161C File Offset: 0x0016F81C
		internal UniqueId GetRelatesTo(Uri relationshipType)
		{
			if (relationshipType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("relationshipType"));
			}
			UniqueId result;
			this.FindRelatesTo(relationshipType, out result);
			return result;
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x00171654 File Offset: 0x0016F854
		private void GetRelatesToValues(int index, out Uri relationshipType, out UniqueId messageId)
		{
			RelatesToHeader relatesToHeader = this.headers[index].HeaderInfo as RelatesToHeader;
			if (relatesToHeader != null)
			{
				relationshipType = relatesToHeader.RelationshipType;
				messageId = relatesToHeader.UniqueId;
				return;
			}
			using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(index))
			{
				RelatesToHeader.ReadHeaderValue(readerAtHeader, this.version.Addressing, out relationshipType, out messageId);
			}
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x001716C4 File Offset: 0x0016F8C4
		internal string[] GetHeaderAttributes(string localName, string ns)
		{
			string[] array = null;
			if (this.ContainsOnlyBufferedMessageHeaders)
			{
				XmlDictionaryReader messageReader = this.bufferedMessageData.GetMessageReader();
				messageReader.ReadStartElement();
				messageReader.ReadStartElement();
				int num = 0;
				while (messageReader.IsStartElement())
				{
					string attribute = messageReader.GetAttribute(localName, ns);
					if (attribute != null)
					{
						if (array == null)
						{
							array = new string[this.headerCount];
						}
						array[num] = attribute;
					}
					if (num == this.headerCount - 1)
					{
						break;
					}
					messageReader.Skip();
					num++;
				}
				messageReader.Close();
			}
			else
			{
				for (int i = 0; i < this.headerCount; i++)
				{
					if (this.headers[i].HeaderType != MessageHeaders.HeaderType.WriteableHeader)
					{
						using (XmlDictionaryReader readerAtHeader = this.GetReaderAtHeader(i))
						{
							string attribute2 = readerAtHeader.GetAttribute(localName, ns);
							if (attribute2 != null)
							{
								if (array == null)
								{
									array = new string[this.headerCount];
								}
								array[i] = attribute2;
							}
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x001717B4 File Offset: 0x0016F9B4
		internal MessageHeader GetMessageHeader(int index)
		{
			if (index < 0 || index >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", index, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			switch (this.headers[index].HeaderType)
			{
			case MessageHeaders.HeaderType.ReadableHeader:
			case MessageHeaders.HeaderType.WriteableHeader:
				return this.headers[index].MessageHeader;
			case MessageHeaders.HeaderType.BufferedMessageHeader:
			{
				MessageHeader messageHeader = this.CaptureBufferedHeader(this.bufferedMessageData, this.headers[index].HeaderInfo, index);
				this.headers[index] = new MessageHeaders.Header(this.headers[index].HeaderKind, messageHeader, this.headers[index].HeaderProcessing);
				this.collectionVersion++;
				return messageHeader;
			}
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidEnumValue", new object[]
				{
					this.headers[index].HeaderType
				})));
			}
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x001718E8 File Offset: 0x0016FAE8
		internal Collection<MessageHeaderInfo> GetHeadersNotUnderstood()
		{
			Collection<MessageHeaderInfo> collection = null;
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderProcessing == MessageHeaders.HeaderProcessing.MustUnderstand)
				{
					if (collection == null)
					{
						collection = new Collection<MessageHeaderInfo>();
					}
					MessageHeaderInfo headerInfo = this.headers[i].HeaderInfo;
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 524302, SR.GetString("TraceCodeDidNotUnderstandMessageHeader"), new MessageHeaderInfoTraceRecord(headerInfo), null, null);
					}
					collection.Add(headerInfo);
				}
			}
			return collection;
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x00171963 File Offset: 0x0016FB63
		[__DynamicallyInvokable]
		public bool HaveMandatoryHeadersBeenUnderstood()
		{
			return this.HaveMandatoryHeadersBeenUnderstood(this.version.Envelope.MustUnderstandActorValues);
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x0017197C File Offset: 0x0016FB7C
		[__DynamicallyInvokable]
		public bool HaveMandatoryHeadersBeenUnderstood(params string[] actors)
		{
			if (actors == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("actors"));
			}
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderProcessing == MessageHeaders.HeaderProcessing.MustUnderstand)
				{
					for (int j = 0; j < actors.Length; j++)
					{
						if (this.headers[i].HeaderInfo.Actor == actors[j])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x001719F8 File Offset: 0x0016FBF8
		internal void Init(MessageVersion version, int initialSize)
		{
			this.nodeCount = 0;
			this.attrCount = 0;
			if (initialSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("initialSize", initialSize, SR.GetString("ValueMustBeNonNegative")));
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			this.version = version;
			this.headers = new MessageHeaders.Header[initialSize];
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00171A62 File Offset: 0x0016FC62
		internal void Init(MessageVersion version)
		{
			this.nodeCount = 0;
			this.attrCount = 0;
			this.version = version;
			this.collectionVersion = 0;
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x00171A80 File Offset: 0x0016FC80
		internal void Init(MessageVersion version, XmlDictionaryReader reader, IBufferedMessageData bufferedMessageData, RecycledMessageState recycledMessageState, bool[] understoodHeaders, bool understoodHeadersModified)
		{
			this.nodeCount = 0;
			this.attrCount = 0;
			this.version = version;
			this.bufferedMessageData = bufferedMessageData;
			if (version.Envelope != EnvelopeVersion.None)
			{
				this.understoodHeadersModified = (understoodHeaders != null && understoodHeadersModified);
				if (reader.IsEmptyElement)
				{
					reader.Read();
					return;
				}
				EnvelopeVersion envelope = version.Envelope;
				reader.ReadStartElement();
				AddressingDictionary addressingDictionary = XD.AddressingDictionary;
				if (MessageHeaders.localNames == null)
				{
					XmlDictionaryString[] array = new XmlDictionaryString[]
					{
						null,
						null,
						null,
						null,
						null,
						null,
						addressingDictionary.To
					};
					array[0] = addressingDictionary.Action;
					array[3] = addressingDictionary.MessageId;
					array[5] = addressingDictionary.RelatesTo;
					array[4] = addressingDictionary.ReplyTo;
					array[2] = addressingDictionary.From;
					array[1] = addressingDictionary.FaultTo;
					Thread.MemoryBarrier();
					MessageHeaders.localNames = array;
				}
				int num = 0;
				while (reader.IsStartElement())
				{
					this.ReadBufferedHeader(reader, recycledMessageState, MessageHeaders.localNames, understoodHeaders != null && understoodHeaders[num++]);
				}
				reader.ReadEndElement();
			}
			this.collectionVersion = 0;
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x00171B78 File Offset: 0x0016FD78
		[__DynamicallyInvokable]
		public void Insert(int headerIndex, MessageHeader header)
		{
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("header"));
			}
			if (!header.IsMessageVersionSupported(this.version))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessageHeaderVersionNotSupported", new object[]
				{
					header.GetType().FullName,
					this.version.Envelope.ToString()
				}), "header"));
			}
			this.Insert(headerIndex, header, this.GetHeaderKind(header));
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x00171C00 File Offset: 0x0016FE00
		private void Insert(int headerIndex, MessageHeader header, MessageHeaders.HeaderKind kind)
		{
			ReadableMessageHeader readableMessageHeader = header as ReadableMessageHeader;
			MessageHeaders.HeaderProcessing headerProcessing = header.MustUnderstand ? MessageHeaders.HeaderProcessing.MustUnderstand : ((MessageHeaders.HeaderProcessing)0);
			if (kind != MessageHeaders.HeaderKind.Unknown)
			{
				headerProcessing |= MessageHeaders.HeaderProcessing.Understood;
			}
			if (readableMessageHeader != null)
			{
				this.InsertHeader(headerIndex, new MessageHeaders.Header(kind, readableMessageHeader, headerProcessing));
				return;
			}
			this.InsertHeader(headerIndex, new MessageHeaders.Header(kind, header, headerProcessing));
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x00171C4C File Offset: 0x0016FE4C
		private void InsertHeader(int headerIndex, MessageHeaders.Header header)
		{
			this.ValidateHeaderKind(header.HeaderKind);
			if (headerIndex < 0 || headerIndex > this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			if (this.headerCount == this.headers.Length)
			{
				if (this.headers.Length == 0)
				{
					this.headers = new MessageHeaders.Header[1];
				}
				else
				{
					MessageHeaders.Header[] array = new MessageHeaders.Header[this.headers.Length * 2];
					this.headers.CopyTo(array, 0);
					this.headers = array;
				}
			}
			if (headerIndex < this.headerCount)
			{
				if (this.bufferedMessageData != null)
				{
					for (int i = headerIndex; i < this.headerCount; i++)
					{
						if (this.headers[i].HeaderType == MessageHeaders.HeaderType.BufferedMessageHeader)
						{
							this.CaptureBufferedHeaders();
							break;
						}
					}
				}
				Array.Copy(this.headers, headerIndex, this.headers, headerIndex + 1, this.headerCount - headerIndex);
			}
			this.headers[headerIndex] = header;
			this.headerCount++;
			this.collectionVersion++;
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x00171D84 File Offset: 0x0016FF84
		internal bool IsUnderstood(int i)
		{
			return (this.headers[i].HeaderProcessing & MessageHeaders.HeaderProcessing.Understood) > (MessageHeaders.HeaderProcessing)0;
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00171D9C File Offset: 0x0016FF9C
		internal bool IsUnderstood(MessageHeaderInfo headerInfo)
		{
			if (headerInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("headerInfo"));
			}
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderInfo == headerInfo && this.IsUnderstood(i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x00171DF4 File Offset: 0x0016FFF4
		private void ReadBufferedHeader(XmlDictionaryReader reader, RecycledMessageState recycledMessageState, XmlDictionaryString[] localNames, bool understood)
		{
			if (this.version.Addressing == AddressingVersion.None && reader.NamespaceURI == AddressingVersion.None.Namespace)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("AddressingHeadersCannotBeAddedToAddressingVersion", new object[]
				{
					this.version.Addressing
				})));
			}
			string actor;
			bool flag;
			bool relay;
			bool isRefParam;
			MessageHeader.GetHeaderAttributes(reader, this.version, out actor, out flag, out relay, out isRefParam);
			MessageHeaders.HeaderKind headerKind = MessageHeaders.HeaderKind.Unknown;
			MessageHeaderInfo messageHeaderInfo = null;
			if (this.version.Envelope.IsUltimateDestinationActor(actor))
			{
				headerKind = (MessageHeaders.HeaderKind)reader.IndexOfLocalName(localNames, this.version.Addressing.DictionaryNamespace);
				switch (headerKind)
				{
				case MessageHeaders.HeaderKind.Action:
					messageHeaderInfo = ActionHeader.ReadHeader(reader, this.version.Addressing, actor, flag, relay);
					break;
				case MessageHeaders.HeaderKind.FaultTo:
					messageHeaderInfo = FaultToHeader.ReadHeader(reader, this.version.Addressing, actor, flag, relay);
					break;
				case MessageHeaders.HeaderKind.From:
					messageHeaderInfo = FromHeader.ReadHeader(reader, this.version.Addressing, actor, flag, relay);
					break;
				case MessageHeaders.HeaderKind.MessageId:
					messageHeaderInfo = MessageIDHeader.ReadHeader(reader, this.version.Addressing, actor, flag, relay);
					break;
				case MessageHeaders.HeaderKind.ReplyTo:
					messageHeaderInfo = ReplyToHeader.ReadHeader(reader, this.version.Addressing, actor, flag, relay);
					break;
				case MessageHeaders.HeaderKind.RelatesTo:
					messageHeaderInfo = RelatesToHeader.ReadHeader(reader, this.version.Addressing, actor, flag, relay);
					break;
				case MessageHeaders.HeaderKind.To:
					messageHeaderInfo = ToHeader.ReadHeader(reader, this.version.Addressing, recycledMessageState.UriCache, actor, flag, relay);
					break;
				default:
					headerKind = MessageHeaders.HeaderKind.Unknown;
					break;
				}
			}
			if (messageHeaderInfo == null)
			{
				messageHeaderInfo = recycledMessageState.HeaderInfoCache.TakeHeaderInfo(reader, actor, flag, relay, isRefParam);
				reader.Skip();
			}
			MessageHeaders.HeaderProcessing headerProcessing = flag ? MessageHeaders.HeaderProcessing.MustUnderstand : ((MessageHeaders.HeaderProcessing)0);
			if (headerKind != MessageHeaders.HeaderKind.Unknown || understood)
			{
				headerProcessing |= MessageHeaders.HeaderProcessing.Understood;
				MessageHeaders.TraceUnderstood(messageHeaderInfo);
			}
			if (headerKind != MessageHeaders.HeaderKind.Unknown && !LocalAppContextSwitches.AllowMultipleStandardSoapHeaders && this.FindHeaderProperty(headerKind) >= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateDuplicateHeaderException(headerKind));
			}
			this.AddHeader(new MessageHeaders.Header(headerKind, messageHeaderInfo, headerProcessing));
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x00171FF8 File Offset: 0x001701F8
		internal void Recycle(HeaderInfoCache headerInfoCache)
		{
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderKind == MessageHeaders.HeaderKind.Unknown)
				{
					headerInfoCache.ReturnHeaderInfo(this.headers[i].HeaderInfo);
				}
			}
			this.Clear();
			this.collectionVersion = 0;
			if (this.understoodHeaders != null)
			{
				this.understoodHeaders.Modified = false;
			}
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x00172064 File Offset: 0x00170264
		internal void RemoveUnderstood(MessageHeaderInfo headerInfo)
		{
			if (headerInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("headerInfo"));
			}
			for (int i = 0; i < this.headerCount; i++)
			{
				if (this.headers[i].HeaderInfo == headerInfo)
				{
					if ((this.headers[i].HeaderProcessing & MessageHeaders.HeaderProcessing.Understood) == (MessageHeaders.HeaderProcessing)0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("HeaderAlreadyNotUnderstood", new object[]
						{
							headerInfo.Name,
							headerInfo.Namespace
						}), "headerInfo"));
					}
					MessageHeaders.Header[] array = this.headers;
					int num = i;
					array[num].HeaderProcessing = (array[num].HeaderProcessing & ~MessageHeaders.HeaderProcessing.Understood);
				}
			}
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x00172120 File Offset: 0x00170320
		[__DynamicallyInvokable]
		public void RemoveAll(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("ns"));
			}
			for (int i = this.headerCount - 1; i >= 0; i--)
			{
				MessageHeaderInfo headerInfo = this.headers[i].HeaderInfo;
				if (headerInfo.Name == name && headerInfo.Namespace == ns)
				{
					this.RemoveAt(i);
				}
			}
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x001721A8 File Offset: 0x001703A8
		[__DynamicallyInvokable]
		public void RemoveAt(int headerIndex)
		{
			if (headerIndex < 0 || headerIndex >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			if (this.bufferedMessageData != null && this.headers[headerIndex].HeaderType == MessageHeaders.HeaderType.BufferedMessageHeader)
			{
				this.CaptureBufferedHeaders(headerIndex);
			}
			Array.Copy(this.headers, headerIndex + 1, this.headers, headerIndex, this.headerCount - headerIndex - 1);
			MessageHeaders.Header[] array = this.headers;
			int num = this.headerCount - 1;
			this.headerCount = num;
			array[num] = default(MessageHeaders.Header);
			this.collectionVersion++;
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x00172278 File Offset: 0x00170478
		internal void ReplaceAt(int headerIndex, MessageHeader header)
		{
			if (headerIndex < 0 || headerIndex >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("header");
			}
			this.ReplaceAt(headerIndex, header, this.GetHeaderKind(header));
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001722F8 File Offset: 0x001704F8
		private void ReplaceAt(int headerIndex, MessageHeader header, MessageHeaders.HeaderKind kind)
		{
			MessageHeaders.HeaderProcessing headerProcessing = header.MustUnderstand ? MessageHeaders.HeaderProcessing.MustUnderstand : ((MessageHeaders.HeaderProcessing)0);
			if (kind != MessageHeaders.HeaderKind.Unknown)
			{
				headerProcessing |= MessageHeaders.HeaderProcessing.Understood;
			}
			ReadableMessageHeader readableMessageHeader = header as ReadableMessageHeader;
			if (readableMessageHeader != null)
			{
				this.headers[headerIndex] = new MessageHeaders.Header(kind, readableMessageHeader, headerProcessing);
			}
			else
			{
				this.headers[headerIndex] = new MessageHeaders.Header(kind, header, headerProcessing);
			}
			this.collectionVersion++;
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x0017235C File Offset: 0x0017055C
		[__DynamicallyInvokable]
		public void SetAction(XmlDictionaryString action)
		{
			if (action == null)
			{
				this.SetHeaderProperty(MessageHeaders.HeaderKind.Action, null);
				return;
			}
			this.SetActionHeader(ActionHeader.Create(action, this.version.Addressing));
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x00172381 File Offset: 0x00170581
		internal void SetActionHeader(ActionHeader actionHeader)
		{
			this.SetHeaderProperty(MessageHeaders.HeaderKind.Action, actionHeader);
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x0017238B File Offset: 0x0017058B
		internal void SetFaultToHeader(FaultToHeader faultToHeader)
		{
			this.SetHeaderProperty(MessageHeaders.HeaderKind.FaultTo, faultToHeader);
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x00172395 File Offset: 0x00170595
		internal void SetFromHeader(FromHeader fromHeader)
		{
			this.SetHeaderProperty(MessageHeaders.HeaderKind.From, fromHeader);
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x0017239F File Offset: 0x0017059F
		internal void SetMessageIDHeader(MessageIDHeader messageIDHeader)
		{
			this.SetHeaderProperty(MessageHeaders.HeaderKind.MessageId, messageIDHeader);
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x001723AC File Offset: 0x001705AC
		internal void SetRelatesTo(Uri relationshipType, UniqueId messageId)
		{
			if (relationshipType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("relationshipType");
			}
			RelatesToHeader relatesToHeader;
			if (messageId != null)
			{
				relatesToHeader = RelatesToHeader.Create(messageId, this.version.Addressing, relationshipType);
			}
			else
			{
				relatesToHeader = null;
			}
			this.SetRelatesTo(RelatesToHeader.ReplyRelationshipType, relatesToHeader);
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x001723F8 File Offset: 0x001705F8
		private void SetRelatesTo(Uri relationshipType, RelatesToHeader relatesToHeader)
		{
			UniqueId uniqueId;
			int num = this.FindRelatesTo(relationshipType, out uniqueId);
			if (num < 0)
			{
				if (relatesToHeader != null)
				{
					this.Add(relatesToHeader, MessageHeaders.HeaderKind.RelatesTo);
				}
				return;
			}
			if (relatesToHeader == null)
			{
				this.RemoveAt(num);
				return;
			}
			this.ReplaceAt(num, relatesToHeader, MessageHeaders.HeaderKind.RelatesTo);
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x00172433 File Offset: 0x00170633
		internal void SetReplyToHeader(ReplyToHeader replyToHeader)
		{
			this.SetHeaderProperty(MessageHeaders.HeaderKind.ReplyTo, replyToHeader);
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x0017243D File Offset: 0x0017063D
		internal void SetToHeader(ToHeader toHeader)
		{
			this.SetHeaderProperty(MessageHeaders.HeaderKind.To, toHeader);
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x00172448 File Offset: 0x00170648
		private void SetHeaderProperty(MessageHeaders.HeaderKind kind, MessageHeader header)
		{
			int num = this.FindHeaderProperty(kind);
			if (num < 0)
			{
				if (header != null)
				{
					this.Add(header, kind);
				}
				return;
			}
			if (header == null)
			{
				this.RemoveAt(num);
				return;
			}
			this.ReplaceAt(num, header, kind);
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00172481 File Offset: 0x00170681
		[__DynamicallyInvokable]
		public void WriteHeader(int headerIndex, XmlWriter writer)
		{
			this.WriteHeader(headerIndex, XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x00172490 File Offset: 0x00170690
		[__DynamicallyInvokable]
		public void WriteHeader(int headerIndex, XmlDictionaryWriter writer)
		{
			this.WriteStartHeader(headerIndex, writer);
			this.WriteHeaderContents(headerIndex, writer);
			writer.WriteEndElement();
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x001724A8 File Offset: 0x001706A8
		[__DynamicallyInvokable]
		public void WriteStartHeader(int headerIndex, XmlWriter writer)
		{
			this.WriteStartHeader(headerIndex, XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x001724B8 File Offset: 0x001706B8
		[__DynamicallyInvokable]
		public void WriteStartHeader(int headerIndex, XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (headerIndex < 0 || headerIndex >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			switch (this.headers[headerIndex].HeaderType)
			{
			case MessageHeaders.HeaderType.ReadableHeader:
			case MessageHeaders.HeaderType.WriteableHeader:
				this.headers[headerIndex].MessageHeader.WriteStartHeader(writer, this.version);
				return;
			case MessageHeaders.HeaderType.BufferedMessageHeader:
				this.WriteStartBufferedMessageHeader(this.bufferedMessageData, headerIndex, writer);
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidEnumValue", new object[]
				{
					this.headers[headerIndex].HeaderType
				})));
			}
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x001725B4 File Offset: 0x001707B4
		[__DynamicallyInvokable]
		public void WriteHeaderContents(int headerIndex, XmlWriter writer)
		{
			this.WriteHeaderContents(headerIndex, XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x001725C4 File Offset: 0x001707C4
		[__DynamicallyInvokable]
		public void WriteHeaderContents(int headerIndex, XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (headerIndex < 0 || headerIndex >= this.headerCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("headerIndex", headerIndex, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.headerCount
				})));
			}
			switch (this.headers[headerIndex].HeaderType)
			{
			case MessageHeaders.HeaderType.ReadableHeader:
			case MessageHeaders.HeaderType.WriteableHeader:
				this.headers[headerIndex].MessageHeader.WriteHeaderContents(writer, this.version);
				return;
			case MessageHeaders.HeaderType.BufferedMessageHeader:
				this.WriteBufferedMessageHeaderContents(this.bufferedMessageData, headerIndex, writer);
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidEnumValue", new object[]
				{
					this.headers[headerIndex].HeaderType
				})));
			}
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x001726C0 File Offset: 0x001708C0
		private static void TraceUnderstood(MessageHeaderInfo info)
		{
			if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524303, SR.GetString("TraceCodeUnderstoodMessageHeader"), new MessageHeaderInfoTraceRecord(info), null, null);
			}
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x001726E8 File Offset: 0x001708E8
		private void WriteBufferedMessageHeader(IBufferedMessageData bufferedMessageData, int bufferedMessageHeaderIndex, XmlWriter writer)
		{
			using (XmlReader bufferedMessageHeaderReader = this.GetBufferedMessageHeaderReader(bufferedMessageData, bufferedMessageHeaderIndex))
			{
				writer.WriteNode(bufferedMessageHeaderReader, false);
			}
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x00172724 File Offset: 0x00170924
		private void WriteStartBufferedMessageHeader(IBufferedMessageData bufferedMessageData, int bufferedMessageHeaderIndex, XmlWriter writer)
		{
			using (XmlReader bufferedMessageHeaderReader = this.GetBufferedMessageHeaderReader(bufferedMessageData, bufferedMessageHeaderIndex))
			{
				writer.WriteStartElement(bufferedMessageHeaderReader.Prefix, bufferedMessageHeaderReader.LocalName, bufferedMessageHeaderReader.NamespaceURI);
				writer.WriteAttributes(bufferedMessageHeaderReader, false);
			}
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x00172778 File Offset: 0x00170978
		private void WriteBufferedMessageHeaderContents(IBufferedMessageData bufferedMessageData, int bufferedMessageHeaderIndex, XmlWriter writer)
		{
			using (XmlReader bufferedMessageHeaderReader = this.GetBufferedMessageHeaderReader(bufferedMessageData, bufferedMessageHeaderIndex))
			{
				if (!bufferedMessageHeaderReader.IsEmptyElement)
				{
					bufferedMessageHeaderReader.ReadStartElement();
					while (bufferedMessageHeaderReader.NodeType != XmlNodeType.EndElement)
					{
						writer.WriteNode(bufferedMessageHeaderReader, false);
					}
					bufferedMessageHeaderReader.ReadEndElement();
				}
			}
		}

		// Token: 0x0400393F RID: 14655
		private int collectionVersion;

		// Token: 0x04003940 RID: 14656
		private int headerCount;

		// Token: 0x04003941 RID: 14657
		private MessageHeaders.Header[] headers;

		// Token: 0x04003942 RID: 14658
		private MessageVersion version;

		// Token: 0x04003943 RID: 14659
		private IBufferedMessageData bufferedMessageData;

		// Token: 0x04003944 RID: 14660
		private UnderstoodHeaders understoodHeaders;

		// Token: 0x04003945 RID: 14661
		private const int InitialHeaderCount = 4;

		// Token: 0x04003946 RID: 14662
		private const int MaxRecycledArrayLength = 8;

		// Token: 0x04003947 RID: 14663
		private static XmlDictionaryString[] localNames;

		// Token: 0x04003948 RID: 14664
		internal const string WildcardAction = "*";

		// Token: 0x04003949 RID: 14665
		private const int MaxBufferedHeaderNodes = 4096;

		// Token: 0x0400394A RID: 14666
		private const int MaxBufferedHeaderAttributes = 2048;

		// Token: 0x0400394B RID: 14667
		private int nodeCount;

		// Token: 0x0400394C RID: 14668
		private int attrCount;

		// Token: 0x0400394D RID: 14669
		private bool understoodHeadersModified;

		// Token: 0x02000E48 RID: 3656
		private enum HeaderType : byte
		{
			// Token: 0x04004A4B RID: 19019
			Invalid,
			// Token: 0x04004A4C RID: 19020
			ReadableHeader,
			// Token: 0x04004A4D RID: 19021
			BufferedMessageHeader,
			// Token: 0x04004A4E RID: 19022
			WriteableHeader
		}

		// Token: 0x02000E49 RID: 3657
		private enum HeaderKind : byte
		{
			// Token: 0x04004A50 RID: 19024
			Action,
			// Token: 0x04004A51 RID: 19025
			FaultTo,
			// Token: 0x04004A52 RID: 19026
			From,
			// Token: 0x04004A53 RID: 19027
			MessageId,
			// Token: 0x04004A54 RID: 19028
			ReplyTo,
			// Token: 0x04004A55 RID: 19029
			RelatesTo,
			// Token: 0x04004A56 RID: 19030
			To,
			// Token: 0x04004A57 RID: 19031
			Unknown
		}

		// Token: 0x02000E4A RID: 3658
		[Flags]
		private enum HeaderProcessing : byte
		{
			// Token: 0x04004A59 RID: 19033
			MustUnderstand = 1,
			// Token: 0x04004A5A RID: 19034
			Understood = 2
		}

		// Token: 0x02000E4B RID: 3659
		private struct Header
		{
			// Token: 0x060082D5 RID: 33493 RVA: 0x001E3A5E File Offset: 0x001E1C5E
			public Header(MessageHeaders.HeaderKind kind, MessageHeaderInfo info, MessageHeaders.HeaderProcessing processing)
			{
				this.kind = kind;
				this.type = MessageHeaders.HeaderType.BufferedMessageHeader;
				this.info = info;
				this.processing = processing;
			}

			// Token: 0x060082D6 RID: 33494 RVA: 0x001E3A7C File Offset: 0x001E1C7C
			public Header(MessageHeaders.HeaderKind kind, ReadableMessageHeader readableHeader, MessageHeaders.HeaderProcessing processing)
			{
				this.kind = kind;
				this.type = MessageHeaders.HeaderType.ReadableHeader;
				this.info = readableHeader;
				this.processing = processing;
			}

			// Token: 0x060082D7 RID: 33495 RVA: 0x001E3A9A File Offset: 0x001E1C9A
			public Header(MessageHeaders.HeaderKind kind, MessageHeader header, MessageHeaders.HeaderProcessing processing)
			{
				this.kind = kind;
				this.type = MessageHeaders.HeaderType.WriteableHeader;
				this.info = header;
				this.processing = processing;
			}

			// Token: 0x17001CEC RID: 7404
			// (get) Token: 0x060082D8 RID: 33496 RVA: 0x001E3AB8 File Offset: 0x001E1CB8
			public MessageHeaders.HeaderType HeaderType
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17001CED RID: 7405
			// (get) Token: 0x060082D9 RID: 33497 RVA: 0x001E3AC0 File Offset: 0x001E1CC0
			public MessageHeaders.HeaderKind HeaderKind
			{
				get
				{
					return this.kind;
				}
			}

			// Token: 0x17001CEE RID: 7406
			// (get) Token: 0x060082DA RID: 33498 RVA: 0x001E3AC8 File Offset: 0x001E1CC8
			public MessageHeaderInfo HeaderInfo
			{
				get
				{
					return this.info;
				}
			}

			// Token: 0x17001CEF RID: 7407
			// (get) Token: 0x060082DB RID: 33499 RVA: 0x001E3AD0 File Offset: 0x001E1CD0
			public MessageHeader MessageHeader
			{
				get
				{
					return (MessageHeader)this.info;
				}
			}

			// Token: 0x17001CF0 RID: 7408
			// (get) Token: 0x060082DC RID: 33500 RVA: 0x001E3ADD File Offset: 0x001E1CDD
			// (set) Token: 0x060082DD RID: 33501 RVA: 0x001E3AE5 File Offset: 0x001E1CE5
			public MessageHeaders.HeaderProcessing HeaderProcessing
			{
				get
				{
					return this.processing;
				}
				set
				{
					this.processing = value;
				}
			}

			// Token: 0x17001CF1 RID: 7409
			// (get) Token: 0x060082DE RID: 33502 RVA: 0x001E3AEE File Offset: 0x001E1CEE
			public ReadableMessageHeader ReadableHeader
			{
				get
				{
					return (ReadableMessageHeader)this.info;
				}
			}

			// Token: 0x04004A5B RID: 19035
			private MessageHeaders.HeaderType type;

			// Token: 0x04004A5C RID: 19036
			private MessageHeaders.HeaderKind kind;

			// Token: 0x04004A5D RID: 19037
			private MessageHeaders.HeaderProcessing processing;

			// Token: 0x04004A5E RID: 19038
			private MessageHeaderInfo info;
		}
	}
}
