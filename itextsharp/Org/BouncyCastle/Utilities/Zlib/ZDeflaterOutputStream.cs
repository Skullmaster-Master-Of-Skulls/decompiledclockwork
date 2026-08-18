using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000338 RID: 824
	public class ZDeflaterOutputStream : Stream
	{
		// Token: 0x06001DB9 RID: 7609 RVA: 0x000B22A1 File Offset: 0x000B12A1
		public ZDeflaterOutputStream(Stream outp) : this(outp, 6, false)
		{
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000B22AC File Offset: 0x000B12AC
		public ZDeflaterOutputStream(Stream outp, int level) : this(outp, level, false)
		{
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000B22B8 File Offset: 0x000B12B8
		public ZDeflaterOutputStream(Stream outp, int level, bool nowrap)
		{
			this.outp = outp;
			this.z.deflateInit(level, nowrap);
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000B2307 File Offset: 0x000B1307
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x000B230A File Offset: 0x000B130A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000B230D File Offset: 0x000B130D
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x000B2310 File Offset: 0x000B1310
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x000B2314 File Offset: 0x000B1314
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x000B2318 File Offset: 0x000B1318
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

		// Token: 0x06001DC2 RID: 7618 RVA: 0x000B231C File Offset: 0x000B131C
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

		// Token: 0x06001DC3 RID: 7619 RVA: 0x000B2404 File Offset: 0x000B1404
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000B2408 File Offset: 0x000B1408
		public override void SetLength(long value)
		{
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000B240A File Offset: 0x000B140A
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000B240D File Offset: 0x000B140D
		public override void Flush()
		{
			this.outp.Flush();
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000B241A File Offset: 0x000B141A
		public override void WriteByte(byte b)
		{
			this.buf1[0] = b;
			this.Write(this.buf1, 0, 1);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000B2434 File Offset: 0x000B1434
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

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000B24FB File Offset: 0x000B14FB
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

		// Token: 0x06001DCA RID: 7626 RVA: 0x000B2524 File Offset: 0x000B1524
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

		// Token: 0x04001473 RID: 5235
		private const int BUFSIZE = 4192;

		// Token: 0x04001474 RID: 5236
		protected ZStream z = new ZStream();

		// Token: 0x04001475 RID: 5237
		protected int flushLevel;

		// Token: 0x04001476 RID: 5238
		protected byte[] buf = new byte[4192];

		// Token: 0x04001477 RID: 5239
		private byte[] buf1 = new byte[1];

		// Token: 0x04001478 RID: 5240
		protected Stream outp;
	}
}
