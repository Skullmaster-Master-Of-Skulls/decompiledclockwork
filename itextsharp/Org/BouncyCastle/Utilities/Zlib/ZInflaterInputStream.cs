using System;
using System.IO;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x0200022D RID: 557
	public class ZInflaterInputStream : Stream
	{
		// Token: 0x060015AC RID: 5548 RVA: 0x0007DAE8 File Offset: 0x0007CAE8
		public ZInflaterInputStream(Stream inp) : this(inp, false)
		{
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0007DAF4 File Offset: 0x0007CAF4
		public ZInflaterInputStream(Stream inp, bool nowrap)
		{
			this.inp = inp;
			this.z.inflateInit(nowrap);
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0007DB6B File Offset: 0x0007CB6B
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0007DB6E File Offset: 0x0007CB6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0007DB71 File Offset: 0x0007CB71
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0007DB74 File Offset: 0x0007CB74
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0007DB78 File Offset: 0x0007CB78
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0007DB7C File Offset: 0x0007CB7C
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

		// Token: 0x060015B4 RID: 5556 RVA: 0x0007DB7E File Offset: 0x0007CB7E
		public override void Write(byte[] b, int off, int len)
		{
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0007DB80 File Offset: 0x0007CB80
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0007DB84 File Offset: 0x0007CB84
		public override void SetLength(long value)
		{
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0007DB88 File Offset: 0x0007CB88
		public override int Read(byte[] b, int off, int len)
		{
			if (len == 0)
			{
				return 0;
			}
			this.z.next_out = b;
			this.z.next_out_index = off;
			this.z.avail_out = len;
			for (;;)
			{
				if (this.z.avail_in == 0 && !this.nomoreinput)
				{
					this.z.next_in_index = 0;
					this.z.avail_in = this.inp.Read(this.buf, 0, 4192);
					if (this.z.avail_in == 0)
					{
						this.z.avail_in = 0;
						this.nomoreinput = true;
					}
				}
				int num = this.z.inflate(this.flushLevel);
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_8;
				}
				if ((this.nomoreinput || num == 1) && this.z.avail_out == len)
				{
					return 0;
				}
				if (this.z.avail_out != len || num != 0)
				{
					goto IL_FF;
				}
			}
			return -1;
			Block_8:
			throw new IOException("inflating: " + this.z.msg);
			IL_FF:
			return len - this.z.avail_out;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0007DCA1 File Offset: 0x0007CCA1
		public override void Flush()
		{
			this.inp.Flush();
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0007DCAE File Offset: 0x0007CCAE
		public override void WriteByte(byte b)
		{
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0007DCB0 File Offset: 0x0007CCB0
		public override void Close()
		{
			this.inp.Close();
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0007DCBD File Offset: 0x0007CCBD
		public override int ReadByte()
		{
			if (this.Read(this.buf1, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(this.buf1[0] & byte.MaxValue);
		}

		// Token: 0x04000F2C RID: 3884
		private const int BUFSIZE = 4192;

		// Token: 0x04000F2D RID: 3885
		protected ZStream z = new ZStream();

		// Token: 0x04000F2E RID: 3886
		protected int flushLevel;

		// Token: 0x04000F2F RID: 3887
		protected byte[] buf = new byte[4192];

		// Token: 0x04000F30 RID: 3888
		private byte[] buf1 = new byte[1];

		// Token: 0x04000F31 RID: 3889
		protected Stream inp;

		// Token: 0x04000F32 RID: 3890
		private bool nomoreinput;
	}
}
