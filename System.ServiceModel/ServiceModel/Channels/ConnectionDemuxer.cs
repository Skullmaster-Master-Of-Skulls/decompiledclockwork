using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D9 RID: 2009
	internal sealed class ConnectionDemuxer : IDisposable
	{
		// Token: 0x06004BC3 RID: 19395 RVA: 0x00114AFC File Offset: 0x00112CFC
		public ConnectionDemuxer(IConnectionListener listener, int maxAccepts, int maxPendingConnections, TimeSpan channelInitializationTimeout, TimeSpan idleTimeout, int maxPooledConnections, TransportSettingsCallback transportSettingsCallback, SingletonPreambleDemuxCallback singletonPreambleCallback, ServerSessionPreambleDemuxCallback serverSessionPreambleCallback, ErrorCallback errorCallback)
		{
			this.connectionReaders = new List<InitialServerConnectionReader>();
			this.acceptor = new ConnectionAcceptor(listener, maxAccepts, maxPendingConnections, new ConnectionAvailableCallback(this.OnConnectionAvailable), errorCallback);
			this.channelInitializationTimeout = channelInitializationTimeout;
			this.idleTimeout = idleTimeout;
			this.maxPooledConnections = maxPooledConnections;
			this.onConnectionClosed = new ConnectionClosedCallback(this.OnConnectionClosed);
			this.transportSettingsCallback = transportSettingsCallback;
			this.singletonPreambleCallback = singletonPreambleCallback;
			this.serverSessionPreambleCallback = serverSessionPreambleCallback;
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x00114B78 File Offset: 0x00112D78
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x00114B7C File Offset: 0x00112D7C
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.isDisposed = true;
			}
			for (int i = 0; i < this.connectionReaders.Count; i++)
			{
				this.connectionReaders[i].Dispose();
			}
			this.connectionReaders.Clear();
			this.acceptor.Dispose();
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x00114C04 File Offset: 0x00112E04
		private ConnectionModeReader SetupModeReader(IConnection connection, bool isCached)
		{
			ConnectionModeReader connectionModeReader;
			if (isCached)
			{
				if (this.onCachedConnectionModeKnown == null)
				{
					this.onCachedConnectionModeKnown = new ConnectionModeCallback(this.OnCachedConnectionModeKnown);
				}
				connectionModeReader = new ConnectionModeReader(connection, this.onCachedConnectionModeKnown, this.onConnectionClosed);
			}
			else
			{
				if (this.onConnectionModeKnown == null)
				{
					this.onConnectionModeKnown = new ConnectionModeCallback(this.OnConnectionModeKnown);
				}
				connectionModeReader = new ConnectionModeReader(connection, this.onConnectionModeKnown, this.onConnectionClosed);
			}
			object thisLock = this.ThisLock;
			ConnectionModeReader result;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					connectionModeReader.Dispose();
					result = null;
				}
				else
				{
					this.connectionReaders.Add(connectionModeReader);
					result = connectionModeReader;
				}
			}
			return result;
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x00114CC0 File Offset: 0x00112EC0
		public void ReuseConnection(IConnection connection, TimeSpan closeTimeout)
		{
			connection.ExceptionEventType = TraceEventType.Information;
			ConnectionModeReader connectionModeReader = this.SetupModeReader(connection, true);
			if (connectionModeReader != null)
			{
				if (this.reuseConnectionCallback == null)
				{
					this.reuseConnectionCallback = new Action<object>(this.ReuseConnectionCallback);
				}
				ActionItem.Schedule(this.reuseConnectionCallback, new ConnectionDemuxer.ReuseConnectionState(connectionModeReader, closeTimeout));
			}
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x00114D0C File Offset: 0x00112F0C
		private void ReuseConnectionCallback(object state)
		{
			ConnectionDemuxer.ReuseConnectionState reuseConnectionState = (ConnectionDemuxer.ReuseConnectionState)state;
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.pooledConnectionCount >= this.maxPooledConnections)
				{
					flag = true;
				}
				else
				{
					this.pooledConnectionCount++;
				}
			}
			if (flag)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 262150, SR.GetString("TraceCodeServerMaxPooledConnectionsQuotaReached", new object[]
					{
						this.maxPooledConnections
					}), new StringTraceRecord("MaxOutboundConnectionsPerEndpoint", this.maxPooledConnections.ToString(CultureInfo.InvariantCulture)), this, null);
				}
				if (TD.ServerMaxPooledConnectionsQuotaReachedIsEnabled())
				{
					TD.ServerMaxPooledConnectionsQuotaReached();
				}
				reuseConnectionState.ModeReader.CloseFromPool(reuseConnectionState.CloseTimeout);
				return;
			}
			if (this.pooledConnectionDequeuedCallback == null)
			{
				this.pooledConnectionDequeuedCallback = new Action(this.PooledConnectionDequeuedCallback);
			}
			reuseConnectionState.ModeReader.StartReading(this.idleTimeout, this.pooledConnectionDequeuedCallback);
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x00114E10 File Offset: 0x00113010
		private void PooledConnectionDequeuedCallback()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.pooledConnectionCount--;
			}
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x00114E58 File Offset: 0x00113058
		private void OnConnectionAvailable(IConnection connection, Action connectionDequeuedCallback)
		{
			ConnectionModeReader connectionModeReader = this.SetupModeReader(connection, false);
			if (connectionModeReader != null)
			{
				connectionModeReader.StartReading(this.channelInitializationTimeout, connectionDequeuedCallback);
				return;
			}
			connectionDequeuedCallback();
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x00114E85 File Offset: 0x00113085
		private void OnCachedConnectionModeKnown(ConnectionModeReader modeReader)
		{
			this.OnConnectionModeKnownCore(modeReader, true);
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00114E8F File Offset: 0x0011308F
		private void OnConnectionModeKnown(ConnectionModeReader modeReader)
		{
			this.OnConnectionModeKnownCore(modeReader, false);
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x00114E9C File Offset: 0x0011309C
		private void OnConnectionModeKnownCore(ConnectionModeReader modeReader, bool isCached)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.connectionReaders.Remove(modeReader);
			}
			bool flag2 = true;
			try
			{
				FramingMode connectionMode;
				try
				{
					connectionMode = modeReader.GetConnectionMode();
				}
				catch (CommunicationException exception)
				{
					TraceEventType exceptionEventType = modeReader.Connection.ExceptionEventType;
					DiagnosticUtility.TraceHandledException(exception, exceptionEventType);
					return;
				}
				catch (TimeoutException ex)
				{
					if (!isCached)
					{
						ex = new TimeoutException(SR.GetString("ChannelInitializationTimeout", new object[]
						{
							this.channelInitializationTimeout
						}), ex);
						ErrorBehavior.ThrowAndCatch(ex);
					}
					if (TD.ChannelInitializationTimeoutIsEnabled())
					{
						TD.ChannelInitializationTimeout(SR.GetString("ChannelInitializationTimeout", new object[]
						{
							this.channelInitializationTimeout
						}));
					}
					TraceEventType exceptionEventType2 = modeReader.Connection.ExceptionEventType;
					DiagnosticUtility.TraceHandledException(ex, exceptionEventType2);
					return;
				}
				if (connectionMode != FramingMode.Singleton)
				{
					if (connectionMode != FramingMode.Duplex)
					{
						Exception ex2 = new InvalidDataException(SR.GetString("FramingModeNotSupported", new object[]
						{
							connectionMode
						}));
						Exception ex3 = new ProtocolException(ex2.Message, ex2);
						FramingEncodingString.AddFaultString(ex3, "http://schemas.microsoft.com/ws/2006/05/framing/faults/UnsupportedMode");
						ErrorBehavior.ThrowAndCatch(ex3);
						return;
					}
					this.OnDuplexConnection(modeReader.Connection, modeReader.ConnectionDequeuedCallback, modeReader.StreamPosition, modeReader.BufferOffset, modeReader.BufferSize, modeReader.GetRemainingTimeout());
				}
				else
				{
					this.OnSingletonConnection(modeReader.Connection, modeReader.ConnectionDequeuedCallback, modeReader.StreamPosition, modeReader.BufferOffset, modeReader.BufferSize, modeReader.GetRemainingTimeout());
				}
				flag2 = false;
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				if (!ExceptionHandler.HandleTransportExceptionHelper(exception2))
				{
					throw;
				}
			}
			finally
			{
				if (flag2)
				{
					modeReader.Dispose();
				}
			}
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x001150CC File Offset: 0x001132CC
		private void OnConnectionClosed(InitialServerConnectionReader connectionReader)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.isDisposed)
				{
					this.connectionReaders.Remove(connectionReader);
				}
			}
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x00115120 File Offset: 0x00113320
		private void OnSingletonConnection(IConnection connection, Action connectionDequeuedCallback, long streamPosition, int offset, int size, TimeSpan timeout)
		{
			if (this.onSingletonPreambleKnown == null)
			{
				this.onSingletonPreambleKnown = new ServerSingletonPreambleCallback(this.OnSingletonPreambleKnown);
			}
			ServerSingletonPreambleConnectionReader serverSingletonPreambleConnectionReader = new ServerSingletonPreambleConnectionReader(connection, connectionDequeuedCallback, streamPosition, offset, size, this.transportSettingsCallback, this.onConnectionClosed, this.onSingletonPreambleKnown);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					serverSingletonPreambleConnectionReader.Dispose();
					return;
				}
				this.connectionReaders.Add(serverSingletonPreambleConnectionReader);
			}
			serverSingletonPreambleConnectionReader.StartReading(this.viaDelegate, timeout);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x001151C0 File Offset: 0x001133C0
		private void OnSingletonPreambleKnown(ServerSingletonPreambleConnectionReader serverSingletonPreambleReader)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.connectionReaders.Remove(serverSingletonPreambleReader);
			}
			if (ConnectionDemuxer.onSingletonPreambleComplete == null)
			{
				ConnectionDemuxer.onSingletonPreambleComplete = Fx.ThunkCallback(new AsyncCallback(ConnectionDemuxer.OnSingletonPreambleComplete));
			}
			ISingletonChannelListener singletonChannelListener = this.singletonPreambleCallback(serverSingletonPreambleReader);
			IAsyncResult asyncResult = this.BeginCompleteSingletonPreamble(serverSingletonPreambleReader, singletonChannelListener, ConnectionDemuxer.onSingletonPreambleComplete, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.EndCompleteSingletonPreamble(asyncResult);
			}
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x0011525C File Offset: 0x0011345C
		private IAsyncResult BeginCompleteSingletonPreamble(ServerSingletonPreambleConnectionReader serverSingletonPreambleReader, ISingletonChannelListener singletonChannelListener, AsyncCallback callback, object state)
		{
			return new ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult(serverSingletonPreambleReader, singletonChannelListener, this, callback, state);
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x00115269 File Offset: 0x00113469
		private void EndCompleteSingletonPreamble(IAsyncResult result)
		{
			ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult.End(result);
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x00115274 File Offset: 0x00113474
		private static void OnSingletonPreambleComplete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ConnectionDemuxer connectionDemuxer = (ConnectionDemuxer)result.AsyncState;
			try
			{
				connectionDemuxer.EndCompleteSingletonPreamble(result);
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

		// Token: 0x06004BD4 RID: 19412 RVA: 0x001152C4 File Offset: 0x001134C4
		private void OnSessionPreambleKnown(ServerSessionPreambleConnectionReader serverSessionPreambleReader)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					return;
				}
				this.connectionReaders.Remove(serverSessionPreambleReader);
			}
			ConnectionDemuxer.TraceOnSessionPreambleKnown(serverSessionPreambleReader);
			this.serverSessionPreambleCallback(serverSessionPreambleReader, this);
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00115328 File Offset: 0x00113528
		private static void TraceOnSessionPreambleKnown(ServerSessionPreambleConnectionReader serverSessionPreambleReader)
		{
			if (TD.SessionPreambleUnderstoodIsEnabled())
			{
				TD.SessionPreambleUnderstood((serverSessionPreambleReader.Via != null) ? serverSessionPreambleReader.Via.ToString() : string.Empty);
			}
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00115358 File Offset: 0x00113558
		private void OnDuplexConnection(IConnection connection, Action connectionDequeuedCallback, long streamPosition, int offset, int size, TimeSpan timeout)
		{
			if (this.onSessionPreambleKnown == null)
			{
				this.onSessionPreambleKnown = new ServerSessionPreambleCallback(this.OnSessionPreambleKnown);
			}
			ServerSessionPreambleConnectionReader serverSessionPreambleConnectionReader = new ServerSessionPreambleConnectionReader(connection, connectionDequeuedCallback, streamPosition, offset, size, this.transportSettingsCallback, this.onConnectionClosed, this.onSessionPreambleKnown);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.isDisposed)
				{
					serverSessionPreambleConnectionReader.Dispose();
					return;
				}
				this.connectionReaders.Add(serverSessionPreambleConnectionReader);
			}
			serverSessionPreambleConnectionReader.StartReading(this.viaDelegate, timeout);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x001153F8 File Offset: 0x001135F8
		public void StartDemuxing()
		{
			this.StartDemuxing(null);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00115401 File Offset: 0x00113601
		public void StartDemuxing(Action<Uri> viaDelegate)
		{
			this.viaDelegate = viaDelegate;
			this.acceptor.StartAccepting();
		}

		// Token: 0x04002F5F RID: 12127
		private static AsyncCallback onSingletonPreambleComplete;

		// Token: 0x04002F60 RID: 12128
		private ConnectionAcceptor acceptor;

		// Token: 0x04002F61 RID: 12129
		private List<InitialServerConnectionReader> connectionReaders;

		// Token: 0x04002F62 RID: 12130
		private bool isDisposed;

		// Token: 0x04002F63 RID: 12131
		private ConnectionModeCallback onConnectionModeKnown;

		// Token: 0x04002F64 RID: 12132
		private ConnectionModeCallback onCachedConnectionModeKnown;

		// Token: 0x04002F65 RID: 12133
		private ConnectionClosedCallback onConnectionClosed;

		// Token: 0x04002F66 RID: 12134
		private ServerSessionPreambleCallback onSessionPreambleKnown;

		// Token: 0x04002F67 RID: 12135
		private ServerSingletonPreambleCallback onSingletonPreambleKnown;

		// Token: 0x04002F68 RID: 12136
		private Action<object> reuseConnectionCallback;

		// Token: 0x04002F69 RID: 12137
		private ServerSessionPreambleDemuxCallback serverSessionPreambleCallback;

		// Token: 0x04002F6A RID: 12138
		private SingletonPreambleDemuxCallback singletonPreambleCallback;

		// Token: 0x04002F6B RID: 12139
		private TransportSettingsCallback transportSettingsCallback;

		// Token: 0x04002F6C RID: 12140
		private Action pooledConnectionDequeuedCallback;

		// Token: 0x04002F6D RID: 12141
		private Action<Uri> viaDelegate;

		// Token: 0x04002F6E RID: 12142
		private TimeSpan channelInitializationTimeout;

		// Token: 0x04002F6F RID: 12143
		private TimeSpan idleTimeout;

		// Token: 0x04002F70 RID: 12144
		private int maxPooledConnections;

		// Token: 0x04002F71 RID: 12145
		private int pooledConnectionCount;

		// Token: 0x02000CFF RID: 3327
		private class CompleteSingletonPreambleAndDispatchRequestAsyncResult : AsyncResult
		{
			// Token: 0x06007AB9 RID: 31417 RVA: 0x001C91FD File Offset: 0x001C73FD
			public CompleteSingletonPreambleAndDispatchRequestAsyncResult(ServerSingletonPreambleConnectionReader serverSingletonPreambleReader, ISingletonChannelListener singletonChannelListener, ConnectionDemuxer demuxer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.serverSingletonPreambleReader = serverSingletonPreambleReader;
				this.singletonChannelListener = singletonChannelListener;
				this.demuxer = demuxer;
				if (this.BeginCompletePreamble())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007ABA RID: 31418 RVA: 0x001C922D File Offset: 0x001C742D
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult>(result);
			}

			// Token: 0x06007ABB RID: 31419 RVA: 0x001C9238 File Offset: 0x001C7438
			private bool BeginCompletePreamble()
			{
				this.timeoutHelper = new TimeoutHelper(this.singletonChannelListener.ReceiveTimeout);
				IAsyncResult asyncResult = this.serverSingletonPreambleReader.BeginCompletePreamble(this.timeoutHelper.RemainingTime(), ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult.onPreambleComplete, this);
				return asyncResult.CompletedSynchronously && this.HandlePreambleComplete(asyncResult);
			}

			// Token: 0x06007ABC RID: 31420 RVA: 0x001C928C File Offset: 0x001C748C
			private static void OnPreambleComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult completeSingletonPreambleAndDispatchRequestAsyncResult = (ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult)result.AsyncState;
				bool flag = false;
				try
				{
					flag = completeSingletonPreambleAndDispatchRequestAsyncResult.HandlePreambleComplete(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					flag = true;
					completeSingletonPreambleAndDispatchRequestAsyncResult.AbortConnection(exception);
				}
				if (flag)
				{
					completeSingletonPreambleAndDispatchRequestAsyncResult.Complete(false);
				}
			}

			// Token: 0x06007ABD RID: 31421 RVA: 0x001C92EC File Offset: 0x001C74EC
			private bool HandlePreambleComplete(IAsyncResult result)
			{
				IConnection upgradedConnection = this.serverSingletonPreambleReader.EndCompletePreamble(result);
				ServerSingletonConnectionReader serverSingletonConnectionReader = new ServerSingletonConnectionReader(this.serverSingletonPreambleReader, upgradedConnection, this.demuxer);
				RequestContext requestContext = serverSingletonConnectionReader.ReceiveRequest(this.timeoutHelper.RemainingTime());
				this.singletonChannelListener.ReceiveRequest(requestContext, this.serverSingletonPreambleReader.ConnectionDequeuedCallback, true);
				return true;
			}

			// Token: 0x06007ABE RID: 31422 RVA: 0x001C9344 File Offset: 0x001C7544
			private void AbortConnection(Exception exception)
			{
				this.serverSingletonPreambleReader.Abort(exception);
			}

			// Token: 0x0400462C RID: 17964
			private ServerSingletonPreambleConnectionReader serverSingletonPreambleReader;

			// Token: 0x0400462D RID: 17965
			private ISingletonChannelListener singletonChannelListener;

			// Token: 0x0400462E RID: 17966
			private ConnectionDemuxer demuxer;

			// Token: 0x0400462F RID: 17967
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004630 RID: 17968
			private static AsyncCallback onPreambleComplete = Fx.ThunkCallback(new AsyncCallback(ConnectionDemuxer.CompleteSingletonPreambleAndDispatchRequestAsyncResult.OnPreambleComplete));
		}

		// Token: 0x02000D00 RID: 3328
		private class ReuseConnectionState
		{
			// Token: 0x06007AC0 RID: 31424 RVA: 0x001C936A File Offset: 0x001C756A
			public ReuseConnectionState(ConnectionModeReader modeReader, TimeSpan closeTimeout)
			{
				this.modeReader = modeReader;
				this.closeTimeout = closeTimeout;
			}

			// Token: 0x17001BC0 RID: 7104
			// (get) Token: 0x06007AC1 RID: 31425 RVA: 0x001C9380 File Offset: 0x001C7580
			public ConnectionModeReader ModeReader
			{
				get
				{
					return this.modeReader;
				}
			}

			// Token: 0x17001BC1 RID: 7105
			// (get) Token: 0x06007AC2 RID: 31426 RVA: 0x001C9388 File Offset: 0x001C7588
			public TimeSpan CloseTimeout
			{
				get
				{
					return this.closeTimeout;
				}
			}

			// Token: 0x04004631 RID: 17969
			private ConnectionModeReader modeReader;

			// Token: 0x04004632 RID: 17970
			private TimeSpan closeTimeout;
		}
	}
}
