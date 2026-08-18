using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.WebHost
{
	// Token: 0x0200000D RID: 13
	internal class SeekableBufferedRequestStream : NonOwnedStream
	{
		// Token: 0x0600005B RID: 91 RVA: 0x0000312A File Offset: 0x0000132A
		public SeekableBufferedRequestStream(HttpRequestBase request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this._request = request;
			base.InnerStream = request.GetBufferedInputStream();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003153 File Offset: 0x00001353
		public override bool CanSeek
		{
			get
			{
				return !base.IsDisposed;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000315E File Offset: 0x0000135E
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003171 File Offset: 0x00001371
		public override long Position
		{
			get
			{
				base.ThrowIfDisposed();
				return base.InnerStream.Position;
			}
			set
			{
				base.ThrowIfDisposed();
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003184 File Offset: 0x00001384
		public override int EndRead(IAsyncResult asyncResult)
		{
			base.ThrowIfDisposed();
			int num = base.InnerStream.EndRead(asyncResult);
			if (num == 0 && !this._isReadToEndComplete)
			{
				this.SwapToSeekableStream();
			}
			return num;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000031B8 File Offset: 0x000013B8
		public override int Read(byte[] buffer, int offset, int count)
		{
			base.ThrowIfDisposed();
			int num = base.InnerStream.Read(buffer, offset, count);
			if (num == 0 && !this._isReadToEndComplete)
			{
				this.SwapToSeekableStream();
			}
			return num;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000331C File Offset: 0x0000151C
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			base.ThrowIfDisposed();
			int bytesRead = await base.InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
			if (bytesRead == 0 && !this._isReadToEndComplete)
			{
				this.SwapToSeekableStream();
			}
			return bytesRead;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003384 File Offset: 0x00001584
		public override int ReadByte()
		{
			base.ThrowIfDisposed();
			int num = base.InnerStream.ReadByte();
			if (num == -1 && !this._isReadToEndComplete)
			{
				this.SwapToSeekableStream();
			}
			return num;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000033B8 File Offset: 0x000015B8
		public override long Seek(long offset, SeekOrigin origin)
		{
			base.ThrowIfDisposed();
			long position = base.InnerStream.Position;
			long? num = null;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = new long?(offset);
				break;
			case SeekOrigin.Current:
				num = new long?(position + offset);
				break;
			case SeekOrigin.End:
				if (this.Length >= 0L)
				{
					num = new long?(this.Length + offset);
				}
				break;
			default:
				throw new InvalidEnumArgumentException("origin", (int)origin, typeof(SeekOrigin));
			}
			if (num == position)
			{
				return position;
			}
			if (!this._isReadToEndComplete)
			{
				byte[] array = new byte[1024];
				while (base.InnerStream.Read(array, 0, array.Length) > 0)
				{
				}
				this.SwapToSeekableStream();
			}
			return base.InnerStream.Seek(offset, origin);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003494 File Offset: 0x00001694
		private void SwapToSeekableStream()
		{
			Stream inputStream = this._request.InputStream;
			inputStream.Position = base.InnerStream.Position;
			base.InnerStream = inputStream;
			this._isReadToEndComplete = true;
		}

		// Token: 0x0400000E RID: 14
		private const int ReadBufferSize = 1024;

		// Token: 0x0400000F RID: 15
		private readonly HttpRequestBase _request;

		// Token: 0x04000010 RID: 16
		private bool _isReadToEndComplete;
	}
}
