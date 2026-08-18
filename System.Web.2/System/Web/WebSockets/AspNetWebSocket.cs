using System;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.WebSockets
{
	// Token: 0x020001B6 RID: 438
	public sealed class AspNetWebSocket : WebSocket, IAsyncAbortableWebSocket
	{
		// Token: 0x06001683 RID: 5763 RVA: 0x0004768C File Offset: 0x0004588C
		internal AspNetWebSocket(IWebSocketPipe pipe, string subProtocol)
		{
			this._pipe = pipe;
			this._subProtocol = subProtocol;
			if (this._pipe == null)
			{
				this.Abort();
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x000476E0 File Offset: 0x000458E0
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				this.ThrowIfDisposed();
				WebSocketCloseStatus closeStatus = this._closeStatus;
				if (closeStatus == (WebSocketCloseStatus)(-1))
				{
					return null;
				}
				return new WebSocketCloseStatus?(closeStatus);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0004770E File Offset: 0x0004590E
		public override string CloseStatusDescription
		{
			get
			{
				this.ThrowIfDisposed();
				return this._closeStatusDescription;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0004771C File Offset: 0x0004591C
		public override WebSocketState State
		{
			get
			{
				this.ThrowIfDisposed();
				return this._state;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0004772A File Offset: 0x0004592A
		public override string SubProtocol
		{
			get
			{
				this.ThrowIfDisposed();
				return this._subProtocol;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00047738 File Offset: 0x00045938
		internal CountdownTask PendingOperationCounter
		{
			get
			{
				return this._pendingOperationCounter;
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00047740 File Offset: 0x00045940
		public override void Abort()
		{
			this.Abort(true, false);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0004774C File Offset: 0x0004594C
		private void Abort(bool throwIfDisposed, bool isDisposing = false)
		{
			object stateLockObj = this._stateLockObj;
			lock (stateLockObj)
			{
				if (throwIfDisposed)
				{
					this.ThrowIfDisposed();
				}
				try
				{
					if (WebSocket.IsStateTerminal(this._state))
					{
						return;
					}
					this._state = WebSocketState.Aborted;
				}
				finally
				{
					if (isDisposing)
					{
						this._disposed = true;
					}
				}
			}
			if (this._pipe != null)
			{
				this._pipe.CloseTcpConnection();
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000477D0 File Offset: 0x000459D0
		internal Task AbortAsync()
		{
			this.Abort(false, false);
			if (Interlocked.Exchange(ref this._abortAsyncCalled, 1) == 0)
			{
				this._pendingOperationCounter.MarkOperationCompleted();
			}
			return this._pendingOperationCounter.Task;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000477FE File Offset: 0x000459FE
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			return this.CloseAsyncImpl(closeStatus, statusDescription, cancellationToken, true)();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00047810 File Offset: 0x00045A10
		private Func<Task> CloseAsyncImpl(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken, bool performValidation = true)
		{
			AspNetWebSocket.<>c__DisplayClass30_0 CS$<>8__locals1 = new AspNetWebSocket.<>c__DisplayClass30_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.sendCloseTaskFunc = null;
			CS$<>8__locals1.receiveCloseTaskFunc = null;
			if (performValidation)
			{
				AspNetWebSocket.ValidateCloseStatusCodeAndDescription(closeStatus, ref statusDescription);
			}
			object stateLockObj = this._stateLockObj;
			lock (stateLockObj)
			{
				if (performValidation)
				{
					this.ThrowIfDisposed();
					this.ThrowIfAborted();
					this.ThrowIfSendUnavailable(true);
					this.ThrowIfReceiveUnavailable(true);
				}
				if (this._sendState != AspNetWebSocket.ChannelState.Closed)
				{
					CS$<>8__locals1.sendCloseTaskFunc = this.CloseOutputAsyncImpl(closeStatus, statusDescription, cancellationToken, false);
				}
				if (this._receiveState != AspNetWebSocket.ChannelState.Closed)
				{
					ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[123]);
					CS$<>8__locals1.receiveCloseTaskFunc = this.ReceiveAsyncImpl(buffer, cancellationToken, false);
				}
				if (CS$<>8__locals1.sendCloseTaskFunc == null && CS$<>8__locals1.receiveCloseTaskFunc == null)
				{
					return AspNetWebSocket._completedTaskFunc;
				}
			}
			return delegate()
			{
				AspNetWebSocket.<>c__DisplayClass30_0.<<CloseAsyncImpl>b__0>d <<CloseAsyncImpl>b__0>d;
				<<CloseAsyncImpl>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<CloseAsyncImpl>b__0>d.<>4__this = CS$<>8__locals1;
				<<CloseAsyncImpl>b__0>d.<>1__state = -1;
				<<CloseAsyncImpl>b__0>d.<>t__builder.Start<AspNetWebSocket.<>c__DisplayClass30_0.<<CloseAsyncImpl>b__0>d>(ref <<CloseAsyncImpl>b__0>d);
				return <<CloseAsyncImpl>b__0>d.<>t__builder.Task;
			};
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000478F8 File Offset: 0x00045AF8
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			return this.CloseOutputAsyncImpl(closeStatus, statusDescription, cancellationToken, true)();
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004790C File Offset: 0x00045B0C
		private Func<Task> CloseOutputAsyncImpl(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken, bool performValidation = true)
		{
			AspNetWebSocket.<>c__DisplayClass32_0 CS$<>8__locals1 = new AspNetWebSocket.<>c__DisplayClass32_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.closeStatus = closeStatus;
			CS$<>8__locals1.statusDescription = statusDescription;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			if (performValidation)
			{
				AspNetWebSocket.ValidateCloseStatusCodeAndDescription(CS$<>8__locals1.closeStatus, ref CS$<>8__locals1.statusDescription);
			}
			object stateLockObj = this._stateLockObj;
			lock (stateLockObj)
			{
				if (performValidation)
				{
					this.ThrowIfDisposed();
					this.ThrowIfAborted();
					this.ThrowIfSendUnavailable(true);
				}
				if (this._sendState == AspNetWebSocket.ChannelState.Closed)
				{
					return AspNetWebSocket._completedTaskFunc;
				}
				this._sendState = AspNetWebSocket.ChannelState.Busy;
				this._pendingOperationCounter.MarkOperationPending();
			}
			return delegate()
			{
				AspNetWebSocket.<>c__DisplayClass32_0.<<CloseOutputAsyncImpl>b__0>d <<CloseOutputAsyncImpl>b__0>d;
				<<CloseOutputAsyncImpl>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<CloseOutputAsyncImpl>b__0>d.<>4__this = CS$<>8__locals1;
				<<CloseOutputAsyncImpl>b__0>d.<>1__state = -1;
				<<CloseOutputAsyncImpl>b__0>d.<>t__builder.Start<AspNetWebSocket.<>c__DisplayClass32_0.<<CloseOutputAsyncImpl>b__0>d>(ref <<CloseOutputAsyncImpl>b__0>d);
				return <<CloseOutputAsyncImpl>b__0>d.<>t__builder.Task;
			};
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000479C8 File Offset: 0x00045BC8
		public override void Dispose()
		{
			throw new NotSupportedException(SR.GetString("AspNetWebSocket_DisposeNotSupported"));
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000479D9 File Offset: 0x00045BD9
		internal void DisposeInternal()
		{
			this.Abort(false, true);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000479E3 File Offset: 0x00045BE3
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			return this.ReceiveAsyncImpl(buffer, cancellationToken, true)();
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x000479F4 File Offset: 0x00045BF4
		private Func<Task<WebSocketReceiveResult>> ReceiveAsyncImpl(ArraySegment<byte> buffer, CancellationToken cancellationToken, bool performValidation = true)
		{
			AspNetWebSocket.<>c__DisplayClass36_0 CS$<>8__locals1 = new AspNetWebSocket.<>c__DisplayClass36_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			object stateLockObj = this._stateLockObj;
			lock (stateLockObj)
			{
				if (performValidation)
				{
					this.ThrowIfDisposed();
					this.ThrowIfAborted();
					this.ThrowIfReceiveUnavailable(false);
				}
				this._receiveState = AspNetWebSocket.ChannelState.Busy;
				this._pendingOperationCounter.MarkOperationPending();
			}
			return delegate()
			{
				AspNetWebSocket.<>c__DisplayClass36_0.<<ReceiveAsyncImpl>b__0>d <<ReceiveAsyncImpl>b__0>d;
				<<ReceiveAsyncImpl>b__0>d.<>t__builder = AsyncTaskMethodBuilder<WebSocketReceiveResult>.Create();
				<<ReceiveAsyncImpl>b__0>d.<>4__this = CS$<>8__locals1;
				<<ReceiveAsyncImpl>b__0>d.<>1__state = -1;
				<<ReceiveAsyncImpl>b__0>d.<>t__builder.Start<AspNetWebSocket.<>c__DisplayClass36_0.<<ReceiveAsyncImpl>b__0>d>(ref <<ReceiveAsyncImpl>b__0>d);
				return <<ReceiveAsyncImpl>b__0>d.<>t__builder.Task;
			};
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00047A80 File Offset: 0x00045C80
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			return this.SendAsyncImpl(buffer, messageType, endOfMessage, cancellationToken, true)();
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00047A94 File Offset: 0x00045C94
		private Func<Task> SendAsyncImpl(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken, bool performValidation = true)
		{
			AspNetWebSocket.<>c__DisplayClass38_0 CS$<>8__locals1 = new AspNetWebSocket.<>c__DisplayClass38_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.messageType = messageType;
			CS$<>8__locals1.endOfMessage = endOfMessage;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			if (performValidation)
			{
				AspNetWebSocket.ValidateSendMessageType(CS$<>8__locals1.messageType);
			}
			object stateLockObj = this._stateLockObj;
			lock (stateLockObj)
			{
				if (performValidation)
				{
					this.ThrowIfDisposed();
					this.ThrowIfAborted();
					this.ThrowIfSendUnavailable(false);
				}
				this._sendState = AspNetWebSocket.ChannelState.Busy;
				this._pendingOperationCounter.MarkOperationPending();
			}
			return delegate()
			{
				AspNetWebSocket.<>c__DisplayClass38_0.<<SendAsyncImpl>b__0>d <<SendAsyncImpl>b__0>d;
				<<SendAsyncImpl>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<SendAsyncImpl>b__0>d.<>4__this = CS$<>8__locals1;
				<<SendAsyncImpl>b__0>d.<>1__state = -1;
				<<SendAsyncImpl>b__0>d.<>t__builder.Start<AspNetWebSocket.<>c__DisplayClass38_0.<<SendAsyncImpl>b__0>d>(ref <<SendAsyncImpl>b__0>d);
				return <<SendAsyncImpl>b__0>d.<>t__builder.Task;
			};
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00047B3C File Offset: 0x00045D3C
		internal static void ValidateCloseStatusCodeAndDescription(WebSocketCloseStatus closeStatus, ref string statusDescription)
		{
			if (closeStatus < WebSocketCloseStatus.NormalClosure || closeStatus > (WebSocketCloseStatus)65535 || closeStatus == (WebSocketCloseStatus)1004 || closeStatus == (WebSocketCloseStatus)1006 || closeStatus == WebSocketCloseStatus.MandatoryExtension || closeStatus == (WebSocketCloseStatus)1015)
			{
				throw new ArgumentOutOfRangeException("closeStatus");
			}
			if (closeStatus == WebSocketCloseStatus.Empty)
			{
				if (statusDescription == string.Empty)
				{
					statusDescription = null;
				}
				if (statusDescription != null)
				{
					throw new ArgumentException(SR.GetString("AspNetWebSocket_CloseStatusEmptyButCloseDescriptionNonNull"), "statusDescription");
				}
			}
			else if (statusDescription != null)
			{
				int byteCount = Encoding.UTF8.GetByteCount(statusDescription);
				if (byteCount > 123)
				{
					throw new ArgumentException(SR.GetString("AspNetWebSocket_CloseDescriptionTooLong", new object[]
					{
						123
					}), "statusDescription");
				}
			}
			else
			{
				statusDescription = string.Empty;
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00047BF8 File Offset: 0x00045DF8
		private static void ValidateSendMessageType(WebSocketMessageType messageType)
		{
			if (messageType <= WebSocketMessageType.Binary)
			{
				return;
			}
			throw new ArgumentException(SR.GetString("AspNetWebSocket_SendMessageTypeInvalid"), "messageType");
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00047C13 File Offset: 0x00045E13
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00047C2E File Offset: 0x00045E2E
		private void ThrowIfAborted()
		{
			if (this._state == WebSocketState.Aborted)
			{
				throw new WebSocketException(WebSocketError.InvalidState);
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00047C44 File Offset: 0x00045E44
		private void ThrowIfSendUnavailable(bool allowClosed = false)
		{
			AspNetWebSocket.ChannelState sendState = this._sendState;
			if (sendState == AspNetWebSocket.ChannelState.Busy)
			{
				throw new InvalidOperationException(SR.GetString("AspNetWebSocket_SendInProgress"));
			}
			if (sendState != AspNetWebSocket.ChannelState.Closed)
			{
				return;
			}
			if (!allowClosed)
			{
				throw new InvalidOperationException(SR.GetString("AspNetWebSocket_CloseAlreadySent"));
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00047C84 File Offset: 0x00045E84
		private void ThrowIfReceiveUnavailable(bool allowClosed = false)
		{
			AspNetWebSocket.ChannelState receiveState = this._receiveState;
			if (receiveState == AspNetWebSocket.ChannelState.Busy)
			{
				throw new InvalidOperationException(SR.GetString("AspNetWebSocket_ReceiveInProgress"));
			}
			if (receiveState != AspNetWebSocket.ChannelState.Closed)
			{
				return;
			}
			if (!allowClosed)
			{
				throw new InvalidOperationException(SR.GetString("AspNetWebSocket_CloseAlreadyReceived"));
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00047CC4 File Offset: 0x00045EC4
		private Task<T> DoWork<T>(Func<Task<T>> taskDelegate, CancellationToken cancellationToken)
		{
			AspNetWebSocket.<DoWork>d__45<T> <DoWork>d__;
			<DoWork>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<DoWork>d__.<>4__this = this;
			<DoWork>d__.taskDelegate = taskDelegate;
			<DoWork>d__.cancellationToken = cancellationToken;
			<DoWork>d__.<>1__state = -1;
			<DoWork>d__.<>t__builder.Start<AspNetWebSocket.<DoWork>d__45<T>>(ref <DoWork>d__);
			return <DoWork>d__.<>t__builder.Task;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00047D18 File Offset: 0x00045F18
		internal Task DoWork(Func<Task> taskDelegate, CancellationToken cancellationToken)
		{
			AspNetWebSocket.<>c__DisplayClass46_0 CS$<>8__locals1 = new AspNetWebSocket.<>c__DisplayClass46_0();
			CS$<>8__locals1.taskDelegate = taskDelegate;
			return this.DoWork<object>(delegate()
			{
				AspNetWebSocket.<>c__DisplayClass46_0.<<DoWork>b__0>d <<DoWork>b__0>d;
				<<DoWork>b__0>d.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
				<<DoWork>b__0>d.<>4__this = CS$<>8__locals1;
				<<DoWork>b__0>d.<>1__state = -1;
				<<DoWork>b__0>d.<>t__builder.Start<AspNetWebSocket.<>c__DisplayClass46_0.<<DoWork>b__0>d>(ref <<DoWork>b__0>d);
				return <<DoWork>b__0>d.<>t__builder.Task;
			}, cancellationToken);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00047D45 File Offset: 0x00045F45
		Task IAsyncAbortableWebSocket.AbortAsync()
		{
			return this.AbortAsync();
		}

		// Token: 0x040016A8 RID: 5800
		private const int _maxCloseMessageByteCount = 123;

		// Token: 0x040016A9 RID: 5801
		private static readonly Task _completedTask = Task.FromResult<object>(null);

		// Token: 0x040016AA RID: 5802
		private static readonly Func<Task> _completedTaskFunc = () => AspNetWebSocket._completedTask;

		// Token: 0x040016AB RID: 5803
		private const WebSocketCloseStatus CLOSE_STATUS_NOT_SET = (WebSocketCloseStatus)(-1);

		// Token: 0x040016AC RID: 5804
		private WebSocketCloseStatus _closeStatus = (WebSocketCloseStatus)(-1);

		// Token: 0x040016AD RID: 5805
		private string _closeStatusDescription;

		// Token: 0x040016AE RID: 5806
		private bool _disposed;

		// Token: 0x040016AF RID: 5807
		private readonly IWebSocketPipe _pipe;

		// Token: 0x040016B0 RID: 5808
		private readonly string _subProtocol;

		// Token: 0x040016B1 RID: 5809
		private int _abortAsyncCalled;

		// Token: 0x040016B2 RID: 5810
		private CountdownTask _pendingOperationCounter = new CountdownTask(1);

		// Token: 0x040016B3 RID: 5811
		internal AspNetWebSocket.ChannelState _receiveState;

		// Token: 0x040016B4 RID: 5812
		internal AspNetWebSocket.ChannelState _sendState;

		// Token: 0x040016B5 RID: 5813
		internal WebSocketState _state = WebSocketState.Open;

		// Token: 0x040016B6 RID: 5814
		private readonly object _stateLockObj = new object();

		// Token: 0x02000915 RID: 2325
		internal enum ChannelState
		{
			// Token: 0x04003736 RID: 14134
			Ready,
			// Token: 0x04003737 RID: 14135
			Busy,
			// Token: 0x04003738 RID: 14136
			Closed
		}
	}
}
