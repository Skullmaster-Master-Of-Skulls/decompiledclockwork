using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000869 RID: 2153
	internal class HttpChannelListener<TChannel> : HttpChannelListener, IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x0600513D RID: 20797 RVA: 0x0012AF4C File Offset: 0x0012914C
		public HttpChannelListener(HttpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.useWebSocketTransport = (bindingElement.WebSocketSettings.TransportUsage == WebSocketTransportUsage.Always || (bindingElement.WebSocketSettings.TransportUsage == WebSocketTransportUsage.WhenDuplex && typeof(TChannel) != typeof(IReplyChannel)));
			if (this.useWebSocketTransport)
			{
				if (AspNetEnvironment.Enabled)
				{
					AspNetEnvironment aspNetEnvironment = AspNetEnvironment.Current;
					if (!aspNetEnvironment.UsingIntegratedPipeline)
					{
						throw FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("WebSocketsNotSupportedInClassicPipeline")));
					}
				}
				else if (!WebSocketHelper.OSSupportsWebSockets())
				{
					throw FxTrace.Exception.AsError(new PlatformNotSupportedException(SR.GetString("WebSocketsServerSideNotSupported")));
				}
				this.currentWebSocketVersion = WebSocketHelper.GetCurrentVersion();
				this.acceptor = new InputQueueChannelAcceptor<TChannel>(this);
				int bufferSize = WebSocketHelper.ComputeServerBufferSize(bindingElement.MaxReceivedMessageSize);
				this.bufferPool = new ConnectionBufferPool(bufferSize);
				this.webSocketLifetimeManager = new CommunicationObjectManager<ServerWebSocketTransportDuplexSessionChannel>(base.ThisLock);
			}
			else
			{
				this.acceptor = (InputQueueChannelAcceptor<TChannel>)new TransportReplyChannelAcceptor(this);
			}
			this.CreatePipeline(bindingElement.MessageHandlerFactory);
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x0012B05B File Offset: 0x0012925B
		public override bool UseWebSocketTransport
		{
			get
			{
				return this.useWebSocketTransport;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x0600513F RID: 20799 RVA: 0x0012B063 File Offset: 0x00129263
		public InputQueueChannelAcceptor<TChannel> Acceptor
		{
			get
			{
				return this.acceptor;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06005140 RID: 20800 RVA: 0x0012B06B File Offset: 0x0012926B
		public override string Method
		{
			get
			{
				if (this.UseWebSocketTransport)
				{
					return "WEBSOCKET";
				}
				return base.Method;
			}
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0012B081 File Offset: 0x00129281
		public TChannel AcceptChannel()
		{
			return this.AcceptChannel(this.DefaultReceiveTimeout);
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x0012B08F File Offset: 0x0012928F
		public IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(this.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0012B09F File Offset: 0x0012929F
		public TChannel AcceptChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			return this.Acceptor.AcceptChannel(timeout);
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0012B0B3 File Offset: 0x001292B3
		public IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.Acceptor.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0012B0C9 File Offset: 0x001292C9
		public TChannel EndAcceptChannel(IAsyncResult result)
		{
			base.ThrowPending();
			return this.Acceptor.EndAcceptChannel(result);
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x0012B0E0 File Offset: 0x001292E0
		public override bool CreateWebSocketChannelAndEnqueue(HttpRequestContext httpRequestContext, HttpPipeline pipeline, HttpResponseMessage httpResponseMessage, string subProtocol, Action dequeuedCallback)
		{
			if (this.Acceptor.PendingCount >= base.WebSocketSettings.MaxPendingConnections)
			{
				if (TD.MaxPendingConnectionsExceededIsEnabled())
				{
					TD.MaxPendingConnectionsExceeded(SR.GetString("WebSocketMaxPendingConnectionsReached", new object[]
					{
						base.WebSocketSettings.MaxPendingConnections,
						"MaxPendingConnections",
						"WebSocketTransportSettings"
					}));
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 262180, SR.GetString("WebSocketMaxPendingConnectionsReached", new object[]
					{
						base.WebSocketSettings.MaxPendingConnections,
						"MaxPendingConnections",
						"WebSocketTransportSettings"
					}), new StringTraceRecord("MaxPendingConnections", base.WebSocketSettings.MaxPendingConnections.ToString(CultureInfo.InvariantCulture)), this, null);
				}
				return false;
			}
			ServerWebSocketTransportDuplexSessionChannel serverWebSocketTransportDuplexSessionChannel = new ServerWebSocketTransportDuplexSessionChannel(this, new EndpointAddress(this.Uri, new AddressHeader[0]), this.Uri, this.bufferPool, httpRequestContext, pipeline, httpResponseMessage, subProtocol);
			httpRequestContext.WebSocketChannel = serverWebSocketTransportDuplexSessionChannel;
			this.webSocketLifetimeManager.Add(serverWebSocketTransportDuplexSessionChannel);
			this.Acceptor.EnqueueAndDispatch((TChannel)((object)serverWebSocketTransportDuplexSessionChannel), dequeuedCallback, true);
			return true;
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x0012B203 File Offset: 0x00129403
		public override byte[] TakeWebSocketInternalBuffer()
		{
			return this.bufferPool.Take();
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x0012B210 File Offset: 0x00129410
		public override void ReturnWebSocketInternalBuffer(byte[] buffer)
		{
			this.bufferPool.Return(buffer);
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x0012B220 File Offset: 0x00129420
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.Acceptor
			});
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x0012B25C File Offset: 0x0012945C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.Acceptor.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0012B290 File Offset: 0x00129490
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0012B298 File Offset: 0x00129498
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.Acceptor.Close(timeoutHelper.RemainingTime());
			if (base.IsAuthenticationSupported)
			{
				base.CloseUserNameTokenAuthenticator(timeoutHelper.RemainingTime());
			}
			if (this.useWebSocketTransport)
			{
				this.webSocketLifetimeManager.Close(timeoutHelper.RemainingTime());
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0012B2FC File Offset: 0x001294FC
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			ICommunicationObject communicationObject = base.UserNameTokenAuthenticator as ICommunicationObject;
			ICommunicationObject[] array;
			if (communicationObject == null)
			{
				if (base.IsAuthenticationSupported)
				{
					base.CloseUserNameTokenAuthenticator(timeoutHelper.RemainingTime());
				}
				array = new ICommunicationObject[]
				{
					this.Acceptor
				};
			}
			else
			{
				array = new ICommunicationObject[]
				{
					this.Acceptor,
					communicationObject
				};
			}
			if (this.useWebSocketTransport)
			{
				return new HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<ServerWebSocketTransportDuplexSessionChannel>(timeoutHelper.RemainingTime(), callback, state, this.webSocketLifetimeManager, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), array);
			}
			return new ChainedCloseAsyncResult(timeoutHelper.RemainingTime(), callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), array);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x0012B3B8 File Offset: 0x001295B8
		protected override void OnEndClose(IAsyncResult result)
		{
			if (this.useWebSocketTransport)
			{
				HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<ServerWebSocketTransportDuplexSessionChannel>.End(result);
				return;
			}
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x0012B3CF File Offset: 0x001295CF
		protected override void OnClosed()
		{
			base.OnClosed();
			if (this.bufferPool != null)
			{
				this.bufferPool.Close();
			}
			if (this.transportIntegrationHandler != null)
			{
				this.transportIntegrationHandler.Dispose();
			}
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x0012B3FD File Offset: 0x001295FD
		protected override void OnAbort()
		{
			if (base.IsAuthenticationSupported)
			{
				base.AbortUserNameTokenAuthenticator();
			}
			this.Acceptor.Abort();
			if (this.useWebSocketTransport)
			{
				this.webSocketLifetimeManager.Abort();
			}
			base.OnAbort();
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0012B431 File Offset: 0x00129631
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.Acceptor.WaitForChannel(timeout);
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x0012B43F File Offset: 0x0012963F
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.Acceptor.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x0012B44F File Offset: 0x0012964F
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.Acceptor.EndWaitForChannel(result);
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x0012B45D File Offset: 0x0012965D
		internal override IAsyncResult BeginHttpContextReceived(HttpRequestContext context, Action acceptorCallback, AsyncCallback callback, object state)
		{
			return new HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TChannel>(context, acceptorCallback, this, callback, state);
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x0012B46A File Offset: 0x0012966A
		internal override bool EndHttpContextReceived(IAsyncResult result)
		{
			return HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TChannel>.End(result);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0012B474 File Offset: 0x00129674
		private void CreatePipeline(HttpMessageHandlerFactory httpMessageHandlerFactory)
		{
			HttpMessageHandler httpMessageHandler;
			if (this.UseWebSocketTransport)
			{
				httpMessageHandler = new DefaultWebSocketConnectionHandler(base.WebSocketSettings.SubProtocol, this.currentWebSocketVersion, base.MessageVersion, base.MessageEncoderFactory, base.TransferMode);
				if (httpMessageHandlerFactory != null)
				{
					httpMessageHandler = httpMessageHandlerFactory.Create(httpMessageHandler);
				}
			}
			else
			{
				if (httpMessageHandlerFactory == null)
				{
					return;
				}
				httpMessageHandler = httpMessageHandlerFactory.Create(new ChannelModelIntegrationHandler());
			}
			if (httpMessageHandler == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpMessageHandlerChannelFactoryNullPipeline", new object[]
				{
					httpMessageHandlerFactory.GetType().Name,
					typeof(HttpRequestContext).Name
				})));
			}
			this.transportIntegrationHandler = new TransportIntegrationHandler(httpMessageHandler);
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x0012B520 File Offset: 0x00129720
		private static void HandleProcessInboundException(Exception ex, HttpRequestContext context)
		{
			if (Fx.IsFatal(ex))
			{
				return;
			}
			if (ex is ProtocolException)
			{
				ProtocolException ex2 = (ProtocolException)ex;
				HttpStatusCode statusCode = HttpStatusCode.BadRequest;
				string statusDescription = string.Empty;
				if (ex2.Data.Contains("System.ServiceModel.Channels.HttpInput.HttpStatusCode"))
				{
					statusCode = (HttpStatusCode)ex2.Data["System.ServiceModel.Channels.HttpInput.HttpStatusCode"];
					ex2.Data.Remove("System.ServiceModel.Channels.HttpInput.HttpStatusCode");
				}
				if (ex2.Data.Contains("System.ServiceModel.Channels.HttpInput.HttpStatusDescription"))
				{
					statusDescription = (string)ex2.Data["System.ServiceModel.Channels.HttpInput.HttpStatusDescription"];
					ex2.Data.Remove("System.ServiceModel.Channels.HttpInput.HttpStatusDescription");
				}
				context.SendResponseAndClose(statusCode, statusDescription);
				return;
			}
			try
			{
				context.SendResponseAndClose(HttpStatusCode.BadRequest);
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

		// Token: 0x06005158 RID: 20824 RVA: 0x0012B600 File Offset: 0x00129800
		private static bool ContextReceiveExceptionHandled(Exception e)
		{
			if (Fx.IsFatal(e))
			{
				return false;
			}
			if (e is CommunicationException)
			{
				DiagnosticUtility.TraceHandledException(e, TraceEventType.Information);
			}
			else if (e is XmlException)
			{
				DiagnosticUtility.TraceHandledException(e, TraceEventType.Information);
			}
			else if (e is IOException)
			{
				DiagnosticUtility.TraceHandledException(e, TraceEventType.Information);
			}
			else if (e is TimeoutException)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(e.Message);
				}
				DiagnosticUtility.TraceHandledException(e, TraceEventType.Information);
			}
			else if (e is OperationCanceledException)
			{
				DiagnosticUtility.TraceHandledException(e, TraceEventType.Information);
			}
			else if (!ExceptionHandler.HandleTransportExceptionHelper(e))
			{
				return false;
			}
			return true;
		}

		// Token: 0x04003206 RID: 12806
		private InputQueueChannelAcceptor<TChannel> acceptor;

		// Token: 0x04003207 RID: 12807
		private bool useWebSocketTransport;

		// Token: 0x04003208 RID: 12808
		private CommunicationObjectManager<ServerWebSocketTransportDuplexSessionChannel> webSocketLifetimeManager;

		// Token: 0x04003209 RID: 12809
		private TransportIntegrationHandler transportIntegrationHandler;

		// Token: 0x0400320A RID: 12810
		private ConnectionBufferPool bufferPool;

		// Token: 0x0400320B RID: 12811
		private string currentWebSocketVersion;

		// Token: 0x02000D4A RID: 3402
		private class HttpContextReceivedAsyncResult<TListenerChannel> : TraceAsyncResult where TListenerChannel : class, IChannel
		{
			// Token: 0x06007CBD RID: 31933 RVA: 0x001D2A64 File Offset: 0x001D0C64
			public HttpContextReceivedAsyncResult(HttpRequestContext requestContext, Action acceptorCallback, HttpChannelListener<TListenerChannel> listener, AsyncCallback callback, object state) : base(callback, state)
			{
				this.context = requestContext;
				this.acceptorCallback = acceptorCallback;
				this.listener = listener;
				if (this.ProcessHttpContextAsync() == AsyncCompletionResult.Completed)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007CBE RID: 31934 RVA: 0x001D2A95 File Offset: 0x001D0C95
			public static bool End(IAsyncResult result)
			{
				return AsyncResult.End<HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TListenerChannel>>(result).enqueued;
			}

			// Token: 0x06007CBF RID: 31935 RVA: 0x001D2AA4 File Offset: 0x001D0CA4
			private static void OnProcessInboundRequest(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TListenerChannel> httpContextReceivedAsyncResult = (HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TListenerChannel>)result.AsyncState;
				Exception exception = null;
				try
				{
					httpContextReceivedAsyncResult.HandleProcessInboundRequest(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				httpContextReceivedAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007CC0 RID: 31936 RVA: 0x001D2AF8 File Offset: 0x001D0CF8
			private AsyncCompletionResult ProcessHttpContextAsync()
			{
				bool flag = false;
				try
				{
					this.context.InitializeHttpPipeline(this.listener.transportIntegrationHandler);
					if (!this.Authenticate())
					{
						return AsyncCompletionResult.Completed;
					}
					if (this.listener.UseWebSocketTransport && !this.context.IsWebSocketRequest)
					{
						this.context.SendResponseAndClose(HttpStatusCode.BadRequest, SR.GetString("WebSocketEndpointOnlySupportWebSocketError"));
						return AsyncCompletionResult.Completed;
					}
					if (!this.listener.UseWebSocketTransport && this.context.IsWebSocketRequest)
					{
						this.context.SendResponseAndClose(HttpStatusCode.BadRequest, SR.GetString("WebSocketEndpointDoesNotSupportWebSocketError"));
						return AsyncCompletionResult.Completed;
					}
					try
					{
						IAsyncResult asyncResult = this.context.BeginProcessInboundRequest(this.listener.Acceptor as ReplyChannelAcceptor, this.acceptorCallback, HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TListenerChannel>.onProcessInboundRequest, this);
						if (asyncResult.CompletedSynchronously)
						{
							this.EndInboundProcessAndEnqueue(asyncResult);
							return AsyncCompletionResult.Completed;
						}
					}
					catch (Exception ex)
					{
						HttpChannelListener<TChannel>.HandleProcessInboundException(ex, this.context);
						throw;
					}
				}
				catch (Exception e)
				{
					flag = true;
					if (!HttpChannelListener<TChannel>.ContextReceiveExceptionHandled(e))
					{
						throw;
					}
				}
				finally
				{
					if (flag)
					{
						this.context.Abort();
					}
				}
				if (!flag)
				{
					return AsyncCompletionResult.Queued;
				}
				return AsyncCompletionResult.Completed;
			}

			// Token: 0x06007CC1 RID: 31937 RVA: 0x001D2C40 File Offset: 0x001D0E40
			private bool Authenticate()
			{
				if (!this.context.ProcessAuthentication())
				{
					if (TD.HttpAuthFailedIsEnabled())
					{
						TD.HttpAuthFailed(this.context.EventTraceActivity);
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						TraceUtility.TraceEvent(TraceEventType.Information, 262183, SR.GetString("TraceCodeHttpAuthFailed"), this);
					}
					return false;
				}
				return true;
			}

			// Token: 0x06007CC2 RID: 31938 RVA: 0x001D2C94 File Offset: 0x001D0E94
			private void HandleProcessInboundRequest(IAsyncResult result)
			{
				bool flag = true;
				try
				{
					try
					{
						this.EndInboundProcessAndEnqueue(result);
						flag = false;
					}
					catch (Exception ex)
					{
						HttpChannelListener<TChannel>.HandleProcessInboundException(ex, this.context);
						throw;
					}
				}
				catch (Exception e)
				{
					if (!HttpChannelListener<TChannel>.ContextReceiveExceptionHandled(e))
					{
						throw;
					}
				}
				finally
				{
					if (flag)
					{
						this.context.Abort();
					}
				}
			}

			// Token: 0x06007CC3 RID: 31939 RVA: 0x001D2D04 File Offset: 0x001D0F04
			private void EndInboundProcessAndEnqueue(IAsyncResult result)
			{
				this.context.EndProcessInboundRequest(result);
				this.enqueued = true;
			}

			// Token: 0x040047AB RID: 18347
			private static AsyncCallback onProcessInboundRequest = Fx.ThunkCallback(new AsyncCallback(HttpChannelListener<TChannel>.HttpContextReceivedAsyncResult<TListenerChannel>.OnProcessInboundRequest));

			// Token: 0x040047AC RID: 18348
			private bool enqueued;

			// Token: 0x040047AD RID: 18349
			private HttpRequestContext context;

			// Token: 0x040047AE RID: 18350
			private Action acceptorCallback;

			// Token: 0x040047AF RID: 18351
			private HttpChannelListener<TListenerChannel> listener;
		}

		// Token: 0x02000D4B RID: 3403
		private class LifetimeWrappedCloseAsyncResult<TCommunicationObject> : AsyncResult where TCommunicationObject : CommunicationObject
		{
			// Token: 0x06007CC5 RID: 31941 RVA: 0x001D2D34 File Offset: 0x001D0F34
			public LifetimeWrappedCloseAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, CommunicationObjectManager<TCommunicationObject> communicationObjectManager, ChainedBeginHandler begin1, ChainedEndHandler end1, ICommunicationObject[] communicationObjects) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.begin1 = begin1;
				this.end1 = end1;
				this.communicationObjects = communicationObjects;
				this.communicationObjectManager = communicationObjectManager;
				IAsyncResult result = communicationObjectManager.BeginClose(this.timeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject>.handleLifetimeManagerClose), this);
				bool flag = base.SyncContinue(result);
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007CC6 RID: 31942 RVA: 0x001D2DA6 File Offset: 0x001D0FA6
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject>>(result);
			}

			// Token: 0x06007CC7 RID: 31943 RVA: 0x001D2DB0 File Offset: 0x001D0FB0
			private static bool HandleLifetimeManagerClose(IAsyncResult result)
			{
				HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject> lifetimeWrappedCloseAsyncResult = (HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject>)result.AsyncState;
				lifetimeWrappedCloseAsyncResult.communicationObjectManager.EndClose(result);
				ChainedCloseAsyncResult result2 = new ChainedCloseAsyncResult(lifetimeWrappedCloseAsyncResult.timeoutHelper.RemainingTime(), lifetimeWrappedCloseAsyncResult.PrepareAsyncCompletion(HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject>.handleChannelClose), lifetimeWrappedCloseAsyncResult, lifetimeWrappedCloseAsyncResult.begin1, lifetimeWrappedCloseAsyncResult.end1, lifetimeWrappedCloseAsyncResult.communicationObjects);
				return lifetimeWrappedCloseAsyncResult.SyncContinue(result2);
			}

			// Token: 0x06007CC8 RID: 31944 RVA: 0x001D2E0B File Offset: 0x001D100B
			private static bool HandleChannelClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
				return true;
			}

			// Token: 0x040047B0 RID: 18352
			private static AsyncResult.AsyncCompletion handleLifetimeManagerClose = new AsyncResult.AsyncCompletion(HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject>.HandleLifetimeManagerClose);

			// Token: 0x040047B1 RID: 18353
			private static AsyncResult.AsyncCompletion handleChannelClose = new AsyncResult.AsyncCompletion(HttpChannelListener<TChannel>.LifetimeWrappedCloseAsyncResult<TCommunicationObject>.HandleChannelClose);

			// Token: 0x040047B2 RID: 18354
			private TimeoutHelper timeoutHelper;

			// Token: 0x040047B3 RID: 18355
			private ICommunicationObject[] communicationObjects;

			// Token: 0x040047B4 RID: 18356
			private CommunicationObjectManager<TCommunicationObject> communicationObjectManager;

			// Token: 0x040047B5 RID: 18357
			private ChainedBeginHandler begin1;

			// Token: 0x040047B6 RID: 18358
			private ChainedEndHandler end1;
		}
	}
}
