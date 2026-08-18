using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000815 RID: 2069
	internal abstract class SessionConnectionReader : IMessageSource
	{
		// Token: 0x06004D4F RID: 19791 RVA: 0x0011A42C File Offset: 0x0011862C
		protected SessionConnectionReader(IConnection connection, IConnection rawConnection, int offset, int size, SecurityMessageProperty security)
		{
			this.offset = offset;
			this.size = size;
			if (size > 0)
			{
				this.buffer = connection.AsyncReadBuffer;
			}
			this.connection = connection;
			this.rawConnection = rawConnection;
			this.onAsyncReadComplete = new WaitCallback(this.OnAsyncReadComplete);
			this.security = security;
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0011A488 File Offset: 0x00118688
		private Message DecodeMessage(TimeSpan timeout)
		{
			if (DiagnosticUtility.ShouldUseActivity && ServiceModelActivity.Current != null && ServiceModelActivity.Current.ActivityType == ActivityType.ProcessAction)
			{
				ServiceModelActivity.Current.Resume();
			}
			if (!this.readIntoEnvelopeBuffer)
			{
				return this.DecodeMessage(this.buffer, ref this.offset, ref this.size, ref this.isAtEOF, timeout);
			}
			int num = this.envelopeOffset;
			return this.DecodeMessage(this.envelopeBuffer, ref num, ref this.size, ref this.isAtEOF, timeout);
		}

		// Token: 0x06004D51 RID: 19793
		protected abstract Message DecodeMessage(byte[] buffer, ref int offset, ref int size, ref bool isAtEof, TimeSpan timeout);

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x0011A504 File Offset: 0x00118704
		// (set) Token: 0x06004D53 RID: 19795 RVA: 0x0011A50C File Offset: 0x0011870C
		protected byte[] EnvelopeBuffer
		{
			get
			{
				return this.envelopeBuffer;
			}
			set
			{
				this.envelopeBuffer = value;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x0011A515 File Offset: 0x00118715
		// (set) Token: 0x06004D55 RID: 19797 RVA: 0x0011A51D File Offset: 0x0011871D
		protected int EnvelopeOffset
		{
			get
			{
				return this.envelopeOffset;
			}
			set
			{
				this.envelopeOffset = value;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x0011A526 File Offset: 0x00118726
		// (set) Token: 0x06004D57 RID: 19799 RVA: 0x0011A52E File Offset: 0x0011872E
		protected int EnvelopeSize
		{
			get
			{
				return this.envelopeSize;
			}
			set
			{
				this.envelopeSize = value;
			}
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0011A538 File Offset: 0x00118738
		public IConnection GetRawConnection()
		{
			IConnection connection = null;
			if (this.rawConnection != null)
			{
				connection = this.rawConnection;
				this.rawConnection = null;
				if (this.size > 0)
				{
					PreReadConnection preReadConnection = connection as PreReadConnection;
					if (preReadConnection != null)
					{
						preReadConnection.AddPreReadData(this.buffer, this.offset, this.size);
					}
					else
					{
						connection = new PreReadConnection(connection, this.buffer, this.offset, this.size);
					}
				}
			}
			return connection;
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0011A5A4 File Offset: 0x001187A4
		public AsyncReceiveResult BeginReceive(TimeSpan timeout, WaitCallback callback, object state)
		{
			if (this.pendingMessage != null || this.pendingException != null)
			{
				return AsyncReceiveResult.Completed;
			}
			this.readTimeoutHelper = new TimeoutHelper(timeout);
			while (!this.isAtEOF)
			{
				if (this.size > 0)
				{
					this.pendingMessage = this.DecodeMessage(this.readTimeoutHelper.RemainingTime());
					if (this.pendingMessage != null)
					{
						this.PrepareMessage(this.pendingMessage);
						return AsyncReceiveResult.Completed;
					}
					if (this.isAtEOF)
					{
						return AsyncReceiveResult.Completed;
					}
				}
				if (this.size != 0)
				{
					throw Fx.AssertAndThrow("BeginReceive: DecodeMessage() should consume the outstanding buffer or return a message.");
				}
				if (!this.usingAsyncReadBuffer)
				{
					this.buffer = this.connection.AsyncReadBuffer;
					this.usingAsyncReadBuffer = true;
				}
				this.pendingCallback = callback;
				this.pendingCallbackState = state;
				bool flag = true;
				AsyncCompletionResult asyncCompletionResult;
				try
				{
					asyncCompletionResult = this.connection.BeginRead(0, this.buffer.Length, this.readTimeoutHelper.RemainingTime(), this.onAsyncReadComplete, null);
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.pendingCallback = null;
						this.pendingCallbackState = null;
					}
				}
				if (asyncCompletionResult == AsyncCompletionResult.Queued)
				{
					return AsyncReceiveResult.Pending;
				}
				this.pendingCallback = null;
				this.pendingCallbackState = null;
				int bytesRead = this.connection.EndRead();
				this.HandleReadComplete(bytesRead, false);
			}
			return AsyncReceiveResult.Completed;
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0011A6D4 File Offset: 0x001188D4
		public Message Receive(TimeSpan timeout)
		{
			Message message = this.GetPendingMessage();
			if (message != null)
			{
				return message;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			while (!this.isAtEOF)
			{
				if (this.size > 0)
				{
					message = this.DecodeMessage(timeoutHelper.RemainingTime());
					if (message != null)
					{
						this.PrepareMessage(message);
						return message;
					}
					if (this.isAtEOF)
					{
						return null;
					}
				}
				if (this.size != 0)
				{
					throw Fx.AssertAndThrow("Receive: DecodeMessage() should consume the outstanding buffer or return a message.");
				}
				if (this.buffer == null)
				{
					this.buffer = DiagnosticUtility.Utility.AllocateByteArray(this.connection.AsyncReadBufferSize);
				}
				if (this.EnvelopeBuffer != null && this.EnvelopeSize - this.EnvelopeOffset >= this.buffer.Length)
				{
					int bytesRead = this.connection.Read(this.EnvelopeBuffer, this.EnvelopeOffset, this.buffer.Length, timeoutHelper.RemainingTime());
					this.HandleReadComplete(bytesRead, true);
				}
				else
				{
					int bytesRead = this.connection.Read(this.buffer, 0, this.buffer.Length, timeoutHelper.RemainingTime());
					this.HandleReadComplete(bytesRead, false);
				}
			}
			return null;
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0011A7E3 File Offset: 0x001189E3
		public Message EndReceive()
		{
			return this.GetPendingMessage();
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x0011A7EC File Offset: 0x001189EC
		private Message GetPendingMessage()
		{
			if (this.pendingException != null)
			{
				Exception exception = this.pendingException;
				this.pendingException = null;
				throw TraceUtility.ThrowHelperError(exception, this.pendingMessage);
			}
			if (this.pendingMessage != null)
			{
				Message result = this.pendingMessage;
				this.pendingMessage = null;
				return result;
			}
			return null;
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x0011A838 File Offset: 0x00118A38
		public AsyncReceiveResult BeginWaitForMessage(TimeSpan timeout, WaitCallback callback, object state)
		{
			AsyncReceiveResult result;
			try
			{
				result = this.BeginReceive(timeout, callback, state);
			}
			catch (TimeoutException ex)
			{
				this.pendingException = ex;
				result = AsyncReceiveResult.Completed;
			}
			return result;
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x0011A870 File Offset: 0x00118A70
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

		// Token: 0x06004D5F RID: 19807 RVA: 0x0011A8C0 File Offset: 0x00118AC0
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

		// Token: 0x06004D60 RID: 19808
		protected abstract void EnsureDecoderAtEof();

		// Token: 0x06004D61 RID: 19809 RVA: 0x0011A910 File Offset: 0x00118B10
		private void HandleReadComplete(int bytesRead, bool readIntoEnvelopeBuffer)
		{
			this.readIntoEnvelopeBuffer = readIntoEnvelopeBuffer;
			if (bytesRead == 0)
			{
				this.EnsureDecoderAtEof();
				this.isAtEOF = true;
				return;
			}
			this.offset = 0;
			this.size = bytesRead;
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x0011A938 File Offset: 0x00118B38
		private void OnAsyncReadComplete(object state)
		{
			try
			{
				Message message;
				for (;;)
				{
					int bytesRead = this.connection.EndRead();
					this.HandleReadComplete(bytesRead, false);
					if (this.isAtEOF)
					{
						goto IL_89;
					}
					message = this.DecodeMessage(this.readTimeoutHelper.RemainingTime());
					if (message != null)
					{
						break;
					}
					if (this.isAtEOF)
					{
						goto IL_89;
					}
					if (this.size != 0)
					{
						goto Block_4;
					}
					if (this.connection.BeginRead(0, this.buffer.Length, this.readTimeoutHelper.RemainingTime(), this.onAsyncReadComplete, null) == AsyncCompletionResult.Queued)
					{
						goto Block_5;
					}
				}
				this.PrepareMessage(message);
				this.pendingMessage = message;
				goto IL_89;
				Block_4:
				throw Fx.AssertAndThrow("OnAsyncReadComplete: DecodeMessage() should consume the outstanding buffer or return a message.");
				Block_5:
				return;
				IL_89:;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.pendingException = exception;
			}
			WaitCallback waitCallback = this.pendingCallback;
			object state2 = this.pendingCallbackState;
			this.pendingCallback = null;
			this.pendingCallbackState = null;
			waitCallback(state2);
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x0011AA1C File Offset: 0x00118C1C
		protected virtual void PrepareMessage(Message message)
		{
			if (this.security != null)
			{
				message.Properties.Security = (SecurityMessageProperty)this.security.CreateCopy();
			}
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x0011AA44 File Offset: 0x00118C44
		protected void SendFault(string faultString, TimeSpan timeout)
		{
			byte[] drainBuffer = new byte[128];
			InitialServerConnectionReader.SendFault(this.connection, faultString, drainBuffer, timeout, 65536);
		}

		// Token: 0x0400305C RID: 12380
		private bool isAtEOF;

		// Token: 0x0400305D RID: 12381
		private bool usingAsyncReadBuffer;

		// Token: 0x0400305E RID: 12382
		private IConnection connection;

		// Token: 0x0400305F RID: 12383
		private byte[] buffer;

		// Token: 0x04003060 RID: 12384
		private int offset;

		// Token: 0x04003061 RID: 12385
		private int size;

		// Token: 0x04003062 RID: 12386
		private byte[] envelopeBuffer;

		// Token: 0x04003063 RID: 12387
		private int envelopeOffset;

		// Token: 0x04003064 RID: 12388
		private int envelopeSize;

		// Token: 0x04003065 RID: 12389
		private bool readIntoEnvelopeBuffer;

		// Token: 0x04003066 RID: 12390
		private WaitCallback onAsyncReadComplete;

		// Token: 0x04003067 RID: 12391
		private Message pendingMessage;

		// Token: 0x04003068 RID: 12392
		private Exception pendingException;

		// Token: 0x04003069 RID: 12393
		private WaitCallback pendingCallback;

		// Token: 0x0400306A RID: 12394
		private object pendingCallbackState;

		// Token: 0x0400306B RID: 12395
		private SecurityMessageProperty security;

		// Token: 0x0400306C RID: 12396
		private TimeoutHelper readTimeoutHelper;

		// Token: 0x0400306D RID: 12397
		private IConnection rawConnection;
	}
}
