using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000C8 RID: 200
	internal class HttpResponseStream : Stream
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x00025F9A File Offset: 0x0002419A
		internal HttpResponseStream(HttpWriter writer)
		{
			this._writer = writer;
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00010D64 File Offset: 0x0000EF64
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00025FAC File Offset: 0x000241AC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._writer.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00025FE4 File Offset: 0x000241E4
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00025FF4 File Offset: 0x000241F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._writer.IgnoringFurtherWrites)
			{
				return;
			}
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
			if (count == 0)
			{
				return;
			}
			this._writer.WriteFromStream(buffer, offset, count);
		}

		// Token: 0x04000508 RID: 1288
		private HttpWriter _writer;
	}
}
