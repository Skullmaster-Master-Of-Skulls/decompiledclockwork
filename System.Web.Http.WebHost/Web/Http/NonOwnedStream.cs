using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http
{
	// Token: 0x02000005 RID: 5
	internal class NonOwnedStream : Stream
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000026F1 File Offset: 0x000008F1
		protected NonOwnedStream()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026F9 File Offset: 0x000008F9
		public NonOwnedStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			this.InnerStream = innerStream;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002716 File Offset: 0x00000916
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000271E File Offset: 0x0000091E
		protected Stream InnerStream { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002727 File Offset: 0x00000927
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000272F File Offset: 0x0000092F
		private protected bool IsDisposed { protected get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002738 File Offset: 0x00000938
		public override bool CanRead
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanRead;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000274F File Offset: 0x0000094F
		public override bool CanSeek
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanSeek;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002766 File Offset: 0x00000966
		public override bool CanTimeout
		{
			get
			{
				return this.InnerStream.CanTimeout;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002773 File Offset: 0x00000973
		public override bool CanWrite
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanWrite;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000278A File Offset: 0x0000098A
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.Length;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000279D File Offset: 0x0000099D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000027B0 File Offset: 0x000009B0
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.InnerStream.Position = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000027D7 File Offset: 0x000009D7
		public override int ReadTimeout
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.ReadTimeout;
			}
			set
			{
				this.ThrowIfDisposed();
				this.InnerStream.ReadTimeout = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027EB File Offset: 0x000009EB
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000027FE File Offset: 0x000009FE
		public override int WriteTimeout
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.WriteTimeout;
			}
			set
			{
				this.ThrowIfDisposed();
				this.InnerStream.WriteTimeout = value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002812 File Offset: 0x00000A12
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000282C File Offset: 0x00000A2C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002846 File Offset: 0x00000A46
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000284E File Offset: 0x00000A4E
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002864 File Offset: 0x00000A64
		protected override void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				base.Dispose(disposing);
				this.IsDisposed = true;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000287C File Offset: 0x00000A7C
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.EndRead(asyncResult);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002890 File Offset: 0x00000A90
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposed();
			this.InnerStream.EndWrite(asyncResult);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028A4 File Offset: 0x00000AA4
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.InnerStream.Flush();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028B7 File Offset: 0x00000AB7
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028CB File Offset: 0x00000ACB
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028E1 File Offset: 0x00000AE1
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028F9 File Offset: 0x00000AF9
		public override int ReadByte()
		{
			this.ThrowIfDisposed();
			return this.InnerStream.ReadByte();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000290C File Offset: 0x00000B0C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.Seek(offset, origin);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002921 File Offset: 0x00000B21
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.InnerStream.SetLength(value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002935 File Offset: 0x00000B35
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.InnerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000294B File Offset: 0x00000B4B
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002963 File Offset: 0x00000B63
		public override void WriteByte(byte value)
		{
			this.ThrowIfDisposed();
			this.InnerStream.WriteByte(value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002977 File Offset: 0x00000B77
		protected void ThrowIfDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(null);
			}
		}
	}
}
