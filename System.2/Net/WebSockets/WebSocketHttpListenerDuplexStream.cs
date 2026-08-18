using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000238 RID: 568
	internal class WebSocketHttpListenerDuplexStream : Stream, WebSocketBase.IWebSocketStream
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x0006F59F File Offset: 0x0006D79F
		public WebSocketHttpListenerDuplexStream(HttpRequestStream inputStream, HttpResponseStream outputStream, HttpListenerContext context)
		{
			this.m_InputStream = inputStream;
			this.m_OutputStream = outputStream;
			this.m_Context = context;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Associate(Logging.WebSockets, inputStream, this);
				Logging.Associate(Logging.WebSockets, outputStream, this);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0006F5DB File Offset: 0x0006D7DB
		public override bool CanRead
		{
			get
			{
				return this.m_InputStream.CanRead;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0006F5E8 File Offset: 0x0006D7E8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0006F5EB File Offset: 0x0006D7EB
		public override bool CanTimeout
		{
			get
			{
				return this.m_InputStream.CanTimeout && this.m_OutputStream.CanTimeout;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0006F607 File Offset: 0x0006D807
		public override bool CanWrite
		{
			get
			{
				return this.m_OutputStream.CanWrite;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0006F614 File Offset: 0x0006D814
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0006F625 File Offset: 0x0006D825
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x0006F636 File Offset: 0x0006D836
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0006F647 File Offset: 0x0006D847
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_InputStream.Read(buffer, offset, count);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0006F657 File Offset: 0x0006D857
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateBuffer(buffer, offset, count);
			return this.ReadAsyncCore(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0006F66C File Offset: 0x0006D86C
		private Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketHttpListenerDuplexStream.<ReadAsyncCore>d__30 <ReadAsyncCore>d__;
			<ReadAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadAsyncCore>d__.<>4__this = this;
			<ReadAsyncCore>d__.buffer = buffer;
			<ReadAsyncCore>d__.offset = offset;
			<ReadAsyncCore>d__.count = count;
			<ReadAsyncCore>d__.cancellationToken = cancellationToken;
			<ReadAsyncCore>d__.<>1__state = -1;
			<ReadAsyncCore>d__.<>t__builder.Start<WebSocketHttpListenerDuplexStream.<ReadAsyncCore>d__30>(ref <ReadAsyncCore>d__);
			return <ReadAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0006F6D0 File Offset: 0x0006D8D0
		private unsafe bool ReadAsyncFast(WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "ReadAsyncFast", string.Empty);
			}
			eventArgs.StartOperationCommon(this);
			eventArgs.StartOperationReceive();
			bool flag = false;
			try
			{
				if (eventArgs.Count == 0 || this.m_InputStream.Closed)
				{
					eventArgs.FinishOperationSuccess(0, true);
					return false;
				}
				uint num = 0U;
				int num2 = eventArgs.Offset;
				int num3 = eventArgs.Count;
				if (this.m_InputStream.BufferedDataChunksAvailable)
				{
					num = this.m_InputStream.GetChunks(eventArgs.Buffer, eventArgs.Offset, eventArgs.Count);
					if (this.m_InputStream.BufferedDataChunksAvailable && (ulong)num == (ulong)((long)eventArgs.Count))
					{
						eventArgs.FinishOperationSuccess(eventArgs.Count, true);
						return false;
					}
				}
				if (num != 0U)
				{
					num2 += (int)num;
					num3 -= (int)num;
					if (num3 > 131072)
					{
						num3 = 131072;
					}
					eventArgs.SetBuffer(eventArgs.Buffer, num2, num3);
				}
				else if (num3 > 131072)
				{
					num3 = 131072;
					eventArgs.SetBuffer(eventArgs.Buffer, num2, num3);
				}
				this.m_InputStream.InternalHttpContext.EnsureBoundHandle();
				uint flags = 0U;
				uint bytesTransferred = 0U;
				uint num4 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody2(this.m_InputStream.InternalHttpContext.RequestQueueHandle, this.m_InputStream.InternalHttpContext.RequestId, flags, (void*)this.m_WebSocket.InternalBuffer.ToIntPtr(eventArgs.Offset), (uint)eventArgs.Count, out bytesTransferred, eventArgs.NativeOverlapped);
				if (num4 != 0U && num4 != 997U && num4 != 38U)
				{
					throw new HttpListenerException((int)num4);
				}
				if (num4 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					eventArgs.FinishOperationSuccess((int)bytesTransferred, true);
					flag = false;
				}
				else if (num4 == 38U)
				{
					eventArgs.FinishOperationSuccess(0, true);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception exception)
			{
				this.m_ReadEventArgs.FinishOperationFailure(exception, true);
				this.m_OutputStream.SetClosedFlag();
				this.m_OutputStream.InternalHttpContext.Abort();
				throw;
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "ReadAsyncFast", flag);
				}
			}
			return flag;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0006F914 File Offset: 0x0006DB14
		public override int ReadByte()
		{
			return this.m_InputStream.ReadByte();
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0006F921 File Offset: 0x0006DB21
		public bool SupportsMultipleWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006F924 File Offset: 0x0006DB24
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_InputStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0006F938 File Offset: 0x0006DB38
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_InputStream.EndRead(asyncResult);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0006F948 File Offset: 0x0006DB48
		public Task MultipleWriteAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			if (sendBuffers.Count == 1)
			{
				ArraySegment<byte> arraySegment = sendBuffers[0];
				return this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken);
			}
			return this.MultipleWriteAsyncCore(sendBuffers, cancellationToken);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0006F98C File Offset: 0x0006DB8C
		private Task MultipleWriteAsyncCore(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			WebSocketHttpListenerDuplexStream.<MultipleWriteAsyncCore>d__38 <MultipleWriteAsyncCore>d__;
			<MultipleWriteAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MultipleWriteAsyncCore>d__.<>4__this = this;
			<MultipleWriteAsyncCore>d__.sendBuffers = sendBuffers;
			<MultipleWriteAsyncCore>d__.cancellationToken = cancellationToken;
			<MultipleWriteAsyncCore>d__.<>1__state = -1;
			<MultipleWriteAsyncCore>d__.<>t__builder.Start<WebSocketHttpListenerDuplexStream.<MultipleWriteAsyncCore>d__38>(ref <MultipleWriteAsyncCore>d__);
			return <MultipleWriteAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0006F9DF File Offset: 0x0006DBDF
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_OutputStream.Write(buffer, offset, count);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0006F9EF File Offset: 0x0006DBEF
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateBuffer(buffer, offset, count);
			return this.WriteAsyncCore(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0006FA04 File Offset: 0x0006DC04
		private Task WriteAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketHttpListenerDuplexStream.<WriteAsyncCore>d__41 <WriteAsyncCore>d__;
			<WriteAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAsyncCore>d__.<>4__this = this;
			<WriteAsyncCore>d__.buffer = buffer;
			<WriteAsyncCore>d__.offset = offset;
			<WriteAsyncCore>d__.count = count;
			<WriteAsyncCore>d__.cancellationToken = cancellationToken;
			<WriteAsyncCore>d__.<>1__state = -1;
			<WriteAsyncCore>d__.<>t__builder.Start<WebSocketHttpListenerDuplexStream.<WriteAsyncCore>d__41>(ref <WriteAsyncCore>d__);
			return <WriteAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0006FA68 File Offset: 0x0006DC68
		private bool WriteAsyncFast(WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "WriteAsyncFast", string.Empty);
			}
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			eventArgs.StartOperationCommon(this);
			eventArgs.StartOperationSend();
			bool flag = false;
			try
			{
				if (this.m_OutputStream.Closed || (eventArgs.Buffer != null && eventArgs.Count == 0))
				{
					eventArgs.FinishOperationSuccess(eventArgs.Count, true);
					return false;
				}
				if (eventArgs.ShouldCloseOutput)
				{
					http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
				}
				else
				{
					http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA;
					http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_BUFFER_DATA;
				}
				this.m_OutputStream.InternalHttpContext.EnsureBoundHandle();
				uint bytesTransferred;
				uint num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody2(this.m_OutputStream.InternalHttpContext.RequestQueueHandle, this.m_OutputStream.InternalHttpContext.RequestId, (uint)http_FLAGS, eventArgs.EntityChunkCount, eventArgs.EntityChunks, out bytesTransferred, SafeLocalFree.Zero, 0U, eventArgs.NativeOverlapped, IntPtr.Zero);
				if (num != 0U && num != 997U)
				{
					throw new HttpListenerException((int)num);
				}
				if (num == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					eventArgs.FinishOperationSuccess((int)bytesTransferred, true);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception exception)
			{
				this.m_WriteEventArgs.FinishOperationFailure(exception, true);
				this.m_OutputStream.SetClosedFlag();
				this.m_OutputStream.InternalHttpContext.Abort();
				throw;
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "WriteAsyncFast", flag);
				}
			}
			return flag;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0006FBD8 File Offset: 0x0006DDD8
		public override void WriteByte(byte value)
		{
			this.m_OutputStream.WriteByte(value);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0006FBE6 File Offset: 0x0006DDE6
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_OutputStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0006FBFA File Offset: 0x0006DDFA
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_OutputStream.EndWrite(asyncResult);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0006FC08 File Offset: 0x0006DE08
		public override void Flush()
		{
			this.m_OutputStream.Flush();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0006FC15 File Offset: 0x0006DE15
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.m_OutputStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0006FC23 File Offset: 0x0006DE23
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0006FC34 File Offset: 0x0006DE34
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0006FC48 File Offset: 0x0006DE48
		public Task CloseNetworkConnectionAsync(CancellationToken cancellationToken)
		{
			WebSocketHttpListenerDuplexStream.<CloseNetworkConnectionAsync>d__50 <CloseNetworkConnectionAsync>d__;
			<CloseNetworkConnectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CloseNetworkConnectionAsync>d__.<>4__this = this;
			<CloseNetworkConnectionAsync>d__.cancellationToken = cancellationToken;
			<CloseNetworkConnectionAsync>d__.<>1__state = -1;
			<CloseNetworkConnectionAsync>d__.<>t__builder.Start<WebSocketHttpListenerDuplexStream.<CloseNetworkConnectionAsync>d__50>(ref <CloseNetworkConnectionAsync>d__);
			return <CloseNetworkConnectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0006FC94 File Offset: 0x0006DE94
		protected override void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this.m_CleanedUp, 1) == 0)
			{
				if (this.m_ReadTaskCompletionSource != null)
				{
					this.m_ReadTaskCompletionSource.TrySetCanceled();
				}
				if (this.m_WriteTaskCompletionSource != null)
				{
					this.m_WriteTaskCompletionSource.TrySetCanceled();
				}
				if (this.m_ReadEventArgs != null)
				{
					this.m_ReadEventArgs.Dispose();
				}
				if (this.m_WriteEventArgs != null)
				{
					this.m_WriteEventArgs.Dispose();
				}
				try
				{
					this.m_InputStream.Close();
				}
				finally
				{
					this.m_OutputStream.Close();
				}
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0006FD2C File Offset: 0x0006DF2C
		public void Abort()
		{
			WebSocketHttpListenerDuplexStream.OnCancel(this);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0006FD34 File Offset: 0x0006DF34
		private static bool CanHandleException(Exception error)
		{
			return error is HttpListenerException || error is ObjectDisposedException || error is IOException;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0006FD54 File Offset: 0x0006DF54
		private static void OnCancel(object state)
		{
			WebSocketHttpListenerDuplexStream webSocketHttpListenerDuplexStream = state as WebSocketHttpListenerDuplexStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, state, "OnCancel", string.Empty);
			}
			try
			{
				webSocketHttpListenerDuplexStream.m_OutputStream.SetClosedFlag();
				webSocketHttpListenerDuplexStream.m_Context.Abort();
			}
			catch
			{
			}
			TaskCompletionSource<int> readTaskCompletionSource = webSocketHttpListenerDuplexStream.m_ReadTaskCompletionSource;
			if (readTaskCompletionSource != null)
			{
				readTaskCompletionSource.TrySetCanceled();
			}
			TaskCompletionSource<object> writeTaskCompletionSource = webSocketHttpListenerDuplexStream.m_WriteTaskCompletionSource;
			if (writeTaskCompletionSource != null)
			{
				writeTaskCompletionSource.TrySetCanceled();
			}
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Exit(Logging.WebSockets, state, "OnCancel", string.Empty);
			}
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0006FDF0 File Offset: 0x0006DFF0
		public void SwitchToOpaqueMode(WebSocketBase webSocket)
		{
			if (this.m_InOpaqueMode)
			{
				throw new InvalidOperationException();
			}
			this.m_WebSocket = webSocket;
			this.m_InOpaqueMode = true;
			this.m_ReadEventArgs = new WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs(webSocket, this);
			this.m_ReadEventArgs.Completed += WebSocketHttpListenerDuplexStream.s_OnReadCompleted;
			this.m_WriteEventArgs = new WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs(webSocket, this);
			this.m_WriteEventArgs.Completed += WebSocketHttpListenerDuplexStream.s_OnWriteCompleted;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Associate(Logging.WebSockets, this, webSocket);
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0006FE68 File Offset: 0x0006E068
		private static void OnWriteCompleted(object sender, WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			WebSocketHttpListenerDuplexStream currentStream = eventArgs.CurrentStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, currentStream, "OnWriteCompleted", string.Empty);
			}
			if (eventArgs.Exception != null)
			{
				currentStream.m_WriteTaskCompletionSource.TrySetException(eventArgs.Exception);
			}
			else
			{
				currentStream.m_WriteTaskCompletionSource.TrySetResult(null);
			}
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Exit(Logging.WebSockets, currentStream, "OnWriteCompleted", string.Empty);
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0006FEE0 File Offset: 0x0006E0E0
		private static void OnReadCompleted(object sender, WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			WebSocketHttpListenerDuplexStream currentStream = eventArgs.CurrentStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, currentStream, "OnReadCompleted", string.Empty);
			}
			if (eventArgs.Exception != null)
			{
				currentStream.m_ReadTaskCompletionSource.TrySetException(eventArgs.Exception);
			}
			else
			{
				currentStream.m_ReadTaskCompletionSource.TrySetResult(eventArgs.BytesTransferred);
			}
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Exit(Logging.WebSockets, currentStream, "OnReadCompleted", string.Empty);
			}
		}

		// Token: 0x040016BC RID: 5820
		private static readonly EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> s_OnReadCompleted = new EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs>(WebSocketHttpListenerDuplexStream.OnReadCompleted);

		// Token: 0x040016BD RID: 5821
		private static readonly EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> s_OnWriteCompleted = new EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs>(WebSocketHttpListenerDuplexStream.OnWriteCompleted);

		// Token: 0x040016BE RID: 5822
		private static readonly Func<Exception, bool> s_CanHandleException = new Func<Exception, bool>(WebSocketHttpListenerDuplexStream.CanHandleException);

		// Token: 0x040016BF RID: 5823
		private static readonly Action<object> s_OnCancel = new Action<object>(WebSocketHttpListenerDuplexStream.OnCancel);

		// Token: 0x040016C0 RID: 5824
		private readonly HttpRequestStream m_InputStream;

		// Token: 0x040016C1 RID: 5825
		private readonly HttpResponseStream m_OutputStream;

		// Token: 0x040016C2 RID: 5826
		private HttpListenerContext m_Context;

		// Token: 0x040016C3 RID: 5827
		private bool m_InOpaqueMode;

		// Token: 0x040016C4 RID: 5828
		private WebSocketBase m_WebSocket;

		// Token: 0x040016C5 RID: 5829
		private WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs m_WriteEventArgs;

		// Token: 0x040016C6 RID: 5830
		private WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs m_ReadEventArgs;

		// Token: 0x040016C7 RID: 5831
		private TaskCompletionSource<object> m_WriteTaskCompletionSource;

		// Token: 0x040016C8 RID: 5832
		private TaskCompletionSource<int> m_ReadTaskCompletionSource;

		// Token: 0x040016C9 RID: 5833
		private int m_CleanedUp;

		// Token: 0x02000782 RID: 1922
		internal class HttpListenerAsyncEventArgs : EventArgs, IDisposable
		{
			// Token: 0x14000072 RID: 114
			// (add) Token: 0x060042B1 RID: 17073 RVA: 0x001170F4 File Offset: 0x001152F4
			// (remove) Token: 0x060042B2 RID: 17074 RVA: 0x0011712C File Offset: 0x0011532C
			private event EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> m_Completed;

			// Token: 0x060042B3 RID: 17075 RVA: 0x00117161 File Offset: 0x00115361
			public HttpListenerAsyncEventArgs(WebSocketBase webSocket, WebSocketHttpListenerDuplexStream stream)
			{
				this.m_WebSocket = webSocket;
				this.m_CurrentStream = stream;
				this.m_AllocateOverlappedOnDemand = LocalAppContextSwitches.AllocateOverlappedOnDemand;
				if (!this.m_AllocateOverlappedOnDemand)
				{
					this.InitializeOverlapped();
				}
			}

			// Token: 0x17000F36 RID: 3894
			// (get) Token: 0x060042B4 RID: 17076 RVA: 0x00117190 File Offset: 0x00115390
			public int BytesTransferred
			{
				get
				{
					return this.m_BytesTransferred;
				}
			}

			// Token: 0x17000F37 RID: 3895
			// (get) Token: 0x060042B5 RID: 17077 RVA: 0x00117198 File Offset: 0x00115398
			public byte[] Buffer
			{
				get
				{
					return this.m_Buffer;
				}
			}

			// Token: 0x17000F38 RID: 3896
			// (get) Token: 0x060042B6 RID: 17078 RVA: 0x001171A0 File Offset: 0x001153A0
			// (set) Token: 0x060042B7 RID: 17079 RVA: 0x001171A8 File Offset: 0x001153A8
			public IList<ArraySegment<byte>> BufferList
			{
				get
				{
					return this.m_BufferList;
				}
				set
				{
					this.m_BufferList = value;
				}
			}

			// Token: 0x17000F39 RID: 3897
			// (get) Token: 0x060042B8 RID: 17080 RVA: 0x001171B1 File Offset: 0x001153B1
			public bool ShouldCloseOutput
			{
				get
				{
					return this.m_ShouldCloseOutput;
				}
			}

			// Token: 0x17000F3A RID: 3898
			// (get) Token: 0x060042B9 RID: 17081 RVA: 0x001171B9 File Offset: 0x001153B9
			public int Offset
			{
				get
				{
					return this.m_Offset;
				}
			}

			// Token: 0x17000F3B RID: 3899
			// (get) Token: 0x060042BA RID: 17082 RVA: 0x001171C1 File Offset: 0x001153C1
			public int Count
			{
				get
				{
					return this.m_Count;
				}
			}

			// Token: 0x17000F3C RID: 3900
			// (get) Token: 0x060042BB RID: 17083 RVA: 0x001171C9 File Offset: 0x001153C9
			public Exception Exception
			{
				get
				{
					return this.m_Exception;
				}
			}

			// Token: 0x17000F3D RID: 3901
			// (get) Token: 0x060042BC RID: 17084 RVA: 0x001171D1 File Offset: 0x001153D1
			public ushort EntityChunkCount
			{
				get
				{
					if (this.m_DataChunks == null)
					{
						return 0;
					}
					return this.m_DataChunkCount;
				}
			}

			// Token: 0x17000F3E RID: 3902
			// (get) Token: 0x060042BD RID: 17085 RVA: 0x001171E3 File Offset: 0x001153E3
			public SafeNativeOverlapped NativeOverlapped
			{
				get
				{
					return this.m_PtrNativeOverlapped;
				}
			}

			// Token: 0x17000F3F RID: 3903
			// (get) Token: 0x060042BE RID: 17086 RVA: 0x001171EB File Offset: 0x001153EB
			public IntPtr EntityChunks
			{
				get
				{
					if (this.m_DataChunks == null)
					{
						return IntPtr.Zero;
					}
					return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_DataChunks, 0);
				}
			}

			// Token: 0x17000F40 RID: 3904
			// (get) Token: 0x060042BF RID: 17087 RVA: 0x00117207 File Offset: 0x00115407
			public WebSocketHttpListenerDuplexStream CurrentStream
			{
				get
				{
					return this.m_CurrentStream;
				}
			}

			// Token: 0x14000073 RID: 115
			// (add) Token: 0x060042C0 RID: 17088 RVA: 0x0011720F File Offset: 0x0011540F
			// (remove) Token: 0x060042C1 RID: 17089 RVA: 0x00117218 File Offset: 0x00115418
			public event EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> Completed
			{
				add
				{
					this.m_Completed += value;
				}
				remove
				{
					this.m_Completed -= value;
				}
			}

			// Token: 0x060042C2 RID: 17090 RVA: 0x00117224 File Offset: 0x00115424
			protected virtual void OnCompleted(WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs e)
			{
				EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> completed = this.m_Completed;
				if (completed != null)
				{
					completed(e.m_CurrentStream, e);
				}
			}

			// Token: 0x060042C3 RID: 17091 RVA: 0x00117248 File Offset: 0x00115448
			public void SetShouldCloseOutput()
			{
				this.m_BufferList = null;
				this.m_Buffer = null;
				this.m_ShouldCloseOutput = true;
			}

			// Token: 0x060042C4 RID: 17092 RVA: 0x0011725F File Offset: 0x0011545F
			public void Dispose()
			{
				this.m_DisposeCalled = true;
				if (Interlocked.CompareExchange(ref this.m_Operating, 2, 0) != 0)
				{
					return;
				}
				if (!this.m_AllocateOverlappedOnDemand)
				{
					this.FreeOverlapped(false);
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x060042C5 RID: 17093 RVA: 0x00117290 File Offset: 0x00115490
			~HttpListenerAsyncEventArgs()
			{
				if (!this.m_AllocateOverlappedOnDemand)
				{
					this.FreeOverlapped(true);
				}
			}

			// Token: 0x060042C6 RID: 17094 RVA: 0x001172C8 File Offset: 0x001154C8
			private void InitializeOverlapped()
			{
				this.m_Overlapped = new Overlapped();
				this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), null));
			}

			// Token: 0x060042C7 RID: 17095 RVA: 0x001172F8 File Offset: 0x001154F8
			private void FreeOverlapped(bool checkForShutdown)
			{
				if (!checkForShutdown || !NclUtilities.HasShutdownStarted)
				{
					if (this.m_PtrNativeOverlapped != null && !this.m_PtrNativeOverlapped.IsInvalid)
					{
						this.m_PtrNativeOverlapped.Dispose();
					}
					if (this.m_DataChunksGCHandle.IsAllocated)
					{
						this.m_DataChunksGCHandle.Free();
						if (this.m_AllocateOverlappedOnDemand)
						{
							this.m_DataChunks = null;
						}
					}
				}
			}

			// Token: 0x060042C8 RID: 17096 RVA: 0x00117358 File Offset: 0x00115558
			internal void StartOperationCommon(WebSocketHttpListenerDuplexStream currentStream)
			{
				if (Interlocked.CompareExchange(ref this.m_Operating, 1, 0) == 0)
				{
					if (this.m_AllocateOverlappedOnDemand)
					{
						this.InitializeOverlapped();
					}
					else
					{
						this.NativeOverlapped.ReinitializeNativeOverlapped();
					}
					this.m_Exception = null;
					this.m_BytesTransferred = 0;
					return;
				}
				if (this.m_DisposeCalled)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				throw new InvalidOperationException();
			}

			// Token: 0x060042C9 RID: 17097 RVA: 0x001173BC File Offset: 0x001155BC
			internal void StartOperationReceive()
			{
				this.m_CompletedOperation = WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Receive;
			}

			// Token: 0x060042CA RID: 17098 RVA: 0x001173C5 File Offset: 0x001155C5
			internal void StartOperationSend()
			{
				this.UpdateDataChunk();
				this.m_CompletedOperation = WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Send;
			}

			// Token: 0x060042CB RID: 17099 RVA: 0x001173D4 File Offset: 0x001155D4
			public void SetBuffer(byte[] buffer, int offset, int count)
			{
				this.m_Buffer = buffer;
				this.m_Offset = offset;
				this.m_Count = count;
			}

			// Token: 0x060042CC RID: 17100 RVA: 0x001173EC File Offset: 0x001155EC
			private void UpdateDataChunk()
			{
				if (this.m_DataChunks == null)
				{
					this.m_DataChunks = new UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK[2];
					this.m_DataChunksGCHandle = GCHandle.Alloc(this.m_DataChunks, GCHandleType.Pinned);
					this.m_DataChunks[0] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
					this.m_DataChunks[0].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
					this.m_DataChunks[1] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
					this.m_DataChunks[1].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
				}
				if (this.m_Buffer != null)
				{
					this.UpdateDataChunk(0, this.m_Buffer, this.m_Offset, this.m_Count);
					this.UpdateDataChunk(1, null, 0, 0);
					this.m_DataChunkCount = 1;
					return;
				}
				if (this.m_BufferList != null)
				{
					this.UpdateDataChunk(0, this.m_BufferList[0].Array, this.m_BufferList[0].Offset, this.m_BufferList[0].Count);
					this.UpdateDataChunk(1, this.m_BufferList[1].Array, this.m_BufferList[1].Offset, this.m_BufferList[1].Count);
					this.m_DataChunkCount = 2;
					return;
				}
				this.m_DataChunks = null;
			}

			// Token: 0x060042CD RID: 17101 RVA: 0x0011753C File Offset: 0x0011573C
			private unsafe void UpdateDataChunk(int index, byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					this.m_DataChunks[index].pBuffer = null;
					this.m_DataChunks[index].BufferLength = 0U;
					return;
				}
				if (this.m_WebSocket.InternalBuffer.IsInternalBuffer(buffer, offset, count))
				{
					this.m_DataChunks[index].pBuffer = (byte*)((void*)this.m_WebSocket.InternalBuffer.ToIntPtr(offset));
				}
				else
				{
					this.m_DataChunks[index].pBuffer = (byte*)((void*)this.m_WebSocket.InternalBuffer.ConvertPinnedSendPayloadToNative(buffer, offset, count));
				}
				this.m_DataChunks[index].BufferLength = (uint)count;
			}

			// Token: 0x060042CE RID: 17102 RVA: 0x001175EE File Offset: 0x001157EE
			internal void Complete()
			{
				if (this.m_AllocateOverlappedOnDemand)
				{
					this.FreeOverlapped(false);
					Interlocked.Exchange(ref this.m_Operating, 0);
				}
				else
				{
					this.m_Operating = 0;
				}
				if (this.m_DisposeCalled)
				{
					this.Dispose();
				}
			}

			// Token: 0x060042CF RID: 17103 RVA: 0x00117623 File Offset: 0x00115823
			private void SetResults(Exception exception, int bytesTransferred)
			{
				this.m_Exception = exception;
				this.m_BytesTransferred = bytesTransferred;
			}

			// Token: 0x060042D0 RID: 17104 RVA: 0x00117634 File Offset: 0x00115834
			internal void FinishOperationFailure(Exception exception, bool syncCompletion)
			{
				this.SetResults(exception, 0);
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.PrintError(Logging.WebSockets, this.m_CurrentStream, (this.m_CompletedOperation == WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Receive) ? "ReadAsyncFast" : "WriteAsyncFast", exception.ToString());
				}
				this.Complete();
				this.OnCompleted(this);
			}

			// Token: 0x060042D1 RID: 17105 RVA: 0x00117688 File Offset: 0x00115888
			internal void FinishOperationSuccess(int bytesTransferred, bool syncCompletion)
			{
				this.SetResults(null, bytesTransferred);
				if (WebSocketBase.LoggingEnabled)
				{
					if (this.m_Buffer != null)
					{
						Logging.Dump(Logging.WebSockets, this.m_CurrentStream, (this.m_CompletedOperation == WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Receive) ? "ReadAsyncFast" : "WriteAsyncFast", this.m_Buffer, this.m_Offset, bytesTransferred);
					}
					else
					{
						if (this.m_BufferList != null)
						{
							using (IEnumerator<ArraySegment<byte>> enumerator = this.BufferList.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ArraySegment<byte> arraySegment = enumerator.Current;
									Logging.Dump(Logging.WebSockets, this, "WriteAsyncFast", arraySegment.Array, arraySegment.Offset, arraySegment.Count);
								}
								goto IL_EA;
							}
						}
						Logging.PrintLine(Logging.WebSockets, TraceEventType.Verbose, 0, string.Format(CultureInfo.InvariantCulture, "Output channel closed for {0}#{1}", new object[]
						{
							this.m_CurrentStream.GetType().Name,
							ValidationHelper.HashString(this.m_CurrentStream)
						}));
					}
				}
				IL_EA:
				if (this.m_ShouldCloseOutput)
				{
					this.m_CurrentStream.m_OutputStream.SetClosedFlag();
				}
				this.Complete();
				this.OnCompleted(this);
			}

			// Token: 0x060042D2 RID: 17106 RVA: 0x001177B4 File Offset: 0x001159B4
			private unsafe void CompletionPortCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				if (errorCode == 0U || errorCode == 38U)
				{
					this.FinishOperationSuccess((int)numBytes, false);
					return;
				}
				this.FinishOperationFailure(new HttpListenerException((int)errorCode), false);
			}

			// Token: 0x04003316 RID: 13078
			private const int Free = 0;

			// Token: 0x04003317 RID: 13079
			private const int InProgress = 1;

			// Token: 0x04003318 RID: 13080
			private const int Disposed = 2;

			// Token: 0x04003319 RID: 13081
			private int m_Operating;

			// Token: 0x0400331A RID: 13082
			private bool m_DisposeCalled;

			// Token: 0x0400331B RID: 13083
			private SafeNativeOverlapped m_PtrNativeOverlapped;

			// Token: 0x0400331C RID: 13084
			private Overlapped m_Overlapped;

			// Token: 0x0400331E RID: 13086
			private byte[] m_Buffer;

			// Token: 0x0400331F RID: 13087
			private IList<ArraySegment<byte>> m_BufferList;

			// Token: 0x04003320 RID: 13088
			private int m_Count;

			// Token: 0x04003321 RID: 13089
			private int m_Offset;

			// Token: 0x04003322 RID: 13090
			private int m_BytesTransferred;

			// Token: 0x04003323 RID: 13091
			private WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation m_CompletedOperation;

			// Token: 0x04003324 RID: 13092
			private UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK[] m_DataChunks;

			// Token: 0x04003325 RID: 13093
			private GCHandle m_DataChunksGCHandle;

			// Token: 0x04003326 RID: 13094
			private ushort m_DataChunkCount;

			// Token: 0x04003327 RID: 13095
			private Exception m_Exception;

			// Token: 0x04003328 RID: 13096
			private bool m_ShouldCloseOutput;

			// Token: 0x04003329 RID: 13097
			private readonly WebSocketBase m_WebSocket;

			// Token: 0x0400332A RID: 13098
			private readonly WebSocketHttpListenerDuplexStream m_CurrentStream;

			// Token: 0x0400332B RID: 13099
			private readonly bool m_AllocateOverlappedOnDemand;

			// Token: 0x02000920 RID: 2336
			public enum HttpListenerAsyncOperation
			{
				// Token: 0x04003DA7 RID: 15783
				None,
				// Token: 0x04003DA8 RID: 15784
				Receive,
				// Token: 0x04003DA9 RID: 15785
				Send
			}
		}

		// Token: 0x02000783 RID: 1923
		private static class Methods
		{
			// Token: 0x0400332C RID: 13100
			public const string CloseNetworkConnectionAsync = "CloseNetworkConnectionAsync";

			// Token: 0x0400332D RID: 13101
			public const string OnCancel = "OnCancel";

			// Token: 0x0400332E RID: 13102
			public const string OnReadCompleted = "OnReadCompleted";

			// Token: 0x0400332F RID: 13103
			public const string OnWriteCompleted = "OnWriteCompleted";

			// Token: 0x04003330 RID: 13104
			public const string ReadAsyncFast = "ReadAsyncFast";

			// Token: 0x04003331 RID: 13105
			public const string ReadAsyncCore = "ReadAsyncCore";

			// Token: 0x04003332 RID: 13106
			public const string WriteAsyncFast = "WriteAsyncFast";

			// Token: 0x04003333 RID: 13107
			public const string WriteAsyncCore = "WriteAsyncCore";

			// Token: 0x04003334 RID: 13108
			public const string MultipleWriteAsyncCore = "MultipleWriteAsyncCore";
		}
	}
}
