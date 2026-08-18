using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200086C RID: 2156
	internal abstract class HttpRequestContext : RequestContextBase
	{
		// Token: 0x0600515E RID: 20830 RVA: 0x0012B757 File Offset: 0x00129957
		protected HttpRequestContext(HttpChannelListener listener, Message requestMessage, EventTraceActivity eventTraceActivity) : base(requestMessage, listener.InternalCloseTimeout, listener.InternalSendTimeout)
		{
			this.listener = listener;
			this.eventTraceActivity = eventTraceActivity;
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x0012B77A File Offset: 0x0012997A
		public bool KeepAliveEnabled
		{
			get
			{
				return this.listener.KeepAliveEnabled;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x0012B787 File Offset: 0x00129987
		public bool HttpMessagesSupported
		{
			get
			{
				return this.listener.HttpMessageSettings.HttpMessagesSupported;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06005161 RID: 20833
		public abstract string HttpMethod { get; }

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06005162 RID: 20834
		public abstract bool IsWebSocketRequest { get; }

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06005163 RID: 20835 RVA: 0x0012B799 File Offset: 0x00129999
		// (set) Token: 0x06005164 RID: 20836 RVA: 0x0012B7A1 File Offset: 0x001299A1
		internal ServerWebSocketTransportDuplexSessionChannel WebSocketChannel
		{
			get
			{
				return this.webSocketChannel;
			}
			set
			{
				this.webSocketChannel = value;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x0012B7AA File Offset: 0x001299AA
		internal HttpChannelListener Listener
		{
			get
			{
				return this.listener;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06005166 RID: 20838 RVA: 0x0012B7B2 File Offset: 0x001299B2
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				return this.eventTraceActivity;
			}
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0012B7BC File Offset: 0x001299BC
		public HttpInput GetHttpInput(bool throwOnError)
		{
			HttpPipeline httpPipeline = this.httpPipeline;
			if (httpPipeline != null && httpPipeline.IsHttpInputInitialized)
			{
				return httpPipeline.HttpInput;
			}
			HttpInput result = null;
			if (throwOnError || !this.errorGettingHttpInput)
			{
				try
				{
					result = this.GetHttpInput();
					this.errorGettingHttpInput = false;
				}
				catch (Exception exception)
				{
					this.errorGettingHttpInput = true;
					if (throwOnError || Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				}
			}
			return result;
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0012B830 File Offset: 0x00129A30
		internal static HttpRequestContext CreateContext(HttpChannelListener listener, HttpListenerContext listenerContext, EventTraceActivity eventTraceActivity)
		{
			return new HttpRequestContext.ListenerHttpContext(listener, listenerContext, eventTraceActivity);
		}

		// Token: 0x06005169 RID: 20841
		protected abstract SecurityMessageProperty OnProcessAuthentication();

		// Token: 0x0600516A RID: 20842
		public abstract HttpOutput GetHttpOutput(Message message);

		// Token: 0x0600516B RID: 20843
		protected abstract HttpInput GetHttpInput();

		// Token: 0x0600516C RID: 20844 RVA: 0x0012B83A File Offset: 0x00129A3A
		public HttpOutput GetHttpOutputCore(Message message)
		{
			if (this.httpOutput != null)
			{
				return this.httpOutput;
			}
			return this.GetHttpOutput(message);
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0012B852 File Offset: 0x00129A52
		protected override void OnAbort()
		{
			if (this.httpOutput != null)
			{
				this.httpOutput.Abort(HttpAbortReason.Aborted);
			}
			this.Cleanup();
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0012B870 File Offset: 0x00129A70
		protected override void OnClose(TimeSpan timeout)
		{
			try
			{
				if (this.httpOutput != null)
				{
					this.httpOutput.Close();
				}
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0012B8AC File Offset: 0x00129AAC
		protected virtual void Cleanup()
		{
			if (this.httpPipeline != null)
			{
				this.httpPipeline.Close();
			}
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0012B8C1 File Offset: 0x00129AC1
		public void InitializeHttpPipeline(TransportIntegrationHandler transportIntegrationHandler)
		{
			this.httpPipeline = HttpPipeline.CreateHttpPipeline(this, transportIntegrationHandler, this.IsWebSocketRequest);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0012B8D8 File Offset: 0x00129AD8
		internal void SetMessage(Message message, Exception requestException)
		{
			if (message == null && requestException == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), new XmlException(SR.GetString("MessageIsEmpty"))));
			}
			this.TraceHttpMessageReceived(message);
			if (requestException != null)
			{
				base.SetRequestMessage(requestException);
				message.Close();
				return;
			}
			message.Properties.Security = ((this.securityProperty != null) ? ((SecurityMessageProperty)this.securityProperty.CreateCopy()) : null);
			base.SetRequestMessage(message);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0012B95C File Offset: 0x00129B5C
		private void TraceHttpMessageReceived(Message message)
		{
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				bool flag = false;
				Guid relatedActivityId = (this.eventTraceActivity != null) ? this.eventTraceActivity.ActivityId : Guid.Empty;
				HttpRequestMessageProperty httpRequestMessageProperty;
				if (message.Headers.MessageId == null && message.Properties.TryGetValue<HttpRequestMessageProperty>(HttpRequestMessageProperty.Name, out httpRequestMessageProperty))
				{
					try
					{
						string text = httpRequestMessageProperty.Headers[EventTraceActivity.Name];
						if (!string.IsNullOrEmpty(text))
						{
							byte[] array = Convert.FromBase64String(text);
							if (array != null && array.Length == 16)
							{
								Guid guid = new Guid(array);
								this.eventTraceActivity = new EventTraceActivity(guid, true);
								message.Properties[EventTraceActivity.Name] = this.eventTraceActivity;
								flag = true;
							}
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
					}
				}
				if (!flag)
				{
					this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message, true);
				}
				if (TD.MessageReceivedByTransportIsEnabled())
				{
					TD.MessageReceivedByTransport(this.eventTraceActivity, (this.listener != null && this.listener.Uri != null) ? this.listener.Uri.AbsoluteUri : string.Empty, relatedActivityId);
				}
			}
		}

		// Token: 0x06005173 RID: 20851
		protected abstract HttpStatusCode ValidateAuthentication();

		// Token: 0x06005174 RID: 20852 RVA: 0x0012BA94 File Offset: 0x00129C94
		private bool PrepareReply(ref Message message)
		{
			bool closeHttpOutput = false;
			if (message == null)
			{
				closeHttpOutput = true;
				message = this.CreateAckMessage(HttpStatusCode.Accepted, string.Empty);
			}
			if (!this.listener.ManualAddressing)
			{
				if (message.Version.Addressing == AddressingVersion.WSAddressingAugust2004)
				{
					if (message.Headers.To == null || this.listener.AnonymousUriPrefixMatcher == null || !this.listener.AnonymousUriPrefixMatcher.IsAnonymousUri(message.Headers.To))
					{
						message.Headers.To = message.Version.Addressing.AnonymousUri;
					}
				}
				else
				{
					if (message.Version.Addressing != AddressingVersion.WSAddressing10 && message.Version.Addressing != AddressingVersion.None)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							message.Version.Addressing
						})));
					}
					if (message.Headers.To != null && (this.listener.AnonymousUriPrefixMatcher == null || !this.listener.AnonymousUriPrefixMatcher.IsAnonymousUri(message.Headers.To)))
					{
						message.Headers.To = null;
					}
				}
			}
			message.Properties.AllowOutputBatching = false;
			this.httpOutput = this.GetHttpOutputCore(message);
			HttpInput httpInput = this.httpPipeline.HttpInput;
			if (httpInput != null)
			{
				HttpDelayedAcceptStream httpDelayedAcceptStream = httpInput.GetInputStream(false) as HttpDelayedAcceptStream;
				if (httpDelayedAcceptStream != null && TransferModeHelper.IsRequestStreamed(this.listener.TransferMode) && httpDelayedAcceptStream.EnableDelayedAccept(this.httpOutput, closeHttpOutput))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x0012BC44 File Offset: 0x00129E44
		protected override void OnReply(Message message, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			Message message2 = message;
			try
			{
				bool flag = this.PrepareReply(ref message2);
				this.httpPipeline.SendReply(message2, timeoutHelper.RemainingTime());
				if (flag)
				{
					this.httpOutput.Close();
				}
				if (TD.MessageSentByTransportIsEnabled())
				{
					TD.MessageSentByTransport(this.eventTraceActivity, this.Listener.Uri.AbsoluteUri);
				}
			}
			finally
			{
				if (message != null && message != message2)
				{
					message2.Close();
				}
			}
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0012BCC8 File Offset: 0x00129EC8
		protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new HttpRequestContext.ReplyAsyncResult(this, message, timeout, callback, state);
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x0012BCD5 File Offset: 0x00129ED5
		protected override void OnEndReply(IAsyncResult result)
		{
			HttpRequestContext.ReplyAsyncResult.End(result);
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0012BCE0 File Offset: 0x00129EE0
		public bool ProcessAuthentication()
		{
			if (TD.HttpContextBeforeProcessAuthenticationIsEnabled())
			{
				TD.HttpContextBeforeProcessAuthentication(this.eventTraceActivity);
			}
			HttpStatusCode httpStatusCode = this.ValidateAuthentication();
			if (httpStatusCode == HttpStatusCode.OK)
			{
				bool flag = false;
				httpStatusCode = HttpStatusCode.Forbidden;
				try
				{
					this.securityProperty = this.OnProcessAuthentication();
					flag = true;
					return true;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (ex.Data.Contains("HttpStatusCode") && ex.Data["HttpStatusCode"] is HttpStatusCode)
					{
						httpStatusCode = (HttpStatusCode)ex.Data["HttpStatusCode"];
					}
					throw;
				}
				finally
				{
					if (!flag)
					{
						this.SendResponseAndClose(httpStatusCode);
					}
				}
			}
			this.SendResponseAndClose(httpStatusCode);
			return false;
		}

		// Token: 0x06005179 RID: 20857 RVA: 0x0012BDA8 File Offset: 0x00129FA8
		internal void SendResponseAndClose(HttpStatusCode statusCode)
		{
			this.SendResponseAndClose(statusCode, string.Empty);
		}

		// Token: 0x0600517A RID: 20858 RVA: 0x0012BDB8 File Offset: 0x00129FB8
		internal void SendResponseAndClose(HttpStatusCode statusCode, string statusDescription)
		{
			if (base.ReplyInitiated)
			{
				this.Close();
				return;
			}
			using (Message message = this.CreateAckMessage(statusCode, statusDescription))
			{
				this.Reply(message);
			}
			this.Close();
		}

		// Token: 0x0600517B RID: 20859 RVA: 0x0012BE08 File Offset: 0x0012A008
		internal void SendResponseAndClose(HttpResponseMessage httpResponseMessage)
		{
			if (base.TryInitiateReply())
			{
				try
				{
					if (this.httpOutput == null)
					{
						this.httpOutput = this.GetHttpOutputCore(new NullMessage());
					}
					this.httpOutput.Send(httpResponseMessage, base.DefaultSendTimeout);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
			}
			try
			{
				this.Close();
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0012BE98 File Offset: 0x0012A098
		private Message CreateAckMessage(HttpStatusCode statusCode, string statusDescription)
		{
			Message message = new NullMessage();
			HttpResponseMessageProperty httpResponseMessageProperty = new HttpResponseMessageProperty();
			httpResponseMessageProperty.StatusCode = statusCode;
			httpResponseMessageProperty.SuppressEntityBody = true;
			if (statusDescription.Length > 0)
			{
				httpResponseMessageProperty.StatusDescription = statusDescription;
			}
			message.Properties.Add(HttpResponseMessageProperty.Name, httpResponseMessageProperty);
			return message;
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x0012BEE4 File Offset: 0x0012A0E4
		public void AcceptWebSocket(HttpResponseMessage response, string protocol, TimeSpan timeout)
		{
			bool flag = false;
			Task<WebSocketContext> task;
			try
			{
				task = this.AcceptWebSocketCore(response, protocol);
				try
				{
					if (!task.Wait(TimeoutHelper.ToMilliseconds(timeout)))
					{
						throw FxTrace.Exception.AsError(new TimeoutException(SR.GetString("AcceptWebSocketTimedOutError")));
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					WebSocketHelper.ThrowCorrectException(ex);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.OnAcceptWebSocketError();
				}
			}
			base.SetReplySent();
			this.OnAcceptWebSocketSuccess(task.Result, response.RequestMessage);
		}

		// Token: 0x0600517E RID: 20862
		protected abstract Task<WebSocketContext> AcceptWebSocketCore(HttpResponseMessage response, string protocol);

		// Token: 0x0600517F RID: 20863 RVA: 0x0012BF7C File Offset: 0x0012A17C
		protected virtual void OnAcceptWebSocketError()
		{
		}

		// Token: 0x06005180 RID: 20864
		protected abstract void OnAcceptWebSocketSuccess(WebSocketContext context, HttpRequestMessage requestMessage);

		// Token: 0x06005181 RID: 20865 RVA: 0x0012BF7E File Offset: 0x0012A17E
		protected void OnAcceptWebSocketSuccess(WebSocketContext context, RemoteEndpointMessageProperty remoteEndpointMessageProperty, byte[] webSocketInternalBuffer, bool shouldDisposeWebSocketAfterClose, HttpRequestMessage requestMessage)
		{
			this.webSocketChannel.SetWebSocketInfo(context, remoteEndpointMessageProperty, this.securityProperty, webSocketInternalBuffer, shouldDisposeWebSocketAfterClose, requestMessage);
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x0012BF98 File Offset: 0x0012A198
		public IAsyncResult BeginAcceptWebSocket(HttpResponseMessage response, string protocol, AsyncCallback callback, object state)
		{
			return new HttpRequestContext.AcceptWebSocketAsyncResult(this, response, protocol, callback, state);
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x0012BFA5 File Offset: 0x0012A1A5
		public void EndAcceptWebSocket(IAsyncResult result)
		{
			HttpRequestContext.AcceptWebSocketAsyncResult.End(result);
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x0012BFAD File Offset: 0x0012A1AD
		internal IAsyncResult BeginProcessInboundRequest(ReplyChannelAcceptor replyChannelAcceptor, Action acceptorCallback, AsyncCallback callback, object state)
		{
			return this.httpPipeline.BeginProcessInboundRequest(replyChannelAcceptor, acceptorCallback, callback, state);
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x0012BFBF File Offset: 0x0012A1BF
		internal void EndProcessInboundRequest(IAsyncResult result)
		{
			this.httpPipeline.EndProcessInboundRequest(result);
		}

		// Token: 0x0400320C RID: 12812
		private HttpOutput httpOutput;

		// Token: 0x0400320D RID: 12813
		private bool errorGettingHttpInput;

		// Token: 0x0400320E RID: 12814
		private HttpChannelListener listener;

		// Token: 0x0400320F RID: 12815
		private SecurityMessageProperty securityProperty;

		// Token: 0x04003210 RID: 12816
		private EventTraceActivity eventTraceActivity;

		// Token: 0x04003211 RID: 12817
		private HttpPipeline httpPipeline;

		// Token: 0x04003212 RID: 12818
		private ServerWebSocketTransportDuplexSessionChannel webSocketChannel;

		// Token: 0x02000D4D RID: 3405
		private class ReplyAsyncResult : AsyncResult
		{
			// Token: 0x06007CCC RID: 31948 RVA: 0x001D2F38 File Offset: 0x001D1138
			public ReplyAsyncResult(HttpRequestContext context, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.context = context;
				this.message = message;
				this.responseMessage = null;
				this.timeoutHelper = new TimeoutHelper(timeout);
				ThreadTrace.Trace("Begin sending http reply");
				this.responseMessage = this.message;
				if (this.SendResponse())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007CCD RID: 31949 RVA: 0x001D2F95 File Offset: 0x001D1195
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<HttpRequestContext.ReplyAsyncResult>(result);
			}

			// Token: 0x06007CCE RID: 31950 RVA: 0x001D2FA0 File Offset: 0x001D11A0
			private void OnSendResponseCompleted(IAsyncResult result)
			{
				try
				{
					this.context.httpOutput.EndSend(result);
					ThreadTrace.Trace("End sending http reply");
					if (this.closeOutputAfterReply)
					{
						this.context.httpOutput.Close();
					}
				}
				finally
				{
					if (this.message != null && this.message != this.responseMessage)
					{
						this.responseMessage.Close();
					}
				}
			}

			// Token: 0x06007CCF RID: 31951 RVA: 0x001D3014 File Offset: 0x001D1214
			private static void OnSendResponseCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpRequestContext.ReplyAsyncResult replyAsyncResult = (HttpRequestContext.ReplyAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					replyAsyncResult.OnSendResponseCompleted(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				replyAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007CD0 RID: 31952 RVA: 0x001D3068 File Offset: 0x001D1268
			private static void OnHttpPipelineSendCallback(object target, HttpResponseMessage httpResponseMessage)
			{
				HttpRequestContext.ReplyAsyncResult replyAsyncResult = (HttpRequestContext.ReplyAsyncResult)target;
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = replyAsyncResult.SendResponse(httpResponseMessage);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
					flag = true;
				}
				if (flag)
				{
					replyAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007CD1 RID: 31953 RVA: 0x001D30B8 File Offset: 0x001D12B8
			public bool SendResponse(HttpResponseMessage httpResponseMessage)
			{
				if (HttpRequestContext.ReplyAsyncResult.onSendCompleted == null)
				{
					HttpRequestContext.ReplyAsyncResult.onSendCompleted = Fx.ThunkCallback(new AsyncCallback(HttpRequestContext.ReplyAsyncResult.OnSendResponseCompletedCallback));
				}
				bool flag = false;
				bool result;
				try
				{
					result = this.SendResponseCore(httpResponseMessage, out flag);
				}
				finally
				{
					if (!flag && this.message != null && this.message != this.responseMessage)
					{
						this.responseMessage.Close();
					}
				}
				return result;
			}

			// Token: 0x06007CD2 RID: 31954 RVA: 0x001D3128 File Offset: 0x001D1328
			public bool SendResponse()
			{
				if (HttpRequestContext.ReplyAsyncResult.onSendCompleted == null)
				{
					HttpRequestContext.ReplyAsyncResult.onSendCompleted = Fx.ThunkCallback(new AsyncCallback(HttpRequestContext.ReplyAsyncResult.OnSendResponseCompletedCallback));
				}
				bool flag = false;
				bool result;
				try
				{
					this.closeOutputAfterReply = this.context.PrepareReply(ref this.responseMessage);
					if (HttpRequestContext.ReplyAsyncResult.onHttpPipelineSend == null)
					{
						HttpRequestContext.ReplyAsyncResult.onHttpPipelineSend = new Action<object, HttpResponseMessage>(HttpRequestContext.ReplyAsyncResult.OnHttpPipelineSendCallback);
					}
					if (this.context.httpPipeline.SendAsyncReply(this.responseMessage, HttpRequestContext.ReplyAsyncResult.onHttpPipelineSend, this) == AsyncCompletionResult.Queued)
					{
						flag = true;
						result = false;
					}
					else
					{
						HttpResponseMessage httpResponseMessage = null;
						if (this.context.HttpMessagesSupported)
						{
							httpResponseMessage = HttpResponseMessageProperty.GetHttpResponseMessageFromMessage(this.responseMessage);
						}
						result = this.SendResponseCore(httpResponseMessage, out flag);
					}
				}
				finally
				{
					if (!flag && this.message != null && this.message != this.responseMessage)
					{
						this.responseMessage.Close();
					}
				}
				return result;
			}

			// Token: 0x06007CD3 RID: 31955 RVA: 0x001D3204 File Offset: 0x001D1404
			private bool SendResponseCore(HttpResponseMessage httpResponseMessage, out bool success)
			{
				success = false;
				IAsyncResult asyncResult;
				if (httpResponseMessage == null)
				{
					asyncResult = this.context.httpOutput.BeginSend(this.timeoutHelper.RemainingTime(), HttpRequestContext.ReplyAsyncResult.onSendCompleted, this);
				}
				else
				{
					asyncResult = this.context.httpOutput.BeginSend(httpResponseMessage, this.timeoutHelper.RemainingTime(), HttpRequestContext.ReplyAsyncResult.onSendCompleted, this);
				}
				success = true;
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.OnSendResponseCompleted(asyncResult);
				return true;
			}

			// Token: 0x040047B8 RID: 18360
			private static AsyncCallback onSendCompleted;

			// Token: 0x040047B9 RID: 18361
			private static Action<object, HttpResponseMessage> onHttpPipelineSend;

			// Token: 0x040047BA RID: 18362
			private bool closeOutputAfterReply;

			// Token: 0x040047BB RID: 18363
			private HttpRequestContext context;

			// Token: 0x040047BC RID: 18364
			private Message message;

			// Token: 0x040047BD RID: 18365
			private Message responseMessage;

			// Token: 0x040047BE RID: 18366
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000D4E RID: 3406
		private class ListenerHttpContext : HttpRequestContext, HttpRequestMessageProperty.IHttpHeaderProvider
		{
			// Token: 0x06007CD4 RID: 31956 RVA: 0x001D3273 File Offset: 0x001D1473
			public ListenerHttpContext(HttpChannelListener listener, HttpListenerContext listenerContext, EventTraceActivity eventTraceActivity) : base(listener, null, eventTraceActivity)
			{
				this.listenerContext = listenerContext;
			}

			// Token: 0x17001BE6 RID: 7142
			// (get) Token: 0x06007CD5 RID: 31957 RVA: 0x001D3285 File Offset: 0x001D1485
			public override string HttpMethod
			{
				get
				{
					return this.listenerContext.Request.HttpMethod;
				}
			}

			// Token: 0x17001BE7 RID: 7143
			// (get) Token: 0x06007CD6 RID: 31958 RVA: 0x001D3297 File Offset: 0x001D1497
			public override bool IsWebSocketRequest
			{
				get
				{
					return this.listenerContext.Request.IsWebSocketRequest;
				}
			}

			// Token: 0x06007CD7 RID: 31959 RVA: 0x001D32A9 File Offset: 0x001D14A9
			protected override HttpInput GetHttpInput()
			{
				return new HttpRequestContext.ListenerHttpContext.ListenerContextHttpInput(this);
			}

			// Token: 0x06007CD8 RID: 31960 RVA: 0x001D32B4 File Offset: 0x001D14B4
			protected override Task<WebSocketContext> AcceptWebSocketCore(HttpResponseMessage response, string protocol)
			{
				HttpChannelUtilities.CopyHeaders(response, new AddHeaderDelegate(this.listenerContext.Response.Headers.Add));
				this.webSocketInternalBuffer = base.Listener.TakeWebSocketInternalBuffer();
				return this.listenerContext.AcceptWebSocketAsync(protocol, WebSocketHelper.GetReceiveBufferSize(this.listener.MaxReceivedMessageSize), base.Listener.WebSocketSettings.GetEffectiveKeepAliveInterval(), new ArraySegment<byte>(this.webSocketInternalBuffer)).Upcast<HttpListenerWebSocketContext, WebSocketContext>();
			}

			// Token: 0x06007CD9 RID: 31961 RVA: 0x001D3330 File Offset: 0x001D1530
			protected override void OnAcceptWebSocketError()
			{
				byte[] array = Interlocked.CompareExchange<byte[]>(ref this.webSocketInternalBuffer, null, this.webSocketInternalBuffer);
				if (array != null)
				{
					base.Listener.ReturnWebSocketInternalBuffer(array);
				}
			}

			// Token: 0x06007CDA RID: 31962 RVA: 0x001D3360 File Offset: 0x001D1560
			protected override void OnAcceptWebSocketSuccess(WebSocketContext context, HttpRequestMessage requestMessage)
			{
				RemoteEndpointMessageProperty remoteEndpointMessageProperty = null;
				if (this.listenerContext.Request.RemoteEndPoint != null)
				{
					remoteEndpointMessageProperty = new RemoteEndpointMessageProperty(this.listenerContext.Request.RemoteEndPoint);
				}
				base.OnAcceptWebSocketSuccess(context, remoteEndpointMessageProperty, this.webSocketInternalBuffer, true, requestMessage);
			}

			// Token: 0x06007CDB RID: 31963 RVA: 0x001D33A8 File Offset: 0x001D15A8
			public override HttpOutput GetHttpOutput(Message message)
			{
				if (this.listenerContext.Request.ContentLength64 == -1L && !OSEnvironmentHelper.IsVistaOrGreater)
				{
					this.listenerContext.Response.KeepAlive = false;
				}
				else
				{
					this.listenerContext.Response.KeepAlive = this.listener.KeepAliveEnabled;
				}
				ICompressedMessageEncoder compressedMessageEncoder = this.listener.MessageEncoderFactory.Encoder as ICompressedMessageEncoder;
				if (compressedMessageEncoder != null && compressedMessageEncoder.CompressionEnabled)
				{
					string supportedCompressionTypes = this.listenerContext.Request.Headers["Accept-Encoding"];
					compressedMessageEncoder.AddCompressedMessageProperties(message, supportedCompressionTypes);
				}
				return HttpOutput.CreateHttpOutput(this.listenerContext.Response, base.Listener, message, this.HttpMethod);
			}

			// Token: 0x06007CDC RID: 31964 RVA: 0x001D345F File Offset: 0x001D165F
			protected override SecurityMessageProperty OnProcessAuthentication()
			{
				return base.Listener.ProcessAuthentication(this.listenerContext);
			}

			// Token: 0x06007CDD RID: 31965 RVA: 0x001D3472 File Offset: 0x001D1672
			protected override HttpStatusCode ValidateAuthentication()
			{
				return base.Listener.ValidateAuthentication(this.listenerContext);
			}

			// Token: 0x06007CDE RID: 31966 RVA: 0x001D3485 File Offset: 0x001D1685
			protected override void OnAbort()
			{
				this.listenerContext.Response.Abort();
				this.Cleanup();
			}

			// Token: 0x06007CDF RID: 31967 RVA: 0x001D34A0 File Offset: 0x001D16A0
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnClose(timeoutHelper.RemainingTime());
				try
				{
					this.listenerContext.Response.Close();
				}
				catch (HttpListenerException listenerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
				}
			}

			// Token: 0x06007CE0 RID: 31968 RVA: 0x001D34F8 File Offset: 0x001D16F8
			void HttpRequestMessageProperty.IHttpHeaderProvider.CopyHeaders(WebHeaderCollection headers)
			{
				HttpListenerRequest request = this.listenerContext.Request;
				headers.Add(request.Headers);
				if (request.UserAgent != null && headers[HttpRequestHeader.UserAgent] == null)
				{
					headers.Add(HttpRequestHeader.UserAgent, request.UserAgent);
				}
			}

			// Token: 0x040047BF RID: 18367
			private HttpListenerContext listenerContext;

			// Token: 0x040047C0 RID: 18368
			private byte[] webSocketInternalBuffer;

			// Token: 0x02000F5D RID: 3933
			private class ListenerContextHttpInput : HttpInput
			{
				// Token: 0x0600876A RID: 34666 RVA: 0x001F695C File Offset: 0x001F4B5C
				public ListenerContextHttpInput(HttpRequestContext.ListenerHttpContext listenerHttpContext) : base(listenerHttpContext.Listener, true, listenerHttpContext.listener.IsChannelBindingSupportEnabled)
				{
					this.listenerHttpContext = listenerHttpContext;
					if (this.listenerHttpContext.listenerContext.Request.ContentLength64 == -1L)
					{
						this.preReadBuffer = new byte[1];
						if (this.listenerHttpContext.listenerContext.Request.InputStream.Read(this.preReadBuffer, 0, 1) == 0)
						{
							this.preReadBuffer = null;
						}
					}
				}

				// Token: 0x17001D93 RID: 7571
				// (get) Token: 0x0600876B RID: 34667 RVA: 0x001F69D8 File Offset: 0x001F4BD8
				public override long ContentLength
				{
					get
					{
						return this.listenerHttpContext.listenerContext.Request.ContentLength64;
					}
				}

				// Token: 0x17001D94 RID: 7572
				// (get) Token: 0x0600876C RID: 34668 RVA: 0x001F69EF File Offset: 0x001F4BEF
				protected override string ContentTypeCore
				{
					get
					{
						if (this.cachedContentType == null)
						{
							this.cachedContentType = this.listenerHttpContext.listenerContext.Request.ContentType;
						}
						return this.cachedContentType;
					}
				}

				// Token: 0x17001D95 RID: 7573
				// (get) Token: 0x0600876D RID: 34669 RVA: 0x001F6A1A File Offset: 0x001F4C1A
				protected override bool HasContent
				{
					get
					{
						return this.preReadBuffer != null || this.ContentLength > 0L;
					}
				}

				// Token: 0x17001D96 RID: 7574
				// (get) Token: 0x0600876E RID: 34670 RVA: 0x001F6A30 File Offset: 0x001F4C30
				protected override string SoapActionHeader
				{
					get
					{
						return this.listenerHttpContext.listenerContext.Request.Headers["SOAPAction"];
					}
				}

				// Token: 0x17001D97 RID: 7575
				// (get) Token: 0x0600876F RID: 34671 RVA: 0x001F6A51 File Offset: 0x001F4C51
				protected override ChannelBinding ChannelBinding
				{
					get
					{
						return ChannelBindingUtility.GetToken(this.listenerHttpContext.listenerContext.Request.TransportContext);
					}
				}

				// Token: 0x06008770 RID: 34672 RVA: 0x001F6A70 File Offset: 0x001F4C70
				protected override void AddProperties(Message message)
				{
					HttpRequestMessageProperty httpRequestMessageProperty = new HttpRequestMessageProperty(this.listenerHttpContext);
					httpRequestMessageProperty.Method = this.listenerHttpContext.listenerContext.Request.HttpMethod;
					if (this.listenerHttpContext.listenerContext.Request.Url.Query.Length > 1)
					{
						httpRequestMessageProperty.QueryString = this.listenerHttpContext.listenerContext.Request.Url.Query.Substring(1);
					}
					message.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessageProperty);
					message.Properties.Via = this.listenerHttpContext.listenerContext.Request.Url;
					RemoteEndpointMessageProperty property = new RemoteEndpointMessageProperty(this.listenerHttpContext.listenerContext.Request.RemoteEndPoint);
					message.Properties.Add(RemoteEndpointMessageProperty.Name, property);
				}

				// Token: 0x06008771 RID: 34673 RVA: 0x001F6B4C File Offset: 0x001F4D4C
				public override void ConfigureHttpRequestMessage(HttpRequestMessage message)
				{
					message.Method = new HttpMethod(this.listenerHttpContext.listenerContext.Request.HttpMethod);
					message.RequestUri = this.listenerHttpContext.listenerContext.Request.Url;
					foreach (object obj in this.listenerHttpContext.listenerContext.Request.Headers.Keys)
					{
						string text = (string)obj;
						message.AddHeader(text, this.listenerHttpContext.listenerContext.Request.Headers[text]);
					}
					message.Properties.Add(RemoteEndpointMessageProperty.Name, new RemoteEndpointMessageProperty(this.listenerHttpContext.listenerContext.Request.RemoteEndPoint));
				}

				// Token: 0x06008772 RID: 34674 RVA: 0x001F6C3C File Offset: 0x001F4E3C
				protected override Stream GetInputStream()
				{
					if (this.preReadBuffer != null)
					{
						return new HttpRequestContext.ListenerHttpContext.ListenerContextHttpInput.ListenerContextInputStream(this.listenerHttpContext, this.preReadBuffer);
					}
					return new HttpRequestContext.ListenerHttpContext.ListenerContextHttpInput.ListenerContextInputStream(this.listenerHttpContext);
				}

				// Token: 0x04004ED3 RID: 20179
				private HttpRequestContext.ListenerHttpContext listenerHttpContext;

				// Token: 0x04004ED4 RID: 20180
				private string cachedContentType;

				// Token: 0x04004ED5 RID: 20181
				private byte[] preReadBuffer;

				// Token: 0x02000FC7 RID: 4039
				private class ListenerContextInputStream : HttpDelayedAcceptStream
				{
					// Token: 0x060088E1 RID: 35041 RVA: 0x001FDC9A File Offset: 0x001FBE9A
					public ListenerContextInputStream(HttpRequestContext.ListenerHttpContext listenerHttpContext) : base(listenerHttpContext.listenerContext.Request.InputStream)
					{
					}

					// Token: 0x060088E2 RID: 35042 RVA: 0x001FDCB2 File Offset: 0x001FBEB2
					public ListenerContextInputStream(HttpRequestContext.ListenerHttpContext listenerHttpContext, byte[] preReadBuffer) : base(new PreReadStream(listenerHttpContext.listenerContext.Request.InputStream, preReadBuffer))
					{
					}

					// Token: 0x060088E3 RID: 35043 RVA: 0x001FDCD0 File Offset: 0x001FBED0
					public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
					{
						IAsyncResult result;
						try
						{
							result = base.BeginRead(buffer, offset, count, callback, state);
						}
						catch (HttpListenerException listenerException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
						}
						return result;
					}

					// Token: 0x060088E4 RID: 35044 RVA: 0x001FDD10 File Offset: 0x001FBF10
					public override int EndRead(IAsyncResult result)
					{
						int result2;
						try
						{
							result2 = base.EndRead(result);
						}
						catch (HttpListenerException listenerException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
						}
						return result2;
					}

					// Token: 0x060088E5 RID: 35045 RVA: 0x001FDD4C File Offset: 0x001FBF4C
					public override int Read(byte[] buffer, int offset, int count)
					{
						int result;
						try
						{
							result = base.Read(buffer, offset, count);
						}
						catch (HttpListenerException listenerException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
						}
						return result;
					}

					// Token: 0x060088E6 RID: 35046 RVA: 0x001FDD88 File Offset: 0x001FBF88
					public override int ReadByte()
					{
						int result;
						try
						{
							result = base.ReadByte();
						}
						catch (HttpListenerException listenerException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateCommunicationException(listenerException));
						}
						return result;
					}
				}
			}
		}

		// Token: 0x02000D4F RID: 3407
		private class AcceptWebSocketAsyncResult : AsyncResult
		{
			// Token: 0x06007CE1 RID: 31969 RVA: 0x001D3540 File Offset: 0x001D1740
			public AcceptWebSocketAsyncResult(HttpRequestContext context, HttpResponseMessage response, string protocol, AsyncCallback callback, object state) : base(callback, state)
			{
				this.context = context;
				this.response = response;
				IAsyncResult result = this.context.AcceptWebSocketCore(response, protocol).AsAsyncResult(HttpRequestContext.AcceptWebSocketAsyncResult.onHandleAcceptWebSocketResult, this);
				if (this.gate.Unlock())
				{
					this.CompleteAcceptWebSocket(result);
					base.Complete(true);
				}
			}

			// Token: 0x06007CE2 RID: 31970 RVA: 0x001D35A4 File Offset: 0x001D17A4
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<HttpRequestContext.AcceptWebSocketAsyncResult>(result);
			}

			// Token: 0x06007CE3 RID: 31971 RVA: 0x001D35B0 File Offset: 0x001D17B0
			private static void HandleAcceptWebSocketResult(IAsyncResult result)
			{
				HttpRequestContext.AcceptWebSocketAsyncResult acceptWebSocketAsyncResult = (HttpRequestContext.AcceptWebSocketAsyncResult)result.AsyncState;
				if (!acceptWebSocketAsyncResult.gate.Signal())
				{
					return;
				}
				Exception exception = null;
				try
				{
					acceptWebSocketAsyncResult.CompleteAcceptWebSocket(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				acceptWebSocketAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007CE4 RID: 31972 RVA: 0x001D360C File Offset: 0x001D180C
			private void CompleteAcceptWebSocket(IAsyncResult result)
			{
				Task<WebSocketContext> task = result as Task<WebSocketContext>;
				if (task.IsFaulted)
				{
					this.context.OnAcceptWebSocketError();
					throw FxTrace.Exception.AsError<WebSocketException>(task.Exception);
				}
				if (task.IsCanceled)
				{
					this.context.OnAcceptWebSocketError();
					throw FxTrace.Exception.AsError(new TimeoutException(SR.GetString("AcceptWebSocketTimedOutError")));
				}
				this.context.SetReplySent();
				this.context.OnAcceptWebSocketSuccess(task.Result, this.response.RequestMessage);
			}

			// Token: 0x040047C1 RID: 18369
			private static AsyncCallback onHandleAcceptWebSocketResult = Fx.ThunkCallback(new AsyncCallback(HttpRequestContext.AcceptWebSocketAsyncResult.HandleAcceptWebSocketResult));

			// Token: 0x040047C2 RID: 18370
			private HttpRequestContext context;

			// Token: 0x040047C3 RID: 18371
			private SignalGate gate = new SignalGate();

			// Token: 0x040047C4 RID: 18372
			private HttpResponseMessage response;
		}
	}
}
