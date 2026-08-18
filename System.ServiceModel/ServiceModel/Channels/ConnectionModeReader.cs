using System;
using System.Diagnostics;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007DB RID: 2011
	internal sealed class ConnectionModeReader : InitialServerConnectionReader
	{
		// Token: 0x06004BDD RID: 19421 RVA: 0x00115415 File Offset: 0x00113615
		public ConnectionModeReader(IConnection connection, ConnectionModeCallback callback, ConnectionClosedCallback closedCallback) : base(connection, closedCallback)
		{
			this.callback = callback;
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x00115426 File Offset: 0x00113626
		public int BufferOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x0011542E File Offset: 0x0011362E
		public int BufferSize
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x00115436 File Offset: 0x00113636
		public long StreamPosition
		{
			get
			{
				return this.decoder.StreamPosition;
			}
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x00115443 File Offset: 0x00113643
		public TimeSpan GetRemainingTimeout()
		{
			return this.receiveTimeoutHelper.RemainingTime();
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x00115450 File Offset: 0x00113650
		private void Complete(Exception e)
		{
			this.readException = e;
			this.Complete();
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0011545F File Offset: 0x0011365F
		private void Complete()
		{
			this.callback(this);
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00115470 File Offset: 0x00113670
		private bool ContinueReading()
		{
			for (;;)
			{
				if (this.size == 0)
				{
					if (ConnectionModeReader.readCallback == null)
					{
						ConnectionModeReader.readCallback = new WaitCallback(ConnectionModeReader.ReadCallback);
					}
					if (base.Connection.BeginRead(0, base.Connection.AsyncReadBufferSize, this.GetRemainingTimeout(), ConnectionModeReader.readCallback, this) == AsyncCompletionResult.Queued)
					{
						return false;
					}
					if (!this.GetReadResult())
					{
						break;
					}
				}
				do
				{
					int num;
					try
					{
						num = this.decoder.Decode(this.buffer, this.offset, this.size);
					}
					catch (CommunicationException exception)
					{
						string faultString;
						if (FramingEncodingString.TryGetFaultString(exception, out faultString))
						{
							byte[] drainBuffer = new byte[128];
							InitialServerConnectionReader.SendFault(base.Connection, faultString, drainBuffer, this.GetRemainingTimeout(), base.MaxViaSize + base.MaxContentTypeSize);
							base.Close(this.GetRemainingTimeout());
						}
						throw;
					}
					if (num > 0)
					{
						this.offset += num;
						this.size -= num;
					}
					if (this.decoder.CurrentState == ServerModeDecoder.State.Done)
					{
						return true;
					}
				}
				while (this.size != 0);
			}
			return false;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00115588 File Offset: 0x00113788
		private static void ReadCallback(object state)
		{
			ConnectionModeReader connectionModeReader = (ConnectionModeReader)state;
			bool flag = false;
			Exception e = null;
			try
			{
				if (connectionModeReader.GetReadResult())
				{
					flag = connectionModeReader.ContinueReading();
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				flag = true;
				e = ex;
			}
			if (flag)
			{
				connectionModeReader.Complete(e);
			}
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x001155DC File Offset: 0x001137DC
		private bool GetReadResult()
		{
			this.offset = 0;
			this.size = base.Connection.EndRead();
			if (this.size != 0)
			{
				base.Connection.ExceptionEventType = TraceEventType.Error;
				if (this.buffer == null)
				{
					this.buffer = base.Connection.AsyncReadBuffer;
				}
				return true;
			}
			if (this.decoder.StreamPosition == 0L)
			{
				base.Close(this.GetRemainingTimeout());
				return false;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.decoder.CreatePrematureEOFException());
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00115660 File Offset: 0x00113860
		public FramingMode GetConnectionMode()
		{
			if (this.readException != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(this.readException, base.Connection.ExceptionEventType);
			}
			return this.decoder.Mode;
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x00115694 File Offset: 0x00113894
		public void StartReading(TimeSpan receiveTimeout, Action connectionDequeuedCallback)
		{
			this.decoder = new ServerModeDecoder();
			this.receiveTimeoutHelper = new TimeoutHelper(receiveTimeout);
			base.ConnectionDequeuedCallback = connectionDequeuedCallback;
			bool flag = false;
			Exception e = null;
			try
			{
				flag = this.ContinueReading();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				flag = true;
				e = ex;
			}
			if (flag)
			{
				this.Complete(e);
			}
		}

		// Token: 0x04002F72 RID: 12146
		private Exception readException;

		// Token: 0x04002F73 RID: 12147
		private ServerModeDecoder decoder;

		// Token: 0x04002F74 RID: 12148
		private byte[] buffer;

		// Token: 0x04002F75 RID: 12149
		private int offset;

		// Token: 0x04002F76 RID: 12150
		private int size;

		// Token: 0x04002F77 RID: 12151
		private ConnectionModeCallback callback;

		// Token: 0x04002F78 RID: 12152
		private static WaitCallback readCallback;

		// Token: 0x04002F79 RID: 12153
		private TimeoutHelper receiveTimeoutHelper;
	}
}
