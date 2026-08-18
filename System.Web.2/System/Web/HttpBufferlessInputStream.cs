using System;
using System.IO;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web
{
	// Token: 0x02000080 RID: 128
	internal class HttpBufferlessInputStream : Stream
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00010C8C File Offset: 0x0000EE8C
		internal HttpBufferlessInputStream(HttpContext context, bool persistEntityBody, bool disableMaxRequestLength)
		{
			this._context = context;
			this._persistEntityBody = persistEntityBody;
			this._disableMaxRequestLength = disableMaxRequestLength;
			HttpRuntimeSection httpRuntime = RuntimeConfig.GetConfig(this._context).HttpRuntime;
			this._maxRequestLength = (long)httpRuntime.MaxRequestLengthBytes;
			this._fileThreshold = httpRuntime.RequestLengthDiskThresholdBytes;
			if (this._persistEntityBody)
			{
				this._rawContent = new HttpRawUploadedContent(this._fileThreshold, this._context.Request.ContentLength);
			}
			int contentLength = this._context.Request.ContentLength;
			this._remainingBytes = ((contentLength > 0) ? contentLength : int.MaxValue);
			this._length = (long)contentLength;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00010D32 File Offset: 0x0000EF32
		internal bool PersistEntityBody
		{
			get
			{
				return this._persistEntityBody;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00010D3A File Offset: 0x0000EF3A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._persistEntityBody)
			{
				this.SetRawContentOnce();
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00010D54 File Offset: 0x0000EF54
		public override long Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00010D5C File Offset: 0x0000EF5C
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00010D6C File Offset: 0x0000EF6C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			HttpWorkerRequest workerRequest = this._context.WorkerRequest;
			if (workerRequest != null && workerRequest.SupportsAsyncRead && !this._context.IsInCancellablePeriod)
			{
				if (!this._preloadedContentRead)
				{
					if (buffer == null)
					{
						throw new ArgumentNullException("buffer");
					}
					if (offset < 0)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count < 0)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					if (buffer.Length - offset < count)
					{
						throw new ArgumentException(SR.GetString("InvalidOffsetOrCount", new object[]
						{
							"offset",
							"count"
						}));
					}
					this._preloadedBytesRead = this.GetPreloadedContent(buffer, ref offset, ref count);
				}
				if (this._remainingBytes == 0)
				{
					count = 0;
				}
				if (this._persistEntityBody)
				{
					this._buffer = buffer;
					this._offset = offset;
					this._count = count;
				}
				try
				{
					return workerRequest.BeginRead(buffer, offset, count, callback, state);
				}
				catch (HttpException)
				{
					if (this._persistEntityBody)
					{
						this.SetRawContentOnce();
					}
					throw;
				}
			}
			return base.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00010E84 File Offset: 0x0000F084
		public override int EndRead(IAsyncResult asyncResult)
		{
			HttpWorkerRequest workerRequest = this._context.WorkerRequest;
			if (workerRequest != null && workerRequest.SupportsAsyncRead && !this._context.IsInCancellablePeriod)
			{
				int num = this._preloadedBytesRead;
				if (this._preloadedBytesRead > 0)
				{
					this._preloadedBytesRead = 0;
				}
				int num2 = 0;
				try
				{
					num2 = workerRequest.EndRead(asyncResult);
				}
				catch (HttpException)
				{
					if (this._persistEntityBody)
					{
						this.SetRawContentOnce();
					}
					throw;
				}
				num += num2;
				if (num2 > 0)
				{
					if (this._persistEntityBody)
					{
						if (this._rawContent != null)
						{
							this._rawContent.AddBytes(this._buffer, this._offset, num2);
						}
						this._buffer = null;
						this._offset = 0;
						this._count = 0;
					}
					int num3 = 0;
					int num4 = 0;
					int num5 = 0;
					this.UpdateCounters(num2, ref num3, ref num4, ref num5);
				}
				if (this._persistEntityBody && ((num2 == 0 && this._count != 0) || this._remainingBytes == 0))
				{
					this.SetRawContentOnce();
				}
				return num;
			}
			return base.EndRead(asyncResult);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00010F88 File Offset: 0x0000F188
		public override int Read(byte[] buffer, int offset, int count)
		{
			HttpWorkerRequest workerRequest = this._context.WorkerRequest;
			if (workerRequest == null || count == 0)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentException(null, "offset");
			}
			if (count < 0)
			{
				throw new ArgumentException(null, "count");
			}
			int preloadedContent = this.GetPreloadedContent(buffer, ref offset, ref count);
			int num = 0;
			while (count > 0 && this._remainingBytes != 0)
			{
				num = workerRequest.ReadEntityBody(buffer, offset, count);
				if (num <= 0)
				{
					if (!this._context.Response.IsClientConnected)
					{
						if (this._persistEntityBody)
						{
							this.SetRawContentOnce();
						}
						throw new HttpException(SR.GetString("HttpBufferlessInputStream_ClientDisconnected"));
					}
					break;
				}
				else
				{
					if (this._persistEntityBody && this._rawContent != null)
					{
						this._rawContent.AddBytes(buffer, offset, num);
					}
					this.UpdateCounters(num, ref offset, ref count, ref preloadedContent);
				}
			}
			if (this._persistEntityBody && ((num == 0 && count != 0) || this._remainingBytes == 0))
			{
				this.SetRawContentOnce();
			}
			return preloadedContent;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00011084 File Offset: 0x0000F284
		private int GetPreloadedContent(byte[] buffer, ref int offset, ref int count)
		{
			if (this._preloadedContentRead)
			{
				return 0;
			}
			if (this._position == 0L)
			{
				this.ValidateRequestEntityLength();
			}
			int num = 0;
			int num2 = 0;
			byte[] preloadedEntityBody = this._context.WorkerRequest.GetPreloadedEntityBody();
			if (preloadedEntityBody != null)
			{
				num2 = preloadedEntityBody.Length - (int)this._position;
				int num3 = Math.Min(count, num2);
				Buffer.BlockCopy(preloadedEntityBody, (int)this._position, buffer, offset, num3);
				if (this._persistEntityBody && this._rawContent != null)
				{
					this._rawContent.AddBytes(preloadedEntityBody, (int)this._position, num3);
				}
				this.UpdateCounters(num3, ref offset, ref count, ref num);
			}
			if (num == num2)
			{
				this._preloadedContentRead = true;
				if (this._context.WorkerRequest.IsEntireEntityBodyIsPreloaded())
				{
					this._remainingBytes = 0;
				}
			}
			return num;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001113C File Offset: 0x0000F33C
		private void UpdateCounters(int bytesRead, ref int offset, ref int count, ref int totalBytesRead)
		{
			this._context.WorkerRequest.UpdateRequestCounters(bytesRead);
			count -= bytesRead;
			offset += bytesRead;
			this._position += (long)bytesRead;
			this._remainingBytes -= bytesRead;
			totalBytesRead += bytesRead;
			if (this._length < this._position)
			{
				this._length = this._position;
			}
			this.ValidateRequestEntityLength();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000111AC File Offset: 0x0000F3AC
		private void ValidateRequestEntityLength()
		{
			if (!this._disableMaxRequestLength && this.Length > this._maxRequestLength)
			{
				if (!(this._context.WorkerRequest is IIS7WorkerRequest))
				{
					this._context.Response.CloseConnectionAfterError();
				}
				throw new HttpException(SR.GetString("Max_request_length_exceeded"), null, 3004);
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00011207 File Offset: 0x0000F407
		private void SetRawContentOnce()
		{
			if (this._persistEntityBody && this._rawContent != null)
			{
				this._rawContent.DoneAddingBytes();
				this._context.Request.SetRawContent(this._rawContent);
				this._rawContent = null;
			}
		}

		// Token: 0x04000298 RID: 664
		private long _position;

		// Token: 0x04000299 RID: 665
		private long _length;

		// Token: 0x0400029A RID: 666
		private long _maxRequestLength;

		// Token: 0x0400029B RID: 667
		private bool _disableMaxRequestLength;

		// Token: 0x0400029C RID: 668
		private int _fileThreshold;

		// Token: 0x0400029D RID: 669
		private bool _preloadedContentRead;

		// Token: 0x0400029E RID: 670
		private HttpContext _context;

		// Token: 0x0400029F RID: 671
		private int _preloadedBytesRead;

		// Token: 0x040002A0 RID: 672
		private bool _persistEntityBody;

		// Token: 0x040002A1 RID: 673
		private HttpRawUploadedContent _rawContent;

		// Token: 0x040002A2 RID: 674
		private byte[] _buffer;

		// Token: 0x040002A3 RID: 675
		private int _offset;

		// Token: 0x040002A4 RID: 676
		private int _count;

		// Token: 0x040002A5 RID: 677
		private int _remainingBytes;
	}
}
