using System;
using System.IO;
using System.Security.Cryptography;
using MailBee;

namespace a.b
{
	// Token: 0x020002AD RID: 685
	internal class cq : Stream
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x0006D6B0 File Offset: 0x0006C6B0
		public cq(az A_0, long A_1, hh A_2)
		{
			try
			{
				this.c = A_1;
				this.d = A_0;
				this.g = A_2;
				this.f = this.g.a(this.g.a().b().e(), this.g.a().b().i(), this.g.b(), this.g.a().b().d());
			}
			catch (CryptographicException ex)
			{
				throw ex;
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0006D748 File Offset: 0x0006C748
		public int d()
		{
			byte[] array = new byte[1];
			if (this.a(array) == 1)
			{
				return (int)array[0];
			}
			return -1;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0006D76B File Offset: 0x0006C76B
		public int a(byte[] A_0)
		{
			return this.Read(A_0, 0, A_0.Length);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0006D778 File Offset: 0x0006C778
		public override int Read(byte[] b, int offset, int len)
		{
			int num = 0;
			while (len > 0)
			{
				if (this.e == null)
				{
					try
					{
						this.e = this.a();
					}
					catch (CryptographicException ex)
					{
						throw new EncryptedDocumentException(ex.Message);
					}
				}
				int num2 = (int)(4096L - (this.b & 4095L));
				num2 = Math.Min(this.c(), Math.Min(num2, len));
				Array.Copy(this.e, (int)(this.b * 4095L), b, offset, num2);
				offset += num2;
				len -= num2;
				this.b += (long)num2;
				if ((this.b & 4095L) == 0L)
				{
					this.e = null;
				}
				num += num2;
			}
			return num;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0006D840 File Offset: 0x0006C840
		public long a(long A_0)
		{
			long num = this.b;
			long num2 = Math.Min((long)this.c(), A_0);
			if (((this.b + num2 ^ num) & -4096L) != 0L)
			{
				this.e = null;
			}
			this.b += num2;
			return num2;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0006D88B File Offset: 0x0006C88B
		public int c()
		{
			return (int)(this.c - this.b);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0006D89B File Offset: 0x0006C89B
		public override void Close()
		{
			this.d.Close();
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0006D8A8 File Offset: 0x0006C8A8
		public bool b()
		{
			return false;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0006D8AC File Offset: 0x0006C8AC
		private byte[] a()
		{
			int num = (int)(this.b >> 12);
			byte[] array = new byte[4];
			p.c(array, 0, num);
			byte[] iv = this.g.a(this.g.a().b().e(), this.g.a().b().d(), array);
			this.f.Key = this.g.b();
			this.f.IV = iv;
			if (this.a != num)
			{
				this.d.au((long)((long)(num - this.a) << 12));
			}
			byte[] a_ = new byte[Math.Min(this.d.aq(), 4096)];
			this.d.ay(a_);
			this.a = num + 1;
			throw new NotImplementedException();
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0006D982 File Offset: 0x0006C982
		public override bool get_CanRead()
		{
			return this.d.CanRead;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0006D98F File Offset: 0x0006C98F
		public override bool get_CanSeek()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0006D996 File Offset: 0x0006C996
		public override bool get_CanWrite()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0006D99D File Offset: 0x0006C99D
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0006D9A4 File Offset: 0x0006C9A4
		public override long get_Length()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0006D9AB File Offset: 0x0006C9AB
		public override long get_Position()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0006D9B2 File Offset: 0x0006C9B2
		public override void set_Position(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0006D9B9 File Offset: 0x0006C9B9
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.d.Seek(offset, origin);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0006D9C8 File Offset: 0x0006C9C8
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0006D9CF File Offset: 0x0006C9CF
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040011F7 RID: 4599
		private int a;

		// Token: 0x040011F8 RID: 4600
		private long b;

		// Token: 0x040011F9 RID: 4601
		private long c;

		// Token: 0x040011FA RID: 4602
		private az d;

		// Token: 0x040011FB RID: 4603
		private byte[] e;

		// Token: 0x040011FC RID: 4604
		private SymmetricAlgorithm f;

		// Token: 0x040011FD RID: 4605
		private hh g;
	}
}
