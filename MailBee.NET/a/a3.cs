using System;

namespace a
{
	// Token: 0x020004C8 RID: 1224
	internal class a3
	{
		// Token: 0x060029AA RID: 10666 RVA: 0x000C1703 File Offset: 0x000C0703
		public a3(be A_0)
		{
			this.a = new ah();
			this.a.f = this;
			this.b = A_0;
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000C172C File Offset: 0x000C072C
		public a8 c(e A_0)
		{
			a8 e = this.a;
			while (e != null && e.a6() != A_0)
			{
				e = e.e;
			}
			return e;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000C1756 File Offset: 0x000C0756
		public bool a()
		{
			return this.a == null;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000C1761 File Offset: 0x000C0761
		public a8 b()
		{
			return this.a;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000C176C File Offset: 0x000C076C
		public ah e()
		{
			a8 a = this.f();
			if (a != null && a.a6() == global::a.e.a)
			{
				return (ah)a;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000C1797 File Offset: 0x000C0797
		public g d()
		{
			if (this.a.a6() == global::a.e.d)
			{
				return (g)this.a;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000C17B8 File Offset: 0x000C07B8
		public bool e(a8 A_0)
		{
			return A_0 == this.a;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000C17C4 File Offset: 0x000C07C4
		public a8 f()
		{
			a8 e = this.a;
			if (e == null)
			{
				return null;
			}
			while (e.e != null)
			{
				e = e.e;
			}
			return e;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000C17F0 File Offset: 0x000C07F0
		public a8 c(a8 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			if (this.a == null)
			{
				return null;
			}
			a8 e = this.a;
			if (e == A_0)
			{
				return null;
			}
			while (e != null)
			{
				if (e.e == A_0)
				{
					return e;
				}
				e = e.e;
			}
			throw new ArgumentException();
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000C1837 File Offset: 0x000C0837
		public a8 b(e A_0)
		{
			return this.c(this.c(A_0));
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000C1846 File Offset: 0x000C0846
		public void b(a8 A_0)
		{
			A_0.e = this.a;
			this.a = A_0;
			this.a.f = this;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000C1868 File Offset: 0x000C0868
		public void d(a8 A_0)
		{
			for (a8 e = this.a; e != null; e = e.e)
			{
				if (e.a6() < A_0.a6())
				{
					this.a(A_0, e);
					return;
				}
			}
			this.b(A_0);
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000C18A6 File Offset: 0x000C08A6
		public void a(a8 A_0, e A_1)
		{
			this.a(A_0, this.c(A_1));
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000C18B8 File Offset: 0x000C08B8
		public void a(a8 A_0, a8 A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			A_0.e = A_1;
			A_0.f = this;
			a8 a;
			if (A_1 == null)
			{
				a = this.f();
			}
			else
			{
				a = this.c(A_1);
			}
			if (a != null)
			{
				a.e = A_0;
				return;
			}
			this.a = A_0;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000C1902 File Offset: 0x000C0902
		public a8 a(e A_0)
		{
			return this.a(this.c(A_0));
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000C1914 File Offset: 0x000C0914
		public a8 a(a8 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			a8 a = this.c(A_0);
			if (a == null)
			{
				this.a = A_0.e;
			}
			else
			{
				a.e = A_0.e;
			}
			A_0.f = null;
			return A_0;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000C1957 File Offset: 0x000C0957
		public be c()
		{
			return this.b;
		}

		// Token: 0x04001C59 RID: 7257
		private a8 a;

		// Token: 0x04001C5A RID: 7258
		private be b;
	}
}
