using System;

namespace a.f
{
	// Token: 0x020000E6 RID: 230
	internal class v : bf
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x00022B82 File Offset: 0x00021B82
		public v(bool A_0, bool A_1, bool A_2, string A_3, bool A_4, bool A_5) : base(A_0)
		{
			this.e = A_2;
			this.d = A_1;
			this.f = A_3;
			this.l = A_4;
			this.m = A_5;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00022BB1 File Offset: 0x00021BB1
		public v(bool A_0, bool A_1, bool A_2, string A_3) : this(A_0, A_1, A_2, A_3, false, false)
		{
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00022BC0 File Offset: 0x00021BC0
		public v(bool A_0, bool A_1) : this(A_0, A_1, true, null)
		{
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00022BCC File Offset: 0x00021BCC
		public v(bool A_0, string A_1) : this(A_0, true, true, A_1)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00022BD8 File Offset: 0x00021BD8
		public v(bool A_0) : this(A_0, true)
		{
		}

		// Token: 0x040004FE RID: 1278
		public bool d;

		// Token: 0x040004FF RID: 1279
		public bool e;

		// Token: 0x04000500 RID: 1280
		public string f;
	}
}
