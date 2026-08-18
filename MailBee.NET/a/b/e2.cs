using System;

namespace a.b
{
	// Token: 0x0200027B RID: 635
	internal class e2 : hy
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00067B91 File Offset: 0x00066B91
		public new void a(bool A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00067B9A File Offset: 0x00066B9A
		public new bool b()
		{
			return this.a;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00067BA2 File Offset: 0x00066BA2
		public override string ToString()
		{
			return string.Format("Table Item: {0}\n", base.ToString());
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00067BB4 File Offset: 0x00066BB4
		public new string a()
		{
			return base.ToString();
		}

		// Token: 0x040010E0 RID: 4320
		private new bool a;
	}
}
