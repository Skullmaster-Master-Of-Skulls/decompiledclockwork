using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000852 RID: 2130
	internal class SocketConnectionListener : IConnectionListener, IDisposable
	{
		// Token: 0x06004FEA RID: 20458 RVA: 0x00125595 File Offset: 0x00123795
		public SocketConnectionListener(Socket listenSocket, ISocketListenerSettings settings, bool useOnlyOverlappedIO) : this(settings, useOnlyOverlappedIO)
		{
			this.listenSocket = listenSocket;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x001255A6 File Offset: 0x001237A6
		public SocketConnectionListener(IPEndPoint localEndpoint, ISocketListenerSettings settings, bool useOnlyOverlappedIO) : this(settings, useOnlyOverlappedIO)
		{
			this.localEndpoint = localEndpoint;
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x001255B7 File Offset: 0x001237B7
		private SocketConnectionListener(ISocketListenerSettings settings, bool useOnlyOverlappedIO)
		{
			this.settings = settings;
			this.useOnlyOverlappedIO = useOnlyOverlappedIO;
			this.connectionBufferPool = new ConnectionBufferPool(settings.BufferSize);
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x001255DE File Offset: 0x001237DE
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x001255E1 File Offset: 0x001237E1
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			return new SocketConnectionListener.AcceptAsyncResult(this, callback, state);
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x001255EB File Offset: 0x001237EB
		private SocketAsyncEventArgs TakeSocketAsyncEventArgs()
		{
			return this.socketAsyncEventArgsPool.Take();
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x001255F8 File Offset: 0x001237F8
		private void ReturnSocketAsyncEventArgs(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			this.socketAsyncEventArgsPool.Return(socketAsyncEventArgs);
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x00125607 File Offset: 0x00123807
		private static int GetAcceptBufferSize(Socket listenSocket)
		{
			return (listenSocket.LocalEndPoint.Serialize().Size + 16) * 2;
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x00125620 File Offset: 0x00123820
		private bool InternalBeginAccept(Func<Socket, bool> acceptAsyncFunc)
		{
			object thisLock = this.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString(), SR.GetString("SocketListenerDisposed")));
				}
				if (!this.isListening)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SocketListenerNotListening")));
				}
				result = acceptAsyncFunc(this.listenSocket);
			}
			return result;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x001256B8 File Offset: 0x001238B8
		public IConnection EndAccept(IAsyncResult result)
		{
			Socket socket = SocketConnectionListener.AcceptAsyncResult.End(result);
			if (socket == null)
			{
				return null;
			}
			if (this.useOnlyOverlappedIO)
			{
				socket.UseOnlyOverlappedIO = true;
			}
			return new SocketConnection(socket, this.connectionBufferPool, false);
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x001256F0 File Offset: 0x001238F0
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.isDisposed)
				{
					if (this.listenSocket != null)
					{
						this.listenSocket.Close();
					}
					if (this.socketAsyncEventArgsPool != null)
					{
						this.socketAsyncEventArgsPool.Close();
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x00125760 File Offset: 0x00123960
		public void Listen()
		{
			TimeSpan timeout = TimeSpan.FromSeconds(1.0);
			BackoffTimeoutHelper backoffTimeoutHelper = new BackoffTimeoutHelper(timeout);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.listenSocket != null)
				{
					this.listenSocket.Listen(this.settings.ListenBacklog);
					this.isListening = true;
				}
				while (!this.isListening)
				{
					try
					{
						this.listenSocket = new Socket(this.localEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
						if (this.localEndpoint.AddressFamily == AddressFamily.InterNetworkV6 && this.settings.TeredoEnabled)
						{
							this.listenSocket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPProtectionLevel, 10);
						}
						this.listenSocket.Bind(this.localEndpoint);
						this.listenSocket.Listen(this.settings.ListenBacklog);
						this.isListening = true;
					}
					catch (SocketException ex)
					{
						bool flag2 = false;
						if (ex.ErrorCode == 10048 && !backoffTimeoutHelper.IsExpired())
						{
							backoffTimeoutHelper.WaitAndBackoff();
							flag2 = true;
						}
						if (!flag2)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SocketConnectionListener.ConvertListenException(ex, this.localEndpoint));
						}
					}
				}
				this.socketAsyncEventArgsPool = new SocketAsyncEventArgsPool(SocketConnectionListener.GetAcceptBufferSize(this.listenSocket));
			}
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x001258DC File Offset: 0x00123ADC
		public static Exception ConvertListenException(SocketException socketException, IPEndPoint localEndpoint)
		{
			if (socketException.ErrorCode == 6)
			{
				return new CommunicationObjectAbortedException(socketException.Message, socketException);
			}
			if (socketException.ErrorCode == 10048)
			{
				return new AddressAlreadyInUseException(SR.GetString("TcpAddressInUse", new object[]
				{
					localEndpoint.ToString()
				}), socketException);
			}
			return new CommunicationException(SR.GetString("TcpListenError", new object[]
			{
				socketException.ErrorCode,
				socketException.Message,
				localEndpoint.ToString()
			}), socketException);
		}

		// Token: 0x0400318E RID: 12686
		private IPEndPoint localEndpoint;

		// Token: 0x0400318F RID: 12687
		private bool isDisposed;

		// Token: 0x04003190 RID: 12688
		private bool isListening;

		// Token: 0x04003191 RID: 12689
		private Socket listenSocket;

		// Token: 0x04003192 RID: 12690
		private ISocketListenerSettings settings;

		// Token: 0x04003193 RID: 12691
		private bool useOnlyOverlappedIO;

		// Token: 0x04003194 RID: 12692
		private ConnectionBufferPool connectionBufferPool;

		// Token: 0x04003195 RID: 12693
		private SocketAsyncEventArgsPool socketAsyncEventArgsPool;

		// Token: 0x02000D3C RID: 3388
		private class AcceptAsyncResult : AsyncResult
		{
			// Token: 0x06007C40 RID: 31808 RVA: 0x001D073C File Offset: 0x001CE93C
			public AcceptAsyncResult(SocketConnectionListener listener, AsyncCallback callback, object state) : base(callback, state)
			{
				if (TD.SocketAcceptEnqueuedIsEnabled())
				{
					TD.SocketAcceptEnqueued(this.EventTraceActivity);
				}
				this.listener = listener;
				this.socketAsyncEventArgs = listener.TakeSocketAsyncEventArgs();
				this.socketAsyncEventArgs.UserToken = this;
				this.socketAsyncEventArgs.Completed += SocketConnectionListener.AcceptAsyncResult.acceptAsyncCompleted;
				base.OnCompleting = SocketConnectionListener.AcceptAsyncResult.onCompleting;
				if (!Thread.CurrentThread.IsThreadPoolThread)
				{
					if (SocketConnectionListener.AcceptAsyncResult.startAccept == null)
					{
						SocketConnectionListener.AcceptAsyncResult.startAccept = new Action<object>(SocketConnectionListener.AcceptAsyncResult.StartAccept);
					}
					ActionItem.Schedule(SocketConnectionListener.AcceptAsyncResult.startAccept, this);
					return;
				}
				bool flag = false;
				bool flag2;
				try
				{
					flag2 = this.StartAccept();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.ReturnSocketAsyncEventArgs();
					}
				}
				if (flag2)
				{
					base.Complete(true);
				}
			}

			// Token: 0x17001BD4 RID: 7124
			// (get) Token: 0x06007C41 RID: 31809 RVA: 0x001D0800 File Offset: 0x001CEA00
			public EventTraceActivity EventTraceActivity
			{
				get
				{
					if (this.eventTraceActivity == null)
					{
						this.eventTraceActivity = new EventTraceActivity(false);
					}
					return this.eventTraceActivity;
				}
			}

			// Token: 0x06007C42 RID: 31810 RVA: 0x001D081C File Offset: 0x001CEA1C
			private static void StartAccept(object state)
			{
				SocketConnectionListener.AcceptAsyncResult acceptAsyncResult = (SocketConnectionListener.AcceptAsyncResult)state;
				Exception exception = null;
				bool flag;
				try
				{
					flag = acceptAsyncResult.StartAccept();
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
					acceptAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007C43 RID: 31811 RVA: 0x001D0868 File Offset: 0x001CEA68
			private bool StartAccept()
			{
				bool result;
				for (;;)
				{
					try
					{
						result = this.listener.InternalBeginAccept(new Func<Socket, bool>(this.DoAcceptAsync));
					}
					catch (SocketException exception)
					{
						if (SocketConnectionListener.AcceptAsyncResult.ShouldAcceptRecover(exception))
						{
							continue;
						}
						throw;
					}
					break;
				}
				return result;
			}

			// Token: 0x06007C44 RID: 31812 RVA: 0x001D08B0 File Offset: 0x001CEAB0
			private static bool ShouldAcceptRecover(SocketException exception)
			{
				return exception.ErrorCode == 10054 || exception.ErrorCode == 10024 || exception.ErrorCode == 10055 || exception.ErrorCode == 10060;
			}

			// Token: 0x06007C45 RID: 31813 RVA: 0x001D08E8 File Offset: 0x001CEAE8
			private bool DoAcceptAsync(Socket listenSocket)
			{
				SocketAsyncEventArgsPool.CleanupAcceptSocket(this.socketAsyncEventArgs);
				if (listenSocket.AcceptAsync(this.socketAsyncEventArgs))
				{
					return false;
				}
				Exception ex = this.HandleAcceptAsyncCompleted();
				if (ex != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
				return true;
			}

			// Token: 0x06007C46 RID: 31814 RVA: 0x001D0928 File Offset: 0x001CEB28
			private static void AcceptAsyncCompleted(object sender, SocketAsyncEventArgs e)
			{
				SocketConnectionListener.AcceptAsyncResult acceptAsyncResult = (SocketConnectionListener.AcceptAsyncResult)e.UserToken;
				Exception ex = acceptAsyncResult.HandleAcceptAsyncCompleted();
				if (ex != null && SocketConnectionListener.AcceptAsyncResult.ShouldAcceptRecover((SocketException)ex))
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
					SocketConnectionListener.AcceptAsyncResult.StartAccept(acceptAsyncResult);
					return;
				}
				acceptAsyncResult.Complete(false, ex);
			}

			// Token: 0x06007C47 RID: 31815 RVA: 0x001D0970 File Offset: 0x001CEB70
			private static void OnInternalCompleting(AsyncResult result, Exception exception)
			{
				SocketConnectionListener.AcceptAsyncResult acceptAsyncResult = result as SocketConnectionListener.AcceptAsyncResult;
				if (TD.SocketAcceptedIsEnabled())
				{
					int num = (acceptAsyncResult.socket != null) ? acceptAsyncResult.socket.GetHashCode() : -1;
					if (num != -1)
					{
						TD.SocketAccepted(acceptAsyncResult.EventTraceActivity, (acceptAsyncResult.listener != null) ? acceptAsyncResult.listener.GetHashCode() : -1, num);
					}
					else
					{
						TD.SocketAcceptClosed(acceptAsyncResult.EventTraceActivity);
					}
				}
				acceptAsyncResult.ReturnSocketAsyncEventArgs();
			}

			// Token: 0x06007C48 RID: 31816 RVA: 0x001D09DB File Offset: 0x001CEBDB
			private void ReturnSocketAsyncEventArgs()
			{
				if (this.socketAsyncEventArgs != null)
				{
					this.socketAsyncEventArgs.UserToken = null;
					this.socketAsyncEventArgs.Completed -= SocketConnectionListener.AcceptAsyncResult.acceptAsyncCompleted;
					this.listener.ReturnSocketAsyncEventArgs(this.socketAsyncEventArgs);
					this.socketAsyncEventArgs = null;
				}
			}

			// Token: 0x06007C49 RID: 31817 RVA: 0x001D0A1C File Offset: 0x001CEC1C
			private Exception HandleAcceptAsyncCompleted()
			{
				Exception result = null;
				if (this.socketAsyncEventArgs.SocketError == SocketError.Success)
				{
					this.socket = this.socketAsyncEventArgs.AcceptSocket;
					this.socketAsyncEventArgs.AcceptSocket = null;
				}
				else
				{
					result = new SocketException((int)this.socketAsyncEventArgs.SocketError);
				}
				return result;
			}

			// Token: 0x06007C4A RID: 31818 RVA: 0x001D0A6C File Offset: 0x001CEC6C
			public static Socket End(IAsyncResult result)
			{
				SocketConnectionListener.AcceptAsyncResult acceptAsyncResult = AsyncResult.End<SocketConnectionListener.AcceptAsyncResult>(result);
				return acceptAsyncResult.socket;
			}

			// Token: 0x04004773 RID: 18291
			private SocketConnectionListener listener;

			// Token: 0x04004774 RID: 18292
			private Socket socket;

			// Token: 0x04004775 RID: 18293
			private SocketAsyncEventArgs socketAsyncEventArgs;

			// Token: 0x04004776 RID: 18294
			private static Action<object> startAccept;

			// Token: 0x04004777 RID: 18295
			private EventTraceActivity eventTraceActivity;

			// Token: 0x04004778 RID: 18296
			private static EventHandler<SocketAsyncEventArgs> acceptAsyncCompleted = new EventHandler<SocketAsyncEventArgs>(SocketConnectionListener.AcceptAsyncResult.AcceptAsyncCompleted);

			// Token: 0x04004779 RID: 18297
			private static Action<AsyncResult, Exception> onCompleting = new Action<AsyncResult, Exception>(SocketConnectionListener.AcceptAsyncResult.OnInternalCompleting);
		}
	}
}
