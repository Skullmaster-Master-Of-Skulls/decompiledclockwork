using System;
using System.IO;

namespace TechnoPro.Common.Public.Entities.Files
{
	// Token: 0x02000335 RID: 821
	public class PositionWrapperStream : Stream
	{
		// Token: 0x060019B0 RID: 6576 RVA: 0x0001E0F5 File Offset: 0x0001C2F5
		public PositionWrapperStream(Stream wrapped)
		{
			this.wrapped = wrapped;
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0001E10D File Offset: 0x0001C30D
		public override bool CanRead { get; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0001E115 File Offset: 0x0001C315
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0001E118 File Offset: 0x0001C318
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0001E11B File Offset: 0x0001C31B
		public override long Length { get; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0001E124 File Offset: 0x0001C324
		// (set) Token: 0x060019B6 RID: 6582 RVA: 0x0001E13D File Offset: 0x0001C33D
		public override long Position
		{
			get
			{
				return (long)this.pos;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0001E145 File Offset: 0x0001C345
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0001E145 File Offset: 0x0001C345
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0001E145 File Offset: 0x0001C345
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0001E14D File Offset: 0x0001C34D
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.pos += count;
			this.wrapped.Write(buffer, offset, count);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0001E16D File Offset: 0x0001C36D
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0001E17C File Offset: 0x0001C37C
		protected override void Dispose(bool disposing)
		{
			this.wrapped.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x040014D3 RID: 5331
		private readonly Stream wrapped;

		// Token: 0x040014D4 RID: 5332
		private int pos = 0;
	}
}
