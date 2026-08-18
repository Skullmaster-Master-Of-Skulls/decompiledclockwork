using System;

namespace System.IO.Compression
{
	// Token: 0x02000005 RID: 5
	internal class CheckSumAndSizeWriteStream : Stream
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002480 File Offset: 0x00000680
		public CheckSumAndSizeWriteStream(Stream baseStream, Stream baseBaseStream, bool leaveOpenOnClose, Action<long, long, uint> saveCrcAndSizes)
		{
			this._baseStream = baseStream;
			this._baseBaseStream = baseBaseStream;
			this._position = 0L;
			this._checksum = 0U;
			this._leaveOpenOnClose = leaveOpenOnClose;
			this._canWrite = true;
			this._isDisposed = false;
			this._initialPosition = 0L;
			this._saveCrcAndSizes = saveCrcAndSizes;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024D5 File Offset: 0x000006D5
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000024E7 File Offset: 0x000006E7
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000024F5 File Offset: 0x000006F5
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._position;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002507 File Offset: 0x00000707
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000250A File Offset: 0x0000070A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000250D File Offset: 0x0000070D
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002515 File Offset: 0x00000715
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name, Messages.HiddenStreamName);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002535 File Offset: 0x00000735
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.ReadingNotSupported);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002547 File Offset: 0x00000747
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.SeekingNotSupported);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002559 File Offset: 0x00000759
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.SetLengthRequiresSeekingAndWriting);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000256C File Offset: 0x0000076C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Messages.ArgumentNeedNonNegative);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Messages.ArgumentNeedNonNegative);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Messages.OffsetLengthInvalid);
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return;
			}
			if (!this._everWritten)
			{
				this._initialPosition = this._baseBaseStream.Position;
				this._everWritten = true;
			}
			this._checksum = Crc32Helper.UpdateCrc32(this._checksum, buffer, offset, count);
			this._baseStream.Write(buffer, offset, count);
			this._position += (long)count;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000261D File Offset: 0x0000081D
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this._baseStream.Flush();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002630 File Offset: 0x00000830
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (!this._everWritten)
				{
					this._initialPosition = this._baseBaseStream.Position;
				}
				if (!this._leaveOpenOnClose)
				{
					this._baseStream.Close();
				}
				if (this._saveCrcAndSizes != null)
				{
					this._saveCrcAndSizes(this._initialPosition, this.Position, this._checksum);
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400000F RID: 15
		private readonly Stream _baseStream;

		// Token: 0x04000010 RID: 16
		private readonly Stream _baseBaseStream;

		// Token: 0x04000011 RID: 17
		private long _position;

		// Token: 0x04000012 RID: 18
		private uint _checksum;

		// Token: 0x04000013 RID: 19
		private readonly bool _leaveOpenOnClose;

		// Token: 0x04000014 RID: 20
		private bool _canWrite;

		// Token: 0x04000015 RID: 21
		private bool _isDisposed;

		// Token: 0x04000016 RID: 22
		private bool _everWritten;

		// Token: 0x04000017 RID: 23
		private long _initialPosition;

		// Token: 0x04000018 RID: 24
		private readonly Action<long, long, uint> _saveCrcAndSizes;
	}
}
