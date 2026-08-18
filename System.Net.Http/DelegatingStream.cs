using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200001D RID: 29
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000068D3 File Offset: 0x00004AD3
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000068E0 File Offset: 0x00004AE0
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000068ED File Offset: 0x00004AED
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000068FA File Offset: 0x00004AFA
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006907 File Offset: 0x00004B07
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00006914 File Offset: 0x00004B14
		public override long Position
		{
			get
			{
				return this.innerStream.Position;
			}
			set
			{
				this.innerStream.Position = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006922 File Offset: 0x00004B22
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000692F File Offset: 0x00004B2F
		public override int ReadTimeout
		{
			get
			{
				return this.innerStream.ReadTimeout;
			}
			set
			{
				this.innerStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000693D File Offset: 0x00004B3D
		public override bool CanTimeout
		{
			get
			{
				return this.innerStream.CanTimeout;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000694A File Offset: 0x00004B4A
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006957 File Offset: 0x00004B57
		public override int WriteTimeout
		{
			get
			{
				return this.innerStream.WriteTimeout;
			}
			set
			{
				this.innerStream.WriteTimeout = value;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006965 File Offset: 0x00004B65
		protected DelegatingStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006974 File Offset: 0x00004B74
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000698B File Offset: 0x00004B8B
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000699A File Offset: 0x00004B9A
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000069AA File Offset: 0x00004BAA
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000069BE File Offset: 0x00004BBE
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.innerStream.EndRead(asyncResult);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000069CC File Offset: 0x00004BCC
		public override int ReadByte()
		{
			return this.innerStream.ReadByte();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000069D9 File Offset: 0x00004BD9
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000069EB File Offset: 0x00004BEB
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000069F8 File Offset: 0x00004BF8
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.innerStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006A06 File Offset: 0x00004C06
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006A14 File Offset: 0x00004C14
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006A24 File Offset: 0x00004C24
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006A38 File Offset: 0x00004C38
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.innerStream.EndWrite(asyncResult);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006A46 File Offset: 0x00004C46
		public override void WriteByte(byte value)
		{
			this.innerStream.WriteByte(value);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006A54 File Offset: 0x00004C54
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x040000D1 RID: 209
		private Stream innerStream;
	}
}
