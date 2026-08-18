using System;
using System.IO;

namespace System.util.zlib
{
	// Token: 0x02000229 RID: 553
	public class ZInflaterInputStream : Stream
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x0007B25A File Offset: 0x0007A25A
		public ZInflaterInputStream(Stream inp) : this(inp, false)
		{
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0007B264 File Offset: 0x0007A264
		public ZInflaterInputStream(Stream inp, bool nowrap)
		{
			this.inp = inp;
			this.z.inflateInit(nowrap);
			this.z.next_in = this.buf;
			this.z.next_in_index = 0;
			this.z.avail_in = 0;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0007B2DB File Offset: 0x0007A2DB
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0007B2DE File Offset: 0x0007A2DE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0007B2E1 File Offset: 0x0007A2E1
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0007B2E4 File Offset: 0x0007A2E4
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0007B2E8 File Offset: 0x0007A2E8
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x0007B2EC File Offset: 0x0007A2EC
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

		// Token: 0x0600158B RID: 5515 RVA: 0x0007B2EE File Offset: 0x0007A2EE
		public override void Write(byte[] b, int off, int len)
		{
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0007B2F0 File Offset: 0x0007A2F0
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0007B2F4 File Offset: 0x0007A2F4
		public override void SetLength(long value)
		{
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0007B2F8 File Offset: 0x0007A2F8
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

		// Token: 0x0600158F RID: 5519 RVA: 0x0007B411 File Offset: 0x0007A411
		public override void Flush()
		{
			this.inp.Flush();
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0007B41E File Offset: 0x0007A41E
		public override void WriteByte(byte b)
		{
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0007B420 File Offset: 0x0007A420
		public override void Close()
		{
			this.inp.Close();
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0007B42D File Offset: 0x0007A42D
		public override int ReadByte()
		{
			if (this.Read(this.buf1, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(this.buf1[0] & byte.MaxValue);
		}

		// Token: 0x04000F08 RID: 3848
		private const int BUFSIZE = 4192;

		// Token: 0x04000F09 RID: 3849
		protected ZStream z = new ZStream();

		// Token: 0x04000F0A RID: 3850
		protected int flushLevel;

		// Token: 0x04000F0B RID: 3851
		protected byte[] buf = new byte[4192];

		// Token: 0x04000F0C RID: 3852
		private byte[] buf1 = new byte[1];

		// Token: 0x04000F0D RID: 3853
		protected Stream inp;

		// Token: 0x04000F0E RID: 3854
		private bool nomoreinput;
	}
}
