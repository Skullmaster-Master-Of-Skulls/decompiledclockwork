using System;

namespace a.b
{
	// Token: 0x02000381 RID: 897
	internal sealed class ea : hs, c8
	{
		// Token: 0x060020A4 RID: 8356 RVA: 0x0008783D File Offset: 0x0008683D
		public ea(string A_0, ej A_1) : base(hu.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("text");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("format");
			}
			this.a = A_0;
			this.b = A_1;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00087870 File Offset: 0x00086870
		protected override void dw(gq A_0)
		{
			A_0.iq(this);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00087879 File Offset: 0x00086879
		public string jg()
		{
			return this.a;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00087881 File Offset: 0x00086881
		public ej jh()
		{
			return this.b;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0008788C File Offset: 0x0008688C
		protected override bool dx(object A_0)
		{
			ea ea = A_0 as ea;
			return ea != null && base.dx(ea) && this.a.Equals(ea.a) && this.b.Equals(ea.b);
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x000878D2 File Offset: 0x000868D2
		protected override int dy()
		{
			return f3.a(f3.a(base.dy(), this.a), this.b);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000878F0 File Offset: 0x000868F0
		public override string ToString()
		{
			return "'" + this.a + "'";
		}

		// Token: 0x04001495 RID: 5269
		private readonly string a;

		// Token: 0x04001496 RID: 5270
		private readonly ej b;
	}
}
