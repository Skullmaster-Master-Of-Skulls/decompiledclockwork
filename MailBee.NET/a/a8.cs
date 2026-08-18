using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailBee;

namespace a
{
	// Token: 0x020004AA RID: 1194
	internal abstract class a8
	{
		// Token: 0x06002907 RID: 10503 RVA: 0x000BE8F6 File Offset: 0x000BD8F6
		public a8()
		{
			this.e = null;
			this.f = null;
		}

		// Token: 0x06002908 RID: 10504
		public abstract IPHostEntry hk(string A_0);

		// Token: 0x06002909 RID: 10505 RVA: 0x000BE90C File Offset: 0x000BD90C
		public void g(IPHostEntry A_0, int A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			if (Global.PreferIPv4Hosts)
			{
				Array.Sort<IPAddress>(A_0.AddressList, global::a.a8.a.a());
			}
			int i = 0;
			while (i < A_0.AddressList.Length)
			{
				IPEndPoint a_ = new IPEndPoint(A_0.AddressList[i], A_1);
				if (i < A_0.AddressList.Length - 1)
				{
					try
					{
						this.d1(a_);
						return;
					}
					catch (a2)
					{
						goto IL_63;
					}
					catch (MailBeeConnectionException)
					{
						this.d2();
						goto IL_63;
					}
					goto IL_55;
				}
				goto IL_55;
				IL_63:
				i++;
				continue;
				IL_55:
				try
				{
					this.d1(a_);
				}
				catch (a2)
				{
					goto IL_63;
				}
				return;
			}
			throw new MailBeeNoIP4HostFoundException(51, A_0, A_1);
		}

		// Token: 0x0600290A RID: 10506
		public abstract void d1(IPEndPoint A_0);

		// Token: 0x0600290B RID: 10507
		public abstract void d2();

		// Token: 0x0600290C RID: 10508
		public abstract bool hl();

		// Token: 0x0600290D RID: 10509 RVA: 0x000BE9BC File Offset: 0x000BD9BC
		public int i(byte[] A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			return this.d4(A_0, 0, A_0.Length);
		}

		// Token: 0x0600290E RID: 10510
		public abstract int d4(byte[] A_0, int A_1, int A_2);

		// Token: 0x0600290F RID: 10511 RVA: 0x000BE9D2 File Offset: 0x000BD9D2
		public int j(byte[] A_0)
		{
			return this.d3(A_0, 0);
		}

		// Token: 0x06002910 RID: 10512
		public abstract int d3(byte[] A_0, int A_1);

		// Token: 0x06002911 RID: 10513
		public abstract bool hm(int A_0);

		// Token: 0x06002912 RID: 10514
		public abstract bool hn();

		// Token: 0x06002913 RID: 10515
		public abstract void ho(IPEndPoint A_0);

		// Token: 0x06002914 RID: 10516 RVA: 0x000BE9DC File Offset: 0x000BD9DC
		public int l(byte[] A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			return this.hp(A_0, 0, A_0.Length);
		}

		// Token: 0x06002915 RID: 10517
		public abstract int hp(byte[] A_0, int A_1, int A_2);

		// Token: 0x06002916 RID: 10518 RVA: 0x000BE9F2 File Offset: 0x000BD9F2
		public int n(byte[] A_0)
		{
			return this.d3(A_0, 0);
		}

		// Token: 0x06002917 RID: 10519
		public abstract int hq(byte[] A_0, int A_1);

		// Token: 0x06002918 RID: 10520
		public abstract void hr();

		// Token: 0x06002919 RID: 10521
		public abstract Socket ht();

		// Token: 0x0600291A RID: 10522
		public abstract void hu(Socket A_0);

		// Token: 0x0600291B RID: 10523
		public abstract Stream d0();

		// Token: 0x0600291C RID: 10524
		public abstract void hv(Stream A_0);

		// Token: 0x0600291D RID: 10525
		public abstract EndPoint hw();

		// Token: 0x0600291E RID: 10526
		public abstract void hx(EndPoint A_0);

		// Token: 0x0600291F RID: 10527
		public abstract int hy();

		// Token: 0x06002920 RID: 10528
		public abstract void hz(int A_0);

		// Token: 0x06002921 RID: 10529
		public abstract ai hs();

		// Token: 0x06002922 RID: 10530
		public abstract global::a.a h0();

		// Token: 0x06002923 RID: 10531
		public abstract void h1(global::a.a A_0);

		// Token: 0x06002924 RID: 10532
		public abstract m h2();

		// Token: 0x06002925 RID: 10533
		public abstract void h3(m A_0);

		// Token: 0x06002926 RID: 10534
		public abstract a1 a8();

		// Token: 0x06002927 RID: 10535
		public abstract void a9(a1 A_0);

		// Token: 0x06002928 RID: 10536
		public abstract bd ba();

		// Token: 0x06002929 RID: 10537
		public abstract void bb(bd A_0);

		// Token: 0x0600292A RID: 10538
		public abstract e a6();

		// Token: 0x0600292B RID: 10539
		public abstract bool a7();

		// Token: 0x0600292C RID: 10540
		public abstract ak h4();

		// Token: 0x0600292D RID: 10541
		public abstract void h5(ak A_0);

		// Token: 0x0600292E RID: 10542
		public abstract bl h6();

		// Token: 0x0600292F RID: 10543
		public abstract void h7(bl A_0);

		// Token: 0x06002930 RID: 10544
		public abstract Task<IPHostEntry> h8(string A_0);

		// Token: 0x06002931 RID: 10545 RVA: 0x000BE9FC File Offset: 0x000BD9FC
		public Task h(IPHostEntry A_0, int A_1)
		{
			a8.b b;
			b.f = this;
			b.c = A_0;
			b.d = A_1;
			b.b = AsyncTaskMethodBuilder.Create();
			b.a = -1;
			AsyncTaskMethodBuilder b2 = b.b;
			b2.Start<a8.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06002932 RID: 10546
		public abstract Task d5(IPEndPoint A_0);

		// Token: 0x06002933 RID: 10547 RVA: 0x000BEA51 File Offset: 0x000BDA51
		public Task<int> k(byte[] A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			return this.d7(A_0, 0, A_0.Length);
		}

		// Token: 0x06002934 RID: 10548
		public abstract Task<int> d7(byte[] A_0, int A_1, int A_2);

		// Token: 0x06002935 RID: 10549 RVA: 0x000BEA67 File Offset: 0x000BDA67
		public Task<int> g(byte[] A_0)
		{
			return this.d6(A_0, 0);
		}

		// Token: 0x06002936 RID: 10550
		public abstract Task<int> d6(byte[] A_0, int A_1);

		// Token: 0x06002937 RID: 10551
		public abstract Task<bool> h9(int A_0, bool A_1);

		// Token: 0x06002938 RID: 10552
		public abstract Task<bool> ia();

		// Token: 0x06002939 RID: 10553
		public abstract Task ib(IPEndPoint A_0);

		// Token: 0x0600293A RID: 10554 RVA: 0x000BEA71 File Offset: 0x000BDA71
		public Task<int> h(byte[] A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			return this.ic(A_0, 0, A_0.Length);
		}

		// Token: 0x0600293B RID: 10555
		public abstract Task<int> ic(byte[] A_0, int A_1, int A_2);

		// Token: 0x0600293C RID: 10556 RVA: 0x000BEA87 File Offset: 0x000BDA87
		public Task<int> m(byte[] A_0)
		{
			return this.d6(A_0, 0);
		}

		// Token: 0x0600293D RID: 10557
		public abstract Task<int> id(byte[] A_0, int A_1);

		// Token: 0x04001BD6 RID: 7126
		public a8 e;

		// Token: 0x04001BD7 RID: 7127
		public a3 f;

		// Token: 0x020004CF RID: 1231
		private class a : IComparer<IPAddress>
		{
			// Token: 0x060029CF RID: 10703 RVA: 0x000C1984 File Offset: 0x000C0984
			private a()
			{
			}

			// Token: 0x060029D0 RID: 10704 RVA: 0x000C198C File Offset: 0x000C098C
			public int Compare(IPAddress x, IPAddress y)
			{
				if (x.AddressFamily == AddressFamily.InterNetwork && y.AddressFamily != AddressFamily.InterNetwork)
				{
					return -1;
				}
				if (x.AddressFamily != AddressFamily.InterNetwork && y.AddressFamily == AddressFamily.InterNetwork)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x060029D1 RID: 10705 RVA: 0x000C19B7 File Offset: 0x000C09B7
			public static a8.a a()
			{
				return global::a.a8.a.a;
			}

			// Token: 0x04001C61 RID: 7265
			private static a8.a a = new a8.a();
		}
	}
}
