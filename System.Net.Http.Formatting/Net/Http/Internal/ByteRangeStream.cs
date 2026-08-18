using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Internal
{
	// Token: 0x0200001A RID: 26
	internal class ByteRangeStream : DelegatingStream
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000484C File Offset: 0x00002A4C
		public ByteRangeStream(Stream innerStream, RangeItemHeaderValue range) : base(innerStream)
		{
			if (range == null)
			{
				throw Error.ArgumentNull("range");
			}
			if (!innerStream.CanSeek)
			{
				throw Error.Argument("innerStream", Resources.ByteRangeStreamNotSeekable, new object[]
				{
					typeof(ByteRangeStream).Name
				});
			}
			if (innerStream.Length < 1L)
			{
				throw Error.ArgumentOutOfRange("innerStream", innerStream.Length, Resources.ByteRangeStreamEmpty, new object[]
				{
					typeof(ByteRangeStream).Name
				});
			}
			if (range.From != null && range.From.Value > innerStream.Length)
			{
				throw Error.ArgumentOutOfRange("range", range.From, Resources.ByteRangeStreamInvalidFrom, new object[]
				{
					innerStream.Length
				});
			}
			long num = innerStream.Length - 1L;
			long num2;
			if (range.To != null)
			{
				if (range.From != null)
				{
					num2 = Math.Min(range.To.Value, num);
					this._lowerbounds = range.From.Value;
				}
				else
				{
					num2 = num;
					this._lowerbounds = Math.Max(innerStream.Length - range.To.Value, 0L);
				}
			}
			else if (range.From != null)
			{
				num2 = num;
				this._lowerbounds = range.From.Value;
			}
			else
			{
				num2 = num;
				this._lowerbounds = 0L;
			}
			this._totalCount = num2 - this._lowerbounds + 1L;
			this.ContentRange = new ContentRangeHeaderValue(this._lowerbounds, num2, innerStream.Length);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004A18 File Offset: 0x00002C18
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004A20 File Offset: 0x00002C20
		public ContentRangeHeaderValue ContentRange { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004A29 File Offset: 0x00002C29
		public override long Length
		{
			get
			{
				return this._totalCount;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004A31 File Offset: 0x00002C31
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004A34 File Offset: 0x00002C34
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.BeginRead(buffer, offset, this.PrepareStreamForRangeRead(count), callback, state);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004A49 File Offset: 0x00002C49
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.Read(buffer, offset, this.PrepareStreamForRangeRead(count));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004A5C File Offset: 0x00002C5C
		public override int ReadByte()
		{
			int num = this.PrepareStreamForRangeRead(1);
			if (num <= 0)
			{
				return -1;
			}
			return base.ReadByte();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004A7D File Offset: 0x00002C7D
		public override void SetLength(long value)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004A8F File Offset: 0x00002C8F
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004AA1 File Offset: 0x00002CA1
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004AB3 File Offset: 0x00002CB3
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004AC5 File Offset: 0x00002CC5
		public override void WriteByte(byte value)
		{
			throw Error.NotSupported(Resources.ByteRangeStreamReadOnly, new object[0]);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004AD8 File Offset: 0x00002CD8
		private int PrepareStreamForRangeRead(int count)
		{
			long num = Math.Min((long)count, this._totalCount - this._currentCount);
			if (num > 0L)
			{
				long position = base.InnerStream.Position;
				if (this._lowerbounds + this._currentCount != position)
				{
					base.InnerStream.Position = this._lowerbounds + this._currentCount;
				}
				this._currentCount += num;
			}
			return (int)num;
		}

		// Token: 0x04000039 RID: 57
		private readonly long _lowerbounds;

		// Token: 0x0400003A RID: 58
		private readonly long _totalCount;

		// Token: 0x0400003B RID: 59
		private long _currentCount;
	}
}
