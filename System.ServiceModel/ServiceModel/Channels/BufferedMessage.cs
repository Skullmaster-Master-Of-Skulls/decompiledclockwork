using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C0 RID: 2496
	internal sealed class BufferedMessage : ReceivedMessage
	{
		// Token: 0x06006212 RID: 25106 RVA: 0x0016CFDB File Offset: 0x0016B1DB
		public BufferedMessage(IBufferedMessageData messageData, RecycledMessageState recycledMessageState) : this(messageData, recycledMessageState, null, false)
		{
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x0016CFE8 File Offset: 0x0016B1E8
		public BufferedMessage(IBufferedMessageData messageData, RecycledMessageState recycledMessageState, bool[] understoodHeaders, bool understoodHeadersModified)
		{
			bool flag = true;
			try
			{
				this.recycledMessageState = recycledMessageState;
				this.messageData = messageData;
				this.properties = recycledMessageState.TakeProperties();
				if (this.properties == null)
				{
					this.properties = new MessageProperties();
				}
				XmlDictionaryReader messageReader = messageData.GetMessageReader();
				MessageVersion messageVersion = messageData.MessageEncoder.MessageVersion;
				if (messageVersion.Envelope == EnvelopeVersion.None)
				{
					this.reader = messageReader;
					this.headers = new MessageHeaders(messageVersion);
				}
				else
				{
					EnvelopeVersion envelopeVersion = ReceivedMessage.ReadStartEnvelope(messageReader);
					if (messageVersion.Envelope != envelopeVersion)
					{
						Exception ex = new ArgumentException(SR.GetString("EncoderEnvelopeVersionMismatch", new object[]
						{
							envelopeVersion,
							messageVersion.Envelope
						}), "reader");
						throw TraceUtility.ThrowHelperError(new CommunicationException(ex.Message, ex), this);
					}
					if (ReceivedMessage.HasHeaderElement(messageReader, envelopeVersion))
					{
						this.headers = recycledMessageState.TakeHeaders();
						if (this.headers == null)
						{
							this.headers = new MessageHeaders(messageVersion, messageReader, messageData, recycledMessageState, understoodHeaders, understoodHeadersModified);
						}
						else
						{
							this.headers.Init(messageVersion, messageReader, messageData, recycledMessageState, understoodHeaders, understoodHeadersModified);
						}
					}
					else
					{
						this.headers = new MessageHeaders(messageVersion);
					}
					ReceivedMessage.VerifyStartBody(messageReader, envelopeVersion);
					int maxValue = int.MaxValue;
					this.bodyAttributes = XmlAttributeHolder.ReadAttributes(messageReader, ref maxValue);
					if (maxValue < 2147479551)
					{
						this.bodyAttributes = null;
					}
					if (base.ReadStartBody(messageReader))
					{
						this.reader = messageReader;
					}
					else
					{
						messageReader.Close();
					}
				}
				flag = false;
			}
			finally
			{
				if (flag && MessageLogger.LoggingEnabled)
				{
					MessageLogger.LogMessage(messageData.Buffer, MessageLoggingSource.Malformed);
				}
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x0016D180 File Offset: 0x0016B380
		public override MessageHeaders Headers
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.headers;
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x06006215 RID: 25109 RVA: 0x0016D19D File Offset: 0x0016B39D
		internal IBufferedMessageData MessageData
		{
			get
			{
				return this.messageData;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x0016D1A5 File Offset: 0x0016B3A5
		public override MessageProperties Properties
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.properties;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x0016D1C2 File Offset: 0x0016B3C2
		internal override RecycledMessageState RecycledMessageState
		{
			get
			{
				return this.recycledMessageState;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x0016D1CA File Offset: 0x0016B3CA
		public override MessageVersion Version
		{
			get
			{
				return this.headers.MessageVersion;
			}
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x0016D1D8 File Offset: 0x0016B3D8
		protected override XmlDictionaryReader OnGetReaderAtBodyContents()
		{
			XmlDictionaryReader result = this.reader;
			this.reader = null;
			return result;
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x0016D1F4 File Offset: 0x0016B3F4
		internal override XmlDictionaryReader GetReaderAtHeader()
		{
			if (!this.headers.ContainsOnlyBufferedMessageHeaders)
			{
				return base.GetReaderAtHeader();
			}
			XmlDictionaryReader messageReader = this.messageData.GetMessageReader();
			if (messageReader.NodeType != XmlNodeType.Element)
			{
				messageReader.MoveToContent();
			}
			messageReader.Read();
			if (ReceivedMessage.HasHeaderElement(messageReader, this.headers.MessageVersion.Envelope))
			{
				return messageReader;
			}
			return base.GetReaderAtHeader();
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x0016D258 File Offset: 0x0016B458
		public XmlDictionaryReader GetBufferedReaderAtBody()
		{
			XmlDictionaryReader messageReader = this.messageData.GetMessageReader();
			if (messageReader.NodeType != XmlNodeType.Element)
			{
				messageReader.MoveToContent();
			}
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				messageReader.Read();
				if (ReceivedMessage.HasHeaderElement(messageReader, this.headers.MessageVersion.Envelope))
				{
					messageReader.Skip();
				}
				if (messageReader.NodeType != XmlNodeType.Element)
				{
					messageReader.MoveToContent();
				}
			}
			return messageReader;
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x0016D2C9 File Offset: 0x0016B4C9
		public XmlDictionaryReader GetMessageReader()
		{
			return this.messageData.GetMessageReader();
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x0016D2D8 File Offset: 0x0016B4D8
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			using (XmlDictionaryReader bufferedReaderAtBody = this.GetBufferedReaderAtBody())
			{
				if (this.Version == MessageVersion.None)
				{
					writer.WriteNode(bufferedReaderAtBody, false);
				}
				else if (!bufferedReaderAtBody.IsEmptyElement)
				{
					bufferedReaderAtBody.ReadStartElement();
					while (bufferedReaderAtBody.NodeType != XmlNodeType.EndElement)
					{
						writer.WriteNode(bufferedReaderAtBody, false);
					}
				}
			}
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x0016D344 File Offset: 0x0016B544
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

		// Token: 0x0600621F RID: 25119 RVA: 0x0016D45C File Offset: 0x0016B65C
		protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
		{
			using (XmlDictionaryReader messageReader = this.GetMessageReader())
			{
				messageReader.MoveToContent();
				EnvelopeVersion envelope = this.Version.Envelope;
				writer.WriteStartElement(messageReader.Prefix, "Envelope", envelope.Namespace);
				writer.WriteAttributes(messageReader, false);
			}
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x0016D4C0 File Offset: 0x0016B6C0
		protected override void OnWriteStartHeaders(XmlDictionaryWriter writer)
		{
			using (XmlDictionaryReader messageReader = this.GetMessageReader())
			{
				messageReader.MoveToContent();
				EnvelopeVersion envelope = this.Version.Envelope;
				messageReader.Read();
				if (ReceivedMessage.HasHeaderElement(messageReader, envelope))
				{
					writer.WriteStartElement(messageReader.Prefix, "Header", envelope.Namespace);
					writer.WriteAttributes(messageReader, false);
				}
				else
				{
					writer.WriteStartElement("s", "Header", envelope.Namespace);
				}
			}
		}

		// Token: 0x06006221 RID: 25121 RVA: 0x0016D54C File Offset: 0x0016B74C
		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			using (XmlDictionaryReader bufferedReaderAtBody = this.GetBufferedReaderAtBody())
			{
				writer.WriteStartElement(bufferedReaderAtBody.Prefix, "Body", this.Version.Envelope.Namespace);
				writer.WriteAttributes(bufferedReaderAtBody, false);
			}
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x0016D5A8 File Offset: 0x0016B7A8
		protected override MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
		{
			if (this.headers.ContainsOnlyBufferedMessageHeaders)
			{
				KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[this.Properties.Count];
				((ICollection<KeyValuePair<string, object>>)this.Properties).CopyTo(array, 0);
				this.messageData.EnableMultipleUsers();
				bool[] array2 = null;
				if (this.headers.HasMustUnderstandBeenModified)
				{
					array2 = new bool[this.headers.Count];
					for (int i = 0; i < this.headers.Count; i++)
					{
						array2[i] = this.headers.IsUnderstood(i);
					}
				}
				return new BufferedMessageBuffer(this.messageData, array, array2, this.headers.HasMustUnderstandBeenModified);
			}
			if (this.reader != null)
			{
				return base.OnCreateBufferedCopy(maxBufferSize, this.reader.Quotas);
			}
			return base.OnCreateBufferedCopy(maxBufferSize, XmlDictionaryReaderQuotas.Max);
		}

		// Token: 0x06006223 RID: 25123 RVA: 0x0016D674 File Offset: 0x0016B874
		protected override string OnGetBodyAttribute(string localName, string ns)
		{
			if (this.bodyAttributes != null)
			{
				return XmlAttributeHolder.GetAttribute(this.bodyAttributes, localName, ns);
			}
			string attribute;
			using (XmlDictionaryReader bufferedReaderAtBody = this.GetBufferedReaderAtBody())
			{
				attribute = bufferedReaderAtBody.GetAttribute(localName, ns);
			}
			return attribute;
		}

		// Token: 0x040038F0 RID: 14576
		private MessageHeaders headers;

		// Token: 0x040038F1 RID: 14577
		private MessageProperties properties;

		// Token: 0x040038F2 RID: 14578
		private IBufferedMessageData messageData;

		// Token: 0x040038F3 RID: 14579
		private RecycledMessageState recycledMessageState;

		// Token: 0x040038F4 RID: 14580
		private XmlDictionaryReader reader;

		// Token: 0x040038F5 RID: 14581
		private XmlAttributeHolder[] bodyAttributes;
	}
}
