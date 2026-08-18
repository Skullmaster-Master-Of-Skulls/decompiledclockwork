using System;
using System.Collections;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000269 RID: 617
	internal class bj : ii
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x000640B4 File Offset: 0x000630B4
		public new virtual ii l()
		{
			this.a();
			if (this.b != null)
			{
				d8 d = this.b.a(this.a, 1);
				if (this.a == this.i())
				{
					return null;
				}
				bh bh = d.a(0).b(26610);
				dx a_ = this.u.f((long)bh.g);
				ii result = ii.a(this.u, a_);
				this.a++;
				return result;
			}
			else
			{
				if (this.c == null)
				{
					return null;
				}
				if (this.a >= this.i())
				{
					return null;
				}
				dx a_2 = this.c.a(this.a);
				ii result2 = ii.a(this.u, a_2);
				this.a++;
				return result2;
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00064178 File Offset: 0x00063178
		public new virtual int e()
		{
			this.b();
			return this.d.a4();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0006418B File Offset: 0x0006318B
		public new virtual int f()
		{
			return this.h(13825);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00064198 File Offset: 0x00063198
		public new virtual int i()
		{
			return this.h(13826);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000641A5 File Offset: 0x000631A5
		public new virtual int k()
		{
			return this.h(13827);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000641B2 File Offset: 0x000631B2
		public new virtual string d()
		{
			return this.d(13843);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000641BF File Offset: 0x000631BF
		public new virtual int m()
		{
			return this.h(13847);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000641CC File Offset: 0x000631CC
		public new virtual int h()
		{
			return this.h(13824);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000641D9 File Offset: 0x000631D9
		internal bj(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x000641E3 File Offset: 0x000631E3
		internal bj(bs A_0, dx A_1, c0 A_2, fb A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000641F0 File Offset: 0x000631F0
		public new x j()
		{
			this.b();
			x x = new x();
			if (this.g())
			{
				try
				{
					if (this.d == null)
					{
						return x;
					}
					foreach (object obj in this.d.a())
					{
						bh bh = ((ew)obj).b(26610);
						bj a_ = (bj)ii.a(this.u, (long)bh.g);
						x.a(a_);
					}
				}
				catch (MailBeePstException ex)
				{
					throw new MailBeePstParsingException(string.Format(Resources.Instance.ErrorDesc_OutlookPstCantGetChildFoldersForFolder01ChildCount23, new object[]
					{
						this.kn(),
						this.fa(),
						this.i(),
						ex.ToString()
					}), 1210);
				}
				return x;
			}
			return x;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00064300 File Offset: 0x00063300
		private new void b()
		{
			if (this.d != null)
			{
				return;
			}
			if (this.g())
			{
				long a_ = (long)(this.w.a + 11);
				try
				{
					dx dx = this.u.f(a_);
					fb a_2 = null;
					if (dx.c > 0L)
					{
						a_2 = this.u.d(dx.c);
					}
					this.d = new ad(new di(this.u, this.u.e(dx.b)), a_2);
				}
				catch (MailBeePstException ex)
				{
				}
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00064398 File Offset: 0x00063398
		private new void a()
		{
			if (this.b != null || this.c != null)
			{
				return;
			}
			if (this.fe() == 3)
			{
				return;
			}
			try
			{
				long a_ = (long)(this.w.a + 12);
				dx dx = this.u.f(a_);
				fb a_2 = null;
				if (dx.c > 0L)
				{
					a_2 = this.u.d(dx.c);
				}
				this.b = new ad(new di(this.u, this.u.e(dx.b)), a_2, 26610);
			}
			catch (Exception)
			{
				dl dl = this.u.d();
				this.c = new h8();
				foreach (object obj in dl.b(this.fd().a))
				{
					dx dx2 = (dx)obj;
					if (ii.a(dx2.a) == 4)
					{
						this.c.a(dx2);
					}
				}
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000644C0 File Offset: 0x000634C0
		public new d6 b(int A_0)
		{
			this.a();
			d6 d = new d6();
			if (this.b != null)
			{
				d8 d2 = this.b.a(this.a, A_0);
				for (int i = 0; i < d2.Count; i++)
				{
					if (this.a >= this.i())
					{
						break;
					}
					bh bh = d2.a(i).b(26610);
					dx a_ = this.u.f((long)bh.g);
					ii a_2 = ii.a(this.u, a_);
					d.a(a_2);
					this.a++;
				}
			}
			else if (this.c != null)
			{
				IEnumerator enumerator = this.c.GetEnumerator();
				enumerator.Reset();
				for (int j = 0; j < this.a; j++)
				{
					enumerator.MoveNext();
				}
				int num = 0;
				while (num < A_0 && this.a < this.i())
				{
					enumerator.MoveNext();
					dx a_3 = (dx)enumerator.Current;
					ii a_4 = ii.a(this.u, a_3);
					d.a(a_4);
					this.a++;
					num++;
				}
			}
			return d;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00064600 File Offset: 0x00063600
		public new cj c()
		{
			this.a();
			if (this.b == null)
			{
				return new cj();
			}
			cj cj = new cj();
			foreach (object obj in this.b.a())
			{
				ew ew = (ew)obj;
				if (this.a == this.i())
				{
					break;
				}
				bh bh = ew.b(26610);
				if (bh.g == 0)
				{
					break;
				}
				cj.a((long)bh.g);
			}
			return cj;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000646A8 File Offset: 0x000636A8
		public new virtual void a(int A_0)
		{
			this.a();
			if (A_0 < 1)
			{
				this.a = 0;
				return;
			}
			if (A_0 > this.i())
			{
				A_0 = this.i();
			}
			this.a = A_0;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000646D4 File Offset: 0x000636D4
		public new virtual bool g()
		{
			return true;
		}

		// Token: 0x040010A3 RID: 4259
		private new int a;

		// Token: 0x040010A4 RID: 4260
		private new ad b;

		// Token: 0x040010A5 RID: 4261
		private new h8 c;

		// Token: 0x040010A6 RID: 4262
		private new ad d;
	}
}
