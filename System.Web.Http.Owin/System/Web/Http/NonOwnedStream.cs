using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http
{
	// Token: 0x02000006 RID: 6
	internal class NonOwnedStream : Stream
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000029A5 File Offset: 0x00000BA5
		protected NonOwnedStream()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029AD File Offset: 0x00000BAD
		public NonOwnedStream(Stream innerStream)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			this.InnerStream = innerStream;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029CA File Offset: 0x00000BCA
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000029D2 File Offset: 0x00000BD2
		protected Stream InnerStream { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000029DB File Offset: 0x00000BDB
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000029E3 File Offset: 0x00000BE3
		private protected bool IsDisposed { protected get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000029EC File Offset: 0x00000BEC
		public override bool CanRead
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanRead;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A03 File Offset: 0x00000C03
		public override bool CanSeek
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanSeek;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A1A File Offset: 0x00000C1A
		public override bool CanTimeout
		{
			get
			{
				return this.InnerStream.CanTimeout;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A27 File Offset: 0x00000C27
		public override bool CanWrite
		{
			get
			{
				return !this.IsDisposed && this.InnerStream.CanWrite;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A3E File Offset: 0x00000C3E
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InnerStream.Length;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A51 File Offset: 0x00000C51
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002A64 File Offset: 0x00000C64
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A78 File Offset: 0x00000C78
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A8B File Offset: 0x00000C8B
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A9F File Offset: 0x00000C9F
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002AB2 File Offset: 0x00000CB2
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

		// Token: 0x0600003D RID: 61 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AFA File Offset: 0x00000CFA
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B02 File Offset: 0x00000D02
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B18 File Offset: 0x00000D18
		protected override void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				base.Dispose(disposing);
				this.IsDisposed = true;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B30 File Offset: 0x00000D30
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.EndRead(asyncResult);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B44 File Offset: 0x00000D44
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.ThrowIfDisposed();
			this.InnerStream.EndWrite(asyncResult);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B58 File Offset: 0x00000D58
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.InnerStream.Flush();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B6B File Offset: 0x00000D6B
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B7F File Offset: 0x00000D7F
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B95 File Offset: 0x00000D95
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BAD File Offset: 0x00000DAD
		public override int ReadByte()
		{
			this.ThrowIfDisposed();
			return this.InnerStream.ReadByte();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.Seek(offset, origin);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.InnerStream.SetLength(value);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BE9 File Offset: 0x00000DE9
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.InnerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BFF File Offset: 0x00000DFF
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			return this.InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C17 File Offset: 0x00000E17
		public override void WriteByte(byte value)
		{
			this.ThrowIfDisposed();
			this.InnerStream.WriteByte(value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C2B File Offset: 0x00000E2B
		protected void ThrowIfDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(null);
			}
		}
	}
}
