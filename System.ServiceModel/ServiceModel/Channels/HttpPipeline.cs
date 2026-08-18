using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000875 RID: 2165
	internal abstract class HttpPipeline
	{
		// Token: 0x060051F9 RID: 20985 RVA: 0x0012E02C File Offset: 0x0012C22C
		public HttpPipeline(HttpRequestContext httpRequestContext)
		{
			this.httpRequestContext = httpRequestContext;
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x0012E03B File Offset: 0x0012C23B
		public HttpInput HttpInput
		{
			get
			{
				if (this.httpInput == null)
				{
					this.httpInput = this.GetHttpInput();
				}
				return this.httpInput;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x0012E057 File Offset: 0x0012C257
		internal bool IsHttpInputInitialized
		{
			get
			{
				return this.httpInput != null;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x0012E062 File Offset: 0x0012C262
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				return this.httpRequestContext.EventTraceActivity;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060051FD RID: 20989 RVA: 0x0012E06F File Offset: 0x0012C26F
		protected HttpRequestContext HttpRequestContext
		{
			get
			{
				return this.httpRequestContext;
			}
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0012E077 File Offset: 0x0012C277
		public static HttpPipeline CreateHttpPipeline(HttpRequestContext httpRequestContext, TransportIntegrationHandler transportIntegrationHandler, bool isWebSocketTransport)
		{
			if (transportIntegrationHandler != null)
			{
				return HttpPipeline.NormalHttpPipeline.CreatePipeline(httpRequestContext, transportIntegrationHandler, isWebSocketTransport);
			}
			if (httpRequestContext.HttpMessagesSupported)
			{
				return new HttpPipeline.HttpMessageSupportedHttpPipeline(httpRequestContext);
			}
			return new HttpPipeline.EmptyHttpPipeline(httpRequestContext);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x0012E09C File Offset: 0x0012C29C
		public static HttpPipeline GetHttpPipeline(HttpRequestMessage httpRequestMessage)
		{
			object obj;
			if (!httpRequestMessage.Properties.TryGetValue("ServiceModel.HttpPipeline", out obj) || obj == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpPipelineMessagePropertyMissingError", new object[]
				{
					"ServiceModel.HttpPipeline"
				})));
			}
			HttpPipeline httpPipeline = obj as HttpPipeline;
			if (httpPipeline == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpPipelineMessagePropertyTypeError", new object[]
				{
					"ServiceModel.HttpPipeline",
					typeof(HttpPipeline)
				})));
			}
			return httpPipeline;
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x0012E128 File Offset: 0x0012C328
		public static void RemoveHttpPipeline(HttpRequestMessage httpRequestMessage)
		{
			httpRequestMessage.Properties.Remove("ServiceModel.HttpPipeline");
		}

		// Token: 0x06005201 RID: 20993
		public abstract Task<HttpResponseMessage> Dispatch(HttpRequestMessage httpRequestMessage);

		// Token: 0x06005202 RID: 20994
		public abstract void SendReply(Message message, TimeSpan timeout);

		// Token: 0x06005203 RID: 20995 RVA: 0x0012E13B File Offset: 0x0012C33B
		public virtual AsyncCompletionResult SendAsyncReply(Message message, Action<object, HttpResponseMessage> asyncSendCallback, object state)
		{
			this.TraceProcessResponseStop();
			return AsyncCompletionResult.Completed;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0012E144 File Offset: 0x0012C344
		public void Close()
		{
			if (Interlocked.Exchange(ref this.isClosed, 1) == 0)
			{
				this.OnClose();
			}
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0012E15A File Offset: 0x0012C35A
		public virtual void Cancel()
		{
			this.httpRequestContext.Abort();
		}

		// Token: 0x06005206 RID: 20998
		internal abstract IAsyncResult BeginProcessInboundRequest(ReplyChannelAcceptor replyChannelAcceptor, Action dequeuedCallback, AsyncCallback callback, object state);

		// Token: 0x06005207 RID: 20999
		internal abstract void EndProcessInboundRequest(IAsyncResult result);

		// Token: 0x06005208 RID: 21000
		protected abstract IAsyncResult BeginParseIncomingMessage(AsyncCallback asynCallback, object state);

		// Token: 0x06005209 RID: 21001
		protected abstract Message EndParseIncomingMesssage(IAsyncResult result, out Exception requestException);

		// Token: 0x0600520A RID: 21002
		protected abstract void OnParseComplete(Message message, Exception requestException);

		// Token: 0x0600520B RID: 21003 RVA: 0x0012E167 File Offset: 0x0012C367
		protected virtual void OnClose()
		{
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x0012E169 File Offset: 0x0012C369
		protected void TraceProcessInboundRequestStart()
		{
			if (TD.HttpPipelineProcessInboundRequestStartIsEnabled())
			{
				TD.HttpPipelineProcessInboundRequestStart(this.EventTraceActivity);
			}
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0012E17D File Offset: 0x0012C37D
		protected void TraceBeginProcessInboundRequestStart()
		{
			if (TD.HttpPipelineBeginProcessInboundRequestStartIsEnabled())
			{
				TD.HttpPipelineBeginProcessInboundRequestStart(this.EventTraceActivity);
			}
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0012E191 File Offset: 0x0012C391
		protected void TraceProcessInboundRequestStop()
		{
			if (TD.HttpPipelineProcessInboundRequestStopIsEnabled())
			{
				TD.HttpPipelineProcessInboundRequestStop(this.EventTraceActivity);
			}
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0012E1A5 File Offset: 0x0012C3A5
		protected void TraceProcessResponseStart()
		{
			if (TD.HttpPipelineProcessResponseStartIsEnabled())
			{
				TD.HttpPipelineProcessResponseStart(this.EventTraceActivity);
			}
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0012E1B9 File Offset: 0x0012C3B9
		protected void TraceBeginProcessResponseStart()
		{
			if (TD.HttpPipelineBeginProcessResponseStartIsEnabled())
			{
				TD.HttpPipelineBeginProcessResponseStart(this.EventTraceActivity);
			}
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x0012E1CD File Offset: 0x0012C3CD
		protected void TraceProcessResponseStop()
		{
			if (TD.HttpPipelineProcessResponseStopIsEnabled())
			{
				TD.HttpPipelineProcessResponseStop(this.EventTraceActivity);
			}
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x0012E1E1 File Offset: 0x0012C3E1
		protected virtual HttpInput GetHttpInput()
		{
			return this.httpRequestContext.GetHttpInput(true);
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0012E1EF File Offset: 0x0012C3EF
		protected HttpOutput GetHttpOutput(Message message)
		{
			return this.httpRequestContext.GetHttpOutputCore(message);
		}

		// Token: 0x04003236 RID: 12854
		private const string HttpPipelineKey = "ServiceModel.HttpPipeline";

		// Token: 0x04003237 RID: 12855
		private HttpRequestContext httpRequestContext;

		// Token: 0x04003238 RID: 12856
		private HttpInput httpInput;

		// Token: 0x04003239 RID: 12857
		private int isClosed;

		// Token: 0x02000D57 RID: 3415
		private class EmptyHttpPipeline : HttpPipeline
		{
			// Token: 0x06007D28 RID: 32040 RVA: 0x001D3F68 File Offset: 0x001D2168
			public EmptyHttpPipeline(HttpRequestContext httpRequestContext) : base(httpRequestContext)
			{
				if (this.httpRequestContext.Listener.RequestInitializationTimeout != HttpTransportDefaults.RequestInitializationTimeout)
				{
					this.requestInitializationTimer = new IOThreadTimer(HttpPipeline.EmptyHttpPipeline.onRequestInitializationTimeout, this, false);
					this.requestInitializationTimer.Set(this.httpRequestContext.Listener.RequestInitializationTimeout);
				}
			}

			// Token: 0x06007D29 RID: 32041 RVA: 0x001D3FC5 File Offset: 0x001D21C5
			public override void SendReply(Message message, TimeSpan timeout)
			{
				this.CancelRequestInitializationTimer();
				this.SendReplyCore(message, timeout);
			}

			// Token: 0x06007D2A RID: 32042 RVA: 0x001D3FD6 File Offset: 0x001D21D6
			public override Task<HttpResponseMessage> Dispatch(HttpRequestMessage httpRequestMessage)
			{
				throw FxTrace.Exception.AsError(new NotSupportedException());
			}

			// Token: 0x06007D2B RID: 32043 RVA: 0x001D3FE7 File Offset: 0x001D21E7
			internal override IAsyncResult BeginProcessInboundRequest(ReplyChannelAcceptor replyChannelAcceptor, Action dequeuedCallback, AsyncCallback callback, object state)
			{
				base.TraceBeginProcessInboundRequestStart();
				return new HttpPipeline.EnqueueMessageAsyncResult(replyChannelAcceptor, dequeuedCallback, this, callback, state);
			}

			// Token: 0x06007D2C RID: 32044 RVA: 0x001D3FFA File Offset: 0x001D21FA
			internal override void EndProcessInboundRequest(IAsyncResult result)
			{
				HttpPipeline.EnqueueMessageAsyncResult.End(result);
				base.TraceProcessInboundRequestStop();
			}

			// Token: 0x06007D2D RID: 32045 RVA: 0x001D4008 File Offset: 0x001D2208
			protected override IAsyncResult BeginParseIncomingMessage(AsyncCallback asynCallback, object state)
			{
				return base.HttpInput.BeginParseIncomingMessage(asynCallback, state);
			}

			// Token: 0x06007D2E RID: 32046 RVA: 0x001D4017 File Offset: 0x001D2217
			protected override Message EndParseIncomingMesssage(IAsyncResult result, out Exception requestException)
			{
				return base.HttpInput.EndParseIncomingMessage(result, out requestException);
			}

			// Token: 0x06007D2F RID: 32047 RVA: 0x001D4028 File Offset: 0x001D2228
			protected override void OnParseComplete(Message message, Exception requestException)
			{
				if (!this.CancelRequestInitializationTimer() && requestException == null)
				{
					requestException = FxTrace.Exception.AsError(new TimeoutException(SR.GetString("RequestInitializationTimeoutReached", new object[]
					{
						base.HttpRequestContext.Listener.RequestInitializationTimeout,
						"RequestInitializationTimeout",
						typeof(HttpTransportBindingElement).Name
					})));
				}
				base.HttpRequestContext.SetMessage(message, requestException);
			}

			// Token: 0x06007D30 RID: 32048 RVA: 0x001D40A0 File Offset: 0x001D22A0
			protected virtual void SendReplyCore(Message message, TimeSpan timeout)
			{
				base.TraceProcessResponseStart();
				ThreadTrace.Trace("Begin sending http reply");
				HttpOutput httpOutput = base.GetHttpOutput(message);
				httpOutput.Send(timeout);
				ThreadTrace.Trace("End sending http reply");
				base.TraceProcessResponseStop();
			}

			// Token: 0x06007D31 RID: 32049 RVA: 0x001D40DC File Offset: 0x001D22DC
			protected bool CancelRequestInitializationTimer()
			{
				if (this.requestInitializationTimer == null)
				{
					return true;
				}
				if (this.requestInitializationTimerCancelled)
				{
					return false;
				}
				bool result = this.requestInitializationTimer.Cancel();
				this.requestInitializationTimerCancelled = true;
				return result;
			}

			// Token: 0x06007D32 RID: 32050 RVA: 0x001D4111 File Offset: 0x001D2311
			protected override void OnClose()
			{
				this.CancelRequestInitializationTimer();
			}

			// Token: 0x06007D33 RID: 32051 RVA: 0x001D411C File Offset: 0x001D231C
			private static void OnRequestInitializationTimeout(object obj)
			{
				HttpPipeline httpPipeline = (HttpPipeline)obj;
				httpPipeline.Cancel();
			}

			// Token: 0x040047DE RID: 18398
			private static Action<object> onRequestInitializationTimeout = Fx.ThunkCallback<object>(new Action<object>(HttpPipeline.EmptyHttpPipeline.OnRequestInitializationTimeout));

			// Token: 0x040047DF RID: 18399
			private IOThreadTimer requestInitializationTimer;

			// Token: 0x040047E0 RID: 18400
			private bool requestInitializationTimerCancelled;
		}

		// Token: 0x02000D58 RID: 3416
		private class HttpMessageSupportedHttpPipeline : HttpPipeline.EmptyHttpPipeline
		{
			// Token: 0x06007D35 RID: 32053 RVA: 0x001D414E File Offset: 0x001D234E
			public HttpMessageSupportedHttpPipeline(HttpRequestContext httpRequestContext) : base(httpRequestContext)
			{
			}

			// Token: 0x17001BFE RID: 7166
			// (get) Token: 0x06007D36 RID: 32054 RVA: 0x001D4157 File Offset: 0x001D2357
			public HttpRequestMessageHttpInput HttpRequestMessageHttpInput
			{
				get
				{
					if (this.httpRequestMessageHttpInput == null)
					{
						this.httpRequestMessageHttpInput = (base.HttpInput as HttpRequestMessageHttpInput);
					}
					return this.httpRequestMessageHttpInput;
				}
			}

			// Token: 0x17001BFF RID: 7167
			// (get) Token: 0x06007D37 RID: 32055 RVA: 0x001D4178 File Offset: 0x001D2378
			public HttpRequestMessage HttpRequestMessage
			{
				get
				{
					return this.HttpRequestMessageHttpInput.HttpRequestMessage;
				}
			}

			// Token: 0x06007D38 RID: 32056 RVA: 0x001D4185 File Offset: 0x001D2385
			protected override IAsyncResult BeginParseIncomingMessage(AsyncCallback asynCallback, object state)
			{
				return this.HttpRequestMessageHttpInput.BeginParseIncomingMessage(this.HttpRequestMessage, asynCallback, state);
			}

			// Token: 0x06007D39 RID: 32057 RVA: 0x001D419C File Offset: 0x001D239C
			protected override void SendReplyCore(Message message, TimeSpan timeout)
			{
				base.TraceProcessResponseStart();
				ThreadTrace.Trace("Begin sending http reply");
				HttpOutput httpOutput = base.GetHttpOutput(message);
				HttpResponseMessage httpResponseMessageFromMessage = HttpResponseMessageProperty.GetHttpResponseMessageFromMessage(message);
				if (httpResponseMessageFromMessage != null)
				{
					httpOutput.Send(httpResponseMessageFromMessage, timeout);
				}
				else
				{
					httpOutput.Send(timeout);
				}
				ThreadTrace.Trace("End sending http reply");
				base.TraceProcessResponseStop();
			}

			// Token: 0x06007D3A RID: 32058 RVA: 0x001D41EC File Offset: 0x001D23EC
			protected override HttpInput GetHttpInput()
			{
				return base.GetHttpInput().CreateHttpRequestMessageInput();
			}

			// Token: 0x040047E1 RID: 18401
			private HttpRequestMessageHttpInput httpRequestMessageHttpInput;
		}

		// Token: 0x02000D59 RID: 3417
		private class NormalHttpPipeline : HttpPipeline
		{
			// Token: 0x06007D3B RID: 32059 RVA: 0x001D41F9 File Offset: 0x001D23F9
			public NormalHttpPipeline(HttpRequestContext httpRequestContext, TransportIntegrationHandler transportIntegrationHandler) : base(httpRequestContext)
			{
				this.defaultSendTimeout = httpRequestContext.DefaultSendTimeout;
				this.cancellationTokenSource = new HttpPipelineCancellationTokenSource(httpRequestContext);
				this.transportIntegrationHandler = transportIntegrationHandler;
			}

			// Token: 0x17001C00 RID: 7168
			// (get) Token: 0x06007D3C RID: 32060 RVA: 0x001D4233 File Offset: 0x001D2433
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x06007D3D RID: 32061 RVA: 0x001D423C File Offset: 0x001D243C
			public static HttpPipeline CreatePipeline(HttpRequestContext httpRequestContext, TransportIntegrationHandler transportIntegrationHandler, bool isWebSocketTransport)
			{
				HttpPipeline.NormalHttpPipeline normalHttpPipeline = isWebSocketTransport ? new HttpPipeline.NormalHttpPipeline.WebSocketHttpPipeline(httpRequestContext, transportIntegrationHandler) : new HttpPipeline.NormalHttpPipeline(httpRequestContext, transportIntegrationHandler);
				normalHttpPipeline.SetPipelineIncomingTimeout();
				return normalHttpPipeline;
			}

			// Token: 0x06007D3E RID: 32062 RVA: 0x001D4264 File Offset: 0x001D2464
			public override void SendReply(Message message, TimeSpan timeout)
			{
				base.TraceProcessResponseStart();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (!this.isShortCutResponse)
				{
					this.CompleteChannelModelIntegrationHandlerTask(message);
					bool flag = false;
					try
					{
						Monitor.TryEnter(this.ThisLock, TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime()), ref flag);
						if (!flag)
						{
							throw FxTrace.Exception.AsError(new TimeoutException(SR.GetString("TimeoutOnSend", new object[]
							{
								timeout
							})));
						}
						this.WaitTransportIntegrationHandlerTask(timeoutHelper.RemainingTime());
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(this.ThisLock);
						}
					}
					if (this.transportIntegrationHandlerTask.Result != null)
					{
						this.httpOutput.Send(this.transportIntegrationHandlerTask.Result, timeoutHelper.RemainingTime());
					}
				}
				base.TraceProcessResponseStop();
			}

			// Token: 0x06007D3F RID: 32063 RVA: 0x001D4338 File Offset: 0x001D2538
			public override AsyncCompletionResult SendAsyncReply(Message message, Action<object, HttpResponseMessage> asyncSendCallback, object state)
			{
				base.TraceBeginProcessResponseStart();
				this.isAsyncReply = true;
				this.asyncSendCallback = asyncSendCallback;
				this.asyncSendState = state;
				this.CompleteChannelModelIntegrationHandlerTask(message);
				return AsyncCompletionResult.Queued;
			}

			// Token: 0x06007D40 RID: 32064 RVA: 0x001D435D File Offset: 0x001D255D
			public override Task<HttpResponseMessage> Dispatch(HttpRequestMessage httpRequestMessage)
			{
				this.httpRequestMessage = httpRequestMessage;
				((HttpRequestMessageHttpInput)base.HttpInput).SetHttpRequestMessage(httpRequestMessage);
				this.channelModelIntegrationHandlerTask = new TaskCompletionSource<HttpResponseMessage>();
				ActionItem.Schedule(HttpPipeline.NormalHttpPipeline.onCreateMessageAndEnqueue, this);
				return this.channelModelIntegrationHandlerTask.Task;
			}

			// Token: 0x06007D41 RID: 32065 RVA: 0x001D4398 File Offset: 0x001D2598
			public override void Cancel()
			{
				this.cancellationTokenSource.Cancel();
			}

			// Token: 0x06007D42 RID: 32066 RVA: 0x001D43A8 File Offset: 0x001D25A8
			internal override IAsyncResult BeginProcessInboundRequest(ReplyChannelAcceptor replyChannelAcceptor, Action dequeuedCallback, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				try
				{
					this.wasProcessInboundRequestSuccessful = false;
					base.TraceProcessInboundRequestStart();
					this.replyChannelAcceptor = replyChannelAcceptor;
					this.dequeuedCallback = dequeuedCallback;
					HttpRequestMessageHttpInput httpRequestMessageHttpInput = (HttpRequestMessageHttpInput)base.HttpInput;
					this.httpRequestMessage = httpRequestMessageHttpInput.HttpRequestMessage;
					this.httpRequestMessage.Properties.Add("ServiceModel.HttpPipeline", this);
					object obj = this.ThisLock;
					lock (obj)
					{
						this.transportIntegrationHandlerTask = this.transportIntegrationHandler.ProcessPipelineAsync(this.httpRequestMessage, this.cancellationTokenSource.Token);
					}
					this.SendHttpPipelineResponse();
					base.TraceProcessInboundRequestStop();
					this.wasProcessInboundRequestSuccessful = true;
					result = new CompletedAsyncResult(callback, state);
				}
				catch (OperationCanceledException)
				{
					if (TD.HttpPipelineFaultedIsEnabled())
					{
						TD.HttpPipelineFaulted(base.EventTraceActivity);
					}
					this.cancellationTokenSource.Cancel();
					throw;
				}
				catch (Exception exception)
				{
					if (!Fx.IsFatal(exception))
					{
						if (TD.HttpPipelineFaultedIsEnabled())
						{
							TD.HttpPipelineFaulted(base.EventTraceActivity);
						}
						this.SendAndClose(HttpPipeline.NormalHttpPipeline.internalServerErrorHttpResponseMessage);
					}
					throw;
				}
				return result;
			}

			// Token: 0x06007D43 RID: 32067 RVA: 0x001D44D0 File Offset: 0x001D26D0
			internal override void EndProcessInboundRequest(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x06007D44 RID: 32068 RVA: 0x001D44D8 File Offset: 0x001D26D8
			protected override IAsyncResult BeginParseIncomingMessage(AsyncCallback asynCallback, object state)
			{
				return base.HttpInput.BeginParseIncomingMessage(this.httpRequestMessage, asynCallback, state);
			}

			// Token: 0x06007D45 RID: 32069 RVA: 0x001D44ED File Offset: 0x001D26ED
			protected override Message EndParseIncomingMesssage(IAsyncResult result, out Exception requestException)
			{
				return base.HttpInput.EndParseIncomingMessage(result, out requestException);
			}

			// Token: 0x06007D46 RID: 32070 RVA: 0x001D44FC File Offset: 0x001D26FC
			protected override void OnParseComplete(Message message, Exception requestException)
			{
				this.cancellationTokenSource.CancelAfter(-1);
				this.httpRequestContext.SetMessage(message, requestException);
				this.isShortCutResponse = false;
			}

			// Token: 0x06007D47 RID: 32071 RVA: 0x001D451E File Offset: 0x001D271E
			protected virtual void SetPipelineIncomingTimeout()
			{
				if (this.httpRequestContext.Listener.RequestInitializationTimeout != HttpTransportDefaults.RequestInitializationTimeout)
				{
					this.cancellationTokenSource.CancelAfter(this.httpRequestContext.Listener.RequestInitializationTimeout);
				}
			}

			// Token: 0x06007D48 RID: 32072 RVA: 0x001D4557 File Offset: 0x001D2757
			protected override void OnClose()
			{
				this.cancellationTokenSource.Dispose();
				if (this.isShortCutResponse && this.wasProcessInboundRequestSuccessful && this.dequeuedCallback != null)
				{
					this.dequeuedCallback();
				}
				base.OnClose();
			}

			// Token: 0x06007D49 RID: 32073 RVA: 0x001D4590 File Offset: 0x001D2790
			protected override HttpInput GetHttpInput()
			{
				HttpInput httpInput = base.GetHttpInput();
				return httpInput.CreateHttpRequestMessageInput();
			}

			// Token: 0x06007D4A RID: 32074 RVA: 0x001D45AA File Offset: 0x001D27AA
			protected virtual void SendHttpPipelineResponse()
			{
				this.transportIntegrationHandlerTask.ContinueWith(delegate(Task<HttpResponseMessage> t)
				{
					if (t.Result != null)
					{
						if (this.isShortCutResponse)
						{
							this.cancellationTokenSource.Dispose();
							this.wasProcessInboundRequestSuccessful = true;
							this.SendAndClose(t.Result);
							return;
						}
						if (this.isAsyncReply)
						{
							this.asyncSendCallback(this.asyncSendState, t.Result);
						}
					}
				}, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously);
			}

			// Token: 0x06007D4B RID: 32075 RVA: 0x001D45C9 File Offset: 0x001D27C9
			protected void SendAndClose(HttpResponseMessage httpResponseMessage)
			{
				base.HttpRequestContext.SendResponseAndClose(httpResponseMessage);
			}

			// Token: 0x06007D4C RID: 32076 RVA: 0x001D45D8 File Offset: 0x001D27D8
			private static void OnCreateMessageAndEnqueue(object state)
			{
				try
				{
					HttpPipeline.NormalHttpPipeline normalHttpPipeline = (HttpPipeline.NormalHttpPipeline)state;
					normalHttpPipeline.CreateMessageAndEnqueue();
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

			// Token: 0x06007D4D RID: 32077 RVA: 0x001D4618 File Offset: 0x001D2818
			private static void OnEnqueued(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				try
				{
					HttpPipeline.EnqueueMessageAsyncResult.End(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					FxTrace.Exception.TraceHandledException(exception, TraceEventType.Error);
				}
			}

			// Token: 0x06007D4E RID: 32078 RVA: 0x001D4660 File Offset: 0x001D2860
			private void CreateMessageAndEnqueue()
			{
				bool flag = false;
				try
				{
					IAsyncResult asyncResult = new HttpPipeline.EnqueueMessageAsyncResult(this.replyChannelAcceptor, this.dequeuedCallback, this, HttpPipeline.NormalHttpPipeline.onEnqueued, this);
					if (asyncResult.CompletedSynchronously)
					{
						HttpPipeline.EnqueueMessageAsyncResult.End(asyncResult);
					}
					flag = true;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					FxTrace.Exception.TraceUnhandledException(exception);
				}
				if (!flag)
				{
					this.SendAndClose(HttpPipeline.NormalHttpPipeline.internalServerErrorHttpResponseMessage);
				}
			}

			// Token: 0x06007D4F RID: 32079 RVA: 0x001D46D0 File Offset: 0x001D28D0
			private HttpResponseMessage CreateHttpResponseMessage(Message message)
			{
				HttpResponseMessage httpResponseMessage = HttpResponseMessageProperty.GetHttpResponseMessageFromMessage(message);
				if (httpResponseMessage == null)
				{
					HttpResponseMessageProperty value = message.Properties.GetValue<HttpResponseMessageProperty>(HttpResponseMessageProperty.Name);
					httpResponseMessage = new HttpResponseMessage();
					httpResponseMessage.StatusCode = (message.IsFault ? HttpStatusCode.InternalServerError : HttpStatusCode.OK);
					this.httpOutput.ConfigureHttpResponseMessage(message, httpResponseMessage, value);
				}
				return httpResponseMessage;
			}

			// Token: 0x06007D50 RID: 32080 RVA: 0x001D4728 File Offset: 0x001D2928
			private void CompleteChannelModelIntegrationHandlerTask(Message replyMessage)
			{
				if (this.channelModelIntegrationHandlerTask != null)
				{
					this.httpOutput = base.GetHttpOutput(replyMessage);
					HttpResponseMessage httpResponseMessage;
					if (replyMessage != null)
					{
						httpResponseMessage = this.CreateHttpResponseMessage(replyMessage);
					}
					else
					{
						httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
					}
					if (httpResponseMessage.RequestMessage == null)
					{
						httpResponseMessage.RequestMessage = this.httpRequestMessage;
						if (replyMessage != null)
						{
							httpResponseMessage.CopyPropertiesFromMessage(replyMessage);
						}
					}
					HttpChannelUtilities.EnsureHttpResponseMessageContentNotNull(httpResponseMessage);
					this.cancellationTokenSource.CancelAfter(TimeoutHelper.ToMilliseconds(this.defaultSendTimeout));
					this.channelModelIntegrationHandlerTask.TrySetResult(httpResponseMessage);
				}
				base.TraceProcessResponseStop();
			}

			// Token: 0x06007D51 RID: 32081 RVA: 0x001D47B1 File Offset: 0x001D29B1
			private void WaitTransportIntegrationHandlerTask(TimeSpan timeout)
			{
				this.transportIntegrationHandlerTask.Wait(timeout, null, null);
				this.wasProcessInboundRequestSuccessful = true;
			}

			// Token: 0x040047E2 RID: 18402
			private static readonly HttpResponseMessage internalServerErrorHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);

			// Token: 0x040047E3 RID: 18403
			private static Action<object> onCreateMessageAndEnqueue = Fx.ThunkCallback<object>(new Action<object>(HttpPipeline.NormalHttpPipeline.OnCreateMessageAndEnqueue));

			// Token: 0x040047E4 RID: 18404
			private static AsyncCallback onEnqueued = Fx.ThunkCallback(new AsyncCallback(HttpPipeline.NormalHttpPipeline.OnEnqueued));

			// Token: 0x040047E5 RID: 18405
			private HttpRequestMessage httpRequestMessage;

			// Token: 0x040047E6 RID: 18406
			private TransportIntegrationHandler transportIntegrationHandler;

			// Token: 0x040047E7 RID: 18407
			private Task<HttpResponseMessage> transportIntegrationHandlerTask;

			// Token: 0x040047E8 RID: 18408
			private TaskCompletionSource<HttpResponseMessage> channelModelIntegrationHandlerTask;

			// Token: 0x040047E9 RID: 18409
			private ReplyChannelAcceptor replyChannelAcceptor;

			// Token: 0x040047EA RID: 18410
			private Action dequeuedCallback;

			// Token: 0x040047EB RID: 18411
			private bool isShortCutResponse = true;

			// Token: 0x040047EC RID: 18412
			private bool wasProcessInboundRequestSuccessful;

			// Token: 0x040047ED RID: 18413
			private bool isAsyncReply;

			// Token: 0x040047EE RID: 18414
			private TimeSpan defaultSendTimeout;

			// Token: 0x040047EF RID: 18415
			private HttpOutput httpOutput;

			// Token: 0x040047F0 RID: 18416
			private object thisLock = new object();

			// Token: 0x040047F1 RID: 18417
			private HttpPipelineCancellationTokenSource cancellationTokenSource;

			// Token: 0x040047F2 RID: 18418
			private Action<object, HttpResponseMessage> asyncSendCallback;

			// Token: 0x040047F3 RID: 18419
			private object asyncSendState;

			// Token: 0x02000F5F RID: 3935
			private class WebSocketHttpPipeline : HttpPipeline.NormalHttpPipeline
			{
				// Token: 0x0600877C RID: 34684 RVA: 0x001F6F36 File Offset: 0x001F5136
				public WebSocketHttpPipeline(HttpRequestContext httpRequestContext, TransportIntegrationHandler transportIntegrationHandler) : base(httpRequestContext, transportIntegrationHandler)
				{
				}

				// Token: 0x0600877D RID: 34685 RVA: 0x001F6F40 File Offset: 0x001F5140
				public override void SendReply(Message message, TimeSpan timeout)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.httpOutput = base.GetHttpOutput(message);
					this.httpOutput.Send(timeoutHelper.RemainingTime());
				}

				// Token: 0x0600877E RID: 34686 RVA: 0x001F6F74 File Offset: 0x001F5174
				protected override void SetPipelineIncomingTimeout()
				{
					this.cancellationTokenSource.CancelAfter(TimeoutHelper.ToMilliseconds(((IDefaultCommunicationTimeouts)this.httpRequestContext.Listener).OpenTimeout));
				}

				// Token: 0x0600877F RID: 34687 RVA: 0x001F6F98 File Offset: 0x001F5198
				protected override void SendHttpPipelineResponse()
				{
					base.WaitTransportIntegrationHandlerTask(this.defaultSendTimeout);
					HttpResponseMessage result = this.transportIntegrationHandlerTask.Result;
					if (result == null)
					{
						this.cancellationTokenSource.Cancel();
						return;
					}
					if (result.StatusCode == HttpStatusCode.SwitchingProtocols)
					{
						string subProtocol = null;
						if (result.Headers.Contains("Sec-WebSocket-Protocol"))
						{
							using (IEnumerator<string> enumerator = result.Headers.GetValues("Sec-WebSocket-Protocol").GetEnumerator())
							{
								if (enumerator.MoveNext())
								{
									string text = enumerator.Current;
									subProtocol = text;
								}
							}
							result.Headers.Remove("Sec-WebSocket-Protocol");
						}
						if (result.RequestMessage != null)
						{
							HttpPipeline.RemoveHttpPipeline(result.RequestMessage);
							result.RequestMessage.Properties.Remove(RemoteEndpointMessageProperty.Name);
						}
						this.isShortCutResponse = false;
						bool flag;
						try
						{
							flag = base.HttpRequestContext.Listener.CreateWebSocketChannelAndEnqueue(base.HttpRequestContext, this, result, subProtocol, this.dequeuedCallback);
						}
						catch (Exception ex)
						{
							if (!Fx.IsFatal(ex))
							{
								if (TD.WebSocketConnectionFailedIsEnabled())
								{
									TD.WebSocketConnectionFailed(base.EventTraceActivity, ex.Message);
								}
								base.HttpRequestContext.SendResponseAndClose(HttpStatusCode.InternalServerError);
							}
							throw;
						}
						this.isShortCutResponse = !flag;
						if (!flag)
						{
							if (TD.WebSocketConnectionDeclinedIsEnabled())
							{
								TD.WebSocketConnectionDeclined(base.EventTraceActivity, HttpStatusCode.ServiceUnavailable.ToString());
							}
							this.httpRequestContext.SendResponseAndClose(HttpStatusCode.ServiceUnavailable);
							return;
						}
					}
					else
					{
						if (TD.WebSocketConnectionDeclinedIsEnabled())
						{
							TD.WebSocketConnectionDeclined(base.EventTraceActivity, result.StatusCode.ToString());
						}
						base.SendAndClose(result);
					}
				}
			}
		}

		// Token: 0x02000D5A RID: 3418
		private class EnqueueMessageAsyncResult : TraceAsyncResult
		{
			// Token: 0x06007D54 RID: 32084 RVA: 0x001D4864 File Offset: 0x001D2A64
			public EnqueueMessageAsyncResult(ReplyChannelAcceptor acceptor, Action dequeuedCallback, HttpPipeline pipeline, AsyncCallback callback, object state) : base(callback, state)
			{
				this.pipeline = pipeline;
				this.acceptor = acceptor;
				this.dequeuedCallback = dequeuedCallback;
				AsyncCallback asynCallback = base.PrepareAsyncCompletion(new AsyncResult.AsyncCompletion(HttpPipeline.EnqueueMessageAsyncResult.HandleParseIncomingMessage));
				IAsyncResult result = this.pipeline.BeginParseIncomingMessage(asynCallback, this);
				base.SyncContinue(result);
			}

			// Token: 0x06007D55 RID: 32085 RVA: 0x001D48B9 File Offset: 0x001D2AB9
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<HttpPipeline.EnqueueMessageAsyncResult>(result);
			}

			// Token: 0x06007D56 RID: 32086 RVA: 0x001D48C4 File Offset: 0x001D2AC4
			private static bool HandleParseIncomingMessage(IAsyncResult result)
			{
				HttpPipeline.EnqueueMessageAsyncResult enqueueMessageAsyncResult = (HttpPipeline.EnqueueMessageAsyncResult)result.AsyncState;
				enqueueMessageAsyncResult.CompleteParseAndEnqueue(result);
				return true;
			}

			// Token: 0x06007D57 RID: 32087 RVA: 0x001D48E8 File Offset: 0x001D2AE8
			private void CompleteParseAndEnqueue(IAsyncResult result)
			{
				using (DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.BoundOperation(base.CallbackActivity) : null)
				{
					Exception ex;
					Message message = this.pipeline.EndParseIncomingMesssage(result, out ex);
					if (message == null && ex == null)
					{
						throw FxTrace.Exception.AsError(new ProtocolException(SR.GetString("MessageXmlProtocolError"), new XmlException(SR.GetString("MessageIsEmpty"))));
					}
					this.pipeline.OnParseComplete(message, ex);
					this.acceptor.Enqueue(this.pipeline.HttpRequestContext, this.dequeuedCallback, true);
				}
			}

			// Token: 0x040047F4 RID: 18420
			private HttpPipeline pipeline;

			// Token: 0x040047F5 RID: 18421
			private ReplyChannelAcceptor acceptor;

			// Token: 0x040047F6 RID: 18422
			private Action dequeuedCallback;
		}
	}
}
