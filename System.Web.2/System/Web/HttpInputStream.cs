using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000A2 RID: 162
	internal class HttpInputStream : Stream
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x00017AD3 File Offset: 0x00015CD3
		internal HttpInputStream(HttpRawUploadedContent data, int offset, int length)
		{
			this.Init(data, offset, length);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00017AE4 File Offset: 0x00015CE4
		protected void Init(HttpRawUploadedContent data, int offset, int length)
		{
			this._data = data;
			this._offset = offset;
			this._length = length;
			this._pos = 0;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00017B02 File Offset: 0x00015D02
		protected void Uninit()
		{
			this._data = null;
			this._offset = 0;
			this._length = 0;
			this._pos = 0;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00017B20 File Offset: 0x00015D20
		internal byte[] GetAsByteArray()
		{
			if (this._length == 0)
			{
				return null;
			}
			return this._data.GetAsByteArray(this._offset, this._length);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00017B43 File Offset: 0x00015D43
		internal void WriteTo(Stream s)
		{
			if (this._data != null && this._length > 0)
			{
				this._data.WriteBytes(this._offset, this._length, s);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00007722 File Offset: 0x00005922
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00017B6E File Offset: 0x00015D6E
		public override long Length
		{
			get
			{
				return (long)this._length;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00017B77 File Offset: 0x00015D77
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00017B80 File Offset: 0x00015D80
		public override long Position
		{
			get
			{
				return (long)this._pos;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00017B8C File Offset: 0x00015D8C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Uninit();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00017BBC File Offset: 0x00015DBC
		public override long Seek(long offset, SeekOrigin origin)
		{
			int num = this._pos;
			int num2 = (int)offset;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = num2;
				break;
			case SeekOrigin.Current:
				num = this._pos + num2;
				break;
			case SeekOrigin.End:
				num = this._length + num2;
				break;
			default:
				throw new ArgumentOutOfRangeException("origin");
			}
			if (num < 0 || num > this._length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this._pos = num;
			return (long)this._pos;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void SetLength(long length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00017C34 File Offset: 0x00015E34
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._length - this._pos;
			if (count < num)
			{
				num = count;
			}
			if (num > 0)
			{
				this._data.CopyBytes(this._offset + this._pos, buffer, offset, num);
			}
			this._pos += num;
			return num;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00010D64 File Offset: 0x0000EF64
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040003C2 RID: 962
		private HttpRawUploadedContent _data;

		// Token: 0x040003C3 RID: 963
		private int _offset;

		// Token: 0x040003C4 RID: 964
		private int _length;

		// Token: 0x040003C5 RID: 965
		private int _pos;
	}
}
