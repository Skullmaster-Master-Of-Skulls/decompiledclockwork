using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200085F RID: 2143
	internal abstract class HttpInput
	{
		// Token: 0x06005065 RID: 20581 RVA: 0x001271B8 File Offset: 0x001253B8
		protected HttpInput(IHttpTransportFactorySettings settings, bool isRequest, bool enableChannelBinding)
		{
			this.settings = settings;
			this.bufferManager = settings.BufferManager;
			this.messageEncoder = settings.MessageEncoderFactory.Encoder;
			this.webException = null;
			this.isRequest = isRequest;
			this.inputStream = null;
			this.enableChannelBinding = enableChannelBinding;
			if (isRequest)
			{
				this.streamed = TransferModeHelper.IsRequestStreamed(settings.TransferMode);
				return;
			}
			this.streamed = TransferModeHelper.IsResponseStreamed(settings.TransferMode);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00127231 File Offset: 0x00125431
		internal static HttpInput CreateHttpInput(HttpWebRequest httpWebRequest, HttpWebResponse httpWebResponse, IHttpTransportFactorySettings settings, ChannelBinding channelBinding)
		{
			return new HttpInput.WebResponseHttpInput(httpWebRequest, httpWebResponse, settings, channelBinding);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0012723C File Offset: 0x0012543C
		protected void Abort()
		{
			this.Abort(HttpAbortReason.Aborted);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00127245 File Offset: 0x00125445
		public virtual void Abort(HttpAbortReason reason)
		{
			if (this.isDisposed)
			{
				return;
			}
			this.abortReason = reason;
			this.TraceRequestResponseAborted(reason);
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00127260 File Offset: 0x00125460
		private void TraceRequestResponseAborted(HttpAbortReason reason)
		{
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, this.isRequest ? 262157 : 262158, this.isRequest ? SR.GetString("TraceCodeHttpChannelRequestAborted") : SR.GetString("TraceCodeHttpChannelResponseAborted"));
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x001272AC File Offset: 0x001254AC
		// (set) Token: 0x0600506B RID: 20587 RVA: 0x001272B4 File Offset: 0x001254B4
		internal WebException WebException
		{
			get
			{
				return this.webException;
			}
			set
			{
				this.webException = value;
			}
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x001272C0 File Offset: 0x001254C0
		public Stream GetInputStream(bool throwOnError)
		{
			if (this.inputStream == null && (throwOnError || !this.errorGettingInputStream))
			{
				try
				{
					this.inputStream = this.GetInputStream();
					this.errorGettingInputStream = false;
				}
				catch (Exception exception)
				{
					this.errorGettingInputStream = true;
					if (throwOnError || Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				}
			}
			return this.inputStream;
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x0600506D RID: 20589
		public abstract long ContentLength { get; }

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x0600506E RID: 20590
		protected abstract string ContentTypeCore { get; }

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x0600506F RID: 20591
		protected abstract bool HasContent { get; }

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06005070 RID: 20592
		protected abstract string SoapActionHeader { get; }

		// Token: 0x06005071 RID: 20593
		protected abstract Stream GetInputStream();

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x0012732C File Offset: 0x0012552C
		protected virtual ChannelBinding ChannelBinding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06005073 RID: 20595 RVA: 0x00127330 File Offset: 0x00125530
		protected string ContentType
		{
			get
			{
				string contentTypeCore = this.ContentTypeCore;
				if (string.IsNullOrEmpty(contentTypeCore))
				{
					return "application/octet-stream";
				}
				return contentTypeCore;
			}
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00127354 File Offset: 0x00125554
		private void ThrowMaxReceivedMessageSizeExceeded()
		{
			if (TD.MaxReceivedMessageSizeExceededIsEnabled())
			{
				TD.MaxReceivedMessageSizeExceeded(SR.GetString("MaxReceivedMessageSizeExceeded", new object[]
				{
					this.settings.MaxReceivedMessageSize
				}));
			}
			if (this.isRequest)
			{
				this.ThrowHttpProtocolException(SR.GetString("MaxReceivedMessageSizeExceeded", new object[]
				{
					this.settings.MaxReceivedMessageSize
				}), HttpStatusCode.RequestEntityTooLarge);
				return;
			}
			string @string = SR.GetString("MaxReceivedMessageSizeExceeded", new object[]
			{
				this.settings.MaxReceivedMessageSize
			});
			Exception innerException = new QuotaExceededException(@string);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(@string, innerException));
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00127404 File Offset: 0x00125604
		private Message DecodeBufferedMessage(ArraySegment<byte> buffer, Stream inputStream)
		{
			Message result;
			try
			{
				if (this.ContentLength == -1L && (long)buffer.Count == this.settings.MaxReceivedMessageSize)
				{
					byte[] buffer2 = new byte[1];
					int num = inputStream.Read(buffer2, 0, 1);
					if (num > 0)
					{
						this.ThrowMaxReceivedMessageSizeExceeded();
					}
				}
				try
				{
					result = this.messageEncoder.ReadMessage(buffer, this.bufferManager, this.ContentType);
				}
				catch (XmlException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), innerException));
				}
			}
			finally
			{
				inputStream.Close();
			}
			return result;
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x001274A8 File Offset: 0x001256A8
		private Message ReadBufferedMessage(Stream inputStream)
		{
			ArraySegment<byte> messageBuffer = this.GetMessageBuffer();
			byte[] array = messageBuffer.Array;
			int num = 0;
			int i = messageBuffer.Count;
			while (i > 0)
			{
				int num2 = inputStream.Read(array, num, i);
				if (num2 == 0)
				{
					if (this.ContentLength != -1L)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("HttpContentLengthIncorrect")));
					}
					break;
				}
				else
				{
					i -= num2;
					num += num2;
				}
			}
			return this.DecodeBufferedMessage(new ArraySegment<byte>(array, 0, num), inputStream);
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00127520 File Offset: 0x00125720
		private Message ReadChunkedBufferedMessage(Stream inputStream)
		{
			Message result;
			try
			{
				result = this.messageEncoder.ReadMessage(inputStream, this.bufferManager, this.settings.MaxBufferSize, this.ContentType);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), innerException));
			}
			return result;
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x00127580 File Offset: 0x00125780
		private Message ReadStreamedMessage(Stream inputStream)
		{
			MaxMessageSizeStream stream = new MaxMessageSizeStream(inputStream, this.settings.MaxReceivedMessageSize);
			Message result;
			try
			{
				result = this.messageEncoder.ReadMessage(stream, this.settings.MaxBufferSize, this.ContentType);
			}
			catch (XmlException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), innerException));
			}
			return result;
		}

		// Token: 0x06005079 RID: 20601
		protected abstract void AddProperties(Message message);

		// Token: 0x0600507A RID: 20602 RVA: 0x001275EC File Offset: 0x001257EC
		private void ApplyChannelBinding(Message message)
		{
			if (this.enableChannelBinding)
			{
				ChannelBindingUtility.TryAddToMessage(this.ChannelBinding, message, true);
			}
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x00127604 File Offset: 0x00125804
		private Exception ProcessHttpAddressing(Message message)
		{
			Exception result = null;
			this.AddProperties(message);
			if (message.Version.Addressing == AddressingVersion.None)
			{
				bool flag = false;
				try
				{
					flag = (message.Headers.Action == null);
				}
				catch (XmlException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (CommunicationException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
				if (!flag)
				{
					result = new ProtocolException(SR.GetString("HttpAddressingNoneHeaderOnWire", new object[]
					{
						XD.AddressingDictionary.Action.Value
					}));
				}
				bool flag2 = false;
				try
				{
					flag2 = (message.Headers.To == null);
				}
				catch (XmlException exception3)
				{
					DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				}
				catch (CommunicationException exception4)
				{
					DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
				}
				if (!flag2)
				{
					result = new ProtocolException(SR.GetString("HttpAddressingNoneHeaderOnWire", new object[]
					{
						XD.AddressingDictionary.To.Value
					}));
				}
				message.Headers.To = message.Properties.Via;
			}
			if (this.isRequest)
			{
				string text = null;
				if (message.Version.Envelope == EnvelopeVersion.Soap11)
				{
					text = this.SoapActionHeader;
				}
				else if (message.Version.Envelope == EnvelopeVersion.Soap12 && !string.IsNullOrEmpty(this.ContentType))
				{
					ContentType contentType = new ContentType(this.ContentType);
					if (contentType.MediaType == "multipart/related" && contentType.Parameters.ContainsKey("start-info"))
					{
						text = new ContentType(contentType.Parameters["start-info"]).Parameters["action"];
					}
					if (text == null)
					{
						text = contentType.Parameters["action"];
					}
				}
				if (text != null)
				{
					text = UrlUtility.UrlDecode(text, Encoding.UTF8);
					if (text.Length >= 2 && text[0] == '"' && text[text.Length - 1] == '"')
					{
						text = text.Substring(1, text.Length - 2);
					}
					if (message.Version.Addressing == AddressingVersion.None)
					{
						message.Headers.Action = text;
					}
					try
					{
						if (text.Length > 0 && string.Compare(message.Headers.Action, text, StringComparison.Ordinal) != 0)
						{
							result = new ActionMismatchAddressingException(SR.GetString("HttpSoapActionMismatchFault", new object[]
							{
								message.Headers.Action,
								text
							}), message.Headers.Action, text);
						}
					}
					catch (XmlException exception5)
					{
						DiagnosticUtility.TraceHandledException(exception5, TraceEventType.Information);
					}
					catch (CommunicationException exception6)
					{
						DiagnosticUtility.TraceHandledException(exception6, TraceEventType.Information);
					}
				}
			}
			this.ApplyChannelBinding(message);
			if (DiagnosticUtility.ShouldUseActivity)
			{
				TraceUtility.TransferFromTransport(message);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord(message), this, null, message);
			}
			if (MessageLogger.LoggingEnabled && message.Version.Addressing == AddressingVersion.None)
			{
				MessageLogger.LogMessage(ref message, MessageLoggingSource.TransportReceive | MessageLoggingSource.LastChance);
			}
			return result;
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0012793C File Offset: 0x00125B3C
		private void ValidateContentType()
		{
			if (!this.HasContent)
			{
				return;
			}
			if (string.IsNullOrEmpty(this.ContentType))
			{
				if (MessageLogger.ShouldLogMalformed)
				{
					Stream stream = this.GetInputStream(false);
					if (stream != null)
					{
						MessageLogger.LogMessage(stream, MessageLoggingSource.Malformed);
					}
				}
				this.ThrowHttpProtocolException(SR.GetString("HttpContentTypeHeaderRequired"), HttpStatusCode.UnsupportedMediaType, "Missing Content Type");
			}
			if (!this.messageEncoder.IsContentTypeSupported(this.ContentType))
			{
				if (MessageLogger.ShouldLogMalformed)
				{
					Stream stream2 = this.GetInputStream(false);
					if (stream2 != null)
					{
						MessageLogger.LogMessage(stream2, MessageLoggingSource.Malformed);
					}
				}
				string statusDescription = string.Format(CultureInfo.InvariantCulture, "Cannot process the message because the content type '{0}' was not the expected type '{1}'.", new object[]
				{
					this.ContentType,
					this.messageEncoder.ContentType
				});
				this.ThrowHttpProtocolException(SR.GetString("ContentTypeMismatch", new object[]
				{
					this.ContentType,
					this.messageEncoder.ContentType
				}), HttpStatusCode.UnsupportedMediaType, statusDescription);
			}
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00127A26 File Offset: 0x00125C26
		public IAsyncResult BeginParseIncomingMessage(AsyncCallback callback, object state)
		{
			return this.BeginParseIncomingMessage(null, callback, state);
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x00127A34 File Offset: 0x00125C34
		public IAsyncResult BeginParseIncomingMessage(HttpRequestMessage httpRequestMessage, AsyncCallback callback, object state)
		{
			bool flag = true;
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = new HttpInput.ParseMessageAsyncResult(httpRequestMessage, this, callback, state);
				flag = false;
				result = asyncResult;
			}
			finally
			{
				if (flag)
				{
					this.Close();
				}
			}
			return result;
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x00127A70 File Offset: 0x00125C70
		public Message EndParseIncomingMessage(IAsyncResult result, out Exception requestException)
		{
			bool flag = true;
			Message result2;
			try
			{
				Message message = HttpInput.ParseMessageAsyncResult.End(result, out requestException);
				flag = false;
				result2 = message;
			}
			finally
			{
				if (flag)
				{
					this.Close();
				}
			}
			return result2;
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00127AA8 File Offset: 0x00125CA8
		public HttpRequestMessageHttpInput CreateHttpRequestMessageInput()
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
			if (this.HasContent)
			{
				httpRequestMessage.Content = new StreamContent(new MaxMessageSizeStream(this.GetInputStream(true), this.settings.MaxReceivedMessageSize));
			}
			HttpChannelUtilities.EnsureHttpRequestMessageContentNotNull(httpRequestMessage);
			this.ConfigureHttpRequestMessage(httpRequestMessage);
			ChannelBinding channelBinding = this.enableChannelBinding ? this.ChannelBinding : null;
			return new HttpRequestMessageHttpInput(httpRequestMessage, this.settings, this.enableChannelBinding, channelBinding);
		}

		// Token: 0x06005081 RID: 20609
		public abstract void ConfigureHttpRequestMessage(HttpRequestMessage message);

		// Token: 0x06005082 RID: 20610 RVA: 0x00127B17 File Offset: 0x00125D17
		public Message ParseIncomingMessage(out Exception requestException)
		{
			return this.ParseIncomingMessage(null, out requestException);
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x00127B24 File Offset: 0x00125D24
		public Message ParseIncomingMessage(HttpRequestMessage httpRequestMessage, out Exception requestException)
		{
			requestException = null;
			bool flag = true;
			Message result;
			try
			{
				this.ValidateContentType();
				ServiceModelActivity serviceModelActivity = null;
				if (DiagnosticUtility.ShouldUseActivity && (ServiceModelActivity.Current == null || ServiceModelActivity.Current.ActivityType != ActivityType.ProcessAction))
				{
					serviceModelActivity = ServiceModelActivity.CreateBoundedActivity(true);
				}
				using (serviceModelActivity)
				{
					if (DiagnosticUtility.ShouldUseActivity && serviceModelActivity != null)
					{
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessingMessage", new object[]
						{
							TraceUtility.RetrieveMessageNumber()
						}), ActivityType.ProcessMessage);
					}
					Message message;
					if (!this.HasContent)
					{
						if (this.messageEncoder.MessageVersion != MessageVersion.None)
						{
							return null;
						}
						message = new NullMessage();
					}
					else
					{
						Stream stream = this.GetInputStream(true);
						if (this.streamed)
						{
							message = this.ReadStreamedMessage(stream);
						}
						else if (this.ContentLength == -1L)
						{
							message = this.ReadChunkedBufferedMessage(stream);
						}
						else if (httpRequestMessage == null)
						{
							message = this.ReadBufferedMessage(stream);
						}
						else
						{
							message = this.ReadBufferedMessage(httpRequestMessage);
						}
					}
					requestException = this.ProcessHttpAddressing(message);
					flag = false;
					result = message;
				}
			}
			finally
			{
				if (flag)
				{
					this.Close();
				}
			}
			return result;
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x00127C44 File Offset: 0x00125E44
		private Message ReadBufferedMessage(HttpRequestMessage httpRequestMessage)
		{
			Message result;
			using (HttpContent content = httpRequestMessage.Content)
			{
				int num = (int)this.ContentLength;
				byte[] array = this.bufferManager.TakeBuffer(num);
				bool flag = false;
				try
				{
					MemoryStream stream = new MemoryStream(array);
					content.CopyToAsync(stream).Wait<CommunicationException>();
					httpRequestMessage.Content = new ByteArrayContent(array, 0, num);
					foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in content.Headers)
					{
						httpRequestMessage.Content.Headers.Add(keyValuePair.Key, keyValuePair.Value);
					}
					result = this.messageEncoder.ReadMessage(new ArraySegment<byte>(array, 0, num), this.bufferManager, this.ContentType);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.bufferManager.ReturnBuffer(array);
					}
				}
			}
			return result;
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00127D4C File Offset: 0x00125F4C
		private void ThrowHttpProtocolException(string message, HttpStatusCode statusCode)
		{
			this.ThrowHttpProtocolException(message, statusCode, null);
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00127D57 File Offset: 0x00125F57
		private void ThrowHttpProtocolException(string message, HttpStatusCode statusCode, string statusDescription)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpInput.CreateHttpProtocolException(message, statusCode, statusDescription, this.webException));
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x00127D74 File Offset: 0x00125F74
		internal static ProtocolException CreateHttpProtocolException(string message, HttpStatusCode statusCode, string statusDescription, Exception innerException)
		{
			ProtocolException ex = new ProtocolException(message, innerException);
			ex.Data.Add("System.ServiceModel.Channels.HttpInput.HttpStatusCode", statusCode);
			if (statusDescription != null && statusDescription.Length > 0)
			{
				ex.Data.Add("System.ServiceModel.Channels.HttpInput.HttpStatusDescription", statusDescription);
			}
			return ex;
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x00127DBD File Offset: 0x00125FBD
		protected virtual void Close()
		{
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x00127DC0 File Offset: 0x00125FC0
		private ArraySegment<byte> GetMessageBuffer()
		{
			long contentLength = this.ContentLength;
			if (contentLength > this.settings.MaxReceivedMessageSize)
			{
				this.ThrowMaxReceivedMessageSizeExceeded();
			}
			int num = (int)contentLength;
			return new ArraySegment<byte>(this.bufferManager.TakeBuffer(num), 0, num);
		}

		// Token: 0x040031BD RID: 12733
		private const string multipartRelatedMediaType = "multipart/related";

		// Token: 0x040031BE RID: 12734
		private const string startInfoHeaderParam = "start-info";

		// Token: 0x040031BF RID: 12735
		private const string defaultContentType = "application/octet-stream";

		// Token: 0x040031C0 RID: 12736
		private HttpAbortReason abortReason;

		// Token: 0x040031C1 RID: 12737
		private BufferManager bufferManager;

		// Token: 0x040031C2 RID: 12738
		private bool isDisposed;

		// Token: 0x040031C3 RID: 12739
		private bool isRequest;

		// Token: 0x040031C4 RID: 12740
		private MessageEncoder messageEncoder;

		// Token: 0x040031C5 RID: 12741
		private IHttpTransportFactorySettings settings;

		// Token: 0x040031C6 RID: 12742
		private bool streamed;

		// Token: 0x040031C7 RID: 12743
		private WebException webException;

		// Token: 0x040031C8 RID: 12744
		private Stream inputStream;

		// Token: 0x040031C9 RID: 12745
		private bool enableChannelBinding;

		// Token: 0x040031CA RID: 12746
		private bool errorGettingInputStream;

		// Token: 0x02000D40 RID: 3392
		private class ParseMessageAsyncResult : TraceAsyncResult
		{
			// Token: 0x06007C6C RID: 31852 RVA: 0x001D11C7 File Offset: 0x001CF3C7
			public ParseMessageAsyncResult(HttpRequestMessage httpRequestMessage, HttpInput httpInput, AsyncCallback callback, object state) : base(callback, state)
			{
				this.httpInput = httpInput;
				this.httpRequestMessage = httpRequestMessage;
				this.BeginParse();
			}

			// Token: 0x06007C6D RID: 31853 RVA: 0x001D11E8 File Offset: 0x001CF3E8
			private void BeginParse()
			{
				this.httpInput.ValidateContentType();
				this.inputStream = this.httpInput.GetInputStream(true);
				if (!this.httpInput.HasContent)
				{
					if (this.httpInput.messageEncoder.MessageVersion != MessageVersion.None)
					{
						base.Complete(true);
						return;
					}
					this.message = new NullMessage();
				}
				else if (this.httpInput.streamed || this.httpInput.ContentLength == -1L)
				{
					if (this.httpInput.streamed)
					{
						this.message = this.httpInput.ReadStreamedMessage(this.inputStream);
					}
					else
					{
						this.message = this.httpInput.ReadChunkedBufferedMessage(this.inputStream);
					}
				}
				if (this.message != null)
				{
					this.requestException = this.httpInput.ProcessHttpAddressing(this.message);
					base.Complete(true);
					return;
				}
				AsyncCompletionResult asyncCompletionResult;
				if (this.httpRequestMessage == null)
				{
					asyncCompletionResult = this.DecodeBufferedMessageAsync();
				}
				else
				{
					asyncCompletionResult = this.DecodeBufferedHttpRequestMessageAsync();
				}
				if (asyncCompletionResult == AsyncCompletionResult.Completed)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007C6E RID: 31854 RVA: 0x001D12F0 File Offset: 0x001CF4F0
			private AsyncCompletionResult DecodeBufferedMessageAsync()
			{
				this.buffer = this.httpInput.GetMessageBuffer();
				this.count = this.buffer.Count;
				this.offset = 0;
				IAsyncResult asyncResult = this.inputStream.BeginRead(this.buffer.Array, this.offset, this.count, HttpInput.ParseMessageAsyncResult.onRead, this);
				if (asyncResult.CompletedSynchronously && this.ContinueReading(this.inputStream.EndRead(asyncResult)))
				{
					return AsyncCompletionResult.Completed;
				}
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x06007C6F RID: 31855 RVA: 0x001D1370 File Offset: 0x001CF570
			private bool ContinueReading(int bytesRead)
			{
				while (bytesRead != 0)
				{
					this.offset += bytesRead;
					this.count -= bytesRead;
					if (this.count <= 0)
					{
						break;
					}
					IAsyncResult asyncResult = this.inputStream.BeginRead(this.buffer.Array, this.offset, this.count, HttpInput.ParseMessageAsyncResult.onRead, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					bytesRead = this.inputStream.EndRead(asyncResult);
				}
				bool result;
				using (DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.BoundOperation(base.CallbackActivity) : null)
				{
					using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity(true) : null)
					{
						if (DiagnosticUtility.ShouldUseActivity)
						{
							ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityProcessingMessage", new object[]
							{
								TraceUtility.RetrieveMessageNumber()
							}), ActivityType.ProcessMessage);
						}
						this.message = this.httpInput.DecodeBufferedMessage(new ArraySegment<byte>(this.buffer.Array, 0, this.offset), this.inputStream);
						this.requestException = this.httpInput.ProcessHttpAddressing(this.message);
					}
					result = true;
				}
				return result;
			}

			// Token: 0x06007C70 RID: 31856 RVA: 0x001D14B8 File Offset: 0x001CF6B8
			private static void OnRead(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpInput.ParseMessageAsyncResult parseMessageAsyncResult = (HttpInput.ParseMessageAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					flag = parseMessageAsyncResult.ContinueReading(parseMessageAsyncResult.inputStream.EndRead(result));
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					parseMessageAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007C71 RID: 31857 RVA: 0x001D1520 File Offset: 0x001CF720
			public static Message End(IAsyncResult result, out Exception requestException)
			{
				HttpInput.ParseMessageAsyncResult parseMessageAsyncResult = AsyncResult.End<HttpInput.ParseMessageAsyncResult>(result);
				requestException = parseMessageAsyncResult.requestException;
				return parseMessageAsyncResult.message;
			}

			// Token: 0x06007C72 RID: 31858 RVA: 0x001D1542 File Offset: 0x001CF742
			private AsyncCompletionResult DecodeBufferedHttpRequestMessageAsync()
			{
				this.message = this.httpInput.ReadBufferedMessage(this.httpRequestMessage);
				this.requestException = this.httpInput.ProcessHttpAddressing(this.message);
				return AsyncCompletionResult.Completed;
			}

			// Token: 0x04004783 RID: 18307
			private ArraySegment<byte> buffer;

			// Token: 0x04004784 RID: 18308
			private int count;

			// Token: 0x04004785 RID: 18309
			private int offset;

			// Token: 0x04004786 RID: 18310
			private HttpInput httpInput;

			// Token: 0x04004787 RID: 18311
			private Stream inputStream;

			// Token: 0x04004788 RID: 18312
			private Message message;

			// Token: 0x04004789 RID: 18313
			private Exception requestException;

			// Token: 0x0400478A RID: 18314
			private HttpRequestMessage httpRequestMessage;

			// Token: 0x0400478B RID: 18315
			private static AsyncCallback onRead = Fx.ThunkCallback(new AsyncCallback(HttpInput.ParseMessageAsyncResult.OnRead));
		}

		// Token: 0x02000D41 RID: 3393
		private class WebResponseHttpInput : HttpInput
		{
			// Token: 0x06007C74 RID: 31860 RVA: 0x001D158C File Offset: 0x001CF78C
			public WebResponseHttpInput(HttpWebRequest httpWebRequest, HttpWebResponse httpWebResponse, IHttpTransportFactorySettings settings, ChannelBinding channelBinding) : base(settings, false, channelBinding != null)
			{
				this.httpWebRequest = httpWebRequest;
				this.channelBinding = channelBinding;
				this.httpWebResponse = httpWebResponse;
				if (this.httpWebResponse.ContentLength == -1L)
				{
					this.preReadBuffer = new byte[1];
					if (this.httpWebResponse.GetResponseStream().Read(this.preReadBuffer, 0, 1) == 0)
					{
						this.preReadBuffer = null;
					}
				}
				this.hasContent = (this.preReadBuffer != null || this.httpWebResponse.ContentLength > 0L);
				if (!this.hasContent)
				{
					this.httpWebResponse.GetResponseStream().Close();
				}
			}

			// Token: 0x06007C75 RID: 31861 RVA: 0x001D1630 File Offset: 0x001CF830
			public override void Abort(HttpAbortReason abortReason)
			{
				this.httpWebRequest.Abort();
				base.Abort(abortReason);
			}

			// Token: 0x17001BD9 RID: 7129
			// (get) Token: 0x06007C76 RID: 31862 RVA: 0x001D1644 File Offset: 0x001CF844
			protected override ChannelBinding ChannelBinding
			{
				get
				{
					return this.channelBinding;
				}
			}

			// Token: 0x17001BDA RID: 7130
			// (get) Token: 0x06007C77 RID: 31863 RVA: 0x001D164C File Offset: 0x001CF84C
			public override long ContentLength
			{
				get
				{
					return this.httpWebResponse.ContentLength;
				}
			}

			// Token: 0x17001BDB RID: 7131
			// (get) Token: 0x06007C78 RID: 31864 RVA: 0x001D1659 File Offset: 0x001CF859
			protected override string ContentTypeCore
			{
				get
				{
					return this.httpWebResponse.ContentType;
				}
			}

			// Token: 0x17001BDC RID: 7132
			// (get) Token: 0x06007C79 RID: 31865 RVA: 0x001D1666 File Offset: 0x001CF866
			protected override bool HasContent
			{
				get
				{
					return this.hasContent;
				}
			}

			// Token: 0x17001BDD RID: 7133
			// (get) Token: 0x06007C7A RID: 31866 RVA: 0x001D166E File Offset: 0x001CF86E
			protected override string SoapActionHeader
			{
				get
				{
					return this.httpWebResponse.Headers["SOAPAction"];
				}
			}

			// Token: 0x06007C7B RID: 31867 RVA: 0x001D1688 File Offset: 0x001CF888
			protected override void AddProperties(Message message)
			{
				HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty(this.httpWebResponse.Headers);
				httpResponseMessageProperty.StatusCode = this.httpWebResponse.StatusCode;
				httpResponseMessageProperty.StatusDescription = this.httpWebResponse.StatusDescription;
				message.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
				message.Properties.Via = message.Version.Addressing.AnonymousUri;
			}

			// Token: 0x06007C7C RID: 31868 RVA: 0x001D16F4 File Offset: 0x001CF8F4
			public override void ConfigureHttpRequestMessage(HttpRequestMessage message)
			{
				throw FxTrace.Exception.AsError(new NotSupportedException());
			}

			// Token: 0x06007C7D RID: 31869 RVA: 0x001D1708 File Offset: 0x001CF908
			protected override void Close()
			{
				try
				{
					this.httpWebResponse.Close();
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				}
			}

			// Token: 0x06007C7E RID: 31870 RVA: 0x001D1748 File Offset: 0x001CF948
			protected override Stream GetInputStream()
			{
				if (this.preReadBuffer != null)
				{
					return new HttpInput.WebResponseHttpInput.WebResponseInputStream(this, this.httpWebResponse, this.preReadBuffer);
				}
				return new HttpInput.WebResponseHttpInput.WebResponseInputStream(this, this.httpWebResponse);
			}

			// Token: 0x0400478C RID: 18316
			private HttpWebRequest httpWebRequest;

			// Token: 0x0400478D RID: 18317
			private HttpWebResponse httpWebResponse;

			// Token: 0x0400478E RID: 18318
			private byte[] preReadBuffer;

			// Token: 0x0400478F RID: 18319
			private ChannelBinding channelBinding;

			// Token: 0x04004790 RID: 18320
			private bool hasContent;

			// Token: 0x02000F58 RID: 3928
			private class WebResponseInputStream : DetectEofStream
			{
				// Token: 0x0600874D RID: 34637 RVA: 0x001F6080 File Offset: 0x001F4280
				public WebResponseInputStream(HttpInput.WebResponseHttpInput parent, HttpWebResponse httpWebResponse) : base(httpWebResponse.GetResponseStream())
				{
					this.httpInput = parent;
					this.webResponse = httpWebResponse;
				}

				// Token: 0x0600874E RID: 34638 RVA: 0x001F609C File Offset: 0x001F429C
				public WebResponseInputStream(HttpInput.WebResponseHttpInput parent, HttpWebResponse httpWebResponse, byte[] prereadBuffer) : base(new PreReadStream(httpWebResponse.GetResponseStream(), prereadBuffer))
				{
					this.httpInput = parent;
					this.webResponse = httpWebResponse;
				}

				// Token: 0x0600874F RID: 34639 RVA: 0x001F60BE File Offset: 0x001F42BE
				public override void Close()
				{
					base.Close();
					this.CloseResponse();
				}

				// Token: 0x06008750 RID: 34640 RVA: 0x001F60CC File Offset: 0x001F42CC
				protected override void OnReceivedEof()
				{
					base.OnReceivedEof();
					this.CloseResponse();
				}

				// Token: 0x06008751 RID: 34641 RVA: 0x001F60DA File Offset: 0x001F42DA
				private void CloseResponse()
				{
					if (this.responseClosed)
					{
						return;
					}
					this.responseClosed = true;
					this.webResponse.Close();
				}

				// Token: 0x06008752 RID: 34642 RVA: 0x001F60F8 File Offset: 0x001F42F8
				public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				{
					IAsyncResult result;
					try
					{
						result = base.BaseStream.BeginRead(buffer, offset, Math.Min(count, 65536), callback, state);
					}
					catch (IOException ioException)
					{
						throw this.CreateResponseIOException(ioException);
					}
					catch (ObjectDisposedException ex)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex.Message, ex));
					}
					catch (WebException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateResponseWebException(webException, this.webResponse, this.httpInput.abortReason));
					}
					return result;
				}

				// Token: 0x06008753 RID: 34643 RVA: 0x001F6194 File Offset: 0x001F4394
				public override int EndRead(IAsyncResult result)
				{
					int result2;
					try
					{
						result2 = base.BaseStream.EndRead(result);
					}
					catch (IOException ioException)
					{
						throw this.CreateResponseIOException(ioException);
					}
					catch (ObjectDisposedException ex)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex.Message, ex));
					}
					catch (WebException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateResponseWebException(webException, this.webResponse, this.httpInput.abortReason));
					}
					return result2;
				}

				// Token: 0x06008754 RID: 34644 RVA: 0x001F6220 File Offset: 0x001F4420
				public override int Read(byte[] buffer, int offset, int count)
				{
					int result;
					try
					{
						result = base.BaseStream.Read(buffer, offset, Math.Min(count, 65536));
					}
					catch (ObjectDisposedException ex)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex.Message, ex));
					}
					catch (IOException ioException)
					{
						throw this.CreateResponseIOException(ioException);
					}
					catch (WebException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateResponseWebException(webException, this.webResponse, this.httpInput.abortReason));
					}
					return result;
				}

				// Token: 0x06008755 RID: 34645 RVA: 0x001F62B8 File Offset: 0x001F44B8
				public override int ReadByte()
				{
					int result;
					try
					{
						result = base.BaseStream.ReadByte();
					}
					catch (ObjectDisposedException ex)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex.Message, ex));
					}
					catch (IOException ioException)
					{
						throw this.CreateResponseIOException(ioException);
					}
					catch (WebException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateResponseWebException(webException, this.webResponse, this.httpInput.abortReason));
					}
					return result;
				}

				// Token: 0x06008756 RID: 34646 RVA: 0x001F6340 File Offset: 0x001F4540
				private Exception CreateResponseIOException(IOException ioException)
				{
					TimeSpan receiveTimeout = this.CanTimeout ? TimeoutHelper.FromMilliseconds(this.ReadTimeout) : TimeSpan.MaxValue;
					return DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateResponseIOException(ioException, receiveTimeout));
				}

				// Token: 0x04004EC6 RID: 20166
				private const int maxSocketRead = 65536;

				// Token: 0x04004EC7 RID: 20167
				private HttpInput.WebResponseHttpInput httpInput;

				// Token: 0x04004EC8 RID: 20168
				private HttpWebResponse webResponse;

				// Token: 0x04004EC9 RID: 20169
				private bool responseClosed;
			}
		}
	}
}
