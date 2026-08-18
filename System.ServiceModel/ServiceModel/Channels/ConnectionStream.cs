using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D2 RID: 2002
	internal class ConnectionStream : Stream
	{
		// Token: 0x06004B6A RID: 19306 RVA: 0x00113BC4 File Offset: 0x00111DC4
		public ConnectionStream(IConnection connection, IDefaultCommunicationTimeouts defaultTimeouts, TimeSpan openTimeout = default(TimeSpan), bool useOpenTimeout = false)
		{
			this.connection = connection;
			this.closeTimeout = defaultTimeouts.CloseTimeout;
			this.ReadTimeout = TimeoutHelper.ToMilliseconds(defaultTimeouts.ReceiveTimeout);
			this.WriteTimeout = TimeoutHelper.ToMilliseconds(defaultTimeouts.SendTimeout);
			this.immediate = true;
			if (useOpenTimeout && ServiceModelAppSettings.EnsureStreamUpgradeOpenTimeout)
			{
				this.connection = new TimeoutConnection(this.connection, openTimeout);
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004B6B RID: 19307 RVA: 0x00113C30 File Offset: 0x00111E30
		public IConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004B6C RID: 19308 RVA: 0x00113C38 File Offset: 0x00111E38
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004B6D RID: 19309 RVA: 0x00113C3B File Offset: 0x00111E3B
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004B6E RID: 19310 RVA: 0x00113C3E File Offset: 0x00111E3E
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x00113C41 File Offset: 0x00111E41
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x00113C44 File Offset: 0x00111E44
		// (set) Token: 0x06004B71 RID: 19313 RVA: 0x00113C4C File Offset: 0x00111E4C
		public TimeSpan CloseTimeout
		{
			get
			{
				return this.closeTimeout;
			}
			set
			{
				this.closeTimeout = value;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06004B72 RID: 19314 RVA: 0x00113C55 File Offset: 0x00111E55
		// (set) Token: 0x06004B73 RID: 19315 RVA: 0x00113C60 File Offset: 0x00111E60
		public override int ReadTimeout
		{
			get
			{
				return this.readTimeout;
			}
			set
			{
				if (value < -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
					{
						-1,
						int.MaxValue
					})));
				}
				this.readTimeout = value;
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x00113CB9 File Offset: 0x00111EB9
		// (set) Token: 0x06004B75 RID: 19317 RVA: 0x00113CC4 File Offset: 0x00111EC4
		public override int WriteTimeout
		{
			get
			{
				return this.writeTimeout;
			}
			set
			{
				if (value < -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeInRange", new object[]
					{
						-1,
						int.MaxValue
					})));
				}
				this.writeTimeout = value;
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x00113D1D File Offset: 0x00111F1D
		// (set) Token: 0x06004B77 RID: 19319 RVA: 0x00113D25 File Offset: 0x00111F25
		public bool Immediate
		{
			get
			{
				return this.immediate;
			}
			set
			{
				this.immediate = value;
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06004B78 RID: 19320 RVA: 0x00113D2E File Offset: 0x00111F2E
		public override long Length
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06004B79 RID: 19321 RVA: 0x00113D49 File Offset: 0x00111F49
		// (set) Token: 0x06004B7A RID: 19322 RVA: 0x00113D64 File Offset: 0x00111F64
		public override long Position
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06004B7B RID: 19323 RVA: 0x00113D7F File Offset: 0x00111F7F
		// (set) Token: 0x06004B7C RID: 19324 RVA: 0x00113D8C File Offset: 0x00111F8C
		public TraceEventType ExceptionEventType
		{
			get
			{
				return this.connection.ExceptionEventType;
			}
			set
			{
				this.connection.ExceptionEventType = value;
			}
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00113D9A File Offset: 0x00111F9A
		public void Abort()
		{
			this.connection.Abort();
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00113DA7 File Offset: 0x00111FA7
		public override void Close()
		{
			this.connection.Close(this.CloseTimeout, false);
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00113DBB File Offset: 0x00111FBB
		public override void Flush()
		{
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00113DBD File Offset: 0x00111FBD
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new ConnectionStream.WriteAsyncResult(this.connection, buffer, offset, count, this.Immediate, TimeoutHelper.FromMilliseconds(this.WriteTimeout), callback, state);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00113DE2 File Offset: 0x00111FE2
		public override void EndWrite(IAsyncResult asyncResult)
		{
			ConnectionStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00113DEA File Offset: 0x00111FEA
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.connection.Write(buffer, offset, count, this.Immediate, TimeoutHelper.FromMilliseconds(this.WriteTimeout));
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x00113E0B File Offset: 0x0011200B
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new ConnectionStream.ReadAsyncResult(this.connection, buffer, offset, count, TimeoutHelper.FromMilliseconds(this.ReadTimeout), callback, state);
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x00113E2A File Offset: 0x0011202A
		public override int EndRead(IAsyncResult asyncResult)
		{
			return ConnectionStream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x00113E32 File Offset: 0x00112032
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Read(buffer, offset, count, TimeoutHelper.FromMilliseconds(this.ReadTimeout));
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x00113E48 File Offset: 0x00112048
		protected int Read(byte[] buffer, int offset, int count, TimeSpan timeout)
		{
			return this.connection.Read(buffer, offset, count, timeout);
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00113E5A File Offset: 0x0011205A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x00113E75 File Offset: 0x00112075
		public override void SetLength(long value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SeekNotSupported")));
		}

		// Token: 0x06004B89 RID: 19337 RVA: 0x00113E90 File Offset: 0x00112090
		public void Shutdown(TimeSpan timeout)
		{
			this.connection.Shutdown(timeout);
		}

		// Token: 0x06004B8A RID: 19338 RVA: 0x00113E9E File Offset: 0x0011209E
		public IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
		{
			return this.connection.BeginValidate(uri, callback, state);
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x00113EAE File Offset: 0x001120AE
		public bool EndValidate(IAsyncResult result)
		{
			return this.connection.EndValidate(result);
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x00113EBC File Offset: 0x001120BC
		public void CompleteOpen()
		{
			TimeoutConnection timeoutConnection = this.connection as TimeoutConnection;
			if (timeoutConnection != null)
			{
				this.connection = timeoutConnection.InnerConnection;
			}
		}

		// Token: 0x04002F44 RID: 12100
		private TimeSpan closeTimeout;

		// Token: 0x04002F45 RID: 12101
		private int readTimeout;

		// Token: 0x04002F46 RID: 12102
		private int writeTimeout;

		// Token: 0x04002F47 RID: 12103
		private IConnection connection;

		// Token: 0x04002F48 RID: 12104
		private bool immediate;

		// Token: 0x02000CFC RID: 3324
		private abstract class IOAsyncResult : AsyncResult
		{
			// Token: 0x06007AAF RID: 31407 RVA: 0x001C908D File Offset: 0x001C728D
			protected IOAsyncResult(IConnection connection, AsyncCallback callback, object state) : base(callback, state)
			{
				this.connection = connection;
			}

			// Token: 0x06007AB0 RID: 31408 RVA: 0x001C909E File Offset: 0x001C729E
			protected WaitCallback GetWaitCompletion()
			{
				if (ConnectionStream.IOAsyncResult.onAsyncIOComplete == null)
				{
					ConnectionStream.IOAsyncResult.onAsyncIOComplete = new WaitCallback(ConnectionStream.IOAsyncResult.OnAsyncIOComplete);
				}
				return ConnectionStream.IOAsyncResult.onAsyncIOComplete;
			}

			// Token: 0x06007AB1 RID: 31409
			protected abstract void HandleIO(IConnection connection);

			// Token: 0x06007AB2 RID: 31410 RVA: 0x001C90C0 File Offset: 0x001C72C0
			private static void OnAsyncIOComplete(object state)
			{
				ConnectionStream.IOAsyncResult ioasyncResult = (ConnectionStream.IOAsyncResult)state;
				Exception exception = null;
				try
				{
					ioasyncResult.HandleIO(ioasyncResult.connection);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				ioasyncResult.Complete(false, exception);
			}

			// Token: 0x04004627 RID: 17959
			private static WaitCallback onAsyncIOComplete;

			// Token: 0x04004628 RID: 17960
			private IConnection connection;
		}

		// Token: 0x02000CFD RID: 3325
		private sealed class ReadAsyncResult : ConnectionStream.IOAsyncResult
		{
			// Token: 0x06007AB3 RID: 31411 RVA: 0x001C910C File Offset: 0x001C730C
			public ReadAsyncResult(IConnection connection, byte[] buffer, int offset, int count, TimeSpan timeout, AsyncCallback callback, object state) : base(connection, callback, state)
			{
				this.buffer = buffer;
				this.offset = offset;
				AsyncCompletionResult asyncCompletionResult = connection.BeginRead(0, Math.Min(count, connection.AsyncReadBufferSize), timeout, base.GetWaitCompletion(), this);
				if (asyncCompletionResult == AsyncCompletionResult.Completed)
				{
					this.HandleIO(connection);
					base.Complete(true);
				}
			}

			// Token: 0x06007AB4 RID: 31412 RVA: 0x001C9162 File Offset: 0x001C7362
			protected override void HandleIO(IConnection connection)
			{
				this.bytesRead = connection.EndRead();
				Buffer.BlockCopy(connection.AsyncReadBuffer, 0, this.buffer, this.offset, this.bytesRead);
			}

			// Token: 0x06007AB5 RID: 31413 RVA: 0x001C9190 File Offset: 0x001C7390
			public static int End(IAsyncResult result)
			{
				ConnectionStream.ReadAsyncResult readAsyncResult = AsyncResult.End<ConnectionStream.ReadAsyncResult>(result);
				return readAsyncResult.bytesRead;
			}

			// Token: 0x04004629 RID: 17961
			private int bytesRead;

			// Token: 0x0400462A RID: 17962
			private byte[] buffer;

			// Token: 0x0400462B RID: 17963
			private int offset;
		}

		// Token: 0x02000CFE RID: 3326
		private sealed class WriteAsyncResult : ConnectionStream.IOAsyncResult
		{
			// Token: 0x06007AB6 RID: 31414 RVA: 0x001C91AC File Offset: 0x001C73AC
			public WriteAsyncResult(IConnection connection, byte[] buffer, int offset, int count, bool immediate, TimeSpan timeout, AsyncCallback callback, object state) : base(connection, callback, state)
			{
				AsyncCompletionResult asyncCompletionResult = connection.BeginWrite(buffer, offset, count, immediate, timeout, base.GetWaitCompletion(), this);
				if (asyncCompletionResult == AsyncCompletionResult.Completed)
				{
					this.HandleIO(connection);
					base.Complete(true);
				}
			}

			// Token: 0x06007AB7 RID: 31415 RVA: 0x001C91EC File Offset: 0x001C73EC
			protected override void HandleIO(IConnection connection)
			{
				connection.EndWrite();
			}

			// Token: 0x06007AB8 RID: 31416 RVA: 0x001C91F4 File Offset: 0x001C73F4
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ConnectionStream.WriteAsyncResult>(result);
			}
		}
	}
}
