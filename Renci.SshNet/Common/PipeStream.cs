using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Renci.SshNet.Common
{
	// Token: 0x020000F8 RID: 248
	public class PipeStream : Stream
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x000247C5 File Offset: 0x000229C5
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x000247CD File Offset: 0x000229CD
		public long MaxBufferLength
		{
			get
			{
				return this._maxBufferLength;
			}
			set
			{
				this._maxBufferLength = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000247D6 File Offset: 0x000229D6
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x000247F0 File Offset: 0x000229F0
		public bool BlockLastReadBuffer
		{
			get
			{
				if (this._isDisposed)
				{
					throw this.CreateObjectDisposedException();
				}
				return this._canBlockLastRead;
			}
			set
			{
				if (this._isDisposed)
				{
					throw this.CreateObjectDisposedException();
				}
				this._canBlockLastRead = value;
				if (!this._canBlockLastRead)
				{
					Queue<byte> buffer = this._buffer;
					lock (buffer)
					{
						Monitor.Pulse(this._buffer);
					}
				}
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00024854 File Offset: 0x00022A54
		public override void Flush()
		{
			if (this._isDisposed)
			{
				throw this.CreateObjectDisposedException();
			}
			this._isFlushed = true;
			Queue<byte> buffer = this._buffer;
			lock (buffer)
			{
				Monitor.Pulse(this._buffer);
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000248B0 File Offset: 0x00022AB0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (offset != 0)
			{
				throw new NotSupportedException("Offsets with value of non-zero are not supported");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("The sum of offset and count is greater than the buffer length.");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset or count is negative.");
			}
			if (this.BlockLastReadBuffer && (long)count >= this._maxBufferLength)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "count({0}) > mMaxBufferLength({1})", new object[]
				{
					count,
					this._maxBufferLength
				}));
			}
			if (this._isDisposed)
			{
				throw this.CreateObjectDisposedException();
			}
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			Queue<byte> buffer2 = this._buffer;
			lock (buffer2)
			{
				while (!this._isDisposed && !this.ReadAvailable(count))
				{
					Monitor.Wait(this._buffer);
				}
				if (this._isDisposed)
				{
					return 0;
				}
				while (num < count && this._buffer.Count > 0)
				{
					buffer[num] = this._buffer.Dequeue();
					num++;
				}
				Monitor.Pulse(this._buffer);
			}
			return num;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000249E8 File Offset: 0x00022BE8
		private bool ReadAvailable(int count)
		{
			long length = this.Length;
			return (this._isFlushed || length >= (long)count) && (length >= (long)(count + 1) || !this.BlockLastReadBuffer);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00024A20 File Offset: 0x00022C20
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("The sum of offset and count is greater than the buffer length.");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "offset or count is negative.");
			}
			if (this._isDisposed)
			{
				throw this.CreateObjectDisposedException();
			}
			if (count == 0)
			{
				return;
			}
			Queue<byte> buffer2 = this._buffer;
			lock (buffer2)
			{
				while (this.Length >= this._maxBufferLength)
				{
					Monitor.Wait(this._buffer);
				}
				this._isFlushed = false;
				for (int i = offset; i < offset + count; i++)
				{
					this._buffer.Enqueue(buffer[i]);
				}
				Monitor.Pulse(this._buffer);
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00024AF0 File Offset: 0x00022CF0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!this._isDisposed)
			{
				Queue<byte> buffer = this._buffer;
				lock (buffer)
				{
					this._isDisposed = true;
					Monitor.Pulse(this._buffer);
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00024B4C File Offset: 0x00022D4C
		public override bool CanRead
		{
			get
			{
				return !this._isDisposed;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00024B4C File Offset: 0x00022D4C
		public override bool CanWrite
		{
			get
			{
				return !this._isDisposed;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00024B57 File Offset: 0x00022D57
		public override long Length
		{
			get
			{
				if (this._isDisposed)
				{
					throw this.CreateObjectDisposedException();
				}
				return (long)this._buffer.Count;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0000CB54 File Offset: 0x0000AD54
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00024B74 File Offset: 0x00022D74
		private ObjectDisposedException CreateObjectDisposedException()
		{
			return new ObjectDisposedException(base.GetType().FullName);
		}

		// Token: 0x040003FC RID: 1020
		private readonly Queue<byte> _buffer = new Queue<byte>();

		// Token: 0x040003FD RID: 1021
		private bool _isFlushed;

		// Token: 0x040003FE RID: 1022
		private long _maxBufferLength = 209715200L;

		// Token: 0x040003FF RID: 1023
		private bool _canBlockLastRead;

		// Token: 0x04000400 RID: 1024
		private bool _isDisposed;
	}
}
