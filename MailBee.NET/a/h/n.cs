using System;
using System.IO;
using MailBee;
using MailBee.Tnef;

namespace a.h
{
	// Token: 0x02000200 RID: 512
	internal class n : Stream
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x000464AF File Offset: 0x000454AF
		public override long get_Position()
		{
			return this.f;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000464B7 File Offset: 0x000454B7
		public override void set_Position(long value)
		{
			this.f = value;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000464C0 File Offset: 0x000454C0
		private void a(Stream A_0, long A_1, long A_2)
		{
			if (A_0 is FileStream && A_2 > 16L)
			{
				A_0 = new BufferedStream(A_0, (A_2 < 4096L) ? ((int)A_2) : 4096);
			}
			this.a = A_0;
			this.e = A_1;
			if (this.a(A_1) != A_1)
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream, 1001);
			}
			this.e = A_2;
			this.d = A_1;
			this.f = A_1;
			this.g = A_1;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00046540 File Offset: 0x00045540
		public n(byte[] A_0, long A_1, long A_2)
		{
			this.b = A_0;
			this.a(new MemoryStream(A_0), A_1, A_2);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004656A File Offset: 0x0004556A
		public n(byte[] A_0) : this(A_0, 0L, (long)A_0.Length)
		{
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00046579 File Offset: 0x00045579
		public n(string A_0, long A_1, long A_2) : this(new FileInfo(A_0), A_1, A_2)
		{
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00046589 File Offset: 0x00045589
		public n(string A_0) : this(new FileInfo(A_0))
		{
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00046597 File Offset: 0x00045597
		public n(FileInfo A_0) : this(A_0, 0L, A_0.Length)
		{
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000465A8 File Offset: 0x000455A8
		public n(FileInfo A_0, long A_1, long A_2)
		{
			this.c = A_0;
			this.a(new FileStream(A_0.FullName, FileMode.Open, FileAccess.Read), A_1, A_2);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000465D9 File Offset: 0x000455D9
		public n(n A_0) : this(A_0, 0L, (long)A_0.b())
		{
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000465EC File Offset: 0x000455EC
		public n(n A_0, long A_1, long A_2)
		{
			this.c = A_0.c;
			this.b = A_0.b;
			Stream a_ = (this.c != null) ? new FileStream(this.c.FullName, FileMode.Open, FileAccess.Read) : new MemoryStream(this.b);
			this.a(a_, A_0.Position + A_1, A_2);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004664F File Offset: 0x0004564F
		public override long get_Length()
		{
			return this.e;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00046658 File Offset: 0x00045658
		public byte[] d(int A_0)
		{
			int num = this.b();
			if (A_0 > -1 && A_0 < num)
			{
				num = A_0;
			}
			byte[] array = new byte[num];
			if (num > 0)
			{
				n n = new n(this);
				try
				{
					n.a(array);
				}
				finally
				{
					n.Close();
				}
			}
			return array;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000466AC File Offset: 0x000456AC
		public byte[] d()
		{
			return this.d(-1);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000466B5 File Offset: 0x000456B5
		public override int ReadByte()
		{
			if (this.e - (this.f - this.d) > 0L)
			{
				this.f += 1L;
				return this.a.ReadByte();
			}
			return -1;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000466EC File Offset: 0x000456EC
		public long a(long A_0)
		{
			if (A_0 <= 0L)
			{
				return 0L;
			}
			if (A_0 > (long)this.b())
			{
				A_0 = (long)this.b();
			}
			long num;
			long num3;
			for (num = 0L; num < A_0; num += num3)
			{
				Stream stream = this.a;
				long num2 = stream.Position;
				num2 = stream.Seek(A_0 - num, SeekOrigin.Current) - num2;
				num3 = ((this.a is n) ? ((n)this.a).a(A_0 - num) : num2);
				if (num3 == 0L)
				{
					return 0L;
				}
				this.f += num3;
			}
			return num;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00046773 File Offset: 0x00045773
		public int b()
		{
			return (int)(this.e - (this.f - this.d));
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0004678A File Offset: 0x0004578A
		public override void Close()
		{
			this.a.Close();
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00046798 File Offset: 0x00045798
		public void e(int A_0)
		{
			lock (this)
			{
				this.g = this.f;
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000467DC File Offset: 0x000457DC
		internal void a(byte[] A_0, int A_1, int A_2)
		{
			int num;
			for (int i = 0; i < A_2; i += num)
			{
				num = this.Read(A_0, A_1 + i, A_2 - i);
				if (num < 0)
				{
					throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream, 1001);
				}
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0004681C File Offset: 0x0004581C
		private void a(byte[] A_0)
		{
			this.a(A_0, 0, A_0.Length);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00046829 File Offset: 0x00045829
		public byte a()
		{
			int num = this.ReadByte();
			if (num < 0)
			{
				throw new MailBeeTnefParsingException(Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream, 1001);
			}
			return (byte)num;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004684B File Offset: 0x0004584B
		public ushort f()
		{
			return (ushort)(((int)this.a() | (int)this.a() << 8) & 65535);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00046863 File Offset: 0x00045863
		public uint e()
		{
			return (uint)((long)((int)this.a() | (int)this.a() << 8 | (int)this.a() << 16 | (int)this.a() << 24) & (long)((ulong)-1));
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004688D File Offset: 0x0004588D
		public ulong c()
		{
			return (ulong)(this.e() & uint.MaxValue) | ((ulong)this.e() & (ulong)-1) << 32;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000468A8 File Offset: 0x000458A8
		public byte[] b(int A_0)
		{
			byte[] array = new byte[A_0];
			this.a(array, 0, A_0);
			return array;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000468C6 File Offset: 0x000458C6
		public string a(int A_0)
		{
			return global::a.h.f.c(this.b(A_0), 0, A_0);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000468D6 File Offset: 0x000458D6
		public string c(int A_0)
		{
			return global::a.h.f.b(this.b(A_0), 0, A_0);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000468E8 File Offset: 0x000458E8
		public override string ToString()
		{
			byte[] a_ = null;
			try
			{
				a_ = this.d();
			}
			catch (IOException arg)
			{
				return "RawInputStream can't get bytes: " + arg;
			}
			return global::a.h.f.a(a_, 512);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004692C File Offset: 0x0004592C
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00046934 File Offset: 0x00045934
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.b != null)
			{
				if ((long)count > (long)this.b.Length - this.f)
				{
					count = (int)((long)this.b.Length - this.f);
				}
				if (count > buffer.Length - offset)
				{
					count = buffer.Length - offset;
				}
				if (count != 0)
				{
					Array.Copy(this.b, (int)this.f, buffer, offset, count);
					this.f += (long)count;
					if (this.a != null)
					{
						this.a.Seek((long)count, SeekOrigin.Current);
					}
				}
				return count;
			}
			if ((long)count > this.e + this.d - this.f)
			{
				count = (int)(this.e + this.d - this.f);
			}
			if (count > buffer.Length - offset)
			{
				count = buffer.Length - offset;
			}
			count = this.a.Read(buffer, offset, count);
			this.f += (long)count;
			return count;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00046A1D File Offset: 0x00045A1D
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00046A24 File Offset: 0x00045A24
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00046A28 File Offset: 0x00045A28
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00046A2F File Offset: 0x00045A2F
		public override bool get_CanRead()
		{
			return true;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00046A32 File Offset: 0x00045A32
		public override bool get_CanSeek()
		{
			return true;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00046A35 File Offset: 0x00045A35
		public override bool get_CanWrite()
		{
			return false;
		}

		// Token: 0x04000E3B RID: 3643
		private Stream a;

		// Token: 0x04000E3C RID: 3644
		private byte[] b;

		// Token: 0x04000E3D RID: 3645
		private FileInfo c;

		// Token: 0x04000E3E RID: 3646
		private long d;

		// Token: 0x04000E3F RID: 3647
		private long e;

		// Token: 0x04000E40 RID: 3648
		private long f;

		// Token: 0x04000E41 RID: 3649
		private long g;
	}
}
