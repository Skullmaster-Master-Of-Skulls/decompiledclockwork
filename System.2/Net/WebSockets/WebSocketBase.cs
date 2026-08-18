using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000230 RID: 560
	internal abstract class WebSocketBase : WebSocket, IDisposable
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x0006CC78 File Offset: 0x0006AE78
		protected WebSocketBase(Stream innerStream, string subProtocol, TimeSpan keepAliveInterval, WebSocketBuffer internalBuffer)
		{
			WebSocketHelpers.ValidateInnerStream(innerStream);
			WebSocketHelpers.ValidateOptions(subProtocol, internalBuffer.ReceiveBufferSize, internalBuffer.SendBufferSize, keepAliveInterval);
			WebSocketBase.s_LoggingEnabled = (Logging.On && Logging.WebSockets.Switch.ShouldTrace(TraceEventType.Critical));
			string text = string.Empty;
			if (WebSocketBase.s_LoggingEnabled)
			{
				text = string.Format(CultureInfo.InvariantCulture, "ReceiveBufferSize: {0}, SendBufferSize: {1},  Protocols: {2}, KeepAliveInterval: {3}, innerStream: {4}, internalBuffer: {5}", new object[]
				{
					internalBuffer.ReceiveBufferSize,
					internalBuffer.SendBufferSize,
					subProtocol,
					keepAliveInterval,
					Logging.GetObjectLogHash(innerStream),
					Logging.GetObjectLogHash(internalBuffer)
				});
				Logging.Enter(Logging.WebSockets, this, "Initialize", text);
			}
			this.m_ThisLock = new object();
			try
			{
				this.m_InnerStream = innerStream;
				this.m_InternalBuffer = internalBuffer;
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Associate(Logging.WebSockets, this, this.m_InnerStream);
					Logging.Associate(Logging.WebSockets, this, this.m_InternalBuffer);
				}
				this.m_CloseOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_CloseOutputOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_ReceiveOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_SendOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_State = WebSocketState.Open;
				this.m_SubProtocol = subProtocol;
				this.m_SendFrameThrottle = new SemaphoreSlim(1, 1);
				this.m_CloseStatus = null;
				this.m_CloseStatusDescription = null;
				this.m_InnerStreamAsWebSocketStream = (innerStream as WebSocketBase.IWebSocketStream);
				if (this.m_InnerStreamAsWebSocketStream != null)
				{
					this.m_InnerStreamAsWebSocketStream.SwitchToOpaqueMode(this);
				}
				this.m_KeepAliveTracker = WebSocketBase.KeepAliveTracker.Create(keepAliveInterval);
			}
			finally
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "Initialize", text);
				}
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x0006CE38 File Offset: 0x0006B038
		internal static bool LoggingEnabled
		{
			get
			{
				return WebSocketBase.s_LoggingEnabled;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0006CE41 File Offset: 0x0006B041
		public override WebSocketState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0006CE4B File Offset: 0x0006B04B
		public override string SubProtocol
		{
			get
			{
				return this.m_SubProtocol;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0006CE53 File Offset: 0x0006B053
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				return this.m_CloseStatus;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0006CE5B File Offset: 0x0006B05B
		public override string CloseStatusDescription
		{
			get
			{
				return this.m_CloseStatusDescription;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0006CE63 File Offset: 0x0006B063
		internal WebSocketBuffer InternalBuffer
		{
			get
			{
				return this.m_InternalBuffer;
			}
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0006CE6B File Offset: 0x0006B06B
		protected void StartKeepAliveTimer()
		{
			this.m_KeepAliveTracker.StartTimer(this);
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060014D1 RID: 5329
		internal abstract SafeHandle SessionHandle { get; }

		// Token: 0x060014D2 RID: 5330 RVA: 0x0006CE79 File Offset: 0x0006B079
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateArraySegment<byte>(buffer, "buffer");
			return this.ReceiveAsyncCore(buffer, cancellationToken);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0006CE90 File Offset: 0x0006B090
		private Task<WebSocketReceiveResult> ReceiveAsyncCore(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketBase.<ReceiveAsyncCore>d__45 <ReceiveAsyncCore>d__;
			<ReceiveAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder<WebSocketReceiveResult>.Create();
			<ReceiveAsyncCore>d__.<>4__this = this;
			<ReceiveAsyncCore>d__.buffer = buffer;
			<ReceiveAsyncCore>d__.cancellationToken = cancellationToken;
			<ReceiveAsyncCore>d__.<>1__state = -1;
			<ReceiveAsyncCore>d__.<>t__builder.Start<WebSocketBase.<ReceiveAsyncCore>d__45>(ref <ReceiveAsyncCore>d__);
			return <ReceiveAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0006CEE4 File Offset: 0x0006B0E4
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Binary && messageType != WebSocketMessageType.Text)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_Argument_InvalidMessageType", new object[]
				{
					messageType,
					"SendAsync",
					WebSocketMessageType.Binary,
					WebSocketMessageType.Text,
					"CloseOutputAsync"
				}), "messageType");
			}
			WebSocketHelpers.ValidateArraySegment<byte>(buffer, "buffer");
			return this.SendAsyncCore(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006CF54 File Offset: 0x0006B154
		private Task SendAsyncCore(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			WebSocketBase.<SendAsyncCore>d__47 <SendAsyncCore>d__;
			<SendAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendAsyncCore>d__.<>4__this = this;
			<SendAsyncCore>d__.buffer = buffer;
			<SendAsyncCore>d__.messageType = messageType;
			<SendAsyncCore>d__.endOfMessage = endOfMessage;
			<SendAsyncCore>d__.cancellationToken = cancellationToken;
			<SendAsyncCore>d__.<>1__state = -1;
			<SendAsyncCore>d__.<>t__builder.Start<WebSocketBase.<SendAsyncCore>d__47>(ref <SendAsyncCore>d__);
			return <SendAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0006CFB8 File Offset: 0x0006B1B8
		private Task SendFrameAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			WebSocketBase.<SendFrameAsync>d__48 <SendFrameAsync>d__;
			<SendFrameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendFrameAsync>d__.<>4__this = this;
			<SendFrameAsync>d__.sendBuffers = sendBuffers;
			<SendFrameAsync>d__.cancellationToken = cancellationToken;
			<SendFrameAsync>d__.<>1__state = -1;
			<SendFrameAsync>d__.<>t__builder.Start<WebSocketBase.<SendFrameAsync>d__48>(ref <SendFrameAsync>d__);
			return <SendFrameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006D00C File Offset: 0x0006B20C
		public override void Abort()
		{
			if (WebSocketBase.s_LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "Abort", string.Empty);
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				if (!WebSocket.IsStateTerminal(this.State))
				{
					this.TakeLocks(ref flag, ref flag2);
					if (!WebSocket.IsStateTerminal(this.State))
					{
						this.m_State = WebSocketState.Aborted;
						if (this.SessionHandle != null && !this.SessionHandle.IsClosed && !this.SessionHandle.IsInvalid)
						{
							WebSocketProtocolComponent.WebSocketAbortHandle(this.SessionHandle);
						}
						this.m_ReceiveOutstandingOperationHelper.CancelIO();
						this.m_SendOutstandingOperationHelper.CancelIO();
						this.m_CloseOutputOutstandingOperationHelper.CancelIO();
						this.m_CloseOutstandingOperationHelper.CancelIO();
						if (this.m_InnerStreamAsWebSocketStream != null)
						{
							this.m_InnerStreamAsWebSocketStream.Abort();
						}
						this.CleanUp();
					}
				}
			}
			finally
			{
				this.ReleaseLocks(ref flag, ref flag2);
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "Abort", string.Empty);
				}
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0006D120 File Offset: 0x0006B320
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateCloseStatus(closeStatus, statusDescription);
			return this.CloseOutputAsyncCore(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0006D134 File Offset: 0x0006B334
		private Task CloseOutputAsyncCore(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketBase.<CloseOutputAsyncCore>d__51 <CloseOutputAsyncCore>d__;
			<CloseOutputAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseOutputAsyncCore>d__.<>4__this = this;
			<CloseOutputAsyncCore>d__.closeStatus = closeStatus;
			<CloseOutputAsyncCore>d__.statusDescription = statusDescription;
			<CloseOutputAsyncCore>d__.cancellationToken = cancellationToken;
			<CloseOutputAsyncCore>d__.<>1__state = -1;
			<CloseOutputAsyncCore>d__.<>t__builder.Start<WebSocketBase.<CloseOutputAsyncCore>d__51>(ref <CloseOutputAsyncCore>d__);
			return <CloseOutputAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0006D190 File Offset: 0x0006B390
		private bool OnCloseOutputCompleted()
		{
			if (WebSocket.IsStateTerminal(this.State))
			{
				return false;
			}
			WebSocketState state = this.State;
			if (state != WebSocketState.Open)
			{
				return state == WebSocketState.CloseReceived;
			}
			this.m_State = WebSocketState.CloseSent;
			return false;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0006D1CC File Offset: 0x0006B3CC
		private Task<bool> StartOnCloseCompleted(bool thisLockTakenSnapshot, bool sessionHandleLockTakenSnapshot, CancellationToken cancellationToken)
		{
			WebSocketBase.<StartOnCloseCompleted>d__53 <StartOnCloseCompleted>d__;
			<StartOnCloseCompleted>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<StartOnCloseCompleted>d__.<>4__this = this;
			<StartOnCloseCompleted>d__.thisLockTakenSnapshot = thisLockTakenSnapshot;
			<StartOnCloseCompleted>d__.sessionHandleLockTakenSnapshot = sessionHandleLockTakenSnapshot;
			<StartOnCloseCompleted>d__.cancellationToken = cancellationToken;
			<StartOnCloseCompleted>d__.<>1__state = -1;
			<StartOnCloseCompleted>d__.<>t__builder.Start<WebSocketBase.<StartOnCloseCompleted>d__53>(ref <StartOnCloseCompleted>d__);
			return <StartOnCloseCompleted>d__.<>t__builder.Task;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0006D227 File Offset: 0x0006B427
		private void FinishOnCloseCompleted()
		{
			this.CleanUp();
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006D22F File Offset: 0x0006B42F
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateCloseStatus(closeStatus, statusDescription);
			return this.CloseAsyncCore(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0006D244 File Offset: 0x0006B444
		private Task CloseAsyncCore(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketBase.<CloseAsyncCore>d__56 <CloseAsyncCore>d__;
			<CloseAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseAsyncCore>d__.<>4__this = this;
			<CloseAsyncCore>d__.closeStatus = closeStatus;
			<CloseAsyncCore>d__.statusDescription = statusDescription;
			<CloseAsyncCore>d__.cancellationToken = cancellationToken;
			<CloseAsyncCore>d__.<>1__state = -1;
			<CloseAsyncCore>d__.<>t__builder.Start<WebSocketBase.<CloseAsyncCore>d__56>(ref <CloseAsyncCore>d__);
			return <CloseAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0006D2A0 File Offset: 0x0006B4A0
		public override void Dispose()
		{
			if (this.m_IsDisposed)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				this.TakeLocks(ref flag, ref flag2);
				if (!this.m_IsDisposed)
				{
					if (!WebSocket.IsStateTerminal(this.State))
					{
						this.Abort();
					}
					else
					{
						this.CleanUp();
					}
					this.m_IsDisposed = true;
				}
			}
			finally
			{
				this.ReleaseLocks(ref flag, ref flag2);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0006D314 File Offset: 0x0006B514
		private void ResetFlagAndTakeLock(object lockObject, ref bool thisLockTaken)
		{
			thisLockTaken = false;
			Monitor.Enter(lockObject, ref thisLockTaken);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0006D320 File Offset: 0x0006B520
		private void ResetFlagsAndTakeLocks(ref bool thisLockTaken, ref bool sessionHandleLockTaken)
		{
			thisLockTaken = false;
			sessionHandleLockTaken = false;
			this.TakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0006D330 File Offset: 0x0006B530
		private void TakeLocks(ref bool thisLockTaken, ref bool sessionHandleLockTaken)
		{
			Monitor.Enter(this.SessionHandle, ref sessionHandleLockTaken);
			Monitor.Enter(this.m_ThisLock, ref thisLockTaken);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0006D34C File Offset: 0x0006B54C
		private void ReleaseLocks(ref bool thisLockTaken, ref bool sessionHandleLockTaken)
		{
			if (thisLockTaken | sessionHandleLockTaken)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (thisLockTaken)
					{
						Monitor.Exit(this.m_ThisLock);
						thisLockTaken = false;
					}
					if (sessionHandleLockTaken)
					{
						Monitor.Exit(this.SessionHandle);
						sessionHandleLockTaken = false;
					}
				}
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0006D39C File Offset: 0x0006B59C
		private void EnsureReceiveOperation()
		{
			if (this.m_ReceiveOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_ReceiveOperation == null)
					{
						this.m_ReceiveOperation = new WebSocketBase.WebSocketOperation.ReceiveOperation(this);
					}
				}
			}
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0006D3F8 File Offset: 0x0006B5F8
		private void EnsureSendOperation()
		{
			if (this.m_SendOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_SendOperation == null)
					{
						this.m_SendOperation = new WebSocketBase.WebSocketOperation.SendOperation(this);
					}
				}
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0006D454 File Offset: 0x0006B654
		private void EnsureKeepAliveOperation()
		{
			if (this.m_KeepAliveOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_KeepAliveOperation == null)
					{
						this.m_KeepAliveOperation = new WebSocketBase.WebSocketOperation.SendOperation(this)
						{
							BufferType = (WebSocketProtocolComponent.BufferType)2147483654U
						};
					}
				}
			}
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0006D4C0 File Offset: 0x0006B6C0
		private void EnsureCloseOutputOperation()
		{
			if (this.m_CloseOutputOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_CloseOutputOperation == null)
					{
						this.m_CloseOutputOperation = new WebSocketBase.WebSocketOperation.CloseOutputOperation(this);
					}
				}
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0006D51C File Offset: 0x0006B71C
		private static void ReleaseLock(object lockObject, ref bool lockTaken)
		{
			if (lockTaken)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					Monitor.Exit(lockObject);
					lockTaken = false;
				}
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0006D550 File Offset: 0x0006B750
		private static WebSocketProtocolComponent.BufferType GetBufferType(WebSocketMessageType messageType, bool endOfMessage)
		{
			if (messageType == WebSocketMessageType.Text)
			{
				if (endOfMessage)
				{
					return (WebSocketProtocolComponent.BufferType)2147483648U;
				}
				return (WebSocketProtocolComponent.BufferType)2147483649U;
			}
			else
			{
				if (endOfMessage)
				{
					return (WebSocketProtocolComponent.BufferType)2147483650U;
				}
				return (WebSocketProtocolComponent.BufferType)2147483651U;
			}
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x0006D574 File Offset: 0x0006B774
		private static WebSocketMessageType GetMessageType(WebSocketProtocolComponent.BufferType bufferType)
		{
			switch (bufferType)
			{
			case (WebSocketProtocolComponent.BufferType)2147483648U:
			case (WebSocketProtocolComponent.BufferType)2147483649U:
				return WebSocketMessageType.Text;
			case (WebSocketProtocolComponent.BufferType)2147483650U:
			case (WebSocketProtocolComponent.BufferType)2147483651U:
				return WebSocketMessageType.Binary;
			case (WebSocketProtocolComponent.BufferType)2147483652U:
				return WebSocketMessageType.Close;
			default:
				throw new WebSocketException(WebSocketError.NativeError, SR.GetString("net_WebSockets_InvalidBufferType", new object[]
				{
					bufferType,
					(WebSocketProtocolComponent.BufferType)2147483652U,
					(WebSocketProtocolComponent.BufferType)2147483651U,
					(WebSocketProtocolComponent.BufferType)2147483650U,
					(WebSocketProtocolComponent.BufferType)2147483649U,
					(WebSocketProtocolComponent.BufferType)2147483648U
				}));
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0006D609 File Offset: 0x0006B809
		internal void ValidateNativeBuffers(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount)
		{
			this.m_InternalBuffer.ValidateNativeBuffers(action, bufferType, dataBuffers, dataBufferCount);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0006D61C File Offset: 0x0006B81C
		internal void ThrowIfClosedOrAborted()
		{
			if (this.State == WebSocketState.Closed || this.State == WebSocketState.Aborted)
			{
				throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
				{
					base.GetType().FullName,
					this.State
				}));
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0006D66F File Offset: 0x0006B86F
		private void ThrowIfAborted(bool aborted, Exception innerException)
		{
			if (aborted)
			{
				throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
				{
					base.GetType().FullName,
					WebSocketState.Aborted
				}), innerException);
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0006D6A4 File Offset: 0x0006B8A4
		private bool CanHandleExceptionDuringClose(Exception error)
		{
			return this.State == WebSocketState.Closed && (error is OperationCanceledException || error is WebSocketException || error is SocketException || error is HttpListenerException || error is IOException);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0006D6DC File Offset: 0x0006B8DC
		private void ThrowIfConvertibleException(string methodName, Exception exception, CancellationToken cancellationToken, bool aborted)
		{
			if (WebSocketBase.s_LoggingEnabled && !string.IsNullOrEmpty(methodName))
			{
				Logging.Exception(Logging.WebSockets, this, methodName, exception);
			}
			OperationCanceledException ex = exception as OperationCanceledException;
			if (ex != null)
			{
				if (cancellationToken.IsCancellationRequested || !aborted)
				{
					return;
				}
				this.ThrowIfAborted(aborted, exception);
			}
			WebSocketException ex2 = exception as WebSocketException;
			if (ex2 != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				this.ThrowIfAborted(aborted, ex2);
				return;
			}
			SocketException ex3 = exception as SocketException;
			if (ex3 != null)
			{
				ex2 = new WebSocketException(ex3.NativeErrorCode, ex3);
			}
			HttpListenerException ex4 = exception as HttpListenerException;
			if (ex4 != null)
			{
				ex2 = new WebSocketException(ex4.ErrorCode, ex4);
			}
			IOException ex5 = exception as IOException;
			if (ex5 != null)
			{
				ex3 = (exception.InnerException as SocketException);
				if (ex3 != null)
				{
					ex2 = new WebSocketException(ex3.NativeErrorCode, ex5);
				}
			}
			if (ex2 != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				this.ThrowIfAborted(aborted, ex2);
				throw ex2;
			}
			AggregateException ex6 = exception as AggregateException;
			if (ex6 != null)
			{
				ReadOnlyCollection<Exception> innerExceptions = ex6.Flatten().InnerExceptions;
				if (innerExceptions.Count == 0)
				{
					return;
				}
				foreach (Exception exception2 in innerExceptions)
				{
					this.ThrowIfConvertibleException(null, exception2, cancellationToken, aborted);
				}
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0006D81C File Offset: 0x0006BA1C
		private void CleanUp()
		{
			if (this.m_CleanedUp)
			{
				return;
			}
			this.m_CleanedUp = true;
			if (this.SessionHandle != null)
			{
				this.SessionHandle.Dispose();
			}
			if (this.m_InternalBuffer != null)
			{
				this.m_InternalBuffer.Dispose(this.State);
			}
			if (this.m_ReceiveOutstandingOperationHelper != null)
			{
				this.m_ReceiveOutstandingOperationHelper.Dispose();
			}
			if (this.m_SendOutstandingOperationHelper != null)
			{
				this.m_SendOutstandingOperationHelper.Dispose();
			}
			if (this.m_CloseOutputOutstandingOperationHelper != null)
			{
				this.m_CloseOutputOutstandingOperationHelper.Dispose();
			}
			if (this.m_CloseOutstandingOperationHelper != null)
			{
				this.m_CloseOutstandingOperationHelper.Dispose();
			}
			if (this.m_InnerStream != null)
			{
				try
				{
					this.m_InnerStream.Close();
				}
				catch (ObjectDisposedException)
				{
				}
				catch (IOException)
				{
				}
				catch (SocketException)
				{
				}
				catch (HttpListenerException)
				{
				}
			}
			this.m_KeepAliveTracker.Dispose();
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0006D918 File Offset: 0x0006BB18
		private void OnBackgroundTaskException(Exception exception)
		{
			if (Interlocked.CompareExchange<Exception>(ref this.m_PendingException, exception, null) == null)
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exception(Logging.WebSockets, this, "Fault", exception);
				}
				this.Abort();
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0006D94C File Offset: 0x0006BB4C
		private void ThrowIfPendingException()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this.m_PendingException, null);
			if (ex != null)
			{
				throw new WebSocketException(WebSocketError.Faulted, ex);
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0006D971 File Offset: 0x0006BB71
		private void ThrowIfDisposed()
		{
			if (this.m_IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0006D990 File Offset: 0x0006BB90
		private void UpdateReceiveState(int newReceiveState, int expectedReceiveState)
		{
			int num = Interlocked.Exchange(ref this.m_ReceiveState, newReceiveState);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0006D9B0 File Offset: 0x0006BBB0
		private bool StartOnCloseReceived(ref bool thisLockTaken)
		{
			this.ThrowIfDisposed();
			if (WebSocket.IsStateTerminal(this.State) || this.State == WebSocketState.CloseReceived)
			{
				return false;
			}
			Monitor.Enter(this.m_ThisLock, ref thisLockTaken);
			if (WebSocket.IsStateTerminal(this.State) || this.State == WebSocketState.CloseReceived)
			{
				return false;
			}
			if (this.State == WebSocketState.Open)
			{
				this.m_State = WebSocketState.CloseReceived;
				if (this.m_CloseReceivedTaskCompletionSource == null)
				{
					this.m_CloseReceivedTaskCompletionSource = new TaskCompletionSource<object>();
				}
				return false;
			}
			return true;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0006DA2C File Offset: 0x0006BC2C
		private void FinishOnCloseReceived(WebSocketCloseStatus closeStatus, string closeStatusDescription)
		{
			if (this.m_CloseReceivedTaskCompletionSource != null)
			{
				this.m_CloseReceivedTaskCompletionSource.TrySetResult(null);
			}
			this.m_CloseStatus = new WebSocketCloseStatus?(closeStatus);
			this.m_CloseStatusDescription = closeStatusDescription;
			if (WebSocketBase.s_LoggingEnabled)
			{
				string param = string.Format(CultureInfo.InvariantCulture, "closeStatus: {0}, closeStatusDescription: {1}, m_State: {2}", new object[]
				{
					closeStatus,
					closeStatusDescription,
					this.m_State
				});
				Logging.PrintInfo(Logging.WebSockets, this, "FinishOnCloseReceived", param);
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0006DAB4 File Offset: 0x0006BCB4
		private static void OnKeepAlive(object sender)
		{
			WebSocketBase.<OnKeepAlive>d__81 <OnKeepAlive>d__;
			<OnKeepAlive>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnKeepAlive>d__.sender = sender;
			<OnKeepAlive>d__.<>1__state = -1;
			<OnKeepAlive>d__.<>t__builder.Start<WebSocketBase.<OnKeepAlive>d__81>(ref <OnKeepAlive>d__);
		}

		// Token: 0x04001659 RID: 5721
		private static volatile bool s_LoggingEnabled;

		// Token: 0x0400165A RID: 5722
		private readonly WebSocketBase.OutstandingOperationHelper m_CloseOutstandingOperationHelper;

		// Token: 0x0400165B RID: 5723
		private readonly WebSocketBase.OutstandingOperationHelper m_CloseOutputOutstandingOperationHelper;

		// Token: 0x0400165C RID: 5724
		private readonly WebSocketBase.OutstandingOperationHelper m_ReceiveOutstandingOperationHelper;

		// Token: 0x0400165D RID: 5725
		private readonly WebSocketBase.OutstandingOperationHelper m_SendOutstandingOperationHelper;

		// Token: 0x0400165E RID: 5726
		private readonly Stream m_InnerStream;

		// Token: 0x0400165F RID: 5727
		private readonly WebSocketBase.IWebSocketStream m_InnerStreamAsWebSocketStream;

		// Token: 0x04001660 RID: 5728
		private readonly string m_SubProtocol;

		// Token: 0x04001661 RID: 5729
		private readonly SemaphoreSlim m_SendFrameThrottle;

		// Token: 0x04001662 RID: 5730
		private readonly object m_ThisLock;

		// Token: 0x04001663 RID: 5731
		private readonly WebSocketBuffer m_InternalBuffer;

		// Token: 0x04001664 RID: 5732
		private readonly WebSocketBase.KeepAliveTracker m_KeepAliveTracker;

		// Token: 0x04001665 RID: 5733
		private volatile bool m_CleanedUp;

		// Token: 0x04001666 RID: 5734
		private volatile TaskCompletionSource<object> m_CloseReceivedTaskCompletionSource;

		// Token: 0x04001667 RID: 5735
		private volatile Task m_CloseOutputTask;

		// Token: 0x04001668 RID: 5736
		private volatile bool m_IsDisposed;

		// Token: 0x04001669 RID: 5737
		private volatile Task m_CloseNetworkConnectionTask;

		// Token: 0x0400166A RID: 5738
		private volatile bool m_CloseAsyncStartedReceive;

		// Token: 0x0400166B RID: 5739
		private volatile WebSocketState m_State;

		// Token: 0x0400166C RID: 5740
		private volatile Task m_KeepAliveTask;

		// Token: 0x0400166D RID: 5741
		private volatile WebSocketBase.WebSocketOperation.ReceiveOperation m_ReceiveOperation;

		// Token: 0x0400166E RID: 5742
		private volatile WebSocketBase.WebSocketOperation.SendOperation m_SendOperation;

		// Token: 0x0400166F RID: 5743
		private volatile WebSocketBase.WebSocketOperation.SendOperation m_KeepAliveOperation;

		// Token: 0x04001670 RID: 5744
		private volatile WebSocketBase.WebSocketOperation.CloseOutputOperation m_CloseOutputOperation;

		// Token: 0x04001671 RID: 5745
		private WebSocketCloseStatus? m_CloseStatus;

		// Token: 0x04001672 RID: 5746
		private string m_CloseStatusDescription;

		// Token: 0x04001673 RID: 5747
		private int m_ReceiveState;

		// Token: 0x04001674 RID: 5748
		private Exception m_PendingException;

		// Token: 0x0200076C RID: 1900
		private abstract class WebSocketOperation
		{
			// Token: 0x17000F2C RID: 3884
			// (get) Token: 0x06004261 RID: 16993 RVA: 0x0011388A File Offset: 0x00111A8A
			// (set) Token: 0x06004262 RID: 16994 RVA: 0x00113892 File Offset: 0x00111A92
			protected bool AsyncOperationCompleted { get; set; }

			// Token: 0x06004263 RID: 16995 RVA: 0x0011389B File Offset: 0x00111A9B
			internal WebSocketOperation(WebSocketBase webSocket)
			{
				this.m_WebSocket = webSocket;
				this.AsyncOperationCompleted = false;
			}

			// Token: 0x17000F2D RID: 3885
			// (get) Token: 0x06004264 RID: 16996 RVA: 0x001138B1 File Offset: 0x00111AB1
			// (set) Token: 0x06004265 RID: 16997 RVA: 0x001138B9 File Offset: 0x00111AB9
			public WebSocketReceiveResult ReceiveResult { get; protected set; }

			// Token: 0x17000F2E RID: 3886
			// (get) Token: 0x06004266 RID: 16998
			protected abstract int BufferCount { get; }

			// Token: 0x17000F2F RID: 3887
			// (get) Token: 0x06004267 RID: 16999
			protected abstract WebSocketProtocolComponent.ActionQueue ActionQueue { get; }

			// Token: 0x06004268 RID: 17000
			protected abstract void Initialize(ArraySegment<byte>? buffer, CancellationToken cancellationToken);

			// Token: 0x06004269 RID: 17001
			protected abstract bool ShouldContinue(CancellationToken cancellationToken);

			// Token: 0x0600426A RID: 17002
			protected abstract bool ProcessAction_NoAction();

			// Token: 0x0600426B RID: 17003 RVA: 0x001138C2 File Offset: 0x00111AC2
			protected virtual void ProcessAction_IndicateReceiveComplete(ArraySegment<byte>? buffer, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount, IntPtr actionContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600426C RID: 17004
			protected abstract void Cleanup();

			// Token: 0x0600426D RID: 17005 RVA: 0x001138CC File Offset: 0x00111ACC
			internal Task<WebSocketReceiveResult> Process(ArraySegment<byte>? buffer, CancellationToken cancellationToken)
			{
				WebSocketBase.WebSocketOperation.<Process>d__19 <Process>d__;
				<Process>d__.<>t__builder = AsyncTaskMethodBuilder<WebSocketReceiveResult>.Create();
				<Process>d__.<>4__this = this;
				<Process>d__.buffer = buffer;
				<Process>d__.cancellationToken = cancellationToken;
				<Process>d__.<>1__state = -1;
				<Process>d__.<>t__builder.Start<WebSocketBase.WebSocketOperation.<Process>d__19>(ref <Process>d__);
				return <Process>d__.<>t__builder.Task;
			}

			// Token: 0x0400326C RID: 12908
			private readonly WebSocketBase m_WebSocket;

			// Token: 0x02000919 RID: 2329
			public class ReceiveOperation : WebSocketBase.WebSocketOperation
			{
				// Token: 0x06004651 RID: 18001 RVA: 0x00125AA4 File Offset: 0x00123CA4
				public ReceiveOperation(WebSocketBase webSocket) : base(webSocket)
				{
				}

				// Token: 0x17000FE0 RID: 4064
				// (get) Token: 0x06004652 RID: 18002 RVA: 0x00125AAD File Offset: 0x00123CAD
				protected override WebSocketProtocolComponent.ActionQueue ActionQueue
				{
					get
					{
						return WebSocketProtocolComponent.ActionQueue.Receive;
					}
				}

				// Token: 0x17000FE1 RID: 4065
				// (get) Token: 0x06004653 RID: 18003 RVA: 0x00125AB0 File Offset: 0x00123CB0
				protected override int BufferCount
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x06004654 RID: 18004 RVA: 0x00125AB4 File Offset: 0x00123CB4
				protected override void Initialize(ArraySegment<byte>? buffer, CancellationToken cancellationToken)
				{
					this.m_PongReceived = false;
					this.m_ReceiveCompleted = false;
					this.m_WebSocket.ThrowIfDisposed();
					switch (Interlocked.CompareExchange(ref this.m_WebSocket.m_ReceiveState, 1, 0))
					{
					case 0:
						this.m_ReceiveState = 1;
						return;
					case 1:
						break;
					case 2:
					{
						WebSocketReceiveResult receiveResult;
						if (!this.m_WebSocket.m_InternalBuffer.ReceiveFromBufferedPayload(buffer.Value, out receiveResult))
						{
							this.m_WebSocket.UpdateReceiveState(0, 2);
						}
						base.ReceiveResult = receiveResult;
						this.m_ReceiveCompleted = true;
						break;
					}
					default:
						return;
					}
				}

				// Token: 0x06004655 RID: 18005 RVA: 0x00125B3E File Offset: 0x00123D3E
				protected override void Cleanup()
				{
				}

				// Token: 0x06004656 RID: 18006 RVA: 0x00125B40 File Offset: 0x00123D40
				protected override bool ShouldContinue(CancellationToken cancellationToken)
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (this.m_ReceiveCompleted)
					{
						return false;
					}
					this.m_WebSocket.ThrowIfDisposed();
					this.m_WebSocket.ThrowIfPendingException();
					WebSocketProtocolComponent.WebSocketReceive(this.m_WebSocket);
					return true;
				}

				// Token: 0x06004657 RID: 18007 RVA: 0x00125B75 File Offset: 0x00123D75
				protected override bool ProcessAction_NoAction()
				{
					if (this.m_PongReceived)
					{
						this.m_ReceiveCompleted = false;
						this.m_PongReceived = false;
						return false;
					}
					this.m_ReceiveCompleted = true;
					return base.ReceiveResult.MessageType == WebSocketMessageType.Close;
				}

				// Token: 0x06004658 RID: 18008 RVA: 0x00125BA8 File Offset: 0x00123DA8
				protected override void ProcessAction_IndicateReceiveComplete(ArraySegment<byte>? buffer, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount, IntPtr actionContext)
				{
					int num = 0;
					this.m_PongReceived = false;
					if (bufferType == (WebSocketProtocolComponent.BufferType)2147483653U)
					{
						this.m_PongReceived = true;
						WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, num);
						return;
					}
					WebSocketReceiveResult receiveResult;
					try
					{
						WebSocketMessageType messageType = WebSocketBase.GetMessageType(bufferType);
						int newReceiveState = 0;
						if (bufferType == (WebSocketProtocolComponent.BufferType)2147483652U)
						{
							ArraySegment<byte> payload = WebSocketHelpers.EmptyPayload;
							WebSocketCloseStatus value;
							string closeStatusDescription;
							this.m_WebSocket.m_InternalBuffer.ConvertCloseBuffer(action, dataBuffers[0], out value, out closeStatusDescription);
							receiveResult = new WebSocketReceiveResult(num, messageType, true, new WebSocketCloseStatus?(value), closeStatusDescription);
						}
						else
						{
							ArraySegment<byte> payload = this.m_WebSocket.m_InternalBuffer.ConvertNativeBuffer(action, dataBuffers[0], bufferType);
							bool endOfMessage = bufferType == (WebSocketProtocolComponent.BufferType)2147483650U || bufferType == (WebSocketProtocolComponent.BufferType)2147483648U || bufferType == (WebSocketProtocolComponent.BufferType)2147483652U;
							if (payload.Count > buffer.Value.Count)
							{
								this.m_WebSocket.m_InternalBuffer.BufferPayload(payload, buffer.Value.Count, messageType, endOfMessage);
								newReceiveState = 2;
								endOfMessage = false;
							}
							num = Math.Min(payload.Count, buffer.Value.Count);
							if (num > 0)
							{
								Buffer.BlockCopy(payload.Array, payload.Offset, buffer.Value.Array, buffer.Value.Offset, num);
							}
							receiveResult = new WebSocketReceiveResult(num, messageType, endOfMessage);
						}
						this.m_WebSocket.UpdateReceiveState(newReceiveState, this.m_ReceiveState);
					}
					finally
					{
						WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, num);
					}
					base.ReceiveResult = receiveResult;
				}

				// Token: 0x04003D84 RID: 15748
				private int m_ReceiveState;

				// Token: 0x04003D85 RID: 15749
				private bool m_PongReceived;

				// Token: 0x04003D86 RID: 15750
				private bool m_ReceiveCompleted;
			}

			// Token: 0x0200091A RID: 2330
			public class SendOperation : WebSocketBase.WebSocketOperation
			{
				// Token: 0x06004659 RID: 18009 RVA: 0x00125D4C File Offset: 0x00123F4C
				public SendOperation(WebSocketBase webSocket) : base(webSocket)
				{
				}

				// Token: 0x17000FE2 RID: 4066
				// (get) Token: 0x0600465A RID: 18010 RVA: 0x00125D55 File Offset: 0x00123F55
				protected override WebSocketProtocolComponent.ActionQueue ActionQueue
				{
					get
					{
						return WebSocketProtocolComponent.ActionQueue.Send;
					}
				}

				// Token: 0x17000FE3 RID: 4067
				// (get) Token: 0x0600465B RID: 18011 RVA: 0x00125D58 File Offset: 0x00123F58
				protected override int BufferCount
				{
					get
					{
						return 2;
					}
				}

				// Token: 0x0600465C RID: 18012 RVA: 0x00125D5C File Offset: 0x00123F5C
				protected virtual WebSocketProtocolComponent.Buffer? CreateBuffer(ArraySegment<byte>? buffer)
				{
					if (buffer == null)
					{
						return null;
					}
					WebSocketProtocolComponent.Buffer value = default(WebSocketProtocolComponent.Buffer);
					this.m_WebSocket.m_InternalBuffer.PinSendBuffer(buffer.Value, out this.m_BufferHasBeenPinned);
					value.Data.BufferData = this.m_WebSocket.m_InternalBuffer.ConvertPinnedSendPayloadToNative(buffer.Value);
					value.Data.BufferLength = (uint)buffer.Value.Count;
					return new WebSocketProtocolComponent.Buffer?(value);
				}

				// Token: 0x0600465D RID: 18013 RVA: 0x00125DE5 File Offset: 0x00123FE5
				protected override bool ProcessAction_NoAction()
				{
					return false;
				}

				// Token: 0x0600465E RID: 18014 RVA: 0x00125DE8 File Offset: 0x00123FE8
				protected override void Cleanup()
				{
					if (this.m_BufferHasBeenPinned)
					{
						this.m_BufferHasBeenPinned = false;
						this.m_WebSocket.m_InternalBuffer.ReleasePinnedSendBuffer();
					}
				}

				// Token: 0x17000FE4 RID: 4068
				// (get) Token: 0x0600465F RID: 18015 RVA: 0x00125E09 File Offset: 0x00124009
				// (set) Token: 0x06004660 RID: 18016 RVA: 0x00125E11 File Offset: 0x00124011
				internal WebSocketProtocolComponent.BufferType BufferType { get; set; }

				// Token: 0x06004661 RID: 18017 RVA: 0x00125E1C File Offset: 0x0012401C
				protected override void Initialize(ArraySegment<byte>? buffer, CancellationToken cancellationToken)
				{
					this.m_WebSocket.ThrowIfDisposed();
					this.m_WebSocket.ThrowIfPendingException();
					WebSocketProtocolComponent.Buffer? buffer2 = this.CreateBuffer(buffer);
					if (buffer2 != null)
					{
						WebSocketProtocolComponent.WebSocketSend(this.m_WebSocket, this.BufferType, buffer2.Value);
						return;
					}
					WebSocketProtocolComponent.WebSocketSendWithoutBody(this.m_WebSocket, this.BufferType);
				}

				// Token: 0x06004662 RID: 18018 RVA: 0x00125E7A File Offset: 0x0012407A
				protected override bool ShouldContinue(CancellationToken cancellationToken)
				{
					if (base.AsyncOperationCompleted)
					{
						return false;
					}
					cancellationToken.ThrowIfCancellationRequested();
					return true;
				}

				// Token: 0x04003D87 RID: 15751
				protected bool m_BufferHasBeenPinned;
			}

			// Token: 0x0200091B RID: 2331
			public class CloseOutputOperation : WebSocketBase.WebSocketOperation.SendOperation
			{
				// Token: 0x06004663 RID: 18019 RVA: 0x00125E8E File Offset: 0x0012408E
				public CloseOutputOperation(WebSocketBase webSocket) : base(webSocket)
				{
					base.BufferType = (WebSocketProtocolComponent.BufferType)2147483652U;
				}

				// Token: 0x17000FE5 RID: 4069
				// (get) Token: 0x06004664 RID: 18020 RVA: 0x00125EA2 File Offset: 0x001240A2
				// (set) Token: 0x06004665 RID: 18021 RVA: 0x00125EAA File Offset: 0x001240AA
				internal WebSocketCloseStatus CloseStatus { get; set; }

				// Token: 0x17000FE6 RID: 4070
				// (get) Token: 0x06004666 RID: 18022 RVA: 0x00125EB3 File Offset: 0x001240B3
				// (set) Token: 0x06004667 RID: 18023 RVA: 0x00125EBB File Offset: 0x001240BB
				internal string CloseReason { get; set; }

				// Token: 0x06004668 RID: 18024 RVA: 0x00125EC4 File Offset: 0x001240C4
				protected override WebSocketProtocolComponent.Buffer? CreateBuffer(ArraySegment<byte>? buffer)
				{
					this.m_WebSocket.ThrowIfDisposed();
					this.m_WebSocket.ThrowIfPendingException();
					if (this.CloseStatus == WebSocketCloseStatus.Empty)
					{
						return null;
					}
					WebSocketProtocolComponent.Buffer value = default(WebSocketProtocolComponent.Buffer);
					if (this.CloseReason != null)
					{
						byte[] bytes = Encoding.UTF8.GetBytes(this.CloseReason);
						ArraySegment<byte> payload = new ArraySegment<byte>(bytes, 0, Math.Min(123, bytes.Length));
						this.m_WebSocket.m_InternalBuffer.PinSendBuffer(payload, out this.m_BufferHasBeenPinned);
						value.CloseStatus.ReasonData = this.m_WebSocket.m_InternalBuffer.ConvertPinnedSendPayloadToNative(payload);
						value.CloseStatus.ReasonLength = (uint)payload.Count;
					}
					value.CloseStatus.CloseStatus = (ushort)this.CloseStatus;
					return new WebSocketProtocolComponent.Buffer?(value);
				}
			}
		}

		// Token: 0x0200076D RID: 1901
		private abstract class KeepAliveTracker : IDisposable
		{
			// Token: 0x0600426E RID: 17006
			public abstract void OnDataReceived();

			// Token: 0x0600426F RID: 17007
			public abstract void OnDataSent();

			// Token: 0x06004270 RID: 17008
			public abstract void Dispose();

			// Token: 0x06004271 RID: 17009
			public abstract void StartTimer(WebSocketBase webSocket);

			// Token: 0x06004272 RID: 17010
			public abstract void ResetTimer();

			// Token: 0x06004273 RID: 17011
			public abstract bool ShouldSendKeepAlive();

			// Token: 0x06004274 RID: 17012 RVA: 0x0011391F File Offset: 0x00111B1F
			public static WebSocketBase.KeepAliveTracker Create(TimeSpan keepAliveInterval)
			{
				if ((int)keepAliveInterval.TotalMilliseconds > 0)
				{
					return new WebSocketBase.KeepAliveTracker.DefaultKeepAliveTracker(keepAliveInterval);
				}
				return new WebSocketBase.KeepAliveTracker.DisabledKeepAliveTracker();
			}

			// Token: 0x0200091D RID: 2333
			private class DisabledKeepAliveTracker : WebSocketBase.KeepAliveTracker
			{
				// Token: 0x0600466B RID: 18027 RVA: 0x001267CE File Offset: 0x001249CE
				public override void OnDataReceived()
				{
				}

				// Token: 0x0600466C RID: 18028 RVA: 0x001267D0 File Offset: 0x001249D0
				public override void OnDataSent()
				{
				}

				// Token: 0x0600466D RID: 18029 RVA: 0x001267D2 File Offset: 0x001249D2
				public override void ResetTimer()
				{
				}

				// Token: 0x0600466E RID: 18030 RVA: 0x001267D4 File Offset: 0x001249D4
				public override void StartTimer(WebSocketBase webSocket)
				{
				}

				// Token: 0x0600466F RID: 18031 RVA: 0x001267D6 File Offset: 0x001249D6
				public override bool ShouldSendKeepAlive()
				{
					return false;
				}

				// Token: 0x06004670 RID: 18032 RVA: 0x001267D9 File Offset: 0x001249D9
				public override void Dispose()
				{
				}
			}

			// Token: 0x0200091E RID: 2334
			private class DefaultKeepAliveTracker : WebSocketBase.KeepAliveTracker
			{
				// Token: 0x06004672 RID: 18034 RVA: 0x001267E3 File Offset: 0x001249E3
				public DefaultKeepAliveTracker(TimeSpan keepAliveInterval)
				{
					this.m_KeepAliveInterval = keepAliveInterval;
					this.m_LastSendActivity = new Stopwatch();
					this.m_LastReceiveActivity = new Stopwatch();
				}

				// Token: 0x06004673 RID: 18035 RVA: 0x00126808 File Offset: 0x00124A08
				public override void OnDataReceived()
				{
					this.m_LastReceiveActivity.Restart();
				}

				// Token: 0x06004674 RID: 18036 RVA: 0x00126815 File Offset: 0x00124A15
				public override void OnDataSent()
				{
					this.m_LastSendActivity.Restart();
				}

				// Token: 0x06004675 RID: 18037 RVA: 0x00126824 File Offset: 0x00124A24
				public override void ResetTimer()
				{
					this.ResetTimer((int)this.m_KeepAliveInterval.TotalMilliseconds);
				}

				// Token: 0x06004676 RID: 18038 RVA: 0x00126848 File Offset: 0x00124A48
				public override void StartTimer(WebSocketBase webSocket)
				{
					int dueTime = (int)this.m_KeepAliveInterval.TotalMilliseconds;
					if (ExecutionContext.IsFlowSuppressed())
					{
						this.m_KeepAliveTimer = new Timer(WebSocketBase.KeepAliveTracker.DefaultKeepAliveTracker.s_KeepAliveTimerElapsedCallback, webSocket, -1, -1);
						this.m_KeepAliveTimer.Change(dueTime, -1);
						return;
					}
					using (ExecutionContext.SuppressFlow())
					{
						this.m_KeepAliveTimer = new Timer(WebSocketBase.KeepAliveTracker.DefaultKeepAliveTracker.s_KeepAliveTimerElapsedCallback, webSocket, -1, -1);
						this.m_KeepAliveTimer.Change(dueTime, -1);
					}
				}

				// Token: 0x06004677 RID: 18039 RVA: 0x001268D8 File Offset: 0x00124AD8
				public override bool ShouldSendKeepAlive()
				{
					TimeSpan idleTime = this.GetIdleTime();
					if (idleTime >= this.m_KeepAliveInterval)
					{
						return true;
					}
					this.ResetTimer((int)(this.m_KeepAliveInterval - idleTime).TotalMilliseconds);
					return false;
				}

				// Token: 0x06004678 RID: 18040 RVA: 0x00126918 File Offset: 0x00124B18
				public override void Dispose()
				{
					this.m_KeepAliveTimer.Dispose();
				}

				// Token: 0x06004679 RID: 18041 RVA: 0x00126925 File Offset: 0x00124B25
				private void ResetTimer(int dueInMilliseconds)
				{
					this.m_KeepAliveTimer.Change(dueInMilliseconds, -1);
				}

				// Token: 0x0600467A RID: 18042 RVA: 0x00126938 File Offset: 0x00124B38
				private TimeSpan GetIdleTime()
				{
					TimeSpan timeElapsed = this.GetTimeElapsed(this.m_LastSendActivity);
					TimeSpan timeElapsed2 = this.GetTimeElapsed(this.m_LastReceiveActivity);
					if (timeElapsed2 < timeElapsed)
					{
						return timeElapsed2;
					}
					return timeElapsed;
				}

				// Token: 0x0600467B RID: 18043 RVA: 0x0012696B File Offset: 0x00124B6B
				private TimeSpan GetTimeElapsed(Stopwatch watch)
				{
					if (watch.IsRunning)
					{
						return watch.Elapsed;
					}
					return this.m_KeepAliveInterval;
				}

				// Token: 0x04003D9A RID: 15770
				private static readonly TimerCallback s_KeepAliveTimerElapsedCallback = new TimerCallback(WebSocketBase.OnKeepAlive);

				// Token: 0x04003D9B RID: 15771
				private readonly TimeSpan m_KeepAliveInterval;

				// Token: 0x04003D9C RID: 15772
				private readonly Stopwatch m_LastSendActivity;

				// Token: 0x04003D9D RID: 15773
				private readonly Stopwatch m_LastReceiveActivity;

				// Token: 0x04003D9E RID: 15774
				private Timer m_KeepAliveTimer;
			}
		}

		// Token: 0x0200076E RID: 1902
		private class OutstandingOperationHelper : IDisposable
		{
			// Token: 0x06004276 RID: 17014 RVA: 0x00113940 File Offset: 0x00111B40
			public bool TryStartOperation(CancellationToken userCancellationToken, out CancellationToken linkedCancellationToken)
			{
				linkedCancellationToken = CancellationToken.None;
				this.ThrowIfDisposed();
				object thisLock = this.m_ThisLock;
				bool result;
				lock (thisLock)
				{
					int num = this.m_OperationsOutstanding + 1;
					this.m_OperationsOutstanding = num;
					int num2 = num;
					if (num2 == 1)
					{
						linkedCancellationToken = this.CreateLinkedCancellationToken(userCancellationToken);
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06004277 RID: 17015 RVA: 0x001139BC File Offset: 0x00111BBC
			public void CompleteOperation(bool ownsCancellationTokenSource)
			{
				if (this.m_IsDisposed)
				{
					return;
				}
				CancellationTokenSource cancellationTokenSource = null;
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					this.m_OperationsOutstanding--;
					if (ownsCancellationTokenSource)
					{
						cancellationTokenSource = this.m_CancellationTokenSource;
						this.m_CancellationTokenSource = null;
					}
				}
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Dispose();
				}
			}

			// Token: 0x06004278 RID: 17016 RVA: 0x00113A34 File Offset: 0x00111C34
			private CancellationToken CreateLinkedCancellationToken(CancellationToken cancellationToken)
			{
				CancellationTokenSource cancellationTokenSource;
				if (cancellationToken == CancellationToken.None)
				{
					cancellationTokenSource = new CancellationTokenSource();
				}
				else
				{
					cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, new CancellationTokenSource().Token);
				}
				this.m_CancellationTokenSource = cancellationTokenSource;
				return cancellationTokenSource.Token;
			}

			// Token: 0x06004279 RID: 17017 RVA: 0x00113A78 File Offset: 0x00111C78
			public void CancelIO()
			{
				CancellationTokenSource cancellationTokenSource = null;
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_OperationsOutstanding == 0)
					{
						return;
					}
					cancellationTokenSource = this.m_CancellationTokenSource;
				}
				if (cancellationTokenSource != null)
				{
					try
					{
						cancellationTokenSource.Cancel();
					}
					catch (ObjectDisposedException)
					{
					}
				}
			}

			// Token: 0x0600427A RID: 17018 RVA: 0x00113AE4 File Offset: 0x00111CE4
			public void Dispose()
			{
				if (this.m_IsDisposed)
				{
					return;
				}
				CancellationTokenSource cancellationTokenSource = null;
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_IsDisposed)
					{
						return;
					}
					this.m_IsDisposed = true;
					cancellationTokenSource = this.m_CancellationTokenSource;
					this.m_CancellationTokenSource = null;
				}
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Dispose();
				}
			}

			// Token: 0x0600427B RID: 17019 RVA: 0x00113B5C File Offset: 0x00111D5C
			private void ThrowIfDisposed()
			{
				if (this.m_IsDisposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
			}

			// Token: 0x0400326E RID: 12910
			private volatile int m_OperationsOutstanding;

			// Token: 0x0400326F RID: 12911
			private volatile CancellationTokenSource m_CancellationTokenSource;

			// Token: 0x04003270 RID: 12912
			private volatile bool m_IsDisposed;

			// Token: 0x04003271 RID: 12913
			private readonly object m_ThisLock = new object();
		}

		// Token: 0x0200076F RID: 1903
		internal interface IWebSocketStream
		{
			// Token: 0x0600427D RID: 17021
			void SwitchToOpaqueMode(WebSocketBase webSocket);

			// Token: 0x0600427E RID: 17022
			void Abort();

			// Token: 0x17000F30 RID: 3888
			// (get) Token: 0x0600427F RID: 17023
			bool SupportsMultipleWrite { get; }

			// Token: 0x06004280 RID: 17024
			Task MultipleWriteAsync(IList<ArraySegment<byte>> buffers, CancellationToken cancellationToken);

			// Token: 0x06004281 RID: 17025
			Task CloseNetworkConnectionAsync(CancellationToken cancellationToken);
		}

		// Token: 0x02000770 RID: 1904
		private static class ReceiveState
		{
			// Token: 0x04003272 RID: 12914
			internal const int SendOperation = -1;

			// Token: 0x04003273 RID: 12915
			internal const int Idle = 0;

			// Token: 0x04003274 RID: 12916
			internal const int Application = 1;

			// Token: 0x04003275 RID: 12917
			internal const int PayloadAvailable = 2;
		}

		// Token: 0x02000771 RID: 1905
		internal static class Methods
		{
			// Token: 0x04003276 RID: 12918
			internal const string ReceiveAsync = "ReceiveAsync";

			// Token: 0x04003277 RID: 12919
			internal const string SendAsync = "SendAsync";

			// Token: 0x04003278 RID: 12920
			internal const string CloseAsync = "CloseAsync";

			// Token: 0x04003279 RID: 12921
			internal const string CloseOutputAsync = "CloseOutputAsync";

			// Token: 0x0400327A RID: 12922
			internal const string Abort = "Abort";

			// Token: 0x0400327B RID: 12923
			internal const string Initialize = "Initialize";

			// Token: 0x0400327C RID: 12924
			internal const string Fault = "Fault";

			// Token: 0x0400327D RID: 12925
			internal const string StartOnCloseCompleted = "StartOnCloseCompleted";

			// Token: 0x0400327E RID: 12926
			internal const string FinishOnCloseReceived = "FinishOnCloseReceived";

			// Token: 0x0400327F RID: 12927
			internal const string OnKeepAlive = "OnKeepAlive";
		}
	}
}
