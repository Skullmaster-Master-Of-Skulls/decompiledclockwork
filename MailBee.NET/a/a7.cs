using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace a
{
	// Token: 0x020004A6 RID: 1190
	internal abstract class a7 : a8
	{
		// Token: 0x06002882 RID: 10370 RVA: 0x000BCE94 File Offset: 0x000BBE94
		public a7()
		{
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000BCE9C File Offset: 0x000BBE9C
		public override IPHostEntry hk(string A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hk(A_0);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000BCEB8 File Offset: 0x000BBEB8
		public override void d1(IPEndPoint A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.d1(A_0);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000BCED4 File Offset: 0x000BBED4
		public override void d2()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.d2();
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000BCEEF File Offset: 0x000BBEEF
		public override bool hl()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hl();
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000BCF0A File Offset: 0x000BBF0A
		public override int d4(byte[] A_0, int A_1, int A_2)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.d4(A_0, A_1, A_2);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000BCF28 File Offset: 0x000BBF28
		public override int d3(byte[] A_0, int A_1)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.d3(A_0, A_1);
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000BCF45 File Offset: 0x000BBF45
		public override bool hm(int A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hm(A_0);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000BCF61 File Offset: 0x000BBF61
		public override bool hn()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hn();
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000BCF7C File Offset: 0x000BBF7C
		public override void ho(IPEndPoint A_0)
		{
			this.e.ho(A_0);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000BCF8A File Offset: 0x000BBF8A
		public override int hp(byte[] A_0, int A_1, int A_2)
		{
			return this.e.hp(A_0, A_1, A_2);
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000BCF9A File Offset: 0x000BBF9A
		public override int hq(byte[] A_0, int A_1)
		{
			return this.e.hq(A_0, A_1);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000BCFA9 File Offset: 0x000BBFA9
		public override void hr()
		{
			this.e.hr();
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000BCFB6 File Offset: 0x000BBFB6
		public override ai hs()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hs();
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000BCFD1 File Offset: 0x000BBFD1
		public override Socket ht()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.ht();
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000BCFEC File Offset: 0x000BBFEC
		public override void hu(Socket A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.hu(A_0);
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000BD008 File Offset: 0x000BC008
		public override Stream d0()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.d0();
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000BD023 File Offset: 0x000BC023
		public override void hv(Stream A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.hv(A_0);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000BD03F File Offset: 0x000BC03F
		public override EndPoint hw()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hw();
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000BD05A File Offset: 0x000BC05A
		public override void hx(EndPoint A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.hx(A_0);
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000BD076 File Offset: 0x000BC076
		public override int hy()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.hy();
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000BD091 File Offset: 0x000BC091
		public override void hz(int A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.hz(A_0);
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000BD0AD File Offset: 0x000BC0AD
		public override m h2()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.h2();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000BD0C8 File Offset: 0x000BC0C8
		public override void h3(m A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.h3(A_0);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000BD0E4 File Offset: 0x000BC0E4
		public override a1 a8()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.a8();
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000BD0FF File Offset: 0x000BC0FF
		public override void a9(a1 A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.a9(A_0);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000BD11B File Offset: 0x000BC11B
		public override bd ba()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.ba();
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000BD136 File Offset: 0x000BC136
		public override void bb(bd A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.bb(A_0);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000BD152 File Offset: 0x000BC152
		public override global::a.a h0()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.h0();
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000BD16D File Offset: 0x000BC16D
		public override void h1(global::a.a A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.h1(A_0);
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000BD189 File Offset: 0x000BC189
		public override ak h4()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.h4();
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000BD1A4 File Offset: 0x000BC1A4
		public override void h5(ak A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.h5(A_0);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000BD1C0 File Offset: 0x000BC1C0
		public override bl h6()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.h6();
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x000BD1DB File Offset: 0x000BC1DB
		public override void h7(bl A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			this.e.h7(A_0);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x000BD1F7 File Offset: 0x000BC1F7
		public override Task<IPHostEntry> h8(string A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.h8(A_0);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x000BD213 File Offset: 0x000BC213
		public override Task d5(IPEndPoint A_0)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.d5(A_0);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x000BD22F File Offset: 0x000BC22F
		public override Task<int> d7(byte[] A_0, int A_1, int A_2)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.d7(A_0, A_1, A_2);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x000BD24D File Offset: 0x000BC24D
		public override Task<int> d6(byte[] A_0, int A_1)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.d6(A_0, A_1);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x000BD26A File Offset: 0x000BC26A
		public override Task<bool> h9(int A_0, bool A_1)
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.h9(A_0, A_1);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000BD287 File Offset: 0x000BC287
		public override Task<bool> ia()
		{
			if (this.e == null)
			{
				throw new InvalidOperationException();
			}
			return this.e.ia();
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000BD2A2 File Offset: 0x000BC2A2
		public override Task ib(IPEndPoint A_0)
		{
			return this.e.ib(A_0);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x000BD2B0 File Offset: 0x000BC2B0
		public override Task<int> ic(byte[] A_0, int A_1, int A_2)
		{
			return this.e.ic(A_0, A_1, A_2);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x000BD2C0 File Offset: 0x000BC2C0
		public override Task<int> id(byte[] A_0, int A_1)
		{
			return this.e.id(A_0, A_1);
		}
	}
}
