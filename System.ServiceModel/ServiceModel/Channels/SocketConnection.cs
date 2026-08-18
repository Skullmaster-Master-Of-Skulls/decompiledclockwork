using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200084F RID: 2127
	internal class SocketConnection : IConnection
	{
		// Token: 0x06004F93 RID: 20371 RVA: 0x00123268 File Offset: 0x00121468
		public SocketConnection(Socket socket, ConnectionBufferPool connectionBufferPool, bool autoBindToCompletionPort)
		{
			if (socket == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("socket");
			}
			this.closeState = SocketConnection.CloseState.Open;
			this.exceptionEventType = TraceEventType.Error;
			this.socket = socket;
			this.connectionBufferPool = connectionBufferPool;
			this.readBuffer = this.connectionBufferPool.Take();
			this.asyncReadBufferSize = this.readBuffer.Length;
			this.socket.SendBufferSize = (this.socket.ReceiveBufferSize = this.asyncReadBufferSize);
			this.asyncSendTimeout = (this.asyncReceiveTimeout = TimeSpan.MaxValue);
			this.socketSyncSendTimeout = (this.socketSyncReceiveTimeout = TimeSpan.MaxValue);
			this.remoteEndpoint = null;
			if (autoBindToCompletionPort)
			{
				this.socket.UseOnlyOverlappedIO = false;
			}
			if (this.socket.UseOnlyOverlappedIO && SocketConnection.onReceiveCompleted == null)
			{
				SocketConnection.onReceiveCompleted = Fx.ThunkCallback(new AsyncCallback(SocketConnection.OnReceiveCompleted));
			}
			this.TraceSocketInfo(socket, 262169, "TraceCodeSocketConnectionCreate", null);
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x00123362 File Offset: 0x00121562
		public int AsyncReadBufferSize
		{
			get
			{
				return this.asyncReadBufferSize;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06004F95 RID: 20373 RVA: 0x0012336A File Offset: 0x0012156A
		public byte[] AsyncReadBuffer
		{
			get
			{
				return this.readBuffer;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06004F96 RID: 20374 RVA: 0x00123372 File Offset: 0x00121572
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06004F97 RID: 20375 RVA: 0x00123375 File Offset: 0x00121575
		// (set) Token: 0x06004F98 RID: 20376 RVA: 0x0012337D File Offset: 0x0012157D
		public TraceEventType ExceptionEventType
		{
			get
			{
				return this.exceptionEventType;
			}
			set
			{
				this.exceptionEventType = value;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x00123388 File Offset: 0x00121588
		public IPEndPoint RemoteIPEndPoint
		{
			get
			{
				if (this.remoteEndpoint == null && this.closeState == SocketConnection.CloseState.Open)
				{
					try
					{
						this.remoteEndpoint = (IPEndPoint)this.socket.RemoteEndPoint;
					}
					catch (SocketException socketException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertReceiveException(socketException, TimeSpan.Zero, TimeSpan.Zero), this.ExceptionEventType);
					}
					catch (ObjectDisposedException ex)
					{
						Exception ex2 = this.ConvertObjectDisposedException(ex, SocketConnection.TransferOperation.Undefined);
						if (ex2 == ex)
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex2, this.ExceptionEventType);
					}
				}
				return this.remoteEndpoint;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00123424 File Offset: 0x00121624
		private IOThreadTimer SendTimer
		{
			get
			{
				if (this.sendTimer == null)
				{
					if (SocketConnection.onSendTimeout == null)
					{
						SocketConnection.onSendTimeout = new Action<object>(SocketConnection.OnSendTimeout);
					}
					this.sendTimer = new IOThreadTimer(SocketConnection.onSendTimeout, this, false);
				}
				return this.sendTimer;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x0012345E File Offset: 0x0012165E
		private IOThreadTimer ReceiveTimer
		{
			get
			{
				if (this.receiveTimer == null)
				{
					if (SocketConnection.onReceiveTimeout == null)
					{
						SocketConnection.onReceiveTimeout = new Action<object>(SocketConnection.OnReceiveTimeout);
					}
					this.receiveTimer = new IOThreadTimer(SocketConnection.onReceiveTimeout, this, false);
				}
				return this.receiveTimer;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x00123498 File Offset: 0x00121698
		private string RemoteEndpointAddress
		{
			get
			{
				if (this.remoteEndpointAddress == null)
				{
					try
					{
						IPEndPoint ipendPoint;
						IPEndPoint iPEndPoint;
						if (this.TryGetEndpoints(out ipendPoint, out iPEndPoint))
						{
							this.remoteEndpointAddress = TraceUtility.GetRemoteEndpointAddressPort(iPEndPoint);
						}
						else
						{
							this.remoteEndpointAddress = string.Empty;
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
				return this.remoteEndpointAddress;
			}
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x001234F8 File Offset: 0x001216F8
		private static void OnReceiveTimeout(object state)
		{
			SocketConnection socketConnection = (SocketConnection)state;
			socketConnection.Abort(SR.GetString("SocketAbortedReceiveTimedOut", new object[]
			{
				socketConnection.asyncReceiveTimeout
			}), SocketConnection.TransferOperation.Read);
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00123534 File Offset: 0x00121734
		private static void OnSendTimeout(object state)
		{
			SocketConnection socketConnection = (SocketConnection)state;
			socketConnection.Abort(TraceEventType.Warning, SR.GetString("SocketAbortedSendTimedOut", new object[]
			{
				socketConnection.asyncSendTimeout
			}), SocketConnection.TransferOperation.Write);
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0012356E File Offset: 0x0012176E
		private static void OnReceiveCompleted(IAsyncResult result)
		{
			((SocketConnection)result.AsyncState).OnReceive(result);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00123581 File Offset: 0x00121781
		private static void OnReceiveAsyncCompleted(object sender, SocketAsyncEventArgs e)
		{
			((SocketConnection)e.UserToken).OnReceiveAsync(sender, e);
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00123595 File Offset: 0x00121795
		private static void OnSendAsyncCompleted(object sender, SocketAsyncEventArgs e)
		{
			((SocketConnection)e.UserToken).OnSendAsync(sender, e);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x001235A9 File Offset: 0x001217A9
		public void Abort()
		{
			this.Abort(null, SocketConnection.TransferOperation.Undefined);
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x001235B4 File Offset: 0x001217B4
		private void Abort(string timeoutErrorString, SocketConnection.TransferOperation transferOperation)
		{
			TraceEventType traceEventType = TraceEventType.Warning;
			if (this.ExceptionEventType == TraceEventType.Information)
			{
				traceEventType = this.ExceptionEventType;
			}
			this.Abort(traceEventType, timeoutErrorString, transferOperation);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x001235DC File Offset: 0x001217DC
		private void Abort(TraceEventType traceEventType)
		{
			this.Abort(traceEventType, null, SocketConnection.TransferOperation.Undefined);
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x001235E8 File Offset: 0x001217E8
		private void Abort(TraceEventType traceEventType, string timeoutErrorString, SocketConnection.TransferOperation transferOperation)
		{
			if (TD.SocketConnectionAbortIsEnabled())
			{
				TD.SocketConnectionAbort(this.socket.GetHashCode());
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.closeState == SocketConnection.CloseState.Closed)
				{
					return;
				}
				this.timeoutErrorString = timeoutErrorString;
				this.timeoutErrorTransferOperation = transferOperation;
				this.aborted = true;
				this.closeState = SocketConnection.CloseState.Closed;
				if (this.asyncReadPending)
				{
					this.CancelReceiveTimer();
				}
				else
				{
					this.DisposeReadEventArgs();
				}
				if (this.asyncWritePending)
				{
					this.CancelSendTimer();
				}
				else
				{
					this.DisposeWriteEventArgs();
				}
			}
			if (DiagnosticUtility.ShouldTrace(traceEventType))
			{
				TraceUtility.TraceEvent(traceEventType, 262171, SR.GetString("TraceCodeSocketConnectionAbort"), this);
			}
			this.socket.Close(0);
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x001236B8 File Offset: 0x001218B8
		private void AbortRead()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.asyncReadPending)
				{
					if (this.closeState != SocketConnection.CloseState.Closed)
					{
						this.SetUserToken(this.asyncReadEventArgs, null);
						this.asyncReadPending = false;
						this.CancelReceiveTimer();
					}
					else
					{
						this.DisposeReadEventArgs();
					}
				}
			}
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x00123728 File Offset: 0x00121928
		private void CancelReceiveTimer()
		{
			IOThreadTimer iothreadTimer = this.receiveTimer;
			this.receiveTimer = null;
			if (iothreadTimer != null)
			{
				iothreadTimer.Cancel();
			}
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x00123750 File Offset: 0x00121950
		private void CancelSendTimer()
		{
			IOThreadTimer iothreadTimer = this.sendTimer;
			this.sendTimer = null;
			if (iothreadTimer != null)
			{
				iothreadTimer.Cancel();
			}
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x00123778 File Offset: 0x00121978
		private void CloseAsyncAndLinger()
		{
			this.readFinTimeout = this.closeTimeoutHelper.RemainingTime();
			try
			{
				if (this.BeginReadCore(0, 1, this.readFinTimeout, SocketConnection.onWaitForFinComplete, this) == AsyncCompletionResult.Queued)
				{
					return;
				}
				int num = this.EndRead();
				if (num > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new CommunicationException(SR.GetString("SocketCloseReadReceivedData", new object[]
					{
						this.socket.RemoteEndPoint
					})), this.ExceptionEventType);
				}
			}
			catch (TimeoutException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("SocketCloseReadTimeout", new object[]
				{
					this.socket.RemoteEndPoint,
					this.readFinTimeout
				}), innerException), this.ExceptionEventType);
			}
			this.ContinueClose(this.closeTimeoutHelper.RemainingTime());
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x00123858 File Offset: 0x00121A58
		private static void OnWaitForFinComplete(object state)
		{
			SocketConnection socketConnection = (SocketConnection)state;
			try
			{
				try
				{
					int num = socketConnection.EndRead();
					if (num > 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new CommunicationException(SR.GetString("SocketCloseReadReceivedData", new object[]
						{
							socketConnection.socket.RemoteEndPoint
						})), socketConnection.ExceptionEventType);
					}
				}
				catch (TimeoutException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("SocketCloseReadTimeout", new object[]
					{
						socketConnection.socket.RemoteEndPoint,
						socketConnection.readFinTimeout
					}), innerException), socketConnection.ExceptionEventType);
				}
				socketConnection.ContinueClose(socketConnection.closeTimeoutHelper.RemainingTime());
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				socketConnection.Abort();
			}
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x0012393C File Offset: 0x00121B3C
		public void Close(TimeSpan timeout, bool asyncAndLinger)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.closeState == SocketConnection.CloseState.Closing || this.closeState == SocketConnection.CloseState.Closed)
				{
					return;
				}
				this.TraceSocketInfo(this.socket, 262170, "TraceCodeSocketConnectionClose", timeout.ToString());
				this.closeState = SocketConnection.CloseState.Closing;
			}
			this.closeTimeoutHelper = new TimeoutHelper(timeout);
			this.Shutdown(this.closeTimeoutHelper.RemainingTime());
			if (asyncAndLinger)
			{
				this.CloseAsyncAndLinger();
				return;
			}
			this.CloseSync();
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x001239E4 File Offset: 0x00121BE4
		private void CloseSync()
		{
			byte[] buffer = new byte[1];
			this.readFinTimeout = this.closeTimeoutHelper.RemainingTime();
			try
			{
				int num = this.ReadCore(buffer, 0, 1, this.readFinTimeout, true);
				if (num > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new CommunicationException(SR.GetString("SocketCloseReadReceivedData", new object[]
					{
						this.socket.RemoteEndPoint
					})), this.ExceptionEventType);
				}
			}
			catch (TimeoutException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("SocketCloseReadTimeout", new object[]
				{
					this.socket.RemoteEndPoint,
					this.readFinTimeout
				}), innerException), this.ExceptionEventType);
			}
			this.ContinueClose(this.closeTimeoutHelper.RemainingTime());
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00123ABC File Offset: 0x00121CBC
		public void ContinueClose(TimeSpan timeout)
		{
			if (timeout <= TimeSpan.Zero && DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262172, SR.GetString("TraceCodeSocketConnectionAbortClose"), this);
			}
			this.socket.Close(TimeoutHelper.ToMilliseconds(timeout));
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.closeState != SocketConnection.CloseState.Closed)
				{
					if (!this.asyncReadPending)
					{
						this.DisposeReadEventArgs();
					}
					if (!this.asyncWritePending)
					{
						this.DisposeWriteEventArgs();
					}
				}
				this.closeState = SocketConnection.CloseState.Closed;
			}
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x00123B60 File Offset: 0x00121D60
		public void Shutdown(TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isShutdown)
				{
					return;
				}
				this.isShutdown = true;
			}
			try
			{
				this.socket.Shutdown(SocketShutdown.Send);
			}
			catch (SocketException socketException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertSendException(socketException, TimeSpan.MaxValue, this.socketSyncSendTimeout), this.ExceptionEventType);
			}
			catch (ObjectDisposedException ex)
			{
				Exception ex2 = this.ConvertObjectDisposedException(ex, SocketConnection.TransferOperation.Undefined);
				if (ex2 == ex)
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex2, this.ExceptionEventType);
			}
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x00123C1C File Offset: 0x00121E1C
		private void ThrowIfNotOpen()
		{
			if (this.closeState == SocketConnection.CloseState.Closing || this.closeState == SocketConnection.CloseState.Closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertObjectDisposedException(new ObjectDisposedException(base.GetType().ToString(), SR.GetString("SocketConnectionDisposed")), SocketConnection.TransferOperation.Undefined), this.ExceptionEventType);
			}
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x00123C6D File Offset: 0x00121E6D
		private void ThrowIfClosed()
		{
			if (this.closeState == SocketConnection.CloseState.Closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertObjectDisposedException(new ObjectDisposedException(base.GetType().ToString(), SR.GetString("SocketConnectionDisposed")), SocketConnection.TransferOperation.Undefined), this.ExceptionEventType);
			}
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x00123CAC File Offset: 0x00121EAC
		private void TraceSocketInfo(Socket socket, int traceCode, string srString, string timeoutString)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
				dictionary["State"] = this.closeState.ToString();
				if (timeoutString != null)
				{
					dictionary["Timeout"] = timeoutString;
				}
				if (socket != null && this.closeState != SocketConnection.CloseState.Closing)
				{
					if (socket.LocalEndPoint != null)
					{
						dictionary["LocalEndpoint"] = socket.LocalEndPoint.ToString();
					}
					if (socket.RemoteEndPoint != null)
					{
						dictionary["RemoteEndPoint"] = socket.RemoteEndPoint.ToString();
					}
				}
				TraceUtility.TraceEvent(TraceEventType.Information, traceCode, SR.GetString(srString), new DictionaryTraceRecord(dictionary), this, null);
			}
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00123D54 File Offset: 0x00121F54
		private bool TryGetEndpoints(out IPEndPoint localIPEndpoint, out IPEndPoint remoteIPEndpoint)
		{
			localIPEndpoint = null;
			remoteIPEndpoint = null;
			if (this.closeState == SocketConnection.CloseState.Open)
			{
				try
				{
					remoteIPEndpoint = (this.remoteEndpoint ?? ((IPEndPoint)this.socket.RemoteEndPoint));
					localIPEndpoint = (IPEndPoint)this.socket.LocalEndPoint;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				}
			}
			return localIPEndpoint != null && remoteIPEndpoint != null;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x00123DD0 File Offset: 0x00121FD0
		public object DuplicateAndClose(int targetProcessId)
		{
			object result = this.socket.DuplicateAndClose(targetProcessId);
			this.Abort(TraceEventType.Information);
			return result;
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00123DF7 File Offset: 0x00121FF7
		public object GetCoreTransport()
		{
			return this.socket;
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00123DFF File Offset: 0x00121FFF
		public IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult<bool>(true, callback, state);
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00123E09 File Offset: 0x00122009
		public bool EndValidate(IAsyncResult result)
		{
			return CompletedAsyncResult<bool>.End(result);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x00123E14 File Offset: 0x00122014
		private Exception ConvertSendException(SocketException socketException, TimeSpan remainingTime, TimeSpan timeout)
		{
			return SocketConnection.ConvertTransferException(socketException, timeout, socketException, SocketConnection.TransferOperation.Write, this.aborted, this.timeoutErrorString, this.timeoutErrorTransferOperation, this, remainingTime);
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x00123E40 File Offset: 0x00122040
		private Exception ConvertReceiveException(SocketException socketException, TimeSpan remainingTime, TimeSpan timeout)
		{
			return SocketConnection.ConvertTransferException(socketException, timeout, socketException, SocketConnection.TransferOperation.Read, this.aborted, this.timeoutErrorString, this.timeoutErrorTransferOperation, this, remainingTime);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x00123E6C File Offset: 0x0012206C
		internal static Exception ConvertTransferException(SocketException socketException, TimeSpan timeout, Exception originalException)
		{
			return SocketConnection.ConvertTransferException(socketException, timeout, originalException, SocketConnection.TransferOperation.Undefined, false, null, SocketConnection.TransferOperation.Undefined, null, TimeSpan.MaxValue);
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x00123E8B File Offset: 0x0012208B
		private Exception ConvertObjectDisposedException(ObjectDisposedException originalException, SocketConnection.TransferOperation transferOperation)
		{
			if (this.timeoutErrorString != null)
			{
				return SocketConnection.ConvertTimeoutErrorException(originalException, transferOperation, this.timeoutErrorString, this.timeoutErrorTransferOperation);
			}
			if (this.aborted)
			{
				return new CommunicationObjectAbortedException(SR.GetString("SocketConnectionDisposed"), originalException);
			}
			return originalException;
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x00123EC4 File Offset: 0x001220C4
		private static Exception ConvertTransferException(SocketException socketException, TimeSpan timeout, Exception originalException, SocketConnection.TransferOperation transferOperation, bool aborted, string timeoutErrorString, SocketConnection.TransferOperation timeoutErrorTransferOperation, SocketConnection socketConnection, TimeSpan remainingTime)
		{
			if (socketException.ErrorCode == 6)
			{
				return new CommunicationObjectAbortedException(socketException.Message, socketException);
			}
			if (timeoutErrorString != null)
			{
				return SocketConnection.ConvertTimeoutErrorException(originalException, transferOperation, timeoutErrorString, timeoutErrorTransferOperation);
			}
			TraceEventType traceEventType = (socketConnection == null) ? TraceEventType.Error : socketConnection.ExceptionEventType;
			if (socketException.ErrorCode == 10053 && remainingTime <= TimeSpan.Zero)
			{
				TimeoutException ex = new TimeoutException(SR.GetString("TcpConnectionTimedOut", new object[]
				{
					timeout
				}), originalException);
				if (TD.TcpConnectionTimedOutIsEnabled() && socketConnection != null)
				{
					int socketId = (socketConnection != null && socketConnection.socket != null) ? socketConnection.socket.GetHashCode() : -1;
					TD.TcpConnectionTimedOut(socketId, socketConnection.RemoteEndpointAddress);
				}
				if (DiagnosticUtility.ShouldTrace(traceEventType))
				{
					TraceUtility.TraceEvent(traceEventType, 262257, SocketConnection.GetEndpointString("TcpConnectionTimedOut", timeout, null, socketConnection), ex, null);
				}
				return ex;
			}
			if (socketException.ErrorCode == 10052 || socketException.ErrorCode == 10053 || socketException.ErrorCode == 10054)
			{
				if (aborted)
				{
					return new CommunicationObjectAbortedException(SR.GetString("TcpLocalConnectionAborted"), originalException);
				}
				CommunicationException ex2 = new CommunicationException(SR.GetString("TcpConnectionResetError", new object[]
				{
					timeout
				}), originalException);
				if (TD.TcpConnectionResetErrorIsEnabled() && socketConnection != null)
				{
					int socketId2 = (socketConnection.socket != null) ? socketConnection.socket.GetHashCode() : -1;
					TD.TcpConnectionResetError(socketId2, socketConnection.RemoteEndpointAddress);
				}
				if (DiagnosticUtility.ShouldTrace(traceEventType))
				{
					TraceUtility.TraceEvent(traceEventType, 262256, SocketConnection.GetEndpointString("TcpConnectionResetError", timeout, null, socketConnection), ex2, null);
				}
				return ex2;
			}
			else
			{
				if (socketException.ErrorCode == 10060)
				{
					TimeoutException ex3 = new TimeoutException(SR.GetString("TcpConnectionTimedOut", new object[]
					{
						timeout
					}), originalException);
					if (DiagnosticUtility.ShouldTrace(traceEventType))
					{
						TraceUtility.TraceEvent(traceEventType, 262257, SocketConnection.GetEndpointString("TcpConnectionTimedOut", timeout, null, socketConnection), ex3, null);
					}
					return ex3;
				}
				if (aborted)
				{
					return new CommunicationObjectAbortedException(SR.GetString("TcpTransferError", new object[]
					{
						socketException.ErrorCode,
						socketException.Message
					}), originalException);
				}
				CommunicationException ex4 = new CommunicationException(SR.GetString("TcpTransferError", new object[]
				{
					socketException.ErrorCode,
					socketException.Message
				}), originalException);
				if (DiagnosticUtility.ShouldTrace(traceEventType))
				{
					TraceUtility.TraceEvent(traceEventType, 262255, SocketConnection.GetEndpointString("TcpTransferError", TimeSpan.MinValue, socketException, socketConnection), ex4, null);
				}
				return ex4;
			}
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x00124135 File Offset: 0x00122335
		private static Exception ConvertTimeoutErrorException(Exception originalException, SocketConnection.TransferOperation transferOperation, string timeoutErrorString, SocketConnection.TransferOperation timeoutErrorTransferOperation)
		{
			if (transferOperation == timeoutErrorTransferOperation)
			{
				return new TimeoutException(timeoutErrorString, originalException);
			}
			return new CommunicationException(timeoutErrorString, originalException);
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0012414C File Offset: 0x0012234C
		private static string GetEndpointString(string sr, TimeSpan timeout, SocketException socketException, SocketConnection socketConnection)
		{
			IPEndPoint ipendPoint = null;
			IPEndPoint ipendPoint2 = null;
			bool flag = socketConnection != null && socketConnection.TryGetEndpoints(out ipendPoint2, out ipendPoint);
			if (string.Compare(sr, "TcpConnectionTimedOut", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!flag)
				{
					return SR.GetString("TcpConnectionTimedOut", new object[]
					{
						timeout
					});
				}
				return SR.GetString("TcpConnectionTimedOutWithIP", new object[]
				{
					timeout,
					ipendPoint2,
					ipendPoint
				});
			}
			else if (string.Compare(sr, "TcpConnectionResetError", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (!flag)
				{
					return SR.GetString("TcpConnectionResetError", new object[]
					{
						timeout
					});
				}
				return SR.GetString("TcpConnectionResetErrorWithIP", new object[]
				{
					timeout,
					ipendPoint2,
					ipendPoint
				});
			}
			else
			{
				if (!flag)
				{
					return SR.GetString("TcpTransferError", new object[]
					{
						socketException.ErrorCode,
						socketException.Message
					});
				}
				return SR.GetString("TcpTransferErrorWithIP", new object[]
				{
					socketException.ErrorCode,
					socketException.Message,
					ipendPoint2,
					ipendPoint
				});
			}
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x00124264 File Offset: 0x00122464
		public AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
			bool flag = true;
			AsyncCompletionResult result;
			try
			{
				if (TD.SocketAsyncWriteStartIsEnabled())
				{
					this.TraceWriteStart(size, true);
				}
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.ThrowIfClosed();
					this.EnsureWriteEventArgs();
					this.SetImmediate(immediate);
					this.SetWriteTimeout(timeout, false);
					this.SetUserToken(this.asyncWriteEventArgs, this);
					this.asyncWritePending = true;
					this.asyncWriteCallback = callback;
					this.asyncWriteState = state;
				}
				this.asyncWriteEventArgs.SetBuffer(buffer, offset, size);
				if (this.socket.SendAsync(this.asyncWriteEventArgs))
				{
					flag = false;
					result = AsyncCompletionResult.Queued;
				}
				else
				{
					this.HandleSendAsyncCompleted();
					flag = false;
					result = AsyncCompletionResult.Completed;
				}
			}
			catch (SocketException socketException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertSendException(socketException, TimeSpan.MaxValue, this.asyncSendTimeout), this.ExceptionEventType);
			}
			catch (ObjectDisposedException ex)
			{
				Exception ex2 = this.ConvertObjectDisposedException(ex, SocketConnection.TransferOperation.Write);
				if (ex2 == ex)
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex2, this.ExceptionEventType);
			}
			finally
			{
				if (flag)
				{
					this.AbortWrite();
				}
			}
			return result;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x001243A8 File Offset: 0x001225A8
		public void EndWrite()
		{
			if (this.asyncWriteException != null)
			{
				this.AbortWrite();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.asyncWriteException, this.ExceptionEventType);
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.asyncWritePending)
				{
					throw Fx.AssertAndThrow("SocketConnection.EndWrite called with no write pending.");
				}
				this.SetUserToken(this.asyncWriteEventArgs, null);
				this.asyncWritePending = false;
				if (this.closeState == SocketConnection.CloseState.Closed)
				{
					this.DisposeWriteEventArgs();
				}
			}
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x00124440 File Offset: 0x00122640
		private void OnSendAsync(object sender, SocketAsyncEventArgs eventArgs)
		{
			this.CancelSendTimer();
			try
			{
				this.HandleSendAsyncCompleted();
			}
			catch (SocketException socketException)
			{
				this.asyncWriteException = this.ConvertSendException(socketException, TimeSpan.MaxValue, this.asyncSendTimeout);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.asyncWriteException = exception;
			}
			this.FinishWrite();
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x001244AC File Offset: 0x001226AC
		private void HandleSendAsyncCompleted()
		{
			if (this.asyncWriteEventArgs.SocketError == SocketError.Success)
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SocketException((int)this.asyncWriteEventArgs.SocketError));
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x001244D6 File Offset: 0x001226D6
		private void DisposeWriteEventArgs()
		{
			if (this.asyncWriteEventArgs != null)
			{
				this.asyncWriteEventArgs.Completed -= SocketConnection.onSocketSendCompleted;
				this.asyncWriteEventArgs.Dispose();
			}
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x001244FC File Offset: 0x001226FC
		private void AbortWrite()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.asyncWritePending)
				{
					if (this.closeState != SocketConnection.CloseState.Closed)
					{
						this.SetUserToken(this.asyncWriteEventArgs, null);
						this.asyncWritePending = false;
						this.CancelSendTimer();
					}
					else
					{
						this.DisposeWriteEventArgs();
					}
				}
			}
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0012456C File Offset: 0x0012276C
		private void FinishWrite()
		{
			WaitCallback waitCallback = this.asyncWriteCallback;
			object state = this.asyncWriteState;
			this.asyncWriteState = null;
			this.asyncWriteCallback = null;
			waitCallback(state);
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0012459C File Offset: 0x0012279C
		public void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			try
			{
				if (TD.SocketWriteStartIsEnabled())
				{
					this.TraceWriteStart(size, false);
				}
				this.SetImmediate(immediate);
				int i = size;
				while (i > 0)
				{
					this.SetWriteTimeout(timeoutHelper.RemainingTime(), true);
					size = Math.Min(i, 65536);
					this.socket.Send(buffer, offset, size, SocketFlags.None);
					i -= size;
					offset += size;
					timeout = timeoutHelper.RemainingTime();
				}
			}
			catch (SocketException socketException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertSendException(socketException, timeoutHelper.RemainingTime(), this.socketSyncSendTimeout), this.ExceptionEventType);
			}
			catch (ObjectDisposedException ex)
			{
				Exception ex2 = this.ConvertObjectDisposedException(ex, SocketConnection.TransferOperation.Write);
				if (ex2 == ex)
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex2, this.ExceptionEventType);
			}
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x00124680 File Offset: 0x00122880
		private void TraceWriteStart(int size, bool async)
		{
			if (!async)
			{
				TD.SocketWriteStart(this.socket.GetHashCode(), size, this.RemoteEndpointAddress);
				return;
			}
			TD.SocketAsyncWriteStart(this.socket.GetHashCode(), size, this.RemoteEndpointAddress);
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x001246B4 File Offset: 0x001228B4
		public void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			try
			{
				this.Write(buffer, offset, size, immediate, timeout);
			}
			finally
			{
				bufferManager.ReturnBuffer(buffer);
			}
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x001246EC File Offset: 0x001228EC
		public int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
			this.ThrowIfNotOpen();
			return this.ReadCore(buffer, offset, size, timeout, false);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x00124708 File Offset: 0x00122908
		private int ReadCore(byte[] buffer, int offset, int size, TimeSpan timeout, bool closing)
		{
			int num = 0;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			try
			{
				this.SetReadTimeout(timeoutHelper.RemainingTime(), true, closing);
				num = this.socket.Receive(buffer, offset, size, SocketFlags.None);
				if (TD.SocketReadStopIsEnabled())
				{
					this.TraceSocketReadStop(num, false);
				}
			}
			catch (SocketException socketException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertReceiveException(socketException, timeoutHelper.RemainingTime(), this.socketSyncReceiveTimeout), this.ExceptionEventType);
			}
			catch (ObjectDisposedException ex)
			{
				Exception ex2 = this.ConvertObjectDisposedException(ex, SocketConnection.TransferOperation.Read);
				if (ex2 == ex)
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex2, this.ExceptionEventType);
			}
			return num;
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x001247BC File Offset: 0x001229BC
		private void TraceSocketReadStop(int bytesRead, bool async)
		{
			if (!async)
			{
				TD.SocketReadStop((this.socket != null) ? this.socket.GetHashCode() : -1, bytesRead, this.RemoteEndpointAddress);
				return;
			}
			TD.SocketAsyncReadStop((this.socket != null) ? this.socket.GetHashCode() : -1, bytesRead, this.RemoteEndpointAddress);
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x00124811 File Offset: 0x00122A11
		public virtual AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			ConnectionUtilities.ValidateBufferBounds(this.AsyncReadBufferSize, offset, size);
			this.ThrowIfNotOpen();
			return this.BeginReadCore(offset, size, timeout, callback, state);
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x00124834 File Offset: 0x00122A34
		private AsyncCompletionResult BeginReadCore(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			bool flag = true;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.ThrowIfClosed();
				this.EnsureReadEventArgs();
				this.asyncReadState = state;
				this.asyncReadCallback = callback;
				this.SetUserToken(this.asyncReadEventArgs, this);
				this.asyncReadPending = true;
				this.SetReadTimeout(timeout, false, false);
			}
			AsyncCompletionResult result;
			try
			{
				if (this.socket.UseOnlyOverlappedIO)
				{
					IAsyncResult asyncResult = this.socket.BeginReceive(this.AsyncReadBuffer, offset, size, SocketFlags.None, SocketConnection.onReceiveCompleted, this);
					if (!asyncResult.CompletedSynchronously)
					{
						flag = false;
						return AsyncCompletionResult.Queued;
					}
					this.asyncReadSize = this.socket.EndReceive(asyncResult);
				}
				else
				{
					if (offset != this.asyncReadEventArgs.Offset || size != this.asyncReadEventArgs.Count)
					{
						this.asyncReadEventArgs.SetBuffer(offset, size);
					}
					if (this.ReceiveAsync())
					{
						flag = false;
						return AsyncCompletionResult.Queued;
					}
					this.HandleReceiveAsyncCompleted();
					this.asyncReadSize = this.asyncReadEventArgs.BytesTransferred;
				}
				if (TD.SocketReadStopIsEnabled())
				{
					this.TraceSocketReadStop(this.asyncReadSize, true);
				}
				flag = false;
				result = AsyncCompletionResult.Completed;
			}
			catch (SocketException socketException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.ConvertReceiveException(socketException, TimeSpan.MaxValue, this.asyncReceiveTimeout), this.ExceptionEventType);
			}
			catch (ObjectDisposedException ex)
			{
				Exception ex2 = this.ConvertObjectDisposedException(ex, SocketConnection.TransferOperation.Read);
				if (ex2 == ex)
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(ex2, this.ExceptionEventType);
			}
			finally
			{
				if (flag)
				{
					this.AbortRead();
				}
			}
			return result;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x00124A14 File Offset: 0x00122C14
		[SecuritySafeCritical]
		private bool ReceiveAsync()
		{
			if (!PartialTrustHelpers.ShouldFlowSecurityContext && !ExecutionContext.IsFlowSuppressed())
			{
				return this.ReceiveAsyncNoFlow();
			}
			return this.socket.ReceiveAsync(this.asyncReadEventArgs);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x00124A3C File Offset: 0x00122C3C
		[SecurityCritical]
		private bool ReceiveAsyncNoFlow()
		{
			bool result;
			using (ExecutionContext.SuppressFlow())
			{
				result = this.socket.ReceiveAsync(this.asyncReadEventArgs);
			}
			return result;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x00124A84 File Offset: 0x00122C84
		private void OnReceive(IAsyncResult result)
		{
			this.CancelReceiveTimer();
			if (result.CompletedSynchronously)
			{
				return;
			}
			try
			{
				this.asyncReadSize = this.socket.EndReceive(result);
				if (TD.SocketReadStopIsEnabled())
				{
					this.TraceSocketReadStop(this.asyncReadSize, true);
				}
			}
			catch (SocketException socketException)
			{
				this.asyncReadException = this.ConvertReceiveException(socketException, TimeSpan.MaxValue, this.asyncReceiveTimeout);
			}
			catch (ObjectDisposedException originalException)
			{
				this.asyncReadException = this.ConvertObjectDisposedException(originalException, SocketConnection.TransferOperation.Read);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.asyncReadException = exception;
			}
			this.FinishRead();
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x00124B38 File Offset: 0x00122D38
		private void OnReceiveAsync(object sender, SocketAsyncEventArgs eventArgs)
		{
			this.CancelReceiveTimer();
			try
			{
				this.HandleReceiveAsyncCompleted();
				this.asyncReadSize = eventArgs.BytesTransferred;
				if (TD.SocketReadStopIsEnabled())
				{
					this.TraceSocketReadStop(this.asyncReadSize, true);
				}
			}
			catch (SocketException socketException)
			{
				this.asyncReadException = this.ConvertReceiveException(socketException, TimeSpan.MaxValue, this.asyncReceiveTimeout);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.asyncReadException = exception;
			}
			this.FinishRead();
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x00124BC4 File Offset: 0x00122DC4
		private void HandleReceiveAsyncCompleted()
		{
			if (this.asyncReadEventArgs.SocketError == SocketError.Success)
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SocketException((int)this.asyncReadEventArgs.SocketError));
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x00124BF0 File Offset: 0x00122DF0
		private void FinishRead()
		{
			WaitCallback waitCallback = this.asyncReadCallback;
			object state = this.asyncReadState;
			this.asyncReadState = null;
			this.asyncReadCallback = null;
			waitCallback(state);
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x00124C20 File Offset: 0x00122E20
		public int EndRead()
		{
			if (this.asyncReadException != null)
			{
				this.AbortRead();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.asyncReadException, this.ExceptionEventType);
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.asyncReadPending)
				{
					throw Fx.AssertAndThrow("SocketConnection.EndRead called with no read pending.");
				}
				this.SetUserToken(this.asyncReadEventArgs, null);
				this.asyncReadPending = false;
				if (this.closeState == SocketConnection.CloseState.Closed)
				{
					this.DisposeReadEventArgs();
				}
			}
			return this.asyncReadSize;
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x00124CBC File Offset: 0x00122EBC
		private void DisposeReadEventArgs()
		{
			if (this.asyncReadEventArgs != null)
			{
				this.asyncReadEventArgs.Completed -= SocketConnection.onReceiveAsyncCompleted;
				this.asyncReadEventArgs.Dispose();
			}
			this.TryReturnReadBuffer();
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x00124CE7 File Offset: 0x00122EE7
		private void TryReturnReadBuffer()
		{
			if (this.readBuffer != null && !this.aborted)
			{
				this.connectionBufferPool.Return(this.readBuffer);
				this.readBuffer = null;
			}
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x00124D12 File Offset: 0x00122F12
		private void SetUserToken(SocketAsyncEventArgs args, object userToken)
		{
			if (args != null)
			{
				args.UserToken = userToken;
			}
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x00124D20 File Offset: 0x00122F20
		private void SetImmediate(bool immediate)
		{
			if (immediate != this.noDelay)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.ThrowIfNotOpen();
					this.socket.NoDelay = immediate;
				}
				this.noDelay = immediate;
			}
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00124D7C File Offset: 0x00122F7C
		private void SetReadTimeout(TimeSpan timeout, bool synchronous, bool closing)
		{
			if (synchronous)
			{
				this.CancelReceiveTimer();
				if (timeout <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("TcpConnectionTimedOut", new object[]
					{
						timeout
					})), this.ExceptionEventType);
				}
				if (this.ShouldUpdateTimeout(this.socketSyncReceiveTimeout, timeout))
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (!closing || this.closeState != SocketConnection.CloseState.Closing)
						{
							this.ThrowIfNotOpen();
						}
						this.socket.ReceiveTimeout = TimeoutHelper.ToMilliseconds(timeout);
					}
					this.socketSyncReceiveTimeout = timeout;
					return;
				}
			}
			else
			{
				this.asyncReceiveTimeout = timeout;
				if (timeout == TimeSpan.MaxValue)
				{
					this.CancelReceiveTimer();
					return;
				}
				this.ReceiveTimer.Set(timeout);
			}
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x00124E60 File Offset: 0x00123060
		private void SetWriteTimeout(TimeSpan timeout, bool synchronous)
		{
			if (synchronous)
			{
				this.CancelSendTimer();
				if (timeout <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new TimeoutException(SR.GetString("TcpConnectionTimedOut", new object[]
					{
						timeout
					})), this.ExceptionEventType);
				}
				if (this.ShouldUpdateTimeout(this.socketSyncSendTimeout, timeout))
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						this.ThrowIfNotOpen();
						this.socket.SendTimeout = TimeoutHelper.ToMilliseconds(timeout);
					}
					this.socketSyncSendTimeout = timeout;
					return;
				}
			}
			else
			{
				this.asyncSendTimeout = timeout;
				if (timeout == TimeSpan.MaxValue)
				{
					this.CancelSendTimer();
					return;
				}
				this.SendTimer.Set(timeout);
			}
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00124F38 File Offset: 0x00123138
		private bool ShouldUpdateTimeout(TimeSpan oldTimeout, TimeSpan newTimeout)
		{
			if (oldTimeout == newTimeout)
			{
				return false;
			}
			long num = oldTimeout.Ticks / 10L;
			long num2 = Math.Max(oldTimeout.Ticks, newTimeout.Ticks) - Math.Min(oldTimeout.Ticks, newTimeout.Ticks);
			return num2 > num;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x00124F88 File Offset: 0x00123188
		private void EnsureReadEventArgs()
		{
			if (this.asyncReadEventArgs == null)
			{
				if (SocketConnection.onReceiveAsyncCompleted == null)
				{
					SocketConnection.onReceiveAsyncCompleted = new EventHandler<SocketAsyncEventArgs>(SocketConnection.OnReceiveAsyncCompleted);
				}
				this.asyncReadEventArgs = new SocketAsyncEventArgs();
				this.asyncReadEventArgs.SetBuffer(this.readBuffer, 0, this.readBuffer.Length);
				this.asyncReadEventArgs.Completed += SocketConnection.onReceiveAsyncCompleted;
			}
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x00124FEA File Offset: 0x001231EA
		private void EnsureWriteEventArgs()
		{
			if (this.asyncWriteEventArgs == null)
			{
				if (SocketConnection.onSocketSendCompleted == null)
				{
					SocketConnection.onSocketSendCompleted = new EventHandler<SocketAsyncEventArgs>(SocketConnection.OnSendAsyncCompleted);
				}
				this.asyncWriteEventArgs = new SocketAsyncEventArgs();
				this.asyncWriteEventArgs.Completed += SocketConnection.onSocketSendCompleted;
			}
		}

		// Token: 0x04003166 RID: 12646
		private static AsyncCallback onReceiveCompleted;

		// Token: 0x04003167 RID: 12647
		private static EventHandler<SocketAsyncEventArgs> onReceiveAsyncCompleted;

		// Token: 0x04003168 RID: 12648
		private static EventHandler<SocketAsyncEventArgs> onSocketSendCompleted;

		// Token: 0x04003169 RID: 12649
		private Socket socket;

		// Token: 0x0400316A RID: 12650
		private TimeSpan asyncSendTimeout;

		// Token: 0x0400316B RID: 12651
		private TimeSpan readFinTimeout;

		// Token: 0x0400316C RID: 12652
		private TimeSpan asyncReceiveTimeout;

		// Token: 0x0400316D RID: 12653
		private TimeSpan socketSyncSendTimeout;

		// Token: 0x0400316E RID: 12654
		private TimeSpan socketSyncReceiveTimeout;

		// Token: 0x0400316F RID: 12655
		private SocketConnection.CloseState closeState;

		// Token: 0x04003170 RID: 12656
		private bool isShutdown;

		// Token: 0x04003171 RID: 12657
		private bool noDelay;

		// Token: 0x04003172 RID: 12658
		private bool aborted;

		// Token: 0x04003173 RID: 12659
		private TraceEventType exceptionEventType;

		// Token: 0x04003174 RID: 12660
		private TimeoutHelper closeTimeoutHelper;

		// Token: 0x04003175 RID: 12661
		private static WaitCallback onWaitForFinComplete = new WaitCallback(SocketConnection.OnWaitForFinComplete);

		// Token: 0x04003176 RID: 12662
		private int asyncReadSize;

		// Token: 0x04003177 RID: 12663
		private SocketAsyncEventArgs asyncReadEventArgs;

		// Token: 0x04003178 RID: 12664
		private byte[] readBuffer;

		// Token: 0x04003179 RID: 12665
		private int asyncReadBufferSize;

		// Token: 0x0400317A RID: 12666
		private object asyncReadState;

		// Token: 0x0400317B RID: 12667
		private WaitCallback asyncReadCallback;

		// Token: 0x0400317C RID: 12668
		private Exception asyncReadException;

		// Token: 0x0400317D RID: 12669
		private bool asyncReadPending;

		// Token: 0x0400317E RID: 12670
		private SocketAsyncEventArgs asyncWriteEventArgs;

		// Token: 0x0400317F RID: 12671
		private object asyncWriteState;

		// Token: 0x04003180 RID: 12672
		private WaitCallback asyncWriteCallback;

		// Token: 0x04003181 RID: 12673
		private Exception asyncWriteException;

		// Token: 0x04003182 RID: 12674
		private bool asyncWritePending;

		// Token: 0x04003183 RID: 12675
		private IOThreadTimer receiveTimer;

		// Token: 0x04003184 RID: 12676
		private static Action<object> onReceiveTimeout;

		// Token: 0x04003185 RID: 12677
		private IOThreadTimer sendTimer;

		// Token: 0x04003186 RID: 12678
		private static Action<object> onSendTimeout;

		// Token: 0x04003187 RID: 12679
		private string timeoutErrorString;

		// Token: 0x04003188 RID: 12680
		private SocketConnection.TransferOperation timeoutErrorTransferOperation;

		// Token: 0x04003189 RID: 12681
		private IPEndPoint remoteEndpoint;

		// Token: 0x0400318A RID: 12682
		private ConnectionBufferPool connectionBufferPool;

		// Token: 0x0400318B RID: 12683
		private string remoteEndpointAddress;

		// Token: 0x02000D39 RID: 3385
		private enum CloseState
		{
			// Token: 0x04004760 RID: 18272
			Open,
			// Token: 0x04004761 RID: 18273
			Closing,
			// Token: 0x04004762 RID: 18274
			Closed
		}

		// Token: 0x02000D3A RID: 3386
		private enum TransferOperation
		{
			// Token: 0x04004764 RID: 18276
			Write,
			// Token: 0x04004765 RID: 18277
			Read,
			// Token: 0x04004766 RID: 18278
			Undefined
		}
	}
}
