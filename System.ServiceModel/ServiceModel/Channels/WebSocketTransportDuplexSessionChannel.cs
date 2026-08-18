using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200087F RID: 2175
	internal abstract class WebSocketTransportDuplexSessionChannel : TransportDuplexSessionChannel
	{
		// Token: 0x06005267 RID: 21095 RVA: 0x0012FC0C File Offset: 0x0012DE0C
		public WebSocketTransportDuplexSessionChannel(HttpChannelListener channelListener, EndpointAddress localAddress, Uri localVia, ConnectionBufferPool bufferPool) : base(channelListener, channelListener, localAddress, localVia, EndpointAddress.AnonymousAddress, channelListener.MessageVersion.Addressing.AnonymousUri)
		{
			this.webSocketSettings = channelListener.WebSocketSettings;
			this.transferMode = channelListener.TransferMode;
			this.maxBufferSize = channelListener.MaxBufferSize;
			this.bufferPool = bufferPool;
			this.transportFactorySettings = channelListener;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0012FC80 File Offset: 0x0012DE80
		public WebSocketTransportDuplexSessionChannel(HttpChannelFactory<IDuplexSessionChannel> channelFactory, EndpointAddress remoteAddresss, Uri via, ConnectionBufferPool bufferPool) : base(channelFactory, channelFactory, EndpointAddress.AnonymousAddress, channelFactory.MessageVersion.Addressing.AnonymousUri, remoteAddresss, via)
		{
			this.webSocketSettings = channelFactory.WebSocketSettings;
			this.transferMode = channelFactory.TransferMode;
			this.maxBufferSize = channelFactory.MaxBufferSize;
			this.bufferPool = bufferPool;
			this.transportFactorySettings = channelFactory;
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06005269 RID: 21097 RVA: 0x0012FCF1 File Offset: 0x0012DEF1
		// (set) Token: 0x0600526A RID: 21098 RVA: 0x0012FCF9 File Offset: 0x0012DEF9
		protected WebSocket WebSocket
		{
			get
			{
				return this.webSocket;
			}
			set
			{
				this.webSocket = value;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x0012FD02 File Offset: 0x0012DF02
		protected WebSocketTransportSettings WebSocketSettings
		{
			get
			{
				return this.webSocketSettings;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x0600526C RID: 21100 RVA: 0x0012FD0A File Offset: 0x0012DF0A
		protected TransferMode TransferMode
		{
			get
			{
				return this.transferMode;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x0012FD12 File Offset: 0x0012DF12
		protected int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x0600526E RID: 21102 RVA: 0x0012FD1A File Offset: 0x0012DF1A
		protected ITransportFactorySettings TransportFactorySettings
		{
			get
			{
				return this.transportFactorySettings;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x0012FD22 File Offset: 0x0012DF22
		// (set) Token: 0x06005270 RID: 21104 RVA: 0x0012FD2A File Offset: 0x0012DF2A
		protected byte[] InternalBuffer
		{
			get
			{
				return this.internalBuffer;
			}
			set
			{
				this.internalBuffer = value;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (set) Token: 0x06005271 RID: 21105 RVA: 0x0012FD33 File Offset: 0x0012DF33
		protected bool ShouldDisposeWebSocketAfterClosed
		{
			set
			{
				this.shouldDisposeWebSocketAfterClosed = value;
			}
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0012FD3C File Offset: 0x0012DF3C
		protected override void OnAbort()
		{
			if (TD.WebSocketConnectionAbortedIsEnabled())
			{
				TD.WebSocketConnectionAborted(base.EventTraceActivity, (this.WebSocket != null) ? this.WebSocket.GetHashCode() : -1);
			}
			this.Cleanup();
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0012FD6C File Offset: 0x0012DF6C
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IWebSocketCloseDetails))
			{
				return this.webSocketCloseDetails as T;
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0012FDA0 File Offset: 0x0012DFA0
		protected override void CompleteClose(TimeSpan timeout)
		{
			if (TD.WebSocketCloseSentIsEnabled())
			{
				TD.WebSocketCloseSent(this.WebSocket.GetHashCode(), this.webSocketCloseDetails.OutputCloseStatus.ToString(), (this.RemoteAddress != null) ? this.RemoteAddress.ToString() : string.Empty);
			}
			Task task = this.CloseAsync();
			task.Wait(timeout, new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "CloseOperation");
			if (TD.WebSocketConnectionClosedIsEnabled())
			{
				TD.WebSocketConnectionClosed(this.WebSocket.GetHashCode());
			}
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0012FE33 File Offset: 0x0012E033
		protected byte[] TakeBuffer()
		{
			return this.bufferPool.Take();
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0012FE40 File Offset: 0x0012E040
		protected override void CloseOutputSessionCore(TimeSpan timeout)
		{
			if (TD.WebSocketCloseOutputSentIsEnabled())
			{
				TD.WebSocketCloseOutputSent(this.WebSocket.GetHashCode(), this.webSocketCloseDetails.OutputCloseStatus.ToString(), (this.RemoteAddress != null) ? this.RemoteAddress.ToString() : string.Empty);
			}
			Task task = this.CloseOutputAsync(CancellationToken.None);
			task.Wait(timeout, new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "CloseOperation");
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x0012FEC4 File Offset: 0x0012E0C4
		protected override void OnClose(TimeSpan timeout)
		{
			try
			{
				base.OnClose(timeout);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0012FEF4 File Offset: 0x0012E0F4
		protected override void ReturnConnectionIfNecessary(bool abort, TimeSpan timeout)
		{
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0012FEF8 File Offset: 0x0012E0F8
		protected override AsyncCompletionResult StartWritingBufferedMessage(Message message, ArraySegment<byte> messageData, bool allowOutputBatching, TimeSpan timeout, WaitCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			WebSocketMessageType webSocketMessageType = WebSocketTransportDuplexSessionChannel.GetWebSocketMessageType(message);
			IOThreadCancellationTokenSource cancellationTokenSource = new IOThreadCancellationTokenSource(timeoutHelper.RemainingTime());
			if (TD.WebSocketAsyncWriteStartIsEnabled())
			{
				TD.WebSocketAsyncWriteStart(this.WebSocket.GetHashCode(), messageData.Count, (this.RemoteAddress != null) ? this.RemoteAddress.ToString() : string.Empty);
			}
			Task task = this.WebSocket.SendAsync(messageData, webSocketMessageType, true, cancellationTokenSource.Token);
			task.ContinueWith(delegate(Task t)
			{
				try
				{
					if (TD.WebSocketAsyncWriteStopIsEnabled())
					{
						TD.WebSocketAsyncWriteStop(this.webSocket.GetHashCode());
					}
					cancellationTokenSource.Dispose();
					WebSocketHelper.ThrowExceptionOnTaskFailure(t, timeout, "SendOperation");
				}
				catch (Exception exception)
				{
					FxTrace.Exception.TraceHandledException(exception, TraceEventType.Information);
					this.pendingWritingMessageException = exception;
				}
				finally
				{
					callback(state);
				}
			}, CancellationToken.None);
			return AsyncCompletionResult.Queued;
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0012FFC1 File Offset: 0x0012E1C1
		protected override void FinishWritingMessage()
		{
			WebSocketTransportDuplexSessionChannel.ThrowOnPendingException(ref this.pendingWritingMessageException);
			base.FinishWritingMessage();
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0012FFD4 File Offset: 0x0012E1D4
		protected override AsyncCompletionResult StartWritingStreamedMessage(Message message, TimeSpan timeout, WaitCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			WebSocketMessageType webSocketMessageType = WebSocketTransportDuplexSessionChannel.GetWebSocketMessageType(message);
			WebSocketTransportDuplexSessionChannel.WebSocketStream webSocketStream = new WebSocketTransportDuplexSessionChannel.WebSocketStream(this.WebSocket, webSocketMessageType, timeoutHelper.RemainingTime());
			this.waitCallback = callback;
			this.state = state;
			this.webSocketStream = webSocketStream;
			IAsyncResult asyncResult = base.MessageEncoder.BeginWriteMessage(message, new TimeoutStream(webSocketStream, ref timeoutHelper), WebSocketTransportDuplexSessionChannel.streamedWriteCallback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return AsyncCompletionResult.Queued;
			}
			base.MessageEncoder.EndWriteMessage(asyncResult);
			webSocketStream.WriteEndOfMessageAsync(timeoutHelper.RemainingTime(), callback, state);
			return AsyncCompletionResult.Queued;
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0013005C File Offset: 0x0012E25C
		protected override AsyncCompletionResult BeginCloseOutput(TimeSpan timeout, WaitCallback callback, object state)
		{
			IOThreadCancellationTokenSource cancellationTokenSource = new IOThreadCancellationTokenSource(timeout);
			Task task = this.CloseOutputAsync(cancellationTokenSource.Token);
			task.ContinueWith(delegate(Task t)
			{
				try
				{
					cancellationTokenSource.Dispose();
					WebSocketHelper.ThrowExceptionOnTaskFailure(t, timeout, "CloseOperation");
				}
				catch (Exception exception)
				{
					FxTrace.Exception.TraceHandledException(exception, TraceEventType.Information);
					this.pendingWritingMessageException = exception;
				}
				finally
				{
					callback(state);
				}
			});
			return AsyncCompletionResult.Queued;
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x001300C4 File Offset: 0x0012E2C4
		protected override void OnSendCore(Message message, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			WebSocketMessageType webSocketMessageType = WebSocketTransportDuplexSessionChannel.GetWebSocketMessageType(message);
			if (this.IsStreamedOutput)
			{
				WebSocketTransportDuplexSessionChannel.WebSocketStream webSocketStream = new WebSocketTransportDuplexSessionChannel.WebSocketStream(this.WebSocket, webSocketMessageType, timeoutHelper.RemainingTime());
				TimeoutStream stream = new TimeoutStream(webSocketStream, ref timeoutHelper);
				base.MessageEncoder.WriteMessage(message, stream);
				webSocketStream.WriteEndOfMessage(timeoutHelper.RemainingTime());
				return;
			}
			ArraySegment<byte> buffer = this.EncodeMessage(message);
			bool flag = false;
			try
			{
				if (TD.WebSocketAsyncWriteStartIsEnabled())
				{
					TD.WebSocketAsyncWriteStart(this.WebSocket.GetHashCode(), buffer.Count, (this.RemoteAddress != null) ? this.RemoteAddress.ToString() : string.Empty);
				}
				Task task = this.WebSocket.SendAsync(buffer, webSocketMessageType, true, CancellationToken.None);
				task.Wait(timeoutHelper.RemainingTime(), new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "SendOperation");
				if (TD.WebSocketAsyncWriteStopIsEnabled())
				{
					TD.WebSocketAsyncWriteStop(this.webSocket.GetHashCode());
				}
				flag = true;
			}
			finally
			{
				try
				{
					base.BufferManager.ReturnBuffer(buffer.Array);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception) || flag)
					{
						throw;
					}
					FxTrace.Exception.TraceUnhandledException(exception);
				}
			}
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x0013020C File Offset: 0x0012E40C
		protected override ArraySegment<byte> EncodeMessage(Message message)
		{
			return base.MessageEncoder.WriteMessage(message, int.MaxValue, base.BufferManager, 0);
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00130226 File Offset: 0x0012E426
		protected void Cleanup()
		{
			if (Interlocked.CompareExchange(ref this.cleanupStatus, 1, 0) == 0)
			{
				this.OnCleanup();
			}
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0013023D File Offset: 0x0012E43D
		protected virtual void OnCleanup()
		{
			if (this.shouldDisposeWebSocketAfterClosed && this.webSocket != null)
			{
				this.webSocket.Dispose();
			}
			if (this.internalBuffer != null)
			{
				this.bufferPool.Return(this.internalBuffer);
				this.internalBuffer = null;
			}
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x0013027C File Offset: 0x0012E47C
		private static void ThrowOnPendingException(ref Exception pendingException)
		{
			Exception ex = pendingException;
			if (ex != null)
			{
				pendingException = null;
				throw FxTrace.Exception.AsError(ex);
			}
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x001302A0 File Offset: 0x0012E4A0
		private Task CloseAsync()
		{
			Task result;
			try
			{
				result = this.WebSocket.CloseAsync(this.webSocketCloseDetails.OutputCloseStatus, this.webSocketCloseDetails.OutputCloseStatusDescription, CancellationToken.None);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw WebSocketHelper.ConvertAndTraceException(ex);
			}
			return result;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x001302FC File Offset: 0x0012E4FC
		private Task CloseOutputAsync(CancellationToken cancellationToken)
		{
			Task result;
			try
			{
				result = this.WebSocket.CloseOutputAsync(this.webSocketCloseDetails.OutputCloseStatus, this.webSocketCloseDetails.OutputCloseStatusDescription, cancellationToken);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw WebSocketHelper.ConvertAndTraceException(ex);
			}
			return result;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00130354 File Offset: 0x0012E554
		private static WebSocketMessageType GetWebSocketMessageType(Message message)
		{
			WebSocketMessageType result = WebSocketMessageType.Binary;
			WebSocketMessageProperty webSocketMessageProperty;
			if (message.Properties.TryGetValue<WebSocketMessageProperty>("WebSocketMessageProperty", out webSocketMessageProperty))
			{
				result = webSocketMessageProperty.MessageType;
			}
			return result;
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00130380 File Offset: 0x0012E580
		private static void StreamWriteCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			WebSocketTransportDuplexSessionChannel webSocketTransportDuplexSessionChannel = (WebSocketTransportDuplexSessionChannel)ar.AsyncState;
			try
			{
				webSocketTransportDuplexSessionChannel.MessageEncoder.EndWriteMessage(ar);
				webSocketTransportDuplexSessionChannel.webSocketStream.WriteEndOfMessage(TimeSpan.MaxValue);
				webSocketTransportDuplexSessionChannel.waitCallback(webSocketTransportDuplexSessionChannel.state);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				webSocketTransportDuplexSessionChannel.AddPendingException(exception);
			}
		}

		// Token: 0x04003269 RID: 12905
		private static AsyncCallback streamedWriteCallback = Fx.ThunkCallback(new AsyncCallback(WebSocketTransportDuplexSessionChannel.StreamWriteCallback));

		// Token: 0x0400326A RID: 12906
		private WebSocket webSocket;

		// Token: 0x0400326B RID: 12907
		private WebSocketTransportSettings webSocketSettings;

		// Token: 0x0400326C RID: 12908
		private TransferMode transferMode;

		// Token: 0x0400326D RID: 12909
		private int maxBufferSize;

		// Token: 0x0400326E RID: 12910
		private WaitCallback waitCallback;

		// Token: 0x0400326F RID: 12911
		private object state;

		// Token: 0x04003270 RID: 12912
		private WebSocketTransportDuplexSessionChannel.WebSocketStream webSocketStream;

		// Token: 0x04003271 RID: 12913
		private byte[] internalBuffer;

		// Token: 0x04003272 RID: 12914
		private ConnectionBufferPool bufferPool;

		// Token: 0x04003273 RID: 12915
		private int cleanupStatus;

		// Token: 0x04003274 RID: 12916
		private ITransportFactorySettings transportFactorySettings;

		// Token: 0x04003275 RID: 12917
		private WebSocketTransportDuplexSessionChannel.WebSocketCloseDetails webSocketCloseDetails = new WebSocketTransportDuplexSessionChannel.WebSocketCloseDetails();

		// Token: 0x04003276 RID: 12918
		private bool shouldDisposeWebSocketAfterClosed = true;

		// Token: 0x04003277 RID: 12919
		private Exception pendingWritingMessageException;

		// Token: 0x02000D5D RID: 3421
		protected class WebSocketMessageSource : IMessageSource
		{
			// Token: 0x06007D60 RID: 32096 RVA: 0x001D4A0F File Offset: 0x001D2C0F
			public WebSocketMessageSource(WebSocketTransportDuplexSessionChannel webSocketTransportDuplexSessionChannel, WebSocket webSocket, bool useStreaming, IDefaultCommunicationTimeouts defaultTimeouts)
			{
				this.Initialize(webSocketTransportDuplexSessionChannel, webSocket, useStreaming, defaultTimeouts);
				this.StartNextReceiveAsync();
			}

			// Token: 0x06007D61 RID: 32097 RVA: 0x001D4A28 File Offset: 0x001D2C28
			public WebSocketMessageSource(WebSocketTransportDuplexSessionChannel webSocketTransportDuplexSessionChannel, WebSocketContext context, bool isStreamed, RemoteEndpointMessageProperty remoteEndpointMessageProperty, IDefaultCommunicationTimeouts defaultTimeouts, HttpRequestMessage requestMessage)
			{
				this.Initialize(webSocketTransportDuplexSessionChannel, context.WebSocket, isStreamed, defaultTimeouts);
				IPrincipal user = (requestMessage == null) ? null : requestMessage.GetUserPrincipal();
				this.context = new ServiceWebSocketContext(context, user);
				this.remoteEndpointMessageProperty = remoteEndpointMessageProperty;
				this.properties = ((requestMessage == null) ? null : new ReadOnlyDictionary<string, object>(requestMessage.Properties));
				this.StartNextReceiveAsync();
			}

			// Token: 0x06007D62 RID: 32098 RVA: 0x001D4A90 File Offset: 0x001D2C90
			private void Initialize(WebSocketTransportDuplexSessionChannel webSocketTransportDuplexSessionChannel, WebSocket webSocket, bool useStreaming, IDefaultCommunicationTimeouts defaultTimeouts)
			{
				this.webSocket = webSocket;
				this.encoder = webSocketTransportDuplexSessionChannel.MessageEncoder;
				this.bufferManager = webSocketTransportDuplexSessionChannel.BufferManager;
				this.localAddress = webSocketTransportDuplexSessionChannel.LocalAddress;
				this.maxBufferSize = webSocketTransportDuplexSessionChannel.MaxBufferSize;
				this.handshakeSecurityMessageProperty = webSocketTransportDuplexSessionChannel.RemoteSecurity;
				this.maxReceivedMessageSize = webSocketTransportDuplexSessionChannel.TransportFactorySettings.MaxReceivedMessageSize;
				this.receiveBufferSize = Math.Min(WebSocketHelper.GetReceiveBufferSize(this.maxReceivedMessageSize), this.maxBufferSize);
				this.useStreaming = useStreaming;
				this.defaultTimeouts = defaultTimeouts;
				this.closeDetails = webSocketTransportDuplexSessionChannel.webSocketCloseDetails;
				this.receiveTimer = new IOThreadTimer(WebSocketTransportDuplexSessionChannel.WebSocketMessageSource.onAsyncReceiveCancelled, this, true);
				this.asyncReceiveState = 1;
			}

			// Token: 0x17001C05 RID: 7173
			// (get) Token: 0x06007D63 RID: 32099 RVA: 0x001D4B41 File Offset: 0x001D2D41
			internal RemoteEndpointMessageProperty RemoteEndpointMessageProperty
			{
				get
				{
					return this.remoteEndpointMessageProperty;
				}
			}

			// Token: 0x06007D64 RID: 32100 RVA: 0x001D4B4C File Offset: 0x001D2D4C
			private static void OnAsyncReceiveCancelled(object target)
			{
				WebSocketTransportDuplexSessionChannel.WebSocketMessageSource webSocketMessageSource = (WebSocketTransportDuplexSessionChannel.WebSocketMessageSource)target;
				webSocketMessageSource.AsyncReceiveCancelled();
			}

			// Token: 0x06007D65 RID: 32101 RVA: 0x001D4B66 File Offset: 0x001D2D66
			private void AsyncReceiveCancelled()
			{
				if (Interlocked.CompareExchange(ref this.asyncReceiveState, 2, 0) == 0)
				{
					this.receiveTask.SetResult(null);
				}
			}

			// Token: 0x06007D66 RID: 32102 RVA: 0x001D4B84 File Offset: 0x001D2D84
			public AsyncReceiveResult BeginReceive(TimeSpan timeout, WaitCallback callback, object state)
			{
				if (this.receiveTask.Task.IsCompleted)
				{
					return AsyncReceiveResult.Completed;
				}
				this.asyncReceiveTimeout = timeout;
				this.receiveTimer.Set(timeout);
				this.receiveTask.Task.ContinueWith(delegate(Task<object> t)
				{
					callback(state);
				});
				return AsyncReceiveResult.Pending;
			}

			// Token: 0x06007D67 RID: 32103 RVA: 0x001D4BEC File Offset: 0x001D2DEC
			public Message EndReceive()
			{
				if (this.asyncReceiveState == 2)
				{
					throw FxTrace.Exception.AsError(WebSocketHelper.GetTimeoutException(null, this.asyncReceiveTimeout, "ReceiveOperation"));
				}
				this.receiveTimer.Cancel();
				Message message = this.GetPendingMessage();
				if (message != null)
				{
					this.StartNextReceiveAsync();
				}
				return message;
			}

			// Token: 0x06007D68 RID: 32104 RVA: 0x001D4C3C File Offset: 0x001D2E3C
			public Message Receive(TimeSpan timeout)
			{
				bool flag = this.receiveTask.Task.Wait(timeout);
				WebSocketTransportDuplexSessionChannel.ThrowOnPendingException(ref this.pendingException);
				if (!flag)
				{
					throw FxTrace.Exception.AsError(new TimeoutException(SR.GetString("WaitForMessageTimedOut", new object[]
					{
						timeout
					}), ThreadNeutralSemaphore.CreateEnterTimedOutException(timeout)));
				}
				Message message = this.GetPendingMessage();
				if (message != null)
				{
					this.StartNextReceiveAsync();
				}
				return message;
			}

			// Token: 0x06007D69 RID: 32105 RVA: 0x001D4CA9 File Offset: 0x001D2EA9
			public void UpdateOpenNotificationMessageProperties(MessageProperties messageProperties)
			{
				this.AddMessageProperties(messageProperties, WebSocketMessageType.Binary);
			}

			// Token: 0x06007D6A RID: 32106 RVA: 0x001D4CB4 File Offset: 0x001D2EB4
			private Task ReadBufferedMessageAsync()
			{
				WebSocketTransportDuplexSessionChannel.WebSocketMessageSource.<ReadBufferedMessageAsync>d__34 <ReadBufferedMessageAsync>d__;
				<ReadBufferedMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
				<ReadBufferedMessageAsync>d__.<>4__this = this;
				<ReadBufferedMessageAsync>d__.<>1__state = -1;
				<ReadBufferedMessageAsync>d__.<>t__builder.Start<WebSocketTransportDuplexSessionChannel.WebSocketMessageSource.<ReadBufferedMessageAsync>d__34>(ref <ReadBufferedMessageAsync>d__);
				return <ReadBufferedMessageAsync>d__.<>t__builder.Task;
			}

			// Token: 0x06007D6B RID: 32107 RVA: 0x001D4CF8 File Offset: 0x001D2EF8
			public AsyncReceiveResult BeginWaitForMessage(TimeSpan timeout, WaitCallback callback, object state)
			{
				AsyncReceiveResult result;
				try
				{
					result = this.BeginReceive(timeout, callback, state);
				}
				catch (TimeoutException exception)
				{
					this.pendingException = FxTrace.Exception.AsError(exception);
					result = AsyncReceiveResult.Completed;
				}
				return result;
			}

			// Token: 0x06007D6C RID: 32108 RVA: 0x001D4D38 File Offset: 0x001D2F38
			public bool EndWaitForMessage()
			{
				bool result;
				try
				{
					Message message = this.EndReceive();
					this.pendingMessage = message;
					result = true;
				}
				catch (TimeoutException ex)
				{
					if (TD.ReceiveTimeoutIsEnabled())
					{
						TD.ReceiveTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					result = false;
				}
				return result;
			}

			// Token: 0x06007D6D RID: 32109 RVA: 0x001D4D88 File Offset: 0x001D2F88
			public bool WaitForMessage(TimeSpan timeout)
			{
				bool result;
				try
				{
					Message message = this.Receive(timeout);
					this.pendingMessage = message;
					result = true;
				}
				catch (TimeoutException ex)
				{
					if (TD.ReceiveTimeoutIsEnabled())
					{
						TD.ReceiveTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					result = false;
				}
				return result;
			}

			// Token: 0x06007D6E RID: 32110 RVA: 0x001D4DD8 File Offset: 0x001D2FD8
			internal void FinishUsingMessageStream(Exception ex)
			{
				if (ex != null && this.pendingException == null)
				{
					this.pendingException = ex;
				}
				this.streamWaitTask.SetResult(null);
			}

			// Token: 0x06007D6F RID: 32111 RVA: 0x001D4DF8 File Offset: 0x001D2FF8
			internal void CheckCloseStatus(WebSocketReceiveResult result)
			{
				if (result.MessageType == WebSocketMessageType.Close)
				{
					if (TD.WebSocketCloseStatusReceivedIsEnabled())
					{
						TD.WebSocketCloseStatusReceived(this.webSocket.GetHashCode(), result.CloseStatus.ToString());
					}
					this.closureReceived = true;
					this.closeDetails.InputCloseStatus = result.CloseStatus;
					this.closeDetails.InputCloseStatusDescription = result.CloseStatusDescription;
				}
			}

			// Token: 0x06007D70 RID: 32112 RVA: 0x001D4E64 File Offset: 0x001D3064
			private void StartNextReceiveAsync()
			{
				WebSocketTransportDuplexSessionChannel.WebSocketMessageSource.<StartNextReceiveAsync>d__40 <StartNextReceiveAsync>d__;
				<StartNextReceiveAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
				<StartNextReceiveAsync>d__.<>4__this = this;
				<StartNextReceiveAsync>d__.<>1__state = -1;
				<StartNextReceiveAsync>d__.<>t__builder.Start<WebSocketTransportDuplexSessionChannel.WebSocketMessageSource.<StartNextReceiveAsync>d__40>(ref <StartNextReceiveAsync>d__);
			}

			// Token: 0x06007D71 RID: 32113 RVA: 0x001D4E9C File Offset: 0x001D309C
			private void AddMessageProperties(MessageProperties messageProperties, WebSocketMessageType incomingMessageType)
			{
				WebSocketMessageProperty property = new WebSocketMessageProperty(this.context, this.webSocket.SubProtocol, incomingMessageType, this.properties);
				messageProperties.Add("WebSocketMessageProperty", property);
				if (this.remoteEndpointMessageProperty != null)
				{
					messageProperties.Add(RemoteEndpointMessageProperty.Name, this.remoteEndpointMessageProperty);
				}
				if (this.handshakeSecurityMessageProperty != null)
				{
					messageProperties.Security = (SecurityMessageProperty)this.handshakeSecurityMessageProperty.CreateCopy();
				}
			}

			// Token: 0x06007D72 RID: 32114 RVA: 0x001D4F0C File Offset: 0x001D310C
			private Message GetPendingMessage()
			{
				WebSocketTransportDuplexSessionChannel.ThrowOnPendingException(ref this.pendingException);
				if (this.pendingMessage != null)
				{
					Message result = this.pendingMessage;
					this.pendingMessage = null;
					return result;
				}
				return null;
			}

			// Token: 0x06007D73 RID: 32115 RVA: 0x001D4F40 File Offset: 0x001D3140
			private Message PrepareMessage(WebSocketReceiveResult result, byte[] buffer, int count)
			{
				if (result.MessageType != WebSocketMessageType.Close)
				{
					Message message;
					if (this.useStreaming)
					{
						TimeoutHelper timeoutHelper = new TimeoutHelper(this.defaultTimeouts.ReceiveTimeout);
						message = this.encoder.ReadMessage(new MaxMessageSizeStream(new TimeoutStream(new WebSocketTransportDuplexSessionChannel.WebSocketStream(this, new ArraySegment<byte>(buffer, 0, count), this.webSocket, result.EndOfMessage, this.bufferManager, this.defaultTimeouts.CloseTimeout), ref timeoutHelper), this.maxReceivedMessageSize), this.maxBufferSize);
					}
					else
					{
						ArraySegment<byte> buffer2 = new ArraySegment<byte>(buffer, 0, count);
						message = this.encoder.ReadMessage(buffer2, this.bufferManager);
					}
					if (message.Version.Addressing != AddressingVersion.None || !this.localAddress.IsAnonymous)
					{
						this.localAddress.ApplyTo(message);
					}
					if (message.Version.Addressing == AddressingVersion.None && message.Headers.Action == null)
					{
						if (result.MessageType == WebSocketMessageType.Binary)
						{
							message.Headers.Action = "http://schemas.microsoft.com/2011/02/websockets/onbinarymessage";
						}
						else
						{
							message.Headers.Action = "http://schemas.microsoft.com/2011/02/websockets/ontextmessage";
						}
					}
					if (message != null)
					{
						this.AddMessageProperties(message.Properties, result.MessageType);
					}
					return message;
				}
				return null;
			}

			// Token: 0x040047FD RID: 18429
			private static readonly Action<object> onAsyncReceiveCancelled = Fx.ThunkCallback<object>(new Action<object>(WebSocketTransportDuplexSessionChannel.WebSocketMessageSource.OnAsyncReceiveCancelled));

			// Token: 0x040047FE RID: 18430
			private MessageEncoder encoder;

			// Token: 0x040047FF RID: 18431
			private BufferManager bufferManager;

			// Token: 0x04004800 RID: 18432
			private EndpointAddress localAddress;

			// Token: 0x04004801 RID: 18433
			private Message pendingMessage;

			// Token: 0x04004802 RID: 18434
			private Exception pendingException;

			// Token: 0x04004803 RID: 18435
			private WebSocketContext context;

			// Token: 0x04004804 RID: 18436
			private WebSocket webSocket;

			// Token: 0x04004805 RID: 18437
			private bool closureReceived;

			// Token: 0x04004806 RID: 18438
			private bool useStreaming;

			// Token: 0x04004807 RID: 18439
			private int receiveBufferSize;

			// Token: 0x04004808 RID: 18440
			private int maxBufferSize;

			// Token: 0x04004809 RID: 18441
			private long maxReceivedMessageSize;

			// Token: 0x0400480A RID: 18442
			private TaskCompletionSource<object> streamWaitTask;

			// Token: 0x0400480B RID: 18443
			private IDefaultCommunicationTimeouts defaultTimeouts;

			// Token: 0x0400480C RID: 18444
			private RemoteEndpointMessageProperty remoteEndpointMessageProperty;

			// Token: 0x0400480D RID: 18445
			private SecurityMessageProperty handshakeSecurityMessageProperty;

			// Token: 0x0400480E RID: 18446
			private WebSocketTransportDuplexSessionChannel.WebSocketCloseDetails closeDetails;

			// Token: 0x0400480F RID: 18447
			private ReadOnlyDictionary<string, object> properties;

			// Token: 0x04004810 RID: 18448
			private TimeSpan asyncReceiveTimeout;

			// Token: 0x04004811 RID: 18449
			private TaskCompletionSource<object> receiveTask;

			// Token: 0x04004812 RID: 18450
			private IOThreadTimer receiveTimer;

			// Token: 0x04004813 RID: 18451
			private int asyncReceiveState;

			// Token: 0x02000F60 RID: 3936
			private static class AsyncReceiveState
			{
				// Token: 0x04004EE0 RID: 20192
				internal const int Started = 0;

				// Token: 0x04004EE1 RID: 20193
				internal const int Finished = 1;

				// Token: 0x04004EE2 RID: 20194
				internal const int Cancelled = 2;
			}
		}

		// Token: 0x02000D5E RID: 3422
		private class WebSocketStream : Stream
		{
			// Token: 0x06007D75 RID: 32117 RVA: 0x001D5086 File Offset: 0x001D3286
			public WebSocketStream(WebSocketTransportDuplexSessionChannel.WebSocketMessageSource messageSource, ArraySegment<byte> initialBuffer, WebSocket webSocket, bool endofMessageReceived, BufferManager bufferManager, TimeSpan closeTimeout) : this(webSocket, WebSocketMessageType.Binary, closeTimeout)
			{
				this.messageSource = messageSource;
				this.initialReadBuffer = initialBuffer;
				this.isForRead = true;
				this.endofMessageReceived = endofMessageReceived;
				this.bufferManager = bufferManager;
				this.messageSourceCleanState = 0;
				this.endOfMessageWritten = 0;
			}

			// Token: 0x06007D76 RID: 32118 RVA: 0x001D50C5 File Offset: 0x001D32C5
			public WebSocketStream(WebSocket webSocket, WebSocketMessageType outgoingMessageType, TimeSpan closeTimeout)
			{
				this.webSocket = webSocket;
				this.isForRead = false;
				this.outgoingMessageType = outgoingMessageType;
				this.messageSourceCleanState = 1;
				this.closeTimeout = closeTimeout;
			}

			// Token: 0x17001C06 RID: 7174
			// (get) Token: 0x06007D77 RID: 32119 RVA: 0x001D50F0 File Offset: 0x001D32F0
			public override bool CanRead
			{
				get
				{
					return this.isForRead;
				}
			}

			// Token: 0x17001C07 RID: 7175
			// (get) Token: 0x06007D78 RID: 32120 RVA: 0x001D50F8 File Offset: 0x001D32F8
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001C08 RID: 7176
			// (get) Token: 0x06007D79 RID: 32121 RVA: 0x001D50FB File Offset: 0x001D32FB
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001C09 RID: 7177
			// (get) Token: 0x06007D7A RID: 32122 RVA: 0x001D50FE File Offset: 0x001D32FE
			public override bool CanWrite
			{
				get
				{
					return !this.isForRead;
				}
			}

			// Token: 0x17001C0A RID: 7178
			// (get) Token: 0x06007D7B RID: 32123 RVA: 0x001D5109 File Offset: 0x001D3309
			public override long Length
			{
				get
				{
					throw FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("SeekNotSupported")));
				}
			}

			// Token: 0x17001C0B RID: 7179
			// (get) Token: 0x06007D7C RID: 32124 RVA: 0x001D5124 File Offset: 0x001D3324
			// (set) Token: 0x06007D7D RID: 32125 RVA: 0x001D513F File Offset: 0x001D333F
			public override long Position
			{
				get
				{
					throw FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("SeekNotSupported")));
				}
				set
				{
					throw FxTrace.Exception.AsError(new NotSupportedException(SR.GetString("SeekNotSupported")));
				}
			}

			// Token: 0x17001C0C RID: 7180
			// (get) Token: 0x06007D7E RID: 32126 RVA: 0x001D515A File Offset: 0x001D335A
			// (set) Token: 0x06007D7F RID: 32127 RVA: 0x001D5162 File Offset: 0x001D3362
			public override int ReadTimeout
			{
				get
				{
					return this.readTimeout;
				}
				set
				{
					this.readTimeout = value;
				}
			}

			// Token: 0x17001C0D RID: 7181
			// (get) Token: 0x06007D80 RID: 32128 RVA: 0x001D516B File Offset: 0x001D336B
			// (set) Token: 0x06007D81 RID: 32129 RVA: 0x001D5173 File Offset: 0x001D3373
			public override int WriteTimeout
			{
				get
				{
					return this.writeTimeout;
				}
				set
				{
					this.writeTimeout = value;
				}
			}

			// Token: 0x06007D82 RID: 32130 RVA: 0x001D517C File Offset: 0x001D337C
			public override void Close()
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(this.closeTimeout);
				base.Close();
				this.Cleanup(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007D83 RID: 32131 RVA: 0x001D51A9 File Offset: 0x001D33A9
			public override void Flush()
			{
			}

			// Token: 0x06007D84 RID: 32132 RVA: 0x001D51AC File Offset: 0x001D33AC
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this.ReadTimeout <= 0)
				{
					throw FxTrace.Exception.AsError(WebSocketHelper.GetTimeoutException(null, TimeoutHelper.FromMilliseconds(this.ReadTimeout), "ReceiveOperation"));
				}
				TimeoutHelper helper = new TimeoutHelper(TimeoutHelper.FromMilliseconds(this.ReadTimeout));
				if (this.endOfMessageReached)
				{
					return new CompletedAsyncResult<int>(0, callback, state);
				}
				if (this.initialReadBuffer.Count != 0)
				{
					int bytesFromInitialReadBuffer = this.GetBytesFromInitialReadBuffer(buffer, offset, count);
					return new CompletedAsyncResult<int>(bytesFromInitialReadBuffer, callback, state);
				}
				if (this.endofMessageReceived)
				{
					this.endOfMessageReached = true;
					return new CompletedAsyncResult<int>(0, callback, state);
				}
				if (TD.WebSocketAsyncReadStartIsEnabled())
				{
					TD.WebSocketAsyncReadStart(this.webSocket.GetHashCode());
				}
				IOThreadCancellationTokenSource cancellationTokenSource = new IOThreadCancellationTokenSource(helper.RemainingTime());
				Task<int> task = this.webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, offset, count), cancellationTokenSource.Token).ContinueWith<int>(delegate(Task<WebSocketReceiveResult> t)
				{
					cancellationTokenSource.Dispose();
					WebSocketHelper.ThrowExceptionOnTaskFailure(t, TimeoutHelper.FromMilliseconds(this.ReadTimeout), "ReceiveOperation");
					this.endOfMessageReached = t.Result.EndOfMessage;
					int count2 = t.Result.Count;
					WebSocketTransportDuplexSessionChannel.WebSocketStream.CheckResultAndEnsureNotCloseMessage(this.messageSource, t.Result);
					if (this.endOfMessageReached)
					{
						this.Cleanup(helper.RemainingTime());
					}
					if (TD.WebSocketAsyncReadStopIsEnabled())
					{
						TD.WebSocketAsyncReadStop(this.webSocket.GetHashCode(), count2, (this.messageSource != null) ? TraceUtility.GetRemoteEndpointAddressPort(this.messageSource.RemoteEndpointMessageProperty) : string.Empty);
					}
					return count2;
				}, TaskContinuationOptions.None);
				return task.AsAsyncResult(callback, state);
			}

			// Token: 0x06007D85 RID: 32133 RVA: 0x001D52C0 File Offset: 0x001D34C0
			public override int EndRead(IAsyncResult asyncResult)
			{
				Task<int> task = (Task<int>)asyncResult;
				WebSocketHelper.ThrowExceptionOnTaskFailure(task, TimeoutHelper.FromMilliseconds(this.ReadTimeout), "ReceiveOperation");
				return task.Result;
			}

			// Token: 0x06007D86 RID: 32134 RVA: 0x001D52F0 File Offset: 0x001D34F0
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this.ReadTimeout <= 0)
				{
					throw FxTrace.Exception.AsError(WebSocketHelper.GetTimeoutException(null, TimeoutHelper.FromMilliseconds(this.ReadTimeout), "ReceiveOperation"));
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(TimeoutHelper.FromMilliseconds(this.ReadTimeout));
				if (this.endOfMessageReached)
				{
					return 0;
				}
				if (this.initialReadBuffer.Count != 0)
				{
					return this.GetBytesFromInitialReadBuffer(buffer, offset, count);
				}
				int num = 0;
				if (this.endofMessageReceived)
				{
					this.endOfMessageReached = true;
				}
				else
				{
					if (TD.WebSocketAsyncReadStartIsEnabled())
					{
						TD.WebSocketAsyncReadStart(this.webSocket.GetHashCode());
					}
					Task<WebSocketReceiveResult> task = this.webSocket.ReceiveAsync(new ArraySegment<byte>(buffer, offset, count), CancellationToken.None);
					task.Wait(timeoutHelper.RemainingTime(), new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "ReceiveOperation");
					if (task.Result.EndOfMessage)
					{
						this.endofMessageReceived = true;
						this.endOfMessageReached = true;
					}
					num = task.Result.Count;
					WebSocketTransportDuplexSessionChannel.WebSocketStream.CheckResultAndEnsureNotCloseMessage(this.messageSource, task.Result);
					if (TD.WebSocketAsyncReadStopIsEnabled())
					{
						TD.WebSocketAsyncReadStop(this.webSocket.GetHashCode(), num, (this.messageSource != null) ? TraceUtility.GetRemoteEndpointAddressPort(this.messageSource.RemoteEndpointMessageProperty) : string.Empty);
					}
				}
				if (this.endOfMessageReached)
				{
					this.Cleanup(timeoutHelper.RemainingTime());
				}
				return num;
			}

			// Token: 0x06007D87 RID: 32135 RVA: 0x001D5443 File Offset: 0x001D3643
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw FxTrace.Exception.AsError(new NotSupportedException());
			}

			// Token: 0x06007D88 RID: 32136 RVA: 0x001D5454 File Offset: 0x001D3654
			public override void SetLength(long value)
			{
				throw FxTrace.Exception.AsError(new NotSupportedException());
			}

			// Token: 0x06007D89 RID: 32137 RVA: 0x001D5468 File Offset: 0x001D3668
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.endOfMessageWritten == 1)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WebSocketStreamWriteCalledAfterEOMSent")));
				}
				if (this.WriteTimeout <= 0)
				{
					throw FxTrace.Exception.AsError(WebSocketHelper.GetTimeoutException(null, TimeoutHelper.FromMilliseconds(this.WriteTimeout), "SendOperation"));
				}
				if (TD.WebSocketAsyncWriteStartIsEnabled())
				{
					TD.WebSocketAsyncWriteStart(this.webSocket.GetHashCode(), count, (this.messageSource != null) ? TraceUtility.GetRemoteEndpointAddressPort(this.messageSource.RemoteEndpointMessageProperty) : string.Empty);
				}
				Task task = this.webSocket.SendAsync(new ArraySegment<byte>(buffer, offset, count), this.outgoingMessageType, false, CancellationToken.None);
				task.Wait(TimeoutHelper.FromMilliseconds(this.WriteTimeout), new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "SendOperation");
				if (TD.WebSocketAsyncWriteStopIsEnabled())
				{
					TD.WebSocketAsyncWriteStop(this.webSocket.GetHashCode());
				}
			}

			// Token: 0x06007D8A RID: 32138 RVA: 0x001D5554 File Offset: 0x001D3754
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this.endOfMessageWritten == 1)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("WebSocketStreamWriteCalledAfterEOMSent")));
				}
				if (this.WriteTimeout <= 0)
				{
					throw FxTrace.Exception.AsError(WebSocketHelper.GetTimeoutException(null, TimeoutHelper.FromMilliseconds(this.WriteTimeout), "SendOperation"));
				}
				if (TD.WebSocketAsyncWriteStartIsEnabled())
				{
					TD.WebSocketAsyncWriteStart(this.webSocket.GetHashCode(), count, (this.messageSource != null) ? TraceUtility.GetRemoteEndpointAddressPort(this.messageSource.RemoteEndpointMessageProperty) : string.Empty);
				}
				IOThreadCancellationTokenSource cancellationTokenSource = new IOThreadCancellationTokenSource(this.WriteTimeout);
				Task task = this.webSocket.SendAsync(new ArraySegment<byte>(buffer, offset, count), this.outgoingMessageType, false, cancellationTokenSource.Token).ContinueWith(delegate(Task t)
				{
					if (TD.WebSocketAsyncWriteStopIsEnabled())
					{
						TD.WebSocketAsyncWriteStop(this.webSocket.GetHashCode());
					}
					cancellationTokenSource.Dispose();
					WebSocketHelper.ThrowExceptionOnTaskFailure(t, TimeoutHelper.FromMilliseconds(this.WriteTimeout), "SendOperation");
				});
				return task.AsAsyncResult(callback, state);
			}

			// Token: 0x06007D8B RID: 32139 RVA: 0x001D5644 File Offset: 0x001D3844
			public override void EndWrite(IAsyncResult asyncResult)
			{
				Task task = (Task)asyncResult;
				WebSocketHelper.ThrowExceptionOnTaskFailure(task, TimeoutHelper.FromMilliseconds(this.WriteTimeout), "SendOperation");
			}

			// Token: 0x06007D8C RID: 32140 RVA: 0x001D5670 File Offset: 0x001D3870
			public void WriteEndOfMessage(TimeSpan timeout)
			{
				if (TD.WebSocketAsyncWriteStartIsEnabled())
				{
					TD.WebSocketAsyncWriteStart(this.webSocket.GetHashCode(), 0, (this.messageSource != null) ? TraceUtility.GetRemoteEndpointAddressPort(this.messageSource.RemoteEndpointMessageProperty) : string.Empty);
				}
				if (Interlocked.CompareExchange(ref this.endOfMessageWritten, 1, 0) == 0)
				{
					Task task = this.webSocket.SendAsync(new ArraySegment<byte>(EmptyArray<byte>.Instance, 0, 0), this.outgoingMessageType, true, CancellationToken.None);
					task.Wait(timeout, new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "SendOperation");
				}
				if (TD.WebSocketAsyncWriteStopIsEnabled())
				{
					TD.WebSocketAsyncWriteStop(this.webSocket.GetHashCode());
				}
			}

			// Token: 0x06007D8D RID: 32141 RVA: 0x001D5718 File Offset: 0x001D3918
			public void WriteEndOfMessageAsync(TimeSpan timeout, WaitCallback callback, object state)
			{
				WebSocketTransportDuplexSessionChannel.WebSocketStream.<WriteEndOfMessageAsync>d__45 <WriteEndOfMessageAsync>d__;
				<WriteEndOfMessageAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
				<WriteEndOfMessageAsync>d__.<>4__this = this;
				<WriteEndOfMessageAsync>d__.timeout = timeout;
				<WriteEndOfMessageAsync>d__.callback = callback;
				<WriteEndOfMessageAsync>d__.state = state;
				<WriteEndOfMessageAsync>d__.<>1__state = -1;
				<WriteEndOfMessageAsync>d__.<>t__builder.Start<WebSocketTransportDuplexSessionChannel.WebSocketStream.<WriteEndOfMessageAsync>d__45>(ref <WriteEndOfMessageAsync>d__);
			}

			// Token: 0x06007D8E RID: 32142 RVA: 0x001D5767 File Offset: 0x001D3967
			private static void CheckResultAndEnsureNotCloseMessage(WebSocketTransportDuplexSessionChannel.WebSocketMessageSource messageSource, WebSocketReceiveResult result)
			{
				messageSource.CheckCloseStatus(result);
				if (result.MessageType == WebSocketMessageType.Close)
				{
					throw FxTrace.Exception.AsError(new ProtocolException(SR.GetString("WebSocketUnexpectedCloseMessageError")));
				}
			}

			// Token: 0x06007D8F RID: 32143 RVA: 0x001D5794 File Offset: 0x001D3994
			private int GetBytesFromInitialReadBuffer(byte[] buffer, int offset, int count)
			{
				int num = (this.initialReadBuffer.Count > count) ? count : this.initialReadBuffer.Count;
				Buffer.BlockCopy(this.initialReadBuffer.Array, this.initialReadBuffer.Offset, buffer, offset, num);
				this.initialReadBuffer = new ArraySegment<byte>(this.initialReadBuffer.Array, this.initialReadBuffer.Offset + num, this.initialReadBuffer.Count - num);
				return num;
			}

			// Token: 0x06007D90 RID: 32144 RVA: 0x001D5810 File Offset: 0x001D3A10
			private void Cleanup(TimeSpan timeout)
			{
				if (this.isForRead)
				{
					if (Interlocked.CompareExchange(ref this.messageSourceCleanState, 1, 0) == 0)
					{
						Exception ex = null;
						try
						{
							if (!this.endofMessageReceived && (this.webSocket.State == WebSocketState.Open || this.webSocket.State == WebSocketState.CloseSent))
							{
								TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
								do
								{
									Task<WebSocketReceiveResult> task = this.webSocket.ReceiveAsync(new ArraySegment<byte>(this.initialReadBuffer.Array), CancellationToken.None);
									task.Wait(timeoutHelper.RemainingTime(), new Action<Exception, TimeSpan, string>(WebSocketHelper.ThrowCorrectException), "ReceiveOperation");
									this.endofMessageReceived = task.Result.EndOfMessage;
								}
								while (!this.endofMessageReceived && (this.webSocket.State == WebSocketState.Open || this.webSocket.State == WebSocketState.CloseSent));
							}
						}
						catch (Exception ex2)
						{
							if (Fx.IsFatal(ex2))
							{
								throw;
							}
							ex = WebSocketHelper.ConvertAndTraceException(ex2, timeout, "CloseOperation");
						}
						this.bufferManager.ReturnBuffer(this.initialReadBuffer.Array);
						this.messageSource.FinishUsingMessageStream(ex);
						return;
					}
				}
				else if (Interlocked.CompareExchange(ref this.endOfMessageWritten, 1, 0) == 0)
				{
					this.WriteEndOfMessage(timeout);
				}
			}

			// Token: 0x04004814 RID: 18452
			private WebSocket webSocket;

			// Token: 0x04004815 RID: 18453
			private WebSocketTransportDuplexSessionChannel.WebSocketMessageSource messageSource;

			// Token: 0x04004816 RID: 18454
			private TimeSpan closeTimeout;

			// Token: 0x04004817 RID: 18455
			private ArraySegment<byte> initialReadBuffer;

			// Token: 0x04004818 RID: 18456
			private bool endOfMessageReached;

			// Token: 0x04004819 RID: 18457
			private bool isForRead;

			// Token: 0x0400481A RID: 18458
			private bool endofMessageReceived;

			// Token: 0x0400481B RID: 18459
			private WebSocketMessageType outgoingMessageType;

			// Token: 0x0400481C RID: 18460
			private BufferManager bufferManager;

			// Token: 0x0400481D RID: 18461
			private int messageSourceCleanState;

			// Token: 0x0400481E RID: 18462
			private int endOfMessageWritten;

			// Token: 0x0400481F RID: 18463
			private int readTimeout;

			// Token: 0x04004820 RID: 18464
			private int writeTimeout;
		}

		// Token: 0x02000D5F RID: 3423
		private class WebSocketCloseDetails : IWebSocketCloseDetails
		{
			// Token: 0x17001C0E RID: 7182
			// (get) Token: 0x06007D91 RID: 32145 RVA: 0x001D5944 File Offset: 0x001D3B44
			// (set) Token: 0x06007D92 RID: 32146 RVA: 0x001D594C File Offset: 0x001D3B4C
			public WebSocketCloseStatus? InputCloseStatus
			{
				get
				{
					return this.inputCloseStatus;
				}
				internal set
				{
					this.inputCloseStatus = value;
				}
			}

			// Token: 0x17001C0F RID: 7183
			// (get) Token: 0x06007D93 RID: 32147 RVA: 0x001D5955 File Offset: 0x001D3B55
			// (set) Token: 0x06007D94 RID: 32148 RVA: 0x001D595D File Offset: 0x001D3B5D
			public string InputCloseStatusDescription
			{
				get
				{
					return this.inputCloseStatusDescription;
				}
				internal set
				{
					this.inputCloseStatusDescription = value;
				}
			}

			// Token: 0x17001C10 RID: 7184
			// (get) Token: 0x06007D95 RID: 32149 RVA: 0x001D5966 File Offset: 0x001D3B66
			internal WebSocketCloseStatus OutputCloseStatus
			{
				get
				{
					return this.outputCloseStatus;
				}
			}

			// Token: 0x17001C11 RID: 7185
			// (get) Token: 0x06007D96 RID: 32150 RVA: 0x001D596E File Offset: 0x001D3B6E
			internal string OutputCloseStatusDescription
			{
				get
				{
					return this.outputCloseStatusDescription;
				}
			}

			// Token: 0x06007D97 RID: 32151 RVA: 0x001D5976 File Offset: 0x001D3B76
			public void SetOutputCloseStatus(WebSocketCloseStatus closeStatus, string closeStatusDescription)
			{
				this.outputCloseStatus = closeStatus;
				this.outputCloseStatusDescription = closeStatusDescription;
			}

			// Token: 0x04004821 RID: 18465
			private WebSocketCloseStatus outputCloseStatus = WebSocketCloseStatus.NormalClosure;

			// Token: 0x04004822 RID: 18466
			private string outputCloseStatusDescription;

			// Token: 0x04004823 RID: 18467
			private WebSocketCloseStatus? inputCloseStatus;

			// Token: 0x04004824 RID: 18468
			private string inputCloseStatusDescription;
		}
	}
}
