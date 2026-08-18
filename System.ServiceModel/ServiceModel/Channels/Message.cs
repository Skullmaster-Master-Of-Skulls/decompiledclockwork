using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B7 RID: 2487
	[__DynamicallyInvokable]
	public abstract class Message : IDisposable
	{
		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06006185 RID: 24965
		[__DynamicallyInvokable]
		public abstract MessageHeaders Headers { [__DynamicallyInvokable] get; }

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06006186 RID: 24966 RVA: 0x0016B6A8 File Offset: 0x001698A8
		[__DynamicallyInvokable]
		protected bool IsDisposed
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state == MessageState.Closed;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06006187 RID: 24967 RVA: 0x0016B6B3 File Offset: 0x001698B3
		[__DynamicallyInvokable]
		public virtual bool IsFault
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
				}
				return false;
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06006188 RID: 24968 RVA: 0x0016B6CB File Offset: 0x001698CB
		[__DynamicallyInvokable]
		public virtual bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
				}
				return false;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06006189 RID: 24969
		[__DynamicallyInvokable]
		public abstract MessageProperties Properties { [__DynamicallyInvokable] get; }

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x0600618A RID: 24970
		[__DynamicallyInvokable]
		public abstract MessageVersion Version { [__DynamicallyInvokable] get; }

		// Token: 0x0600618B RID: 24971 RVA: 0x0016B6E4 File Offset: 0x001698E4
		internal virtual void SetProperty(string name, object value)
		{
			MessageProperties properties = this.Properties;
			if (properties != null)
			{
				properties[name] = value;
			}
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x0016B704 File Offset: 0x00169904
		internal virtual bool GetProperty(string name, out object result)
		{
			MessageProperties properties = this.Properties;
			if (properties != null)
			{
				return properties.TryGetValue(name, out result);
			}
			result = null;
			return false;
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x0600618D RID: 24973 RVA: 0x0016B728 File Offset: 0x00169928
		internal virtual RecycledMessageState RecycledMessageState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x0600618E RID: 24974 RVA: 0x0016B72B File Offset: 0x0016992B
		[__DynamicallyInvokable]
		public MessageState State
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x0016B733 File Offset: 0x00169933
		internal void BodyToString(XmlDictionaryWriter writer)
		{
			this.OnBodyToString(writer);
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x0016B73C File Offset: 0x0016993C
		[__DynamicallyInvokable]
		public void Close()
		{
			if (this.state != MessageState.Closed)
			{
				this.state = MessageState.Closed;
				this.OnClose();
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524304, SR.GetString("TraceCodeMessageClosed"), this);
					return;
				}
			}
			else if (DiagnosticUtility.ShouldTraceVerbose)
			{
				TraceUtility.TraceEvent(TraceEventType.Verbose, 524305, SR.GetString("TraceCodeMessageClosedAgain"), this);
			}
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x0016B79C File Offset: 0x0016999C
		[__DynamicallyInvokable]
		public MessageBuffer CreateBufferedCopy(int maxBufferSize)
		{
			if (maxBufferSize < 0)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxBufferSize", maxBufferSize, SR.GetString("ValueMustBeNonNegative")), this);
			}
			switch (this.state)
			{
			case MessageState.Created:
				this.state = MessageState.Copied;
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524306, SR.GetString("TraceCodeMessageCopied"), this, this);
				}
				return this.OnCreateBufferedCopy(maxBufferSize);
			case MessageState.Read:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenRead")), this);
			case MessageState.Written:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenWritten")), this);
			case MessageState.Copied:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenCopied")), this);
			case MessageState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
			default:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidMessageState")), this);
			}
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x0016B885 File Offset: 0x00169A85
		private static Type GetObjectType(object value)
		{
			if (value != null)
			{
				return value.GetType();
			}
			return typeof(object);
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x0016B89B File Offset: 0x00169A9B
		[__DynamicallyInvokable]
		public static Message CreateMessage(MessageVersion version, string action, object body)
		{
			return Message.CreateMessage(version, action, body, DataContractSerializerDefaults.CreateSerializer(Message.GetObjectType(body), int.MaxValue));
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x0016B8B5 File Offset: 0x00169AB5
		[__DynamicallyInvokable]
		public static Message CreateMessage(MessageVersion version, string action, object body, XmlObjectSerializer serializer)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			return new BodyWriterMessage(version, action, new XmlObjectSerializerBodyWriter(body, serializer));
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x0016B8F5 File Offset: 0x00169AF5
		[__DynamicallyInvokable]
		public static Message CreateMessage(MessageVersion version, string action, XmlReader body)
		{
			return Message.CreateMessage(version, action, XmlDictionaryReader.CreateDictionaryReader(body));
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x0016B904 File Offset: 0x00169B04
		[__DynamicallyInvokable]
		public static Message CreateMessage(MessageVersion version, string action, XmlDictionaryReader body)
		{
			if (body == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("body");
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			return Message.CreateMessage(version, action, new XmlReaderBodyWriter(body, version.Envelope));
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x0016B93F File Offset: 0x00169B3F
		[__DynamicallyInvokable]
		public static Message CreateMessage(MessageVersion version, string action, BodyWriter body)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			if (body == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("body"));
			}
			return new BodyWriterMessage(version, action, body);
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0016B979 File Offset: 0x00169B79
		internal static Message CreateMessage(MessageVersion version, ActionHeader actionHeader, BodyWriter body)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			if (body == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("body"));
			}
			return new BodyWriterMessage(version, actionHeader, body);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x0016B9B3 File Offset: 0x00169BB3
		[__DynamicallyInvokable]
		public static Message CreateMessage(MessageVersion version, string action)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			return new BodyWriterMessage(version, action, EmptyBodyWriter.Value);
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x0016B9D9 File Offset: 0x00169BD9
		internal static Message CreateMessage(MessageVersion version, ActionHeader actionHeader)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			return new BodyWriterMessage(version, actionHeader, EmptyBodyWriter.Value);
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x0016B9FF File Offset: 0x00169BFF
		[__DynamicallyInvokable]
		public static Message CreateMessage(XmlReader envelopeReader, int maxSizeOfHeaders, MessageVersion version)
		{
			return Message.CreateMessage(XmlDictionaryReader.CreateDictionaryReader(envelopeReader), maxSizeOfHeaders, version);
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x0016BA10 File Offset: 0x00169C10
		[__DynamicallyInvokable]
		public static Message CreateMessage(XmlDictionaryReader envelopeReader, int maxSizeOfHeaders, MessageVersion version)
		{
			if (envelopeReader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("envelopeReader"));
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			return new StreamedMessage(envelopeReader, maxSizeOfHeaders, version);
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x0016BA58 File Offset: 0x00169C58
		public static Message CreateMessage(MessageVersion version, FaultCode faultCode, string reason, string action)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			if (faultCode == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("faultCode"));
			}
			if (reason == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reason"));
			}
			return Message.CreateMessage(version, MessageFault.CreateFault(faultCode, reason), action);
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x0016BABC File Offset: 0x00169CBC
		public static Message CreateMessage(MessageVersion version, FaultCode faultCode, string reason, object detail, string action)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			if (faultCode == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("faultCode"));
			}
			if (reason == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("reason"));
			}
			return Message.CreateMessage(version, MessageFault.CreateFault(faultCode, new FaultReason(reason), detail), action);
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x0016BB28 File Offset: 0x00169D28
		public static Message CreateMessage(MessageVersion version, MessageFault fault, string action)
		{
			if (fault == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("fault"));
			}
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			return new BodyWriterMessage(version, action, new FaultBodyWriter(fault, version.Envelope));
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x0016BB78 File Offset: 0x00169D78
		internal Exception CreateMessageDisposedException()
		{
			return new ObjectDisposedException("", SR.GetString("MessageClosed"));
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x0016BB8E File Offset: 0x00169D8E
		[__DynamicallyInvokable]
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x0016BB98 File Offset: 0x00169D98
		[__DynamicallyInvokable]
		public T GetBody<T>()
		{
			XmlDictionaryReader readerAtBodyContents = this.GetReaderAtBodyContents();
			return this.OnGetBody<T>(readerAtBodyContents);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x0016BBB3 File Offset: 0x00169DB3
		[__DynamicallyInvokable]
		protected virtual T OnGetBody<T>(XmlDictionaryReader reader)
		{
			return this.GetBodyCore<T>(reader, DataContractSerializerDefaults.CreateSerializer(typeof(T), int.MaxValue));
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x0016BBD0 File Offset: 0x00169DD0
		[__DynamicallyInvokable]
		public T GetBody<T>(XmlObjectSerializer serializer)
		{
			if (serializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("serializer"));
			}
			return this.GetBodyCore<T>(this.GetReaderAtBodyContents(), serializer);
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x0016BBF8 File Offset: 0x00169DF8
		private T GetBodyCore<T>(XmlDictionaryReader reader, XmlObjectSerializer serializer)
		{
			T result;
			try
			{
				result = (T)((object)serializer.ReadObject(reader));
				this.ReadFromBodyContentsToEnd(reader);
			}
			finally
			{
				if (reader != null)
				{
					((IDisposable)reader).Dispose();
				}
			}
			return result;
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x0016BC38 File Offset: 0x00169E38
		internal virtual XmlDictionaryReader GetReaderAtHeader()
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			this.WriteStartEnvelope(xmlDictionaryWriter);
			MessageHeaders headers = this.Headers;
			for (int i = 0; i < headers.Count; i++)
			{
				headers.WriteHeader(i, xmlDictionaryWriter);
			}
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.WriteEndElement();
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			XmlDictionaryReader reader = xmlBuffer.GetReader(0);
			reader.ReadStartElement();
			reader.MoveToStartElement();
			return reader;
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x0016BCB5 File Offset: 0x00169EB5
		[__DynamicallyInvokable]
		public XmlDictionaryReader GetReaderAtBodyContents()
		{
			this.EnsureReadMessageState();
			if (this.IsEmpty)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageIsEmpty")), this);
			}
			return this.OnGetReaderAtBodyContents();
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x0016BCE4 File Offset: 0x00169EE4
		internal void EnsureReadMessageState()
		{
			switch (this.state)
			{
			case MessageState.Created:
				this.state = MessageState.Read;
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524307, SR.GetString("TraceCodeMessageRead"), this);
					return;
				}
				return;
			case MessageState.Read:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenRead")), this);
			case MessageState.Written:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenWritten")), this);
			case MessageState.Copied:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenCopied")), this);
			case MessageState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
			default:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidMessageState")), this);
			}
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x0016BDA0 File Offset: 0x00169FA0
		internal SeekableMessageNavigator GetNavigator(bool navigateBody, int maxNodes)
		{
			if (this.IsDisposed)
			{
				throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
			}
			if (this.messageNavigator == null)
			{
				this.messageNavigator = new SeekableMessageNavigator(this, maxNodes, XmlSpace.Default, navigateBody, false);
			}
			else
			{
				this.messageNavigator.ForkNodeCount(maxNodes);
			}
			return this.messageNavigator;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x0016BDF0 File Offset: 0x00169FF0
		internal void InitializeReply(Message request)
		{
			UniqueId messageId = request.Headers.MessageId;
			if (messageId == null)
			{
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("RequestMessageDoesNotHaveAMessageID")), request);
			}
			this.Headers.RelatesTo = messageId;
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x0016BE34 File Offset: 0x0016A034
		internal static bool IsFaultStartElement(XmlDictionaryReader reader, EnvelopeVersion version)
		{
			return reader.IsStartElement(XD.MessageDictionary.Fault, version.DictionaryNamespace);
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x0016BE4C File Offset: 0x0016A04C
		[__DynamicallyInvokable]
		protected virtual void OnBodyToString(XmlDictionaryWriter writer)
		{
			writer.WriteString(SR.GetString("MessageBodyIsUnknown"));
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x0016BE5E File Offset: 0x0016A05E
		[__DynamicallyInvokable]
		protected virtual MessageBuffer OnCreateBufferedCopy(int maxBufferSize)
		{
			return this.OnCreateBufferedCopy(maxBufferSize, XmlDictionaryReaderQuotas.Max);
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x0016BE6C File Offset: 0x0016A06C
		internal MessageBuffer OnCreateBufferedCopy(int maxBufferSize, XmlDictionaryReaderQuotas quotas)
		{
			XmlBuffer xmlBuffer = new XmlBuffer(maxBufferSize);
			XmlDictionaryWriter writer = xmlBuffer.OpenSection(quotas);
			this.OnWriteMessage(writer);
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			return new DefaultMessageBuffer(this, xmlBuffer);
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x0016BEA2 File Offset: 0x0016A0A2
		[__DynamicallyInvokable]
		protected virtual void OnClose()
		{
		}

		// Token: 0x060061B0 RID: 25008 RVA: 0x0016BEA4 File Offset: 0x0016A0A4
		[__DynamicallyInvokable]
		protected virtual XmlDictionaryReader OnGetReaderAtBodyContents()
		{
			XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
			XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(XmlDictionaryReaderQuotas.Max);
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				this.OnWriteStartEnvelope(xmlDictionaryWriter);
				this.OnWriteStartBody(xmlDictionaryWriter);
			}
			this.OnWriteBodyContents(xmlDictionaryWriter);
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				xmlDictionaryWriter.WriteEndElement();
				xmlDictionaryWriter.WriteEndElement();
			}
			xmlBuffer.CloseSection();
			xmlBuffer.Close();
			XmlDictionaryReader reader = xmlBuffer.GetReader(0);
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				reader.ReadStartElement();
				reader.ReadStartElement();
			}
			reader.MoveToContent();
			return reader;
		}

		// Token: 0x060061B1 RID: 25009 RVA: 0x0016BF48 File Offset: 0x0016A148
		[__DynamicallyInvokable]
		protected virtual void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			MessageDictionary messageDictionary = XD.MessageDictionary;
			writer.WriteStartElement(messageDictionary.Prefix.Value, messageDictionary.Body, this.Version.Envelope.DictionaryNamespace);
		}

		// Token: 0x060061B2 RID: 25010 RVA: 0x0016BF82 File Offset: 0x0016A182
		[__DynamicallyInvokable]
		public void WriteBodyContents(XmlDictionaryWriter writer)
		{
			this.EnsureWriteMessageState(writer);
			this.OnWriteBodyContents(writer);
		}

		// Token: 0x060061B3 RID: 25011 RVA: 0x0016BF92 File Offset: 0x0016A192
		public IAsyncResult BeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			this.EnsureWriteMessageState(writer);
			return this.OnBeginWriteBodyContents(writer, callback, state);
		}

		// Token: 0x060061B4 RID: 25012 RVA: 0x0016BFA4 File Offset: 0x0016A1A4
		public void EndWriteBodyContents(IAsyncResult result)
		{
			this.OnEndWriteBodyContents(result);
		}

		// Token: 0x060061B5 RID: 25013
		[__DynamicallyInvokable]
		protected abstract void OnWriteBodyContents(XmlDictionaryWriter writer);

		// Token: 0x060061B6 RID: 25014 RVA: 0x0016BFAD File Offset: 0x0016A1AD
		protected virtual IAsyncResult OnBeginWriteBodyContents(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			return new Message.OnWriteBodyContentsAsyncResult(writer, this, callback, state);
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x0016BFB8 File Offset: 0x0016A1B8
		protected virtual void OnEndWriteBodyContents(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x0016BFC0 File Offset: 0x0016A1C0
		[__DynamicallyInvokable]
		public void WriteStartEnvelope(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("writer"), this);
			}
			this.OnWriteStartEnvelope(writer);
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x0016BFE0 File Offset: 0x0016A1E0
		[__DynamicallyInvokable]
		protected virtual void OnWriteStartEnvelope(XmlDictionaryWriter writer)
		{
			EnvelopeVersion envelope = this.Version.Envelope;
			if (envelope != EnvelopeVersion.None)
			{
				MessageDictionary messageDictionary = XD.MessageDictionary;
				writer.WriteStartElement(messageDictionary.Prefix.Value, messageDictionary.Envelope, envelope.DictionaryNamespace);
				this.WriteSharedHeaderPrefixes(writer);
			}
		}

		// Token: 0x060061BA RID: 25018 RVA: 0x0016C02C File Offset: 0x0016A22C
		[__DynamicallyInvokable]
		protected virtual void OnWriteStartHeaders(XmlDictionaryWriter writer)
		{
			EnvelopeVersion envelope = this.Version.Envelope;
			if (envelope != EnvelopeVersion.None)
			{
				MessageDictionary messageDictionary = XD.MessageDictionary;
				writer.WriteStartElement(messageDictionary.Prefix.Value, messageDictionary.Header, envelope.DictionaryNamespace);
			}
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x0016C070 File Offset: 0x0016A270
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.IsDisposed)
			{
				return base.ToString();
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(new EncodingFallbackAwareXmlTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			string result;
			try
			{
				this.ToString(xmlDictionaryWriter);
				xmlDictionaryWriter.Flush();
				result = stringWriter.ToString();
			}
			catch (XmlException ex)
			{
				result = SR.GetString("MessageBodyToStringError", new object[]
				{
					ex.GetType().ToString(),
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x0016C104 File Offset: 0x0016A304
		internal void ToString(XmlDictionaryWriter writer)
		{
			if (this.IsDisposed)
			{
				throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
			}
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				this.WriteStartEnvelope(writer);
				this.WriteStartHeaders(writer);
				MessageHeaders headers = this.Headers;
				for (int i = 0; i < headers.Count; i++)
				{
					headers.WriteHeader(i, writer);
				}
				writer.WriteEndElement();
				MessageDictionary messageDictionary = XD.MessageDictionary;
				this.WriteStartBody(writer);
			}
			this.BodyToString(writer);
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x0016C1A0 File Offset: 0x0016A3A0
		[__DynamicallyInvokable]
		public string GetBodyAttribute(string localName, string ns)
		{
			if (localName == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("localName"), this);
			}
			if (ns == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("ns"), this);
			}
			switch (this.state)
			{
			case MessageState.Created:
				return this.OnGetBodyAttribute(localName, ns);
			case MessageState.Read:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenRead")), this);
			case MessageState.Written:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenWritten")), this);
			case MessageState.Copied:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenCopied")), this);
			case MessageState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
			default:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidMessageState")), this);
			}
		}

		// Token: 0x060061BE RID: 25022 RVA: 0x0016C265 File Offset: 0x0016A465
		[__DynamicallyInvokable]
		protected virtual string OnGetBodyAttribute(string localName, string ns)
		{
			return null;
		}

		// Token: 0x060061BF RID: 25023 RVA: 0x0016C268 File Offset: 0x0016A468
		internal void ReadFromBodyContentsToEnd(XmlDictionaryReader reader)
		{
			Message.ReadFromBodyContentsToEnd(reader, this.Version.Envelope);
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x0016C27B File Offset: 0x0016A47B
		private static void ReadFromBodyContentsToEnd(XmlDictionaryReader reader, EnvelopeVersion envelopeVersion)
		{
			if (envelopeVersion != EnvelopeVersion.None)
			{
				reader.ReadEndElement();
				reader.ReadEndElement();
			}
			reader.MoveToContent();
		}

		// Token: 0x060061C1 RID: 25025 RVA: 0x0016C298 File Offset: 0x0016A498
		internal static bool ReadStartBody(XmlDictionaryReader reader, EnvelopeVersion envelopeVersion, out bool isFault, out bool isEmpty)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				isEmpty = true;
				isFault = false;
				reader.ReadEndElement();
				return false;
			}
			reader.Read();
			if (reader.NodeType != XmlNodeType.Element)
			{
				reader.MoveToContent();
			}
			if (reader.NodeType == XmlNodeType.Element)
			{
				isFault = Message.IsFaultStartElement(reader, envelopeVersion);
				isEmpty = false;
			}
			else
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					isEmpty = true;
					isFault = false;
					Message.ReadFromBodyContentsToEnd(reader, envelopeVersion);
					return false;
				}
				isEmpty = false;
				isFault = false;
			}
			return true;
		}

		// Token: 0x060061C2 RID: 25026 RVA: 0x0016C310 File Offset: 0x0016A510
		[__DynamicallyInvokable]
		public void WriteBody(XmlWriter writer)
		{
			this.WriteBody(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x060061C3 RID: 25027 RVA: 0x0016C31E File Offset: 0x0016A51E
		[__DynamicallyInvokable]
		public void WriteBody(XmlDictionaryWriter writer)
		{
			this.WriteStartBody(writer);
			this.WriteBodyContents(writer);
			writer.WriteEndElement();
		}

		// Token: 0x060061C4 RID: 25028 RVA: 0x0016C334 File Offset: 0x0016A534
		[__DynamicallyInvokable]
		public void WriteStartBody(XmlWriter writer)
		{
			this.WriteStartBody(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x0016C342 File Offset: 0x0016A542
		[__DynamicallyInvokable]
		public void WriteStartBody(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("writer"), this);
			}
			this.OnWriteStartBody(writer);
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x0016C35F File Offset: 0x0016A55F
		internal void WriteStartHeaders(XmlDictionaryWriter writer)
		{
			this.OnWriteStartHeaders(writer);
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x0016C368 File Offset: 0x0016A568
		[__DynamicallyInvokable]
		public void WriteMessage(XmlWriter writer)
		{
			this.WriteMessage(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x0016C376 File Offset: 0x0016A576
		[__DynamicallyInvokable]
		public void WriteMessage(XmlDictionaryWriter writer)
		{
			this.EnsureWriteMessageState(writer);
			this.OnWriteMessage(writer);
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x0016C388 File Offset: 0x0016A588
		private void EnsureWriteMessageState(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw TraceUtility.ThrowHelperError(new ArgumentNullException("writer"), this);
			}
			switch (this.state)
			{
			case MessageState.Created:
				this.state = MessageState.Written;
				if (DiagnosticUtility.ShouldTraceVerbose)
				{
					TraceUtility.TraceEvent(TraceEventType.Verbose, 524308, SR.GetString("TraceCodeMessageWritten"), this);
					return;
				}
				return;
			case MessageState.Read:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenRead")), this);
			case MessageState.Written:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenWritten")), this);
			case MessageState.Copied:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageHasBeenCopied")), this);
			case MessageState.Closed:
				throw TraceUtility.ThrowHelperError(this.CreateMessageDisposedException(), this);
			default:
				throw TraceUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InvalidMessageState")), this);
			}
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x0016C457 File Offset: 0x0016A657
		public IAsyncResult BeginWriteMessage(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			this.EnsureWriteMessageState(writer);
			return this.OnBeginWriteMessage(writer, callback, state);
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x0016C469 File Offset: 0x0016A669
		public void EndWriteMessage(IAsyncResult result)
		{
			this.OnEndWriteMessage(result);
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x0016C472 File Offset: 0x0016A672
		[__DynamicallyInvokable]
		protected virtual void OnWriteMessage(XmlDictionaryWriter writer)
		{
			this.WriteMessagePreamble(writer);
			this.OnWriteBodyContents(writer);
			this.WriteMessagePostamble(writer);
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x0016C48C File Offset: 0x0016A68C
		internal void WriteMessagePreamble(XmlDictionaryWriter writer)
		{
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				this.OnWriteStartEnvelope(writer);
				MessageHeaders headers = this.Headers;
				int count = headers.Count;
				if (count > 0)
				{
					this.OnWriteStartHeaders(writer);
					for (int i = 0; i < count; i++)
					{
						headers.WriteHeader(i, writer);
					}
					writer.WriteEndElement();
				}
				this.OnWriteStartBody(writer);
			}
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x0016C4EC File Offset: 0x0016A6EC
		internal void WriteMessagePostamble(XmlDictionaryWriter writer)
		{
			if (this.Version.Envelope != EnvelopeVersion.None)
			{
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x0016C50C File Offset: 0x0016A70C
		protected virtual IAsyncResult OnBeginWriteMessage(XmlDictionaryWriter writer, AsyncCallback callback, object state)
		{
			return new Message.OnWriteMessageAsyncResult(writer, this, callback, state);
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x0016C517 File Offset: 0x0016A717
		protected virtual void OnEndWriteMessage(IAsyncResult result)
		{
			ScheduleActionItemAsyncResult.End(result);
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x0016C520 File Offset: 0x0016A720
		private void WriteSharedHeaderPrefixes(XmlDictionaryWriter writer)
		{
			MessageHeaders headers = this.Headers;
			int count = headers.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (this.Version.Addressing != AddressingVersion.None || !(headers[i].Namespace == AddressingVersion.None.Namespace))
				{
					IMessageHeaderWithSharedNamespace messageHeaderWithSharedNamespace = headers[i] as IMessageHeaderWithSharedNamespace;
					if (messageHeaderWithSharedNamespace != null)
					{
						XmlDictionaryString sharedPrefix = messageHeaderWithSharedNamespace.SharedPrefix;
						string value = sharedPrefix.Value;
						if (value.Length != 1)
						{
							throw TraceUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "IMessageHeaderWithSharedNamespace must use a single lowercase letter prefix.", new object[0])), this);
						}
						int num2 = (int)(value[0] - 'a');
						if (num2 < 0 || num2 >= 26)
						{
							throw TraceUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "IMessageHeaderWithSharedNamespace must use a single lowercase letter prefix.", new object[0])), this);
						}
						int num3 = 1 << num2;
						if ((num & num3) == 0)
						{
							writer.WriteXmlnsAttribute(value, messageHeaderWithSharedNamespace.SharedNamespace);
							num |= num3;
						}
					}
				}
			}
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x0016C62F File Offset: 0x0016A82F
		[__DynamicallyInvokable]
		protected Message()
		{
		}

		// Token: 0x040038D7 RID: 14551
		private MessageState state;

		// Token: 0x040038D8 RID: 14552
		private SeekableMessageNavigator messageNavigator;

		// Token: 0x040038D9 RID: 14553
		internal const int InitialBufferSize = 1024;

		// Token: 0x02000E43 RID: 3651
		private class OnWriteBodyContentsAsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x060082C3 RID: 33475 RVA: 0x001E3890 File Offset: 0x001E1A90
			public OnWriteBodyContentsAsyncResult(XmlDictionaryWriter writer, Message message, AsyncCallback callback, object state) : base(callback, state)
			{
				this.message = message;
				this.writer = writer;
				base.Schedule();
			}

			// Token: 0x060082C4 RID: 33476 RVA: 0x001E38AF File Offset: 0x001E1AAF
			protected override void OnDoWork()
			{
				this.message.OnWriteBodyContents(this.writer);
			}

			// Token: 0x04004A3C RID: 19004
			private Message message;

			// Token: 0x04004A3D RID: 19005
			private XmlDictionaryWriter writer;
		}

		// Token: 0x02000E44 RID: 3652
		private class OnWriteMessageAsyncResult : ScheduleActionItemAsyncResult
		{
			// Token: 0x060082C5 RID: 33477 RVA: 0x001E38C2 File Offset: 0x001E1AC2
			public OnWriteMessageAsyncResult(XmlDictionaryWriter writer, Message message, AsyncCallback callback, object state) : base(callback, state)
			{
				this.message = message;
				this.writer = writer;
				base.Schedule();
			}

			// Token: 0x060082C6 RID: 33478 RVA: 0x001E38E1 File Offset: 0x001E1AE1
			protected override void OnDoWork()
			{
				this.message.OnWriteMessage(this.writer);
			}

			// Token: 0x04004A3E RID: 19006
			private Message message;

			// Token: 0x04004A3F RID: 19007
			private XmlDictionaryWriter writer;
		}
	}
}
