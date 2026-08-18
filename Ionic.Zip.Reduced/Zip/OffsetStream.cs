using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000024 RID: 36
	internal class OffsetStream : Stream, IDisposable
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003FB4 File Offset: 0x000021B4
		public OffsetStream(Stream s)
		{
			this._originalPosition = s.Position;
			this._innerStream = s;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FCF File Offset: 0x000021CF
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003FDF File Offset: 0x000021DF
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003FE6 File Offset: 0x000021E6
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003FF3 File Offset: 0x000021F3
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004000 File Offset: 0x00002200
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004003 File Offset: 0x00002203
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004010 File Offset: 0x00002210
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000401D File Offset: 0x0000221D
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004031 File Offset: 0x00002231
		public override long Position
		{
			get
			{
				return this._innerStream.Position - this._originalPosition;
			}
			set
			{
				this._innerStream.Position = this._originalPosition + value;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004046 File Offset: 0x00002246
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(this._originalPosition + offset, origin) - this._originalPosition;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004063 File Offset: 0x00002263
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000406A File Offset: 0x0000226A
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004072 File Offset: 0x00002272
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0400005D RID: 93
		private long _originalPosition;

		// Token: 0x0400005E RID: 94
		private Stream _innerStream;
	}
}
