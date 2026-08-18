using System;

namespace System.IO.Compression
{
	// Token: 0x02000004 RID: 4
	internal class SubReadStream : Stream
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022CF File Offset: 0x000004CF
		public SubReadStream(Stream superStream, long startPosition, long maxLength)
		{
			this._startInSuperStream = startPosition;
			this._positionInSuperStream = startPosition;
			this._endInSuperStream = startPosition + maxLength;
			this._superStream = superStream;
			this._canRead = true;
			this._isDisposed = false;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002303 File Offset: 0x00000503
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._endInSuperStream - this._startInSuperStream;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002318 File Offset: 0x00000518
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000232D File Offset: 0x0000052D
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._positionInSuperStream - this._startInSuperStream;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000233F File Offset: 0x0000053F
		public override bool CanRead
		{
			get
			{
				return this._superStream.CanRead && this._canRead;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002356 File Offset: 0x00000556
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002359 File Offset: 0x00000559
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000235C File Offset: 0x0000055C
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name, Messages.HiddenStreamName);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000237C File Offset: 0x0000057C
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Messages.ReadingNotSupported);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002394 File Offset: 0x00000594
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			if (this._superStream.Position != this._positionInSuperStream)
			{
				this._superStream.Seek(this._positionInSuperStream, SeekOrigin.Begin);
			}
			if (this._positionInSuperStream + (long)count > this._endInSuperStream)
			{
				count = (int)(this._endInSuperStream - this._positionInSuperStream);
			}
			int num = this._superStream.Read(buffer, offset, count);
			this._positionInSuperStream += (long)num;
			return num;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002415 File Offset: 0x00000615
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.SeekingNotSupported);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002427 File Offset: 0x00000627
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.SetLengthRequiresSeekingAndWriting);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002439 File Offset: 0x00000639
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.WritingNotSupported);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000244B File Offset: 0x0000064B
		public override void Flush()
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException(Messages.WritingNotSupported);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000245D File Offset: 0x0000065D
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				this._canRead = false;
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000009 RID: 9
		private readonly long _startInSuperStream;

		// Token: 0x0400000A RID: 10
		private long _positionInSuperStream;

		// Token: 0x0400000B RID: 11
		private readonly long _endInSuperStream;

		// Token: 0x0400000C RID: 12
		private readonly Stream _superStream;

		// Token: 0x0400000D RID: 13
		private bool _canRead;

		// Token: 0x0400000E RID: 14
		private bool _isDisposed;
	}
}
