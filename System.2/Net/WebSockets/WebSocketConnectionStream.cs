using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000233 RID: 563
	internal class WebSocketConnectionStream : BufferedReadStream, WebSocketBase.IWebSocketStream
	{
		// Token: 0x0600151B RID: 5403 RVA: 0x0006E600 File Offset: 0x0006C800
		public WebSocketConnectionStream(ConnectStream connectStream, string connectionGroupName) : base(new WebSocketConnectionStream.WebSocketConnection(connectStream.Connection), false)
		{
			this.m_ConnectStream = connectStream;
			this.m_ConnectionGroupName = connectionGroupName;
			this.m_CloseConnectStreamLock = new object();
			this.m_IsFastPathAllowed = (this.m_ConnectStream.Connection.NetworkStream.GetType() == WebSocketConnectionStream.s_NetworkStreamType);
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Associate(Logging.WebSockets, this, this.m_ConnectStream.Connection);
			}
			this.ConsumeConnectStreamBuffer(connectStream);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0006E681 File Offset: 0x0006C881
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x0006E684 File Offset: 0x0006C884
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0006E687 File Offset: 0x0006C887
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0006E68A File Offset: 0x0006C88A
		public bool SupportsMultipleWrite
		{
			get
			{
				return ((WebSocketConnectionStream.WebSocketConnection)base.BaseStream).SupportsMultipleWrite;
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0006E69C File Offset: 0x0006C89C
		public Task CloseNetworkConnectionAsync(CancellationToken cancellationToken)
		{
			WebSocketConnectionStream.<CloseNetworkConnectionAsync>d__19 <CloseNetworkConnectionAsync>d__;
			<CloseNetworkConnectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseNetworkConnectionAsync>d__.<>4__this = this;
			<CloseNetworkConnectionAsync>d__.cancellationToken = cancellationToken;
			<CloseNetworkConnectionAsync>d__.<>1__state = -1;
			<CloseNetworkConnectionAsync>d__.<>t__builder.Start<WebSocketConnectionStream.<CloseNetworkConnectionAsync>d__19>(ref <CloseNetworkConnectionAsync>d__);
			return <CloseNetworkConnectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0006E6E8 File Offset: 0x0006C8E8
		public override void Close()
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "Close", string.Empty);
			}
			try
			{
				object closeConnectStreamLock = this.m_CloseConnectStreamLock;
				lock (closeConnectStreamLock)
				{
					this.m_ConnectStream.Connection.ServicePoint.CloseConnectionGroup(this.m_ConnectionGroupName);
				}
				base.Close();
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "Close", string.Empty);
				}
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0006E78C File Offset: 0x0006C98C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketConnectionStream.<ReadAsync>d__21 <ReadAsync>d__;
			<ReadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsync>d__.<>4__this = this;
			<ReadAsync>d__.buffer = buffer;
			<ReadAsync>d__.offset = offset;
			<ReadAsync>d__.count = count;
			<ReadAsync>d__.cancellationToken = cancellationToken;
			<ReadAsync>d__.<>1__state = -1;
			<ReadAsync>d__.<>t__builder.Start<WebSocketConnectionStream.<ReadAsync>d__21>(ref <ReadAsync>d__);
			return <ReadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0006E7F0 File Offset: 0x0006C9F0
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketConnectionStream.<WriteAsync>d__22 <WriteAsync>d__;
			<WriteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsync>d__.<>4__this = this;
			<WriteAsync>d__.buffer = buffer;
			<WriteAsync>d__.offset = offset;
			<WriteAsync>d__.count = count;
			<WriteAsync>d__.cancellationToken = cancellationToken;
			<WriteAsync>d__.<>1__state = -1;
			<WriteAsync>d__.<>t__builder.Start<WebSocketConnectionStream.<WriteAsync>d__22>(ref <WriteAsync>d__);
			return <WriteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0006E854 File Offset: 0x0006CA54
		public void SwitchToOpaqueMode(WebSocketBase webSocket)
		{
			if (this.m_InOpaqueMode)
			{
				throw new InvalidOperationException();
			}
			this.m_WebSocketConnection = (base.BaseStream as WebSocketConnectionStream.WebSocketConnection);
			if (this.m_WebSocketConnection != null && this.m_IsFastPathAllowed)
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Associate(Logging.WebSockets, this, this.m_WebSocketConnection);
				}
				this.m_WebSocketConnection.SwitchToOpaqueMode(webSocket);
				this.m_InOpaqueMode = true;
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0006E8BC File Offset: 0x0006CABC
		public Task MultipleWriteAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			WebSocketConnectionStream.<MultipleWriteAsync>d__24 <MultipleWriteAsync>d__;
			<MultipleWriteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MultipleWriteAsync>d__.<>4__this = this;
			<MultipleWriteAsync>d__.sendBuffers = sendBuffers;
			<MultipleWriteAsync>d__.cancellationToken = cancellationToken;
			<MultipleWriteAsync>d__.<>1__state = -1;
			<MultipleWriteAsync>d__.<>t__builder.Start<WebSocketConnectionStream.<MultipleWriteAsync>d__24>(ref <MultipleWriteAsync>d__);
			return <MultipleWriteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0006E90F File Offset: 0x0006CB0F
		private static bool CanHandleException(Exception error)
		{
			return error is SocketException || error is ObjectDisposedException || error is WebException || error is IOException;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0006E934 File Offset: 0x0006CB34
		private static void OnCancel(object state)
		{
			WebSocketConnectionStream webSocketConnectionStream = state as WebSocketConnectionStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, state, "OnCancel", string.Empty);
			}
			try
			{
				object closeConnectStreamLock = webSocketConnectionStream.m_CloseConnectStreamLock;
				lock (closeConnectStreamLock)
				{
					webSocketConnectionStream.m_ConnectStream.Connection.NetworkStream.InternalAbortSocket();
					((ICloseEx)webSocketConnectionStream.m_ConnectStream).CloseEx(CloseExState.Abort);
				}
				webSocketConnectionStream.CancelWebSocketConnection();
			}
			catch
			{
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, state, "OnCancel", string.Empty);
				}
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0006E9F4 File Offset: 0x0006CBF4
		private void CancelWebSocketConnection()
		{
			if (this.m_InOpaqueMode)
			{
				WebSocketConnectionStream.WebSocketConnection obj = (WebSocketConnectionStream.WebSocketConnection)base.BaseStream;
				WebSocketConnectionStream.s_OnCancelWebSocketConnection(obj);
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0006EA20 File Offset: 0x0006CC20
		public void Abort()
		{
			WebSocketConnectionStream.OnCancel(this);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0006EA28 File Offset: 0x0006CC28
		private void ConsumeConnectStreamBuffer(ConnectStream connectStream)
		{
			if (connectStream.Eof)
			{
				return;
			}
			byte[] array = new byte[1024];
			int num = 0;
			int num2 = array.Length;
			int num3;
			while ((num3 = connectStream.FillFromBufferedData(array, ref num, ref num2)) > 0)
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Dump(Logging.WebSockets, this, "ConsumeConnectStreamBuffer", array, 0, num3);
				}
				base.Append(array, 0, num3);
				num = 0;
				num2 = array.Length;
			}
		}

		// Token: 0x04001698 RID: 5784
		private static readonly Func<Exception, bool> s_CanHandleException = new Func<Exception, bool>(WebSocketConnectionStream.CanHandleException);

		// Token: 0x04001699 RID: 5785
		private static readonly Action<object> s_OnCancel = new Action<object>(WebSocketConnectionStream.OnCancel);

		// Token: 0x0400169A RID: 5786
		private static readonly Action<object> s_OnCancelWebSocketConnection = new Action<object>(WebSocketConnectionStream.WebSocketConnection.OnCancel);

		// Token: 0x0400169B RID: 5787
		private static readonly Type s_NetworkStreamType = typeof(NetworkStream);

		// Token: 0x0400169C RID: 5788
		private readonly ConnectStream m_ConnectStream;

		// Token: 0x0400169D RID: 5789
		private readonly string m_ConnectionGroupName;

		// Token: 0x0400169E RID: 5790
		private readonly bool m_IsFastPathAllowed;

		// Token: 0x0400169F RID: 5791
		private readonly object m_CloseConnectStreamLock;

		// Token: 0x040016A0 RID: 5792
		private bool m_InOpaqueMode;

		// Token: 0x040016A1 RID: 5793
		private WebSocketConnectionStream.WebSocketConnection m_WebSocketConnection;

		// Token: 0x0200077A RID: 1914
		private static class Methods
		{
			// Token: 0x040032CD RID: 13005
			public const string Close = "Close";

			// Token: 0x040032CE RID: 13006
			public const string CloseNetworkConnectionAsync = "CloseNetworkConnectionAsync";

			// Token: 0x040032CF RID: 13007
			public const string OnCancel = "OnCancel";

			// Token: 0x040032D0 RID: 13008
			public const string ReadAsync = "ReadAsync";

			// Token: 0x040032D1 RID: 13009
			public const string WriteAsync = "WriteAsync";

			// Token: 0x040032D2 RID: 13010
			public const string MultipleWriteAsync = "MultipleWriteAsync";
		}

		// Token: 0x0200077B RID: 1915
		private class WebSocketConnection : DelegatedStream, WebSocketBase.IWebSocketStream
		{
			// Token: 0x06004290 RID: 17040 RVA: 0x001159C2 File Offset: 0x00113BC2
			internal WebSocketConnection(Connection connection) : base(connection)
			{
				this.m_InnerStream = connection;
				this.m_InOpaqueMode = false;
				this.m_SupportsMultipleWrites = (connection.NetworkStream.GetType().Assembly == WebSocketConnectionStream.s_NetworkStreamType.Assembly);
			}

			// Token: 0x17000F31 RID: 3889
			// (get) Token: 0x06004291 RID: 17041 RVA: 0x001159FE File Offset: 0x00113BFE
			internal Socket InnerSocket
			{
				get
				{
					return this.GetInnerSocket(false);
				}
			}

			// Token: 0x17000F32 RID: 3890
			// (get) Token: 0x06004292 RID: 17042 RVA: 0x00115A07 File Offset: 0x00113C07
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F33 RID: 3891
			// (get) Token: 0x06004293 RID: 17043 RVA: 0x00115A0A File Offset: 0x00113C0A
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F34 RID: 3892
			// (get) Token: 0x06004294 RID: 17044 RVA: 0x00115A0D File Offset: 0x00113C0D
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F35 RID: 3893
			// (get) Token: 0x06004295 RID: 17045 RVA: 0x00115A10 File Offset: 0x00113C10
			public bool SupportsMultipleWrite
			{
				get
				{
					return this.m_SupportsMultipleWrites;
				}
			}

			// Token: 0x06004296 RID: 17046 RVA: 0x00115A18 File Offset: 0x00113C18
			public Task CloseNetworkConnectionAsync(CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06004297 RID: 17047 RVA: 0x00115A20 File Offset: 0x00113C20
			public override void Close()
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "Close", string.Empty);
				}
				try
				{
					base.Close();
					if (Interlocked.Increment(ref this.m_CleanedUp) == 1)
					{
						if (this.m_WriteEventArgs != null)
						{
							this.m_WriteEventArgs.Completed -= WebSocketConnectionStream.WebSocketConnection.s_OnWriteCompleted;
							this.m_WriteEventArgs.Dispose();
						}
						if (this.m_ReadEventArgs != null)
						{
							this.m_ReadEventArgs.Completed -= WebSocketConnectionStream.WebSocketConnection.s_OnReadCompleted;
							this.m_ReadEventArgs.Dispose();
						}
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "Close", string.Empty);
					}
				}
			}

			// Token: 0x06004298 RID: 17048 RVA: 0x00115AD4 File Offset: 0x00113CD4
			internal Socket GetInnerSocket(bool skipStateCheck)
			{
				if (!skipStateCheck)
				{
					this.m_WebSocket.ThrowIfClosedOrAborted();
				}
				Socket internalSocket;
				try
				{
					internalSocket = this.m_InnerStream.NetworkStream.InternalSocket;
				}
				catch (ObjectDisposedException)
				{
					this.m_WebSocket.ThrowIfClosedOrAborted();
					throw;
				}
				return internalSocket;
			}

			// Token: 0x06004299 RID: 17049 RVA: 0x00115B24 File Offset: 0x00113D24
			private static IAsyncResult BeginMultipleWrite(IList<ArraySegment<byte>> sendBuffers, AsyncCallback callback, object asyncState)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = asyncState as WebSocketConnectionStream.WebSocketConnection;
				BufferOffsetSize[] array = new BufferOffsetSize[sendBuffers.Count];
				for (int i = 0; i < sendBuffers.Count; i++)
				{
					ArraySegment<byte> arraySegment = sendBuffers[i];
					array[i] = new BufferOffsetSize(arraySegment.Array, arraySegment.Offset, arraySegment.Count, false);
				}
				WebSocketHelpers.ThrowIfConnectionAborted(webSocketConnection.m_InnerStream, false);
				return webSocketConnection.m_InnerStream.NetworkStream.BeginMultipleWrite(array, callback, asyncState);
			}

			// Token: 0x0600429A RID: 17050 RVA: 0x00115B9C File Offset: 0x00113D9C
			private static void EndMultipleWrite(IAsyncResult asyncResult)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = asyncResult.AsyncState as WebSocketConnectionStream.WebSocketConnection;
				WebSocketHelpers.ThrowIfConnectionAborted(webSocketConnection.m_InnerStream, false);
				webSocketConnection.m_InnerStream.NetworkStream.EndMultipleWrite(asyncResult);
			}

			// Token: 0x0600429B RID: 17051 RVA: 0x00115BD4 File Offset: 0x00113DD4
			public Task MultipleWriteAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
			{
				if (!this.m_InOpaqueMode)
				{
					return Task.Factory.FromAsync<IList<ArraySegment<byte>>>(WebSocketConnectionStream.WebSocketConnection.s_BeginMultipleWrite, WebSocketConnectionStream.WebSocketConnection.s_EndMultipleWrite, sendBuffers, this);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "MultipleWriteAsync", string.Empty);
				}
				bool flag = false;
				Task result;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebSocketHelpers.ThrowIfConnectionAborted(this.m_InnerStream, false);
					this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
					this.m_WriteEventArgs.SetBuffer(null, 0, 0);
					this.m_WriteEventArgs.BufferList = sendBuffers;
					flag = this.InnerSocket.SendAsync(this.m_WriteEventArgs);
					if (!flag)
					{
						if (this.m_WriteEventArgs.SocketError != SocketError.Success)
						{
							throw new SocketException(this.m_WriteEventArgs.SocketError);
						}
						result = Task.CompletedTask;
					}
					else
					{
						result = this.m_WriteTaskCompletionSource.Task;
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "MultipleWriteAsync", flag);
					}
				}
				return result;
			}

			// Token: 0x0600429C RID: 17052 RVA: 0x00115CD0 File Offset: 0x00113ED0
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				WebSocketHelpers.ValidateBuffer(buffer, offset, count);
				if (!this.m_InOpaqueMode)
				{
					return base.WriteAsync(buffer, offset, count, cancellationToken);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "WriteAsync", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
				}
				bool flag = false;
				Task result;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebSocketHelpers.ThrowIfConnectionAborted(this.m_InnerStream, false);
					this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
					this.m_WriteEventArgs.BufferList = null;
					this.m_WriteEventArgs.SetBuffer(buffer, offset, count);
					flag = this.InnerSocket.SendAsync(this.m_WriteEventArgs);
					if (!flag)
					{
						if (this.m_WriteEventArgs.SocketError != SocketError.Success)
						{
							throw new SocketException(this.m_WriteEventArgs.SocketError);
						}
						result = Task.CompletedTask;
					}
					else
					{
						result = this.m_WriteTaskCompletionSource.Task;
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "WriteAsync", flag);
					}
				}
				return result;
			}

			// Token: 0x0600429D RID: 17053 RVA: 0x00115DCC File Offset: 0x00113FCC
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				WebSocketHelpers.ValidateBuffer(buffer, offset, count);
				if (!this.m_InOpaqueMode)
				{
					return base.ReadAsync(buffer, offset, count, cancellationToken);
				}
				return this.ReadAsyncCore(buffer, offset, count, cancellationToken, false);
			}

			// Token: 0x0600429E RID: 17054 RVA: 0x00115DF8 File Offset: 0x00113FF8
			internal Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool ignoreReadError)
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "ReadAsyncCore", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
				}
				bool flag = false;
				this.m_IgnoreReadError = ignoreReadError;
				Task<int> result;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebSocketHelpers.ThrowIfConnectionAborted(this.m_InnerStream, true);
					this.m_ReadTaskCompletionSource = new TaskCompletionSource<int>();
					this.m_ReadEventArgs.SetBuffer(buffer, offset, count);
					Socket innerSocket;
					if (ignoreReadError)
					{
						innerSocket = this.GetInnerSocket(true);
					}
					else
					{
						innerSocket = this.InnerSocket;
					}
					flag = innerSocket.ReceiveAsync(this.m_ReadEventArgs);
					if (!flag)
					{
						if (this.m_ReadEventArgs.SocketError != SocketError.Success)
						{
							if (!this.m_IgnoreReadError)
							{
								throw new SocketException(this.m_ReadEventArgs.SocketError);
							}
							result = Task.FromResult<int>(0);
						}
						else
						{
							result = Task.FromResult<int>(this.m_ReadEventArgs.BytesTransferred);
						}
					}
					else
					{
						result = this.m_ReadTaskCompletionSource.Task;
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "ReadAsyncCore", flag);
					}
				}
				return result;
			}

			// Token: 0x0600429F RID: 17055 RVA: 0x00115F00 File Offset: 0x00114100
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!this.m_InOpaqueMode)
				{
					return base.FlushAsync(cancellationToken);
				}
				cancellationToken.ThrowIfCancellationRequested();
				return Task.CompletedTask;
			}

			// Token: 0x060042A0 RID: 17056 RVA: 0x00115F1E File Offset: 0x0011411E
			public void Abort()
			{
			}

			// Token: 0x060042A1 RID: 17057 RVA: 0x00115F20 File Offset: 0x00114120
			internal static void OnCancel(object state)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = state as WebSocketConnectionStream.WebSocketConnection;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, webSocketConnection, "OnCancel", string.Empty);
				}
				try
				{
					TaskCompletionSource<int> readTaskCompletionSource = webSocketConnection.m_ReadTaskCompletionSource;
					if (readTaskCompletionSource != null)
					{
						readTaskCompletionSource.TrySetCanceled();
					}
					TaskCompletionSource<object> writeTaskCompletionSource = webSocketConnection.m_WriteTaskCompletionSource;
					if (writeTaskCompletionSource != null)
					{
						writeTaskCompletionSource.TrySetCanceled();
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, webSocketConnection, "OnCancel", string.Empty);
					}
				}
			}

			// Token: 0x060042A2 RID: 17058 RVA: 0x00115FA4 File Offset: 0x001141A4
			public void SwitchToOpaqueMode(WebSocketBase webSocket)
			{
				this.m_WebSocket = webSocket;
				this.m_InOpaqueMode = true;
				this.m_ReadEventArgs = new SocketAsyncEventArgs();
				this.m_ReadEventArgs.UserToken = this;
				this.m_ReadEventArgs.Completed += WebSocketConnectionStream.WebSocketConnection.s_OnReadCompleted;
				this.m_WriteEventArgs = new SocketAsyncEventArgs();
				this.m_WriteEventArgs.UserToken = this;
				this.m_WriteEventArgs.Completed += WebSocketConnectionStream.WebSocketConnection.s_OnWriteCompleted;
			}

			// Token: 0x060042A3 RID: 17059 RVA: 0x0011600D File Offset: 0x0011420D
			private static string GetIOCompletionTraceMsg(SocketAsyncEventArgs eventArgs)
			{
				return string.Format(CultureInfo.InvariantCulture, "LastOperation: {0}, SocketError: {1}", new object[]
				{
					eventArgs.LastOperation,
					eventArgs.SocketError
				});
			}

			// Token: 0x060042A4 RID: 17060 RVA: 0x00116040 File Offset: 0x00114240
			private static void OnWriteCompleted(object sender, SocketAsyncEventArgs eventArgs)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = eventArgs.UserToken as WebSocketConnectionStream.WebSocketConnection;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, webSocketConnection, "OnWriteCompleted", WebSocketConnectionStream.WebSocketConnection.GetIOCompletionTraceMsg(eventArgs));
				}
				if (eventArgs.SocketError != SocketError.Success)
				{
					webSocketConnection.m_WriteTaskCompletionSource.TrySetException(new SocketException(eventArgs.SocketError));
				}
				else
				{
					webSocketConnection.m_WriteTaskCompletionSource.TrySetResult(null);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, webSocketConnection, "OnWriteCompleted", string.Empty);
				}
			}

			// Token: 0x060042A5 RID: 17061 RVA: 0x001160C0 File Offset: 0x001142C0
			private static void OnReadCompleted(object sender, SocketAsyncEventArgs eventArgs)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = eventArgs.UserToken as WebSocketConnectionStream.WebSocketConnection;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, webSocketConnection, "OnReadCompleted", WebSocketConnectionStream.WebSocketConnection.GetIOCompletionTraceMsg(eventArgs));
				}
				if (eventArgs.SocketError != SocketError.Success)
				{
					if (!webSocketConnection.m_IgnoreReadError)
					{
						webSocketConnection.m_ReadTaskCompletionSource.TrySetException(new SocketException(eventArgs.SocketError));
					}
					else
					{
						webSocketConnection.m_ReadTaskCompletionSource.TrySetResult(0);
					}
				}
				else
				{
					webSocketConnection.m_ReadTaskCompletionSource.TrySetResult(eventArgs.BytesTransferred);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, webSocketConnection, "OnReadCompleted", string.Empty);
				}
			}

			// Token: 0x040032D3 RID: 13011
			private static readonly EventHandler<SocketAsyncEventArgs> s_OnReadCompleted = new EventHandler<SocketAsyncEventArgs>(WebSocketConnectionStream.WebSocketConnection.OnReadCompleted);

			// Token: 0x040032D4 RID: 13012
			private static readonly EventHandler<SocketAsyncEventArgs> s_OnWriteCompleted = new EventHandler<SocketAsyncEventArgs>(WebSocketConnectionStream.WebSocketConnection.OnWriteCompleted);

			// Token: 0x040032D5 RID: 13013
			private static readonly Func<IList<ArraySegment<byte>>, AsyncCallback, object, IAsyncResult> s_BeginMultipleWrite = new Func<IList<ArraySegment<byte>>, AsyncCallback, object, IAsyncResult>(WebSocketConnectionStream.WebSocketConnection.BeginMultipleWrite);

			// Token: 0x040032D6 RID: 13014
			private static readonly Action<IAsyncResult> s_EndMultipleWrite = new Action<IAsyncResult>(WebSocketConnectionStream.WebSocketConnection.EndMultipleWrite);

			// Token: 0x040032D7 RID: 13015
			private readonly Connection m_InnerStream;

			// Token: 0x040032D8 RID: 13016
			private readonly bool m_SupportsMultipleWrites;

			// Token: 0x040032D9 RID: 13017
			private bool m_InOpaqueMode;

			// Token: 0x040032DA RID: 13018
			private WebSocketBase m_WebSocket;

			// Token: 0x040032DB RID: 13019
			private SocketAsyncEventArgs m_WriteEventArgs;

			// Token: 0x040032DC RID: 13020
			private SocketAsyncEventArgs m_ReadEventArgs;

			// Token: 0x040032DD RID: 13021
			private TaskCompletionSource<object> m_WriteTaskCompletionSource;

			// Token: 0x040032DE RID: 13022
			private TaskCompletionSource<int> m_ReadTaskCompletionSource;

			// Token: 0x040032DF RID: 13023
			private int m_CleanedUp;

			// Token: 0x040032E0 RID: 13024
			private bool m_IgnoreReadError;

			// Token: 0x0200091F RID: 2335
			private static class Methods
			{
				// Token: 0x04003D9F RID: 15775
				public const string Close = "Close";

				// Token: 0x04003DA0 RID: 15776
				public const string OnCancel = "OnCancel";

				// Token: 0x04003DA1 RID: 15777
				public const string OnReadCompleted = "OnReadCompleted";

				// Token: 0x04003DA2 RID: 15778
				public const string OnWriteCompleted = "OnWriteCompleted";

				// Token: 0x04003DA3 RID: 15779
				public const string ReadAsyncCore = "ReadAsyncCore";

				// Token: 0x04003DA4 RID: 15780
				public const string WriteAsync = "WriteAsync";

				// Token: 0x04003DA5 RID: 15781
				public const string MultipleWriteAsync = "MultipleWriteAsync";
			}
		}
	}
}
