using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000591 RID: 1425
	public class OutputStreamCounter : Stream
	{
		// Token: 0x060030B8 RID: 12472 RVA: 0x0012CC8F File Offset: 0x0012BC8F
		public OutputStreamCounter(Stream _outc)
		{
			this.outc = _outc;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x0012CC9E File Offset: 0x0012BC9E
		public int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0012CCA6 File Offset: 0x0012BCA6
		public void ResetCounter()
		{
			this.counter = 0;
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x0012CCAF File Offset: 0x0012BCAF
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x0012CCB2 File Offset: 0x0012BCB2
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x0012CCB5 File Offset: 0x0012BCB5
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x0012CCB8 File Offset: 0x0012BCB8
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x0012CCBF File Offset: 0x0012BCBF
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x0012CCC6 File Offset: 0x0012BCC6
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

		// Token: 0x060030C1 RID: 12481 RVA: 0x0012CCCD File Offset: 0x0012BCCD
		public override void Flush()
		{
			this.outc.Flush();
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0012CCDA File Offset: 0x0012BCDA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0012CCE1 File Offset: 0x0012BCE1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0012CCE8 File Offset: 0x0012BCE8
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0012CCEF File Offset: 0x0012BCEF
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.counter += count;
			this.outc.Write(buffer, offset, count);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0012CD0D File Offset: 0x0012BD0D
		public override void Close()
		{
			this.outc.Close();
		}

		// Token: 0x04002175 RID: 8565
		protected Stream outc;

		// Token: 0x04002176 RID: 8566
		protected int counter;
	}
}
