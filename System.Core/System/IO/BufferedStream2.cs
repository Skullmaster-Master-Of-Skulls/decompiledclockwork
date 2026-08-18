using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200009F RID: 159
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	internal abstract class BufferedStream2 : Stream
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", SR.GetString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			if (this._writePos == 0)
			{
				if (!this.CanWrite)
				{
					__Error.WriteNotSupported();
				}
				if (this._readPos < this._readLen)
				{
					this.FlushRead();
				}
				this._readPos = 0;
				this._readLen = 0;
			}
			if (count == 0)
			{
				return;
			}
			int num2;
			for (;;)
			{
				if (this._writePos > this.bufferSize)
				{
					Thread.Sleep(1);
				}
				else
				{
					if (this._writePos == 0 && count >= this.bufferSize)
					{
						break;
					}
					Thread.BeginCriticalRegion();
					Interlocked.Increment(ref this._pendingBufferCopy);
					int num = Interlocked.Add(ref this._writePos, count);
					num2 = num - count;
					if (num <= this.bufferSize)
					{
						goto IL_157;
					}
					Interlocked.Decrement(ref this._pendingBufferCopy);
					Thread.EndCriticalRegion();
					if (this._writePos > this.bufferSize && num2 <= this.bufferSize && num2 > 0)
					{
						while (this._pendingBufferCopy != 0)
						{
							Thread.SpinWait(1);
						}
						this.WriteCore(this._buffer, 0, num2, true);
						this._writePos = 0;
					}
				}
			}
			this.WriteCore(array, offset, count, true);
			return;
			IL_157:
			if (this._buffer == null)
			{
				Interlocked.CompareExchange<byte[]>(ref this._buffer, new byte[this.bufferSize], null);
			}
			Buffer.BlockCopy(array, offset, this._buffer, num2, count);
			Interlocked.Decrement(ref this._pendingBufferCopy);
			Thread.EndCriticalRegion();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C848 File Offset: 0x0000AA48
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Flush()
		{
			try
			{
				if (this._writePos > 0)
				{
					this.FlushWrite(false);
				}
				else if (this._readPos < this._readLen)
				{
					this.FlushRead();
				}
			}
			finally
			{
				this._writePos = 0;
				this._readPos = 0;
				this._readLen = 0;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected void FlushRead()
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000C8A6 File Offset: 0x0000AAA6
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected void FlushWrite(bool blockForWrite)
		{
			if (this._writePos > 0)
			{
				this.WriteCore(this._buffer, 0, this._writePos, blockForWrite);
			}
			this._writePos = 0;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000C8CC File Offset: 0x0000AACC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._writePos > 0)
				{
					this.FlushWrite(disposing);
				}
			}
			finally
			{
				this._readPos = 0;
				this._readLen = 0;
				this._writePos = 0;
				base.Dispose(disposing);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000C918 File Offset: 0x0000AB18
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000C920 File Offset: 0x0000AB20
		protected long UnderlyingStreamPosition
		{
			get
			{
				return this.pos;
			}
			set
			{
				Interlocked.Exchange(ref this.pos, value);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C92F File Offset: 0x0000AB2F
		protected long AddUnderlyingStreamPosition(long posDelta)
		{
			return Interlocked.Add(ref this.pos, posDelta);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000C93D File Offset: 0x0000AB3D
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected internal void DiscardBuffer()
		{
			this._readPos = 0;
			this._readLen = 0;
			this._writePos = 0;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000C954 File Offset: 0x0000AB54
		private void WriteCore(byte[] buffer, int offset, int count, bool blockForWrite)
		{
			long num;
			this.WriteCore(buffer, offset, count, blockForWrite, out num);
		}

		// Token: 0x0600045D RID: 1117
		protected abstract void WriteCore(byte[] buffer, int offset, int count, bool blockForWrite, out long streamPos);

		// Token: 0x040004F0 RID: 1264
		protected internal const int DefaultBufferSize = 32768;

		// Token: 0x040004F1 RID: 1265
		protected int bufferSize;

		// Token: 0x040004F2 RID: 1266
		private byte[] _buffer;

		// Token: 0x040004F3 RID: 1267
		private int _pendingBufferCopy;

		// Token: 0x040004F4 RID: 1268
		private int _writePos;

		// Token: 0x040004F5 RID: 1269
		private int _readPos;

		// Token: 0x040004F6 RID: 1270
		private int _readLen;

		// Token: 0x040004F7 RID: 1271
		protected long pos;
	}
}
