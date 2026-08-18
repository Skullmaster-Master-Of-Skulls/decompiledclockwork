using System;

namespace a.b
{
	// Token: 0x02000340 RID: 832
	internal class gd : ja
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x00081B04 File Offset: 0x00080B04
		public string k6()
		{
			return this.b;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00081B0C File Offset: 0x00080B0C
		public void k7(string A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00081B15 File Offset: 0x00080B15
		public string k8()
		{
			return this.c;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00081B1D File Offset: 0x00080B1D
		public void k9(string A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00081B26 File Offset: 0x00080B26
		public string la()
		{
			return this.d;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00081B2E File Offset: 0x00080B2E
		public void lb(string A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00081B37 File Offset: 0x00080B37
		public string lc()
		{
			return this.e;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00081B3F File Offset: 0x00080B3F
		public void ld(string A_0)
		{
			this.e = A_0;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00081B48 File Offset: 0x00080B48
		public bool le()
		{
			return this.Equals(gd.a);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00081B55 File Offset: 0x00080B55
		public sealed override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.a(obj));
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00081B7C File Offset: 0x00080B7C
		public sealed override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.a());
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00081B94 File Offset: 0x00080B94
		private bool a(object A_0)
		{
			gd gd = A_0 as gd;
			return gd != null && string.Equals(this.b, gd.b) && string.Equals(this.c, gd.c) && string.Equals(this.d, gd.d) && string.Equals(this.e, gd.e);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00081BF7 File Offset: 0x00080BF7
		private int a()
		{
			return f3.a(f3.a(f3.a(this.b.GetHashCode(), this.c), this.d), this.e);
		}

		// Token: 0x040013CC RID: 5068
		public static gd a = new gd();

		// Token: 0x040013CD RID: 5069
		private string b;

		// Token: 0x040013CE RID: 5070
		private string c;

		// Token: 0x040013CF RID: 5071
		private string d;

		// Token: 0x040013D0 RID: 5072
		private string e;
	}
}
