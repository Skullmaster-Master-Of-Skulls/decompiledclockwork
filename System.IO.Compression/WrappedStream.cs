using System;

namespace System.IO.Compression
{
	// Token: 0x02000003 RID: 3
	internal class WrappedStream : Stream
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		internal WrappedStream(Stream baseStream, bool canRead, bool canWrite, bool canSeek, EventHandler onClosed) : this(baseStream, canRead, canWrite, canSeek, false, onClosed)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		internal WrappedStream(Stream baseStream, bool canRead, bool canWrite, bool canSeek, bool closeBaseStream, EventHandler onClosed)
		{
			this._baseStream = baseStream;
			this._onClosed = onClosed;
			this._canRead = canRead;
			this._canSeek = canSeek;
			this._canWrite = canWrite;
			this._isDisposed = false;
			this._closeBaseStream = closeBaseStream;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
		internal WrappedStream(Stream baseStream, EventHandler onClosed) : this(baseStream, true, true, true, onClosed)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F9 File Offset: 0x000002F9
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Length;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000211F File Offset: 0x0000031F
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfCantSeek();
				this._baseStream.Position = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanRead
		{
			get
			{
				return this._canRead && this._baseStream.CanRead;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		public override bool CanSeek
		{
			get
			{
				return this._canSeek && this._baseStream.CanSeek;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002167 File Offset: 0x00000367
		public override bool CanWrite
		{
			get
			{
				return this._canWrite && this._baseStream.CanWrite;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000217E File Offset: 0x0000037E
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name, Messages.HiddenStreamName);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000219E File Offset: 0x0000039E
		private void ThrowIfCantRead()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Messages.WritingNotSupported);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B3 File Offset: 0x000003B3
		private void ThrowIfCantWrite()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Messages.WritingNotSupported);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021C8 File Offset: 0x000003C8
		private void ThrowIfCantSeek()
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException(Messages.SeekingNotSupported);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021DD File Offset: 0x000003DD
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F9 File Offset: 0x000003F9
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			return this._baseStream.Seek(offset, origin);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002214 File Offset: 0x00000414
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			this.ThrowIfCantWrite();
			this._baseStream.SetLength(value);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002234 File Offset: 0x00000434
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002250 File Offset: 0x00000450
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Flush();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000226C File Offset: 0x0000046C
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (this._onClosed != null)
				{
					this._onClosed(this, null);
				}
				if (this._closeBaseStream)
				{
					this._baseStream.Dispose();
				}
				this._canRead = false;
				this._canWrite = false;
				this._canSeek = false;
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000002 RID: 2
		private readonly Stream _baseStream;

		// Token: 0x04000003 RID: 3
		private readonly EventHandler _onClosed;

		// Token: 0x04000004 RID: 4
		private bool _canRead;

		// Token: 0x04000005 RID: 5
		private bool _canWrite;

		// Token: 0x04000006 RID: 6
		private bool _canSeek;

		// Token: 0x04000007 RID: 7
		private bool _isDisposed;

		// Token: 0x04000008 RID: 8
		private readonly bool _closeBaseStream;
	}
}
