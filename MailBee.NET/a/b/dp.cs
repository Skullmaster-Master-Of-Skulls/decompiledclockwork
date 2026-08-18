using System;
using System.Collections.Generic;

namespace a.b
{
	// Token: 0x020002D4 RID: 724
	internal class dp : e9
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x0007102E File Offset: 0x0007002E
		public dp(h0 A_0, hj A_1, List<gx> A_2, c3 A_3)
		{
			this.a = A_0;
			this.c = A_2;
			this.d = A_3;
			this.e = A_1;
			this.b = new ga(A_0, A_1.i());
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00071068 File Offset: 0x00070068
		public override he ie(int A_0)
		{
			int num = A_0 * 64;
			int num2 = num / this.a.l();
			int num3 = num % this.a.l();
			cc cc = this.b.a() as cc;
			for (int i = 0; i < num2; i++)
			{
				cc.a();
			}
			he he = cc.a();
			if (he == null)
			{
				throw new IndexOutOfRangeException("Big block " + num2 + " outside stream");
			}
			he.b(he.g() + num3);
			he he2 = he.b();
			he2.e(64);
			return he2;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000710F8 File Offset: 0x000700F8
		public override he @if(int A_0)
		{
			he result;
			try
			{
				result = this.ie(A_0);
			}
			catch (IndexOutOfRangeException)
			{
				int num = this.a.ij();
				this.a.@if(num);
				d7 d = this.a.ik();
				int a_ = this.b.b();
				for (;;)
				{
					d.a(a_);
					int num2 = this.a.ih(a_);
					if (num2 == -2)
					{
						break;
					}
					a_ = num2;
				}
				this.a.ii(a_, num);
				this.a.ii(num, -2);
				result = this.@if(A_0);
			}
			return result;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00071198 File Offset: 0x00070198
		public override ct ig(int A_0)
		{
			return gx.a(A_0, this.d, this.c);
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000711AC File Offset: 0x000701AC
		public override int ih(int A_0)
		{
			ct ct = this.ig(A_0);
			return ct.a().e(ct.b());
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000711D4 File Offset: 0x000701D4
		public override void ii(int A_0, int A_1)
		{
			ct ct = this.ig(A_0);
			ct.a().a(ct.b(), A_1);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000711FC File Offset: 0x000701FC
		public override int ij()
		{
			int num = this.a.f().e();
			int num2 = 0;
			for (int i = 0; i < this.c.Count; i++)
			{
				gx gx = this.c[i];
				if (gx.g())
				{
					for (int j = 0; j < num; j++)
					{
						if (gx.e(j) == -1)
						{
							return num2 + j;
						}
					}
				}
				num2 += num;
			}
			gx gx2 = gx.a(this.a.f(), false);
			int num3 = this.a.ij();
			gx2.d(num3);
			if (this.d.c() == 0)
			{
				this.d.h(num3);
				this.d.e(1);
			}
			else
			{
				d7 d = this.a.ik();
				int a_ = this.d.e();
				for (;;)
				{
					d.a(a_);
					int num4 = this.a.ih(a_);
					if (num4 == -2)
					{
						break;
					}
					a_ = num4;
				}
				this.a.ii(a_, num3);
				this.d.e(this.d.c() + 1);
			}
			this.a.ii(num3, -2);
			this.c.Add(gx2);
			return num2;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0007133E File Offset: 0x0007033E
		public override d7 ik()
		{
			return new d7((long)this.e.h(), this);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00071352 File Offset: 0x00070352
		public override int il()
		{
			return 64;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00071358 File Offset: 0x00070358
		public void a()
		{
			foreach (gx gx in this.c)
			{
				he a_ = this.a.ie(gx.f());
				ib.a(gx, a_);
			}
		}

		// Token: 0x04001268 RID: 4712
		private h0 a;

		// Token: 0x04001269 RID: 4713
		private ga b;

		// Token: 0x0400126A RID: 4714
		private List<gx> c;

		// Token: 0x0400126B RID: 4715
		private c3 d;

		// Token: 0x0400126C RID: 4716
		private hj e;
	}
}
