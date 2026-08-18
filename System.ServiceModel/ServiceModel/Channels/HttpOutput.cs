using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000860 RID: 2144
	internal abstract class HttpOutput
	{
		// Token: 0x0600508A RID: 20618 RVA: 0x00127E00 File Offset: 0x00126000
		protected HttpOutput(IHttpTransportFactorySettings settings, Message message, bool isRequest, bool supportsConcurrentIO)
		{
			this.settings = settings;
			this.message = message;
			this.isRequest = isRequest;
			this.bufferManager = settings.BufferManager;
			this.messageEncoder = settings.MessageEncoderFactory.Encoder;
			ICompressedMessageEncoder compressedMessageEncoder = this.messageEncoder as ICompressedMessageEncoder;
			this.canSendCompressedResponses = (compressedMessageEncoder != null && compressedMessageEncoder.CompressionEnabled);
			if (isRequest)
			{
				this.streamed = TransferModeHelper.IsRequestStreamed(settings.TransferMode);
			}
			else
			{
				this.streamed = TransferModeHelper.IsResponseStreamed(settings.TransferMode);
			}
			this.supportsConcurrentIO = supportsConcurrentIO;
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x0600508B RID: 20619 RVA: 0x00127EAA File Offset: 0x001260AA
		protected virtual bool IsChannelBindingSupportEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x0600508C RID: 20620 RVA: 0x00127EAD File Offset: 0x001260AD
		protected virtual ChannelBinding ChannelBinding
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x00127EB0 File Offset: 0x001260B0
		protected void Abort()
		{
			this.Abort(HttpAbortReason.Aborted);
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x00127EB9 File Offset: 0x001260B9
		public virtual void Abort(HttpAbortReason reason)
		{
			if (this.isDisposed)
			{
				return;
			}
			this.abortReason = reason;
			this.TraceRequestResponseAborted(reason);
			this.CleanupBuffer();
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x00127ED8 File Offset: 0x001260D8
		private void TraceRequestResponseAborted(HttpAbortReason reason)
		{
			if (this.isRequest)
			{
				if (TD.HttpChannelRequestAbortedIsEnabled())
				{
					TD.HttpChannelRequestAborted(this.eventTraceActivity);
				}
			}
			else if (TD.HttpChannelResponseAbortedIsEnabled())
			{
				TD.HttpChannelResponseAborted(this.eventTraceActivity);
			}
			if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, this.isRequest ? 262157 : 262158, this.isRequest ? SR.GetString("TraceCodeHttpChannelRequestAborted") : SR.GetString("TraceCodeHttpChannelResponseAborted"), this.message);
			}
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x00127F58 File Offset: 0x00126158
		public void Close()
		{
			if (this.isDisposed)
			{
				return;
			}
			try
			{
				if (this.outputStream != null)
				{
					this.outputStream.Close();
				}
			}
			finally
			{
				this.CleanupBuffer();
			}
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x00127F9C File Offset: 0x0012619C
		private void CleanupBuffer()
		{
			byte[] array = Interlocked.Exchange<byte[]>(ref this.bufferToRecycle, null);
			if (array != null)
			{
				this.bufferManager.ReturnBuffer(array);
			}
			this.isDisposed = true;
		}

		// Token: 0x06005092 RID: 20626
		protected abstract void AddMimeVersion(string version);

		// Token: 0x06005093 RID: 20627
		protected abstract void AddHeader(string name, string value);

		// Token: 0x06005094 RID: 20628
		protected abstract void SetContentType(string contentType);

		// Token: 0x06005095 RID: 20629
		protected abstract void SetContentEncoding(string contentEncoding);

		// Token: 0x06005096 RID: 20630
		protected abstract void SetStatusCode(HttpStatusCode statusCode);

		// Token: 0x06005097 RID: 20631
		protected abstract void SetStatusDescription(string statusDescription);

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06005098 RID: 20632 RVA: 0x00127FCC File Offset: 0x001261CC
		protected virtual bool CleanupChannelBinding
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x00127FCF File Offset: 0x001261CF
		protected virtual void SetContentLength(int contentLength)
		{
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600509A RID: 20634 RVA: 0x00127FD1 File Offset: 0x001261D1
		protected virtual string HttpMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x00127FD4 File Offset: 0x001261D4
		public virtual ChannelBinding TakeChannelBinding()
		{
			return null;
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x00127FD7 File Offset: 0x001261D7
		private void ApplyChannelBinding()
		{
			if (this.IsChannelBindingSupportEnabled)
			{
				ChannelBindingUtility.TryAddToMessage(this.ChannelBinding, this.message, this.CleanupChannelBinding);
			}
		}

		// Token: 0x0600509D RID: 20637
		protected abstract Stream GetOutputStream();

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x00127FF8 File Offset: 0x001261F8
		protected virtual bool WillGetOutputStreamCompleteSynchronously
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x00127FFB File Offset: 0x001261FB
		protected bool CanSendCompressedResponses
		{
			get
			{
				return this.canSendCompressedResponses;
			}
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x00128003 File Offset: 0x00126203
		protected virtual IAsyncResult BeginGetOutputStream(AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x00128014 File Offset: 0x00126214
		protected virtual Stream EndGetOutputStream(IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x00128028 File Offset: 0x00126228
		public void ConfigureHttpResponseMessage(Message message, HttpResponseMessage httpResponseMessage, HttpResponseMessageProperty responseProperty)
		{
			HttpChannelUtilities.EnsureHttpResponseMessageContentNotNull(httpResponseMessage);
			string action = message.Headers.Action;
			if (message.Version.Addressing == AddressingVersion.None)
			{
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					message.Properties.Add(AddressingProperty.Name, new AddressingProperty(message.Headers));
				}
				message.Headers.Action = null;
				message.Headers.To = null;
			}
			bool flag = responseProperty != null;
			string text = null;
			if (message.Version == MessageVersion.None && flag && !string.IsNullOrEmpty(responseProperty.Headers[HttpResponseHeader.ContentType]))
			{
				text = responseProperty.Headers[HttpResponseHeader.ContentType];
				responseProperty.Headers.Remove(HttpResponseHeader.ContentType);
				if (!this.messageEncoder.IsContentTypeSupported(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("ResponseContentTypeNotSupported", new object[]
					{
						text
					})));
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				MtomMessageEncoder mtomMessageEncoder = this.messageEncoder as MtomMessageEncoder;
				if (mtomMessageEncoder == null)
				{
					text = this.messageEncoder.ContentType;
				}
				else
				{
					text = mtomMessageEncoder.GetContentType(out this.mtomBoundary);
					httpResponseMessage.Headers.Add("MIME-Version", "1.0");
				}
			}
			if (this.isRequest && FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				HttpOutput.EnsureEventTraceActivity(message);
			}
			if (this.CanSendCompressedResponses)
			{
				string text2 = text;
				string contentEncoding;
				if (HttpChannelUtilities.GetHttpResponseTypeAndEncodingForCompression(ref text2, out contentEncoding))
				{
					text = text2;
					this.SetContentEncoding(contentEncoding);
				}
			}
			if (httpResponseMessage.Content != null && !string.IsNullOrEmpty(text))
			{
				MediaTypeHeaderValue contentType;
				if (!MediaTypeHeaderValue.TryParse(text, out contentType))
				{
					throw FxTrace.Exception.Argument("contentType", SR.GetString("InvalidContentTypeError", new object[]
					{
						text
					}));
				}
				httpResponseMessage.Content.Headers.ContentType = contentType;
			}
			if (string.Compare(this.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase) == 0 || (flag && responseProperty.SuppressEntityBody))
			{
				httpResponseMessage.Content.Headers.ContentLength = new long?(0L);
				httpResponseMessage.Content.Headers.ContentType = null;
			}
			if (flag)
			{
				httpResponseMessage.StatusCode = responseProperty.StatusCode;
				if (responseProperty.StatusDescription != null)
				{
					responseProperty.StatusDescription = responseProperty.StatusDescription;
				}
				foreach (string text3 in responseProperty.Headers.AllKeys)
				{
					httpResponseMessage.AddHeader(text3, responseProperty.Headers[text3]);
				}
			}
			if (!message.IsEmpty)
			{
				using (HttpContent content = httpResponseMessage.Content)
				{
					if (this.streamed)
					{
						IStreamedMessageEncoder streamedMessageEncoder = this.messageEncoder as IStreamedMessageEncoder;
						Stream stream = null;
						if (streamedMessageEncoder != null)
						{
							stream = streamedMessageEncoder.GetResponseMessageStream(message);
						}
						if (stream != null)
						{
							httpResponseMessage.Content = new StreamContent(stream);
						}
						else
						{
							httpResponseMessage.Content = new OpaqueContent(this.messageEncoder, message, this.mtomBoundary);
						}
					}
					else
					{
						ArraySegment<byte> arraySegment = this.SerializeBufferedMessage(message, false);
						httpResponseMessage.Content = new HttpOutput.HttpOutputByteArrayContent(arraySegment.Array, arraySegment.Offset, arraySegment.Count, this.bufferManager);
					}
					httpResponseMessage.Content.Headers.Clear();
					foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in content.Headers)
					{
						httpResponseMessage.Content.Headers.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x001283A8 File Offset: 0x001265A8
		protected virtual bool PrepareHttpSend(Message message)
		{
			string action = message.Headers.Action;
			if (message.Version.Addressing == AddressingVersion.None)
			{
				if (MessageLogger.LogMessagesAtTransportLevel)
				{
					message.Properties.Add(AddressingProperty.Name, new AddressingProperty(message.Headers));
				}
				message.Headers.Action = null;
				message.Headers.To = null;
			}
			string text = null;
			if (message.Version == MessageVersion.None)
			{
				object obj = null;
				if (message.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj))
				{
					HttpResponseMessageProperty httpResponseMessageProperty = (HttpResponseMessageProperty)obj;
					if (!string.IsNullOrEmpty(httpResponseMessageProperty.Headers[HttpResponseHeader.ContentType]))
					{
						text = httpResponseMessageProperty.Headers[HttpResponseHeader.ContentType];
						if (!this.messageEncoder.IsContentTypeSupported(text))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("ResponseContentTypeNotSupported", new object[]
							{
								text
							})));
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				MtomMessageEncoder mtomMessageEncoder = this.messageEncoder as MtomMessageEncoder;
				if (mtomMessageEncoder == null)
				{
					text = this.messageEncoder.ContentType;
				}
				else
				{
					text = mtomMessageEncoder.GetContentType(out this.mtomBoundary);
					this.AddMimeVersion("1.0");
				}
			}
			if (this.isRequest && FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				HttpOutput.EnsureEventTraceActivity(message);
			}
			this.SetContentType(text);
			return message is NullMessage;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x001284F7 File Offset: 0x001266F7
		protected bool PrepareHttpSend(HttpResponseMessage httpResponseMessage)
		{
			this.PrepareHttpSendCore(httpResponseMessage);
			return HttpChannelUtilities.IsEmpty(httpResponseMessage);
		}

		// Token: 0x060050A5 RID: 20645
		protected abstract void PrepareHttpSendCore(HttpResponseMessage message);

		// Token: 0x060050A6 RID: 20646 RVA: 0x00128508 File Offset: 0x00126708
		private static void EnsureEventTraceActivity(Message message)
		{
			if (message.Headers.MessageId == null)
			{
				EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				if (eventTraceActivity == null)
				{
					eventTraceActivity = new EventTraceActivity(false);
					EventTraceActivityHelper.TryAttachActivity(message, eventTraceActivity);
				}
				HttpRequestMessageProperty httpRequestMessageProperty;
				if (!message.Properties.TryGetValue<HttpRequestMessageProperty>(HttpRequestMessageProperty.Name, out httpRequestMessageProperty))
				{
					httpRequestMessageProperty = new HttpRequestMessageProperty();
					message.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessageProperty);
				}
				httpRequestMessageProperty.Headers.Add(EventTraceActivity.Name, Convert.ToBase64String(eventTraceActivity.ActivityId.ToByteArray()));
			}
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x0012858C File Offset: 0x0012678C
		private ArraySegment<byte> SerializeBufferedMessage(Message message)
		{
			return this.SerializeBufferedMessage(message, true);
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00128598 File Offset: 0x00126798
		private ArraySegment<byte> SerializeBufferedMessage(Message message, bool shouldRecycleBuffer)
		{
			MtomMessageEncoder mtomMessageEncoder = this.messageEncoder as MtomMessageEncoder;
			ArraySegment<byte> result;
			if (mtomMessageEncoder == null)
			{
				result = this.messageEncoder.WriteMessage(message, int.MaxValue, this.bufferManager);
			}
			else
			{
				result = mtomMessageEncoder.WriteMessage(message, int.MaxValue, this.bufferManager, 0, this.mtomBoundary);
			}
			if (shouldRecycleBuffer)
			{
				this.bufferToRecycle = result.Array;
			}
			return result;
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x001285F9 File Offset: 0x001267F9
		private Stream GetWrappedOutputStream()
		{
			if (!this.supportsConcurrentIO)
			{
				return new BufferedStream(this.outputStream, 32768);
			}
			return new BufferedOutputAsyncStream(this.outputStream, 16384, 4);
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00128628 File Offset: 0x00126828
		private void WriteStreamedMessage(TimeSpan timeout)
		{
			this.outputStream = this.GetWrappedOutputStream();
			if (HttpOutput.onStreamSendTimeout == null)
			{
				HttpOutput.onStreamSendTimeout = new Action<object>(HttpOutput.OnStreamSendTimeout);
			}
			IOThreadTimer iothreadTimer = new IOThreadTimer(HttpOutput.onStreamSendTimeout, this, true);
			iothreadTimer.Set(timeout);
			try
			{
				MtomMessageEncoder mtomMessageEncoder = this.messageEncoder as MtomMessageEncoder;
				if (mtomMessageEncoder == null)
				{
					this.messageEncoder.WriteMessage(this.message, this.outputStream);
				}
				else
				{
					mtomMessageEncoder.WriteMessage(this.message, this.outputStream, this.mtomBoundary);
				}
				if (this.supportsConcurrentIO)
				{
					this.outputStream.Close();
				}
			}
			finally
			{
				iothreadTimer.Cancel();
			}
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x001286DC File Offset: 0x001268DC
		private static void OnStreamSendTimeout(object state)
		{
			HttpOutput httpOutput = (HttpOutput)state;
			httpOutput.Abort(HttpAbortReason.TimedOut);
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x001286F7 File Offset: 0x001268F7
		private IAsyncResult BeginWriteStreamedMessage(HttpResponseMessage httpResponseMessage, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new HttpOutput.WriteStreamedMessageAsyncResult(timeout, this, httpResponseMessage, callback, state);
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x00128704 File Offset: 0x00126904
		private void EndWriteStreamedMessage(IAsyncResult result)
		{
			HttpOutput.WriteStreamedMessageAsyncResult.End(result);
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0012870C File Offset: 0x0012690C
		public IAsyncResult BeginSend(HttpResponseMessage httpResponseMessage, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginSendCore(httpResponseMessage, timeout, callback, state);
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x00128719 File Offset: 0x00126919
		public IAsyncResult BeginSend(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginSendCore(null, timeout, callback, state);
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x00128728 File Offset: 0x00126928
		private IAsyncResult BeginSendCore(HttpResponseMessage httpResponseMessage, TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = true;
			IAsyncResult result;
			try
			{
				bool suppressEntityBody;
				if (httpResponseMessage != null)
				{
					suppressEntityBody = this.PrepareHttpSend(httpResponseMessage);
				}
				else
				{
					suppressEntityBody = this.PrepareHttpSend(this.message);
				}
				this.TraceHttpSendStart();
				IAsyncResult asyncResult = new HttpOutput.SendAsyncResult(this, httpResponseMessage, suppressEntityBody, timeout, callback, state);
				flag = false;
				result = asyncResult;
			}
			finally
			{
				if (flag)
				{
					this.Abort();
				}
			}
			return result;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00128788 File Offset: 0x00126988
		private void TraceHttpSendStart()
		{
			if (TD.HttpSendMessageStartIsEnabled())
			{
				if (this.streamed)
				{
					TD.HttpSendStreamedMessageStart(this.eventTraceActivity);
					return;
				}
				TD.HttpSendMessageStart(this.eventTraceActivity);
			}
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x001287B0 File Offset: 0x001269B0
		public virtual void EndSend(IAsyncResult result)
		{
			bool flag = true;
			try
			{
				HttpOutput.SendAsyncResult.End(result);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.Abort();
				}
			}
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x001287E4 File Offset: 0x001269E4
		private void LogMessage()
		{
			if (MessageLogger.LogMessagesAtTransportLevel)
			{
				MessageLogger.LogMessage(ref this.message, MessageLoggingSource.TransportSend);
			}
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x001287FC File Offset: 0x001269FC
		public void Send(HttpResponseMessage httpResponseMessage, TimeSpan timeout)
		{
			bool flag = this.PrepareHttpSend(httpResponseMessage);
			this.TraceHttpSendStart();
			if (flag)
			{
				if (!this.isRequest)
				{
					this.outputStream = this.GetOutputStream();
				}
				else
				{
					this.SetContentLength(0);
					this.LogMessage();
				}
			}
			else if (this.streamed)
			{
				this.outputStream = this.GetOutputStream();
				this.ApplyChannelBinding();
				OpaqueContent opaqueContent = httpResponseMessage.Content as OpaqueContent;
				if (opaqueContent != null)
				{
					opaqueContent.WriteToStream(this.outputStream);
				}
				else if (!httpResponseMessage.Content.CopyToAsync(this.outputStream).Wait(timeout))
				{
					throw FxTrace.Exception.AsError(new TimeoutException(SR.GetString("TimeoutOnSend", new object[]
					{
						timeout
					})));
				}
			}
			else if (this.IsChannelBindingSupportEnabled)
			{
				this.outputStream = this.GetOutputStream();
				this.ApplyChannelBinding();
				ArraySegment<byte> arraySegment = this.SerializeBufferedMessage(httpResponseMessage);
				this.outputStream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			else
			{
				ArraySegment<byte> arraySegment2 = this.SerializeBufferedMessage(httpResponseMessage);
				this.SetContentLength(arraySegment2.Count);
				if (!this.isRequest || arraySegment2.Count > 0)
				{
					this.outputStream = this.GetOutputStream();
					this.outputStream.Write(arraySegment2.Array, arraySegment2.Offset, arraySegment2.Count);
				}
			}
			this.TraceSend();
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x00128964 File Offset: 0x00126B64
		private ArraySegment<byte> SerializeBufferedMessage(HttpResponseMessage httpResponseMessage)
		{
			HttpOutput.HttpOutputByteArrayContent httpOutputByteArrayContent = httpResponseMessage.Content as HttpOutput.HttpOutputByteArrayContent;
			if (httpOutputByteArrayContent == null)
			{
				byte[] result = httpResponseMessage.Content.ReadAsByteArrayAsync().Result;
				return new ArraySegment<byte>(result, 0, result.Length);
			}
			return httpOutputByteArrayContent.Content;
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x001289A4 File Offset: 0x00126BA4
		public void Send(TimeSpan timeout)
		{
			bool flag = this.PrepareHttpSend(this.message);
			this.TraceHttpSendStart();
			if (flag)
			{
				if (!this.isRequest)
				{
					this.outputStream = this.GetOutputStream();
				}
				else
				{
					this.SetContentLength(0);
					this.LogMessage();
				}
			}
			else if (this.streamed)
			{
				this.outputStream = this.GetOutputStream();
				this.ApplyChannelBinding();
				this.WriteStreamedMessage(timeout);
			}
			else if (this.IsChannelBindingSupportEnabled)
			{
				this.outputStream = this.GetOutputStream();
				this.ApplyChannelBinding();
				ArraySegment<byte> arraySegment = this.SerializeBufferedMessage(this.message);
				this.outputStream.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			else
			{
				ArraySegment<byte> arraySegment2 = this.SerializeBufferedMessage(this.message);
				this.SetContentLength(arraySegment2.Count);
				if (!this.isRequest || arraySegment2.Count > 0)
				{
					this.outputStream = this.GetOutputStream();
					this.outputStream.Write(arraySegment2.Array, arraySegment2.Offset, arraySegment2.Count);
				}
			}
			this.TraceSend();
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00128ABF File Offset: 0x00126CBF
		private void TraceSend()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262164, SR.GetString("TraceCodeMessageSent"), new MessageTraceRecord(this.message), this, null);
			}
			if (TD.HttpSendStopIsEnabled())
			{
				TD.HttpSendStop(this.eventTraceActivity);
			}
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x00128AFC File Offset: 0x00126CFC
		internal static HttpOutput CreateHttpOutput(HttpWebRequest httpWebRequest, IHttpTransportFactorySettings settings, Message message, bool enableChannelBindingSupport)
		{
			return new HttpOutput.WebRequestHttpOutput(httpWebRequest, settings, message, enableChannelBindingSupport);
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x00128B07 File Offset: 0x00126D07
		internal static HttpOutput CreateHttpOutput(HttpListenerResponse httpListenerResponse, IHttpTransportFactorySettings settings, Message message, string httpMethod)
		{
			return new HttpOutput.ListenerResponseHttpOutput(httpListenerResponse, settings, message, httpMethod);
		}

		// Token: 0x040031CB RID: 12747
		private const string DefaultMimeVersion = "1.0";

		// Token: 0x040031CC RID: 12748
		private HttpAbortReason abortReason;

		// Token: 0x040031CD RID: 12749
		private bool isDisposed;

		// Token: 0x040031CE RID: 12750
		private bool isRequest;

		// Token: 0x040031CF RID: 12751
		private Message message;

		// Token: 0x040031D0 RID: 12752
		private IHttpTransportFactorySettings settings;

		// Token: 0x040031D1 RID: 12753
		private byte[] bufferToRecycle;

		// Token: 0x040031D2 RID: 12754
		private BufferManager bufferManager;

		// Token: 0x040031D3 RID: 12755
		private MessageEncoder messageEncoder;

		// Token: 0x040031D4 RID: 12756
		private bool streamed;

		// Token: 0x040031D5 RID: 12757
		private static Action<object> onStreamSendTimeout;

		// Token: 0x040031D6 RID: 12758
		private string mtomBoundary;

		// Token: 0x040031D7 RID: 12759
		private Stream outputStream;

		// Token: 0x040031D8 RID: 12760
		private bool supportsConcurrentIO;

		// Token: 0x040031D9 RID: 12761
		private EventTraceActivity eventTraceActivity;

		// Token: 0x040031DA RID: 12762
		private bool canSendCompressedResponses;

		// Token: 0x02000D42 RID: 3394
		private class HttpOutputByteArrayContent : ByteArrayContent
		{
			// Token: 0x06007C7F RID: 31871 RVA: 0x001D1771 File Offset: 0x001CF971
			public HttpOutputByteArrayContent(byte[] content, int offset, int count, BufferManager bufferManager) : base(content, offset, count)
			{
				this.content = new ArraySegment<byte>(content, offset, count);
				this.bufferManager = bufferManager;
			}

			// Token: 0x17001BDE RID: 7134
			// (get) Token: 0x06007C80 RID: 31872 RVA: 0x001D1792 File Offset: 0x001CF992
			public ArraySegment<byte> Content
			{
				get
				{
					return this.content;
				}
			}

			// Token: 0x06007C81 RID: 31873 RVA: 0x001D179A File Offset: 0x001CF99A
			protected override Task<Stream> CreateContentReadStreamAsync()
			{
				return base.CreateContentReadStreamAsync().ContinueWith<Stream>((Task<Stream> t) => new HttpOutput.HttpOutputByteArrayContent.HttpOutputByteArrayContentStream(t.Result, this));
			}

			// Token: 0x06007C82 RID: 31874 RVA: 0x001D17B3 File Offset: 0x001CF9B3
			protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
			{
				return base.SerializeToStreamAsync(stream, context).ContinueWith(delegate(Task t)
				{
					this.Cleanup();
					HttpChannelUtilities.HandleContinueWithTask(t);
				});
			}

			// Token: 0x06007C83 RID: 31875 RVA: 0x001D17D0 File Offset: 0x001CF9D0
			private void Cleanup()
			{
				if (!this.cleaned)
				{
					lock (this)
					{
						if (!this.cleaned)
						{
							this.cleaned = true;
							this.bufferManager.ReturnBuffer(this.content.Array);
						}
					}
				}
			}

			// Token: 0x04004791 RID: 18321
			private BufferManager bufferManager;

			// Token: 0x04004792 RID: 18322
			private volatile bool cleaned;

			// Token: 0x04004793 RID: 18323
			private ArraySegment<byte> content;

			// Token: 0x02000F59 RID: 3929
			private class HttpOutputByteArrayContentStream : DelegatingStream
			{
				// Token: 0x06008757 RID: 34647 RVA: 0x001F6379 File Offset: 0x001F4579
				public HttpOutputByteArrayContentStream(Stream innerStream, HttpOutput.HttpOutputByteArrayContent content) : base(innerStream)
				{
					this.content = content;
				}

				// Token: 0x06008758 RID: 34648 RVA: 0x001F6389 File Offset: 0x001F4589
				public override void Close()
				{
					base.Close();
					this.content.Cleanup();
				}

				// Token: 0x04004ECA RID: 20170
				private HttpOutput.HttpOutputByteArrayContent content;
			}
		}

		// Token: 0x02000D43 RID: 3395
		private class WriteStreamedMessageAsyncResult : AsyncResult
		{
			// Token: 0x06007C86 RID: 31878 RVA: 0x001D1854 File Offset: 0x001CFA54
			public WriteStreamedMessageAsyncResult(TimeSpan timeout, HttpOutput httpOutput, HttpResponseMessage httpResponseMessage, AsyncCallback callback, object state) : base(callback, state)
			{
				this.httpResponseMessage = httpResponseMessage;
				this.httpOutput = httpOutput;
				httpOutput.outputStream = httpOutput.GetWrappedOutputStream();
				if (HttpOutput.onStreamSendTimeout == null)
				{
					HttpOutput.onStreamSendTimeout = new Action<object>(HttpOutput.OnStreamSendTimeout);
				}
				this.SetTimer(timeout);
				bool flag = false;
				bool flag2 = true;
				try
				{
					flag = this.HandleWriteStreamedMessage(null);
					flag2 = false;
				}
				finally
				{
					if (flag || flag2)
					{
						this.sendTimer.Cancel();
					}
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007C87 RID: 31879 RVA: 0x001D18E0 File Offset: 0x001CFAE0
			private bool HandleWriteStreamedMessage(IAsyncResult result)
			{
				if (this.httpResponseMessage == null)
				{
					if (result == null)
					{
						MtomMessageEncoder mtomMessageEncoder = this.httpOutput.messageEncoder as MtomMessageEncoder;
						if (mtomMessageEncoder == null)
						{
							result = this.httpOutput.messageEncoder.BeginWriteMessage(this.httpOutput.message, this.httpOutput.outputStream, HttpOutput.WriteStreamedMessageAsyncResult.onWriteStreamedMessage, this);
						}
						else
						{
							result = mtomMessageEncoder.BeginWriteMessage(this.httpOutput.message, this.httpOutput.outputStream, this.httpOutput.mtomBoundary, HttpOutput.WriteStreamedMessageAsyncResult.onWriteStreamedMessage, this);
						}
						if (!result.CompletedSynchronously)
						{
							return false;
						}
					}
					this.httpOutput.messageEncoder.EndWriteMessage(result);
					if (this.httpOutput.supportsConcurrentIO)
					{
						this.httpOutput.outputStream.Close();
					}
					return true;
				}
				OpaqueContent opaqueContent = this.httpResponseMessage.Content as OpaqueContent;
				if (result == null)
				{
					if (opaqueContent != null)
					{
						result = opaqueContent.BeginWriteToStream(this.httpOutput.outputStream, HttpOutput.WriteStreamedMessageAsyncResult.onWriteStreamedMessage, this);
					}
					else
					{
						result = this.httpResponseMessage.Content.CopyToAsync(this.httpOutput.outputStream).AsAsyncResult(HttpOutput.WriteStreamedMessageAsyncResult.onWriteStreamedMessage, this);
					}
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				if (opaqueContent != null)
				{
					opaqueContent.EndWriteToStream(result);
				}
				if (this.httpOutput.supportsConcurrentIO)
				{
					this.httpOutput.outputStream.Close();
				}
				return true;
			}

			// Token: 0x06007C88 RID: 31880 RVA: 0x001D1A34 File Offset: 0x001CFC34
			private static void OnWriteStreamedMessage(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpOutput.WriteStreamedMessageAsyncResult writeStreamedMessageAsyncResult = (HttpOutput.WriteStreamedMessageAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = writeStreamedMessageAsyncResult.HandleWriteStreamedMessage(result);
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
					writeStreamedMessageAsyncResult.sendTimer.Cancel();
					writeStreamedMessageAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007C89 RID: 31881 RVA: 0x001D1A9C File Offset: 0x001CFC9C
			private void SetTimer(TimeSpan timeout)
			{
				this.sendTimer = new IOThreadTimer(HttpOutput.onStreamSendTimeout, this.httpOutput, true);
				this.sendTimer.Set(timeout);
			}

			// Token: 0x06007C8A RID: 31882 RVA: 0x001D1AC1 File Offset: 0x001CFCC1
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<HttpOutput.WriteStreamedMessageAsyncResult>(result);
			}

			// Token: 0x04004794 RID: 18324
			private HttpOutput httpOutput;

			// Token: 0x04004795 RID: 18325
			private IOThreadTimer sendTimer;

			// Token: 0x04004796 RID: 18326
			private static AsyncCallback onWriteStreamedMessage = Fx.ThunkCallback(new AsyncCallback(HttpOutput.WriteStreamedMessageAsyncResult.OnWriteStreamedMessage));

			// Token: 0x04004797 RID: 18327
			private HttpResponseMessage httpResponseMessage;
		}

		// Token: 0x02000D44 RID: 3396
		private class SendAsyncResult : AsyncResult
		{
			// Token: 0x06007C8C RID: 31884 RVA: 0x001D1AE4 File Offset: 0x001CFCE4
			public SendAsyncResult(HttpOutput httpOutput, HttpResponseMessage httpResponseMessage, bool suppressEntityBody, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.httpOutput = httpOutput;
				this.httpResponseMessage = httpResponseMessage;
				this.suppressEntityBody = suppressEntityBody;
				if (suppressEntityBody && httpOutput.isRequest)
				{
					httpOutput.SetContentLength(0);
					this.httpOutput.TraceSend();
					this.httpOutput.LogMessage();
					base.Complete(true);
					return;
				}
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.Send();
			}

			// Token: 0x06007C8D RID: 31885 RVA: 0x001D1B53 File Offset: 0x001CFD53
			private void Send()
			{
				if (this.httpOutput.IsChannelBindingSupportEnabled)
				{
					this.SendWithChannelBindingToken();
					return;
				}
				this.SendWithoutChannelBindingToken();
			}

			// Token: 0x06007C8E RID: 31886 RVA: 0x001D1B70 File Offset: 0x001CFD70
			private void SendWithoutChannelBindingToken()
			{
				if (!this.suppressEntityBody && !this.httpOutput.streamed)
				{
					if (this.httpResponseMessage != null)
					{
						this.buffer = this.httpOutput.SerializeBufferedMessage(this.httpResponseMessage);
					}
					else
					{
						this.buffer = this.httpOutput.SerializeBufferedMessage(this.httpOutput.message);
					}
					this.httpOutput.SetContentLength(this.buffer.Count);
				}
				if (this.httpOutput.WillGetOutputStreamCompleteSynchronously)
				{
					this.httpOutput.outputStream = this.httpOutput.GetOutputStream();
				}
				else
				{
					if (HttpOutput.SendAsyncResult.onGetOutputStream == null)
					{
						HttpOutput.SendAsyncResult.onGetOutputStream = Fx.ThunkCallback(new AsyncCallback(HttpOutput.SendAsyncResult.OnGetOutputStream));
					}
					IAsyncResult asyncResult = this.httpOutput.BeginGetOutputStream(HttpOutput.SendAsyncResult.onGetOutputStream, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.httpOutput.outputStream = this.httpOutput.EndGetOutputStream(asyncResult);
				}
				if (this.WriteMessage(true))
				{
					this.httpOutput.TraceSend();
					base.Complete(true);
				}
			}

			// Token: 0x06007C8F RID: 31887 RVA: 0x001D1C74 File Offset: 0x001CFE74
			private void SendWithChannelBindingToken()
			{
				if (this.httpOutput.WillGetOutputStreamCompleteSynchronously)
				{
					this.httpOutput.outputStream = this.httpOutput.GetOutputStream();
					this.httpOutput.ApplyChannelBinding();
				}
				else
				{
					if (HttpOutput.SendAsyncResult.onGetOutputStream == null)
					{
						HttpOutput.SendAsyncResult.onGetOutputStream = Fx.ThunkCallback(new AsyncCallback(HttpOutput.SendAsyncResult.OnGetOutputStream));
					}
					IAsyncResult asyncResult = this.httpOutput.BeginGetOutputStream(HttpOutput.SendAsyncResult.onGetOutputStream, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.httpOutput.outputStream = this.httpOutput.EndGetOutputStream(asyncResult);
					this.httpOutput.ApplyChannelBinding();
				}
				if (!this.httpOutput.streamed)
				{
					if (this.httpResponseMessage != null)
					{
						this.buffer = this.httpOutput.SerializeBufferedMessage(this.httpResponseMessage);
					}
					else
					{
						this.buffer = this.httpOutput.SerializeBufferedMessage(this.httpOutput.message);
					}
					this.httpOutput.SetContentLength(this.buffer.Count);
				}
				if (this.WriteMessage(true))
				{
					this.httpOutput.TraceSend();
					base.Complete(true);
				}
			}

			// Token: 0x06007C90 RID: 31888 RVA: 0x001D1D88 File Offset: 0x001CFF88
			private bool WriteMessage(bool isStillSynchronous)
			{
				if (this.suppressEntityBody)
				{
					return true;
				}
				if (this.httpOutput.streamed)
				{
					if (isStillSynchronous)
					{
						if (HttpOutput.SendAsyncResult.onWriteStreamedMessageLater == null)
						{
							HttpOutput.SendAsyncResult.onWriteStreamedMessageLater = new Action<object>(HttpOutput.SendAsyncResult.OnWriteStreamedMessageLater);
						}
						ActionItem.Schedule(HttpOutput.SendAsyncResult.onWriteStreamedMessageLater, this);
						return false;
					}
					return this.WriteStreamedMessage();
				}
				else
				{
					if (HttpOutput.SendAsyncResult.onWriteBody == null)
					{
						HttpOutput.SendAsyncResult.onWriteBody = Fx.ThunkCallback(new AsyncCallback(HttpOutput.SendAsyncResult.OnWriteBody));
					}
					IAsyncResult asyncResult = this.httpOutput.outputStream.BeginWrite(this.buffer.Array, this.buffer.Offset, this.buffer.Count, HttpOutput.SendAsyncResult.onWriteBody, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.CompleteWriteBody(asyncResult);
					return true;
				}
			}

			// Token: 0x06007C91 RID: 31889 RVA: 0x001D1E42 File Offset: 0x001D0042
			private bool WriteStreamedMessage()
			{
				if (HttpOutput.SendAsyncResult.onWriteStreamedMessage == null)
				{
					HttpOutput.SendAsyncResult.onWriteStreamedMessage = Fx.ThunkCallback(new AsyncCallback(HttpOutput.SendAsyncResult.OnWriteStreamedMessage));
				}
				return this.HandleWriteStreamedMessage(null);
			}

			// Token: 0x06007C92 RID: 31890 RVA: 0x001D1E68 File Offset: 0x001D0068
			private bool HandleWriteStreamedMessage(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.httpOutput.BeginWriteStreamedMessage(this.httpResponseMessage, this.timeoutHelper.RemainingTime(), HttpOutput.SendAsyncResult.onWriteStreamedMessage, this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.httpOutput.EndWriteStreamedMessage(result);
				return true;
			}

			// Token: 0x06007C93 RID: 31891 RVA: 0x001D1EA8 File Offset: 0x001D00A8
			private static void OnWriteStreamedMessage(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpOutput.SendAsyncResult sendAsyncResult = (HttpOutput.SendAsyncResult)result.AsyncState;
				Exception ex = null;
				bool flag = false;
				try
				{
					flag = sendAsyncResult.HandleWriteStreamedMessage(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					flag = true;
					ex = ex2;
				}
				if (flag)
				{
					if (ex != null)
					{
						sendAsyncResult.httpOutput.TraceSend();
					}
					sendAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007C94 RID: 31892 RVA: 0x001D1F14 File Offset: 0x001D0114
			private void CompleteWriteBody(IAsyncResult result)
			{
				this.httpOutput.outputStream.EndWrite(result);
			}

			// Token: 0x06007C95 RID: 31893 RVA: 0x001D1F27 File Offset: 0x001D0127
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<HttpOutput.SendAsyncResult>(result);
			}

			// Token: 0x06007C96 RID: 31894 RVA: 0x001D1F30 File Offset: 0x001D0130
			private static void OnGetOutputStream(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpOutput.SendAsyncResult sendAsyncResult = (HttpOutput.SendAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag = false;
				try
				{
					sendAsyncResult.httpOutput.outputStream = sendAsyncResult.httpOutput.EndGetOutputStream(result);
					sendAsyncResult.httpOutput.ApplyChannelBinding();
					if (!sendAsyncResult.httpOutput.streamed && sendAsyncResult.httpOutput.IsChannelBindingSupportEnabled)
					{
						sendAsyncResult.buffer = sendAsyncResult.httpOutput.SerializeBufferedMessage(sendAsyncResult.httpOutput.message);
						sendAsyncResult.httpOutput.SetContentLength(sendAsyncResult.buffer.Count);
					}
					if (sendAsyncResult.WriteMessage(false))
					{
						sendAsyncResult.httpOutput.TraceSend();
						flag = true;
					}
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
					sendAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007C97 RID: 31895 RVA: 0x001D2008 File Offset: 0x001D0208
			private static void OnWriteStreamedMessageLater(object state)
			{
				HttpOutput.SendAsyncResult sendAsyncResult = (HttpOutput.SendAsyncResult)state;
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = sendAsyncResult.WriteStreamedMessage();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					flag = true;
					ex = ex2;
				}
				if (flag)
				{
					if (ex != null)
					{
						sendAsyncResult.httpOutput.TraceSend();
					}
					sendAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007C98 RID: 31896 RVA: 0x001D2064 File Offset: 0x001D0264
			private static void OnWriteBody(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpOutput.SendAsyncResult sendAsyncResult = (HttpOutput.SendAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					sendAsyncResult.CompleteWriteBody(result);
					sendAsyncResult.httpOutput.TraceSend();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				sendAsyncResult.Complete(false, exception);
			}

			// Token: 0x04004798 RID: 18328
			private HttpOutput httpOutput;

			// Token: 0x04004799 RID: 18329
			private static AsyncCallback onGetOutputStream;

			// Token: 0x0400479A RID: 18330
			private static Action<object> onWriteStreamedMessageLater;

			// Token: 0x0400479B RID: 18331
			private static AsyncCallback onWriteStreamedMessage;

			// Token: 0x0400479C RID: 18332
			private static AsyncCallback onWriteBody;

			// Token: 0x0400479D RID: 18333
			private bool suppressEntityBody;

			// Token: 0x0400479E RID: 18334
			private ArraySegment<byte> buffer;

			// Token: 0x0400479F RID: 18335
			private TimeoutHelper timeoutHelper;

			// Token: 0x040047A0 RID: 18336
			private HttpResponseMessage httpResponseMessage;
		}

		// Token: 0x02000D45 RID: 3397
		private class WebRequestHttpOutput : HttpOutput
		{
			// Token: 0x06007C99 RID: 31897 RVA: 0x001D20C4 File Offset: 0x001D02C4
			public WebRequestHttpOutput(HttpWebRequest httpWebRequest, IHttpTransportFactorySettings settings, Message message, bool enableChannelBindingSupport) : base(settings, message, true, false)
			{
				this.httpWebRequest = httpWebRequest;
				this.enableChannelBindingSupport = enableChannelBindingSupport;
			}

			// Token: 0x06007C9A RID: 31898 RVA: 0x001D20DF File Offset: 0x001D02DF
			public override void Abort(HttpAbortReason abortReason)
			{
				this.httpWebRequest.Abort();
				base.Abort(abortReason);
			}

			// Token: 0x06007C9B RID: 31899 RVA: 0x001D20F3 File Offset: 0x001D02F3
			protected override void AddMimeVersion(string version)
			{
				this.httpWebRequest.Headers["MIME-Version"] = version;
			}

			// Token: 0x06007C9C RID: 31900 RVA: 0x001D210B File Offset: 0x001D030B
			protected override void AddHeader(string name, string value)
			{
				this.httpWebRequest.Headers.Add(name, value);
			}

			// Token: 0x06007C9D RID: 31901 RVA: 0x001D211F File Offset: 0x001D031F
			protected override void SetContentType(string contentType)
			{
				this.httpWebRequest.ContentType = contentType;
			}

			// Token: 0x06007C9E RID: 31902 RVA: 0x001D212D File Offset: 0x001D032D
			protected override void SetContentEncoding(string contentEncoding)
			{
				this.httpWebRequest.Headers.Add("Content-Encoding", contentEncoding);
			}

			// Token: 0x06007C9F RID: 31903 RVA: 0x001D2145 File Offset: 0x001D0345
			protected override void SetContentLength(int contentLength)
			{
				if (contentLength == 0 && !this.enableChannelBindingSupport)
				{
					this.httpWebRequest.ContentLength = (long)contentLength;
				}
			}

			// Token: 0x06007CA0 RID: 31904 RVA: 0x001D215F File Offset: 0x001D035F
			protected override void SetStatusCode(HttpStatusCode statusCode)
			{
			}

			// Token: 0x06007CA1 RID: 31905 RVA: 0x001D2161 File Offset: 0x001D0361
			protected override void SetStatusDescription(string statusDescription)
			{
			}

			// Token: 0x17001BDF RID: 7135
			// (get) Token: 0x06007CA2 RID: 31906 RVA: 0x001D2163 File Offset: 0x001D0363
			protected override bool WillGetOutputStreamCompleteSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001BE0 RID: 7136
			// (get) Token: 0x06007CA3 RID: 31907 RVA: 0x001D2166 File Offset: 0x001D0366
			protected override bool IsChannelBindingSupportEnabled
			{
				get
				{
					return this.enableChannelBindingSupport;
				}
			}

			// Token: 0x17001BE1 RID: 7137
			// (get) Token: 0x06007CA4 RID: 31908 RVA: 0x001D216E File Offset: 0x001D036E
			protected override ChannelBinding ChannelBinding
			{
				get
				{
					return this.channelBindingToken;
				}
			}

			// Token: 0x17001BE2 RID: 7138
			// (get) Token: 0x06007CA5 RID: 31909 RVA: 0x001D2176 File Offset: 0x001D0376
			protected override bool CleanupChannelBinding
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007CA6 RID: 31910 RVA: 0x001D217C File Offset: 0x001D037C
			public override ChannelBinding TakeChannelBinding()
			{
				ChannelBinding result = this.channelBindingToken;
				this.channelBindingToken = null;
				return result;
			}

			// Token: 0x06007CA7 RID: 31911 RVA: 0x001D2198 File Offset: 0x001D0398
			protected override Stream GetOutputStream()
			{
				Stream result;
				try
				{
					Stream stream;
					if (this.IsChannelBindingSupportEnabled)
					{
						TransportContext context;
						stream = this.httpWebRequest.GetRequestStream(out context);
						this.channelBindingToken = ChannelBindingUtility.GetToken(context);
					}
					else
					{
						stream = this.httpWebRequest.GetRequestStream();
					}
					stream = new HttpOutput.WebRequestHttpOutput.WebRequestOutputStream(stream, this.httpWebRequest, this);
					result = stream;
				}
				catch (WebException webException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException, this.httpWebRequest, this.abortReason));
				}
				return result;
			}

			// Token: 0x06007CA8 RID: 31912 RVA: 0x001D2218 File Offset: 0x001D0418
			protected override IAsyncResult BeginGetOutputStream(AsyncCallback callback, object state)
			{
				return new HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult(this.httpWebRequest, this, callback, state);
			}

			// Token: 0x06007CA9 RID: 31913 RVA: 0x001D2228 File Offset: 0x001D0428
			protected override Stream EndGetOutputStream(IAsyncResult result)
			{
				return HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult.End(result, out this.channelBindingToken);
			}

			// Token: 0x06007CAA RID: 31914 RVA: 0x001D2238 File Offset: 0x001D0438
			protected override bool PrepareHttpSend(Message message)
			{
				bool flag = false;
				string text = message.Headers.Action;
				if (text != null)
				{
					text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
					{
						UrlUtility.UrlPathEncode(text)
					});
				}
				bool flag2 = base.PrepareHttpSend(message);
				object obj;
				if (message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
				{
					HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)obj;
					this.httpWebRequest.Method = httpRequestMessageProperty.Method;
					WebHeaderCollection headers = httpRequestMessageProperty.Headers;
					flag2 = (flag2 || httpRequestMessageProperty.SuppressEntityBody);
					for (int i = 0; i < headers.Count; i++)
					{
						string text2 = headers.Keys[i];
						string text3 = headers[i];
						if (string.Compare(text2, "accept", StringComparison.OrdinalIgnoreCase) == 0)
						{
							this.httpWebRequest.Accept = text3;
						}
						else if (string.Compare(text2, "connection", StringComparison.OrdinalIgnoreCase) == 0)
						{
							if (text3.IndexOf("keep-alive", StringComparison.OrdinalIgnoreCase) != -1)
							{
								this.httpWebRequest.KeepAlive = true;
							}
							else
							{
								this.httpWebRequest.Connection = text3;
							}
						}
						else if (string.Compare(text2, "SOAPAction", StringComparison.OrdinalIgnoreCase) == 0)
						{
							if (text == null)
							{
								text = text3;
							}
							else if (text3.Length > 0 && string.Compare(text3, text, StringComparison.Ordinal) != 0)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("HttpSoapActionMismatch", new object[]
								{
									text,
									text3
								})));
							}
						}
						else if (string.Compare(text2, "content-length", StringComparison.OrdinalIgnoreCase) != 0)
						{
							if (string.Compare(text2, "content-type", StringComparison.OrdinalIgnoreCase) == 0)
							{
								this.httpWebRequest.ContentType = text3;
								flag = true;
							}
							else if (string.Compare(text2, "expect", StringComparison.OrdinalIgnoreCase) == 0)
							{
								if (text3.ToUpperInvariant().IndexOf("100-CONTINUE", StringComparison.OrdinalIgnoreCase) != -1)
								{
									this.httpWebRequest.ServicePoint.Expect100Continue = true;
								}
								else
								{
									this.httpWebRequest.Expect = text3;
								}
							}
							else if (string.Compare(text2, "host", StringComparison.OrdinalIgnoreCase) != 0)
							{
								if (string.Compare(text2, "referer", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this.httpWebRequest.Referer = text3;
								}
								else if (string.Compare(text2, "transfer-encoding", StringComparison.OrdinalIgnoreCase) == 0)
								{
									if (text3.ToUpperInvariant().IndexOf("CHUNKED", StringComparison.OrdinalIgnoreCase) != -1)
									{
										this.httpWebRequest.SendChunked = true;
									}
									else
									{
										this.httpWebRequest.TransferEncoding = text3;
									}
								}
								else if (string.Compare(text2, "user-agent", StringComparison.OrdinalIgnoreCase) == 0)
								{
									this.httpWebRequest.UserAgent = text3;
								}
								else if (string.Compare(text2, "if-modified-since", StringComparison.OrdinalIgnoreCase) == 0)
								{
									DateTime ifModifiedSince;
									if (!DateTime.TryParse(text3, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AssumeLocal, out ifModifiedSince))
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("HttpIfModifiedSinceParseError", new object[]
										{
											text3
										})));
									}
									this.httpWebRequest.IfModifiedSince = ifModifiedSince;
								}
								else if (string.Compare(text2, "date", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text2, "proxy-connection", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text2, "range", StringComparison.OrdinalIgnoreCase) != 0)
								{
									this.httpWebRequest.Headers.Add(text2, text3);
								}
							}
						}
					}
				}
				if (text != null)
				{
					if (message.Version.Envelope == EnvelopeVersion.Soap11)
					{
						this.httpWebRequest.Headers["SOAPAction"] = text;
					}
					else if (message.Version.Envelope == EnvelopeVersion.Soap12)
					{
						if (message.Version.Addressing == AddressingVersion.None)
						{
							bool flag3 = true;
							if (flag && (this.httpWebRequest.ContentType.Contains("action") || this.httpWebRequest.ContentType.ToUpperInvariant().IndexOf("ACTION", StringComparison.OrdinalIgnoreCase) != -1))
							{
								try
								{
									ContentType contentType = new ContentType(this.httpWebRequest.ContentType);
									if (contentType.Parameters.ContainsKey("action"))
									{
										string text4 = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
										{
											contentType.Parameters["action"]
										});
										if (string.Compare(text4, text, StringComparison.Ordinal) != 0)
										{
											throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("HttpSoapActionMismatchContentType", new object[]
											{
												text,
												text4
											})));
										}
										flag3 = false;
									}
								}
								catch (FormatException ex)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("HttpContentTypeFormatException", new object[]
									{
										ex.Message,
										this.httpWebRequest.ContentType
									}), ex));
								}
							}
							if (flag3)
							{
								this.httpWebRequest.ContentType = string.Format(CultureInfo.InvariantCulture, "{0}; action={1}", new object[]
								{
									this.httpWebRequest.ContentType,
									text
								});
							}
						}
					}
					else if (message.Version.Envelope != EnvelopeVersion.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("EnvelopeVersionUnknown", new object[]
						{
							message.Version.Envelope.ToString()
						})));
					}
				}
				if (flag2)
				{
					this.httpWebRequest.SendChunked = false;
				}
				else if (this.IsChannelBindingSupportEnabled)
				{
					this.httpWebRequest.SendChunked = true;
				}
				return flag2;
			}

			// Token: 0x06007CAB RID: 31915 RVA: 0x001D2788 File Offset: 0x001D0988
			protected override void PrepareHttpSendCore(HttpResponseMessage message)
			{
			}

			// Token: 0x040047A1 RID: 18337
			private HttpWebRequest httpWebRequest;

			// Token: 0x040047A2 RID: 18338
			private ChannelBinding channelBindingToken;

			// Token: 0x040047A3 RID: 18339
			private bool enableChannelBindingSupport;

			// Token: 0x02000F5A RID: 3930
			private class GetOutputStreamAsyncResult : AsyncResult
			{
				// Token: 0x06008759 RID: 34649 RVA: 0x001F639C File Offset: 0x001F459C
				public GetOutputStreamAsyncResult(HttpWebRequest httpWebRequest, HttpOutput httpOutput, AsyncCallback callback, object state) : base(callback, state)
				{
					this.httpWebRequest = httpWebRequest;
					this.httpOutput = httpOutput;
					IAsyncResult asyncResult = null;
					try
					{
						asyncResult = httpWebRequest.BeginGetRequestStream(HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult.onGetRequestStream, this);
					}
					catch (WebException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException, httpWebRequest, httpOutput.abortReason));
					}
					if (asyncResult.CompletedSynchronously)
					{
						this.CompleteGetRequestStream(asyncResult);
						base.Complete(true);
					}
				}

				// Token: 0x0600875A RID: 34650 RVA: 0x001F6410 File Offset: 0x001F4610
				private void CompleteGetRequestStream(IAsyncResult result)
				{
					try
					{
						TransportContext context;
						this.outputStream = new HttpOutput.WebRequestHttpOutput.WebRequestOutputStream(this.httpWebRequest.EndGetRequestStream(result, out context), this.httpWebRequest, this.httpOutput);
						this.channelBindingToken = ChannelBindingUtility.GetToken(context);
					}
					catch (WebException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException, this.httpWebRequest, this.httpOutput.abortReason));
					}
				}

				// Token: 0x0600875B RID: 34651 RVA: 0x001F6484 File Offset: 0x001F4684
				public static Stream End(IAsyncResult result, out ChannelBinding channelBindingToken)
				{
					HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult getOutputStreamAsyncResult = AsyncResult.End<HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult>(result);
					channelBindingToken = getOutputStreamAsyncResult.channelBindingToken;
					return getOutputStreamAsyncResult.outputStream;
				}

				// Token: 0x0600875C RID: 34652 RVA: 0x001F64A8 File Offset: 0x001F46A8
				private static void OnGetRequestStream(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult getOutputStreamAsyncResult = (HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						getOutputStreamAsyncResult.CompleteGetRequestStream(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					getOutputStreamAsyncResult.Complete(false, exception);
				}

				// Token: 0x04004ECB RID: 20171
				private static AsyncCallback onGetRequestStream = Fx.ThunkCallback(new AsyncCallback(HttpOutput.WebRequestHttpOutput.GetOutputStreamAsyncResult.OnGetRequestStream));

				// Token: 0x04004ECC RID: 20172
				private HttpOutput httpOutput;

				// Token: 0x04004ECD RID: 20173
				private HttpWebRequest httpWebRequest;

				// Token: 0x04004ECE RID: 20174
				private Stream outputStream;

				// Token: 0x04004ECF RID: 20175
				private ChannelBinding channelBindingToken;
			}

			// Token: 0x02000F5B RID: 3931
			private class WebRequestOutputStream : BytesReadPositionStream
			{
				// Token: 0x0600875E RID: 34654 RVA: 0x001F6514 File Offset: 0x001F4714
				public WebRequestOutputStream(Stream requestStream, HttpWebRequest httpWebRequest, HttpOutput httpOutput) : base(requestStream)
				{
					this.httpWebRequest = httpWebRequest;
					this.httpOutput = httpOutput;
				}

				// Token: 0x0600875F RID: 34655 RVA: 0x001F652C File Offset: 0x001F472C
				public override void Close()
				{
					try
					{
						base.Close();
					}
					catch (ObjectDisposedException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestCanceledException(webException, this.httpWebRequest, this.httpOutput.abortReason));
					}
					catch (IOException ioException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestIOException(ioException, this.httpWebRequest));
					}
					catch (WebException webException2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException2, this.httpWebRequest, this.httpOutput.abortReason));
					}
				}

				// Token: 0x17001D92 RID: 7570
				// (get) Token: 0x06008760 RID: 34656 RVA: 0x001F65C8 File Offset: 0x001F47C8
				// (set) Token: 0x06008761 RID: 34657 RVA: 0x001F65D1 File Offset: 0x001F47D1
				public override long Position
				{
					get
					{
						return (long)this.bytesSent;
					}
					set
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
					}
				}

				// Token: 0x06008762 RID: 34658 RVA: 0x001F65EC File Offset: 0x001F47EC
				public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				{
					this.bytesSent += count;
					IAsyncResult result;
					try
					{
						result = base.BeginWrite(buffer, offset, count, callback, state);
					}
					catch (ObjectDisposedException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestCanceledException(webException, this.httpWebRequest, this.httpOutput.abortReason));
					}
					catch (IOException ioException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestIOException(ioException, this.httpWebRequest));
					}
					catch (WebException webException2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException2, this.httpWebRequest, this.httpOutput.abortReason));
					}
					return result;
				}

				// Token: 0x06008763 RID: 34659 RVA: 0x001F66A0 File Offset: 0x001F48A0
				public override void EndWrite(IAsyncResult result)
				{
					try
					{
						base.EndWrite(result);
					}
					catch (ObjectDisposedException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestCanceledException(webException, this.httpWebRequest, this.httpOutput.abortReason));
					}
					catch (IOException ioException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestIOException(ioException, this.httpWebRequest));
					}
					catch (WebException webException2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException2, this.httpWebRequest, this.httpOutput.abortReason));
					}
				}

				// Token: 0x06008764 RID: 34660 RVA: 0x001F673C File Offset: 0x001F493C
				public override void Write(byte[] buffer, int offset, int count)
				{
					try
					{
						base.Write(buffer, offset, count);
					}
					catch (ObjectDisposedException webException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestCanceledException(webException, this.httpWebRequest, this.httpOutput.abortReason));
					}
					catch (IOException ioException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestIOException(ioException, this.httpWebRequest));
					}
					catch (WebException webException2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateRequestWebException(webException2, this.httpWebRequest, this.httpOutput.abortReason));
					}
					this.bytesSent += count;
				}

				// Token: 0x04004ED0 RID: 20176
				private HttpWebRequest httpWebRequest;

				// Token: 0x04004ED1 RID: 20177
				private HttpOutput httpOutput;

				// Token: 0x04004ED2 RID: 20178
				private int bytesSent;
			}
		}

		// Token: 0x02000D46 RID: 3398
		private class ListenerResponseHttpOutput : HttpOutput
		{
			// Token: 0x06007CAC RID: 31916 RVA: 0x001D278A File Offset: 0x001D098A
			public ListenerResponseHttpOutput(HttpListenerResponse listenerResponse, IHttpTransportFactorySettings settings, Message message, string httpMethod) : base(settings, message, false, true)
			{
				this.listenerResponse = listenerResponse;
				this.httpMethod = httpMethod;
				if (message.IsFault)
				{
					this.SetStatusCode(HttpStatusCode.InternalServerError);
					return;
				}
				this.SetStatusCode(HttpStatusCode.OK);
			}

			// Token: 0x17001BE3 RID: 7139
			// (get) Token: 0x06007CAD RID: 31917 RVA: 0x001D27C4 File Offset: 0x001D09C4
			protected override string HttpMethod
			{
				get
				{
					return this.httpMethod;
				}
			}

			// Token: 0x06007CAE RID: 31918 RVA: 0x001D27CC File Offset: 0x001D09CC
			public override void Abort(HttpAbortReason abortReason)
			{
				this.listenerResponse.Abort();
				base.Abort(abortReason);
			}

			// Token: 0x06007CAF RID: 31919 RVA: 0x001D27E0 File Offset: 0x001D09E0
			protected override void AddMimeVersion(string version)
			{
				this.listenerResponse.Headers["MIME-Version"] = version;
			}

			// Token: 0x06007CB0 RID: 31920 RVA: 0x001D27F8 File Offset: 0x001D09F8
			protected override bool PrepareHttpSend(Message message)
			{
				bool result = base.PrepareHttpSend(message);
				if (base.CanSendCompressedResponses)
				{
					string contentType = this.listenerResponse.ContentType;
					string contentEncoding;
					if (HttpChannelUtilities.GetHttpResponseTypeAndEncodingForCompression(ref contentType, out contentEncoding))
					{
						if (contentType != this.listenerResponse.ContentType)
						{
							this.SetContentType(contentType);
						}
						this.SetContentEncoding(contentEncoding);
					}
				}
				HttpResponseMessageProperty value = message.Properties.GetValue<HttpResponseMessageProperty>(HttpResponseMessageProperty.Name, true);
				bool flag = value != null;
				bool flag2 = string.Compare(this.httpMethod, "HEAD", StringComparison.OrdinalIgnoreCase) == 0;
				if (flag2 || (flag && value.SuppressEntityBody))
				{
					result = true;
					this.SetContentLength(0);
					this.SetContentType(null);
					this.listenerResponse.SendChunked = false;
				}
				if (flag)
				{
					this.SetStatusCode(value.StatusCode);
					if (value.StatusDescription != null)
					{
						this.SetStatusDescription(value.StatusDescription);
					}
					WebHeaderCollection headers = value.Headers;
					for (int i = 0; i < headers.Count; i++)
					{
						string text = headers.Keys[i];
						string text2 = headers[i];
						if (string.Compare(text, "content-length", StringComparison.OrdinalIgnoreCase) == 0)
						{
							int contentLength = -1;
							if (flag2 && int.TryParse(text2, out contentLength))
							{
								this.SetContentLength(contentLength);
							}
						}
						else if (string.Compare(text, "content-type", StringComparison.OrdinalIgnoreCase) == 0)
						{
							if (flag2 || !value.SuppressEntityBody)
							{
								this.SetContentType(text2);
							}
						}
						else if (string.Compare(text, "Connection", StringComparison.OrdinalIgnoreCase) == 0 && text2 != null && string.Compare(text2.Trim(), "close", StringComparison.OrdinalIgnoreCase) == 0 && !LocalAppContextSwitches.DisableExplicitConnectionCloseHeader)
						{
							this.listenerResponse.KeepAlive = false;
						}
						else
						{
							this.AddHeader(text, text2);
						}
					}
				}
				return result;
			}

			// Token: 0x06007CB1 RID: 31921 RVA: 0x001D29A1 File Offset: 0x001D0BA1
			protected override void PrepareHttpSendCore(HttpResponseMessage message)
			{
				this.listenerResponse.StatusCode = (int)message.StatusCode;
				if (message.ReasonPhrase != null)
				{
					this.listenerResponse.StatusDescription = message.ReasonPhrase;
				}
				HttpChannelUtilities.CopyHeaders(message, new AddHeaderDelegate(this.AddHeader));
			}

			// Token: 0x06007CB2 RID: 31922 RVA: 0x001D29E0 File Offset: 0x001D0BE0
			protected override void AddHeader(string name, string value)
			{
				if (string.Compare(name, "WWW-Authenticate", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.listenerResponse.AddHeader(name, value);
					return;
				}
				this.listenerResponse.AppendHeader(name, value);
			}

			// Token: 0x06007CB3 RID: 31923 RVA: 0x001D2A0B File Offset: 0x001D0C0B
			protected override void SetContentType(string contentType)
			{
				this.listenerResponse.ContentType = contentType;
			}

			// Token: 0x06007CB4 RID: 31924 RVA: 0x001D2A19 File Offset: 0x001D0C19
			protected override void SetContentEncoding(string contentEncoding)
			{
				this.listenerResponse.AddHeader("Content-Encoding", contentEncoding);
			}

			// Token: 0x06007CB5 RID: 31925 RVA: 0x001D2A2C File Offset: 0x001D0C2C
			protected override void SetContentLength(int contentLength)
			{
				this.listenerResponse.ContentLength64 = (long)contentLength;
			}

			// Token: 0x06007CB6 RID: 31926 RVA: 0x001D2A3B File Offset: 0x001D0C3B
			protected override void SetStatusCode(HttpStatusCode statusCode)
			{
				this.listenerResponse.StatusCode = (int)statusCode;
			}

			// Token: 0x06007CB7 RID: 31927 RVA: 0x001D2A49 File Offset: 0x001D0C49
			protected override void SetStatusDescription(string statusDescription)
			{
				this.listenerResponse.StatusDescription = statusDescription;
			}

			// Token: 0x06007CB8 RID: 31928 RVA: 0x001D2A57 File Offset: 0x001D0C57
			protected override Stream GetOutputStream()
			{
				return new HttpOutput.ListenerResponseHttpOutput.ListenerResponseOutputStream(this.listenerResponse);
			}

			// Token: 0x040047A4 RID: 18340
			private HttpListenerResponse listenerResponse;

			// Token: 0x040047A5 RID: 18341
			private string httpMethod;

			// Token: 0x02000F5C RID: 3932
			private class ListenerResponseOutputStream : BytesReadPositionStream
			{
				// Token: 0x06008765 RID: 34661 RVA: 0x001F67E8 File Offset: 0x001F49E8
				public ListenerResponseOutputStream(HttpListenerResponse listenerResponse) : base(listenerResponse.OutputStream)
				{
				}

				// Token: 0x06008766 RID: 34662 RVA: 0x001F67F8 File Offset: 0x001F49F8
				public override void Close()
				{
					try
					{
						base.Close();
					}
					catch (HttpListenerException listenerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
					}
				}

				// Token: 0x06008767 RID: 34663 RVA: 0x001F6830 File Offset: 0x001F4A30
				public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
				{
					IAsyncResult result;
					try
					{
						result = base.BeginWrite(buffer, offset, count, callback, state);
					}
					catch (HttpListenerException listenerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
					}
					catch (ApplicationException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("HttpResponseAborted"), innerException));
					}
					return result;
				}

				// Token: 0x06008768 RID: 34664 RVA: 0x001F6898 File Offset: 0x001F4A98
				public override void EndWrite(IAsyncResult result)
				{
					try
					{
						base.EndWrite(result);
					}
					catch (HttpListenerException listenerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
					}
					catch (ApplicationException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("HttpResponseAborted"), innerException));
					}
				}

				// Token: 0x06008769 RID: 34665 RVA: 0x001F68F8 File Offset: 0x001F4AF8
				public override void Write(byte[] buffer, int offset, int count)
				{
					try
					{
						base.Write(buffer, offset, count);
					}
					catch (HttpListenerException listenerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
					}
					catch (ApplicationException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("HttpResponseAborted"), innerException));
					}
				}
			}
		}
	}
}
