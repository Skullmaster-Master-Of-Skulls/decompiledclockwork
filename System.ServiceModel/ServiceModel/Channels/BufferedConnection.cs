using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C9 RID: 1993
	internal class BufferedConnection : DelegatingConnection
	{
		// Token: 0x06004B13 RID: 19219 RVA: 0x00113099 File Offset: 0x00111299
		public BufferedConnection(IConnection connection, TimeSpan flushTimeout, int writeBufferSize) : base(connection)
		{
			this.flushTimeout = Ticks.FromTimeSpan(flushTimeout);
			this.writeBufferSize = writeBufferSize;
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x001130B5 File Offset: 0x001112B5
		private object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x001130B8 File Offset: 0x001112B8
		public override void Close(TimeSpan timeout, bool asyncAndLinger)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.Flush(timeoutHelper.RemainingTime());
			base.Close(timeoutHelper.RemainingTime(), asyncAndLinger);
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x001130E8 File Offset: 0x001112E8
		private void CancelFlushTimer()
		{
			if (this.flushTimer != null)
			{
				this.flushTimer.Cancel();
				this.pendingTimeout = TimeSpan.Zero;
			}
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x0011310C File Offset: 0x0011130C
		private void Flush(TimeSpan timeout)
		{
			this.ThrowPendingWriteException();
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.FlushCore(timeout);
			}
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x00113154 File Offset: 0x00111354
		private void FlushCore(TimeSpan timeout)
		{
			if (this.pendingWriteSize > 0)
			{
				ThreadTrace.Trace("BC:Flush");
				base.Connection.Write(this.writeBuffer, 0, this.pendingWriteSize, false, timeout);
				this.pendingWriteSize = 0;
			}
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x0011318C File Offset: 0x0011138C
		private void OnFlushTimer(object state)
		{
			ThreadTrace.Trace("BC:Flush timer");
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				try
				{
					this.FlushCore(this.pendingTimeout);
					this.pendingTimeout = TimeSpan.Zero;
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.pendingWriteException = exception;
					this.CancelFlushTimer();
				}
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x00113210 File Offset: 0x00111410
		private void SetFlushTimer()
		{
			if (this.flushTimer == null)
			{
				int maxSkewInMilliseconds = Ticks.ToMilliseconds(Math.Min(this.flushTimeout / 10L, Ticks.FromMilliseconds(100)));
				this.flushTimer = new IOThreadTimer(new Action<object>(this.OnFlushTimer), null, true, maxSkewInMilliseconds);
			}
			this.flushTimer.Set(Ticks.ToTimeSpan(this.flushTimeout));
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x00113274 File Offset: 0x00111474
		public override void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			if (size <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("size", size, SR.GetString("ValueMustBePositive")));
			}
			this.ThrowPendingWriteException();
			if (immediate || this.flushTimeout == 0L)
			{
				ThreadTrace.Trace("BC:Write now");
				this.WriteNow(buffer, offset, size, timeout, bufferManager);
			}
			else
			{
				ThreadTrace.Trace("BC:Write later");
				this.WriteLater(buffer, offset, size, timeout);
				bufferManager.ReturnBuffer(buffer);
			}
			ThreadTrace.Trace("BC:Write done");
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x001132FC File Offset: 0x001114FC
		public override void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			if (size <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("size", size, SR.GetString("ValueMustBePositive")));
			}
			this.ThrowPendingWriteException();
			if (immediate || this.flushTimeout == 0L)
			{
				ThreadTrace.Trace("BC:Write now");
				this.WriteNow(buffer, offset, size, timeout);
			}
			else
			{
				ThreadTrace.Trace("BC:Write later");
				this.WriteLater(buffer, offset, size, timeout);
			}
			ThreadTrace.Trace("BC:Write done");
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x0011337A File Offset: 0x0011157A
		private void WriteNow(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			this.WriteNow(buffer, offset, size, timeout, null);
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x00113388 File Offset: 0x00111588
		private void WriteNow(byte[] buffer, int offset, int size, TimeSpan timeout, BufferManager bufferManager)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.pendingWriteSize > 0)
				{
					int num = this.writeBufferSize - this.pendingWriteSize;
					this.CancelFlushTimer();
					if (size <= num)
					{
						Buffer.BlockCopy(buffer, offset, this.writeBuffer, this.pendingWriteSize, size);
						if (bufferManager != null)
						{
							bufferManager.ReturnBuffer(buffer);
						}
						this.pendingWriteSize += size;
						this.FlushCore(timeout);
						return;
					}
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.FlushCore(timeoutHelper.RemainingTime());
					timeout = timeoutHelper.RemainingTime();
				}
				if (bufferManager == null)
				{
					base.Connection.Write(buffer, offset, size, true, timeout);
				}
				else
				{
					base.Connection.Write(buffer, offset, size, true, timeout, bufferManager);
				}
			}
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x00113464 File Offset: 0x00111664
		private void WriteLater(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				bool flag2 = this.pendingWriteSize == 0;
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				while (size > 0)
				{
					if (size >= this.writeBufferSize && this.pendingWriteSize == 0)
					{
						base.Connection.Write(buffer, offset, size, false, timeoutHelper.RemainingTime());
						size = 0;
					}
					else
					{
						if (this.writeBuffer == null)
						{
							this.writeBuffer = DiagnosticUtility.Utility.AllocateByteArray(this.writeBufferSize);
						}
						int num = this.writeBufferSize - this.pendingWriteSize;
						int num2 = size;
						if (num2 > num)
						{
							num2 = num;
						}
						Buffer.BlockCopy(buffer, offset, this.writeBuffer, this.pendingWriteSize, num2);
						this.pendingWriteSize += num2;
						if (this.pendingWriteSize == this.writeBufferSize)
						{
							this.FlushCore(timeoutHelper.RemainingTime());
							flag2 = true;
						}
						size -= num2;
						offset += num2;
					}
				}
				if (this.pendingWriteSize > 0)
				{
					if (flag2)
					{
						this.SetFlushTimer();
						this.pendingTimeout = TimeoutHelper.Add(this.pendingTimeout, timeoutHelper.RemainingTime());
					}
				}
				else
				{
					this.CancelFlushTimer();
				}
			}
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x001135B4 File Offset: 0x001117B4
		public override AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			ThreadTrace.Trace("BC:BeginWrite");
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.Flush(timeoutHelper.RemainingTime());
			return base.BeginWrite(buffer, offset, size, immediate, timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x001135F7 File Offset: 0x001117F7
		public override void EndWrite()
		{
			ThreadTrace.Trace("BC:EndWrite");
			base.EndWrite();
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x0011360C File Offset: 0x0011180C
		public override void Shutdown(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.Flush(timeoutHelper.RemainingTime());
			base.Shutdown(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x0011363C File Offset: 0x0011183C
		private void ThrowPendingWriteException()
		{
			if (this.pendingWriteException != null)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (this.pendingWriteException != null)
					{
						Exception exception = this.pendingWriteException;
						this.pendingWriteException = null;
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
				}
			}
		}

		// Token: 0x04002F30 RID: 12080
		private byte[] writeBuffer;

		// Token: 0x04002F31 RID: 12081
		private int writeBufferSize;

		// Token: 0x04002F32 RID: 12082
		private int pendingWriteSize;

		// Token: 0x04002F33 RID: 12083
		private Exception pendingWriteException;

		// Token: 0x04002F34 RID: 12084
		private IOThreadTimer flushTimer;

		// Token: 0x04002F35 RID: 12085
		private long flushTimeout;

		// Token: 0x04002F36 RID: 12086
		private TimeSpan pendingTimeout;

		// Token: 0x04002F37 RID: 12087
		private const int maxFlushSkew = 100;
	}
}
