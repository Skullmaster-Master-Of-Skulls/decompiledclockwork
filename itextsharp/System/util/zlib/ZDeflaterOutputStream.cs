using System;
using System.IO;

namespace System.util.zlib
{
	// Token: 0x020000F5 RID: 245
	public class ZDeflaterOutputStream : Stream
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x000327C1 File Offset: 0x000317C1
		public ZDeflaterOutputStream(Stream outp) : this(outp, 6, false)
		{
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000327CC File Offset: 0x000317CC
		public ZDeflaterOutputStream(Stream outp, int level) : this(outp, level, false)
		{
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000327D8 File Offset: 0x000317D8
		public ZDeflaterOutputStream(Stream outp, int level, bool nowrap)
		{
			this.outp = outp;
			this.z.deflateInit(level, nowrap);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00032827 File Offset: 0x00031827
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0003282A File Offset: 0x0003182A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0003282D File Offset: 0x0003182D
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00032830 File Offset: 0x00031830
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00032834 File Offset: 0x00031834
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00032838 File Offset: 0x00031838
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0003283C File Offset: 0x0003183C
		public override void Write(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return;
			}
			this.z.next_in = b;
			this.z.next_in_index = off;
			this.z.avail_in = len;
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = 4192;
				int num = this.z.deflate(this.flushLevel);
				if (num != 0)
				{
					break;
				}
				if (this.z.avail_out < 4192)
				{
					this.outp.Write(this.buf, 0, 4192 - this.z.avail_out);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					return;
				}
			}
			throw new IOException("deflating: " + this.z.msg);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00032924 File Offset: 0x00031924
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00032928 File Offset: 0x00031928
		public override void SetLength(long value)
		{
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0003292A File Offset: 0x0003192A
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0003292D File Offset: 0x0003192D
		public override void Flush()
		{
			this.outp.Flush();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0003293A File Offset: 0x0003193A
		public override void WriteByte(byte b)
		{
			this.buf1[0] = b;
			this.Write(this.buf1, 0, 1);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00032954 File Offset: 0x00031954
		public void Finish()
		{
			for (;;)
			{
				this.z.next_out = this.buf;
				this.z.next_out_index = 0;
				this.z.avail_out = 4192;
				int num = this.z.deflate(4);
				if (num != 1 && num != 0)
				{
					break;
				}
				if (4192 - this.z.avail_out > 0)
				{
					this.outp.Write(this.buf, 0, 4192 - this.z.avail_out);
				}
				if (this.z.avail_in <= 0 && this.z.avail_out != 0)
				{
					goto Block_4;
				}
			}
			throw new IOException("deflating: " + this.z.msg);
			Block_4:
			this.Flush();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00032A1B File Offset: 0x00031A1B
		public void End()
		{
			if (this.z == null)
			{
				return;
			}
			this.z.deflateEnd();
			this.z.free();
			this.z = null;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00032A44 File Offset: 0x00031A44
		public override void Close()
		{
			try
			{
				this.Finish();
			}
			catch (IOException)
			{
			}
			finally
			{
				this.End();
				this.outp.Close();
				this.outp = null;
			}
		}

		// Token: 0x040007FE RID: 2046
		private const int BUFSIZE = 4192;

		// Token: 0x040007FF RID: 2047
		protected ZStream z = new ZStream();

		// Token: 0x04000800 RID: 2048
		protected int flushLevel;

		// Token: 0x04000801 RID: 2049
		protected byte[] buf = new byte[4192];

		// Token: 0x04000802 RID: 2050
		private byte[] buf1 = new byte[1];

		// Token: 0x04000803 RID: 2051
		protected Stream outp;
	}
}
