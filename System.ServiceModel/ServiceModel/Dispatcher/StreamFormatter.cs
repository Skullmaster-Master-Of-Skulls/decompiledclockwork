using System;
using System.IO;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005B0 RID: 1456
	internal class StreamFormatter
	{
		// Token: 0x060038D5 RID: 14549 RVA: 0x000DC25C File Offset: 0x000DA45C
		internal static StreamFormatter Create(MessageDescription messageDescription, string operationName, bool isRequest)
		{
			MessagePartDescription messagePartDescription = StreamFormatter.ValidateAndGetStreamPart(messageDescription, isRequest, operationName);
			if (messagePartDescription == null)
			{
				return null;
			}
			return new StreamFormatter(messageDescription, messagePartDescription, operationName, isRequest);
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000DC280 File Offset: 0x000DA480
		private StreamFormatter(MessageDescription messageDescription, MessagePartDescription streamPart, string operationName, bool isRequest)
		{
			if (streamPart == messageDescription.Body.ReturnValue)
			{
				this.streamIndex = -1;
			}
			else
			{
				this.streamIndex = streamPart.Index;
			}
			this.wrapperName = messageDescription.Body.WrapperName;
			this.wrapperNS = messageDescription.Body.WrapperNamespace;
			this.partName = streamPart.Name;
			this.partNS = streamPart.Namespace;
			this.isRequest = isRequest;
			this.operationName = operationName;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000DC300 File Offset: 0x000DA500
		internal void Serialize(XmlDictionaryWriter writer, object[] parameters, object returnValue)
		{
			Stream streamAndWriteStartWrapperIfNecessary = this.GetStreamAndWriteStartWrapperIfNecessary(writer, parameters, returnValue);
			writer.WriteValue(new StreamFormatter.OperationStreamProvider(streamAndWriteStartWrapperIfNecessary));
			this.WriteEndWrapperIfNecessary(writer);
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000DC32C File Offset: 0x000DA52C
		private Stream GetStreamAndWriteStartWrapperIfNecessary(XmlDictionaryWriter writer, object[] parameters, object returnValue)
		{
			Stream streamValue = this.GetStreamValue(parameters, returnValue);
			if (streamValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(this.partName);
			}
			if (this.WrapperName != null)
			{
				writer.WriteStartElement(this.WrapperName, this.WrapperNamespace);
			}
			writer.WriteStartElement(this.PartName, this.PartNamespace);
			return streamValue;
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000DC383 File Offset: 0x000DA583
		private void WriteEndWrapperIfNecessary(XmlDictionaryWriter writer)
		{
			writer.WriteEndElement();
			if (this.wrapperName != null)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000DC399 File Offset: 0x000DA599
		internal IAsyncResult BeginSerialize(XmlDictionaryWriter writer, object[] parameters, object returnValue, AsyncCallback callback, object state)
		{
			return new StreamFormatter.SerializeAsyncResult(this, writer, parameters, returnValue, callback, state);
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000DC3A8 File Offset: 0x000DA5A8
		public void EndSerialize(IAsyncResult result)
		{
			StreamFormatter.SerializeAsyncResult.End(result);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000DC3B0 File Offset: 0x000DA5B0
		internal void Deserialize(object[] parameters, ref object retVal, Message message)
		{
			this.SetStreamValue(parameters, ref retVal, new StreamFormatter.MessageBodyStream(message, this.WrapperName, this.WrapperNamespace, this.PartName, this.PartNamespace, this.isRequest));
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000DC3E9 File Offset: 0x000DA5E9
		// (set) Token: 0x060038DE RID: 14558 RVA: 0x000DC3F1 File Offset: 0x000DA5F1
		internal string WrapperName
		{
			get
			{
				return this.wrapperName;
			}
			set
			{
				this.wrapperName = value;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000DC3FA File Offset: 0x000DA5FA
		// (set) Token: 0x060038E0 RID: 14560 RVA: 0x000DC402 File Offset: 0x000DA602
		internal string WrapperNamespace
		{
			get
			{
				return this.wrapperNS;
			}
			set
			{
				this.wrapperNS = value;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000DC40B File Offset: 0x000DA60B
		internal string PartName
		{
			get
			{
				return this.partName;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000DC413 File Offset: 0x000DA613
		internal string PartNamespace
		{
			get
			{
				return this.partNS;
			}
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000DC41B File Offset: 0x000DA61B
		private Stream GetStreamValue(object[] parameters, object returnValue)
		{
			if (this.streamIndex == -1)
			{
				return (Stream)returnValue;
			}
			return (Stream)parameters[this.streamIndex];
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000DC43A File Offset: 0x000DA63A
		private void SetStreamValue(object[] parameters, ref object returnValue, Stream streamValue)
		{
			if (this.streamIndex == -1)
			{
				returnValue = streamValue;
				return;
			}
			parameters[this.streamIndex] = streamValue;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000DC454 File Offset: 0x000DA654
		private static MessagePartDescription ValidateAndGetStreamPart(MessageDescription messageDescription, bool isRequest, string operationName)
		{
			MessagePartDescription streamPart = StreamFormatter.GetStreamPart(messageDescription);
			if (streamPart != null)
			{
				return streamPart;
			}
			if (!StreamFormatter.HasStream(messageDescription))
			{
				return null;
			}
			if (messageDescription.IsTypedMessage)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidStreamInTypedMessage", new object[]
				{
					messageDescription.MessageName
				})));
			}
			if (isRequest)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidStreamInRequest", new object[]
				{
					operationName
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidStreamInResponse", new object[]
			{
				operationName
			})));
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000DC4F4 File Offset: 0x000DA6F4
		private static bool HasStream(MessageDescription messageDescription)
		{
			if (messageDescription.Body.ReturnValue != null && messageDescription.Body.ReturnValue.Type == typeof(Stream))
			{
				return true;
			}
			foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
			{
				if (messagePartDescription.Type == typeof(Stream))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000DC590 File Offset: 0x000DA790
		private static MessagePartDescription GetStreamPart(MessageDescription messageDescription)
		{
			if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
			{
				if (messageDescription.Body.Parts.Count == 0 && messageDescription.Body.ReturnValue.Type == typeof(Stream))
				{
					return messageDescription.Body.ReturnValue;
				}
			}
			else if (messageDescription.Body.Parts.Count == 1 && messageDescription.Body.Parts[0].Type == typeof(Stream))
			{
				return messageDescription.Body.Parts[0];
			}
			return null;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000DC63B File Offset: 0x000DA83B
		internal static bool IsStream(MessageDescription messageDescription)
		{
			return StreamFormatter.GetStreamPart(messageDescription) != null;
		}

		// Token: 0x040029B9 RID: 10681
		private string wrapperName;

		// Token: 0x040029BA RID: 10682
		private string wrapperNS;

		// Token: 0x040029BB RID: 10683
		private string partName;

		// Token: 0x040029BC RID: 10684
		private string partNS;

		// Token: 0x040029BD RID: 10685
		private int streamIndex;

		// Token: 0x040029BE RID: 10686
		private bool isRequest;

		// Token: 0x040029BF RID: 10687
		private string operationName;

		// Token: 0x040029C0 RID: 10688
		private const int returnValueIndex = -1;

		// Token: 0x02000CB0 RID: 3248
		private class SerializeAsyncResult : AsyncResult
		{
			// Token: 0x06007951 RID: 31057 RVA: 0x001C4F24 File Offset: 0x001C3124
			internal SerializeAsyncResult(StreamFormatter streamFormatter, XmlDictionaryWriter writer, object[] parameters, object returnValue, AsyncCallback callback, object state) : base(callback, state)
			{
				this.streamFormatter = streamFormatter;
				this.writer = writer;
				Stream streamAndWriteStartWrapperIfNecessary = streamFormatter.GetStreamAndWriteStartWrapperIfNecessary(writer, parameters, returnValue);
				IAsyncResult result = writer.WriteValueAsync(new StreamFormatter.OperationStreamProvider(streamAndWriteStartWrapperIfNecessary)).AsAsyncResult(base.PrepareAsyncCompletion(StreamFormatter.SerializeAsyncResult.handleEndSerialize), this);
				bool flag = base.SyncContinue(result);
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007952 RID: 31058 RVA: 0x001C4F88 File Offset: 0x001C3188
			private static bool HandleEndSerialize(IAsyncResult result)
			{
				StreamFormatter.SerializeAsyncResult serializeAsyncResult = (StreamFormatter.SerializeAsyncResult)result.AsyncState;
				serializeAsyncResult.streamFormatter.WriteEndWrapperIfNecessary(serializeAsyncResult.writer);
				return true;
			}

			// Token: 0x06007953 RID: 31059 RVA: 0x001C4FB3 File Offset: 0x001C31B3
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<StreamFormatter.SerializeAsyncResult>(result);
			}

			// Token: 0x0400452A RID: 17706
			private static AsyncResult.AsyncCompletion handleEndSerialize = new AsyncResult.AsyncCompletion(StreamFormatter.SerializeAsyncResult.HandleEndSerialize);

			// Token: 0x0400452B RID: 17707
			private StreamFormatter streamFormatter;

			// Token: 0x0400452C RID: 17708
			private XmlDictionaryWriter writer;
		}

		// Token: 0x02000CB1 RID: 3249
		internal class MessageBodyStream : Stream
		{
			// Token: 0x06007955 RID: 31061 RVA: 0x001C4FCF File Offset: 0x001C31CF
			internal MessageBodyStream(Message message, string wrapperName, string wrapperNs, string elementName, string elementNs, bool isRequest)
			{
				this.message = message;
				this.position = 0L;
				this.wrapperName = wrapperName;
				this.wrapperNs = wrapperNs;
				this.elementName = elementName;
				this.elementNs = elementNs;
				this.isRequest = isRequest;
			}

			// Token: 0x06007956 RID: 31062 RVA: 0x001C500C File Offset: 0x001C320C
			public override int Read(byte[] buffer, int offset, int count)
			{
				this.EnsureStreamIsOpen();
				if (buffer == null)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentNullException("buffer"), this.message);
				}
				if (offset < 0)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", offset, SR.GetString("ValueMustBeNonNegative")), this.message);
				}
				if (count < 0)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", count, SR.GetString("ValueMustBeNonNegative")), this.message);
				}
				if (buffer.Length - offset < count)
				{
					throw TraceUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxInvalidStreamOffsetLength", new object[]
					{
						offset + count
					})), this.message);
				}
				int result;
				try
				{
					if (this.reader == null)
					{
						this.reader = this.message.GetReaderAtBodyContents();
						if (this.wrapperName != null)
						{
							this.reader.MoveToContent();
							this.reader.ReadStartElement(this.wrapperName, this.wrapperNs);
						}
						this.reader.MoveToContent();
						if (this.reader.NodeType == XmlNodeType.EndElement)
						{
							return 0;
						}
						this.reader.ReadStartElement(this.elementName, this.elementNs);
					}
					if (this.reader.MoveToContent() != XmlNodeType.Text)
					{
						StreamFormatter.MessageBodyStream.Exhaust(this.reader);
						result = 0;
					}
					else
					{
						int num = this.reader.ReadContentAsBase64(buffer, offset, count);
						this.position += (long)num;
						if (num == 0)
						{
							StreamFormatter.MessageBodyStream.Exhaust(this.reader);
						}
						result = num;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new IOException(SR.GetString("SFxStreamIOException"), ex));
				}
				return result;
			}

			// Token: 0x06007957 RID: 31063 RVA: 0x001C51C4 File Offset: 0x001C33C4
			private void EnsureStreamIsOpen()
			{
				if (this.message.State == MessageState.Closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(SR.GetString(this.isRequest ? "SFxStreamRequestMessageClosed" : "SFxStreamResponseMessageClosed")));
				}
			}

			// Token: 0x06007958 RID: 31064 RVA: 0x001C51FD File Offset: 0x001C33FD
			private static void Exhaust(XmlDictionaryReader reader)
			{
				if (reader != null)
				{
					while (reader.Read())
					{
					}
				}
			}

			// Token: 0x17001B90 RID: 7056
			// (get) Token: 0x06007959 RID: 31065 RVA: 0x001C520A File Offset: 0x001C340A
			// (set) Token: 0x0600795A RID: 31066 RVA: 0x001C5218 File Offset: 0x001C3418
			public override long Position
			{
				get
				{
					this.EnsureStreamIsOpen();
					return this.position;
				}
				set
				{
					throw TraceUtility.ThrowHelperError(new NotSupportedException(), this.message);
				}
			}

			// Token: 0x0600795B RID: 31067 RVA: 0x001C522A File Offset: 0x001C342A
			public override void Close()
			{
				this.message.Close();
				if (this.reader != null)
				{
					this.reader.Close();
					this.reader = null;
				}
				base.Close();
			}

			// Token: 0x17001B91 RID: 7057
			// (get) Token: 0x0600795C RID: 31068 RVA: 0x001C5257 File Offset: 0x001C3457
			public override bool CanRead
			{
				get
				{
					return this.message.State != MessageState.Closed;
				}
			}

			// Token: 0x17001B92 RID: 7058
			// (get) Token: 0x0600795D RID: 31069 RVA: 0x001C526A File Offset: 0x001C346A
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001B93 RID: 7059
			// (get) Token: 0x0600795E RID: 31070 RVA: 0x001C526D File Offset: 0x001C346D
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001B94 RID: 7060
			// (get) Token: 0x0600795F RID: 31071 RVA: 0x001C5270 File Offset: 0x001C3470
			public override long Length
			{
				get
				{
					throw TraceUtility.ThrowHelperError(new NotSupportedException(), this.message);
				}
			}

			// Token: 0x06007960 RID: 31072 RVA: 0x001C5282 File Offset: 0x001C3482
			public override void Flush()
			{
				throw TraceUtility.ThrowHelperError(new NotSupportedException(), this.message);
			}

			// Token: 0x06007961 RID: 31073 RVA: 0x001C5294 File Offset: 0x001C3494
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw TraceUtility.ThrowHelperError(new NotSupportedException(), this.message);
			}

			// Token: 0x06007962 RID: 31074 RVA: 0x001C52A6 File Offset: 0x001C34A6
			public override void SetLength(long value)
			{
				throw TraceUtility.ThrowHelperError(new NotSupportedException(), this.message);
			}

			// Token: 0x06007963 RID: 31075 RVA: 0x001C52B8 File Offset: 0x001C34B8
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw TraceUtility.ThrowHelperError(new NotSupportedException(), this.message);
			}

			// Token: 0x0400452D RID: 17709
			private Message message;

			// Token: 0x0400452E RID: 17710
			private XmlDictionaryReader reader;

			// Token: 0x0400452F RID: 17711
			private long position;

			// Token: 0x04004530 RID: 17712
			private string wrapperName;

			// Token: 0x04004531 RID: 17713
			private string wrapperNs;

			// Token: 0x04004532 RID: 17714
			private string elementName;

			// Token: 0x04004533 RID: 17715
			private string elementNs;

			// Token: 0x04004534 RID: 17716
			private bool isRequest;
		}

		// Token: 0x02000CB2 RID: 3250
		private class OperationStreamProvider : IStreamProvider
		{
			// Token: 0x06007964 RID: 31076 RVA: 0x001C52CA File Offset: 0x001C34CA
			internal OperationStreamProvider(Stream stream)
			{
				this.stream = stream;
			}

			// Token: 0x06007965 RID: 31077 RVA: 0x001C52D9 File Offset: 0x001C34D9
			public Stream GetStream()
			{
				return this.stream;
			}

			// Token: 0x06007966 RID: 31078 RVA: 0x001C52E1 File Offset: 0x001C34E1
			public void ReleaseStream(Stream stream)
			{
			}

			// Token: 0x04004535 RID: 17717
			private Stream stream;
		}
	}
}
